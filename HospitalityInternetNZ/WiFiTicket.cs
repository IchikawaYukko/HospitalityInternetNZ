using System;

namespace HospitalityInternetNZ {
    // TODO move another file
    class WiFiTicket {
        public string username { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public enum TicketStatus {
            WRONG_ID_OR_PASS,
            USING,
            UNUSED,
            EXPIRED
        }
        public WiFiTicket() {
        }
        public WiFiTicket(string user, string pass) {
            this.username = user;
            this.password = pass;

            if (!username.Contains("@")) {
                // Username must include @.
                this.status = (int)WiFiTicket.TicketStatus.WRONG_ID_OR_PASS;
            }
        }
    }
}
