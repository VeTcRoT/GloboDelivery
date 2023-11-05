using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GloboDelivery.IdentityServer.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(Configuration.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(Configuration.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Configuration.Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Configuration.Customer)).GetAwaiter().GetResult();
            }
            else
                return;

            var adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, Configuration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Role, Configuration.Admin)
            }).Result;

            var managerUser = new IdentityUser
            {
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111"
            };

            _userManager.CreateAsync(managerUser, "Manager123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(managerUser, Configuration.Manager).GetAwaiter().GetResult();

            var managerClaims = _userManager.AddClaimsAsync(managerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Role, Configuration.Manager)
            }).Result;

            var customerUser = new IdentityUser
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111"
            };

            _userManager.CreateAsync(customerUser, "Customer123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, Configuration.Customer).GetAwaiter().GetResult();

            var customerClaims = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Role, Configuration.Customer)
            }).Result;
        }
    }
}
