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
    public class Details : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public Details(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public LeavePermissionRequest LeavePermissionRequest { get; set; }


        public async Task OnGetAsync(int? id)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Username == User.Identity.Name);
            LeavePermissionRequest = await _context.LeavePermissionRequests
                .Include(x => x.Employee)
                .Include(l => l.RequestReason)
                .Include(l => l.RequestStatus).FirstOrDefaultAsync(m => m.Id == id);

        }
        public async Task<> OnPostAsync(int? id)
        {


        }
    }
}
