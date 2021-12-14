using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.ServiceCommon;
using Newtonsoft.Json.Linq;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenOrderDoc : BaseBLL<BmsCenOrderDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenOrderDoc
    {
        IBLL.ReagentSys.IBGoods IBGoods { get; set; }
        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }
        IBBSampleOperate IBBSampleOperate { get; set; }
        public IDAO.IDHRDeptDao IDHRDeptDao { get; set; }
        public IBLL.Business.IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.IDRBACUserDao IDRBACUserDao { get; set; }
        IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }
        IBLL.ReagentSys.IBCenOrgCondition IBCenOrgCondition { get; set; }

        IBLL.ReagentSys.IBReaReqOperation IBReaReqOperation { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        public BaseResultDataValue AddBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                    entity.UserID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                entity.UserName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.OperDate = DateTime.Now;
                entity.DataAddTime = DateTime.Now;
                this.Entity = entity;
                baseResultDataValue.success = this.Add();
                if (baseResultDataValue.success)
                {
                    entity = this.Get(this.Entity.Id);
                    baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：entity为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultData AddBmsCenOrderDoc(string jsonEntity)
        {
            BaseResultData baseresultdata = new BaseResultData();
            JObject jsonObject = JObject.Parse(jsonEntity);
            JToken tokenOrderDoc = jsonObject.SelectToken("OrderDocInfo");
            string labNo = jsonObject.SelectToken("LabNo").ToString();
            string compNo = jsonObject.SelectToken("CompNo").ToString();
            string orderDocNo = jsonObject.SelectToken("OrderDocNo").ToString();
            baseresultdata = AddBmsCenOrderDoc(jsonEntity, orderDocNo, labNo, compNo);
            return baseresultdata;
        }

        public BaseResultData AddBmsCenOrderDoc(string jsonEntity, string orderDocNo, string labNo, string compNo)
        {
            BaseResultData baseresultdata = new BaseResultData();
            CenOrg compOrg = null;
            BaseResultDataValue brdvComp = IBCenOrg.JudgeOrgIsExist(compNo, ref compOrg);
            if (!brdvComp.success)
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = brdvComp.ErrorInfo;
                return baseresultdata;
            }
            CenOrg labOrg = null;
            BaseResultDataValue brdvLab = IBCenOrg.JudgeOrgIsExist(labNo, ref labOrg);
            if (!brdvLab.success)
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = brdvLab.ErrorInfo;
                return baseresultdata;
            }
            IList<BmsCenOrderDoc> listOrderDoc = this.SearchListByHQL(" bmscenorderdoc.OrderDocNo=\'" + orderDocNo + "\'" + " and bmscenorderdoc.Comp.Id=" + compOrg.Id.ToString());
            if (listOrderDoc == null || listOrderDoc.Count == 0)
            {
                BmsCenOrderDoc entityOrderDoc = JsonSerializer.JsonDotNetDeserializeObjectIgnoreNull<BmsCenOrderDoc>(jsonEntity);
                entityOrderDoc.Comp = compOrg;
                entityOrderDoc.Lab = labOrg;
                if (string.IsNullOrWhiteSpace(entityOrderDoc.CompanyName))
                    entityOrderDoc.CompanyName = entityOrderDoc.Comp.CName;
                if (string.IsNullOrWhiteSpace(entityOrderDoc.LabName))
                    entityOrderDoc.LabName = entityOrderDoc.Lab.CName;
                IList<BmsCenOrderDtl> listOrderDtl = entityOrderDoc.BmsCenOrderDtlList;
                if (listOrderDtl != null && listOrderDtl.Count > 0)
                {
                    foreach (BmsCenOrderDtl orderDtl in listOrderDtl)
                    {
                        Goods goods = null;
                        BaseResultDataValue brdvGoods = IBGoods.JudgeGoodsIsExist(compOrg.Id.ToString(), labOrg.Id.ToString(), orderDtl.GoodsNo, ref goods);
                        if (!brdvGoods.success)
                        {
                            baseresultdata.success = false;
                            baseresultdata.code = "";
                            baseresultdata.message = brdvGoods.ErrorInfo;
                            return baseresultdata;
                        }
                        orderDtl.Goods = goods;
                        EidtOrderDtlGoodsInfo(orderDtl);
                    }
                    this.Entity = entityOrderDoc;
                    this.Add();
                }
            }
            else
            {
                baseresultdata.success = false;
                baseresultdata.code = "";
                baseresultdata.message = "订单号为【" + orderDocNo + "】的订单已存在！";
            }
            return baseresultdata;
        }

        public BaseResultDataValue OrderDocDataAuthentication(string jsonEntity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }

        public BaseResultDataValue EditBmsCenOrderDoc(BmsCenOrderDoc bmsCenOrderDoc, string mainFields, IList<BmsCenOrderDtl> listAddBmsCenOrderDtl, IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string childFields, string delBmsCenOrderDtlID, int IsAutoCreateBmsCenSaleDoc)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            this.Entity = bmsCenOrderDoc;
            this.Entity.OperDate = DateTime.Now;
            this.Entity.DataUpdateTime = DateTime.Now;
            if (bmsCenOrderDoc != null && !string.IsNullOrEmpty(mainFields))
            {
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, mainFields);
                if (tempArray != null)
                {
                    baseResultDataValue.success = this.Update(tempArray);
                }
                baseResultDataValue = _addBmsCenOrderDtl(listAddBmsCenOrderDtl);
                baseResultDataValue = _editBmsCenOrderDtl(listUpdateBmsCenOrderDtl, childFields);
                baseResultDataValue = _delBmsCenOrderDtl(delBmsCenOrderDtlID);
                if (IsAutoCreateBmsCenSaleDoc != 0 && bmsCenOrderDoc.Status == IsAutoCreateBmsCenSaleDoc)
                {
                    baseResultDataValue = IBBmsCenSaleDoc.AddBmsCenSaleDocByOrderDoc(bmsCenOrderDoc.Id);
                }
            }
            else
            {
                throw new Exception("错误信息：EditBmsCenOrderDoc方法bmsCenOrderDoc或mainFields参数不能为空！");
            }
            return baseResultDataValue;
        }

        protected BaseResultDataValue _addBmsCenOrderDtl(IList<BmsCenOrderDtl> listAddBmsCenOrderDtl)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listAddBmsCenOrderDtl != null && listAddBmsCenOrderDtl.Count > 0)
            {
                foreach (BmsCenOrderDtl bmsCenOrderDtl in listAddBmsCenOrderDtl)
                {
                    bmsCenOrderDtl.DataAddTime = DateTime.Now;
                    IBBmsCenOrderDtl.Entity = bmsCenOrderDtl;
                    if (!IBBmsCenOrderDtl.Add())
                        throw new Exception("AddBmsCenOrderDtl返回False，新增失败！");
                }
            }
            return baseResultDataValue;
        }

        protected BaseResultDataValue _editBmsCenOrderDtl(IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(fields))
            {
                if (listUpdateBmsCenOrderDtl != null && listUpdateBmsCenOrderDtl.Count > 0)
                {
                    foreach (BmsCenOrderDtl bmsCenOrderDtl in listUpdateBmsCenOrderDtl)
                    {
                        bmsCenOrderDtl.DataUpdateTime = DateTime.Now;
                        IBBmsCenOrderDtl.Entity = bmsCenOrderDtl;
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            if (!IBBmsCenOrderDtl.Update(tempArray))
                                throw new Exception("EditBmsCenOrderDtl返回False，更新失败！");
                        }
                    }
                }
            }
            else
            {
                throw new Exception("错误信息：EditBmsCenOrderDtl方法fields参数不能为空！");
            }
            return baseResultDataValue;
        }

        protected BaseResultDataValue _delBmsCenOrderDtl(string delBmsCenOrderDtlID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(delBmsCenOrderDtlID))
            {
                IList<string> listID = delBmsCenOrderDtlID.Split(',').ToList();
                foreach (string id in listID)
                {
                    IBBmsCenOrderDtl.Remove(Int64.Parse(id));
                    //if (!IBBmsCenOrderDtl.Remove(Int64.Parse(id)))
                    //    throw new Exception("DelBmsCenOrderDtl返回False，更新失败！");
                }
            }
            return baseResultDataValue;
        }

        public string GetBaronOrderJson(string fileTime, string userNo, string logName, string goodsJson, string orderMemo)
        {
            return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + logName + "\",\"OrderType\":\"1\"" +
                    ",\"LogName\":\"" + "" + "\",\"username\":\"\"" +
                    ",\"CustomerName\":\"\"" +//科室名称 @author Jcall @version 2017-10-31
                    ",\"OrderMemo\":\"" + orderMemo + "\"," + goodsJson + "}";

        }

        public string GetBaronOrderJson(string fileTime, string OrderNo, string goodsJson)
        {
            return "{\"filetime\":\"" + fileTime + "\"" + ",\"OrderNo\":\"" + OrderNo + "\"," + goodsJson + "}";
        }

        public string GetBaronGoodsJson(IList<BmsCenOrderDtl> listBmsCenOrderDtl, string userNo)
        {
            string strResult = "";
            StringBuilder strBuilder = new StringBuilder();
            foreach (BmsCenOrderDtl bmsCenOrderDtl in listBmsCenOrderDtl)
            {
                strBuilder.Append(",{\"goodsid\":\"" + bmsCenOrderDtl.ProdGoodsNo +
                    "\",\"count\":\"" + bmsCenOrderDtl.GoodsQty.ToString() +
                    "\",\"goodsmemo\":\"" + bmsCenOrderDtl.LabMemo +
                    "\",\"cCusCode\":\"" + userNo +
                    "\"}");
            }
            if (strBuilder.Length > 0)
            {
                strResult = strBuilder.ToString();
                strResult = "\"goods\":[" + strResult.Remove(0, 1) + "]";
            }
            return strResult;
        }

        public BaseResultDataValue AddOtherOrderDocNoByInterface(long orderDocID, string jsonResult)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<BmsCenOrderDtl> listBmsCenOrderDtl = IBBmsCenOrderDtl.SearchListByHQL(" bmscenorderdtl.BmsCenOrderDoc.Id=" + orderDocID.ToString());
            if (listBmsCenOrderDtl != null && listBmsCenOrderDtl.Count > 0 && (!string.IsNullOrEmpty(jsonResult)))
            {
                JObject jsonObject = JObject.Parse(jsonResult);
                if (jsonObject.Property("orderCode") != null && jsonObject.Property("Rows") != null)
                {
                    string otherOrderDocNo = jsonObject["orderCode"].ToString();
                    JArray jsonArray = (JArray)jsonObject["Rows"];
                    if (jsonArray != null && jsonArray.Count > 0 && (!string.IsNullOrEmpty(otherOrderDocNo)))
                    {
                        foreach (JObject jo in jsonArray)
                        {
                            string goodsNo = jo["cInvCode"].ToString();
                            IList<BmsCenOrderDtl> listOrderDtl = listBmsCenOrderDtl.Where(p => p.ProdGoodsNo == goodsNo).ToList();
                            if (listOrderDtl != null && listOrderDtl.Count > 0)
                            {
                                foreach (BmsCenOrderDtl orderDtl in listOrderDtl)
                                {
                                    orderDtl.OtherOrderDocNo = otherOrderDocNo;
                                    IBBmsCenOrderDtl.Entity = orderDtl;
                                    if (IBBmsCenOrderDtl.Edit())
                                        IBBSampleOperate.AddObjectOperate(orderDtl, orderDtl.GetType().Name, "EditEntity", "订货单-获取第三方系统订货单号并更新"); ;
                                }
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "AddOtherOrderDocNoByInterface：接口返回的货品信息，无法对照订单中货品信息！";
                                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                            }
                        }//foreach
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "AddOtherOrderDocNoByInterface：接口返回信息中无订货单号或货品明细信息！";
                        ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "AddOtherOrderDocNoByInterface：接口返回信息中订货单号或货品明细信息属性！";
                    ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddOtherOrderDocNoByInterface：订货单中无货品明细信息！ID：" + orderDocID.ToString();
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditCheckBmsCenOrderDocByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = this.Get(id);
            if (orderDoc != null)
            {
                orderDoc.Status = 1;
                if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                    orderDoc.CheckerID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                orderDoc.Checker = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                orderDoc.CheckTime = DateTime.Now;
                this.Entity = orderDoc;
                baseResultDataValue.success = this.Edit();
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：无法根据id的值获取订单信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditAutoCheckBmsCenOrderDocByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = this.Get(id);
            if (orderDoc != null && orderDoc.Status <= 0)
            {
                orderDoc.Status = 1;
                orderDoc.Checker = "系统自动审核";
                orderDoc.CheckTime = DateTime.Now;
                this.Entity = orderDoc;
                baseResultDataValue.success = this.Edit();
            }
            else
            {
                baseResultDataValue.success = false;
                if (orderDoc == null)
                    baseResultDataValue.ErrorInfo = "错误信息：无法根据订货单ID获取订货单信息！";
                else
                    baseResultDataValue.ErrorInfo = "错误信息：此订单已经审核！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 更新订单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditBmsCenOrderDocThirdFlag(long id, int isThirdFlag, string reason)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = this.Get(id);
            orderDoc.IsThirdFlag = isThirdFlag;
            this.Entity = orderDoc;
            brdv.success = this.Edit();
            return brdv;
        }

        /// <summary>
        /// 订货单逻辑删除
        /// </summary>
        /// <param name="idList">订货单ID</param>
        /// <param name="deleteFlag">删除标志（0或null为正常单子,1为逻辑删除）</param>
        /// <returns></returns>
        public BaseResultDataValue EditOrderDocDeleteFlagByID(string idList, int deleteFlag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(idList))
            {
                IList<string> list = idList.Split(',').ToList();
                foreach (string id in list)
                {
                    BmsCenOrderDoc orderDoc = this.Get(long.Parse(id));
                    orderDoc.DeleteFlag = deleteFlag;
                    this.Entity = orderDoc;
                    if (this.Edit())
                    {
                        if (deleteFlag == 1)
                            IBBSampleOperate.AddObjectOperate(orderDoc, "BmsCenOrderDoc", "BmsCenOrderDocLogicalDelete", "订货单逻辑删除");
                    }
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "参数idList为空！";
            }
            return brdv;
        }

        /// <summary>
        /// 计算订单总额
        /// </summary>
        /// <param name="docID">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditBmsCenOrderDocTotalPrice(long docID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = this.Get(docID);
            if (orderDoc.BmsCenOrderDtlList != null && orderDoc.BmsCenOrderDtlList.Count > 0)
            {
                double totalPrice = 0;
                foreach (BmsCenOrderDtl orderDtl in orderDoc.BmsCenOrderDtlList)
                {
                    totalPrice = totalPrice + orderDtl.Price * orderDtl.GoodsQty;
                }
                orderDoc.TotalPrice = totalPrice;
                this.Entity = orderDoc;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }

        private void EidtOrderDtlGoodsInfo(BmsCenOrderDtl orderDtl)
        {
            if (orderDtl != null && orderDtl.Goods != null)
            {
                if (string.IsNullOrWhiteSpace(orderDtl.GoodsName))//产品名称
                    orderDtl.GoodsName = orderDtl.Goods.CName;

                if (string.IsNullOrWhiteSpace(orderDtl.GoodsNo))//产品编码
                    orderDtl.GoodsNo = orderDtl.Goods.GoodsNo;

                if (string.IsNullOrWhiteSpace(orderDtl.GoodsUnit))//包装单位
                    orderDtl.GoodsUnit = orderDtl.Goods.UnitName;

                if (string.IsNullOrWhiteSpace(orderDtl.UnitMemo))//包装单位描述
                    orderDtl.UnitMemo = orderDtl.Goods.UnitMemo;

                if (string.IsNullOrWhiteSpace(orderDtl.ProdGoodsNo))//厂商产品编号
                    orderDtl.ProdGoodsNo = orderDtl.Goods.ProdGoodsNo;

                if (string.IsNullOrWhiteSpace(orderDtl.ProdOrgName))//厂商产品名称
                    orderDtl.ProdOrgName = orderDtl.Goods.ProdOrgName;

                if (orderDtl.Prod == null)//厂商产品名称
                    orderDtl.Prod = orderDtl.Goods.Prod;
                if (orderDtl.Goods.Prod != null)
                {
                    if (string.IsNullOrWhiteSpace(orderDtl.ProdGoodsNo))//厂商产品编号
                        orderDtl.ProdGoodsNo = orderDtl.Goods.Prod.OrgNo.ToString();

                    if (string.IsNullOrWhiteSpace(orderDtl.ProdOrgName))//厂商产品名称
                        orderDtl.ProdOrgName = orderDtl.Goods.Prod.CName;
                }
            }
        }

        public EntityList<BmsCenOrderDoc> SearchBmsCenOrderDoc(string orderDocWhere, string orderDtlWhere, string sort, int page, int limit)
        {
            return ((IDBmsCenOrderDocDao)base.DBDao).SearchBmsCenOrderDocDao(orderDocWhere, orderDtlWhere, sort, page, limit);
        }

        public BmsCenOrderDoc GetOrderDocByOtherOrderDocNo(string otherOrderDocNo)
        {
            BmsCenOrderDoc bBmsCenOrderDoc = null;
            if (!string.IsNullOrEmpty(otherOrderDocNo))
            {
                IList<BmsCenOrderDtl> listBmsCenOrderDtl = IBBmsCenOrderDtl.SearchListByHQL(" bmscenorderdtl.OtherOrderDocNo=\'" + otherOrderDocNo + "\'");
                if (listBmsCenOrderDtl != null && listBmsCenOrderDtl.Count > 0)
                    bBmsCenOrderDoc = listBmsCenOrderDtl[0].BmsCenOrderDoc;
            }
            return bBmsCenOrderDoc;
        }

        public DataSet GetBmsCenOrderDtlInfoByID(string idList, string where, string sort, string xmlPath)
        {
            EntityList<BmsCenOrderDtl> entityList = null;
            if (string.IsNullOrEmpty(idList) && string.IsNullOrEmpty(where))
                return null;
            else
            {
                if (!string.IsNullOrEmpty(idList))
                    entityList = IBBmsCenOrderDtl.SearchListByHQL(" bmscenorderdtl.BmsCenOrderDoc.Id in (" + idList + ")", sort, 0, 0);
                else if (!string.IsNullOrEmpty(where))
                    entityList = IBBmsCenOrderDtl.SearchListByHQL(where, sort, 0, 0);
                if (entityList != null && entityList.count > 0)
                    return CommonRS.GetListObjectToDataSet<BmsCenOrderDtl>(entityList.list, xmlPath);
                else
                    return null;
            }
        }

        public BaseResultDataValue BmsCenOrderDocAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc t = DBDao.Get(Id);
            if (t != null)
            {
                if (t.Comp != null && t.Comp.OrgNo > 0)
                {
                    var deptlist = IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='" + t.Comp.OrgNo + "' ");

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
                        {
                            string strBmsCenOrderDtl = "";
                            var BmsCenOrderDtllist = IBBmsCenOrderDtl.SearchListByHQL(" BmsCenOrderDoc.Id=" + Id);
                            if (BmsCenOrderDtllist != null && BmsCenOrderDtllist.Count > 0)
                            {
                                strBmsCenOrderDtl = BmsCenOrderDtllist[0].GoodsName + " " + BmsCenOrderDtllist[0].GoodsQty + BmsCenOrderDtllist[0].GoodsUnit;
                                if (BmsCenOrderDtllist.Count > 1)
                                {
                                    strBmsCenOrderDtl = strBmsCenOrderDtl + "...";
                                }
                            }
                            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                            data.Add("first", new TemplateDataObject() { value = "你好！" + t.CompanyName + "，智方云检验服务平台有新的订单需要您处理。" });
                            data.Add("keyword1", new TemplateDataObject() { value = "订单通知", color = "#cc3399" });
                            data.Add("keyword2", new TemplateDataObject() { value = "收到" + t.LabName + "的订单\r\n订单时间：" + t.OperDate.Value.ToString("yyyy-MM-dd HH:mm") + "\r\n订单编号：" + t.OrderDocNo + "\r\n订单内容：\r\n         " + strBmsCenOrderDtl });
                            data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请点击“详情”查看明细，祝您工作愉快！" });


                            //data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您收到了新的订单.订单号：" + t.OrderDocNo + "." });
                            //data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.LabName });
                            //data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "采购人:" + t.UserName + ",采购时间：" + t.OperDate.Value.ToString("yyyy-MM-dd HH:mm") });

                            List<long> tmpempid = new List<long>();
                            foreach (var emp in deptlist[0].HREmployeeList)
                            {
                                if (emp.IsUse)
                                {
                                    tmpempid.Add(emp.Id);
                                }
                            }
                            //IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoctocomp", "http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoctocomp", "WeiXin/WeiXinMainRouter.aspx?operate=ORDERINFOCOMP&id=" + Id);
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "AddBmsCenOrderDocAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id;
                            ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id);
                        }
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "AddBmsCenOrderDocAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo;
                        ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo);
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "AddBmsCenOrderDocAndPush，此订单无供货商！订单ID：" + Id;
                    ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocAndPush，此订单无供货商！订单ID：" + Id);
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "AddBmsCenOrderDocAndPush，无此订单！订单ID：" + Id;
                ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocAndPush，此订单无供货商！订单ID：" + Id);
            }
            return brdv;
        }

        public BaseResultDataValue BmsCenOrderDocConfirmAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc t = DBDao.Get(Id);
            if (t != null)
            {
                if (t.Lab != null && t.Lab.OrgNo > 0)
                {
                    var deptlist = IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='" + t.Lab.OrgNo + "' ");

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
                        {
                            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                            data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您的订单已被确认." });
                            data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.OrderDocNo });
                            string tmp1 = t.CompanyName != null ? t.CompanyName : "";
                            data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "您的订单已被供应商(" + tmp1 + ")确认。" });
                            if (t.StatusName != null)
                                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = t.StatusName });
                            if (t.DataUpdateTime != null)
                                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = t.DataUpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });
                            data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "确认人：" + t.Confirm + ",确认时间：" + t.ConfirmTime.Value.ToString("yyyy-MM-dd HH:mm") });
                            List<long> tmpempid = new List<long>();
                            foreach (var emp in deptlist[0].HREmployeeList)
                            {
                                if (emp.IsUse)
                                {
                                    tmpempid.Add(emp.Id);
                                }
                            }
                            //IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "bmscenstatuschange", "http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "bmscenstatuschange", "WeiXin/WeiXinMainRouter.aspx?operate=ORDERCONFIRM&id=" + Id);
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "此订单订货方对应的部门没有员工！部门ID：" + deptlist[0].Id;
                            ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocConfirmAndPush，此订单订货方对应的部门没有员工！部门ID：" + deptlist[0].Id);
                        }
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "此订单供货商没有对应的部门！订货方t.Lab.OrgNo：" + t.Lab.OrgNo;
                        ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocConfirmAndPush，此订单供货商没有对应的部门！订货方t.Lab.OrgNo：" + t.Lab.OrgNo);
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "此订单无订货方！订单ID：" + Id;
                    ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocConfirmAndPush，此订单无订货方！订单ID：" + Id);
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "无此订单！订单ID：" + Id;
                ZhiFang.Common.Log.Log.Debug("AddBmsCenOrderDocConfirmAndPush，无此订单！订单ID：" + Id);
            }
            return brdv;
        }

        public BaseResultDataValue BmsCenOrderDocReviewAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc t = DBDao.Get(Id);
            if (t != null)
            {
                if (t.Status != 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "此订单的状态为" + t.Status + "！订单ID：" + Id;
                    ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush，此订单的状态为" + t.Status + "！订单ID：" + Id);
                    return brdv;
                }
                if (t.Lab != null && t.Lab.OrgNo > 0)
                {
                    var deptlist = IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='" + t.Lab.OrgNo + "' ");

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
                        {
                            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                            data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您有一个待审核的订单.订单号：" + t.OrderDocNo + "." });
                            data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.LabName });
                            data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "采购人:" + t.UserName + ",采购时间：" + t.OperDate.Value.ToString("yyyy-MM-dd HH:mm") });
                            List<long> tmpempid = new List<long>();
                            foreach (var emp in deptlist[0].HREmployeeList)
                            {
                                if (emp.IsUse)
                                {
                                    tmpempid.Add(emp.Id);
                                }
                            }
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoc", "WeiXin/WeiXinMainRouter.aspx?operate=ORDERINFOPURCHASINGCENTER&id=" + Id);
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "此订单订货方对应的部门没有员工！部门ID：" + deptlist[0].Id;
                            ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush，此订单订货方对应的部门没有员工！部门ID：" + deptlist[0].Id);
                        }
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "此订单供货商没有对应的部门！订货方t.Lab.OrgNo：" + t.Lab.OrgNo;
                        ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush，此订单供货商没有对应的部门！订货方t.Lab.OrgNo：" + t.Lab.OrgNo);
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "此订单无订货方！订单ID：" + Id;
                    ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush，此订单无订货方！订单ID：" + Id);
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "无此订单！订单ID：" + Id;
                ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush，无此订单！订单ID：" + Id);
            }
            return brdv;
        }

        public BaseResultDataValue BmsCenOrderDocCheckedAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc t = DBDao.Get(Id);
            if (t != null)
            {
                if (t.Comp != null && t.Comp.OrgNo > 0)
                {
                    var deptlist = IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='" + t.Comp.OrgNo + "' ");

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
                        {
                            string strBmsCenOrderDtl = "";
                            var BmsCenOrderDtllist = IBBmsCenOrderDtl.SearchListByHQL(" BmsCenOrderDoc.Id=" + Id);
                            if (BmsCenOrderDtllist != null && BmsCenOrderDtllist.Count > 0)
                            {
                                strBmsCenOrderDtl = BmsCenOrderDtllist[0].GoodsName + " " + BmsCenOrderDtllist[0].GoodsQty + BmsCenOrderDtllist[0].GoodsUnit;
                                if (BmsCenOrderDtllist.Count > 1)
                                {
                                    strBmsCenOrderDtl = strBmsCenOrderDtl + "...";
                                }
                            }
                            Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                            data.Add("first", new TemplateDataObject() { value = "你好！" + t.CompanyName + "，智方云检验服务平台有新的订单需要您处理。" });
                            data.Add("keyword1", new TemplateDataObject() { value = "订单通知", color = "#cc3399" });
                            data.Add("keyword2", new TemplateDataObject() { value = "收到" + t.LabName + "的订单\r\n订单时间：" + t.OperDate.Value.ToString("yyyy-MM-dd HH:mm") + "\r\n订单编号：" + t.OrderDocNo + "\r\n订单内容：\r\n         " + strBmsCenOrderDtl });
                            data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "请点击“详情”查看明细，祝您工作愉快！" });


                            //data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您收到了新的订单.订单号：" + t.OrderDocNo + "." });
                            //data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.LabName });
                            //data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "采购人:" + t.UserName + ",采购时间：" + t.OperDate.Value.ToString("yyyy-MM-dd HH:mm") });

                            List<long> tmpempid = new List<long>();
                            foreach (var emp in deptlist[0].HREmployeeList)
                            {
                                if (emp.IsUse)
                                {
                                    tmpempid.Add(emp.Id);
                                }
                            }
                            ZhiFang.Common.Log.Log.Info("微信开始推送订单：" + t.OrderDocNo);
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoctocomp", "WeiXin/WeiXinMainRouter.aspx?operate=ORDERINFOCOMP&id=" + Id);
                            //"http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "BmsCenOrderDocCheckedAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id;
                            ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckedAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id);
                        }
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "BmsCenOrderDocCheckedAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo;
                        ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckedAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo);
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "BmsCenOrderDocCheckedAndPush，此订单无供货商！订单ID：" + Id;
                    ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckedAndPush，此订单无供货商！订单ID：" + Id);
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "BmsCenOrderDocCheckedAndPush，无此订单！订单ID：" + Id;
                ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckedAndPush，此订单无供货商！订单ID：" + Id);
            }
            return brdv;
        }

        //public BaseResultDataValue BmsCenOrderDocCheckedAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    BmsCenOrderDoc t = DBDao.Get(Id);
        //    if (t != null)
        //    {
        //        if (t.Comp != null && t.Comp.OrgNo > 0)
        //        {
        //            var deptlist = IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='" + t.Comp.OrgNo + "' ");

        //            if (deptlist != null && deptlist.Count > 0)
        //            {
        //                if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
        //                {
        //                    Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
        //                    data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您收到了新的订单.订单号：" + t.OrderDocNo + "." });
        //                    data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.LabName });
        //                    data.Add("remark", new TemplateDataObject() { color = "#1d73cd", value = "采购人:" + t.Checker + ",采购时间：" + t.CheckTime.Value.ToString("yyyy-MM-dd HH:mm") });
        //                    List<long> tmpempid = new List<long>();
        //                    foreach (var emp in deptlist[0].HREmployeeList)
        //                    {
        //                        if (emp.IsUse)
        //                        {
        //                            tmpempid.Add(emp.Id);
        //                        }
        //                    }
        //                    IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoc", "http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
        //                }
        //                else
        //                {
        //                    brdv.success = false;
        //                    brdv.ErrorInfo = "BmsCenOrderDocCheckAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id;
        //                    ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckAndPush，此订单供货商对应的部门没有员工！部门ID：" + deptlist[0].Id);
        //                }
        //            }
        //            else
        //            {
        //                brdv.success = false;
        //                brdv.ErrorInfo = "BmsCenOrderDocCheckAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo;
        //                ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckAndPush，此订单供货商没有对应的部门！供货商t.Comp.OrgNo：" + t.Comp.OrgNo);
        //            }
        //        }
        //        else
        //        {
        //            brdv.success = false;
        //            brdv.ErrorInfo = "BmsCenOrderDocCheckAndPush，此订单无供货商！订单ID：" + Id;
        //            ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckAndPush，此订单无供货商！订单ID：" + Id);
        //        }
        //    }
        //    else
        //    {
        //        brdv.success = false;
        //        brdv.ErrorInfo = "BmsCenOrderDocCheckAndPush，无此订单！订单ID：" + Id;
        //        ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocCheckAndPush，此订单无供货商！订单ID：" + Id);
        //    }
        //    return brdv;
        //}

        #region 客户端订单处理

        /// <summary>
        /// 客户端删除平台订单信息(订单状态都为暂存或已提交才能删除)
        /// </summary>
        /// <param name="labOrgNo">实验室编号</param>
        /// <param name="orderDocNo">订单编号</param>
        /// <returns></returns>
        public BaseResultBool DelBmsCenOrderDocByLabOrgNoAndOrderDocNo(string labOrgNo, string orderDocNo)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(labOrgNo))
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "实验室编号不能为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(orderDocNo))
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "订单编号不能为空!";
                return baseResultBool;
            }
            int orgNo = -1;
            if (!int.TryParse(labOrgNo, out orgNo))
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "实验室编号值格式不正确!";
                return baseResultBool;
            }
            try
            {
                IList<BmsCenOrderDoc> tempList = this.SearchListByHQL(string.Format(" (bmscenorderdoc.DeleteFlag=0 or bmscenorderdoc.DeleteFlag is null) and bmscenorderdoc.Status in(0,1) and bmscenorderdoc.Lab.OrgNo={0} and bmscenorderdoc.OrderDocNo='{1}'", orgNo, orderDocNo));
                if (tempList == null || tempList.Count <= 0)
                {
                    baseResultBool.BoolFlag = false;
                    baseResultBool.ErrorInfo = string.Format("获取实验室编号为【{0}】,订单编号为【{1}】的订单信息为空!", labOrgNo, orderDocNo);
                    return baseResultBool;
                }
                var tempList2 = tempList.Where(p => (p.Status != (int)OrderDocStatus.无 && p.Status != (int)OrderDocStatus.已提交)).ToList();
                if (tempList2.Count > 0)
                {
                    baseResultBool.BoolFlag = false;
                    baseResultBool.ErrorInfo = string.Format("实验室编号为【{0}】,订单编号为【{1}】的订单已被确认,不能删除!", labOrgNo, orderDocNo);
                    return baseResultBool;
                }
                int count = 0;
                List<string> tmpa = new List<string>();
                foreach (var model in tempList)
                {
                    model.DeleteFlag = 1;
                    this.Entity = model;
                    tmpa.Clear();

                    tmpa.Add("Id=" + model.Id + " ");
                    tmpa.Add("DeleteFlag=" + model.DeleteFlag + "");

                    if (this.Update(tmpa.ToArray()))
                        count++;
                }
                // and bmscenorderdoc.Lab.OrgNo={0}
                //string hql = string.Format("update BmsCenOrderDoc bmscenorderdoc set bmscenorderdoc.DeleteFlag=1 where (bmscenorderdoc.DeleteFlag=0  or bmscenorderdoc.DeleteFlag is null) and bmscenorderdoc.Status in(0,1) and bmscenorderdoc.Lab.OrgNo={0} and bmscenorderdoc.OrderDocNo='{1}'", labOrgNo, orderDocNo);
                //count = ((IDBmsCenOrderDocDao)base.DBDao).UpdateByHql(hql);

                //int count = this.DeleteByHql(string.Format("from BmsCenOrderDoc bmscenorderdoc where bmscenorderdoc.Status in(0,1) and bmscenorderdoc.Lab.OrgNo={0} and bmscenorderdoc.OrderDocNo='{1}'", labOrgNo, orderDocNo));
                if (count > 0)
                {
                    baseResultBool.BoolFlag = true;
                    baseResultBool.ErrorInfo = string.Format("实验室编号为【{0}】,订单编号为【{1}】删除的订单记录数为{2},已删除成功!", labOrgNo, orderDocNo, count);
                    return baseResultBool;
                }

            }
            catch (Exception ex)
            {
                baseResultBool.BoolFlag = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        /// <summary>
        /// 客户端申请生成订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool AddReaBmsCenOrderDocAndDtl(BmsCenOrderDoc entity, Dictionary<string, BmsCenOrderDtl> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(entity.OrderDocNo))
                entity.OrderDocNo = this.GetReqDocNo();
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            this.Entity = entity;
            tempBaseResultBool.success = this.Add();
            if (tempBaseResultBool.success)
            {
                foreach (var item in dtAddList)
                {
                    item.Value.OrderDocNo = entity.OrderDocNo;
                    if (string.IsNullOrEmpty(item.Value.OrderDtlNo))
                        item.Value.OrderDtlNo = this.GetReqDocNo();
                    IBBmsCenOrderDtl.Entity = item.Value;
                    tempBaseResultBool.success = IBBmsCenOrderDtl.Add();
                }
            }
            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        public BaseResultDataValue AddReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, IList<BmsCenOrderDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(entity.OrderDocNo))
                entity.OrderDocNo = this.GetReqDocNo();
            if (string.IsNullOrEmpty(entity.StatusName))
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            this.Entity = entity;
            tempBaseResultDataValue.success = this.Add();
            if (tempBaseResultDataValue.success)
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    BaseResultBool tempBaseResultBool = IBBmsCenOrderDtl.AddDtList(dtAddList, this.Entity, empID, empName);
                    tempBaseResultDataValue.success = tempBaseResultBool.success;
                    tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                }
            }
            if (tempBaseResultDataValue.success) AddReaReqOperation(this.Entity, empID, empName);
            return tempBaseResultDataValue;
        }
        public BaseResultBool EditReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, string[] tempArray, IList<BmsCenOrderDtl> dtAddList, IList<BmsCenOrderDtl> dtEditList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = entity;
            BmsCenOrderDoc oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                BmsCenOrderDoc tempEntity = new BmsCenOrderDoc();
                tempEntity.Id = entity.Id;
                tempEntity.Status = entity.Status;
                if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBBmsCenOrderDtl.AddDtList(dtAddList, tempEntity, empID, empName);
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBBmsCenOrderDtl.EditDtList(dtEditList, tempEntity);
                if (tempBaseResultBool.success) AddReaReqOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        bool EditReaBmsCenOrderDocStatusCheck(BmsCenOrderDoc entity, BmsCenOrderDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            #region 暂存
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key)
                {
                    return false;
                }
                tmpa.Add("UserID=" + empID + " ");
                tmpa.Add("UserName='" + empName + "'");

            }
            #endregion
            #region 申请
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.申请.Key)
            {
                //审核应用时,可以先编辑保存状态为已申请的申请单
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                if (string.IsNullOrEmpty(entity.UserName)) entity.UserName = empName;
                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
            }
            #endregion

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key)
                {
                    return false;
                }
                entity.CheckerID = empID;
                entity.Checker = empName;
                tmpa.Add("CheckerID=" + entity.CheckerID + " ");
                tmpa.Add("Checker='" + entity.Checker + "'");
                tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审核退回.Key)
            {
                // && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key)
                {
                    return false;
                }
                //如果是客户端的申请开单,需要给开单明细打上审核退回原因

            }

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.已上传.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.部分验收.Key || entity.Status.ToString() == ReaBmsOrderDocStatus.全部验收.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.已上传.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.部分验收.Key)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 添加申请操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private void AddReaReqOperation(BmsCenOrderDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.暂存.Key) return;

            ReaReqOperation sco = new ReaReqOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "BmsCenOrderDoc";
            if (!string.IsNullOrEmpty(entity.CheckMemo))
                sco.Memo = entity.CheckMemo;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaReqOperation.Entity = sco;
            IBReaReqOperation.Add();
        }
        /// <summary>
        /// 获取订单总单号
        /// </summary>
        /// <returns></returns>
        private string GetReqDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        #endregion

        #region 客户端订单上传平台
        /// <summary>
        /// 将订单及订单明细信息与平台同步及上传
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResultBool EditBmsCenOrderDocSyncToPlatform(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BmsCenOrderDoc entity = this.Get(id);
            IList<BmsCenOrderDtl> dtList = new List<BmsCenOrderDtl>();
            IList<ReaGoodsOrgLink> goodsOrgLinkList = new List<ReaGoodsOrgLink>();
            #region 是否能同步和上传的处理
            if (entity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            if (!entity.ReaCompID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的供应商为空！";
                return tempBaseResultBool;
            }

            ReaCenOrg reaCenOrg = IDReaCenOrgDao.Get(entity.ReaCompID.Value);
            if (reaCenOrg.PlatformOrgNo <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的供应商的平台机构编码为空！";
                return tempBaseResultBool;
            }

            dtList = IBBmsCenOrderDtl.SearchListByHQL("bmscenorderdtl.BmsCenOrderDoc.Id=" + id);
            if (dtList == null || dtList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的订单明细信息为空！";
                return tempBaseResultBool;
            }
            StringBuilder goodsOrgLinkIdStr = new StringBuilder();
            foreach (var item in dtList)
            {
                if (!item.OrderGoodsID.HasValue)
                {
                    goodsOrgLinkIdStr.Append(item.OrderGoodsID.Value + ",");
                }
            }
            if (string.IsNullOrEmpty(goodsOrgLinkIdStr.ToString()))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "处理订单明细的货品与供应商信息为空！";
                return tempBaseResultBool;
            }
            goodsOrgLinkList = IDReaGoodsOrgLinkDao.GetListByHQL("reagoodsorglink.Id in(" + goodsOrgLinkIdStr.ToString().TrimEnd(',') + ")");
            if (goodsOrgLinkList == null || goodsOrgLinkList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取订单明细的货品与供应商信息为空！";
                return tempBaseResultBool;
            }
            //货品与供应商关系中的货品平台编码是否存在为空
            var tempList = goodsOrgLinkList.Where(p => string.IsNullOrEmpty(p.CenOrgGoodsNo)).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                string reagoodStr = "";
                foreach (var item in tempList)
                {
                    reagoodStr += (item.ReaGoods.CName + ";");
                }
                reagoodStr = reagoodStr.TrimEnd(';');
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("货品为{0}的平台编码存在为空,请在货品与供应商关系中维护好后再操作！", reagoodStr);
                return tempBaseResultBool;
            }
            #endregion

            //订单上传处理

            #region 同步更新订单的供应商的平台编码
            entity.ReaServerCompCode = reaCenOrg.PlatformOrgNo.ToString();
            this.Entity = entity;
            tempBaseResultBool.success = this.Edit();
            #endregion

            #region 同步更新订单明细有货品平台编码
            foreach (var item in dtList)
            {
                if (!item.OrderGoodsID.HasValue)
                {
                    var tempList2 = goodsOrgLinkList.Where(p => p.Id == item.OrderGoodsID.Value);
                    item.GoodsNo = tempList2.ElementAt(0).CenOrgGoodsNo;
                    IBBmsCenOrderDtl.Entity = item;
                    tempBaseResultBool.success = IBBmsCenOrderDtl.Edit();
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "货品明细为：" + item.ReaGoodsName + ",同步货品平台编码失败！";
                        break;
                    }
                }
            }
            #endregion
            return tempBaseResultBool;
        }
        //ClientOrderUploadPlatform

        #endregion

    }
}