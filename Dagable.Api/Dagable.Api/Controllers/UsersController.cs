using Dagable.Api.Core.User;
using Dagable.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dagable.Api.Controllers
{
    [ApiController, Route("[controller]"), Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet, Route("me/my-graphs")]
        public async Task<IActionResult> MyGraphs()
        {
            return Ok(await _userServices.GetUserGraphs()); 
        }

        [HttpGet, Route("me")]
        public async Task<IActionResult> Me()
        {
            return Ok(await _userServices.GetUserSettings());
        }

        [HttpPost, Route("me/update-settings")]
        public async Task<IActionResult> UpdateSettings(UserSettingsDTO userSettings)
        {
            return Ok(await _userServices.UpdateUserSettings(userSettings));
        }
    }
}
