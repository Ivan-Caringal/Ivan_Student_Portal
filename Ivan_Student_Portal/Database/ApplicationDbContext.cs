using Ivan_Student_Portal.Models.StudentTable;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace Ivan_Student_Portal.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
