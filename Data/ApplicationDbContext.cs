using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<LeavePermissionRequest> LeavePermissionRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestReason> RequestReasons { get; set; }
    }
}
