using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Areas.Admin.Pages.Managers
{
    public class IndexModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public IndexModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Manager> Manager { get;set; }

        public async Task OnGetAsync()
        {
            Manager = await _context.Managers.ToListAsync();
        }
    }
}
