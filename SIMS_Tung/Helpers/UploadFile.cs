using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ASM_SIMS.Helpers
{
    public class UploadFile
    {
        private readonly IFormFile _formFile;

        public UploadFile(IFormFile file)
        {
            _formFile = file;
        }

        public async Task<string> UploadAsync(string type)
        {
            try
            {
                string pathUpload = Path.Combine("wwwroot", "SIMS", "uploads", type);
                Directory.CreateDirectory(pathUpload);

                string fileName = Guid.NewGuid() + "_" + Path.GetFileName(_formFile.FileName);
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), pathUpload, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await _formFile.CopyToAsync(stream); // ✅ phải dùng await
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return $"error_{Guid.NewGuid().ToString()}_{_formFile.FileName}";
            }
        }

    }
}
