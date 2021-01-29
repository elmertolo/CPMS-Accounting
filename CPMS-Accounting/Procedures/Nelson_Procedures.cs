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
using CPMS_Accounting.Procedures;

namespace CPMS_Accounting.Procedures
{


    public static class p
    {

        public static string message;

        public static bool IsKeyPressedNumeric(ref object sender , ref KeyPressEventArgs e)
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

                    if (gClient.ShortName == "PNB")
                    {

                    }

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
            foreach (var item in salesInvoiceList)
            {
                if (line.Quantity == item.Quantity && 
                    line.Batch == item.Batch && 
                    line.checkName == item.checkName && 
                    line.deliveryDate == item.deliveryDate && 
                    line.drList == item.drList && 
                    line.checkType == item.checkType)
                {
                    return true;
                }
            }
            return false;
        }



    }
}
