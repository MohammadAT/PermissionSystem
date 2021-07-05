using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Areas.Admin.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userEmployee;


        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userEmployee)
        {
            _context = context;
            _userEmployee = userEmployee;
        }

        public IActionResult OnGet()
        {

            // used for puplite drop list
            ViewData["ManagerId"] = new SelectList(_context.Managers, "Id", "Name");

            return Page();
        }

        //drop down list 
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return FirstName + " " + LastName;
            }
        }

        //[BindProperty]
        //public Manager Manager { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Username { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Department { get; set; }

            [Required]
            [Display(Name = "Manager")]

            public int ManagerId { get; set; }

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // fill user object and create a user for Manager
            var user = new ApplicationUser
            {
                Name = Input.Name,
                UserName = Input.Username,
                ResetPasswordRequired = true,
                Email = Input.Email
            };

            var result = await _userEmployee.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                // add role "Manager" to user
                var roleResult = await _userEmployee.AddToRoleAsync(user, RoleName.Employee);
                if (roleResult.Succeeded)
                {
                    // fill manager object and save it to database 
                    var employee = new Employee
                    {
                        Name = Input.Name,
                        Department = Input.Department,
                        Username = Input.Username,
                        ManagerId = Input.ManagerId
                    };

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    AddErrors(roleResult);
                    return Page();
                }
            }
            else
            {
                AddErrors(result);
                return Page();
            }


            return RedirectToPage("./Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
