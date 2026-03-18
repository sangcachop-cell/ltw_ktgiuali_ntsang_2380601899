using NguyenTanSang_1899.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenTanSang_1899.Models
{
    public class ChiTietDangKy
    {
        [Key]
        public int Id { get; set; }
        public int MaDK { get; set; }
        public string MaHP { get; set; }

        [ForeignKey("MaDK")]
        public DangKy DangKy { get; set; }

        [ForeignKey("MaHP")]
        public HocPhan HocPhan { get; set; }
    }
}