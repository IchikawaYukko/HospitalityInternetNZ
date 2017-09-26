using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
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
        const string STATE_FILENAME = "test.cookie";

        static void Main(string[] args)
        {
            if (args.Length == 0) {
                System.Console.WriteLine(HELP_MSG);
                Environment.Exit(1);
            }
            var tickets = new List<WiFiTicket>();

            var hNZauth = new HospitalityNZauth();

            if (hNZauth.CheckConnected()) {
                switch (args[0]) {
                    case "login":   //login 46rf@h t632xgvm
                        if (args.Length != 3) {
                            Console.WriteLine("Please specify username and password.\n\nExample: login 1234@h passw0rd");
                            Environment.Exit(1);
                        }

                        // Check Logged in or not
                        if (hNZauth.CheckLoggedIn()) {
                            Console.WriteLine("You are already logged in.");
                        } else {
                            // Try login
                            hNZauth.Login(args[1], args[2]);

                            hNZauth.SaveState(STATE_FILENAME);
                            Console.WriteLine(hNZauth.CheckUsage());
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
                            // Check remaining usage(MBs or Times)
                            hNZauth.LoadState(STATE_FILENAME);
                            var state = hNZauth.CheckUsage();

                            Console.WriteLine("Logged in as:" + state["unicodeusername"]);
                            Console.WriteLine("Charge Type: " + state["chargetype"]);
                            Console.WriteLine("Remains : " + state["msg"]);
                            Console.WriteLine("Remains [data]: " + state["byteamount"] + "bytes");
                            Console.WriteLine("Remains [time]: " + state["sessionlength"] + "sec");
                            Console.WriteLine("Session Cookie: " + state["session"]);
                            Console.WriteLine("Registerd MAC address: " + state["umac"]);
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

        //private static checkArgs(string[] arg) {
        //}
    }

    // TODO move another file
    class WiFiTicket {
        public readonly string username;
        public readonly string password;
        public int status;
        public enum TicketStatus {
            WRONG_ID_OR_PASS,
            USING,
            UNUSED,
            EXPIRED
        }

        public WiFiTicket(string user, string pass) {
            this.username = user;
            this.password = pass;

            if (!username.Contains("@")) {
                // Username must include @.
                this.status = (int) WiFiTicket.TicketStatus.WRONG_ID_OR_PASS;
            }
        }
    }
    public static class StringExtentions {
        public static string SubStringByToken(this string text, string startToken, string endToken) {
            var start = text.IndexOf(startToken) + startToken.Length;
            var end   = text.IndexOf(endToken, start);

            return text.Substring(start, end - start);
        }
    }
}
