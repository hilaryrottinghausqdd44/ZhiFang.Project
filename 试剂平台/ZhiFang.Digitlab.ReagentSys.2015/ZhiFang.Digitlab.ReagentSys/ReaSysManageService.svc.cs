using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Digitlab.ReagentSys.ServerContract;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.ServiceCommon;
using System.Web;
using ZhiFang.Common.Public;
using System.IO;
using System.Data;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using System.ServiceModel.Channels;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaSysManageService : IReaSysManageService
    {

        IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        IBLL.Business.IBBSampleOperate IBBSampleOperate { get; set; }

        IBLL.ReagentSys.IBReaCenOrg IBReaCenOrg { get; set; }

        IBLL.ReagentSys.IBReaGoods IBReaGoods { get; set; }

        IBLL.ReagentSys.IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }

        IBLL.ReagentSys.IBReaBmsInDoc IBReaBmsInDoc { get; set; }

        IBLL.ReagentSys.IBReaBmsInDtl IBReaBmsInDtl { get; set; }

        IBLL.ReagentSys.IBReaBmsTransferDoc IBReaBmsTransferDoc { get; set; }

        IBLL.ReagentSys.IBReaBmsTransferDtl IBReaBmsTransferDtl { get; set; }

        IBLL.ReagentSys.IBReaBmsReqDoc IBReaBmsReqDoc { get; set; }

        IBLL.ReagentSys.IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }

        IBLL.ReagentSys.IBReaDeptGoods IBReaDeptGoods { get; set; }

        IBLL.ReagentSys.IBReaGoodsLot IBReaGoodsLot { get; set; }

        IBLL.ReagentSys.IBReaGoodsRegister IBReaGoodsRegister { get; set; }

        IBLL.ReagentSys.IBReaGoodsUnit IBReaGoodsUnit { get; set; }

        IBLL.ReagentSys.IBReaPlace IBReaPlace { get; set; }
        IBLL.ReagentSys.IBReaStorage IBReaStorage { get; set; }
        IBLL.ReagentSys.IBReaReqOperation IBReaReqOperation { get; set; }
        IBLL.ReagentSys.IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }
        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }
        IBBmsCenSaleDocConfirm IBBmsCenSaleDocConfirm { get; set; }
        IBBmsCenSaleDtlConfirm IBBmsCenSaleDtlConfirm { get; set; }
        IBLL.ReagentSys.IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        IBReaChooseGoodsTemplate IBReaChooseGoodsTemplate { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        #region ReaCenOrg
        //Add  ReaCenOrg
        public BaseResultDataValue ST_UDTO_AddReaCenOrg(ReaCenOrg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaCenOrg.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaCenOrg
        public BaseResultBool ST_UDTO_UpdateReaCenOrg(ReaCenOrg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaCenOrg.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaCenOrg
        public BaseResultBool ST_UDTO_UpdateReaCenOrgByField(ReaCenOrg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCenOrg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaCenOrg.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaCenOrg.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaCenOrg
        public BaseResultBool ST_UDTO_DelReaCenOrg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaCenOrg.Entity = IBReaCenOrg.Get(id);
                if (IBReaCenOrg.Entity != null)
                {
                    long labid = IBReaCenOrg.Entity.LabID;
                    string entityName = IBReaCenOrg.Entity.GetType().Name;
                    baseResultBool.success = IBReaCenOrg.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenOrg(ReaCenOrg entity)
        {
            EntityList<ReaCenOrg> entityList = new EntityList<ReaCenOrg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaCenOrg.Entity = entity;
                try
                {
                    entityList.list = IBReaCenOrg.Search();
                    entityList.count = IBReaCenOrg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenOrg>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCenOrg> entityList = new EntityList<ReaCenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaCenOrg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaCenOrg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenOrg>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaCenOrg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaCenOrg>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaGoods
        //Add  ReaGoods
        public BaseResultDataValue ST_UDTO_AddReaGoods(ReaGoods entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.GoodsSort = IBReaGoods.GetMaxGoodsSort();
                IBReaGoods.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoods.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaGoods
        public BaseResultBool ST_UDTO_UpdateReaGoods(ReaGoods entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoods.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoods.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaGoods
        public BaseResultBool ST_UDTO_UpdateReaGoodsByField(ReaGoods entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoods.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoods.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoods.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoods.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoods
        public BaseResultBool ST_UDTO_DelReaGoods(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoods.Entity = IBReaGoods.Get(id);
                if (IBReaGoods.Entity != null)
                {
                    long labid = IBReaGoods.Entity.LabID;
                    string entityName = IBReaGoods.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoods.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoods(ReaGoods entity)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoods.Entity = entity;
                try
                {
                    entityList.list = IBReaGoods.Search();
                    entityList.count = IBReaGoods.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoods.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoods>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultBool ST_UDTO_UpdateGonvertGroupCode(string idList, string Code)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //IList<string> IDList = idList.Split(',').ToList();
            //string fields = "Id,GonvertGroupCode,DataUpdateTime";
            //foreach (string id in IDList)
            //{
            //    ReaGoods entity = IBReaGoods.Get(long.Parse(id));
            //    if (entity != null)
            //    {
            //        entity.DataUpdateTime = DateTime.Now;
            //        //entity.GonvertGroupCode = Code;
            //        IBReaGoods.Entity = entity;
            //        try
            //        {
            //            if ((fields != null) && (fields.Length > 0))
            //            {
            //                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoods.Entity, fields);
            //                if (tempArray != null)
            //                {
            //                    baseResultBool.success = IBReaGoods.Update(tempArray);
            //                    if (baseResultBool.success)
            //                    {
            //                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改相同码操作");
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                baseResultBool.success = false;
            //                baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
            //                //baseResultBool.success = IBReaGoods.Edit();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            baseResultBool.success = false;
            //            baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //            //throw new Exception(ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        baseResultBool.success = false;
            //        baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            //    }

            //}
            return baseResultBool;
        }


        #endregion

        #region ReaGoodsOrgLink
        //Add  ReaGoodsOrgLink
        public BaseResultDataValue ST_UDTO_AddReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {

                    baseResultDataValue.success = IBReaGoodsOrgLink.Add();
                    if (baseResultDataValue.success)
                    {
                        long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                        string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        IBReaGoodsOrgLink.AddReaReqOperation(entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.新增货品价格.Key));
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_UpdateReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoodsOrgLink.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_UpdateReaGoodsOrgLinkByField(ReaGoodsOrgLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsOrgLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsOrgLink.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                                IBReaGoodsOrgLink.AddReaReqOperation(entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.编辑货品价格.Key));
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsOrgLink.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_DelReaGoodsOrgLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsOrgLink.Entity = IBReaGoodsOrgLink.Get(id);
                if (IBReaGoodsOrgLink.Entity != null)
                {
                    long labid = IBReaGoodsOrgLink.Entity.LabID;
                    string entityName = IBReaGoodsOrgLink.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsOrgLink.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsOrgLink.Search();
                    entityList.count = IBReaGoodsOrgLink.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsOrgLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsOrgLink>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaBmsInDoc
        //Add  ReaBmsInDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsInDoc(ReaBmsInDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsInDoc.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaBmsInDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsInDoc(ReaBmsInDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsInDoc.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaBmsInDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsInDocByField(ReaBmsInDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsInDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsInDoc.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsInDoc.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaBmsInDoc
        public BaseResultBool ST_UDTO_DelReaBmsInDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsInDoc.Entity = IBReaBmsInDoc.Get(id);
                if (IBReaBmsInDoc.Entity != null)
                {
                    long labid = IBReaBmsInDoc.Entity.LabID;
                    string entityName = IBReaBmsInDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsInDoc.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDoc(ReaBmsInDoc entity)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsInDoc.Search();
                    entityList.count = IBReaBmsInDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDoc>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDoc>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsInDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsInDoc>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaBmsInDtl
        //Add  ReaBmsInDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsInDtl(ReaBmsInDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsInDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaBmsInDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsInDtl(ReaBmsInDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsInDtl.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaBmsInDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsInDtlByField(ReaBmsInDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsInDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsInDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsInDtl.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaBmsInDtl
        public BaseResultBool ST_UDTO_DelReaBmsInDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsInDtl.Entity = IBReaBmsInDtl.Get(id);
                if (IBReaBmsInDtl.Entity != null)
                {
                    long labid = IBReaBmsInDtl.Entity.LabID;
                    string entityName = IBReaBmsInDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsInDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtl(ReaBmsInDtl entity)
        {
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsInDtl.Search();
                    entityList.count = IBReaBmsInDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsInDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsInDtl>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaBmsReqDoc
        //Add  ReaBmsReqDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsReqDoc.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsReqDoc.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocByField(ReaBmsReqDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsReqDoc.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsReqDoc.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_DelReaBmsReqDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsReqDoc.Entity = IBReaBmsReqDoc.Get(id);
                if (IBReaBmsReqDoc.Entity != null)
                {
                    long labid = IBReaBmsReqDoc.Entity.LabID;
                    string entityName = IBReaBmsReqDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsReqDoc.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            EntityList<ReaBmsReqDoc> entityList = new EntityList<ReaBmsReqDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsReqDoc.Search();
                    entityList.count = IBReaBmsReqDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDoc>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDoc> entityList = new EntityList<ReaBmsReqDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDoc>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsReqDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsReqDoc>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaBmsReqDtl
        //Add  ReaBmsReqDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsReqDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsReqDtl.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtlByField(ReaBmsReqDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsReqDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsReqDtl.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_DelReaBmsReqDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsReqDtl.Entity = IBReaBmsReqDtl.Get(id);
                if (IBReaBmsReqDtl.Entity != null)
                {
                    long labid = IBReaBmsReqDtl.Entity.LabID;
                    string entityName = IBReaBmsReqDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsReqDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsReqDtl.Search();
                    entityList.count = IBReaBmsReqDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsReqDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsReqDtl>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaBmsTransferDoc
        //Add  ReaBmsTransferDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsTransferDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsTransferDoc.Get(IBReaBmsTransferDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsTransferDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDocByField(ReaBmsTransferDoc entity, string fields)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsTransferDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsTransferDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsTransferDoc.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_DelReaBmsTransferDoc(long longReaBmsTransferDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDoc.Remove(longReaBmsTransferDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsTransferDoc.Search();
                tempEntityList.count = IBReaBmsTransferDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsTransferDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsTransferDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsTransferDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsTransferDoc>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region ReaBmsTransferDtl
        //Add  ReaBmsTransferDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsTransferDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsTransferDtl.Get(IBReaBmsTransferDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsTransferDtl.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDtlByField(ReaBmsTransferDtl entity, string fields)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsTransferDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsTransferDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsTransferDtl.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_DelReaBmsTransferDtl(long longReaBmsTransferDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDtl.Remove(longReaBmsTransferDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            EntityList<ReaBmsTransferDtl> tempEntityList = new EntityList<ReaBmsTransferDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsTransferDtl.Search();
                tempEntityList.count = IBReaBmsTransferDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDtl>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDtl> tempEntityList = new EntityList<ReaBmsTransferDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsTransferDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsTransferDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDtl>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsTransferDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsTransferDtl>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion     

        #region ReaDeptGoods
        //Add  ReaDeptGoods
        public BaseResultDataValue ST_UDTO_AddReaDeptGoods(ReaDeptGoods entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaDeptGoods.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaDeptGoods.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaDeptGoods
        public BaseResultBool ST_UDTO_UpdateReaDeptGoods(ReaDeptGoods entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaDeptGoods.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaDeptGoods.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaDeptGoods
        public BaseResultBool ST_UDTO_UpdateReaDeptGoodsByField(ReaDeptGoods entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaDeptGoods.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaDeptGoods.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaDeptGoods.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaDeptGoods.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaDeptGoods
        public BaseResultBool ST_UDTO_DelReaDeptGoods(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaDeptGoods.Entity = IBReaDeptGoods.Get(id);
                if (IBReaDeptGoods.Entity != null)
                {
                    long labid = IBReaDeptGoods.Entity.LabID;
                    string entityName = IBReaDeptGoods.Entity.GetType().Name;
                    baseResultBool.success = IBReaDeptGoods.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoods(ReaDeptGoods entity)
        {
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaDeptGoods.Entity = entity;
                try
                {
                    entityList.list = IBReaDeptGoods.Search();
                    entityList.count = IBReaDeptGoods.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaDeptGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaDeptGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaDeptGoods.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaDeptGoods>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaGoodsLot
        //Add  ReaGoodsLot
        public BaseResultDataValue ST_UDTO_AddReaGoodsLot(ReaGoodsLot entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;

                try
                {
                    BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid();
                    baseResultDataValue.success = baseResultBool.success;
                    baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;

                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaGoodsLot
        public BaseResultBool ST_UDTO_UpdateReaGoodsLot(ReaGoodsLot entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;
                try
                {
                    baseResultBool = IBReaGoodsLot.EditValid();
                    if (baseResultBool.success == false) return baseResultBool;

                    baseResultBool.success = IBReaGoodsLot.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaGoodsLot
        public BaseResultBool ST_UDTO_UpdateReaGoodsLotByField(ReaGoodsLot entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;

                try
                {
                    baseResultBool = IBReaGoodsLot.EditValid();
                    if (baseResultBool.success == false) return baseResultBool;

                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsLot.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsLot.Update(tempArray);
                            //if (baseResultBool.success)
                            //{
                            //    IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            //}
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsLot.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoodsLot
        public BaseResultBool ST_UDTO_DelReaGoodsLot(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsLot.Entity = IBReaGoodsLot.Get(id);
                if (IBReaGoodsLot.Entity != null)
                {
                    long labid = IBReaGoodsLot.Entity.LabID;
                    string entityName = IBReaGoodsLot.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsLot.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLot(ReaGoodsLot entity)
        {
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsLot.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsLot.Search();
                    entityList.count = IBReaGoodsLot.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsLot>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLotByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsLot.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsLot.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsLot>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLotById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsLot.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsLot>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaGoodsRegister
        public Stream ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile()
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();
            ReaGoodsRegister entity = null;
            string entityStr = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (file.FileName.Length > 0)
                {
                    string[] temp = file.FileName.Split('.');
                    if (temp[temp.Length - 1].ToLower() != "pdf")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "entity":
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            entityStr = HttpContext.Current.Request.Form["entity"].Trim();
                        break;
                    case "file":
                        break;
                    default:
                        break;
                }
            }
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string hrdeptID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID);
            string hrdeptCode = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptCode);
            // ZhiFang.Common.Log.Log.Debug("新增注册证信息机构编码:" + hrdeptCode);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "登录人信息为空!请登录后再操作!";
            }
            if (baseResultBool.success && String.IsNullOrEmpty(hrdeptID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "登录人机构信息为空!请登录后再操作!";
            }
            //if (baseResultBool.success && String.IsNullOrEmpty(hrdeptCode))
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "登录人机构编号信息为空!请登录后再操作!";
            //}
            if (baseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity为空!";
            }
            if (baseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增注册证信息出错:" + baseResultBool.ErrorInfo);
            }
            if (baseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ReaGoodsRegister>(entityStr);
                    entity.EmpID = long.Parse(employeeID);
                    entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "新增注册证信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增注册证信息序列化出错:" + ex.Message);
                }
            }

            if (baseResultBool.success)
            {
                entity.DataAddTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultBool = IBReaGoodsRegister.AddReaGoodsRegisterAndUploadRegisterFile(file);
                    if (baseResultBool.success)
                    {
                        baseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaGoodsRegister.Entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增注册证信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            ReaGoodsRegister entity = null;
            string fields = "";
            string fFileEntity = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (!String.IsNullOrEmpty(file.FileName))
                {
                    string[] temp = file.FileName.Split('.');
                    if (temp[temp.Length - 1].ToLower() != "pdf")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }

            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "fields":
                        if (HttpContext.Current.Request.Form["fields"].Trim() != "")
                            fields = HttpContext.Current.Request.Form["fields"].Trim();
                        break;
                    case "entity"://Entity
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["entity"].Trim();
                        break;

                }
            }
            if (!string.IsNullOrEmpty(fFileEntity))
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ReaGoodsRegister>(fFileEntity);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            if (entity == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (baseResultBool.success)
            {
                entity.EmpID = long.Parse(employeeID);
                entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBReaGoodsRegister.UpdateReaGoodsRegisterAndUploadRegisterFileByField(tempArray, file);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + baseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + baseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新注册证信息出错1:" + baseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_ReaGoodsRegisterPreviewPdf(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = "";
                fileStream = IBReaGoodsRegister.GetReaGoodsRegisterFileStream(id, ref filename);

                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "注册证文件不存在!请重新上传或联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                    return memoryStream;
                }
                else
                {
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                    return fileStream;
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "预览注册证文件错误!" + ex.Message;
                ZhiFang.Common.Log.Log.Error(errorInfo);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }
        }
        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsRegister.SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsRegister.SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        //Add  ReaGoodsRegister
        public BaseResultDataValue ST_UDTO_AddReaGoodsRegister(ReaGoodsRegister entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoodsRegister.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaGoodsRegister
        public BaseResultBool ST_UDTO_UpdateReaGoodsRegister(ReaGoodsRegister entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoodsRegister.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaGoodsRegister
        public BaseResultBool ST_UDTO_UpdateReaGoodsRegisterByField(ReaGoodsRegister entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsRegister.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsRegister.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoodsRegister
        public BaseResultBool ST_UDTO_DelReaGoodsRegister(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsRegister.Entity = IBReaGoodsRegister.Get(id);
                if (IBReaGoodsRegister.Entity != null)
                {
                    long labid = IBReaGoodsRegister.Entity.LabID;
                    string entityName = IBReaGoodsRegister.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsRegister.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegister(ReaGoodsRegister entity)
        {
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsRegister.Search();
                    entityList.count = IBReaGoodsRegister.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsRegister.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsRegister.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsRegister.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsRegister>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaGoodsUnit
        //Add  ReaGoodsUnit
        public BaseResultDataValue ST_UDTO_AddReaGoodsUnit(ReaGoodsUnit entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoodsUnit.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaGoodsUnit
        public BaseResultBool ST_UDTO_UpdateReaGoodsUnit(ReaGoodsUnit entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoodsUnit.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaGoodsUnit
        public BaseResultBool ST_UDTO_UpdateReaGoodsUnitByField(ReaGoodsUnit entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsUnit.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsUnit.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsUnit.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoodsUnit
        public BaseResultBool ST_UDTO_DelReaGoodsUnit(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsUnit.Entity = IBReaGoodsUnit.Get(id);
                if (IBReaGoodsUnit.Entity != null)
                {
                    long labid = IBReaGoodsUnit.Entity.LabID;
                    string entityName = IBReaGoodsUnit.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsUnit.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnit(ReaGoodsUnit entity)
        {
            EntityList<ReaGoodsUnit> entityList = new EntityList<ReaGoodsUnit>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsUnit.Search();
                    entityList.count = IBReaGoodsUnit.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsUnit>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsUnit> entityList = new EntityList<ReaGoodsUnit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsUnit.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsUnit.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsUnit>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnitById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsUnit.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsUnit>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaPlace
        //Add  ReaPlace
        public BaseResultDataValue ST_UDTO_AddReaPlace(ReaPlace entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaPlace.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaPlace
        public BaseResultBool ST_UDTO_UpdateReaPlace(ReaPlace entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaPlace.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaPlace
        public BaseResultBool ST_UDTO_UpdateReaPlaceByField(ReaPlace entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaPlace.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaPlace.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaPlace.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaPlace
        public BaseResultBool ST_UDTO_DelReaPlace(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaPlace.Entity = IBReaPlace.Get(id);
                if (IBReaPlace.Entity != null)
                {
                    long labid = IBReaPlace.Entity.LabID;
                    string entityName = IBReaPlace.Entity.GetType().Name;
                    baseResultBool.success = IBReaPlace.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaPlace(ReaPlace entity)
        {
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaPlace.Entity = entity;
                try
                {
                    entityList.list = IBReaPlace.Search();
                    entityList.count = IBReaPlace.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaPlace>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaPlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaPlace.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaPlace.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaPlace>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaPlaceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaPlace.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaPlace>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaStorage
        //Add  ReaStorage
        public BaseResultDataValue ST_UDTO_AddReaStorage(ReaStorage entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaStorage.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaStorage
        public BaseResultBool ST_UDTO_UpdateReaStorage(ReaStorage entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaStorage.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaStorage
        public BaseResultBool ST_UDTO_UpdateReaStorageByField(ReaStorage entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaStorage.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaStorage.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaStorage.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaStorage
        public BaseResultBool ST_UDTO_DelReaStorage(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaStorage.Entity = IBReaStorage.Get(id);
                if (IBReaStorage.Entity != null)
                {
                    long labid = IBReaStorage.Entity.LabID;
                    string entityName = IBReaStorage.Entity.GetType().Name;
                    baseResultBool.success = IBReaStorage.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaStorage(ReaStorage entity)
        {
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaStorage.Entity = entity;
                try
                {
                    entityList.list = IBReaStorage.Search();
                    entityList.count = IBReaStorage.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaStorage>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaStorageByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaStorage.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaStorage.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaStorage>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaStorageById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaStorage.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaStorage>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaReqOperation
        //Add  ReaReqOperation
        public BaseResultDataValue ST_UDTO_AddReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaReqOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaReqOperation.Get(IBReaReqOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaReqOperation.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  ReaReqOperation
        public BaseResultBool ST_UDTO_UpdateReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaReqOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaReqOperation
        public BaseResultBool ST_UDTO_UpdateReaReqOperationByField(ReaReqOperation entity, string fields)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaReqOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaReqOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaReqOperation.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ReaReqOperation
        public BaseResultBool ST_UDTO_DelReaReqOperation(long longReaReqOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaReqOperation.Remove(longReaReqOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            EntityList<ReaReqOperation> tempEntityList = new EntityList<ReaReqOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaReqOperation.Search();
                tempEntityList.count = IBReaReqOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaReqOperation>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaReqOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaReqOperation> tempEntityList = new EntityList<ReaReqOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaReqOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaReqOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaReqOperation>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaReqOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaReqOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaReqOperation>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region ReaBmsQtyDtl
        //Add  ReaBmsQtyDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsQtyDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsQtyDtl.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlByField(ReaBmsQtyDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsQtyDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsQtyDtl.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_DelReaBmsQtyDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsQtyDtl.Entity = IBReaBmsQtyDtl.Get(id);
                if (IBReaBmsQtyDtl.Entity != null)
                {
                    long labid = IBReaBmsQtyDtl.Entity.LabID;
                    string entityName = IBReaBmsQtyDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsQtyDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsQtyDtl.Search();
                    entityList.count = IBReaBmsQtyDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsQtyDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsQtyDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsQtyDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsQtyDtl>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaCheckInOperation
        //Add  ReaCheckInOperation
        public BaseResultDataValue ST_UDTO_AddReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaCheckInOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaCheckInOperation.Get(IBReaCheckInOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaCheckInOperation.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  ReaCheckInOperation
        public BaseResultBool ST_UDTO_UpdateReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaCheckInOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaCheckInOperation
        public BaseResultBool ST_UDTO_UpdateReaCheckInOperationByField(ReaCheckInOperation entity, string fields)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCheckInOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaCheckInOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaCheckInOperation.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ReaCheckInOperation
        public BaseResultBool ST_UDTO_DelReaCheckInOperation(long longReaCheckInOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaCheckInOperation.Remove(longReaCheckInOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            EntityList<ReaCheckInOperation> tempEntityList = new EntityList<ReaCheckInOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaCheckInOperation.Search();
                tempEntityList.count = IBReaCheckInOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaCheckInOperation>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCheckInOperation> tempEntityList = new EntityList<ReaCheckInOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaCheckInOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaCheckInOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaCheckInOperation>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaCheckInOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaCheckInOperation>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region ReaCenBarCodeFormat
        //Add  ReaCenBarCodeFormat
        public BaseResultDataValue ST_UDTO_AddReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaCenBarCodeFormat.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaCenBarCodeFormat.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormatByField(ReaCenBarCodeFormat entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCenBarCodeFormat.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaCenBarCodeFormat.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaCenBarCodeFormat.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_DelReaCenBarCodeFormat(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaCenBarCodeFormat.Entity = IBReaCenBarCodeFormat.Get(id);
                if (IBReaCenBarCodeFormat.Entity != null)
                {
                    long labid = IBReaCenBarCodeFormat.Entity.LabID;
                    string entityName = IBReaCenBarCodeFormat.Entity.GetType().Name;
                    baseResultBool.success = IBReaCenBarCodeFormat.RemoveByHQL(id);
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    entityList.list = IBReaCenBarCodeFormat.Search();
                    entityList.count = IBReaCenBarCodeFormat.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenBarCodeFormat>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaCenBarCodeFormat.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaCenBarCodeFormat.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenBarCodeFormat>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaCenBarCodeFormat.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaCenBarCodeFormat>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ReaChooseGoodsTemplate
        //Add  ReaChooseGoodsTemplate
        public BaseResultDataValue ST_UDTO_AddReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                entity.IsUse = true;
                entity.DataAddTime = DateTime.Now;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaChooseGoodsTemplate.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaChooseGoodsTemplate.Edit();
                    if (baseResultBool.success)
                    {
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplateByField(ReaChooseGoodsTemplate entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaChooseGoodsTemplate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaChooseGoodsTemplate.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaChooseGoodsTemplate.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_DelReaChooseGoodsTemplate(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaChooseGoodsTemplate.Entity = IBReaChooseGoodsTemplate.Get(id);
                if (IBReaChooseGoodsTemplate.Entity != null)
                {
                    long labid = IBReaChooseGoodsTemplate.Entity.LabID;
                    string entityName = IBReaChooseGoodsTemplate.Entity.GetType().Name;
                    baseResultBool.success = IBReaChooseGoodsTemplate.RemoveByHQL(id);
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            EntityList<ReaChooseGoodsTemplate> entityList = new EntityList<ReaChooseGoodsTemplate>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    entityList.list = IBReaChooseGoodsTemplate.Search();
                    entityList.count = IBReaChooseGoodsTemplate.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaChooseGoodsTemplate>(entityList);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaChooseGoodsTemplate> entityList = new EntityList<ReaChooseGoodsTemplate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaChooseGoodsTemplate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaChooseGoodsTemplate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaChooseGoodsTemplate>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaChooseGoodsTemplate.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaChooseGoodsTemplate>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region 客户端部门采购定制服务
        public BaseResultDataValue ST_UDTO_SearchApplyHRDeptByByHRDeptId(int page, int limit, string fields, string where, string sort, bool isPlanish, long deptId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<HRDept> entityList = new EntityList<HRDept>();
            try
            {
                //IList<HRDept> deptList = IBHRDept.SearchHRDeptByHREmpID(empId);
                //deptId = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                IList<HRDept> deptList = IBHRDept.SearchHRDeptListByHRDeptId(deptId);

                entityList.count = deptList.Count;
                //查询处理
                if (!String.IsNullOrEmpty(where))
                {
                    where = where.Replace("(", "");
                    where = where.Replace(")", "");
                    if (where.Contains("CName"))
                    {
                        where = where.Replace(" like ", ";");
                        string[] strArr = where.Split(';');
                        if (strArr.Length == 2 && !string.IsNullOrEmpty(strArr[1]))
                        {
                            //"部门%"
                            var tempList = deptList.Where(p => p.CName.StartsWith(strArr[1]) || p.EName.ToLower().Contains(strArr[1].ToLower()));
                            //tempList = tempList.Where((x, i) => tempList.ToList().FindIndex(z => z.EName == x.EName) == i).ToList();
                            deptList = tempList.ToList();
                        }
                    }
                }
                #region 分页处理
                if (limit < deptList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = deptList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        deptList = list.ToList();
                    }
                }
                entityList.list = deptList;
                #endregion
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<HRDept>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        //
        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId(long deptId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaGoodsCenOrgVO> entityList = new List<ReaGoodsCenOrgVO>();
                string goodIdStr = IBReaDeptGoods.SearchReaDeptGoodsListByHRDeptId(deptId);
                entityList = IBReaGoodsOrgLink.SearchReaCenOrgGoodsListByGoodIdStr(goodIdStr);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取采购申请货品库存数量
        /// </summary>
        /// <param name="goodIdStr"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaGoodsCurrentQtyVO> entityList = new List<ReaGoodsCurrentQtyVO>();
                entityList = IBReaBmsQtyDtl.SearchReaGoodsCurrentQtyByGoodIdStr(idStr, goodIdStr);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 部门采购申请新增服务
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.ApplyID.HasValue)
                    entity.ApplyID = empID;
                if (string.IsNullOrEmpty(entity.ApplyName))
                    entity.ApplyName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsReqDoc.AddReaBmsReqDocAndDt(entity, dtAddList, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsReqDoc.Get(IBReaBmsReqDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsReqDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 部门采购申请更新服务
        /// </summary>
        /// <param name="entity">待更新的主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDocAndDt(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDocAndDt:" + ex.Source);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 部门采购申请明细更新(验证主单后只操作明细)
        /// </summary>
        /// <param name="entity">主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDtlOfCheck(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDtlOfCheck:" + tempBaseResultBool.ErrorInfo);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 部门采购申请审核及撤消审核服务
        /// </summary>
        /// <param name="entity">待审核的主单信息</param>
        /// <param name="dtAddList">审核前的新增的明细集合</param>
        /// <param name="dtEditList">审核前的待更新的明细集合</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDocAndDtOfCheck(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck:" + tempBaseResultBool.ErrorInfo);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 依据客户端的申请主单(已审核)生成客户端订单信息
        /// </summary>
        /// <param name="idStr">申请主单IDStr</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的申请主单参数(idStr)为空";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultBool = IBReaBmsReqDoc.AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(idStr, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 客户端订单处理
        public BaseResultDataValue ST_UDTO_AddReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, IList<BmsCenOrderDtl> dtAddList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.UserID = empID;
                if (string.IsNullOrEmpty(entity.UserName))
                    entity.UserName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                IBBmsCenOrderDoc.Entity = entity;
                tempBaseResultDataValue = IBBmsCenOrderDoc.AddReaBmsCenOrderDocAndDt(entity, dtAddList, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenOrderDoc.Get(IBBmsCenOrderDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsReqDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, string fields, IList<BmsCenOrderDtl> dtAddList, IList<BmsCenOrderDtl> dtEditList)
        {
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
            {
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
            }
            IBBmsCenOrderDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDoc.Entity, fields);
                tempBaseResultBool = IBBmsCenOrderDoc.EditReaBmsCenOrderDocAndDt(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsCenOrderDocAndDt:" + ex.Source);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_DelReaBmsCenOrderDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenOrderDtl.Entity = IBBmsCenOrderDtl.Get(id);
                //是否是申请明细转换生成的订单明细(需要依订单明细的ID到申请明细里查询)

                if (IBBmsCenOrderDtl.Entity != null)
                {
                    long labid = IBBmsCenOrderDtl.Entity.LabID;
                    string entityName = IBBmsCenOrderDtl.Entity.GetType().Name;
                    long docID = IBBmsCenOrderDtl.Entity.BmsCenOrderDoc.Id;
                    baseResultBool.success = IBBmsCenOrderDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(docID);
                        IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        #endregion


    }
}
