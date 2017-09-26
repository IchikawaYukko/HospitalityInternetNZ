using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;

namespace HospitalityInternetNZ {
    class HospitalityNZauth {
        private string _user;
        private string _pass;
        private HttpClient _client;
        private HttpClientHandler _handler;
        private CookieCollection _session_cookie;
        private string _usage_check_url;
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
                BaseAddress = new Uri(BASE_URL)
            };

        }

        // Check PC connected to right Wi-Fi Access point
        public bool CheckConnected() {
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


        public bool CheckLoggedIn() {
            try {
                // Try access to initial URL
                _client.Timeout = TimeSpan.FromMilliseconds(LOGIN_CHECK_TIMEOUT);
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

        public Dictionary<string, string> CheckUsage() {
            var r = _client.GetAsync(_usage_check_url).Result;
            var state = new Dictionary<string, string>();
            var result = r.Content.ReadAsStringAsync().Result;

            state.Add("msg", result.SubStringByToken("msg = \"", "\""));
            state.Add("byteamount", result.SubStringByToken("byteamount = \"", "\""));
            state.Add("session", result.SubStringByToken("session = \"", "\""));
            state.Add("umac", result.SubStringByToken("umac = \"", "\""));
            state.Add("unicodeusername", result.SubStringByToken("unicodeusername = \"", "\""));
            state.Add("sessionlength", result.SubStringByToken("sessionlength = \"", "\""));
            state.Add("chargetype", result.SubStringByToken("chargetype = \"", "\""));

            return state;
        }

        public void Login(string username, string password) {   // TODO Use WiFiTicket instead string
            this._user = username;  // HACK: no needed?
            this._pass = password;

            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                {"myusername", _user},
                {"mypassword", _pass}
            });

            try {
                // TODO clear old session cookie in _handler.CookieContainer
                _handler.CookieContainer.Add(new Uri(BASE_URL), _session_cookie);
                var r = _client.PostAsync(LOGIN_URL, content).Result;

                var result = r.Content.ReadAsStringAsync().Result;

                //Error handlings
                if (result.Contains("Can%20not%20read%20data%20from%20Cookie")) {
                    Console.WriteLine("Can not read data from Cookie");     // HACK
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Invalid%20username%20or%20password")) {
                    Console.WriteLine("Invalid username or password");

                    // TODO throw exception
                }
                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Your%20have%20run%20out%20of%20your%20qouta")) {
                    Console.WriteLine("Your have run out of your qouta");
                }

                // save the URL for later uses.
                _usage_check_url = BASE_URL + result.Substring(
                    result.IndexOf("/loginpages"),
                    result.LastIndexOf("\"") - result.IndexOf("/loginpages")
                );
            } catch (Exception e) {
                Console.WriteLine(e.InnerException.ToString());
            }
        }

        public void Logout() {
            var r = _client.GetAsync(LOGOUT_URL).Result;
        }

        //Save Session cookie and Usage check URL.
        public void SaveState(string filename) {
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
            }catch (FileNotFoundException e) {
                // TODO
            }
        }
    }
}
