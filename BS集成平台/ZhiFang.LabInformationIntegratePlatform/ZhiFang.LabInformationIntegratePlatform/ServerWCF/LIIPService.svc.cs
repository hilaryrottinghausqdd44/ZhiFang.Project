using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{
    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LIIPService: ServerContract.ILIIPService
    {
        IBIntergrateSystemSet IBIntergrateSystemSet { get; set; }

        #region IntergrateSystemSet_集成系统设置
        //Add  IntergrateSystemSet
        public BaseResultDataValue ST_UDTO_AddIntergrateSystemSet(IntergrateSystemSet entity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (entity != null)
                {
                    entity.DataAddTime = DateTime.Now;
                    //entity.DataUpdateTime = DateTime.Now;
                    IBIntergrateSystemSet.Entity = entity;

                    brdv.success = IBIntergrateSystemSet.Add();
                    if (brdv.success)
                    {
                        brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    }
                }

                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ST_UDTO_AddIntergrateSystemSet.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
        }
        //Update  IntergrateSystemSet
        //public BaseResultBool ST_UDTO_UpdateIntergrateSystemSet(IntergrateSystemSet entity)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        entity.DataUpdateTime = DateTime.Now;
        //        IBIntergrateSystemSet.Entity = entity;
        //        try
        //        {
        //            baseResultBool.success = IBIntergrateSystemSet.Edit();
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //    return baseResultBool;
        //}
        //Update  IntergrateSystemSet
        public BaseResultBool ST_UDTO_UpdateIntergrateSystemSetByField(IntergrateSystemSet entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                //if (!string.IsNullOrEmpty(fields))
                    //fields = fields + ",DataUpdateTime";
                //entity.DataUpdateTime = DateTime.Now;
                IBIntergrateSystemSet.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBIntergrateSystemSet.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBIntergrateSystemSet.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBIntergrateSystemSet.Edit();
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
        //Delele  IntergrateSystemSet
        public BaseResultBool ST_UDTO_DelIntergrateSystemSet(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBIntergrateSystemSet.Entity = IBIntergrateSystemSet.Get(id);
                if (IBIntergrateSystemSet.Entity != null)
                {
                    long labid = IBIntergrateSystemSet.Entity.LabID;
                    string entityName = IBIntergrateSystemSet.Entity.GetType().Name;
                    baseResultBool.success = IBIntergrateSystemSet.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
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

        public BaseResultDataValue ST_UDTO_SearchIntergrateSystemSet(IntergrateSystemSet entity)
        {
            EntityList<IntergrateSystemSet> entityList = new EntityList<IntergrateSystemSet>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBIntergrateSystemSet.Entity = entity;
                try
                {
                    entityList.list = IBIntergrateSystemSet.Search();
                    entityList.count = IBIntergrateSystemSet.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<IntergrateSystemSet>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchIntergrateSystemSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<IntergrateSystemSet> entityList = new EntityList<IntergrateSystemSet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBIntergrateSystemSet.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBIntergrateSystemSet.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<IntergrateSystemSet>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchIntergrateSystemSetById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBIntergrateSystemSet.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<IntergrateSystemSet>(entity);
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
    }
}
