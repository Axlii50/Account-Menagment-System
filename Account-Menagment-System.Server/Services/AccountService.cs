using Account_Menagment_System.Server.Data;
using Account_Menagment_System.Server.models.Account;
using Account_Menagment_System.Server.models.database.Account;
using Account_Menagment_System.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Login = Account_Menagment_System.Server.models.Account.Login;

namespace Account_Menagment_System.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly Account_Menagment_SystemServerContext context;

        public AccountService(Account_Menagment_SystemServerContext context)
        {
            this.context = context;
        }

        public async Task<Account?> GetAccount(Login login)
        {
            return await context.Account.FirstOrDefaultAsync(acc => acc.Password == login.Password && acc.Login == login.UserName);
        }

        public async Task<Account?> GetAccount(Guid id)
        {
            return await context.Account.FindAsync(id);
        }

        public async Task<Account?> GetAccount(string login)
        {
            return await context.Account.FirstOrDefaultAsync(acc => acc.Login == login);
        }

        public async Task<AccountDTO> ChangeState(Guid id, bool state)
        {
            var account = await GetAccount(id);

            account.Active = state;

            context.Update(account);
            await context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountDTO[]> GetAccounts()
        {
           return await context.Account.Where(acc => !acc.IsAdmin).Select(acc => (AccountDTO)acc).ToArrayAsync();
        }
    }
}
