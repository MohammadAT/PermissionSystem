using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public IndexModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LeavePermissionRequest> LeavePermissionRequest { get; set; }

        public async Task OnGetAsync()
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Username == User.Identity.Name);
            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(x => x.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).Where(m => m.Employee.ManagerId == manager.Id).ToListAsync();
        }
        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var request = await _context.LeavePermissionRequests.FindAsync(id);
            if (request != null)
            {
                request.RequestStatusId = 2;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejecteAsync(int id)
        {
            var request = await _context.LeavePermissionRequests.FindAsync(id);
            if (request != null)
            {
                request.RequestStatusId = 3;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
