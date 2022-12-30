using Dagable.Api.Core.User;
using Dagable.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Dagable.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DagableDbContext _dbContext;

        public UserRepository(DagableDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IUserRepository.GetUserSettings(Guid)"/>
        public async Task<UserSettingsDTO> GetUserSettings(Guid userId)
        {
            var userSettings = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId);
            if(userSettings == null) { 
                return new UserSettingsDTO();
            }
            return new UserSettingsDTO
            {
                NodeShape = userSettings.NodeStyle,
                NodeColor = userSettings.NodeColor,
                UseVerticalLayout = userSettings.IsVerticalLayout
            };
        }

        /// <inheritdoc cref="IUserRepository.UpdateUserSettings(Guid, UserSettingsDTO)"/>
        public async Task<UserSettingsDTO> UpdateUserSettings(Guid userId, UserSettingsDTO userSettings) {
            var dbUserSettings = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId);
            if(dbUserSettings == null)
            {
                await _dbContext.UserSettings.AddAsync(new Migrations.DbModels.UserSettings()
                {
                    UserId = userId,
                    NodeStyle = userSettings.NodeShape,
                    NodeColor = userSettings.NodeColor,
                    IsVerticalLayout = userSettings.UseVerticalLayout,
                    User = new Migrations.DbModels.User()
                    {
                        Id = userId,
                        DisplayName = "unset"
                    }
                });
                await _dbContext.SaveChangesAsync();
                return userSettings;
            }
            dbUserSettings.IsVerticalLayout = userSettings.UseVerticalLayout;
            dbUserSettings.NodeColor= userSettings.NodeColor;
            dbUserSettings.NodeStyle= userSettings.NodeShape;
            await _dbContext.SaveChangesAsync();
            return userSettings;
        }
    }
}
