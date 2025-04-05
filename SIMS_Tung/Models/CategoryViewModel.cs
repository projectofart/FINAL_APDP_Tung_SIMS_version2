using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ASM_SIMS.Validations;

namespace ASM_SIMS.Models
{
    public class CategoryViewModel
    {
        public List<CategoryDetail> categoryList { get; set; }
    }

    public class CategoryDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string NameCategory { get; set; }

        [AllowNull]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose your Avatar")]
        [AllowedSizeFile(3 * 1024 * 1024)]
        [AllowedTypeFile(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
        public IFormFile? ViewAvatar { get; set; }

        public string? Avatar { get; set; } 

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
