using Microsoft.EntityFrameworkCore;
using NguyenTanSang_1899.Models;

namespace NguyenTanSang_1899.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NganhHoc> NganhHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khóa chính kép
            modelBuilder.Entity<ChiTietDangKy>()
                .HasKey(ct => new { ct.MaDK, ct.MaHP });

            base.OnModelCreating(modelBuilder);
        }
    }
}