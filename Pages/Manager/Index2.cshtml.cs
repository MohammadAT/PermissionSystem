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
    public class Index2Model : PageModel
    {
        private readonly PermissionSystem.Data.ApplicationDbContext _context;

        public Index2Model(PermissionSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LeavePermissionRequest> LeavePermissionRequest { get; set; }


    }
}