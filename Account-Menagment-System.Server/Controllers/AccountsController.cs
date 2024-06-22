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

        [HttpGet]
        public async Task<IActionResult> GetAccountData([FromBody] AccountData accountData)
        {
            if (accountData == null) return BadRequest();

            var account = await accountService.GetAccount(accountData);

            if (account == null) return NotFound("Account not found");

            return Json((AccountDTO)account);
        }
    }
}
