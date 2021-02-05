
namespace CPMS_Accounting
{
    partial class frmPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblRowsAffected = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lblDRList = new System.Windows.Forms.Label();
            this.dtpPODate = new System.Windows.Forms.DateTimePicker();
            this.cbApprovedBy = new System.Windows.Forms.ComboBox();
            this.cbCheckedBy = new System.Windows.Forms.ComboBox();
            this.lblCheckedBy = new System.Windows.Forms.Label();
            this.lblApprovedBy = new System.Windows.Forms.Label();
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.gbSearchItem = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gbListToProcess = new System.Windows.Forms.GroupBox();
            this.dgvListToProcess = new System.Windows.Forms.DataGridView();
            this.gbItemList = new System.Windows.Forms.GroupBox();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.gbPONo = new System.Windows.Forms.GroupBox();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.btnSavePrintPO = new System.Windows.Forms.Button();
            this.btnCancelClose = new System.Windows.Forms.Button();
            this.btnRefreshPage = new System.Windows.Forms.Button();
            this.btnAddSelectedItem = new System.Windows.Forms.Button();
            this.pnlActionButtons = new System.Windows.Forms.GroupBox();
            this.pbHeader = new System.Windows.Forms.PictureBox();
            this.btnDeletePORecord = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.gbDetails.SuspendLayout();
            this.gbSearchItem.SuspendLayout();
            this.gbListToProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListToProcess)).BeginInit();
            this.gbItemList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.gbPONo.SuspendLayout();
            this.pnlActionButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRowsAffected
            // 
            this.lblRowsAffected.AutoSize = true;
            this.lblRowsAffected.Location = new System.Drawing.Point(578, 9);
            this.lblRowsAffected.Name = "lblRowsAffected";
            this.lblRowsAffected.Size = new System.Drawing.Size(35, 13);
            this.lblRowsAffected.TabIndex = 4;
            this.lblRowsAffected.Text = "label7";
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(331, 9);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(83, 13);
            this.lblBankName.TabIndex = 3;
            this.lblBankName.Text = "Producers Bank";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(81, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(40, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Nelson";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(290, 9);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(35, 13);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "Bank:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 9);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(63, 13);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "User Name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblRowsAffected);
            this.panel1.Controls.Add(this.lblBankName);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Location = new System.Drawing.Point(12, 685);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 32);
            this.panel1.TabIndex = 23;
            // 
            // gbDetails
            // 
            this.gbDetails.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbDetails.Controls.Add(this.lblDRList);
            this.gbDetails.Controls.Add(this.dtpPODate);
            this.gbDetails.Controls.Add(this.cbApprovedBy);
            this.gbDetails.Controls.Add(this.cbCheckedBy);
            this.gbDetails.Controls.Add(this.lblCheckedBy);
            this.gbDetails.Controls.Add(this.lblApprovedBy);
            this.gbDetails.Location = new System.Drawing.Point(12, 160);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(248, 294);
            this.gbDetails.TabIndex = 26;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "DETAILS";
            // 
            // lblDRList
            // 
            this.lblDRList.AutoSize = true;
            this.lblDRList.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDRList.Location = new System.Drawing.Point(6, 19);
            this.lblDRList.Name = "lblDRList";
            this.lblDRList.Size = new System.Drawing.Size(138, 15);
            this.lblDRList.TabIndex = 1;
            this.lblDRList.Text = "PURCHASE ORDER DATE:";
            // 
            // dtpPODate
            // 
            this.dtpPODate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPODate.Location = new System.Drawing.Point(10, 41);
            this.dtpPODate.Name = "dtpPODate";
            this.dtpPODate.Size = new System.Drawing.Size(226, 23);
            this.dtpPODate.TabIndex = 2;
            // 
            // cbApprovedBy
            // 
            this.cbApprovedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbApprovedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbApprovedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbApprovedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbApprovedBy.FormattingEnabled = true;
            this.cbApprovedBy.Location = new System.Drawing.Point(90, 213);
            this.cbApprovedBy.Name = "cbApprovedBy";
            this.cbApprovedBy.Size = new System.Drawing.Size(146, 23);
            this.cbApprovedBy.TabIndex = 16;
            // 
            // cbCheckedBy
            // 
            this.cbCheckedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbCheckedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCheckedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCheckedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckedBy.FormattingEnabled = true;
            this.cbCheckedBy.Location = new System.Drawing.Point(90, 184);
            this.cbCheckedBy.Name = "cbCheckedBy";
            this.cbCheckedBy.Size = new System.Drawing.Size(146, 23);
            this.cbCheckedBy.TabIndex = 14;
            // 
            // lblCheckedBy
            // 
            this.lblCheckedBy.AutoSize = true;
            this.lblCheckedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckedBy.Location = new System.Drawing.Point(11, 187);
            this.lblCheckedBy.Name = "lblCheckedBy";
            this.lblCheckedBy.Size = new System.Drawing.Size(73, 15);
            this.lblCheckedBy.TabIndex = 13;
            this.lblCheckedBy.Text = "Checked By:";
            // 
            // lblApprovedBy
            // 
            this.lblApprovedBy.AutoSize = true;
            this.lblApprovedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprovedBy.Location = new System.Drawing.Point(8, 216);
            this.lblApprovedBy.Name = "lblApprovedBy";
            this.lblApprovedBy.Size = new System.Drawing.Size(81, 15);
            this.lblApprovedBy.TabIndex = 15;
            this.lblApprovedBy.Text = "Approved By:";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPONumber.Location = new System.Drawing.Point(17, 27);
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(136, 23);
            this.txtPONumber.TabIndex = 3;
            this.txtPONumber.TextChanged += new System.EventHandler(this.txtPONumber_TextChanged);
            this.txtPONumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPONumber_KeyDown);
            this.txtPONumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPONumber_KeyPress);
            // 
            // gbSearchItem
            // 
            this.gbSearchItem.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbSearchItem.Controls.Add(this.btnSearch);
            this.gbSearchItem.Controls.Add(this.txtSearch);
            this.gbSearchItem.Location = new System.Drawing.Point(272, 86);
            this.gbSearchItem.Name = "gbSearchItem";
            this.gbSearchItem.Size = new System.Drawing.Size(316, 68);
            this.gbSearchItem.TabIndex = 25;
            this.gbSearchItem.TabStop = false;
            this.gbSearchItem.Text = "SEARCH ITEM";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Cooper Black", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(233, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Go";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(221, 21);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // gbListToProcess
            // 
            this.gbListToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbListToProcess.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbListToProcess.Controls.Add(this.dgvListToProcess);
            this.gbListToProcess.Location = new System.Drawing.Point(12, 478);
            this.gbListToProcess.Name = "gbListToProcess";
            this.gbListToProcess.Size = new System.Drawing.Size(808, 201);
            this.gbListToProcess.TabIndex = 27;
            this.gbListToProcess.TabStop = false;
            this.gbListToProcess.Text = "ITEMS TO PROCESS";
            // 
            // dgvListToProcess
            // 
            this.dgvListToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListToProcess.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvListToProcess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListToProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListToProcess.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListToProcess.Location = new System.Drawing.Point(6, 19);
            this.dgvListToProcess.Name = "dgvListToProcess";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvListToProcess.Size = new System.Drawing.Size(796, 176);
            this.dgvListToProcess.TabIndex = 8;
            // 
            // gbItemList
            // 
            this.gbItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbItemList.Controls.Add(this.dgvItemList);
            this.gbItemList.Location = new System.Drawing.Point(266, 160);
            this.gbItemList.Name = "gbItemList";
            this.gbItemList.Size = new System.Drawing.Size(554, 294);
            this.gbItemList.TabIndex = 28;
            this.gbItemList.TabStop = false;
            this.gbItemList.Text = "ITEM LIST";
            // 
            // dgvItemList
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Ivory;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvItemList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemList.Location = new System.Drawing.Point(6, 19);
            this.dgvItemList.Name = "dgvItemList";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItemList.Size = new System.Drawing.Size(542, 269);
            this.dgvItemList.TabIndex = 0;
            this.dgvItemList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellDoubleClick);
            // 
            // gbPONo
            // 
            this.gbPONo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbPONo.Controls.Add(this.btnAddRecord);
            this.gbPONo.Controls.Add(this.txtPONumber);
            this.gbPONo.Location = new System.Drawing.Point(12, 86);
            this.gbPONo.Name = "gbPONo";
            this.gbPONo.Size = new System.Drawing.Size(248, 68);
            this.gbPONo.TabIndex = 31;
            this.gbPONo.TabStop = false;
            this.gbPONo.Text = "PURCHASE ORDER NUMBER";
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddRecord.BackgroundImage")));
            this.btnAddRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddRecord.FlatAppearance.BorderSize = 0;
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.Font = new System.Drawing.Font("Cooper Black", 9F);
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(160, 27);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(77, 23);
            this.btnAddRecord.TabIndex = 19;
            this.btnAddRecord.Text = "Add";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // btnSavePrintPO
            // 
            this.btnSavePrintPO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePrintPO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSavePrintPO.BackgroundImage")));
            this.btnSavePrintPO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSavePrintPO.FlatAppearance.BorderSize = 0;
            this.btnSavePrintPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePrintPO.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnSavePrintPO.ForeColor = System.Drawing.Color.White;
            this.btnSavePrintPO.Location = new System.Drawing.Point(6, 451);
            this.btnSavePrintPO.Name = "btnSavePrintPO";
            this.btnSavePrintPO.Size = new System.Drawing.Size(158, 62);
            this.btnSavePrintPO.TabIndex = 22;
            this.btnSavePrintPO.Text = "SAVE / PRINT";
            this.btnSavePrintPO.UseVisualStyleBackColor = true;
            this.btnSavePrintPO.Click += new System.EventHandler(this.btnSavePrintPO_Click);
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
            this.btnCancelClose.Location = new System.Drawing.Point(832, 91);
            this.btnCancelClose.Name = "btnCancelClose";
            this.btnCancelClose.Size = new System.Drawing.Size(158, 62);
            this.btnCancelClose.TabIndex = 30;
            this.btnCancelClose.Text = "CANCEL / CLOSE";
            this.btnCancelClose.UseVisualStyleBackColor = true;
            this.btnCancelClose.Click += new System.EventHandler(this.btnCancelClose_Click);
            // 
            // btnRefreshPage
            // 
            this.btnRefreshPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshPage.BackgroundImage")));
            this.btnRefreshPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshPage.FlatAppearance.BorderSize = 0;
            this.btnRefreshPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshPage.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnRefreshPage.ForeColor = System.Drawing.Color.White;
            this.btnRefreshPage.Location = new System.Drawing.Point(6, 11);
            this.btnRefreshPage.Name = "btnRefreshPage";
            this.btnRefreshPage.Size = new System.Drawing.Size(158, 62);
            this.btnRefreshPage.TabIndex = 29;
            this.btnRefreshPage.Text = "CLEAR";
            this.btnRefreshPage.UseVisualStyleBackColor = true;
            this.btnRefreshPage.Click += new System.EventHandler(this.btnReloadDrList_Click);
            // 
            // btnAddSelectedItem
            // 
            this.btnAddSelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSelectedItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSelectedItem.BackgroundImage")));
            this.btnAddSelectedItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddSelectedItem.FlatAppearance.BorderSize = 0;
            this.btnAddSelectedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSelectedItem.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnAddSelectedItem.ForeColor = System.Drawing.Color.White;
            this.btnAddSelectedItem.Location = new System.Drawing.Point(6, 79);
            this.btnAddSelectedItem.Name = "btnAddSelectedItem";
            this.btnAddSelectedItem.Size = new System.Drawing.Size(158, 62);
            this.btnAddSelectedItem.TabIndex = 24;
            this.btnAddSelectedItem.Text = "ADD SELECTED ITEM";
            this.btnAddSelectedItem.UseVisualStyleBackColor = true;
            this.btnAddSelectedItem.Click += new System.EventHandler(this.btnAddSelectedItem_Click);
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActionButtons.Controls.Add(this.btnDeletePORecord);
            this.pnlActionButtons.Controls.Add(this.btnAddSelectedItem);
            this.pnlActionButtons.Controls.Add(this.btnSavePrintPO);
            this.pnlActionButtons.Controls.Add(this.btnRefreshPage);
            this.pnlActionButtons.Location = new System.Drawing.Point(826, 160);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(170, 519);
            this.pnlActionButtons.TabIndex = 32;
            this.pnlActionButtons.TabStop = false;
            // 
            // pbHeader
            // 
            this.pbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbHeader.BackgroundImage")));
            this.pbHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHeader.Location = new System.Drawing.Point(0, 0);
            this.pbHeader.Name = "pbHeader";
            this.pbHeader.Size = new System.Drawing.Size(1008, 80);
            this.pbHeader.TabIndex = 33;
            this.pbHeader.TabStop = false;
            // 
            // btnDeletePORecord
            // 
            this.btnDeletePORecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePORecord.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeletePORecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeletePORecord.BackgroundImage")));
            this.btnDeletePORecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeletePORecord.FlatAppearance.BorderSize = 0;
            this.btnDeletePORecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePORecord.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeletePORecord.ForeColor = System.Drawing.Color.White;
            this.btnDeletePORecord.Location = new System.Drawing.Point(6, 383);
            this.btnDeletePORecord.Name = "btnDeletePORecord";
            this.btnDeletePORecord.Size = new System.Drawing.Size(158, 62);
            this.btnDeletePORecord.TabIndex = 31;
            this.btnDeletePORecord.Text = "DELETE RECORD";
            this.btnDeletePORecord.UseVisualStyleBackColor = false;
            this.btnDeletePORecord.Click += new System.EventHandler(this.btnDeletePORecord_Click);
            // 
            // frmPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pbHeader);
            this.Controls.Add(this.pnlActionButtons);
            this.Controls.Add(this.btnCancelClose);
            this.Controls.Add(this.gbPONo);
            this.Controls.Add(this.gbItemList);
            this.Controls.Add(this.gbListToProcess);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbSearchItem);
            this.Controls.Add(this.panel1);
            this.Name = "frmPurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PURCHASE ORDER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPurcahseOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.gbSearchItem.ResumeLayout(false);
            this.gbSearchItem.PerformLayout();
            this.gbListToProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListToProcess)).EndInit();
            this.gbItemList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.gbPONo.ResumeLayout(false);
            this.gbPONo.PerformLayout();
            this.pnlActionButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblRowsAffected;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lblDRList;
        private System.Windows.Forms.DateTimePicker dtpPODate;
        private System.Windows.Forms.ComboBox cbApprovedBy;
        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.Label lblApprovedBy;
        private System.Windows.Forms.Label lblCheckedBy;
        private System.Windows.Forms.ComboBox cbCheckedBy;
        private System.Windows.Forms.GroupBox gbSearchItem;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox gbListToProcess;
        private System.Windows.Forms.DataGridView dgvListToProcess;
        private System.Windows.Forms.GroupBox gbItemList;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.GroupBox gbPONo;
        private System.Windows.Forms.Button btnSavePrintPO;
        private System.Windows.Forms.Button btnCancelClose;
        private System.Windows.Forms.Button btnRefreshPage;
        private System.Windows.Forms.Button btnAddSelectedItem;
        private System.Windows.Forms.GroupBox pnlActionButtons;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.PictureBox pbHeader;
        private System.Windows.Forms.Button btnDeletePORecord;
    }
}