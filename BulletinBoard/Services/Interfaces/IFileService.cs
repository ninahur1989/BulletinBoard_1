using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IFileService
    {
        public Task<List<Image>> UploadAsync(IList<IFormFile> files, string userId);
        public void Delete(List<Image> images);
    }
}
