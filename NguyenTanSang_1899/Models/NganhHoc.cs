using NguyenTanSang_1899.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTanSang_1899.Models
{
    public class NganhHoc
    {
        [Key]
        public string MaNganh { get; set; }

        public string TenNganh { get; set; }

        // Navigation
        public ICollection<SinhVien> SinhViens { get; set; }
    }
}