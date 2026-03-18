using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenTanSang_1899.Models;

namespace NguyenTanSang_1899.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = _context.SinhViens.Include(s => s.NganhHoc);
            return View(await list.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sv = await _context.SinhViens
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(m => m.MaSV == id);

            if (sv == null) return NotFound();

            return View(sv);
        }
        public IActionResult Create()
        {
            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVien sv, IFormFile file)
        {
            ModelState.Remove("Hinh");
            ModelState.Remove("NganhHoc");
            ModelState.Remove("DangKys");

            if (!ModelState.IsValid)
            {
                ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", sv.MaNganh);
                return View(sv);
            }

            try
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    sv.Hinh = "/images/" + fileName;
                }
                else
                {
                    sv.Hinh = "/images/default.png";
                }

                _context.Add(sv);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
            }

            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        // 🔹 EDIT GET
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sv = await _context.SinhViens.FindAsync(id);
            if (sv == null) return NotFound();

            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SinhVien sv, IFormFile file)
        {
            if (id != sv.MaSV) return NotFound();

            ModelState.Remove("Hinh");
            ModelState.Remove("NganhHoc");
            ModelState.Remove("DangKys");

            if (!ModelState.IsValid)
            {
                ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", sv.MaNganh);
                return View(sv);
            }

            try
            {
                var existing = await _context.SinhViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.MaSV == id);

                if (existing == null) return NotFound();

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    sv.Hinh = "/images/" + fileName;
                }
                else
                {
                    sv.Hinh = existing.Hinh;
                }

                _context.Update(sv);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
            }

            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sv = await _context.SinhViens
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(m => m.MaSV == id);

            if (sv == null) return NotFound();

            return View(sv);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sv = await _context.SinhViens.FindAsync(id);

            if (sv != null)
            {
                _context.SinhViens.Remove(sv);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}