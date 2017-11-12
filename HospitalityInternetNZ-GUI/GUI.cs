using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalityInternetNZ_GUI {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Exception logger (for debug)
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionThrowed);

            try {
                Application.Run(new Form_ticket());
            } catch (Exception e) {
                var p = e;  // TODO for debug. remove.
            }
        }

        // Exception logger (for debug)
        static void UnhandledExceptionThrowed(object sender, UnhandledExceptionEventArgs e) {
            try {
                MessageBox.Show(((Exception)e.ExceptionObject).Message, "Error");
            } finally {
                //Environment.Exit(1);
            }
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            try {
                MessageBox.Show(e.Exception.Message, "Error");
            } finally {
                //Application.Exit();
            }
        }
    }
}
