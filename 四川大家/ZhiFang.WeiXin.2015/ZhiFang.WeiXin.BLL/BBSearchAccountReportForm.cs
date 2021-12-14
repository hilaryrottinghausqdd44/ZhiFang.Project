
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBSearchAccountReportForm : ZhiFang.BLL.Base.BaseBLL<BSearchAccountReportForm>, ZhiFang.WeiXin.IBLL.IBBSearchAccountReportForm
	{
        public IDBScanningBarCodeReportFormDao IDBScanningBarCodeReportFormDao { get; set; }
        public IDBAccountHospitalSearchContextDao IDBAccountHospitalSearchContextDao { get; set; }
        public IBRFReportItemFull IBRFReportItemFull { get; set; }
        public IBRFReportMicroFull IBRFReportMicroFull { get; set; }
        public IBRFReportMarrowFull IBRFReportMarrowFull { get; set; }
        public IDAO.IDOSUserOrderFormDao IDOSUserOrderFormDao { get; set; }
        public List<BSearchAccountReportForm> SearchRF(long BScanningBarCodeReportFormID, string Barcode, string SearchUserName)
        {
            List<BSearchAccountReportForm> bsarflist = new List<BSearchAccountReportForm>();
            var tmplist = base.SearchListByHQL(" SBarCodeRFID=" + BScanningBarCodeReportFormID.ToString());
            if (tmplist != null && tmplist.Count > 0)
            {
                bsarflist = tmplist.ToList();
                foreach (var bsarf in bsarflist)
                {
                    bsarf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                    bsarf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                    bsarf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                }
                this.UpdateReadFlag(BScanningBarCodeReportFormID);
            }
            else
            {
                tmplist = base.SearchListByHQL(" Barcode='" + Barcode.ToString() + "' and Name='" + SearchUserName + "'");
                bsarflist = tmplist.ToList();
                foreach (var bsarf in bsarflist)
                {
                    bsarf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                    bsarf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                    bsarf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                }
                this.UpdateReadFlag(Barcode, SearchUserName);
            }
            IDBScanningBarCodeReportFormDao.UpdateByHql("update BScanningBarCodeReportForm as bsbcr set bsbcr.ReadedFlag=true where bsbcr.Id=" + BScanningBarCodeReportFormID );
           
            return bsarflist;
        }

        public bool UpdateReadFlag(long BScanningBarCodeReportFormID)
        {
            return this.DBDao.UpdateByHql("update BSearchAccountReportForm as bsarf set bsarf.ReadedFlag=true where bsarf.SBarCodeRFID=" + BScanningBarCodeReportFormID)>0;
        }

        public bool UpdateReadFlag(string Barcode, string Name)
        {
            return this.DBDao.UpdateByHql("update BSearchAccountReportForm as bsarf set bsarf.ReadedFlag=true where bsarf.Barcode='" + Barcode + "' and bsarf.Name='" + Name + "'") > 0;
        }

        public List<BSearchAccountReportForm> SearchRF(string Barcode, string SearchUserName)
        {
            List<BSearchAccountReportForm> bsarflist = new List<BSearchAccountReportForm>();
            var tmplist = base.SearchListByHQL(" Barcode='" + Barcode.Trim() + "' and Name='" + SearchUserName.Trim() + "'");
            bsarflist = tmplist.ToList();
            foreach (var bsarf in bsarflist)
            {
                bsarf.ItemList = IBRFReportItemFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                bsarf.MicroList = IBRFReportMicroFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
                bsarf.MarrowList = IBRFReportMarrowFull.SearchListByHQL(" ReportFormIndexID=" + bsarf.ReportFormIndexID).ToList();
            }

            return bsarflist;
        }

        public List<BAccountHospitalSearchContext> SendAddWeiXinBySearchAccount()
        {
            //bool flag=this.Add();
            //if (!flag)
            //{
            //    if(en
            //    this.Update();
            //}
            if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") != null && ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") == "1")
            {
                this.Entity.Name = ZhiFang.WeiXin.Common.SecurityHelp.MD5Encrypt(this.Entity.Name, ZhiFang.WeiXin.Common.SecurityHelp.PWDMD5Key);
            }
            if(this.Add())
            {
                if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("IsPushWeiXinReportFromMessage") == "1")
                {
                    #region where
                    List<string> lhql = new List<string>();
                    if (Entity.Barcode != null && Entity.Barcode.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='Barcode' and FieldsValue='" + Entity.Barcode.Trim() + "' and Name='" + Entity.Name + "' )");
                    }
                    if (Entity.IDNumber != null && Entity.IDNumber.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='IDNumber' and FieldsValue='" + Entity.IDNumber.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    if (Entity.MediCare != null && Entity.MediCare.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='MediCare' and FieldsValue='" + Entity.MediCare.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    if (Entity.MobileCode != null && Entity.MobileCode.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='MobileCode' and FieldsValue='" + Entity.MobileCode.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    if (Entity.PatNo != null && Entity.PatNo.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='PatNo' and FieldsValue='" + Entity.PatNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    if (Entity.TakeNo != null && Entity.TakeNo.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='TakeNo' and FieldsValue='" + Entity.TakeNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    if (Entity.VisNo != null && Entity.VisNo.Trim() != "")
                    {
                        lhql.Add(" (FieldsCode='TakeNo' and FieldsValue='" + Entity.VisNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                    }
                    string hql = " (1=2) ";
                    foreach (var h in lhql)
                    {
                        hql += " or " + h;
                    }
                    #endregion
                    ZhiFang.Common.Log.Log.Debug("push_hql:" + hql);
                    var lbahscd = IDBAccountHospitalSearchContextDao.GetListByHQL(" ( " + hql + " ) ").ToList();
                    if (lbahscd != null)
                    {
                        string AccountIDl = "0";
                        foreach (var bahscd in lbahscd)
                        {
                            AccountIDl += "," + bahscd.AccountID;
                        }
                        IDBAccountHospitalSearchContextDao.UpdateByHql("update BSearchAccount  set UnReadCount=(UnReadCount+1) ,RFIndexList=(RFIndexList+'," + this.Entity.ReportFormIndexID + "') where  id in (" + AccountIDl + ")");
                        return lbahscd;
                    }
                }

                return null;
            }
            return null;
        }
        public Dictionary<string,string> SendAddWeiXinBySearchAccountDic()
        {
            Dictionary<string, string> tmpdicopenid = new Dictionary<string, string>();
            if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") != null && ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") == "1")
            {
                this.Entity.Name = ZhiFang.WeiXin.Common.SecurityHelp.MD5Encrypt(this.Entity.Name, ZhiFang.WeiXin.Common.SecurityHelp.PWDMD5Key);
            }
            if (this.Add())
            {
                if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("IsPushWeiXinReportFromMessage") == "1")
                {
                    if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("IsPayCode") == "1" )
                    {
                        if (Entity.TakeNo != null && Entity.TakeNo.Trim() != "")
                        {
                            var uoflist = IDOSUserOrderFormDao.GetListByHQL(" PayCode='" + Entity.TakeNo + "' ");
                            if (uoflist != null && uoflist.Count > 0)
                            {
                                foreach (var i in uoflist)
                                {
                                    tmpdicopenid.Add(i.UserOpenID, i.WeiXinUserID.ToString());
                                }
                                return tmpdicopenid;
                            }
                        }
                    }
                    else
                    {
                        #region where
                        List<string> lhql = new List<string>();
                        if (Entity.Barcode != null && Entity.Barcode.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='Barcode' and FieldsValue='" + Entity.Barcode.Trim() + "' and Name='" + Entity.Name + "' )");
                        }
                        if (Entity.IDNumber != null && Entity.IDNumber.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='IDNumber' and FieldsValue='" + Entity.IDNumber.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        if (Entity.MediCare != null && Entity.MediCare.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='MediCare' and FieldsValue='" + Entity.MediCare.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        if (Entity.MobileCode != null && Entity.MobileCode.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='MobileCode' and FieldsValue='" + Entity.MobileCode.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        if (Entity.PatNo != null && Entity.PatNo.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='PatNo' and FieldsValue='" + Entity.PatNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        if (Entity.TakeNo != null && Entity.TakeNo.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='TakeNo' and FieldsValue='" + Entity.TakeNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        if (Entity.VisNo != null && Entity.VisNo.Trim() != "")
                        {
                            lhql.Add(" (FieldsCode='TakeNo' and FieldsValue='" + Entity.VisNo.Trim() + "' and Name='" + Entity.Name + "' ) ");
                        }
                        string hql = " (1=2) ";
                        foreach (var h in lhql)
                        {
                            hql += " or " + h;
                        }
                        #endregion
                        ZhiFang.Common.Log.Log.Debug("push_hql:" + hql);
                        var lbahscd = IDBAccountHospitalSearchContextDao.GetListByHQL(" ( " + hql + " ) ").ToList();
                        if (lbahscd != null)
                        {
                            string AccountIDl = "0";
                            foreach (var bahscd in lbahscd)
                            {
                                AccountIDl += "," + bahscd.AccountID;
                                tmpdicopenid.Add(bahscd.WeiXinAccount, bahscd.AccountID.ToString());
                            }
                            IDBAccountHospitalSearchContextDao.UpdateByHql("update BSearchAccount  set UnReadCount=(UnReadCount+1) ,RFIndexList=(RFIndexList+'," + this.Entity.ReportFormIndexID + "') where  id in (" + AccountIDl + ")");
                            return tmpdicopenid;
                        }
                    }
                }

                return null;
            }
            return null;
        }

        //由于未考虑附属检测账户（亲友）的报告查询，先按照消费码进行查询（暂行办法）
        //后期如果确定了查询方式
        //1、没有附属账户，则进行OPenID关联报告查询
        //2、附属账户查询，进行附属账户关联报告查询
        //以上两种查询都需要更改微信的报告接收服务并更改BSearchAccountReportForm实体。
        public ZhiFang.Entity.Base.EntityList<BSearchAccountReportForm> SearchListByUserPayCodeAndDateRoundType(string UserOpenID, UserSearchReportDataRoundType userSearchReportDateRoundType,int page,int limit)
        {
            string hql = "";

            IList<OSUserOrderForm> tmposuoflist = IDOSUserOrderFormDao.GetListByHQL(" UserOpenID= '" + UserOpenID + "' and Status= "+UserOrderFormStatus.完全使用.Key+ " and IsUse=1 ");

            List<string> tmposuofopenidlist = new List<string>();

            if (tmposuoflist != null && tmposuoflist.Count > 0)
            {
                foreach (var uof in tmposuoflist)
                {
                    tmposuofopenidlist.Add(uof.PayCode);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("SearchListByUserPayCodeAndDateRoundType,未找到订单列表，UserOpenID：" + UserOpenID);
                return null;
            }

            switch (userSearchReportDateRoundType)
            {
                case (UserSearchReportDataRoundType.OneMonth):
                    hql = " DataAddTime>='"+ DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss")+"' ";
                    break;
                case (UserSearchReportDataRoundType.SixMonth):
                    hql = " DataAddTime>='" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    break;
                case (UserSearchReportDataRoundType.SixMonthBefore):
                    hql = " DataAddTime<='" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    break;
                default:
                    ZhiFang.Common.Log.Log.Error("SearchListByUserPayCodeAndDateRoundType,未找到userSearchReportDateRoundType：" + userSearchReportDateRoundType.ToString());
                    return null;
                    break;
            }

            hql += " and  TakeNo in ('" + string.Join("','", tmposuofopenidlist.ToArray()) + "') ";

            ZhiFang.Common.Log.Log.Debug("SearchListByUserPayCodeAndDateRoundType,hql：" + hql);
            var list= SearchListByHQL(hql, "DataAddTime desc ", page, limit);
            return list;
        }
    }
}