using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;
using ZhiFang.WebAssist.Common;
using ZhiFang.WebAssist.ServerContract;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IBLL.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WebAssist.ServerWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WebAssistLisService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WebAssistLisService.svc 或 WebAssistLisService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebAssistLisService : IWebAssistLisService
    {
        #region IBLL
        IBDepartment IBDepartment { get; set; }
        IBDoctor IBDoctor { get; set; }
        IBPUser IBPUser { get; set; }
        IBLL.WebAssist.IBNPUser IBNPUser { get; set; }
        IBWardType IBWardType { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }
        IBLL.WebAssist.IBAntiGroup IBAntiGroup { get; set; }
        IBLL.WebAssist.IBChargeType IBChargeType { get; set; }
        IBLL.WebAssist.IBSamplingGroup IBSamplingGroup { get; set; }
        IBLL.WebAssist.IBSamplingitem IBSamplingitem { get; set; }
        IBLL.WebAssist.IBSectionItem IBSectionItem { get; set; }
        IBLL.WebAssist.IBStatusType IBStatusType { get; set; }
        IBLL.WebAssist.IBMDMicroTestDevelop IBMDMicroTestDevelop { get; set; }
        IBLL.WebAssist.IBMDMicroTestValue IBMDMicroTestValue { get; set; }
        IBLL.WebAssist.IBMEMicroAppraisalValue IBMEMicroAppraisalValue { get; set; }
        IBLL.WebAssist.IBMEMicroDSTValue IBMEMicroDSTValue { get; set; }
        IBLL.WebAssist.IBMEMicroInoculant IBMEMicroInoculant { get; set; }
        IBLL.WebAssist.IBNRequestItem IBNRequestItem { get; set; }
        IBLL.WebAssist.IBPGroup IBPGroup { get; set; }

        #endregion

        public void DoWork()
        {
        }

        #region Doctor
        //Add  Doctor
        public BaseResultDataValue WA_UDTO_AddDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDoctor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDoctor.Get(IBDoctor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDoctor.Entity);
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
        //Update  Doctor
        public BaseResultBool WA_UDTO_UpdateDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDoctor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Doctor
        public BaseResultBool WA_UDTO_UpdateDoctorByField(Doctor entity, string fields, long empID, string empName)
        {
            IBDoctor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDoctor.Entity, fields);
                    if (tempArray != null)
                    {
                        Doctor serverDoctor = IBDoctor.Get(IBDoctor.Entity.Id);
                        tempBaseResultBool.success = IBDoctor.Update(tempArray);

                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBDoctor.AddSCOperation(serverDoctor, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDoctor.Edit();
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
        //Delele  Doctor
        public BaseResultBool WA_UDTO_DelDoctor(int longDoctorID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDoctor.Remove(longDoctorID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchDoctor(Doctor entity)
        {
            IBDoctor.Entity = entity;
            EntityList<Doctor> tempEntityList = new EntityList<Doctor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDoctor.Search();
                tempEntityList.count = IBDoctor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Doctor>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Doctor> tempEntityList = new EntityList<Doctor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDoctor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDoctor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Doctor>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDoctorById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDoctor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Doctor>(tempEntity);
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

        #region NPUser
        //Add  NPUser
        public BaseResultDataValue WA_UDTO_AddNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBNPUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBNPUser.Get(IBNPUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBNPUser.Entity);
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
        //Update  NPUser
        public BaseResultBool WA_UDTO_UpdateNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNPUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  NPUser
        public BaseResultBool WA_UDTO_UpdateNPUserByField(NPUser entity, string fields)
        {
            IBNPUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBNPUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBNPUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBNPUser.Edit();
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
        //Delele  NPUser
        public BaseResultBool WA_UDTO_DelNPUser(int longNPUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNPUser.Remove(longNPUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchNPUser(NPUser entity)
        {
            IBNPUser.Entity = entity;
            EntityList<NPUser> tempEntityList = new EntityList<NPUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBNPUser.Search();
                tempEntityList.count = IBNPUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NPUser>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchNPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<NPUser> tempEntityList = new EntityList<NPUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBNPUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBNPUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NPUser>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchNPUserById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBNPUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<NPUser>(tempEntity);
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

        #region Department
        //Add  Department
        public BaseResultDataValue WA_UDTO_AddDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDepartment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDepartment.Get(IBDepartment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDepartment.Entity);
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
        //Update  Department
        public BaseResultBool WA_UDTO_UpdateDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Department
        public BaseResultBool WA_UDTO_UpdateDepartmentByField(Department entity, string fields, long empID, string empName)
        {
            IBDepartment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDepartment.Entity, fields);
                    if (tempArray != null)
                    {
                        Department serverDept = IBDepartment.Get(IBDepartment.Entity.Id);
                        tempBaseResultBool.success = IBDepartment.Update(tempArray);
                        if (tempBaseResultBool.success)
                        {
                            string[] arrFields = fields.Split(',');
                            IBDepartment.AddSCOperation(serverDept, arrFields, empID, empName);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDepartment.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WA_UDTO_UpdateDepartmentByField.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  Department
        public BaseResultBool WA_UDTO_DelDepartment(int longDepartmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartment.Remove(longDepartmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchDepartment(Department entity)
        {
            IBDepartment.Entity = entity;
            EntityList<Department> tempEntityList = new EntityList<Department>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDepartment.Search();
                tempEntityList.count = IBDepartment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Department>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Department> tempEntityList = new EntityList<Department>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDepartment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDepartment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Department>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDepartment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Department>(tempEntity);
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

        public BaseResultBool WA_UDTO_AddWarpAndDept()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                //tempBaseResultBool = IBBloodBReqForm.AddWarpAndDept();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WA_UDTO_AddWarpAndDept.Error.StackTrace:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        #endregion

        #region DepartmentUser
        //Add  DepartmentUser
        public BaseResultDataValue WA_UDTO_AddDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBDepartmentUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBDepartmentUser.Get(IBDepartmentUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBDepartmentUser.Entity);
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
        //Update  DepartmentUser
        public BaseResultBool WA_UDTO_UpdateDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartmentUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  DepartmentUser
        public BaseResultBool WA_UDTO_UpdateDepartmentUserByField(DepartmentUser entity, string fields)
        {
            IBDepartmentUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBDepartmentUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBDepartmentUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBDepartmentUser.Edit();
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
        //Delele  DepartmentUser
        public BaseResultBool WA_UDTO_DelDepartmentUser(long longDepartmentUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBDepartmentUser.Remove(longDepartmentUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchDepartmentUser(DepartmentUser entity)
        {
            IBDepartmentUser.Entity = entity;
            EntityList<DepartmentUser> tempEntityList = new EntityList<DepartmentUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBDepartmentUser.Search();
                tempEntityList.count = IBDepartmentUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<DepartmentUser>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDepartmentUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<DepartmentUser> tempEntityList = new EntityList<DepartmentUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBDepartmentUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBDepartmentUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<DepartmentUser>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchDepartmentUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBDepartmentUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<DepartmentUser>(tempEntity);
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

        #region WardType

        public BaseResultDataValue WA_UDTO_SearchWardType(WardType entity)
        {
            IBWardType.Entity = entity;
            EntityList<WardType> tempEntityList = new EntityList<WardType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBWardType.Search();
                tempEntityList.count = IBWardType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<WardType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchWardTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<WardType> tempEntityList = new EntityList<WardType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBWardType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBWardType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<WardType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchWardTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBWardType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<WardType>(tempEntity);
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

        #region AntiGroup
        //Add  AntiGroup
        public BaseResultDataValue WA_UDTO_AddAntiGroup(AntiGroup entity)
        {
            IBAntiGroup.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBAntiGroup.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBAntiGroup.Get(IBAntiGroup.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAntiGroup.Entity);
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
        //Update  AntiGroup
        public BaseResultBool WA_UDTO_UpdateAntiGroup(AntiGroup entity)
        {
            IBAntiGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAntiGroup.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  AntiGroup
        public BaseResultBool WA_UDTO_UpdateAntiGroupByField(AntiGroup entity, string fields)
        {
            IBAntiGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAntiGroup.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBAntiGroup.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAntiGroup.Edit();
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
        //Delele  AntiGroup
        public BaseResultBool WA_UDTO_DelAntiGroup(long longAntiGroupID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAntiGroup.Remove(longAntiGroupID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchAntiGroup(AntiGroup entity)
        {
            IBAntiGroup.Entity = entity;
            EntityList<AntiGroup> tempEntityList = new EntityList<AntiGroup>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAntiGroup.Search();
                tempEntityList.count = IBAntiGroup.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AntiGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchAntiGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AntiGroup> tempEntityList = new EntityList<AntiGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBAntiGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBAntiGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AntiGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchAntiGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAntiGroup.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AntiGroup>(tempEntity);
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

        #region ChargeType
        //Add  ChargeType
        public BaseResultDataValue WA_UDTO_AddChargeType(ChargeType entity)
        {
            IBChargeType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBChargeType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBChargeType.Get(IBChargeType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBChargeType.Entity);
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
        //Update  ChargeType
        public BaseResultBool WA_UDTO_UpdateChargeType(ChargeType entity)
        {
            IBChargeType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBChargeType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ChargeType
        public BaseResultBool WA_UDTO_UpdateChargeTypeByField(ChargeType entity, string fields)
        {
            IBChargeType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBChargeType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBChargeType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBChargeType.Edit();
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
        //Delele  ChargeType
        public BaseResultBool WA_UDTO_DelChargeType(long longChargeTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBChargeType.Remove(longChargeTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchChargeType(ChargeType entity)
        {
            IBChargeType.Entity = entity;
            EntityList<ChargeType> tempEntityList = new EntityList<ChargeType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBChargeType.Search();
                tempEntityList.count = IBChargeType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ChargeType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchChargeTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ChargeType> tempEntityList = new EntityList<ChargeType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBChargeType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBChargeType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ChargeType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchChargeTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBChargeType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ChargeType>(tempEntity);
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

        #region SamplingGroup
        //Add  SamplingGroup
        public BaseResultDataValue WA_UDTO_AddSamplingGroup(SamplingGroup entity)
        {
            IBSamplingGroup.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSamplingGroup.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSamplingGroup.Get(IBSamplingGroup.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSamplingGroup.Entity);
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
        //Update  SamplingGroup
        public BaseResultBool WA_UDTO_UpdateSamplingGroup(SamplingGroup entity)
        {
            IBSamplingGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSamplingGroup.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SamplingGroup
        public BaseResultBool WA_UDTO_UpdateSamplingGroupByField(SamplingGroup entity, string fields)
        {
            IBSamplingGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSamplingGroup.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSamplingGroup.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSamplingGroup.Edit();
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
        //Delele  SamplingGroup
        public BaseResultBool WA_UDTO_DelSamplingGroup(long longSamplingGroupID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSamplingGroup.Remove(longSamplingGroupID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSamplingGroup(SamplingGroup entity)
        {
            IBSamplingGroup.Entity = entity;
            EntityList<SamplingGroup> tempEntityList = new EntityList<SamplingGroup>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSamplingGroup.Search();
                tempEntityList.count = IBSamplingGroup.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SamplingGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSamplingGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SamplingGroup> tempEntityList = new EntityList<SamplingGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSamplingGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSamplingGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SamplingGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSamplingGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSamplingGroup.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SamplingGroup>(tempEntity);
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

        #region Samplingitem
        //Add  Samplingitem
        public BaseResultDataValue WA_UDTO_AddSamplingitem(Samplingitem entity)
        {
            IBSamplingitem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSamplingitem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSamplingitem.Get(IBSamplingitem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSamplingitem.Entity);
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
        //Update  Samplingitem
        public BaseResultBool WA_UDTO_UpdateSamplingitem(Samplingitem entity)
        {
            IBSamplingitem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSamplingitem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  Samplingitem
        public BaseResultBool WA_UDTO_UpdateSamplingitemByField(Samplingitem entity, string fields)
        {
            IBSamplingitem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSamplingitem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSamplingitem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSamplingitem.Edit();
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
        //Delele  Samplingitem
        public BaseResultBool WA_UDTO_DelSamplingitem(long longSamplingitemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSamplingitem.Remove(longSamplingitemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSamplingitem(Samplingitem entity)
        {
            IBSamplingitem.Entity = entity;
            EntityList<Samplingitem> tempEntityList = new EntityList<Samplingitem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSamplingitem.Search();
                tempEntityList.count = IBSamplingitem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Samplingitem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSamplingitemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<Samplingitem> tempEntityList = new EntityList<Samplingitem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSamplingitem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSamplingitem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Samplingitem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSamplingitemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSamplingitem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<Samplingitem>(tempEntity);
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

        #region SectionItem
        //Add  SectionItem
        public BaseResultDataValue WA_UDTO_AddSectionItem(SectionItem entity)
        {
            IBSectionItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSectionItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSectionItem.Get(IBSectionItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSectionItem.Entity);
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
        //Update  SectionItem
        public BaseResultBool WA_UDTO_UpdateSectionItem(SectionItem entity)
        {
            IBSectionItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSectionItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SectionItem
        public BaseResultBool WA_UDTO_UpdateSectionItemByField(SectionItem entity, string fields)
        {
            IBSectionItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSectionItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSectionItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSectionItem.Edit();
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
        //Delele  SectionItem
        public BaseResultBool WA_UDTO_DelSectionItem(long longSectionItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSectionItem.Remove(longSectionItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchSectionItem(SectionItem entity)
        {
            IBSectionItem.Entity = entity;
            EntityList<SectionItem> tempEntityList = new EntityList<SectionItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSectionItem.Search();
                tempEntityList.count = IBSectionItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SectionItem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSectionItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SectionItem> tempEntityList = new EntityList<SectionItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSectionItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSectionItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SectionItem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchSectionItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSectionItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SectionItem>(tempEntity);
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

        #region StatusType
        //Add  StatusType
        public BaseResultDataValue WA_UDTO_AddStatusType(StatusType entity)
        {
            IBStatusType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBStatusType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBStatusType.Get(IBStatusType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBStatusType.Entity);
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
        //Update  StatusType
        public BaseResultBool WA_UDTO_UpdateStatusType(StatusType entity)
        {
            IBStatusType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBStatusType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  StatusType
        public BaseResultBool WA_UDTO_UpdateStatusTypeByField(StatusType entity, string fields)
        {
            IBStatusType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBStatusType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBStatusType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBStatusType.Edit();
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
        //Delele  StatusType
        public BaseResultBool WA_UDTO_DelStatusType(long longStatusTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBStatusType.Remove(longStatusTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchStatusType(StatusType entity)
        {
            IBStatusType.Entity = entity;
            EntityList<StatusType> tempEntityList = new EntityList<StatusType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBStatusType.Search();
                tempEntityList.count = IBStatusType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<StatusType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchStatusTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<StatusType> tempEntityList = new EntityList<StatusType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBStatusType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBStatusType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<StatusType>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchStatusTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBStatusType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<StatusType>(tempEntity);
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

        #region MDMicroTestDevelop
        //Add  MDMicroTestDevelop
        public BaseResultDataValue WA_UDTO_AddMDMicroTestDevelop(MDMicroTestDevelop entity)
        {
            IBMDMicroTestDevelop.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBMDMicroTestDevelop.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBMDMicroTestDevelop.Get(IBMDMicroTestDevelop.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBMDMicroTestDevelop.Entity);
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
        //Update  MDMicroTestDevelop
        public BaseResultBool WA_UDTO_UpdateMDMicroTestDevelop(MDMicroTestDevelop entity)
        {
            IBMDMicroTestDevelop.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMDMicroTestDevelop.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  MDMicroTestDevelop
        public BaseResultBool WA_UDTO_UpdateMDMicroTestDevelopByField(MDMicroTestDevelop entity, string fields)
        {
            IBMDMicroTestDevelop.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBMDMicroTestDevelop.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBMDMicroTestDevelop.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBMDMicroTestDevelop.Edit();
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
        //Delele  MDMicroTestDevelop
        public BaseResultBool WA_UDTO_DelMDMicroTestDevelop(long longMDMicroTestDevelopID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMDMicroTestDevelop.Remove(longMDMicroTestDevelopID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelop(MDMicroTestDevelop entity)
        {
            IBMDMicroTestDevelop.Entity = entity;
            EntityList<MDMicroTestDevelop> tempEntityList = new EntityList<MDMicroTestDevelop>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBMDMicroTestDevelop.Search();
                tempEntityList.count = IBMDMicroTestDevelop.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MDMicroTestDevelop>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelopByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<MDMicroTestDevelop> tempEntityList = new EntityList<MDMicroTestDevelop>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBMDMicroTestDevelop.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBMDMicroTestDevelop.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MDMicroTestDevelop>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelopById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBMDMicroTestDevelop.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<MDMicroTestDevelop>(tempEntity);
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

        #region MDMicroTestValue
        //Add  MDMicroTestValue
        public BaseResultDataValue WA_UDTO_AddMDMicroTestValue(MDMicroTestValue entity)
        {
            IBMDMicroTestValue.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBMDMicroTestValue.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBMDMicroTestValue.Get(IBMDMicroTestValue.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBMDMicroTestValue.Entity);
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
        //Update  MDMicroTestValue
        public BaseResultBool WA_UDTO_UpdateMDMicroTestValue(MDMicroTestValue entity)
        {
            IBMDMicroTestValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMDMicroTestValue.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  MDMicroTestValue
        public BaseResultBool WA_UDTO_UpdateMDMicroTestValueByField(MDMicroTestValue entity, string fields)
        {
            IBMDMicroTestValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBMDMicroTestValue.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBMDMicroTestValue.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBMDMicroTestValue.Edit();
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
        //Delele  MDMicroTestValue
        public BaseResultBool WA_UDTO_DelMDMicroTestValue(long longMDMicroTestValueID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMDMicroTestValue.Remove(longMDMicroTestValueID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestValue(MDMicroTestValue entity)
        {
            IBMDMicroTestValue.Entity = entity;
            EntityList<MDMicroTestValue> tempEntityList = new EntityList<MDMicroTestValue>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBMDMicroTestValue.Search();
                tempEntityList.count = IBMDMicroTestValue.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MDMicroTestValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<MDMicroTestValue> tempEntityList = new EntityList<MDMicroTestValue>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBMDMicroTestValue.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBMDMicroTestValue.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MDMicroTestValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMDMicroTestValueById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBMDMicroTestValue.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<MDMicroTestValue>(tempEntity);
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

        #region MEMicroAppraisalValue
        //Add  MEMicroAppraisalValue
        public BaseResultDataValue WA_UDTO_AddMEMicroAppraisalValue(MEMicroAppraisalValue entity)
        {
            IBMEMicroAppraisalValue.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBMEMicroAppraisalValue.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBMEMicroAppraisalValue.Get(IBMEMicroAppraisalValue.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBMEMicroAppraisalValue.Entity);
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
        //Update  MEMicroAppraisalValue
        public BaseResultBool WA_UDTO_UpdateMEMicroAppraisalValue(MEMicroAppraisalValue entity)
        {
            IBMEMicroAppraisalValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroAppraisalValue.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  MEMicroAppraisalValue
        public BaseResultBool WA_UDTO_UpdateMEMicroAppraisalValueByField(MEMicroAppraisalValue entity, string fields)
        {
            IBMEMicroAppraisalValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBMEMicroAppraisalValue.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBMEMicroAppraisalValue.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBMEMicroAppraisalValue.Edit();
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
        //Delele  MEMicroAppraisalValue
        public BaseResultBool WA_UDTO_DelMEMicroAppraisalValue(long longMEMicroAppraisalValueID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroAppraisalValue.Remove(longMEMicroAppraisalValueID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValue(MEMicroAppraisalValue entity)
        {
            IBMEMicroAppraisalValue.Entity = entity;
            EntityList<MEMicroAppraisalValue> tempEntityList = new EntityList<MEMicroAppraisalValue>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBMEMicroAppraisalValue.Search();
                tempEntityList.count = IBMEMicroAppraisalValue.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroAppraisalValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<MEMicroAppraisalValue> tempEntityList = new EntityList<MEMicroAppraisalValue>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBMEMicroAppraisalValue.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBMEMicroAppraisalValue.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroAppraisalValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValueById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBMEMicroAppraisalValue.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<MEMicroAppraisalValue>(tempEntity);
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

        #region MEMicroDSTValue
        //Add  MEMicroDSTValue
        public BaseResultDataValue WA_UDTO_AddMEMicroDSTValue(MEMicroDSTValue entity)
        {
            IBMEMicroDSTValue.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBMEMicroDSTValue.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBMEMicroDSTValue.Get(IBMEMicroDSTValue.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBMEMicroDSTValue.Entity);
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
        //Update  MEMicroDSTValue
        public BaseResultBool WA_UDTO_UpdateMEMicroDSTValue(MEMicroDSTValue entity)
        {
            IBMEMicroDSTValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroDSTValue.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  MEMicroDSTValue
        public BaseResultBool WA_UDTO_UpdateMEMicroDSTValueByField(MEMicroDSTValue entity, string fields)
        {
            IBMEMicroDSTValue.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBMEMicroDSTValue.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBMEMicroDSTValue.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBMEMicroDSTValue.Edit();
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
        //Delele  MEMicroDSTValue
        public BaseResultBool WA_UDTO_DelMEMicroDSTValue(long longMEMicroDSTValueID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroDSTValue.Remove(longMEMicroDSTValueID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchMEMicroDSTValue(MEMicroDSTValue entity)
        {
            IBMEMicroDSTValue.Entity = entity;
            EntityList<MEMicroDSTValue> tempEntityList = new EntityList<MEMicroDSTValue>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBMEMicroDSTValue.Search();
                tempEntityList.count = IBMEMicroDSTValue.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroDSTValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroDSTValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<MEMicroDSTValue> tempEntityList = new EntityList<MEMicroDSTValue>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBMEMicroDSTValue.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBMEMicroDSTValue.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroDSTValue>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroDSTValueById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBMEMicroDSTValue.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<MEMicroDSTValue>(tempEntity);
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

        #region MEMicroInoculant
        //Add  MEMicroInoculant
        public BaseResultDataValue WA_UDTO_AddMEMicroInoculant(MEMicroInoculant entity)
        {
            IBMEMicroInoculant.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBMEMicroInoculant.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBMEMicroInoculant.Get(IBMEMicroInoculant.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBMEMicroInoculant.Entity);
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
        //Update  MEMicroInoculant
        public BaseResultBool WA_UDTO_UpdateMEMicroInoculant(MEMicroInoculant entity)
        {
            IBMEMicroInoculant.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroInoculant.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  MEMicroInoculant
        public BaseResultBool WA_UDTO_UpdateMEMicroInoculantByField(MEMicroInoculant entity, string fields)
        {
            IBMEMicroInoculant.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBMEMicroInoculant.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBMEMicroInoculant.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBMEMicroInoculant.Edit();
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
        //Delele  MEMicroInoculant
        public BaseResultBool WA_UDTO_DelMEMicroInoculant(long longMEMicroInoculantID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBMEMicroInoculant.Remove(longMEMicroInoculantID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchMEMicroInoculant(MEMicroInoculant entity)
        {
            IBMEMicroInoculant.Entity = entity;
            EntityList<MEMicroInoculant> tempEntityList = new EntityList<MEMicroInoculant>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBMEMicroInoculant.Search();
                tempEntityList.count = IBMEMicroInoculant.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroInoculant>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroInoculantByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<MEMicroInoculant> tempEntityList = new EntityList<MEMicroInoculant>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBMEMicroInoculant.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBMEMicroInoculant.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<MEMicroInoculant>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchMEMicroInoculantById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBMEMicroInoculant.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<MEMicroInoculant>(tempEntity);
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

        #region NRequestItem
        //Add  NRequestItem
        public BaseResultDataValue WA_UDTO_AddNRequestItem(NRequestItem entity)
        {
            IBNRequestItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBNRequestItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBNRequestItem.Get(IBNRequestItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBNRequestItem.Entity);
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
        //Update  NRequestItem
        public BaseResultBool WA_UDTO_UpdateNRequestItem(NRequestItem entity)
        {
            IBNRequestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNRequestItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  NRequestItem
        public BaseResultBool WA_UDTO_UpdateNRequestItemByField(NRequestItem entity, string fields)
        {
            IBNRequestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBNRequestItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBNRequestItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBNRequestItem.Edit();
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
        //Delele  NRequestItem
        public BaseResultBool WA_UDTO_DelNRequestItem(string longNRequestItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBNRequestItem.Remove(longNRequestItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WA_UDTO_SearchNRequestItem(NRequestItem entity)
        {
            IBNRequestItem.Entity = entity;
            EntityList<NRequestItem> tempEntityList = new EntityList<NRequestItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBNRequestItem.Search();
                tempEntityList.count = IBNRequestItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NRequestItem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchNRequestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<NRequestItem> tempEntityList = new EntityList<NRequestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBNRequestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBNRequestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<NRequestItem>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchNRequestItemById(string id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBNRequestItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<NRequestItem>(tempEntity);
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

        #region PGroup
        public BaseResultDataValue WA_UDTO_SearchPGroup(PGroup entity)
        {
            IBPGroup.Entity = entity;
            EntityList<PGroup> tempEntityList = new EntityList<PGroup>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPGroup.Search();
                tempEntityList.count = IBPGroup.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchPGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PGroup> tempEntityList = new EntityList<PGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGroup>(tempEntityList);
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

        public BaseResultDataValue WA_UDTO_SearchPGroupById(int id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPGroup.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PGroup>(tempEntity);
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
