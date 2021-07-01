using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public CreateModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
        ViewData["RequestReasonId"] = new SelectList(_context.RequestReasons, "Id", "Name");
        ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public LeavePermissionRequest LeavePermissionRequest { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LeavePermissionRequests.Add(LeavePermissionRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
