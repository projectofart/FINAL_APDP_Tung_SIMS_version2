using ASM_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using ASM_SIMS.Helpers;
using ASM_SIMS.DB;

namespace ASM_SIMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SimsDataContext _dbcontext;
        public CategoryController(SimsDataContext context)
        {
            _dbcontext = context;
        }
        public IActionResult Index()
        {
            CategoryViewModel categoryModel = new CategoryViewModel();
            categoryModel.categoryList = new List<CategoryDetail>();

            var data = _dbcontext.Categories
                .Where(c => c.DeletedAt == null) // ✅ Chỉ lấy cái chưa xóa
                .ToList();

            foreach (var item in data)
            {
                categoryModel.categoryList.Add(new CategoryDetail
                {
                    Id = item.Id,
                    NameCategory = item.NameCategory,
                    Description = item.Description,
                    Avatar = item.Avatar,
                    Status = item.Status,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                });
            }

            ViewData["title"] = "Category";
            return View(categoryModel);
        }


        [HttpGet]
        public IActionResult Create()
        {

            CategoryDetail model = new CategoryDetail();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDetail model, IFormFile ViewAvatar)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFile uploadFile = new UploadFile(ViewAvatar);
                    string fileAvatar = await uploadFile.UploadAsync("images"); // ✅ gọi đúng tên & có await
                    var dataCreate = new Categories()
                    {
                        NameCategory = model.NameCategory,
                        Description = model.Description,
                        Avatar = fileAvatar,
                        Status = "Active",
                        CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),

                    };
                    _dbcontext.Categories.Add(dataCreate);
                    _dbcontext.SaveChanges(true);
                    TempData["save"] = true;

                }
                catch(Exception ex)
                {
                    TempData["save"] = false;
                    return Ok(ex.Message.ToString());
                }
                return RedirectToAction("Index", "Category");
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _dbcontext.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.DeletedAt = DateTime.Now;
                _dbcontext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
    }

}
