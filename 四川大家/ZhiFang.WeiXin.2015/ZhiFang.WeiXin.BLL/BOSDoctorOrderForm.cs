using ZhiFang.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSDoctorOrderForm : BaseBLL<OSDoctorOrderForm>, ZhiFang.WeiXin.IBLL.IBOSDoctorOrderForm
    {
        IDAO.IDBDoctorAccountDao IDBDoctorAccountDao { get; set; }
        IDAO.IDBWeiXinAccountDao IDBWeiXinAccountDao { get; set; }
        IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IDAO.IDOSDoctorOrderItemDao IDOSDoctorOrderItemDao { get; set; }
        IDAO.IDBLabTestItemDao IDBLabTestItemDao { get; set; }
        IDAO.IDOSRecommendationItemProductDao IDOSRecommendationItemProductDao { get; set; }
        IBBParameter IBBParameter { get; set; }

        public BaseResultDataValue AddOSDoctorOrderFormVO(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageTestAction, string DoctorId, Entity.ViewObject.Request.OSDoctorOrderFormVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BDoctorAccount da = IDBDoctorAccountDao.Get(long.Parse(DoctorId));
            BWeiXinAccount dw = IDBWeiXinAccountDao.Get(da.WeiXinUserID.Value);

            long DOFID = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
            if (da.WeiXinUserID == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关的微信账号！";
                return brdv;
            }
            if (entity.OrderItem == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到医嘱项目信息！";
                return brdv;
            }

            BWeiXinAccount pw = IDBWeiXinAccountDao.Get(entity.UserAccountID);
            if (pw == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关的患者微信账号！";
                return brdv;
            }
            string collectionpriceDefault = (string)IBBParameter.GetCache(BParameterParaNoClass.DefaultCollectionPrice.Key.ToString());
            OSDoctorOrderForm osdof = new OSDoctorOrderForm();
            osdof.Id = DOFID;
            osdof.IsUse = true;
            osdof.HospitalID = da.HospitalID;
            osdof.HospitalName = da.HospitalName;
            osdof.DoctorOpenID = dw.WeiXinAccount;
            osdof.DoctorAccountID = long.Parse(DoctorId);
            osdof.DoctorName = da.Name;
            osdof.DoctorWeiXinUserID = dw.Id;
            osdof.Age = pw.Birthday.HasValue ? DateTime.Now.Year - pw.Birthday.Value.Year : 0;//后期改为从数据库中取
            osdof.AgeUnitID = 1;//默认为岁
            osdof.AgeUnitName = "岁";
            osdof.AreaID = entity.AreaID;
            osdof.DeptID = da.HospitalDeptID;
            osdof.DeptName = da.HospitalDeptName;
            osdof.Memo = entity.Memo;
            //osdof.PatNo = entity.PatNo;
            osdof.SexID = pw.SexID;
            osdof.SexName = pw.SexID == 1 ? "男" : "女";
            osdof.Status = long.Parse(DoctorOrderFormStatus.下医嘱单.Key);
            osdof.UserAccountID = pw.Id;// entity.UserAccountID;
            osdof.UserName = (pw.Name != null && pw.Name.Trim() != "") ? pw.Name : pw.UserName;// entity.UserName;
            osdof.UserOpenID = pw.WeiXinAccount;// entity.UserOpenID;
            osdof.UserWeiXinUserID = pw.Id;
            //osdof.FeatureCode = "";
            osdof.FeatureCode = Common.NextRuleNumber.GetFeatureCode();
            osdof.CollectionFlag = entity.CollectionFlag;
            osdof.DoctMobileCode = dw.MobileCode;
            osdof.UserMobileCode = pw.MobileCode;
            if (entity.CollectionFlag)
            {
                if (collectionpriceDefault != null && collectionpriceDefault.Trim() != "")
                {
                    osdof.CollectionPrice = double.Parse(collectionpriceDefault);
                }
                else
                {
                    osdof.CollectionPrice = 0;
                }
            }
            if (!da.DoctorAccountType.HasValue || da.DoctorAccountType.Value == 1)
            {
                osdof.TypeID = long.Parse(OSDoctorOrderFormType.普通.Key);
                osdof.TypeName = OSDoctorOrderFormType.普通.Value.Name;
            }
            if (da.DoctorAccountType.HasValue && da.DoctorAccountType.Value == 2)
            {
                osdof.TypeID = long.Parse(OSDoctorOrderFormType.内部.Key);
                osdof.TypeName = OSDoctorOrderFormType.内部.Value.Name;
            }
            string OrderFormContent = "";
            if (DBDao.Save(osdof))
            {
                List<string> RecommendationItemProductIDList = new List<string>();
                List<string> ItemProductIDList = new List<string>();

                foreach (var item in entity.OrderItem)
                {
                    if (item.RecommendationItemProductID.HasValue)
                    {
                        RecommendationItemProductIDList.Add(item.RecommendationItemProductID.Value.ToString());
                    }
                    else
                    {
                        ItemProductIDList.Add(item.ItemID.ToString());
                    }
                }
                IList<OSRecommendationItemProduct> RecommendationItemProductList = (RecommendationItemProductIDList.Count > 0) ? IDOSRecommendationItemProductDao.GetListByHQL(" Id in (" + string.Join(",", RecommendationItemProductIDList.ToArray()) + ") and  AreaID =" + entity.AreaID) : new List<OSRecommendationItemProduct>();

                IList<BLabTestItem> BLabTestItemList = (ItemProductIDList.Count > 0) ? IDBLabTestItemDao.GetListByHQL(" Id in (" + string.Join(",", ItemProductIDList.ToArray()) + ") ") : new List<BLabTestItem>();

                foreach (var item in entity.OrderItem)
                {
                    OSDoctorOrderItem osdo = new OSDoctorOrderItem();
                    osdo.AreaID = osdof.AreaID;
                    osdo.DOFID = DOFID;
                    osdo.HospitalID = da.HospitalID;
                    osdo.ItemID = item.ItemID;

                    #region 价格赋值
                    if (item.RecommendationItemProductID.HasValue)
                    {
                        osdo.RecommendationItemProductID = item.RecommendationItemProductID;
                        if (RecommendationItemProductList != null && RecommendationItemProductList.Count() > 0)
                        {
                            var rip = RecommendationItemProductList.Where(a => a.Id == item.RecommendationItemProductID);
                            if (rip != null && rip.Count() > 0)
                            {
                                osdo.DiscountPrice = rip.ElementAt(0).DiscountPrice;
                                //osdo.GreatMasterPrice = rip.ElementAt(0).DiscountPrice;
                                osdo.GreatMasterPrice = rip.ElementAt(0).GreatMasterPrice;
                                osdo.MarketPrice = rip.ElementAt(0).MarketPrice;
                                osdo.ItemID = rip.ElementAt(0).ItemID;
                                osdo.ItemCName = rip.ElementAt(0).CName;
                                osdo.ItemNo = rip.ElementAt(0).ItemNo;
                                osdo.ItemBonusPrice = rip.ElementAt(0).BonusPercent;
                                if (da.DoctorAccountType.HasValue && da.DoctorAccountType.Value == 2)
                                {
                                    osdo.ItemBonusPrice = 0;
                                }
                                OrderFormContent += osdo.ItemCName + ",";
                            }
                            else
                            {
                                throw new Exception("AddOSDoctorOrderFormVO.未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                            }
                        }
                        else
                        {
                            throw new Exception("AddOSDoctorOrderFormVO.RecommendationItemProductList为空或未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                        }
                    }
                    else
                    {
                        if (BLabTestItemList != null && BLabTestItemList.Count() > 0)
                        {
                            var rip = BLabTestItemList.Where(a => a.Id == item.ItemID);
                            if (rip != null && rip.Count() > 0)
                            {
                                if (da.DoctorAccountType.HasValue && da.DoctorAccountType.Value == 2)
                                {
                                    osdo.DiscountPrice = rip.ElementAt(0).GreatMasterPrice;
                                    osdo.GreatMasterPrice = rip.ElementAt(0).GreatMasterPrice;
                                    osdo.ItemBonusPrice = 0;
                                }
                                else
                                {
                                    osdo.DiscountPrice = rip.ElementAt(0).Price;
                                    osdo.GreatMasterPrice = rip.ElementAt(0).Price;
                                    osdo.ItemBonusPrice = rip.ElementAt(0).BonusPercent;
                                }
                                osdo.MarketPrice = rip.ElementAt(0).MarketPrice;
                                osdo.ItemID = rip.ElementAt(0).Id;
                                osdo.ItemCName = rip.ElementAt(0).CName;
                                osdo.ItemNo = rip.ElementAt(0).ItemNo;
                                OrderFormContent += osdo.ItemCName + ",";
                            }
                            else
                            {
                                throw new Exception("AddOSDoctorOrderFormVO.未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                            }
                        }
                        else
                        {
                            throw new Exception("AddOSDoctorOrderFormVO.RecommendationItemProductList为空或未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                        }
                    }
                    #endregion
                    IDOSDoctorOrderItemDao.Save(osdo);
                }

                PushWeiXin(PushWeiXinMessageTestAction, entity, OrderFormContent, DOFID);
            }
            else
            {
                throw new Exception("AddOSDoctorOrderFormVO.新增医嘱单异常！");
            }
            return brdv;
        }

        public BaseResultDataValue AddOSDoctorOrderFormVOByUser(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageTestAction, Entity.ViewObject.Request.OSDoctorOrderFormVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //BDoctorAccount da = IDBDoctorAccountDao.Get(long.Parse(DoctorId));
            // BWeiXinAccount dw = IDBWeiXinAccountDao.Get(da.WeiXinUserID.Value);

            long DOFID = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
            if (entity.UserWeiXinUserID == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关的微信账号！";
                return brdv;
            }
            if (entity.OrderItem == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到医嘱项目信息！";
                return brdv;
            }

            BWeiXinAccount pw = IDBWeiXinAccountDao.Get(entity.UserAccountID);
            if (pw == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到相关的患者微信账号！";
                return brdv;
            }
            string collectionpriceDefault =IBBParameter.SearchListByParaNo(BParameterParaNoClass.DefaultCollectionPrice.Key.ToString()).First().ParaValue;
            OSDoctorOrderForm osdof = new OSDoctorOrderForm();
            osdof.Id = DOFID;
            osdof.IsUse = true;
            var hospital=ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("DefaultHospital");
            if (string.IsNullOrEmpty(hospital))
            {
                brdv.success = false;
                brdv.ErrorInfo = "未找到默认医院！";
                return brdv;
            }
            osdof.HospitalID =long.Parse(hospital.Trim().Split(',')[0]);
            osdof.HospitalName = hospital.Trim().Split(',')[1];
            //osdof.DoctorOpenID = dw.WeiXinAccount;
            //osdof.DoctorAccountID = long.Parse(DoctorId);
            //osdof.DoctorName = da.Name;
            //osdof.DoctorWeiXinUserID = dw.Id;
            osdof.Age = pw.Birthday.HasValue ? DateTime.Now.Year - pw.Birthday.Value.Year : 0;//后期改为从数据库中取
            osdof.AgeUnitID = 1;//默认为岁
            osdof.AgeUnitName = "岁";
            osdof.AreaID = entity.AreaID;
            //osdof.DeptID = da.HospitalDeptID;
            //osdof.DeptName = da.HospitalDeptName;
            osdof.Memo = entity.Memo;
            //osdof.PatNo = entity.PatNo;
            osdof.SexID = pw.SexID;
            osdof.SexName = pw.SexID == 1 ? "男" : "女";
            osdof.Status = long.Parse(DoctorOrderFormStatus.下医嘱单.Key);
            osdof.UserAccountID = pw.Id;// entity.UserAccountID;
            osdof.UserName = (pw.Name != null && pw.Name.Trim() != "") ? pw.Name : pw.UserName;// entity.UserName;
            osdof.UserOpenID = pw.WeiXinAccount;// entity.UserOpenID;
            osdof.UserWeiXinUserID = pw.Id;
            //osdof.FeatureCode = "";
            osdof.FeatureCode = Common.NextRuleNumber.GetFeatureCode();
            osdof.CollectionFlag = entity.CollectionFlag;
            osdof.DoctMobileCode = pw.MobileCode;
            osdof.UserMobileCode = pw.MobileCode;
            if (entity.CollectionFlag)
            {
                if (collectionpriceDefault != null && collectionpriceDefault.Trim() != "")
                {
                    osdof.CollectionPrice = double.Parse(collectionpriceDefault);
                }
                else
                {
                    osdof.CollectionPrice = 0;
                }
            }
            osdof.TypeID = long.Parse(OSDoctorOrderFormType.普通.Key);
            osdof.TypeName = OSDoctorOrderFormType.普通.Value.Name;
            string OrderFormContent = "";
            if (DBDao.Save(osdof))
            {
                List<string> RecommendationItemProductIDList = new List<string>();
                List<string> ItemProductIDList = new List<string>();

                foreach (var item in entity.OrderItem)
                {
                    if (item.RecommendationItemProductID.HasValue)
                    {
                        RecommendationItemProductIDList.Add(item.RecommendationItemProductID.Value.ToString());
                    }
                    else
                    {
                        ItemProductIDList.Add(item.ItemID.ToString());
                    }
                }
                IList<OSRecommendationItemProduct> RecommendationItemProductList = (RecommendationItemProductIDList.Count > 0) ? IDOSRecommendationItemProductDao.GetListByHQL(" Id in (" + string.Join(",", RecommendationItemProductIDList.ToArray()) + ") and  AreaID =" + entity.AreaID) : new List<OSRecommendationItemProduct>();

                IList<BLabTestItem> BLabTestItemList = (ItemProductIDList.Count > 0) ? IDBLabTestItemDao.GetListByHQL(" Id in (" + string.Join(",", ItemProductIDList.ToArray()) + ") ") : new List<BLabTestItem>();

                foreach (var item in entity.OrderItem)
                {
                    OSDoctorOrderItem osdo = new OSDoctorOrderItem();
                    osdo.AreaID = osdof.AreaID;
                    osdo.DOFID = DOFID;
                    osdo.ItemID = item.ItemID;
                    osdo.ItemID = osdof.HospitalID;
                    #region 价格赋值
                    if (item.RecommendationItemProductID.HasValue)
                    {
                        osdo.RecommendationItemProductID = item.RecommendationItemProductID;
                        if (RecommendationItemProductList != null && RecommendationItemProductList.Count() > 0)
                        {
                            var rip = RecommendationItemProductList.Where(a => a.Id == item.RecommendationItemProductID);
                            if (rip != null && rip.Count() > 0)
                            {
                                osdo.DiscountPrice = rip.ElementAt(0).DiscountPrice;
                                //osdo.GreatMasterPrice = rip.ElementAt(0).DiscountPrice;
                                osdo.GreatMasterPrice = rip.ElementAt(0).GreatMasterPrice;
                                osdo.MarketPrice = rip.ElementAt(0).MarketPrice;
                                osdo.ItemID = rip.ElementAt(0).ItemID;
                                osdo.ItemCName = rip.ElementAt(0).CName;
                                osdo.ItemNo = rip.ElementAt(0).ItemNo;
                                OrderFormContent += osdo.ItemCName + ",";
                            }
                            else
                            {
                                throw new Exception("AddOSDoctorOrderFormVO.未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                            }
                        }
                        else
                        {
                            throw new Exception("AddOSDoctorOrderFormVO.RecommendationItemProductList为空或未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                        }
                    }
                    else
                    {
                        if (BLabTestItemList != null && BLabTestItemList.Count() > 0)
                        {
                            var rip = BLabTestItemList.Where(a => a.Id == item.ItemID);
                            if (rip != null && rip.Count() > 0)
                            {

                                osdo.DiscountPrice = rip.ElementAt(0).Price;
                                osdo.GreatMasterPrice = rip.ElementAt(0).Price;
                                osdo.MarketPrice = rip.ElementAt(0).MarketPrice;
                                osdo.ItemID = rip.ElementAt(0).Id;
                                osdo.ItemCName = rip.ElementAt(0).CName;
                                osdo.ItemNo = rip.ElementAt(0).ItemNo;
                                OrderFormContent += osdo.ItemCName + ",";
                            }
                            else
                            {
                                throw new Exception("AddOSDoctorOrderFormVO.未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                            }
                        }
                        else
                        {
                            throw new Exception("AddOSDoctorOrderFormVO.RecommendationItemProductList为空或未找到到相关的特推项目！RecommendationItemProductID：" + item.RecommendationItemProductID);
                        }
                    }
                    #endregion
                    IDOSDoctorOrderItemDao.Save(osdo);
                }

                PushWeiXin(PushWeiXinMessageTestAction, entity, OrderFormContent, DOFID);
            }
            else
            {
                throw new Exception("AddOSDoctorOrderFormVO.新增医嘱单异常！");
            }
            return brdv;
        }

        private void PushWeiXin(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, Entity.ViewObject.Request.OSDoctorOrderFormVO entity, string OrderFormContent, long Id)
        {
            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
            string urgencycolor = "#11cd6e";
            data.Add("first", new TemplateDataObject() { color = urgencycolor, value = "您有一个新的医嘱单。" });
            data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = entity.DoctorName });
            data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = OrderFormContent.Length > 0 ? OrderFormContent.Remove(OrderFormContent.Length - 1) : "" });
            data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "点击查看" });
            string url = "operate=ORDERFORMPUSH&id=" + Id;
            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, new List<string>() { entity.UserOpenID }, data, "orderformpush", url);
        }

        public BaseResultDataValue DS_UDTO_SearchOSDoctorOrderForm(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }
        public BaseResultDataValue DS_UDTO_SearchOSDoctorOrderFormList(string beginDate, string endDate, string patName, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }

        public EntityList<Entity.ViewObject.Response.OSDoctorOrderFormVO> VO_OSDoctorOrderFormList(IList<OSDoctorOrderForm> listOSDoctorOrderForm)
        {
            EntityList<Entity.ViewObject.Response.OSDoctorOrderFormVO> listVOEntity = new EntityList<Entity.ViewObject.Response.OSDoctorOrderFormVO>();
            if (listOSDoctorOrderForm != null && listOSDoctorOrderForm.Count > 0)
            {
                IList<Entity.ViewObject.Response.OSDoctorOrderFormVO> listVO = new List<Entity.ViewObject.Response.OSDoctorOrderFormVO>();
                foreach (OSDoctorOrderForm osDoctorOrderForm in listOSDoctorOrderForm)
                {
                    listVO.Add((Entity.ViewObject.Response.OSDoctorOrderFormVO)VO_OSDoctorOrderForm(osDoctorOrderForm));
                }
                listVOEntity.count = listVO.Count;
                listVOEntity.list = listVO;
            }
            return listVOEntity;
        }

        public Entity.ViewObject.Response.OSDoctorOrderFormVO VO_OSDoctorOrderForm(OSDoctorOrderForm osDoctorOrderForm)
        {
            Entity.ViewObject.Response.OSDoctorOrderFormVO voEntity = new Entity.ViewObject.Response.OSDoctorOrderFormVO();
            if (osDoctorOrderForm != null)
            {
                #region VO赋值

                voEntity.Id = osDoctorOrderForm.Id;
                voEntity.LabID = osDoctorOrderForm.LabID;
                voEntity.AD = osDoctorOrderForm.AreaID;
                voEntity.HD = osDoctorOrderForm.HospitalID;
                voEntity.HN = osDoctorOrderForm.HospitalName;
                voEntity.DAD = osDoctorOrderForm.DoctorAccountID;
                voEntity.WXD = osDoctorOrderForm.DoctorWeiXinUserID;
                voEntity.DN = osDoctorOrderForm.DoctorName;
                voEntity.DOD = osDoctorOrderForm.DoctorOpenID;
                voEntity.UAD = osDoctorOrderForm.UserAccountID;
                voEntity.UWD = osDoctorOrderForm.UserWeiXinUserID;
                voEntity.UN = osDoctorOrderForm.UserName;
                voEntity.UOD = osDoctorOrderForm.UserOpenID;
                voEntity.FC = osDoctorOrderForm.FeatureCode;
                voEntity.SS = osDoctorOrderForm.Status;
                voEntity.Age = osDoctorOrderForm.Age;
                voEntity.AUD = osDoctorOrderForm.AgeUnitID;
                voEntity.AUN = osDoctorOrderForm.AgeUnitName;
                voEntity.SD = osDoctorOrderForm.SexID;
                voEntity.SN = osDoctorOrderForm.SexName;
                voEntity.DD = osDoctorOrderForm.DeptID;
                voEntity.DPN = osDoctorOrderForm.DeptName;
                voEntity.PN = osDoctorOrderForm.PatNo;
                voEntity.MM = osDoctorOrderForm.Memo;
                voEntity.DO = osDoctorOrderForm.DispOrder;
                voEntity.IU = osDoctorOrderForm.IsUse;
                voEntity.DUT = osDoctorOrderForm.DataUpdateTime;
                voEntity.DataAddTime = osDoctorOrderForm.DataAddTime;
                voEntity.CF = osDoctorOrderForm.CollectionFlag;
                voEntity.CP = osDoctorOrderForm.CollectionPrice;
                voEntity.TI = osDoctorOrderForm.TypeID;
                voEntity.TN = osDoctorOrderForm.TypeName;

                #endregion
            }
            return voEntity;
        }

    }
}