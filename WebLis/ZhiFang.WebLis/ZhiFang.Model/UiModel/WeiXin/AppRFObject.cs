using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.Common.Dictionary;

namespace ZhiFang.Model.UiModel.WeiXin

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
        public static AppRFObject SetValue(ReportForm bsarf)
        {
            AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.ReportFromFull.PATNO, HospitalName = bsarf.ReportFromFull.CLIENTNAME, PatientName = bsarf.ReportFromFull.CNAME, ReportId = bsarf.ReportFromFull.ReportFormID.ToString(), ReportType = bsarf.ReportFromFull.SectionType };
            if (bsarf.ReportFromFull.CHECKDATE.HasValue)
            {
                info.ReportTime = bsarf.ReportFromFull.CHECKDATE.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (bsarf.ReportFromFull.COLLECTDATE.HasValue)
            {
                info.VisitTime = bsarf.ReportFromFull.COLLECTDATE.Value.ToString("yyyy-MM-dd");
            }
            if (bsarf.ReportFromFull.PATNO != null && bsarf.ReportFromFull.PATNO.Trim() != "")
            {
                info.PatNumber = bsarf.ReportFromFull.PATNO;
            }
            switch(info.ReportType)
                {
                case "1":info.ReportType = SectionType.Normal.ToString();break;
                case "2": info.ReportType = SectionType.Micro.ToString(); break;
                case "3": info.ReportType = SectionType.NormalIncImage.ToString(); break;
                case "4": info.ReportType = SectionType.MicroIncImage.ToString(); break;
                case "5": info.ReportType = SectionType.CellMorphology.ToString(); break;
                case "6": info.ReportType = SectionType.FishCheck.ToString(); break;
                case "7": info.ReportType = SectionType.SensorCheck.ToString(); break;
                case "8": info.ReportType = SectionType.ChromosomeCheck.ToString(); break;
                case "9": info.ReportType = SectionType.PathologyCheck.ToString(); break;
                default: info.ReportType = SectionType.Normal.ToString(); break;
            }
            AppRFObject ARFO = new AppRFObject() { info = info };
            if (bsarf.ItemList != null)
            {
                ARFO.list = new List<AppRIResult>();
                foreach (var item in bsarf.ItemList)
                {
                    ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT, ResultStatus = item.RESULTSTATUS, ResultDesc = item.REPORTDESC, ShortCode = item.TESTITEMNAME });
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
                                    aa.MethodName = a.SusDesc;
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