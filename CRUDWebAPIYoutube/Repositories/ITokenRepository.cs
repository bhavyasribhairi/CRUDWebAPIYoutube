using Microsoft.AspNetCore.Identity;

namespace CRUDWebAPIYoutube.Repositories
{
    public interface ITokenRepository
    {

       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
