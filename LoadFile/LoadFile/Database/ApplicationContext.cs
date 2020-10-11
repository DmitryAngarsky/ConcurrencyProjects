using LoadFile.Model;
using Microsoft.EntityFrameworkCore;

namespace LoadFile.Database
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}