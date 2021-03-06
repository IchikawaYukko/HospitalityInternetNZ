﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;

namespace HospitalityInternetNZ {
    class HospitalityNZauth {
        private HttpClient _client;
        private HttpClientHandler _handler;
        private CookieCollection _session_cookie;
        private string _usage_check_url;
        public readonly string TEMPDIR_PATH;
        const string AUTH_SERVER_ADDR = "10.0.0.100";
        const string INIT_URL = "http://2.2.2.2/";
        const string LOGOUT_URL = "http://1.1.1.1/";
        const string BASE_URL = "http://" + AUTH_SERVER_ADDR + "/";
        const string LOGIN_URL = BASE_URL + "loginpages/userlogin.shtml";
        const string DNS_URL = BASE_URL + "loginpages/dns.shtml";
        const double LOGIN_CHECK_TIMEOUT = 5000; //ms

        public HospitalityNZauth() {
            _handler = new HttpClientHandler() {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            _client = new HttpClient(_handler) {
                BaseAddress = new Uri(BASE_URL),
                Timeout = TimeSpan.FromMilliseconds(LOGIN_CHECK_TIMEOUT)
            };

            // Get and check exist temp directory.
            if (System.IO.Directory.Exists(Path.GetTempPath())) {
                this.TEMPDIR_PATH = Path.GetTempPath();
            } else {
                this.TEMPDIR_PATH = "";
            }
        }

        // Check PC connected to right Wi-Fi Access point
        public bool CheckConnected() {  // HACK to async class
            using (var p = new Ping()) {
                try {
                    PingReply reply = p.Send(AUTH_SERVER_ADDR);

                    if (reply.Status == IPStatus.Success) {
                        return true;
                    } else {
                        return false;
                    }
                } catch (PingException) {
                    // Ping sending failed
                    return false;
                }
            }

        }


        public bool CheckLoggedIn() {   // HACK to async class
            try {
                // Try access to initial URL
                var r = _client.GetAsync(INIT_URL).Result;

                // get and save session cookie
                var cookie = _handler.CookieContainer;
                _session_cookie = cookie.GetCookies(new Uri(BASE_URL + "loginpages/dns.shtml"));

                return false;
            } catch (AggregateException e) {
                //If timed out accessing initial URL, It has already logged in
                if (!(e.InnerException is System.Threading.Tasks.TaskCanceledException)) {
                    Console.WriteLine(e.InnerException.ToString());
                    throw;
                }
                
                return true;
            }
        }

        // Check remaining usage(MBs or Times)
        public Dictionary<string, string> CheckUsage() {    // HACK to async class
            string result = "";
            var state = new Dictionary<string, string>();

            try {
                var r = _client.GetAsync(_usage_check_url).Result;
                result = r.Content.ReadAsStringAsync().Result;
            } catch (AggregateException e) {
                //Ignore TaskCanceledException. it means to timed out accessing URL
                if (!(e.InnerException is System.Threading.Tasks.TaskCanceledException)) {
                    Console.WriteLine(e.InnerException.ToString());
                    throw;
                }
            }

            // ignore Administrator Login Page
            if (result.Contains("Welcome To Administrator Login Page")) {
                state.Add("msg", "");
                state.Add("byteamount", "");
                state.Add("session", "");
                state.Add("umac", "");
                state.Add("unicodeusername", "");
                state.Add("sessionlength", "");
                state.Add("chargetype", "");

                return state;
            }

            // parse result text
            state.Add("msg", result.SubStringByToken("msg = \"", "\""));
            state.Add("byteamount", result.SubStringByToken("byteamount = \"", "\""));
            state.Add("session", result.SubStringByToken("session = \"", "\""));
            state.Add("umac", result.SubStringByToken("umac = \"", "\""));
            state.Add("unicodeusername", result.SubStringByToken("unicodeusername = \"", "\""));
            state.Add("sessionlength", result.SubStringByToken("sessionlength = \"", "\""));
            state.Add("chargetype", result.SubStringByToken("chargetype = \"", "\""));

            return state;
            /*
Logged in as:46rf@h
Charge Type: DATA
Remains : The user is offline.
Remains [data]: 301356540bytes
Remains [time]: 0sec
Session Cookie: FVS74HO7pIOfpHO7rGeDuJNUoTRH4HeL1MfL1IOD1HvT1Ie/1He7hYBgoUOvrJN3tIb067
Registerd MAC address: 24:FD:52:3F:61:20
*/
        }

        public void Login(WiFiTicket ticket) {  // HACK to async class
            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"myusername", ticket.username},
                {"mypassword", ticket.password}
            });

            try {
                _handler.CookieContainer.Add(new Uri(BASE_URL), _session_cookie);
                var r = _client.PostAsync(LOGIN_URL, content).Result;

                var result = r.Content.ReadAsStringAsync().Result;

                //Error handlings
                var unknown_error = true;   // HACK
                if (result.Contains("Can%20not%20read%20data%20from%20Cookie")) {
                    // TODO throw exception
                    Console.WriteLine("Can not read data from Cookie");     // HACK
                    unknown_error = false;
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Invalid%20username%20or%20password")) {
                    unknown_error = false;
                    throw new LoginFailedException("Invalid username or password");
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Your%20have%20run%20out%20of%20your%20qouta")) {
                    unknown_error = false;
                    throw new LoginFailedException("Your have run out of your qouta");
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("The%20account%20is%20expired")) {
                    unknown_error = false;
                    throw new LoginFailedException("The account is expired");
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("loginpages/popup1.shtml")) {
                    //when success page, reset flag
                    unknown_error = false;
                }

                // TODO "Deny login because you have loggedin the system via the other machine."

                if (unknown_error) {
                    throw new LoginFailedException(r.RequestMessage.RequestUri.AbsoluteUri + result);
                }

                // save the URL for later uses.
                _usage_check_url = BASE_URL + "loginpages" + result.SubStringByToken("/loginpages", "\"");
            } catch (LoginFailedException) {
                throw;
            } catch (Exception e) {
                Console.WriteLine(e.InnerException.ToString());
            }
        }

        public void Logout() {  // HACK to async class
            var r = _client.GetAsync(LOGOUT_URL).Result;
        }

        //Save Session cookie and Usage check URL.
        public void SaveState(string filename) {
            // TODO set hidden attibute to session file.
            using ( var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            using ( var sw = new StreamWriter(fs)) {
                foreach (Cookie cc in this._session_cookie) {
                    sw.WriteLine(cc.ToString());
                }
                sw.WriteLine(this._usage_check_url);
            }
        }

        //Load saved states.
        public void LoadState(string filename) {
            try {
                using ( var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                using ( var sr = new StreamReader(fs)) {
                    string line;
                    var cookie = new Cookie();

                    for (var i = 0; i < 2; i++) {
                        line = sr.ReadLine();
                        cookie.Name = line.Substring(0, line.LastIndexOf("="));
                        cookie.Value = line.Substring(line.IndexOf("=") + 1);

                        this._handler.CookieContainer.Add(new Uri(BASE_URL), cookie);
                    }

                    this._usage_check_url = sr.ReadLine();
                } 
            }catch (FileNotFoundException) {
                // TODO
            }
        }
    }

    public static class StringExtentions {
        public static string SubStringByToken(this string text, string startToken, string endToken) {
            var start = text.IndexOf(startToken) + startToken.Length;
            var end = text.IndexOf(endToken, start);

            return text.Substring(start, end - start);
        }
    }

    class LoginFailedException : Exception {
        public LoginFailedException() {
        }

        public LoginFailedException(string message)
            : base(message)
        {

        }
    }
}

