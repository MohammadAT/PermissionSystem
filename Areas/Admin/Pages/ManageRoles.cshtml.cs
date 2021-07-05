using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;

namespace PermissionSystem.Areas.Admin.Pages
{
    public class ManageRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public ManageRolesModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public IList<IdentityRole> Roles { get; set; }
        public IList<UserRole> UserRoles { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Name { get; set; }
        }
        public class UserRole
        {
            public string UserName { get; set; }
            public string RoleName { get; set; }

        }
        public async Task OnGet()
        {
            await GetRoles();
            //await GetUsersRolesList(); TODO
        }

        //private Task GetUsersRolesList()
        //{
        //    TODO
        //}

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            var role = new IdentityRole
            {
                Name = Input.Name
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToPage();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return RedirectToPage();

        }

        private async Task GetRoles()
        {
            Roles = await _context.Roles.ToListAsync();
        }
    }
}
