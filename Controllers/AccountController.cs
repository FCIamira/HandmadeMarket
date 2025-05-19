using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly HandmadeContext handmadeContext;
      

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config,HandmadeContext handmadeContext)
        {
            this.userManager = userManager;
            this.config = config;
            this.handmadeContext = handmadeContext;
         
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO userFromConsumer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userFromConsumer.UserName,
                    Email = userFromConsumer.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, userFromConsumer.Password);
                if (result.Succeeded)
                {
                    // Assign Roles 
                    if (!string.IsNullOrEmpty(userFromConsumer.Role))
                    {
                        await userManager.AddToRoleAsync(user, userFromConsumer.Role);
                      // Assign Customer Role 
                        if (userFromConsumer.Role == "Customer")
                        {
                            Customer customer = new Customer
                            {
                                UserId = user.Id,
                                FirstName = userFromConsumer.FirstName,  
                                LastName = userFromConsumer.LastName,
                                Email = userFromConsumer.Email,
                                Phone = userFromConsumer.Phone,
                                Address = userFromConsumer.Address,
                                Password = userFromConsumer.Password
                            };

                            handmadeContext.Customers.Add(customer);
                            await handmadeContext.SaveChangesAsync();
                        }
                        // Assign Seller Role 
                        if (userFromConsumer.Role == "Seller")
                        {
                            Seller seller = new Seller
                            {
                                UserId = user.Id,
                                 storeName = userFromConsumer.UserName,
                                email = userFromConsumer.Email,
                               phoneNumber = userFromConsumer.Phone,
                                createdAt = DateTime.Now,
                               
                            };

                            handmadeContext.Sellers.Add(seller);
                            await handmadeContext.SaveChangesAsync();
                        }

                    }

                    return Ok("Account Created & Role Assigned");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO userFromConsumer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userFromConsumer.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userFromConsumer.Password);
                    if (found)
                    {
                        #region Create Token
                        string jti = Guid.NewGuid().ToString();

                        var userRoles = await userManager.GetRolesAsync(user);

                        List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, jti)
                };

                        if (userRoles != null)
                        {
                            foreach (var role in userRoles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }

                        //-----------------------------------------------

                        SymmetricSecurityKey signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));

                        SigningCredentials signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:Iss"],
                            audience: config["JWT:Aud"],
                            expires: DateTime.Now.AddHours(1), 
                            claims: claims,
                            signingCredentials: signingCredentials
                        );
                        return Ok(new
                        {
                            expired = DateTime.Now.AddHours(1), 
                            token = new JwtSecurityTokenHandler().WriteToken(myToken) 
                        });
                        #endregion
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }

    }
}
