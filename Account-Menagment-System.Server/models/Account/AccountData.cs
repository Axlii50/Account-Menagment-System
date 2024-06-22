namespace Account_Menagment_System.Server.models.Account
{
    public class AccountData
    {
        public Guid Id { get; set; }

        public static implicit operator Guid(AccountData model)
        {
            return model.Id;
        }
    }
}
