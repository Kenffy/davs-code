using Authenticate.Extensions;
using Authenticate.Models;
using Authenticate.Models.Dto;
using Authenticate.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        protected ResponseDto _response;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _response = new ResponseDto();
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<ResponseDto>> GetCurrentUser()
        {
            var result = await _userManager.FindUserByEmailFromClaimsPrincipal(User);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserDto>(result);
                return Ok(_response);
            }
        }

        [HttpGet("id:string")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetUserById(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserDto>(result);
                return Ok(_response);
            }
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> GetUsers()
        {
            var results = await _userManager.Users.ToListAsync();
            if(results == null)
            {
                _response.IsSuccess = false;
                _response.Result = null;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<List<UserDto>>(results);
                return Ok(_response);
            }
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("An error occured while updating the user address");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                _response.IsSuccess = false;
                _response.Message = "User not found";
                return BadRequest(_response);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                _response.IsSuccess = false;
                _response.Message = "Wrong email or password!";
                return BadRequest(_response);
            }

            _response.Result = new AuthUserDto
            {
                Email = user.Email,
                Token = _tokenService.createToken(user, roles),
                DisplayName = user.DisplayName,
            };
            return Ok(_response);

        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                _response.IsSuccess = false;
                _response.Message = "Something went wrong!";
                return BadRequest(_response);
            }

            _response.Message = "User successfully created.";
            return Ok(_response);
        }
    }
}