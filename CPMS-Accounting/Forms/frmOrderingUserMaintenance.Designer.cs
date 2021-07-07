
namespace CPMS_Accounting.Forms
{
    partial class frmOrderingUserMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderingUserMaintenance));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.cbUserLevel = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDeparment = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.btnDeleteRecord = new System.Windows.Forms.Button();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.btnCancelClose = new System.Windows.Forms.Button();
            this.btnRefreshView = new System.Windows.Forms.Button();
            this.gbUserId = new System.Windows.Forms.GroupBox();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.gbUserId.SuspendLayout();
            this.gbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CPMS_Accounting.Properties.Resources.Header_Login;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-4, -4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1090, 82);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // gbDetails
            // 
            this.gbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetails.BackColor = System.Drawing.Color.Transparent;
            this.gbDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbDetails.Controls.Add(this.cbUserLevel);
            this.gbDetails.Controls.Add(this.label12);
            this.gbDetails.Controls.Add(this.cbDeparment);
            this.gbDetails.Controls.Add(this.label11);
            this.gbDetails.Controls.Add(this.txtConfirmPassword);
            this.gbDetails.Controls.Add(this.label10);
            this.gbDetails.Controls.Add(this.txtPassword);
            this.gbDetails.Controls.Add(this.label9);
            this.gbDetails.Controls.Add(this.txtPosition);
            this.gbDetails.Controls.Add(this.label8);
            this.gbDetails.Controls.Add(this.txtSuffix);
            this.gbDetails.Controls.Add(this.label3);
            this.gbDetails.Controls.Add(this.txtLastName);
            this.gbDetails.Controls.Add(this.label2);
            this.gbDetails.Controls.Add(this.txtMiddleName);
            this.gbDetails.Controls.Add(this.label1);
            this.gbDetails.Controls.Add(this.txtFirstName);
            this.gbDetails.Controls.Add(this.lblFirstName);
            this.gbDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbDetails.Location = new System.Drawing.Point(12, 183);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(661, 355);
            this.gbDetails.TabIndex = 40;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "DETAILS";
            // 
            // cbUserLevel
            // 
            this.cbUserLevel.FormattingEnabled = true;
            this.cbUserLevel.Items.AddRange(new object[] {
            "USER",
            "SUPERVISOR",
            "MANAGER",
            "ADMINISTRATOR"});
            this.cbUserLevel.Location = new System.Drawing.Point(114, 93);
            this.cbUserLevel.Name = "cbUserLevel";
            this.cbUserLevel.Size = new System.Drawing.Size(146, 21);
            this.cbUserLevel.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(21, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "USER LEVEL:   ";
            // 
            // cbDeparment
            // 
            this.cbDeparment.FormattingEnabled = true;
            this.cbDeparment.Items.AddRange(new object[] {
            "DEVELOPMENT",
            "ACCOUNTING",
            "SALES",
            "HR",
            "CHECKTRONIC",
            "SECUR",
            "CHECKERS",
            "PACKERS"});
            this.cbDeparment.Location = new System.Drawing.Point(114, 120);
            this.cbDeparment.Name = "cbDeparment";
            this.cbDeparment.Size = new System.Drawing.Size(146, 21);
            this.cbDeparment.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(21, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "DEPARTMENT:";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(114, 64);
            this.txtConfirmPassword.MaxLength = 25;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(146, 20);
            this.txtConfirmPassword.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(21, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 28);
            this.label10.TabIndex = 18;
            this.label10.Text = "CONFIRM\r\nPASSWORD:    ";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(114, 36);
            this.txtPassword.MaxLength = 25;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(146, 20);
            this.txtPassword.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(21, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "PASSWORD:    ";
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(114, 147);
            this.txtPosition.MaxLength = 45;
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(146, 20);
            this.txtPosition.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(21, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "POSITION :       ";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(393, 111);
            this.txtSuffix.MaxLength = 3;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(47, 20);
            this.txtSuffix.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(302, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "SUFFIX:             ";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(393, 85);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(146, 20);
            this.txtLastName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(302, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "LAST NAME:     ";
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(393, 59);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(146, 20);
            this.txtMiddleName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(302, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "MIDDLE NAME:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(393, 33);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(146, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFirstName.Location = new System.Drawing.Point(302, 36);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(86, 15);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "FIRST NAME:   ";
            // 
            // btnDeleteRecord
            // 
            this.btnDeleteRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRecord.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteRecord.BackgroundImage")));
            this.btnDeleteRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteRecord.FlatAppearance.BorderSize = 0;
            this.btnDeleteRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteRecord.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteRecord.ForeColor = System.Drawing.Color.White;
            this.btnDeleteRecord.Location = new System.Drawing.Point(689, 387);
            this.btnDeleteRecord.Name = "btnDeleteRecord";
            this.btnDeleteRecord.Size = new System.Drawing.Size(158, 62);
            this.btnDeleteRecord.TabIndex = 42;
            this.btnDeleteRecord.Text = "DELETE RECORD";
            this.btnDeleteRecord.UseVisualStyleBackColor = false;
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecord.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveRecord.BackgroundImage")));
            this.btnSaveRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveRecord.FlatAppearance.BorderSize = 0;
            this.btnSaveRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveRecord.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRecord.ForeColor = System.Drawing.Color.White;
            this.btnSaveRecord.Location = new System.Drawing.Point(689, 463);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(158, 62);
            this.btnSaveRecord.TabIndex = 41;
            this.btnSaveRecord.Text = "SAVE";
            this.btnSaveRecord.UseVisualStyleBackColor = false;
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // btnCancelClose
            // 
            this.btnCancelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelClose.BackgroundImage")));
            this.btnCancelClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelClose.FlatAppearance.BorderSize = 0;
            this.btnCancelClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelClose.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnCancelClose.ForeColor = System.Drawing.Color.White;
            this.btnCancelClose.Location = new System.Drawing.Point(689, 96);
            this.btnCancelClose.Name = "btnCancelClose";
            this.btnCancelClose.Size = new System.Drawing.Size(158, 62);
            this.btnCancelClose.TabIndex = 48;
            this.btnCancelClose.Text = "CANCEL / CLOSE";
            this.btnCancelClose.UseVisualStyleBackColor = true;
            this.btnCancelClose.Click += new System.EventHandler(this.btnCancelClose_Click);
            // 
            // btnRefreshView
            // 
            this.btnRefreshView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshView.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefreshView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshView.BackgroundImage")));
            this.btnRefreshView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshView.FlatAppearance.BorderSize = 0;
            this.btnRefreshView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshView.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnRefreshView.ForeColor = System.Drawing.Color.White;
            this.btnRefreshView.Location = new System.Drawing.Point(689, 193);
            this.btnRefreshView.Name = "btnRefreshView";
            this.btnRefreshView.Size = new System.Drawing.Size(158, 62);
            this.btnRefreshView.TabIndex = 47;
            this.btnRefreshView.Text = "REFRESH";
            this.btnRefreshView.UseVisualStyleBackColor = false;
            this.btnRefreshView.Click += new System.EventHandler(this.btnRefreshView_Click);
            // 
            // gbUserId
            // 
            this.gbUserId.BackColor = System.Drawing.Color.Transparent;
            this.gbUserId.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbUserId.Controls.Add(this.btnAddRecord);
            this.gbUserId.Controls.Add(this.txtUserId);
            this.gbUserId.Location = new System.Drawing.Point(12, 109);
            this.gbUserId.Name = "gbUserId";
            this.gbUserId.Size = new System.Drawing.Size(248, 68);
            this.gbUserId.TabIndex = 49;
            this.gbUserId.TabStop = false;
            this.gbUserId.Text = "USER ID";
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddRecord.BackgroundImage")));
            this.btnAddRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddRecord.FlatAppearance.BorderSize = 0;
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.Font = new System.Drawing.Font("Cooper Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(165, 27);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(77, 23);
            this.btnAddRecord.TabIndex = 19;
            this.btnAddRecord.Text = "Create";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            // 
            // txtUserId
            // 
            this.txtUserId.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Location = new System.Drawing.Point(12, 27);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(147, 23);
            this.txtUserId.TabIndex = 3;
            this.txtUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserId_KeyDown);
            // 
            // gbSearch
            // 
            this.gbSearch.BackColor = System.Drawing.Color.Transparent;
            this.gbSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtSearch);
            this.gbSearch.Location = new System.Drawing.Point(279, 109);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(248, 68);
            this.gbSearch.TabIndex = 50;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "SEARCH USER";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Cooper Black", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(165, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Go";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(153, 21);
            this.txtSearch.TabIndex = 6;
            // 
            // frmOrderingUserMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 550);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.gbUserId);
            this.Controls.Add(this.btnCancelClose);
            this.Controls.Add(this.btnRefreshView);
            this.Controls.Add(this.btnDeleteRecord);
            this.Controls.Add(this.btnSaveRecord);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmOrderingUserMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrderingUserMaintenance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOrderingUserMaintenance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.gbUserId.ResumeLayout(false);
            this.gbUserId.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.ComboBox cbUserLevel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbDeparment;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Button btnDeleteRecord;
        private System.Windows.Forms.Button btnSaveRecord;
        private System.Windows.Forms.Button btnCancelClose;
        private System.Windows.Forms.Button btnRefreshView;
        private System.Windows.Forms.GroupBox gbUserId;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
    }
}