using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WeiXin.BusinessObject;
using System.ServiceModel.Channels;
using ZhiFang.Entity.Base;
using System.Web;
using ZhiFang.WeiXin.BLL;
using ZhiFang.WeiXin.Entity;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.Entity.Statistics;
using ZhiFang.Common.Log;
using System.Data;

namespace ZhiFang.WeiXin.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZhiFangWeiXinService : ZhiFang.WeiXin.ServerContract.IZhiFangWeiXinService
    {
        IBLL.IBBParameter IBBParameter { get; set; }
        IBLL.IBBDoctorAccount IBBDoctorAccount { get; set; }
        IBLL.IBBDict IBBDict { get; set; }
        IBLL.IBBDictType IBBDictType { get; set; }
        IBLL.IBOSManagerRefundFormAttachment IBOSManagerRefundFormAttachment { get; set; }

        IBLL.IBOSDoctorBonus IBOSDoctorBonus { get; set; }

        IBLL.IBOSDoctorBonusAttachment IBOSDoctorBonusAttachment { get; set; }

        IBLL.IBOSDoctorBonusOperation IBOSDoctorBonusOperation { get; set; }

        IBLL.IBOSDoctorBonusForm IBOSDoctorBonusForm { get; set; }

        IBLL.IBOSDoctorOrderForm IBOSDoctorOrderForm { get; set; }

        IBLL.IBOSDoctorOrderItem IBOSDoctorOrderItem { get; set; }

        IBLL.IBOSItemProductClassTree IBOSItemProductClassTree { get; set; }

        IBLL.IBOSItemProductClassTreeLink IBOSItemProductClassTreeLink { get; set; }

        IBLL.IBOSManagerRefundForm IBOSManagerRefundForm { get; set; }

        IBLL.IBOSManagerRefundFormOperation IBOSManagerRefundFormOperation { get; set; }

        IBLL.IBOSRecommendationItemProduct IBOSRecommendationItemProduct { get; set; }

        IBLL.IBOSShoppingCart IBOSShoppingCart { get; set; }

        IBLL.IBOSUserConsumerForm IBOSUserConsumerForm { get; set; }

        IBLL.IBOSUserConsumerItem IBOSUserConsumerItem { get; set; }

        IBLL.IBOSUserOrderForm IBOSUserOrderForm { get; set; }
        IBLL.IBOSUserOrderItem IBOSUserOrderItem { get; set; }
        IBLL.IBFinanceIncome IBFinanceIncome { get; set; }
        IBLL.IBBLabTestItem IBBLabTestItem { get; set; }
        IBLL.IBUserConsumerItemDetails IBUserConsumerItemDetails { get; set; }
        IBLL.IBBRuleNumber IBBRuleNumber { get; set; }
        IBLL.IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IBLL.IBItemColorAndSampleTypeDetail IBItemColorAndSampleTypeDetail { get; set; }
        IBLL.IBItemColorDict IBItemColorDict { get; set; }
        IBLL.IBCLIENTELE IBCLIENTELE { get; set; }
        IBLL.IBBusinessLogicClientControl IBBusinessLogicClientControl { get; set; }
        IBLL.IBBLabGroupItem IBBLabGroupItem { get; set; }
        IBLL.IBBTestItemControl IBBTestItemControl { get; set; }
        IBLL.IBBarCodeForm IBBarCodeForm { get; set; }
        IBLL.IBPatDiagInfo IBPatDiagInfo { get; set; }
        IBLL.IBNRequestItem IBNRequestItem { get; set; }
        IBLL.IBNRequestForm IBNRequestForm { get; set; }
        IBLL.IBGenderType IBGenderType { get; set; }
        IBLL.IBItemAllItem IBItemAllItem { get; set; }
        IBLL.IBBItemCon IBBItemCon { get; set; }
        IBLL.IBSampleType IBSampleType { get; set; }
        IBLL.IBBLabDistrict IBBLabDistrict { get; set; }
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

        #region BParameter
        //Add  BParameter
        public BaseResultDataValue ST_UDTO_AddBParameter(BParameter entity)
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
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
        #endregion

        #region 医生相片上传及下载处理
        /// <summary>
        /// 医生相片上传
        /// </summary>
        /// <returns></returns>
        public Message ST_UDTO_UploadBDoctorAccountImageByAccountID()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            brdv.ResultDataValue = "{id:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                string fileName = "";
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到上传图片信息！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }
                if (brdv.success && (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength <= 0))
                {
                    brdv.ErrorInfo = "上传的图片为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                //医生Id
                string accountID = HttpContext.Current.Request.Form["Id"];
                //图片类型(职业证书:ProfessionalAbility;个人照片:PersonImage)
                string imageType = HttpContext.Current.Request.Form["ImageType"];
                if (brdv.success && String.IsNullOrEmpty(accountID))
                {
                    brdv.ErrorInfo = "医生ID值为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(imageType))
                {
                    brdv.ErrorInfo = "图片类型值为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && !String.IsNullOrEmpty(file.FileName))
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if (brdv.success && fileExt.ToLower() != ".png")
                    {
                        brdv.ErrorInfo = "上传的图片格式需是png！";
                        brdv.ResultDataValue = "{id:''}";
                        brdv.success = false;
                    }
                    fileName = accountID + fileExt;// ".png";
                }
                string parentPath = parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UpLoadPicturePath.Key.ToString());
                if (brdv.success && string.IsNullOrEmpty(parentPath))
                {
                    brdv.ErrorInfo = "上传图片保存路径为空,请设置系统参数UpLoadPicturePath！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && file != null && !string.IsNullOrEmpty(fileName))
                {
                    //上传图片保存路径
                    string savePath = "Images\\" + imageType;
                    parentPath = parentPath + savePath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);

                    string filepath = Path.Combine(parentPath, fileName);
                    file.SaveAs(filepath);
                    string urlKey = "";
                    //ZhiFang.Common.Log.Log.Debug("filepath:" + filepath);
                    switch (imageType)
                    {
                        case "ProfessionalAbility":
                            urlKey = "ProfessionalAbilityImageUrl";
                            break;
                        case "PersonImage":
                            urlKey = "PersonImageUrl";
                            break;
                        default:
                            break;
                    }
                    if (!String.IsNullOrEmpty(urlKey))
                    {
                        savePath = savePath + "\\" + fileName;
                        string[] strParams = { "Id=" + accountID, urlKey + "='" + savePath + "'" };
                        IBBDoctorAccount.Update(strParams);
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("上传图片错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "{id:''}";
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载医生图片
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream ST_UDTO_DownLoadBDoctorAccountImageByAccountID(long accountID, long operateType, string imageType)
        {
            FileStream fileStream = null;
            if (String.IsNullOrEmpty(imageType))
            {
                string errorInfo = "图片类型值为空";
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(accountID, errorInfo);
                return memoryStream;
            }
            try
            {
                //上传图片保存路径
                string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UpLoadPicturePath.Key.ToString());

                BDoctorAccount enity = IBBDoctorAccount.Get(accountID);
                string filePath = "";
                switch (imageType)
                {
                    case "ProfessionalAbility":
                        filePath = enity.ProfessionalAbilityImageUrl;
                        break;
                    case "PersonImage":
                        filePath = enity.PersonImageUrl;
                        break;
                    default:
                        break;
                }
                //ZhiFang.Common.Log.Log.Error("filePath:" + filePath);
                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath = parentPath + filePath;
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = accountID + ".png";
                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("图片下载错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }

        #endregion

        #region OSDoctorBonus
        //Add  OSDoctorBonus
        public BaseResultDataValue ST_UDTO_AddOSDoctorBonus(OSDoctorBonus entity)
        {
            IBOSDoctorBonus.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorBonus.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorBonus.Get(IBOSDoctorBonus.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorBonus.Entity);
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
        //Update  OSDoctorBonus
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonus(OSDoctorBonus entity)
        {
            IBOSDoctorBonus.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonus.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorBonus
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusByField(OSDoctorBonus entity, string fields)
        {
            IBOSDoctorBonus.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorBonus.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorBonus.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorBonus.Edit();
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
        //Delele  OSDoctorBonus
        public BaseResultBool ST_UDTO_DelOSDoctorBonus(long longOSDoctorBonusID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonus.Remove(longOSDoctorBonusID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonus(OSDoctorBonus entity)
        {
            IBOSDoctorBonus.Entity = entity;
            EntityList<OSDoctorBonus> tempEntityList = new EntityList<OSDoctorBonus>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorBonus.Search();
                tempEntityList.count = IBOSDoctorBonus.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonus>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorBonus> tempEntityList = new EntityList<OSDoctorBonus>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorBonus.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorBonus.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonus>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorBonus.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorBonus>(tempEntity);
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
        /// 咨询费打款明细报表Excel导出
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelOSDoctorBonusDetail(long operateType, string where)
        {
            FileStream fileStream = null;
            string filename = "咨询费打款明细报表.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(where))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBOSDoctorBonus.GetExportExcelOSDoctorBonusDetail(where, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出咨询费打款明细报表数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出咨询费打款明细报表错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }

        #endregion

        #region 医生结算单附件表上传及下载处理
        /// <summary>
        /// 医生结算单附件表附件上传服务
        /// </summary>
        /// <returns></returns>
        public Message ST_UDTO_UploadOSDoctorBonusAttachment()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = nullValue;
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //需要保存的数据对象名称
                string objectName = "", fkObjectId = "", fkObjectName = "", fileName = "", newFileName = "", contentType = "";
                objectName = HttpContext.Current.Request.Form["ObjectEName"];
                //需要保存的数据对象的子对象Id值
                fkObjectId = HttpContext.Current.Request.Form["FKObjectId"];
                //需要保存的数据对象的子对象名称
                fkObjectName = HttpContext.Current.Request.Form["FKObjectName"];
                string fileSize = HttpContext.Current.Request.Form["FileSize"];
                fileSize = HttpContext.Current.Request.Form["FileSize"];
                //文件名称
                fileName = HttpContext.Current.Request.Form["FileName"];
                newFileName = HttpContext.Current.Request.Form["NewFileName"];
                //公共附件分类保存的文件夹名称
                string saveCategory = HttpContext.Current.Request.Form["SaveCategory"];
                //业务模块代码
                string businessModuleCode = HttpContext.Current.Request.Form["BusinessModuleCode"];
                int len = 0;
                if (!string.IsNullOrEmpty(fileSize))
                {
                    len = Int32.Parse(fileSize);
                }
                if (file != null)
                {
                    //file.FileName处理
                    //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                    len = file.ContentLength;
                    contentType = file.ContentType;
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    if (string.IsNullOrEmpty(fileName))
                        fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    //ZhiFang.Common.Log.Log.Debug("FileName:" + fileName);
                }

                if (String.IsNullOrEmpty(objectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectId))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象值为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && !string.IsNullOrEmpty(fkObjectId) && len > 0 && !string.IsNullOrEmpty(fileName))
                {
                    //上传附件路径
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UploadFilesPath.Key.ToString());
                    string tempPath = "\\" + objectName + "\\";
                    if (!String.IsNullOrEmpty(saveCategory))
                    {
                        tempPath = tempPath + saveCategory + "\\";
                    }
                    tempPath = tempPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));

                    OSDoctorBonusAttachment entity = new OSDoctorBonusAttachment();
                    entity.BusinessModuleCode = businessModuleCode;
                    entity.FileName = fileName;
                    entity.FileSize = len;// / 1024;
                    entity.FilePath = tempPath;
                    entity.IsUse = true;
                    entity.NewFileName = newFileName;
                    entity.FileExt = fileExt;
                    entity.FileType = contentType;
                    brdv = IBOSDoctorBonusAttachment.AddSCAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity);
                    if (brdv.success)
                        brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("医生结算单附件上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载医生结算单附件表附件文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream ST_UDTO_DownLoadOSDoctorBonusAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                OSDoctorBonusAttachment file = null;
                file = IBOSDoctorBonusAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "附件不存在!请重新上传或联系管理员。";
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                        return memoryStream;
                    }
                    else
                    {
                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        string filename = file.NewFileName + file.FileExt;
                        if (string.IsNullOrEmpty(filename))
                        {
                            filename = file.FileName;
                        }
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0)
                        {
                            //下载文件
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)
                        {
                            //直接打开文件
                            WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                //ZhiFang.Common.Log.Log.Debug("IE或其他浏览器直接打开文件");
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "附件不存在!请重新上传或联系管理员。";
                ZhiFang.Common.Log.Log.Error("附件下载错误:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }
        }

        #endregion

        #region OSDoctorBonusAttachment
        //Add  OSDoctorBonusAttachment
        public BaseResultDataValue ST_UDTO_AddOSDoctorBonusAttachment(OSDoctorBonusAttachment entity)
        {
            IBOSDoctorBonusAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorBonusAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorBonusAttachment.Get(IBOSDoctorBonusAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorBonusAttachment.Entity);
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
        //Update  OSDoctorBonusAttachment
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusAttachment(OSDoctorBonusAttachment entity)
        {
            IBOSDoctorBonusAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorBonusAttachment
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusAttachmentByField(OSDoctorBonusAttachment entity, string fields)
        {
            IBOSDoctorBonusAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorBonusAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorBonusAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorBonusAttachment.Edit();
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
        //Delele  OSDoctorBonusAttachment
        public BaseResultBool ST_UDTO_DelOSDoctorBonusAttachment(long longOSDoctorBonusAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusAttachment.Remove(longOSDoctorBonusAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachment(OSDoctorBonusAttachment entity)
        {
            IBOSDoctorBonusAttachment.Entity = entity;
            EntityList<OSDoctorBonusAttachment> tempEntityList = new EntityList<OSDoctorBonusAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorBonusAttachment.Search();
                tempEntityList.count = IBOSDoctorBonusAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorBonusAttachment> tempEntityList = new EntityList<OSDoctorBonusAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorBonusAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorBonusAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorBonusAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorBonusAttachment>(tempEntity);
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

        #region OSDoctorBonusOperation
        //Add  OSDoctorBonusOperation
        public BaseResultDataValue ST_UDTO_AddOSDoctorBonusOperation(OSDoctorBonusOperation entity)
        {
            IBOSDoctorBonusOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorBonusOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorBonusOperation.Get(IBOSDoctorBonusOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorBonusOperation.Entity);
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
        //Update  OSDoctorBonusOperation
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusOperation(OSDoctorBonusOperation entity)
        {
            IBOSDoctorBonusOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorBonusOperation
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusOperationByField(OSDoctorBonusOperation entity, string fields)
        {
            IBOSDoctorBonusOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorBonusOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorBonusOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorBonusOperation.Edit();
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
        //Delele  OSDoctorBonusOperation
        public BaseResultBool ST_UDTO_DelOSDoctorBonusOperation(long longOSDoctorBonusOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusOperation.Remove(longOSDoctorBonusOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperation(OSDoctorBonusOperation entity)
        {
            IBOSDoctorBonusOperation.Entity = entity;
            EntityList<OSDoctorBonusOperation> tempEntityList = new EntityList<OSDoctorBonusOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorBonusOperation.Search();
                tempEntityList.count = IBOSDoctorBonusOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorBonusOperation> tempEntityList = new EntityList<OSDoctorBonusOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorBonusOperation.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorBonusOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorBonusOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorBonusOperation>(tempEntity);
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

        #region 医生奖金结算单PDF预览及Excel导出
        /// <summary>
        /// 预览医生奖金结算单PDF
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateType"></param>
        /// <param name="isPreview"></param>
        /// <param name="templetName"></param>
        /// <returns></returns>
        public Stream ST_UDTO_OSDoctorBonusFormPreviewPdf(long id, int operateType, bool isPreview, string templetName)
        {
            FileStream tempFileStream = null;
            string fileName = "医生奖金结算单.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isPreview == true && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSDoctorBonusFormPreviewPdf：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBOSDoctorBonusForm.ExcelToPdfFile(id, isPreview, templetName, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "ST_UDTO_OSDoctorBonusFormPreviewPdf：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSDoctorBonusFormPreviewPdf：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        /// <summary>
        /// 医生奖金结算单Excel导出
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelOSDoctorBonusFormDetail(long operateType, string where)
        {
            FileStream fileStream = null;
            string filename = "医生奖金结算清单.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(where))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }
                //else if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                //{
                //    DateTime dtStart = DateTime.Parse(startDate);
                //    DateTime dtEnd = DateTime.Parse(endDate);
                //    if (dtStart.CompareTo(dtEnd) > 0)
                //    {
                //        isExec = false;
                //        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "开始日期大于结束日期!");
                //        return memoryStream;
                //    }
                //}
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBOSDoctorBonusForm.GetExportExcelOSDoctorBonusFormDetail(where, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出医生奖金结算清单数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出医生奖金结算清单错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }

        #endregion

        #region OSDoctorBonusForm
        //Add  OSDoctorBonusForm
        public BaseResultDataValue ST_UDTO_AddOSDoctorBonusForm(OSDoctorBonusForm entity)
        {
            IBOSDoctorBonusForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorBonusForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorBonusForm.Get(IBOSDoctorBonusForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorBonusForm.Entity);
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
        public BaseResultDataValue ST_UDTO_AddOSDoctorBonusFormAndDetails(OSDoctorBonusApply entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null || entity.OSDoctorBonusForm == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：entity参数(OSDoctorBonusForm)不能为空！";
                return tempBaseResultDataValue;
            }
            if (tempBaseResultDataValue.success && entity.IsSettlement == true)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：entity的IsSettlement=true,不能保存！";
                return tempBaseResultDataValue;
            }
            if (tempBaseResultDataValue.success && String.IsNullOrEmpty(entity.BonusFormRound))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：entity的结算周期(BonusFormRound)参数不能为空！";
                return tempBaseResultDataValue;
            }
            if (tempBaseResultDataValue.success && entity.OSDoctorBonusList == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：entity的医生奖金记录不能为空！";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = -1;
                string empIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                if (!String.IsNullOrEmpty(empIdStr))
                    empID = long.Parse(empIdStr);
                string empName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBOSDoctorBonusForm.AddOSDoctorBonusFormAndDetails(entity, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorBonusForm.Get(IBOSDoctorBonusForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorBonusForm.Entity);
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

        //Update  OSDoctorBonusForm
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusForm(OSDoctorBonusForm entity)
        {
            IBOSDoctorBonusForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorBonusForm
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusFormByField(OSDoctorBonusForm entity, string fields)
        {
            IBOSDoctorBonusForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorBonusForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorBonusForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorBonusForm.Edit();
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
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusFormAndDetails(OSDoctorBonusApply entity, string fields)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null || entity.OSDoctorBonusForm == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：entity参数不能为空！";
            }
            else
            {
                IBOSDoctorBonusForm.Entity = entity.OSDoctorBonusForm;
            }
            if (tempBaseResultBool.success && String.IsNullOrEmpty(fields))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorBonusForm.Entity, fields);
                    if (tempArray != null)
                    {
                        long empID = -1;
                        string empIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                        if (!String.IsNullOrEmpty(empIdStr))
                            empID = long.Parse(empIdStr);
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBOSDoctorBonusForm.UpdateOSDoctorBonusFormAndDetails(entity, tempArray, empID, EmpName);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error("医生奖金结算单ID：" + entity.OSDoctorBonusForm.Id + "的状态为：" + OSDoctorBonusFormStatus.GetStatusDic()[entity.OSDoctorBonusForm.Status.ToString()].Name + "出错:" + ex.StackTrace);
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 批量选择医生奖金记录的检查并打款操作处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateOSDoctorBonusListPayStatus(OSDoctorBonusApply entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null || entity.OSDoctorBonusForm == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：entity参数不能为空！";
                return tempBaseResultBool;
            }
            if (entity.OSDoctorBonus == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：entity.OSDoctorBonus参数不能为空！";
                return tempBaseResultBool;
            }
            IBOSDoctorBonusForm.Entity = entity.OSDoctorBonusForm;
            try
            {
                long empID = -1;
                string empIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                if (!String.IsNullOrEmpty(empIdStr))
                    empID = long.Parse(empIdStr);
                string empName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);

                IBOSDoctorBonusForm.PayToUserFunc = (SysWeiXinPayToUser.PayToUser)BusinessObject.PayToUser.PayToUserWeiXin;
                tempBaseResultBool = IBOSDoctorBonusForm.UpdateOSDoctorBonusListPayStatus(entity, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("医生奖金结算单ID：" + entity.OSDoctorBonusForm.Id + "的状态为：" + OSDoctorBonusFormStatus.GetStatusDic()[entity.OSDoctorBonusForm.Status.ToString()].Name + "出错:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        //Delele  OSDoctorBonusForm
        public BaseResultBool ST_UDTO_DelOSDoctorBonusForm(long longOSDoctorBonusFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusForm.Remove(longOSDoctorBonusFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 物理删除医生奖金结算单并同时删除医生奖金结算记录,操作记录及附件信息
        /// </summary>
        /// <param name="longOSDoctorBonusFormID">医生奖金结算单Id</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_DelOSDoctorBonusFormAndDetails(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBOSDoctorBonusForm.DelOSDoctorBonusFormAndDetails(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 检查奖金记录里是否还有未打款
        /// </summary>
        /// <param name="id">医生奖金结算单Id</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_SearchCheckIsUpdatePayed(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorBonusForm.SearchCheckIsUpdatePayed(id);
                tempBaseResultBool.BoolFlag = tempBaseResultBool.success;
            }
            catch (Exception ex)
            {
                tempBaseResultBool.BoolFlag = false;
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusForm(OSDoctorBonusForm entity)
        {
            IBOSDoctorBonusForm.Entity = entity;
            EntityList<OSDoctorBonusForm> tempEntityList = new EntityList<OSDoctorBonusForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorBonusForm.Search();
                tempEntityList.count = IBOSDoctorBonusForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorBonusForm> tempEntityList = new EntityList<OSDoctorBonusForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorBonusForm.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorBonusForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorBonusForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorBonusFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorBonusForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorBonusForm>(tempEntity);
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
        /// 通过结算周期获取医生奖金结算单及结算记录明细的结算申请数据
        /// 系统当前时间只有大于或等于结算月的最后一天才能进行结算申请
        /// </summary>
        /// <param name="bonusFormRound">结算周期</param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchSettlementApplyInfoByBonusFormRound(string bonusFormRound)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(bonusFormRound))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "结算周期为空!";
                return tempBaseResultDataValue;
            }
            try
            {

                long empID = -1;
                string empIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                if (!String.IsNullOrEmpty(empIdStr))
                    empID = long.Parse(empIdStr);
                string empName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                OSDoctorBonusApply tempEntity = IBOSDoctorBonusForm.SearchSettlementApplyInfoByBonusFormRound(bonusFormRound, empID, empName);

                try
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity);
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

        #region OSDoctorOrderForm
        //Add  OSDoctorOrderForm
        public BaseResultDataValue ST_UDTO_AddOSDoctorOrderForm(OSDoctorOrderForm entity)
        {
            IBOSDoctorOrderForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorOrderForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorOrderForm.Get(IBOSDoctorOrderForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorOrderForm.Entity);
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
        //Update  OSDoctorOrderForm
        public BaseResultBool ST_UDTO_UpdateOSDoctorOrderForm(OSDoctorOrderForm entity)
        {
            IBOSDoctorOrderForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorOrderForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorOrderForm
        public BaseResultBool ST_UDTO_UpdateOSDoctorOrderFormByField(OSDoctorOrderForm entity, string fields)
        {
            IBOSDoctorOrderForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorOrderForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorOrderForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorOrderForm.Edit();
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
        //Delele  OSDoctorOrderForm
        public BaseResultBool ST_UDTO_DelOSDoctorOrderForm(long longOSDoctorOrderFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorOrderForm.Remove(longOSDoctorOrderFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderForm(OSDoctorOrderForm entity)
        {
            IBOSDoctorOrderForm.Entity = entity;
            EntityList<OSDoctorOrderForm> tempEntityList = new EntityList<OSDoctorOrderForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorOrderForm.Search();
                tempEntityList.count = IBOSDoctorOrderForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorOrderForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorOrderForm> tempEntityList = new EntityList<OSDoctorOrderForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorOrderForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorOrderForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorOrderForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorOrderForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorOrderForm>(tempEntity);
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

        #region OSDoctorOrderItem
        //Add  OSDoctorOrderItem
        public BaseResultDataValue ST_UDTO_AddOSDoctorOrderItem(OSDoctorOrderItem entity)
        {
            IBOSDoctorOrderItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSDoctorOrderItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSDoctorOrderItem.Get(IBOSDoctorOrderItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSDoctorOrderItem.Entity);
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
        //Update  OSDoctorOrderItem
        public BaseResultBool ST_UDTO_UpdateOSDoctorOrderItem(OSDoctorOrderItem entity)
        {
            IBOSDoctorOrderItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorOrderItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSDoctorOrderItem
        public BaseResultBool ST_UDTO_UpdateOSDoctorOrderItemByField(OSDoctorOrderItem entity, string fields)
        {
            IBOSDoctorOrderItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSDoctorOrderItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSDoctorOrderItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSDoctorOrderItem.Edit();
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
        //Delele  OSDoctorOrderItem
        public BaseResultBool ST_UDTO_DelOSDoctorOrderItem(long longOSDoctorOrderItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSDoctorOrderItem.Remove(longOSDoctorOrderItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItem(OSDoctorOrderItem entity)
        {
            IBOSDoctorOrderItem.Entity = entity;
            EntityList<OSDoctorOrderItem> tempEntityList = new EntityList<OSDoctorOrderItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSDoctorOrderItem.Search();
                tempEntityList.count = IBOSDoctorOrderItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorOrderItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSDoctorOrderItem> tempEntityList = new EntityList<OSDoctorOrderItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSDoctorOrderItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSDoctorOrderItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSDoctorOrderItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSDoctorOrderItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSDoctorOrderItem>(tempEntity);
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

        #region OSItemProductClassTree
        //Add  OSItemProductClassTree
        public BaseResultDataValue ST_UDTO_AddOSItemProductClassTree(OSItemProductClassTree entity)
        {
            IBOSItemProductClassTree.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSItemProductClassTree.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSItemProductClassTree.Get(IBOSItemProductClassTree.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSItemProductClassTree.Entity);
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
        //Update  OSItemProductClassTree
        public BaseResultBool ST_UDTO_UpdateOSItemProductClassTree(OSItemProductClassTree entity)
        {
            IBOSItemProductClassTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSItemProductClassTree.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSItemProductClassTree
        public BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeByField(OSItemProductClassTree entity, string fields)
        {
            IBOSItemProductClassTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSItemProductClassTree.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSItemProductClassTree.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSItemProductClassTree.Edit();
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
        //Delele  OSItemProductClassTree
        public BaseResultBool ST_UDTO_DelOSItemProductClassTree(long longOSItemProductClassTreeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSItemProductClassTree.Remove(longOSItemProductClassTreeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTree(OSItemProductClassTree entity)
        {
            IBOSItemProductClassTree.Entity = entity;
            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSItemProductClassTree.Search();
                tempEntityList.count = IBOSItemProductClassTree.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSItemProductClassTree>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSItemProductClassTree.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSItemProductClassTree.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSItemProductClassTree>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSItemProductClassTree.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSItemProductClassTree>(tempEntity);
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

        #region OSItemProductClassTreeLink
        //Add  OSItemProductClassTreeLink
        public BaseResultDataValue ST_UDTO_AddOSItemProductClassTreeLink(OSItemProductClassTreeLink entity)
        {
            IBOSItemProductClassTreeLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSItemProductClassTreeLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSItemProductClassTreeLink.Get(IBOSItemProductClassTreeLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSItemProductClassTreeLink.Entity);
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
        //Update  OSItemProductClassTreeLink
        public BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLink(OSItemProductClassTreeLink entity)
        {
            IBOSItemProductClassTreeLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSItemProductClassTreeLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSItemProductClassTreeLink
        public BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLinkByField(OSItemProductClassTreeLink entity, string fields)
        {
            IBOSItemProductClassTreeLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSItemProductClassTreeLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSItemProductClassTreeLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSItemProductClassTreeLink.Edit();
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
        //Delele  OSItemProductClassTreeLink
        public BaseResultBool ST_UDTO_DelOSItemProductClassTreeLink(long longOSItemProductClassTreeLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSItemProductClassTreeLink.Remove(longOSItemProductClassTreeLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLink(OSItemProductClassTreeLink entity)
        {
            IBOSItemProductClassTreeLink.Entity = entity;
            EntityList<OSItemProductClassTreeLink> tempEntityList = new EntityList<OSItemProductClassTreeLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSItemProductClassTreeLink.Search();
                tempEntityList.count = IBOSItemProductClassTreeLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSItemProductClassTreeLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSItemProductClassTreeLink> tempEntityList = new EntityList<OSItemProductClassTreeLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSItemProductClassTreeLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSItemProductClassTreeLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSItemProductClassTreeLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSItemProductClassTreeLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSItemProductClassTreeLink>(tempEntity);
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

        #region OSManagerRefundForm
        #region 注释新增、修改、删除，基本退款单服务
        //Add  OSManagerRefundForm
        //public BaseResultDataValue ST_UDTO_AddOSManagerRefundForm(OSManagerRefundForm entity)
        //{
        //    IBOSManagerRefundForm.Entity = entity;
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempBaseResultDataValue.success = IBOSManagerRefundForm.Add();
        //        if (tempBaseResultDataValue.success)
        //        {
        //            IBOSManagerRefundForm.Get(IBOSManagerRefundForm.Entity.Id);
        //            tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSManagerRefundForm.Entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        //Update  OSManagerRefundForm
        //public BaseResultBool ST_UDTO_UpdateOSManagerRefundForm(OSManagerRefundForm entity)
        //{
        //    IBOSManagerRefundForm.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBOSManagerRefundForm.Edit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        //Update  OSManagerRefundForm
        //public BaseResultBool ST_UDTO_UpdateOSManagerRefundFormByField(OSManagerRefundForm entity, string fields)
        //{
        //    IBOSManagerRefundForm.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        if ((fields != null) && (fields.Length > 0))
        //        {
        //            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSManagerRefundForm.Entity, fields);
        //            if (tempArray != null)
        //            {
        //                tempBaseResultBool.success = IBOSManagerRefundForm.Update(tempArray);
        //            }
        //        }
        //        else
        //        {
        //            tempBaseResultBool.success = false;
        //            tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //            //tempBaseResultBool.success = IBOSManagerRefundForm.Edit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        //Delele  OSManagerRefundForm
        //public BaseResultBool ST_UDTO_DelOSManagerRefundForm(long longOSManagerRefundFormID)
        //{
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBOSManagerRefundForm.Remove(longOSManagerRefundFormID);
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        #endregion

        //public BaseResultBool ST_UDTO_OSManagerRefundFormOneReview(ZhiFang.Entity.Base.BaseEntity entity)
        public BaseResultBool ST_UDTO_OSManagerRefundFormOneReview(RefundFormVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeID) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeID).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,员工身份读取EmployeeID错误！");
                    return tempBaseResultBool;
                }
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeName) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeName).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,读取EmployeeName错误！");
                    return tempBaseResultBool;
                }
                tempBaseResultBool = IBOSManagerRefundForm.OSManagerRefundFormOneReview(entity.RefundFormCode, entity.Reason, entity.Result, Cookie.CookieHelper.Read(DicCookieSession.EmployeeID), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "服务错误！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,服务错误！" + ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultBool ST_UDTO_OSManagerRefundFormTwoReview(RefundFormVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeID) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeID).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,员工身份读取EmployeeID错误！");
                    return tempBaseResultBool;
                }
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeName) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeName).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,读取EmployeeName错误！");
                    return tempBaseResultBool;
                }
                tempBaseResultBool = IBOSManagerRefundForm.OSManagerRefundFormTwoReview(entity.Id, entity.Reason, entity.Result, Cookie.CookieHelper.Read(DicCookieSession.EmployeeID), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "服务错误！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,服务错误！" + ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultBool ST_UDTO_OSManagerRefundFormThreeReview(RefundFormThreeReviewVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeID) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeID).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,员工身份读取EmployeeID错误！");
                    return tempBaseResultBool;
                }
                if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeName) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeName).Trim() == "")
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "员工身份读取错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,读取EmployeeName错误！");
                    return tempBaseResultBool;
                }
                tempBaseResultBool = IBOSManagerRefundForm.OSManagerRefundFormThreeReview(new SysWeiXinPayBack.PayBack(PayBase.RunRefundApplyList), entity, Cookie.CookieHelper.Read(DicCookieSession.EmployeeID), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "服务错误！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormOneReview,服务错误！" + ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundForm(OSManagerRefundForm entity)
        {
            IBOSManagerRefundForm.Entity = entity;
            EntityList<OSManagerRefundForm> tempEntityList = new EntityList<OSManagerRefundForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSManagerRefundForm.Search();
                tempEntityList.count = IBOSManagerRefundForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundForm>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL(int page, int limit, string fields, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSManagerRefundForm> tempEntityList = new EntityList<OSManagerRefundForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    Newtonsoft.Json.Linq.JArray tempJArray = Newtonsoft.Json.Linq.JArray.Parse(sort);
                    List<string> SortList = new List<string>();
                    string sortStr = "";
                    string tempStr = "";
                    foreach (var tempObject in tempJArray)
                    {
                        tempStr = tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper();
                        SortList.Add(tempStr);
                    }
                    if (SortList.Count > 0)
                    {
                        sortStr = string.Join(",", SortList.ToArray());
                    }
                    //ZhiFang.Common.Log.Log.Debug("sortStr:" + sortStr);
                    tempEntityList = IBOSManagerRefundForm.SearchListByHQL(where, sortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBOSManagerRefundForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL.序列化错误异常：" + ex.ToString());
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL.异常："+ex.ToString());
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSManagerRefundForm> tempEntityList = new EntityList<OSManagerRefundForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSManagerRefundForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSManagerRefundForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSManagerRefundForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSManagerRefundForm>(tempEntity);
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

        #region 退款单附件上传及下载处理
        /// <summary>
        /// 退款单附件上传服务
        /// </summary>
        /// <returns></returns>
        public Message ST_UDTO_UploadOSManagerRefundFormAttachment()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = nullValue;
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //需要保存的数据对象名称
                string objectName = "", fkObjectId = "", fkObjectName = "", fileName = "", newFileName = "", contentType = "";
                objectName = HttpContext.Current.Request.Form["ObjectEName"];
                //需要保存的数据对象的子对象Id值
                fkObjectId = HttpContext.Current.Request.Form["FKObjectId"];
                //需要保存的数据对象的子对象名称
                fkObjectName = HttpContext.Current.Request.Form["FKObjectName"];
                string fileSize = HttpContext.Current.Request.Form["FileSize"];
                fileSize = HttpContext.Current.Request.Form["FileSize"];
                //文件名称
                fileName = HttpContext.Current.Request.Form["FileName"];
                newFileName = HttpContext.Current.Request.Form["NewFileName"];
                //公共附件分类保存的文件夹名称
                string saveCategory = HttpContext.Current.Request.Form["SaveCategory"];
                //业务模块代码
                string businessModuleCode = HttpContext.Current.Request.Form["BusinessModuleCode"];
                int len = 0;
                if (!string.IsNullOrEmpty(fileSize))
                {
                    len = Int32.Parse(fileSize);
                }
                if (file != null)
                {
                    //file.FileName处理
                    //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                    len = file.ContentLength;
                    contentType = file.ContentType;
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    if (string.IsNullOrEmpty(fileName))
                        fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    //ZhiFang.Common.Log.Log.Debug("FileName:" + fileName);
                }

                if (String.IsNullOrEmpty(objectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectId))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象值为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && !string.IsNullOrEmpty(fkObjectId) && len > 0 && !string.IsNullOrEmpty(fileName))
                {
                    //上传附件路径
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UploadFilesPath.Key.ToString());
                    string tempPath = "\\" + objectName + "\\";
                    if (!String.IsNullOrEmpty(saveCategory))
                    {
                        tempPath = tempPath + saveCategory + "\\";
                    }
                    tempPath = tempPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));

                    OSManagerRefundFormAttachment entity = new OSManagerRefundFormAttachment();
                    entity.BusinessModuleCode = businessModuleCode;
                    entity.FileName = fileName;
                    entity.FileSize = len;// / 1024;
                    entity.FilePath = tempPath;
                    entity.IsUse = true;
                    entity.NewFileName = newFileName;
                    entity.FileExt = fileExt;
                    entity.FileType = contentType;
                    brdv = IBOSManagerRefundFormAttachment.AddSCAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity);
                    if (brdv.success)
                        brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("退款单附件上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 下载退款单附件文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream ST_UDTO_DownLoadOSManagerRefundFormAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                OSManagerRefundFormAttachment file = null;
                file = IBOSManagerRefundFormAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "附件不存在!请重新上传或联系管理员。";
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                        return memoryStream;
                    }
                    else
                    {
                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        string filename = file.NewFileName + file.FileExt;
                        if (string.IsNullOrEmpty(filename))
                        {
                            filename = file.FileName;
                        }
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0)
                        {
                            //下载文件
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)
                        {
                            //直接打开文件
                            WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                //ZhiFang.Common.Log.Log.Debug("IE或其他浏览器直接打开文件");
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "附件不存在!请重新上传或联系管理员。";
                ZhiFang.Common.Log.Log.Error("附件下载错误:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }
        }

        #endregion

        #region 退款申请PDF预览及Excel导出
        /// <summary>
        /// 预览退款申请单PDF文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateType"></param>
        /// <param name="isPreview"></param>
        /// <param name="templetName"></param>
        /// <returns></returns>
        public Stream ST_UDTO_OSManagerRefundFormPreviewPdf(long id, int operateType, bool isPreview, string templetName)
        {
            FileStream tempFileStream = null;
            string fileName = "退款申请单.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isPreview == true && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormPreviewPdf：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBOSManagerRefundForm.ExcelToPdfFile(id, isPreview, templetName, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "ST_UDTO_OSManagerRefundFormPreviewPdf：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_OSManagerRefundFormPreviewPdf：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        /// <summary>
        /// 退款申请清单Excel导出
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelOSManagerRefundFormDetail(long operateType, string where)
        {
            FileStream fileStream = null;
            string filename = "退款申请清单.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(where))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }
                //else if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                //{
                //    DateTime dtStart = DateTime.Parse(startDate);
                //    DateTime dtEnd = DateTime.Parse(endDate);
                //    if (dtStart.CompareTo(dtEnd) > 0)
                //    {
                //        isExec = false;
                //        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "开始日期大于结束日期!");
                //        return memoryStream;
                //    }
                //}
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBOSManagerRefundForm.GetExportExcelOSManagerRefundFormDetail(where, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出退款申请清单数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出退款申请清单错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }
        #endregion

        #region OSManagerRefundFormAttachment
        //Add  OSManagerRefundFormAttachment
        public BaseResultDataValue ST_UDTO_AddOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity)
        {
            IBOSManagerRefundFormAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSManagerRefundFormAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSManagerRefundFormAttachment.Get(IBOSManagerRefundFormAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSManagerRefundFormAttachment.Entity);
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
        //Update  OSManagerRefundFormAttachment
        public BaseResultBool ST_UDTO_UpdateOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity)
        {
            IBOSManagerRefundFormAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSManagerRefundFormAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSManagerRefundFormAttachment
        public BaseResultBool ST_UDTO_UpdateOSManagerRefundFormAttachmentByField(OSManagerRefundFormAttachment entity, string fields)
        {
            IBOSManagerRefundFormAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSManagerRefundFormAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSManagerRefundFormAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSManagerRefundFormAttachment.Edit();
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
        //Delele  OSManagerRefundFormAttachment
        public BaseResultBool ST_UDTO_DelOSManagerRefundFormAttachment(long longOSManagerRefundFormAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSManagerRefundFormAttachment.Remove(longOSManagerRefundFormAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity)
        {
            IBOSManagerRefundFormAttachment.Entity = entity;
            EntityList<OSManagerRefundFormAttachment> tempEntityList = new EntityList<OSManagerRefundFormAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSManagerRefundFormAttachment.Search();
                tempEntityList.count = IBOSManagerRefundFormAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundFormAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSManagerRefundFormAttachment> tempEntityList = new EntityList<OSManagerRefundFormAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSManagerRefundFormAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSManagerRefundFormAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundFormAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSManagerRefundFormAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSManagerRefundFormAttachment>(tempEntity);
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

        #region OSManagerRefundFormOperation
        //Add  OSManagerRefundFormOperation
        public BaseResultDataValue ST_UDTO_AddOSManagerRefundFormOperation(OSManagerRefundFormOperation entity)
        {
            IBOSManagerRefundFormOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSManagerRefundFormOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSManagerRefundFormOperation.Get(IBOSManagerRefundFormOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSManagerRefundFormOperation.Entity);
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
        //Update  OSManagerRefundFormOperation
        public BaseResultBool ST_UDTO_UpdateOSManagerRefundFormOperation(OSManagerRefundFormOperation entity)
        {
            IBOSManagerRefundFormOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSManagerRefundFormOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSManagerRefundFormOperation
        public BaseResultBool ST_UDTO_UpdateOSManagerRefundFormOperationByField(OSManagerRefundFormOperation entity, string fields)
        {
            IBOSManagerRefundFormOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSManagerRefundFormOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSManagerRefundFormOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSManagerRefundFormOperation.Edit();
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
        //Delele  OSManagerRefundFormOperation
        public BaseResultBool ST_UDTO_DelOSManagerRefundFormOperation(long longOSManagerRefundFormOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSManagerRefundFormOperation.Remove(longOSManagerRefundFormOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperation(OSManagerRefundFormOperation entity)
        {
            IBOSManagerRefundFormOperation.Entity = entity;
            EntityList<OSManagerRefundFormOperation> tempEntityList = new EntityList<OSManagerRefundFormOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSManagerRefundFormOperation.Search();
                tempEntityList.count = IBOSManagerRefundFormOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundFormOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSManagerRefundFormOperation> tempEntityList = new EntityList<OSManagerRefundFormOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSManagerRefundFormOperation.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSManagerRefundFormOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSManagerRefundFormOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSManagerRefundFormOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSManagerRefundFormOperation>(tempEntity);
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

        #region OSRecommendationItemProduct
        //Add  OSRecommendationItemProduct
        public BaseResultDataValue ST_UDTO_AddOSRecommendationItemProduct(OSRecommendationItemProduct entity)
        {
            IBOSRecommendationItemProduct.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSRecommendationItemProduct.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSRecommendationItemProduct.Get(IBOSRecommendationItemProduct.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSRecommendationItemProduct.Entity);
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
        //Update  OSRecommendationItemProduct
        public BaseResultBool ST_UDTO_UpdateOSRecommendationItemProduct(OSRecommendationItemProduct entity)
        {
            IBOSRecommendationItemProduct.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSRecommendationItemProduct.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSRecommendationItemProduct
        public BaseResultBool ST_UDTO_UpdateOSRecommendationItemProductByField(OSRecommendationItemProduct entity, string fields)
        {
            IBOSRecommendationItemProduct.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSRecommendationItemProduct.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSRecommendationItemProduct.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSRecommendationItemProduct.Edit();
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
        //Delele  OSRecommendationItemProduct
        public BaseResultBool ST_UDTO_DelOSRecommendationItemProduct(long longOSRecommendationItemProductID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSRecommendationItemProduct.Remove(longOSRecommendationItemProductID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProduct(OSRecommendationItemProduct entity)
        {
            IBOSRecommendationItemProduct.Entity = entity;
            EntityList<OSRecommendationItemProduct> tempEntityList = new EntityList<OSRecommendationItemProduct>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSRecommendationItemProduct.Search();
                tempEntityList.count = IBOSRecommendationItemProduct.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSRecommendationItemProduct>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSRecommendationItemProduct> tempEntityList = new EntityList<OSRecommendationItemProduct>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSRecommendationItemProduct.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSRecommendationItemProduct.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSRecommendationItemProduct>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductOrEffectiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool effective)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSRecommendationItemProduct> tempEntityList = new EntityList<OSRecommendationItemProduct>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSRecommendationItemProduct.SearchOSRecommendationItemProductOrEffective(where, CommonServiceMethod.GetSortHQL(sort), page, limit, effective);
                }
                else
                {
                    tempEntityList = IBOSRecommendationItemProduct.SearchOSRecommendationItemProductOrEffective(where, page, limit, effective);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSRecommendationItemProduct>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSRecommendationItemProduct.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSRecommendationItemProduct>(tempEntity);
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
        ///特推项目产品图片上传
        /// </summary>
        /// <returns></returns>
        public Message ST_UDTO_UploadRecommendationItemProductImageByID()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            brdv.ResultDataValue = "{id:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                string fileName = "";
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到上传图片信息！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }
                if (brdv.success && (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength <= 0))
                {
                    brdv.ErrorInfo = "上传的图片为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                //特推项目产品Id
                string recommendationitemproductID = HttpContext.Current.Request.Form["Id"];
                //图片类型(RecommendationItemProduct;)
                string imageType = HttpContext.Current.Request.Form["ImageType"];
                if (brdv.success && String.IsNullOrEmpty(recommendationitemproductID))
                {
                    brdv.ErrorInfo = "特推项目产品ID值为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(imageType))
                {
                    brdv.ErrorInfo = "图片类型值为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && !String.IsNullOrEmpty(file.FileName))
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if (brdv.success && fileExt.ToLower() != ".png")
                    {
                        brdv.ErrorInfo = "上传的图片格式需是png！";
                        brdv.ResultDataValue = "{id:''}";
                        brdv.success = false;
                    }
                    fileName = recommendationitemproductID + fileExt;// ".png";
                }
                string parentPath = parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UpLoadPicturePath.Key.ToString());
                if (brdv.success && string.IsNullOrEmpty(parentPath))
                {
                    brdv.ErrorInfo = "上传图片保存路径为空,请设置系统参数UpLoadPicturePath！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                if (brdv.success && file != null && !string.IsNullOrEmpty(fileName))
                {
                    //上传图片保存路径
                    string savePath = "Images\\" + imageType;
                    parentPath = parentPath + savePath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);

                    string filepath = Path.Combine(parentPath, fileName);
                    file.SaveAs(filepath);
                    string urlKey = "";
                    //ZhiFang.Common.Log.Log.Debug("filepath:" + filepath);
                    switch (imageType)
                    {
                        case "RecommendationItemProduct":
                            urlKey = "Image";
                            break;
                        default:
                            break;
                    }
                    if (!String.IsNullOrEmpty(urlKey))
                    {
                        savePath = savePath + "\\" + fileName;
                        string[] strParams = { "Id=" + recommendationitemproductID, urlKey + "='" + savePath + "'" };
                        IBOSRecommendationItemProduct.Update(strParams);
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("上传图片错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "{id:''}";
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载特推项目图片
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream ST_UDTO_DownLoadOSRecommendationItemProductByID(long recommendationitemproductID, long operateType, string imageType)
        {
            FileStream fileStream = null;
            if (String.IsNullOrEmpty(imageType))
            {
                string errorInfo = "图片类型值为空";
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(recommendationitemproductID, errorInfo);
                return memoryStream;
            }
            try
            {
                //上传图片保存路径
                string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UpLoadPicturePath.Key.ToString());

                OSRecommendationItemProduct enity = IBOSRecommendationItemProduct.Get(recommendationitemproductID);
                string filePath = "";
                switch (imageType)
                {
                    case "RecommendationItemProduct":
                        filePath = enity.Image;
                        break;
                    default:
                        break;
                }
                //ZhiFang.Common.Log.Log.Error("filePath:" + filePath);
                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath = parentPath + filePath;
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = recommendationitemproductID + ".png";
                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("图片下载错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }


        #endregion

        #region OSShoppingCart
        //Add  OSShoppingCart
        public BaseResultDataValue ST_UDTO_AddOSShoppingCart(OSShoppingCart entity)
        {
            IBOSShoppingCart.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSShoppingCart.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSShoppingCart.Get(IBOSShoppingCart.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSShoppingCart.Entity);
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
        //Update  OSShoppingCart
        public BaseResultBool ST_UDTO_UpdateOSShoppingCart(OSShoppingCart entity)
        {
            IBOSShoppingCart.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSShoppingCart.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSShoppingCart
        public BaseResultBool ST_UDTO_UpdateOSShoppingCartByField(OSShoppingCart entity, string fields)
        {
            IBOSShoppingCart.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSShoppingCart.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSShoppingCart.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSShoppingCart.Edit();
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
        //Delele  OSShoppingCart
        public BaseResultBool ST_UDTO_DelOSShoppingCart(long longOSShoppingCartID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSShoppingCart.Remove(longOSShoppingCartID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSShoppingCart(OSShoppingCart entity)
        {
            IBOSShoppingCart.Entity = entity;
            EntityList<OSShoppingCart> tempEntityList = new EntityList<OSShoppingCart>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSShoppingCart.Search();
                tempEntityList.count = IBOSShoppingCart.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSShoppingCart>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSShoppingCartByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSShoppingCart> tempEntityList = new EntityList<OSShoppingCart>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSShoppingCart.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSShoppingCart.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSShoppingCart>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSShoppingCartById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSShoppingCart.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSShoppingCart>(tempEntity);
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

        #region OSUserConsumerForm
        //Add  OSUserConsumerForm
        public BaseResultDataValue ST_UDTO_AddOSUserConsumerForm(OSUserConsumerForm entity)
        {
            if (entity != null && String.IsNullOrEmpty(entity.OSUserConsumerFormCode))
            {
                entity.OSUserConsumerFormCode = IBBRuleNumber.GetOSUserConsumerFormCode();
            }
            IBOSUserConsumerForm.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSUserConsumerForm.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSUserConsumerForm.Get(IBOSUserConsumerForm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSUserConsumerForm.Entity);
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
        //Update  OSUserConsumerForm
        public BaseResultBool ST_UDTO_UpdateOSUserConsumerForm(OSUserConsumerForm entity)
        {
            IBOSUserConsumerForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserConsumerForm.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSUserConsumerForm
        public BaseResultBool ST_UDTO_UpdateOSUserConsumerFormByField(OSUserConsumerForm entity, string fields)
        {
            IBOSUserConsumerForm.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSUserConsumerForm.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSUserConsumerForm.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSUserConsumerForm.Edit();
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
        //Delele  OSUserConsumerForm
        public BaseResultBool ST_UDTO_DelOSUserConsumerForm(long longOSUserConsumerFormID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserConsumerForm.Remove(longOSUserConsumerFormID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerForm(OSUserConsumerForm entity)
        {
            IBOSUserConsumerForm.Entity = entity;
            EntityList<OSUserConsumerForm> tempEntityList = new EntityList<OSUserConsumerForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSUserConsumerForm.Search();
                tempEntityList.count = IBOSUserConsumerForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserConsumerForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserConsumerForm> tempEntityList = new EntityList<OSUserConsumerForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSUserConsumerForm.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSUserConsumerForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserConsumerForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSUserConsumerForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSUserConsumerForm>(tempEntity);
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

        #region OSUserConsumerItem
        //Add  OSUserConsumerItem
        public BaseResultDataValue ST_UDTO_AddOSUserConsumerItem(OSUserConsumerItem entity)
        {
            IBOSUserConsumerItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSUserConsumerItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSUserConsumerItem.Get(IBOSUserConsumerItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSUserConsumerItem.Entity);
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
        //Update  OSUserConsumerItem
        public BaseResultBool ST_UDTO_UpdateOSUserConsumerItem(OSUserConsumerItem entity)
        {
            IBOSUserConsumerItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserConsumerItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSUserConsumerItem
        public BaseResultBool ST_UDTO_UpdateOSUserConsumerItemByField(OSUserConsumerItem entity, string fields)
        {
            IBOSUserConsumerItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSUserConsumerItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSUserConsumerItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSUserConsumerItem.Edit();
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
        //Delele  OSUserConsumerItem
        public BaseResultBool ST_UDTO_DelOSUserConsumerItem(long longOSUserConsumerItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserConsumerItem.Remove(longOSUserConsumerItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerItem(OSUserConsumerItem entity)
        {
            IBOSUserConsumerItem.Entity = entity;
            EntityList<OSUserConsumerItem> tempEntityList = new EntityList<OSUserConsumerItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSUserConsumerItem.Search();
                tempEntityList.count = IBOSUserConsumerItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserConsumerItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserConsumerItem> tempEntityList = new EntityList<OSUserConsumerItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSUserConsumerItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSUserConsumerItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserConsumerItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserConsumerItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSUserConsumerItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSUserConsumerItem>(tempEntity);
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

        #region OSUserOrderForm
        public BaseResultBool ST_UDTO_OSUserOrderFormRefundE(long OrderFormID, string MessageStr, string RefundReason, double RefundPrice)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (!CheckEmpInfo())
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "请登录！";
                return tempBaseResultBool;
            }
            try
            {
                tempBaseResultBool = IBOSUserOrderForm.OSUserOrderFormRefundApplyByEmp(OrderFormID, long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName), RefundReason, RefundPrice);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_UpdateOSUserOrderFormStatusOfCancelOrder(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (!CheckEmpInfo())
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "请登录！";
                return tempBaseResultBool;
            }
            try
            {
                tempBaseResultBool = IBOSUserOrderForm.UpdateOSUserOrderFormStatusOfCancelOrder(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        #region 关闭更新删除基础服务
        //Add  OSUserOrderForm
        //public BaseResultDataValue ST_UDTO_AddOSUserOrderForm(OSUserOrderForm entity)
        //{
        //    IBOSUserOrderForm.Entity = entity;
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempBaseResultDataValue.success = IBOSUserOrderForm.Add();
        //        if (tempBaseResultDataValue.success)
        //        {
        //            IBOSUserOrderForm.Get(IBOSUserOrderForm.Entity.Id);
        //            tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSUserOrderForm.Entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        //Update  OSUserOrderForm
        //public BaseResultBool ST_UDTO_UpdateOSUserOrderForm(OSUserOrderForm entity)
        //{
        //    IBOSUserOrderForm.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBOSUserOrderForm.Edit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Update  OSUserOrderForm
        //public BaseResultBool ST_UDTO_UpdateOSUserOrderFormByField(OSUserOrderForm entity, string fields)
        //{
        //    IBOSUserOrderForm.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        if ((fields != null) && (fields.Length > 0))
        //        {
        //            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSUserOrderForm.Entity, fields);
        //            if (tempArray != null)
        //            {
        //                tempBaseResultBool.success = IBOSUserOrderForm.Update(tempArray);
        //            }
        //        }
        //        else
        //        {
        //            tempBaseResultBool.success = false;
        //            tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //            //tempBaseResultBool.success = IBOSUserOrderForm.Edit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Delele  OSUserOrderForm
        //public BaseResultBool ST_UDTO_DelOSUserOrderForm(long longOSUserOrderFormID)
        //{
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBOSUserOrderForm.Remove(longOSUserOrderFormID);
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        #endregion

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderForm(OSUserOrderForm entity)
        {
            IBOSUserOrderForm.Entity = entity;
            EntityList<OSUserOrderForm> tempEntityList = new EntityList<OSUserOrderForm>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSUserOrderForm.Search();
                tempEntityList.count = IBOSUserOrderForm.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserOrderForm>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchOSUserOrderFormNoPlanishByHQL(int page, int limit, string fields, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserOrderForm> tempEntityList = new EntityList<OSUserOrderForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSUserOrderForm.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSUserOrderForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
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

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserOrderForm> tempEntityList = new EntityList<OSUserOrderForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSUserOrderForm.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSUserOrderForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserOrderForm>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSUserOrderForm.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSUserOrderForm>(tempEntity);
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

        #region OSUserOrderItem
        //Add  OSUserOrderItem
        public BaseResultDataValue ST_UDTO_AddOSUserOrderItem(OSUserOrderItem entity)
        {
            IBOSUserOrderItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBOSUserOrderItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBOSUserOrderItem.Get(IBOSUserOrderItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBOSUserOrderItem.Entity);
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
        //Update  OSUserOrderItem
        public BaseResultBool ST_UDTO_UpdateOSUserOrderItem(OSUserOrderItem entity)
        {
            IBOSUserOrderItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserOrderItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  OSUserOrderItem
        public BaseResultBool ST_UDTO_UpdateOSUserOrderItemByField(OSUserOrderItem entity, string fields)
        {
            IBOSUserOrderItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBOSUserOrderItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBOSUserOrderItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBOSUserOrderItem.Edit();
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
        //Delele  OSUserOrderItem
        public BaseResultBool ST_UDTO_DelOSUserOrderItem(long longOSUserOrderItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBOSUserOrderItem.Remove(longOSUserOrderItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderItem(OSUserOrderItem entity)
        {
            IBOSUserOrderItem.Entity = entity;
            EntityList<OSUserOrderItem> tempEntityList = new EntityList<OSUserOrderItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBOSUserOrderItem.Search();
                tempEntityList.count = IBOSUserOrderItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserOrderItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<OSUserOrderItem> tempEntityList = new EntityList<OSUserOrderItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBOSUserOrderItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBOSUserOrderItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<OSUserOrderItem>(tempEntityList);
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
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSUserOrderItemByHQL,序列化错误:" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSUserOrderItemByHQL,查询错误:" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchOSUserOrderItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBOSUserOrderItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<OSUserOrderItem>(tempEntity);
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

        #region 用户订单解锁
        public BaseResultDataValue ST_UDTO_UnLockOSUserOrderFormById(long OSUserOrderFormId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (!CheckEmpInfo())
            {
                brdv.success = false;
                brdv.ErrorInfo = "请登录！";
                return brdv;
            }
            try
            {
                brdv = IBOSUserOrderForm.ST_UDTO_UnLockOSUserOrderFormById(OSUserOrderFormId, long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                Log.Error("ST_UDTO_UnLockOSUserOrderFormById.错误信息：" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region 收入明细报表/项目统计报表及Excel/PDF导出
        public BaseResultDataValue ST_UDTO_SearchFinanceIncomeList(int page, int limit, string fields, string searchEntity, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(searchEntity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询实体参数为空!";
                return tempBaseResultDataValue;
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "收入明细报表查询实体信息序列化出错!";
                return tempBaseResultDataValue;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询实体的消费开始日期或消费结束日期为空!";
                return tempBaseResultDataValue;
            }
            EntityList<FinanceIncome> tempEntityList = new EntityList<FinanceIncome>();
            try
            {
                tempEntityList = IBFinanceIncome.SearchFinanceIncomeList(search, page, limit);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList);
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
        /// 获取财务收入报表Excel转PDF的文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        public Stream ST_UDTO_GetFinanceIncomeExcelToPdfFile(string searchEntity, int operateType)
        {
            FileStream tempFileStream = null;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(searchEntity))
            {
                byte[] bError = Encoding.UTF8.GetBytes("查询实体参数为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                byte[] bError = Encoding.UTF8.GetBytes("财务收入报表查询实体信息序列化出错");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                byte[] bError = Encoding.UTF8.GetBytes("查询实体的消费开始日期或消费结束日期为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            string fileName = "财务收入报表.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_GetFinanceIncomeExcelToPdfFile：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBFinanceIncome.GetFinanceIncomeExcelToPdfFile(search, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "ST_UDTO_GetFinanceIncomeExcelToPdfFile：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_GetFinanceIncomeExcelToPdfFile：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        /// <summary>
        /// 财务收入报表Excel导出
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelFinanceIncome(long operateType, string searchEntity)
        {
            FileStream fileStream = null;
            string filename = "财务收入报表.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(searchEntity))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                isExec = false;
                byte[] bError = Encoding.UTF8.GetBytes("财务收入报表查询实体信息序列化出错");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                isExec = false;
                byte[] bError = Encoding.UTF8.GetBytes("查询实体的消费开始日期或消费结束日期为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBFinanceIncome.GetExportExcelFinanceIncome(search, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出财务收入报表数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出财务收入报表错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }

        public BaseResultDataValue ST_UDTO_SearchUserConsumerItemDetails(int page, int limit, string fields, string searchEntity, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(searchEntity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询实体参数为空!";
                return tempBaseResultDataValue;
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "项目统计报表查询实体信息序列化出错!";
                return tempBaseResultDataValue;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询实体的消费开始日期或消费结束日期为空!";
                return tempBaseResultDataValue;
            }
            EntityList<UserConsumerItemDetails> tempEntityList = new EntityList<UserConsumerItemDetails>();
            try
            {
                tempEntityList = IBUserConsumerItemDetails.SearchUserConsumerItemDetails(search, page, limit);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "项目统计报表序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "项目统计报表查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 获取项目统计报表Excel转PDF的文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        public Stream ST_UDTO_GetUserConsumerItemExcelToPdfFile(string searchEntity, int operateType)
        {
            FileStream tempFileStream = null;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(searchEntity))
            {
                byte[] bError = Encoding.UTF8.GetBytes("查询实体参数为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                byte[] bError = Encoding.UTF8.GetBytes("项目统计报表查询实体信息序列化出错");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                byte[] bError = Encoding.UTF8.GetBytes("查询实体的消费开始日期或消费结束日期为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            string fileName = "项目统计报表.pdf";

            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_GetUserConsumerItemExcelToPdfFile：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBUserConsumerItemDetails.GetUserConsumerItemExcelToPdfFile(search, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "ST_UDTO_GetUserConsumerItemExcelToPdfFile：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_GetUserConsumerItemExcelToPdfFile：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        /// <summary>
        /// 获取项目统计报表Excel导出文件
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelUserConsumerItemDetails(long operateType, string searchEntity)
        {
            FileStream fileStream = null;
            string filename = "项目统计报表.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(searchEntity))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }
            }
            UserConsumerFormSearch search = null;
            try
            {
                search = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<UserConsumerFormSearch>(searchEntity);
            }
            catch (Exception ee)
            {
                isExec = false;
                byte[] bError = Encoding.UTF8.GetBytes("项目统计报表查询实体信息序列化出错");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (!search.ConsumptionStartDate.HasValue || !search.ConsumptionEndDate.HasValue)
            {
                isExec = false;
                byte[] bError = Encoding.UTF8.GetBytes("查询实体的消费开始日期或消费结束日期为空");
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBUserConsumerItemDetails.GetExportExcelUserConsumerItemDetails(search, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出项目统计报表数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出项目统计报表错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }
        #endregion
        private bool CheckEmpInfo()
        {
            if (Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) == null || Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID).Trim() == "")
            {
                return false;
            }
            return true;
        }

        #region BLabTestItem
        //Add  BLabTestItem
        public BaseResultDataValue ST_UDTO_AddBLabTestItem(BLabTestItem entity)
        {
            IBBLabTestItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBLabTestItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBLabTestItem.Get(IBBLabTestItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBLabTestItem.Entity);
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
        public BaseResultDataValue ST_UDTO_AddBLabTestItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBBLabTestItem.AddByBLabTestItemVO(entity);
                if (tempBaseResultDataValue.success)
                {
                    //IBBLabTestItem.Get(IBBLabTestItem.Entity.Id);
                    //tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBLabTestItem.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddBLabTestItemVO.错误信息:"+ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        //Update  BLabTestItem
        public BaseResultBool ST_UDTO_UpdateBLabTestItem(BLabTestItem entity)
        {
            IBBLabTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBLabTestItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BLabTestItem
        public BaseResultBool ST_UDTO_UpdateBLabTestItemByField(BLabTestItem entity, string fields)
        {
            IBBLabTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBLabTestItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBLabTestItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBLabTestItem.Edit();
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

        public BaseResultBool ST_UDTO_UpdateBLabTestItemByFieldVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity, string fields)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool = IBBLabTestItem.UpdateBLabTestItemByFieldVO(tempArray, entity);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBLabTestItem.Edit();
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
        //Delele  BLabTestItem
        public BaseResultBool ST_UDTO_DelBLabTestItem(long longBLabTestItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBLabTestItem.Remove(longBLabTestItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBLabTestItem(BLabTestItem entity)
        {
            IBBLabTestItem.Entity = entity;
            EntityList<BLabTestItem> tempEntityList = new EntityList<BLabTestItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBLabTestItem.Search();
                tempEntityList.count = IBBLabTestItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabTestItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBLabTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BLabTestItem> tempEntityList = new EntityList<BLabTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBLabTestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBLabTestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabTestItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBLabTestItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBLabTestItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BLabTestItem>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID(int page, int limit, string fields, string pitemNo, string areaID, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BLabTestItem> entityList = new EntityList<BLabTestItem>();
            try
            {
                entityList = IBBLabTestItem.SearchBLabGroupItemByPItemNoAndAreaID(pitemNo, areaID, page, limit, sort);
            }
            catch (Exception ee)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID.无法获取组套明细列表!" + ee.Message);
            }
            if (entityList != null)
            {
                try
                {
                    //baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(entityList);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabTestItem>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID.无法获取组套明细列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchOSBLabTestItemByAreaID(int page, int limit, string where, string sort, string areaID, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (areaID == null || areaID.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取区域！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByAreaID.无法获取区域!AreaID为空！");
                return baseResultDataValue;
            }
            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> ELBLTIVO = IBBLabTestItem.SearchAllBLabTestItemByAreaID(areaID, page, limit, sort, where);
            if (ELBLTIVO != null)
            {
                try
                {
                    baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(ELBLTIVO);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByAreaID.无法获取组套列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchOSBLabTestItemByLabCode(int page, int limit, string where, string sort, string LabCode, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (LabCode == null || LabCode.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取送检单位！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByLabCode.无法获取送检单位!LabCode为空！");
                return baseResultDataValue;
            }
            if ((sort != null) && (sort.Length > 0))
            {
                sort =  CommonServiceMethod.GetSortHQL(sort);
            }
            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> ELBLTIVO = IBBLabTestItem.SearchAllBLabTestItemByLabCode(LabCode, page, limit, sort, where);
            if (ELBLTIVO != null)
            {
                try
                {
                    baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(ELBLTIVO);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByLabCode.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchOSBLabTestItemByLabCode.无法获取组套列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID(int page, int limit, string fields, string pitemNo, string areaID, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BLabTestItem> entityList = new EntityList<BLabTestItem>();
            try
            {
                entityList = IBBLabTestItem.SearchBLabGroupItemSubItemByPItemNoAndAreaID(pitemNo, areaID, page, limit, sort);
            }
            catch (Exception ee)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID.无法获取组套明细列表!" + ee.Message);
            }
            if (entityList != null)
            {
                try
                {
                    //baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(entityList);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabTestItem>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID.无法获取组套明细列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode(int page, int limit, string fields, string pitemNo, string LabCode, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<Entity.ViewObject.Response.BLabGroupItemVO> entityList = new EntityList<Entity.ViewObject.Response.BLabGroupItemVO>();
            try
            {
                entityList = IBBLabTestItem.SearchBLabGroupItemSubItemVOByPItemNoAndLabCode(pitemNo, LabCode, page, limit, sort);
            }
            catch (Exception ee)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode.无法获取组套明细列表!" + ee.Message);
            }
            if (entityList != null&& entityList.list!=null && entityList.list.Count>0)
            {
                try
                {
                    //baseResultDataValue.ResultDataValue = Common.JsonSerializer.JsonDotNetSerializer(entityList);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Entity.ViewObject.Response.BLabGroupItemVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "程序错误！";
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode.程序错误:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取组套明细列表！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode.无法获取组套明细列表!");

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_BLabTestItemCopy(string SourceLabCode, List<string> LabCodeList, List<string> ItemNoList, bool Isall, int OverRideType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_BLabTestItemCopy.SourceLabCode:" + SourceLabCode + "@LabCodeList:" + string.Join("','", LabCodeList.ToArray()) + "@ItemNoList:" + string.Join("','", ItemNoList.ToArray()) + "@isall:" + Isall.ToString() + "@OverRideType:" + OverRideType.ToString());
                if (LabCodeList == null || LabCodeList.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "实验室列表为空！";
                    Log.Debug("ST_UDTO_BLabTestItemCopy.LabCodeList为空！");
                    return baseResultDataValue;
                }

                if (ItemNoList == null || ItemNoList.Count <= 0)
                {
                    if (!Isall)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目列表为空！";
                        Log.Debug("ST_UDTO_BLabTestItemCopy.ItemNoList为空！");
                        return baseResultDataValue;
                    }
                }
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_BLabTestItemCopy.LabCodeList:" + string.Join("','", LabCodeList.ToArray()) + "@ItemNoList:" + string.Join("','", ItemNoList.ToArray()) + "@Isall:" + Isall.ToString() + "@OverRideType:" + OverRideType.ToString());
                if (Isall)
                {
                    baseResultDataValue = IBBLabTestItem.TestItemCopyAll(SourceLabCode, LabCodeList, OverRideType);
                }
                else
                {
                    baseResultDataValue = IBBLabTestItem.TestItemCopy(SourceLabCode, LabCodeList, ItemNoList, OverRideType);
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "实验室项目复制异常！";
                ZhiFang.Common.Log.Log.Error("ST_UDTO_BLabTestItemCopy.实验室项目复制异常!" + e.ToString());
            }

            return baseResultDataValue;
        }

        /// <summary>
        /// 对照功能左侧列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="LabCode"></param>
        /// <param name="Type">0全部、1已对照、2未对照</param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchBLabTestItemByLabCodeAndType(string where, string LabCode,string Type, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (LabCode == null || LabCode.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "实验室编号为空!";
                return baseResultDataValue;
            }

            if (Type == null || Type.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询类型为空!";
                return baseResultDataValue;
            }

            try
            {
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                ////if (isPlanish)
                ////{
                //    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<Entity.ViewObject.Response.BLabGroupItemVO>(entityList);
                ////}
                ////else
                ////{
                ////    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList, fields);
                ////}
                baseResultDataValue = IBBLabTestItem.SearchBLabTestItemByLabCodeAndType(Type, LabCode, where, sort);
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "实验室项目查询异常！";
                ZhiFang.Common.Log.Log.Error("SearchBLabTestItemByLabCodeAndType.实验室项目查询异常!" + e.ToString());
            }

            return baseResultDataValue;
        }

        #endregion
       
        #region 微信帐号密码重置
        public BaseResultDataValue WXADS_WeiXinAccountPwdRest(string WeiXinAccount)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (WeiXinAccount == null || WeiXinAccount.Trim()=="")
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：参数错误1！";
                return brdv;
            }
            if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeID) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeID).Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：参数错误2！";
                return brdv;
            }
            if (Cookie.CookieHelper.Read(DicCookieSession.EmployeeName) == null || Cookie.CookieHelper.Read(DicCookieSession.EmployeeName).Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：参数错误3！";
                return brdv;
            }
            long empID = -1;
            string empIdStr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            if (!String.IsNullOrEmpty(empIdStr))
                empID = long.Parse(empIdStr);
            string empName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            try
            {
                
                brdv = IBBWeiXinAccount.WeiXinAccountPwdRest(WeiXinAccount, empIdStr, empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("WXADS_DoctorAccountBindWeiXinAccountChange.错误信息：" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region ItemColorAndSampleTypeDetail
        //Add  ItemColorAndSampleTypeDetail
        public BaseResultDataValue ST_UDTO_AddItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity)
        {
            IBItemColorAndSampleTypeDetail.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBItemColorAndSampleTypeDetail.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBItemColorAndSampleTypeDetail.Get(IBItemColorAndSampleTypeDetail.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBItemColorAndSampleTypeDetail.Entity);
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
        //Update  ItemColorAndSampleTypeDetail
        public BaseResultBool ST_UDTO_UpdateItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity)
        {
            IBItemColorAndSampleTypeDetail.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBItemColorAndSampleTypeDetail.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ItemColorAndSampleTypeDetail
        public BaseResultBool ST_UDTO_UpdateItemColorAndSampleTypeDetailByField(ItemColorAndSampleTypeDetail entity, string fields)
        {
            IBItemColorAndSampleTypeDetail.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBItemColorAndSampleTypeDetail.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBItemColorAndSampleTypeDetail.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBItemColorAndSampleTypeDetail.Edit();
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
        //Delele  ItemColorAndSampleTypeDetail
        public BaseResultBool ST_UDTO_DelItemColorAndSampleTypeDetail(long longItemColorAndSampleTypeDetailID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBItemColorAndSampleTypeDetail.Remove(longItemColorAndSampleTypeDetailID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity)
        {
            IBItemColorAndSampleTypeDetail.Entity = entity;
            EntityList<ItemColorAndSampleTypeDetail> tempEntityList = new EntityList<ItemColorAndSampleTypeDetail>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBItemColorAndSampleTypeDetail.Search();
                tempEntityList.count = IBItemColorAndSampleTypeDetail.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ItemColorAndSampleTypeDetail>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetailByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ItemColorAndSampleTypeDetail> tempEntityList = new EntityList<ItemColorAndSampleTypeDetail>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBItemColorAndSampleTypeDetail.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBItemColorAndSampleTypeDetail.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ItemColorAndSampleTypeDetail>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetailById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBItemColorAndSampleTypeDetail.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ItemColorAndSampleTypeDetail>(tempEntity);
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

        #region ItemColorDict
        //Add  ItemColorDict
        public BaseResultDataValue ST_UDTO_AddItemColorDict(ItemColorDict entity)
        {
            IBItemColorDict.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBItemColorDict.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBItemColorDict.Get(IBItemColorDict.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBItemColorDict.Entity);
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
        //Update  ItemColorDict
        public BaseResultBool ST_UDTO_UpdateItemColorDict(ItemColorDict entity)
        {
            IBItemColorDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBItemColorDict.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ItemColorDict
        public BaseResultBool ST_UDTO_UpdateItemColorDictByField(ItemColorDict entity, string fields)
        {
            IBItemColorDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBItemColorDict.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBItemColorDict.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBItemColorDict.Edit();
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
        //Delele  ItemColorDict
        public BaseResultBool ST_UDTO_DelItemColorDict(long longItemColorDictID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBItemColorDict.Remove(longItemColorDictID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchItemColorDict(ItemColorDict entity)
        {
            IBItemColorDict.Entity = entity;
            EntityList<ItemColorDict> tempEntityList = new EntityList<ItemColorDict>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBItemColorDict.Search();
                tempEntityList.count = IBItemColorDict.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ItemColorDict>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchItemColorDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ItemColorDict> tempEntityList = new EntityList<ItemColorDict>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBItemColorDict.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBItemColorDict.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ItemColorDict>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchItemColorDictById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBItemColorDict.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ItemColorDict>(tempEntity);
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

        #region CLIENTELE
        //Add  CLIENTELE
        public BaseResultDataValue ST_UDTO_AddCLIENTELE(CLIENTELE entity)
        {
            IBCLIENTELE.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCLIENTELE.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCLIENTELE.Get(IBCLIENTELE.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCLIENTELE.Entity);
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
        //Update  CLIENTELE
        public BaseResultBool ST_UDTO_UpdateCLIENTELE(CLIENTELE entity)
        {
            IBCLIENTELE.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCLIENTELE.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CLIENTELE
        public BaseResultBool ST_UDTO_UpdateCLIENTELEByField(CLIENTELE entity, string fields)
        {
            IBCLIENTELE.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCLIENTELE.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCLIENTELE.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCLIENTELE.Edit();
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
        //Delele  CLIENTELE
        public BaseResultBool ST_UDTO_DelCLIENTELE(long longCLIENTELEID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCLIENTELE.Remove(longCLIENTELEID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCLIENTELE(CLIENTELE entity)
        {
            IBCLIENTELE.Entity = entity;
            EntityList<CLIENTELE> tempEntityList = new EntityList<CLIENTELE>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCLIENTELE.Search();
                tempEntityList.count = IBCLIENTELE.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CLIENTELE>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCLIENTELEByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CLIENTELE> tempEntityList = new EntityList<CLIENTELE>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCLIENTELE.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCLIENTELE.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CLIENTELE>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCLIENTELEById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCLIENTELE.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CLIENTELE>(tempEntity);
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

        #region BusinessLogicClientControl
        //Add  BusinessLogicClientControl
        public BaseResultDataValue ST_UDTO_AddBusinessLogicClientControl(BusinessLogicClientControl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.AddTime = DateTime.Now;
                IBBusinessLogicClientControl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBusinessLogicClientControl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  BusinessLogicClientControl
        public BaseResultBool ST_UDTO_UpdateBusinessLogicClientControl(BusinessLogicClientControl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBBusinessLogicClientControl.Entity = entity;
                try
                {
                    baseResultBool.success = IBBusinessLogicClientControl.Edit();
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
        //Update  BusinessLogicClientControl
        public BaseResultBool ST_UDTO_UpdateBusinessLogicClientControlByField(BusinessLogicClientControl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBBusinessLogicClientControl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBusinessLogicClientControl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBusinessLogicClientControl.Update(tempArray);
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
                        //baseResultBool.success = IBBusinessLogicClientControl.Edit();
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
        //Delele  BusinessLogicClientControl
        public BaseResultBool ST_UDTO_DelBusinessLogicClientControl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBusinessLogicClientControl.Entity = IBBusinessLogicClientControl.Get(id);
                if (IBBusinessLogicClientControl.Entity != null)
                {
                    long labid = IBBusinessLogicClientControl.Entity.LabID;
                    string entityName = IBBusinessLogicClientControl.Entity.GetType().Name;
                    baseResultBool.success = IBBusinessLogicClientControl.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControl(BusinessLogicClientControl entity)
        {
            EntityList<BusinessLogicClientControl> entityList = new EntityList<BusinessLogicClientControl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBusinessLogicClientControl.Entity = entity;
                try
                {
                    entityList.list = IBBusinessLogicClientControl.Search();
                    entityList.count = IBBusinessLogicClientControl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BusinessLogicClientControl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BusinessLogicClientControl> entityList = new EntityList<BusinessLogicClientControl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBusinessLogicClientControl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBusinessLogicClientControl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BusinessLogicClientControl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBusinessLogicClientControl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BusinessLogicClientControl>(entity);
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

        #region BLabGroupItem
        //Add  BLabGroupItem
        //public BaseResultDataValue ST_UDTO_AddBLabGroupItem(BLabGroupItem entity)
        //{
        //    IBBLabGroupItem.Entity = entity;
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempBaseResultDataValue.success = IBBLabGroupItem.Add();
        //        if (tempBaseResultDataValue.success)
        //        {
        //            IBBLabGroupItem.Get(IBBLabGroupItem.Entity.Id);
        //            tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBLabGroupItem.Entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        ////Update  BLabGroupItem
        //public BaseResultBool ST_UDTO_UpdateBLabGroupItem(BLabGroupItem entity)
        //{
        //    IBBLabGroupItem.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBBLabGroupItem.Edit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Update  BLabGroupItem
        //public BaseResultBool ST_UDTO_UpdateBLabGroupItemByField(BLabGroupItem entity, string fields)
        //{
        //    IBBLabGroupItem.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        if ((fields != null) && (fields.Length > 0))
        //        {
        //            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBLabGroupItem.Entity, fields);
        //            if (tempArray != null)
        //            {
        //                tempBaseResultBool.success = IBBLabGroupItem.Update(tempArray);
        //            }
        //        }
        //        else
        //        {
        //            tempBaseResultBool.success = false;
        //            tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
        //            //tempBaseResultBool.success = IBBLabGroupItem.Edit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        ////Delele  BLabGroupItem
        //public BaseResultBool ST_UDTO_DelBLabGroupItem(long longBLabGroupItemID)
        //{
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBBLabGroupItem.Remove(longBLabGroupItemID);
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}

        //public BaseResultDataValue ST_UDTO_SearchBLabGroupItem(BLabGroupItem entity)
        //{
        //    IBBLabGroupItem.Entity = entity;
        //    EntityList<BLabGroupItem> tempEntityList = new EntityList<BLabGroupItem>();
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        tempEntityList.list = IBBLabGroupItem.Search();
        //        tempEntityList.count = IBBLabGroupItem.GetTotalCount();
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
        //        try
        //        {
        //            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabGroupItem>(tempEntityList);
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}

        //public BaseResultDataValue ST_UDTO_SearchBLabGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    EntityList<BLabGroupItem> tempEntityList = new EntityList<BLabGroupItem>();
        //    try
        //    {
        //        if ((sort != null) && (sort.Length > 0))
        //        {
        //            tempEntityList = IBBLabGroupItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
        //        }
        //        else
        //        {
        //            tempEntityList = IBBLabGroupItem.SearchListByHQL(where, page, limit);
        //        }
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
        //        try
        //        {
        //            if (isPlanish)
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BLabGroupItem>(tempEntityList);
        //            }
        //            else
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}

        //public BaseResultDataValue ST_UDTO_SearchBLabGroupItemById(long id, string fields, bool isPlanish)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        var tempEntity = IBBLabGroupItem.Get(id);
        //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
        //        try
        //        {
        //            if (isPlanish)
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BLabGroupItem>(tempEntity);
        //            }
        //            else
        //            {
        //                tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tempBaseResultDataValue.success = false;
        //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
        //            //throw new Exception(ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultDataValue;
        //}
        #endregion

        #region BTestItemControl
        //Add  BTestItemControl
        public BaseResultDataValue ST_UDTO_AddBTestItemControl(BTestItemControl entity)
        {
            IBBTestItemControl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTestItemControl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTestItemControl.Get(IBBTestItemControl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTestItemControl.Entity);
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
        //Update  BTestItemControl
        public BaseResultBool ST_UDTO_UpdateBTestItemControl(BTestItemControl entity)
        {
            IBBTestItemControl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTestItemControl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTestItemControl
        public BaseResultBool ST_UDTO_UpdateBTestItemControlByField(BTestItemControl entity, string fields)
        {
            IBBTestItemControl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTestItemControl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTestItemControl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTestItemControl.Edit();
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
        //Delele  BTestItemControl
        public BaseResultBool ST_UDTO_DelBTestItemControl(long longBTestItemControlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTestItemControl.Remove(longBTestItemControlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBTestItemControl(BTestItemControl entity)
        {
            IBBTestItemControl.Entity = entity;
            EntityList<BTestItemControl> tempEntityList = new EntityList<BTestItemControl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTestItemControl.Search();
                tempEntityList.count = IBBTestItemControl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTestItemControl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBTestItemControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTestItemControl> tempEntityList = new EntityList<BTestItemControl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTestItemControl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTestItemControl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTestItemControl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBTestItemControlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTestItemControl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTestItemControl>(tempEntity);
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

        #region BarCodeForm
        //Add  BarCodeForm
        public BaseResultDataValue ST_UDTO_AddBarCodeForm(BarCodeForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBBarCodeForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBarCodeForm.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  BarCodeForm
        public BaseResultBool ST_UDTO_UpdateBarCodeForm(BarCodeForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBBarCodeForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBBarCodeForm.Edit();
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
        //Update  BarCodeForm
        public BaseResultBool ST_UDTO_UpdateBarCodeFormByField(BarCodeForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + "";
                IBBarCodeForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBarCodeForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBarCodeForm.Update(tempArray);
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
                        //baseResultBool.success = IBBarCodeForm.Edit();
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
        //Delele  BarCodeForm
        public BaseResultBool ST_UDTO_DelBarCodeForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBarCodeForm.Entity = IBBarCodeForm.Get(id);
                if (IBBarCodeForm.Entity != null)
                {
                    long labid = IBBarCodeForm.Entity.LabID;
                    string entityName = IBBarCodeForm.Entity.GetType().Name;
                    baseResultBool.success = IBBarCodeForm.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchBarCodeForm(BarCodeForm entity)
        {
            EntityList<BarCodeForm> entityList = new EntityList<BarCodeForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBarCodeForm.Entity = entity;
                try
                {
                    entityList.list = IBBarCodeForm.Search();
                    entityList.count = IBBarCodeForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BarCodeForm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBarCodeFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BarCodeForm> entityList = new EntityList<BarCodeForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBarCodeForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBarCodeForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BarCodeForm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBarCodeFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBarCodeForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BarCodeForm>(entity);
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

        #region PatDiagInfo
        //Add  PatDiagInfo
        public BaseResultDataValue ST_UDTO_AddPatDiagInfo(PatDiagInfo entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBPatDiagInfo.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBPatDiagInfo.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  PatDiagInfo
        public BaseResultBool ST_UDTO_UpdatePatDiagInfo(PatDiagInfo entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBPatDiagInfo.Entity = entity;
                try
                {
                    baseResultBool.success = IBPatDiagInfo.Edit();
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
        //Update  PatDiagInfo
        public BaseResultBool ST_UDTO_UpdatePatDiagInfoByField(PatDiagInfo entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBPatDiagInfo.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPatDiagInfo.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBPatDiagInfo.Update(tempArray);
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
                        //baseResultBool.success = IBPatDiagInfo.Edit();
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
        //Delele  PatDiagInfo
        public BaseResultBool ST_UDTO_DelPatDiagInfo(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBPatDiagInfo.Entity = IBPatDiagInfo.Get(id);
                if (IBPatDiagInfo.Entity != null)
                {
                    long labid = IBPatDiagInfo.Entity.LabID;
                    string entityName = IBPatDiagInfo.Entity.GetType().Name;
                    baseResultBool.success = IBPatDiagInfo.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchPatDiagInfo(PatDiagInfo entity)
        {
            EntityList<PatDiagInfo> entityList = new EntityList<PatDiagInfo>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBPatDiagInfo.Entity = entity;
                try
                {
                    entityList.list = IBPatDiagInfo.Search();
                    entityList.count = IBPatDiagInfo.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<PatDiagInfo>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchPatDiagInfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<PatDiagInfo> entityList = new EntityList<PatDiagInfo>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBPatDiagInfo.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBPatDiagInfo.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<PatDiagInfo>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchPatDiagInfoById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBPatDiagInfo.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<PatDiagInfo>(entity);
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

        #region NRequestItem
        //Add  NRequestItem
        public BaseResultDataValue ST_UDTO_AddNRequestItem(NRequestItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBNRequestItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBNRequestItem.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  NRequestItem
        public BaseResultBool ST_UDTO_UpdateNRequestItem(NRequestItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBNRequestItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBNRequestItem.Edit();
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
        //Update  NRequestItem
        public BaseResultBool ST_UDTO_UpdateNRequestItemByField(NRequestItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBNRequestItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBNRequestItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBNRequestItem.Update(tempArray);
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
                        //baseResultBool.success = IBNRequestItem.Edit();
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
        //Delele  NRequestItem
        public BaseResultBool ST_UDTO_DelNRequestItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBNRequestItem.Entity = IBNRequestItem.Get(id);
                if (IBNRequestItem.Entity != null)
                {
                    long labid = IBNRequestItem.Entity.LabID;
                    string entityName = IBNRequestItem.Entity.GetType().Name;
                    baseResultBool.success = IBNRequestItem.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestItem(NRequestItem entity)
        {
            EntityList<NRequestItem> entityList = new EntityList<NRequestItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBNRequestItem.Entity = entity;
                try
                {
                    entityList.list = IBNRequestItem.Search();
                    entityList.count = IBNRequestItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<NRequestItem>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<NRequestItem> entityList = new EntityList<NRequestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBNRequestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBNRequestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<NRequestItem>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBNRequestItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<NRequestItem>(entity);
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

        #region NRequestForm
        //Add  NRequestForm
        public BaseResultDataValue ST_UDTO_AddNRequestForm(NRequestForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBNRequestForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBNRequestForm.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  NRequestForm
        public BaseResultBool ST_UDTO_UpdateNRequestForm(NRequestForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBNRequestForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBNRequestForm.Edit();
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
        //Update  NRequestForm
        public BaseResultBool ST_UDTO_UpdateNRequestFormByField(NRequestForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBNRequestForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBNRequestForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBNRequestForm.Update(tempArray);
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
                        //baseResultBool.success = IBNRequestForm.Edit();
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
        //Delele  NRequestForm
        public BaseResultBool ST_UDTO_DelNRequestForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBNRequestForm.Entity = IBNRequestForm.Get(id);
                if (IBNRequestForm.Entity != null)
                {
                    long labid = IBNRequestForm.Entity.LabID;
                    string entityName = IBNRequestForm.Entity.GetType().Name;
                    baseResultBool.success = IBNRequestForm.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestForm(NRequestForm entity)
        {
            EntityList<NRequestForm> entityList = new EntityList<NRequestForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBNRequestForm.Entity = entity;
                try
                {
                    entityList.list = IBNRequestForm.Search();
                    entityList.count = IBNRequestForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<NRequestForm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<NRequestForm> entityList = new EntityList<NRequestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBNRequestForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBNRequestForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<NRequestForm>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchNRequestFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBNRequestForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<NRequestForm>(entity);
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

        #region GenderType
        //Add  GenderType
        public BaseResultDataValue ST_UDTO_AddGenderType(GenderType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBGenderType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBGenderType.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  GenderType
        public BaseResultBool ST_UDTO_UpdateGenderType(GenderType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBGenderType.Entity = entity;
                try
                {
                    baseResultBool.success = IBGenderType.Edit();
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
        //Update  GenderType
        public BaseResultBool ST_UDTO_UpdateGenderTypeByField(GenderType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBGenderType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBGenderType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBGenderType.Update(tempArray);
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
                        //baseResultBool.success = IBGenderType.Edit();
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
        //Delele  GenderType
        public BaseResultBool ST_UDTO_DelGenderType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBGenderType.Entity = IBGenderType.Get(id);
                if (IBGenderType.Entity != null)
                {
                    long labid = IBGenderType.Entity.LabID;
                    string entityName = IBGenderType.Entity.GetType().Name;
                    baseResultBool.success = IBGenderType.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchGenderType(GenderType entity)
        {
            EntityList<GenderType> entityList = new EntityList<GenderType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBGenderType.Entity = entity;
                try
                {
                    entityList.list = IBGenderType.Search();
                    entityList.count = IBGenderType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GenderType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGenderTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<GenderType> entityList = new EntityList<GenderType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBGenderType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBGenderType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<GenderType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchGenderTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBGenderType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<GenderType>(entity);
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
        #region BLabDistrict
        //Add  BLabDistrict
        public BaseResultDataValue ST_UDTO_AddBLabDistrict(BLabDistrict entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.AddTime = DateTime.Now;
                IBBLabDistrict.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBBLabDistrict.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
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
        //Update  BLabDistrict
        public BaseResultBool ST_UDTO_UpdateBLabDistrict(BLabDistrict entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBBLabDistrict.Entity = entity;
                try
                {
                    baseResultBool.success = IBBLabDistrict.Edit();
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
        //Update  BLabDistrict
        public BaseResultBool ST_UDTO_UpdateBLabDistrictByField(BLabDistrict entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields ;
                IBBLabDistrict.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBLabDistrict.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBBLabDistrict.Update(tempArray);
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
                        //baseResultBool.success = IBBLabDistrict.Edit();
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
        //Delele  BLabDistrict
        public BaseResultBool ST_UDTO_DelBLabDistrict(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBBLabDistrict.Entity = IBBLabDistrict.Get(id);
                if (IBBLabDistrict.Entity != null)
                {
                    long labid = IBBLabDistrict.Entity.LabID;
                    string entityName = IBBLabDistrict.Entity.GetType().Name;
                    baseResultBool.success = IBBLabDistrict.Remove(id);
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

        public BaseResultDataValue ST_UDTO_SearchBLabDistrict(BLabDistrict entity)
        {
            EntityList<BLabDistrict> entityList = new EntityList<BLabDistrict>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBBLabDistrict.Entity = entity;
                try
                {
                    entityList.list = IBBLabDistrict.Search();
                    entityList.count = IBBLabDistrict.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BLabDistrict>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBLabDistrictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BLabDistrict> entityList = new EntityList<BLabDistrict>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBLabDistrict.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBLabDistrict.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BLabDistrict>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchBLabDistrictById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBBLabDistrict.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<BLabDistrict>(entity);
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
        public string MobileSendTest(string PhoneNumber,string Content)
        {
            try
            {
                //int sdkappid = 1400056809;
                //string appkey = "18d466f4362df7f391b77ca4e0240680";
                //int tmplId = 68357;
                //ZhiFang.WeiXin.BusinessObject.QCloud.SmsSingleSenderResult singleResult;
                //ZhiFang.WeiXin.BusinessObject.QCloud.SmsSender singleSender = new ZhiFang.WeiXin.BusinessObject.QCloud.SmsSender(sdkappid, appkey);

                //singleResult = singleSender.Send(0, "86", PhoneNumber, Content, "", "");
                //ZhiFang.Common.Log.Log.Debug("MobileSendTest.singleResult:" + singleResult.ToString());
                //if (singleResult.result == 0)
                //{
                //    return true.ToString();
                //}
                //else
                //{
                //    return singleResult.errmsg;
                //}

                //List<string> templParams = new List<string>();
                //templParams.Add("123");
                //templParams.Add("321");
                //// 指定模板单发
                //// 假设短信模板内容为：测试短信，{1}，{2}，{3}，上学。
                //singleResult = singleSender.SendWithParam("86", PhoneNumber, tmplId, templParams, "", "", "");
                //ZhiFang.Common.Log.Log.Debug("MobileSendTest.singleResult:" + singleResult.ToString() + ",tmplId:" + tmplId);
                //if (singleResult.result == 0)
                //{
                //    return true.ToString();
                //}
                //else
                //{
                //    return singleResult.errmsg;
                //}

                //ZhiFang.WeiXin.BusinessObject.QCloud.SmsMultiSenderResult multiResult;
                //ZhiFang.WeiXin.BusinessObject.QCloud.SmsMultiSender multiSender = new ZhiFang.WeiXin.BusinessObject.QCloud.SmsMultiSender(sdkappid, appkey);
                //List<string> phoneNumbers = new List<string>();
                //phoneNumbers.Add(PhoneNumber);
                //phoneNumbers.Add(PhoneNumber);
                //phoneNumbers.Add(PhoneNumber);

                //// 普通群发
                //// 下面是 3 个假设的号码
                //multiResult = multiSender.Send(0, "86", phoneNumbers, "测试短信，普通群发，深圳，小明，上学。", "", "");
                //ZhiFang.Common.Log.Log.Debug("MobileSendTest.multiResult:" + multiResult.ToString());

                //// 指定模板群发
                //// 假设短信模板内容为：测试短信，{1}，{2}，{3}，上学。
                //templParams.Clear();
                //templParams.Add("指定模板群发");
                //templParams.Add("深圳");
                //templParams.Add("小明");
                //multiResult = multiSender.SendWithParam("86", phoneNumbers, tmplId, templParams, "", "", "");
                //ZhiFang.Common.Log.Log.Debug("MobileSendTest.multiResult:" + multiResult.ToString() + ",tmplId:" + tmplId);

                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        #region 微信消费采样
        public BaseResultDataValue GetNRequestFromListByByDetailsAndRBAC(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<NRequestForm> entityList = new EntityList<NRequestForm>();
            EntityList<NRequestFormVO> nrfventityList = new EntityList<NRequestFormVO>();
            List<NRequestFormVO> nrvl = new List<NRequestFormVO>()
;            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBNRequestForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBNRequestForm.SearchListByHQL(where, page, limit);
                }
                foreach (var item in entityList.list)
                {
                    NRequestFormVO nrv = new NRequestFormVO();
                    nrv =  ZhiFang.WeiXin.Common.ClassMapperHelp.GetMapper<NRequestFormVO, NRequestForm>(item);
                    EntityList<NRequestItem> nrequestitemnl = IBNRequestItem.SearchListByHQL("nrequestitem.NRequestFormNo="+ item.Id,1,999999);
                    if (nrequestitemnl.count>0) {
                        long barcodeformno = nrequestitemnl.list.GroupBy(a => a.BarCodeFormNo).First().ToList()[0].BarCodeFormNo;
                        BarCodeForm bcf = IBBarCodeForm.Get(barcodeformno);
                        if (string.IsNullOrEmpty(nrv.BarCodeNo)) {
                            nrv.BarCodeNo = bcf.BarCode;
                        }                        
                        nrv.ItemName = bcf.ItemName;
                    }
                    if (!string.IsNullOrEmpty(item.OperDate.ToString()) && !string.IsNullOrEmpty(item.OperTime.ToString())) {
                        string ndate = item.OperDate.ToString().Split(' ')[0]+" "+ item.OperTime.ToString().Split(' ')[1];
                        item.OperDate = DateTime.Parse(ndate);
                    }
                    if (!string.IsNullOrEmpty(item.CollectDate.ToString()) && !string.IsNullOrEmpty(item.CollectTime.ToString()))
                    {
                        string ndate = item.CollectDate.ToString().Split(' ')[0] + " " + item.CollectTime.ToString().Split(' ')[1];
                        item.CollectDate = DateTime.Parse(ndate);
                    }
                    if (string.IsNullOrEmpty(item.WebLisSourceOrgName)) {
                        CLIENTELE cLIENTELE= IBCLIENTELE.Get(long.Parse(item.WebLisSourceOrgID));                        
                        item.WebLisSourceOrgName = cLIENTELE.CNAME;
                    }
                    nrvl.Add(nrv);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    nrfventityList.list = nrvl;
                    nrfventityList.count = nrvl.Count;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<NRequestFormVO>(nrfventityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(nrfventityList, fields);
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

        public BaseResultDataValue OSConsumerUserOrderForm(string PayCode, string WeblisSourceOrgID, string WeblisSourceOrgName, string AreaID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (!CheckEmpInfo())
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取采样人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return baseResultDataValue;
                }
                baseResultDataValue = IBOSUserOrderForm.OSConsumerUserOrderForm(PayCode, EmployeeName, EmployeeID, WeblisSourceOrgID, WeblisSourceOrgName, AreaID);
               
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "OSConsumerUserOrderForm.异常：" + ex.ToString();
                ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.异常："+ ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 项目过滤
        /// </summary>
        /// <param name="SuperGroupNo">检验大组</param>
        /// <param name="ItemKey">联想输入</param>
        ///<param name="rows">每页行数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="labcode">机构</param>
        /// <returns></returns>
        public BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int page, string labcode)
        {
            BaseResultDataValue resultObj = new BaseResultDataValue();
            try
            {
                resultObj = IBBLabTestItem.GetTestItem(supergroupno, itemkey, rows, page, labcode);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("GetTestItem.异常" + e.ToString());
                resultObj.ErrorInfo = "异常";// e.ToString();
                resultObj.ResultDataValue = "";
                resultObj.success = false;
            }
            return resultObj;
        }

        public BaseResultDataValue GetTestDetailByItemID(string itemid, string labcode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<TestItemDetail> ttdList = new List<TestItemDetail>();
            try
            {
                GetSubLabItem(itemid, labcode, ref ttdList);
                if (ttdList.Count > 0)
                {
                    brdv.success = true;

                    brdv.ResultDataValue = JsonHelp.Json<TestItemDetail>(ttdList); //.JsonDotNetSerializer(ttdList);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GetTestDetailByItemID.异常" + e.ToString();// e.ToString();
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error("GetTestDetailByItemID.异常" + e.ToString());
            }
            return brdv;
        }
        public void GetSubLabItem(string ItemNo, string labcode, ref List<TestItemDetail> listtestitem)
        {
            try
            {
                List<TestItemDetail> dsitem = new List<TestItemDetail>();
                if (labcode != null && labcode != "")
                {
                   
                    IList<BLabGroupItem>  BLabGroupItemIList = IBBLabGroupItem.SearchListByHQL("blabgroupitem.PItemNo='" + ItemNo+ "' and blabgroupitem.LabCode=" + labcode);
                    foreach (var item in BLabGroupItemIList)
                    {
                        TestItemDetail testItemDetail = new TestItemDetail();
                        IList<BLabTestItem> BLabTestItemIList = IBBLabTestItem.SearchListByHQL("blabtestitem.ItemNo='"+item.ItemNo+ "' and blabtestitem.LabCode='"+item.LabCode+"'");
                        testItemDetail.ItemNo = item.ItemNo;
                        testItemDetail.CName = BLabTestItemIList[0].CName;
                        testItemDetail.EName = BLabTestItemIList[0].EName;
                        testItemDetail.ColorName = BLabTestItemIList[0].Color;
                        testItemDetail.Prices = BLabTestItemIList[0].Price.ToString();
                        dsitem.Add(testItemDetail);
                    }
                }
                else
                {
                    IList <GroupItem> groupItems = IBBItemCon.SearchListByHQL("groupitem.PItemNo='" + ItemNo+"'");
                    foreach (var item in groupItems)
                    {
                        TestItemDetail testItemDetail = new TestItemDetail();
                        IList<TestItem> testItems = IBItemAllItem.SearchListByHQL("testitem.Id='" + item.ItemNo+"'" );
                        testItemDetail.ItemNo = item.ItemNo.ToString();
                        testItemDetail.CName = testItems[0].CName;
                        testItemDetail.EName = testItems[0].EName;
                        testItemDetail.ColorName = testItems[0].Color;
                        testItemDetail.Prices = testItems[0].Price.ToString();
                        dsitem.Add(testItemDetail);
                    }
                }

                if (dsitem != null && dsitem.Count > 0)
                {
                    for (int i = 0; i < dsitem.Count; i++)
                    {
                        GetSubLabItem(dsitem[i].ItemNo.ToString(), labcode, ref listtestitem);
                        TestItemDetail ttd = new TestItemDetail();
                        ttd.CName = dsitem[i].CName;
                        ttd.ItemNo = dsitem[i].ItemNo;
                        ttd.EName = dsitem[i].EName;
                        ttd.ColorName = dsitem[i].ColorName;
                        ttd.Prices = dsitem[i].Prices;
                        if (ttd.ColorName != "")
                        {
                            IList<ItemColorDict> itemColorDicts = IBItemColorDict.SearchListByHQL("itemcolordict.ColorName='" + ttd.ColorName+"'");
                            if (itemColorDicts != null && itemColorDicts.Count>0)
                                ttd.ColorValue = itemColorDicts[0].ColorValue;
                            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();

                            IList<ItemColorAndSampleTypeDetail> ItemColorAndSampleTypeDetailList = IBItemColorAndSampleTypeDetail.SearchListByHQL("itemcolorandsampletypedetail.ColorId=" + itemColorDicts[0].Id);
                            foreach (var item2 in ItemColorAndSampleTypeDetailList)
                            {
                                IList<SampleType> SampleTypeList = IBSampleType.SearchListByHQL("sampletype.Id=" + item2.SampleTypeNo);
                                foreach (var sampletype in SampleTypeList) 
                                {
                                    SampleTypeDetail sampleDetail = new SampleTypeDetail();
                                    sampleDetail.CName = sampletype.CName;
                                    sampleDetail.SampleTypeID = sampletype.Id.ToString();
                                    sampleDetailList.Add(sampleDetail);
                                }
                            }
                            ttd.SampleTypeDetail = sampleDetailList;
                        }
                        listtestitem.Add(ttd);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public BaseResultDataValue UnConsumerUserOrderForm(ConsumerUserOrderFormVO jsonentity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (!CheckEmpInfo())
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取采样人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("UnConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return baseResultDataValue;
                }
                //取消锁定
                if (IBOSUserOrderForm.UnLockOSUserOrderFormByPayCode(jsonentity.PayCode, EmployeeName, EmployeeID, jsonentity.WeblisSourceOrgID, jsonentity.WeblisSourceOrgName, jsonentity.ConsumerAreaID) <= 0)
                {
                    baseResultDataValue.success = false;
                    #region 检查状态
                    EntityList<OSUserOrderForm> entityList = IBOSUserOrderForm.SearchListByHQL("osuserorderform.PayCode='" + jsonentity.PayCode + "'", 1, 999999);
                    if (entityList.count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("UnConsumerUserOrderForm.错误的消费码！PayCode:" + jsonentity.PayCode);
                        baseResultDataValue.ErrorInfo = "错误的消费码！";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    string status = entityList.list[0].Status.ToString();
                    if (status != UserOrderFormStatus.使用中.Key)
                    {
                        ZhiFang.Common.Log.Log.Debug("UnConsumerUserOrderForm.状态错误！PayCode:" + jsonentity.PayCode + ",status:" + UserOrderFormStatus.GetStatusDic()[status].Name);
                        if (status == UserOrderFormStatus.待缴费.Key)
                        {
                            baseResultDataValue.ErrorInfo = "错误的消费码:订单未缴费！";
                        }

                        if (status == UserOrderFormStatus.已交费.Key)
                        {
                            baseResultDataValue.ErrorInfo = "错误的消费码:订单未开始使用！";
                        }
                        if (status == UserOrderFormStatus.部分使用.Key || status == UserOrderFormStatus.完全使用.Key)
                        {
                            baseResultDataValue.ErrorInfo = "错误的消费码:订单已使用！";
                        }
                        if (status == UserOrderFormStatus.取消处理中.Key || status == UserOrderFormStatus.取消成功.Key || status == UserOrderFormStatus.取消订单.Key)
                        {
                            baseResultDataValue.ErrorInfo = "错误的消费码:订单取消中！";
                        }
                        if (status == UserOrderFormStatus.退款中.Key || status == UserOrderFormStatus.退款完成.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请处理中.Key || status == UserOrderFormStatus.退款申请.Key || status == UserOrderFormStatus.退款申请被打回.Key)
                        {
                            baseResultDataValue.ErrorInfo = "错误的消费码:订单退款中！";
                        }
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    #endregion
                }                
                baseResultDataValue.success = true;
                return baseResultDataValue;

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetWeChatConsumptionSamplingInfo(string PayCode, string WeblisSourceOrgID, string WeblisSourceOrgName, string AreaID,string AreaNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (!CheckEmpInfo())
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("OSConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                return baseResultDataValue;
            }

            try
            {
                BaseResultDataValue oscbrdv = IBOSUserOrderForm.OSConsumerUserOrderForm(PayCode, EmployeeName, EmployeeID, WeblisSourceOrgID, WeblisSourceOrgName, AreaID);
                if (oscbrdv.success)
                {
                    UserOrderFormVO  userOrderFormVO = Common.JsonSerializer.JsonDotNetDeserializeObject<UserOrderFormVO>(oscbrdv.ResultDataValue);

                    //BaseResultDataValue tibrdv = IBBLabTestItem.GetTestItem("COMBI", null, 1, 1, WeblisSourceOrgID);

                   IList<BLabTestItem> aiieentityList =  IBBLabTestItem.SearchListByHQL(" blabtestitem.LabCode=" + WeblisSourceOrgID + "and blabtestitem.Visible=1" + " and blabtestitem.UseFlag=1");
                    
                    string labcode = "";
                    if (aiieentityList.Count > 0) {
                        labcode = WeblisSourceOrgID;
                    }
                    else 
                    {
                        labcode = AreaNo;
                    }
                    List<TestItemDetail> ttdList = new List<TestItemDetail>();
                    foreach (var item in userOrderFormVO.UserOrderItem)
                    {
                        BaseResultDataValue tdbtbrdv = GetTestDetailByItemID(item.ItemNo, labcode);
                        if (tdbtbrdv.success)
                        {
                            List<TestItemDetail> tidlist = Common.JsonSerializer.JsonDotNetDeserializeObject<List<TestItemDetail>>(tdbtbrdv.ResultDataValue);
                            
                            
                            foreach (var testitemd in tidlist){ testitemd.SItemNo = item.ItemNo; }
                            ttdList.AddRange(tidlist);
                        }
                        else 
                        {
                            return tdbtbrdv;
                        }
                    }

                    WeChatConsumptionSamplingInfoVO weChatConsumptionSamplingInfoVO = new WeChatConsumptionSamplingInfoVO();
                    weChatConsumptionSamplingInfoVO.userOrderFormVO = userOrderFormVO;
                    weChatConsumptionSamplingInfoVO.BLabTestItems = aiieentityList.ToList();
                    weChatConsumptionSamplingInfoVO.testItemDetails = ttdList;
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = JsonHelp.JsonDotNetSerializer(weChatConsumptionSamplingInfoVO);
                                     
                }
                else 
                {                  
                    return oscbrdv;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "GetWeChatConsumptionSamplingInfo错误：" + ex.ToString();
                ZhiFang.Common.Log.Log.Error("GetWeChatConsumptionSamplingInfo.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SaveOSConsumerUserOrderForm(NrequestCombiItemBarCodeEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (jsonentity == null)
                {
                    brdv.ErrorInfo = "参数错误！";
                    brdv.success = false;
                    return brdv;
                }
                string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (!CheckEmpInfo())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                    ZhiFang.Common.Log.Log.Error("SaveOSConsumerUserOrderForm.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                    return brdv;
                }
                long NRequestFormNo;
                BaseResult brdvNF = this.NrequestFormAddOrUpdate_WeiXinConsumer(jsonentity, out NRequestFormNo);
                if (brdvNF.success)
                {
                    brdv = IBOSUserConsumerForm.SaveOSUserConsumerForm(NRequestFormNo, jsonentity);
                    if (brdv.success)
                    {
                        brdv.ResultDataValue = "true";
                        brdv.success = true;
                    }
                    else
                    {
                        NRequestForm nrfl = new NRequestForm();
                        IList<NRequestItem> nril = new List<NRequestItem>();
                        List<BarCodeForm> bcfl = new List<BarCodeForm>();
                        nrfl = IBNRequestForm.Get(NRequestFormNo);
                        if (nrfl != null ) {
                            nril = IBNRequestItem.SearchListByHQL("nrequestitem.NRequestFormNo=" + nrfl.Id);                        
                        }
                        if (nril != null && nril.Count>0) {
                            foreach (var item in nril)
                            {                                
                                bcfl.AddRange(IBBarCodeForm.SearchListByHQL("barcodeform.Id="+item.BarCodeFormNo+ " and barcodeform.WebLisFlag=5"));
                            }                            
                        }
                        if (nrfl==null || nril.Count <= 0 || bcfl.Count <= 0) {
                            IBNRequestForm.Remove(NRequestFormNo);
                        }
                        brdv.ResultDataValue = "false";
                        brdv.success = false;
                        brdv.ErrorInfo = "消费单保存失败！" + brdv.ErrorInfo;
                    }
                }
                else
                {
                    brdv.ResultDataValue = "false";
                    brdv.ErrorInfo = "新增申请单错误！" + brdvNF.ErrorInfo;
                    ZhiFang.Common.Log.Log.Error("SaveOSConsumerUserOrderForm.新增申请单错误！brdvNF.ErrorInfo:" + brdvNF.ErrorInfo);
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("SaveOSConsumerUserOrderForm.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }
        public BaseResult NrequestFormAddOrUpdate_WeiXinConsumer(NrequestCombiItemBarCodeEntity jsonentity, out long NRequestFormNo)
        {
            BaseResult br = new BaseResult();
            NRequestFormNo = 0;
            try
            {
                string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                #region 验证

                #region 检查订单状态
                var checkpaycode = IBOSUserOrderForm.CheckPayCodeIsUseing(jsonentity.PayCode, EmployeeName, EmployeeID, jsonentity.NrequestForm.ClientNo, jsonentity.NrequestForm.ClientName);

                if (!checkpaycode.success)
                {
                    return checkpaycode;
                }
                #endregion

                bool b = jsonentity.BarCodeList.GroupBy(l => l.BarCode).Where(g => g.Count() > 1).Count() > 0;

                var barcodelist = jsonentity.BarCodeList.GroupBy(l => l.BarCode);
                foreach (var bg in barcodelist)
                {
                    if (bg.GroupBy(barcode => barcode.ColorName).Count() > 1)
                    {
                        br.success = false;
                        br.ErrorInfo = "输入的条码号有重复！BarCode:" + bg.Key;
                        ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate_WeiXinConsumer.Account:" + EmployeeName + "输入的条码号有重复！BarCode:" + bg.Key);
                        return br;
                    }
                }

                //if (b)
                //{
                //    br.success = false;
                //    br.ErrorInfo = "输入的条码号有重复！";
                //    return br;
                //}
                //数据库中条码为唯一值
                string repeatbarcodestr;
                if (IBBarCodeForm.IsExistBarCode(jsonentity.flag, jsonentity.BarCodeList, out repeatbarcodestr))
                {
                    br.success = false;
                    br.ErrorInfo = "条码号:'" + repeatbarcodestr + "'已存在！";
                    return br;
                }
                #endregion                

                NRequestForm nrf_m = null;
                NRequestItem nri_m = new NRequestItem();
                BarCodeForm bcf_m = new BarCodeForm();

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion

                //申请单号
                long nRequestFormNo;

                if ((long)jsonentity.NrequestForm.Id == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.Id;
                NRequestFormNo = nRequestFormNo;

                #region 对象赋值

                #region 组合项目
                List<NRequestItem> nri_List = new List<NRequestItem>();
                foreach (UiCombiItem uicombiItem in jsonentity.CombiItems)
                {
                    //明细
                    foreach (UiCombiItemDetail uicombiItemDetail in uicombiItem.CombiItemDetailList)
                    {
                        nri_m = new NRequestItem();

                        ////假组套,组套中只包含自己
                        //if (uicombiItem.CombiItemDetailList.Count == 1 && uicombiItem.CombiItemNo == uicombiItemDetail.CombiItemDetailNo)
                        //{

                        //}
                        //else
                        nri_m.CombiItemNo = int.Parse(uicombiItem.CombiItemNo);//uicombiItemDetail.CombiItemDetailNo;
                        nri_m.ParItemNo = int.Parse(uicombiItemDetail.CombiItemDetailNo.ToString());
                        nri_m.NRequestFormNo = nRequestFormNo;

                        //nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nri_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nri_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nri_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nri_List.Add(nri_m);
                    }
                }
                #endregion

                #region 表单对象
                nrf_m = new NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                nrf_m.Id = nRequestFormNo;
                //nrf_m.WebLisSourceOrgID = ClientNo;
                //nrf_m.WebLisSourceOrgName = txtClientNo;
                //nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                nrf_m.Collecter = EmployeeName;
                //jsonentity.NrequestForm.CollecterID = EmployeeID;
                jsonentity.NrequestForm.CollecterName = EmployeeID;
                #endregion

                #region 条码

                List<BarCodeForm> bcf_List = new List<BarCodeForm>();
                List<string> barcodestringlist = new List<string>();
                foreach (UiBarCode uibc in jsonentity.BarCodeList)
                {
                    bcf_m = new BarCodeForm();

                    bcf_m.BarCode = uibc.BarCode;
                    bcf_m.Color = uibc.ColorName;
                    barcodestringlist.Add(uibc.BarCode);
                    int sampleTypeNo;
                    int.TryParse(uibc.SampleType, out sampleTypeNo);
                    bcf_m.SampleTypeNo = sampleTypeNo;

                    //bcf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                    bcf_m.WebLisSourceOrgId = nrf_m.ClientNo;
                    bcf_m.WebLisSourceOrgName = nrf_m.ClientName;
                    bcf_m.ClientNo = nrf_m.ClientNo;
                    bcf_m.ClientName = nrf_m.ClientName;
                    bcf_m.CollectDate = nrf_m.CollectDate;
                    bcf_m.CollectTime = nrf_m.CollectTime;
                    bcf_m.Collecter = EmployeeName;
                    bcf_m.CollecterID = long.Parse(EmployeeID);
                    bool flag = false;
                    if (jsonentity.flag == "1")
                    {
                        bcf_m.Id = GUIDHelp.GetGUIDLong();
                        //1条码对应多个子项目                       
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == int.Parse(strItem));
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.Id;
                                string ItemCenterNo = IBBTestItemControl.GetCenterNo(jsonentity.NrequestForm.Area, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = IBItemAllItem.Get(long.Parse(ItemCenterNo));
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }

                    }
                    else if (jsonentity.flag == "0")
                    {
                        IList<BarCodeForm> ds = IBBarCodeForm.SearchListByHQL("barcodeform.BarCode='"+uibc.BarCode+"'");
                        if (ds != null && ds.Count > 0)
                        {
                            long barCodeFormNo;
                            long.TryParse(ds[0].Id.ToString(), out barCodeFormNo);
                            bcf_m.Id = barCodeFormNo;

                        }
                        else
                            bcf_m.Id = GUIDHelp.GetGUIDLong();

                        //1条码对应多个子项目                     
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == int.Parse(strItem));
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.Id;
                                string ItemCenterNo = IBBTestItemControl.GetCenterNo(jsonentity.NrequestForm.Area, nilist.ElementAt(0).CombiItemNo.ToString());
                                TestItem ti = IBItemAllItem.Get(long.Parse(ItemCenterNo));
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }
                    }
                    if (bcf_m.ItemName != null && bcf_m.ItemName.Length > 0)
                    {
                        bcf_m.ItemName = bcf_m.ItemName.Remove(bcf_m.ItemName.LastIndexOf(','));
                    }
                    if (bcf_m.ItemNo != null && bcf_m.ItemNo.Length > 0)
                    {
                        bcf_m.ItemNo = bcf_m.ItemNo.Remove(bcf_m.ItemNo.LastIndexOf(','));
                    }
                    bcf_List.Add(bcf_m);
                }

                #endregion

                #endregion

                if (jsonentity.flag.Trim().ToString() == "0")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {
                        //先删除
                        NRequestItem Entity = IBNRequestItem.Get(nRequestFormNo);
                        if (Entity != null)
                        {
                             IBNRequestItem.Remove(nRequestFormNo);
                        }

                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = int.Parse(IBBTestItemControl.GetCenterNo(nrf_m.Area, nri.ParItemNo.ToString()));
                            nri.CombiItemNo = int.Parse(IBBTestItemControl.GetCenterNo(nrf_m.Area, nri.CombiItemNo.ToString()));
                            nri.DataAddTime = DateTime.Now;
                            IBNRequestItem.Entity = nri;
                            bool i = IBNRequestItem.Add();
                            if (i)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;
                        }

                    }

                    #endregion

                    #region BarCodeForm
                    if (bcf_List != null && bNRequestItemRusult == true)
                    {

                        foreach (BarCodeForm bcf in bcf_List)
                        {

                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            //bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = EmployeeName;
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            bool i = true;
                            BarCodeForm barCodeForm = IBBarCodeForm.Get((long)bcf.Id);
                            if (barCodeForm != null)
                            {
                                IBBarCodeForm.Entity = bcf;
                                i = IBBarCodeForm.Edit();
                            }
                            else {
                                IBBarCodeForm.Entity = bcf;
                                 i = IBBarCodeForm.Add();
                            }

                            if (i)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && nRequestFormNo > 0 && bBarCodeFormRusult == true)
                    {
                        nrf_m.Id = nRequestFormNo;
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        //nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                       // nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        nrf_m.Barcode = (barcodestringlist.Count > 0) ? string.Join(",", barcodestringlist.ToArray()) : "";
                        IBNRequestForm.Entity = nrf_m;
                        bool  i = IBNRequestForm.Edit();
                        if (i)
                        {
                            bNRequestFormRusult = true;
                        }
                    }

                    #endregion
                }
                else if (jsonentity.flag.Trim().ToString() == "1")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {


                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = int.Parse(IBBTestItemControl.GetCenterNo(nrf_m.Area, nri.ParItemNo.ToString()));
                            int result = 0;
                            if (int.TryParse(IBBTestItemControl.GetCenterNo(nrf_m.Area, nri.CombiItemNo.ToString()), out result))
                            {
                                nri.CombiItemNo = result;
                            }
                            IBNRequestItem.Entity = nri;
                            IBNRequestItem.Entity.DataAddTime = DateTime.Now;
                            if (IBNRequestItem.Add())
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;

                        }

                    }

                    #endregion

                    #region BarCodeForm

                    if (bcf_List != null && bNRequestItemRusult == true)
                    {
                        //ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
                        foreach (BarCodeForm bcf in bcf_List)
                        {
                            //ZhiFang.Common.Log.Log.Debug("bcf.BarCode:" + bcf.BarCode);
                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            //bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = EmployeeName;
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            IBBarCodeForm.Entity = bcf;
                            if (IBBarCodeForm.Add())
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && bBarCodeFormRusult == true)
                    {
                        nrf_m.Id = nRequestFormNo;
                        nrf_m.SerialNo = nRequestFormNo.ToString();
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        //nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        //nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        nrf_m.ZDY10 = jsonentity.PayCode;//暂定
                        nrf_m.Barcode = (barcodestringlist.Count > 0) ? string.Join(",", barcodestringlist.ToArray()) : "";
                        IBNRequestForm.Entity = nrf_m;
                        IBNRequestForm.Entity.DataAddTime = DateTime.Now;
                        if (IBNRequestForm.Add())
                        {
                            bNRequestFormRusult = true;
                        }
                    }

                    #endregion
                }

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    br.success = true;
                else
                    br.success = false;

            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
                //throw;
            }
            return br;
        }

        public BaseResultDataValue SearchUnConsumerUserOrderFormList(string PayCode,string WeblisSourceOrgID,string WeblisSourceOrgName,string ConsumerAreaID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(WeblisSourceOrgID) || string.IsNullOrEmpty(WeblisSourceOrgName))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }
            string EmployeeID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string EmployeeName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (!CheckEmpInfo())
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取采样人信息！请重新登录！";
                ZhiFang.Common.Log.Log.Error("ZhiFang.WeiXin_ZhiFangWeiXinService_SearchUnConsumerUserOrderFormList.无法获取采样人信息！请重新登录！IP:" + HttpContext.Current.Request.UserHostAddress);
                return brdv;
            }
            try
            {
                if (string.IsNullOrEmpty(PayCode)) {
                    PayCode = "";
                }
                List<OSUserOrderForm> osuof = new List<OSUserOrderForm>();
                if (PayCode.ToLower().IndexOf("del") < 0 && PayCode.ToLower().IndexOf("drop") < 0 && PayCode.ToLower().IndexOf("update") < 0)
                {
                    if (PayCode != null && PayCode.Trim() != "")
                    {
                        osuof = IBOSUserOrderForm.SearchListByHQL("osuserorderform.PayCode='" + PayCode + "' and osuserorderform.Status=" + UserOrderFormStatus.使用中.Key + " and osuserorderform.WeblisSourceOrgID=" + WeblisSourceOrgID + " and osuserorderform.WeblisSourceOrgName='" + WeblisSourceOrgName + "' and osuserorderform.EmpID=" + EmployeeID + " and osuserorderform.EmpAccount='" + EmployeeName + "'").ToList();
                    }
                    else
                    {
                        osuof = IBOSUserOrderForm.SearchListByHQL("osuserorderform.Status = " + UserOrderFormStatus.使用中.Key + " and osuserorderform.WeblisSourceOrgID = " + WeblisSourceOrgID + " and osuserorderform.WeblisSourceOrgName = '" + WeblisSourceOrgName + "' and osuserorderform.EmpID = " + EmployeeID + " and osuserorderform.EmpAccount = '" + EmployeeName + "'").ToList();
                    }
                }
                if (osuof != null && osuof.Count > 0)
                {
                    brdv.success = true;
                    brdv.ResultDataValue = JsonHelp.JsonDotNetSerializer(osuof);
                }
                else 
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未查找到相关订单！";
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序错误！";
                ZhiFang.Common.Log.Log.Error("ZhiFang.WeiXin_ZhiFangWeiXinService_SearchUnConsumerUserOrderFormList.程序错误:" + ex.ToString());
                //throw new Exception(ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue PrintNRequestForm_PDF(string where, long LabId, string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string EmpID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmployeeName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (EmpID == null || EmpID == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未登录，请登录后操作！";
                    return baseResultDataValue;
                }
                Dictionary<string, string> title = new Dictionary<string, string>();
                title.Add("Text_DateRange", StartDateTime + "-" + EndDateTime);
                SortedList<string, string> dataTable = IBNRequestForm.StatisticsNRequestForm_Frx("", where, LabId, EmpID, EmployeeName, title);
                try
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现错误：" + e.Message;
            }
            return baseResultDataValue;
        }


        #endregion
    }
}
