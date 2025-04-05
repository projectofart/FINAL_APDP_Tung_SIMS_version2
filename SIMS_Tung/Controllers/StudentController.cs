using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM_SIMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly SimsDataContext _dbContext;

        public StudentController(SimsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var students = _dbContext.Students
                .Where(s => s.DeletedAt == null)
                .Include(s => s.ClassRoom)
                .Include(s => s.Course)
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    Address = s.Address,
                    ClassRoomId = s.ClassRoomId,
                    CourseId = s.CourseId,
                    Status = s.Status,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                }).ToList();

            ViewBag.ClassRooms = _dbContext.ClassRooms.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            ViewData["Title"] = "Students";
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ClassRooms = _dbContext.ClassRooms.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(new StudentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentViewModel model)
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
                        RoleId = 3,
                        Username = model.Email,
                        Password = password,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address ?? "",
                        CreatedAt = DateTime.Now
                    };
                    _dbContext.Accounts.Add(account);
                    _dbContext.SaveChanges();

                    var student = new Student
                    {
                        AccountId = account.Id,
                        FullName = model.FullName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        ClassRoomId = model.ClassRoomId,
                        CourseId = model.CourseId,
                        Status = model.Status,
                        CreatedAt = DateTime.Now
                    };
                    _dbContext.Students.Add(student);
                    _dbContext.SaveChanges();
                    TempData["save"] = true;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["save"] = false;
                    ModelState.AddModelError("", $"Lỗi khi thêm sinh viên: {ex.Message} | Inner: {ex.InnerException?.Message}");
                }
            }
            ViewBag.ClassRooms = _dbContext.ClassRooms.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _dbContext.Students
                .Include(s => s.ClassRoom)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Id == id && s.DeletedAt == null);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentViewModel
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                ClassRoomId = student.ClassRoomId,
                CourseId = student.CourseId,
                Status = student.Status
            };
            ViewBag.ClassRooms = _dbContext.ClassRooms.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var student = _dbContext.Students.FirstOrDefault(s => s.Id == model.Id && s.DeletedAt == null);
                    if (student == null) return NotFound();

                    student.FullName = model.FullName;
                    student.Email = model.Email;
                    student.Phone = model.Phone;
                    student.Address = model.Address;
                    student.ClassRoomId = model.ClassRoomId;
                    student.CourseId = model.CourseId;
                    student.Status = model.Status;
                    student.UpdatedAt = DateTime.Now;

                    _dbContext.Students.Update(student);
                    _dbContext.SaveChanges();
                    TempData["save"] = true;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["save"] = false;
                    ModelState.AddModelError("", $"Lỗi khi sửa sinh viên: {ex.Message} | Inner: {ex.InnerException?.Message}");
                }
            }
            ViewBag.ClassRooms = _dbContext.ClassRooms.ToList();
            ViewBag.Courses = _dbContext.Courses.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id && s.DeletedAt == null);
            if (student == null) return NotFound();

            try
            {
                student.DeletedAt = DateTime.Now;
                student.Status = "Deleted";

                // Cập nhật tài khoản tương ứng
                var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == student.AccountId);
                if (account != null)
                {
                    account.DeletedAt = DateTime.Now;
                    account.Username = $"DELETED_{account.Id}";
                    account.Password = "DELETED";
                    _dbContext.Accounts.Update(account);
                }

                _dbContext.Students.Update(student);
                _dbContext.SaveChanges();
                TempData["save"] = true;
            }
            catch (Exception ex)
            {
                TempData["save"] = false;
                ModelState.AddModelError("", $"Lỗi khi xóa sinh viên: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}