using CRUDWebAPIYoutube.Data;
using CRUDWebAPIYoutube.Models;
using CRUDWebAPIYoutube.Models.DTO;
using CRUDWebAPIYoutube.Repositories;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPIYoutube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DetailsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDetailRepository detailRepository;

        public DetailsController(ApplicationDbContext dbContext, IDetailRepository detailRepository)
        {
            this.dbContext = dbContext;
            this.detailRepository = detailRepository;
        }
        [HttpGet]
        [Route("getDetails")]
        [Authorize(Roles ="Reader")]
        public async Task<IEnumerable<Details>> GetDetails([FromQuery]string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            return await detailRepository.GetDetails(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
        }


        [HttpPost]
        [Route("addDetails")]
        //[Authorize(Roles = "Writer")]
        public async Task<bool> InsertDetails(DetailsDTO detailsDTO)
        {
            return await detailRepository.InsertDetails(detailsDTO);
            

        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        public async Task<bool> UpdateDetails()
        {
            return true;
        }

        [HttpDelete]
        [Authorize(Roles = "WRITER")]
        public async Task<bool> DeleteDetails()
        {

            return true;
        }
    }
}
