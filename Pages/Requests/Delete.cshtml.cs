using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Requests
{
    public class DeleteModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public DeleteModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeavePermissionRequest LeavePermissionRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(l => l.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).FirstOrDefaultAsync(m => m.Id == id);

            if (LeavePermissionRequest == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeavePermissionRequest = await _context.LeavePermissionRequests.FindAsync(id);

            if (LeavePermissionRequest != null)
            {
                _context.LeavePermissionRequests.Remove(LeavePermissionRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
