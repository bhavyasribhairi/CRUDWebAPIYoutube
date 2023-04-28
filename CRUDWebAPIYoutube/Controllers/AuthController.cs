using CRUDWebAPIYoutube.Models.DTO;
using CRUDWebAPIYoutube.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebAPIYoutube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }



        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName,


            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    //add roles if user is addedd successfully
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                }


            }
            return BadRequest("something went wrong");

        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDTO loginUserDTO)
        {
            var user = await userManager.FindByEmailAsync(loginUserDTO.UserName);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken= tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO()
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                    return BadRequest("Roles are not assigned to this user");

                }
                return BadRequest("Password is not correct");

            }
            return BadRequest("Please register! user not found");

        }

    }
}
