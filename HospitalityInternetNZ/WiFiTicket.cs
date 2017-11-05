using System;

namespace HospitalityInternetNZ {
    // TODO move another file
    public class WiFiTicket {
        public string username { get; set; }
        public string password { get; set; }
        public TicketStatus status { get; set; }
        public enum TicketStatus {
            UNUSED,
            WRONG_ID_OR_PASS,
            USING,
            EXPIRED
        }

        public WiFiTicket() {
        }
        public WiFiTicket(string user, string pass) {
            this.username = user;
            this.password = pass;

            if (!username.Contains("@")) {
                // Username must include @.
                this.status = WiFiTicket.TicketStatus.WRONG_ID_OR_PASS;
            }
        }
    }
}
