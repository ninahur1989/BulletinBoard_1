using BulletinBoard.Data.Static;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Data.Helpers
{
    public class ImageFormHelper : IImageFormHelper
    {
        public List<IFormFile> ImageToFormFile(Post post)
        {
            var img = new List<IFormFile>();

            foreach (var a in post.Images)
            {
                img.Add(new FormFile(null, 0, 0, a.Name, a.Name));
            }

            return img;
        }

        public List<Image> FormFileToImage(List<IFormFile> files, string path)
        {
            var result = new List<Image>(ImageLimit.ImageLimitPerPost);
            foreach (var file in files)
            {
                result.Add(new Image()
                {
                    Name = file.Name,
                    FolderName = path,
                });
            }

            return result;
        }

        public async Task CheckExistedImagesAsync(Post post, PostVM model, AppDbContext _context, IFileService _fileService)
        {
            try
            {
                var fileExistedDBImages = ImageToFormFile(post);

                var addedToExisted = new List<IFormFile>(ImageLimit.ImageLimitPerPost);

                var newListFiles = new List<IFormFile>(ImageLimit.ImageLimitPerPost);

                var viewExistedImage = new List<IFormFile>(ImageLimit.ImageLimitPerPost);

                if (model.ExistedImage != null)
                {
                    for (int i = 0; i < model.ExistedImage.Count; i++)
                    {
                        for (int j = 0; j < fileExistedDBImages.Count; j++)
                        {
                            if (model.ExistedImage[i].FileName == "preview" + j + "input")
                            {
                                viewExistedImage.Add(fileExistedDBImages[j]);
                                continue;
                            }
                            else if (j == fileExistedDBImages.Count - 1)
                            {
                                addedToExisted.Add(model.ExistedImage[i]);
                            }
                        }
                    }
                }

                foreach (IFormFile file in viewExistedImage)
                    fileExistedDBImages.Remove(file);

                var imageExistedDBImages = FormFileToImage(fileExistedDBImages, @"uploads\");

                foreach (var image in imageExistedDBImages)
                {
                    var imageDB = await _context.Images.FirstOrDefaultAsync(x => x.Name == image.Name);
                    if (imageDB != null)
                        _context.Images.Remove(imageDB);
                }
                await _context.SaveChangesAsync();

                _fileService.Delete(imageExistedDBImages);

                if (addedToExisted != null)
                    newListFiles.AddRange(addedToExisted);

                if (model.ImageFile != null)
                    newListFiles.AddRange(model.ImageFile);

                if (newListFiles.Count > 0)
                {
                    var newListImages = await _fileService.UploadAsync(newListFiles, post.UserId);
                    post.Images.AddRange(newListImages);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
