using CRUDWebAPIYoutube.Models;
using CRUDWebAPIYoutube.Models.DTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CRUDWebAPIYoutube.Repositories
{
    public interface IDetailRepository
    {

        public  Task<IEnumerable<Details>> GetDetails(string? filterOn = null, 
            string? filterQuery = null, string? sortBy =null, bool? isAscending = true,
            int? pageNumber=1 ,int? pageSize=1000);
        Task<bool> InsertDetails(DetailsDTO detailsDTO);
    }
}
