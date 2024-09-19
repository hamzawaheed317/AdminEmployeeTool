using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminEmployeeTool.Models;
using AdminEmployeeTool.EmployeeArchitect.EmployeeModels;

namespace AdminEmployeeTool.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminEmployeeTool.Models.Announcement> Announcement { get; set; } = default!;

        public DbSet<AdminEmployeeTool.Models.EventViewModel> EventViewModel { get; set; } = default!;

        public DbSet<Initiative> Initiative { get; set; } = default!;
    }
}
