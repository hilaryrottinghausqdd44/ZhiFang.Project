using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.ServiceModel;
using Newtonsoft.Json;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.RBAC.ViewObject.Response;
using System.Reflection;
using System.Runtime.Serialization;

namespace ZhiFang.ServiceCommon.RBAC
{
    public class RBACServiceCommon
    {
        #region IRBACService 成员

        public virtual ZhiFang.IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACRoleRight IBRBACRoleRight { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACRoleModule IBRBACRoleModule { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACRole IBRBACRole { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACModuleOper IBRBACModuleOper { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACModule IBRBACModule { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACEmpRoles IBRBACEmpRoles { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACEmpOptions IBRBACEmpOptions { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACRowFilter IBRBACRowFilter { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBImportFile IBImportFile { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHRPosition IBHRPosition { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHREmpIdentity IBHREmpIdentity { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHRDeptIdentity IBHRDeptIdentity { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBHRDeptEmp IBHRDeptEmp { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBSLog IBSLog { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACPreconditions IBRBACPreconditions { get; set; }
        #endregion

        #region 单表操作

        #region RBACUser
        //Add  RBACUser
        public BaseResultDataValue RBAC_UDTO_AddRBACUser(RBACUser entity)
        {

            IBRBACUser.Entity = entity;
            if (entity.PWD != null)
                entity.PWD = SecurityHelp.MD5Encrypt(entity.PWD, SecurityHelp.PWDMD5Key);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACUser.Get(IBRBACUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACUser.Entity);
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
        //Update  RBACUser
        public BaseResultBool RBAC_UDTO_UpdateRBACUser(RBACUser entity)
        {
            IBRBACUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACUser
        public BaseResultBool RBAC_UDTO_UpdateRBACUserByField(RBACUser entity, string fields)
        {
            IBRBACUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACUser.Entity, fields);
                    if (tempArray != null)
                    {
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            string[] fieldArray = tempArray[i].Split('=');
                            if (fieldArray[0] == "PWD" && entity.PWD != null)
                            {
                                if (entity.Id.ToString() != Cookie.CookieHelper.Read(DicCookieSession.UserID))
                                {
                                    tempBaseResultBool.success = false;
                                    tempBaseResultBool.ErrorInfo = "错误信息：非法修改密码！";
                                    return tempBaseResultBool;
                                }
                                tempArray[i] = fieldArray[0] + "=" + "'" + SecurityHelp.MD5Encrypt(entity.PWD, SecurityHelp.PWDMD5Key) + "'";
                                break;
                            }
                        }
                        tempBaseResultBool.success = IBRBACUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACUser.Edit();
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
        //Delele  RBACUser
        public BaseResultBool RBAC_UDTO_DelRBACUser(long longRBACUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACUser.Remove(longRBACUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACUser(RBACUser entity)
        {
            IBRBACUser.Entity = entity;
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACUser.Search();
                tempEntityList.count = IBRBACUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACUser>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACUser.SearchListByHQL(where, page, limit);
                }
                try
                {
                    string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);

                    RBAC_UDTO_AddSLog(new SLog()
                    {
                        IP = "pi",
                        OperateName = "权限管理",
                        OperateType = "10000001",
                        Comment = "人员查询 条件：" + where,
                        EmpID = long.Parse(EmpID),
                        EmpName = EmpName
                    });
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Debug("RABCServiceCommon.cs.RBAC_UDTO_SearchRBACUserByHQL:平台写入日志错误：" + ee.ToString());
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACUser>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
              
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("RABCServiceCommon.cs.RBAC_UDTO_SearchRBACUserByHQL.异常!"+ex.ToString());
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "程序异常!";
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACUser>(tempEntity);
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

        #region RBACRoleRight
        //Add  RBACRoleRight
        public BaseResultDataValue RBAC_UDTO_AddRBACRoleRight(RBACRoleRight entity)
        {
            IBRBACRoleRight.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACRoleRight.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRoleRight.Get(IBRBACRoleRight.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRoleRight.Entity);
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
        //Update  RBACRoleRight
        public BaseResultBool RBAC_UDTO_UpdateRBACRoleRight(RBACRoleRight entity)
        {
            IBRBACRoleRight.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRoleRight.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACRoleRight
        public BaseResultBool RBAC_UDTO_UpdateRBACRoleRightByField(RBACRoleRight entity, string fields)
        {
            IBRBACRoleRight.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRoleRight.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACRoleRight.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRoleRight.Edit();
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
        //Delele  RBACRoleRight
        public BaseResultBool RBAC_UDTO_DelRBACRoleRight(long longRBACRoleRightID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRoleRight.Remove(longRBACRoleRightID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleRight(RBACRoleRight entity)
        {
            IBRBACRoleRight.Entity = entity;
            EntityList<RBACRoleRight> tempEntityList = new EntityList<RBACRoleRight>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACRoleRight.Search();
                tempEntityList.count = IBRBACRoleRight.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleRight>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRoleRight> tempEntityList = new EntityList<RBACRoleRight>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRoleRight.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACRoleRight.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleRight>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACRoleRight.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACRoleRight>(tempEntity);
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

        #region RBACRoleModule
        //Add  RBACRoleModule
        public BaseResultDataValue RBAC_UDTO_AddRBACRoleModule(RBACRoleModule entity)
        {
            IBRBACRoleModule.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACRoleModule.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRoleModule.Get(IBRBACRoleModule.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRoleModule.Entity);
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
        //Update  RBACRoleModule
        public BaseResultBool RBAC_UDTO_UpdateRBACRoleModule(RBACRoleModule entity)
        {
            IBRBACRoleModule.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRoleModule.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACRoleModule
        public BaseResultBool RBAC_UDTO_UpdateRBACRoleModuleByField(RBACRoleModule entity, string fields)
        {
            IBRBACRoleModule.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRoleModule.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACRoleModule.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRoleModule.Edit();
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
        //Delele  RBACRoleModule
        public BaseResultBool RBAC_UDTO_DelRBACRoleModule(long longRBACRoleModuleID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRoleModule.Remove(longRBACRoleModuleID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleModule(RBACRoleModule entity)
        {
            IBRBACRoleModule.Entity = entity;
            EntityList<RBACRoleModule> tempEntityList = new EntityList<RBACRoleModule>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACRoleModule.Search();
                tempEntityList.count = IBRBACRoleModule.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleModule>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRoleModule> tempEntityList = new EntityList<RBACRoleModule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRoleModule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACRoleModule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleModule>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleModuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACRoleModule.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACRoleModule>(tempEntity);
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

        #region RBACRole
        //Add  RBACRole
        public BaseResultDataValue RBAC_UDTO_AddRBACRole(RBACRole entity)
        {
            IBRBACRole.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACRole.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRole.Get(IBRBACRole.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRole.Entity);
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
        //Update  RBACRole
        public BaseResultBool RBAC_UDTO_UpdateRBACRole(RBACRole entity)
        {
            IBRBACRole.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRole.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACRole
        public BaseResultBool RBAC_UDTO_UpdateRBACRoleByField(RBACRole entity, string fields)
        {
            IBRBACRole.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRole.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACRole.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRole.Edit();
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
        //Delele  RBACRole
        public BaseResultBool RBAC_UDTO_DelRBACRole(long longRBACRoleID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRole.Remove(longRBACRoleID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACRole(RBACRole entity)
        {
            IBRBACRole.Entity = entity;
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACRole.Search();
                tempEntityList.count = IBRBACRole.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRole>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRole.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACRole.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRole>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACRole.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACRole>(tempEntity);
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

        #region RBACModule
        //Add  RBACModule
        public BaseResultDataValue RBAC_UDTO_AddRBACModule(RBACModule entity)
        {
            IBRBACModule.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACModule.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACModule.Get(IBRBACModule.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACModule.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_AddRBACModuleaaa(RBACModule entity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". RBAC_UDTO_AddRBACModuleaaa.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
        //Update  RBACModule
        public BaseResultBool RBAC_UDTO_UpdateRBACModule(RBACModule entity)
        {
            IBRBACModule.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACModule.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACModule
        public BaseResultBool RBAC_UDTO_UpdateRBACModuleByField(RBACModule entity, string fields)
        {
            IBRBACModule.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACModule.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACModule.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACModule.Edit();
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
        //Delele  RBACModule
        public BaseResultBool RBAC_UDTO_DelRBACModule(long longRBACModuleID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACModule.Remove(longRBACModuleID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACModule(RBACModule entity)
        {
            IBRBACModule.Entity = entity;
            EntityList<RBACModule> tempEntityList = new EntityList<RBACModule>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACModule.Search();
                tempEntityList.count = IBRBACModule.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModule>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACModule> tempEntityList = new EntityList<RBACModule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACModule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACModule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModule>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACModule.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACModule>(tempEntity);
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

        #region RBACModuleOper
        //Add  RBACModuleOper
        public BaseResultDataValue RBAC_UDTO_AddRBACModuleOper(RBACModuleOper entity)
        {
            IBRBACModuleOper.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACModuleOper.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACModuleOper.Get(IBRBACModuleOper.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACModuleOper.Entity);
                    //if (tempBaseResultDataValue.success == true) SYSDataRowRoleCacheBase.IsRefreshModuleOperCache = true;
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
        //Update  RBACModuleOper
        public BaseResultBool RBAC_UDTO_UpdateRBACModuleOper(RBACModuleOper entity)
        {
            IBRBACModuleOper.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACModuleOper.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACModuleOper
        public BaseResultBool RBAC_UDTO_UpdateRBACModuleOperByField(RBACModuleOper entity, string fields)
        {
            IBRBACModuleOper.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACModuleOper.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACModuleOper.Update(tempArray);
                        //if (tempBaseResultBool.success == true) SYSDataRowRoleCacheBase.IsRefreshModuleOperCache = true;
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACModuleOper.Edit();
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
        //Delele  RBACModuleOper
        public BaseResultBool RBAC_UDTO_DelRBACModuleOper(long longRBACModuleOperID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACModuleOper.Remove(longRBACModuleOperID);
                //if (tempBaseResultBool.success == true) SYSDataRowRoleCacheBase.IsRefreshModuleOperCache = true;
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleOper(RBACModuleOper entity)
        {
            IBRBACModuleOper.Entity = entity;
            EntityList<RBACModuleOper> tempEntityList = new EntityList<RBACModuleOper>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACModuleOper.Search();
                tempEntityList.count = IBRBACModuleOper.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModuleOper>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleOperByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACModuleOper> tempEntityList = new EntityList<RBACModuleOper>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACModuleOper.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACModuleOper.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModuleOper>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleOperById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACModuleOper.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACModuleOper>(tempEntity);
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

        #region RBACEmpRoles
        //Add  RBACEmpRoles
        public BaseResultDataValue RBAC_UDTO_AddRBACEmpRoles(RBACEmpRoles entity)
        {
            IBRBACEmpRoles.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACEmpRoles.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACEmpRoles.Get(IBRBACEmpRoles.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACEmpRoles.Entity);
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
        //Update  RBACEmpRoles
        public BaseResultBool RBAC_UDTO_UpdateRBACEmpRoles(RBACEmpRoles entity)
        {
            IBRBACEmpRoles.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACEmpRoles.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACEmpRoles
        public BaseResultBool RBAC_UDTO_UpdateRBACEmpRolesByField(RBACEmpRoles entity, string fields)
        {
            IBRBACEmpRoles.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACEmpRoles.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACEmpRoles.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACEmpRoles.Edit();
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
        //Delele  RBACEmpRoles
        public BaseResultBool RBAC_UDTO_DelRBACEmpRoles(long longRBACEmpRolesID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACEmpRoles.Remove(longRBACEmpRolesID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpRoles(RBACEmpRoles entity)
        {
            IBRBACEmpRoles.Entity = entity;
            EntityList<RBACEmpRoles> tempEntityList = new EntityList<RBACEmpRoles>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACEmpRoles.Search();
                tempEntityList.count = IBRBACEmpRoles.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACEmpRoles>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpRolesByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACEmpRoles> tempEntityList = new EntityList<RBACEmpRoles>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACEmpRoles.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACEmpRoles.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACEmpRoles>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpRolesById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACEmpRoles.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACEmpRoles>(tempEntity);
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

        #region RBACEmpOptions
        //Add  RBACEmpOptions
        public BaseResultDataValue RBAC_UDTO_AddRBACEmpOptions(RBACEmpOptions entity)
        {
            IBRBACEmpOptions.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACEmpOptions.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACEmpOptions.Get(IBRBACEmpOptions.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACEmpOptions.Entity);
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
        //Update  RBACEmpOptions
        public BaseResultBool RBAC_UDTO_UpdateRBACEmpOptions(RBACEmpOptions entity)
        {
            IBRBACEmpOptions.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACEmpOptions.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACEmpOptions
        public BaseResultBool RBAC_UDTO_UpdateRBACEmpOptionsByField(RBACEmpOptions entity, string fields)
        {
            IBRBACEmpOptions.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACEmpOptions.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACEmpOptions.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACEmpOptions.Edit();
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
        //Delele  RBACEmpOptions
        public BaseResultBool RBAC_UDTO_DelRBACEmpOptions(long longRBACEmpOptionsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACEmpOptions.Remove(longRBACEmpOptionsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptions(RBACEmpOptions entity)
        {
            IBRBACEmpOptions.Entity = entity;
            EntityList<RBACEmpOptions> tempEntityList = new EntityList<RBACEmpOptions>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACEmpOptions.Search();
                tempEntityList.count = IBRBACEmpOptions.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACEmpOptions>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptionsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACEmpOptions> tempEntityList = new EntityList<RBACEmpOptions>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACEmpOptions.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACEmpOptions.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACEmpOptions>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptionsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACEmpOptions.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACEmpOptions>(tempEntity);
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

        #region RBACRowFilter

        //Add  RBACRowFilter
        public BaseResultDataValue RBAC_UDTO_AddRBACRowFilter(RBACRowFilter entity)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACRowFilter.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRowFilter.Get(IBRBACRowFilter.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRowFilter.Entity);
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
        //Update  RBACRowFilter
        public BaseResultBool RBAC_UDTO_UpdateRBACRowFilter(RBACRowFilter entity)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRowFilter.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACRowFilter
        public BaseResultBool RBAC_UDTO_UpdateRBACRowFilterByField(RBACRowFilter entity, string fields)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRowFilter.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACRowFilter.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRowFilter.Edit();
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
        //Delele  RBACRowFilter
        public BaseResultBool RBAC_UDTO_DelRBACRowFilter(long longRBACRowFilterID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACRowFilter.Remove(longRBACRowFilterID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACRowFilter(RBACRowFilter entity)
        {
            IBRBACRowFilter.Entity = entity;
            EntityList<RBACRowFilter> tempEntityList = new EntityList<RBACRowFilter>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACRowFilter.Search();
                tempEntityList.count = IBRBACRowFilter.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRowFilter>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRowFilterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRowFilter> tempEntityList = new EntityList<RBACRowFilter>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRowFilter.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACRowFilter.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRowFilter>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACRowFilterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACRowFilter.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACRowFilter>(tempEntity);
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

        #region HRPosition
        //Add  HRPosition
        public BaseResultDataValue RBAC_UDTO_AddHRPosition(HRPosition entity)
        {
            IBHRPosition.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHRPosition.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHRPosition.Get(IBHRPosition.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHRPosition.Entity);
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
        //Update  HRPosition
        public BaseResultBool RBAC_UDTO_UpdateHRPosition(HRPosition entity)
        {
            IBHRPosition.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRPosition.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HRPosition
        public BaseResultBool RBAC_UDTO_UpdateHRPositionByField(HRPosition entity, string fields)
        {
            IBHRPosition.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHRPosition.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHRPosition.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHRPosition.Edit();
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
        //Delele  HRPosition
        public BaseResultBool RBAC_UDTO_DelHRPosition(long longHRPositionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRPosition.Remove(longHRPositionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHRPosition(HRPosition entity)
        {
            IBHRPosition.Entity = entity;
            EntityList<HRPosition> tempEntityList = new EntityList<HRPosition>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHRPosition.Search();
                tempEntityList.count = IBHRPosition.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRPosition>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRPositionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HRPosition> tempEntityList = new EntityList<HRPosition>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHRPosition.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHRPosition.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRPosition>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRPositionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHRPosition.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HRPosition>(tempEntity);
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

        #region HREmployee
        //Add  HREmployee
        public BaseResultDataValue RBAC_UDTO_AddHREmployee(HREmployee entity)
        {
            IBHREmployee.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHREmployee.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHREmployee.Get(IBHREmployee.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHREmployee.Entity);
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
        //Update  HREmployee
        public BaseResultBool RBAC_UDTO_UpdateHREmployee(HREmployee entity)
        {
            IBHREmployee.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHREmployee.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HREmployee
        public BaseResultBool RBAC_UDTO_UpdateHREmployeeByField(HREmployee entity, string fields)
        {
            IBHREmployee.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHREmployee.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHREmployee.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHREmployee.Edit();
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
        //Delele  HREmployee
        public BaseResultBool RBAC_UDTO_DelHREmployee(long longHREmployeeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHREmployee.Remove(longHREmployeeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHREmployee(HREmployee entity)
        {
            IBHREmployee.Entity = entity;
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHREmployee.Search();
                tempEntityList.count = IBHREmployee.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHREmployee.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHREmployee.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQLEx(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHREmployee.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHREmployee.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHREmployeeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHREmployee.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HREmployee>(tempEntity);
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

        #region HREmpIdentity
        //Add  HREmpIdentity
        public BaseResultDataValue RBAC_UDTO_AddHREmpIdentity(HREmpIdentity entity)
        {
            IBHREmpIdentity.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHREmpIdentity.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHREmpIdentity.Get(IBHREmpIdentity.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHREmpIdentity.Entity);
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
        //Update  HREmpIdentity
        public BaseResultBool RBAC_UDTO_UpdateHREmpIdentity(HREmpIdentity entity)
        {
            IBHREmpIdentity.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHREmpIdentity.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HREmpIdentity
        public BaseResultBool RBAC_UDTO_UpdateHREmpIdentityByField(HREmpIdentity entity, string fields)
        {
            IBHREmpIdentity.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHREmpIdentity.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHREmpIdentity.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHREmpIdentity.Edit();
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
        //Delele  HREmpIdentity
        public BaseResultBool RBAC_UDTO_DelHREmpIdentity(long longHREmpIdentityID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHREmpIdentity.Remove(longHREmpIdentityID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHREmpIdentity(HREmpIdentity entity)
        {
            IBHREmpIdentity.Entity = entity;
            EntityList<HREmpIdentity> tempEntityList = new EntityList<HREmpIdentity>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHREmpIdentity.Search();
                tempEntityList.count = IBHREmpIdentity.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmpIdentity>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmpIdentity> tempEntityList = new EntityList<HREmpIdentity>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHREmpIdentity.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHREmpIdentity.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmpIdentity>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHREmpIdentity.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HREmpIdentity>(tempEntity);
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

        #region HRDept
        //Add  HRDept
        public BaseResultDataValue RBAC_UDTO_AddHRDept(HRDept entity)
        {
            IBHRDept.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHRDept.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHRDept.Get(IBHRDept.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHRDept.Entity);
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
        //Update  HRDept
        public BaseResultBool RBAC_UDTO_UpdateHRDept(HRDept entity)
        {
            IBHRDept.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDept.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HRDept
        public BaseResultBool RBAC_UDTO_UpdateHRDeptByField(HRDept entity, string fields)
        {
            IBHRDept.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHRDept.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHRDept.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHRDept.Edit();
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
        //Delele  HRDept
        public BaseResultBool RBAC_UDTO_DelHRDept(long longHRDeptID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDept.Remove(longHRDeptID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHRDept(HRDept entity)
        {
            IBHRDept.Entity = entity;
            EntityList<HRDept> tempEntityList = new EntityList<HRDept>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHRDept.Search();
                tempEntityList.count = IBHRDept.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDept>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HRDept> tempEntityList = new EntityList<HRDept>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHRDept.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHRDept.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDept>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHRDept.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HRDept>(tempEntity);
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

        #region HRDeptIdentity
        //Add  HRDeptIdentity
        public BaseResultDataValue RBAC_UDTO_AddHRDeptIdentity(HRDeptIdentity entity)
        {
            IBHRDeptIdentity.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHRDeptIdentity.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHRDeptIdentity.Get(IBHRDeptIdentity.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHRDeptIdentity.Entity);
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
        //Update  HRDeptIdentity
        public BaseResultBool RBAC_UDTO_UpdateHRDeptIdentity(HRDeptIdentity entity)
        {
            IBHRDeptIdentity.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDeptIdentity.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HRDeptIdentity
        public BaseResultBool RBAC_UDTO_UpdateHRDeptIdentityByField(HRDeptIdentity entity, string fields)
        {
            IBHRDeptIdentity.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHRDeptIdentity.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHRDeptIdentity.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHRDeptIdentity.Edit();
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
        //Delele  HRDeptIdentity
        public BaseResultBool RBAC_UDTO_DelHRDeptIdentity(long longHRDeptIdentityID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDeptIdentity.Remove(longHRDeptIdentityID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentity(HRDeptIdentity entity)
        {
            IBHRDeptIdentity.Entity = entity;
            EntityList<HRDeptIdentity> tempEntityList = new EntityList<HRDeptIdentity>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHRDeptIdentity.Search();
                tempEntityList.count = IBHRDeptIdentity.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDeptIdentity>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HRDeptIdentity> tempEntityList = new EntityList<HRDeptIdentity>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHRDeptIdentity.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHRDeptIdentity.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDeptIdentity>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentityById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHRDeptIdentity.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HRDeptIdentity>(tempEntity);
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

        #region HRDeptEmp
        //Add  HRDeptEmp
        public BaseResultDataValue RBAC_UDTO_AddHRDeptEmp(HRDeptEmp entity)
        {
            IBHRDeptEmp.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBHRDeptEmp.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBHRDeptEmp.Get(IBHRDeptEmp.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBHRDeptEmp.Entity);
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
        //Update  HRDeptEmp
        public BaseResultBool RBAC_UDTO_UpdateHRDeptEmp(HRDeptEmp entity)
        {
            IBHRDeptEmp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDeptEmp.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  HRDeptEmp
        public BaseResultBool RBAC_UDTO_UpdateHRDeptEmpByField(HRDeptEmp entity, string fields)
        {
            IBHRDeptEmp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBHRDeptEmp.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBHRDeptEmp.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBHRDeptEmp.Edit();
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
        //Delele  HRDeptEmp
        public BaseResultBool RBAC_UDTO_DelHRDeptEmp(long longHRDeptEmpID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBHRDeptEmp.Remove(longHRDeptEmpID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptEmp(HRDeptEmp entity)
        {
            IBHRDeptEmp.Entity = entity;
            EntityList<HRDeptEmp> tempEntityList = new EntityList<HRDeptEmp>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBHRDeptEmp.Search();
                tempEntityList.count = IBHRDeptEmp.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDeptEmp>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HRDeptEmp> tempEntityList = new EntityList<HRDeptEmp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBHRDeptEmp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBHRDeptEmp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HRDeptEmp>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBHRDeptEmp.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HRDeptEmp>(tempEntity);
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

        #region SLog
        //Add  SLog
        public BaseResultDataValue RBAC_UDTO_AddSLog(SLog entity)
        {
            IBSLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSLog.Get(IBSLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSLog.Entity);
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
        //Update  SLog
        public BaseResultBool RBAC_UDTO_UpdateSLog(SLog entity)
        {
            IBSLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SLog
        public BaseResultBool RBAC_UDTO_UpdateSLogByField(SLog entity, string fields)
        {
            IBSLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSLog.Edit();
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
        //Delele  SLog
        public BaseResultBool RBAC_UDTO_DelSLog(long longSLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSLog.Remove(longSLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchSLog(SLog entity)
        {
            IBSLog.Entity = entity;
            EntityList<SLog> tempEntityList = new EntityList<SLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSLog.Search();
                tempEntityList.count = IBSLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SLog>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchSLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SLog> tempEntityList = new EntityList<SLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SLog>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchSLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SLog>(tempEntity);
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

        #region RBACPreconditions
        //Add  RBACPreconditions
        public BaseResultDataValue RBAC_UDTO_AddRBACPreconditions(RBACPreconditions entity)
        {
            IBRBACPreconditions.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBRBACPreconditions.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBRBACPreconditions.Get(IBRBACPreconditions.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACPreconditions.Entity);
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
        //Update  RBACPreconditions
        public BaseResultBool RBAC_UDTO_UpdateRBACPreconditions(RBACPreconditions entity)
        {
            IBRBACPreconditions.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACPreconditions.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  RBACPreconditions
        public BaseResultBool RBAC_UDTO_UpdateRBACPreconditionsByField(RBACPreconditions entity, string fields)
        {
            IBRBACPreconditions.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACPreconditions.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBRBACPreconditions.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACPreconditions.Edit();
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
        //Delele  RBACPreconditions
        public BaseResultBool RBAC_UDTO_DelRBACPreconditions(long longRBACPreconditionsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBRBACPreconditions.Remove(longRBACPreconditionsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACPreconditions(RBACPreconditions entity)
        {
            IBRBACPreconditions.Entity = entity;
            EntityList<RBACPreconditions> tempEntityList = new EntityList<RBACPreconditions>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBRBACPreconditions.Search();
                tempEntityList.count = IBRBACPreconditions.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACPreconditions>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACPreconditionsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACPreconditions> tempEntityList = new EntityList<RBACPreconditions>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACPreconditions.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBRBACPreconditions.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACPreconditions>(tempEntityList);
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

        public BaseResultDataValue RBAC_UDTO_SearchRBACPreconditionsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACPreconditions.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACPreconditions>(tempEntity);
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
        #endregion 单表操作

        #region IRBACService 查询类操作

        #region 查询员工
        /// <summary>
        /// 查询部门直属员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                tempEntityList = IBHREmployee.SearchHREmployeeByHRDeptID(where, page, limit, CommonServiceMethod.GetSortHQL(sort));

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList);
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
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询部门直属的未分配角色的员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeNoRoleByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                tempEntityList.list = IBHREmployee.SearchHREmployeeByHRDeptID(where, page, limit, CommonServiceMethod.GetSortHQL(sort), 2);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList.list);
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
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询部门直属的已分配角色的员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeRoleByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                tempEntityList.list = IBHREmployee.SearchHREmployeeByHRDeptID(where, page, limit, CommonServiceMethod.GetSortHQL(sort), 1);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList.list);
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
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询部门直属的已分配特定角色的员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptIDAndRBACRoleID(long longHRDeptID, long longRBACRoleID, bool isPlanish, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                tempEntityList.list = IBHREmployee.SearchHREmployeeByHRDeptIDAndRBACRoleID(longHRDeptID, longRBACRoleID);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList.list);
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
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询部门直属员工列表(包含子部门),并过滤已分配特定角色的员工
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeNoRBACRoleIDByHRDeptID(long longHRDeptID, long longRBACRoleID, bool isPlanish, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<HREmployee> tempEntityList = new EntityList<HREmployee>();
            try
            {
                tempEntityList.list = IBHREmployee.SearchHREmployeeNoRBACRoleIDByHRDeptID(longHRDeptID, longRBACRoleID);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<HREmployee>(tempEntityList.list);
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
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 根据Session中人员ID查询该员工的信息
        /// </summary>
        /// <returns>员工压平Json字符串(只压平基类型属性)</returns>
        public BaseResultDataValue RBAC_UDTO_GetHREmployeeBySessionHREmpID(string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string tempID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID); //EmployeeID 员工ID
                if ((tempID != null) && (tempID.Length > 0))
                {
                    var tempEntity = IBHREmployee.Get(Int64.Parse(tempID));
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<HREmployee>(tempEntity);
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
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 自动生成和修改员工账号名
        /// </summary>
        /// <param name="longHREmployeeID">员工ID</param>
        /// <param name="strUserAccount">用户账户名</param>
        /// <returns>用户账户名</returns>
        public BaseResultDataValue RBAC_RJ_AutoCreateUserAccount(long longHREmployeeID, string strUserAccount)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string tempStr = IBRBACUser.RJ_AutoCreateUserAccount(longHREmployeeID.ToString(), strUserAccount);
                tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr("UserAccount:" + "\"" + tempStr + "\"");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 重置用户名密码
        /// </summary>
        /// <param name="longRBACUserID">用户ID</param>
        /// <returns>六位的新密码</returns>
        public BaseResultDataValue RBAC_RJ_ResetAccountPassword(long longRBACUserID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string tempStr = SecurityHelp.MD5Decrypt(IBRBACUser.RJ_ResetAccountPassword(longRBACUserID), SecurityHelp.PWDMD5Key);
                tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr("AccountPassword:" + "\"" + tempStr + "\"");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        //为多个或单个员工增加或减少，多个或单个角色。
        public BaseResultDataValue RBAC_RJ_SetEmpRolesByEmpIdList(string empIdList, string roleIdList, int flag)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (!string.IsNullOrEmpty(empIdList) && !string.IsNullOrEmpty(roleIdList))
                    tempBaseResultDataValue.success = IBRBACEmpRoles.RBAC_RJ_SetEmpRolesByEmpIdList(empIdList.Split(','), roleIdList.Split(','), flag);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        //批量导入员工(可支持账户同步生成) 支持EXCEL\XML两种方式。
        public BaseResultDataValue RBAC_RJ_AddInEmpList()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                FileType tempFileType = FileType.XML;
                bool tempIsCreateAccount = false;
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "filetype":
                            if (HttpContext.Current.Request.Form["filetype"].Trim() != "")
                            {
                                tempFileType = (FileType)Enum.Parse(typeof(FileType), HttpContext.Current.Request.Form["filetype"].Trim());
                            }
                            break;
                        case "isCreateAccount":
                            if (HttpContext.Current.Request.Form["isCreateAccount"].Trim() != "")
                            {
                                tempIsCreateAccount = bool.Parse(HttpContext.Current.Request.Form["AppType"].Trim());
                            }
                            break;
                    }
                }
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    tempBaseResultDataValue.ErrorInfo = "未检测到文件！";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }

                HttpPostedFile tempFile = HttpContext.Current.Request.Files[0];
                int len = tempFile.ContentLength;
                if (len > 0)
                {
                    FileStream tempFileStream = (FileStream)tempFile.InputStream;
                    tempBaseResultDataValue.ResultDataValue = IBImportFile.RJ_AddInEmpList(tempFileStream, tempFileType, tempIsCreateAccount);
                }
                else
                {
                    tempBaseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    tempBaseResultDataValue.success = false;
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        //批量导入部门 支持EXCEL\XML两种方式。
        public BaseResultDataValue RBAC_RJ_AddInDeptList()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                FileType tempFileType = FileType.XML;
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "filetype":
                            if (HttpContext.Current.Request.Form["filetype"].Trim() != "")
                            {
                                tempFileType = (FileType)Enum.Parse(typeof(FileType), HttpContext.Current.Request.Form["filetype"].Trim());
                            }
                            break;
                    }
                }
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    tempBaseResultDataValue.ErrorInfo = "未检测到文件！";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }

                HttpPostedFile tempFile = HttpContext.Current.Request.Files[0];
                int len = tempFile.ContentLength;
                if (len > 0)
                {
                    FileStream tempFileStream = (FileStream)tempFile.InputStream;
                    tempBaseResultDataValue.ResultDataValue = IBImportFile.RJ_AddInDeptList(tempFileStream, tempFileType);
                }
                else
                {
                    tempBaseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    tempBaseResultDataValue.success = false;
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 查询账户

        public BaseResultDataValue RBAC_UDTO_SearchRBACUserListByHQL(string where, int start, int limit, int page, bool isPlanish, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACUser> tempEntityList = new EntityList<RBACUser>();
            tempEntityList = IBRBACUser.SearchListByHQL(where, page, limit);

            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
            if (isPlanish)
            {
                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACUser>(tempEntityList.list);
            }
            else
            {
                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
            }

            tempBaseResultDataValue.success = true;
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 验证用户账户是否存在
        /// </summary>
        /// <param name="strUserAccount">账户名</param>
        /// <returns>bool</returns>
        public BaseResultDataValue RBAC_RJ_ValidateUserAccountIsExist(string strUserAccount)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                bool tempBool = IBRBACUser.IsExistUserAccount(strUserAccount);
                tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr("result:" + "\"" + tempBool.ToString().ToLower() + "\"");
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 查询部门
        public BaseResultDataValue RBAC_RJ_GetHRDeptFrameTree(string strHRDeptID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempHRDeptId = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strHRDeptID.Trim())) || (strHRDeptID.ToLower().Trim() == "root")))
                    tempHRDeptId = Int64.Parse(strHRDeptID);
                //tempHRDeptId = 2;
                tempBaseResultTree = IBHRDept.SearchHRDeptTree(tempHRDeptId);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        public BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string strHRDeptID, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<HRDept> tempBaseResultTree = new BaseResultTree<HRDept>();
            long tempHRDeptId = 0;
            //fields = "HRDept_CName,HRDept_Id";
            try
            {
                if (!((string.IsNullOrEmpty(strHRDeptID.Trim())) || (strHRDeptID.ToLower().Trim() == "root")))
                    tempHRDeptId = Int64.Parse(strHRDeptID);
                tempBaseResultTree = IBHRDept.SearchHRDeptListTree(tempHRDeptId);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        public BaseResultDataValue RBAC_RJ_GetHRDeptEmployeeFrameTree(string strHRDeptID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempHRDeptID = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strHRDeptID.Trim())) || (strHRDeptID.ToLower().Trim() == "root")))
                    tempHRDeptID = Int64.Parse(strHRDeptID);
                tempBaseResultTree = IBHRDept.GetHRDeptEmployeeFrameTree(tempHRDeptID);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        #region 查询角色

        /// <summary>
        /// 根据用户ID查询该用户拥有的角色列表
        /// </summary>
        /// <param name="longHREmpID">员工ID</param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchRoleByHREmpID(long longHREmpID, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            try
            {
                if (longHREmpID > 0)
                {
                    tempEntityList.list = IBRBACRole.SearchRoleByHREmpID(longHREmpID);
                    tempEntityList.count = tempEntityList.list.Count;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRole>(tempEntityList);
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
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 根据模块ID查询拥有该模块权限的角色列表
        /// </summary>
        /// <param name="longModuleID"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>         
        public BaseResultDataValue RBAC_UDTO_SearchRoleByModuleID(long longModuleID, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            try
            {
                if (longModuleID > 0)
                {
                    tempEntityList.list = IBRBACRole.SearchRoleByModuleID(longModuleID);
                    tempEntityList.count = tempEntityList.list.Count;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRole>(tempEntityList);
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
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 根据模块ID查询拥有该模块权限的角色操作列表
        /// </summary>
        /// <param name="longModuleID"></param>
        /// <returns>返回手工组合的Json串</returns>
        public BaseResultDataValue RBAC_UDTO_SearchRoleModuleOperByModuleID(long longModuleID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (longModuleID > 0)
                {
                    tempBaseResultDataValue.ResultDataValue = IBRBACRole.SearchRoleModuleOperByModuleID(longModuleID);

                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 根据角色ID获取角色单列树
        /// </summary>
        /// <param name="strRBACRoleID"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_RJ_GetRBACRoleFrameTree(string strRBACRoleID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempRBACRoleId = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strRBACRoleID.Trim())) || (strRBACRoleID.ToLower().Trim() == "root")))
                    tempRBACRoleId = Int64.Parse(strRBACRoleID);
                tempBaseResultTree = IBRBACRole.SearchRBACRoleTree(tempRBACRoleId);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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
        /// 根据角色ID获取角色列表树
        /// </summary>
        /// <param name="strRBACRoleID"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_RJ_GetRBACRoleFrameListTree(string strRBACRoleID, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<RBACRole> tempBaseResultTree = new BaseResultTree<RBACRole>();
            long tempRBACRoleID = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strRBACRoleID.Trim())) || (strRBACRoleID.ToLower().Trim() == "root")))
                    tempRBACRoleID = Int64.Parse(strRBACRoleID);
                tempBaseResultTree = IBRBACRole.SearchRBACRoleListTree(tempRBACRoleID);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        #region 查询模块
        
        public BaseResultDataValue RBAC_UDTO_SearchModuleByHREmpIDRole(long id, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultList<RBACModule> returnList = new BaseResultList<RBACModule>();
            EntityList<RBACModule> entityList = new EntityList<RBACModule>();
            try
            {
                IList<RBACModule> empList = IBHREmployee.RBAC_UDTO_SearchModuleByHREmpIDRole(id, page, limit);
                entityList.list = empList;
                entityList.count = empList.Count;

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModule>(entityList);
                    }
                    else
                    {
                        brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue RBAC_RJ_CheckEmpModuleRight(long londHREmployeeID, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACEmpOptions.SearchRBACEmpOptionsByEmpID(londHREmployeeID.ToString());
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACEmpOptions>(tempEntity);
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

        /// <summary>
        /// 根据模块ID获取此模块的应用组件
        /// </summary>
        /// <param name="longModuleID"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchBTDAppComponentsByModuleID(long longModuleID, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBRBACModule.Get(longModuleID);
                if (tempEntity != null)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDAppComponents>(tempEntity.BTDAppComponents);
                        }
                        else
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity.BTDAppComponents);
                        }
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        /// <summary>
        /// 根据模块ID获取模块单列树
        /// </summary>
        /// <param name="RBACModuleID"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleToTree(string RBACModuleID)
        {
            BaseResultDataValue result = new BaseResultDataValue();
            result.success = true;
            try
            {
                long rbacmoduleid = 0;
                if (RBACModuleID != null && RBACModuleID.Trim().Length > 0 && RBACModuleID.Trim().ToLower() != "root")
                {
                    rbacmoduleid = Convert.ToInt64(RBACModuleID);
                }
                BaseResultTree<RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
                ListTreeRoot = IBRBACModule.SearchRBACModuleToTree(rbacmoduleid, false);
                result.success = true;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.Indented, settings);
                string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.None, settings);//去掉回车和空格
                result.ResultDataValue = resulta;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            return result;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleToListTree(string RBACModuleID, string fields)
        {
            BaseResultDataValue result = new BaseResultDataValue();
            result.success = true;
            try
            {
                BaseResultTree<RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
                long rbacmoduleid = 0;
                if (RBACModuleID != null && RBACModuleID.Trim().Length > 0 && RBACModuleID.Trim().ToLower() != "root")
                {
                    rbacmoduleid = Convert.ToInt64(RBACModuleID);
                }
                ListTreeRoot = IBRBACModule.SearchRBACModuleToTree(rbacmoduleid, true);
                result.success = true;

                if (ListTreeRoot.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                        result.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(ListTreeRoot, fields);
                    }
                    catch (Exception ex)
                    {
                        result.success = false;
                        result.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            return result;
        }

        public BaseResultDataValue RBAC_UDTO_GetRBACModuleById(long id, string fields, bool isPlanish)
        {
            var tempEntity = IBRBACModule.Get(id);
            BaseResultDataValue brdv = new BaseResultDataValue();
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
            if (isPlanish)
            {
                brdv.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<RBACModule>(tempEntity);
            }
            else
            {
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
            }
            return brdv;
        }

        /// <summary>
        /// 根据Session中员工ID查询该人员所具有权限的模块树
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchModuleTreeBySessionHREmpID()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            object tempBaseResultTree = null;
            try
            {
                string tempEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID); //EmployeeID 员工ID
                string tempUserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);

                if (tempUserAccount == DicCookieSession.SuperUser)
                    tempBaseResultTree = IBRBACModule.SearchRBACModuleToTree(0, false);
                else
                {
                    if ((tempEmployeeID != null) && (tempEmployeeID.Length > 0))
                        tempBaseResultTree = IBRBACModule.SearchModuleTreeByHREmpID(Int64.Parse(tempEmployeeID));
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "错误信息：Session过期！";
                        return tempBaseResultDataValue;
                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "程序错误,序列化错误!";
                    ZhiFang.Common.Log.Log.Error("RBAC_UDTO_SearchModuleTreeBySessionHREmpID.序列化错误:" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "程序错误!";
                ZhiFang.Common.Log.Log.Error("RBAC_UDTO_SearchModuleTreeBySessionHREmpID.程序异常:" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }      

        /// <summary>
        /// 根据员工ID查询该人员所具有权限的模块树
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchModuleTreeByHREmpID(long longHREmpID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            try
            {
                tempBaseResultTree = IBRBACModule.SearchModuleTreeByHREmpID(longHREmpID);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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
        /// 根据角色ID查询该角色所具有权限的模块树
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchModuleTreeByRBACRoleID(long RBACRoleID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            try
            {
                tempBaseResultTree = IBRBACModule.SearchModuleTreeByRBACRoleID(RBACRoleID);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        #region 查询模块操作
        //根据Session人员ID、Cookie中模块ID查询是否具有操作此模块的权限
        public BaseResultDataValue RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID(long CurModuleID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<RBACModule> tempList = new List<RBACModule>();
            try
            {
                string UserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);  //UserAccount 用户账户
                if (!string.IsNullOrEmpty(UserAccount) && UserAccount == DicCookieSession.SuperUser)
                {
                    tempBaseResultDataValue.success = true;
                    tempBaseResultDataValue.ResultDataValue = "true";
                }
                else
                {
                    string tempHREmpID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);  //EmployeeID 员工ID
                    string tempModuleID = CurModuleID > 0 ? CurModuleID.ToString() : Cookie.CookieHelper.Read(DicCookieSession.CurModuleID);  //ModuleID 模块ID
                    if (string.IsNullOrEmpty(tempHREmpID))
                    {
                        tempBaseResultDataValue.ErrorInfo = "无法从Session中获取EmployeeID员工ID信息";
                    }
                    else if (string.IsNullOrEmpty(tempModuleID))
                    {
                        tempBaseResultDataValue.ErrorInfo = "无法从Cookie中获取ModuleID模块ID信息";
                    }
                    else
                    {
                        tempList = IBRBACModule.SearchModuleByHREmpIDAndModuleID(Convert.ToInt64(tempHREmpID), Convert.ToInt64(tempModuleID));
                    }

                    if (tempList != null && tempList.Count > 0)
                    {
                        tempBaseResultDataValue.success = true;
                        tempBaseResultDataValue.ResultDataValue = "true";
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ResultDataValue = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        //根据人员ID、模块ID查询是否具有操作此模块的权限
        public BaseResultDataValue RBAC_UDTO_SearchModuleRoleByHREmpIDAndModuleID(string strHREmpID, string strModuleID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<RBACModule> tempList = new List<RBACModule>();
            try
            {
                string UserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);  //UserAccount 用户账户
                if (!string.IsNullOrEmpty(UserAccount) && UserAccount == DicCookieSession.SuperUser)
                {
                    tempBaseResultDataValue.success = true;
                    tempBaseResultDataValue.ResultDataValue = "true";
                }
                else
                {
                    if (string.IsNullOrEmpty(strHREmpID))
                    {
                        tempBaseResultDataValue.ErrorInfo = "无法从Session中获取EmployeeID员工ID信息";
                    }
                    else if (string.IsNullOrEmpty(strModuleID))
                    {
                        tempBaseResultDataValue.ErrorInfo = "无法从Cookie中获取ModuleID模块ID信息";
                    }
                    else
                    {
                        tempList = IBRBACModule.SearchModuleByHREmpIDAndModuleID(Convert.ToInt64(strHREmpID), Convert.ToInt64(strModuleID));
                    }

                    if (tempList != null && tempList.Count > 0)
                    {
                        tempBaseResultDataValue.success = true;
                        tempBaseResultDataValue.ResultDataValue = "true";
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ResultDataValue = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 根据模块ID查询其包含的模块操作列表
        /// </summary>
        /// <param name="longModuleID"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_SearchModuleOperByModuleID(long longModuleID, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACModuleOper> tempEntityList = new EntityList<RBACModuleOper>();
            try
            {
                if (longModuleID > 0)
                {
                    tempEntityList.list = IBRBACModuleOper.SearchModuleOperIDByModuleID(longModuleID);
                    tempEntityList.count = tempEntityList.list.Count;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModuleOper>(tempEntityList);
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
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID(string strModuleOperID, bool isPreconditions)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempModuleOperID = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strModuleOperID.Trim())) || (strModuleOperID.ToLower().Trim() == "root")))
                    tempModuleOperID = Int64.Parse(strModuleOperID);
                tempBaseResultTree = IBRBACRoleRight.SearchRBACRowFilterTreeByModuleOperID(tempModuleOperID, isPreconditions);
                if (tempBaseResultTree != null && tempBaseResultTree.Tree != null && tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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

        #endregion

        #region IRBACService 业务逻辑相关

        //登录服务
        public virtual bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate)
        {
            bool tempBool = false;
            if (strUserAccount.Trim() == DicCookieSession.SuperUser && strPassWord == DicCookieSession.SuperUserPwd)
            {
                tempBool = true;
                if (!isValidate)
                    SetUserSession(null);
            }
            else
            {
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(strUserAccount);
                if (tempRBACUser.Count == 1)
                {
                    strPassWord = SecurityHelp.MD5Encrypt(strPassWord, SecurityHelp.PWDMD5Key);
                    if (tempRBACUser[0].IsUse.HasValue && tempRBACUser[0].IsUse.Value)
                    {
                        if (tempRBACUser[0].HREmployee.IsUse.HasValue && tempRBACUser[0].HREmployee.IsUse.Value && tempRBACUser[0].HREmployee.IsEnabled == 1)
                        {
                            tempBool = (tempRBACUser[0].Account == strUserAccount) && (tempRBACUser[0].PWD == strPassWord) && (!tempRBACUser[0].AccLock);
                            if (tempBool && !isValidate)
                            {
                                SetUserSession(tempRBACUser[0]);

                                try
                                {
                                    string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                                    string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                                    IBSLog.Entity = new SLog()
                                    {
                                        IP = "pi",
                                        OperateName = "安全日志",
                                        OperateType = "10000010",
                                        Comment = EmpName+" 登录成功",
                                        EmpID = long.Parse(EmpID),
                                        EmpName = EmpName
                                    };
                                    IBSLog.Save();

                                }
                                catch (Exception ee)
                                {
                                    ZhiFang.Common.Log.Log.Debug("LIIPCommonService.svc.cs.RBAC_BA_Login:平台写入日志错误：" + ee.ToString());
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("员工被禁用或者逻辑删除！");
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("员工帐号被逻辑删除！");
                    }
                }
            }
            return tempBool;
        }

        public virtual void SetUserSession(RBACUser rbacUser)
        {
            if (rbacUser != null)
            {
                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = "";
                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "";




                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, rbacUser.Account);//员工账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, rbacUser.UseCode);//员工代码
                //////////////////////////////////////改为Hospital
                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = rbacUser.LabID.ToString();//实验室ID
                if (rbacUser.LabID > 0)
                    HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "1";
                HttpContext.Current.Response.Cookies[DicCookieSession.UserID].Value = rbacUser.Id.ToString();//账户ID
                HttpContext.Current.Response.Cookies[DicCookieSession.UserAccount].Value = rbacUser.Account;//账户名
                HttpContext.Current.Response.Cookies[DicCookieSession.UseCode].Value = rbacUser.UseCode;//账户代码

                //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID

                if (rbacUser.HREmployee != null)
                {
                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, rbacUser.HREmployee.Id); //员工ID
                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);//员工姓名 

                    SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);//员工代码 



                    //员工时间戳
                    //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, rbacUser.HREmployee.Id.ToString());// 员工ID
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);// 员工姓名
                    Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);// 员工代码
                    if (rbacUser.HREmployee.HRDept != null)
                    {
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id);//部门ID
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id.ToString());//部门ID
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, rbacUser.HREmployee.HRDept.UseCode);//部门名称
                    }

                    //获取员工具有权限的模块列表
                    IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByHREmpID(rbacUser.HREmployee.Id);
                    if (tempRBACModuleList != null && tempRBACModuleList.Count > 0)
                    {
                        Dictionary<string, string> tempRBACModuleDic = new Dictionary<string, string>();
                        foreach (RBACModule tempRBACModule in tempRBACModuleList)
                        {
                            if (!tempRBACModuleDic.ContainsKey(tempRBACModule.Id.ToString()))
                                tempRBACModuleDic.Add(tempRBACModule.Id.ToString(), tempRBACModule.Url);
                        }
                        SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, tempRBACModuleDic);
                    }
                    //获取员工具有权限的模块操作列表
                    //IList<RBACModuleOper> tempRBACModuleOperList = IBRBACModuleOper.SearchModuleOperByHREmpID(rbacUser.HREmployee.Id);
                    //if (tempRBACModuleOperList != null && tempRBACModuleOperList.Count > 0)
                    //{
                    //    Dictionary<string, string> tempRBACModuleOperDic = new Dictionary<string, string>();
                    //    foreach (RBACModuleOper tempRBACModuleOper in tempRBACModuleOperList)
                    //    {
                    //        if (!tempRBACModuleOperDic.ContainsKey(tempRBACModuleOper.Id.ToString()))
                    //            tempRBACModuleOperDic.Add(tempRBACModuleOper.Id.ToString(), tempRBACModuleOper.OperateURL);
                    //    }
                    //    SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, tempRBACModuleOperDic);
                    //}
                }
            }
            else
            {
                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, "");//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, DicCookieSession.SuperUser);//账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, "");//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, ""); //员工ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, DicCookieSession.SuperUserName);//员工姓名 
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, "");//部门ID
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, "");//部门名称
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, "");
                SessionHelper.SetSessionValue(DicCookieSession.CurModuleID, "");

                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = "";//实验室ID
                HttpContext.Current.Response.Cookies[DicCookieSession.UserID].Value = "";//账户ID
                HttpContext.Current.Response.Cookies[DicCookieSession.UserAccount].Value = DicCookieSession.SuperUser;//账户名
                HttpContext.Current.Response.Cookies[DicCookieSession.UseCode].Value = "";//账户代码

                Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, "");// 员工ID
                Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, DicCookieSession.SuperUserName);// 员工姓名
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, "");//员工代码 
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, "");//部门ID
                Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, "");//部门名称            
            }
        }
        //注销服务
        public bool RBAC_BA_Logout(string strUserAccount)
        {
            try
            {
                try
                {
                    string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    IBSLog.Entity = new SLog()
                    {
                        IP = "pi",
                        OperateName = "安全日志",
                        OperateType = "10000010",
                        Comment = EmpName + " 退出登录",
                        EmpID = long.Parse(EmpID),
                        EmpName = EmpName
                    };
                    IBSLog.Save();

                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Debug("LIIPCommonService.svc.cs.RBAC_BA_Logout:平台写入日志错误：" + ee.ToString());
                }
                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, null);//实验室ID
                SessionHelper.SetSessionValue(DicCookieSession.UserAccount, null);//员工账户名
                SessionHelper.SetSessionValue(DicCookieSession.UseCode, null);//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, null); //员工ID
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, null);//员工姓名
                SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, null);//员工代码
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, null);//部门ID
                SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, null);//部门名称
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, null);
                SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, null);

                Cookie.CookieHelper.Remove(SysPublicSet.SysDicCookieSession.LabID);//实验室ID
                Cookie.CookieHelper.Remove(DicCookieSession.UserID);//账户ID
                Cookie.CookieHelper.Remove(DicCookieSession.UserAccount);//账户名
                Cookie.CookieHelper.Remove(DicCookieSession.UseCode);//账户代码
                Cookie.CookieHelper.Remove(DicCookieSession.EmployeeID);// 员工ID
                Cookie.CookieHelper.Remove(DicCookieSession.EmployeeName);// 员工姓名
                Cookie.CookieHelper.Remove(DicCookieSession.EmployeeUseCode);// 员工代码
                

                return IBRBACUser.RBAC_BA_Logout(strUserAccount);
            }
            catch
            {
                return false;
            }
        }
        //身份验证令牌服务
        public bool RBAC_RJ_Authentication()
        {
            throw new NotImplementedException();
        }

        public bool RBAC_RJ_JudgeModuleByRBACUserCode(string strUserCode, long longModuleID)
        {
            IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByUserCode(strUserCode);
            bool tempFlag = false;
            foreach (RBACRoleModule tempRBACRoleModule in tempRBACModuleList[0].RBACRoleModuleList)
            {
                if (tempRBACRoleModule.RBACModule.Id == longModuleID)
                {
                    tempFlag = true;
                    break;
                }
            }
            return tempFlag;
        }

        public BaseResultBool SYS_BA_GetEntityInfo()
        {
            throw new NotImplementedException();
        }

        public BaseResultBool SYS_BA_GetEntityListInfo()
        {
            throw new NotImplementedException();
        }

        public BaseResultBool RBAC_BA_GetVerificationcode()
        {
            throw new NotImplementedException();
        }

        public BaseResultBool RBAC_BA_GetRBACInfoByRBACUserCode(string strUserCode)
        {
            throw new NotImplementedException();
        }

        public BaseResultDataValue RBAC_RJ_CopyRoleRightByModuleOperID(long sourceModuleOperID, long targetModuleOperID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBRBACRoleRight.AddRBACRoleRightByModuleOperID(sourceModuleOperID, targetModuleOperID);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 行数据条件业务相关
        public IList<string> GetAssemblyList()
        {
            //实体程序集的集合
            IList<string> assemblyList = new List<string>();
            assemblyList.Add("ZhiFang.Entity.RBAC");
            assemblyList.Add("ZhiFang.Entity.ProjectProgressMonitorManage");
            assemblyList.Add("ZhiFang.Entity.OA");
            return assemblyList;
        }
        /// <summary>
        /// 数据对象结构树
        /// </summary>
        /// <param name="EntityName">数据对象类型名</param>
        /// <returns>数据对象树(EntityFrameTree)的Json字符串</returns>
        public BaseResultDataValue RBAC_UDTO_GetEntityFrameTree(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string tmpEntityName = EntityName;
            try
            {
                if (!string.IsNullOrEmpty(EntityName))
                {
                    List<EntityFrameTree> eftl = new List<EntityFrameTree>();
                    eftl = this.GetEntityFrameTree(EntityName);
                    string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(eftl);
                    brdv.ResultDataValue = result;
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }
        /// <summary>
        /// 实体对象选择列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where">只支持CName的模糊匹配查询</param>
        /// <returns></returns>
        public BaseResultDataValue RBAC_UDTO_GetEntityList(int page, int limit, string fields, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                EntityList<BaseResultEntityClassInfo> entityList = new EntityList<BaseResultEntityClassInfo>();
                List<BaseResultEntityClassInfo> ilistbreci = new List<BaseResultEntityClassInfo>();
                //实体程序集的集合
                IList<string> assemblyList = GetAssemblyList();
                foreach (var assembly in assemblyList)
                {
                    // Assembly.Load("ZhiFang.Digitlab.Entity").GetTypes()获取ZhiFang.Digitlab.Entity名空间中的对象类型列表
                    #region 单个实体程序集的处理
                    foreach (var a in System.Reflection.Assembly.Load(assembly).GetTypes())
                    {
                        BaseResultEntityClassInfo breci = new BaseResultEntityClassInfo();
                        breci.EName = a.Name;
                        foreach (var ta in a.GetCustomAttributes(true))
                        {
                            if (ta.GetType() == typeof(DataDescAttribute))
                            {
                                DataDescAttribute tat = (DataDescAttribute)ta;
                                if (tat.CName != null)
                                {
                                    breci.CName = tat.CName;
                                }
                                if (tat.ClassCName != null)
                                {
                                    breci.ClassName = a.Name;
                                }
                                if (tat.ShortCode != null)
                                {
                                    breci.ShortCode = tat.ShortCode;
                                }
                                if (tat.Desc != null)
                                {
                                    breci.Description = tat.Desc;
                                }
                            }
                        }
                        if (breci.CName != null)
                        {
                            ilistbreci.Add(breci);
                        }
                    }
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace").Trim() != "")
                    {
                        foreach (var a in System.Reflection.Assembly.Load(ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace")).GetTypes())
                        {
                            BaseResultEntityClassInfo breci = new BaseResultEntityClassInfo();
                            breci.EName = a.Name;
                            foreach (var ta in a.GetCustomAttributes(true))
                            {
                                if (ta.GetType() == typeof(DataDescAttribute))
                                {
                                    DataDescAttribute tat = (DataDescAttribute)ta;
                                    if (tat.CName != null)
                                    {
                                        breci.CName = tat.CName;
                                    }
                                    if (tat.ClassCName != null)
                                    {
                                        breci.ClassName = a.Name;
                                    }
                                    if (tat.ShortCode != null)
                                    {
                                        breci.ShortCode = tat.ShortCode;
                                    }
                                    if (tat.Desc != null)
                                    {
                                        breci.Description = tat.Desc;
                                    }
                                }
                            }
                            if (breci.CName != null)
                            {
                                ilistbreci.Add(breci);
                            }
                        }
                    }
                    #endregion
                }
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
                            var tempList = ilistbreci.Where(p => p.CName.StartsWith(strArr[1]) || p.ShortCode.ToLower().Contains(strArr[1].ToLower()));
                            tempList = tempList.Where((x, i) => tempList.ToList().FindIndex(z => z.ClassName == x.ClassName) == i).ToList();
                            ilistbreci = tempList.ToList();
                        }
                    }
                }
                #region 分页处理
                entityList.count = ilistbreci.Count;
                if (limit < ilistbreci.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = ilistbreci.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        ilistbreci = list.ToList();
                    }
                }
                entityList.list = ilistbreci;
                #endregion
                string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                brdv.ResultDataValue = result;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }

        /// <summary>
        /// 返回实体对象的结构树
        /// </summary>
        /// <param name="EntityName">实体名称</param>
        /// <returns>ListEntityFrameTree</returns>
        public List<EntityFrameTree> GetEntityFrameTree(string EntityName)
        {
            List<EntityFrameTree> eftl = new List<EntityFrameTree>();
            string entitynamespace = "ZhiFang.Entity.RBAC";
            //实体程序集的集合
            IList<string> assemblyList = GetAssemblyList();

            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
            {
                entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
            }
            string tmpEntityName = EntityName;
            if (!string.IsNullOrEmpty(EntityName))
            {
                if (EntityName.IndexOf('_') > 0)
                {
                    string[] entity = EntityName.Split('_');
                    Type tmpt = null;
                    foreach (var assembly in assemblyList)
                    {
                        tmpt = Assembly.Load(assembly).GetType(assembly + "." + entity[0]);
                        if (tmpt != null)
                        {
                            entitynamespace = assembly;
                            break;
                        }
                    }
                    if (tmpt == null)
                    {
                        return eftl;
                    }
                    for (int i = 1; i < entity.Length; i++)
                    {
                        tmpt = tmpt.GetProperty(entity[i]).PropertyType;
                    }
                    EntityName = tmpt.Name;
                }
                Type t = null;
                foreach (var assembly in assemblyList)
                {
                    t = Assembly.Load(assembly).GetType(assembly + "." + EntityName);
                    if (t != null)
                    {
                        entitynamespace = assembly;
                        break;
                    }
                }
                if (t == null)
                {
                    return eftl;
                }
                Type[] ta = Assembly.Load("ZhiFang.Entity.RBAC").GetTypes();
                Type[] tab = Assembly.Load(entitynamespace).GetTypes();

                //判断属性是否同时定义或声明了DataContractAttribute和DataDescAttribute，如果是，则添加该属性。
                if (t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataContractAttribute)) > 0 && t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataDescAttribute)) > 0)
                {
                    PropertyInfo[] pia = t.GetProperties();
                    foreach (var p in pia)
                    {
                        EntityFrameTree eft = new EntityFrameTree();
                        eft.InteractionField = tmpEntityName + '_' + p.Name;
                        eft.FieldClass = p.PropertyType.Name;
                        foreach (var pattribute in p.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                DataDescAttribute da = (DataDescAttribute)pattribute;
                                eft.text = da.CName;
                                //eft.Checked = false;
                            }
                        }
                        //属性类型是否存在entitynamespace命名空间下的实体对象列表中，如果不存在则为叶子节点
                        if (ta.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                        {
                            //属性类型是否存在entitynamespace命名空间下的实体对象列表中，如果不存在则为叶子节点
                            if (tab.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                            {
                                eft.leaf = true;
                            }
                            else
                            {
                                eft.leaf = false;
                            }
                        }
                        else
                        {
                            eft.leaf = false;
                        }
                        //过滤列表
                        if (p.PropertyType.Name != "IList`1")
                        {
                            eftl.Add(eft);
                        }
                    }
                }
            }
            return eftl;
        }
        /// <summary>
        /// 返回实体对象的基本属性,不包含子对象的及其属性信息
        /// </summary>
        /// <param name="EntityName">实体名称</param>
        /// <returns>ListEntityFrameTree</returns>
        public BaseResultDataValue RBAC_UDTO_SearchBaseEntityAttribute(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(EntityName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "EntityName不能为空!";
                return brdv;
            }
            List<EntityBaseAttribute> eftl = new List<EntityBaseAttribute>();
            try
            {
                string entitynamespace = "ZhiFang.Entity.RBAC";
                //实体程序集的集合
                IList<string> assemblyList = GetAssemblyList();

                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
                {
                    entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
                }
                string tmpEntityName = EntityName.Trim();
                if (EntityName.IndexOf('_') > 0)
                {
                    string[] entity = EntityName.Split('_');
                    Type tmpt = null;
                    foreach (var assembly in assemblyList)
                    {
                        tmpt = Assembly.Load(assembly).GetType(assembly + "." + entity[0]);
                        if (tmpt != null)
                        {
                            entitynamespace = assembly;
                            break;
                        }
                    }
                    if (tmpt == null)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "系统不存在Entity为:" + EntityName;
                        return brdv;
                    }
                    for (int i = 1; i < entity.Length; i++)
                    {
                        tmpt = tmpt.GetProperty(entity[i]).PropertyType;
                    }
                    EntityName = tmpt.Name;
                }
                Type t = null;
                foreach (var assembly in assemblyList)
                {
                    t = Assembly.Load(assembly).GetType(assembly + "." + EntityName);
                    if (t != null)
                    {
                        entitynamespace = assembly;
                        break;
                    }
                }
                if (t == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "系统不存在Entity为:" + EntityName;
                    return brdv;
                }
                Type[] ta = Assembly.Load("ZhiFang.Entity.RBAC").GetTypes();
                Type[] tab = Assembly.Load(entitynamespace).GetTypes();

                //判断属性是否同时定义或声明了DataContractAttribute和DataDescAttribute，如果是，则添加该属性。
                if (t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataContractAttribute)) > 0 && t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataDescAttribute)) > 0)
                {
                    PropertyInfo[] pia = t.GetProperties();
                    foreach (var p in pia)
                    {
                        if (p.Name.Contains("DataTimeStamp") || p.PropertyType.Name == "IList`1") continue;

                        //属性类型是否存在entitynamespace命名空间下的实体对象列表中，如果不存在则为叶子节点
                        if (ta.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                        {
                            //属性类型是否存在entitynamespace命名空间下的实体对象列表中，如果不存在则为叶子节点
                            if (tab.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                            {
                                EntityBaseAttribute eft = new EntityBaseAttribute();
                                eft.InteractionField = tmpEntityName.ToLower() + '.' + p.Name;
                                eft.ValueType = p.PropertyType.Name;
                                foreach (var pattribute in p.GetCustomAttributes(false))
                                {
                                    if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                                    {
                                        DataDescAttribute da = (DataDescAttribute)pattribute;
                                        eft.CName = da.CName;
                                        //eft.Checked = false;
                                    }
                                }
                                eftl.Add(eft);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(eftl);
            brdv.ResultDataValue = result;
            return brdv;
        }

        //新增行数据过滤条件时同时处行数据条件的理角色权限
        public BaseResultDataValue RBAC_UDTO_AddRBACRowFilterAndRBACRoleRightByModuleOperId(RBACRowFilter entity, long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBRBACRowFilter.RBACRowFilterAndRBACRoleRightAddByModuleOperId(moduleOperId, addRoleIdStr, isDefaultCondition, editRoleRightIdStr);
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRowFilter.Get(IBRBACRowFilter.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRowFilter.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("新增行数据过滤条件错误信息:" + ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //修改行数据过滤条件时同时处行数据条件的理角色权限
        public BaseResultBool RBAC_UDTO_UpdateRBACRowFilterAndRBACRoleRightByFieldAndModuleOperId(RBACRowFilter entity, string fields, long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRowFilter.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool = IBRBACRowFilter.UpdateRBACRowFilterAndRBACRoleRightByModuleOperId(tempArray, moduleOperId, addRoleIdStr, isDefaultCondition, editRoleRightIdStr);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRowFilter.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("修改行数据过滤条件错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 依行过滤条件ID和模块操作ID删除行数据条件操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        public BaseResultBool RBAC_UDTO_DeleteRBACRoleRightByModuleOperId(long id, long moduleOperId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBRBACRowFilter.DeleteRBACRoleRightByModuleOperId(id, moduleOperId);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("删除行数据条件操作错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightByModuleIdAndModuleOperID(int page, int limit, string fields, long moduleId, long moduleOperId, string sort, bool isPlanish, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRoleVO> tempEntityList = new EntityList<RBACRoleVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRoleRight.SearchRBACRoleRightByModuleIdAndModuleOperID(page, limit, CommonServiceMethod.GetSortHQL(sort), moduleId, moduleOperId, where);
                }
                else
                {
                    tempEntityList = IBRBACRoleRight.SearchRBACRoleRightByModuleIdAndModuleOperID(page, limit, "", moduleId, moduleOperId, where);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleVO>(tempEntityList);
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

        #region 预置条件
        public BaseResultDataValue RBAC_RJ_SearchRBACRowFilterTreeByPreconditionsId(string id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long preconditionsId = 0;
            try
            {
                if (!((string.IsNullOrEmpty(id.Trim())) || (id.ToLower().Trim() == "root")))
                    preconditionsId = Int64.Parse(id);
                tempBaseResultTree = IBRBACRoleRight.SearchRBACRowFilterTreeByPreconditionsId(preconditionsId);
                if (tempBaseResultTree != null && tempBaseResultTree.Tree != null && tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
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
        public BaseResultDataValue RBAC_UDTO_AddRBACRowFilterAndRBACRoleRightByPreconditionsId(RBACRowFilter entity, long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBRBACRowFilter.AddRBACRowFilterAndRBACRoleRightByPreconditionsId(preconditionsId, addRoleIdStr, editRoleRightIdStr, moduleOperId);
                if (tempBaseResultDataValue.success)
                {
                    IBRBACRowFilter.Get(IBRBACRowFilter.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBRBACRowFilter.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("依预置条件ID新增行数据过滤条件错误信息:" + ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RBAC_UDTO_UpdateRBACRowFilterAndRBACRoleRightByFieldAndPreconditionsId(RBACRowFilter entity, string fields, long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId)
        {
            IBRBACRowFilter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBRBACRowFilter.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool = IBRBACRowFilter.UpdateRBACRowFilterAndRBACRoleRightByPreconditionsId(tempArray, preconditionsId, addRoleIdStr, editRoleRightIdStr, moduleOperId);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBRBACRowFilter.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("依预置条件ID,修改行数据过滤条件错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 依行过滤条件ID和预置条件ID删除行数据条件操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        public BaseResultBool RBAC_UDTO_DeleteRBACRowFilterAndRBACRoleRightByPreconditionsId(long id, long preconditionsId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBRBACRowFilter.DeleteRBACRowFilterAndRBACRoleRightByPreconditionsId(id, preconditionsId);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("删除行数据条件操作错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightByModuleIdAndPreconditionsId(int page, int limit, string fields, long moduleId, long preconditionsId, string sort, bool isPlanish, string where, string rowFilterId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<RBACRoleVO> tempEntityList = new EntityList<RBACRoleVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBRBACRoleRight.SearchRBACRoleRightByModuleIdAndPreconditionsId(page, limit, CommonServiceMethod.GetSortHQL(sort), moduleId, preconditionsId, where, rowFilterId);
                }
                else
                {
                    tempEntityList = IBRBACRoleRight.SearchRBACRoleRightByModuleIdAndPreconditionsId(page, limit, "", moduleId, preconditionsId, where, rowFilterId);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleVO>(tempEntityList);
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
        #endregion

        #region 行数据条件的相关复制新增服务
        /// <summary>
        /// 将选择的模块服务复制新增到指定的模块中
        /// </summary>
        /// <param name="moduleId">待复制的模块ID</param>
        /// <param name="copyModuleOpeIdStr">选择需要复制的模块服务Id字符串(123,222)</param>
        /// <returns></returns>
        public BaseResultBool RBAC_UDTO_CopyRBACModuleOperOfModuleId(long moduleId, string copyModuleOpeIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBRBACModuleOper.CopyRBACModuleOperOfModule(moduleId, copyModuleOpeIdStr);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("将选择的模块服务复制新增到指定的模块中操作错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RBAC_UDTO_CopyPreconditionsOfModuleOperId(long moduleoperId, string copyModuleOpeIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBRBACPreconditions.CopyPreconditionsOfRBACModuleOper(moduleoperId, copyModuleOpeIdStr);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("将选择的模块服务的预置条件项复制新增到指定的模块服务错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RBAC_UDTO_CopyRBACRowFilterOfPreconditionsIdStr(string preconditionsIdStr, string rowfilterIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBRBACRowFilter.CopyRBACRowFilterOfPreconditionsIdStr(preconditionsIdStr, rowfilterIdStr);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("将某一预置条件下选择的行过滤条件复制新增到指定的预置条件项错误信息:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #endregion
             
    }
}