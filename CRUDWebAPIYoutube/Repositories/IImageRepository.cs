using CRUDWebAPIYoutube.Models;

namespace CRUDWebAPIYoutube.Repositories
{
    public interface IImageRepository
    {

        Task<Image> Upload(Image image);
    }
}
