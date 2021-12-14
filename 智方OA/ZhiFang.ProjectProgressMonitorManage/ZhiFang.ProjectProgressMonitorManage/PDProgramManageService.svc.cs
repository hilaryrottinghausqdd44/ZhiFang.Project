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
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.ProjectProgressMonitorManage.BusinessObject;
using Spring.Context;
using Spring.Context.Support;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PDProgramManageService : IPDProgramManageService
    {
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPGMProgram IBPGMProgram { get; set; }
        public IBBParameter IBBParameter { get; set; }
        #region PGMProgram
        //Add  PGMProgram
        public BaseResultDataValue PGM_UDTO_AddPGMProgram(PGMProgram entity)
        {
            IBPGMProgram.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPGMProgram.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPGMProgram.Get(IBPGMProgram.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPGMProgram.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("新增程序信息出错:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PGMProgram
        public BaseResultBool PGM_UDTO_UpdatePGMProgram(PGMProgram entity)
        {
            IBPGMProgram.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPGMProgram.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PGMProgram
        public BaseResultBool PGM_UDTO_UpdatePGMProgramByField(PGMProgram entity, string fields)
        {
            IBPGMProgram.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPGMProgram.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPGMProgram.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPGMProgram.Edit();
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
        public Stream PGM_UDTO_AddPGMProgramByFormData()
        {
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
            PGMProgram entity = null;
            string fFileEntity = ""; //原服务FFile实体参数的json串 不包括文档内容 FFile entity
            string fFileContent = "";  //文档内容fFileCopyUserList
            int fFileOperationType = -1; //原服务参数 int fFileOperationType

            string ffileOperationMemo = ""; //原服务参数 string ffileOperationMemo
            string programType = "";//程序类型
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;

            int iTotal = HttpContext.Current.Request.Files.Count;

            HttpPostedFile file = null;
            if (iTotal != 0)
            {
                file = HttpContext.Current.Request.Files[0];
            }

            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "programType":
                        if (HttpContext.Current.Request.Form["programType"].Trim() != "")
                            programType = HttpContext.Current.Request.Form["programType"].Trim();
                        break;
                    case "fFileEntity":
                        if (HttpContext.Current.Request.Form["fFileEntity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["fFileEntity"].Trim();
                        break;
                    case "fFileContent":
                        if (HttpContext.Current.Request.Form["fFileContent"].Trim() != "")
                            fFileContent = HttpContext.Current.Request.Form["fFileContent"].Trim();
                        break;
                    case "fFileOperationType":
                        if (HttpContext.Current.Request.Form["fFileOperationType"].Trim() != "")
                            fFileOperationType = int.Parse(HttpContext.Current.Request.Form["fFileOperationType"].Trim());
                        break;
                    case "ffileOperationMemo":
                        if (HttpContext.Current.Request.Form["ffileOperationMemo"].Trim() != "")
                            ffileOperationMemo = HttpContext.Current.Request.Form["ffileOperationMemo"].Trim();
                        break;
                }
            }
            if (!string.IsNullOrEmpty(fFileEntity))//PGMProgram实体参数的json串,序列化实体
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PGMProgram>(fFileEntity);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("新增程序信息序列化出错:" + ex.Message);
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            if (entity != null)
            {
                entity.Content = fFileContent;
                IBPGMProgram.Entity = entity;
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
                        tempBaseResultBool = IBPGMProgram.AddPGMProgramByFormData(entity, file, fFileOperationType, ffileOperationMemo, programType);
                        if (tempBaseResultBool.success)
                        {
                            IBPGMProgram.Get(IBPGMProgram.Entity.Id);
                            tempBaseResultBool.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPGMProgram.Entity);
                        }
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Error("新增程序信息出错:" + ex.Message);
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            //return tempBaseResultBool;
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        /// <summary>
        /// FormData方式更新程序信息
        /// </summary>
        /// <returns></returns>
        public Stream PGM_UDTO_UpdatePGMProgramByFieldAndFormData()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            PGMProgram entity = null;
            string fields = "";
            int fFileOperationType = 0;
            string ffileOperationMemo = "";
            string fFileEntity = ""; //原服务FFile实体参数的json串 不包括文档内容 FFile entity
            string fFileContent = "";  //文档内容
            string programType = "";//程序类型
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            int iTotal = HttpContext.Current.Request.Files.Count;

            HttpPostedFile file = null;
            if (iTotal != 0)
            {
                file = HttpContext.Current.Request.Files[0];
            }
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "programType":
                        if (HttpContext.Current.Request.Form["programType"].Trim() != "")
                            programType = HttpContext.Current.Request.Form["programType"].Trim();
                        break;
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
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PGMProgram>(fFileEntity);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Debug("程序信息序列化出错:" + ex.Message);
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    // throw new Exception(ex.Message);
                }
            IBPGMProgram.Entity = entity;
            if (entity != null)
            {
                entity.Content = fFileContent;
                IBPGMProgram.Entity = entity;
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
                }
                else
                {
                    IBPGMProgram.Entity = entity;
                    BaseResultDataValue tempResultDataValue = IBPGMProgram.UploadAttachment(file, programType);
                    if (!tempResultDataValue.success)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = tempResultDataValue.ErrorInfo;
                    }
                    else
                    {
                        try
                        {
                            if ((fields != null) && (fields.Length > 0))
                            {
                                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPGMProgram.Entity, fields);
                                if (tempArray != null)
                                {
                                    tempBaseResultBool = IBPGMProgram.UpdatePGMProgramByFieldAndFormData(tempArray, entity, file, fFileOperationType, ffileOperationMemo);
                                }
                            }
                            else
                            {
                                tempBaseResultBool.success = false;
                                tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                                //tempBaseResultBool.success = IBPGMProgram.Edit();
                            }
                        }
                        catch (Exception ex)
                        {
                            ZhiFang.Common.Log.Log.Debug("程序处理附件信息出错:" + ex.Message);
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：entity参数不能为空！";
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        /// <summary>
        /// 查询某一类型树的程序数据信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue PGM_UDTO_SearchPGMProgramByBDictTreeId(string where, bool isSearchChildNode, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PGMProgram> tempEntityList = new EntityList<PGMProgram>();
            try
            {
                string maxLevelStr = "";
                tempEntityList = IBPGMProgram.SearchPGMProgramByBDictTreeId(where, isSearchChildNode, page, limit, CommonServiceMethod.GetSortHQL(sort), maxLevelStr);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGMProgram>(tempEntityList);
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
        //Delele  PGMProgram
        public BaseResultBool PGM_UDTO_DelPGMProgram(long longPGMProgramID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPGMProgram.Remove(longPGMProgramID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue PGM_UDTO_SearchPGMProgram(PGMProgram entity)
        {
            IBPGMProgram.Entity = entity;
            EntityList<PGMProgram> tempEntityList = new EntityList<PGMProgram>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPGMProgram.Search();
                tempEntityList.count = IBPGMProgram.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGMProgram>(tempEntityList);
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

        //查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。ByHQL
        public BaseResultDataValue PGM_UDTO_SearchPGMProgramByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PGMProgram> tempEntityList = new EntityList<PGMProgram>();
            #region 行数据权限处理
            try
            {
                string rowRoleHQL = "";
                rowRoleHQL = IBPGMProgram.GetDataRowRoleHQL(false);
                if (!string.IsNullOrEmpty(rowRoleHQL))
                {
                    where += (" and " + rowRoleHQL);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.ErrorInfo = "处理权限行数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            ZhiFang.Common.Log.Log.Debug("PGM_UDTO_SearchPGMProgramByHQL--where:" + where+ ",page:"+ page + ",limit:" + limit + ",fields:" + fields + ",sort:" + sort);
            #endregion

            try
            {

                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPGMProgram.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPGMProgram.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGMProgram>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "PGM_UDTO_SearchPGMProgramByHQL.HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue PGM_UDTO_SearchPGMProgramById(long id, string fields, bool isPlanish, bool isUpdateCounts)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPGMProgram.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PGMProgram>(tempEntity);
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
            //更新总阅读数
            if (tempBaseResultDataValue.success && isUpdateCounts == true)
            {
                IBPGMProgram.UpdateCountsById(id);
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 下载程序附件服务
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream PGM_UDTO_DownLoadPGMProgramAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            string errorInfo = "程序附件不存在!请重新上传或联系管理员。";
            try
            {
                string filePath = "";
                PGMProgram file = IBPGMProgram.Get(id);
                string basePath = "";
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                if (file != null && (!string.IsNullOrEmpty(file.FilePath)))
                {
                    filePath = file.FilePath;// + fileName;
                    if (file != null && (!string.IsNullOrEmpty(basePath)) && (!string.IsNullOrEmpty(filePath)))
                        filePath = basePath + filePath;
                }

                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                        return memoryStream;
                    }
                    else
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
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
                ZhiFang.Common.Log.Log.Error("下载程序附件错误:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }

        }
        /// <summary>
        /// 程序的发布操作
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public BaseResultBool PGM_UpdateStatusByStrIds(string strIds, string status)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPGMProgram.UpdateStatusByStrIds(strIds, status);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 程序的禁用或启用操作
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public BaseResultBool PGM_UpdateIsUseByStrIds(string strIds, bool isUse)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPGMProgram.UpdateIsUseByStrIds(strIds, isUse);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region  行数据权限的测试
        /// <summary>
        /// 浏览直接调用该服务的测试
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PGMProgram> tempEntityList = new EntityList<PGMProgram>();
            Cookie.CookieHelper.Write(DicCookieSession.CurModuleID, "5286921890808333282");
            try
            {
                string rowRoleHQL = IBPGMProgram.GetDataRowRoleHQL(false);
                if (!string.IsNullOrEmpty(rowRoleHQL))
                {
                    where += (" and " + rowRoleHQL);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.ErrorInfo = "处理权限行数据错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            ZhiFang.Common.Log.Log.Debug("PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL--HQL:" + where);

            try
            {

                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPGMProgram.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPGMProgram.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PGMProgram>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL.HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
            }
            return tempBaseResultDataValue;
        }
        #endregion

    }
}
