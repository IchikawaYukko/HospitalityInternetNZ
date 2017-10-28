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
    public partial class Form1 : Form {
        HospitalityNZauth hNZauth;
        BindingList<WiFiTicket> tickets;

        public Form1() {
            InitializeComponent();

            this.hNZauth = new HospitalityNZauth();
            tickets = new BindingList<WiFiTicket>();
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
    }
}
