using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dagable.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Claim> GetLoggedInUser()
        {
            var user = _httpContextAccessor.HttpContext.User.Claims;
            var id = _httpContextAccessor.HttpContext.User.Identity.Name;
            return user;
        }
    }
}
