using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Tamro.Entities
{
    public class TamroDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity { Id = 1, Email = "tomas.medonas@tamro.com", FirstName = "Tomas", LastName = "Medonas", PhoneNumber = "58671071"},
                new UserEntity { Id = 2, Email = "uldis.priekulis@tamro.com", FirstName = "Uldis", LastName = "Priekulis", PhoneNumber = "58671072" },
                new UserEntity { Id = 3, Email = "vugar.gasimov@avidatech.eu", FirstName = "Vugar",  LastName = "Gasimov", PhoneNumber = "58671073" }
            );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> Users { get; set; }

        public TamroDBContext([NotNull] DbContextOptions options) : base(options)
        {
        }
    }
}
