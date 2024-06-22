using Account_Menagment_System.Server.models.database.Account;

namespace Account_Menagment_System.Server.models.Account
{
    public class ChangeStateAccount
    {
        public Guid ID { get; set; }
        public bool State { get; set; }

        public static implicit operator Guid(ChangeStateAccount model)
        {
            return model.ID;
        }

        public static implicit operator bool(ChangeStateAccount model)
        {
            return model.State;
        }
    }
}
