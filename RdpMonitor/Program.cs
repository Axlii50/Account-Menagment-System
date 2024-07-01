using Microsoft.Win32;
using Newtonsoft.Json;
using RdpMonitor;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceProcess;

partial class Program
{
    

    static void Main()
    {
       var worker =  new BackGroundWorker();

        while(true)
        {
            worker.Timer_Elapsed();

            Thread.Sleep(600000);
        }
        
    }
}


