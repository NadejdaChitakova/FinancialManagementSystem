using FinancialManagement.Entities;
using FinancialManagement.Interfaces;
using FinancialManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("Accounts/GetAccount")]
        public async Task<IActionResult> Get([FromHeader] int accountId)
        {
            var account = await _accountService.GetAccount(accountId);

            return Ok(account);
        }

        [HttpPost]
        [Route("Accounts/CreateAccount")]
        public async Task<IActionResult> Create([FromBody] AccountResource request)
        {
            await _accountService.CreateAccount(request);

            return Ok();
        }

        [HttpPut]
        [Route("Accounts/UpdateAccount")]
        public async Task<IActionResult> UpdateTransaction([FromBody] AccountResource request)
        {
            await _accountService.UpdateAccount(request);

            return Ok();
        }

        [HttpDelete]
        [Route("Accounts/RemoveAccount")]
        public async Task<IActionResult> DeleteTransaction([FromHeader] int accountId)
        {
            await _accountService.DeleteAccount(accountId);

            return Ok();
        }
    }
}
