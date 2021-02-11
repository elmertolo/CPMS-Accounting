﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Models;
using CPMS_Accounting.Procedures;

namespace CPMS_Accounting.Forms
{
    public partial class frmProducts : Form
    {
        Main frm;
        public frmProducts(Main frm1)
        {
            InitializeComponent();
            this.frm = frm1;
        }
        List<ProductModel> productList = new List<ProductModel>();
        DataTable dt = new DataTable();
        ProcessServices proc = new ProcessServices();
        ProductModel product = new ProductModel();

        int liaddmod = 0;
        private void frmCheques_Load(object sender, EventArgs e)
        {
            DisplayAllProducts();
            DeliveryLocation();
            addToolStripMenuItem.Enabled = true;
            modifyToolStripMenuItem.Enabled = true;

            EnableControls(false, liaddmod);

           
        }
        private void DisplayAllProducts()
        {
            productList.Clear();
            proc.GetProducts(productList);

            dt.Clear();
            dt.Columns.Clear();
            dt.Columns.Add("Product Code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Unit Price");
            dt.Columns.Add("Document Stamp Price");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Delivery Location");

            productList.ForEach(x =>
            {
                dt.Rows.Add(new object[] { x.ProductCode, x.Description , x.UnitPrice , x.DocStampPrice, x.Unit, x.DeliveryLocation });
            });

            dgvProducts.DataSource = dt;
            ProcessServices.bg_dtg(dgvProducts);
            dgvProducts.Columns[0].Width = 90;
            dgvProducts.Columns[1].Width = 200;
            dgvProducts.Columns[2].Width = 60;
            dgvProducts.Columns[3].Width = 70;
            dgvProducts.Columns[4].Width = 50;
            dgvProducts.Columns[5].Width = 90;

          

        }
        private void EnableControls(bool _bool, int _addmod)
        {
            txtProductCode.Enabled = _bool;
            txtBankCode.Enabled = _bool;
            txtChequeName.Enabled = _bool;
            txtDescription.Enabled = _bool;
            txtDocStampPrice.Enabled = _bool;
            txtUnit.Enabled = _bool;
            txtUnitPrice.Enabled = _bool;
            txtType.Enabled = _bool;
            cmbLocation.Enabled = _bool;
            //_item.Enabled = _bool;
            if (_addmod == 1)
            {
                addToolStripMenuItem.Enabled = false;
            }
            else if (_addmod == 2)
                modifyToolStripMenuItem.Enabled = false;
            else
            {
                addToolStripMenuItem.Enabled = true;
                modifyToolStripMenuItem.Enabled = true;
            }
                
             saveToolStripMenuItem.Enabled = _bool;
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 2;
            EnableControls(true,liaddmod);

        }
        private void DeliveryLocation()
        {
            cmbLocation.Items.Add("Direct");
            cmbLocation.Items.Add("Provincial");
            cmbLocation.SelectedIndex = 0;
        }
        private void ClearTools()
        {
            txtBankCode.Text = "";
            txtProductCode.Text = "";
            txtDocStampPrice.Text = "";
            txtDescription.Text = "";
            txtChequeName.Text = "";
            txtType.Text = "";
            txtUnitPrice.Text = "";
        }
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 1;
            EnableControls(true, liaddmod);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            liaddmod = 0;
            EnableControls(false, liaddmod);
           
            ClearTools();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "Delivery Receipt Number Update", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                product.ProductCode = txtProductCode.Text;
                product.BankCode = txtBankCode.Text;
                product.ChequeName = txtChequeName.Text;
                product.Description = txtDescription.Text;
                product.ChkType = txtType.Text;
                product.DocStampPrice = double.Parse(txtDocStampPrice.Text);
                product.UnitPrice = double.Parse(txtUnitPrice.Text);
                product.Unit = txtUnit.Text;
                product.DeliveryLocation = cmbLocation.Text;
                product.DateModified = DateTime.Now;


                if (liaddmod == 2)
                {
                    proc.AddProducts(product);
                    MessageBox.Show("Data has been Added!!!","Saving Data",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                }
                else if (liaddmod == 1)
                {
                    proc.ModifyProducts(product);
                    MessageBox.Show("Data has been Updated!!!","Updating Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
                ClearTools();
                liaddmod = 0;
                EnableControls(false, liaddmod);
                DisplayAllProducts();
            }
            else
                MessageBox.Show("Process has been cancelled!!!","Cancel Process", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            txtProductCode.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBankCode_TextChanged(object sender, EventArgs e)
        {
            txtBankCode.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            txtType.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            txtUnit.CharacterCasing = CharacterCasing.Upper;

        }
    }
}