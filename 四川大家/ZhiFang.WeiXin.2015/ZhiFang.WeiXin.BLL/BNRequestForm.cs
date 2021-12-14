
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using FastReport;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BNRequestForm : BaseBLL<NRequestForm>, ZhiFang.WeiXin.IBLL.IBNRequestForm
    {
        IDNRequestItemDao IDNRequestItemDao { get; set; }
        IDBarCodeFormDao IDBarCodeFormDao { get; set; }
        IDCLIENTELEDao IDCLIENTELEDao { get; set; }
        public SortedList<string, string> StatisticsNRequestForm_Frx(string itemwhere, string where, long LabId, string empID, string employeeName, Dictionary<string, string> title = null, string FileType = "PDF")
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("InspectionName");
            dataTable.Columns.Add("ReceiveName");
            dataTable.Columns.Add("InspectionTime");

            dataTable.Columns.Add("RequestTime");
            dataTable.Columns.Add("NrequestFormNo");
            dataTable.Columns.Add("BarCode");
            dataTable.Columns.Add("PersonID");
            dataTable.Columns.Add("SickTypeName");
            dataTable.Columns.Add("PatNo");
            dataTable.Columns.Add("CName");
            dataTable.Columns.Add("Gennder");
            dataTable.Columns.Add("Age");
            dataTable.Columns.Add("Doctor");
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("OperTime");
            //申请单
            IList<NRequestForm> nRequestForms = DBDao.GetListByHQL(where);  
            if (nRequestForms.Count <= 0)
            {
                return new SortedList<string, string>();
            }
            List<NRequestFormVO> nrvl = new List<NRequestFormVO>();
            foreach (var item in nRequestForms)
            {
                NRequestFormVO nrv = new NRequestFormVO();
                nrv = ZhiFang.WeiXin.Common.ClassMapperHelp.GetMapper<NRequestFormVO, NRequestForm>(item);
                nrv.NrequestFormNo = item.Id;
                IList<NRequestItem> nrequestitemnl = IDNRequestItemDao.GetListByHQL("nrequestitem.NRequestFormNo=" + item.Id);
                if (nrequestitemnl.Count > 0)
                {
                    long barcodeformno = nrequestitemnl.GroupBy(a => a.BarCodeFormNo).First().ToList()[0].BarCodeFormNo;
                    BarCodeForm bcf = IDBarCodeFormDao.Get(barcodeformno);
                    if (string.IsNullOrEmpty(nrv.BarCodeNo))
                    {
                        nrv.BarCodeNo = bcf.BarCode;
                    }
                    nrv.ItemName = bcf.ItemName;
                }
                if (!string.IsNullOrEmpty(item.OperDate.ToString()) && !string.IsNullOrEmpty(item.OperTime.ToString()))
                {
                    string ndate = item.OperDate.ToString().Split(' ')[0] + " " + item.OperTime.ToString().Split(' ')[1];
                    nrv.OperDate = DateTime.Parse(ndate);
                }
                if (!string.IsNullOrEmpty(item.CollectDate.ToString()) && !string.IsNullOrEmpty(item.CollectTime.ToString()))
                {
                    string ndate = item.CollectDate.ToString().Split(' ')[0] + " " + item.CollectTime.ToString().Split(' ')[1];
                    nrv.CollectDate = DateTime.Parse(ndate);
                }
                if (string.IsNullOrEmpty(item.WebLisSourceOrgName))
                {
                    CLIENTELE cLIENTELE = IDCLIENTELEDao.Get(long.Parse(item.WebLisSourceOrgID));
                    nrv.WebLisSourceOrgName = cLIENTELE.CNAME;
                }
                
                nrvl.Add(nrv);
            }
            
            foreach (var item in nrvl)
            {
                 DataRow dataRow = dataTable.NewRow();
                //dataRow["ReceiveName"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefualtDeliveryRuleLibName");
                dataRow["ReceiveName"] = item.WebLisSourceOrgName;
                dataRow["InspectionName"] = item.ClientName;
                dataRow["OperTime"] = item.OperDate.HasValue ? item.OperDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                dataRow["InspectionTime"] = item.OperDate.HasValue ? item.OperDate.Value.ToString("yyyy-MM-dd") : "";
                dataRow["RequestTime"] = DateTime.Now.ToString("yyyy-MM-dd");
               
                dataRow["BarCode"] = item.BarCodeNo;
                //项目名称
                dataRow["ItemName"] = item.ItemName;

                dataRow["PersonID"] = item.PersonID;
                dataRow["SickTypeName"] = item.JztypeName;
                dataRow["PatNo"] = item.PatNo;
                dataRow["CName"] = item.CName;
                dataRow["NrequestFormNo"] = item.NrequestFormNo;
                dataRow["Gennder"] = item.GenderName;
                dataRow["Doctor"] = item.DoctorName;
                dataRow["Age"] = item.Age + item.AgeUnitName;
                dataTable.Rows.Add(dataRow);
                
            }
            return StatisticsNRequestFormFull_Frx(dataTable, title, FileType);
        }
        public SortedList<string, string> StatisticsNRequestFormFull_Frx(DataTable dataTable, Dictionary<string, string> titledic, string FileType = "PDF")
        {
            DataView dt = dataTable.DefaultView;
            dt.Sort = "ReceiveName";
            dataTable = dt.ToTable();
            var result = dataTable.Rows.Cast<DataRow>().GroupBy(a => a["ReceiveName"].ToString());
            SortedList<string, DataTable> result_Table = new SortedList<string, DataTable>();
            foreach (var item in result)
            {

                var igrp = item.GroupBy(b => b["NrequestFormNo"].ToString());
                foreach (var igp in igrp)
                {
                    var tb = dataTable.Copy();
                    tb.Clear();
                    DataRow dr = tb.NewRow();
                    dr.ItemArray = igp.First().ItemArray;
                    //for (int i = 1; i < igp.Count(); i++)
                    // {
                    //dr["BarCode"] += "\r\n" + item.ElementAt(i)["BarCode"];
                    //dr["ItemName"] += "\r\n" + item.ElementAt(i)["ItemName"];
                    // }
                    if (result_Table.ContainsKey(item.Key))
                    {
                        result_Table[item.Key].Rows.Add(dr.ItemArray);
                    }
                    else
                    {
                        tb.Rows.Add(dr);
                        result_Table.Add(item.Key, tb);
                    }
                }
            }
            // 生成文件
            SortedList<string, string> pdflist = new SortedList<string, string>();
            foreach (var item in result_Table)
            {
                FastReport.Report report = new FastReport.Report();
                report.Clear();
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\PrintRequestFormPDF.frx");
                if (titledic != null && titledic.Count > 0)
                {
                    for (int i = 0; i < titledic.Count; i++)
                    {
                        TextObject text = (TextObject)report.FindObject(titledic.ElementAt(i).Key.ToString());
                        if (text != null)
                        {
                            text.Text = titledic.ElementAt(i).Value.ToString();
                        }
                    }
                }
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(item.Value);
                dataSet.Tables[0].TableName = "Table";
                report.RegisterData(dataSet);
                report.Prepare();
                string basedir = System.AppDomain.CurrentDomain.BaseDirectory;
                string FilePath = "DeliveryDirPdf" + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".pdf";
                switch (FileType.ToUpper().Trim())
                {
                    case "PDF":
                        FilePath = "DeliveryDirPdf" + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".pdf";
                        if (!Directory.Exists(basedir + "DeliveryDirPdf"))
                        {
                            Directory.CreateDirectory(basedir + "DeliveryDirPdf");
                        }
                        report.Export(new FastReport.Export.Pdf.PDFExport(), basedir + FilePath);
                        break;
                    case "EXCEL":
                        FilePath = "DeliveryDirPdf" + "\\" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt() + ".xlsx";
                        //判断并创建目录
                        if (!Directory.Exists(basedir + ZhiFang.Common.Public.ConfigHelper.GetConfigString("DeliveryDir")))
                        {
                            Directory.CreateDirectory(basedir + ZhiFang.Common.Public.ConfigHelper.GetConfigString("DeliveryDir"));
                        }
                        FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                        report.Export(export, basedir + FilePath);
                        break;
                }
                //判断并创建目录               
                report.Dispose();
                pdflist.Add(item.Key, FilePath);
            }

            return pdflist;
        }
    }
}