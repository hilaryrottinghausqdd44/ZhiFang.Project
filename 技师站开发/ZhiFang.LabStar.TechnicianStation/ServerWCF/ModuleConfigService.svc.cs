using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.IBLL.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ModuleConfigService : IModuleConfigService
    {
        IBBModuleFormControlList IBBModuleFormControlList { get; set; }
        IBBModuleGridControlList IBBModuleGridControlList { get; set; }
        IBBModuleFormControlSet IBBModuleFormControlSet { get; set; }
        IBBModuleGridControlSet IBBModuleGridControlSet { get; set; }
        IBBModuleGridList IBBModuleGridList { get; set; }
        IBBModuleFormList IBBModuleFormList { get; set; }

        #region BModuleFormControlSet
        //Add  BModuleFormControlSet
        public BaseResultDataValue ST_UDTO_AddBModuleFormControlSet(BModuleFormControlSet entity)
        {
            IBBModuleFormControlSet.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBModuleFormControlSet.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBModuleFormControlSet.Get(IBBModuleFormControlSet.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleFormControlSet.Entity);
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
        //Update  BModuleFormControlSet
        public BaseResultBool ST_UDTO_UpdateBModuleFormControlSet(BModuleFormControlSet entity)
        {
            IBBModuleFormControlSet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormControlSet.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleFormControlSet
        public BaseResultBool ST_UDTO_UpdateBModuleFormControlSetByField(BModuleFormControlSet entity, string fields)
        {
            IBBModuleFormControlSet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleFormControlSet.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleFormControlSet.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleFormControlSet.Edit();
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
        //Delele  BModuleFormControlSet
        public BaseResultBool ST_UDTO_DelBModuleFormControlSet(long longBModuleFormControlSetID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormControlSet.Remove(longBModuleFormControlSetID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlSet(BModuleFormControlSet entity)
        {
            IBBModuleFormControlSet.Entity = entity;
            EntityList<BModuleFormControlSet> tempEntityList = new EntityList<BModuleFormControlSet>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleFormControlSet.Search();
                tempEntityList.count = IBBModuleFormControlSet.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormControlSet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleFormControlSet> tempEntityList = new EntityList<BModuleFormControlSet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleFormControlSet.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleFormControlSet.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormControlSet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlSetById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleFormControlSet.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleFormControlSet>(tempEntity);
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

        #region BModuleFormControlList
        //Add  BModuleFormControlList
        public BaseResultDataValue ST_UDTO_AddBModuleFormControlList(BModuleFormControlList entity)
        {
            IBBModuleFormControlList.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBModuleFormControlList.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBModuleFormControlList.Get(IBBModuleFormControlList.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleFormControlList.Entity);
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
        //Update  BModuleFormControlList
        public BaseResultBool ST_UDTO_UpdateBModuleFormControlList(BModuleFormControlList entity)
        {
            IBBModuleFormControlList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormControlList.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleFormControlList
        public BaseResultBool ST_UDTO_UpdateBModuleFormControlListByField(BModuleFormControlList entity, string fields)
        {
            IBBModuleFormControlList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleFormControlList.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleFormControlList.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleFormControlList.Edit();
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
        //Delele  BModuleFormControlList
        public BaseResultBool ST_UDTO_DelBModuleFormControlList(long longBModuleFormControlListID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormControlList.Remove(longBModuleFormControlListID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlList(BModuleFormControlList entity)
        {
            IBBModuleFormControlList.Entity = entity;
            EntityList<BModuleFormControlList> tempEntityList = new EntityList<BModuleFormControlList>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleFormControlList.Search();
                tempEntityList.count = IBBModuleFormControlList.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormControlList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleFormControlList> tempEntityList = new EntityList<BModuleFormControlList>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleFormControlList.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleFormControlList.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormControlList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormControlListById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleFormControlList.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleFormControlList>(tempEntity);
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

        #region BModuleGridList
        //Add  BModuleGridList
        public BaseResultDataValue ST_UDTO_AddBModuleGridList(BModuleGridList entity)
        {
            IBBModuleGridList.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var gridlist = IBBModuleGridList.SearchListByHQL("GridCode='" + entity.GridCode + "'");
                if (gridlist.Count == 0)
                {
                    tempBaseResultDataValue.success = IBBModuleGridList.Add();
                    if (tempBaseResultDataValue.success)
                    {
                        IBBModuleGridList.Get(IBBModuleGridList.Entity.Id);
                        tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleGridList.Entity);
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GridCode 已存在！";
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
        //Update  BModuleGridList
        public BaseResultBool ST_UDTO_UpdateBModuleGridList(BModuleGridList entity)
        {
            IBBModuleGridList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridList.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleGridList
        public BaseResultBool ST_UDTO_UpdateBModuleGridListByField(BModuleGridList entity, string fields)
        {
            IBBModuleGridList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleGridList.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleGridList.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleGridList.Edit();
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
        //Delele  BModuleGridList
        public BaseResultBool ST_UDTO_DelBModuleGridList(long longBModuleGridListID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridList.Remove(longBModuleGridListID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleGridList(BModuleGridList entity)
        {
            IBBModuleGridList.Entity = entity;
            EntityList<BModuleGridList> tempEntityList = new EntityList<BModuleGridList>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleGridList.Search();
                tempEntityList.count = IBBModuleGridList.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleGridList> tempEntityList = new EntityList<BModuleGridList>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleGridList.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleGridList.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridListById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleGridList.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleGridList>(tempEntity);
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

        #region BModuleGridControlSet
        //Add  BModuleGridControlSet
        public BaseResultDataValue ST_UDTO_AddBModuleGridControlSet(BModuleGridControlSet entity)
        {
            IBBModuleGridControlSet.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBModuleGridControlSet.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBModuleGridControlSet.Get(IBBModuleGridControlSet.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleGridControlSet.Entity);
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
        //Update  BModuleGridControlSet
        public BaseResultBool ST_UDTO_UpdateBModuleGridControlSet(BModuleGridControlSet entity)
        {
            IBBModuleGridControlSet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridControlSet.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleGridControlSet
        public BaseResultBool ST_UDTO_UpdateBModuleGridControlSetByField(BModuleGridControlSet entity, string fields)
        {
            IBBModuleGridControlSet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleGridControlSet.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleGridControlSet.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleGridControlSet.Edit();
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
        //Delele  BModuleGridControlSet
        public BaseResultBool ST_UDTO_DelBModuleGridControlSet(long longBModuleGridControlSetID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridControlSet.Remove(longBModuleGridControlSetID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlSet(BModuleGridControlSet entity)
        {
            IBBModuleGridControlSet.Entity = entity;
            EntityList<BModuleGridControlSet> tempEntityList = new EntityList<BModuleGridControlSet>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleGridControlSet.Search();
                tempEntityList.count = IBBModuleGridControlSet.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridControlSet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleGridControlSet> tempEntityList = new EntityList<BModuleGridControlSet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleGridControlSet.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleGridControlSet.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridControlSet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlSetById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleGridControlSet.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleGridControlSet>(tempEntity);
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

        #region BModuleGridControlList
        //Add  BModuleGridControlList
        public BaseResultDataValue ST_UDTO_AddBModuleGridControlList(BModuleGridControlList entity)
        {
            IBBModuleGridControlList.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBModuleGridControlList.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBModuleGridControlList.Get(IBBModuleGridControlList.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleGridControlList.Entity);
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
        //Update  BModuleGridControlList
        public BaseResultBool ST_UDTO_UpdateBModuleGridControlList(BModuleGridControlList entity)
        {
            IBBModuleGridControlList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridControlList.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleGridControlList
        public BaseResultBool ST_UDTO_UpdateBModuleGridControlListByField(BModuleGridControlList entity, string fields)
        {
            IBBModuleGridControlList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleGridControlList.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleGridControlList.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleGridControlList.Edit();
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
        //Delele  BModuleGridControlList
        public BaseResultBool ST_UDTO_DelBModuleGridControlList(long longBModuleGridControlListID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleGridControlList.Remove(longBModuleGridControlListID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlList(BModuleGridControlList entity)
        {
            IBBModuleGridControlList.Entity = entity;
            EntityList<BModuleGridControlList> tempEntityList = new EntityList<BModuleGridControlList>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleGridControlList.Search();
                tempEntityList.count = IBBModuleGridControlList.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridControlList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleGridControlList> tempEntityList = new EntityList<BModuleGridControlList>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleGridControlList.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleGridControlList.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridControlList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleGridControlListById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleGridControlList.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleGridControlList>(tempEntity);
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

        #region BModuleFormList
        //Add  BModuleFormList
        public BaseResultDataValue ST_UDTO_AddBModuleFormList(BModuleFormList entity)
        {
            IBBModuleFormList.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var formlist = IBBModuleFormList.SearchListByHQL("FormCode='" + entity.FormCode + "'");
                if (formlist.Count() == 0)
                {
                    tempBaseResultDataValue.success = IBBModuleFormList.Add();
                    if (tempBaseResultDataValue.success)
                    {
                        IBBModuleFormList.Get(IBBModuleFormList.Entity.Id);
                        tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBModuleFormList.Entity);
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "FormCode 已存在！";
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
        //Update  BModuleFormList
        public BaseResultBool ST_UDTO_UpdateBModuleFormList(BModuleFormList entity)
        {
            IBBModuleFormList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormList.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BModuleFormList
        public BaseResultBool ST_UDTO_UpdateBModuleFormListByField(BModuleFormList entity, string fields)
        {
            IBBModuleFormList.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBModuleFormList.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBModuleFormList.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBModuleFormList.Edit();
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
        //Delele  BModuleFormList
        public BaseResultBool ST_UDTO_DelBModuleFormList(long longBModuleFormListID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBModuleFormList.Remove(longBModuleFormListID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBModuleFormList(BModuleFormList entity)
        {
            IBBModuleFormList.Entity = entity;
            EntityList<BModuleFormList> tempEntityList = new EntityList<BModuleFormList>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBModuleFormList.Search();
                tempEntityList.count = IBBModuleFormList.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BModuleFormList> tempEntityList = new EntityList<BModuleFormList>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBModuleFormList.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBModuleFormList.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormList>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBModuleFormListById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBModuleFormList.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BModuleFormList>(tempEntity);
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

        #region 动态配置
        /// <summary>
        /// 获得表单配置项
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchBModuleFormControlSetListByFormCode(string FormCode, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<BModuleFormControlList> bModuleFormControlLists = IBBModuleFormControlSet.SearchBModuleFormControlSetListByFormCode(FormCode, CommonServiceMethod.GetSortHQL(sort));
                try
                {
                    EntityList<BModuleFormControlList> entityList = new EntityList<BModuleFormControlList>();
                    if (bModuleFormControlLists.Count > 0) {
                        entityList.list = bModuleFormControlLists;
                        entityList.count = bModuleFormControlLists.Count();
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleFormControlList>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList, fields);
                    }

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.SearchBModuleFormControlSetListByFormCode异常:" + ex.ToString());

            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获得列表配置项
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchBModuleGridControlListByGridCode(string GridCode, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<BModuleGridControlList> bModuleGrodControlLists = IBBModuleGridControlSet.SearchBModuleGridControlListByGridCode(GridCode, CommonServiceMethod.GetSortHQL(sort));
                try
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BModuleGridControlList>(bModuleGrodControlLists);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(bModuleGrodControlLists, fields);
                    }

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.SearchBModuleGridControlListByGridCode:" + ex.ToString());

            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 根据模块编码查询模块（如模块不存在则返回模块编码）
        /// </summary>
        /// <param name="GridCodes"></param>
        /// <param name="FormCodes"></param>
        /// <param name="CheartCodes"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchModuleAggregateList(string GridCodes, string FormCodes, string CheartCodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                SetModuleConfigDefault();
                DataTable dataTable = IBBModuleGridList.SearchModuleAggregateList(GridCodes, FormCodes, CheartCodes);
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.SearchModuleAggregateList:" + ex.ToString());
            }

            return baseResultDataValue;
        }
        /// <summary>
        /// 批量新增 表格配置
        /// </summary>
        /// <param name="BModuleGridControlSets"></param>
        /// <returns></returns>
        public BaseResultBool AddBModuleGridControlSets(List<BModuleGridControlSet> BModuleGridControlSets)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                BaseResultBool.success = IBBModuleGridControlSet.AddBModuleGridControlSets(BModuleGridControlSets);
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.AddBModuleGridControlSets:" + ex.ToString());
            }
            return BaseResultBool;
        }
        /// <summary>
        /// 批量新增 表单配置
        /// </summary>
        /// <param name="BModuleFormControlSets"></param>
        /// <returns></returns>
        public BaseResultBool AddBModuleFormControlSets(List<BModuleFormControlSet> BModuleFormControlSets)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                BaseResultBool.success = IBBModuleFormControlSet.AddBModuleFormControlSets(BModuleFormControlSets);
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.AddBModuleFormControlSets:" + ex.ToString());
            }
            return BaseResultBool;
        }
        /// <summary>
        /// 批量修改 表单配置
        /// </summary>
        /// <param name="BModuleFormControlSetVOs"></param>
        /// <returns></returns>
        public BaseResultBool EditBModuleFormControlSets(List<BModuleFormControlSetVO> BModuleFormControlSetVOs)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                BaseResultBool.success = IBBModuleFormControlSet.EditBModuleFormControlSets(BModuleFormControlSetVOs);
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.EditBModuleFormControlSets:" + ex.ToString());
            }
            return BaseResultBool;
        }
        /// <summary>
        /// 批量修改 表格配置
        /// </summary>
        /// <param name="BModuleFormControlSetVOs"></param>
        /// <returns></returns>
        public BaseResultBool EditBModuleGridControlSets(List<BModuleGridControlSetVO> BModuleGridControlSetVOs)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                BaseResultBool.success = IBBModuleGridControlSet.EditBModuleGridControlSets(BModuleGridControlSetVOs);
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.EditBModuleGridControlSets:" + ex.ToString());
            }
            return BaseResultBool;
        }
        /// <summary>
        /// 读取数据库配置项 生成默认配置文件
        /// </summary>
        /// <param name="key">固定键：ZhiFang.mODulEcONFigdefAulT_Admin_2021</param>
        /// <param name="type">AlL(生成所有配置),gRiD(表格配置),foRM(表单配置)</param>
        /// 通过工具请求时需要发送 cookie 900000=1，000100=labid
        /// <returns></returns>
        public BaseResultBool GetModuleConfigDefault(string key, string type)
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                if (key != "ZhiFang.mODulEcONFigdefAulT_Admin_2021")
                {
                    throw new Exception("ERROR:9001");
                }
                switch (type)
                {
                    case "AlL":
                        var gridflag = IBBModuleGridList.GetModuleGridConfigDefault();
                        var formflag = IBBModuleFormList.GetModuleFormConfigDefault();
                        if (gridflag && formflag)
                        {
                            BaseResultBool.success = true;
                        }
                        break;
                    case "gRiD":
                        BaseResultBool.success = IBBModuleGridList.GetModuleGridConfigDefault();
                        break;
                    case "foRM":
                        BaseResultBool.success = IBBModuleFormList.GetModuleFormConfigDefault();
                        break;
                }
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.GetModuleConfigDefault:" + ex.ToString());
            }
            return BaseResultBool;
        }
        public BaseResultBool SetModuleConfigDefault()
        {
            BaseResultBool BaseResultBool = new BaseResultBool();
            try
            {
                IBBModuleGridList.AddSetModuleGridConfigDefault();
                IBBModuleFormList.AddSetModuleFormConfigDefault();
            }
            catch (Exception ex)
            {
                BaseResultBool.success = false;
                BaseResultBool.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("ModuleConfigService.svc.SetModuleConfigDefault:" + ex.ToString());
            }
            return BaseResultBool;
        }
        /// <summary>
        /// 更新默认配置
        /// </summary>
        /// <returns></returns>
        public BaseResultBool UpdateModuleConfigDefault()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBModuleGridList.EditSetModuleGridConfigDefault();
                IBBModuleFormList.EditSetModuleFormConfigDefault();
                baseResultBool.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = ex.Message;
            }
            return baseResultBool;
        }

        #endregion


    }
}
