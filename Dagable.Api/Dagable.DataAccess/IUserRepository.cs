using System.Security.Claims;

namespace Dagable.DataAccess
{
    public interface IUserRepository
    {
        IEnumerable<Claim> GetLoggedInUser();
    }
}