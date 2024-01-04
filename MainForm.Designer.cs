
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
            this.lstProtectedCatgories = new System.Windows.Forms.ListBox();
            this.lblProtectedCategories = new System.Windows.Forms.Label();
            this.btnAddProtected = new System.Windows.Forms.Button();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.btnNaskban = new System.Windows.Forms.Button();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.Location = new System.Drawing.Point(130, 78);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(74, 22);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "ورود";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(14, 50);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPassword.Size = new System.Drawing.Size(192, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(250, 52);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(43, 13);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "گذرواژه:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(14, 26);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.Size = new System.Drawing.Size(192, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(210, 26);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(89, 13);
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
            this.grpLogin.Location = new System.Drawing.Point(8, 18);
            this.grpLogin.Margin = new System.Windows.Forms.Padding(2);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Padding = new System.Windows.Forms.Padding(2);
            this.grpLogin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpLogin.Size = new System.Drawing.Size(334, 126);
            this.grpLogin.TabIndex = 10;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "ورود";
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(8, 162);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(2);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(334, 34);
            this.btnApprove.TabIndex = 11;
            this.btnApprove.Text = "تأیید دسته‌ای پیشنهادهای من برای ارتباطات گنجینه گنجور";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 210);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "0";
            // 
            // btnApproveEdits
            // 
            this.btnApproveEdits.Location = new System.Drawing.Point(6, 270);
            this.btnApproveEdits.Margin = new System.Windows.Forms.Padding(2);
            this.btnApproveEdits.Name = "btnApproveEdits";
            this.btnApproveEdits.Size = new System.Drawing.Size(334, 34);
            this.btnApproveEdits.TabIndex = 13;
            this.btnApproveEdits.Text = "تأیید دسته‌ای ویرایش‌های کژدم و سایر کاربران مورد اعتماد";
            this.btnApproveEdits.UseVisualStyleBackColor = true;
            this.btnApproveEdits.Click += new System.EventHandler(this.btnApproveEdits_Click);
            // 
            // btnApproveMetres
            // 
            this.btnApproveMetres.Location = new System.Drawing.Point(8, 312);
            this.btnApproveMetres.Margin = new System.Windows.Forms.Padding(2);
            this.btnApproveMetres.Name = "btnApproveMetres";
            this.btnApproveMetres.Size = new System.Drawing.Size(334, 34);
            this.btnApproveMetres.TabIndex = 14;
            this.btnApproveMetres.Text = "تأیید دسته‌ای وزنیابی‌های سیستمی";
            this.btnApproveMetres.UseVisualStyleBackColor = true;
            this.btnApproveMetres.Click += new System.EventHandler(this.btnApproveMetres_Click);
            // 
            // lstTrustedUsers
            // 
            this.lstTrustedUsers.FormattingEnabled = true;
            this.lstTrustedUsers.Location = new System.Drawing.Point(347, 57);
            this.lstTrustedUsers.Name = "lstTrustedUsers";
            this.lstTrustedUsers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstTrustedUsers.Size = new System.Drawing.Size(290, 277);
            this.lstTrustedUsers.TabIndex = 15;
            this.lstTrustedUsers.DoubleClick += new System.EventHandler(this.lstTrustedUsers_DoubleClickAsync);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(402, 31);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserId.Size = new System.Drawing.Size(235, 20);
            this.txtUserId.TabIndex = 16;
            // 
            // btnAddTrusted
            // 
            this.btnAddTrusted.Location = new System.Drawing.Point(372, 29);
            this.btnAddTrusted.Name = "btnAddTrusted";
            this.btnAddTrusted.Size = new System.Drawing.Size(24, 23);
            this.btnAddTrusted.TabIndex = 17;
            this.btnAddTrusted.Text = "+";
            this.btnAddTrusted.UseVisualStyleBackColor = true;
            this.btnAddTrusted.Click += new System.EventHandler(this.btnAddTrusted_Click);
            // 
            // btnDelTrusted
            // 
            this.btnDelTrusted.Location = new System.Drawing.Point(347, 29);
            this.btnDelTrusted.Name = "btnDelTrusted";
            this.btnDelTrusted.Size = new System.Drawing.Size(24, 23);
            this.btnDelTrusted.TabIndex = 18;
            this.btnDelTrusted.Text = "-";
            this.btnDelTrusted.UseVisualStyleBackColor = true;
            this.btnDelTrusted.Click += new System.EventHandler(this.btnDelTrusted_Click);
            // 
            // lstProtectedCatgories
            // 
            this.lstProtectedCatgories.FormattingEnabled = true;
            this.lstProtectedCatgories.Location = new System.Drawing.Point(10, 390);
            this.lstProtectedCatgories.Name = "lstProtectedCatgories";
            this.lstProtectedCatgories.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstProtectedCatgories.Size = new System.Drawing.Size(626, 56);
            this.lstProtectedCatgories.TabIndex = 19;
            this.lstProtectedCatgories.DoubleClick += new System.EventHandler(this.lstProtectedCatgories_DoubleClick);
            // 
            // lblProtectedCategories
            // 
            this.lblProtectedCategories.AutoSize = true;
            this.lblProtectedCategories.Location = new System.Drawing.Point(14, 368);
            this.lblProtectedCategories.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProtectedCategories.Name = "lblProtectedCategories";
            this.lblProtectedCategories.Size = new System.Drawing.Size(119, 13);
            this.lblProtectedCategories.TabIndex = 20;
            this.lblProtectedCategories.Text = "بخش‌های محافظت شده";
            // 
            // btnAddProtected
            // 
            this.btnAddProtected.Location = new System.Drawing.Point(611, 363);
            this.btnAddProtected.Name = "btnAddProtected";
            this.btnAddProtected.Size = new System.Drawing.Size(24, 23);
            this.btnAddProtected.TabIndex = 21;
            this.btnAddProtected.Text = "+";
            this.btnAddProtected.UseVisualStyleBackColor = true;
            this.btnAddProtected.Click += new System.EventHandler(this.btnAddProtected_Click);
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.Location = new System.Drawing.Point(372, 366);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCategoryId.Size = new System.Drawing.Size(235, 20);
            this.txtCategoryId.TabIndex = 22;
            // 
            // btnNaskban
            // 
            this.btnNaskban.Location = new System.Drawing.Point(10, 464);
            this.btnNaskban.Margin = new System.Windows.Forms.Padding(2);
            this.btnNaskban.Name = "btnNaskban";
            this.btnNaskban.Size = new System.Drawing.Size(624, 34);
            this.btnNaskban.TabIndex = 23;
            this.btnNaskban.Text = "درج ارتباطات تأیید شدهٔ نسک‌بان";
            this.btnNaskban.UseVisualStyleBackColor = true;
            this.btnNaskban.Click += new System.EventHandler(this.btnNaskban_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(647, 514);
            this.Controls.Add(this.btnNaskban);
            this.Controls.Add(this.txtCategoryId);
            this.Controls.Add(this.btnAddProtected);
            this.Controls.Add(this.lblProtectedCategories);
            this.Controls.Add(this.lstProtectedCatgories);
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
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.ListBox lstProtectedCatgories;
        private System.Windows.Forms.Label lblProtectedCategories;
        private System.Windows.Forms.Button btnAddProtected;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.Button btnNaskban;
    }
}

