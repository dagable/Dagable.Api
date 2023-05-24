using Dagable.Api.Core.User;
using Dagable.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dagable.Api.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IGraphsRepository _graphsRepository;

        public UserServices(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IGraphsRepository graphsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _graphsRepository = graphsRepository;
        }

        /// <inheritdoc cref="IUserServices.GetLoggedInUserId"/>
        public Guid GetLoggedInUserId()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return new Guid(user);
        }

        /// <inheritdoc cref="IUserServices.GetUserGraphs"/>
        public async Task<List<UserGraphsHeadingDTO>> GetUserGraphs()
        {
            var graphs = await _graphsRepository.FindAllGraphsForUser(GetLoggedInUserId());
            return graphs.Select(x => new UserGraphsHeadingDTO { CreatedOn = x.CreatedAt, Name = string.IsNullOrEmpty(x.Name) ? x.GraphGuid.ToString() : x.Name, Description = x.Description, GraphGuid = x.GraphGuid }).ToList();
        }

        /// <inheritdoc cref="IUserServices.GetUserSettings"/>
        public async Task<UserSettingsDTO> GetUserSettings()
        {
            return await _userRepository.GetUserSettings(GetLoggedInUserId());
        }

        /// <inheritdoc cref="IUserServices.UpdateUserSettings(UserSettingsDTO)"/>
        public async Task<UserSettingsDTO> UpdateUserSettings(UserSettingsDTO updatedValues)
        {
            return await _userRepository.UpdateUserSettings(GetLoggedInUserId(), updatedValues);
        }
    }
}
