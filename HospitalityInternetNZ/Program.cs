using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace HospitalityInternetNZ
{
    class Program
    {
        const string VERSION = "1.0.0-alpha"; // TODO Application.ProductVersion;
        const string HELP_MSG =
@"Login Helper for HospitalityInternetNZ based Wi-Fi system
Version:" + VERSION +
@"

Usage:
HospitalityInternetNZ <sub-command>

Sub-commands:
  login Username Password  Try login.
  logout                   Try logout.
  stat                     Same as status.
  status                   Show connection details.

MIT License.";
        const string STATE_FILENAME = "hnzauth_session.cookie";

        static void Main(string[] args)
        {
            if (args.Length == 0) {
                System.Console.WriteLine(HELP_MSG);
                Environment.Exit(1);
            }
            var hNZauth = new HospitalityNZauth();

            if (hNZauth.CheckConnected()) {
                switch (args[0]) {
                    case "login":
                        if (args.Length != 3) {
                            Console.WriteLine("Please specify username and password.\n\nExample: login 1234@h passw0rd");
                            Environment.Exit(1);
                        }

                        // Check Logged in or not
                        if (hNZauth.CheckLoggedIn()) {
                            Console.WriteLine("You are already logged in.");
                            hNZauth.LoadState(hNZauth.TEMPDIR_PATH + STATE_FILENAME);
                            PrintUsage(hNZauth.CheckUsage());
                        } else {
                            // Try login
                            var ticket = new WiFiTicket(args[1], args[2]);
                            try {
                                hNZauth.Login(ticket);
                            } catch (LoginFailedException e) {
                                Console.WriteLine(e.Message);
                            }

                            hNZauth.SaveState(hNZauth.TEMPDIR_PATH + STATE_FILENAME);
                            PrintUsage(hNZauth.CheckUsage());
                        }
                        break;
                    case "logout":
                        if (hNZauth.CheckLoggedIn()) {
                            // Try logout
                            hNZauth.Logout();
                            Console.WriteLine("Logged out.");
                        } else {
                            Console.WriteLine("You are not logged in.");
                        }
                        break;
                    case "stat":
                    case "status":
                        if (hNZauth.CheckLoggedIn()) {
                            hNZauth.LoadState(hNZauth.TEMPDIR_PATH + STATE_FILENAME);
                            PrintUsage(hNZauth.CheckUsage());
                        } else {
                            Console.WriteLine("You are not logged in.");
                        }
                        break;
                    default:
                        Console.WriteLine(args[0] + ": No such sub-command");
                        break;
                }
            } else {
                Console.WriteLine("Not connected valid access point. Please check connection.");
            }
        }

        // format state message
        private static void PrintUsage(IDictionary<string, string> state) {
            Console.WriteLine("Logged in as:" + state["unicodeusername"]);
            Console.WriteLine("Charge Type: " + state["chargetype"]);
            Console.WriteLine("Remains : " + state["msg"]);
            Console.WriteLine("Remains [data]: " + state["byteamount"] + "bytes");
            Console.WriteLine("Remains [time]: " + state["sessionlength"] + "sec");
            Console.WriteLine("Session Cookie: " + state["session"]);
            Console.WriteLine("Registered MAC address: " + state["umac"]);
        }
    }
}
