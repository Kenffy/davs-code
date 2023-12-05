using Authenticate;
using Authenticate.Data;
using Authenticate.Models;
using Authenticate.Services.IServices;
using Authenticate.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppIdentityDbContext>(option =>
{
    //option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));
});
builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection("ApiConfigurations"));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(MappingHelper));

var SecretKey = builder.Configuration.GetValue<string>("ApiConfigurations:SecretKey");
var Issuer = builder.Configuration.GetValue<string>("ApiConfigurations:Issuer");
var Audience = builder.Configuration.GetValue<string>("ApiConfigurations:Audience");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidAudience = Audience,
        ValidateAudience = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Description = "Enter the Bearer Authorization string as following: `Bearer {Token}`",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeWithDavs Auth API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeWithDavs Auth API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



// ######################### Create and init database ###########################
// Initialize and create database by startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<AppIdentityDbContext>();
    try
    {
        await context.Database.MigrateAsync();
        await AppIdentitySeedDbContext.SeedUsersAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

// ###############################################################################

app.Run();
