using Authenticate.Models;

namespace Authenticate.Services.IServices
{
    public interface ITokenService
    {
        string createToken(AppUser user, IEnumerable<string> roles);
    }
}
