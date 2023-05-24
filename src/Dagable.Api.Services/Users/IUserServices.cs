using Dagable.Api.Core.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dagable.Api.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Method used to obtain the user settings for the current user
        /// </summary>
        /// <returns>the user settings for the current user</returns>
        Task<UserSettingsDTO> GetUserSettings();

        /// <summary>
        /// Method used to update the current users settings
        /// </summary>
        /// <param name="updatedValues">the updated values for this current users settings</param>
        /// <returns>the newly updated user settings</returns>
        Task<UserSettingsDTO> UpdateUserSettings(UserSettingsDTO updatedValues);

        /// <summary>
        /// Obtains the current userId from the current httpContext. Obtains from them bearer token nameIdentifier
        /// </summary>
        /// <returns>The current userId of the authenticated user</returns>
        /// <exception cref="ArgumentNullException"></exception>
        Guid GetLoggedInUserId();

        /// <summary>
        /// For a specific user, obtains a list of the information for all the graph objects. 
        /// </summary>
        /// <returns>A list of graph information for all of a users graphs</returns>
        Task<List<UserGraphsHeadingDTO>> GetUserGraphs();
    }
}