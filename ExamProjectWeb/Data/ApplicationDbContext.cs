using ExamProjectWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamProjectWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Here's the constructor of the DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // A property, which is a table of the database
        public DbSet<UserInformationModel> UserInfos { get; set; }
    }
}
