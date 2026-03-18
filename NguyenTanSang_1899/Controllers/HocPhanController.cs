using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenTanSang_1899.Models;

public class HocPhanController : Controller
{
    private readonly ApplicationDbContext _context;

    public HocPhanController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("MaSV") == null)
            return RedirectToAction("DangNhap", "NguoiDung");

        return View(_context.HocPhans.ToList());
    }

    public IActionResult DangKy(string id)
    {
        var maSV = HttpContext.Session.GetString("MaSV");

        if (maSV == null)
            return RedirectToAction("DangNhap", "NguoiDung");

        var hocPhan = _context.HocPhans.FirstOrDefault(x => x.MaHP == id);

        // ❌ HẾT SLOT
        if (hocPhan.SoLuong <= 0)
        {
            TempData["msg"] = "Hết chỗ!";
            return RedirectToAction("Index");
        }

        // ❌ TRÙNG
        var check = _context.ChiTietDangKys
            .Include(x => x.DangKy)
            .Any(x => x.MaHP == id && x.DangKy.MaSV == maSV);

        if (check)
        {
            TempData["msg"] = "Bạn đã đăng ký rồi!";
            return RedirectToAction("Index");
        }

        // ✅ tạo đăng ký
        var dk = new DangKy
        {
            MaSV = maSV,
            NgayDK = DateTime.Now
        };

        _context.DangKys.Add(dk);
        _context.SaveChanges();

        // ✅ chi tiết
        var ct = new ChiTietDangKy
        {
            MaDK = dk.MaDK,
            MaHP = id
        };

        _context.ChiTietDangKys.Add(ct);

        // ✅ giảm slot
        hocPhan.SoLuong--;

        _context.SaveChanges();

        TempData["msg"] = "Đăng ký thành công!";
        return RedirectToAction("Index");
    }
    public IActionResult GioHang()
    {
        var maSV = HttpContext.Session.GetString("MaSV");

        if (maSV == null)
            return RedirectToAction("DangNhap", "NguoiDung");

        var data = _context.ChiTietDangKys
            .Include(x => x.HocPhan)
            .Include(x => x.DangKy)
            .Where(x => x.DangKy.MaSV == maSV)
            .ToList();

        return View(data);
    }
    public IActionResult Xoa(string id)
    {
        var maSV = HttpContext.Session.GetString("MaSV");

        var ct = _context.ChiTietDangKys
            .Include(x => x.DangKy)
            .FirstOrDefault(x => x.MaHP == id && x.DangKy.MaSV == maSV);

        if (ct != null)
        {
            var hp = _context.HocPhans.FirstOrDefault(x => x.MaHP == id);

            // ✅ trả lại slot
            hp.SoLuong++;

            _context.ChiTietDangKys.Remove(ct);
            _context.SaveChanges();
        }

        return RedirectToAction("GioHang");
    }
}