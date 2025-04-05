using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ASM_SIMS.Controllers
{
    public class TeacherProfileController : Controller
    {
        private readonly SimsDataContext _dbContext;

        public TeacherProfileController(SimsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ✅ Đổi tên action từ Profile -> Index để mặc định truy cập /TeacherProfile
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Index", "Login");

            var account = _dbContext.Accounts
                .FirstOrDefault(a => a.Username == username && a.DeletedAt == null);

            if (account == null || account.RoleId != 2)
                return Unauthorized();

            var teacher = _dbContext.Teachers
                .Include(t => t.Course)
                .FirstOrDefault(t => t.AccountId == account.Id && t.DeletedAt == null);

            if (teacher == null)
                return NotFound();

            var model = new TeacherViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Address = teacher.Address,
                Status = teacher.Status,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt,
                CourseId = teacher.CourseId
            };

            ViewBag.Course = teacher.Course;

            // ✅ Thêm lớp học đang dạy
            var classRooms = _dbContext.ClassRooms
                .Where(c => c.TeacherId == teacher.Id && c.DeletedAt == null)
                .ToList();
            ViewBag.ClassRooms = classRooms;

            return View("Profile", model);
        }

    }
}
