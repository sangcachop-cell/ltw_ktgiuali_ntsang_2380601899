using NguyenTanSang_1899.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenTanSang_1899.Models
{
    public class SinhVien
    {
        [Key]
        public string MaSV { get; set; }

        [Required]
        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string Hinh { get; set; }

        // Foreign key
        public string MaNganh { get; set; }

        [ForeignKey("MaNganh")]
        public NganhHoc NganhHoc { get; set; }

        // Navigation
        public ICollection<DangKy> DangKys { get; set; }
    }
}