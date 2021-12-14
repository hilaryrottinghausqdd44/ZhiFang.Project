using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDeleteReportPDFFile()
        {
            //ServerContract.IReportFormService reportFormServer = new ServiceWCF.ReportFormService();
            //BaseResultDataValue brdv = reportFormServer.DeleteReportPDFFile("2018-03-01", "2018-03-02");
            //Assert.AreEqual(true, brdv.success);
        }
        [TestMethod]
        public void TestDeleteReportPDFFile1()
        {
            //ServerContract.IReportFormService reportFormServer = new ServiceWCF.ReportFormService();
            //BaseResultDataValue brdv = reportFormServer.DeleteReportPDFFile("2018-07-01", "2018-08-01");
            //Assert.AreEqual(false, brdv.success);
        }

        public void TestSelectReport()
        {
            //ServerContract.IReportFormService reportFormServe = new ServiceWCF.ReportFormService();
            //BaseResultDataValue brdv = reportFormServe.SelectReport("(RECEIVEDATE >= '2018-08-27' and RECEIVEDATE < '2018-08-28')", "CName",1,25, "SerialNo = '7052700001'");
            //Assert.AreEqual(true,brdv.success);
        }
    }
}
