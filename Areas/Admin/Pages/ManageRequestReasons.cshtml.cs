using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Areas.Admin.Pages
{
    public class ManageRequestReasonsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageRequestReasonsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RequestReason> RequestReasons { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Name { get; set; }
        }

        public async Task OnGet()
        {
            await GetRequestReasons();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            await GetRequestReasons();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var requestReason = new RequestReason
            {
                Name = Input.Name
            };

             _context.RequestReasons.Add(requestReason);
            await _context.SaveChangesAsync();

            await GetRequestReasons();
            return Page();

        }

        private async Task GetRequestReasons()
        {
            RequestReasons = await _context.RequestReasons.ToListAsync();
        }

        public static implicit operator ManageRequestReasonsModel(Models.Manager v)
        {
            throw new NotImplementedException();
        }
    }
}
