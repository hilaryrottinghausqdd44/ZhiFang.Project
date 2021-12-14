using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using System.IO;
//using System.Web.UI;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using System.Collections;

namespace ZhiFang.BLL.Report
{
    public class PrintFrom : ShowFrom, ZhiFang.IBLL.Report.IBPrintFrom
    {
        ZhiFang.IBLL.Report.IBReportForm rfb = ZhiFang.BLLFactory.BLLFactory<IBReportForm>.GetBLL("ReportForm");
        
        /// <summary>
        /// 获取html模板
        /// </summary>
        /// <returns></returns>
        public ArrayList HtmlRepotrForm(string Fromno, int sectionno, int clientno, out int printno)
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL("PGroupPrint");
            ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pf = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL("PrintFormat");
            DataSet ds = new DataSet();


            //获取speciallyitemno
            DataTable dtitem = item.GetReportItemList(Fromno.ToString());
            string speciallyitemno = "";
            if (ds != null && dtitem.Rows.Count > 0)
            {
                for (int j = 0; j < dtitem.Rows.Count; j++)
                {
                    speciallyitemno += dtitem.Rows[j]["itemno"].ToString() + ",";
                }
            }
            speciallyitemno = speciallyitemno.Substring(0, speciallyitemno.LastIndexOf(","));

            //获取模板id
            PrintFormatNo = pg.PrintFormatNo(sectionno, clientno, speciallyitemno);
            printno = base.GetPrintFormatNo(PrintFormatNo);
            ArrayList al = new ArrayList();
            string tmphtml = "";

            if (PrintFormatNo > 0)
            {
                try
                {
                    //获取printformatno信息列表
                    Model.PrintFormat print = pf.GetModel(PrintFormatNo.ToString());
                    string showmodel = print.PintFormatAddress.ToString().Trim() + "\\" + print.Id + "\\" + print.Id + ".XSLT";
                    DataTable Fdt = arfb.GetFromInfo(Fromno.ToString());
                    Fdt = base.SetUserImage(Fdt);
                    DataTable Idt = arfb.GetFromItemList(Fromno.ToString());
                    //判断是否进行分页
                    if (Idt.Rows.Count > 0)
                    {
                        if (Idt.Rows.Count > format.ItemParaLineNum)
                        {
                            int IndexPage = ZhiFang.Tools.PagePaging.GetCountMaxPage(Idt, int.Parse(format.ItemParaLineNum.ToString()));
                            for (int i = 0; i < IndexPage; i++)
                            {
                                //下一页
                                DataTable NextPage = ZhiFang.Tools.PagePaging.NextPage(Idt, i, int.Parse(format.ItemParaLineNum.ToString()));
                                tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(NextPage, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel));
                                al.Add(tmphtml);
                            }

                        }
                        else
                        {
                            tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel));
                            al.Add(tmphtml);
                        }
                    }
                }
                catch
                {

                }
            }
            return al;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public string PrintHtml(string FormNo)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法：PrintFrom.ashx");
            string formno = "";
            int sectionno;
            int clientno;
            string tmphtml = "";
            Model.PGroupPrint pgp_m = new Model.PGroupPrint();
            DataTable set = rfb.GetReportFormFullList(FormNo);
            DateTime now = DateTime.Now;
            for (int i = 0; i < set.Rows.Count; i++)
            {
                DateTime receivedate = DateTime.Parse(set.Rows[i]["receivedate"].ToString());
                if (set.Rows[i]["formno"] != DBNull.Value && set.Rows[i]["formno"].ToString().Trim() != "")
                {
                    formno = set.Rows[i]["formno"].ToString();
                }

                if (set.Rows[i]["sectionno"].ToString() == "" || set.Rows[i]["sectionno"].ToString() == null)
                {
                    sectionno = 0;
                }
                else
                {
                    sectionno = int.Parse(set.Rows[i]["sectionno"].ToString());
                }
                if (set.Rows[i]["clientno"].ToString() == "" || set.Rows[i]["clientno"].ToString() == null)
                {
                    clientno = 0;
                }
                else
                {
                    clientno = int.Parse(set.Rows[i]["clientno"].ToString());
                }

                //导入html
                try
                {
                    int printno;
                    ArrayList aaa = this.HtmlRepotrForm(formno.ToString(), sectionno, clientno, out printno);
                    for (int n = 0; n < aaa.Count; n++)
                    {
                        if (aaa[n].ToString() != "")
                        {
                            try
                            {
                                ZhiFang.Common.Log.Log.Debug(aaa[n].ToString());
                                string tmp = aaa[n].ToString().Substring(aaa[n].ToString().IndexOf("<html"), aaa[n].ToString().Length - aaa[n].ToString().IndexOf("<html"));
                                tmp = tmp.Replace("\r\n ", " ");
                                tmp = tmp.Replace("!creatdate!", DateTime.Now.ToShortDateString());
                                if (n == 0)
                                {
                                    tmphtml += tmp;
                                }
                                else
                                {
                                    tmphtml += "<div class=\"PagePrevious\"></div>" + tmp;
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "打印文件错误" + ex.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "打印文件错误" + ex.ToString());
                }
                //进度表 
                //Response.Cookies["State"].Value = Convert.ToString(Math.Round((decimal)(5 * i / set.Rows.Count)) + 95);
            }
            //Response.Cookies["State"].Value = Convert.ToString(100);
            return tmphtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public Model.PrintFormat GetPrintModelInfo(string FormNo)
        {
            string formno = "";
            int sectionno;
            int clientno;
            string tmphtml = "";            
            Model.PGroupPrint pgp_m = new Model.PGroupPrint();
            DataTable set = rfb.GetReportFormFullList(FormNo);
            DateTime now = DateTime.Now;
            if (set.Rows.Count > 0)
            {
                DateTime receivedate = DateTime.Parse(set.Rows[0]["receivedate"].ToString());

                if (set.Rows[0]["formno"] != DBNull.Value && set.Rows[0]["formno"].ToString().Trim() != "")
                {
                    formno = set.Rows[0]["formno"].ToString();
                }

                if (set.Rows[0]["sectionno"].ToString() == "" || set.Rows[0]["sectionno"].ToString() == null)
                {
                    sectionno = 0;
                }
                else
                {
                    sectionno = int.Parse(set.Rows[0]["sectionno"].ToString());
                }
                if (set.Rows[0]["clientno"].ToString() == "" || set.Rows[0]["clientno"].ToString() == null)
                {
                    clientno = 0;
                }
                else
                {
                    clientno = int.Parse(set.Rows[0]["clientno"].ToString());
                }

                ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL("PGroupPrint");
                ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pf = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL("PrintFormat");
                DataSet ds = new DataSet();


                //获取speciallyitemno
                DataTable dtitem = item.GetReportItemList(formno.ToString());
                string speciallyitemno = "";
                if (ds != null && dtitem.Rows.Count > 0)
                {
                    for (int j = 0; j < dtitem.Rows.Count; j++)
                    {
                        speciallyitemno += dtitem.Rows[j]["itemno"].ToString() + ",";
                    }
                }
                speciallyitemno = speciallyitemno.Substring(0, speciallyitemno.LastIndexOf(","));

                //获取模板id
                PrintFormatNo = pg.PrintFormatNo(sectionno, clientno, speciallyitemno);
                return pf.GetModel(base.GetPrintFormatNo(PrintFormatNo).ToString());
            }
            else
            {
                return null;
            }
        }

        #region IBPrintFrom 成员
        /*
        public int PrintHtml(string formno, int section, int clientno, string itemno, string AddXml, int printno, StringBuilder sb, DateTime receivedate)
        {
            throw new NotImplementedException();
        }
        */
        #endregion


       
    }
}
