using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;

namespace BulletinBoard.Data.Helpers
{
    public interface IImageFormHelper
    {
        public List<IFormFile> ImageToFormFile(Post post);
        public List<Image> FormFileToImage(List<IFormFile> files, string path);
        public Task CheckExistedImagesAsync(Post post, PostVM model, AppDbContext _context, IFileService _fileService);
    }
}
