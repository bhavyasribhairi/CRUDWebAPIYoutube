using CRUDWebAPIYoutube.Controllers;
using CRUDWebAPIYoutube.Models;
using CRUDWebAPIYoutube.Models.DTO;
using CRUDWebAPIYoutube.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages.Html;

namespace CRUDWebAPIYoutube.BusinessLogic
{
    public class ImageWebBridge :ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILogger<ImagesController> logger;

        public ImageWebBridge(IImageRepository imageRepository, ILogger<ImagesController> logger)
        {
            this.imageRepository = imageRepository;
            this.logger = logger;
        }
        public Task<Image> ValidateModel(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", "jpeg", "png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDTO.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extensions ");
                //return false;

            }
            logger.LogInformation("Image is under allowed extensions");
            if (imageUploadRequestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB");
                //return false;
            }
            logger.LogInformation("Image file size accepted");
            var image = new Image
            {
                Id = Guid.NewGuid(),
                File = imageUploadRequestDTO.File,
                FileName= imageUploadRequestDTO.FileName,
                FileDescription= imageUploadRequestDTO.FileDescription,
                FileExtension= Path.GetExtension(imageUploadRequestDTO.File.FileName),
                FileSizeInBytes = imageUploadRequestDTO.File.Length,
            };

            return imageRepository.Upload(image);

        }




    }
}
