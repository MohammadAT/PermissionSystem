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
            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(l => l.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).ToListAsync();
        }
    }
}
