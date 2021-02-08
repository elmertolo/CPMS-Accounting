
namespace CPMS_Accounting
{
    partial class frmSalesInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSalesInvoice));
            this.dgvDRList = new System.Windows.Forms.DataGridView();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblDRList = new System.Windows.Forms.Label();
            this.txtSalesInvoiceNumber = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvListToProcess = new System.Windows.Forms.DataGridView();
            this.btnViewSelected = new System.Windows.Forms.Button();
            this.btnPrintSalesInvoice = new System.Windows.Forms.Button();
            this.cbCheckedBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbApprovedBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.gbBatchToProcess = new System.Windows.Forms.GroupBox();
            this.gbBatchList = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRowsAffected = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.btnReloadDrList = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbSINo = new System.Windows.Forms.GroupBox();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.pnlActionButtons = new System.Windows.Forms.GroupBox();
            this.btnCancelSiRecord = new System.Windows.Forms.Button();
            this.btnCancelClose = new System.Windows.Forms.Button();
            this.bgwLoadBatchList = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDRList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListToProcess)).BeginInit();
            this.gbSearch.SuspendLayout();
            this.gbDetails.SuspendLayout();
            this.gbBatchToProcess.SuspendLayout();
            this.gbBatchList.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbSINo.SuspendLayout();
            this.pnlActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDRList
            // 
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            this.dgvDRList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvDRList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDRList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDRList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDRList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvDRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDRList.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvDRList.Location = new System.Drawing.Point(6, 19);
            this.dgvDRList.Name = "dgvDRList";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDRList.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvDRList.Size = new System.Drawing.Size(542, 269);
            this.dgvDRList.TabIndex = 0;
            this.dgvDRList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDRList_CellContentClick);
            this.dgvDRList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDRList_CellDoubleClick);
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Location = new System.Drawing.Point(10, 60);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(226, 23);
            this.dtpInvoiceDate.TabIndex = 2;
            // 
            // lblDRList
            // 
            this.lblDRList.AutoSize = true;
            this.lblDRList.BackColor = System.Drawing.Color.Transparent;
            this.lblDRList.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDRList.Location = new System.Drawing.Point(6, 38);
            this.lblDRList.Name = "lblDRList";
            this.lblDRList.Size = new System.Drawing.Size(78, 15);
            this.lblDRList.TabIndex = 1;
            this.lblDRList.Text = "Invoice Date:";
            // 
            // txtSalesInvoiceNumber
            // 
            this.txtSalesInvoiceNumber.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesInvoiceNumber.Location = new System.Drawing.Point(12, 27);
            this.txtSalesInvoiceNumber.Name = "txtSalesInvoiceNumber";
            this.txtSalesInvoiceNumber.Size = new System.Drawing.Size(147, 23);
            this.txtSalesInvoiceNumber.TabIndex = 3;
            this.txtSalesInvoiceNumber.TextChanged += new System.EventHandler(this.txtSalesInvoiceNumber_TextChanged);
            this.txtSalesInvoiceNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesInvoiceNumber_KeyDown);
            this.txtSalesInvoiceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSalesInvoiceNumber_KeyPress);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(153, 21);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // dgvListToProcess
            // 
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black;
            this.dgvListToProcess.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvListToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListToProcess.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvListToProcess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvListToProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListToProcess.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvListToProcess.Location = new System.Drawing.Point(6, 19);
            this.dgvListToProcess.Name = "dgvListToProcess";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListToProcess.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvListToProcess.Size = new System.Drawing.Size(796, 194);
            this.dgvListToProcess.TabIndex = 8;
            // 
            // btnViewSelected
            // 
            this.btnViewSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewSelected.BackColor = System.Drawing.SystemColors.Control;
            this.btnViewSelected.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewSelected.BackgroundImage")));
            this.btnViewSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnViewSelected.FlatAppearance.BorderSize = 0;
            this.btnViewSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewSelected.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewSelected.ForeColor = System.Drawing.Color.White;
            this.btnViewSelected.Location = new System.Drawing.Point(6, 81);
            this.btnViewSelected.Name = "btnViewSelected";
            this.btnViewSelected.Size = new System.Drawing.Size(158, 62);
            this.btnViewSelected.TabIndex = 9;
            this.btnViewSelected.Text = "INSERT SELECTED";
            this.btnViewSelected.UseVisualStyleBackColor = false;
            this.btnViewSelected.Click += new System.EventHandler(this.btnViewSelected_Click);
            // 
            // btnPrintSalesInvoice
            // 
            this.btnPrintSalesInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintSalesInvoice.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrintSalesInvoice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintSalesInvoice.BackgroundImage")));
            this.btnPrintSalesInvoice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintSalesInvoice.FlatAppearance.BorderSize = 0;
            this.btnPrintSalesInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintSalesInvoice.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintSalesInvoice.ForeColor = System.Drawing.Color.White;
            this.btnPrintSalesInvoice.Location = new System.Drawing.Point(6, 451);
            this.btnPrintSalesInvoice.Name = "btnPrintSalesInvoice";
            this.btnPrintSalesInvoice.Size = new System.Drawing.Size(158, 62);
            this.btnPrintSalesInvoice.TabIndex = 10;
            this.btnPrintSalesInvoice.Text = "GENERATE / PRINT";
            this.btnPrintSalesInvoice.UseVisualStyleBackColor = false;
            this.btnPrintSalesInvoice.Click += new System.EventHandler(this.btnPrintSalesInvoice_Click);
            // 
            // cbCheckedBy
            // 
            this.cbCheckedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbCheckedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCheckedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCheckedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckedBy.FormattingEnabled = true;
            this.cbCheckedBy.Location = new System.Drawing.Point(91, 227);
            this.cbCheckedBy.Name = "cbCheckedBy";
            this.cbCheckedBy.Size = new System.Drawing.Size(146, 23);
            this.cbCheckedBy.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Checked By:";
            // 
            // cbApprovedBy
            // 
            this.cbApprovedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbApprovedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbApprovedBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbApprovedBy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbApprovedBy.FormattingEnabled = true;
            this.cbApprovedBy.Location = new System.Drawing.Point(91, 256);
            this.cbApprovedBy.Name = "cbApprovedBy";
            this.cbApprovedBy.Size = new System.Drawing.Size(146, 23);
            this.cbApprovedBy.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Approved By:";
            // 
            // gbSearch
            // 
            this.gbSearch.BackColor = System.Drawing.Color.Transparent;
            this.gbSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtSearch);
            this.gbSearch.Location = new System.Drawing.Point(266, 86);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(248, 68);
            this.gbSearch.TabIndex = 17;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "SEARCH BATCH";
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
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gbDetails
            // 
            this.gbDetails.BackColor = System.Drawing.Color.Transparent;
            this.gbDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbDetails.Controls.Add(this.lblDRList);
            this.gbDetails.Controls.Add(this.dtpInvoiceDate);
            this.gbDetails.Controls.Add(this.cbApprovedBy);
            this.gbDetails.Controls.Add(this.label4);
            this.gbDetails.Controls.Add(this.label3);
            this.gbDetails.Controls.Add(this.cbCheckedBy);
            this.gbDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbDetails.Location = new System.Drawing.Point(6, 160);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(248, 294);
            this.gbDetails.TabIndex = 18;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "DETAILS";
            // 
            // gbBatchToProcess
            // 
            this.gbBatchToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBatchToProcess.BackColor = System.Drawing.Color.Transparent;
            this.gbBatchToProcess.Controls.Add(this.dgvListToProcess);
            this.gbBatchToProcess.Location = new System.Drawing.Point(12, 460);
            this.gbBatchToProcess.Name = "gbBatchToProcess";
            this.gbBatchToProcess.Size = new System.Drawing.Size(808, 219);
            this.gbBatchToProcess.TabIndex = 19;
            this.gbBatchToProcess.TabStop = false;
            this.gbBatchToProcess.Text = "BATCH DETAILS";
            // 
            // gbBatchList
            // 
            this.gbBatchList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBatchList.BackColor = System.Drawing.Color.Transparent;
            this.gbBatchList.Controls.Add(this.dgvDRList);
            this.gbBatchList.Location = new System.Drawing.Point(266, 160);
            this.gbBatchList.Name = "gbBatchList";
            this.gbBatchList.Size = new System.Drawing.Size(554, 294);
            this.gbBatchList.TabIndex = 20;
            this.gbBatchList.TabStop = false;
            this.gbBatchList.Text = "BATCH LIST";
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
            this.panel1.Location = new System.Drawing.Point(18, 685);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 32);
            this.panel1.TabIndex = 21;
            // 
            // lblRowsAffected
            // 
            this.lblRowsAffected.AutoSize = true;
            this.lblRowsAffected.Location = new System.Drawing.Point(578, 9);
            this.lblRowsAffected.Name = "lblRowsAffected";
            this.lblRowsAffected.Size = new System.Drawing.Size(80, 13);
            this.lblRowsAffected.TabIndex = 4;
            this.lblRowsAffected.Text = "Rows Affected:";
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
            // btnReloadDrList
            // 
            this.btnReloadDrList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadDrList.BackColor = System.Drawing.SystemColors.Control;
            this.btnReloadDrList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReloadDrList.BackgroundImage")));
            this.btnReloadDrList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReloadDrList.FlatAppearance.BorderSize = 0;
            this.btnReloadDrList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadDrList.Font = new System.Drawing.Font("Cooper Black", 12F);
            this.btnReloadDrList.ForeColor = System.Drawing.Color.White;
            this.btnReloadDrList.Location = new System.Drawing.Point(6, 13);
            this.btnReloadDrList.Name = "btnReloadDrList";
            this.btnReloadDrList.Size = new System.Drawing.Size(158, 62);
            this.btnReloadDrList.TabIndex = 22;
            this.btnReloadDrList.Text = "REFRESH";
            this.btnReloadDrList.UseVisualStyleBackColor = false;
            this.btnReloadDrList.Click += new System.EventHandler(this.btnReloadDrList_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprint.BackColor = System.Drawing.SystemColors.Control;
            this.btnReprint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReprint.BackgroundImage")));
            this.btnReprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReprint.FlatAppearance.BorderSize = 0;
            this.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprint.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprint.ForeColor = System.Drawing.Color.White;
            this.btnReprint.Location = new System.Drawing.Point(6, 383);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(158, 62);
            this.btnReprint.TabIndex = 23;
            this.btnReprint.Text = "REPRINT";
            this.btnReprint.UseVisualStyleBackColor = false;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::CPMS_Accounting.Properties.Resources.Header_SalesInvoice1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1008, 80);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // gbSINo
            // 
            this.gbSINo.BackColor = System.Drawing.Color.Transparent;
            this.gbSINo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gbSINo.Controls.Add(this.btnAddRecord);
            this.gbSINo.Controls.Add(this.txtSalesInvoiceNumber);
            this.gbSINo.Location = new System.Drawing.Point(6, 86);
            this.gbSINo.Name = "gbSINo";
            this.gbSINo.Size = new System.Drawing.Size(248, 68);
            this.gbSINo.TabIndex = 35;
            this.gbSINo.TabStop = false;
            this.gbSINo.Text = "SALES INVOICE NUMBER";
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
            this.btnAddRecord.Text = "Enter";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActionButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlActionButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlActionButtons.Controls.Add(this.btnCancelSiRecord);
            this.pnlActionButtons.Controls.Add(this.btnViewSelected);
            this.pnlActionButtons.Controls.Add(this.btnReloadDrList);
            this.pnlActionButtons.Controls.Add(this.btnReprint);
            this.pnlActionButtons.Controls.Add(this.btnPrintSalesInvoice);
            this.pnlActionButtons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnlActionButtons.Location = new System.Drawing.Point(826, 160);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(170, 519);
            this.pnlActionButtons.TabIndex = 36;
            this.pnlActionButtons.TabStop = false;
            // 
            // btnCancelSiRecord
            // 
            this.btnCancelSiRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSiRecord.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelSiRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelSiRecord.BackgroundImage")));
            this.btnCancelSiRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelSiRecord.FlatAppearance.BorderSize = 0;
            this.btnCancelSiRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSiRecord.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelSiRecord.ForeColor = System.Drawing.Color.White;
            this.btnCancelSiRecord.Location = new System.Drawing.Point(6, 315);
            this.btnCancelSiRecord.Name = "btnCancelSiRecord";
            this.btnCancelSiRecord.Size = new System.Drawing.Size(158, 62);
            this.btnCancelSiRecord.TabIndex = 24;
            this.btnCancelSiRecord.Text = "TAG AS CANCELLED";
            this.btnCancelSiRecord.UseVisualStyleBackColor = false;
            this.btnCancelSiRecord.Click += new System.EventHandler(this.btnCancelSiRecord_Click);
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
            this.btnCancelClose.TabIndex = 37;
            this.btnCancelClose.Text = "CANCEL / CLOSE";
            this.btnCancelClose.UseVisualStyleBackColor = true;
            this.btnCancelClose.Click += new System.EventHandler(this.btnCancelClose_Click);
            // 
            // bgwLoadBatchList
            // 
            this.bgwLoadBatchList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadBatchList_DoWork);
            this.bgwLoadBatchList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadBatchList_RunWorkerCompleted);
            // 
            // frmSalesInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btnCancelClose);
            this.Controls.Add(this.pnlActionButtons);
            this.Controls.Add(this.gbSINo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbBatchList);
            this.Controls.Add(this.gbBatchToProcess);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSalesInvoice";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SALES INVOICE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSalesInvoice_FormClosing);
            this.Load += new System.EventHandler(this.frmSalesInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDRList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListToProcess)).EndInit();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.gbBatchToProcess.ResumeLayout(false);
            this.gbBatchList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbSINo.ResumeLayout(false);
            this.gbSINo.PerformLayout();
            this.pnlActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDRList;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblDRList;
        private System.Windows.Forms.TextBox txtSalesInvoiceNumber;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvListToProcess;
        private System.Windows.Forms.Button btnViewSelected;
        private System.Windows.Forms.Button btnPrintSalesInvoice;
        private System.Windows.Forms.ComboBox cbApprovedBy;
        private System.Windows.Forms.ComboBox cbCheckedBy;
        private System.Windows.Forms.Label label3;
        //private System.Windows.Forms.ComboBox cbApprovedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.GroupBox gbBatchToProcess;
        private System.Windows.Forms.GroupBox gbBatchList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Button btnReloadDrList;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblRowsAffected;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gbSINo;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.GroupBox pnlActionButtons;
        private System.Windows.Forms.Button btnCancelSiRecord;
        private System.Windows.Forms.Button btnCancelClose;
        private System.ComponentModel.BackgroundWorker bgwLoadBatchList;
    }
}