using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace RdpMonitor
{
    internal class BackGroundWorker
    {

        public BackGroundWorker()
        {
            #region Temp
            //var query = new EventLogQuery("Security", PathType.LogName, "*[System/EventID=4624] and *[EventData[Data[@Name='LogonType']='10']]");
            //var watcher = new EventLogWatcher(query);
            //watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(OnEventRecordWritten);
            //watcher.Enabled = true; 
            #endregion


            Logger.WriteLine("Monitoring RDP connections. Press Enter to exit.");
        }

        public async void Timer_Elapsed()
        {
            var accounts = RetrieveAllAccounts();

            Logger.WriteLine("Accounts:  ");
            foreach(var account in accounts)
            {
                Logger.WriteLine(account.ToString());
            }

            foreach (var account in accounts)
            {
                var accountData = await Api.GetAccountRDPInfo(account);
                //RdpHelper.MonitorRam(account, 9999999);

                if (accountData == null)
                {
                    Console.WriteLine(account + "    nie zostało znalezione!");
                    continue;
                }

                RdpHelper.DisableAccount(account, accountData.IsActive);

                if (!accountData.IsAdmin)
                    RdpHelper.MonitorRam(account, accountData.RamAmountInMB);
                Logger.WriteLine(" ");
            }
        }

        string[] RetrieveAllAccounts()
        {
            List<string> accounts = new List<string>();

            try
            {
                // Connect to the local machine
                DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);

                // Get the children of the directory entry (all users and groups)
                foreach (DirectoryEntry child in localMachine.Children)
                {
                    // Check if the entry is a user
                    if (child.SchemaClassName == "User")
                    {
                        accounts.Add(child.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine("An error occurred: " + ex.Message);
            }

            return accounts.ToArray();
        }

        //private static void OnEventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        //{
        //    if (e.EventRecord != null)
        //    {
        //        var properties = e.EventRecord.Properties;
        //        var username = properties[5].Value.ToString(); // Index 5 should correspond to the TargetUserName

        //        Logger.WriteLine($"Detected RDP logon for user: {username}");

        //        var data = Api.GetAccountRDPInfo(username).Result;

        //        Logger.WriteLine($"{data.IsActive}    {data.RamAmountInMB}  {data.IsAdmin}   {data.ThreadCount}");


        //        if (!data.IsActive)
        //        {
        //            RdpHelper.LogOff(username);
        //        }
        //        else if (!data.IsAdmin)
        //        {
        //            Logger.WriteLine($"Ustawiam zasoby dla: {username}  ->     {data.RamAmountInMB}    {data.ThreadCount}");
        //            //RdpHelper.SetResourceLimits(username, data.ThreadCount, data.RamAmountInMB);
        //        }
        //    }
        //}
        
    }
}
