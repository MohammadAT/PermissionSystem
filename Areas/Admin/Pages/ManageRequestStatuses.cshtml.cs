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
    public class ManageRequestStatusesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageRequestStatusesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RequestStatus> RequestStatuses { get; set; }


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
            await GetRequestStatuses();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await GetRequestStatuses();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var requestStatus = new RequestStatus
            {
                Name = Input.Name
            };

            _context.RequestStatuses.Add(requestStatus);
            await _context.SaveChangesAsync();

            await GetRequestStatuses();
            return Page();

        }


        private async Task GetRequestStatuses()
        {
            RequestStatuses = await _context.RequestStatuses.ToListAsync();
        }
    }
}
