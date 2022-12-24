using Dagable.Api.Core.User;

namespace Dagable.DataAccess
{
    public interface IUserRepository
    {
        /// <summary>
        /// Method used to obtain the user settings for a provided user id
        /// </summary>
        /// <param name="userId">The guid of the user we want to get the settings of</param>
        /// <returns>The user settings for the given user id</returns>
        Task<UserSettingsDTO> GetUserSettings(Guid userId);

        /// <summary>
        /// Method used to update the user settings
        /// </summary>
        /// <param name="userId">the userId to assign the settings to</param>
        /// <param name="userSettings">The user settings to be saved</param>
        /// <returns>the updated user settings</returns>
        Task<UserSettingsDTO> UpdateUserSettings(Guid userId, UserSettingsDTO userSettings);
    }
}