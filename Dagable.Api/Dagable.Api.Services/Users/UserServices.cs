using Dagable.Api.Core.User;
using Dagable.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dagable.Api.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserServices(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        private Guid GetLoggedInUserId()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new Guid(user);
        }

        public async Task<UserSettingsDTO> GetUserSettings()
        {
            return await _userRepository.GetUserSettings(GetLoggedInUserId());
        }

        public async Task<UserSettingsDTO> UpdateUserSettings(UserSettingsDTO updatedValues)
        {
            return await _userRepository.UpdateUserSettings(GetLoggedInUserId(), updatedValues);
        }
    }
}
