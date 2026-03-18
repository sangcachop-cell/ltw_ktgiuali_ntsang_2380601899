using Microsoft.AspNetCore.Mvc;
using NguyenTanSang_1899.Models;

public class NguoiDungController : Controller
{
    private readonly ApplicationDbContext _context;

    public NguoiDungController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult DangNhap()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DangNhap(string maSV)
    {
        var sv = _context.SinhViens.FirstOrDefault(x => x.MaSV == maSV);

        if (sv != null)
        {
            HttpContext.Session.SetString("MaSV", sv.MaSV);
            return RedirectToAction("Index", "HocPhan");
        }

        ViewBag.Error = "Sai mã SV!";
        return View();
    }

    public IActionResult DangXuat()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("DangNhap");
    }
}