using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterAPI.Entities;
using TwitterAPI.Models;
using TwitterAPI.Repositories;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        //Register User
        [HttpPost, Route("Register")]
        [AllowAnonymous] 
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userRepository.Register(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user.");
            }
        }
        [HttpPost]
        [Route("Validate")]
        
        public async Task<IActionResult> ValidUser(Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userRepository.ValidUser(login.Email, login.Password);
                if (user == null)
                    return Unauthorized();

                var authResponse = new AuthResponse
                {
                    UserId = user.UserId,
                    Role = user.Role,
                    Token = GetToken(user),
                };

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while validating the user.");
            }

        }

        [HttpGet, Route("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "An error occurred while retrieving the tweets.");
            }
        }
        [HttpPut, Route("EditProfile")]
        //[Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> Edit([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userRepository.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the user profile.");
            }
        }

        [HttpGet, Route("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete, Route("DeleteProfile/{id}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the user profile.");
            }
        }
        private string GetToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            //generate token using tokenDescription
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
        
        
    }
}
