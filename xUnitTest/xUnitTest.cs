using ASM_SIMS.Controllers;
using ASM_SIMS.DB;
using ASM_SIMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace xUnitTest
{
    public class TeacherControllerTests
    {
        private DbContextOptions<SimsDataContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithTeacherViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Index_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Teachers.Add(new Teacher { Id = 1, FullName = "Test Teacher", Email = "test@teacher.com", Status = "Active" });
                context.SaveChanges();

                var controller = new TeacherController(context);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<TeacherViewModel>>(viewResult.Model);
                Assert.Single(model);
            }
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithTeacherViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new TeacherController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<TeacherViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new TeacherController(context);
                var model = new TeacherViewModel
                {
                    FullName = "Test Teacher",
                    Email = "test@teacher.com",
                    Phone = "1234567890",
                    Status = "Active"
                };

                // Act
                var result = controller.Create(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithTeacherViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Teachers.Add(new Teacher { Id = 1, FullName = "Test Teacher", Email = "test@teacher.com", Status = "Active" });
                context.SaveChanges();

                var controller = new TeacherController(context);

                // Act
                var result = controller.Edit(1) as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<TeacherViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Teachers.Add(new Teacher { Id = 1, FullName = "Test Teacher", Email = "test@teacher.com", Status = "Active" });
                context.SaveChanges();

                var controller = new TeacherController(context);
                var model = new TeacherViewModel
                {
                    Id = 1,
                    FullName = "Updated Teacher",
                    Email = "updated@teacher.com",
                    Phone = "0987654321",
                    Status = "Active"
                };

                // Act
                var result = controller.Edit(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Delete_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = GetDbContextOptions("Delete_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Teachers.Add(new Teacher { Id = 1, FullName = "Test Teacher", Email = "test@teacher.com", Status = "Active" });
                context.SaveChanges();

                var controller = new TeacherController(context);

                // Act
                var result = controller.Delete(1) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }
    }
    public class StudentControllerTests
    {
        private DbContextOptions<SimsDataContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithStudentViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Index_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Students.Add(new Student { Id = 1, FullName = "Test Student", Email = "test@student.com", Status = "Active" });
                context.SaveChanges();

                var controller = new StudentController(context);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<StudentViewModel>>(viewResult.Model);
                Assert.Single(model);
            }
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithStudentViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new StudentController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<StudentViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new StudentController(context);
                var model = new StudentViewModel
                {
                    FullName = "Test Student",
                    Email = "test@student.com",
                    Phone = "1234567890",
                    Status = "Active"
                };

                // Act
                var result = controller.Create(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithStudentViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Students.Add(new Student { Id = 1, FullName = "Test Student", Email = "test@student.com", Status = "Active" });
                context.SaveChanges();

                var controller = new StudentController(context);

                // Act
                var result = controller.Edit(1) as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<StudentViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Students.Add(new Student { Id = 1, FullName = "Test Student", Email = "test@student.com", Status = "Active" });
                context.SaveChanges();

                var controller = new StudentController(context);
                var model = new StudentViewModel
                {
                    Id = 1,
                    FullName = "Updated Student",
                    Email = "updated@student.com",
                    Phone = "0987654321",
                    Status = "Active"
                };

                // Act
                var result = controller.Edit(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Delete_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = GetDbContextOptions("Delete_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Students.Add(new Student { Id = 1, FullName = "Test Student", Email = "test@student.com", Status = "Active" });
                context.SaveChanges();

                var controller = new StudentController(context);

                // Act
                var result = controller.Delete(1) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }
    }
    public class ClassRoomControllerTests
    {
        private DbContextOptions<SimsDataContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithClassRoomViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Index_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.ClassRooms.Add(new ClassRoom { Id = 1, ClassName = "Test Class", Status = "Active" });
                context.SaveChanges();

                var controller = new ClassRoomController(context);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<ClassRoomViewModel>>(viewResult.Model);
                Assert.Single(model);
            }
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithClassRoomViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new ClassRoomController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<ClassRoomViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new ClassRoomController(context);
                var model = new ClassRoomViewModel
                {
                    ClassName = "Test Class",
                    CourseId = 1,
                    TeacherId = 1,
                    StartDate = new DateOnly(2025, 4, 1),
                    EndDate = new DateOnly(2025, 4, 30),
                    Schedule = "Mon-Wed-Fri",
                    Location = "Room 101",
                    Status = "Active"
                };

                // Act
                var result = controller.Create(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithClassRoomViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.ClassRooms.Add(new ClassRoom { Id = 1, ClassName = "Test Class", Status = "Active" });
                context.SaveChanges();

                var controller = new ClassRoomController(context);

                // Act
                var result = controller.Edit(1) as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<ClassRoomViewModel>(viewResult.Model);
            }
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.ClassRooms.Add(new ClassRoom { Id = 1, ClassName = "Test Class", Status = "Active" });
                context.SaveChanges();

                var controller = new ClassRoomController(context);
                var model = new ClassRoomViewModel
                {
                    Id = 1,
                    ClassName = "Updated Class",
                    CourseId = 1,
                    TeacherId = 1,
                    StartDate = new DateOnly(2025, 4, 1),
                    EndDate = new DateOnly(2025, 4, 30),
                    Schedule = "Mon-Wed-Fri",
                    Location = "Room 101",
                    Status = "Active"
                };

                // Act
                var result = controller.Edit(model) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void Delete_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = GetDbContextOptions("Delete_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.ClassRooms.Add(new ClassRoom { Id = 1, ClassName = "Test Class", Status = "Active" });
                context.SaveChanges();

                var controller = new ClassRoomController(context);

                // Act
                var result = controller.Delete(1) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }
    }
    public class xUnitTest
    {

        [Fact]
        public void Index_Get_ReturnsViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Index_Get")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger);

                // Act
                var result = controller.Index();

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void Index_Post_WithInvalidModel_ReturnsViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Index_Post_InvalidModel")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger);
                controller.ModelState.AddModelError("email", "Required");

                // Act
                var result = controller.Index(new LoginViewModel()) as ViewResult;

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void Index_Post_WithValidAdmin_ShouldRedirectTo_Dashboard()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Index_Post_ValidAdmin")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                context.Accounts.Add(new ASM_SIMS.DB.Account
                {
                    Username = "admin1@gmail.com",
                    Email = "admin1@gmail.com",
                    Password = "admin123",
                    RoleId = 1 // Admin
                });
                context.SaveChanges();

                var httpContext = new DefaultHttpContext();
                httpContext.Session = new TestSession();

                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = httpContext
                    }
                };

                // Act
                var result = controller.Index(new LoginViewModel
                {
                    email = "admin1@gmail.com",
                    password = "admin123"
                }) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("Dashboard", result.ControllerName);
            }
        }

        [Fact]
        public void Index_Post_WithValidTeacher_ShouldRedirectTo_TeacherProfile()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Index_Post_ValidTeacher")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                context.Accounts.Add(new ASM_SIMS.DB.Account
                {
                    Username = "teacher1@gmail.com",
                    Email = "teacher1@gmail.com",
                    Password = "teach123",
                    RoleId = 2 // Teacher
                });
                context.SaveChanges();

                var httpContext = new DefaultHttpContext();
                httpContext.Session = new TestSession();

                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = httpContext
                    }
                };

                // Act
                var result = controller.Index(new LoginViewModel
                {
                    email = "teacher1@gmail.com",
                    password = "teach123"
                }) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("TeacherProfile", result.ControllerName);
            }
        }
        [Fact]
        public void TestCategories()
        {
            // Arrange
            var course = new Courses();
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;
            var deletedAt = DateTime.Now;

            // Act
            course.CreatedAt = createdAt;
            course.Id = 1;
            course.CategoryId = 2;
            course.NameCourse = "Test Course";
            course.Description = "Test Description";
            course.StartDate = new DateOnly(2025, 4, 1);
            course.EndDate = new DateOnly(2025, 4, 30);
            course.AvatarCourse = "test_avatar.png";
            course.Vote = 5;
            course.Status = true;
            course.UpdatedAt = updatedAt;
            course.DeletedAt = deletedAt;
            course.Category = new Categories { Id = 2, NameCategory = "Test Category" };

            // Assert
            Assert.Equal(createdAt, course.CreatedAt);
            Assert.Equal(1, course.Id);
            Assert.Equal(2, course.CategoryId);
            Assert.Equal("Test Course", course.NameCourse);
            Assert.Equal("Test Description", course.Description);
            Assert.Equal(new DateOnly(2025, 4, 1), course.StartDate);
            Assert.Equal(new DateOnly(2025, 4, 30), course.EndDate);
            Assert.Equal("test_avatar.png", course.AvatarCourse);
            Assert.Equal(5, course.Vote);
            Assert.True(course.Status);
            Assert.Equal(updatedAt, course.UpdatedAt);
            Assert.Equal(deletedAt, course.DeletedAt);
            Assert.Equal(2, course.Category.Id);
            Assert.Equal("Test Category", course.Category.NameCategory);
        }
        [Fact]
        public void Index_Post_WithValidStudent_ShouldRedirectTo_StudentProfile()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Index_Post_ValidStudent")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                context.Accounts.Add(new ASM_SIMS.DB.Account
                {
                    Username = "student1@gmail.com",
                    Email = "student1@gmail.com",
                    Password = "stud123",
                    RoleId = 3 // Student
                });
                context.SaveChanges();

                var httpContext = new DefaultHttpContext();
                httpContext.Session = new TestSession();

                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = httpContext
                    }
                };

                // Act
                var result = controller.Index(new LoginViewModel
                {
                    email = "student1@gmail.com",
                    password = "student123"
                }) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("StudentProfile", result.ControllerName);
            }
        }

        [Fact]
        public void Logout_ShouldClearSessionAndRedirectTo_Login()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: "Logout")
                .Options;

            using (var context = new SimsDataContext(options))
            {
                var httpContext = new DefaultHttpContext();
                httpContext.Session = new TestSession();
                httpContext.Session.SetString("Username", "testuser");

                var logger = new Mock<ILogger<LoginController>>().Object;
                var controller = new LoginController(context, logger)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = httpContext
                    }
                };

                // Act
                var result = controller.Logout() as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("Login", result.ControllerName);
                Assert.Null(httpContext.Session.GetString("Username"));
            }
        }
        private DbContextOptions<SimsDataContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<SimsDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithCourseViewModel()
        {
            // Arrange
            var options = GetDbContextOptions("Index_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Courses.Add(new Courses { Id = 1, NameCourse = "Test Course", Status = true });
                context.SaveChanges();

                var controller = new CoursesController(context);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsType<CourseViewModel>(viewResult.Model);
                Assert.Single(model.courseList);
            }
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithCourseDetail()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new CoursesController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<CourseDetail>(viewResult.Model);
            }
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Create_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                var controller = new CoursesController(context);
                var model = new CourseDetail { NameCourse = "Test Course", CategoryId = 1, StartDate = new DateOnly(2025, 4, 1), EndDate = new DateOnly(2025, 4, 30) };

                // Act
                var result = await controller.Create(model, null) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
                Assert.Equal("Courses", redirectResult.ControllerName);
            }
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithCourseDetail()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Get_ReturnsViewResult");
            using (var context = new SimsDataContext(options))
            {
                context.Courses.Add(new Courses { Id = 1, NameCourse = "Test Course", Status = true });
                context.SaveChanges();

                var controller = new CoursesController(context);

                // Act
                var result = controller.Edit(1) as ViewResult;

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<CourseDetail>(viewResult.Model);
            }
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var options = GetDbContextOptions("Edit_Post_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Courses.Add(new Courses { Id = 1, NameCourse = "Test Course", Status = true });
                context.SaveChanges();

                var controller = new CoursesController(context);
                var model = new CourseDetail { Id = 1, NameCourse = "Updated Course", CategoryId = 1, StartDate = new DateOnly(2025, 4, 1), EndDate = new DateOnly(2025, 4, 30) };

                // Act
                var result = await controller.Edit(model, null) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
                Assert.Equal("Courses", redirectResult.ControllerName);
            }
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = GetDbContextOptions("Delete_ReturnsRedirectToActionResult");
            using (var context = new SimsDataContext(options))
            {
                context.Courses.Add(new Courses { Id = 1, NameCourse = "Test Course", Status = true });
                context.SaveChanges();

                var controller = new CoursesController(context);

                // Act
                var result = await controller.Delete(1) as RedirectToActionResult;

                // Assert
                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

    }

    // session class to mock HttpContext.Session
    public class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new();

        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear() => _sessionStorage.Clear();
        public void Remove(string key) => _sessionStorage.Remove(key);
        public void Set(string key, byte[] value) => _sessionStorage[key] = value;
        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
