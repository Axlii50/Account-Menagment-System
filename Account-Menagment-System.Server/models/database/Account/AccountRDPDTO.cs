﻿namespace Account_Menagment_System.Server.models.database.Account
{
    public class AccountRDPDTO
    {
        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
        public bool IsBotActive { get; set; }

        public int ThreadCount { get; set; }
        public int RamAmountInMB { get; set; }
    }
}
