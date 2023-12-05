using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orders;
using Orders.Data;
using Orders.Repository;
using Orders.Repository.IRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StoreDbContext>(option =>
{
    //option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
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
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeWithDavs Order API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeWithDavs Order API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
