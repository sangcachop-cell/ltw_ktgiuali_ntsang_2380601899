using NguyenTanSang_1899.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTanSang_1899.Models
{
    public class HocPhan
    {
        [Key]
        public string MaHP { get; set; }

        [Required]
        public string TenHP { get; set; }

        public int? SoTinChi { get; set; }

        public int SoLuong { get; set; } = 50;

        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}