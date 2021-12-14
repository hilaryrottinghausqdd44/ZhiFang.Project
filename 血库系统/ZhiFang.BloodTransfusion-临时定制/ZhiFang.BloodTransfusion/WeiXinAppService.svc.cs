using System;
using System.ServiceModel.Activation;
using ZhiFang.Common.Public;
using ZhiFang.BloodTransfusion.ServerContract;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WeiXin;
using ZhiFang.IBLL.WeiXin;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.RBAC;
using static ZhiFang.Entity.Base.SysPublicSet;

namespace ZhiFang.BloodTransfusion
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WeiXinAppService : IWeiXinAppService
    {
        IBBAccountType IBBAccountType { get; set; }
        IBBWeiXinEmpLink IBBWeiXinEmpLink { get; set; }
        IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IBBWeiXinUserGroup IBBWeiXinUserGroup { get; set; }
        IBBWeiXinPushMessageTemplate IBBWeiXinPushMessageTemplate { get; set; }
        //IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }

        #region BAccountType
        //Add  BAccountType
        public BaseResultDataValue ST_UDTO_AddBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBAccountType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBAccountType.Get(IBBAccountType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBAccountType.Entity);
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
        //Update  BAccountType
        public BaseResultBool ST_UDTO_UpdateBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBAccountType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BAccountType
        public BaseResultBool ST_UDTO_UpdateBAccountTypeByField(BAccountType entity, string fields)
        {
            IBBAccountType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBAccountType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBAccountType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBAccountType.Edit();
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
        //Delele  BAccountType
        public BaseResultBool ST_UDTO_DelBAccountType(long longBAccountTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBAccountType.Remove(longBAccountTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            EntityList<BAccountType> tempEntityList = new EntityList<BAccountType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBAccountType.Search();
                tempEntityList.count = IBBAccountType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BAccountType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BAccountType> tempEntityList = new EntityList<BAccountType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBAccountType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBAccountType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BAccountType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBAccountTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBAccountType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BAccountType>(tempEntity);
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

        #region BWeiXinEmpLink
        //Add  BWeiXinEmpLink
        public BaseResultDataValue ST_UDTO_AddBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinEmpLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinEmpLink.Get(IBBWeiXinEmpLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinEmpLink.Entity);
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
        //Update  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinEmpLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateBWeiXinEmpLinkByField(BWeiXinEmpLink entity, string fields)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinEmpLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinEmpLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinEmpLink.Edit();
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
        //Delele  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_DelBWeiXinEmpLink(long longBWeiXinEmpLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinEmpLink.Remove(longBWeiXinEmpLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            EntityList<BWeiXinEmpLink> tempEntityList = new EntityList<BWeiXinEmpLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinEmpLink.Search();
                tempEntityList.count = IBBWeiXinEmpLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinEmpLink> tempEntityList = new EntityList<BWeiXinEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinEmpLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinEmpLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinEmpLink>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AddBWeiXinEmpLinkByUserAccount(string strUserAccount, string strPassWord, bool isValidate)
        {
            //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：" + strUserAccount.ToString()+ "@@@strPassWord:"+ strPassWord);
            // IBBWeiXinEmpLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：1");
                string ErrorInfo;
                //ZhiFang.Common.Log.Log.Debug("WeiXinOpenID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.WeiXinOpenID));
                string WeiXinOpenID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID);
                if (WeiXinOpenID != null && WeiXinOpenID.Trim() != "")
                {
                    HREmployee emp;
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：2");
                    tempBaseResultDataValue.success = IBBWeiXinEmpLink.AddByUserAccountOpenID(strUserAccount, strPassWord, WeiXinOpenID, out ErrorInfo, out emp);
                    if (emp != null)
                    {
                        Cookie.CookieHelper.Write(SysDicCookieSession.LabID, "");
                        //Cookie.CookieHelper.Write(DicCookieSession.IsLabFlag, "");

                        SessionHelper.SetSessionValue(SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                        SessionHelper.SetSessionValue(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);//员工账户名
                        SessionHelper.SetSessionValue(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);//员工代码

                        Cookie.CookieHelper.Write(SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                        //if (emp.LabID > 0)
                        //    Cookie.CookieHelper.Write(DicCookieSession.IsLabFlag, "1");

                        Cookie.CookieHelper.Write(DicCookieSession.UserID, emp.RBACUserList[0].Id.ToString());
                        Cookie.CookieHelper.Write(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);
                        Cookie.CookieHelper.Write(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);

                        //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID
                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, emp.Id); //员工ID
                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, emp.CName);//员工姓名 

                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, emp.UseCode);//员工代码 

                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, emp.HRDept.Id);//部门ID
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称

                        //员工时间戳
                        //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, emp.Id.ToString());// 员工ID
                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, emp.CName);// 员工姓名
                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, emp.UseCode);// 员工代码

                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, emp.HRDept.Id.ToString());//部门ID
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称

                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, emp.HRDept.UseCode);//部门名称
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount.Emp为空!");
                    }
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：3");
                }
                else
                {
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：4");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：未能读取Cookie中的OpenId";
                    ZhiFang.Common.Log.Log.Debug("错误信息：未能读取Cookie中的OpenId");
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("账户和微信绑定异常，错误信息：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region BWeiXinAccount
        //Add  BWeiXinAccount
        public BaseResultDataValue ST_UDTO_AddBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinAccount.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinAccount.Get(IBBWeiXinAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinAccount.Entity);
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
        //Update  BWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateBWeiXinAccountByField(BWeiXinAccount entity, string fields)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinAccount.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinAccount.Edit();
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
        //Delele  BWeiXinAccount
        public BaseResultBool ST_UDTO_DelBWeiXinAccount(long longBWeiXinAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinAccount.Remove(longBWeiXinAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinAccount.Search();
                tempEntityList.count = IBBWeiXinAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinAccount>(tempEntity);
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

        #region BWeiXinUserGroup
        //Add  BWeiXinUserGroup
        public BaseResultDataValue ST_UDTO_AddBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinUserGroup.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinUserGroup.Get(IBBWeiXinUserGroup.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinUserGroup.Entity);
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
        //Update  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinUserGroup.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateBWeiXinUserGroupByField(BWeiXinUserGroup entity, string fields)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinUserGroup.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinUserGroup.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinUserGroup.Edit();
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
        //Delele  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_DelBWeiXinUserGroup(long longBWeiXinUserGroupID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinUserGroup.Remove(longBWeiXinUserGroupID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            EntityList<BWeiXinUserGroup> tempEntityList = new EntityList<BWeiXinUserGroup>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinUserGroup.Search();
                tempEntityList.count = IBBWeiXinUserGroup.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinUserGroup>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinUserGroup> tempEntityList = new EntityList<BWeiXinUserGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinUserGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinUserGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinUserGroup>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinUserGroup.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinUserGroup>(tempEntity);
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

        public BaseResultDataValue PushAddReaBmsCenOrderDoc(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //try
            //{
            //    tempBaseResultDataValue = IBReaBmsCenOrderDoc.ReaBmsCenOrderDocAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);
               
            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    ZhiFang.Common.Log.Log.Debug("PushAddReaBmsCenOrderDoc.ex:" + ex.ToString());
            //    //throw new Exception(ex.Message);
            //}
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue PushConfirmReaBmsCenOrderDoc(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //try
            //{
            //    tempBaseResultDataValue = IBReaBmsCenOrderDoc.ReaBmsCenOrderDocConfirmAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);

            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    ZhiFang.Common.Log.Log.Debug("PushAddReaBmsCenOrderDoc.ex:" + ex.ToString());
            //    //throw new Exception(ex.Message);
            //}
            return tempBaseResultDataValue;
        }
    }
}
