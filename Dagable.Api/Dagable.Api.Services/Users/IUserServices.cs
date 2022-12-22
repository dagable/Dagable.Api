using Dagable.Api.Core.User;
using System.Threading.Tasks;

namespace Dagable.Api.Services
{
    public interface IUserServices
    {
        Task<UserSettingsDTO> GetUserSettings();

        Task<UserSettingsDTO> UpdateUserSettings(UserSettingsDTO updatedValues);
    }
}