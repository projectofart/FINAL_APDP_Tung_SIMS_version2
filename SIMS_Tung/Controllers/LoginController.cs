using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASM_SIMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly SimsDataContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(SimsDataContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Accounts
                        .FirstOrDefault(a => a.Email == model.email
                                          && a.Password == model.password
                                          && a.DeletedAt == null);

                    if (user != null)
                    {
                        // ✅ Ghi session
                        HttpContext.Session.SetString("Username", user.Username ?? "");
                        HttpContext.Session.SetInt32("RoleId", user.RoleId);

                        // ✅ Chuyển hướng theo Role
                        return user.RoleId switch
                        {
                            1 => RedirectToAction("Index", "Dashboard"),              // Admin
                            2 => RedirectToAction("Index", "TeacherProfile"),              // Teacher
                            3 => RedirectToAction("Index", "StudentProfile"),         // Student
                            _ => RedirectToAction("Index", "Login")
                        };
                    }

                    ViewData["MessageLogin"] = "Email hoặc mật khẩu sai hoặc tài khoản đã bị xóa.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi đăng nhập.");
                    ViewData["MessageLogin"] = "Lỗi hệ thống. Vui lòng thử lại sau.";
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // ❌ Xoá tất cả session
            return RedirectToAction("Index", "Login");
        }
    }
}
