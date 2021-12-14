using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.SA.ServerContract;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.SA.Common;

namespace ZhiFang.SA
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CommonService: ICommonService
    {
        IBLL.Common.IBFFile IBFFile { get; set; }
        IBLL.Common.IBFFileAttachment IBFFileAttachment { get; set; }
        IBLL.Common.IBFFileCopyUser IBFFileCopyUser { get; set; }
        IBLL.Common.IBFFileInteraction IBFFileInteraction { get; set; }
        IBLL.Common.IBFFileOperation IBFFileOperation { get; set; }
        IBLL.Common.IBFFileReadingLog IBFFileReadingLog { get; set; }
        IBLL.Common.IBFFileReadingUser IBFFileReadingUser { get; set; }
        ZhiFang.IBLL.SA.IBBDictTree IBBDictTree { get; set; }

        #region FFile
        /// <summary>
        /// 查询某一类型树的直属文档列表(包含某一类型树的所有子类型树)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileByBDictTreeId(string where, bool isSearchChildNode, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFile> tempEntityList = new EntityList<FFile>();
            try
            {
                string maxLevelStr = "";
                tempEntityList = IBFFile.SearchFFileByBDictTreeId(where, isSearchChildNode, page, limit, CommonServiceMethod.GetSortHQL(sort), maxLevelStr);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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

        //Add  FFile
        public BaseResultDataValue QMS_UDTO_AddFFile(FFile entity)
        {
            IBFFile.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultDataValue.success = IBFFile.Add();
                    if (tempBaseResultDataValue.success)
                    {
                        IBFFile.Get(IBFFile.Entity.Id);
                        tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFile.Entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultDataValue;
        }
        public Stream QMS_UDTO_AddFFileByFormData()
        {
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
            FFile entity = null;
            IList<FFileCopyUser> fFileCopyUserList = null;
            IList<FFileReadingUser> fFileReadingUserList = null;
            string fFileEntity = ""; //原服务FFile实体参数的json串 不包括文档内容 FFile entity
            string fFileContent = "";  //文档内容
            string strFFileCopyUserList = ""; //原服务参数的json串 IList< FFileCopyUser > fFileCopyUserList
            int fFileOperationType = -1; //原服务参数 int fFileOperationType
            int ffileReadingUserType = -1;
            int ffileCopyUserType = -1;  //原服务参数 int ffileCopyUserType
            string ffileOperationMemo = ""; //原服务参数 string ffileOperationMemo
            string strFileReadingUserList = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile newThumbnails = null;//新闻缩略图上传
            int iTotal = HttpContext.Current.Request.Files.Count;
            if (iTotal > 0)
            {
                newThumbnails = HttpContext.Current.Request.Files[0];
            }
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "fFileEntity":
                        if (HttpContext.Current.Request.Form["fFileEntity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["fFileEntity"].Trim();
                        break;
                    case "fFileContent":
                        if (HttpContext.Current.Request.Form["fFileContent"].Trim() != "")
                            fFileContent = HttpContext.Current.Request.Form["fFileContent"].Trim();
                        break;
                    case "fFileCopyUserList":
                        if (HttpContext.Current.Request.Form["fFileCopyUserList"].Trim() != "")
                            strFFileCopyUserList = HttpContext.Current.Request.Form["fFileCopyUserList"].Trim();
                        break;
                    case "ffileCopyUserType":
                        if (HttpContext.Current.Request.Form["ffileCopyUserType"].Trim() != "")
                            ffileCopyUserType = int.Parse(HttpContext.Current.Request.Form["ffileCopyUserType"].Trim());
                        break;
                    case "fFileReadingUserList"://阅读人
                        if (HttpContext.Current.Request.Form["fFileReadingUserList"].Trim() != "")
                            strFileReadingUserList = HttpContext.Current.Request.Form["fFileReadingUserList"].Trim();
                        break;
                    case "ffileReadingUserType"://阅读人类型
                        if (HttpContext.Current.Request.Form["ffileReadingUserType"].Trim() != "")
                            ffileReadingUserType = int.Parse(HttpContext.Current.Request.Form["ffileReadingUserType"].Trim());
                        break;
                    case "fFileOperationType":
                        if (HttpContext.Current.Request.Form["fFileOperationType"].Trim() != "")
                            fFileOperationType = int.Parse(HttpContext.Current.Request.Form["fFileOperationType"].Trim());
                        break;
                    case "ffileOperationMemo":
                        if (HttpContext.Current.Request.Form["ffileOperationMemo"].Trim() != "")
                            ffileOperationMemo = HttpContext.Current.Request.Form["ffileOperationMemo"].Trim();
                        break;
                    case "file":

                        break;
                }
            }
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
                ZhiFang.Common.Log.Log.Error("新增文档信息出错1:" + tempBaseResultBool.ErrorInfo);
            }
            if (tempBaseResultBool.success && string.IsNullOrEmpty(fFileEntity))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "fFileEntity为空!";
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<FFile>(fFileEntity);
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "新增文档信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增文档信息序列化出错:" + ex.Message);
                }
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    if (!string.IsNullOrEmpty(strFFileCopyUserList))
                        fFileCopyUserList = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<FFileCopyUser>>(strFFileCopyUserList);
                    if (!string.IsNullOrEmpty(strFileReadingUserList))
                        fFileReadingUserList = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<FFileReadingUser>>(strFileReadingUserList);
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "新增文档信息序列化抄送人或阅读人信息出错!";
                    ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo + ex.Message);
                }
            }
            if (tempBaseResultBool.success)
            {
                //发布人,发布时间处理
                if (entity.Status == int.Parse(FFileStatus.发布.Key))
                {
                    entity.PublisherDateTime = DateTime.Now;
                    if (!entity.PublisherId.HasValue)
                    {
                        entity.PublisherId = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    }
                    if (entity.PublisherName == null)
                    {
                        entity.PublisherName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    }
                }
                entity.Content = fFileContent;
                IBFFile.Entity = entity;
                try
                {
                    tempBaseResultBool = IBFFile.AddFFileAndFFileCopyUser(entity, fFileCopyUserList, fFileOperationType, ffileCopyUserType, ffileOperationMemo, fFileReadingUserList, ffileReadingUserType, newThumbnails);
                    if (tempBaseResultBool.success)
                    {
                        IBFFile.Get(entity.Id);
                        tempBaseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增文档信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        /// <summary>
        /// 更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息
        /// 需要登录后才可操作(超级管理员也不能操作新增及编辑文档信息)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public Stream QMS_UDTO_UpdateFFileByFieldAndFormData()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            FFile entity = null;
            IList<FFileCopyUser> fFileCopyUserList = null;
            IList<FFileReadingUser> fFileReadingUserList = null;
            string fields = "";
            int fFileOperationType = 0;
            int ffileCopyUserType = -1;
            int ffileReadingUserType = -1;
            string ffileOperationMemo = "";

            string fFileEntity = ""; //原服务FFile实体参数的json串 不包括文档内容 FFile entity
            string fFileContent = "";  //文档内容
            string strFFileCopyUserList = ""; //原服务参数的json串 IList< FFileCopyUser > 
            string strFileReadingUserList = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "fields":
                        if (HttpContext.Current.Request.Form["fields"].Trim() != "")
                            fields = HttpContext.Current.Request.Form["fields"].Trim();
                        break;
                    case "fFileEntity"://Entity
                        if (HttpContext.Current.Request.Form["fFileEntity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["fFileEntity"].Trim();
                        break;
                    case "fFileContent"://文档内容
                        if (HttpContext.Current.Request.Form["fFileContent"].Trim() != "")
                            fFileContent = HttpContext.Current.Request.Form["fFileContent"].Trim();
                        break;
                    case "fFileCopyUserList"://抄送人
                        if (HttpContext.Current.Request.Form["fFileCopyUserList"].Trim() != "")
                            strFFileCopyUserList = HttpContext.Current.Request.Form["fFileCopyUserList"].Trim();
                        break;
                    case "ffileCopyUserType"://抄送人类型
                        if (HttpContext.Current.Request.Form["ffileCopyUserType"].Trim() != "")
                            ffileCopyUserType = int.Parse(HttpContext.Current.Request.Form["ffileCopyUserType"].Trim());
                        break;

                    case "fFileReadingUserList"://阅读人
                        if (HttpContext.Current.Request.Form["fFileReadingUserList"].Trim() != "")
                            strFileReadingUserList = HttpContext.Current.Request.Form["fFileReadingUserList"].Trim();
                        break;
                    case "ffileReadingUserType"://阅读人类型
                        if (HttpContext.Current.Request.Form["ffileReadingUserType"].Trim() != "")
                            ffileReadingUserType = int.Parse(HttpContext.Current.Request.Form["ffileReadingUserType"].Trim());
                        break;
                    case "fFileOperationType"://文档操作类型
                        if (HttpContext.Current.Request.Form["fFileOperationType"].Trim() != "")
                            fFileOperationType = int.Parse(HttpContext.Current.Request.Form["fFileOperationType"].Trim());
                        break;

                    case "ffileOperationMemo":
                        if (HttpContext.Current.Request.Form["ffileOperationMemo"].Trim() != "")
                            ffileOperationMemo = HttpContext.Current.Request.Form["ffileOperationMemo"].Trim();
                        break;
                }
            }
            if (!string.IsNullOrEmpty(fFileEntity))
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<FFile>(fFileEntity);
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新文档信息序列化出错:" + tempBaseResultBool.ErrorInfo);
                }
            }
            if (!string.IsNullOrEmpty(strFFileCopyUserList))
                fFileCopyUserList = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<FFileCopyUser>>(strFFileCopyUserList);

            if (!string.IsNullOrEmpty(strFileReadingUserList))
                fFileReadingUserList = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<FFileReadingUser>>(strFileReadingUserList);
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (tempBaseResultBool.success)
            {
                entity = IBFFile.EditFFileOperationInfo(entity, "edit", ref fields);
                IBFFile.Entity = entity;
                entity.Content = fFileContent;
                IBFFile.Entity = entity;
                try
                {
                    //发布人,发布时间处理
                    if (IBFFile.Entity.Status == int.Parse(FFileStatus.发布.Key))
                    {
                        IBFFile.Entity.PublisherDateTime = DateTime.Now;
                        if (IBFFile.Entity.PublisherId == null)
                        {
                            IBFFile.Entity.PublisherId = long.Parse(employeeID);
                        }
                        if (IBFFile.Entity.PublisherName == null)
                        {
                            IBFFile.Entity.PublisherName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        }
                        if (!fields.Contains("PublisherDateTime"))
                        {
                            fields = fields + ",PublisherDateTime";
                        }
                        if (!fields.Contains("PublisherId"))
                        {
                            fields = fields + ",PublisherId";
                        }
                        if (!fields.Contains("PublisherName"))
                        {
                            fields = fields + ",PublisherName";
                        }
                        entity = IBFFile.Entity;
                    }
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFile.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool = IBFFile.SaveFFileAndFFileCopyUserAndFFileReadingUser(tempArray, fFileCopyUserList, fFileReadingUserList, fFileOperationType, ffileCopyUserType, ffileReadingUserType, ffileOperationMemo, entity);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新文档信息出错:" + tempBaseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新文档信息出错:" + tempBaseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新文档信息出错1:" + tempBaseResultBool.ErrorInfo);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }

        /// <summary>
        /// 新增文档信息及文档抄送对象信息
        /// 需要登录后才可操作(超级管理员也不能操作新增及编辑文档信息)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type">操作记录的操作类型值</param>
        /// <param name="ffileCopyUserType">搅抄送对象类型,(=-1默认没有选择)</param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserType, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType)
        {
            IBFFile.Entity = entity;
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultBool = IBFFile.AddFFileAndFFileCopyUser(entity, fFileCopyUserList, fFileOperationType, ffileCopyUserType, ffileOperationMemo, fFileReadingUserList, ffileReadingUserType, null);
                    if (tempBaseResultBool.success)
                    {
                        IBFFile.Get(IBFFile.Entity.Id);
                        tempBaseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFile.Entity);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        //Update  FFile
        public BaseResultBool QMS_UDTO_UpdateFFile(FFile entity)
        {
            IBFFile.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultBool.success = IBFFile.Edit();
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        //Update  FFile
        public BaseResultBool QMS_UDTO_UpdateFFileByField(FFile entity, string fields)
        {
            IBFFile.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFile.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool.success = IBFFile.Update(tempArray);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //tempBaseResultBool.success = IBFFile.Edit();
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息
        /// 需要登录后才可操作(超级管理员也不能操作新增及编辑文档信息)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField(FFile entity, IList<FFileCopyUser> fFileCopyUserList, IList<FFileReadingUser> fFileReadingUserList, string fields, int fFileOperationType, int ffileCopyUserType, int ffileReadingUserType, string ffileOperationMemo)
        {

            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity信息为空!";
            }

            if (tempBaseResultBool.success == true)
            {
                try
                {
                    entity = IBFFile.EditFFileOperationInfo(entity, "edit", ref fields);
                    IBFFile.Entity = entity;
                    //发布人,发布时间处理
                    if (IBFFile.Entity.Status == int.Parse(FFileStatus.发布.Key))
                    {
                        IBFFile.Entity.PublisherDateTime = DateTime.Now;
                        if (IBFFile.Entity.PublisherId == null)
                        {
                            IBFFile.Entity.PublisherId = long.Parse(employeeID);
                        }
                        if (IBFFile.Entity.PublisherName == null)
                        {
                            IBFFile.Entity.PublisherName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                        }
                        if (!fields.Contains("PublisherDateTime"))
                        {
                            fields = fields + ",PublisherDateTime";
                        }
                        if (!fields.Contains("PublisherId"))
                        {
                            fields = fields + ",PublisherId";
                        }
                        if (!fields.Contains("PublisherName"))
                        {
                            fields = fields + ",PublisherName";
                        }
                        entity = IBFFile.Entity;
                    }
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFile.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool = IBFFile.SaveFFileAndFFileCopyUserAndFFileReadingUser(tempArray, fFileCopyUserList, fFileReadingUserList, fFileOperationType, ffileCopyUserType, ffileReadingUserType, ffileOperationMemo, entity);
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //tempBaseResultBool.success = IBFFile.Edit();
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 删除文档信息(更新IsUse为false,文档状态为作废)
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool QMS_UDTO_DeleleFFileByIds(string strIds, bool isUse, int fFileOperationType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultBool = IBFFile.UpdateFFileIsUseByIds(strIds, isUse, fFileOperationType);
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 置顶/撤消置顶文档信息(更新IsTop为false,为撤消置顶)
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool QMS_UDTO_UpdateFFileIsTopByIds(string strIds, bool isTop, int fFileOperationType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                try
                {
                    tempBaseResultBool = IBFFile.UpdateFFileIsTopByIds(strIds, isTop, fFileOperationType);
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            return tempBaseResultBool;
        }

        //Delele  FFile
        public BaseResultBool QMS_UDTO_DelFFile(long longFFileID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFile.Remove(longFFileID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFile(FFile entity)
        {
            IBFFile.Entity = entity;
            EntityList<FFile> tempEntityList = new EntityList<FFile>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFile.Search();
                tempEntityList.count = IBFFile.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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
        /// <summary>
        /// 查询文档表ByHQL
        /// 登录帐号及登录名称不能为空,为空时返回空文档信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (String.IsNullOrEmpty(employeeID) && String.IsNullOrEmpty(employeeName))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                EntityList<FFile> tempEntityList = new EntityList<FFile>();
                try
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBFFile.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBFFile.SearchListByHQL(where, page, limit);
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 调用该服务时需要处理是否添加文件浏览的操作记录及是否添加文档阅读记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="isAddFFileReadingLog">是否需要添加文档阅读记录信息:1需要,0:不需要</param>
        /// <param name="isAddFFileOperation">是否需要添加文档操作记录信息:1需要,0:不需要</param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileById(long id, string fields, bool isPlanish, int isAddFFileReadingLog, int isAddFFileOperation)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            //if (String.IsNullOrEmpty(employeeID))
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            //}
            //else
            //{
            FFile tempEntity = null;
            try
            {
                tempEntity = IBFFile.GetFFileAndAddFFileCopyUser(id, isAddFFileReadingLog, isAddFFileOperation);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                throw new Exception(ex.Message);
            }
            if (tempEntity != null)
            {
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFile>(tempEntity);
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
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取信息为空";
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            // }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询当前登录者的抄送文档信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileCopyUserListByHQLAndEmployeeID(int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                EntityList<FFile> tempEntityList = new EntityList<FFile>();
                try
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBFFile.SearchFFileCopyUserListByHQLAndEmployeeID(where, isSearchChildNode, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBFFile.SearchFFileCopyUserListByHQLAndEmployeeID(where, isSearchChildNode, page, limit);
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 查询当前登录者的阅读文档信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID(int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                EntityList<FFile> tempEntityList = new EntityList<FFile>();
                try
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBFFile.SearchFFileReadingUserListByHQLAndEmployeeID(where, isSearchChildNode, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBFFile.SearchFFileReadingUserListByHQLAndEmployeeID(where, isSearchChildNode, page, limit);
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids(string dictreeids, int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            else
            {
                EntityList<FFile> tempEntityList = new EntityList<FFile>();
                try
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBFFile.SearchFFileReadingUserListByHQLAndEmployeeID(dictreeids, where, isSearchChildNode, CommonServiceMethod.GetSortHQL(sort), page, limit);
                    }
                    else
                    {
                        tempEntityList = IBFFile.SearchFFileReadingUserListByHQLAndEmployeeID(dictreeids, where, isSearchChildNode, null, page, limit);
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);
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
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue QMS_FFileWeiXinMessagePushById(long id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (id <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：id为空或小于等于0！";
                    return tempBaseResultDataValue;
                }
                tempBaseResultDataValue.success = IBFFile.FFileWeiXinMessagePushById((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, id);
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

        #region FFileAttachment
        //Add  FFileAttachment
        public BaseResultDataValue QMS_UDTO_AddFFileAttachment(FFileAttachment entity)
        {
            IBFFileAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFileAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileAttachment.Get(IBFFileAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileAttachment.Entity);
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
        //Update  FFileAttachment
        public BaseResultBool QMS_UDTO_UpdateFFileAttachment(FFileAttachment entity)
        {
            IBFFileAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFileAttachment
        public BaseResultBool QMS_UDTO_UpdateFFileAttachmentByField(FFileAttachment entity, string fields)
        {
            IBFFileAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileAttachment.Edit();
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
        //Delele  FFileAttachment
        public BaseResultBool QMS_UDTO_DelFFileAttachment(long longFFileAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileAttachment.Remove(longFFileAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileAttachment(FFileAttachment entity)
        {
            IBFFileAttachment.Entity = entity;
            EntityList<FFileAttachment> tempEntityList = new EntityList<FFileAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileAttachment.Search();
                tempEntityList.count = IBFFileAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileAttachment>(tempEntityList);
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
        //查询文档附件表ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileAttachment> tempEntityList = new EntityList<FFileAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileAttachment>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileAttachment>(tempEntity);
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
        #endregion

        #region FFileCopyUser
        //Add  FFileCopyUser
        public BaseResultDataValue QMS_UDTO_AddFFileCopyUser(FFileCopyUser entity)
        {
            IBFFileCopyUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFileCopyUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileCopyUser.Get(IBFFileCopyUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileCopyUser.Entity);
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
        //Update  FFileCopyUser
        public BaseResultBool QMS_UDTO_UpdateFFileCopyUser(FFileCopyUser entity)
        {
            IBFFileCopyUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileCopyUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFileCopyUser
        public BaseResultBool QMS_UDTO_UpdateFFileCopyUserByField(FFileCopyUser entity, string fields)
        {
            IBFFileCopyUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileCopyUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileCopyUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileCopyUser.Edit();
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
        //Delele  FFileCopyUser
        public BaseResultBool QMS_UDTO_DelFFileCopyUser(long longFFileCopyUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileCopyUser.Remove(longFFileCopyUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileCopyUser(FFileCopyUser entity)
        {
            IBFFileCopyUser.Entity = entity;
            EntityList<FFileCopyUser> tempEntityList = new EntityList<FFileCopyUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileCopyUser.Search();
                tempEntityList.count = IBFFileCopyUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileCopyUser>(tempEntityList);
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
        //查询文档抄送对象表ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileCopyUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileCopyUser> tempEntityList = new EntityList<FFileCopyUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileCopyUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileCopyUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileCopyUser>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileCopyUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileCopyUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileCopyUser>(tempEntity);
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
        #endregion

        #region FFileInteraction
        //Add  FFileInteraction
        public BaseResultDataValue QMS_UDTO_AddFFileInteraction(FFileInteraction entity)
        {
            IBFFileInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            try
            {
                tempBaseResultDataValue.success = IBFFileInteraction.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileInteraction.Get(IBFFileInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileInteraction.Entity);
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
        //Update  FFileInteraction
        public BaseResultBool QMS_UDTO_UpdateFFileInteraction(FFileInteraction entity)
        {
            IBFFileInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                tempBaseResultBool.success = IBFFileInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);

            }
            return tempBaseResultBool;
        }
        //Update  FFileInteraction
        public BaseResultBool QMS_UDTO_UpdateFFileInteractionByField(FFileInteraction entity, string fields)
        {
            IBFFileInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileInteraction.Edit();
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
        //Delele  FFileInteraction
        public BaseResultBool QMS_UDTO_DelFFileInteraction(long longFFileInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileInteraction.Remove(longFFileInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileInteraction(FFileInteraction entity)
        {
            IBFFileInteraction.Entity = entity;
            EntityList<FFileInteraction> tempEntityList = new EntityList<FFileInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileInteraction.Search();
                tempEntityList.count = IBFFileInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileInteraction>(tempEntityList);
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
        //查询文档交流表（不附带附件）ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileInteraction> tempEntityList = new EntityList<FFileInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileInteraction>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileInteraction>(tempEntity);
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
        #endregion

        #region FFileOperation
        //Add  FFileOperation
        public BaseResultDataValue QMS_UDTO_AddFFileOperation(FFileOperation entity)
        {
            IBFFileOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFileOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileOperation.Get(IBFFileOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileOperation.Entity);
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
        //Update  FFileOperation
        public BaseResultBool QMS_UDTO_UpdateFFileOperation(FFileOperation entity)
        {
            IBFFileOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFileOperation
        public BaseResultBool QMS_UDTO_UpdateFFileOperationByField(FFileOperation entity, string fields)
        {
            IBFFileOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileOperation.Edit();
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
        //Delele  FFileOperation
        public BaseResultBool QMS_UDTO_DelFFileOperation(long longFFileOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileOperation.Remove(longFFileOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileOperation(FFileOperation entity)
        {
            IBFFileOperation.Entity = entity;
            EntityList<FFileOperation> tempEntityList = new EntityList<FFileOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileOperation.Search();
                tempEntityList.count = IBFFileOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileOperation>(tempEntityList);
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
        //查询文档操作记录表ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileOperation> tempEntityList = new EntityList<FFileOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileOperation>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileOperation>(tempEntity);
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
        #endregion

        #region FFileReadingLog
        //Add  FFileReadingLog
        public BaseResultDataValue QMS_UDTO_AddFFileReadingLog(FFileReadingLog entity)
        {
            IBFFileReadingLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFileReadingLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileReadingLog.Get(IBFFileReadingLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileReadingLog.Entity);
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
        //Update  FFileReadingLog
        public BaseResultBool QMS_UDTO_UpdateFFileReadingLog(FFileReadingLog entity)
        {
            IBFFileReadingLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileReadingLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFileReadingLog
        public BaseResultBool QMS_UDTO_UpdateFFileReadingLogByField(FFileReadingLog entity, string fields)
        {
            IBFFileReadingLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileReadingLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileReadingLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileReadingLog.Edit();
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
        //Delele  FFileReadingLog
        public BaseResultBool QMS_UDTO_DelFFileReadingLog(long longFFileReadingLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileReadingLog.Remove(longFFileReadingLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileReadingLog(FFileReadingLog entity)
        {
            IBFFileReadingLog.Entity = entity;
            EntityList<FFileReadingLog> tempEntityList = new EntityList<FFileReadingLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileReadingLog.Search();
                tempEntityList.count = IBFFileReadingLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileReadingLog>(tempEntityList);
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
        //查询文档阅读记录表ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileReadingLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileReadingLog> tempEntityList = new EntityList<FFileReadingLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileReadingLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileReadingLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileReadingLog>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileReadingLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileReadingLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileReadingLog>(tempEntity);
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
        #endregion

        #region FFileReadingUser
        //Add  FFileReadingUser
        public BaseResultDataValue QMS_UDTO_AddFFileReadingUser(FFileReadingUser entity)
        {
            IBFFileReadingUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBFFileReadingUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBFFileReadingUser.Get(IBFFileReadingUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileReadingUser.Entity);
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
        //Update  FFileReadingUser
        public BaseResultBool QMS_UDTO_UpdateFFileReadingUser(FFileReadingUser entity)
        {
            IBFFileReadingUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileReadingUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  FFileReadingUser
        public BaseResultBool QMS_UDTO_UpdateFFileReadingUserByField(FFileReadingUser entity, string fields)
        {
            IBFFileReadingUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBFFileReadingUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBFFileReadingUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBFFileReadingUser.Edit();
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
        //Delele  FFileReadingUser
        public BaseResultBool QMS_UDTO_DelFFileReadingUser(long longFFileReadingUserID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBFFileReadingUser.Remove(longFFileReadingUserID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue QMS_UDTO_SearchFFileReadingUser(FFileReadingUser entity)
        {
            IBFFileReadingUser.Entity = entity;
            EntityList<FFileReadingUser> tempEntityList = new EntityList<FFileReadingUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBFFileReadingUser.Search();
                tempEntityList.count = IBFFileReadingUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileReadingUser>(tempEntityList);
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
        //查询文档阅读对象表ByHQL
        public BaseResultDataValue QMS_UDTO_SearchFFileReadingUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<FFileReadingUser> tempEntityList = new EntityList<FFileReadingUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBFFileReadingUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBFFileReadingUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFileReadingUser>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchFFileReadingUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBFFileReadingUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<FFileReadingUser>(tempEntity);
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
        #endregion

        /// <summary>
        /// 下载QMS的文档附件表的文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream QMS_UDTO_FFileAttachmentDownLoadFiles(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                FFileAttachment file = IBFFileAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "附件不存在!请重新上传或联系管理员。";
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
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
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;// "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("IE或其他浏览器直接打开文件");
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
                ZhiFang.Common.Log.Log.Error("文档附件下载错误:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        /// <summary>
        /// 依文档Id及查询类型获取文档的抄送对象信息或阅读对象信息
        /// </summary>
        /// <param name="ffileId">文档Id</param>
        /// <param name="searchType">查询类型</param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SearchFFileCopyUserOrReadingUserByFFileId(long ffileId, string searchType)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            JObject jobj = new JObject();
            try
            {
                tempBaseResultDataValue = IBFFile.SearchFFileCopyUserOrReadingUserByFFileId(ffileId, searchType);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            // }
            return tempBaseResultDataValue;
        }

        #region 生成帮助文档处理
        /// <summary>
        /// 生成帮助文档处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_SaveHelpHtmlAndJson(long id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string basePath = "";
            //一级保存路径
            //basePath = (string)IBBParameter.GetCache(BParameterParaNo.SaveHelpHtmlAndJson.ToString());

            //ZhiFang.Common.Log.Log.Debug("AppDomainAppPath:" + HttpRuntime.AppDomainAppPath.ToString());            
            //修改为只支持程序根目录下的相对路径help文件夹下
            basePath = HttpRuntime.AppDomainAppPath.ToString() + "\\help";

            try
            {
                FFile entity = IBFFile.Get(id);
                if (entity == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取帮助文档信息为空!";
                    return tempBaseResultDataValue;
                }
                if (string.IsNullOrEmpty(entity.No))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取文档编码信息为空!";
                    return tempBaseResultDataValue;
                }
                //详细内容的src及href的替换
                string oaTestSrc = "src=\"http://" + HttpContext.Current.Request.Url.Host + "/" + HttpContext.Current.Request.ApplicationPath;
                string oaTestHref = "href=\"http://" + HttpContext.Current.Request.Url.Host + "/" + HttpContext.Current.Request.ApplicationPath;
                string oaSrc = "src=\"http://" + HttpContext.Current.Request.Url.Host + "/" + HttpContext.Current.Request.ApplicationPath;
                string oaHref = "href=\"http://" + HttpContext.Current.Request.Url.Host + "/" + HttpContext.Current.Request.ApplicationPath;

                if (entity.Content.Contains(oaTestSrc))
                    entity.Content = entity.Content.Replace(oaTestSrc, "src=\"..");
                if (entity.Content.Contains(oaTestHref))
                    entity.Content = entity.Content.Replace(oaTestHref, "href=\"..");

                if (entity.Content.Contains(oaSrc))
                    entity.Content = entity.Content.Replace(oaSrc, "src=\"..");
                if (entity.Content.Contains(oaHref))
                    entity.Content = entity.Content.Replace(oaHref, "href=\"..");

                //添加分类子文件夹+ subPath +"\\"+
                string subPath = entity.BDictTree.CName;
                if (string.IsNullOrEmpty(subPath)) subPath = "其他";

                string jsonFilePath = basePath + "\\jsons\\" + entity.No;
                if (!Directory.Exists(jsonFilePath))
                    Directory.CreateDirectory(jsonFilePath);

                JObject jresult = new JObject();
                jresult.Add("ErrorInfo", true);
                jresult.Add("success", true);
                jresult.Add("ResultDataFormatType", "JSON");
                jresult.Add("ResultDataValue", "");
                string fields = "FFile_Content,FFile_Id,FFile_Title,FFile_CreatorName";
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);

                EntityList<FFile> tempEntityList = new EntityList<FFile>();
                IList<FFile> list = new List<FFile>();
                tempEntityList.list = list;
                tempEntityList.list.Add(entity);
                tempEntityList.count = 1;
                string fileResultDataValue = tempParseObjectProperty.GetObjectListPlanish<FFile>(tempEntityList);

                #region JsonFile
                //string filepath = Path.Combine(jsonFilePath, "FFile.json");               
                //FileStream fsjson = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //StreamWriter swjson = new StreamWriter(fsjson, Encoding.UTF8);
                //jresult["ResultDataValue"] = fileResultDataValue;
                //swjson.WriteLine(jresult.ToString()); // 写入
                //swjson.Close();
                //fsjson.Close();
                #endregion

                #region 附件处理
                //JObject attachmentObj = new JObject();
                //attachmentObj.Add("ErrorInfo", true);
                //attachmentObj.Add("success", true);
                //attachmentObj.Add("ResultDataFormatType", "JSON");
                //attachmentObj.Add("ResultDataValue", "");
                //string where = "ffileattachment.IsUse=1 and ffileattachment.FFile.Id=" + entity.Id;
                //EntityList<FFileAttachment> ffileAttachmentList = IBFFileAttachment.SearchListByHQL(where, "", 1, 100);
                //string attachmentResultDataValue = "";
                //if (ffileAttachmentList.count > 0)
                //{
                //    //FFileAttachment_FFile_Id
                //    string fieldsAttachment = "FFileAttachment_Id, FFileAttachment_FileName, FFileAttachment_FileExt,FFileAttachment_NewFileName,FFileAttachment_FileType,FFileAttachment_CreatorName,FFileAttachment_FileSize,FFileAttachment_CreatorName, FFileAttachment_DataAddTime";
                //    ParseObjectProperty ffileAttachmentProperty = new ParseObjectProperty(fieldsAttachment);
                //    attachmentResultDataValue = ffileAttachmentProperty.GetObjectPropertyNoPlanish(ffileAttachmentList, fieldsAttachment);
                //    attachmentObj["ResultDataValue"] = attachmentResultDataValue;
                //}
                //string attachmentPath = Path.Combine(jsonFilePath, "FFileAttachment.json");
                //FileStream attachmentFS = new FileStream(attachmentPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //StreamWriter attachmentSW = new StreamWriter(attachmentFS, Encoding.UTF8);
                //attachmentSW.WriteLine(attachmentObj.ToString());
                //attachmentSW.Close();
                //attachmentFS.Close();
                #endregion

                string HtmlFilePath = basePath + "\\html\\" + entity.No;
                if (!Directory.Exists(HtmlFilePath))
                    Directory.CreateDirectory(HtmlFilePath);

                string AttachmentFilePath = basePath + "\\html\\" + entity.No + "\\Files";
                if (!Directory.Exists(AttachmentFilePath))
                    Directory.CreateDirectory(AttachmentFilePath);

                #region HtmlFile
                string Htmlfilepathall = Path.Combine(HtmlFilePath, "index.html");
                if (File.Exists(Htmlfilepathall))
                {
                    File.Delete(Htmlfilepathall);
                }
                FileStream fshtml = new FileStream(Htmlfilepathall, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter swhtml = new StreamWriter(fshtml, Encoding.UTF8);
                string html = "";
                html = entity.Content;
                html = "<html>" + html + "</html>";
                List<string> srclist = new List<string>();
                string tmphtml = html;
                while (tmphtml.IndexOf("src=\"../") >= 0)
                {
                    string tmp = tmphtml.Substring(tmphtml.IndexOf("src=\"../") + 8);
                    tmp = tmp.Substring(0, tmp.IndexOf("\""));
                    tmphtml = tmphtml.Replace("src=\"../" + tmp + "\"", "");

                    if (tmp.Trim() != "")
                    {
                        srclist.Add("../" + tmp);
                    }
                }

                //System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
                //xd.LoadXml(html);
                //xd.ChildNodes

                // srclist = GetSrcList(xd);
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_SaveHelpHtmlAndJson.html:" + html);
                foreach (var src in srclist)
                {
                    //ZhiFang.Common.Log.Log.Error("QMS_UDTO_SaveHelpHtmlAndJson.Files:" + AppDomain.CurrentDomain.BaseDirectory + src.Replace("../", ""));
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + src.Replace("../", "")))
                    {
                        File.Copy(AppDomain.CurrentDomain.BaseDirectory + src.Replace("../", ""), AttachmentFilePath + "\\" + src.Substring(src.LastIndexOf("/")), true);
                    }
                    html = html.Replace(src, "Files" + src.Substring(src.LastIndexOf("/")));
                    //ZhiFang.Common.Log.Log.Error("QMS_UDTO_SaveHelpHtmlAndJson.html:" + html);
                }

                swhtml.WriteLine(html); // 写入
                swhtml.Close();
                fshtml.Close();
                #endregion              

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "生成帮助文档错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_SaveHelpHtmlAndJson.错误:" + tempBaseResultDataValue.ErrorInfo);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 获取到生成的帮助文档
        /// </summary>
        /// <param name="no"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue QMS_UDTO_GetHelpHtmlAndJson(string no, string fileName)
        {
            ZhiFang.Common.Log.Log.Debug("no:" + no + "@fileName:" + fileName);
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //一级保存路径
                string basePath = "";// (string)IBBParameter.GetCache(BParameterParaNo.SaveHelpHtmlAndJson.ToString());
                //修改为只支持程序根目录下的相对路径help文件夹下
                basePath = HttpRuntime.AppDomainAppPath.ToString() + "\\help";
                string info = "该功能无帮助文档!";
                if (String.IsNullOrEmpty(no))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "帮助文档编号为空!";
                    return tempBaseResultDataValue;
                }

                if (fileName.ToLower() == "ffileattachment.json")
                {
                    info = "无文档附件!";
                }
                string jsonFilePath = basePath + "\\jsons\\" + no;
                if (!Directory.Exists(jsonFilePath))
                    Directory.CreateDirectory(jsonFilePath);
                string filePath = jsonFilePath + "\\" + fileName;
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var items = JObject.Parse(json);
                    string resultDataValue = "";
                    if (items != null)
                    {
                        resultDataValue = items["ResultDataValue"].Value<String>();
                        if (!String.IsNullOrEmpty(resultDataValue))
                        {
                            tempBaseResultDataValue.ResultDataValue = resultDataValue;
                        }
                        else
                        {
                            tempBaseResultDataValue.success = false;
                            tempBaseResultDataValue.ErrorInfo = "读取文件内容信息为空!";
                        }
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "读取文件内容信息出错!";
                    }
                    //ZhiFang.Common.Log.Log.Debug("ResultDataValue:" + resultDataValue);
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = info;
                }
            }
            catch (Exception e)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + e.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_GetHelpHtmlAndJson.查询错误:" + e.ToString());
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 新闻缩略图上传及下载
        /// <summary>
        /// 上传新闻缩略图服务
        /// </summary>
        /// <returns></returns>
        public Message QMS_UDTO_UploadNewsThumbnails()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            long id = 0;
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "";
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                    string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                    if (allkeys.Contains("FFile_Id") && !String.IsNullOrEmpty(HttpContext.Current.Request.Form["FFile_Id"].Trim()))
                    {
                        long.TryParse(HttpContext.Current.Request.Form["FFile_Id"].Trim(), out id);
                    }
                    else
                    {
                        brdv.ErrorInfo = "未获取新闻缩略图上传的Id值!";
                        brdv.ResultDataValue = "";
                        brdv.success = false;
                    }
                    if (file == null)
                    {
                        brdv.ErrorInfo = "未检测到文件!";
                        brdv.ResultDataValue = "";
                        brdv.success = false;
                    }
                    if (id == 0)
                    {
                        brdv.ErrorInfo = "未获取新闻缩略图上传的Id值!";
                        brdv.ResultDataValue = "";
                        brdv.success = false;
                    }
                    if (brdv.success)
                    {
                        IBFFile.Entity = IBFFile.Get(id);
                        brdv = IBFFile.UploadNewsThumbnails(file, "update");

                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("新闻缩略图上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "";
                brdv.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载新闻缩略图文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream QMS_UDTO_DownLoadNewsThumbnailsById(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                fileStream = IBFFile.DownLoadNewsThumbnailsById(id);
                if (!string.IsNullOrEmpty(filePath))
                {
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = id + ".png";
                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "image/png";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/png";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }

                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("下载新闻缩略图文件错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }

        #endregion

        #region 获取程序内部字典
        public BaseResultDataValue GetEnumDic(string enumname)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetEnumDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetEnumDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue GetClassDic(string classname, string classnamespace)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string entitynamespace = "ZhiFang.Entity.Base";
            if (classname == null || classname.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classname为空！";
                return tempBaseResultDataValue;
            }
            if (classnamespace == null || classnamespace.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classnamespace为空！";
                return tempBaseResultDataValue;
            }
            try
            {
                entitynamespace = classnamespace;
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
                if (t == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：未找到类字典：" + classname + ",命名空间：" + classnamespace + "！";
                    return tempBaseResultDataValue;
                }
                string jsonstring = "";
                var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in t.GetFields())
                {
                    JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                    jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                }
                jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonstring + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (jsonpara.Length <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：参数为空！";
                    ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：参数为空");
                }
                string jsonresult = "";
                foreach (ClassDicSearchPara cdsp in jsonpara)
                {
                    if (cdsp.classname == null || cdsp.classname.Trim() == "" || cdsp.classnamespace == null || cdsp.classnamespace.Trim() == "")
                    {
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":},";
                    }
                    else
                    {
                        string entitynamespace = "";
                        entitynamespace = cdsp.classnamespace;
                        Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + cdsp.classname);
                        if (t == null)
                        {
                            ZhiFang.Common.Log.Log.Error("GetClassDicList错误信息：未找到类字典：" + cdsp.classname + ",命名空间：" + cdsp.classnamespace + "！");
                            jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[]},";
                            continue;
                        }
                        string jsonstring = "";
                        var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                        foreach (FieldInfo field in t.GetFields())
                        {
                            JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                            jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                            //jsonstring += ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)) + ",";
                        }
                        jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[" + jsonstring + "]},";
                    }
                }
                jsonresult = jsonresult.Substring(0, jsonresult.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonresult + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        #endregion
    }
}
