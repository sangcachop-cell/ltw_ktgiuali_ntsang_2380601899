using NguyenTanSang_1899.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenTanSang_1899.Models
{
    public class DangKy
    {
        [Key]
        public int MaDK { get; set; }

        public DateTime? NgayDK { get; set; }

        // Foreign key
        public string MaSV { get; set; }

        [ForeignKey("MaSV")]
        public SinhVien SinhVien { get; set; }

        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}