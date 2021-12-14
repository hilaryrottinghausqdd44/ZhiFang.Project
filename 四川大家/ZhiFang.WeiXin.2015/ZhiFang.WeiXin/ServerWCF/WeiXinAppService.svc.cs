using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.Common;
using System.Web;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.BusinessObject.LabObject;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WeiXin
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WeiXinAppService : ZhiFang.WeiXin.ServerContract.IWeiXinAppService
    {
        IBLL.IBBAccountType IBBAccountType { get; set; }

        IBLL.IBBDoctorAccount IBBDoctorAccount { get; set; }

        IBLL.IBBIcons IBBIcons { get; set; }

        IBLL.IBBSearchAccount IBBSearchAccount { get; set; }

        IBLL.IBBWeiXinAccount IBBWeiXinAccount { get; set; }

        IBLL.IBBWeiXinUserGroup IBBWeiXinUserGroup { get; set; }

        IBLL.IBBScanningBarCodeReportForm IBBScanningBarCodeReportForm { get; set; }

        IBLL.IBBSearchAccountReportForm IBBSearchAccountReportForm { get; set; }

        IBLL.IBBHospitalSearch IBBHospitalSearch { get; set; }

        IBLL.IBBAccountHospitalSearchContext IBBAccountHospitalSearchContext { get; set; }

        #region BAccountType
        //Add  BAccountType
        public BaseResultDataValue ST_UDTO_AddBAccountType(ZhiFang.WeiXin.Entity.BAccountType entity)
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
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

        #region BDoctorAccount
        //Add  BDoctorAccount
        public BaseResultDataValue ST_UDTO_AddBDoctorAccount(BDoctorAccount entity)
        {
            IBBDoctorAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBDoctorAccount.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBDoctorAccount.Get(IBBDoctorAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBDoctorAccount.Entity);
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
        //Update  BDoctorAccount
        public BaseResultBool ST_UDTO_UpdateBDoctorAccount(BDoctorAccount entity)
        {
            IBBDoctorAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDoctorAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BDoctorAccount
        public BaseResultBool ST_UDTO_UpdateBDoctorAccountByField(BDoctorAccount entity, string fields)
        {
            IBBDoctorAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBDoctorAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBDoctorAccount.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBDoctorAccount.Edit();
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
        //Delele  BDoctorAccount
        public BaseResultBool ST_UDTO_DelBDoctorAccount(long longBDoctorAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDoctorAccount.Remove(longBDoctorAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBDoctorAccount(BDoctorAccount entity)
        {
            IBBDoctorAccount.Entity = entity;
            EntityList<BDoctorAccount> tempEntityList = new EntityList<BDoctorAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBDoctorAccount.Search();
                tempEntityList.count = IBBDoctorAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDoctorAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBDoctorAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDoctorAccount> tempEntityList = new EntityList<BDoctorAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBDoctorAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBDoctorAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDoctorAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBDoctorAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBDoctorAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDoctorAccount>(tempEntity);
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

        #region BIcons
        //Add  BIcons
        public BaseResultDataValue ST_UDTO_AddBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBIcons.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBIcons.Get(IBBIcons.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBIcons.Entity);
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
        //Update  BIcons
        public BaseResultBool ST_UDTO_UpdateBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBIcons.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BIcons
        public BaseResultBool ST_UDTO_UpdateBIconsByField(BIcons entity, string fields)
        {
            IBBIcons.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBIcons.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBIcons.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBIcons.Edit();
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
        //Delele  BIcons
        public BaseResultBool ST_UDTO_DelBIcons(long longBIconsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBIcons.Remove(longBIconsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            EntityList<BIcons> tempEntityList = new EntityList<BIcons>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBIcons.Search();
                tempEntityList.count = IBBIcons.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BIcons>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BIcons> tempEntityList = new EntityList<BIcons>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBIcons.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBIcons.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BIcons>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBIconsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBIcons.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BIcons>(tempEntity);
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

        #region BSearchAccount
        //Add  BSearchAccount
        public BaseResultDataValue ST_UDTO_AddBSearchAccount(BSearchAccount entity)
        {
            IBBSearchAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBSearchAccount.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBSearchAccount.Get(IBBSearchAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBSearchAccount.Entity);
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
        //Update  BSearchAccount
        public BaseResultBool ST_UDTO_UpdateBSearchAccount(BSearchAccount entity)
        {
            IBBSearchAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBSearchAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BSearchAccount
        public BaseResultBool ST_UDTO_UpdateBSearchAccountByField(BSearchAccount entity, string fields)
        {
            IBBSearchAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBSearchAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBSearchAccount.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBSearchAccount.Edit();
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
        //Delele  BSearchAccount
        public BaseResultBool ST_UDTO_DelBSearchAccount(long longBSearchAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBSearchAccount.Remove(longBSearchAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBSearchAccount(BSearchAccount entity)
        {
            IBBSearchAccount.Entity = entity;
            EntityList<BSearchAccount> tempEntityList = new EntityList<BSearchAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBSearchAccount.Search();
                tempEntityList.count = IBBSearchAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BSearchAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBSearchAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BSearchAccount> tempEntityList = new EntityList<BSearchAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBSearchAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBSearchAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BSearchAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBSearchAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBSearchAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BSearchAccount>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AddBSearchAccountVO(SearchAccountVO entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            if (entity == null || entity.Name.Trim().Length < 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：SearchAccountName为空！";
                return tempBaseResultDataValue;
            }
            if (!(entity.SexID.HasValue) || entity.SexID.Value < 1 || entity.SexID.Value > 3)
            {
                entity.SexID = 1;
            }
            var entitybo = new BSearchAccount();
            entitybo.Name = entity.Name;
            entitybo.SexID = entity.SexID.Value;
            entitybo.WeiXinUserID = long.Parse(ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinUserID));
            entitybo.WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID);
            try
            {
                if (entity.Birthday != null)
                {
                    entitybo.Birthday = Convert.ToDateTime(entity.Birthday);
                }
                if (entity.MobileCode != null)
                {
                    entitybo.MobileCode = entity.MobileCode;
                }
                if (entity.IDNumber != null)
                {
                    entitybo.IDNumber = entity.IDNumber;
                }
                if (entity.MediCare != null)
                {
                    entitybo.MediCare = entity.MediCare;
                }
                IBBSearchAccount.Entity = entitybo;
                List<BAccountHospitalSearchContext> bahscl = new List<BAccountHospitalSearchContext>();

                if (entity.SearchList.Count > 0)
                {
                    foreach (var skey in entity.SearchList)
                    {
                        bahscl.Add(new BAccountHospitalSearchContext() { FieldsCode = skey.FieldsCode, FieldsValue = skey.FieldsValue, Comment = skey.Comment, WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID), DispOrder = skey.DispOrder, Name = entity.Name });
                    }

                    tempBaseResultDataValue.success = IBBSearchAccount.Add(bahscl);
                    if (tempBaseResultDataValue.success)
                    {
                        tempBaseResultDataValue.ResultDataValue = IBBSearchAccount.Entity.Id.ToString();
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "查询条件为空！";
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

        public BaseResultDataValue ST_UDTO_UpdateBSearchAccountVO(SearchAccountVO entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null || entity.Name.Trim().Length < 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：SearchAccountName为空！";
                return tempBaseResultDataValue;
            }
            if (!(entity.SexID.HasValue) || entity.SexID.Value < 1 || entity.SexID.Value > 3)
            {
                entity.SexID = 1;
            }
            var entitybo = new BSearchAccount();
            entitybo.Id = long.Parse(entity.Id);
            entitybo.Name = entity.Name;
            entitybo.SexID = entity.SexID.Value;
            entitybo.WeiXinUserID = long.Parse(ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinUserID));
            entitybo.WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID);
            try
            {
                if (entity.Birthday != null)
                {
                    entitybo.Birthday = Convert.ToDateTime(entity.Birthday);
                }
                if (entity.MobileCode != null)
                {
                    entitybo.MobileCode = entity.MobileCode;
                }
                if (entity.IDNumber != null)
                {
                    entitybo.IDNumber = entity.IDNumber;
                }
                if (entity.MediCare != null)
                {
                    entitybo.MediCare = entity.MediCare;
                }
                IBBSearchAccount.Entity = entitybo;
                List<BAccountHospitalSearchContext> bahscl = new List<BAccountHospitalSearchContext>();

                if (entity.SearchList.Count > 0)
                {
                    foreach (var skey in entity.SearchList)
                    {
                        bahscl.Add(new BAccountHospitalSearchContext() { FieldsCode = skey.FieldsCode, FieldsValue = skey.FieldsValue, Comment = skey.Comment, WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID), DispOrder = skey.DispOrder, Name = entity.Name });
                    }

                    tempBaseResultDataValue.success = IBBSearchAccount.Update(bahscl);
                    if (tempBaseResultDataValue.success)
                    {
                        tempBaseResultDataValue.ResultDataValue = IBBSearchAccount.Entity.Id.ToString();
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "查询条件为空！";
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

        public BaseResultDataValue ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            List<BSearchAccount> tempEntityList = new List<BSearchAccount>();
            List<SearchAccountVO> tempEntityListvo = new List<SearchAccountVO>();
            try
            {
                tempEntityList = IBBSearchAccount.SearchSearchAccountVOListByHQL(long.Parse(ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinUserID)), ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID));
                try
                {
                    foreach (var bsa in tempEntityList)
                    {
                        //ZhiFang.Common.Log.Log.Debug("Id =" + bsa.Id + ", IDNumber = " + bsa.IDNumber + ", MediCare =" + bsa.MediCare + ", MobileCode = " + bsa.MobileCode + ", Name = " + bsa.Name + ", SexID =" + bsa.SexID);
                        SearchAccountVO savo = new SearchAccountVO() { Id = bsa.Id.ToString(), IDNumber = bsa.IDNumber, MediCare = bsa.MediCare, MobileCode = bsa.MobileCode, Name = bsa.Name, SexID = bsa.SexID, UnReadRFCount = bsa.UnReadCount, RFIndexList = bsa.RFIndexList };
                        if (bsa.Birthday.HasValue)
                        {
                            savo.Birthday = bsa.Birthday.Value.ToString("yyyy-MM-dd");
                        }
                        if (bsa.ListBAccountHospitalSearchContext != null && bsa.ListBAccountHospitalSearchContext.Count > 0)
                        {
                            savo.SearchList = new List<SearchAccountSearchKeyVO>();
                            foreach (var item in bsa.ListBAccountHospitalSearchContext)
                            {
                                savo.SearchList.Add(new SearchAccountSearchKeyVO() { Comment = item.Comment, FieldsCode = item.FieldsCode, FieldsValue = item.FieldsValue });
                            }

                        }
                        tempEntityListvo.Add(savo);
                    }
                    tempBaseResultDataValue.success = true;
                    tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(tempEntityListvo);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Debug(ex.ToString());
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

        #region BWeiXinAccount
        //Add  BWeiXinAccount
        public BaseResultDataValue ST_UDTO_AddBWeiXinAccount(BWeiXinAccount entity)
        {
            
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity.PassWord != null&& entity.PassWord.Trim() !="" )
                {
                    entity.PassWord= SecurityHelp.MD5Encrypt(entity.PassWord, SecurityHelp.PWDMD5Key);
                }
                IBBWeiXinAccount.Entity = entity;
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
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

        //Update  BWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid(string MobileCode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (MobileCode != null)
                {
                    long WeiXinAccountId;
                    tempBaseResultBool.success = IBBWeiXinAccount.UpdateBWeiXinAccountMobileCodeByOpenid(MobileCode, ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID), out WeiXinAccountId);
                    ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinUserID, WeiXinAccountId.ToString());
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：手机号为空！";
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

        public BaseResultBool Login(string MobileCode, string Pwd)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (MobileCode != null)
                {

                    IList<BWeiXinAccount> BWeiXinAccount = IBBWeiXinAccount.SearchListByHQL(" MobileCode='" + MobileCode + "'");
                    if (BWeiXinAccount.Count == 1)
                    {
                        //ZhiFang.Common.Log.Log.Debug("Pwd:" + Pwd + "@SecurityHelp.MD5Encrypt(Pwd, SecurityHelp.PWDMD5Key)" + SecurityHelp.MD5Encrypt(Pwd, SecurityHelp.PWDMD5Key) + "@BWeiXinAccount[0].PassWord:" + BWeiXinAccount[0].PassWord);
                        Pwd = SecurityHelp.MD5Encrypt(Pwd, SecurityHelp.PWDMD5Key);

                        tempBaseResultBool.BoolFlag = (BWeiXinAccount[0].MobileCode == MobileCode) && (BWeiXinAccount[0].PassWord == Pwd);
                        if (tempBaseResultBool.BoolFlag)
                        {
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinUserID, BWeiXinAccount[0].Id.ToString());
                            ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinOpenID, BWeiXinAccount[0].WeiXinAccount.ToString());
                        }
                    }

                    tempBaseResultBool.success = tempBaseResultBool.BoolFlag;
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：手机号为空！";
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

        public BaseResultBool Capchcwoaduntnge(string OldPwd, string NewPwd)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (OldPwd != null && OldPwd.Trim() != "" && NewPwd != null && NewPwd.Trim() != "")
                {

                    bool flag = IBBWeiXinAccount.ChangePwd(OldPwd, NewPwd, ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID));
                    tempBaseResultBool.success = flag;
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：新密码或旧密码为空！";
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

        public BaseResultBool ChangeLoginPasswordFlag(bool Flag)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                bool resultflag = IBBWeiXinAccount.ChangeLoginPasswordFlag(Flag, ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID));
                tempBaseResultBool.success = resultflag;
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue WXADS_DoctorAccountBindWeiXinAccountChange(long id,string AccountCode, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (AccountCode==null || AccountCode.Trim()=="")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + "绑定号为空！";
                    return brdv;
                }
                if (password == null || password.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + "密码为空！";
                    return brdv;
                }
                brdv = IBBWeiXinAccount.DoctorAccountBindWeiXinAccountChange(id, AccountCode, password);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WXADS_DoctorAccountBindWeiXinAccountChange.错误信息：" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue WXADS_SearchWeiXinAccount_User(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinAccount.WXADS_SearchWeiXinAccount_User(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinAccount.WXADS_SearchWeiXinAccount_User(where, " AddTime " ,page, limit);
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
                    tempBaseResultDataValue.ErrorInfo = "WXADS_SearchWeiXinAccount_User序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "WXADS_SearchWeiXinAccount_User查询错误：" + ex.Message;
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
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

        #region BScanningBarCodeReportForm
        //Add  BScanningBarCodeReportForm
        public BaseResultDataValue ST_UDTO_AddBScanningBarCodeReportForm(BScanningBarCodeReportForm entity)
        {
            IBBScanningBarCodeReportForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBScanningBarCodeReportForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBScanningBarCodeReportForm.Get(IBBScanningBarCodeReportForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBScanningBarCodeReportForm.Entity);
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
        //Update  BScanningBarCodeReportForm
        public BaseResultBool ST_UDTO_UpdateBScanningBarCodeReportForm(BScanningBarCodeReportForm entity)
        {
            IBBScanningBarCodeReportForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBScanningBarCodeReportForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BScanningBarCodeReportForm
        public BaseResultBool ST_UDTO_UpdateBScanningBarCodeReportFormByField(BScanningBarCodeReportForm entity, string fields)
        {
            IBBScanningBarCodeReportForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBScanningBarCodeReportForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBScanningBarCodeReportForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBScanningBarCodeReportForm.Edit();
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
        //Delele  BScanningBarCodeReportForm
        public BaseResultBool ST_UDTO_DelBScanningBarCodeReportForm(long longBScanningBarCodeReportFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBScanningBarCodeReportForm.Remove(longBScanningBarCodeReportFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportForm(BScanningBarCodeReportForm entity)
        {
            IBBScanningBarCodeReportForm.Entity = entity;
            EntityList<BScanningBarCodeReportForm> tempEntityList = new EntityList<BScanningBarCodeReportForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBScanningBarCodeReportForm.Search();
                tempEntityList.count = IBBScanningBarCodeReportForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BScanningBarCodeReportForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BScanningBarCodeReportForm> tempEntityList = new EntityList<BScanningBarCodeReportForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBScanningBarCodeReportForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBScanningBarCodeReportForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BScanningBarCodeReportForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBScanningBarCodeReportForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BScanningBarCodeReportForm>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AddBScanningBarCodeReportForm(string Barcode, string SearchUserName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                BScanningBarCodeReportForm entity = new BScanningBarCodeReportForm();
                IBBScanningBarCodeReportForm.Entity = entity;
                IBBScanningBarCodeReportForm.Entity.BarCode = Barcode;
                IBBScanningBarCodeReportForm.Entity.Name = SearchUserName;
                IBBScanningBarCodeReportForm.Entity.WeiXinUserID = long.Parse(ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinUserID));
                IBBScanningBarCodeReportForm.Entity.WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID);

                int count;
                tempBaseResultDataValue.success = IBBScanningBarCodeReportForm.AddSearch(Barcode, SearchUserName, out count);
                if (tempBaseResultDataValue.success)
                {
                    tempBaseResultDataValue.ResultDataValue = "{Id:" + IBBScanningBarCodeReportForm.Entity.Id.ToString() + ",count:" + count + "}";
                }
                else
                {
                    tempBaseResultDataValue.ErrorInfo = "错误信息：新增扫一扫失败！";
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

        public BaseResultDataValue ST_UDTO_GetBScanningBarCodeReportFormList(string BScanningBarCodeReportFormID, string Barcode, string SearchUserName)
        {
            throw new Exception();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                List<AppRFObject> LARFO = new List<AppRFObject>();
                var BSearchAccountReportFormList = IBBSearchAccountReportForm.SearchRF(long.Parse(BScanningBarCodeReportFormID), Barcode, SearchUserName);
                foreach (var bsarf in BSearchAccountReportFormList)
                {
                    LARFO.Add(AppRFObject.SetValue(bsarf));
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
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

        public BaseResultDataValue ST_UDTO_GetBScanningBarCodeReportFormListByBarcodeSearchUserName(string Barcode, string SearchUserName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                List<AppRFObject> LARFO = new List<AppRFObject>();
                var BSearchAccountReportFormList = IBBSearchAccountReportForm.SearchRF(Barcode, SearchUserName);
                foreach (var bsarf in BSearchAccountReportFormList)
                {
                    LARFO.Add(AppRFObject.SetValue(bsarf));
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
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

        public BaseResultDataValue ST_UDTO_GetBSearchAccountRFListByBarcode(string Barcode, int Page, int limit)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            List<BSearchAccountReportForm> tempList = new List<BSearchAccountReportForm>();
            List<AppRFObject> LARFO = new List<AppRFObject>();
            try
            {
                if (Barcode == null || Barcode.Trim() == "")
                {
                    tempBaseResultDataValue.ErrorInfo = "参数错误:Barcode为空!";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }
                string hqlwhere = "";
                hqlwhere = " Barcode='" + Barcode.ToString() + "' ";
                tempList = IBBSearchAccountReportForm.SearchListByHQL(hqlwhere, Page, limit).list.ToList();
                foreach (var bsarf in tempList)
                {
                    AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
                    if (bsarf.ReportFormTime.HasValue)
                    {
                        info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    if (bsarf.COLLECTDATE.HasValue)
                    {
                        info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                    }
                    if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
                    {
                        info.PatNumber = bsarf.PatNo;
                    }
                    AppRFObject ARFO = new AppRFObject() { info = info, list = new List<AppRIResult>() };
                    //if (bsarf.ItemList != null)
                    //{
                    //    foreach (var item in bsarf.ItemList)
                    //    {
                    //        ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MicroList != null)
                    //{
                    //    foreach (var item in bsarf.MicroList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MarrowList != null)
                    //{
                    //    foreach (var item in bsarf.MarrowList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    LARFO.Add(ARFO);
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetBSearchAccountRFListByPatNoAndName:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return tempBaseResultDataValue;
        }
        
        #endregion

        #region BHospitalSearchKey
        public BaseResultDataValue ST_UDTO_GetBHospitalSearchKeyList(string HospitalCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BHospitalSearch> tempEntityList = new EntityList<BHospitalSearch>();
            try
            {
                tempEntityList = IBBHospitalSearch.SearchListByHQL(" HospitalCode='" + HospitalCode + "'", 0, 0);
                //BSearchAccount_Name,BSearchAccount_Id,BSearchAccount_DataTimeStamp
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("BHospitalSearch_Name,BHospitalSearch_Id,BHospitalSearch_DataTimeStamp");
                try
                {
                    //if (isPlanish)
                    //{
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BHospitalSearch>(tempEntityList);
                    //}
                    //else
                    //{
                    //    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    //}
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

        #region 手机注册
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="MobileCode">手机号</param>
        /// <returns>是否成功和时间</returns>
        public BaseResultDataValue SJBhttp_SmsOperator_Vaild(string MobileCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string vaildcode;
                brdv.success = SJBhttp_SmsOperatorHelp.SendMessage_Vaild(MobileCode, out vaildcode);
                brdv.ResultDataValue = "{\"vaildcode\":\"" + vaildcode + "\",\"TimeOut\":" + ZhiFang.WeiXin.Common.ConfigHelper.GetConfigInt("VaildCodeTimeOut") + "}";
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return brdv;

        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="MobileCode">手机号</param>
        /// <returns>是否成功和时间</returns>
        public BaseResultDataValue VaildMobileCode(string MobileCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string vaildcode;
                if (!IBBWeiXinAccount.CheckWeiXinAccountByMobileCode(MobileCode))
                {
                    return SJBhttp_SmsOperator_Vaild(MobileCode);
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + "手机号为：'" + MobileCode + "'已被注册！";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return brdv;

        }

        #endregion

        #region jsapi
        /// <summary>
        /// 获取jsapi签名
        /// </summary>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="url">URL地址</param>
        /// <returns></returns>
        public BaseResultDataValue GetJSAPISignature(string noncestr, string timestamp, string url)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("noncestr=" + noncestr + ";timestamp=" + timestamp + ";url=" + url);
                if (!(noncestr != null && noncestr.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：noncestr格式不正确！";
                    return brdv;
                }
                if (!(timestamp != null && timestamp.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：timestamp格式不正确！";
                    return brdv;
                }
                if (!(url != null && url.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：url格式不正确！";
                    return brdv;
                }
                int expires_in;
                ZhiFang.Common.Log.Log.Debug("HttpContext.Current.Application:" + HttpContext.Current.Application.AllKeys.Length);
                string signature = BasePage.GetSignature(HttpContext.Current.Application, noncestr, timestamp, url, out expires_in);
                brdv.ResultDataValue = "{\"signature\":\"" + signature + "\",\"TimeOut\":" + expires_in + "}";
                brdv.success = true;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return brdv;

        }
        #endregion

        #region BAccountHospitalSearchContext

        public BaseResultDataValue ST_UDTO_AddBAccountHospitalSearchContext(string HospitalCode, string HospitalSearchID, string key, string value, string FieldsName, string Comment, long AccountID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BAccountHospitalSearchContext bahsce = new BAccountHospitalSearchContext();
            //bahsce.HospitalCode = HospitalCode;
            //bahsce.HospitalSearchID = long.Parse(HospitalSearchID);
            bahsce.SearchContext = key + "=" + value;
            bahsce.Comment = Comment;
            bahsce.FieldsCode = key;
            bahsce.FieldsValue = value;
            bahsce.AccountID = AccountID;
            bahsce.FieldsName = FieldsName;
            bahsce.WeiXinAccount = ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID);
            IBBAccountHospitalSearchContext.Entity = bahsce;
            try
            {
                if (IBBAccountHospitalSearchContext.AddSearchContextByHSearchID())
                {
                    IBBWeiXinAccount.Get(IBBWeiXinAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBAccountHospitalSearchContext.Entity);
                    tempBaseResultDataValue.success = true;
                    tempBaseResultDataValue.ErrorInfo = "新增成功！";
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "新增错误！";
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

        public BaseResultDataValue ST_UDTO_GetBAccountHospitalSearchContextList(long SearchAccountId, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            List<BAccountHospitalSearchContext> tempEntityList = new List<BAccountHospitalSearchContext>();
            try
            {
                tempEntityList = IBBAccountHospitalSearchContext.SearchListBySearchAccountId(SearchAccountId);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
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

        public BaseResultDataValue ST_UDTO_GetBSearchAccountRFList(string SearchAccountId, string Name, int Page, int limit)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                List<BSearchAccountReportForm> tempList = new List<BSearchAccountReportForm>();
                List<AppRFObject> LARFO = new List<AppRFObject>();
                if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") != null && ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") == "1")
                {
                    Name = ZhiFang.WeiXin.Common.SecurityHelp.MD5Encrypt(Name);
                }
                tempList = IBBAccountHospitalSearchContext.SearchRFListBySearchAccountId(long.Parse(SearchAccountId), ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(DicCookieSession.WeiXinOpenID), Name, Page, limit);
                foreach (var bsarf in tempList)
                {
                    AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
                    if (bsarf.ReportFormTime.HasValue)
                    {
                        info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    if (bsarf.COLLECTDATE.HasValue)
                    {
                        info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                    }
                    if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
                    {
                        info.PatNumber = bsarf.PatNo;
                    }
                    AppRFObject ARFO = new AppRFObject() { info = info, list = new List<AppRIResult>() };
                    //if (bsarf.ItemList != null)
                    //{
                    //    foreach (var item in bsarf.ItemList)
                    //    {
                    //        ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MicroList != null)
                    //{
                    //    foreach (var item in bsarf.MicroList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MarrowList != null)
                    //{
                    //    foreach (var item in bsarf.MarrowList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    LARFO.Add(ARFO);
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetBSearchAccountRFList:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_GetSearchAccountReportFormListById(string ReportFormIndexIdList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            List<BSearchAccountReportForm> tempList = new List<BSearchAccountReportForm>();

            List<AppRFObject> LARFO = new List<AppRFObject>();
            tempList = IBBAccountHospitalSearchContext.SearchAccountReportFormByReportFormIndexIdList(ReportFormIndexIdList);
            try
            {
                foreach (var bsarf in tempList)
                {
                    LARFO.Add(AppRFObject.SetValue(bsarf));
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetSearchAccountReportFormListById:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_GetSearchAccountReportFormById(string ReportFormIndexId, string SearchAccountId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BSearchAccountReportForm entity = new BSearchAccountReportForm();
            try
            {
                List<AppRFObject> LARFO = new List<AppRFObject>();
                entity = IBBAccountHospitalSearchContext.UpdateSearchAccountReportFormByReportFormIndexId(ReportFormIndexId, long.Parse(SearchAccountId));

                if (entity != null)
                {
                    LARFO.Add(AppRFObject.SetValue(entity));
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetSearchAccountReportFormById:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_GetBSearchAccountRFListByPatNoAndName(string PatNo, string Name, int Page, int limit)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            List<BSearchAccountReportForm> tempList = new List<BSearchAccountReportForm>();
            List<AppRFObject> LARFO = new List<AppRFObject>();
            try
            {

                if (PatNo == null || PatNo.Trim() == "")
                {
                    tempBaseResultDataValue.ErrorInfo = "参数错误:PatNo为空!";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }
                if (Name == null || Name.Trim() == "")
                {
                    tempBaseResultDataValue.ErrorInfo = "参数错误:Name为空!";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetBSearchAccountRFListByPatNoAndName.PatNo:" + PatNo + "@Name:"+Name);
                string hqlwhere = "";
                if (ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") != null && ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("RFNameEncryptionflag") == "1")
                {
                    Name = ZhiFang.WeiXin.Common.SecurityHelp.MD5Encrypt(Name,SecurityHelp.PWDMD5Key);
                }
                hqlwhere = " PatNo='" + PatNo.ToString() + "' and Name='" + Name + "'";
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetBSearchAccountRFListByPatNoAndName.hqlwhere:" + hqlwhere);
                tempList = IBBSearchAccountReportForm.SearchListByHQL(hqlwhere, Page, limit).list.ToList();
                foreach (var bsarf in tempList)
                {
                    AppRFUserInfo info = new AppRFUserInfo() { CheckListNumber = bsarf.VisNo, HospitalName = bsarf.HospitalName, PatientName = bsarf.Name, ReportId = bsarf.ReportFormIndexID.ToString(), ReportType = bsarf.ReportFormType };
                    if (bsarf.ReportFormTime.HasValue)
                    {
                        info.ReportTime = bsarf.ReportFormTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    if (bsarf.COLLECTDATE.HasValue)
                    {
                        info.VisitTime = bsarf.COLLECTDATE.Value.ToString("yyyy-MM-dd");
                    }
                    if (bsarf.PatNo != null && bsarf.PatNo.Trim() != "")
                    {
                        info.PatNumber = bsarf.PatNo;
                    }
                    AppRFObject ARFO = new AppRFObject() { info = info, list = new List<AppRIResult>() };
                    //if (bsarf.ItemList != null)
                    //{
                    //    foreach (var item in bsarf.ItemList)
                    //    {
                    //        ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MicroList != null)
                    //{
                    //    foreach (var item in bsarf.MicroList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    //if (bsarf.MarrowList != null)
                    //{
                    //    foreach (var item in bsarf.MarrowList)
                    //    {
                    //        //ARFO.list.Add(new AppRIResult() { ItemId = item.ITEMNO.ToString(), ItemsName = item.TESTITEMNAME, ReferenceValue = item.REFRANGE, Result = item.REPORTVALUE, Unit = item.UNIT });
                    //    }
                    //}
                    LARFO.Add(ARFO);
                }
                tempBaseResultDataValue.success = true;
                tempBaseResultDataValue.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(LARFO);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_GetBSearchAccountRFListByPatNoAndName:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return tempBaseResultDataValue;
        }

        
        #endregion
    }
}
