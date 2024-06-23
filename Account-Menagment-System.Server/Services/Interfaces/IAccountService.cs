using Account_Menagment_System.Server.models.Account;
using Account_Menagment_System.Server.models.database.Account;

namespace Account_Menagment_System.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccount(Login login);
        Task<Account> GetAccount(Guid id);
        Task<AccountDTO> ChangeState(Guid id, bool state);
        Task<AccountDTO[]> GetAccounts();
    }
}