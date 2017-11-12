using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalityInternetNZ;

namespace HospitalityInternetNZ_GUI {
    public partial class Form_ticket : Form {
        HospitalityNZauth hNZauth;
        BindingList<WiFiTicket> tickets;
        const string STATE_FILENAME = "session.cookie"; // HACK shouldn't hard code. or use temp dir.
        const string TICKET_FILENAME = "tickets.xml";

        public Form_ticket() {
            InitializeComponent();

            this.hNZauth = new HospitalityNZauth();

            LoadTickets();
            //can be edit by data grid view
            tickets.AllowNew = true;
            tickets.AllowEdit = true;
            tickets.AllowRemove = true;

            ticketGridView.DataSource = this.tickets;
        }

        private async void button_logout_Click(object sender, EventArgs e) {
            if (hNZauth.CheckLoggedIn()) {
                // Try logout
                await Task.Run(() => {  // HACK
                    hNZauth.Logout();
                });
                this.label_conn_status.Text = "Logged out.";
            } else {
                this.label_conn_status.Text = "You are not logged in.";
            }
        }

        private void button_debug1_Click(object sender, EventArgs e) {
            
        }

        private void button_debug2_Click(object sender, EventArgs e) {
            var p = 0; // debug
        }

        private async void button_login_Click(object sender, EventArgs e) {
            if (this.tickets.Count == 0) {
                this.label_conn_status.Text = "No tickets registered. Please add ticket on above list.";
                return;
            }

            await Login(this.tickets[0]);
        }

        private async Task Login(WiFiTicket t) {
            // Check Logged in or not
            bool loggedin = false;
            await Task.Run(() => {  // HACK
                loggedin = hNZauth.CheckLoggedIn();
            });
            if (loggedin) {
                this.label_conn_status.Text = "You are already logged in.";
                hNZauth.LoadState(STATE_FILENAME);
                //PrintUsage(hNZauth.CheckUsage());
            } else {
                // Try login
                hNZauth.Login(t);

                hNZauth.SaveState(STATE_FILENAME);
                this.label_conn_status.Text = FormatUsage(hNZauth.CheckUsage());
            }
        }

        //Save tickets to XML
        private void SaveTickets() {
            List<WiFiTicket> ticket_list = this.tickets.ToList<WiFiTicket>();

            System.Xml.Serialization.XmlSerializer ser =
                new System.Xml.Serialization.XmlSerializer(
                    typeof(List<WiFiTicket>));
            using (
                System.IO.StreamWriter sw = new System.IO.StreamWriter
                    (TICKET_FILENAME, false, new System.Text.UTF8Encoding(false))
            ) {
                ser.Serialize(sw, ticket_list);
                sw.Close();
            }
        }

        //Load tickets from XML
        private void LoadTickets() {
            var ticket_list = new List<WiFiTicket>();
            using (var sr = new System.IO.StreamReader(TICKET_FILENAME, new System.Text.UTF8Encoding(false))) {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(List<WiFiTicket>));
                ticket_list = (List<WiFiTicket>) ser.Deserialize(sr);
                sr.Close();
            }
            BindingList<WiFiTicket> ticket_bdlist = new BindingList<WiFiTicket>(ticket_list);
            this.tickets = ticket_bdlist;
        }

        // format state message
        private static string FormatUsage(IDictionary<string, string> state) {
            string message;

            message = "Logged in as:" + state["unicodeusername"];
            message += "\nCharge Type: " + state["chargetype"];
            message += "\nRemains : " + state["msg"];
            message += "\nRemains [data]: " + state["byteamount"] + "bytes";
            message += "\nRemains [time]: " + state["sessionlength"] + "sec";
            message += "\nSession Cookie: " + state["session"];
            message += "\nRegistered MAC address: " + state["umac"];

            return message;
        }

        //Save tickets when new ticket added.
        private void ticketGridView_RowValidated(object sender, DataGridViewCellEventArgs e) {
            SaveTickets();
        }

        //Save tickets when ticket deleted.
        private void ticketGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
            SaveTickets();
        }
    }
}
