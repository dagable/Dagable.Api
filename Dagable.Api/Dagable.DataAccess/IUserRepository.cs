using Dagable.Api.Core.User;

namespace Dagable.DataAccess
{
    public interface IUserRepository
    {
        Task<UserSettingsDTO> GetUserSettings(Guid userId);
        Task<UserSettingsDTO> UpdateUserSettings(Guid userId, UserSettingsDTO userSettings);
    }
}