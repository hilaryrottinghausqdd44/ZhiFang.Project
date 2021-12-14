
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBAccountHospitalSearchContext : ZhiFang.BLL.Base.BaseBLL<BAccountHospitalSearchContext>, ZhiFang.WeiXin.IBLL.IBBAccountHospitalSearchContext
	{
        //public ZhiFang.WeiXin.IBLL.IBBHospitalSearch IBBHospitalSearch { get; set; }
        public ZhiFang.WeiXin.IBLL.IBBSearchAccountReportForm IBBSearchAccountReportForm { get; set; }
        public IBRFReportItemFull IBRFReportItemFull { get; set; }
        public IBRFReportMicroFull IBRFReportMicroFull { get; set; }
        public IBRFReportMarrowFull IBRFReportMarrowFull { get; set; }
        public ZhiFang.WeiXin.IDAO.IDBSearchAccountDao IDBSearchAccountDao { get; set; }
        public List<BAccountHospitalSearchContext> SearchListBySearchAccountId(long SearchAccountId)
        {
            var bahsclist = this.SearchListByHQL(" AccountID=" + SearchAccountId + "  order by HospitalCode asc");
            return bahsclist.ToList();
        }


        public List<BSearchAccountReportForm> SearchRFListBySearchAccountId(long SearchAccountId,string OpenID,string Name,int page,int count)
        {
            //ZhiFang.Common.Log.Log.Debug(" SearchAccountId=" + SearchAccountId + " and OpenID='" + OpenID + "' and Name='" + Name + "'");
            var bahsclist = this.SearchListByHQL(" AccountID=" + SearchAccountId + " and WeiXinAccount='" + OpenID + "' order by HospitalCode asc");
            if(bahsclist!=null)
            {
                string hql = " 1=2 ";
                foreach(var a in bahsclist)
                {
                    hql += " or " + (a.FieldsCode + "='" + a.FieldsValue + "' and Name='" + Name + "' ");
                }
                var sarflist = IBBSearchAccountReportForm.SearchListByHQL("(" + hql + ")", " ReportFormTime DESC ", page, count);
                if (sarflist.list != null)
                {
                    foreach (var rf in sarflist.list)
                    {
                        switch ((ReportFormType)Enum.Parse(typeof(ReportFormType), rf.ReportFormType))
                        {
                            case ReportFormType.Normal:
                                rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.Micro:
                                rf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.NormalIncImage:
                                 rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.MicroIncImage:
                                rf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.CellMorphology:
                                rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.FishCheck:
                                rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.SensorCheck:
                                rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.ChromosomeCheck:
                                rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.PathologyCheck:
                                rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            case ReportFormType.all:
                               rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                                break;
                            default: 
                                rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList(); 
                                break;
                        }

                    }
                    return sarflist.list.ToList();
                }
               return null; 
            }
            return null;
        }

        public bool AddSearchContextByHSearchID()
        {
            if (Entity.HospitalSearchID.HasValue)
            {
                //var entitybhs = IBBHospitalSearch.Get(Entity.HospitalSearchID.Value);
                //this.Entity.FieldsCode = entitybhs.FieldsCode;
                //this.Entity.FieldsMeaning = entitybhs.FieldsMeaning;
                //this.Entity.FieldsName = entitybhs.FieldsName;
                this.Entity.UnReadCount = 0;
                this.Entity.ReadedFlag = true;
                return this.Add();
            }
            return false;
        }


        public List<BSearchAccountReportForm> SearchAccountReportFormByReportFormIndexIdList(string ReportFormIndexIdList)
        {
            List<BSearchAccountReportForm> lbsarf = new List<BSearchAccountReportForm>();
            if (ReportFormIndexIdList.IndexOf(",") == 0 && ReportFormIndexIdList.Trim() != "")
            {
                ReportFormIndexIdList = ReportFormIndexIdList.Substring(1);
            }
            lbsarf = IBBSearchAccountReportForm.SearchListByHQL(" ReportFormIndexID in (" + ReportFormIndexIdList + " ) order by ReportFormTime,HospitalCode asc").ToList();
            foreach (var rf in lbsarf)
            {
                if (rf.ReportFormType != null && rf.ReportFormType.Trim() != "")
                {
                    switch ((ReportFormType)Enum.Parse(typeof(ReportFormType), rf.ReportFormType))
                    {
                        case ReportFormType.Normal:
                            rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.Micro:
                            rf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.NormalIncImage:
                            rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.MicroIncImage:
                            rf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.CellMorphology:
                            rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.FishCheck:
                            rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.SensorCheck:
                            rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.ChromosomeCheck:
                            rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.PathologyCheck:
                            rf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        case ReportFormType.all:
                            rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                        default:
                            rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                            break;
                    }
                }
                else 
                {
                    rf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + rf.ReportFormIndexID).ToList();
                }

            }
            return lbsarf;
        }


        public BSearchAccountReportForm UpdateSearchAccountReportFormByReportFormIndexId(string ReportFormIndexId,long SearchAccountId)
        {
            List<BSearchAccountReportForm> lbsarf = new List<BSearchAccountReportForm>();

            lbsarf = IBBSearchAccountReportForm.SearchListByHQL(" ReportFormIndexID =" + ReportFormIndexId).ToList();
            if (lbsarf.Count > 0)
            {
                switch ((ReportFormType)Enum.Parse(typeof(ReportFormType), lbsarf[0].ReportFormType))
                {
                    case ReportFormType.Normal:
                        lbsarf[0].ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.Micro:
                        lbsarf[0].MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.NormalIncImage:
                        lbsarf[0].ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.MicroIncImage:
                        lbsarf[0].MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.CellMorphology:
                        lbsarf[0].MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.FishCheck:
                        lbsarf[0].MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.SensorCheck:
                        lbsarf[0].MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.ChromosomeCheck:
                        lbsarf[0].MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.PathologyCheck:
                        lbsarf[0].MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    case ReportFormType.all:
                        lbsarf[0].ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                    default:
                        lbsarf[0].ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + lbsarf[0].ReportFormIndexID).ToList();
                        break;
                }
                //IDBSearchAccountDao.UpdateByHql("update BSearchAccountReportForm as bsarf set bsarf.ExportCount=(bsarf.ExportCount+1)      where bsarf.ReportFormIndexID=" + ReportFormIndexId.ToString());
                //var tmpsaentity = IDBSearchAccountDao.Get(SearchAccountId);
                //if (tmpsaentity.RFIndexList.IndexOf(ReportFormIndexId.ToString()) >= 0)
                //{
                //    IDBSearchAccountDao.UpdateByHql("update BSearchAccount as bsa set bsa.UnReadCount=" + (tmpsaentity.UnReadCount - 1).ToString() + " , bsa.RFIndexList='" + tmpsaentity.RFIndexList.Replace("," + ReportFormIndexId.ToString(), "").ToString() + "' where bsa.Id=" + SearchAccountId);
                   
                //}
                
                return lbsarf[0];
            }
            return null;
        }
    }
}