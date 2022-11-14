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
                            FolderName = "uploads",
                        });

                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return images;
        }

        public void Delete(List<Image> images)
        {
            foreach (var file in images)
            {
                File.Delete(@"C:\Users\Admin\Desktop\ds\BulletinBoard\BulletinBoard\wwwroot\uploads\" + file.Name);
            }
        }

        public List<IFormFile> ImageToFormFile(Post post)
        {
            var img = new List<IFormFile>();
            foreach (var a in post.Images)
            {
                string path = "/wwwroot/uploads/" + a.Name;
                img.Add(new FormFile(null, 0, 0, a.Name, a.Name));
            }
            return img;
        }
    }
}
