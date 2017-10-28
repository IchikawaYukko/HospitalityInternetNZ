namespace HospitalityInternetNZ_GUI {
    partial class Form1 {
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
            this.button_addticket = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_deleteticket = new System.Windows.Forms.Button();
            this.button_login = new System.Windows.Forms.Button();
            this.button_logout = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ticketGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ticketGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button_addticket
            // 
            this.button_addticket.Location = new System.Drawing.Point(60, 189);
            this.button_addticket.Name = "button_addticket";
            this.button_addticket.Size = new System.Drawing.Size(75, 23);
            this.button_addticket.TabIndex = 0;
            this.button_addticket.Text = "デバッグ1";
            this.button_addticket.UseVisualStyleBackColor = true;
            this.button_addticket.Click += new System.EventHandler(this.button_addticket_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(130, 340);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_deleteticket
            // 
            this.button_deleteticket.Location = new System.Drawing.Point(188, 189);
            this.button_deleteticket.Name = "button_deleteticket";
            this.button_deleteticket.Size = new System.Drawing.Size(75, 23);
            this.button_deleteticket.TabIndex = 2;
            this.button_deleteticket.Text = "デバッグ2";
            this.button_deleteticket.UseVisualStyleBackColor = true;
            this.button_deleteticket.Click += new System.EventHandler(this.button_deleteticket_Click);
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(60, 287);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 23);
            this.button_login.TabIndex = 3;
            this.button_login.Text = "接続";
            this.button_login.UseVisualStyleBackColor = true;
            // 
            // button_logout
            // 
            this.button_logout.Location = new System.Drawing.Point(188, 287);
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
            this.ticketGridView.Location = new System.Drawing.Point(50, 33);
            this.ticketGridView.Name = "ticketGridView";
            this.ticketGridView.RowTemplate.Height = 21;
            this.ticketGridView.Size = new System.Drawing.Size(269, 150);
            this.ticketGridView.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "接続状態";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "チケット一覧";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 375);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ticketGridView);
            this.Controls.Add(this.button_logout);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.button_deleteticket);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_addticket);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ticketGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_addticket;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_deleteticket;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView ticketGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}

