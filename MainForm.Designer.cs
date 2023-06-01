
namespace BatchGanjoorLinkApprover
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.btnApprove = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnApproveEdits = new System.Windows.Forms.Button();
            this.btnApproveMetres = new System.Windows.Forms.Button();
            this.lstTrustedUsers = new System.Windows.Forms.ListBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.btnAddTrusted = new System.Windows.Forms.Button();
            this.btnDelTrusted = new System.Windows.Forms.Button();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.Location = new System.Drawing.Point(260, 156);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(148, 44);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "ورود";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(28, 100);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPassword.Size = new System.Drawing.Size(380, 33);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(500, 104);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(83, 27);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "گذرواژه:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(28, 52);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.Size = new System.Drawing.Size(380, 33);
            this.txtEmail.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(420, 52);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(174, 27);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "پست الکترونیکی:";
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.lblEmail);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Controls.Add(this.txtEmail);
            this.grpLogin.Controls.Add(this.txtPassword);
            this.grpLogin.Controls.Add(this.lblPassword);
            this.grpLogin.Location = new System.Drawing.Point(16, 36);
            this.grpLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLogin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpLogin.Size = new System.Drawing.Size(668, 252);
            this.grpLogin.TabIndex = 10;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "ورود";
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(16, 324);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(668, 68);
            this.btnApprove.TabIndex = 11;
            this.btnApprove.Text = "تأیید دسته‌ای پیشنهادهای من";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(16, 420);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 27);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "0";
            // 
            // btnApproveEdits
            // 
            this.btnApproveEdits.Location = new System.Drawing.Point(12, 540);
            this.btnApproveEdits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApproveEdits.Name = "btnApproveEdits";
            this.btnApproveEdits.Size = new System.Drawing.Size(668, 68);
            this.btnApproveEdits.TabIndex = 13;
            this.btnApproveEdits.Text = "تأیید دسته‌ای ویرایش‌های کژدم و سایر کاربران مورد اعتماد";
            this.btnApproveEdits.UseVisualStyleBackColor = true;
            this.btnApproveEdits.Click += new System.EventHandler(this.btnApproveEdits_Click);
            // 
            // btnApproveMetres
            // 
            this.btnApproveMetres.Location = new System.Drawing.Point(16, 648);
            this.btnApproveMetres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApproveMetres.Name = "btnApproveMetres";
            this.btnApproveMetres.Size = new System.Drawing.Size(668, 68);
            this.btnApproveMetres.TabIndex = 14;
            this.btnApproveMetres.Text = "تأیید دسته‌ای وزنیابی‌های سیستمی";
            this.btnApproveMetres.UseVisualStyleBackColor = true;
            this.btnApproveMetres.Click += new System.EventHandler(this.btnApproveMetres_Click);
            // 
            // lstTrustedUsers
            // 
            this.lstTrustedUsers.FormattingEnabled = true;
            this.lstTrustedUsers.ItemHeight = 25;
            this.lstTrustedUsers.Location = new System.Drawing.Point(694, 114);
            this.lstTrustedUsers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lstTrustedUsers.Name = "lstTrustedUsers";
            this.lstTrustedUsers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstTrustedUsers.Size = new System.Drawing.Size(576, 579);
            this.lstTrustedUsers.TabIndex = 15;
            this.lstTrustedUsers.DoubleClick += new System.EventHandler(this.lstTrustedUsers_DoubleClickAsync);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(804, 62);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserId.Size = new System.Drawing.Size(466, 33);
            this.txtUserId.TabIndex = 16;
            // 
            // btnAddTrusted
            // 
            this.btnAddTrusted.Location = new System.Drawing.Point(744, 58);
            this.btnAddTrusted.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddTrusted.Name = "btnAddTrusted";
            this.btnAddTrusted.Size = new System.Drawing.Size(48, 46);
            this.btnAddTrusted.TabIndex = 17;
            this.btnAddTrusted.Text = "+";
            this.btnAddTrusted.UseVisualStyleBackColor = true;
            this.btnAddTrusted.Click += new System.EventHandler(this.btnAddTrusted_Click);
            // 
            // btnDelTrusted
            // 
            this.btnDelTrusted.Location = new System.Drawing.Point(694, 58);
            this.btnDelTrusted.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDelTrusted.Name = "btnDelTrusted";
            this.btnDelTrusted.Size = new System.Drawing.Size(48, 46);
            this.btnDelTrusted.TabIndex = 18;
            this.btnDelTrusted.Text = "-";
            this.btnDelTrusted.UseVisualStyleBackColor = true;
            this.btnDelTrusted.Click += new System.EventHandler(this.btnDelTrusted_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1300, 790);
            this.Controls.Add(this.btnDelTrusted);
            this.Controls.Add(this.btnAddTrusted);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.lstTrustedUsers);
            this.Controls.Add(this.btnApproveMetres);
            this.Controls.Add(this.btnApproveEdits);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.grpLogin);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تأییدکنندهٔ دسته‌ای تصاویر مرتبط";
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnApproveEdits;
        private System.Windows.Forms.Button btnApproveMetres;
        private System.Windows.Forms.ListBox lstTrustedUsers;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Button btnAddTrusted;
        private System.Windows.Forms.Button btnDelTrusted;
    }
}

