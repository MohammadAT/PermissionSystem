using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Manager
{
    public class Details : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public Details(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public LeavePermissionRequest LeavePermissionRequest { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Status")]
            public int StatusId { get; set; }

            [Required]
            [Display(Name = "Manager Note")]
            public string ManagerNote { get; set; }



        }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Username == User.Identity.Name);
            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(x => x.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).FirstOrDefaultAsync(m => m.Id == id);

            ViewData["RequestStatus"] = new SelectList(_context.RequestStatuses, "Id", "Name");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var request = await _context.LeavePermissionRequests.FindAsync(id);
            if (request != null)
            {
                request.RequestStatusId = Input.StatusId;
                request.ManagerNote = Input.ManagerNote;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");

        }

    }
}

