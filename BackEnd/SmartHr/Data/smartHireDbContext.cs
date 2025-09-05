using Microsoft.EntityFrameworkCore;

namespace Smart_HR___RMS.Data
{
    public class SmartHireDbContext : DbContext
    {
        public SmartHireDbContext() { }

        public SmartHireDbContext(DbContextOptions<SmartHireDbContext> options) : base(options) { }

        public DbSet<UserRegisters> UserRegisters { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.UserRegister)
                .WithMany(ur => ur.UserDetails)
                .HasForeignKey(ud => ud.UserRegisterId)
                .IsRequired();
        }
    }
}
