
namespace CPMS_Accounting
{
    partial class RecentBatch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deliveryReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stickersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentStampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDRList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecentBatch = new System.Windows.Forms.TextBox();
            this.deliveryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDRList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.printDRToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(885, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.searchToolStripMenuItem.Text = "Generate";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // printDRToolStripMenuItem
            // 
            this.printDRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deliveryReceiptToolStripMenuItem,
            this.stickersToolStripMenuItem,
            this.packingToolStripMenuItem,
            this.salesInvoiceToolStripMenuItem,
            this.documentStampToolStripMenuItem,
            this.deliveryReportToolStripMenuItem});
            this.printDRToolStripMenuItem.Enabled = false;
            this.printDRToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printDRToolStripMenuItem.Name = "printDRToolStripMenuItem";
            this.printDRToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.printDRToolStripMenuItem.Text = "Print Reports";
            this.printDRToolStripMenuItem.Click += new System.EventHandler(this.printDRToolStripMenuItem_Click);
            // 
            // deliveryReceiptToolStripMenuItem
            // 
            this.deliveryReceiptToolStripMenuItem.Name = "deliveryReceiptToolStripMenuItem";
            this.deliveryReceiptToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.deliveryReceiptToolStripMenuItem.Text = "Delivery Receipt";
            this.deliveryReceiptToolStripMenuItem.Click += new System.EventHandler(this.deliveryReceiptToolStripMenuItem_Click);
            // 
            // stickersToolStripMenuItem
            // 
            this.stickersToolStripMenuItem.Name = "stickersToolStripMenuItem";
            this.stickersToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.stickersToolStripMenuItem.Text = "Stickers";
            this.stickersToolStripMenuItem.Click += new System.EventHandler(this.stickersToolStripMenuItem_Click);
            // 
            // packingToolStripMenuItem
            // 
            this.packingToolStripMenuItem.Name = "packingToolStripMenuItem";
            this.packingToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.packingToolStripMenuItem.Text = "Packing";
            this.packingToolStripMenuItem.Click += new System.EventHandler(this.packingToolStripMenuItem_Click);
            // 
            // salesInvoiceToolStripMenuItem
            // 
            this.salesInvoiceToolStripMenuItem.Name = "salesInvoiceToolStripMenuItem";
            this.salesInvoiceToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.salesInvoiceToolStripMenuItem.Text = "Sales Invoice";
            this.salesInvoiceToolStripMenuItem.Click += new System.EventHandler(this.salesInvoiceToolStripMenuItem_Click);
            // 
            // documentStampToolStripMenuItem
            // 
            this.documentStampToolStripMenuItem.Name = "documentStampToolStripMenuItem";
            this.documentStampToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.documentStampToolStripMenuItem.Text = "Document Stamp";
            this.documentStampToolStripMenuItem.Click += new System.EventHandler(this.documentStampToolStripMenuItem_Click);
            // 
            // dgvDRList
            // 
            this.dgvDRList.AllowUserToAddRows = false;
            this.dgvDRList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDRList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDRList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDRList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDRList.Location = new System.Drawing.Point(13, 73);
            this.dgvDRList.Name = "dgvDRList";
            this.dgvDRList.ReadOnly = true;
            this.dgvDRList.Size = new System.Drawing.Size(860, 364);
            this.dgvDRList.TabIndex = 1;
            this.dgvDRList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDRList_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Batch  :";
            // 
            // txtRecentBatch
            // 
            this.txtRecentBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecentBatch.Location = new System.Drawing.Point(83, 36);
            this.txtRecentBatch.Name = "txtRecentBatch";
            this.txtRecentBatch.Size = new System.Drawing.Size(134, 22);
            this.txtRecentBatch.TabIndex = 1;
            this.txtRecentBatch.TextChanged += new System.EventHandler(this.txtRecentBatch_TextChanged);
            // 
            // deliveryReportToolStripMenuItem
            // 
            this.deliveryReportToolStripMenuItem.Name = "deliveryReportToolStripMenuItem";
            this.deliveryReportToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.deliveryReportToolStripMenuItem.Text = "Delivery Report";
            this.deliveryReportToolStripMenuItem.Click += new System.EventHandler(this.deliveryReportToolStripMenuItem_Click);
            // 
            // RecentBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 449);
            this.Controls.Add(this.txtRecentBatch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDRList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RecentBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recent Batch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecentBatch_FormClosing);
            this.Load += new System.EventHandler(this.RecentBatch_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDRList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvDRList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRecentBatch;
        private System.Windows.Forms.ToolStripMenuItem printDRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deliveryReceiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stickersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentStampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deliveryReportToolStripMenuItem;
    }
}