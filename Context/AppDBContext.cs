using DocumentSharingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Context
{
    public class AppDBContext:DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext>options):base(options)
        {
            
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
    }
}
