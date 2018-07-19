using Microsoft.EntityFrameworkCore;

namespace WitnesbyApp.Models
{
    public class VaultContext : DbContext
    {
        public VaultContext(DbContextOptions<VaultContext> options)
            : base(options)
        {
        }

        public DbSet<VaultItem> VaultItems { get; set; }
    }
}
