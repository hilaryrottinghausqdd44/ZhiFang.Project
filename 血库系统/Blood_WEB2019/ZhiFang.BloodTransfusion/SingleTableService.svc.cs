using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BloodTransfusion
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SingleTableService : SingleTableServiceCommon, ZhiFang.BloodTransfusion.ServerContract.ISingleTableService
    {
        public virtual IBBParameter IBBParameter { get; set; }
        public virtual IBBObjectOperate IBBObjectOperate { get; set; }
        public virtual IBBObjectOperateType IBBObjectOperateType { get; set; }
        public virtual IBBDict IBBDict { get; set; }
        public virtual IBBDictType IBBDictType { get; set; }
        public virtual IBBDictTree IBBDictTree { get; set; }

        #region BParameter
        public BaseResultDataValue ST_UDTO_AddBParameterByParaNo(BParameter entity)
        {
            IBBParameter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<BParameter> list = IBBParameter.SearchListByParaNo(entity.ParaNo);
            if (list.Count > 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "系统编码有重复,请修改后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultDataValue.success = IBBParameter.AddAndSetCache();
                    if (tempBaseResultDataValue.success)
                    {
                        IBBParameter.Get(IBBParameter.Entity.Id);
                        tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBParameter.Entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateBParameterByParaNoAndField(BParameter entity, string fields)
        {
            IBBParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<BParameter> list = IBBParameter.SearchListByParaNo(IBBParameter.Entity.ParaNo);
            if (list.Count > 0 && list[0].Id != entity.Id)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "系统编码有重复,请修改后再操作!";
            }
            else
            {
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBParameter.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool.success = IBBParameter.UpdateAndSetCache(tempArray);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //tempBaseResultBool.success = IBBParameter.Edit();
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        //Add  BParameter
        public BaseResultDataValue ST_UDTO_AddBParameter(BParameter entity)
        {
            IBBParameter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBParameter.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBParameter.Get(IBBParameter.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBParameter.Entity);
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
        //Update  BParameter
        public BaseResultBool ST_UDTO_UpdateBParameter(BParameter entity)
        {
            IBBParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBParameter.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BParameter
        public BaseResultBool ST_UDTO_UpdateBParameterByField(BParameter entity, string fields)
        {
            IBBParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBParameter.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBParameter.Update(tempArray);
                        if (tempBaseResultBool.success && !string.IsNullOrEmpty(entity.ParaNo))
                        {
                            BParameterCache.SetCache(entity.ParaNo, entity.ParaValue);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBParameter.Edit();
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
        //Delele  BParameter
        public BaseResultBool ST_UDTO_DelBParameter(long longBParameterID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBParameter.Remove(longBParameterID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBParameter(BParameter entity)
        {
            IBBParameter.Entity = entity;
            EntityList<BParameter> tempEntityList = new EntityList<BParameter>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBParameter.Search();
                tempEntityList.count = IBBParameter.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BParameter>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BParameter> tempEntityList = new EntityList<BParameter>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBParameter.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBParameter.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BParameter>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBParameterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBParameter.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BParameter>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_SearchBParameterByByParaNo(string paraNo)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            if (string.IsNullOrEmpty(paraNo))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "参数(paraNo)值为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                string fields = "BParameter_ParaValue";
                //long labID = 0;
                //string labIDStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                //if (string.IsNullOrEmpty(labIDStr)) labID = long.Parse(labIDStr);

                var tempEntity = IBBParameter.GetParameterByParaNo(paraNo);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish<BParameter>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_SearchBParameterOfUserSetByHQL(string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempBaseResultDataValue = IBBParameter.SearchBParameterOfUserSetByHQL(where, sort);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("BT_UDTO_SearchBParameterOfUserSetByHQL:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateBParameterListByBatch(IList<BParameter> entityList)
        {
            //IBBParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entityList == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误：传入参数entityList为空!";
                return tempBaseResultBool;
            }
            try
            {
                IList<BParameter> editEntityList = new List<BParameter>();
                tempBaseResultBool = IBBParameter.EditBParameterListByBatch(entityList, ref editEntityList);
                if (tempBaseResultBool.success)
                {
                    //ZhiFang.Common.Log.Log.Debug("集成平台服务访问URL.Key" + SYSParaNo.集成平台服务访问URL.Key);
                    foreach (var item in editEntityList)
                    {
                        ZhiFang.Common.Log.Log.Debug("更新运行参数缓存:ParaNo:" + item.ParaNo + ",ParaValue:" + item.ParaValue);
                        BParameterCache.SetCache(item.ParaNo, item.ParaValue);
                    }
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

        #endregion

        #region BObjectOperate
        //Add  BObjectOperate
        public BaseResultDataValue ST_UDTO_AddBObjectOperate(BObjectOperate entity)
        {
            IBBObjectOperate.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBObjectOperate.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBObjectOperate.Get(IBBObjectOperate.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBObjectOperate.Entity);
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
        //Update  BObjectOperate
        public BaseResultBool ST_UDTO_UpdateBObjectOperate(BObjectOperate entity)
        {
            IBBObjectOperate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBObjectOperate.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BObjectOperate
        public BaseResultBool ST_UDTO_UpdateBObjectOperateByField(BObjectOperate entity, string fields)
        {
            IBBObjectOperate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBObjectOperate.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBObjectOperate.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBObjectOperate.Edit();
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
        //Delele  BObjectOperate
        public BaseResultBool ST_UDTO_DelBObjectOperate(long longBObjectOperateID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBObjectOperate.Remove(longBObjectOperateID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBObjectOperate(BObjectOperate entity)
        {
            IBBObjectOperate.Entity = entity;
            EntityList<BObjectOperate> tempEntityList = new EntityList<BObjectOperate>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBObjectOperate.Search();
                tempEntityList.count = IBBObjectOperate.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BObjectOperate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBObjectOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BObjectOperate> tempEntityList = new EntityList<BObjectOperate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBObjectOperate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBObjectOperate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BObjectOperate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBObjectOperateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBObjectOperate.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BObjectOperate>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AddBObjectOperateGetId(BObjectOperate entity, string ObjectTypeShortCode, string OperateTypeShortCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBObjectOperate.AddByCode(entity, ObjectTypeShortCode, OperateTypeShortCode);
                if (tempBaseResultDataValue.success)
                {
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        #endregion

        #region BObjectOperateType
        //Add  BObjectOperateType
        public BaseResultDataValue ST_UDTO_AddBObjectOperateType(BObjectOperateType entity)
        {
            IBBObjectOperateType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBObjectOperateType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBObjectOperateType.Get(IBBObjectOperateType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBObjectOperateType.Entity);
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
        //Update  BObjectOperateType
        public BaseResultBool ST_UDTO_UpdateBObjectOperateType(BObjectOperateType entity)
        {
            IBBObjectOperateType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBObjectOperateType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BObjectOperateType
        public BaseResultBool ST_UDTO_UpdateBObjectOperateTypeByField(BObjectOperateType entity, string fields)
        {
            IBBObjectOperateType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBObjectOperateType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBObjectOperateType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBObjectOperateType.Edit();
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
        //Delele  BObjectOperateType
        public BaseResultBool ST_UDTO_DelBObjectOperateType(long longBObjectOperateTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBObjectOperateType.Remove(longBObjectOperateTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBObjectOperateType(BObjectOperateType entity)
        {
            IBBObjectOperateType.Entity = entity;
            EntityList<BObjectOperateType> tempEntityList = new EntityList<BObjectOperateType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBObjectOperateType.Search();
                tempEntityList.count = IBBObjectOperateType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BObjectOperateType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBObjectOperateTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BObjectOperateType> tempEntityList = new EntityList<BObjectOperateType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBObjectOperateType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBObjectOperateType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BObjectOperateType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBObjectOperateTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBObjectOperateType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BObjectOperateType>(tempEntity);
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

        #region BDictType
        //Add  BDictType
        public BaseResultDataValue ST_UDTO_AddBDictType(BDictType entity)
        {
            IBBDictType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBDictType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBDictType.Get(IBBDictType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBDictType.Entity);
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
        //Update  BDictType
        public BaseResultBool ST_UDTO_UpdateBDictType(BDictType entity)
        {
            IBBDictType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BDictType
        public BaseResultBool ST_UDTO_UpdateBDictTypeByField(BDictType entity, string fields)
        {
            IBBDictType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBDictType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBDictType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBDictType.Edit();
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
        //Delele  BDictType
        public BaseResultBool ST_UDTO_DelBDictType(long longBDictTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictType.Remove(longBDictTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBDictType(BDictType entity)
        {
            IBBDictType.Entity = entity;
            EntityList<BDictType> tempEntityList = new EntityList<BDictType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBDictType.Search();
                tempEntityList.count = IBBDictType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBDictTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDictType> tempEntityList = new EntityList<BDictType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBDictType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBDictType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictType>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBDictTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBDictType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDictType>(tempEntity);
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

        #region BDict
        //Add  BDict
        public BaseResultDataValue ST_UDTO_AddBDict(BDict entity)
        {
            IBBDict.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBDict.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBDict.Get(IBBDict.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBDict.Entity);
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
        //Update  BDict
        public BaseResultBool ST_UDTO_UpdateBDict(BDict entity)
        {
            IBBDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDict.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BDict
        public BaseResultBool ST_UDTO_UpdateBDictByField(BDict entity, string fields)
        {
            IBBDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBDict.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBDict.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBDict.Edit();
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
        //Delele  BDict
        public BaseResultBool ST_UDTO_DelBDict(long longBDictID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDict.Remove(longBDictID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBDict(BDict entity)
        {
            IBBDict.Entity = entity;
            EntityList<BDict> tempEntityList = new EntityList<BDict>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBDict.Search();
                tempEntityList.count = IBBDict.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDict>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDict> tempEntityList = new EntityList<BDict>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBDict.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBDict.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDict>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBDictById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBDict.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDict>(tempEntity);
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

        #region BDictTree
        //Add  BDictTree
        public BaseResultDataValue UDTO_AddBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBDictTree.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBDictTree.Get(IBBDictTree.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBDictTree.Entity);
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
        //Update  BDictTree
        public BaseResultBool UDTO_UpdateBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictTree.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BDictTree
        public BaseResultBool UDTO_UpdateBDictTreeByField(BDictTree entity, string fields)
        {
            IBBDictTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBDictTree.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBDictTree.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBDictTree.Edit();
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
        //Delele  BDictTree
        public BaseResultBool UDTO_DelBDictTree(long longBDictTreeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictTree.Remove(longBDictTreeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue UDTO_SearchBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBDictTree.Search();
                tempEntityList.count = IBBDictTree.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictTree>(tempEntityList);
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
        //查询类型树ByHQL
        public BaseResultDataValue UDTO_SearchBDictTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBDictTree.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBDictTree.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictTree>(tempEntityList);
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

        public BaseResultDataValue UDTO_SearchBDictTreeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBDictTree.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDictTree>(tempEntity);
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

        #region 文件类型树
        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="id">支持传入多个节点id值,如(1,2,4,5)</param>
        /// <param name="fields"></param>
        /// <param name="maxLevelStr">最大层数参数</param>
        /// <returns></returns>
        public BaseResultDataValue UDTO_SearchBDictTreeListTreeByIdListStr(string id, string idListStr, string fields, string maxLevelStr)
        {
            //BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //BaseResultTree<BDictTree> tempBaseResultTree = new BaseResultTree<BDictTree>();
            ////string idListStr = "0";
            //try
            //{

            //    tempBaseResultTree = IBBDictTree.SearchBDictTreeListTree(id, idListStr, maxLevelStr);
            //    if (tempBaseResultTree.Tree != null && tempBaseResultTree.Tree.Count > 0)
            //    {
            //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
            //        try
            //        {
            //            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
            //        }
            //        catch (Exception ex)
            //        {
            //            tempBaseResultDataValue.success = false;
            //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    //throw new Exception(ex.Message);
            //}

            ////ZhiFang.Common.Log.Log.Debug("ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
            //return tempBaseResultDataValue;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultTree<BDictTree> baseResultTree = new BaseResultTree<BDictTree>();
            try
            {
                baseResultTree = this.IBBDictTree.SearchBDictTreeListTree(id, idListStr, maxLevelStr);
                bool flag = baseResultTree.Tree != null && baseResultTree.Tree.Count > 0;
                if (flag)
                {
                    ParseObjectProperty parseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = parseObjectProperty.GetObjectPropertyNoPlanish<BaseResultTree<BDictTree>>(baseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    }
                }
            }
            catch (Exception ex2)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex2.Message;
            }
            return baseResultDataValue;
        }

        #endregion
        #endregion
    }
}
