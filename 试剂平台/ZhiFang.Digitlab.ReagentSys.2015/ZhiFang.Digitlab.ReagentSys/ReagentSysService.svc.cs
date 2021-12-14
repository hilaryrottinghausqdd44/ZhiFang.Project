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

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReagentSysService : IReagentSysService
    {
        #region IReagentSysService 成员

        IBLL.Business.IBBSampleOperate IBBSampleOperate { get; set; }

        IBLL.ReagentSys.IBBmsAccountInput IBBmsAccountInput { get; set; }

        IBLL.ReagentSys.IBBmsAccountSaleDoc IBBmsAccountSaleDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDtl IBBmsCenSaleDtl { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDtlBarCode IBBmsCenSaleDtlBarCode { get; set; }

        IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

        IBLL.ReagentSys.IBCenOrgCondition IBCenOrgCondition { get; set; }

        IBLL.ReagentSys.IBCenOrgType IBCenOrgType { get; set; }

        IBLL.ReagentSys.IBCenMsg IBCenMsg { get; set; }

        IBLL.ReagentSys.IBCenQtyDtl IBCenQtyDtl { get; set; }

        IBLL.ReagentSys.IBGoods IBGoods { get; set; }

        IBLL.ReagentSys.IBGoodsRegister IBGoodsRegister { get; set; }

        IBLL.ReagentSys.IBBmsCenOrderDocHistory IBBmsCenOrderDocHistory { get; set; }

        IBLL.ReagentSys.IBBmsCenOrderDtlHistory IBBmsCenOrderDtlHistory { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDocHistory IBBmsCenSaleDocHistory { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDtlHistory IBBmsCenSaleDtlHistory { get; set; }

        IBLL.ReagentSys.IBCenQtyDtlTemp IBCenQtyDtlTemp { get; set; }

        IBLL.ReagentSys.IBCenQtyDtlTempHistory IBCenQtyDtlTempHistory { get; set; }

        IBLL.ReagentSys.IBTestEquipLab IBTestEquipLab { get; set; }

        IBLL.ReagentSys.IBTestEquipProd IBTestEquipProd { get; set; }

        IBLL.ReagentSys.IBTestEquipType IBTestEquipType { get; set; }

        ZhiFang.Digitlab.IBLL.Business.IBSCAttachment IBSCAttachment { get; set; }

        ZhiFang.Digitlab.IBLL.Business.IBSCInteraction IBSCInteraction { get; set; }

        ZhiFang.Digitlab.IBLL.Business.IBSCOperation IBSCOperation { get; set; }

        IBLL.ReagentSys.IBGoodsQualification IBGoodsQualification { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDocConfirm IBBmsCenSaleDocConfirm { get; set; }

        IBLL.ReagentSys.IBBmsCenSaleDtlConfirm IBBmsCenSaleDtlConfirm { get; set; }

        ZhiFang.Digitlab.IBLL.Business.IBBParameter IBBParameter { get; set; }

        #endregion


        #region BmsCenOrderDoc
        //Add  BmsCenOrderDoc
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenOrderDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenOrderDoc.Add();
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
        //Update  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenOrderDoc.Edit();
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
        //Update  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocByField(BmsCenOrderDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenOrderDoc.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenOrderDoc.Edit();
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
        //Delele  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_DelBmsCenOrderDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenOrderDoc.Entity = IBBmsCenOrderDoc.Get(id);
                if (IBBmsCenOrderDoc.Entity != null)
                {
                    long labid = IBBmsCenOrderDoc.Entity.LabID;
                    string entityName = IBBmsCenOrderDoc.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenOrderDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            EntityList<BmsCenOrderDoc> entityList = new EntityList<BmsCenOrderDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenOrderDoc.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenOrderDoc.Search();
                    entityList.count = IBBmsCenOrderDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDoc> entityList = new EntityList<BmsCenOrderDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenOrderDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenOrderDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenOrderDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenOrderDoc>(entity);
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


        #region BmsCenOrderDtl
        //Add  BmsCenOrderDtl
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenOrderDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenOrderDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(IBBmsCenOrderDtl.Entity.BmsCenOrderDoc.Id);
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
        //Update  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenOrderDtl.Edit();
                    if (baseResultBool.success)
                    {
                        BmsCenOrderDtl orderDtl = IBBmsCenOrderDtl.Get(entity.Id);
                        IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(orderDtl.BmsCenOrderDoc.Id);
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
        //Update  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlByField(BmsCenOrderDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenOrderDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                BmsCenOrderDtl orderDtl = IBBmsCenOrderDtl.Get(entity.Id);
                                IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(orderDtl.BmsCenOrderDoc.Id);
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenOrderDtl.Edit();
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
        //Delele  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_DelBmsCenOrderDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenOrderDtl.Entity = IBBmsCenOrderDtl.Get(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            EntityList<BmsCenOrderDtl> entityList = new EntityList<BmsCenOrderDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenOrderDtl.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenOrderDtl.Search();
                    entityList.count = IBBmsCenOrderDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDtl> entityList = new EntityList<BmsCenOrderDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenOrderDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenOrderDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenOrderDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenOrderDtl>(entity);
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


        #region BmsCenSaleDoc
        //Add  BmsCenSaleDoc
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                if (!entity.IsAccountInput.HasValue)
                    entity.IsAccountInput = 0;
                IBBmsCenSaleDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDoc.Add();
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
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDoc.Edit();
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
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocByField(BmsCenSaleDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDoc.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDoc.Edit();
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
        //Delele  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_DelBmsCenSaleDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDoc.Entity = IBBmsCenSaleDoc.Get(id);
                if (IBBmsCenSaleDoc.Entity != null)
                {
                    long labid = IBBmsCenSaleDoc.Entity.LabID;
                    string entityName = IBBmsCenSaleDoc.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            EntityList<BmsCenSaleDoc> entityList = new EntityList<BmsCenSaleDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDoc.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDoc.Search();
                    entityList.count = IBBmsCenSaleDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDoc> entityList = new EntityList<BmsCenSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDoc>(entity);
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


        #region BmsCenSaleDtl
        //Add  BmsCenSaleDtl
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenSaleDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(IBBmsCenSaleDtl.Entity.BmsCenSaleDoc.Id);
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
        //Update  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDtl.Edit();
                    if (baseResultBool.success)
                    {
                        BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(entity.Id);
                        IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.BmsCenSaleDoc.Id);
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
        //Update  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlByField(BmsCenSaleDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(entity.Id);
                                IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.BmsCenSaleDoc.Id);
                                IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenSaleDtl.Edit();
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
        //Delele  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDtl.Entity = IBBmsCenSaleDtl.Get(id);
                if (IBBmsCenSaleDtl.Entity != null)
                {
                    long labid = IBBmsCenSaleDtl.Entity.LabID;
                    string entityName = IBBmsCenSaleDtl.Entity.GetType().Name;
                    long saleDocID = IBBmsCenSaleDtl.Entity.BmsCenSaleDoc.Id;
                    baseResultBool.success = IBBmsCenSaleDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDocID);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDtl.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDtl.Search();
                    entityList.count = IBBmsCenSaleDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDtl>(entity);
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


        #region BmsCenSaleDtlBarCode
        //Add  BmsCenSaleDtlBarCode
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenSaleDtlBarCode.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDtlBarCode.Add();
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
        //Update  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlBarCode.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDtlBarCode.Edit();
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
        //Update  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCodeByField(BmsCenSaleDtlBarCode entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlBarCode.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlBarCode.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDtlBarCode.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDtlBarCode.Edit();
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
        //Delele  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtlBarCode(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDtlBarCode.Entity = IBBmsCenSaleDtlBarCode.Get(id);
                if (IBBmsCenSaleDtlBarCode.Entity != null)
                {
                    long labid = IBBmsCenSaleDtlBarCode.Entity.LabID;
                    string entityName = IBBmsCenSaleDtlBarCode.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDtlBarCode.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            EntityList<BmsCenSaleDtlBarCode> entityList = new EntityList<BmsCenSaleDtlBarCode>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDtlBarCode.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDtlBarCode.Search();
                    entityList.count = IBBmsCenSaleDtlBarCode.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlBarCode>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlBarCode> entityList = new EntityList<BmsCenSaleDtlBarCode>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDtlBarCode.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDtlBarCode.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlBarCode>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDtlBarCode.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDtlBarCode>(entity);
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


        #region CenOrg
        //Add  CenOrg
        public BaseResultDataValue ST_UDTO_AddCenOrg(CenOrg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrg.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenOrg.Add();
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
        //Update  CenOrg
        public BaseResultBool ST_UDTO_UpdateCenOrg(CenOrg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrg.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenOrg.Edit();
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
        //Update  CenOrg
        public BaseResultBool ST_UDTO_UpdateCenOrgByField(CenOrg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenOrg.Update(tempArray);
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
                        //baseResultBool.success = IBCenOrg.Edit();
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
        //Delele  CenOrg
        public BaseResultBool ST_UDTO_DelCenOrg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenOrg.Entity = IBCenOrg.Get(id);
                if (IBCenOrg.Entity != null)
                {
                    long labid = IBCenOrg.Entity.LabID;
                    string entityName = IBCenOrg.Entity.GetType().Name;
                    baseResultBool.success = IBCenOrg.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrg(CenOrg entity)
        {
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenOrg.Entity = entity;
                try
                {
                    entityList.list = IBCenOrg.Search();
                    entityList.count = IBCenOrg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenOrg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenOrg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenOrg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenOrg>(entity);
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


        #region CenOrgCondition
        //Add  CenOrgCondition
        public BaseResultDataValue ST_UDTO_AddCenOrgCondition(CenOrgCondition entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenOrgCondition.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenOrgCondition.Add();
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
        //Update  CenOrgCondition
        public BaseResultBool ST_UDTO_UpdateCenOrgCondition(CenOrgCondition entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgCondition.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenOrgCondition.Edit();
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
        //Update  CenOrgCondition
        public BaseResultBool ST_UDTO_UpdateCenOrgConditionByField(CenOrgCondition entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgCondition.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrgCondition.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenOrgCondition.Update(tempArray);
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
                        //baseResultBool.success = IBCenOrgCondition.Edit();
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
        //Delele  CenOrgCondition
        public BaseResultBool ST_UDTO_DelCenOrgCondition(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenOrgCondition.Entity = IBCenOrgCondition.Get(id);
                if (IBCenOrgCondition.Entity != null)
                {
                    long labid = IBCenOrgCondition.Entity.LabID;
                    string entityName = IBCenOrgCondition.Entity.GetType().Name;
                    baseResultBool.success = IBCenOrgCondition.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgCondition(CenOrgCondition entity)
        {
            EntityList<CenOrgCondition> entityList = new EntityList<CenOrgCondition>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenOrgCondition.Entity = entity;
                try
                {
                    entityList.list = IBCenOrgCondition.Search();
                    entityList.count = IBCenOrgCondition.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgCondition>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgConditionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrgCondition> entityList = new EntityList<CenOrgCondition>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenOrgCondition.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenOrgCondition.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgCondition>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgConditionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenOrgCondition.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenOrgCondition>(entity);
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


        #region CenOrgType
        //Add  CenOrgType
        public BaseResultDataValue ST_UDTO_AddCenOrgType(CenOrgType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenOrgType.Add();
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
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgType(CenOrgType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenOrgType.Edit();
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
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgTypeByField(CenOrgType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrgType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenOrgType.Update(tempArray);
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
                        //baseResultBool.success = IBCenOrgType.Edit();
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
        //Delele  CenOrgType
        public BaseResultBool ST_UDTO_DelCenOrgType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenOrgType.Entity = IBCenOrgType.Get(id);
                if (IBCenOrgType.Entity != null)
                {
                    long labid = IBCenOrgType.Entity.LabID;
                    string entityName = IBCenOrgType.Entity.GetType().Name;
                    baseResultBool.success = IBCenOrgType.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgType(CenOrgType entity)
        {
            EntityList<CenOrgType> entityList = new EntityList<CenOrgType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenOrgType.Entity = entity;
                try
                {
                    entityList.list = IBCenOrgType.Search();
                    entityList.count = IBCenOrgType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrgType> entityList = new EntityList<CenOrgType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenOrgType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenOrgType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenOrgType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenOrgType>(entity);
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


        #region CenMsg
        //Add  CenMsg
        public BaseResultDataValue ST_UDTO_AddCenMsg(CenMsg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenMsg.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenMsg.Add();
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
        //Update  CenMsg
        public BaseResultBool ST_UDTO_UpdateCenMsg(CenMsg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenMsg.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenMsg.Edit();
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
        //Update  CenMsg
        public BaseResultBool ST_UDTO_UpdateCenMsgByField(CenMsg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenMsg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenMsg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenMsg.Update(tempArray);
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
                        //baseResultBool.success = IBCenMsg.Edit();
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
        //Delele  CenMsg
        public BaseResultBool ST_UDTO_DelCenMsg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenMsg.Entity = IBCenMsg.Get(id);
                if (IBCenMsg.Entity != null)
                {
                    long labid = IBCenMsg.Entity.LabID;
                    string entityName = IBCenMsg.Entity.GetType().Name;
                    baseResultBool.success = IBCenMsg.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenMsg(CenMsg entity)
        {
            EntityList<CenMsg> entityList = new EntityList<CenMsg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenMsg.Entity = entity;
                try
                {
                    entityList.list = IBCenMsg.Search();
                    entityList.count = IBCenMsg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenMsg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenMsg> entityList = new EntityList<CenMsg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenMsg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenMsg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenMsg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenMsgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenMsg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenMsg>(entity);
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


        #region CenQtyDtl
        //Add  CenQtyDtl
        public BaseResultDataValue ST_UDTO_AddCenQtyDtl(CenQtyDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenQtyDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenQtyDtl.Add();
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
        //Update  CenQtyDtl
        public BaseResultBool ST_UDTO_UpdateCenQtyDtl(CenQtyDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenQtyDtl.Edit();
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
        //Update  CenQtyDtl
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlByField(CenQtyDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenQtyDtl.Update(tempArray);
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
                        //baseResultBool.success = IBCenQtyDtl.Edit();
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
        //Delele  CenQtyDtl
        public BaseResultBool ST_UDTO_DelCenQtyDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenQtyDtl.Entity = IBCenQtyDtl.Get(id);
                if (IBCenQtyDtl.Entity != null)
                {
                    long labid = IBCenQtyDtl.Entity.LabID;
                    string entityName = IBCenQtyDtl.Entity.GetType().Name;
                    baseResultBool.success = IBCenQtyDtl.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtl(CenQtyDtl entity)
        {
            EntityList<CenQtyDtl> entityList = new EntityList<CenQtyDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenQtyDtl.Entity = entity;
                try
                {
                    entityList.list = IBCenQtyDtl.Search();
                    entityList.count = IBCenQtyDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtl> entityList = new EntityList<CenQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenQtyDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenQtyDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenQtyDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenQtyDtl>(entity);
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


        #region Goods
        //Add  Goods
        public BaseResultDataValue ST_UDTO_AddGoods(Goods entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBGoods.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBGoods.Add();
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
        //Update  Goods
        public BaseResultBool ST_UDTO_UpdateGoods(Goods entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBGoods.Entity = entity;
                try
                {
                    baseResultBool.success = IBGoods.Edit();
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
        //Update  Goods
        public BaseResultBool ST_UDTO_UpdateGoodsByField(Goods entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBGoods.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoods.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBGoods.Update(tempArray);
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
                        //baseResultBool.success = IBGoods.Edit();
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
        //Delele  Goods
        public BaseResultBool ST_UDTO_DelGoods(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBGoods.Entity = IBGoods.Get(id);
                if (IBGoods.Entity != null)
                {
                    if (IBGoods.Entity.DownloadFlag == 1)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "货品：" + IBGoods.Entity.CName + "，编码：" + IBGoods.Entity.GoodsNo + "，已被客户端下载使用，不能删除！";
                        return baseResultBool;
                    }
                    long labid = IBGoods.Entity.LabID;
                    string entityName = IBGoods.Entity.GetType().Name;
                    baseResultBool.success = IBGoods.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchGoods(Goods entity)
        {
            EntityList<Goods> entityList = new EntityList<Goods>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBGoods.Entity = entity;
                try
                {
                    entityList.list = IBGoods.Search();
                    entityList.count = IBGoods.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<Goods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<Goods> entityList = new EntityList<Goods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<Goods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBGoods.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<Goods>(entity);
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


        #region GoodsRegister
        public Stream ST_UDTO_AddGoodsRegisterAndUploadRegisterFile()
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();
            GoodsRegister entity = null;
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
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsRegister>(entityStr);
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
                IBGoodsRegister.Entity = entity;
                try
                {
                    if (String.IsNullOrEmpty(hrdeptCode))
                        hrdeptCode = entity.CenOrgNo;
                    baseResultBool = IBGoodsRegister.AddGoodsRegisterAndUploadRegisterFile(file, hrdeptID, hrdeptCode);
                    if (baseResultBool.success)
                    {
                        baseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoodsRegister.Entity.Id);
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
        public Stream ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            GoodsRegister entity = null;
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
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsRegister>(fFileEntity);
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
                IBGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBGoodsRegister.UpdateGoodsRegisterAndUploadRegisterFileByField(tempArray, file);
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
        public Stream ST_UDTO_GoodsRegisterPreviewPdf(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = "";
                fileStream = IBGoodsRegister.GetGoodsRegisterFileStream(id, ref filename);

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
        //Add  GoodsRegister
        public BaseResultDataValue ST_UDTO_AddGoodsRegister(GoodsRegister entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBGoodsRegister.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBGoodsRegister.Add();
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
        //Update  GoodsRegister
        public BaseResultBool ST_UDTO_UpdateGoodsRegister(GoodsRegister entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBGoodsRegister.Entity = entity;
                try
                {
                    baseResultBool.success = IBGoodsRegister.Edit();
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
        //Update  GoodsRegister
        public BaseResultBool ST_UDTO_UpdateGoodsRegisterByField(GoodsRegister entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBGoodsRegister.Update(tempArray);
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
                        //baseResultBool.success = IBGoodsRegister.Edit();
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
        //Delele  GoodsRegister
        public BaseResultBool ST_UDTO_DelGoodsRegister(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBGoodsRegister.Entity = IBGoodsRegister.Get(id);
                if (IBGoodsRegister.Entity != null)
                {
                    long labid = IBGoodsRegister.Entity.LabID;
                    string entityName = IBGoodsRegister.Entity.GetType().Name;
                    baseResultBool.success = IBGoodsRegister.RemoveByHQL(id);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsRegister(GoodsRegister entity)
        {

            EntityList<GoodsRegister> entityList = new EntityList<GoodsRegister>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBGoodsRegister.Entity = entity;
                try
                {
                    entityList.list = IBGoodsRegister.Search();
                    entityList.count = IBGoodsRegister.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GoodsRegister>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsRegister> entityList = new EntityList<GoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBGoodsRegister.SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBGoodsRegister.SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GoodsRegister>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsRegister> entityList = new EntityList<GoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBGoodsRegister.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBGoodsRegister.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GoodsRegister>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBGoodsRegister.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<GoodsRegister>(entity);
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


        #region BmsCenOrderDocHistory
        //Add  BmsCenOrderDocHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenOrderDocHistory.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenOrderDocHistory.Add();
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
        //Update  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDocHistory.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenOrderDocHistory.Edit();
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
        //Update  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistoryByField(BmsCenOrderDocHistory entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDocHistory.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDocHistory.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenOrderDocHistory.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenOrderDocHistory.Edit();
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
        //Delele  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_DelBmsCenOrderDocHistory(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenOrderDocHistory.Entity = IBBmsCenOrderDocHistory.Get(id);
                if (IBBmsCenOrderDocHistory.Entity != null)
                {
                    long labid = IBBmsCenOrderDocHistory.Entity.LabID;
                    string entityName = IBBmsCenOrderDocHistory.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenOrderDocHistory.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            EntityList<BmsCenOrderDocHistory> entityList = new EntityList<BmsCenOrderDocHistory>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenOrderDocHistory.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenOrderDocHistory.Search();
                    entityList.count = IBBmsCenOrderDocHistory.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDocHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDocHistory> entityList = new EntityList<BmsCenOrderDocHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenOrderDocHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenOrderDocHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDocHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenOrderDocHistory.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenOrderDocHistory>(entity);
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


        #region BmsCenOrderDtlHistory
        //Add  BmsCenOrderDtlHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenOrderDtlHistory.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenOrderDtlHistory.Add();
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
        //Update  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDtlHistory.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenOrderDtlHistory.Edit();
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
        //Update  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistoryByField(BmsCenOrderDtlHistory entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenOrderDtlHistory.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDtlHistory.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenOrderDtlHistory.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenOrderDtlHistory.Edit();
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
        //Delele  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_DelBmsCenOrderDtlHistory(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenOrderDtlHistory.Entity = IBBmsCenOrderDtlHistory.Get(id);
                if (IBBmsCenOrderDtlHistory.Entity != null)
                {
                    long labid = IBBmsCenOrderDtlHistory.Entity.LabID;
                    string entityName = IBBmsCenOrderDtlHistory.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenOrderDtlHistory.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            EntityList<BmsCenOrderDtlHistory> entityList = new EntityList<BmsCenOrderDtlHistory>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenOrderDtlHistory.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenOrderDtlHistory.Search();
                    entityList.count = IBBmsCenOrderDtlHistory.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDtlHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDtlHistory> entityList = new EntityList<BmsCenOrderDtlHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenOrderDtlHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenOrderDtlHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenOrderDtlHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenOrderDtlHistory.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenOrderDtlHistory>(entity);
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


        #region BmsCenSaleDocHistory
        //Add  BmsCenSaleDocHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenSaleDocHistory.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDocHistory.Add();
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
        //Update  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDocHistory.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDocHistory.Edit();
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
        //Update  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistoryByField(BmsCenSaleDocHistory entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDocHistory.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDocHistory.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDocHistory.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDocHistory.Edit();
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
        //Delele  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_DelBmsCenSaleDocHistory(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDocHistory.Entity = IBBmsCenSaleDocHistory.Get(id);
                if (IBBmsCenSaleDocHistory.Entity != null)
                {
                    long labid = IBBmsCenSaleDocHistory.Entity.LabID;
                    string entityName = IBBmsCenSaleDocHistory.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDocHistory.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            EntityList<BmsCenSaleDocHistory> entityList = new EntityList<BmsCenSaleDocHistory>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDocHistory.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDocHistory.Search();
                    entityList.count = IBBmsCenSaleDocHistory.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDocHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDocHistory> entityList = new EntityList<BmsCenSaleDocHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDocHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDocHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDocHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDocHistory.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDocHistory>(entity);
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


        #region BmsCenSaleDtlHistory
        //Add  BmsCenSaleDtlHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsCenSaleDtlHistory.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDtlHistory.Add();
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
        //Update  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlHistory.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDtlHistory.Edit();
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
        //Update  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistoryByField(BmsCenSaleDtlHistory entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlHistory.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlHistory.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDtlHistory.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDtlHistory.Edit();
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
        //Delele  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtlHistory(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDtlHistory.Entity = IBBmsCenSaleDtlHistory.Get(id);
                if (IBBmsCenSaleDtlHistory.Entity != null)
                {
                    long labid = IBBmsCenSaleDtlHistory.Entity.LabID;
                    string entityName = IBBmsCenSaleDtlHistory.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDtlHistory.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            EntityList<BmsCenSaleDtlHistory> entityList = new EntityList<BmsCenSaleDtlHistory>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDtlHistory.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDtlHistory.Search();
                    entityList.count = IBBmsCenSaleDtlHistory.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlHistory> entityList = new EntityList<BmsCenSaleDtlHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDtlHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDtlHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDtlHistory.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDtlHistory>(entity);
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


        #region CenQtyDtlTemp
        //Add  CenQtyDtlTemp
        public BaseResultDataValue ST_UDTO_AddCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenQtyDtlTemp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenQtyDtlTemp.Add();
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
        //Update  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtlTemp.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenQtyDtlTemp.Edit();
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
        //Update  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempByField(CenQtyDtlTemp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtlTemp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtlTemp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenQtyDtlTemp.Update(tempArray);
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
                        //baseResultBool.success = IBCenQtyDtlTemp.Edit();
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
        //Delele  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_DelCenQtyDtlTemp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenQtyDtlTemp.Entity = IBCenQtyDtlTemp.Get(id);
                if (IBCenQtyDtlTemp.Entity != null)
                {
                    long labid = IBCenQtyDtlTemp.Entity.LabID;
                    string entityName = IBCenQtyDtlTemp.Entity.GetType().Name;
                    baseResultBool.success = IBCenQtyDtlTemp.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            EntityList<CenQtyDtlTemp> entityList = new EntityList<CenQtyDtlTemp>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenQtyDtlTemp.Entity = entity;
                try
                {
                    entityList.list = IBCenQtyDtlTemp.Search();
                    entityList.count = IBCenQtyDtlTemp.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtlTemp>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtlTemp> entityList = new EntityList<CenQtyDtlTemp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenQtyDtlTemp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenQtyDtlTemp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtlTemp>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenQtyDtlTemp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenQtyDtlTemp>(entity);
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


        #region CenQtyDtlTempHistory
        //Add  CenQtyDtlTempHistory
        public BaseResultDataValue ST_UDTO_AddCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBCenQtyDtlTempHistory.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenQtyDtlTempHistory.Add();
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
        //Update  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtlTempHistory.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenQtyDtlTempHistory.Edit();
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
        //Update  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistoryByField(CenQtyDtlTempHistory entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenQtyDtlTempHistory.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtlTempHistory.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenQtyDtlTempHistory.Update(tempArray);
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
                        //baseResultBool.success = IBCenQtyDtlTempHistory.Edit();
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
        //Delele  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_DelCenQtyDtlTempHistory(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenQtyDtlTempHistory.Entity = IBCenQtyDtlTempHistory.Get(id);
                if (IBCenQtyDtlTempHistory.Entity != null)
                {
                    long labid = IBCenQtyDtlTempHistory.Entity.LabID;
                    string entityName = IBCenQtyDtlTempHistory.Entity.GetType().Name;
                    baseResultBool.success = IBCenQtyDtlTempHistory.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            EntityList<CenQtyDtlTempHistory> entityList = new EntityList<CenQtyDtlTempHistory>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenQtyDtlTempHistory.Entity = entity;
                try
                {
                    entityList.list = IBCenQtyDtlTempHistory.Search();
                    entityList.count = IBCenQtyDtlTempHistory.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtlTempHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtlTempHistory> entityList = new EntityList<CenQtyDtlTempHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenQtyDtlTempHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenQtyDtlTempHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenQtyDtlTempHistory>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenQtyDtlTempHistory.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenQtyDtlTempHistory>(entity);
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


        #region TestEquipLab
        //Add  TestEquipLab
        public BaseResultDataValue ST_UDTO_AddTestEquipLab(TestEquipLab entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBTestEquipLab.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBTestEquipLab.Add();
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
        //Update  TestEquipLab
        public BaseResultBool ST_UDTO_UpdateTestEquipLab(TestEquipLab entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipLab.Entity = entity;
                try
                {
                    baseResultBool.success = IBTestEquipLab.Edit();
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
        //Update  TestEquipLab
        public BaseResultBool ST_UDTO_UpdateTestEquipLabByField(TestEquipLab entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipLab.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipLab.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBTestEquipLab.Update(tempArray);
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
                        //baseResultBool.success = IBTestEquipLab.Edit();
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
        //Delele  TestEquipLab
        public BaseResultBool ST_UDTO_DelTestEquipLab(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBTestEquipLab.Entity = IBTestEquipLab.Get(id);
                if (IBTestEquipLab.Entity != null)
                {
                    long labid = IBTestEquipLab.Entity.LabID;
                    string entityName = IBTestEquipLab.Entity.GetType().Name;
                    baseResultBool.success = IBTestEquipLab.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipLab(TestEquipLab entity)
        {
            EntityList<TestEquipLab> entityList = new EntityList<TestEquipLab>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBTestEquipLab.Entity = entity;
                try
                {
                    entityList.list = IBTestEquipLab.Search();
                    entityList.count = IBTestEquipLab.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipLab>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipLabByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipLab> entityList = new EntityList<TestEquipLab>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBTestEquipLab.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBTestEquipLab.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipLab>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipLabById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBTestEquipLab.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<TestEquipLab>(entity);
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


        #region TestEquipProd
        //Add  TestEquipProd
        public BaseResultDataValue ST_UDTO_AddTestEquipProd(TestEquipProd entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBTestEquipProd.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBTestEquipProd.Add();
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
        //Update  TestEquipProd
        public BaseResultBool ST_UDTO_UpdateTestEquipProd(TestEquipProd entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipProd.Entity = entity;
                try
                {
                    baseResultBool.success = IBTestEquipProd.Edit();
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
        //Update  TestEquipProd
        public BaseResultBool ST_UDTO_UpdateTestEquipProdByField(TestEquipProd entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipProd.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipProd.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBTestEquipProd.Update(tempArray);
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
                        //baseResultBool.success = IBTestEquipProd.Edit();
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
        //Delele  TestEquipProd
        public BaseResultBool ST_UDTO_DelTestEquipProd(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBTestEquipProd.Entity = IBTestEquipProd.Get(id);
                if (IBTestEquipProd.Entity != null)
                {
                    long labid = IBTestEquipProd.Entity.LabID;
                    string entityName = IBTestEquipProd.Entity.GetType().Name;
                    baseResultBool.success = IBTestEquipProd.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipProd(TestEquipProd entity)
        {
            EntityList<TestEquipProd> entityList = new EntityList<TestEquipProd>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBTestEquipProd.Entity = entity;
                try
                {
                    entityList.list = IBTestEquipProd.Search();
                    entityList.count = IBTestEquipProd.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipProd>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipProdByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipProd> entityList = new EntityList<TestEquipProd>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBTestEquipProd.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBTestEquipProd.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipProd>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipProdById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBTestEquipProd.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<TestEquipProd>(entity);
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


        #region TestEquipType
        //Add  TestEquipType
        public BaseResultDataValue ST_UDTO_AddTestEquipType(TestEquipType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBTestEquipType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBTestEquipType.Add();
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
        //Update  TestEquipType
        public BaseResultBool ST_UDTO_UpdateTestEquipType(TestEquipType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipType.Entity = entity;
                try
                {
                    baseResultBool.success = IBTestEquipType.Edit();
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
        //Update  TestEquipType
        public BaseResultBool ST_UDTO_UpdateTestEquipTypeByField(TestEquipType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBTestEquipType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBTestEquipType.Update(tempArray);
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
                        //baseResultBool.success = IBTestEquipType.Edit();
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
        //Delele  TestEquipType
        public BaseResultBool ST_UDTO_DelTestEquipType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBTestEquipType.Entity = IBTestEquipType.Get(id);
                if (IBTestEquipType.Entity != null)
                {
                    long labid = IBTestEquipType.Entity.LabID;
                    string entityName = IBTestEquipType.Entity.GetType().Name;
                    baseResultBool.success = IBTestEquipType.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipType(TestEquipType entity)
        {
            EntityList<TestEquipType> entityList = new EntityList<TestEquipType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBTestEquipType.Entity = entity;
                try
                {
                    entityList.list = IBTestEquipType.Search();
                    entityList.count = IBTestEquipType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipType> entityList = new EntityList<TestEquipType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBTestEquipType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBTestEquipType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<TestEquipType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBTestEquipType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<TestEquipType>(entity);
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


        //#region FFeedback
        ////Add  FFeedback
        //public BaseResultDataValue ST_UDTO_AddFFeedback(FFeedback entity)
        //{
        //    IBFFeedback.Entity = entity;
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempBaseResultDataValue.success = IBFFeedback.Add();
        //        if (tempBaseResultDataValue.success)
        //        {
        //            IBFFeedback.Get(IBFFeedback.Entity.Id);
        //            tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFeedback.Entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        ////Update  FFeedback
        //public BaseResultBool ST_UDTO_UpdateFFeedback(FFeedback entity)
        //{
        //    IBFFeedback.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBFFeedback.Edit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Update  FFeedback
        //public BaseResultBool ST_UDTO_UpdateFFeedbackByField(FFeedback entity, string fields)
        //{
        //    IBFFeedback.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        if ((fields != null) && (fields.Length > 0))
        //        {
        //            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFeedback.Entity, fields);
        //            if (tempArray != null)
        //            {
        //                tempBaseResultBool.success = IBFFeedback.Update(tempArray);
        //            }
        //        }
        //        else
        //        {
        //            tempBaseResultBool.success = false;
        //            tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //            //tempBaseResultBool.success = IBFFeedback.Edit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Delele  FFeedback
        //public BaseResultBool ST_UDTO_DelFFeedback(long id)
        //{
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBFFeedback.Remove(longFFeedbackID);
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}

        //public BaseResultDataValue ST_UDTO_SearchFFeedback(FFeedback entity)
        //{
        //    IBFFeedback.Entity = entity;
        //    EntityList<FFeedback> tempEntityList = new EntityList<FFeedback>();
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempEntityList.list = IBFFeedback.Search();
        //        tempEntityList.count = IBFFeedback.GetTotalCount();
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
        //        try
        //        {
        //            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFeedback>(tempEntityList);
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}

        //public BaseResultDataValue ST_UDTO_SearchFFeedbackByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    EntityList<FFeedback> tempEntityList = new EntityList<FFeedback>();
        //    try
        //    {
        //        if ((sort != null) && (sort.Length > 0))
        //        {
        //            tempEntityList = IBFFeedback.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
        //        }
        //        else
        //        {
        //            tempEntityList = IBFFeedback.SearchListByHQL(where, page, limit);
        //        }
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
        //        try
        //        {
        //            if (isPlanish)
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFeedback>(tempEntityList);
        //            }
        //            else
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}

        //public BaseResultDataValue ST_UDTO_SearchFFeedbackById(long id, string fields, bool isPlanish)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        var tempEntity = IBFFeedback.Get(id);
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
        //        try
        //        {
        //            if (isPlanish)
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFeedback>(tempEntity);
        //            }
        //            else
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        //#endregion


        #region BmsAccountInput
        public BaseResultDataValue ST_UDTO_AddBmsAccountInputAndDtList(BmsAccountInput entity, string saleDocIDStr)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "entity信息为空!";
                return baseResultDataValue;
            }
            if (entity.Lab == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "机构信息为空!";
                return baseResultDataValue;
            }
            if (String.IsNullOrEmpty(saleDocIDStr))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "待入帐供货单信息为空!";
                return baseResultDataValue;
            }
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                return baseResultDataValue;
            }
            string employeeCName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!entity.UserID.HasValue)
                entity.UserID = long.Parse(employeeID);
            if (String.IsNullOrEmpty(entity.UserName))
                entity.UserName = employeeCName;
            entity.DataAddTime = DateTime.Now;
            IBBmsAccountInput.Entity = entity;
            try
            {
                baseResultDataValue = IBBmsAccountInput.AddBmsAccountInputAndDtList(saleDocIDStr);
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
            return baseResultDataValue;
        }
        public BaseResultBool ST_UDTO_DeleteBmsAccountInputAndDtList(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool = IBBmsAccountInput.DeleteBmsAccountInputAndDtList(id);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        //Add  BmsAccountInput
        public BaseResultDataValue ST_UDTO_AddBmsAccountInput(BmsAccountInput entity)
        {
            IBBmsAccountInput.Entity = entity;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsAccountInput.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsAccountInput.Add();
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
        //Update  BmsAccountInput
        public BaseResultBool ST_UDTO_UpdateBmsAccountInput(BmsAccountInput entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsAccountInput.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsAccountInput.Edit();
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
        //Update  BmsAccountInput
        public BaseResultBool ST_UDTO_UpdateBmsAccountInputByField(BmsAccountInput entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsAccountInput.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsAccountInput.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsAccountInput.Update(tempArray);
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
                        //baseResultBool.success = IBBmsAccountInput.Edit();
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
        //Delele  BmsAccountInput
        public BaseResultBool ST_UDTO_DelBmsAccountInput(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsAccountInput.Entity = IBBmsAccountInput.Get(id);
                if (IBBmsAccountInput.Entity != null)
                {
                    long labid = IBBmsAccountInput.Entity.LabID;
                    string entityName = IBBmsAccountInput.Entity.GetType().Name;
                    baseResultBool.success = IBBmsAccountInput.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInput(BmsAccountInput entity)
        {

            EntityList<BmsAccountInput> entityList = new EntityList<BmsAccountInput>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsAccountInput.Entity = entity;
                try
                {
                    entityList.list = IBBmsAccountInput.Search();
                    entityList.count = IBBmsAccountInput.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsAccountInput>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInputByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsAccountInput> entityList = new EntityList<BmsAccountInput>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsAccountInput.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsAccountInput.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsAccountInput>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInputById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsAccountInput.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsAccountInput>(entity);
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


        #region BmsAccountSaleDoc
        //Add  BmsAccountSaleDoc
        public BaseResultDataValue ST_UDTO_AddBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBmsAccountSaleDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsAccountSaleDoc.Add();
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
        //Update  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsAccountSaleDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsAccountSaleDoc.Edit();
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
        //Update  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsAccountSaleDocByField(BmsAccountSaleDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsAccountSaleDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsAccountSaleDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsAccountSaleDoc.Update(tempArray);
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
                        //baseResultBool.success = IBBmsAccountSaleDoc.Edit();
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
        //Delele  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_DelBmsAccountSaleDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsAccountSaleDoc.Entity = IBBmsAccountSaleDoc.Get(id);
                if (IBBmsAccountSaleDoc.Entity != null)
                {
                    long labid = IBBmsAccountSaleDoc.Entity.LabID;
                    string entityName = IBBmsAccountSaleDoc.Entity.GetType().Name;
                    baseResultBool.success = IBBmsAccountSaleDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            EntityList<BmsAccountSaleDoc> entityList = new EntityList<BmsAccountSaleDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsAccountSaleDoc.Entity = entity;
                try
                {
                    entityList.list = IBBmsAccountSaleDoc.Search();
                    entityList.count = IBBmsAccountSaleDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsAccountSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsAccountSaleDoc> entityList = new EntityList<BmsAccountSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsAccountSaleDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsAccountSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsAccountSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsAccountSaleDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsAccountSaleDoc>(entity);
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


        #region BmsCenSaleDocConfirm
        //Add  BmsCenSaleDocConfirm
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDocConfirm.Add();
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
        //Update  BmsCenSaleDocConfirm
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDocConfirm.Edit();
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
        //Update  BmsCenSaleDocConfirm
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocConfirmByField(BmsCenSaleDocConfirm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDocConfirm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDocConfirm.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDocConfirm.Edit();
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
        //Delele  BmsCenSaleDocConfirm
        public BaseResultBool ST_UDTO_DelBmsCenSaleDocConfirm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDocConfirm.Entity = IBBmsCenSaleDocConfirm.Get(id);
                if (IBBmsCenSaleDocConfirm.Entity != null)
                {
                    long labid = IBBmsCenSaleDocConfirm.Entity.LabID;
                    string entityName = IBBmsCenSaleDocConfirm.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDocConfirm.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity)
        {
            EntityList<BmsCenSaleDocConfirm> entityList = new EntityList<BmsCenSaleDocConfirm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDocConfirm.Search();
                    entityList.count = IBBmsCenSaleDocConfirm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDocConfirm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDocConfirm> entityList = new EntityList<BmsCenSaleDocConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDocConfirm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDocConfirm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDocConfirm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirmById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDocConfirm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDocConfirm>(entity);
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

        #region BmsCenSaleDtlConfirm
        //Add  BmsCenSaleDtlConfirm
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlConfirm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBmsCenSaleDtlConfirm.Add();
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
        //Update  BmsCenSaleDtlConfirm
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlConfirm.Entity = entity;
                try
                {
                    baseResultBool.success = IBBmsCenSaleDtlConfirm.Edit();
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
        //Update  BmsCenSaleDtlConfirm
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlConfirmByField(BmsCenSaleDtlConfirm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBmsCenSaleDtlConfirm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlConfirm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBmsCenSaleDtlConfirm.Update(tempArray);
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
                        //baseResultBool.success = IBBmsCenSaleDtlConfirm.Edit();
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
        //Delele  BmsCenSaleDtlConfirm
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtlConfirm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBmsCenSaleDtlConfirm.Entity = IBBmsCenSaleDtlConfirm.Get(id);
                if (IBBmsCenSaleDtlConfirm.Entity != null)
                {
                    long labid = IBBmsCenSaleDtlConfirm.Entity.LabID;
                    string entityName = IBBmsCenSaleDtlConfirm.Entity.GetType().Name;
                    baseResultBool.success = IBBmsCenSaleDtlConfirm.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity)
        {
            EntityList<BmsCenSaleDtlConfirm> entityList = new EntityList<BmsCenSaleDtlConfirm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBmsCenSaleDtlConfirm.Entity = entity;
                try
                {
                    entityList.list = IBBmsCenSaleDtlConfirm.Search();
                    entityList.count = IBBmsCenSaleDtlConfirm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlConfirm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlConfirm> entityList = new EntityList<BmsCenSaleDtlConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBmsCenSaleDtlConfirm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBmsCenSaleDtlConfirm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlConfirm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirmById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBmsCenSaleDtlConfirm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BmsCenSaleDtlConfirm>(entity);
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



        public BaseResultDataValue ST_UDTO_CenOrgUploadExcelData()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "false";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/upload/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string filepath = Path.Combine(parentPath, Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    //baseResultDataValue.ResultDataValue = filepath;
                    ExcelHelper eh = new ExcelHelper(filepath, "YES");
                    DataSet set = eh.GetExcelDataSet(null);
                    if (set != null && set.Tables.Count > 0 && set.Tables[0].Rows.Count >= 1)
                    {
                        DataTable dt = set.Tables[0];
                        string errorinfo;
                        bool flag = IBCenOrg.ExcelSave(dt, out errorinfo);
                        if (flag)
                        {
                            baseResultDataValue.success = true;
                            return baseResultDataValue;
                        }
                        else
                        {
                            baseResultDataValue.ErrorInfo = "Excel文件数据导入错误！ErrorInfo：" + errorinfo;
                            baseResultDataValue.success = false;
                            return baseResultDataValue;
                        }
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "Excel文件数据错误！";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.ErrorInfo = e.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
        }

        #region SCAttachment
        /// <summary>
        /// 上传公共附件
        /// </summary>
        /// <returns></returns>
        public Message SC_UploadAddSCAttachment()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = nullValue;
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //需要保存的数据对象名称
                string objectName = "", fkObjectId = "", fkObjectName = "", fileName = "", newFileName = "", contentType = "";
                objectName = HttpContext.Current.Request.Form["ObjectEName"];
                //需要保存的数据对象的子对象Id值
                fkObjectId = HttpContext.Current.Request.Form["FKObjectId"];
                //需要保存的数据对象的子对象名称
                fkObjectName = HttpContext.Current.Request.Form["FKObjectName"];
                string fileSize = HttpContext.Current.Request.Form["FileSize"];
                fileSize = HttpContext.Current.Request.Form["FileSize"];
                //文件名称
                fileName = HttpContext.Current.Request.Form["FileName"];
                newFileName = HttpContext.Current.Request.Form["NewFileName"];
                //公共附件分类保存的文件夹名称
                string saveCategory = HttpContext.Current.Request.Form["SaveCategory"];
                //业务模块代码
                string businessModuleCode = HttpContext.Current.Request.Form["BusinessModuleCode"];
                int len = 0;
                if (!string.IsNullOrEmpty(fileSize))
                {
                    len = Int32.Parse(fileSize);
                }
                if (file != null)
                {
                    //file.FileName处理
                    //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                    len = file.ContentLength;
                    contentType = file.ContentType;
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    if (string.IsNullOrEmpty(fileName))
                        fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    //ZhiFang.Common.Log.Log.Debug("FileName:" + fileName);
                }

                if (String.IsNullOrEmpty(objectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectId))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象值为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && !string.IsNullOrEmpty(fkObjectId) && len > 0 && !string.IsNullOrEmpty(fileName))
                {
                    //上传附件路径
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UploadFilesPath.ToString());
                    string tempPath = "\\" + objectName + "\\";
                    if (!String.IsNullOrEmpty(saveCategory))
                    {
                        tempPath = tempPath + saveCategory + "\\";
                    }
                    tempPath = tempPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));

                    switch (objectName)
                    {
                        case "SCAttachment"://公共附件
                            SCAttachment entity = new SCAttachment();
                            entity.BusinessModuleCode = businessModuleCode;
                            entity.FileName = fileName;
                            entity.FileSize = len;// / 1024;
                            entity.FilePath = tempPath;
                            entity.IsUse = true;
                            entity.NewFileName = newFileName;
                            entity.FileExt = fileExt;
                            entity.FileType = contentType;
                            brdv = IBSCAttachment.AddSCAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity);
                            if (brdv.success)
                                brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                            //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCAttachment.Entity);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("公共附件上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载公共附件的文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream SC_UDTO_DownLoadSCAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                SCAttachment file = IBSCAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "附件不存在!请重新上传或联系管理员。";
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                        return memoryStream;
                    }
                    else
                    {
                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        string filename = file.NewFileName + file.FileExt;
                        if (string.IsNullOrEmpty(filename))
                        {
                            filename = file.FileName;
                        }
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "附件不存在!请重新上传或联系管理员。";
                ZhiFang.Common.Log.Log.Error("公共附件下载错误信息:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }

        }

        //Add  SCAttachment
        public BaseResultDataValue SC_UDTO_AddSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCAttachment.Get(IBSCAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCAttachment.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  SCAttachment
        public BaseResultBool SC_UDTO_UpdateSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCAttachment
        public BaseResultBool SC_UDTO_UpdateSCAttachmentByField(SCAttachment entity, string fields)
        {
            IBSCAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCAttachment.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  SCAttachment
        public BaseResultBool SC_UDTO_DelSCAttachment(long longSCAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCAttachment.Remove(longSCAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            EntityList<SCAttachment> tempEntityList = new EntityList<SCAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCAttachment.Search();
                tempEntityList.count = IBSCAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCAttachment>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //查询公共附件表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCAttachment> tempEntityList = new EntityList<SCAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCAttachment>(tempEntityList);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SC_UDTO_SearchSCAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCAttachment>(tempEntity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region SCInteraction
        //Add  SCInteraction
        public BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBSCInteraction.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBSCInteraction.Add();
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
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool.success = IBSCInteraction.Edit();
                if (baseResultBool.success)
                {
                    IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultBool.success = IBSCInteraction.Update(tempArray);
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
                    //baseResultBool.success = IBSCInteraction.Edit();
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        //Delele  SCInteraction
        public BaseResultBool SC_UDTO_DelSCInteraction(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBSCInteraction.Entity = IBSCInteraction.Get(id);
                if (IBSCInteraction.Entity != null)
                {
                    long labid = IBSCInteraction.Entity.LabID;
                    string entityName = IBSCInteraction.Entity.GetType().Name;
                    baseResultBool.success = IBSCInteraction.RemoveByHQL(id);
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

        public BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            EntityList<SCInteraction> entityList = new EntityList<SCInteraction>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                entityList.list = IBSCInteraction.Search();
                entityList.count = IBSCInteraction.GetTotalCount();
                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCInteraction>(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        //查询公共交流表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCInteraction> entityList = new EntityList<SCInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCInteraction>(entityList);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCInteraction.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCInteraction>(entity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region SCOperation
        //Add  SCOperation
        public BaseResultDataValue SC_UDTO_AddSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCOperation.Get(IBSCOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCOperation.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  SCOperation
        public BaseResultBool SC_UDTO_UpdateSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCOperation
        public BaseResultBool SC_UDTO_UpdateSCOperationByField(SCOperation entity, string fields)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCOperation.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  SCOperation
        public BaseResultBool SC_UDTO_DelSCOperation(long longSCOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Remove(longSCOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCOperation.Search();
                tempEntityList.count = IBSCOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //查询公共操作记录表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SC_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCOperation>(tempEntity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion       

        #region 获取程序内部字典
        public BaseResultDataValue GetEnumDic(string enumname)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetEnumDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetEnumDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue GetClassDic(string classname, string classnamespace)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string entitynamespace = "ZhiFang.Entity.Base";
            if (classname == null || classname.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classname为空！";
                return tempBaseResultDataValue;
            }
            if (classnamespace == null || classnamespace.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classnamespace为空！";
                return tempBaseResultDataValue;
            }
            try
            {
                entitynamespace = classnamespace;
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
                if (t == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：未找到类字典：" + classname + ",命名空间：" + classnamespace + "！";
                    return tempBaseResultDataValue;
                }
                string jsonstring = "";
                var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in t.GetFields())
                {
                    JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                    jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                }
                jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonstring + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (jsonpara.Length <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：参数为空！";
                    ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：参数为空");
                }
                string jsonresult = "";
                foreach (ClassDicSearchPara cdsp in jsonpara)
                {
                    if (cdsp.classname == null || cdsp.classname.Trim() == "" || cdsp.classnamespace == null || cdsp.classnamespace.Trim() == "")
                    {
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":},";
                    }
                    else
                    {
                        string entitynamespace = "";
                        entitynamespace = cdsp.classnamespace;
                        Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + cdsp.classname);
                        if (t == null)
                        {
                            ZhiFang.Common.Log.Log.Error("GetClassDicList错误信息：未找到类字典：" + cdsp.classname + ",命名空间：" + cdsp.classnamespace + "！");
                            jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[]},";
                            continue;
                        }
                        string jsonstring = "";
                        var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                        foreach (FieldInfo field in t.GetFields())
                        {
                            JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                            jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                            //jsonstring += ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)) + ",";
                        }
                        jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[" + jsonstring + "]},";
                    }
                }
                jsonresult = jsonresult.Substring(0, jsonresult.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonresult + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region GoodsQualification
        public Stream ST_UDTO_AddGoodsQualificationAndUploadRegisterFile()
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();
            GoodsQualification entity = null;
            string entityStr = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
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
            if (baseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity为空!";
            }
            if (baseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增资质证件信息出错:" + baseResultBool.ErrorInfo);
            }
            if (baseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsQualification>(entityStr);

                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "新增资质证件信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增资质证件信息序列化出错:" + ex.Message);
                }
            }

            if (baseResultBool.success)
            {
                if (file != null && !String.IsNullOrEmpty(file.FileName))
                {
                    entity.FileType = file.ContentType;
                    entity.FileName = file.FileName;
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    entity.FileExt = fileExt;
                }
                entity.DataAddTime = DateTime.Now;
                IBGoodsQualification.Entity = entity;
                try
                {
                    if (String.IsNullOrEmpty(hrdeptCode))
                        hrdeptCode = "";
                    baseResultBool = IBGoodsQualification.AddGoodsQualificationAndUploadRegisterFile(file, hrdeptID, hrdeptCode);
                    if (baseResultBool.success)
                    {
                        baseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增资质证件信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            GoodsQualification entity = null;
            string fields = "";
            string fFileEntity = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                //if (!String.IsNullOrEmpty(file.FileName))
                //{
                //    string[] temp = file.FileName.Split('.');
                //    if (temp[temp.Length - 1].ToLower() != "pdf")
                //    {
                //        baseResultBool.success = false;
                //        baseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                //        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                //        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                //        return ResponseResultStream.GetResultInfoOfStream(strResult);
                //    }
                //}
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
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsQualification>(fFileEntity);
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
                if (file != null && !String.IsNullOrEmpty(file.FileName))
                {
                    entity.FileType = file.ContentType;
                    entity.FileName = file.FileName;
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    entity.FileExt = fileExt;
                    if (!fields.Contains("FileType")) fields += ",FileType,FileName,FileExt";
                }
                entity.DataUpdateTime = DateTime.Now;
                IBGoodsQualification.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsQualification.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBGoodsQualification.UpdateGoodsQualificationAndUploadRegisterFileByField(tempArray, file);
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
                        ZhiFang.Common.Log.Log.Error("更新资质证件信息出错:" + baseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新资质证件信息出错:" + baseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新资质证件信息出错1:" + baseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_GoodsQualificationPreviewPdf(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = "";
                GoodsQualification entity = null;
                fileStream = IBGoodsQualification.GetGoodsQualificationFileStream(id, ref entity);
                filename = entity.FileName;
                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "资质证件文件不存在!请重新上传或联系管理员。";
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
                        System.Web.HttpContext.Current.Response.ContentType = entity.FileType;
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = entity.FileType;
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                    return fileStream;
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "预览资质证件文件错误!" + ex.Message;
                ZhiFang.Common.Log.Log.Error(errorInfo);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }
        }
        //Add  GoodsQualification
        public BaseResultDataValue ST_UDTO_AddGoodsQualification(GoodsQualification entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBGoodsQualification.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBGoodsQualification.Add();
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
        //Update  GoodsQualification
        public BaseResultBool ST_UDTO_UpdateGoodsQualification(GoodsQualification entity)
        {

            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBGoodsQualification.Entity = entity;
                try
                {
                    baseResultBool.success = IBGoodsQualification.Edit();
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
        //Update  GoodsQualification
        public BaseResultBool ST_UDTO_UpdateGoodsQualificationByField(GoodsQualification entity, string fields)
        {

            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBGoodsQualification.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsQualification.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBGoodsQualification.Update(tempArray);
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
                        //baseResultBool.success = IBGoodsQualification.Edit();
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
        //Delele  GoodsQualification
        public BaseResultBool ST_UDTO_DelGoodsQualification(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBGoodsQualification.Entity = IBGoodsQualification.Get(id);
                if (IBGoodsQualification.Entity != null)
                {
                    long labid = IBGoodsQualification.Entity.LabID;
                    string entityName = IBGoodsQualification.Entity.GetType().Name;
                    baseResultBool.success = IBGoodsQualification.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsQualification(GoodsQualification entity)
        {

            EntityList<GoodsQualification> entityList = new EntityList<GoodsQualification>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBGoodsQualification.Entity = entity;
                try
                {
                    entityList.list = IBGoodsQualification.Search();
                    entityList.count = IBGoodsQualification.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GoodsQualification>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsQualificationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsQualification> entityList = new EntityList<GoodsQualification>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBGoodsQualification.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBGoodsQualification.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GoodsQualification>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsQualificationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBGoodsQualification.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<GoodsQualification>(entity);
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

        #region 供货单多批次验收
        /// <summary>
        /// 获取某一供货单的明细(包括原始明细及合并后的明细)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlForCheckByBmsCenSaleDocId(int page, int limit, string fields, long bmsCenSaleDocId, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlOV> entityList = new EntityList<BmsCenSaleDtlOV>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBBmsCenSaleDtl.SearchListForCheckByBmsCenSaleDocId(bmsCenSaleDocId, sort, page, limit);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtlOV>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);//fields
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
        public BaseResultDataValue ST_UDTO_SearchMergerDtListForCheckByBmsCenSaleDocId(int page, int limit, string fields, long bmsCenSaleDocId, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtl> entityList = new EntityList<BmsCenSaleDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBBmsCenSaleDtl.SearchMergerDtListForCheckByBmsCenSaleDocId(bmsCenSaleDocId, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BmsCenSaleDtl>(entityList);
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
        #endregion

    }
}
