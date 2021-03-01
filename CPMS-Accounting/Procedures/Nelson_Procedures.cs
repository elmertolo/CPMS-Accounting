using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CPMS_Accounting.Models;
using static CPMS_Accounting.GlobalVariables;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Diagnostics;
using System.IO;
using CPMS_Accounting.Forms;
using System.Security.Cryptography;
//using ProducersBank.Services;

namespace CPMS_Accounting.Procedures
{


    public static class p
    {

        //02152021 Log4Net
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string message;

        public static bool IsKeyPressedNumeric(ref object sender, ref KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.') || ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -2)))
            {
                return true;
            }
            return false;

        }

        public static bool ValidateInputFieldsSI(string salesInvoiceNumber, string checkedBy, string approvedBy)
        {
            if (string.IsNullOrWhiteSpace(checkedBy) || string.IsNullOrWhiteSpace(approvedBy) || string.IsNullOrWhiteSpace(salesInvoiceNumber))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool ValidateInputFieldsPO(string purchaseOrderNumber, string checkedBy, string approvedBy)
        {
            if (string.IsNullOrWhiteSpace(checkedBy) || string.IsNullOrWhiteSpace(approvedBy) || string.IsNullOrWhiteSpace(purchaseOrderNumber))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static double GetVatAmount(double subTotal)
        {
            return Math.Round(subTotal / 1.12 * .12);
        }

        public static double GetNetOfVatAmount(double subTotal)
        {
            return Math.Round(subTotal / 1.12);
        }

        public static void setCrystalReportsDBInfo(ref ReportDocument rpt)
        {
            log.Info("Fetching Crystal Reports Database Information");

            TableLogOnInfo logoninfo = new TableLogOnInfo();
            foreach (CrystalDecisions.CrystalReports.Engine.Table crystalTtables in rpt.Database.Tables)
            {

                logoninfo = crystalTtables.LogOnInfo;
                logoninfo.ReportName = rpt.Name;
                logoninfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["ServerName"].ToString();
                logoninfo.ConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"].ToString();
                logoninfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["UserId"].ToString();
                logoninfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["Password"].ToString();
                logoninfo.TableName = crystalTtables.Name;
                crystalTtables.ApplyLogOnInfo(logoninfo);
                crystalTtables.Location = crystalTtables.Name;
            }
        }

        public static void FillCRReportParameters(string reportType, ref ReportDocument crystalDoucument)
        {
            log.Info("Adding Values on Crystal Report Parameters");

            switch (reportType)
            {
                case "SalesInvoice":

                    //gCrystalDocument.SetDataSource(gReportDT);
                    crystalDoucument.SetParameterValue("prHeaderReportTitle", gSIheaderReportTitle.ToString() ?? "");
                    //gSalesInvoiceFinished Global model used to supply parameters
                    crystalDoucument.SetParameterValue("prHeaderReportAddress1", gClient.Address1.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prHeaderReportAddress2", gClient.Address2.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prHeaderReportAddress3", gClient.Address3.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prHeaderCompanyName", gClient.Description.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prSalesInvoiceDate", gSalesInvoiceFinished.SalesInvoiceDateTime.ToString("MMMMM dd, yyyy") ?? "");
                    crystalDoucument.SetParameterValue("prSalesInvoiceNumber", gSalesInvoiceFinished.SalesInvoiceNumber.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prPreparedBy", gSalesInvoiceFinished.GeneratedBy.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prCheckedBy", gSalesInvoiceFinished.CheckedBy.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prApprovedBy", gSalesInvoiceFinished.ApprovedBy.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prSubtotalAmount", gSalesInvoiceFinished.TotalAmount.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prVatAmount", gSalesInvoiceFinished.VatAmount.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prNetOfVatAmount", gSalesInvoiceFinished.NetOfVatAmount.ToString() ?? "");
                    crystalDoucument.SetParameterValue("prClientCode", gClient.ClientCode.ToString() ?? "");

                    break;

                default:
                    message = "Report type not recognized.";

                    break;

            }

        }

        public static bool LoadReportPath(string reportType, ref ReportDocument crystalDocument)
        {

            string reportPath;

            //Determine path when running through IDE or not
            if (Debugger.IsAttached)
            {
                reportPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Reports\" + gClient.ShortName + "_" + reportType + ".rpt";
            }
            else
            {
                reportPath = Directory.GetCurrentDirectory().ToString() + @"\Reports\" + gClient.ShortName + "_" + reportType + ".rpt";

            }

            if (!File.Exists(reportPath))
            {
                return false;
            }

            crystalDocument.Load(reportPath);
            return true;


        }

        public static bool BatchRecordHasDuplicate(SalesInvoiceModel line, List<SalesInvoiceModel> salesInvoiceList)
        {

            //PNB Added Location
            if (gClient.ShortName == "PNB")
            {
                foreach (var item in salesInvoiceList)
                {
                    if (line.Quantity == item.Quantity &&
                        line.Batch == item.Batch &&
                        line.checkName == item.checkName &&
                        line.deliveryDate == item.deliveryDate &&
                        line.checkType == item.checkType &&
                        line.Location == item.Location)

                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                foreach (var item in salesInvoiceList)
                {
                    if (line.Quantity == item.Quantity &&
                        line.Batch == item.Batch &&
                        line.checkName == item.checkName &&
                        line.deliveryDate == item.deliveryDate &&
                        line.checkType == item.checkType)
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        delegate void SetDataSourceDelegate(ref DataTable dt, ref frmProgress progressBar, DataGridView dgv);
        public static void setDataSource(ref DataTable dt, ref frmProgress progressBar, DataGridView dgv)
        {
            // Invoke method if required:
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new SetDataSourceDelegate(setDataSource), dt, progressBar, dgv);

            }
            else
            {
                dgv.DataSource = dt;
                //progressBar.DialogResult = DialogResult.Cancel;
                progressBar.Close();

            }


        }

        public static void MessageAndLog(string message, ref log4net.ILog log, string level)
        {
            string newMessage = message.Replace("\r\n", string.Empty);

            if (level.ToLower() == "info")
            {
                MessageBox.Show(message, "INFORMATION" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                log.Info(newMessage);
            }
            if (level.ToLower() == "warn")
            {
                MessageBox.Show(message, "WARNING" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log.Warn(newMessage);
            }
            if (level.ToLower() == "error")
            {
                MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error(newMessage);
            }
            if (level.ToLower() == "fatal")
            {
                MessageBox.Show(message, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                log.Fatal(newMessage);
            }
            if (level.ToLower() == "debug")
            {
                MessageBox.Show(message, "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                log.Fatal(newMessage);
            }

        }

        //This should be pasted on Form Keypress event.
        public static void MakeEnteredCharacterCapital(KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        /// <summary>
        /// If the two SHA1 hashes are the same, returns true.
        /// Otherwise returns false.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private static bool MatchSHA1(byte[] p1, byte[] p2)
        {
            bool result = false;
            if (p1 != null && p2 != null)
            {
                if (p1.Length == p2.Length)
                {
                    result = true;
                    for (int i = 0; i < p1.Length; i++)
                    {
                        if (p1[i] != p2[i])
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static byte[] GetSHA1(string userID, string password)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userID + password));
        }

        /// <summary>
        /// The SHA1 hash string is impossible to transform back to its original string.
        /// </summary>
        public static string HashSHA1Decryption(string value)
        {
            var shaSHA1 = System.Security.Cryptography.SHA1.Create();
            var inputEncodingBytes = Encoding.ASCII.GetBytes(value);
            var hashString = shaSHA1.ComputeHash(inputEncodingBytes);

            var stringBuilder = new StringBuilder();
            for (var x = 0; x < hashString.Length; x++)
            {
                stringBuilder.Append(hashString[x].ToString("X2"));
            }
            return stringBuilder.ToString();

        }





    }
}
