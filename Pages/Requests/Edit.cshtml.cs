using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Requests
{
    public class EditModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public EditModel(PermissionSystem.Data.ApplicationDbContext context)
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
           ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
           ViewData["RequestReasonId"] = new SelectList(_context.RequestReasons, "Id", "Id");
           ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeavePermissionRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePermissionRequestExists(LeavePermissionRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeavePermissionRequestExists(int id)
        {
            return _context.LeavePermissionRequests.Any(e => e.Id == id);
        }
    }
}
