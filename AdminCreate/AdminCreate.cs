using Microsoft.AspNetCore.Identity;
using PermissionSystem.Data;
using PermissionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionSystem.AdminCreate
{
    public class AdminCreate
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminCreate(RoleManager<IdentityRole> roleManager, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }


        public async Task<bool> CreateAdminRole()
        {
            if (IsAdminRoleExist())
            {
                return true;
            }

            var role = new IdentityRole
            {
                Name = RoleName.Admin
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        private bool IsAdminRoleExist()
        {
            var result = _context.Roles.FirstOrDefault(r=>r.Name == RoleName.Admin);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateAdminUser()
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == "AdminUser");
            
            if (userInDb != null)
            {
                return true;
            }

            var user = new ApplicationUser
            {
                Name = "AdminUser",
                UserName = "AdminUser",
                ResetPasswordRequired = false,
                Email = "AdminUser@admin.com",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "Zaq@1234");
            if (result.Succeeded)
            {
                // add role "Admin" to user
                var roleResult = await _userManager.AddToRoleAsync(user, RoleName.Admin);
                if (roleResult.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
