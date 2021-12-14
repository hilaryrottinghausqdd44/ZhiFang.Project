using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LIIP;
using ZhiFang.IBLL.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LIIPCommonService : ILIIPCommonService
    {
        IBBHospital IBBHospital { get; set; }
        IBSLog IBSLog { get; set; }

        IBBHospitalArea IBBHospitalArea { get; set; }

        IBBHospitalDept IBBHospitalDept { get; set; }

        IBBHospitalLevel IBBHospitalLevel { get; set; }

        IBBHospitalType IBBHospitalType { get; set; }

        IBBHospitalTypeLink IBBHospitalTypeLink { get; set; }

        IBBHospitalEmpLink IBBHospitalEmpLink { get; set; }

        ZhiFang.IBLL.Common.IBBParameter IBBParameter { get; set; }

        IBSAccountRegister IBSAccountRegister { get; set; }
        IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        #region BHospital
        //Add  BHospital
        public BaseResultDataValue ST_UDTO_AddBHospital(BHospital entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = IBBHospital.SaveEntity(entity);
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
        //Update  BHospital
        public BaseResultBool ST_UDTO_UpdateBHospital(BHospital entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospital.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospital.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospital
        public BaseResultBool ST_UDTO_UpdateBHospitalByField(BHospital entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            var empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeName);
            var empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(Entity.RBAC.DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(empname) || string.IsNullOrEmpty(empid))
            {
                return new BaseResultBool() { success = false, ErrorInfo = "未能找到当前登录者身份信息！请重新登陆！" };
            }
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime,EditerName,EditerId";
                entity.DataUpdateTime = DateTime.Now;
                entity.EditerName = empname;
                entity.EditerId = long.Parse(empid);

                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] f = fields.Split(',');
                        if (f.Contains("Name"))
                        {
                            entity.PinYinZiTou = "";
                            entity.Shortcode = "";
                            if (!string.IsNullOrEmpty(entity.Name))
                            {
                                entity.PinYinZiTou = ZhiFang.LIIP.Common.PinYinConverter.GetFirst(entity.Name);
                                entity.Shortcode = ZhiFang.LIIP.Common.PinYinConverter.GetFirst(entity.Name);
                            }
                            if (!f.Contains("PinYinZiTou"))
                            {
                                f.Append("PinYinZiTou");
                            }
                            if (!f.Contains("Shortcode"))
                            {
                                f.Append("Shortcode");
                            }
                        }
                        fields = string.Join(",", f);
                        IBBHospital.Entity = entity;
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospital.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospital.Update(tempArray);
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
                        //baseResultBool.success = IBBHospital.Edit();
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
        //Delele  BHospital
        public BaseResultBool ST_UDTO_DelBHospital(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospital.Entity = IBBHospital.Get(id);
                if (IBBHospital.Entity != null)
                {
                    long labid = IBBHospital.Entity.LabID;
                    string entityName = IBBHospital.Entity.GetType().Name;
                    baseResultBool.success = IBBHospital.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBHospital(BHospital entity)
        {
            EntityList<BHospital> entityList = new EntityList<BHospital>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospital.Entity = entity;
                try
                {
                    entityList.list = IBBHospital.Search();
                    entityList.count = IBBHospital.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospital>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospital> entityList = new EntityList<BHospital>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospital.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospital.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospital>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospital.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospital>(entity);
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

        #region BHospitalArea
        //Add  BHospitalArea
        public BaseResultDataValue ST_UDTO_AddBHospitalArea(BHospitalArea entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalArea.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalArea.Add();
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
        //Update  BHospitalArea
        public BaseResultBool ST_UDTO_UpdateBHospitalArea(BHospitalArea entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalArea.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospitalArea.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospitalArea
        public BaseResultBool ST_UDTO_UpdateBHospitalAreaByField(BHospitalArea entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalArea.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalArea.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalArea.Update(tempArray);
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
                        //baseResultBool.success = IBBHospitalArea.Edit();
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
        //Delele  BHospitalArea
        public BaseResultBool ST_UDTO_DelBHospitalArea(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                var dd = IBBHospitalArea.SearchListByHQL("PHospitalAreaID=" + id);
                if (dd.Count > 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "有子项不可删除！";
                    return baseResultBool;
                }
                BHospitalArea entity = IBBHospitalArea.Get(id);
                entity.DataUpdateTime = DateTime.Now;
                entity.IsUse = false;
                IBBHospitalArea.Entity = entity;
                if (IBBHospitalArea.Entity != null)
                {
                    long labid = IBBHospitalArea.Entity.LabID;
                    string entityName = IBBHospitalArea.Entity.GetType().Name;
                    string fields = "Id,DataUpdateTime,IsUse";
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalArea.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultBool.success = IBBHospitalArea.Update(tempArray);
                        if (baseResultBool.success)
                        {
                            //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                        }
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalArea(BHospitalArea entity)
        {
            EntityList<BHospitalArea> entityList = new EntityList<BHospitalArea>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospitalArea.Entity = entity;
                try
                {
                    entityList.list = IBBHospitalArea.Search();
                    entityList.count = IBBHospitalArea.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalArea>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalAreaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalArea> entityList = new EntityList<BHospitalArea>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalArea.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalArea.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalArea>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalAreaById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalArea.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalArea>(entity);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalAreaFiltrationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<long> ids = new List<long>();
                ids.Add(id);

                List<long> sonIds = IBBHospitalArea.SearchListFiltrationById(ids);
                foreach (var item in sonIds)
                {
                    ids.Add(item);
                }

                string strwhere = " Id not in (";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0)
                    {
                        strwhere += ids[i];
                    }
                    else
                    {
                        strwhere += "," + ids[i];
                    }
                }
                strwhere += ")";
                IList<BHospitalArea> entityList = IBBHospitalArea.SearchListByHQL(strwhere);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalArea>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_UpdateBHospitalAreaLevelNameTreeByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<BHospitalArea> sonIds = IBBHospitalArea.SearchBHospitalAreaLevelNameTreeByID(id);
                try
                {
                    //声明要修改的参数
                    string fields = "Id,HospitalAreaLevelName";
                    BHospitalArea entity = new BHospitalArea();
                    if (!string.IsNullOrEmpty(fields))
                        fields = fields + ",DataUpdateTime";
                    entity.DataUpdateTime = DateTime.Now;
                    entity.Id = id;
                    //拼接HospitalAreaLevelName字段值
                    string NameStr = "";
                    for (int i = sonIds.Count - 1; i >= 0; i--)
                    {
                        NameStr += sonIds[i].Name + "_";
                    }
                    entity.HospitalAreaLevelName = NameStr.Substring(0, NameStr.Length - 1);
                    IBBHospitalArea.Entity = entity;
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalArea.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultDataValue.success = IBBHospitalArea.Update(tempArray);
                        if (baseResultDataValue.success)
                        {
                            baseResultDataValue.ResultDataValue = NameStr.Substring(0, NameStr.Length - 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBHospitalAreaSonByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<long> ids = new List<long>();
                ids.Add(id);
                List<long> entityList = IBBHospitalArea.SearchListFiltrationById(ids);
                entityList.Add(id);
                string idstr = "";
                foreach (var item in entityList)
                {
                    idstr += item + ",";
                }
                baseResultDataValue.ResultDataValue = idstr.Substring(0, idstr.Length - 1);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return baseResultDataValue;
        }

        #endregion

        #region BHospitalDept
        //Add  BHospitalDept
        public BaseResultDataValue ST_UDTO_AddBHospitalDept(BHospitalDept entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalDept.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalDept.Add();
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
        //Update  BHospitalDept
        public BaseResultBool ST_UDTO_UpdateBHospitalDept(BHospitalDept entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalDept.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospitalDept.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospitalDept
        public BaseResultBool ST_UDTO_UpdateBHospitalDeptByField(BHospitalDept entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalDept.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalDept.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalDept.Update(tempArray);
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
                        //baseResultBool.success = IBBHospitalDept.Edit();
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
        //Delele  BHospitalDept
        public BaseResultBool ST_UDTO_DelBHospitalDept(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospitalDept.Entity = IBBHospitalDept.Get(id);
                if (IBBHospitalDept.Entity != null)
                {
                    long labid = IBBHospitalDept.Entity.LabID;
                    string entityName = IBBHospitalDept.Entity.GetType().Name;
                    baseResultBool.success = IBBHospitalDept.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalDept(BHospitalDept entity)
        {
            EntityList<BHospitalDept> entityList = new EntityList<BHospitalDept>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospitalDept.Entity = entity;
                try
                {
                    entityList.list = IBBHospitalDept.Search();
                    entityList.count = IBBHospitalDept.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalDept>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalDeptByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalDept> entityList = new EntityList<BHospitalDept>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalDept.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalDept.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalDept>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalDeptById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalDept.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalDept>(entity);
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

        #region BHospitalLevel
        //Add  BHospitalLevel
        public BaseResultDataValue ST_UDTO_AddBHospitalLevel(BHospitalLevel entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalLevel.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalLevel.Add();
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
        //Update  BHospitalLevel
        public BaseResultBool ST_UDTO_UpdateBHospitalLevel(BHospitalLevel entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalLevel.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospitalLevel.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospitalLevel
        public BaseResultBool ST_UDTO_UpdateBHospitalLevelByField(BHospitalLevel entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalLevel.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalLevel.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalLevel.Update(tempArray);
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
                        //baseResultBool.success = IBBHospitalLevel.Edit();
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
        //Delele  BHospitalLevel
        public BaseResultBool ST_UDTO_DelBHospitalLevel(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospitalLevel.Entity = IBBHospitalLevel.Get(id);
                if (IBBHospitalLevel.Entity != null)
                {
                    long labid = IBBHospitalLevel.Entity.LabID;
                    string entityName = IBBHospitalLevel.Entity.GetType().Name;
                    baseResultBool.success = IBBHospitalLevel.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalLevel(BHospitalLevel entity)
        {
            EntityList<BHospitalLevel> entityList = new EntityList<BHospitalLevel>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospitalLevel.Entity = entity;
                try
                {
                    entityList.list = IBBHospitalLevel.Search();
                    entityList.count = IBBHospitalLevel.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalLevel>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalLevel> entityList = new EntityList<BHospitalLevel>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalLevel.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalLevel.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalLevel>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalLevelById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalLevel.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalLevel>(entity);
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

        #region BHospitalType
        //Add  BHospitalType
        public BaseResultDataValue ST_UDTO_AddBHospitalType(BHospitalType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalType.Add();
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
        //Update  BHospitalType
        public BaseResultBool ST_UDTO_UpdateBHospitalType(BHospitalType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalType.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospitalType.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospitalType
        public BaseResultBool ST_UDTO_UpdateBHospitalTypeByField(BHospitalType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalType.Update(tempArray);
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
                        //baseResultBool.success = IBBHospitalType.Edit();
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
        //Delele  BHospitalType
        public BaseResultBool ST_UDTO_DelBHospitalType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospitalType.Entity = IBBHospitalType.Get(id);
                if (IBBHospitalType.Entity != null)
                {
                    long labid = IBBHospitalType.Entity.LabID;
                    string entityName = IBBHospitalType.Entity.GetType().Name;
                    baseResultBool.success = IBBHospitalType.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalType(BHospitalType entity)
        {
            EntityList<BHospitalType> entityList = new EntityList<BHospitalType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospitalType.Entity = entity;
                try
                {
                    entityList.list = IBBHospitalType.Search();
                    entityList.count = IBBHospitalType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalType>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalType> entityList = new EntityList<BHospitalType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalType>(entity);
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


        #region BHospitalTypeLink
        //Add  BHospitalTypeLink
        public BaseResultDataValue ST_UDTO_AddBHospitalTypeLink(BHospitalTypeLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalTypeLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalTypeLink.Add();
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
        //Update  BHospitalTypeLink
        public BaseResultBool ST_UDTO_UpdateBHospitalTypeLink(BHospitalTypeLink entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalTypeLink.Entity = entity;
                try
                {
                    baseResultBool.success = IBBHospitalTypeLink.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
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
        //Update  BHospitalTypeLink
        public BaseResultBool ST_UDTO_UpdateBHospitalTypeLinkByField(BHospitalTypeLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalTypeLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalTypeLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalTypeLink.Update(tempArray);
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
                        //baseResultBool.success = IBBHospitalTypeLink.Edit();
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
        //Delele  BHospitalTypeLink
        public BaseResultBool ST_UDTO_DelBHospitalTypeLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospitalTypeLink.Entity = IBBHospitalTypeLink.Get(id);
                if (IBBHospitalTypeLink.Entity != null)
                {
                    long labid = IBBHospitalTypeLink.Entity.LabID;
                    string entityName = IBBHospitalTypeLink.Entity.GetType().Name;
                    baseResultBool.success = IBBHospitalTypeLink.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalTypeLink(BHospitalTypeLink entity)
        {
            EntityList<BHospitalTypeLink> entityList = new EntityList<BHospitalTypeLink>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBHospitalTypeLink.Entity = entity;
                try
                {
                    entityList.list = IBBHospitalTypeLink.Search();
                    entityList.count = IBBHospitalTypeLink.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalTypeLink>(entityList);
                    }
                    catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalTypeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalTypeLink> entityList = new EntityList<BHospitalTypeLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalTypeLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalTypeLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalTypeLink>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBHospitalTypeLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalTypeLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalTypeLink>(entity);
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

        #region BHospitalEmpLink
        //Add  BHospitalEmpLink
        public BaseResultDataValue ST_UDTO_AddBHospitalEmpLink(BHospitalEmpLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalEmpLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBHospitalEmpLink.Add();
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

        public BaseResultDataValue ST_UDTO_BatchAddBHospitalEmpLink(List<BHospitalEmpLink> entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null && entity.Count > 0)
            {
                try
                {
                    baseResultDataValue.success = IBBHospitalEmpLink.BatchAddBHospitalEmpLink(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = "true";
                        try
                        {
                            string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                            string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                            IBSLog.Entity = new SLog()
                            {
                                IP = "pi",
                                OperateName = "权限管理",
                                OperateType = "10000001",
                                Comment = "人员 - 新增医院",
                                EmpID = long.Parse(EmpID),
                                EmpName = EmpName
                            };
                            IBSLog.Save();

                        }
                        catch (Exception ee)
                        {
                            ZhiFang.Common.Log.Log.Debug("LIIPCommonService.svc.cs.ST_UDTO_BatchAddBHospitalEmpLink:平台写入日志错误：" + ee.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_BatchAddBHospitalEmpLink.异常：" + ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  BHospitalEmpLink
        //public BaseResultBool ST_UDTO_UpdateBHospitalEmpLink(BHospitalEmpLink entity)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        entity.DataUpdateTime = DateTime.Now;
        //        IBBHospitalEmpLink.Entity = entity;
        //        try
        //        {
        //            baseResultBool.success = IBBHospitalEmpLink.Edit();
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
        ////Update  BHospitalEmpLink
        //public BaseResultBool ST_UDTO_UpdateBHospitalEmpLinkByField(BHospitalEmpLink entity, string fields)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        if (!string.IsNullOrEmpty(fields))
        //            fields = fields + ",DataUpdateTime";
        //        entity.DataUpdateTime = DateTime.Now;
        //        IBBHospitalEmpLink.Entity = entity;
        //        try
        //        {
        //            if ((fields != null) && (fields.Length > 0))
        //            {
        //                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalEmpLink.Entity, fields);
        //                if (tempArray != null)
        //                {
        //                    baseResultBool.success = IBBHospitalEmpLink.Update(tempArray);
        //                    if (baseResultBool.success)
        //                    {
        //                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                baseResultBool.success = false;
        //                baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //                //baseResultBool.success = IBBHospitalEmpLink.Edit();
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
        //Delele  BHospitalEmpLink
        public BaseResultBool ST_UDTO_DelBHospitalEmpLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBHospitalEmpLink.Entity = IBBHospitalEmpLink.Get(id);
                if (IBBHospitalEmpLink.Entity != null)
                {
                    long labid = IBBHospitalEmpLink.Entity.LabID;
                    string entityName = IBBHospitalEmpLink.Entity.GetType().Name;
                    baseResultBool.success = IBBHospitalEmpLink.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                        try
                        {
                            string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                            string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                            IBSLog.Entity = new SLog()
                            {
                                IP = "pi",
                                OperateName = "权限管理",
                                OperateType = "10000001",
                                Comment = "人员 - 删除医院",
                                EmpID = long.Parse(EmpID),
                                EmpName = EmpName
                            };
                            IBSLog.Save();

                        }
                        catch (Exception ee)
                        {
                            ZhiFang.Common.Log.Log.Debug("LIIPCommonService.svc.cs.ST_UDTO_DelBHospitalEmpLink:平台写入日志错误：" + ee.ToString());
                        }
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

        public BaseResultBool BHospitalEmpLinkSetLinkType(long id, long typeid)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool.success = IBBHospitalEmpLink.BHospitalEmpLinkSetLinkType(id, typeid);
                if (baseResultBool.success)
                {
                    //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("BHospitalEmpLinkSetLinkType.错误信息：" + ex.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        //public BaseResultDataValue ST_UDTO_SearchBHospitalEmpLink(BHospitalEmpLink entity)
        //{
        //    EntityList<BHospitalEmpLink> entityList = new EntityList<BHospitalEmpLink>();
        //    BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
        //    if (entity != null)
        //    {
        //        IBBHospitalEmpLink.Entity = entity;
        //        try
        //        {
        //            entityList.list = IBBHospitalEmpLink.Search();
        //            entityList.count = IBBHospitalEmpLink.GetTotalCount();
        //            ParseObjectProperty pop = new ParseObjectProperty("");
        //            try
        //            {
        //                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalEmpLink>(entityList);
        //            }
        //            catch (Exception ex)
        //            {
        //                baseResultDataValue.success = false;
        //                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //                //throw new Exception(ex.Message);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultDataValue.success = false;
        //            baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        baseResultDataValue.success = false;
        //        baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultDataValue;
        //}

        public BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalEmpLink> entityList = new EntityList<BHospitalEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalEmpLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalEmpLink>(entityList);
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
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBHospitalEmpLinkByHQL.异常:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBHospitalEmpLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BHospitalEmpLink>(entity);
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

        public BaseResultDataValue ST_UDTO_SearchSelectHospitalListByEmpId(int page, int limit, string fields, string EmpId, string sort, bool isPlanish, bool flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                try
                {
                    string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    IBSLog.Entity = new SLog()
                    {
                        IP = "pi",
                        OperateName = "权限管理",
                        OperateType = "10000001",
                        Comment = "人员医院查询",
                        EmpID = long.Parse(EmpID),
                        EmpName = EmpName
                    };
                    IBSLog.Save();

                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Debug("LIIPCommonService.svc.cs.ST_UDTO_SearchSelectHospitalListByEmpId:平台写入日志错误：" + ee.ToString());
                }
                if (sort == null || sort.Trim() == "")
                {
                    sort = " HospitalCode ASC ";
                }
                else
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                if (flag)
                {
                    var entityList = IBBHospitalEmpLink.SearchSelectHospitalListByEmpId(EmpId, sort, page, limit);
                    ParseObjectProperty pop = new ParseObjectProperty(fields);

                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<BHospitalEmpLink>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                    return brdv;

                }
                else
                {
                    var entityList = IBBHospitalEmpLink.SearchUnSelectHospitalListByEmpId(EmpId, sort, page, limit);
                    ParseObjectProperty pop = new ParseObjectProperty(fields);

                    if (isPlanish)
                    {
                        brdv.ResultDataValue = pop.GetObjectListPlanish<BHospital>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                    return brdv;
                }

                
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchSelectHospitalListByEmpId.异常：" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region SAccountRegister
        //Add  SAccountRegister
        public BaseResultDataValue ST_UDTO_AddSAccountRegister(SAccountRegister entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (entity != null)
                {
                    if (string.IsNullOrEmpty(entity.Account))
                    {
                        return new BaseResultDataValue() { ErrorInfo = "账户名不能为空!", success = false };
                    }
                    entity.DataAddTime = DateTime.Now;
                    entity.DataUpdateTime = DateTime.Now;
                    IBSAccountRegister.Entity = entity;

                    brdv.success = IBSAccountRegister.Add();
                    if (brdv.success)
                    {
                        brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }

                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：实体参数不能为空！";
                }
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddSAccountRegister.异常：" + e.ToString());
                return brdv;
            }
        }

        //Update  SAccountRegister
        public BaseResultBool ST_UDTO_UpdateSAccountRegisterByField(SAccountRegister entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBSAccountRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSAccountRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBSAccountRegister.Update(tempArray);
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
                        //baseResultBool.success = IBSAccountRegister.Edit();
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

        //Delele  SAccountRegister
        public BaseResultBool ST_UDTO_DelSAccountRegister(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBSAccountRegister.Entity = IBSAccountRegister.Get(id);
                if (IBSAccountRegister.Entity != null)
                {
                    long labid = IBSAccountRegister.Entity.LabID;
                    string entityName = IBSAccountRegister.Entity.GetType().Name;
                    baseResultBool.success = IBSAccountRegister.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchSAccountRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SAccountRegister> entityList = new EntityList<SAccountRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSAccountRegister.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSAccountRegister.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SAccountRegister>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchSAccountRegisterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSAccountRegister.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SAccountRegister>(entity);
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

        /// <summary>
        /// 校验是否有权限访问
        /// </summary>
        /// <returns></returns>
        private bool CheckIsCheckAuthorization()
        {
            //获得当前web操作上下文
            WebOperationContext woc = WebOperationContext.Current;
            //获得当前请求头中的Authorization
            var auth = woc.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization];
            //如果auth为空，或者不等于admin/123，则响应405 MethodNotAllowed 
            if (string.IsNullOrEmpty(auth) || auth != "admin/123")
            {
                woc.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.MethodNotAllowed;
                return false;
            }
            return true;

        }

        public BaseResultDataValue ST_UDTO_SearchTreeBHospitalArea()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string fields = "Id,Name,PHospitalAreaID,PHospitalAreaName";
                BaseResultTree<BHospitalArea> entityList = IBBHospitalArea.SearchBHospitalAreaListTree();
                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                }
                catch (Exception ex)
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

        public BaseResultDataValue ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode(string LabCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            try
            {
                baseResultDataValue.ResultDataValue = IBBHospitalArea.GetBHospitalAreaCenterHospitalLabCodeByLabCode(LabCode); ;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode.异常：" + e.ToString());
                baseResultDataValue.ErrorInfo = "程序异常！";
            }

            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchTreeGridBHospitalAreaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalAreaVO> entityList = new EntityList<BHospitalAreaVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalArea.ST_UDTO_SearchTreeGridBHospitalAreaByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalArea.ST_UDTO_SearchTreeGridBHospitalAreaByHQL(where, page, limit);
                }
                //ParseObjectProperty pop = new ParseObjectProperty(fields);
                //try
                //{
                //    if (isPlanish)
                //    {
                //        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalAreaVO>(entityList);
                //    }
                //    else
                //    {
                //        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    baseResultDataValue.success = false;
                //    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                //    //throw new Exception(ex.Message);
                //}

                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(entityList);
                //BHospitalAreaVO aaa = new BHospitalAreaVO();
                //aaa.aaaName = "1";
                //aaa.IsChild = true;
                //baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(aaa);
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
        /// 根据参数编码获取参数值
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <returns></returns>
        public BaseResultDataValue SC_GetMSGParameterByParaNo(string paraNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var para = IBBParameter.GetParameterByParaNo(paraNo);
            if (para != null && para.ParaValue != null && para.ParaValue.Trim().Length > 0)
            {
                brdv.ResultDataValue = para.ParaValue;
            }
            return brdv;
        }

        public BaseResultBool ST_UDTO_UpdateBHospitalAreaByWhere(BHospitalArea entity, string fields, string where)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBBHospitalArea.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBHospitalArea.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBHospitalArea.ST_UDTO_UpdateBHospitalAreaByWhere(tempArray, where);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_ApprovalSAccountRegister(SAccountRegister entity, bool IsPass)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (entity != null)
                {
                    brdv = IBSAccountRegister.ST_UDTO_ApprovalSAccountRegister(entity, IsPass);
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.LIIPCommonService.svc.ST_UDTO_ApprovalSAccountRegister.异常：" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalEmpLink> entityList = new EntityList<BHospitalEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHospitalEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHospitalEmpLink.SearchListByHQL(where, page, limit);
                }
                List<BHospitalEmpLinkVO> bHospitalEmpLinkVOs = new List<BHospitalEmpLinkVO>();
                if (entityList.count > 0)
                {
                    List<long> empid = new List<long>();
                    foreach (var item in entityList.list)
                    {
                        empid.Add(item.EmpID.Value);
                    }
                    IList<Entity.RBAC.RBACUser> rBACUsers = IBRBACUser.SearchListByHQL("HREmployee.Id in (" + string.Join(",", empid) + ")");
                    if (rBACUsers.Count > 0)
                    {
                        foreach (var item in rBACUsers)
                        {
                            BHospitalEmpLink bHospitalEmpLink = entityList.list.Where(a => a.EmpID == item.HREmployee.Id).ToList()[0];
                            BHospitalEmpLinkVO bHospitalEmpLinkVO = LIIP.Common.ClassMapperHelp.GetMapper<BHospitalEmpLinkVO, BHospitalEmpLink>(bHospitalEmpLink);
                            bHospitalEmpLinkVO.Account = item.Account;
                            bHospitalEmpLinkVOs.Add(bHospitalEmpLinkVO);
                        }
                    }
                }
                //ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    /*if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHospitalEmpLinkVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }*/
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(bHospitalEmpLinkVOs);
                }
                catch (Exception ex)
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
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL.异常:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
    
    }
}
