using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Parky_project.BL.Dtos.Identity;
using Parky_project.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace parky_project.API.Controllers
{
    

    
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public IdentityController(UserManager<User> userManager,IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        #region admin register
        [HttpPost]
        [Route("AdminRegister")]
        public async Task<ActionResult> Register(CustomerRegisterDto customerDto)
        {
            var user = new User
            {
                UserName = customerDto.UserName,
                Email = customerDto.Email,
                address= customerDto.address
               
            };
            var result = await _userManager.CreateAsync(user, customerDto.Password);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),

                    new Claim(ClaimTypes.Role,"Admin")
                };
                await _userManager.AddClaimsAsync(user, claims);

                return Ok("You Are Registered");
            }
            else
            {
                return BadRequest("Try Again !!");
            }
        }
        #endregion

        #region Admin Login

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto userLoginning)
        {
            var user = await _userManager.FindByNameAsync(userLoginning.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {

                var AuthenticatedUser = await _userManager.CheckPasswordAsync(user, userLoginning.Password);
                if (!AuthenticatedUser)
                {
                    return BadRequest("your loginning falid!!");
                }
                else
                {
                    #region claims
                    var loginClaims = await _userManager.GetClaimsAsync(user);
                    #endregion

                    #region secretKEY
                    var key = _config.GetValue<string>("secretKey");
                    //conver key to byte
                    var keyAsByte = Encoding.ASCII.GetBytes(key);
                    //convert key byte to object
                    var keyObject = new SymmetricSecurityKey(keyAsByte);
                    #endregion

                    #region signing creditional
                    var signingCreditional = new SigningCredentials(keyObject, SecurityAlgorithms.Aes128CbcHmacSha256);

                    #endregion

                    #region compination to get token
                    var expiring = DateTime.Now.AddDays(2);
                    var token = new JwtSecurityToken
                        (
                        signingCredentials: signingCreditional,
                        claims: loginClaims,
                        expires: expiring
                        );

                    #endregion

                    #region return token
                    //convert token as string
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var returningToken = tokenHandler.WriteToken(token);

                    return new TokenDto(returningToken, expiring);
                    #endregion

                }
            }
        }
        #endregion
    }
}
