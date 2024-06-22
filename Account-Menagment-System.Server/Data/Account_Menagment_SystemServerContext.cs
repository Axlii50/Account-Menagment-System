using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_Menagment_System.Server.models.database.Account;
using Microsoft.EntityFrameworkCore;

namespace Account_Menagment_System.Server.Data
{
    public class Account_Menagment_SystemServerContext : DbContext
    {
        public Account_Menagment_SystemServerContext (DbContextOptions<Account_Menagment_SystemServerContext> options)
            : base(options)
        {
        }

        public DbSet<Account_Menagment_System.Server.models.database.Account.Account> Account { get; set; } = default!;
    }
}
