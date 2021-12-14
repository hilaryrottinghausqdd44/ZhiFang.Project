using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using System.Data;
using System.Reflection;
using ZhiFang.WeiXin.Common;
using System.IO;
using ZhiFang.WeiXin.Entity.Statistics;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using System.Linq;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSUserConsumerForm : BaseBLL<OSUserConsumerForm>, ZhiFang.WeiXin.IBLL.IBOSUserConsumerForm
    {
        IBBParameter IBBParameter { get; set; }
        IBOSUserOrderForm IBOSUserOrderForm { get; set; }
        IDOSUserOrderItemDao IDOSUserOrderItemDao { get; set; }
        IDOSUserConsumerItemDao IDOSUserConsumerItemDao { get; set; }
        IDOSUserOrderFormDao IDOSUserOrderFormDao { get; set; }
        public bool OSDoctorBonusIByIdStr(string idStr, string doctorBonusID)
        {
            return ((IDOSUserConsumerFormDao)base.DBDao).OSDoctorBonusIByIdStr(idStr, doctorBonusID);
        }

        public void GetDoctorUserConsumerInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID)
        {
            IList<OSUserConsumerVO> OSUserConsumerList = new List<OSUserConsumerVO>();
            IList<OSUserConsumerForm> list = this.SearchListByHQL(" osuserconsumerform.DoctorAccountID=" + DoctorAccountID.ToString() + " and osuserconsumerform.Status=1");
            if (list != null && list.Count > 0)
            {
                foreach (OSUserConsumerForm ucf in list)
                {
                    OSUserConsumerVO ucfVO = new OSUserConsumerVO();
                    ucfVO.Id = ucf.Id;
                    ucfVO.DOFID = ucf.DOFID;
                    ucfVO.CFCode = ucf.OSUserConsumerFormCode;
                    ucfVO.Price = ucf.AdvicePrice;
                    ucfVO.DT = ucf.DataAddTime;
                    OSUserConsumerList.Add(ucfVO);
                }
            }
            doctorChargeVO.OSUserConsumerList = OSUserConsumerList;
        }

        public void GetDoctorUserConsumerInfoByDay(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID, int page, int limit)
        {
            doctorChargeVO.OSUserConsumerDayList = new List<OSUserConsumerDayVO>();
            if (page <= 0)
                page = 1;
            if (limit <= 0)
                limit = 1;            
            DateTime startday = DateTime.Now.AddDays(-1 * (page * limit-1));
            DateTime endday = DateTime.Now.AddDays(-1 * (page - 1) * limit);
            
            string start = startday.ToString("yyyy-MM-dd") + " 00:00:00";
            string end = endday.ToString("yyyy-MM-dd") + " 23:59:59";
            ZhiFang.Common.Log.Log.Debug(" osuserconsumerform.DoctorAccountID=" + DoctorAccountID.ToString() + " and osuserconsumerform.Status=1 and DataAddTime>='" + start + "' and DataAddTime<='" + end + "' ");
            IList<OSUserConsumerForm> list = this.SearchListByHQL(" osuserconsumerform.DoctorAccountID=" + DoctorAccountID.ToString() + " and osuserconsumerform.Status=1 and DataAddTime>='" + start + "' and DataAddTime<='" + end + "' ");
            for (DateTime t = startday; t <= endday; t = t.AddDays(1))
            {
                OSUserConsumerDayVO tmp = new OSUserConsumerDayVO();
                tmp.DateTime = t.ToString("yyyy-MM-dd");
                if (list != null && list.Count > 0)
                {
                    //ZhiFang.Common.Log.Log.Debug("1"+t.ToString("yyyy-MM-dd HH:mm:ss"));
                    var tmpdb = list.Where(a => a.DataAddTime.Value.Date == t.Date);
                    if (tmpdb != null && tmpdb.Count() > 0)
                    {
                        //ZhiFang.Common.Log.Log.Debug("2");
                        if (tmpdb.Sum(a => a.AdvicePrice).HasValue)
                        {
                            //ZhiFang.Common.Log.Log.Debug("3");
                            tmp.Price = tmpdb.Sum(a => a.AdvicePrice).Value;
                        }
                        tmp.Count = tmpdb.Count();
                        doctorChargeVO.OSUserConsumerDayList.Add(tmp);
                    }
                }                
            }
        }

        public EntityList<OSUserConsumerFormVO> GetGetOSUserConsumerForm(string start, string end, string DoctorAccountID, int page, int limit)
        {
            start = Convert.ToDateTime(start).ToString("yyyy-MM-dd 00:00:00");
            end = Convert.ToDateTime(end).ToString("yyyy-MM-dd 23:59:59");
            return TransVO(this.SearchListByHQL(" osuserconsumerform.DoctorAccountID=" + DoctorAccountID.ToString() + " and osuserconsumerform.Status=1 and DataAddTime>='" + start + "' and DataAddTime<='" + end + "' ",page, limit));
        }

        private EntityList<OSUserConsumerFormVO> TransVO(EntityList<OSUserConsumerForm> entityList)
        {
            if (entityList.list != null && entityList.list.Count > 0)
            {
                EntityList<OSUserConsumerFormVO> tmp = new EntityList<OSUserConsumerFormVO>();
                tmp.count = entityList.count;
                tmp.list = new List<OSUserConsumerFormVO>();
                for (int i = 0; i < entityList.list.Count; i++)
                {
                    OSUserConsumerFormVO osucfvo = new OSUserConsumerFormVO();
                    osucfvo.AD = entityList.list[i].AreaID;
                    osucfvo.AP = entityList.list[i].AdvicePrice;
                    osucfvo.DAID = entityList.list[i].DoctorAccountID;
                    osucfvo.DataAddTime = entityList.list[i].DataAddTime;
                    osucfvo.DataTimeStamp = entityList.list[i].DataTimeStamp;
                    osucfvo.DataUpdateTime = entityList.list[i].DataUpdateTime;
                    osucfvo.Discount = entityList.list[i].Discount;
                    osucfvo.DispOrder = entityList.list[i].DispOrder;
                    osucfvo.DN = entityList.list[i].DoctorName;
                    osucfvo.DoctorOpenID = entityList.list[i].DoctorOpenID;
                    osucfvo.DOFID = entityList.list[i].DOFID;
                    osucfvo.DP = entityList.list[i].DiscountPrice;
                    osucfvo.EmpID = entityList.list[i].EmpID;
                    osucfvo.EmpName = entityList.list[i].EmpAccount;
                    osucfvo.GMP = entityList.list[i].GreatMasterPrice;
                    osucfvo.HD = entityList.list[i].HospitalID;
                    osucfvo.Id = entityList.list[i].Id;
                    osucfvo.IsUse = entityList.list[i].IsUse;
                    osucfvo.LabID = entityList.list[i].LabID;
                    osucfvo.Memo = entityList.list[i].Memo;
                    osucfvo.MP = entityList.list[i].MarketPrice;
                    osucfvo.NRQFID = entityList.list[i].NRQFID;
                    osucfvo.OID = entityList.list[i].OrgID;
                    osucfvo.OSDBID = entityList.list[i].OSDoctorBonusID;
                    osucfvo.P = entityList.list[i].Price;
                    osucfvo.PC = entityList.list[i].PayCode;
                    osucfvo.Status = entityList.list[i].Status;
                    osucfvo.UAID = entityList.list[i].UserAccountID;
                    osucfvo.UCFC = entityList.list[i].OSUserConsumerFormCode;
                    osucfvo.UN = entityList.list[i].UserName;
                    osucfvo.UOID = entityList.list[i].UserOpenID;
                    osucfvo.UWXUID = entityList.list[i].UserWeiXinUserID;
                    osucfvo.WeblisOrgID = entityList.list[i].WeblisSourceOrgID;
                    osucfvo.WeblisOrgName = entityList.list[i].WeblisSourceOrgName;
                    osucfvo.WeiXinUserID = entityList.list[i].WeiXinUserID;
                    tmp.list.Add(osucfvo);
                }
                return tmp;
            }
            return null;
        }

        public BaseResultDataValue SaveOSUserConsumerForm(long NRequestFormNo, NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityList<BWeiXinAccount> dtUserAccount = new EntityList<BWeiXinAccount>();
            EntityList<BDoctorAccount> dtDoctAccount = new EntityList<BDoctorAccount>();
            EntityList<OSDoctorOrderForm> dtdof = new EntityList<OSDoctorOrderForm>();
            EntityList<OSUserOrderForm> dtuof = new EntityList<OSUserOrderForm>();
            EntityList<OSUserOrderItem> dtuoi = new EntityList<OSUserOrderItem>();
            brdv = IBOSUserOrderForm.CheckUserOrderForm(jsonentity.PayCode, out dtUserAccount, out dtDoctAccount, out dtdof, out dtuof, out dtuoi);
            if (!brdv.success)
            {
                return brdv;
            }
            brdv.success = SaveOSUserConsumerFormByNReqeustFormNo(NRequestFormNo, dtuof.list[0], jsonentity);
            brdv.ResultDataValue = brdv.success.ToString();
            return brdv;
        }
        public bool SaveOSUserConsumerFormByNReqeustFormNo(long nRequestFormNo, OSUserOrderForm DrOSUserOrderForm, NrequestCombiItemBarCodeEntity jsonentity) {
            try
            {
                string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                OSUserConsumerForm osucf = new OSUserConsumerForm();
                DateTime dtnow = DateTime.Now;
                OSUserOrderForm osuof = DrOSUserOrderForm;
                long osucfid = GUIDHelp.GetGUIDLong();
                string OSUserConsumerFormCode = dtnow.ToString("yyyyMMdd") + GUIDHelp.GetGUIDString().Substring(0, 5) + dtnow.ToString("fff") + dtnow.ToString("HHmmss");
                osucf.AreaID = osuof.AreaID;
                osucf.HospitalID = osuof.HospitalID;
                osucf.Id = osucfid;
                osucf.OSUserConsumerFormCode = OSUserConsumerFormCode;
                osucf.NRQFID = nRequestFormNo;
                osucf.DOFID = osuof.DOFID;
                osucf.DoctorAccountID = osuof.DoctorAccountID;
                osucf.WeiXinUserID = osuof.WeiXinUserID;
                osucf.DoctorOpenID = osuof.DoctorOpenID;
                osucf.DoctorName = osuof.DoctorName;
                osucf.MarketPrice = osuof.MarketPrice;
                osucf.GreatMasterPrice = osuof.GreatMasterPrice;
                osucf.DiscountPrice = osuof.DiscountPrice;
                osucf.Discount = osuof.Discount;
                osucf.Price = osuof.Price;
                osucf.AdvicePrice = osuof.AdvicePrice;
                osucf.UserAccountID = osuof.UserAccountID;
                osucf.UserWeiXinUserID = osuof.UserWeiXinUserID;
                osucf.UserName = osuof.UserName;
                osucf.UserOpenID = osuof.UserOpenID;
                osucf.Status = 1;//Status：1消费成功、2消费失败、3已结算。
                osucf.PayCode = osuof.PayCode;
                osucf.Memo = osuof.Memo;
                osucf.IsUse = true;
                osucf.OrgID = long.Parse(jsonentity.NrequestForm.WebLisSourceOrgID);
                osucf.WeblisSourceOrgID = jsonentity.NrequestForm.WebLisSourceOrgID;
                osucf.WeblisSourceOrgName = jsonentity.NrequestForm.WebLisSourceOrgName;
                osucf.EmpID =  long.Parse(EmployeeID);
                osucf.EmpAccount = EmployeeName;
                osucf.ConsumerAreaID = long.Parse(jsonentity.NrequestForm.ClientNo);
                osucf.DataAddTime = dtnow;
                DBDao.Save(osucf);

                IList<OSUserOrderItem> dsosuoi = IDOSUserOrderItemDao.GetListByHQL("osuserorderitem.UOFID=" + osuof.Id);

                if (dsosuoi != null  && dsosuoi.Count > 0 )
                {
                    for (int i = 0; i < dsosuoi.Count; i++)
                    {
                        OSUserOrderItem osuoi = dsosuoi[i];
                        OSUserConsumerItem osuci = new OSUserConsumerItem();
                        osuci.AreaID = osuoi.AreaID;
                        osuci.Discount = osuoi.Discount;
                        osuci.DiscountPrice = osuoi.DiscountPrice;
                        osuci.GreatMasterPrice = osuoi.GreatMasterPrice;
                        osuci.HospitalID = osuoi.HospitalID;
                        osuci.IsUse = osuoi.IsUse;
                        osuci.ItemID = long.Parse(osuoi.ItemID.ToString());
                        osuci.ItemNo = osuoi.ItemNo;
                        osuci.ItemCName = osuoi.ItemCName;
                        osuci.MarketPrice = osuoi.MarketPrice;
                        osuci.Memo = osuoi.Memo;
                        osuci.OSUserConsumerFormID = osucfid;
                        osuci.Id = GUIDHelp.GetGUIDLong();
                        osuci.RecommendationItemProductID = osuoi.RecommendationItemProductID;
                        osuci.UOIID = osuoi.Id;
                        osuci.DataAddTime = dtnow;
                        IDOSUserConsumerItemDao.Save(osuci);
                    }
                }
                string HQL = " update OSUserOrderForm set Status=4,ConsumerFinishedTime='" + dtnow.ToString("yyyy-MM-dd HH:mm:ss") + "' ,OS_UserConsumerFormID=" + osucfid + ",OS_UserConsumerFormCode='" + OSUserConsumerFormCode + "' where UOFID=" + osuof.Id ;
                IDOSUserOrderFormDao.UpdateByHql(HQL);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("SaveOSUserConsumerForm.异常：" + e.ToString());
                return false;
            }
        }
        
    }
}