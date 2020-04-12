using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Sharp3389
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: execute-assembly Sharp3389.exe [options]");
                Console.WriteLine("\r");
                Console.WriteLine("Options：");
                Console.WriteLine("     -query          Query RDP status And port");
                Console.WriteLine("     -rdp0 or 1      Open/Close RDP port (0:on|1:off)");
            }
            
            if (args.Length == 1 && args[0] == "-query")
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey rdpstatus = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", RegistryKeyPermissionCheck.ReadSubTree);
                Console.WriteLine("Current status：" + rdpstatus.GetValue("fDenyTSConnections").ToString());

                RegistryKey rdpport = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", RegistryKeyPermissionCheck.ReadSubTree);
                Console.WriteLine("Current port：" + rdpport.GetValue("PortNumber").ToString());
                hklm.Close();
                rdpstatus.Close();
                rdpport.Close();
            }

            if (args.Length == 1 && args[0] == "-rdp0")
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey rdpopen = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true);
                rdpopen.SetValue("fDenyTSConnections", "0", RegistryValueKind.DWord);

                string sValue = rdpopen.GetValue("fDenyTSConnections").ToString();
                int value = Convert.ToInt32(sValue);
                if (value == 0)
                {
                    Console.WriteLine("Successfully，Current status：" + sValue);
                }         
                hklm.Close();
                rdpopen.Close();
            }

            if (args.Length == 1 && args[0] == "-rdp1")
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey rdpclose = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true);
                rdpclose.SetValue("fDenyTSConnections", "1", RegistryValueKind.DWord);

                string sValue = rdpclose.GetValue("fDenyTSConnections").ToString();
                int value = Convert.ToInt32(sValue);
                if (value == 1)
                {
                    Console.WriteLine("Successfully，Current status：" + sValue);
                }
                hklm.Close();
                rdpclose.Close();
            }
        }
    }
}