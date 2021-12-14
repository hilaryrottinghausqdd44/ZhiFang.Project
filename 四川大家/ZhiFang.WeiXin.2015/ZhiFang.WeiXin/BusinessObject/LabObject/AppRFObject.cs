using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.BusinessObject.LabObject
{
    public class AppRFObject
    {
        /// <summary>
        /// 报告信息（简单）
        /// </summary>
        public AppRFUserInfo info { get; set; }
        /// <summary>
        /// 报告生化免疫类项目
        /// </summary>
        public List<AppRIResult> list { get; set; }
        /// <summary>
        /// 报告微生物类项目
        /// </summary>
        public List<AppRMicroResult> MicroItemlist { get; set; }
        /// <summary>
        /// 报告骨髓、病理、细胞学类项目
        /// </summary>
        public List<AppRMarrowResult> Marrowlist { get; set; }
        public static AppRFObject SetValue(BSearchAccountReportForm bsarf)
        {
            AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
            if (bsarf.ReportFormTime.HasValue)
            {
                info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (bsarf.COLLECTDATE.HasValue)
            {
                info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
            }
            if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
            {
                info.PatNumber = bsarf.PatNo;
            }
            AppRFObject ARFO = new AppRFObject() { info = info };
            if (bsarf.ItemList != null)
            {
                ARFO.list = new List<AppRIResult>();
                foreach (var item in bsarf.ItemList)
                {
                    ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT, ResultStatus = item.RESULTSTATUS, ResultDesc = item.REPORTDESC, ShortCode = item.Shortcode });
                }
            }
            if (bsarf.MicroList != null && bsarf.MicroList.Count > 0)
            {
                ARFO.MicroItemlist = new List<AppRMicroResult>();
                var item = bsarf.MicroList.GroupBy(a => a.ItemName + ',' + a.ItemNo);
                foreach (var i in item)
                {
                    AppRMicroResult armr = new AppRMicroResult();
                    var tmpi = i.Key.Split(',');
                    armr.MicroItemName = tmpi[0];
                    armr.MicroItemNo = tmpi[1];
                    #region 过程描述
                    var desclist = i.Where(m => m.DescNo != 0);
                    if (desclist.Count() > 0)
                        armr.DescList = new List<string>();
                    foreach (var itemdesc in desclist)
                    {
                        armr.DescList.Add(itemdesc.DescName);
                    }
                    #endregion
                    #region 项目描述
                    var itemdesclist = i.Where(m => m.ItemDesc != null && m.ItemDesc.Trim() != "");
                    if (itemdesclist.Count() > 0)
                        armr.MicroItemDesc = itemdesclist.ElementAt(0).ItemDesc;
                    #endregion

                    armr.MicroList = new List<AppMicro>();
                    var micro = i.Where(b=>b.MicroNo!=0 && b.MicroName!=null && b.MicroName.Trim()!=null).GroupBy(b => b.MicroName + ',' + b.MicroNo);
                    foreach (var m in micro)
                    {
                        AppMicro am = new AppMicro();
                        var tmpm = m.Key.Split(',');
                        if (tmpm[0].Trim() != "" && tmpm[1] != "0")
                        {
                            am.MicroName = tmpm[0];
                            am.MicroNo = tmpm[1];
                            am.AnitList = new List<AppAnit>();
                            foreach (var a in m)
                            {
                                if (a.AntiName != null && a.AntiName.Trim() != "" && a.AntiNo != null && a.AntiNo > 0)
                                {
                                    AppAnit aa = new AppAnit();
                                    aa.AnitName = a.AntiName;
                                    aa.AnitNo = a.AntiNo.ToString();
                                    aa.MethodName = a.MethodName;
                                    aa.RefRange = a.RefRange;
                                    aa.Suscept = a.Suscept;
                                    if (a.SusQuan != null && a.SusQuan > 0)
                                        aa.SusQuan = a.SusQuan.ToString();
                                    am.MicroDesc = a.MicroDesc;
                                    am.AnitList.Add(aa);
                                }
                            }
                            armr.MicroList.Add(am);
                        }
                    }
                    ARFO.MicroItemlist.Add(armr);
                }
            }
            if (bsarf.MarrowList != null && bsarf.MarrowList.Count > 0)
            {
                ARFO.Marrowlist = new List<AppRMarrowResult>();
                foreach (var marrowitem in bsarf.MarrowList)
                {
                    //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT, ResultStatus = item.RESULTSTATUS });
                }
            }
            return ARFO;
        }

        public string ReportFormPDFUrl { get; set; }
    }
}