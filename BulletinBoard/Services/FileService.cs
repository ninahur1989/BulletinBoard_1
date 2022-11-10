using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;

namespace BulletinBoard.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<Image>> UploadAsync(IList<IFormFile> files, string userId)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            List<Image> images = new List<Image>();
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    string newName = userId + Guid.NewGuid() + file.FileName;
                    string filePath = Path.Combine(uploads, newName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        images.Add(new Image()
                        {
                            Name = newName,
                            Path = filePath,
                        });

                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return images;
        }
    }
}
