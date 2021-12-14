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

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReagentSysService : IReagentSysService
    {
        #region IReagentSysService 成员

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

        IBLL.ReagentSys.IBFFeedback IBFFeedback { get; set; }

        ZhiFang.Digitlab.IBLL.Business.IBSCInteraction IBSCInteraction { get; set; }
        IBLL.ReagentSys.IBGoodsQualification IBGoodsQualification { get; set; }
        #endregion


        #region BmsCenOrderDoc
        //Add  BmsCenOrderDoc
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            //entity.CompanyName = "";
            IBBmsCenOrderDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenOrderDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenOrderDoc.Get(IBBmsCenOrderDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenOrderDoc.Entity);
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
        //Update  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            IBBmsCenOrderDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocByField(BmsCenOrderDoc entity, string fields)
        {
            IBBmsCenOrderDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenOrderDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDoc.Edit();
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
        //Delele  BmsCenOrderDoc
        public BaseResultBool ST_UDTO_DelBmsCenOrderDoc(long longBmsCenOrderDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDoc.Remove(longBmsCenOrderDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDoc(BmsCenOrderDoc entity)
        {
            IBBmsCenOrderDoc.Entity = entity;
            EntityList<BmsCenOrderDoc> tempEntityList = new EntityList<BmsCenOrderDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenOrderDoc.Search();
                tempEntityList.count = IBBmsCenOrderDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDoc> tempEntityList = new EntityList<BmsCenOrderDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenOrderDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenOrderDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenOrderDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenOrderDoc>(tempEntity);
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


        #region BmsCenOrderDtl
        //Add  BmsCenOrderDtl
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            IBBmsCenOrderDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenOrderDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenOrderDtl.Get(IBBmsCenOrderDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenOrderDtl.Entity);
                    IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(IBBmsCenOrderDtl.Entity.BmsCenOrderDoc.Id);
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
        //Update  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            IBBmsCenOrderDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDtl.Edit();
                if (tempBaseResultBool.success)
                {
                    BmsCenOrderDtl orderDtl = IBBmsCenOrderDtl.Get(entity.Id);
                    IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(orderDtl.BmsCenOrderDoc.Id);
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
        //Update  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlByField(BmsCenOrderDtl entity, string fields)
        {
            IBBmsCenOrderDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenOrderDtl.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            BmsCenOrderDtl orderDtl = IBBmsCenOrderDtl.Get(entity.Id);
                            IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(orderDtl.BmsCenOrderDoc.Id);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDtl.Edit();
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
        //Delele  BmsCenOrderDtl
        public BaseResultBool ST_UDTO_DelBmsCenOrderDtl(long longBmsCenOrderDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {

                BmsCenOrderDtl orderDtl = IBBmsCenOrderDtl.Get(longBmsCenOrderDtlID);
                long docID = orderDtl.BmsCenOrderDoc.Id;
                tempBaseResultBool.success = IBBmsCenOrderDtl.Remove(longBmsCenOrderDtlID);
                if (tempBaseResultBool.success)
                    IBBmsCenOrderDoc.EditBmsCenOrderDocTotalPrice(docID);

            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtl(BmsCenOrderDtl entity)
        {
            IBBmsCenOrderDtl.Entity = entity;
            EntityList<BmsCenOrderDtl> tempEntityList = new EntityList<BmsCenOrderDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenOrderDtl.Search();
                tempEntityList.count = IBBmsCenOrderDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDtl> tempEntityList = new EntityList<BmsCenOrderDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenOrderDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenOrderDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenOrderDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenOrderDtl>(tempEntity);
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


        #region BmsCenSaleDoc
        //Add  BmsCenSaleDoc
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            if (entity != null && !entity.IsAccountInput.HasValue)
                entity.IsAccountInput = 0;
            IBBmsCenSaleDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenSaleDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenSaleDoc.Get(IBBmsCenSaleDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenSaleDoc.Entity);
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
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            IBBmsCenSaleDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocByField(BmsCenSaleDoc entity, string fields)
        {
            IBBmsCenSaleDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenSaleDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenSaleDoc.Edit();
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
        //Delele  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_DelBmsCenSaleDoc(long longBmsCenSaleDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDoc.Remove(longBmsCenSaleDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDoc(BmsCenSaleDoc entity)
        {
            IBBmsCenSaleDoc.Entity = entity;
            EntityList<BmsCenSaleDoc> tempEntityList = new EntityList<BmsCenSaleDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenSaleDoc.Search();
                tempEntityList.count = IBBmsCenSaleDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDoc> tempEntityList = new EntityList<BmsCenSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenSaleDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenSaleDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenSaleDoc>(tempEntity);
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


        #region BmsCenSaleDtl
        //Add  BmsCenSaleDtl
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            IBBmsCenSaleDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenSaleDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenSaleDtl.Get(IBBmsCenSaleDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenSaleDtl.Entity);
                    IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(IBBmsCenSaleDtl.Entity.BmsCenSaleDoc.Id);
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
        //Update  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            IBBmsCenSaleDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDtl.Edit();
                if (tempBaseResultBool.success)
                {
                    BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(entity.Id);
                    IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.BmsCenSaleDoc.Id);
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
        //Update  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlByField(BmsCenSaleDtl entity, string fields)
        {
            IBBmsCenSaleDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenSaleDtl.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(entity.Id);
                            IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.BmsCenSaleDoc.Id);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenSaleDtl.Edit();
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
        //Delele  BmsCenSaleDtl
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtl(long longBmsCenSaleDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(longBmsCenSaleDtlID);
                long saleDocID = saleDtl.BmsCenSaleDoc.Id;
                tempBaseResultBool.success = IBBmsCenSaleDtl.Remove(longBmsCenSaleDtlID);
                if (tempBaseResultBool.success)
                    IBBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtl(BmsCenSaleDtl entity)
        {
            IBBmsCenSaleDtl.Entity = entity;
            EntityList<BmsCenSaleDtl> tempEntityList = new EntityList<BmsCenSaleDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenSaleDtl.Search();
                tempEntityList.count = IBBmsCenSaleDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtl> tempEntityList = new EntityList<BmsCenSaleDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenSaleDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenSaleDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenSaleDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenSaleDtl>(tempEntity);
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


        #region BmsCenSaleDtlBarCode
        //Add  BmsCenSaleDtlBarCode
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            IBBmsCenSaleDtlBarCode.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenSaleDtlBarCode.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenSaleDtlBarCode.Get(IBBmsCenSaleDtlBarCode.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenSaleDtlBarCode.Entity);
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
        //Update  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            IBBmsCenSaleDtlBarCode.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDtlBarCode.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCodeByField(BmsCenSaleDtlBarCode entity, string fields)
        {
            IBBmsCenSaleDtlBarCode.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlBarCode.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenSaleDtlBarCode.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenSaleDtlBarCode.Edit();
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
        //Delele  BmsCenSaleDtlBarCode
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtlBarCode(long longBmsCenSaleDtlBarCodeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDtlBarCode.Remove(longBmsCenSaleDtlBarCodeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity)
        {
            IBBmsCenSaleDtlBarCode.Entity = entity;
            EntityList<BmsCenSaleDtlBarCode> tempEntityList = new EntityList<BmsCenSaleDtlBarCode>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenSaleDtlBarCode.Search();
                tempEntityList.count = IBBmsCenSaleDtlBarCode.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtlBarCode>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlBarCode> tempEntityList = new EntityList<BmsCenSaleDtlBarCode>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenSaleDtlBarCode.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenSaleDtlBarCode.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtlBarCode>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenSaleDtlBarCode.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenSaleDtlBarCode>(tempEntity);
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


        #region CenOrg
        //Add  CenOrg
        public BaseResultDataValue ST_UDTO_AddCenOrg(CenOrg entity)
        {
            IBCenOrg.Entity = entity;
            IBCenOrg.Entity.DataUpdateTime = DateTime.Now;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenOrg.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenOrg.Get(IBCenOrg.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenOrg.Entity);
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
        //Update  CenOrg
        public BaseResultBool ST_UDTO_UpdateCenOrg(CenOrg entity)
        {
            IBCenOrg.Entity = entity;
            IBCenOrg.Entity.DataUpdateTime = DateTime.Now;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrg.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenOrg
        public BaseResultBool ST_UDTO_UpdateCenOrgByField(CenOrg entity, string fields)
        {
            IBCenOrg.Entity = entity;
            IBCenOrg.Entity.DataUpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(fields))
                fields = fields + ",DataUpdateTime";
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrg.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenOrg.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenOrg.Edit();
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
        //Delele  CenOrg
        public BaseResultBool ST_UDTO_DelCenOrg(long longCenOrgID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrg.Remove(longCenOrgID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenOrg(CenOrg entity)
        {
            IBCenOrg.Entity = entity;
            EntityList<CenOrg> tempEntityList = new EntityList<CenOrg>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenOrg.Search();
                tempEntityList.count = IBCenOrg.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrg>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrg> tempEntityList = new EntityList<CenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenOrg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenOrg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrg>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenOrg.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenOrg>(tempEntity);
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


        #region CenOrgCondition
        //Add  CenOrgCondition
        public BaseResultDataValue ST_UDTO_AddCenOrgCondition(CenOrgCondition entity)
        {
            IBCenOrgCondition.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenOrgCondition.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenOrgCondition.Get(IBCenOrgCondition.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenOrgCondition.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  CenOrgCondition
        public BaseResultBool ST_UDTO_UpdateCenOrgCondition(CenOrgCondition entity)
        {
            IBCenOrgCondition.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrgCondition.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenOrgCondition
        public BaseResultBool ST_UDTO_UpdateCenOrgConditionByField(CenOrgCondition entity, string fields)
        {
            IBCenOrgCondition.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrgCondition.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenOrgCondition.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenOrgCondition.Edit();
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
        //Delele  CenOrgCondition
        public BaseResultBool ST_UDTO_DelCenOrgCondition(long longCenOrgConditionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrgCondition.Remove(longCenOrgConditionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenOrgCondition(CenOrgCondition entity)
        {
            IBCenOrgCondition.Entity = entity;
            EntityList<CenOrgCondition> tempEntityList = new EntityList<CenOrgCondition>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenOrgCondition.Search();
                tempEntityList.count = IBCenOrgCondition.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrgCondition>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgConditionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrgCondition> tempEntityList = new EntityList<CenOrgCondition>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenOrgCondition.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenOrgCondition.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrgCondition>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgConditionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenOrgCondition.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenOrgCondition>(tempEntity);
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


        #region CenOrgType
        //Add  CenOrgType
        public BaseResultDataValue ST_UDTO_AddCenOrgType(CenOrgType entity)
        {
            IBCenOrgType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenOrgType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenOrgType.Get(IBCenOrgType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenOrgType.Entity);
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
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgType(CenOrgType entity)
        {
            IBCenOrgType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrgType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgTypeByField(CenOrgType entity, string fields)
        {
            IBCenOrgType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrgType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenOrgType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenOrgType.Edit();
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
        //Delele  CenOrgType
        public BaseResultBool ST_UDTO_DelCenOrgType(long longCenOrgTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenOrgType.Remove(longCenOrgTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenOrgType(CenOrgType entity)
        {
            IBCenOrgType.Entity = entity;
            EntityList<CenOrgType> tempEntityList = new EntityList<CenOrgType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenOrgType.Search();
                tempEntityList.count = IBCenOrgType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrgType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrgType> tempEntityList = new EntityList<CenOrgType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenOrgType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenOrgType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenOrgType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenOrgType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenOrgType>(tempEntity);
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


        #region CenMsg
        //Add  CenMsg
        public BaseResultDataValue ST_UDTO_AddCenMsg(CenMsg entity)
        {
            IBCenMsg.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenMsg.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenMsg.Get(IBCenMsg.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenMsg.Entity);
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
        //Update  CenMsg
        public BaseResultBool ST_UDTO_UpdateCenMsg(CenMsg entity)
        {
            IBCenMsg.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenMsg.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenMsg
        public BaseResultBool ST_UDTO_UpdateCenMsgByField(CenMsg entity, string fields)
        {
            IBCenMsg.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenMsg.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenMsg.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenMsg.Edit();
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
        //Delele  CenMsg
        public BaseResultBool ST_UDTO_DelCenMsg(long longCenMsgID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenMsg.Remove(longCenMsgID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenMsg(CenMsg entity)
        {
            IBCenMsg.Entity = entity;
            EntityList<CenMsg> tempEntityList = new EntityList<CenMsg>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenMsg.Search();
                tempEntityList.count = IBCenMsg.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenMsg>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenMsg> tempEntityList = new EntityList<CenMsg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenMsg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenMsg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenMsg>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenMsgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenMsg.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenMsg>(tempEntity);
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


        #region CenQtyDtl
        //Add  CenQtyDtl
        public BaseResultDataValue ST_UDTO_AddCenQtyDtl(CenQtyDtl entity)
        {
            IBCenQtyDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenQtyDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenQtyDtl.Get(IBCenQtyDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenQtyDtl.Entity);
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
        //Update  CenQtyDtl
        public BaseResultBool ST_UDTO_UpdateCenQtyDtl(CenQtyDtl entity)
        {
            IBCenQtyDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenQtyDtl
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlByField(CenQtyDtl entity, string fields)
        {
            IBCenQtyDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenQtyDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenQtyDtl.Edit();
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
        //Delele  CenQtyDtl
        public BaseResultBool ST_UDTO_DelCenQtyDtl(long longCenQtyDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtl.Remove(longCenQtyDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtl(CenQtyDtl entity)
        {
            IBCenQtyDtl.Entity = entity;
            EntityList<CenQtyDtl> tempEntityList = new EntityList<CenQtyDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenQtyDtl.Search();
                tempEntityList.count = IBCenQtyDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtl> tempEntityList = new EntityList<CenQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenQtyDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenQtyDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenQtyDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenQtyDtl>(tempEntity);
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


        #region Goods
        //Add  Goods
        public BaseResultDataValue ST_UDTO_AddGoods(Goods entity)
        {
            IBGoods.Entity = entity;
            IBGoods.Entity.DataUpdateTime = DateTime.Now;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddGoods:" + entity.CenOrg.Id.ToString());
                tempBaseResultDataValue.success = IBGoods.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBGoods.Get(IBGoods.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoods.Entity);
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
        //Update  Goods
        public BaseResultBool ST_UDTO_UpdateGoods(Goods entity)
        {
            IBGoods.Entity = entity;
            IBGoods.Entity.DataUpdateTime = DateTime.Now;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGoods.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Goods
        public BaseResultBool ST_UDTO_UpdateGoodsByField(Goods entity, string fields)
        {
            IBGoods.Entity = entity;
            IBGoods.Entity.DataUpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(fields))
                fields = fields + ",DataUpdateTime";
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoods.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBGoods.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGoods.Edit();
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
        //Delele  Goods
        public BaseResultBool ST_UDTO_DelGoods(long longGoodsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                Goods aa = IBGoods.Get(1);
                tempBaseResultBool.success = IBGoods.Remove(longGoodsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchGoods(Goods entity)
        {
            IBGoods.Entity = entity;
            EntityList<Goods> tempEntityList = new EntityList<Goods>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBGoods.Search();
                tempEntityList.count = IBGoods.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Goods>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Goods> tempEntityList = new EntityList<Goods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Goods>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGoods.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Goods>(tempEntity);
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


        #region GoodsRegister
        public Stream ST_UDTO_AddGoodsRegisterAndUploadRegisterFile()
        {
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
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
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
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
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "登录人信息为空!请登录后再操作!";
            }
            if (tempBaseResultBool.success && String.IsNullOrEmpty(hrdeptID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "登录人机构信息为空!请登录后再操作!";
            }
            //if (tempBaseResultBool.success && String.IsNullOrEmpty(hrdeptCode))
            //{
            //    tempBaseResultBool.success = false;
            //    tempBaseResultBool.ErrorInfo = "登录人机构编号信息为空!请登录后再操作!";
            //}
            if (tempBaseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity为空!";
            }
            if (tempBaseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增注册证信息出错:" + tempBaseResultBool.ErrorInfo);
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsRegister>(entityStr);
                    entity.EmpID = long.Parse(employeeID);
                    entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "新增注册证信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增注册证信息序列化出错:" + ex.Message);
                }
            }

            if (tempBaseResultBool.success)
            {
                IBGoodsRegister.Entity = entity;
                try
                {
                    if (String.IsNullOrEmpty(hrdeptCode))
                        hrdeptCode = entity.CenOrgNo;
                    tempBaseResultBool = IBGoodsRegister.AddGoodsRegisterAndUploadRegisterFile(file, hrdeptID, hrdeptCode);
                    if (tempBaseResultBool.success)
                    {
                        IBGoodsRegister.Get(IBGoodsRegister.Entity.Id);
                        tempBaseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoodsRegister.Entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增注册证信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
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
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
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
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (tempBaseResultBool.success)
            {
                entity.EmpID = long.Parse(employeeID);
                entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                IBGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool = IBGoodsRegister.UpdateGoodsRegisterAndUploadRegisterFileByField(tempArray, file);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + tempBaseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + tempBaseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新注册证信息出错1:" + tempBaseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
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
            IBGoodsRegister.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBGoodsRegister.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBGoodsRegister.Get(IBGoodsRegister.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoodsRegister.Entity);
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
        //Update  GoodsRegister
        public BaseResultBool ST_UDTO_UpdateGoodsRegister(GoodsRegister entity)
        {
            IBGoodsRegister.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGoodsRegister.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  GoodsRegister
        public BaseResultBool ST_UDTO_UpdateGoodsRegisterByField(GoodsRegister entity, string fields)
        {
            IBGoodsRegister.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsRegister.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBGoodsRegister.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGoodsRegister.Edit();
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
        //Delele  GoodsRegister
        public BaseResultBool ST_UDTO_DelGoodsRegister(long longGoodsRegisterID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGoodsRegister.Remove(longGoodsRegisterID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchGoodsRegister(GoodsRegister entity)
        {
            IBGoodsRegister.Entity = entity;
            EntityList<GoodsRegister> tempEntityList = new EntityList<GoodsRegister>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBGoodsRegister.Search();
                tempEntityList.count = IBGoodsRegister.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GoodsRegister>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsRegister> tempEntityList = new EntityList<GoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGoodsRegister.SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGoodsRegister.SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GoodsRegister>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsRegister> tempEntityList = new EntityList<GoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGoodsRegister.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGoodsRegister.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GoodsRegister>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsRegisterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGoodsRegister.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<GoodsRegister>(tempEntity);
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


        #region BmsCenOrderDocHistory
        //Add  BmsCenOrderDocHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            IBBmsCenOrderDocHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenOrderDocHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenOrderDocHistory.Get(IBBmsCenOrderDocHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenOrderDocHistory.Entity);
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
        //Update  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            IBBmsCenOrderDocHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDocHistory.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistoryByField(BmsCenOrderDocHistory entity, string fields)
        {
            IBBmsCenOrderDocHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDocHistory.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenOrderDocHistory.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDocHistory.Edit();
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
        //Delele  BmsCenOrderDocHistory
        public BaseResultBool ST_UDTO_DelBmsCenOrderDocHistory(long longBmsCenOrderDocHistoryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDocHistory.Remove(longBmsCenOrderDocHistoryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistory(BmsCenOrderDocHistory entity)
        {
            IBBmsCenOrderDocHistory.Entity = entity;
            EntityList<BmsCenOrderDocHistory> tempEntityList = new EntityList<BmsCenOrderDocHistory>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenOrderDocHistory.Search();
                tempEntityList.count = IBBmsCenOrderDocHistory.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDocHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDocHistory> tempEntityList = new EntityList<BmsCenOrderDocHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenOrderDocHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenOrderDocHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDocHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenOrderDocHistory.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenOrderDocHistory>(tempEntity);
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


        #region BmsCenOrderDtlHistory
        //Add  BmsCenOrderDtlHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            IBBmsCenOrderDtlHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenOrderDtlHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenOrderDtlHistory.Get(IBBmsCenOrderDtlHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenOrderDtlHistory.Entity);
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
        //Update  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            IBBmsCenOrderDtlHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDtlHistory.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistoryByField(BmsCenOrderDtlHistory entity, string fields)
        {
            IBBmsCenOrderDtlHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenOrderDtlHistory.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenOrderDtlHistory.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenOrderDtlHistory.Edit();
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
        //Delele  BmsCenOrderDtlHistory
        public BaseResultBool ST_UDTO_DelBmsCenOrderDtlHistory(long longBmsCenOrderDtlHistoryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenOrderDtlHistory.Remove(longBmsCenOrderDtlHistoryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity)
        {
            IBBmsCenOrderDtlHistory.Entity = entity;
            EntityList<BmsCenOrderDtlHistory> tempEntityList = new EntityList<BmsCenOrderDtlHistory>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenOrderDtlHistory.Search();
                tempEntityList.count = IBBmsCenOrderDtlHistory.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDtlHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenOrderDtlHistory> tempEntityList = new EntityList<BmsCenOrderDtlHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenOrderDtlHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenOrderDtlHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenOrderDtlHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenOrderDtlHistory.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenOrderDtlHistory>(tempEntity);
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


        #region BmsCenSaleDocHistory
        //Add  BmsCenSaleDocHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            IBBmsCenSaleDocHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenSaleDocHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenSaleDocHistory.Get(IBBmsCenSaleDocHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenSaleDocHistory.Entity);
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
        //Update  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            IBBmsCenSaleDocHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDocHistory.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistoryByField(BmsCenSaleDocHistory entity, string fields)
        {
            IBBmsCenSaleDocHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDocHistory.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenSaleDocHistory.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenSaleDocHistory.Edit();
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
        //Delele  BmsCenSaleDocHistory
        public BaseResultBool ST_UDTO_DelBmsCenSaleDocHistory(long longBmsCenSaleDocHistoryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDocHistory.Remove(longBmsCenSaleDocHistoryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistory(BmsCenSaleDocHistory entity)
        {
            IBBmsCenSaleDocHistory.Entity = entity;
            EntityList<BmsCenSaleDocHistory> tempEntityList = new EntityList<BmsCenSaleDocHistory>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenSaleDocHistory.Search();
                tempEntityList.count = IBBmsCenSaleDocHistory.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDocHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDocHistory> tempEntityList = new EntityList<BmsCenSaleDocHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenSaleDocHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenSaleDocHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDocHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenSaleDocHistory.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenSaleDocHistory>(tempEntity);
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


        #region BmsCenSaleDtlHistory
        //Add  BmsCenSaleDtlHistory
        public BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            IBBmsCenSaleDtlHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsCenSaleDtlHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsCenSaleDtlHistory.Get(IBBmsCenSaleDtlHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsCenSaleDtlHistory.Entity);
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
        //Update  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            IBBmsCenSaleDtlHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDtlHistory.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistoryByField(BmsCenSaleDtlHistory entity, string fields)
        {
            IBBmsCenSaleDtlHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlHistory.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsCenSaleDtlHistory.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsCenSaleDtlHistory.Edit();
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
        //Delele  BmsCenSaleDtlHistory
        public BaseResultBool ST_UDTO_DelBmsCenSaleDtlHistory(long longBmsCenSaleDtlHistoryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsCenSaleDtlHistory.Remove(longBmsCenSaleDtlHistoryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity)
        {
            IBBmsCenSaleDtlHistory.Entity = entity;
            EntityList<BmsCenSaleDtlHistory> tempEntityList = new EntityList<BmsCenSaleDtlHistory>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsCenSaleDtlHistory.Search();
                tempEntityList.count = IBBmsCenSaleDtlHistory.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtlHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsCenSaleDtlHistory> tempEntityList = new EntityList<BmsCenSaleDtlHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsCenSaleDtlHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsCenSaleDtlHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsCenSaleDtlHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsCenSaleDtlHistory.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsCenSaleDtlHistory>(tempEntity);
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


        #region CenQtyDtlTemp
        //Add  CenQtyDtlTemp
        public BaseResultDataValue ST_UDTO_AddCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            IBCenQtyDtlTemp.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenQtyDtlTemp.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenQtyDtlTemp.Get(IBCenQtyDtlTemp.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenQtyDtlTemp.Entity);
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
        //Update  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            IBCenQtyDtlTemp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtlTemp.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempByField(CenQtyDtlTemp entity, string fields)
        {
            IBCenQtyDtlTemp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtlTemp.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenQtyDtlTemp.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenQtyDtlTemp.Edit();
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
        //Delele  CenQtyDtlTemp
        public BaseResultBool ST_UDTO_DelCenQtyDtlTemp(long longCenQtyDtlTempID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtlTemp.Remove(longCenQtyDtlTempID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTemp(CenQtyDtlTemp entity)
        {
            IBCenQtyDtlTemp.Entity = entity;
            EntityList<CenQtyDtlTemp> tempEntityList = new EntityList<CenQtyDtlTemp>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenQtyDtlTemp.Search();
                tempEntityList.count = IBCenQtyDtlTemp.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtlTemp>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtlTemp> tempEntityList = new EntityList<CenQtyDtlTemp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenQtyDtlTemp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenQtyDtlTemp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtlTemp>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenQtyDtlTemp.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenQtyDtlTemp>(tempEntity);
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


        #region CenQtyDtlTempHistory
        //Add  CenQtyDtlTempHistory
        public BaseResultDataValue ST_UDTO_AddCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            IBCenQtyDtlTempHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCenQtyDtlTempHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCenQtyDtlTempHistory.Get(IBCenQtyDtlTempHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenQtyDtlTempHistory.Entity);
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
        //Update  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            IBCenQtyDtlTempHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtlTempHistory.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistoryByField(CenQtyDtlTempHistory entity, string fields)
        {
            IBCenQtyDtlTempHistory.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenQtyDtlTempHistory.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCenQtyDtlTempHistory.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCenQtyDtlTempHistory.Edit();
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
        //Delele  CenQtyDtlTempHistory
        public BaseResultBool ST_UDTO_DelCenQtyDtlTempHistory(long longCenQtyDtlTempHistoryID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCenQtyDtlTempHistory.Remove(longCenQtyDtlTempHistoryID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistory(CenQtyDtlTempHistory entity)
        {
            IBCenQtyDtlTempHistory.Entity = entity;
            EntityList<CenQtyDtlTempHistory> tempEntityList = new EntityList<CenQtyDtlTempHistory>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCenQtyDtlTempHistory.Search();
                tempEntityList.count = IBCenQtyDtlTempHistory.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtlTempHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CenQtyDtlTempHistory> tempEntityList = new EntityList<CenQtyDtlTempHistory>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCenQtyDtlTempHistory.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCenQtyDtlTempHistory.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CenQtyDtlTempHistory>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCenQtyDtlTempHistory.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CenQtyDtlTempHistory>(tempEntity);
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


        #region TestEquipLab
        //Add  TestEquipLab
        public BaseResultDataValue ST_UDTO_AddTestEquipLab(TestEquipLab entity)
        {
            entity.DataAddTime = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipLab.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBTestEquipLab.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBTestEquipLab.Get(IBTestEquipLab.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBTestEquipLab.Entity);
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
        //Update  TestEquipLab
        public BaseResultBool ST_UDTO_UpdateTestEquipLab(TestEquipLab entity)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipLab.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipLab.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  TestEquipLab
        public BaseResultBool ST_UDTO_UpdateTestEquipLabByField(TestEquipLab entity, string fields)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipLab.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipLab.Entity, fields + ",DataUpdateTime");
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBTestEquipLab.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBTestEquipLab.Edit();
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
        //Delele  TestEquipLab
        public BaseResultBool ST_UDTO_DelTestEquipLab(long longTestEquipLabID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipLab.Remove(longTestEquipLabID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchTestEquipLab(TestEquipLab entity)
        {
            IBTestEquipLab.Entity = entity;
            EntityList<TestEquipLab> tempEntityList = new EntityList<TestEquipLab>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBTestEquipLab.Search();
                tempEntityList.count = IBTestEquipLab.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipLab>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipLabByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipLab> tempEntityList = new EntityList<TestEquipLab>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBTestEquipLab.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBTestEquipLab.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipLab>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipLabById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBTestEquipLab.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<TestEquipLab>(tempEntity);
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


        #region TestEquipProd
        //Add  TestEquipProd
        public BaseResultDataValue ST_UDTO_AddTestEquipProd(TestEquipProd entity)
        {
            entity.DataAddTime = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipProd.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBTestEquipProd.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBTestEquipProd.Get(IBTestEquipProd.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBTestEquipProd.Entity);
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
        //Update  TestEquipProd
        public BaseResultBool ST_UDTO_UpdateTestEquipProd(TestEquipProd entity)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipProd.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipProd.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  TestEquipProd
        public BaseResultBool ST_UDTO_UpdateTestEquipProdByField(TestEquipProd entity, string fields)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipProd.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipProd.Entity, fields + ",DataUpdateTime");
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBTestEquipProd.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBTestEquipProd.Edit();
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
        //Delele  TestEquipProd
        public BaseResultBool ST_UDTO_DelTestEquipProd(long longTestEquipProdID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipProd.Remove(longTestEquipProdID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchTestEquipProd(TestEquipProd entity)
        {
            IBTestEquipProd.Entity = entity;
            EntityList<TestEquipProd> tempEntityList = new EntityList<TestEquipProd>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBTestEquipProd.Search();
                tempEntityList.count = IBTestEquipProd.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipProd>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipProdByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipProd> tempEntityList = new EntityList<TestEquipProd>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBTestEquipProd.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBTestEquipProd.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipProd>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipProdById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBTestEquipProd.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<TestEquipProd>(tempEntity);
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


        #region TestEquipType
        //Add  TestEquipType
        public BaseResultDataValue ST_UDTO_AddTestEquipType(TestEquipType entity)
        {
            entity.DataAddTime = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBTestEquipType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBTestEquipType.Get(IBTestEquipType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBTestEquipType.Entity);
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
        //Update  TestEquipType
        public BaseResultBool ST_UDTO_UpdateTestEquipType(TestEquipType entity)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  TestEquipType
        public BaseResultBool ST_UDTO_UpdateTestEquipTypeByField(TestEquipType entity, string fields)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBTestEquipType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBTestEquipType.Entity, fields + ",DataUpdateTime");
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBTestEquipType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBTestEquipType.Edit();
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
        //Delele  TestEquipType
        public BaseResultBool ST_UDTO_DelTestEquipType(long longTestEquipTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBTestEquipType.Remove(longTestEquipTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchTestEquipType(TestEquipType entity)
        {
            IBTestEquipType.Entity = entity;
            EntityList<TestEquipType> tempEntityList = new EntityList<TestEquipType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBTestEquipType.Search();
                tempEntityList.count = IBTestEquipType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<TestEquipType> tempEntityList = new EntityList<TestEquipType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBTestEquipType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBTestEquipType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<TestEquipType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchTestEquipTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBTestEquipType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<TestEquipType>(tempEntity);
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


        #region FFeedback
        //Add  FFeedback
        public BaseResultDataValue ST_UDTO_AddFFeedback(FFeedback entity)
        {
            IBFFeedback.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFeedback.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFeedback.Get(IBFFeedback.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFeedback.Entity);
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
        //Update  FFeedback
        public BaseResultBool ST_UDTO_UpdateFFeedback(FFeedback entity)
        {
            IBFFeedback.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFeedback.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFeedback
        public BaseResultBool ST_UDTO_UpdateFFeedbackByField(FFeedback entity, string fields)
        {
            IBFFeedback.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFeedback.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFeedback.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFeedback.Edit();
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
        //Delele  FFeedback
        public BaseResultBool ST_UDTO_DelFFeedback(long longFFeedbackID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFeedback.Remove(longFFeedbackID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchFFeedback(FFeedback entity)
        {
            IBFFeedback.Entity = entity;
            EntityList<FFeedback> tempEntityList = new EntityList<FFeedback>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFeedback.Search();
                tempEntityList.count = IBFFeedback.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFeedback>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchFFeedbackByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFeedback> tempEntityList = new EntityList<FFeedback>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFeedback.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFeedback.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFeedback>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchFFeedbackById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFeedback.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFeedback>(tempEntity);
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


        #region BmsAccountInput
        public BaseResultDataValue ST_UDTO_AddBmsAccountInputAndDtList(BmsAccountInput entity, string saleDocIDStr)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "entity信息为空!";
                return tempBaseResultDataValue;
            }
            if (entity.Lab == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "机构信息为空!";
                return tempBaseResultDataValue;
            }
            if (String.IsNullOrEmpty(saleDocIDStr))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "待入帐供货单信息为空!";
                return tempBaseResultDataValue;
            }
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                return tempBaseResultDataValue;
            }
            string employeeCName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!entity.UserID.HasValue) entity.UserID = long.Parse(employeeID);
            if (String.IsNullOrEmpty(entity.UserName)) entity.UserName = employeeCName;
            //if (String.IsNullOrEmpty(entity.LabName)) entity.LabName = SessionHelper.GetSessionValue(DicCookieSession.LabName);
            IBBmsAccountInput.Entity = entity;
            try
            {
                tempBaseResultDataValue = IBBmsAccountInput.AddBmsAccountInputAndDtList(saleDocIDStr);
                if (tempBaseResultDataValue.success)
                {
                    IBBmsAccountInput.Get(IBBmsAccountInput.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsAccountInput.Entity);
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
        public BaseResultBool ST_UDTO_DeleteBmsAccountInputAndDtList(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBBmsAccountInput.DeleteBmsAccountInputAndDtList(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Add  BmsAccountInput
        public BaseResultDataValue ST_UDTO_AddBmsAccountInput(BmsAccountInput entity)
        {
            IBBmsAccountInput.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsAccountInput.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsAccountInput.Get(IBBmsAccountInput.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsAccountInput.Entity);
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
        //Update  BmsAccountInput
        public BaseResultBool ST_UDTO_UpdateBmsAccountInput(BmsAccountInput entity)
        {
            IBBmsAccountInput.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsAccountInput.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsAccountInput
        public BaseResultBool ST_UDTO_UpdateBmsAccountInputByField(BmsAccountInput entity, string fields)
        {
            IBBmsAccountInput.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsAccountInput.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsAccountInput.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsAccountInput.Edit();
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
        //Delele  BmsAccountInput
        public BaseResultBool ST_UDTO_DelBmsAccountInput(long longBmsAccountInputID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsAccountInput.Remove(longBmsAccountInputID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInput(BmsAccountInput entity)
        {
            IBBmsAccountInput.Entity = entity;
            EntityList<BmsAccountInput> tempEntityList = new EntityList<BmsAccountInput>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsAccountInput.Search();
                tempEntityList.count = IBBmsAccountInput.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsAccountInput>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInputByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsAccountInput> tempEntityList = new EntityList<BmsAccountInput>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsAccountInput.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsAccountInput.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsAccountInput>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountInputById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsAccountInput.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsAccountInput>(tempEntity);
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


        #region BmsAccountSaleDoc
        //Add  BmsAccountSaleDoc
        public BaseResultDataValue ST_UDTO_AddBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            IBBmsAccountSaleDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBmsAccountSaleDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBmsAccountSaleDoc.Get(IBBmsAccountSaleDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBmsAccountSaleDoc.Entity);
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
        //Update  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            IBBmsAccountSaleDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsAccountSaleDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_UpdateBmsAccountSaleDocByField(BmsAccountSaleDoc entity, string fields)
        {
            IBBmsAccountSaleDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBmsAccountSaleDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBmsAccountSaleDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBmsAccountSaleDoc.Edit();
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
        //Delele  BmsAccountSaleDoc
        public BaseResultBool ST_UDTO_DelBmsAccountSaleDoc(long longBmsAccountSaleDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBmsAccountSaleDoc.Remove(longBmsAccountSaleDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDoc(BmsAccountSaleDoc entity)
        {
            IBBmsAccountSaleDoc.Entity = entity;
            EntityList<BmsAccountSaleDoc> tempEntityList = new EntityList<BmsAccountSaleDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBmsAccountSaleDoc.Search();
                tempEntityList.count = IBBmsAccountSaleDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsAccountSaleDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BmsAccountSaleDoc> tempEntityList = new EntityList<BmsAccountSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBmsAccountSaleDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBmsAccountSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BmsAccountSaleDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBmsAccountSaleDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BmsAccountSaleDoc>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_CenOrgUploadExcelData()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "false";
                    brdv.success = false;
                    return brdv;
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
                    //brdv.ResultDataValue = filepath;
                    ExcelHelper eh = new ExcelHelper(filepath, "YES");
                    DataSet set = eh.GetExcelDataSet(null);
                    if (set != null && set.Tables.Count > 0 && set.Tables[0].Rows.Count >= 1)
                    {
                        DataTable dt = set.Tables[0];
                        string errorinfo;
                        bool flag = IBCenOrg.ExcelSave(dt, out errorinfo);
                        if (flag)
                        {
                            brdv.success = true;
                            return brdv;
                        }
                        else
                        {
                            brdv.ErrorInfo = "Excel文件数据导入错误！ErrorInfo：" + errorinfo;
                            brdv.success = false;
                            return brdv;
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "Excel文件数据错误！";
                        brdv.success = false;
                        return brdv;
                    }
                }
                else
                {
                    brdv.ErrorInfo = "文件大小为0或为空！";
                    brdv.success = false;
                    return brdv;
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message;
                brdv.ResultDataValue = "";
                brdv.success = false;
                return brdv;
            }
        }

        #region SCInteraction
        //Add  SCInteraction
        public BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCInteraction.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCInteraction.Get(IBSCInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCInteraction.Entity);
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
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCInteraction.Edit();
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
        //Delele  SCInteraction
        public BaseResultBool SC_UDTO_DelSCInteraction(long longSCInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCInteraction.Remove(longSCInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCInteraction.Search();
                tempEntityList.count = IBSCInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCInteraction>(tempEntityList);
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
        //查询公共交流表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCInteraction>(tempEntityList);
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

        public BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCInteraction>(tempEntity);
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


        #region GoodsQualification
        public Stream ST_UDTO_AddGoodsQualificationAndUploadRegisterFile()
        {
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
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
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "登录人信息为空!请登录后再操作!";
            }
            if (tempBaseResultBool.success && String.IsNullOrEmpty(hrdeptID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "登录人机构信息为空!请登录后再操作!";
            }
            if (tempBaseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity为空!";
            }
            if (tempBaseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增资质证件信息出错:" + tempBaseResultBool.ErrorInfo);
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<GoodsQualification>(entityStr);

                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "新增资质证件信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增资质证件信息序列化出错:" + ex.Message);
                }
            }

            if (tempBaseResultBool.success)
            {
                if (file != null && !String.IsNullOrEmpty(file.FileName))
                {
                    entity.FileType = file.ContentType;
                    entity.FileName = file.FileName;
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    entity.FileExt = fileExt;
                }
                IBGoodsQualification.Entity = entity;
                try
                {
                    if (String.IsNullOrEmpty(hrdeptCode))
                        hrdeptCode = "";
                    tempBaseResultBool = IBGoodsQualification.AddGoodsQualificationAndUploadRegisterFile(file, hrdeptID, hrdeptCode);
                    if (tempBaseResultBool.success)
                    {
                        IBGoodsQualification.Get(IBGoodsQualification.Entity.Id);
                        tempBaseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoodsQualification.Entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增资质证件信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
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
                //        tempBaseResultBool.success = false;
                //        tempBaseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                //        ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                //        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
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
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (tempBaseResultBool.success)
            {
                if (file != null&& !String.IsNullOrEmpty(file.FileName))
                {
                    entity.FileType = file.ContentType;
                    entity.FileName = file.FileName;
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    entity.FileExt = fileExt;
                    if (!fields.Contains("FileType")) fields += ",FileType,FileName,FileExt";
                }
                IBGoodsQualification.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsQualification.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool = IBGoodsQualification.UpdateGoodsQualificationAndUploadRegisterFileByField(tempArray, file);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新资质证件信息出错:" + tempBaseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新资质证件信息出错:" + tempBaseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新资质证件信息出错1:" + tempBaseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
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
            IBGoodsQualification.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBGoodsQualification.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBGoodsQualification.Get(IBGoodsQualification.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBGoodsQualification.Entity);
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
        //Update  GoodsQualification
        public BaseResultBool ST_UDTO_UpdateGoodsQualification(GoodsQualification entity)
        {
            IBGoodsQualification.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGoodsQualification.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  GoodsQualification
        public BaseResultBool ST_UDTO_UpdateGoodsQualificationByField(GoodsQualification entity, string fields)
        {
            IBGoodsQualification.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGoodsQualification.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBGoodsQualification.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBGoodsQualification.Edit();
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
        //Delele  GoodsQualification
        public BaseResultBool ST_UDTO_DelGoodsQualification(long longGoodsQualificationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBGoodsQualification.Remove(longGoodsQualificationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchGoodsQualification(GoodsQualification entity)
        {
            IBGoodsQualification.Entity = entity;
            EntityList<GoodsQualification> tempEntityList = new EntityList<GoodsQualification>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBGoodsQualification.Search();
                tempEntityList.count = IBGoodsQualification.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GoodsQualification>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchGoodsQualificationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<GoodsQualification> tempEntityList = new EntityList<GoodsQualification>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBGoodsQualification.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBGoodsQualification.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<GoodsQualification>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchGoodsQualificationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBGoodsQualification.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<GoodsQualification>(tempEntity);
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

    }
}
