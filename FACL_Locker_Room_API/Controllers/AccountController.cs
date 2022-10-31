using FACL_Locker_Room_Core.Interface;
using FACL_Locker_Room_Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FACL_Locker_Room_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _account;

        public AccountController(IAccountService Account)
        {
            _account = Account;

        }

        // Get cuurent Apiversion from Appsettings
        [HttpGet("GetCurrentVersion")]
        public async Task<IActionResult> GetCurrentVersionAsync()
        {
            var response = await _account.GetApiVersion();
            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        // Create Account
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountModel Model)
        {
            var response = await _account.CreatAccountAsync(Model);
            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Get Account
        [HttpPost("GetAccount")]
        public async Task<IActionResult> GetAccount([FromBody] GetAccountModel Model)
        {
            var response = await _account.GetAccountAsync(Model);
            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


    }
}
