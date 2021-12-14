using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  class BBmsCenOrderDoc : BaseBLL<BmsCenOrderDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenOrderDoc
	{
        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }
        IBBSampleOperate IBBSampleOperate { get; set; }
        public IDAO.IDHRDeptDao IDHRDeptDao { get; set; }
        public IBLL.Business.IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.IDRBACUserDao IDRBACUserDao { get; set; }
        
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
            //return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + userNo + "\",\"OrderType\":\"1\"" +
            //       ",\"LogName\":\"" + logName + "\",\"username\":\"\"" + "," + goodsJson + "}";
            string IsOld = ZhiFang.Common.Public.ConfigHelper.GetConfigString("BaronOrderJsonType").Trim();
            if (string.IsNullOrEmpty(IsOld))
                return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + logName + "\",\"OrderType\":\"1\"" +
                    ",\"LogName\":\"" + "" + "\",\"username\":\"\"" + "," + goodsJson + "}";
            else
                return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + logName + "\",\"OrderType\":\"1\"" +
                        ",\"LogName\":\"" + "" + "\",\"username\":\"\"" + "," +
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

                string IsOld = ZhiFang.Common.Public.ConfigHelper.GetConfigString("BaronOrderJsonType").Trim();
                if (string.IsNullOrEmpty(IsOld))
                    strBuilder.Append(",{\"goodsid\":\"" + bmsCenOrderDtl.ProdGoodsNo +
                        "\",\"count\":\"" + bmsCenOrderDtl.GoodsQty.ToString() +
                        "\",\"cCusCode\":\"" + userNo +
                        "\"}");
                else
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
                this.Entity= orderDoc;
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
            if (orderDoc != null && orderDoc.Status<=0)
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
                    baseResultDataValue.ErrorInfo = "错误信息：无法根据id的值获取订单信息！";
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
        public BaseResultDataValue EditBmsCenOrderDocThirdFlag(long id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc orderDoc = this.Get(id);
            orderDoc.IsThirdFlag = 1;
            this.Entity = orderDoc;
            this.Edit();
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

        public BaseResultDataValue BmsCenOrderDocAndPush(SysWeiXinTemplate.PushWeiXinMessage PushWeiXinMessageAction, long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BmsCenOrderDoc t = DBDao.Get(Id);
            if (t != null)
            {
                if (t.Comp != null && t.Comp.OrgNo>0)
                {
                    var deptlist= IDHRDeptDao.GetListByHQL(" hrdept.UseCode ='"+t.Comp.OrgNo+"' ");

                    if (deptlist != null && deptlist.Count > 0)
                    {
                        if (deptlist[0].HREmployeeList != null && deptlist[0].HREmployeeList.Count > 0)
                        {
                            string strBmsCenOrderDtl = "";
                            var BmsCenOrderDtllist = IBBmsCenOrderDtl.SearchListByHQL(" BmsCenOrderDoc.Id=" + Id);
                            if (BmsCenOrderDtllist != null && BmsCenOrderDtllist.Count > 0)
                            {
                                strBmsCenOrderDtl = BmsCenOrderDtllist[0].GoodsName+" " + BmsCenOrderDtllist[0].GoodsQty + BmsCenOrderDtllist[0].GoodsUnit ;
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
                            data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你好！您的订单已被确认."});
                            data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = t.OrderDocNo });
                            string tmp1 = t.CompanyName != null ? t.CompanyName : "";
                            data.Add("keyword2", new TemplateDataObject() { color = "#000000", value ="您的订单已被供应商(" + tmp1 + ")确认。"  });
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
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "bmscenstatuschange", "http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
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
                            IBBWeiXinAccount.PushWeiXinMessage(PushWeiXinMessageAction, tmpempid, data, "addbmscenorderdoctocomp", "WeiXin/WeiXinMainRouter.aspx?operate=ORDERINFOCOMP&id=" + Id);//"http://r.zhifang.com.cn/rea_new/webapp/rea/order/show/info.html?id=" + Id);
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
    }
}