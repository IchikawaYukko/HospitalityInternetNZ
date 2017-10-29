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

        public Form_ticket() {
            InitializeComponent();

            this.hNZauth = new HospitalityNZauth();

            //ticket list
            tickets = new BindingList<WiFiTicket>();
            //can be edit by data grid view
            tickets.AllowNew = true;
            tickets.AllowEdit = true;
            tickets.AllowRemove = true;

            ticketGridView.DataSource = this.tickets;

            tickets.Add(new WiFiTicket("t5t5@h", "5n8k782h"));  // TODO for debug. remove later.
        }

        private void button_logout_Click(object sender, EventArgs e) {
            if (hNZauth.CheckLoggedIn()) {
                // Try logout
                hNZauth.Logout();
                //Console.WriteLine("Logged out.");
            } else {
                //Console.WriteLine("You are not logged in.");
            }
        }

        private void button_addticket_Click(object sender, EventArgs e) {
        }

        private void button_deleteticket_Click(object sender, EventArgs e) {
            var p = 0; // temp
        }

        private void button_login_Click(object sender, EventArgs e) {
            if (this.tickets.Count == 0) {
                this.label_conn_status.Text = "No tickets registered. Please add ticket on above list.";
                return;
            }

            // Check Logged in or not
            if (hNZauth.CheckLoggedIn()) {
                this.label_conn_status.Text = "You are already logged in.";
                hNZauth.LoadState(STATE_FILENAME);
                //PrintUsage(hNZauth.CheckUsage());
            } else {
                // Try login
                hNZauth.Login(this.tickets[0]);

                hNZauth.SaveState(STATE_FILENAME);
                this.label_conn_status.Text = FormatUsage(hNZauth.CheckUsage());
            }
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
    }
}
