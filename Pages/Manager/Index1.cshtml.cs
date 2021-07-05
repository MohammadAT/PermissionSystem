using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Areas.Admin.Pages.Manager
{
    public class Index1tModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public Index1tModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ManageRequestReasonsModel Manager { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == id);

            if (Manager == null)
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

            Manager = await _context.Managers.FindAsync(id);

            if (Manager != null)
            {
                _context.Managers.DefaultIfEmpty();
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
