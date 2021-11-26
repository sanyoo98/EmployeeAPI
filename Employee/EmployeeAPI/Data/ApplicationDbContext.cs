using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Basic> basics { get; set; }
    }
}
