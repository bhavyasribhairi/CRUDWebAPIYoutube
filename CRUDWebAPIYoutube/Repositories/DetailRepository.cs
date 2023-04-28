using AutoMapper;
using CRUDWebAPIYoutube.Data;
using CRUDWebAPIYoutube.Models;
using CRUDWebAPIYoutube.Models.DTO;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Linq;

namespace CRUDWebAPIYoutube.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper Mapper;

        public DetailRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            Mapper = mapper;
        }



        public async Task<IEnumerable<Details>> GetDetails(string? filterOn,
            string? filterQuery, string? sortBy, bool? isAscending, int? pageNumber , int? pageSize)
        {
            var details = dbContext.Details.Include("Contact").AsQueryable();


            //Filtering
            if (filterOn == "PanNumber")
            {
                details = details.Where(x => x.PanNumber.Equals(filterQuery));
            }
            if (filterOn == "FullName")
            {
                details = details.Where(x => x.contact.FullName.Contains(filterQuery));
            }
            //Sorting
            if (!string.IsNullOrEmpty(sortBy) && sortBy.Equals("FullName") && isAscending == true)
            {
                details = details.OrderBy(x => x.contact.FullName);
            }
            else
            {
                details = details.OrderByDescending(x => x.contact.FullName);
            }

            //Pagination
            if (pageNumber != 0 && pageNumber!= null)
            {
                var skipResults = (pageNumber - 1) * pageSize;
                return details.Skip((int)skipResults).Take((int)pageSize).ToList();

            }
            return details;



        }

        public async Task<bool> InsertDetails(DetailsDTO detailsDTO)
        {

            var detailsToEnter = new Details()
            {
                Id = Guid.NewGuid(),
                ContactId = Guid.NewGuid(),
                PanNumber = detailsDTO.PanNumber,
                AadharNumber = detailsDTO.AadharNumber,
                contact = detailsDTO.contact
                
            };
            detailsToEnter.contact.Id = detailsToEnter.ContactId;
                
            
            //detailsToEnter= Mapper.Map<Details>(detailsDTO);
            await dbContext.Details.AddAsync(detailsToEnter);
            var result = await dbContext.SaveChangesAsync();
            return true;

        }
    }
}
