
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrder));
            this.label7 = new System.Windows.Forms.Label();
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
            this.btnReloadDrList = new System.Windows.Forms.Button();
            this.btnAddSelectedItem = new System.Windows.Forms.Button();
            this.pnlActionButtons = new System.Windows.Forms.GroupBox();
            this.pbHeader = new System.Windows.Forms.PictureBox();
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "label7";
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
            this.panel1.Controls.Add(this.label7);
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
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvListToProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListToProcess.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvListToProcess.Location = new System.Drawing.Point(6, 19);
            this.dgvListToProcess.Name = "dgvListToProcess";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
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
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Ivory;
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            this.dgvItemList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvItemList.Location = new System.Drawing.Point(6, 19);
            this.dgvItemList.Name = "dgvItemList";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
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
            this.btnSavePrintPO.Location = new System.Drawing.Point(6, 525);
            this.btnSavePrintPO.Name = "btnSavePrintPO";
            this.btnSavePrintPO.Size = new System.Drawing.Size(158, 62);
            this.btnSavePrintPO.TabIndex = 22;
            this.btnSavePrintPO.Text = "SAVE AND PRINT PURCHASE ORDER";
            this.btnSavePrintPO.UseVisualStyleBackColor = true;
            this.btnSavePrintPO.Click += new System.EventHandler(this.btnSavePrintPO_Click);
            // 
            // btnCancelClose
            // 
            this.btnCancelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelClose.Location = new System.Drawing.Point(6, 457);
            this.btnCancelClose.Name = "btnCancelClose";
            this.btnCancelClose.Size = new System.Drawing.Size(158, 62);
            this.btnCancelClose.TabIndex = 30;
            this.btnCancelClose.Text = "CANCEL AND CLOSE";
            this.btnCancelClose.UseVisualStyleBackColor = true;
            this.btnCancelClose.Click += new System.EventHandler(this.btnCancelClose_Click);
            // 
            // btnReloadDrList
            // 
            this.btnReloadDrList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadDrList.Location = new System.Drawing.Point(6, 87);
            this.btnReloadDrList.Name = "btnReloadDrList";
            this.btnReloadDrList.Size = new System.Drawing.Size(158, 62);
            this.btnReloadDrList.TabIndex = 29;
            this.btnReloadDrList.Text = "CLEAR";
            this.btnReloadDrList.UseVisualStyleBackColor = true;
            this.btnReloadDrList.Click += new System.EventHandler(this.btnReloadDrList_Click);
            // 
            // btnAddSelectedItem
            // 
            this.btnAddSelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSelectedItem.Location = new System.Drawing.Point(6, 19);
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
            this.pnlActionButtons.Controls.Add(this.btnAddSelectedItem);
            this.pnlActionButtons.Controls.Add(this.btnCancelClose);
            this.pnlActionButtons.Controls.Add(this.btnSavePrintPO);
            this.pnlActionButtons.Controls.Add(this.btnReloadDrList);
            this.pnlActionButtons.Location = new System.Drawing.Point(826, 86);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(170, 593);
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
            // frmPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pbHeader);
            this.Controls.Add(this.pnlActionButtons);
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
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.Button btnReloadDrList;
        private System.Windows.Forms.Button btnAddSelectedItem;
        private System.Windows.Forms.GroupBox pnlActionButtons;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.PictureBox pbHeader;
    }
}