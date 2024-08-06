namespace Account_Menagment_System.Server.models.database.Account
{
    public class AccountDTO : IIdentification
    {
        public Guid ID { get; set; }

        public string Login { get; set; }

        public bool IsActive { get; set; }

        public bool IsBotActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
