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
                var r = _client.GetAsync(INIT_URL).Result;
                var cookie = _handler.CookieContainer;
                _session_cookie = cookie.GetCookies(new Uri(BASE_URL + "loginpages/dns.shtml"));
                //foreach (Cookie c in session) {
                //    Console.WriteLine(c.Name);
                //    Console.WriteLine(c.Value);
                //}
                return false;
            } catch (Exception e) {
                //If timed out accessing initial URL, It has already logged in
                if (!(e.InnerException.InnerException.InnerException is System.Net.Sockets.SocketException)) {
                    Console.WriteLine(e.InnerException.ToString());
                }
                return true;
            }
        }

        public string CheckUsage() {
            var r = _client.GetAsync(_usage_check_url).Result;

            var result = r.Content.ReadAsStringAsync().Result;

            // HACK
            var start = result.IndexOf("msg = \"") + "msg = \"".Length;
            var end = result.IndexOf("\";", start);
            return  result.Substring(start, end - start); // TODO return Dictionary instead string.
            //var byte = temp2.Substring(r.Content.ReadAsStringAsync().Result.IndexOf("byteamount = \"")
        }

        public void Login(string username, string password) {
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

                Console.WriteLine(r);
                if (result.Contains("Can%20not%20read%20data%20from%20Cookie")) {
                    Console.WriteLine("Can not read data from Cookie");     // HACK
                }

                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Invalid%20username%20or%20password")) {
                    Console.WriteLine("Invalid username or password");

                    // throw
                }

                if (r.RequestMessage.RequestUri.AbsoluteUri.Contains("Your%20have%20run%20out%20of%20your%20qouta")) {
                    Console.WriteLine("Your have run out of your qouta");
                }

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

        public void LoadState(string filename) {
            try {
                using ( var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                using ( var sr = new StreamReader(fs)) {

                } 
            }catch (FileNotFoundException e) {
            }
        }
    }
}
