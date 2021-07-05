using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public IndexModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LeavePermissionRequest> LeavePermissionRequest { get;set; }

        public async Task OnGetAsync()
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Username == User.Identity.Name);
            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(l => l.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).Where(x => x.EmployeeId == employee.Id).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var request = await _context.LeavePermissionRequests.FindAsync(id);

            if (request != null)
            {
                _context.LeavePermissionRequests.Remove(request);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
