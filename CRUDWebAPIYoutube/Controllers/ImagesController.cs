using CRUDWebAPIYoutube.BusinessLogic;
using CRUDWebAPIYoutube.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebAPIYoutube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageWebBridge imageWebBridge;
        private readonly ILogger<ImagesController> logger;

        public ImagesController(ImageWebBridge imageWebBridge, ILogger<ImagesController> logger)
        {
            this.imageWebBridge = imageWebBridge;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDTO imageUploadRequestDTO)
        {
            logger.LogInformation("Request is accepted");
            
            if (imageUploadRequestDTO != null)
            {
                logger.LogInformation("Null check passed");
                var result = await imageWebBridge.ValidateModel(imageUploadRequestDTO);

                return Ok(result);

            }
            return BadRequest("Please upload a file and details");

        }

    }
}

