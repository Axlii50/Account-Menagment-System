using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Account_Menagment_System.Server.Data;
using Account_Menagment_System.Server.models.database.Account;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Account_Menagment_System.Server.Services;
using Account_Menagment_System.Server.models.Account;
using Account_Menagment_System.Server.Services.Interfaces;
using System.Diagnostics;

namespace Account_Menagment_System.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            if (loginModel == null) return BadRequest();

            var account = await accountService.GetAccount(loginModel);

            if(account == null) return NotFound("Account not found");

            return Json((AccountDTO)account);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStateAccount([FromBody] ChangeStateAccount changeStateAccount)
        {
            if (changeStateAccount == null) return BadRequest();

            return Json(await accountService.ChangeState(changeStateAccount, changeStateAccount));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStateBot([FromBody] ChangeStateAccount changeStateAccount)
        {
            if (changeStateAccount == null) return BadRequest();

            return Json(await accountService.ChangeStateBot(changeStateAccount, changeStateAccount));
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountData([FromBody] AccountData accountData)
        {
            if (accountData == null) return BadRequest();

            var account = await accountService.GetAccount(accountData);

            if (account == null) return NotFound("Account not found");

            return Json((AccountDTO)account);
        }

        [HttpGet]
        public async Task<IActionResult> GetRDPAccountData([Bind("accountLogin")]string accountLogin)
        {
            if (accountLogin == string.Empty) return BadRequest();

            var account = await accountService.GetAccount(accountLogin);

            if (account == null) return NotFound("Account not found");

            return Json((AccountRDPDTO)account);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountsData([FromBody] AccountData accountData)
        {
            if (accountData == null) return BadRequest();

            var account = await accountService.GetAccount(accountData);

            if (account == null) return NotFound("Account not found");
            if(!account.IsAdmin) return BadRequest("Account is not Admin");

            return Json(await accountService.GetAccounts());
        }

        [HttpGet]
        public async Task<IActionResult> IsBotActive([Bind("accountLogin")] string accountLogin)
        {
            if (accountLogin == string.Empty) return BadRequest();

            var account = await accountService.GetAccount(accountLogin);

            if (account == null) return NotFound("Account not found");

            Debug.WriteLine(account.BotState);
            Debug.WriteLine(account.BotExpirationDate <= DateTime.Now);

            return Json(new { State = account.IsBotActive});
        }
    }
}
