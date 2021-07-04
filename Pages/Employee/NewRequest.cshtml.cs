using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PermissionSystem.Data;
using PermissionSystem.Models;

namespace PermissionSystem.Pages.Employee
{
    public class NewRequestModel : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public NewRequestModel(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["RequestReasonId"] = new SelectList(_context.RequestReasons, "Id", "Name");
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Name");
            return Page();
        }

        //[BindProperty]
        //public LeavePermissionRequest LeavePermissionRequest { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {

            [Required]
            public string TimeFrom { get; set; }

            [Required]
            public string TimeTo { get; set; }

            [Required]
            public DateTime Date { get; set; }

            public string EmployeeNote { get; set; }

            [Required]
            public int RequestReasonId { get; set; }

            [Required]
            public int RequestStatusId { get; set; }

        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Username == User.Identity.Name);
            
            if (employee == null)
            {
                return Page();
            }

            var newRequest = new LeavePermissionRequest
            {
                Date = Input.Date,
                EmployeeId = employee.Id,
                EmployeeNote = Input.EmployeeNote,
                RequestReasonId = Input.RequestReasonId,
                RequestStatusId = Input.RequestStatusId,
                TimeFrom = Input.TimeFrom,
                TimeTo = Input.TimeTo
            };

            _context.LeavePermissionRequests.Add(newRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
