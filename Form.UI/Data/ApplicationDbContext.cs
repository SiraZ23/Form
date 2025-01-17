using Form.UI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Form.UI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Student> students { get; set; }
    }
}
