using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.ProjectProgressMonitorManage.ServerContract;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.ProjectProgressMonitorManage.Common;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Globalization;
using ZhiFang.ProjectProgressMonitorManage.BusinessObject;
using System.Diagnostics;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SystemCommonService : ISystemCommonService
    {
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBSCAttachment IBSCAttachment { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBSCInteraction IBSCInteraction { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBSCOperation IBSCOperation { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBBParameter IBBParameter { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACModuleOper IBRBACModuleOper { get; set; }
        #region SCAttachment
        /// <summary>
        /// 上传公共附件
        /// </summary>
        /// <returns></returns>
        public Message SC_UploadAddSCAttachment()
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
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
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

                    switch (objectName)
                    {
                        case "SCAttachment"://公共附件
                            SCAttachment entity = new SCAttachment();
                            entity.BusinessModuleCode = businessModuleCode;
                            entity.FileName = fileName;
                            entity.FileSize = len;// / 1024;
                            entity.FilePath = tempPath;
                            entity.IsUse = true;
                            entity.NewFileName = newFileName;
                            entity.FileExt = fileExt;
                            entity.FileType = contentType;
                            brdv = IBSCAttachment.AddSCAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity);
                            if (brdv.success)
                                brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                            //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCAttachment.Entity);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("公共附件上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载公共附件的文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream SC_UDTO_DownLoadSCAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                SCAttachment file = IBSCAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
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
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
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
                ZhiFang.Common.Log.Log.Error("公共附件下载错误信息:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }

        }

        //Add  SCAttachment
        public BaseResultDataValue SC_UDTO_AddSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCAttachment.Get(IBSCAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCAttachment.Entity);
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
        //Update  SCAttachment
        public BaseResultBool SC_UDTO_UpdateSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCAttachment
        public BaseResultBool SC_UDTO_UpdateSCAttachmentByField(SCAttachment entity, string fields)
        {
            IBSCAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCAttachment.Edit();
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
        //Delele  SCAttachment
        public BaseResultBool SC_UDTO_DelSCAttachment(long longSCAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCAttachment.Remove(longSCAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCAttachment(SCAttachment entity)
        {
            IBSCAttachment.Entity = entity;
            EntityList<SCAttachment> tempEntityList = new EntityList<SCAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCAttachment.Search();
                tempEntityList.count = IBSCAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCAttachment>(tempEntityList);
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
        //查询公共附件表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCAttachment> tempEntityList = new EntityList<SCAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCAttachment>(tempEntityList);
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

        public BaseResultDataValue SC_UDTO_SearchSCAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCAttachment>(tempEntity);
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

        #region SCInteraction

        /// <summary>
        /// 新增交流内容服务扩展(支持新增话题或新增交流内容)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultDataValue SC_UDTO_AddSCInteractionExtend(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBSCInteraction.AddSCInteractionExtend();
                if (tempBaseResultDataValue.success)
                {
                    IBSCInteraction.Get(IBSCInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCInteraction.Entity);
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
        /// <summary>
        /// 依某一业务对象ID获取该业务对象ID下的所有交流内容信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="bobjectID"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue SC_UDTO_SearchAllSCInteractionByBobjectID(int page, int limit, string fields, string bobjectID, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCInteraction.SearchAllSCInteractionByBobjectID(bobjectID, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCInteraction.SearchAllSCInteractionByBobjectID(bobjectID, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCInteraction>(tempEntityList);
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
        //Add  SCInteraction
        public BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCInteraction.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCInteraction.Get(IBSCInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCInteraction.Entity);
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
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCInteraction.Edit();
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
        //Delele  SCInteraction
        public BaseResultBool SC_UDTO_DelSCInteraction(long longSCInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCInteraction.Remove(longSCInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCInteraction.Search();
                tempEntityList.count = IBSCInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCInteraction>(tempEntityList);
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
        //查询公共交流表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCInteraction>(tempEntityList);
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

        public BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCInteraction>(tempEntity);
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

        #region SCOperation
        //Add  SCOperation
        public BaseResultDataValue SC_UDTO_AddSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSCOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSCOperation.Get(IBSCOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSCOperation.Entity);
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
        //Update  SCOperation
        public BaseResultBool SC_UDTO_UpdateSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SCOperation
        public BaseResultBool SC_UDTO_UpdateSCOperationByField(SCOperation entity, string fields)
        {
            IBSCOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSCOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSCOperation.Edit();
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
        //Delele  SCOperation
        public BaseResultBool SC_UDTO_DelSCOperation(long longSCOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSCOperation.Remove(longSCOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue SC_UDTO_SearchSCOperation(SCOperation entity)
        {
            IBSCOperation.Entity = entity;
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSCOperation.Search();
                tempEntityList.count = IBSCOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
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
        //查询公共操作记录表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SCOperation> tempEntityList = new EntityList<SCOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSCOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SCOperation>(tempEntityList);
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

        public BaseResultDataValue SC_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSCOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SCOperation>(tempEntity);
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

        #region 电子签名图片处理
        /// <summary>
        /// 上传电子签名图片
        /// </summary>
        /// <returns></returns>
        public Message SC_UDTO_UploadEmpSignByEmpId()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            brdv.ResultDataValue = "{id:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到电子签名图片信息！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //员工Id
                string empId = HttpContext.Current.Request.Form["EmpId"];
                if (brdv.success && String.IsNullOrEmpty(empId))
                {
                    brdv.ErrorInfo = "员工Id值为空！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                if (brdv.success && fileExt.ToLower() != ".png")
                {
                    brdv.ErrorInfo = "上传的图片格式需是png！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }

                string fileName = empId + ".png";

                if (brdv.success && file != null && !string.IsNullOrEmpty(fileName))
                {
                    //上传电子签名保存路径
                    string parentPath = parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());

                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);

                    string filepath = Path.Combine(parentPath, fileName);
                    file.SaveAs(filepath);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("上传电子签名图片错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "{id:''}";
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载电子签名图片
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream PGM_UDTO_DownLoadEmpSignByEmpId(long empId, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                //上传电子签名保存路径
                string filePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadEmpSignPath.ToString());
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = empId + ".png";

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
                ZhiFang.Common.Log.Log.Error("电子签名图片下载错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }

        #endregion

        #region 测试用--模块操作
        public BaseResultDataValue SC_UDTO_GetCacheModuleOperByModuleIdAndUrl(long moduleId, string url)
        {
            System.Diagnostics.Process lastProcess = System.Diagnostics.Process.GetCurrentProcess();

            SYSDataRowRoleCacheBase.ModuleOperCacheSize = SYSDataRowRoleCacheBase.GetSizeStr(lastProcess.PrivateMemorySize64);
            ZhiFang.Common.Log.Log.Debug("缓存前的IIS进程:" + lastProcess.ProcessName + ",内存大小:" + SYSDataRowRoleCacheBase.ModuleOperCacheSize);
            

            BaseResultDataValue tempResult = new BaseResultDataValue();
            IList<SYSCacheModuleOper> list = new List<SYSCacheModuleOper>();
            

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

            if (SYSDataRowRoleCacheBase.IsRefreshModuleOperCache == true)
            {
                list = IBRBACModuleOper.GetModuleOperCacheList();
                if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(SYSDataRowRoleCacheBase.ModuleOperCacheKey))
                {
                    BParameterCache.ApplicationCache.Application.Set(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
                else
                {
                    BParameterCache.ApplicationCache.Application.Add(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
            }
            else
            {
                if (BParameterCache.ApplicationCache != null)
                {
                    list = (List<SYSCacheModuleOper>)BParameterCache.ApplicationCache.Application.Get(SYSDataRowRoleCacheBase.ModuleOperCacheKey);
                }
            }

            if (moduleId > 0 && !String.IsNullOrEmpty(url))
            {
                var tempList = new List<SYSCacheModuleOper>();
                if (list != null && list.Count() > 0)
                {
                    tempList = list.Where(s => s.ModuleId == moduleId && s.ServiceURLEName == @"" + url + "").ToList();
                }
                watch.Stop();
                if (tempList != null && tempList.Count() > 0)
                {
                    System.Diagnostics.Process curprocess = System.Diagnostics.Process.GetCurrentProcess();
                    string sizeStr = SYSDataRowRoleCacheBase.GetSizeStr(curprocess.PrivateMemorySize64);
                    tempResult.ResultDataValue = "{MemorySize:" + "\"" + SYSDataRowRoleCacheBase.ModuleOperCacheSize + "\"" + ",CurMemorySize:" + "\"" + sizeStr + "\"" + ",ModuleId:" + "\"" + tempList.ElementAt(0).ModuleId + "\"," + "Seconds:" + "\"" + watch.ElapsedMilliseconds.ToString() + "毫秒\"" + "}";
                }
            }
            return tempResult;
        }
        /// <summary>
        /// 测试用
        /// </summary>
        private void GetTestModuleOperCacheList()
        {
            if (SYSDataRowRoleCacheBase.IsRefreshModuleOperCache == true)
            {
                System.Diagnostics.Process curprocess = System.Diagnostics.Process.GetCurrentProcess(); SYSDataRowRoleCacheBase.ModuleOperCacheSize = SYSDataRowRoleCacheBase.GetSizeStr(curprocess.PrivateMemorySize64);
                ZhiFang.Common.Log.Log.Debug("1.测试模块操作花的进程名称:" + curprocess.ProcessName + ",内存大小:" + SYSDataRowRoleCacheBase.ModuleOperCacheSize);
                List<SYSCacheModuleOper> list = new List<SYSCacheModuleOper>();
                double objSize = 0;
                for (int i = 1; i <= 5000; i++)
                {
                    SYSCacheModuleOper model = new SYSCacheModuleOper();
                    model.ModuleId = i;
                    model.ModuleOperId = i;
                    model.RowFilterBaseCName = "SYSCacheModuleOper";
                    model.ServiceURLEName = "/SystemCommonService.svc/SC_UDTO_GetTestCacheModuleOperList";
                    objSize += System.Runtime.InteropServices.Marshal.SizeOf(model);
                    list.Add(model);
                }
                curprocess = System.Diagnostics.Process.GetCurrentProcess();
                string sizeStr = SYSDataRowRoleCacheBase.GetSizeStr(curprocess.PrivateMemorySize64);
                ZhiFang.Common.Log.Log.Debug("2.测试模块操作花的进程名称:" + curprocess.ProcessName + ",内存大小:" + sizeStr);

                if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(SYSDataRowRoleCacheBase.ModuleOperCacheKey))
                {
                    BParameterCache.ApplicationCache.Application.Set(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
                else
                {
                    BParameterCache.ApplicationCache.Application.Add(SYSDataRowRoleCacheBase.ModuleOperCacheKey, list);
                }
            }
        }
        #endregion
    }
}
