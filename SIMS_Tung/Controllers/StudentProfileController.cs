using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ASM_SIMS.Controllers
{
    public class StudentProfileController : Controller
    {
        private readonly SimsDataContext _dbContext;

        public StudentProfileController(SimsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Username == username && a.DeletedAt == null);

            if (account == null || account.RoleId != 3)
                return Unauthorized();

            var student = _dbContext.Students
                .Include(s => s.ClassRoom)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.AccountId == account.Id && s.DeletedAt == null);

            if (student == null)
                return NotFound();

            var model = new StudentViewModel
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                ClassRoomId = student.ClassRoomId,
                CourseId = student.CourseId,
                Status = student.Status,
                CreatedAt = student.CreatedAt
            };

            ViewBag.ClassRoom = student.ClassRoom;
            ViewBag.Course = student.Course;
            return View("Profile",model);
        }
    }
}