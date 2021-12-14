using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    //检验业务系统基础表服务及其相关的定制服务
    public class LabStarBaseTableService : ILabStarBaseTableService
    {
        #region
        ZhiFang.IBLL.LabStar.IBBHost IBBHost { get; set; }

        ZhiFang.IBLL.LabStar.IBBHostType IBBHostType { get; set; }

        ZhiFang.IBLL.LabStar.IBBHostTypeUser IBBHostTypeUser { get; set; }

        ZhiFang.IBLL.LabStar.IBBPara IBBPara { get; set; }

        ZhiFang.IBLL.LabStar.IBBParaItem IBBParaItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBChargeType IBLBChargeType { get; set; }

        ZhiFang.IBLL.LabStar.IBLBCollectPart IBLBCollectPart { get; set; }

        ZhiFang.IBLL.LabStar.IBLBDestination IBLBDestination { get; set; }

        ZhiFang.IBLL.LabStar.IBLBDiag IBLBDiag { get; set; }

        ZhiFang.IBLL.LabStar.IBLBDict IBLBDict { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquip IBLBEquip { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipResultTH IBLBEquipResultTH { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipSection IBLBEquipSection { get; set; }

        ZhiFang.IBLL.LabStar.IBLBExpertRule IBLBExpertRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBExpertRuleList IBLBExpertRuleList { get; set; }

        ZhiFang.IBLL.LabStar.IBLBFolk IBLBFolk { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItem IBLBItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemCharge IBLBItemCharge { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemCalc IBLBItemCalc { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemCalcFormula IBLBItemCalcFormula { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemComp IBLBItemComp { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemGroup IBLBItemGroup { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemRange IBLBItemRange { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemRangeExp IBLBItemRangeExp { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemTimeW IBLBItemTimeW { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemExp IBLBItemExp { get; set; }

        ZhiFang.IBLL.LabStar.IBLBPhrase IBLBPhrase { get; set; }

        ZhiFang.IBLL.LabStar.IBLBPhyPeriod IBLBPhyPeriod { get; set; }

        ZhiFang.IBLL.LabStar.IBLBOrderModel IBLBOrderModel { get; set; }

        ZhiFang.IBLL.LabStar.IBLBOrderModelItem IBLBOrderModelItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSampleType IBLBSampleType { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSection IBLBSection { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSectionItem IBLBSectionItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSectionPrint IBLBSectionPrint { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSectionHisComp IBLBSectionHisComp { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSickType IBLBSickType { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSpecialty IBLBSpecialty { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSectionHisComp IBLBSuperSection { get; set; }

        ZhiFang.IBLL.LabStar.IBLBParItemSplit IBLBParItemSplit { get; set; }

        ZhiFang.IBLL.LabStar.IBLBPhrasesWatch IBLBPhrasesWatch { get; set; }

        ZhiFang.IBLL.LabStar.IBLBPhrasesWatchClass IBLBPhrasesWatchClass { get; set; }

        ZhiFang.IBLL.LabStar.IBLBPhrasesWatchClassItem IBLBPhrasesWatchClassItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBRight IBLBRight { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDate IBLBReportDate { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateItem IBLBReportDateItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateRule IBLBReportDateRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSampleItem IBLBSampleItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSamplingChargeItem IBLBSamplingChargeItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSamplingGroup IBLBSamplingGroup { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSamplingItem IBLBSamplingItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBTcuvete IBLBTcuvete { get; set; }

        ZhiFang.IBLL.LabStar.IBLBTranRule IBLBTranRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBTranRuleItem IBLBTranRuleItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBTranRuleType IBLBTranRuleType { get; set; }

        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        ZhiFang.IBLL.LabStar.IBLBDicCodeLink IBLBDicCodeLink { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemCodeLink IBLBItemCodeLink { get; set; }
        ZhiFang.IBLL.LabStar.IBBPrintModel IBBPrintModel { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateDesc IBLBReportDateDesc { get; set; }
        ZhiFang.IBLL.LabStar.IBLBTGetMaxNo IBLBTGetMaxNo { get; set; }
        ZhiFang.IBLL.LabStar.IBLBTranRuleHostSection IBLBTranRuleHostSection { get; set; }
        ZhiFang.IBLL.LabStar.IBLisBarCodeRefuseRecord IBLisBarCodeRefuseRecord { get; set; }
        ZhiFang.IBLL.LabStar.IBLisQueue IBLisQueue { get; set; }
        #endregion

        #region BHost
        //Add  BHost
        public BaseResultDataValue LS_UDTO_AddBHost(BHost entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHost.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHost.Add();
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

        //Update  BHost
        public BaseResultBool LS_UDTO_UpdateBHostByField(BHost entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHost.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHost.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHost.Update(tempArray);
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
                        //baseResultBool.success = IBBHost.Edit();
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

        //Delele  BHost
        public BaseResultBool LS_UDTO_DelBHost(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHost.Entity = IBBHost.Get(id);
                if (IBBHost.Entity != null)
                {
                    long labid = IBBHost.Entity.LabID;
                    string entityName = IBBHost.Entity.GetType().Name;
                    baseResultBool.success = IBBHost.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBHostByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHost> entityList = new EntityList<BHost>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHost.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHost.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHost>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBHostById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHost.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHost>(entity);
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

        #region BHostType
        //Add  BHostType
        public BaseResultDataValue LS_UDTO_AddBHostType(BHostType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHostType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHostType.Add();
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

        //Update  BHostType
        public BaseResultBool LS_UDTO_UpdateBHostTypeByField(BHostType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHostType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHostType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHostType.Update(tempArray);
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
                        //baseResultBool.success = IBBHostType.Edit();
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

        //Delele  BHostType
        public BaseResultBool LS_UDTO_DelBHostType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHostType.Entity = IBBHostType.Get(id);
                if (IBBHostType.Entity != null)
                {
                    long labid = IBBHostType.Entity.LabID;
                    string entityName = IBBHostType.Entity.GetType().Name;
                    baseResultBool.success = IBBHostType.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBHostTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHostType> entityList = new EntityList<BHostType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHostType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHostType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHostType>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBHostTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHostType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHostType>(entity);
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

        #region BHostTypeUser
        //Add  BHostTypeUser
        public BaseResultDataValue LS_UDTO_AddBHostTypeUser(BHostTypeUser entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHostTypeUser.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHostTypeUser.Add();
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

        //Update  BHostTypeUser
        public BaseResultBool LS_UDTO_UpdateBHostTypeUserByField(BHostTypeUser entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHostTypeUser.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHostTypeUser.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHostTypeUser.Update(tempArray);
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
                        //baseResultBool.success = IBBHostTypeUser.Edit();
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
        //Delele  BHostTypeUser
        public BaseResultBool LS_UDTO_DelBHostTypeUser(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHostTypeUser.Entity = IBBHostTypeUser.Get(id);
                if (IBBHostTypeUser.Entity != null)
                {
                    long labid = IBBHostTypeUser.Entity.LabID;
                    string entityName = IBBHostTypeUser.Entity.GetType().Name;
                    baseResultBool.success = IBBHostTypeUser.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBHostTypeUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHostTypeUser> entityList = new EntityList<BHostTypeUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHostTypeUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHostTypeUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHostTypeUser>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBHostTypeUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHostTypeUser.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHostTypeUser>(entity);
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

        #region BPara
        //Add  BPara
        public BaseResultDataValue LS_UDTO_AddBPara(BPara entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBPara.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBPara.Add();
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

        //Update  BPara
        public BaseResultBool LS_UDTO_UpdateBParaByField(BPara entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBPara.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPara.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBPara.Update(tempArray);
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
                        //baseResultBool.success = IBBPara.Edit();
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
        //Delele  BPara
        public BaseResultBool LS_UDTO_DelBPara(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBPara.Entity = IBBPara.Get(id);
                if (IBBPara.Entity != null)
                {
                    long labid = IBBPara.Entity.LabID;
                    string entityName = IBBPara.Entity.GetType().Name;
                    baseResultBool.success = IBBPara.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBParaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BPara> entityList = new EntityList<BPara>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBPara.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBPara.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPara>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBParaById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBPara.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BPara>(entity);
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

        #region BParaItem
        //Add  BParaItem
        public BaseResultDataValue LS_UDTO_AddBParaItem(BParaItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBParaItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBParaItem.Add();
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

        //Update  BParaItem
        public BaseResultBool LS_UDTO_UpdateBParaItemByField(BParaItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBParaItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBParaItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBParaItem.Update(tempArray);
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
                        //baseResultBool.success = IBBParaItem.Edit();
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
        //Delele  BParaItem
        public BaseResultBool LS_UDTO_DelBParaItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBParaItem.Entity = IBBParaItem.Get(id);
                if (IBBParaItem.Entity != null)
                {
                    long labid = IBBParaItem.Entity.LabID;
                    string entityName = IBBParaItem.Entity.GetType().Name;
                    baseResultBool.success = IBBParaItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBParaItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BParaItem> entityList = new EntityList<BParaItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBParaItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBParaItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BParaItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBParaItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBParaItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BParaItem>(entity);
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

        #region LBChargeType
        //Add  LBChargeType
        public BaseResultDataValue LS_UDTO_AddLBChargeType(LBChargeType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBChargeType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBChargeType.Add();
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

        //Update  LBChargeType
        public BaseResultBool LS_UDTO_UpdateLBChargeTypeByField(LBChargeType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBChargeType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBChargeType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBChargeType.Update(tempArray);
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
                        //baseResultBool.success = IBLBChargeType.Edit();
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
        //Delele  LBChargeType
        public BaseResultBool LS_UDTO_DelLBChargeType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBChargeType.Entity = IBLBChargeType.Get(id);
                if (IBLBChargeType.Entity != null)
                {
                    long labid = IBLBChargeType.Entity.LabID;
                    string entityName = IBLBChargeType.Entity.GetType().Name;
                    baseResultBool.success = IBLBChargeType.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBChargeTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBChargeType> entityList = new EntityList<LBChargeType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBChargeType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBChargeType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBChargeType>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBChargeTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBChargeType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBChargeType>(entity);
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

        #region LBCollectPart
        //Add  LBCollectPart
        public BaseResultDataValue LS_UDTO_AddLBCollectPart(LBCollectPart entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBCollectPart.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBCollectPart.Add();
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

        //Update  LBCollectPart
        public BaseResultBool LS_UDTO_UpdateLBCollectPartByField(LBCollectPart entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBCollectPart.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBCollectPart.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBCollectPart.Update(tempArray);
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
                        //baseResultBool.success = IBLBCollectPart.Edit();
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
        //Delele  LBCollectPart
        public BaseResultBool LS_UDTO_DelLBCollectPart(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBCollectPart.Entity = IBLBCollectPart.Get(id);
                if (IBLBCollectPart.Entity != null)
                {
                    long labid = IBLBCollectPart.Entity.LabID;
                    string entityName = IBLBCollectPart.Entity.GetType().Name;
                    baseResultBool.success = IBLBCollectPart.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBCollectPartByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBCollectPart> entityList = new EntityList<LBCollectPart>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBCollectPart.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBCollectPart.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBCollectPart>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBCollectPartById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBCollectPart.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBCollectPart>(entity);
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

        #region LBDestination
        //Add  LBDestination
        public BaseResultDataValue LS_UDTO_AddLBDestination(LBDestination entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBDestination.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBDestination.Add();
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

        //Update  LBDestination
        public BaseResultBool LS_UDTO_UpdateLBDestinationByField(LBDestination entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBDestination.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBDestination.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBDestination.Update(tempArray);
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
                        //baseResultBool.success = IBLBDestination.Edit();
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
        //Delele  LBDestination
        public BaseResultBool LS_UDTO_DelLBDestination(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBDestination.Entity = IBLBDestination.Get(id);
                if (IBLBDestination.Entity != null)
                {
                    long labid = IBLBDestination.Entity.LabID;
                    string entityName = IBLBDestination.Entity.GetType().Name;
                    baseResultBool.success = IBLBDestination.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBDestinationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBDestination> entityList = new EntityList<LBDestination>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBDestination.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBDestination.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBDestination>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBDestinationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBDestination.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBDestination>(entity);
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

        #region LBDiag
        //Add  LBDiag
        public BaseResultDataValue LS_UDTO_AddLBDiag(LBDiag entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBDiag.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBDiag.Add();
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

        //Update  LBDiag
        public BaseResultBool LS_UDTO_UpdateLBDiagByField(LBDiag entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBDiag.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBDiag.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBDiag.Update(tempArray);
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
                        //baseResultBool.success = IBLBDiag.Edit();
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
        //Delele  LBDiag
        public BaseResultBool LS_UDTO_DelLBDiag(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBDiag.Entity = IBLBDiag.Get(id);
                if (IBLBDiag.Entity != null)
                {
                    long labid = IBLBDiag.Entity.LabID;
                    string entityName = IBLBDiag.Entity.GetType().Name;
                    baseResultBool.success = IBLBDiag.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBDiagByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBDiag> entityList = new EntityList<LBDiag>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBDiag.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBDiag.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBDiag>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBDiagById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBDiag.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBDiag>(entity);
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

        #region LBDict
        //Add  LBDict
        public BaseResultDataValue LS_UDTO_AddLBDict(LBDict entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBDict.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBDict.Add();
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

        //Update  LBDict
        public BaseResultBool LS_UDTO_UpdateLBDictByField(LBDict entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBDict.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBDict.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBDict.Update(tempArray);
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
                        //baseResultBool.success = IBLBDict.Edit();
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
        //Delele  LBDict
        public BaseResultBool LS_UDTO_DelLBDict(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBDict.Entity = IBLBDict.Get(id);
                if (IBLBDict.Entity != null)
                {
                    long labid = IBLBDict.Entity.LabID;
                    string entityName = IBLBDict.Entity.GetType().Name;
                    baseResultBool.success = IBLBDict.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBDict> entityList = new EntityList<LBDict>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBDict.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBDict.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBDict>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBDictById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBDict.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBDict>(entity);
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

        #region LBEquip
        //Add  LBEquip
        public BaseResultDataValue LS_UDTO_AddLBEquip(LBEquip entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.EquipNo = IBLisCommon.GetMaxNoByFieldName<LBEquip>("EquipNo");
                IBLBEquip.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBEquip.Add();
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

        //Update  LBEquip
        public BaseResultBool LS_UDTO_UpdateLBEquipByField(LBEquip entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquip.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBEquip.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBEquip.Update(tempArray);
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
                        //baseResultBool.success = IBLBEquip.Edit();
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
        //Delele  LBEquip
        public BaseResultBool LS_UDTO_DelLBEquip(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBEquip.Entity = IBLBEquip.Get(id);
                if (IBLBEquip.Entity != null)
                {
                    long labid = IBLBEquip.Entity.LabID;
                    string entityName = IBLBEquip.Entity.GetType().Name;
                    baseResultBool.success = IBLBEquip.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquip> entityList = new EntityList<LBEquip>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquip.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquip.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquip>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBEquip.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBEquip>(entity);
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

        #region LBEquipItem
        //Add  LBEquipItem
        public BaseResultDataValue LS_UDTO_AddLBEquipItem(LBEquipItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBEquipItem.Add();
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

        //Update  LBEquipItem
        public BaseResultBool LS_UDTO_UpdateLBEquipItemByField(LBEquipItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBEquipItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBEquipItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBEquipItem.Edit();
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
        //Delele  LBEquipItem
        public BaseResultBool LS_UDTO_DelLBEquipItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBEquipItem.Entity = IBLBEquipItem.Get(id);
                if (IBLBEquipItem.Entity != null)
                {
                    long labid = IBLBEquipItem.Entity.LabID;
                    string entityName = IBLBEquipItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBEquipItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipItem> entityList = new EntityList<LBEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBEquipItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBEquipItem>(entity);
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

        #region LBEquipResultTH
        //Add  LBEquipResultTH
        public BaseResultDataValue LS_UDTO_AddLBEquipResultTH(LBEquipResultTH entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipResultTH.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBEquipResultTH.Add();
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

        //Update  LBEquipResultTH
        public BaseResultBool LS_UDTO_UpdateLBEquipResultTHByField(LBEquipResultTH entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipResultTH.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBEquipResultTH.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBEquipResultTH.Update(tempArray);
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
                        //baseResultBool.success = IBLBEquipResultTH.Edit();
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
        //Delele  LBEquipResultTH
        public BaseResultBool LS_UDTO_DelLBEquipResultTH(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBEquipResultTH.Entity = IBLBEquipResultTH.Get(id);
                if (IBLBEquipResultTH.Entity != null)
                {
                    long labid = IBLBEquipResultTH.Entity.LabID;
                    string entityName = IBLBEquipResultTH.Entity.GetType().Name;
                    baseResultBool.success = IBLBEquipResultTH.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipResultTHByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipResultTH> entityList = new EntityList<LBEquipResultTH>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipResultTH.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipResultTH.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipResultTH>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipResultTHById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBEquipResultTH.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBEquipResultTH>(entity);
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

        #region LBEquipSection
        //Add  LBEquipSection
        public BaseResultDataValue LS_UDTO_AddLBEquipSection(LBEquipSection entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipSection.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBEquipSection.Add();
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

        //Update  LBEquipSection
        public BaseResultBool LS_UDTO_UpdateLBEquipSectionByField(LBEquipSection entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBEquipSection.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBEquipSection.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBEquipSection.Update(tempArray);
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
                        //baseResultBool.success = IBLBEquipSection.Edit();
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
        //Delele  LBEquipSection
        public BaseResultBool LS_UDTO_DelLBEquipSection(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBEquipSection.Entity = IBLBEquipSection.Get(id);
                if (IBLBEquipSection.Entity != null)
                {
                    long labid = IBLBEquipSection.Entity.LabID;
                    string entityName = IBLBEquipSection.Entity.GetType().Name;
                    baseResultBool.success = IBLBEquipSection.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipSection> entityList = new EntityList<LBEquipSection>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipSection.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipSection.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipSection>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipSectionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBEquipSection.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBEquipSection>(entity);
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

        #region LBExpertRule
        //Add  LBExpertRule
        public BaseResultDataValue LS_UDTO_AddLBExpertRule(LBExpertRule entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBExpertRule.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBExpertRule.Add();
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

        //Update  LBExpertRule
        public BaseResultBool LS_UDTO_UpdateLBExpertRuleByField(LBExpertRule entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBExpertRule.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBExpertRule.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBExpertRule.Update(tempArray);
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
                        //baseResultBool.success = IBLBExpertRule.Edit();
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
        //Delele  LBExpertRule
        public BaseResultBool LS_UDTO_DelLBExpertRule(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBExpertRule.Entity = IBLBExpertRule.Get(id);
                if (IBLBExpertRule.Entity != null)
                {
                    long labid = IBLBExpertRule.Entity.LabID;
                    string entityName = IBLBExpertRule.Entity.GetType().Name;
                    baseResultBool.success = IBLBExpertRule.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBExpertRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBExpertRule> entityList = new EntityList<LBExpertRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBExpertRule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBExpertRule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBExpertRule>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBExpertRuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBExpertRule.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBExpertRule>(entity);
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

        #region LBExpertRuleList
        //Add  LBExpertRuleList
        public BaseResultDataValue LS_UDTO_AddLBExpertRuleList(LBExpertRuleList entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBExpertRuleList.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBExpertRuleList.Add();
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
        
        //Update  LBExpertRuleList
        public BaseResultBool LS_UDTO_UpdateLBExpertRuleListByField(LBExpertRuleList entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBExpertRuleList.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBExpertRuleList.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBExpertRuleList.Update(tempArray);
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
                        //baseResultBool.success = IBLBExpertRuleList.Edit();
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
        //Delele  LBExpertRuleList
        public BaseResultBool LS_UDTO_DelLBExpertRuleList(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBExpertRuleList.Entity = IBLBExpertRuleList.Get(id);
                if (IBLBExpertRuleList.Entity != null)
                {
                    long labid = IBLBExpertRuleList.Entity.LabID;
                    string entityName = IBLBExpertRuleList.Entity.GetType().Name;
                    baseResultBool.success = IBLBExpertRuleList.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBExpertRuleListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBExpertRuleList> entityList = new EntityList<LBExpertRuleList>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBExpertRuleList.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBExpertRuleList.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBExpertRuleList>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBExpertRuleListById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBExpertRuleList.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBExpertRuleList>(entity);
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

        #region LBFolk
        //Add  LBFolk
        public BaseResultDataValue LS_UDTO_AddLBFolk(LBFolk entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBFolk.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBFolk.Add();
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

        //Update  LBFolk
        public BaseResultBool LS_UDTO_UpdateLBFolkByField(LBFolk entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBFolk.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBFolk.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBFolk.Update(tempArray);
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
                        //baseResultBool.success = IBLBFolk.Edit();
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
        //Delele  LBFolk
        public BaseResultBool LS_UDTO_DelLBFolk(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBFolk.Entity = IBLBFolk.Get(id);
                if (IBLBFolk.Entity != null)
                {
                    long labid = IBLBFolk.Entity.LabID;
                    string entityName = IBLBFolk.Entity.GetType().Name;
                    baseResultBool.success = IBLBFolk.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBFolkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBFolk> entityList = new EntityList<LBFolk>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBFolk.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBFolk.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBFolk>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBFolkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBFolk.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBFolk>(entity);
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

        #region LBItem
        //Add  LBItem
        public BaseResultDataValue LS_UDTO_AddLBItem(LBItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.ItemNo = IBLisCommon.GetMaxNoByFieldName<LBItem>("ItemNo");
                IBLBItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItem.Add();
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

        //Update  LBItem
        public BaseResultBool LS_UDTO_UpdateLBItemByField(LBItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBItem.Edit();
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
        //Delele  LBItem
        public BaseResultBool LS_UDTO_DelLBItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItem.Entity = IBLBItem.Get(id);
                if (IBLBItem.Entity != null)
                {
                    long labid = IBLBItem.Entity.LabID;
                    string entityName = IBLBItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCurPageByHQL(long id, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityPageList<LBItem> entityList = new EntityPageList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItem.SearchListByHQL(id, where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItem.SearchListByHQL(id, where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItem>(entity);
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

        public BaseResultDataValue LS_UDTO_SearchByLBSectionItemHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string lbsectionId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBLBItem.SearchLBItemEntityListByLBSectionItemHQL(where, sort, page, limit, lbsectionId);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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
        public BaseResultDataValue LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBLBItem.SearchNotLBParItemSplitPLBItemListByHQL(where, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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
        public BaseResultDataValue LS_UDTO_SearchAlreadyLBParItemSplitPLBItemListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBLBItem.SearchAlreadyLBParItemSplitPLBItemListByHQL(where, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        #region LBItemCharge
        //Add  LBItemCharge
        public BaseResultDataValue LS_UDTO_AddLBItemCharge(LBItemCharge entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCharge.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemCharge.Add();
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

        //Update  LBItemCharge
        public BaseResultBool LS_UDTO_UpdateLBItemChargeByField(LBItemCharge entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCharge.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemCharge.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemCharge.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemCharge.Edit();
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
        //Delele  LBItemCharge
        public BaseResultBool LS_UDTO_DelLBItemCharge(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemCharge.Entity = IBLBItemCharge.Get(id);
                if (IBLBItemCharge.Entity != null)
                {
                    long labid = IBLBItemCharge.Entity.LabID;
                    string entityName = IBLBItemCharge.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemCharge.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemChargeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemCharge> entityList = new EntityList<LBItemCharge>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemCharge.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemCharge.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemCharge>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemChargeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemCharge.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemCharge>(entity);
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

        #region LBItemCalc
        //Add  LBItemCalc
        public BaseResultDataValue LS_UDTO_AddLBItemCalc(LBItemCalc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCalc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemCalc.Add();
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

        //Update  LBItemCalc
        public BaseResultBool LS_UDTO_UpdateLBItemCalcByField(LBItemCalc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCalc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemCalc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemCalc.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemCalc.Edit();
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
        //Delele  LBItemCalc
        public BaseResultBool LS_UDTO_DelLBItemCalc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemCalc.Entity = IBLBItemCalc.Get(id);
                if (IBLBItemCalc.Entity != null)
                {
                    long labid = IBLBItemCalc.Entity.LabID;
                    string entityName = IBLBItemCalc.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemCalc.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCalcByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemCalc> entityList = new EntityList<LBItemCalc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemCalc.QueryLBItemCalc(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemCalc.QueryLBItemCalc(where, "", page, limit);
                }
                //string aa = entityList.list[0].LBCalcItem.CName;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemCalc>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCalcById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemCalc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemCalc>(entity);
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

        #region LBItemCalcFormula
        //Add  LBItemCalcFormula
        public BaseResultDataValue LS_UDTO_AddLBItemCalcFormula(LBItemCalcFormula entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCalcFormula.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemCalcFormula.Add();
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

        //Update  LBItemCalcFormula
        public BaseResultBool LS_UDTO_UpdateLBItemCalcFormulaByField(LBItemCalcFormula entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCalcFormula.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemCalcFormula.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemCalcFormula.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemCalcFormula.Edit();
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
        //Delele  LBItemCalcFormula
        public BaseResultBool LS_UDTO_DelLBItemCalcFormula(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemCalcFormula.Entity = IBLBItemCalcFormula.Get(id);
                if (IBLBItemCalcFormula.Entity != null)
                {
                    long labid = IBLBItemCalcFormula.Entity.LabID;
                    string entityName = IBLBItemCalcFormula.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemCalcFormula.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCalcFormulaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemCalcFormula> entityList = new EntityList<LBItemCalcFormula>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemCalcFormula.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemCalcFormula.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemCalcFormula>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCalcFormulaById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemCalcFormula.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemCalcFormula>(entity);
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

        #region LBItemComp
        //Add  LBItemComp
        public BaseResultDataValue LS_UDTO_AddLBItemComp(LBItemComp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemComp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemComp.Add();
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

        //Update  LBItemComp
        public BaseResultBool LS_UDTO_UpdateLBItemCompByField(LBItemComp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemComp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemComp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemComp.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemComp.Edit();
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
        //Delele  LBItemComp
        public BaseResultBool LS_UDTO_DelLBItemComp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemComp.Entity = IBLBItemComp.Get(id);
                if (IBLBItemComp.Entity != null)
                {
                    long labid = IBLBItemComp.Entity.LabID;
                    string entityName = IBLBItemComp.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemComp.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCompByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemComp> entityList = new EntityList<LBItemComp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemComp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemComp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemComp>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCompById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemComp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemComp>(entity);
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

        #region LBItemGroup
        //Add  LBItemGroup
        public BaseResultDataValue LS_UDTO_AddLBItemGroup(LBItemGroup entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemGroup.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemGroup.Add();
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

        //Update  LBItemGroup
        public BaseResultBool LS_UDTO_UpdateLBItemGroupByField(LBItemGroup entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemGroup.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemGroup.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemGroup.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemGroup.Edit();
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
        //Delele  LBItemGroup
        public BaseResultBool LS_UDTO_DelLBItemGroup(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemGroup.Entity = IBLBItemGroup.Get(id);
                if (IBLBItemGroup.Entity != null)
                {
                    long labid = IBLBItemGroup.Entity.LabID;
                    string entityName = IBLBItemGroup.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemGroup.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemGroup> entityList = new EntityList<LBItemGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemGroup>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemGroup.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemGroup>(entity);
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

        #region LBItemRange
        //Add  LBItemRange
        public BaseResultDataValue LS_UDTO_AddLBItemRange(LBItemRange entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemRange.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemRange.Add();
                    if (baseResultDataValue.success)
                    {
                        if (entity.LBItem != null)
                            IBLBItem.EditLBItemDefaultRange(entity.LBItem.Id);
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

        //Update  LBItemRange
        public BaseResultBool LS_UDTO_UpdateLBItemRangeByField(LBItemRange entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemRange.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemRange.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemRange.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                if (entity.LBItem != null)
                                    IBLBItem.EditLBItemDefaultRange(entity.LBItem.Id);
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBLBItemRange.Edit();
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
        //Delele  LBItemRange
        public BaseResultBool LS_UDTO_DelLBItemRange(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                LBItemRange entity = IBLBItemRange.Get(id);
                IBLBItemRange.Entity = entity;
                if (entity != null)
                {
                    long labid = entity.LabID;
                    string entityName = entity.GetType().Name;
                    baseResultBool.success = IBLBItemRange.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        if (entity.LBItem != null)
                            IBLBItem.EditLBItemDefaultRange(entity.LBItem.Id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemRangeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemRange> entityList = new EntityList<LBItemRange>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemRange.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemRange.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemRange>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemRangeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemRange.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemRange>(entity);
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

        #region LBItemRangeExp
        //Add  LBItemRangeExp
        public BaseResultDataValue LS_UDTO_AddLBItemRangeExp(LBItemRangeExp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemRangeExp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemRangeExp.Add();
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

        //Update  LBItemRangeExp
        public BaseResultBool LS_UDTO_UpdateLBItemRangeExpByField(LBItemRangeExp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemRangeExp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemRangeExp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemRangeExp.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemRangeExp.Edit();
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
        //Delele  LBItemRangeExp
        public BaseResultBool LS_UDTO_DelLBItemRangeExp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemRangeExp.Entity = IBLBItemRangeExp.Get(id);
                if (IBLBItemRangeExp.Entity != null)
                {
                    long labid = IBLBItemRangeExp.Entity.LabID;
                    string entityName = IBLBItemRangeExp.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemRangeExp.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemRangeExpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemRangeExp> entityList = new EntityList<LBItemRangeExp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemRangeExp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemRangeExp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemRangeExp>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemRangeExpById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemRangeExp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemRangeExp>(entity);
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

        #region LBItemTimeW
        //Add  LBItemTimeW
        public BaseResultDataValue LS_UDTO_AddLBItemTimeW(LBItemTimeW entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemTimeW.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemTimeW.Add();
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

        //Update  LBItemTimeW
        public BaseResultBool LS_UDTO_UpdateLBItemTimeWByField(LBItemTimeW entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemTimeW.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemTimeW.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemTimeW.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemTimeW.Edit();
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
        //Delele  LBItemTimeW
        public BaseResultBool LS_UDTO_DelLBItemTimeW(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemTimeW.Entity = IBLBItemTimeW.Get(id);
                if (IBLBItemTimeW.Entity != null)
                {
                    long labid = IBLBItemTimeW.Entity.LabID;
                    string entityName = IBLBItemTimeW.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemTimeW.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemTimeWByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemTimeW> entityList = new EntityList<LBItemTimeW>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemTimeW.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemTimeW.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemTimeW>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemTimeWById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemTimeW.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemTimeW>(entity);
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

        #region LBItemExp
        //Add  LBItemExp
        public BaseResultDataValue LS_UDTO_AddLBItemExp(LBItemExp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemExp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemExp.Add();
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

        //Update  LBItemExp
        public BaseResultBool LS_UDTO_UpdateLBItemExpByField(LBItemExp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemExp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemExp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemExp.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemExp.Edit();
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
        //Delele  LBItemExp
        public BaseResultBool LS_UDTO_DelLBItemExp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemExp.Entity = IBLBItemExp.Get(id);
                if (IBLBItemExp.Entity != null)
                {
                    long labid = IBLBItemExp.Entity.LabID;
                    string entityName = IBLBItemExp.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemExp.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemExpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemExp> entityList = new EntityList<LBItemExp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemExp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemExp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemExp>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemExpById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemExp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemExp>(entity);
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

        #region LBPhrase
        //Add  LBPhrase
        public BaseResultDataValue LS_UDTO_AddLBPhrase(LBPhrase entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrase.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBPhrase.Add();
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

        //Update  LBPhrase
        public BaseResultBool LS_UDTO_UpdateLBPhraseByField(LBPhrase entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrase.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBPhrase.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBPhrase.Update(tempArray);
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
                        //baseResultBool.success = IBLBPhrase.Edit();
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
        //Delele  LBPhrase
        public BaseResultBool LS_UDTO_DelLBPhrase(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBPhrase.Entity = IBLBPhrase.Get(id);
                if (IBLBPhrase.Entity != null)
                {
                    long labid = IBLBPhrase.Entity.LabID;
                    string entityName = IBLBPhrase.Entity.GetType().Name;
                    baseResultBool.success = IBLBPhrase.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhraseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBPhrase> entityList = new EntityList<LBPhrase>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBPhrase.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBPhrase.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhrase>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhraseById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBPhrase.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBPhrase>(entity);
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

        #region LBPhyPeriod
        //Add  LBPhyPeriod
        public BaseResultDataValue LS_UDTO_AddLBPhyPeriod(LBPhyPeriod entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhyPeriod.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBPhyPeriod.Add();
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

        //Update  LBPhyPeriod 
        public BaseResultBool LS_UDTO_UpdateLBPhyPeriodByField(LBPhyPeriod entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhyPeriod.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBPhyPeriod.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBPhyPeriod.Update(tempArray);
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
                        //baseResultBool.success = IBLBPhyPeriod .Edit();
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
        //Delele  LBPhyPeriod 
        public BaseResultBool LS_UDTO_DelLBPhyPeriod(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBPhyPeriod.Entity = IBLBPhyPeriod.Get(id);
                if (IBLBPhyPeriod.Entity != null)
                {
                    long labid = IBLBPhyPeriod.Entity.LabID;
                    string entityName = IBLBPhyPeriod.Entity.GetType().Name;
                    baseResultBool.success = IBLBPhyPeriod.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhyPeriodByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBPhyPeriod> entityList = new EntityList<LBPhyPeriod>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBPhyPeriod.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBPhyPeriod.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhyPeriod>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhyPeriodById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBPhyPeriod.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBPhyPeriod>(entity);
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

        #region LBSampleType
        //Add  LBSampleType
        public BaseResultDataValue LS_UDTO_AddLBSampleType(LBSampleType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.SampleTypeNo = IBLisCommon.GetMaxNoByFieldName<LBSampleType>("SampleTypeNo");
                IBLBSampleType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSampleType.Add();
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

        //Update  LBSampleType
        public BaseResultBool LS_UDTO_UpdateLBSampleTypeByField(LBSampleType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSampleType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSampleType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSampleType.Update(tempArray);
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
                        //baseResultBool.success = IBLBSampleType.Edit();
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
        //Delele  LBSampleType
        public BaseResultBool LS_UDTO_DelLBSampleType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSampleType.Entity = IBLBSampleType.Get(id);
                if (IBLBSampleType.Entity != null)
                {
                    long labid = IBLBSampleType.Entity.LabID;
                    string entityName = IBLBSampleType.Entity.GetType().Name;
                    baseResultBool.success = IBLBSampleType.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSampleType> entityList = new EntityList<LBSampleType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSampleType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSampleType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSampleType>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSampleTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSampleType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSampleType>(entity);
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

        #region LBSection
        //Add  LBSection
        public BaseResultDataValue LS_UDTO_AddLBSection(LBSection entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.SectionNo = IBLisCommon.GetMaxNoByFieldName<LBSection>("SectionNo");
                IBLBSection.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSection.Add();
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

        //Update  LBSection
        public BaseResultBool LS_UDTO_UpdateLBSectionByField(LBSection entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSection.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSection.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSection.Update(tempArray);
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
                        //baseResultBool.success = IBLBSection.Edit();
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
        //Delele  LBSection
        public BaseResultBool LS_UDTO_DelLBSection(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSection.Entity = IBLBSection.Get(id);
                if (IBLBSection.Entity != null)
                {
                    long labid = IBLBSection.Entity.LabID;
                    string entityName = IBLBSection.Entity.GetType().Name;
                    baseResultBool.success = IBLBSection.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSection> entityList = new EntityList<LBSection>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSection.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSection.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSection>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSection.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSection>(entity);
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

        #region LBSectionItem
        //Add  LBSectionItem
        public BaseResultDataValue LS_UDTO_AddLBSectionItem(LBSectionItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSectionItem.Add();
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

        //Update  LBSectionItem
        public BaseResultBool LS_UDTO_UpdateLBSectionItemByField(LBSectionItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSectionItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSectionItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBSectionItem.Edit();
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
        //Delele  LBSectionItem
        public BaseResultBool LS_UDTO_DelLBSectionItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSectionItem.Entity = IBLBSectionItem.Get(id);
                if (IBLBSectionItem.Entity != null)
                {
                    long labid = IBLBSectionItem.Entity.LabID;
                    string entityName = IBLBSectionItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBSectionItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSectionItem> entityList = new EntityList<LBSectionItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSectionItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSectionItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSectionItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSectionItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSectionItem>(entity);
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

        #region LBSectionPrint
        //Add  LBSectionPrint
        public BaseResultDataValue LS_UDTO_AddLBSectionPrint(LBSectionPrint entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionPrint.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSectionPrint.Add();
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

        //Update  LBSectionPrint
        public BaseResultBool LS_UDTO_UpdateLBSectionPrintByField(LBSectionPrint entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionPrint.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSectionPrint.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSectionPrint.Update(tempArray);
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
                        //baseResultBool.success = IBLBSectionPrint.Edit();
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
        //Delele  LBSectionPrint
        public BaseResultBool LS_UDTO_DelLBSectionPrint(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSectionPrint.Entity = IBLBSectionPrint.Get(id);
                if (IBLBSectionPrint.Entity != null)
                {
                    long labid = IBLBSectionPrint.Entity.LabID;
                    string entityName = IBLBSectionPrint.Entity.GetType().Name;
                    baseResultBool.success = IBLBSectionPrint.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionPrintByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSectionPrint> entityList = new EntityList<LBSectionPrint>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSectionPrint.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSectionPrint.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSectionPrint>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionPrintById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSectionPrint.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSectionPrint>(entity);
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

        #region LBSectionHisComp
        //Add  LBSectionHisComp
        public BaseResultDataValue LS_UDTO_AddLBSectionHisComp(LBSectionHisComp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionHisComp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSectionHisComp.Add();
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

        //Update  LBSectionHisComp
        public BaseResultBool LS_UDTO_UpdateLBSectionHisCompByField(LBSectionHisComp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSectionHisComp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSectionHisComp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSectionHisComp.Update(tempArray);
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
                        //baseResultBool.success = IBLBSectionHisComp.Edit();
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
        //Delele  LBSectionHisComp
        public BaseResultBool LS_UDTO_DelLBSectionHisComp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSectionHisComp.Entity = IBLBSectionHisComp.Get(id);
                if (IBLBSectionHisComp.Entity != null)
                {
                    long labid = IBLBSectionHisComp.Entity.LabID;
                    string entityName = IBLBSectionHisComp.Entity.GetType().Name;
                    baseResultBool.success = IBLBSectionHisComp.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionHisCompByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSectionHisComp> entityList = new EntityList<LBSectionHisComp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSectionHisComp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSectionHisComp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSectionHisComp>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionHisCompById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSectionHisComp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSectionHisComp>(entity);
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

        #region LBSickType
        //Add  LBSickType
        public BaseResultDataValue LS_UDTO_AddLBSickType(LBSickType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSickType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSickType.Add();
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

        //Update  LBSickType
        public BaseResultBool LS_UDTO_UpdateLBSickTypeByField(LBSickType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSickType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSickType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSickType.Update(tempArray);
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
                        //baseResultBool.success = IBLBSickType.Edit();
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
        //Delele  LBSickType
        public BaseResultBool LS_UDTO_DelLBSickType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSickType.Entity = IBLBSickType.Get(id);
                if (IBLBSickType.Entity != null)
                {
                    long labid = IBLBSickType.Entity.LabID;
                    string entityName = IBLBSickType.Entity.GetType().Name;
                    baseResultBool.success = IBLBSickType.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSickType> entityList = new EntityList<LBSickType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSickType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSickType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSickType>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSickTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSickType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSickType>(entity);
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

        #region LBSpecialty
        //Add  LBSpecialty
        public BaseResultDataValue LS_UDTO_AddLBSpecialty(LBSpecialty entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSpecialty.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSpecialty.Add();
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

        //Update  LBSpecialty
        public BaseResultBool LS_UDTO_UpdateLBSpecialtyByField(LBSpecialty entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSpecialty.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSpecialty.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSpecialty.Update(tempArray);
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
                        //baseResultBool.success = IBLBSpecialty.Edit();
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
        //Delele  LBSpecialty
        public BaseResultBool LS_UDTO_DelLBSpecialty(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSpecialty.Entity = IBLBSpecialty.Get(id);
                if (IBLBSpecialty.Entity != null)
                {
                    long labid = IBLBSpecialty.Entity.LabID;
                    string entityName = IBLBSpecialty.Entity.GetType().Name;
                    baseResultBool.success = IBLBSpecialty.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSpecialtyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSpecialty> entityList = new EntityList<LBSpecialty>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSpecialty.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSpecialty.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSpecialty>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSpecialtyById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSpecialty.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSpecialty>(entity);
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

        #region LBRight
        //Add  LBRight
        public BaseResultDataValue LS_UDTO_AddLBRight(LBRight entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBRight.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBRight.Add();
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

        //Update  LBRight
        public BaseResultBool LS_UDTO_UpdateLBRightByField(LBRight entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBRight.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBRight.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBRight.Update(tempArray);
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
                        //baseResultBool.success = IBLBRight.Edit();
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
        //Delele  LBRight
        public BaseResultBool LS_UDTO_DelLBRight(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBRight.Entity = IBLBRight.Get(id);
                if (IBLBRight.Entity != null)
                {
                    long labid = IBLBRight.Entity.LabID;
                    string entityName = IBLBRight.Entity.GetType().Name;
                    baseResultBool.success = IBLBRight.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBRightByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBRight> entityList = new EntityList<LBRight>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBRight.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBRight.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBRight>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBRightById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBRight.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBRight>(entity);
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

        #region LBReportDate
        //Add  LBReportDate
        public BaseResultDataValue LS_UDTO_AddLBReportDate(LBReportDate entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDate.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBReportDate.Add();
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

        //Update  LBReportDate
        public BaseResultBool LS_UDTO_UpdateLBReportDateByField(LBReportDate entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBReportDate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBReportDate.Update(tempArray);
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
                        //baseResultBool.success = IBLBReportDate.Edit();
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
        //Delele  LBReportDate
        public BaseResultBool LS_UDTO_DelLBReportDate(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBReportDate.Entity = IBLBReportDate.Get(id);
                if (IBLBReportDate.Entity != null)
                {
                    long labid = IBLBReportDate.Entity.LabID;
                    string entityName = IBLBReportDate.Entity.GetType().Name;
                    baseResultBool.success = IBLBReportDate.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDate> entityList = new EntityList<LBReportDate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBReportDate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBReportDate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDate>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBReportDate.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBReportDate>(entity);
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

        #region LBReportDateItem
        //Add  LBReportDateItem
        public BaseResultDataValue LS_UDTO_AddLBReportDateItem(LBReportDateItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBReportDateItem.Add();
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

        //Update  LBReportDateItem
        public BaseResultBool LS_UDTO_UpdateLBReportDateItemByField(LBReportDateItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBReportDateItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBReportDateItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBReportDateItem.Edit();
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
        //Delele  LBReportDateItem
        public BaseResultBool LS_UDTO_DelLBReportDateItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBReportDateItem.Entity = IBLBReportDateItem.Get(id);
                if (IBLBReportDateItem.Entity != null)
                {
                    long labid = IBLBReportDateItem.Entity.LabID;
                    string entityName = IBLBReportDateItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBReportDateItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDateItem> entityList = new EntityList<LBReportDateItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBReportDateItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBReportDateItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDateItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBReportDateItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBReportDateItem>(entity);
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

        #region LBReportDateRule
        //Add  LBReportDateRule
        public BaseResultDataValue LS_UDTO_AddLBReportDateRule(LBReportDateRule entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateRule.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBReportDateRule.Add();
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

        //Update  LBReportDateRule
        public BaseResultBool LS_UDTO_UpdateLBReportDateRuleByField(LBReportDateRule entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateRule.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBReportDateRule.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBReportDateRule.Update(tempArray);
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
                        //baseResultBool.success = IBLBReportDateRule.Edit();
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
        //Delele  LBReportDateRule
        public BaseResultBool LS_UDTO_DelLBReportDateRule(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBReportDateRule.Entity = IBLBReportDateRule.Get(id);
                if (IBLBReportDateRule.Entity != null)
                {
                    long labid = IBLBReportDateRule.Entity.LabID;
                    string entityName = IBLBReportDateRule.Entity.GetType().Name;
                    baseResultBool.success = IBLBReportDateRule.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDateRule> entityList = new EntityList<LBReportDateRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBReportDateRule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBReportDateRule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDateRule>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateRuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBReportDateRule.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBReportDateRule>(entity);
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

        #region LBParItemSplit
        //Add  LBParItemSplit
        public BaseResultDataValue LS_UDTO_AddLBParItemSplit(LBParItemSplit entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBParItemSplit.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBParItemSplit.Add();
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

        //Update  LBParItemSplit
        public BaseResultBool LS_UDTO_UpdateLBParItemSplitByField(LBParItemSplit entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBParItemSplit.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBParItemSplit.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBParItemSplit.Update(tempArray);
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
                        //baseResultBool.success = IBLBParItemSplit.Edit();
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
        //Delele  LBParItemSplit
        public BaseResultBool LS_UDTO_DelLBParItemSplit(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBParItemSplit.Entity = IBLBParItemSplit.Get(id);
                if (IBLBParItemSplit.Entity != null)
                {
                    long labid = IBLBParItemSplit.Entity.LabID;
                    string entityName = IBLBParItemSplit.Entity.GetType().Name;
                    baseResultBool.success = IBLBParItemSplit.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBParItemSplitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBParItemSplit> entityList = new EntityList<LBParItemSplit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBParItemSplit.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBParItemSplit.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBParItemSplit>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBParItemSplitById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBParItemSplit.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBParItemSplit>(entity);
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
        public BaseResultDataValue LS_UDTO_SearchAddLBParItemSplitListByParItemId(int page, int limit, string fields, string where, string sort, bool isPlanish, string parItemId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBParItemSplit> entityList = new EntityList<LBParItemSplit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBLBParItemSplit.SearchAddLBParItemSplitListByParItemId(where, sort, page, limit, parItemId);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBParItemSplit>(entityList);
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
        public BaseResultDataValue LS_UDTO_SearchEditLBParItemSplitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBParItemSplit> entityList = new EntityList<LBParItemSplit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBLBParItemSplit.SearchEditLBParItemSplitByHQL(where, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBParItemSplit>(entityList);
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
        public BaseResultDataValue LS_UDTO_AddLBParItemSplitList(IList<LBParItemSplit> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList != null)
            {
                try
                {
                    baseResultDataValue = IBLBParItemSplit.AddLBParItemSplitList(entityList);
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
                baseResultDataValue.ErrorInfo = "错误信息：传入参数不能为空！";
            }
            return baseResultDataValue;
        }
        public BaseResultBool LS_UDTO_UpdateLBParItemSplitList(IList<LBParItemSplit> entityList)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entityList != null)
            {
                try
                {
                    baseResultBool = IBLBParItemSplit.EditLBParItemSplitList(entityList);
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
                baseResultBool.ErrorInfo = "错误信息：传入参数不能为空！";
            }
            return baseResultBool;
        }
        public BaseResultBool LS_UDTO_DelLBParItemSplitByParItemId(long parItemId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool = IBLBParItemSplit.DelLBParItemSplitByParItemId(parItemId);
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

        #region LBPhrasesWatch
        //Add  LBPhrasesWatch
        public BaseResultDataValue LS_UDTO_AddLBPhrasesWatch(LBPhrasesWatch entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatch.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBPhrasesWatch.Add();
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

        //Update  LBPhrasesWatch
        public BaseResultBool LS_UDTO_UpdateLBPhrasesWatchByField(LBPhrasesWatch entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatch.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBPhrasesWatch.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBPhrasesWatch.Update(tempArray);
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
                        //baseResultBool.success = IBLBPhrasesWatch.Edit();
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
        //Delele  LBPhrasesWatch
        public BaseResultBool LS_UDTO_DelLBPhrasesWatch(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBPhrasesWatch.Entity = IBLBPhrasesWatch.Get(id);
                if (IBLBPhrasesWatch.Entity != null)
                {
                    long labid = IBLBPhrasesWatch.Entity.LabID;
                    string entityName = IBLBPhrasesWatch.Entity.GetType().Name;
                    baseResultBool.success = IBLBPhrasesWatch.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBPhrasesWatch> entityList = new EntityList<LBPhrasesWatch>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBPhrasesWatch.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBPhrasesWatch.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhrasesWatch>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBPhrasesWatch.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBPhrasesWatch>(entity);
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

        #region LBPhrasesWatchClass
        //Add  LBPhrasesWatchClass
        public BaseResultDataValue LS_UDTO_AddLBPhrasesWatchClass(LBPhrasesWatchClass entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatchClass.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBPhrasesWatchClass.Add();
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

        //Update  LBPhrasesWatchClass
        public BaseResultBool LS_UDTO_UpdateLBPhrasesWatchClassByField(LBPhrasesWatchClass entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatchClass.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBPhrasesWatchClass.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBPhrasesWatchClass.Update(tempArray);
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
                        //baseResultBool.success = IBLBPhrasesWatchClass.Edit();
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
        //Delele  LBPhrasesWatchClass
        public BaseResultBool LS_UDTO_DelLBPhrasesWatchClass(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBPhrasesWatchClass.Entity = IBLBPhrasesWatchClass.Get(id);
                if (IBLBPhrasesWatchClass.Entity != null)
                {
                    long labid = IBLBPhrasesWatchClass.Entity.LabID;
                    string entityName = IBLBPhrasesWatchClass.Entity.GetType().Name;
                    baseResultBool.success = IBLBPhrasesWatchClass.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBPhrasesWatchClass> entityList = new EntityList<LBPhrasesWatchClass>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBPhrasesWatchClass.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBPhrasesWatchClass.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhrasesWatchClass>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBPhrasesWatchClass.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBPhrasesWatchClass>(entity);
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

        #region LBPhrasesWatchClassItem
        //Add  LBPhrasesWatchClassItem
        public BaseResultDataValue LS_UDTO_AddLBPhrasesWatchClassItem(LBPhrasesWatchClassItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatchClassItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBPhrasesWatchClassItem.Add();
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

        //Update  LBPhrasesWatchClassItem
        public BaseResultBool LS_UDTO_UpdateLBPhrasesWatchClassItemByField(LBPhrasesWatchClassItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBPhrasesWatchClassItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBPhrasesWatchClassItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBPhrasesWatchClassItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBPhrasesWatchClassItem.Edit();
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
        //Delele  LBPhrasesWatchClassItem
        public BaseResultBool LS_UDTO_DelLBPhrasesWatchClassItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBPhrasesWatchClassItem.Entity = IBLBPhrasesWatchClassItem.Get(id);
                if (IBLBPhrasesWatchClassItem.Entity != null)
                {
                    long labid = IBLBPhrasesWatchClassItem.Entity.LabID;
                    string entityName = IBLBPhrasesWatchClassItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBPhrasesWatchClassItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBPhrasesWatchClassItem> entityList = new EntityList<LBPhrasesWatchClassItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBPhrasesWatchClassItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBPhrasesWatchClassItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhrasesWatchClassItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBPhrasesWatchClassItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBPhrasesWatchClassItem>(entity);
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

        #region LBOrderModel
        //Add  LBOrderModel
        public BaseResultDataValue LS_UDTO_AddLBOrderModel(LBOrderModel entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBOrderModel.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBOrderModel.Add();
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

        //Update  LBOrderModel
        public BaseResultBool LS_UDTO_UpdateLBOrderModelByField(LBOrderModel entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBOrderModel.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBOrderModel.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBOrderModel.Update(tempArray);
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
                        //baseResultBool.success = IBLBOrderModel.Edit();
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
        //Delele  LBOrderModel
        public BaseResultBool LS_UDTO_DelLBOrderModel(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBOrderModel.Entity = IBLBOrderModel.Get(id);
                if (IBLBOrderModel.Entity != null)
                {
                    long labid = IBLBOrderModel.Entity.LabID;
                    string entityName = IBLBOrderModel.Entity.GetType().Name;
                    baseResultBool.success = IBLBOrderModel.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBOrderModelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBOrderModel> entityList = new EntityList<LBOrderModel>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBOrderModel.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBOrderModel.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBOrderModel>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBOrderModelById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBOrderModel.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBOrderModel>(entity);
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

        #region LBOrderModelItem
        //Add  LBOrderModelItem
        public BaseResultDataValue LS_UDTO_AddLBOrderModelItem(LBOrderModelItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBOrderModelItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBOrderModelItem.Add();
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

        //Update  LBOrderModelItem
        public BaseResultBool LS_UDTO_UpdateLBOrderModelItemByField(LBOrderModelItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBOrderModelItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBOrderModelItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBOrderModelItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBOrderModelItem.Edit();
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
        //Delele  LBOrderModelItem
        public BaseResultBool LS_UDTO_DelLBOrderModelItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBOrderModelItem.Entity = IBLBOrderModelItem.Get(id);
                if (IBLBOrderModelItem.Entity != null)
                {
                    long labid = IBLBOrderModelItem.Entity.LabID;
                    string entityName = IBLBOrderModelItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBOrderModelItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBOrderModelItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBOrderModelItem> entityList = new EntityList<LBOrderModelItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBOrderModelItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBOrderModelItem.SearchListByHQL(where, page, limit);
                }
                if (entityList.count > 0)
                {
                    List<long> ids = new List<long>();
                    entityList.list.ToList().ForEach(f => ids.Add(f.ItemID));
                    ids.Distinct();
                    IList<LBItem> lBItems =  IBLBItem.SearchListByHQL("Id in (" + string.Join(",", ids) + ")");
                    foreach (var item in entityList.list)
                    {
                        item.ItemName = lBItems.Where(w => w.Id == item.ItemID).First().CName;
                    }
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBOrderModelItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBOrderModelItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBOrderModelItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBOrderModelItem>(entity);
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

        #region LBSampleItem
        //Add  LBSampleItem
        public BaseResultDataValue LS_UDTO_AddLBSampleItemList(IList<LBSampleItem> entityList, string ofType, bool isHasDel)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList != null)
            {
                //entity.DataAddTime = DateTime.Now;
                //entity.DataUpdateTime = DateTime.Now;
                //IBLBSampleItem.Entity = entity;
                try
                {
                    baseResultDataValue = IBLBSampleItem.AddLBSampleItemList(entityList, ofType, isHasDel);
                    //if (baseResultDataValue.success)
                    //{
                    //    baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    //    //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    //}
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
        //Add  LBSampleItem
        public BaseResultDataValue LS_UDTO_AddLBSampleItem(LBSampleItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSampleItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSampleItem.Add();
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

        //Update  LBSampleItem
        public BaseResultBool LS_UDTO_UpdateLBSampleItemByField(LBSampleItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSampleItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSampleItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSampleItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBSampleItem.Edit();
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
        //Delele  LBSampleItem
        public BaseResultBool LS_UDTO_DelLBSampleItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSampleItem.Entity = IBLBSampleItem.Get(id);
                if (IBLBSampleItem.Entity != null)
                {
                    long labid = IBLBSampleItem.Entity.LabID;
                    string entityName = IBLBSampleItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBSampleItem.RemoveByHQL(id);
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
                ZhiFang.LabStar.Common.LogHelp.Error("按选择的样本类型或按选择的检验项目新增失败!" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        public BaseResultDataValue LS_UDTO_SearchLBSampleItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSampleItem> entityList = new EntityList<LBSampleItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSampleItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSampleItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSampleItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSampleItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSampleItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSampleItem>(entity);
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

        #region LBSamplingChargeItem
        //Add  LBSamplingChargeItem
        public BaseResultDataValue LS_UDTO_AddLBSamplingChargeItem(LBSamplingChargeItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingChargeItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSamplingChargeItem.Add();
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

        //Update  LBSamplingChargeItem
        public BaseResultBool LS_UDTO_UpdateLBSamplingChargeItemByField(LBSamplingChargeItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingChargeItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSamplingChargeItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSamplingChargeItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBSamplingChargeItem.Edit();
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
        //Delele  LBSamplingChargeItem
        public BaseResultBool LS_UDTO_DelLBSamplingChargeItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSamplingChargeItem.Entity = IBLBSamplingChargeItem.Get(id);
                if (IBLBSamplingChargeItem.Entity != null)
                {
                    long labid = IBLBSamplingChargeItem.Entity.LabID;
                    string entityName = IBLBSamplingChargeItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBSamplingChargeItem.RemoveByHQL(id);
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


        public BaseResultDataValue LS_UDTO_SearchLBSamplingChargeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingChargeItem> entityList = new EntityList<LBSamplingChargeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSamplingChargeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSamplingChargeItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingChargeItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSamplingChargeItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSamplingChargeItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSamplingChargeItem>(entity);
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

        #region LBSamplingGroup
        //Add  LBSamplingGroup
        public BaseResultDataValue LS_UDTO_AddLBSamplingGroup(LBSamplingGroup entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingGroup.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSamplingGroup.Add();
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

        //Update  LBSamplingGroup
        public BaseResultBool LS_UDTO_UpdateLBSamplingGroupByField(LBSamplingGroup entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingGroup.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSamplingGroup.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSamplingGroup.Update(tempArray);
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
                        //baseResultBool.success = IBLBSamplingGroup.Edit();
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
        //Delele  LBSamplingGroup
        public BaseResultBool LS_UDTO_DelLBSamplingGroup(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSamplingGroup.Entity = IBLBSamplingGroup.Get(id);
                if (IBLBSamplingGroup.Entity != null)
                {
                    long labid = IBLBSamplingGroup.Entity.LabID;
                    string entityName = IBLBSamplingGroup.Entity.GetType().Name;
                    baseResultBool.success = IBLBSamplingGroup.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSamplingGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingGroup> entityList = new EntityList<LBSamplingGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSamplingGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSamplingGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingGroup>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSamplingGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSamplingGroup.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSamplingGroup>(entity);
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

        #region LBSamplingItem
        //Add  LBSamplingItem
        public BaseResultDataValue LS_UDTO_AddLBSamplingItem(LBSamplingItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBSamplingItem.Add();
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

        //Update  LBSamplingItem
        public BaseResultBool LS_UDTO_UpdateLBSamplingItemByField(LBSamplingItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBSamplingItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBSamplingItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBSamplingItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBSamplingItem.Edit();
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
        //Delele  LBSamplingItem
        public BaseResultBool LS_UDTO_DelLBSamplingItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBSamplingItem.Entity = IBLBSamplingItem.Get(id);
                if (IBLBSamplingItem.Entity != null)
                {
                    long labid = IBLBSamplingItem.Entity.LabID;
                    string entityName = IBLBSamplingItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBSamplingItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBSamplingItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingItem> entityList = new EntityList<LBSamplingItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSamplingItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSamplingItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSamplingItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBSamplingItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBSamplingItem>(entity);
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

        #region LBTcuvete
        //Add  LBTcuvete
        public BaseResultDataValue LS_UDTO_AddLBTcuvete(LBTcuvete entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTcuvete.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTcuvete.Add();
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

        //Update  LBTcuvete
        public BaseResultBool LS_UDTO_UpdateLBTcuveteByField(LBTcuvete entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTcuvete.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTcuvete.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTcuvete.Update(tempArray);
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
                        //baseResultBool.success = IBLBTcuvete.Edit();
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
        //Delele  LBTcuvete
        public BaseResultBool LS_UDTO_DelLBTcuvete(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTcuvete.Entity = IBLBTcuvete.Get(id);
                if (IBLBTcuvete.Entity != null)
                {
                    long labid = IBLBTcuvete.Entity.LabID;
                    string entityName = IBLBTcuvete.Entity.GetType().Name;
                    baseResultBool.success = IBLBTcuvete.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTcuveteByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTcuvete> entityList = new EntityList<LBTcuvete>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTcuvete.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTcuvete.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTcuvete>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTcuveteById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTcuvete.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTcuvete>(entity);
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

        #region LBTranRule
        //Add  LBTranRule
        public BaseResultDataValue LS_UDTO_AddLBTranRule(LBTranRule entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                if (entity.IsUseNextNo) {
                    BaseResultDataValue baseResultData = IBLBTranRule.GetLBTranRuleNextSampleNo(int.Parse(entity.SampleNoMin), entity.NextSampleNo);
                    if (baseResultData.success)
                    {
                        if (entity.NextSampleNo != baseResultData.ResultDataValue) {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "下一样本号与最初生成的不符！";
                            return baseResultDataValue;
                        }
                    }
                    else {
                        return baseResultData;
                    }
                }
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRule.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTranRule.Add();
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

        //Update  LBTranRule
        public BaseResultBool LS_UDTO_UpdateLBTranRuleByField(LBTranRule entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRule.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTranRule.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTranRule.Update(tempArray);
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
                        //baseResultBool.success = IBLBTranRule.Edit();
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
        //Delele  LBTranRule
        public BaseResultBool LS_UDTO_DelLBTranRule(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTranRuleItem.DelLBTranRuleItemByHQL(id);
                IBLBTranRule.Entity = IBLBTranRule.Get(id);
                if (IBLBTranRule.Entity != null)
                {
                    long labid = IBLBTranRule.Entity.LabID;
                    string entityName = IBLBTranRule.Entity.GetType().Name;
                    baseResultBool.success = IBLBTranRule.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTranRule> entityList = new EntityList<LBTranRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTranRule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTranRule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTranRule>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTranRule.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTranRule>(entity);
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

        #region LBTranRuleItem
        //Add  LBTranRuleItem
        public BaseResultDataValue LS_UDTO_AddLBTranRuleItem(LBTranRuleItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTranRuleItem.Add();
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

        //Update  LBTranRuleItem
        public BaseResultBool LS_UDTO_UpdateLBTranRuleItemByField(LBTranRuleItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTranRuleItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTranRuleItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBTranRuleItem.Edit();
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
        //Delele  LBTranRuleItem
        public BaseResultBool LS_UDTO_DelLBTranRuleItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTranRuleItem.Entity = IBLBTranRuleItem.Get(id);
                if (IBLBTranRuleItem.Entity != null)
                {
                    long labid = IBLBTranRuleItem.Entity.LabID;
                    string entityName = IBLBTranRuleItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBTranRuleItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTranRuleItem> entityList = new EntityList<LBTranRuleItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTranRuleItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTranRuleItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTranRuleItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTranRuleItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTranRuleItem>(entity);
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

        #region LBTranRuleType
        //Add  LBTranRuleType
        public BaseResultDataValue LS_UDTO_AddLBTranRuleType(LBTranRuleType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTranRuleType.Add();
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

        //Update  LBTranRuleType
        public BaseResultBool LS_UDTO_UpdateLBTranRuleTypeByField(LBTranRuleType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTranRuleType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTranRuleType.Update(tempArray);
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
                        //baseResultBool.success = IBLBTranRuleType.Edit();
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
        //Delele  LBTranRuleType
        public BaseResultBool LS_UDTO_DelLBTranRuleType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTranRule.DelLBTranRuleAndTranRuleItem(id);
                IBLBTranRuleType.Entity = IBLBTranRuleType.Get(id);
                if (IBLBTranRuleType.Entity != null)
                {
                    long labid = IBLBTranRuleType.Entity.LabID;
                    string entityName = IBLBTranRuleType.Entity.GetType().Name;
                    baseResultBool.success = IBLBTranRuleType.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTranRuleType> entityList = new EntityList<LBTranRuleType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTranRuleType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTranRuleType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTranRuleType>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTranRuleType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTranRuleType>(entity);
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

        #region LBDicCodeLink
        //Add  LBDicCodeLink
        public BaseResultDataValue LS_UDTO_AddLBDicCodeLink(LBDicCodeLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBDicCodeLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBDicCodeLink.Add();
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

        //Update  LBDicCodeLink
        public BaseResultBool LS_UDTO_UpdateLBDicCodeLinkByField(LBDicCodeLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBDicCodeLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBDicCodeLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBDicCodeLink.Update(tempArray);
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
                        //baseResultBool.success = IBLBDicCodeLink.Edit();
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
        //Delele  LBDicCodeLink
        public BaseResultBool LS_UDTO_DelLBDicCodeLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBDicCodeLink.Entity = IBLBDicCodeLink.Get(id);
                if (IBLBDicCodeLink.Entity != null)
                {
                    long labid = IBLBDicCodeLink.Entity.LabID;
                    string entityName = IBLBDicCodeLink.Entity.GetType().Name;
                    baseResultBool.success = IBLBDicCodeLink.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBDicCodeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBDicCodeLink> entityList = new EntityList<LBDicCodeLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBDicCodeLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBDicCodeLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBDicCodeLink>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBDicCodeLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBDicCodeLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBDicCodeLink>(entity);
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

        #region LBItemCodeLink
        //Add  LBItemCodeLink
        public BaseResultDataValue LS_UDTO_AddLBItemCodeLink(LBItemCodeLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCodeLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBItemCodeLink.Add();
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

        //Update  LBItemCodeLink
        public BaseResultBool LS_UDTO_UpdateLBItemCodeLinkByField(LBItemCodeLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBItemCodeLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemCodeLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBItemCodeLink.Update(tempArray);
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
                        //baseResultBool.success = IBLBItemCodeLink.Edit();
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
        //Delele  LBItemCodeLink
        public BaseResultBool LS_UDTO_DelLBItemCodeLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBItemCodeLink.Entity = IBLBItemCodeLink.Get(id);
                if (IBLBItemCodeLink.Entity != null)
                {
                    long labid = IBLBItemCodeLink.Entity.LabID;
                    string entityName = IBLBItemCodeLink.Entity.GetType().Name;
                    baseResultBool.success = IBLBItemCodeLink.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCodeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemCodeLink> entityList = new EntityList<LBItemCodeLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemCodeLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemCodeLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemCodeLink>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBItemCodeLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBItemCodeLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBItemCodeLink>(entity);
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

        #region BPrintModel
        //Add  BPrintModel
        public BaseResultDataValue LS_UDTO_AddBPrintModel(BPrintModel entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBPrintModel.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBPrintModel.Add();
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

        //Update  BPrintModel
        public BaseResultBool LS_UDTO_UpdateBPrintModelByField(BPrintModel entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBPrintModel.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPrintModel.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBPrintModel.Update(tempArray);
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
                        //baseResultBool.success = IBBPrintModel.Edit();
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
        //Delele  BPrintModel
        public BaseResultBool LS_UDTO_DelBPrintModel(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBPrintModel.Entity = IBBPrintModel.Get(id);
                if (IBBPrintModel.Entity != null)
                {
                    long labid = IBBPrintModel.Entity.LabID;
                    string entityName = IBBPrintModel.Entity.GetType().Name;
                    baseResultBool.success = IBBPrintModel.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchBPrintModelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BPrintModel> entityList = new EntityList<BPrintModel>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBPrintModel.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBPrintModel.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPrintModel>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchBPrintModelById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBPrintModel.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BPrintModel>(entity);
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

        #region LBReportDateDesc
        //Add  LBReportDateDesc
        public BaseResultDataValue LS_UDTO_AddLBReportDateDesc(LBReportDateDesc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateDesc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBReportDateDesc.Add();
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

        //Update  LBReportDateDesc
        public BaseResultBool LS_UDTO_UpdateLBReportDateDescByField(LBReportDateDesc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBReportDateDesc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBReportDateDesc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBReportDateDesc.Update(tempArray);
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
                        //baseResultBool.success = IBLBReportDateDesc.Edit();
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
        //Delele  LBReportDateDesc
        public BaseResultBool LS_UDTO_DelLBReportDateDesc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBReportDateDesc.Entity = IBLBReportDateDesc.Get(id);
                if (IBLBReportDateDesc.Entity != null)
                {
                    long labid = IBLBReportDateDesc.Entity.LabID;
                    string entityName = IBLBReportDateDesc.Entity.GetType().Name;
                    baseResultBool.success = IBLBReportDateDesc.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateDescByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDateDesc> entityList = new EntityList<LBReportDateDesc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBReportDateDesc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBReportDateDesc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDateDesc>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBReportDateDescById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBReportDateDesc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBReportDateDesc>(entity);
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

        #region LBTGetMaxNo
        //Add  LBTGetMaxNo
        public BaseResultDataValue LS_UDTO_AddLBTGetMaxNo(LBTGetMaxNo entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTGetMaxNo.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTGetMaxNo.Add();
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

        //Update  LBTGetMaxNo
        public BaseResultBool LS_UDTO_UpdateLBTGetMaxNoByField(LBTGetMaxNo entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTGetMaxNo.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTGetMaxNo.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTGetMaxNo.Update(tempArray);
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
                        //baseResultBool.success = IBLBTGetMaxNo.Edit();
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
        //Delele  LBTGetMaxNo
        public BaseResultBool LS_UDTO_DelLBTGetMaxNo(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTGetMaxNo.Entity = IBLBTGetMaxNo.Get(id);
                if (IBLBTGetMaxNo.Entity != null)
                {
                    long labid = IBLBTGetMaxNo.Entity.LabID;
                    string entityName = IBLBTGetMaxNo.Entity.GetType().Name;
                    baseResultBool.success = IBLBTGetMaxNo.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTGetMaxNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTGetMaxNo> entityList = new EntityList<LBTGetMaxNo>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTGetMaxNo.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTGetMaxNo.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTGetMaxNo>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTGetMaxNoById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTGetMaxNo.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTGetMaxNo>(entity);
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

        #region LBTranRuleHostSection
        //Add  LBTranRuleHostSection
        public BaseResultDataValue LS_UDTO_AddLBTranRuleHostSection(LBTranRuleHostSection entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleHostSection.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBTranRuleHostSection.Add();
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

        //Update  LBTranRuleHostSection
        public BaseResultBool LS_UDTO_UpdateLBTranRuleHostSectionByField(LBTranRuleHostSection entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBTranRuleHostSection.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBTranRuleHostSection.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBTranRuleHostSection.Update(tempArray);
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
                        //baseResultBool.success = IBLBTranRuleHostSection.Edit();
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
        //Delele  LBTranRuleHostSection
        public BaseResultBool LS_UDTO_DelLBTranRuleHostSection(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBTranRuleHostSection.Entity = IBLBTranRuleHostSection.Get(id);
                if (IBLBTranRuleHostSection.Entity != null)
                {
                    long labid = IBLBTranRuleHostSection.Entity.LabID;
                    string entityName = IBLBTranRuleHostSection.Entity.GetType().Name;
                    baseResultBool.success = IBLBTranRuleHostSection.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleHostSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTranRuleHostSection> entityList = new EntityList<LBTranRuleHostSection>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTranRuleHostSection.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTranRuleHostSection.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTranRuleHostSection>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleHostSectionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBTranRuleHostSection.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBTranRuleHostSection>(entity);
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

        #region LisBarCodeRefuseRecord
        //Add  LisBarCodeRefuseRecord
        public BaseResultDataValue LS_UDTO_AddLisBarCodeRefuseRecord(LisBarCodeRefuseRecord entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeRefuseRecord.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisBarCodeRefuseRecord.Add();
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

        //Update  LisBarCodeRefuseRecord
        public BaseResultBool LS_UDTO_UpdateLisBarCodeRefuseRecordByField(LisBarCodeRefuseRecord entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeRefuseRecord.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisBarCodeRefuseRecord.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisBarCodeRefuseRecord.Update(tempArray);
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
                        //baseResultBool.success = IBLisBarCodeRefuseRecord.Edit();
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
        //Delele  LisBarCodeRefuseRecord
        public BaseResultBool LS_UDTO_DelLisBarCodeRefuseRecord(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisBarCodeRefuseRecord.Entity = IBLisBarCodeRefuseRecord.Get(id);
                if (IBLisBarCodeRefuseRecord.Entity != null)
                {
                    long labid = IBLisBarCodeRefuseRecord.Entity.LabID;
                    string entityName = IBLisBarCodeRefuseRecord.Entity.GetType().Name;
                    baseResultBool.success = IBLisBarCodeRefuseRecord.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeRefuseRecordByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisBarCodeRefuseRecord> entityList = new EntityList<LisBarCodeRefuseRecord>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisBarCodeRefuseRecord.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisBarCodeRefuseRecord.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeRefuseRecord>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeRefuseRecordById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisBarCodeRefuseRecord.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisBarCodeRefuseRecord>(entity);
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

        #region LisQueue
        //Add  LisQueue
        public BaseResultDataValue LS_UDTO_AddLisQueue(LisQueue entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisQueue.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisQueue.Add();
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

        //Update  LisQueue
        public BaseResultBool LS_UDTO_UpdateLisQueueByField(LisQueue entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisQueue.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisQueue.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisQueue.Update(tempArray);
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
                        //baseResultBool.success = IBLisQueue.Edit();
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
        //Delele  LisQueue
        public BaseResultBool LS_UDTO_DelLisQueue(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisQueue.Entity = IBLisQueue.Get(id);
                if (IBLisQueue.Entity != null)
                {
                    long labid = IBLisQueue.Entity.LabID;
                    string entityName = IBLisQueue.Entity.GetType().Name;
                    baseResultBool.success = IBLisQueue.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisQueueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisQueue> entityList = new EntityList<LisQueue>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisQueue.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisQueue.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisQueue>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisQueueById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisQueue.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisQueue>(entity);
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

        #region 定制服务

        #region 仪器定制服务

        /// <summary>
        /// 复制仪器并新增
        /// </summary>
        /// <param name="equipID">源仪器ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LBEquipCopyByID(long equipID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBEquip.AddNewLBEquipByLBEquip(equipID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "仪器复制失败：" + ex.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 复制仪器项目
        /// </summary>
        /// <param name="fromEquipID">源仪器ID</param>
        /// <param name="toEquipID">目标仪器ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LBEquipItemCopyByEquipID(long fromEquipID, long toEquipID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBEquip.AddCopyLBEquipItemByLBEquip(fromEquipID, toEquipID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "仪器复制失败：" + ex.Message;
            }
            return baseResultDataValue;
        }


        #endregion

        #region 项目相关表定制服务

        public BaseResultDataValue LS_UDTO_SearchLBItemListByHQL(long itemID, long groupItemID, long sectionID, long equipID, string where, string fields, int page, int limit, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string otherWhere = " 1=1 ";
            if (groupItemID > 0)
                otherWhere += " and lbitem.Id in (select h.LBItem.Id from LBItemGroup h where h.LBGroup.Id=" + groupItemID.ToString() + ")";
            if (sectionID > 0)
                otherWhere += " and lbitem.Id in (select h.LBItem.Id from LBSectionItem h where h.LBSection.Id=" + sectionID.ToString() + ")";
            else if (sectionID == -1)
                otherWhere += " and lbitem.Id not in (select distinct h.LBItem.Id from LBSectionItem h)";
            if (equipID > 0)
                otherWhere += " and lbitem.Id in (select h.LBItem.Id from LBEquipItem h where h.LBEquip.ID=" + equipID.ToString() + ")";
            else if (equipID == -1)
                otherWhere += " and lbitem.GroupType=0 and lbitem.Id not in (select distinct h.LBItem.Id from LBEquipItem h)";
            if (!string.IsNullOrWhiteSpace(where))
                where = otherWhere + " and " + where;
            else
                where = otherWhere;

            EntityPageList<LBItem> entityList = new EntityPageList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItem.SearchListByHQL(itemID, where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItem.SearchListByHQL(itemID, where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_AddDelLBItemGroup(IList<LBItemGroup> addEntityList, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBItemGroup.AddDelLBItemGroup(addEntityList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除组合项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddCopyLBItemGroup(long fromGroupItemID, long toGroupItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBItemGroup.AddCopyLBItemGroup(fromGroupItemID, toGroupItemID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "复制组合项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBEquipItem(IList<LBEquipItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBEquipItem.AddDelLBEquipItem(addEntityList, isCheckEntityExist, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除仪器项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_DeleteLBEquipItem(string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBEquipItem.DeleteLBEquipItem(delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除仪器项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBSectionItem(IList<LBSectionItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //baseResultDataValue = IBLBSectionItem.AddDelLBSectionItem(addEntityList, isCheckEntityExist, delIDList);
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "IsUse", true }, { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBSectionItem, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除小组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_DeleteLBSectionItem(string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBSectionItem.DeleteLBSectionItem(delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除小组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddCopyLBSectionItem(long fromSectionID, long toSectionID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBSectionItem.AddCopyLBSectionItem(fromSectionID, toSectionID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "复制小组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBItemCalc(IList<LBItemCalc> addEntityList, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBItemCalc.AddDelLBItemCalc(addEntityList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除小组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据项目ID字符串获取项目树
        /// </summary>
        /// <param name="listItemID">项目ID字符串</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetItemTreeByItemIDList(string listItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<tree> itemTree = IBLBItem.GetItemTreeByItemIDList(listItemID);
                if (itemTree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(itemTree);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取项目树发生错误：" + ex.ToString();
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLBItemGroupByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItemGroup> entityList = new EntityList<LBItemGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemGroup.QueryLBItemGroup(where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLBItemGroup.QueryLBItemGroup(where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItemGroup>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionItemVOByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSectionItemVO> entityList = new EntityList<LBSectionItemVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSectionItem.QueryLBSectionItemVO(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSectionItem.QueryLBSectionItemVO(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSectionItemVO>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBSectionDefultSingleItemVO(long sectionID, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSectionSingleItemVO> entityList = new EntityList<LBSectionSingleItemVO>();
            try
            {
                entityList = IBLBSectionItem.QueryLBSectionDefultSingleItemVO(sectionID);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSectionSingleItemVO>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLBEquipItemVOByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipItemVO> entityList = new EntityList<LBEquipItemVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipItem.QueryLBEquipItemVO(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipItem.QueryLBEquipItemVO(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipItemVO>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLBEquipItemByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipItem> entityList = new EntityList<LBEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipItem.QueryLBEquipItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipItem.QueryLBEquipItem(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipItem>(entityList);
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

        #region LBPhrase 定制服务

        public BaseResultDataValue LS_UDTO_AddDelLBPhrase(IList<LBPhrase> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBPhrase, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除短语错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLBPhraseValue(string phraseType, string typeName, string typeCode, long objectID, string otherWhere)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string fields = "LBPhraseVO_PhraseType, LBPhraseVO_TypeName, LBPhraseVO_TypeCode, LBPhraseVO_CName, LBPhraseVO_Shortcode, LBPhraseVO_PinYinZiTou";
                EntityList<LBPhraseVO> listLBPhraseVO = IBLBPhrase.QueryLBPhraseVO(phraseType, typeName, typeCode, objectID, otherWhere);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBPhraseVO>(listLBPhraseVO);
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
            }
            return baseResultDataValue;
        }

        #endregion

        #region LBRight 定制服务

        public BaseResultDataValue LS_UDTO_AddDelLBRight(IList<LBRight> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBRight, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除检验权限错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryCommonSectionRightByEmpID(string empIDList, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            EntityList<LBSection> entityList = new EntityList<LBSection>();
            try
            {
                entityList = IBLBRight.QueryCommoSectionByEmpID(empIDList);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSection>(entityList);
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

        public BaseResultDataValue LS_UDTO_DelelteEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBLBRight.DelelteEmpSectionDataRight(empIDList, sectionIDList, roleIDList, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "删除失败：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBLBRight.AddEmpSectionDataRight(empIDList, sectionIDList, roleIDList, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增失败：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 专家规则

        public BaseResultDataValue LS_UDTO_QueryLBExpertRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBExpertRule> entityList = new EntityList<LBExpertRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBExpertRule.QueryLBExpertRuleByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBExpertRule.QueryLBExpertRuleByHQL(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBExpertRule>(entityList);
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

        /// <summary>
        /// 复制专家规则并新增
        /// </summary>
        /// <param name="expertRuleID">专家规则ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LBExpertRuleCopyByRuleID(long expertRuleID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBExpertRule.AddNewLBExpertRuleByLBExpertRule(expertRuleID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "专家规则复制失败：" + ex.Message;
            }
            return baseResultDataValue;
        }

        #endregion

        #region 参数表定制服务

        /// <summary>
        /// 查询系统出厂参数设置
        /// </summary>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryFactorySettingPara(string paraTypeCode, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<BPara> entityList = new EntityList<BPara>();
                entityList.list = IBBPara.QueryFactoryParaListByParaClassName(paraTypeCode);
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPara>(entityList);
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

        /// <summary>
        /// 查询系统默认参数设置
        /// </summary>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QuerySystemDefaultPara(string paraTypeCode, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                EntityList<BPara> entityList = new EntityList<BPara>();
                entityList.list = IBBPara.GetSystemDefaultPara(paraTypeCode, empID, empName);
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPara>(entityList);
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

        /// <summary>
        /// 根据参数名称查询相关参数信息
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryParaInfoByParaName(string paraName, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<BaseClassDicEntity> entityList = new EntityList<BaseClassDicEntity>();
                entityList.list = IBBPara.QueryFactoryParaInfoByParaName(paraName);
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BaseClassDicEntity>(entityList);
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

        /// <summary>
        /// 查询系统个性参数设置
        /// </summary>
        /// <param name="where">通用查询条件</param>
        /// <param name="systemTypeCode">参数相关性</param>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QuerySystemParaItem(string where, string systemTypeCode, string paraTypeCode, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<BParaItem> entityList = new EntityList<BParaItem>();
                entityList.list = IBBParaItem.QuerySystemParaItem(where, systemTypeCode, paraTypeCode);
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BParaItem>(entityList);
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

        /// <summary>
        /// 查询个性设置信息列表
        /// 例如：按站点设置的参数，则查询站点列表
        /// </summary>
        /// <param name="systemTypeCode">系统相关性ID</param>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryParaSystemTypeInfo(string systemTypeCode, string paraTypeCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<object> list = IBBParaItem.QueryParaSystemTypeInfo(systemTypeCode, paraTypeCode);
                if (list != null && list.Count > 0)
                    baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(list);

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
        /// 根据参数编码和参数相关性对象ID查找个性化参数值
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="fields">返回属性列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryParaValueByParaNo(string paraNo, string objectID, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BPara entity = IBBParaItem.QueryParaValueByParaNo(paraNo, objectID);
                if (fields == null || fields.Trim().Length == 0)
                    fields = "BPara_ParaValue";
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BPara>(entity);
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

        /// <summary>
        /// 根据参数分类编码和参数相关性对象ID查找个性化参数值
        /// </summary>
        /// <param name="ParaTypeCode">参数分类编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="fields">返回属性列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryParaValueByParaTypeCode(string paraTypeCode, string objectID, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                EntityList<BPara> entityList = new EntityList<BPara>();
                entityList.list = IBBParaItem.QueryParaValueByParaTypeCode(paraTypeCode, objectID, empID, empName);
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPara>(entityList);
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
        /// 保存系统默认参数设置
        /// </summary>
        /// <param name="entityList">默认参数列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_SaveSystemDefaultPara(IList<BPara> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList != null && entityList.Count > 0)
            {
                try
                {
                    string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    baseResultDataValue = IBBPara.AddAndEditPara(entityList, empID, empName);
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

        /// <summary>
        /// 保存系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <param name="entityList">个性参数列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_SaveSystemParaItem(string objectInfo, IList<BParaItem> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList != null && entityList.Count > 0)
            {
                try
                {
                    string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    if (objectInfo != null && objectInfo.Trim().Length > 0 && entityList != null)
                        baseResultDataValue = IBBParaItem.AddAndEditParaItem(objectInfo, entityList, empID, empName);
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

        /// <summary>
        /// 复制系统个性参数设置
        /// </summary>
        /// <param name="fromObjectID">源个性实体ID，例如：从生化检验小组复制参数</param>
        /// <param name="toObjectID">复制到个性实体的ID，例如：复制到免疫检验小组</param>
        /// <param name="toObjectName">复制到个性实体的名称</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddCopySystemParaItem(string fromObjectID, string toObjectID, string toObjectName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (fromObjectID != null && fromObjectID.Trim().Length > 0)
                    baseResultDataValue = IBBParaItem.AddCopyParaItemByObjectID(fromObjectID, toObjectID, toObjectName, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 个性参数值设置为系统默认值---按ObjectID设置
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="paraTypeCode"></param> 
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_SetParaItemDefaultValue(string objectID, string paraTypeCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (objectID != null && objectID.Trim().Length > 0)
                    baseResultDataValue = IBBParaItem.EditParaItemDefaultValueByObjectID(objectID, paraTypeCode, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 删除系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_DeleteSystemParaItem(string objectInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (objectInfo != null && objectInfo.Trim().Length > 0)
                    baseResultDataValue = IBBParaItem.DeleteParaItemByObjectID(objectInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 导入导出服务

        //基础表数据导入
        public Message LS_UDTO_UploadBaseTableDataByExcelFile()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            
            try
            {
                int iTotal = 0;
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_UploadBaseTableDataByExcelFile上传开始！");
                iTotal = HttpContext.Current.Request.Files.Count;
                ZhiFang.Common.Log.Log.Debug("上传文件数量:" + iTotal);
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue), "text/plain", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("LS_UDTO_UploadBaseTableDataByExcelFile错误:" + ex.Message);
                return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue), "text/plain", Encoding.UTF8);
            }

            try
            {
                string entityName = HttpContext.Current.Request.Form["entityName"];
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }

                    string filepath = Path.Combine(parentPath, ZhiFang.Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    switch (entityName.ToLower())
                    {
                        case "lbitem":
                            baseResultDataValue = IBLisCommon.AddEntityDataFormByExcelFile(entityName, "项目信息", IBLBItem, filepath, HttpContext.Current.Server.MapPath("~/"));
                            break;
                        case "lbequip":
                            baseResultDataValue = IBLisCommon.AddEntityDataFormByExcelFile(entityName, "仪器信息", IBLBEquip, filepath, HttpContext.Current.Server.MapPath("~/"));
                            break;
                        case "lbspecialty":
                            baseResultDataValue = IBLisCommon.AddEntityDataFormByExcelFile(entityName, "专业信息", IBLBSpecialty, filepath, HttpContext.Current.Server.MapPath("~/"));
                            break;
                        case "lbsampletype":
                            baseResultDataValue = IBLisCommon.AddEntityDataFormByExcelFile(entityName, "样本类型信息", IBLBSampleType, filepath, HttpContext.Current.Server.MapPath("~/"));
                            break;
                    }
                    
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                };
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("LS_UDTO_UploadBaseTableDataByExcelFile错误:" + ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //

        public Message LS_UDTO_OutputBaseTableDataExcelFilePath()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string entityName = "";
            string idList = "";
            string where = "";
            string isHeader = "";
            string sort = "";
            string tempFileName = "";
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            DataSet ds = null;

            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "entityName":
                        entityName = HttpContext.Current.Request.Form["entityName"];
                        break;
                    case "idList":
                        idList = HttpContext.Current.Request.Form["idList"];
                        break;
                    case "where":
                        where = HttpContext.Current.Request.Form["where"];
                        break;
                    case "isHeader":
                        isHeader = HttpContext.Current.Request.Form["isHeader"];
                        break;
                    case "sort":
                        sort = HttpContext.Current.Request.Form["sort"];
                        break;
                }
            }
            try
            {

                string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + ZhiFang.LabStar.Common.ExcelDataCommon.GetExcelExtName();
                string tempFilePath = basePath + "\\TempExcelFile\\" + excelName;
                if (!Directory.Exists(basePath + "\\TempExcelFile"))
                {
                    Directory.CreateDirectory(basePath + "\\TempExcelFile");
                }
                switch (entityName.ToLower())
                {
                    case "lbitem":
                        tempFileName = "项目信息列表";
                        ds = IBLisCommon.QueryEntityDataInfo(entityName, IBLBItem, idList, where, sort, HttpContext.Current.Server.MapPath("~/"));
                        break;
                    case "lbequip":
                        tempFileName = "仪器信息列表";
                        ds = IBLisCommon.QueryEntityDataInfo(entityName, IBLBEquip, idList, where, sort, HttpContext.Current.Server.MapPath("~/"));
                        break;
                    case "lbspecialty":
                        tempFileName = "专业信息列表";
                        ds = IBLisCommon.QueryEntityDataInfo(entityName, IBLBSpecialty, idList, where, sort, HttpContext.Current.Server.MapPath("~/"));
                        break;
                    case "lbsampletype":
                        tempFileName = "样本类型信息列表";
                        ds = IBLisCommon.QueryEntityDataInfo(entityName, IBLBSampleType, idList, where, sort, HttpContext.Current.Server.MapPath("~/"));
                        break;
                }
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string headerText = "";
                    if (isHeader == "1")
                        headerText = tempFileName;

                    if (!ZhiFang.LabStar.Common.ExcelDataCommon.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
                    {
                        tempFilePath = "";
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "生成Excel文件失败！";
                    }
                }
                else
                {
                    tempFilePath = "";
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无任何要导出的记录信息！";
                }
                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    baseResultDataValue.ResultDataValue = "/TempExcelFile/" + excelName;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        public Stream LS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType)
        {
            FileStream tempFileStream = null;
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string extName = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(downFileName))
                    downFileName = "错误信息文件" + extName;
                else
                    downFileName = downFileName + extName;
                string tempFilePath = basePath + "\\UploadBaseTableInfo\\" + fileName;
                if (isUpLoadFile == 1)
                    tempFilePath = basePath + "\\TempExcelFile\\" + fileName;

                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + downFileName);
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                    }
                    else if (operateType == 1)//直接打开PDF文件
                    {
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/vnd.ms-excel";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + downFileName);
                        //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                tempFileStream = null;
                throw new Exception(ex.Message);
            }
            return tempFileStream;
        }

        #endregion

        #region 历史对比

        public BaseResultDataValue LS_UDTO_AddDelLBSectionHisComp(IList<LBSectionHisComp> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "IsUse", true }, { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBSectionHisComp, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除小组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        #endregion

        #region 字典对照定制服务
        public BaseResultDataValue LS_UDTO_SearchLBSickTypeDicContrastNumByHQL(long SectionID, int GroupType, string CName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                DataTable dataTable = IBLBSickType.LS_UDTO_SearchLBSickTypeDicContrastNumByHQL(SectionID, GroupType, CName);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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

        public BaseResultDataValue LS_UDTO_SearchLBItemAndLBItemCodeLink(long SectionID, int GroupType, long SickTypeID, string ItemCName, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                EntityList<LBItemCodeLinkVO> entityList = new EntityList<LBItemCodeLinkVO>();
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItemCodeLink.LS_UDTO_SearchLBItemAndLBItemCodeLink(SectionID, GroupType, SickTypeID, ItemCName, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBItemCodeLink.LS_UDTO_SearchLBItemAndLBItemCodeLink(SectionID, GroupType, SickTypeID, ItemCName, "", page, limit);
                }
                if (entityList.count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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

        public BaseResultDataValue LS_UDTO_AddOrUPDateLBItemCodeLink(LBItemCodeLinkVO entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (entity == null || entity.LinkDicDataCode == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数不可为空!";
                    return baseResultDataValue;
                }
                IList<LBItemCodeLink> lBItemCodeLinks = IBLBItemCodeLink.SearchListByHQL("LinkSystemID = " + entity.LinkSystemID + " and DicDataID = '" + entity.DicDataID + "' and LinkDicDataCode='" + entity.LinkDicDataCode + "'");
                if (lBItemCodeLinks.Count > 0 && lBItemCodeLinks.First().Id != entity.Id)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "同项目编码不可重复!";
                    return baseResultDataValue;
                }
                if (entity.Id == 0)
                {
                    LBItemCodeLink lBItemCodeLink = Common.ClassMapperHelp.GetMapper<LBItemCodeLink, LBItemCodeLinkVO>(entity);
                    lBItemCodeLink.DataAddTime = new DateTime();
                    lBItemCodeLink.Id = GUIDHelp.GetGUIDLong();
                    IBLBItemCodeLink.Entity = lBItemCodeLink;
                    baseResultDataValue.success = IBLBItemCodeLink.Add();
                }
                else
                {
                    LBItemCodeLink lBItemCodeLink = Common.ClassMapperHelp.GetMapper<LBItemCodeLink, LBItemCodeLinkVO>(entity);
                    var fields = "Id,DataUpdateTime,LinkDicDataCode,LinkDicDataName,TransTypeID";
                    entity.DataUpdateTime = DateTime.Now;
                    IBLBItemCodeLink.Entity = lBItemCodeLink;
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBItemCodeLink.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultDataValue.success = IBLBItemCodeLink.Update(tempArray);
                    }
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

        public BaseResultDataValue LS_UDTO_SearchLBItemBySickTypeID(long SectionID, int GroupType, long SickTypeID, string ItemCName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                List<LBItem> list = IBLBItemCodeLink.LS_UDTO_SearchLBItemAndLBItemBySickTypeID(SectionID, GroupType, SickTypeID, ItemCName);
                if (list.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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

        public BaseResultDataValue LS_UDTO_SearchLBSickTypeOtherDicContrastNum(string ContrastDicType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (string.IsNullOrEmpty(ContrastDicType))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "字典类型为必选项！";
                    return baseResultDataValue;
                }
                DataTable dataTable = IBLBSickType.LS_UDTO_SearchLBSickTypeOtherDicContrastNum(ContrastDicType);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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

        public BaseResultDataValue LS_UDTO_SearchBasicsDicAndLBDicCodeLink(long SickTypeId, string ContrastDicType, string DicInfo, string CName, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBDicCodeLink.SearchBasicsDicAndLBDicCodeLink(SickTypeId, ContrastDicType, DicInfo, CName, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBDicCodeLink.SearchBasicsDicAndLBDicCodeLink(SickTypeId, ContrastDicType, DicInfo, CName, "", page, limit);
                }
                if (entityList.count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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

        public BaseResultDataValue LS_UDTO_AddOrUPDateLBDicCodeLink(LBDicCodeLinkVO entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (entity == null || entity.LinkDicDataCode == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数不可为空!";
                    return baseResultDataValue;
                }
                IList<LBDicCodeLink> lBDicCodeLinks = IBLBDicCodeLink.SearchListByHQL("DicTypeCode='" + entity.DicDataID + "' and LinkSystemID = " + entity.LinkSystemID + " and DicDataID = '" + entity.DicDataID + "' and LinkDicDataCode='" + entity.LinkDicDataCode + "'");
                if (lBDicCodeLinks.Count > 0 && lBDicCodeLinks.First().Id != entity.Id)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "对照编码不可重复!";
                    return baseResultDataValue;

                }

                var dictype = ContrastDicType.GetStatusDic().Where(a => a.Value.Id == entity.DicType).First();
                entity.DicTypeCode = dictype.Value.Code;
                entity.DicTypeName = dictype.Value.Name;
                if (entity.Id == 0)
                {
                    LBDicCodeLink lBDicCodeLink = Common.ClassMapperHelp.GetMapper<LBDicCodeLink, LBDicCodeLinkVO>(entity);
                    lBDicCodeLink.DataAddTime = new DateTime();
                    lBDicCodeLink.Id = GUIDHelp.GetGUIDLong();
                    IBLBDicCodeLink.Entity = lBDicCodeLink;
                    baseResultDataValue.success = IBLBDicCodeLink.Add();
                }
                else
                {
                    LBDicCodeLink lBDicCodeLink = Common.ClassMapperHelp.GetMapper<LBDicCodeLink, LBDicCodeLinkVO>(entity);
                    var fields = "Id,DataUpdateTime,LinkDicDataCode,LinkDicDataName,TransTypeID";
                    entity.DataUpdateTime = DateTime.Now;
                    IBLBDicCodeLink.Entity = lBDicCodeLink;
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBDicCodeLink.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultDataValue.success = IBLBDicCodeLink.Update(tempArray);
                    }
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
        public BaseResultDataValue LS_UDTO_SearchBasicsDicDataBySickTypeId(long SickTypeId, string ContrastDicType, string DicInfo, string CName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
                entityList = IBLBDicCodeLink.SearchBasicsDicDataBySickTypeId(SickTypeId, ContrastDicType, DicInfo, CName);
                if (entityList.count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList.list);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
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
        public BaseResultDataValue LS_UDTO_CopyLBItemCodeLinkContrast(string SickTypeIds, string thisSickTypeId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBItemCodeLink.AddCopyLBItemCodeLinkContrast(SickTypeIds, thisSickTypeId);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_CopyLBDicCodeLinkContrast(string SickTypeIds, string thisSickTypeId, string dictype)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBDicCodeLink.AddCopyLBDicCodeLinkContrast(SickTypeIds, thisSickTypeId, dictype);
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

        #region 打印模板定制服务
        public BaseResultDataValue LS_UDTO_SearchBPrintModelAndAutoUploadModel(string BusinessTypeCode, string ModelTypeCode, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<BPrintModelVO> entityList = new EntityList<BPrintModelVO>();
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBPrintModel.LS_UDTO_SearchBPrintModelAndAutoUploadModel(BusinessTypeCode, ModelTypeCode, where, CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    entityList = IBBPrintModel.LS_UDTO_SearchBPrintModelAndAutoUploadModel(BusinessTypeCode, ModelTypeCode, where, "");
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BPrintModelVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询模板异常：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_UploadReportModel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                #region 验证
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsConstraintUpload") && HttpContext.Current.Request.Form["IsConstraintUpload"] != null && HttpContext.Current.Request.Form["IsConstraintUpload"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否上传标记不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("BusinessTypeCode") && HttpContext.Current.Request.Form["BusinessTypeCode"] != null && HttpContext.Current.Request.Form["BusinessTypeCode"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "业务类型编码不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("ModelTypeCode") && HttpContext.Current.Request.Form["ModelTypeCode"] != null && HttpContext.Current.Request.Form["ModelTypeCode"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "模板类型编码不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("FileName") && HttpContext.Current.Request.Form["FileName"] != null && HttpContext.Current.Request.Form["FileName"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "文件名称不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsProtect") && HttpContext.Current.Request.Form["IsProtect"] != null && HttpContext.Current.Request.Form["IsProtect"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否加强保护不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsUse") && HttpContext.Current.Request.Form["IsUse"] != null && HttpContext.Current.Request.Form["IsUse"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否使用不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                #endregion
                #region 数据获取
                string BusinessTypeCode = HttpContext.Current.Request.Form.GetValues("BusinessTypeCode")[0];
                string ModelTypeCode = HttpContext.Current.Request.Form.GetValues("ModelTypeCode")[0];
                string FileName = HttpContext.Current.Request.Form.GetValues("FileName")[0];
                int IsUse = int.Parse(HttpContext.Current.Request.Form.GetValues("IsUse")[0]);
                int IsProtect = int.Parse(HttpContext.Current.Request.Form.GetValues("IsProtect")[0]);
                string IsConstraintUpload = HttpContext.Current.Request.Form.GetValues("IsConstraintUpload")[0];//是否强制上传标记

                int DispOrder = 0;
                if (HttpContext.Current.Request.Form.AllKeys.Contains("DispOrder") && HttpContext.Current.Request.Form["DispOrder"] != null && HttpContext.Current.Request.Form["DispOrder"].Trim() != "")
                {
                    DispOrder = int.Parse(HttpContext.Current.Request.Form.GetValues("DispOrder")[0]);
                }
                string FileComment = "";
                if (HttpContext.Current.Request.Form.AllKeys.Contains("FileComment") && HttpContext.Current.Request.Form["FileComment"] != null && HttpContext.Current.Request.Form["FileComment"].Trim() != "")
                {
                    FileComment = HttpContext.Current.Request.Form.GetValues("FileComment")[0];
                }
                string labid = ZhiFang.Common.Public.Cookie.Get(SysPublicSet.SysDicCookieSession.LabID);
                string empname = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeName);//人员姓名
                string empid = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeID);//人员ID
                var BusinessType = PrintModelBusinessType.GetStatusDic();
                var ModelType = PrintModelModelType.GetStatusDic();
                KeyValuePair<string, BaseClassDicEntity> btdic = BusinessType.Where(a => a.Key == BusinessTypeCode).First();
                KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == ModelTypeCode).First();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
                #endregion
                if (IsConstraintUpload == "0")
                {
                    #region 文件重名验证与新增
                    string dicpath = path;
                    if (!Directory.Exists(dicpath))
                    {
                        Directory.CreateDirectory(dicpath);
                    }
                    dicpath = path + @"\" + btdic.Value.Code;
                    if (!Directory.Exists(dicpath))
                    {
                        Directory.CreateDirectory(dicpath);
                    }
                    dicpath = path + @"\" + btdic.Value.Code + @"\" + mtdic.Value.Code;
                    if (!Directory.Exists(dicpath))
                    {
                        Directory.CreateDirectory(dicpath);
                    }
                    dicpath = path + @"\" + btdic.Value.Code + @"\" + mtdic.Value.Code + @"\" + labid;
                    if (!Directory.Exists(dicpath))
                    {
                        Directory.CreateDirectory(dicpath);
                    }
                    dicpath = path + @"\" + btdic.Value.Code + @"\" + mtdic.Value.Code + @"\" + labid + @"\" + FileName;
                    if (File.Exists(dicpath))
                    {
                        baseResultDataValue.ErrorInfo = "同名文件已存在";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    else
                    {
                        //获得需要打印的质控图
                        for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        {
                            var item = HttpContext.Current.Request.Files[i];
                            item.SaveAs(path + @"\" + btdic.Value.Code + @"\" + mtdic.Value.Code + @"\" + labid + @"\" + item.FileName);
                        }
                        BPrintModel bPrintModel = new BPrintModel();
                        bPrintModel.BusinessTypeName = btdic.Value.Code;
                        bPrintModel.BusinessTypeCode = btdic.Value.Id;
                        bPrintModel.ModelTypeName = mtdic.Value.Code;
                        bPrintModel.ModelTypeCode = mtdic.Value.Id;
                        bPrintModel.FileName = FileName;
                        bPrintModel.FileComment = FileComment;
                        bPrintModel.DispOrder = DispOrder;
                        bPrintModel.OperaterID = long.Parse(empid);
                        bPrintModel.Operater = empname;
                        bPrintModel.FinalOperaterID = long.Parse(empid);
                        bPrintModel.FinalOperater = empname;
                        bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                        bPrintModel.IsUse = IsUse == 0 ? false : true;
                        bPrintModel.FileUploadTime = DateTime.Now;
                        bPrintModel.DataAddTime = DateTime.Now;
                        IBBPrintModel.Entity = bPrintModel;
                        bool isok = IBBPrintModel.Add();
                        if (isok)
                        {
                            baseResultDataValue.success = true;
                        }
                        else
                        {
                            baseResultDataValue.ErrorInfo = "数据新增失败！";
                            baseResultDataValue.success = false;
                        }
                    }
                    #endregion
                }
                else if (IsConstraintUpload == "1")
                {
                    #region 文件新增
                    var item = HttpContext.Current.Request.Files[0];
                    string newfilename = FileName;
                    string addfilename = IBBPrintModel.FileNameReconsitution(path, btdic.Value.Code, mtdic.Value.Code, labid, FileName, out newfilename);
                    item.SaveAs(addfilename);
                    BPrintModel bPrintModel = new BPrintModel();
                    bPrintModel.BusinessTypeName = btdic.Value.Code;
                    bPrintModel.BusinessTypeCode = btdic.Value.Id;
                    bPrintModel.ModelTypeName = mtdic.Value.Code;
                    bPrintModel.ModelTypeCode = mtdic.Value.Id;
                    bPrintModel.FileName = newfilename;
                    bPrintModel.FileComment = FileComment;
                    bPrintModel.DispOrder = DispOrder;
                    bPrintModel.OperaterID = long.Parse(empid);
                    bPrintModel.Operater = empname;
                    bPrintModel.FinalOperaterID = long.Parse(empid);
                    bPrintModel.FinalOperater = empname;
                    bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                    bPrintModel.IsUse = IsUse == 0 ? false : true;
                    bPrintModel.FileUploadTime = DateTime.Now;
                    bPrintModel.DataAddTime = DateTime.Now;
                    IBBPrintModel.Entity = bPrintModel;
                    bool isok = IBBPrintModel.Add();
                    if (isok)
                    {
                        baseResultDataValue.success = true;
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "数据新增失败！";
                        baseResultDataValue.success = false;
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_UploadReportModel.异常：" + e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_UpdateReportModel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                #region 验证
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("Id") && HttpContext.Current.Request.Form["Id"] != null && HttpContext.Current.Request.Form["Id"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "Id不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsProtect") && HttpContext.Current.Request.Form["IsProtect"] != null && HttpContext.Current.Request.Form["IsProtect"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否加强保护不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsUse") && HttpContext.Current.Request.Form["IsUse"] != null && HttpContext.Current.Request.Form["IsUse"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否使用不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!(HttpContext.Current.Request.Form.AllKeys.Contains("IsConstraintUpload") && HttpContext.Current.Request.Form["IsConstraintUpload"] != null && HttpContext.Current.Request.Form["IsConstraintUpload"].Trim() != ""))
                {
                    baseResultDataValue.ErrorInfo = "是否上传标记不可为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                #endregion
                #region 数据重组与更新
                string Id = HttpContext.Current.Request.Form.GetValues("Id")[0];
                int IsUse = int.Parse(HttpContext.Current.Request.Form.GetValues("IsUse")[0]);
                int IsProtect = int.Parse(HttpContext.Current.Request.Form.GetValues("IsProtect")[0]);
                int DispOrder = 0;
                if (HttpContext.Current.Request.Form.AllKeys.Contains("DispOrder") && HttpContext.Current.Request.Form["DispOrder"] != null && HttpContext.Current.Request.Form["DispOrder"].Trim() != "")
                {
                    DispOrder = int.Parse(HttpContext.Current.Request.Form.GetValues("DispOrder")[0]);
                }
                string FileComment = "";
                if (HttpContext.Current.Request.Form.AllKeys.Contains("FileComment") && HttpContext.Current.Request.Form["FileComment"] != null && HttpContext.Current.Request.Form["FileComment"].Trim() != "")
                {
                    FileComment = HttpContext.Current.Request.Form.GetValues("FileComment")[0];
                }
                string FileName = "";
                if (HttpContext.Current.Request.Form.AllKeys.Contains("FileName") && HttpContext.Current.Request.Form["FileName"] != null && HttpContext.Current.Request.Form["FileName"].Trim() != "")
                {
                    FileName = HttpContext.Current.Request.Form.GetValues("FileName")[0];
                }
                string labid = ZhiFang.Common.Public.Cookie.Get(SysPublicSet.SysDicCookieSession.LabID);
                string empname = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeName);//人员姓名
                string empid = ZhiFang.Common.Public.Cookie.Get(DicCookieSession.EmployeeID);//人员ID
                string IsConstraintUpload = HttpContext.Current.Request.Form.GetValues("IsConstraintUpload")[0];//是否强制上传标记
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
                #endregion

                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    #region 直接数据库数据修改
                    BPrintModel bPrintModel = new BPrintModel();
                    bPrintModel.Id = long.Parse(Id);
                    bPrintModel.FileComment = FileComment;
                    bPrintModel.DispOrder = DispOrder;
                    bPrintModel.FinalOperaterID = long.Parse(empid);
                    bPrintModel.FinalOperater = empname;
                    bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                    bPrintModel.IsUse = IsUse == 0 ? false : true;
                    bPrintModel.DataUpdateTime = DateTime.Now;
                    IBBPrintModel.Entity = bPrintModel;
                    string fields = "DataUpdateTime,IsUse,Id,IsProtect,FileComment,DispOrder,FinalOperaterID,FinalOperater";
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPrintModel.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultDataValue.success = IBBPrintModel.Update(tempArray);
                    }
                    #endregion
                }
                else
                {
                    #region 文件存在时的处理
                    BPrintModel bPrint = IBBPrintModel.Get(long.Parse(Id));
                    string oldpath = path + @"\" + bPrint.BusinessTypeName + @"\" + bPrint.ModelTypeName + @"\" + bPrint.LabID + @"\" + bPrint.FileName;
                    var item = HttpContext.Current.Request.Files[0];
                    string newpath = path + @"\" + bPrint.BusinessTypeName + @"\" + bPrint.ModelTypeName + @"\" + bPrint.LabID + @"\" + item.FileName;
                    if (oldpath == newpath)
                    {
                        if (File.Exists(oldpath))
                        {
                            File.Delete(oldpath);
                        }
                        item.SaveAs(newpath);
                        BPrintModel bPrintModel = new BPrintModel();
                        bPrintModel.Id = long.Parse(Id);
                        bPrintModel.FileComment = FileComment;
                        bPrintModel.DispOrder = DispOrder;
                        bPrintModel.FinalOperaterID = long.Parse(empid);
                        bPrintModel.FinalOperater = empname;
                        bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                        bPrintModel.IsUse = IsUse == 0 ? false : true;
                        bPrintModel.DataUpdateTime = DateTime.Now;
                        IBBPrintModel.Entity = bPrintModel;
                        string fields = "DataUpdateTime,IsUse,Id,IsProtect,FileComment,DispOrder,FinalOperaterID,FinalOperater";
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPrintModel.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultDataValue.success = IBBPrintModel.Update(tempArray);
                        }
                    }
                    else
                    {
                        if (File.Exists(newpath))
                        {
                            if (IsConstraintUpload == "0")
                            {
                                baseResultDataValue.ErrorInfo = "同名文件已存在";
                                baseResultDataValue.success = false;
                                return baseResultDataValue;
                            }
                            else if (IsConstraintUpload == "1")
                            {
                                if (File.Exists(oldpath))
                                {
                                    File.Delete(oldpath);
                                }
                                string nfilename = "";
                                newpath = IBBPrintModel.FileNameReconsitution(path, bPrint.BusinessTypeName, bPrint.ModelTypeName, bPrint.LabID.ToString(), bPrint.FileName, out nfilename);
                                item.SaveAs(newpath);
                                BPrintModel bPrintModel = new BPrintModel();
                                bPrintModel.Id = long.Parse(Id);
                                bPrintModel.FileName = nfilename;
                                bPrintModel.FileComment = FileComment;
                                bPrintModel.DispOrder = DispOrder;
                                bPrintModel.FinalOperaterID = long.Parse(empid);
                                bPrintModel.FinalOperater = empname;
                                bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                                bPrintModel.IsUse = IsUse == 0 ? false : true;
                                bPrintModel.DataUpdateTime = DateTime.Now;
                                IBBPrintModel.Entity = bPrintModel;
                                string fields = "DataUpdateTime,IsUse,Id,IsProtect,FileComment,DispOrder,FinalOperaterID,FinalOperater,FileName";
                                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPrintModel.Entity, fields);
                                if (tempArray != null)
                                {
                                    baseResultDataValue.success = IBBPrintModel.Update(tempArray);
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(oldpath))
                            {
                                File.Delete(oldpath);
                            }
                            item.SaveAs(newpath);
                            BPrintModel bPrintModel = new BPrintModel();
                            bPrintModel.Id = long.Parse(Id);
                            bPrintModel.FileName = FileName;
                            bPrintModel.FileComment = FileComment;
                            bPrintModel.DispOrder = DispOrder;
                            bPrintModel.FinalOperaterID = long.Parse(empid);
                            bPrintModel.FinalOperater = empname;
                            bPrintModel.IsProtect = IsProtect == 0 ? false : true;
                            bPrintModel.IsUse = IsUse == 0 ? false : true;
                            bPrintModel.DataUpdateTime = DateTime.Now;
                            IBBPrintModel.Entity = bPrintModel;
                            string fields = "DataUpdateTime,IsUse,Id,IsProtect,FileComment,DispOrder,FinalOperaterID,FinalOperater,FileName";
                            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBPrintModel.Entity, fields);
                            if (tempArray != null)
                            {
                                baseResultDataValue.success = IBBPrintModel.Update(tempArray);
                            }

                        }
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }

        public BaseResultBool LS_UDTO_DelReportModelById(long Id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBPrintModel.Entity = IBBPrintModel.Get(Id);
                if (IBBPrintModel.Entity != null)
                {
                    var BusinessType = PrintModelBusinessType.GetStatusDic();
                    var ModelType = PrintModelModelType.GetStatusDic();
                    KeyValuePair<string, BaseClassDicEntity> btdic = BusinessType.Where(a => a.Key == IBBPrintModel.Entity.BusinessTypeCode).First();
                    KeyValuePair<string, BaseClassDicEntity> mtdic = ModelType.Where(a => a.Key == IBBPrintModel.Entity.ModelTypeCode).First();
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template");
                    string dicpath = path + @"\" + btdic.Value.Code + @"\" + mtdic.Value.Code + @"\" + IBBPrintModel.Entity.LabID + @"\" + IBBPrintModel.Entity.FileName;
                    if (File.Exists(dicpath))
                    {
                        File.Delete(dicpath);
                    }
                    long labid = IBBPrintModel.Entity.LabID;
                    string entityName = IBBPrintModel.Entity.GetType().Name;
                    baseResultBool.success = IBBPrintModel.RemoveByHQL(Id);
                    if (baseResultBool.success)
                    {
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

        public BaseResultDataValue LS_UDTO_PrintDataByPrintModel(string Data, long PrintModelID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(Data) || PrintModelID == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "数据和模板ID不允许为空";
                    return baseResultDataValue;
                }
                string pdfpath = IBBPrintModel.PrintDataByPrintModel(Data, PrintModelID);
                if (string.IsNullOrEmpty(pdfpath))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "生成失败";
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = pdfpath;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PrintDataByPrintModel.异常：" + ex.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_ExportDataByPrintModel(string Data, long PrintModelID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(Data) || PrintModelID == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "数据和模板ID不允许为空";
                    return baseResultDataValue;
                }
                string pdfpath = IBBPrintModel.ExportDataByPrintModel(Data, PrintModelID);
                if (string.IsNullOrEmpty(pdfpath))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "生成失败";
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = pdfpath;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_ExportDataByPrintModel.异常：" + ex.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
            }
            return baseResultDataValue;
        }
        #endregion
        #endregion

    }
}
