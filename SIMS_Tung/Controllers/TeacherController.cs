using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM_SIMS.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SimsDataContext _dbContext;

        public TeacherController(SimsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var teachers = _dbContext.Teachers
                .Where(t => t.DeletedAt == null)
                .Include(t => t.Account)
                .Select(t => new TeacherViewModel
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    Phone = t.Phone,
                    Address = t.Address,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                }).ToList();

            ViewData["Title"] = "Teachers";
            return View(teachers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_dbContext.Courses, "Id", "NameCourse");
            return View(new TeacherViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string[] nameParts = model.FullName.Trim().Split(' ');
                    string ho = nameParts[0];
                    string dem = nameParts.Length > 2 ? nameParts[1] : "";
                    string ten = nameParts[^1];
                    string password = $"{ten}{ho[0]}{(dem.Length > 0 ? dem[0] : null)}";

                    var account = new ASM_SIMS.DB.Account
                    {
                        RoleId = 2,
                        Username = model.Email,
                        Password = password,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address ?? "",
                        CreatedAt = DateTime.Now
                    };
                    _dbContext.Accounts.Add(account);
                    _dbContext.SaveChanges();

                    var teacher = new Teacher
                    {
                        AccountId = account.Id,
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        CourseId = model.CourseId,
                        Status = model.Status,
                        CreatedAt = DateTime.Now
                    };
                    _dbContext.Teachers.Add(teacher);
                    _dbContext.SaveChanges();
                    TempData["save"] = true;
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["save"] = false;
                    ModelState.AddModelError("", $"Database error: {dbEx.Message} | Inner: {dbEx.InnerException?.Message}");
                }
                catch (Exception ex)
                {
                    TempData["save"] = false;
                    ModelState.AddModelError("", $"An error occurred: {ex.Message} | Inner: {ex.InnerException?.Message}");
                }
            }
            ViewBag.Courses = new SelectList(_dbContext.Courses, "Id", "NameCourse");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var teacher = _dbContext.Teachers
                .Include(t => t.Account)
                .FirstOrDefault(t => t.Id == id && t.DeletedAt == null);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Address = teacher.Address,
                Status = teacher.Status
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var teacher = _dbContext.Teachers
                        .FirstOrDefault(t => t.Id == model.Id && t.DeletedAt == null);

                    if (teacher == null)
                    {
                        return NotFound();
                    }

                    teacher.FullName = model.FullName;
                    teacher.Email = model.Email;
                    teacher.Phone = model.Phone;
                    teacher.Address = model.Address;
                    teacher.Status = model.Status;
                    teacher.UpdatedAt = DateTime.Now;

                    _dbContext.Teachers.Update(teacher);
                    _dbContext.SaveChanges();
                    TempData["save"] = true;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["save"] = false;
                    ModelState.AddModelError("", $"Lỗi khi sửa giảng viên: {ex.Message} | Inner: {ex.InnerException?.Message}");
                }
            }
            return View(model);
        }

        [HttpPost]

  
        public IActionResult Delete(int id)
        {
            var teacher = _dbContext.Teachers
                .FirstOrDefault(t => t.Id == id && t.DeletedAt == null);

            if (teacher == null)
            {
                return NotFound();
            }

            try
            {
                // Gỡ các phòng học đang tham chiếu Teacher
                var relatedClassRooms = _dbContext.ClassRooms
                    .Where(c => c.TeacherId == teacher.Id)
                    .ToList();

               

                // Cập nhật soft delete cho giáo viên
                teacher.DeletedAt = DateTime.Now;
                teacher.Status = "Deleted";

                // Cập nhật thông tin account
                var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == teacher.AccountId);
                if (account != null)
                {
                    account.DeletedAt = DateTime.Now;
                   
                    account.Username = $"DELETED_{account.Id}";
                    account.Password = "DELETED";
                    _dbContext.Accounts.Update(account);
                }

                _dbContext.Teachers.Update(teacher);
                _dbContext.SaveChanges();

                TempData["save"] = true;
            }
            catch (Exception ex)
            {
                TempData["save"] = false;
                ModelState.AddModelError("", $"Lỗi khi xóa giảng viên: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
