using FilesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesAPI
{
    public class DatabaseContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
