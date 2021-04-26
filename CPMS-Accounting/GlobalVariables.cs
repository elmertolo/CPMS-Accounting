using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using CPMS_Accounting.Models;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CPMS_Accounting.Procedures;

namespace CPMS_Accounting
{ 
    public static class GlobalVariables
    {
        
        /// <summary>
        /// Global Variables to be supplied upon application login
        /// </summary>
        public static ClientListModel gClient = new ClientListModel();
        public static UserListModel gUser = new UserListModel();
        public static SalesInvoiceFinishedModel gSalesInvoiceFinished = new SalesInvoiceFinishedModel();
        public static PurchaseOrderModel gPurchaseOrderFinished = new PurchaseOrderModel();
        public static PriceListModel gProduct = new PriceListModel();
        public static ChequeTypesModel gChequeTypes = new ChequeTypesModel(); // Added by ET  April 12, 2021

        //Crystal Report Global Variable (Crystal Report Prerequisites)
        public static ReportDocument gCrystalDocument;

        //02182021 User Level Management
        public static UserLevelModel gUserLevel = new UserLevelModel();

        //02222021 Encryption
        public static bool gEncryptionOn;
        public static string gEncryptionType;

        /// <summary>
        /// This variables is used for SalesInvoice Processes only.
        /// </summary
        //variables from appconfig file=================================================
        //public static List<SalesInvoiceModel> gSalesInvoiceList = new List<SalesInvoiceModel>();

        public static int gViewReportFirst;
        public static string gHeaderReportCompanyName; //"PRODUCERS BANK";
        public static string gSIheaderReportTitle; //"SALES INVOICE";
        public static string gSIHeaderReportAddress1; //"6197 Ayala Avenue";
        public static string gSIHeaderReportAddress2; //"Salcedo Village";
        public static string gSIHeaderReportAddress3; //"Makati City";                                                                                                 //resettable variables

        //=============================================================================

        ///03102021
        ///Daily Backup
        ///Started Reading Config from Json File
        /// </summary>
        public static bool gDailyBackupOn;
        public static DateTime gLastBackupDate;
        public static string gBackupPath;

        


    }
}
