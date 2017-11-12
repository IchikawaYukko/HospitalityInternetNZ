namespace HospitalityInternetNZ_GUI {
    partial class Form_ticket {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.button_debug1 = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_debug2 = new System.Windows.Forms.Button();
            this.button_login = new System.Windows.Forms.Button();
            this.button_logout = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ticketGridView = new System.Windows.Forms.DataGridView();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_conn_status = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ticketGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button_debug1
            // 
            this.button_debug1.Location = new System.Drawing.Point(287, 122);
            this.button_debug1.Name = "button_debug1";
            this.button_debug1.Size = new System.Drawing.Size(75, 23);
            this.button_debug1.TabIndex = 0;
            this.button_debug1.Text = "デバッグ1";
            this.button_debug1.UseVisualStyleBackColor = true;
            this.button_debug1.Click += new System.EventHandler(this.button_debug1_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(344, 99);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_debug2
            // 
            this.button_debug2.Location = new System.Drawing.Point(303, 70);
            this.button_debug2.Name = "button_debug2";
            this.button_debug2.Size = new System.Drawing.Size(75, 23);
            this.button_debug2.TabIndex = 2;
            this.button_debug2.Text = "デバッグ2";
            this.button_debug2.UseVisualStyleBackColor = true;
            this.button_debug2.Click += new System.EventHandler(this.button_debug2_Click);
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(328, 151);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 23);
            this.button_login.TabIndex = 3;
            this.button_login.Text = "接続";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // button_logout
            // 
            this.button_logout.Location = new System.Drawing.Point(328, 215);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(75, 23);
            this.button_logout.TabIndex = 4;
            this.button_logout.Text = "切断";
            this.button_logout.UseVisualStyleBackColor = true;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ticketGridView
            // 
            this.ticketGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ticketGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.username,
            this.password,
            this.status});
            this.ticketGridView.Location = new System.Drawing.Point(12, 24);
            this.ticketGridView.Name = "ticketGridView";
            this.ticketGridView.RowTemplate.Height = 21;
            this.ticketGridView.Size = new System.Drawing.Size(269, 150);
            this.ticketGridView.TabIndex = 5;
            this.ticketGridView.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.ticketGridView_RowValidated);
            this.ticketGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ticketGridView_UserDeletedRow);
            // 
            // username
            // 
            this.username.DataPropertyName = "username";
            this.username.HeaderText = "ユーザー名";
            this.username.Name = "username";
            this.username.Width = 80;
            // 
            // password
            // 
            this.password.DataPropertyName = "password";
            this.password.HeaderText = "パスワード";
            this.password.Name = "password";
            this.password.Width = 80;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状態";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "接続状態";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "チケット一覧";
            // 
            // label_conn_status
            // 
            this.label_conn_status.AutoSize = true;
            this.label_conn_status.Location = new System.Drawing.Point(30, 226);
            this.label_conn_status.Name = "label_conn_status";
            this.label_conn_status.Size = new System.Drawing.Size(87, 12);
            this.label_conn_status.TabIndex = 8;
            this.label_conn_status.Text = "connstatus_here";
            // 
            // Form_ticket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 375);
            this.Controls.Add(this.label_conn_status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ticketGridView);
            this.Controls.Add(this.button_logout);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.button_debug2);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_debug1);
            this.Name = "Form_ticket";
            this.Text = "チケット状態";
            ((System.ComponentModel.ISupportInitialize)(this.ticketGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_debug1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_debug2;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView ticketGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.Label label_conn_status;
        private System.Windows.Forms.Timer timer1;
    }
}

