using CRUDWebAPIYoutube.Controllers;
using CRUDWebAPIYoutube.Data;
using CRUDWebAPIYoutube.Models;

namespace CRUDWebAPIYoutube.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<ImagesController> logger;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext, ILogger<ImagesController> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
        }
        async Task<Image> IImageRepository.Upload(Image image)
        {

            logger.LogInformation("Processing LocalPath...");
            var localFilePath = Path.Combine(
                webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}"
                );


            //iploading image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            logger.LogInformation("FileSaved in local path! ");

            //

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;


            //add images to table 

            await applicationDbContext.Images.AddAsync(image);
            await applicationDbContext.SaveChangesAsync();
            logger.LogInformation("FileSaved in Database");

            return image;
        }
    }
}
