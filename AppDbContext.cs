
using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVienCRUD
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=localhost\SQLEXPRESS01;Database=QuanLySinhVienDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(s => s.FullName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(s => s.Grade)
                    .IsRequired();
            });
        }
    }
}
