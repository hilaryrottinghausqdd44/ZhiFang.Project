using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using Newtonsoft.Json.Linq;
using ZhiFang.ReagentSys.Client.ServerContract;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using System.ServiceModel.Channels;
using System.Reflection;
using Newtonsoft.Json;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaSysManageService : IReaSysManageService
    {
        IBCenOrg IBCenOrg { get; set; }
        IBSCAttachment IBSCAttachment { get; set; }
        IBSCInteraction IBSCInteraction { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBBParameter IBBParameter { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        IBReaCenOrg IBReaCenOrg { get; set; }

        IBReaGoods IBReaGoods { get; set; }

        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }

        IBReaBmsInDoc IBReaBmsInDoc { get; set; }

        IBReaBmsInDtl IBReaBmsInDtl { get; set; }

        IBReaBmsOutDoc IBReaBmsOutDoc { get; set; }

        IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }

        IBReaBmsTransferDoc IBReaBmsTransferDoc { get; set; }

        IBReaBmsTransferDtl IBReaBmsTransferDtl { get; set; }

        IBReaBmsReqDoc IBReaBmsReqDoc { get; set; }

        IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }

        IBReaDeptGoods IBReaDeptGoods { get; set; }

        IBReaGoodsLot IBReaGoodsLot { get; set; }

        IBReaGoodsRegister IBReaGoodsRegister { get; set; }

        IBReaGoodsUnit IBReaGoodsUnit { get; set; }

        IBReaPlace IBReaPlace { get; set; }
        IBReaStorage IBReaStorage { get; set; }
        IBReaReqOperation IBReaReqOperation { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc { get; set; }
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaBmsCenSaleDocConfirm IBReaBmsCenSaleDocConfirm { get; set; }
        IBReaBmsCenSaleDtlConfirm IBReaBmsCenSaleDtlConfirm { get; set; }
        IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        IBReaChooseGoodsTemplate IBReaChooseGoodsTemplate { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaEquipReagentLink IBReaEquipReagentLink { get; set; }
        IBReaTestEquipLab IBReaTestEquipLab { get; set; }
        IBReaTestEquipProd IBReaTestEquipProd { get; set; }
        IBReaTestEquipType IBReaTestEquipType { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IBReaBmsCheckDoc IBReaBmsCheckDoc { get; set; }
        IBReaBmsCheckDtl IBReaBmsCheckDtl { get; set; }
        IBReaBmsQtyBalanceDoc IBReaBmsQtyBalanceDoc { get; set; }
        IBReaBmsQtyBalanceDtl IBReaBmsQtyBalanceDtl { get; set; }
        IBReaBmsQtyMonthBalanceDoc IBReaBmsQtyMonthBalanceDoc { get; set; }
        IBReaUserStorageLink IBReaUserStorageLink { get; set; }
        IBBReport IBBReport { get; set; }
        IBBTemplate IBBTemplate { get; set; }
        IBCenOrgType IBCenOrgType { get; set; }
        IBReaBusinessInterface IBReaBusinessInterface { get; set; }
        IBReaBusinessInterfaceLink IBReaBusinessInterfaceLink { get; set; }
        IBReaEquipTestItemReaGoodLink IBReaEquipTestItemReaGoodLink { get; set; }
        IBReaTestEquipItem IBReaTestEquipItem { get; set; }
        IBReaTestItem IBReaTestItem { get; set; }
        IBReaAlertInfoSettings IBReaAlertInfoSettings { get; set; }
        IBReaMonthUsageStatisticsDoc IBReaMonthUsageStatisticsDoc { get; set; }
        IBReaMonthUsageStatisticsDtl IBReaMonthUsageStatisticsDtl { get; set; }
        IBReaLisTestStatisticalResults IBReaLisTestStatisticalResults { get; set; }
        IBBUserUIConfig IBBUserUIConfig { get; set; }

        IBReaStorageGoodsLink IBReaStorageGoodsLink { get; set; }

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

        #region CenOrg
        public BaseResultDataValue ST_UDTO_SearchCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrg> entityList = new EntityList<CenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenOrg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenOrg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrg>(entityList);
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


        public BaseResultBool ST_UDTO_UpdateCenOrgByField(CenOrg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenOrg.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBCenOrg.Edit();
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


        public BaseResultDataValue ST_UDTO_SearchCenOrgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenOrg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenOrg>(entity);
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
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNoClass.UploadFilesPath.ToString());
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
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
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
        //Add  SCInteraction
        public BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBSCInteraction.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBSCInteraction.Add();
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool.success = IBSCInteraction.Edit();
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        //Update  SCInteraction
        public BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields)
        {
            IBSCInteraction.Entity = entity;
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSCInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultBool.success = IBSCInteraction.Update(tempArray);

                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //baseResultBool.success = IBSCInteraction.Edit();
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        //Delele  SCInteraction
        public BaseResultBool SC_UDTO_DelSCInteraction(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBSCInteraction.Entity = IBSCInteraction.Get(id);
                if (IBSCInteraction.Entity != null)
                {
                    long labid = IBSCInteraction.Entity.LabID;
                    string entityName = IBSCInteraction.Entity.GetType().Name;
                    baseResultBool.success = IBSCInteraction.RemoveByHQL(id);

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

        public BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity)
        {
            IBSCInteraction.Entity = entity;
            EntityList<SCInteraction> entityList = new EntityList<SCInteraction>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                entityList.list = IBSCInteraction.Search();
                entityList.count = IBSCInteraction.GetTotalCount();
                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCInteraction>(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        //查询公共交流表ByHQL
        public BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<SCInteraction> entityList = new EntityList<SCInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBSCInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBSCInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<SCInteraction>(entityList);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBSCInteraction.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<SCInteraction>(entity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
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

        #region ReaCenOrg
        //Add  ReaCenOrg
        public BaseResultDataValue ST_UDTO_AddReaCenOrg(ReaCenOrg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                if (entity.PlatformOrgNo.HasValue)
                {
                    BaseResultBool baseResultBool = IBReaCenOrg.EditVerification(entity);
                    if (baseResultBool.success == false)
                    {
                        baseResultDataValue.success = baseResultBool.success;
                        baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
                        return baseResultDataValue;
                    }
                }
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaCenOrg.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaCenOrg
        public BaseResultBool ST_UDTO_UpdateReaCenOrg(ReaCenOrg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    baseResultBool = IBReaCenOrg.EditVerification(entity);
                    if (baseResultBool.success == false) return baseResultBool;

                    baseResultBool.success = IBReaCenOrg.Edit();
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
        //Update  ReaCenOrg
        public BaseResultBool ST_UDTO_UpdateReaCenOrgByField(ReaCenOrg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                baseResultBool = IBReaCenOrg.EditVerification(entity);
                if (baseResultBool.success == false) return baseResultBool;

                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaCenOrg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCenOrg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaCenOrg.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaCenOrg.Edit();
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
        //Delele  ReaCenOrg
        public BaseResultBool ST_UDTO_DelReaCenOrg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaCenOrg.Entity = IBReaCenOrg.Get(id);
                if (IBReaCenOrg.Entity != null)
                {
                    long labid = IBReaCenOrg.Entity.LabID;
                    string entityName = IBReaCenOrg.Entity.GetType().Name;
                    baseResultBool.success = IBReaCenOrg.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenOrg(ReaCenOrg entity)
        {
            EntityList<ReaCenOrg> entityList = new EntityList<ReaCenOrg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaCenOrg.Entity = entity;
                try
                {
                    entityList.list = IBReaCenOrg.Search();
                    entityList.count = IBReaCenOrg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenOrg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCenOrg> entityList = new EntityList<ReaCenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaCenOrg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaCenOrg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenOrg>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaCenOrg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaCenOrg>(entity);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgTreeByOrgID(string id, int orgType)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempId = 0;
            try
            {
                if (!((string.IsNullOrEmpty(id.Trim())) || (id.ToLower().Trim() == "root")))
                    tempId = Int64.Parse(id);
                tempBaseResultTree = IBReaCenOrg.SearchReaCenOrgTreeByOrgID(tempId, orgType);
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
        public BaseResultDataValue ST_UDTO_SearchReaCenOrgListTreeByOrgID(string id, string fields, int orgType)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<ReaCenOrg> tempBaseResultTree = new BaseResultTree<ReaCenOrg>();
            long tempId = 0;
            try
            {
                if (!((string.IsNullOrEmpty(id.Trim())) || (id.ToLower().Trim() == "root")))
                    tempId = Int64.Parse(id);
                tempBaseResultTree = IBReaCenOrg.SearchReaCenOrgListTreeByOrgID(tempId, orgType);
                if (tempBaseResultTree.Tree.Count > 0)
                {
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

        public BaseResultDataValue ST_UDTO_SearchReaCenOrgAndChildListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isSearchChild)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCenOrg> entityList = new EntityList<ReaCenOrg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaCenOrg.SearchReaCenOrgAndChildListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit, isSearchChild);
                }
                else
                {
                    entityList = IBReaCenOrg.SearchReaCenOrgAndChildListByHQL(where, "", page, limit, isSearchChild);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenOrg>(entityList);
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

        #region ReaGoods
        //Add  ReaGoods
        public BaseResultDataValue ST_UDTO_AddReaGoods(ReaGoods entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                string labIdstr = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID);
                if (string.IsNullOrEmpty(labIdstr))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "获取机构信息失败，请重新登录！";
                }
                long labID = long.Parse(labIdstr); ;

                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.Id = IBReaGoods.GetMaxGoodsId(labID);

                IBReaGoods.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoods.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaGoods
        public BaseResultBool ST_UDTO_UpdateReaGoods(ReaGoods entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoods.Entity = entity;
                try
                {
                    if (!string.IsNullOrEmpty(entity.GoodsNo))
                    {
                        bool result = IBReaGoods.EditVerificationGoodsNo(entity);
                        if (!result)
                        {
                            string info = "机构货品为:" + entity.CName + ",的货品平台编码已经存在!";
                            baseResultBool.success = false;
                            baseResultBool.ErrorInfo = "错误信息:" + info;
                            return baseResultBool;
                        }
                    }
                    baseResultBool.success = IBReaGoods.Edit();
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
        //Update  ReaGoods
        public BaseResultBool ST_UDTO_UpdateReaGoodsByField(ReaGoods entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoods.Entity = entity;
                try
                {
                    if (!string.IsNullOrEmpty(entity.GoodsNo))
                    {
                        bool result = IBReaGoods.EditVerificationGoodsNo(entity);
                        if (!result)
                        {
                            //string info = "机构货品为:" + entity.CName + ",的货品平台编码已经存在!";
                            string info = "机构货品为:" + entity.CName + ",的货品平台编码:" + entity.GoodsNo + ",已经存在,货品平台编码不允许出现重复!";
                            baseResultBool.success = false;
                            baseResultBool.ErrorInfo = "错误信息:" + info;
                            return baseResultBool;
                        }
                    }
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoods.Entity, fields);
                        if (tempArray != null)
                        {
                            ReaGoods serverReaGoods = IBReaGoods.Get(IBReaGoods.Entity.Id);
                            baseResultBool.success = IBReaGoods.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                string[] arrFields = fields.Split(',');
                                IBReaGoods.AddSCOperation(serverReaGoods, arrFields);
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoods.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoods
        public BaseResultBool ST_UDTO_DelReaGoods(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoods.Entity = IBReaGoods.Get(id);
                if (IBReaGoods.Entity != null)
                {
                    long labid = IBReaGoods.Entity.LabID;
                    string entityName = IBReaGoods.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoods.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoods(ReaGoods entity)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoods.Entity = entity;
                try
                {
                    entityList.list = IBReaGoods.Search();
                    entityList.count = IBReaGoods.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoods.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoods>(entity);
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

        public BaseResultBool ST_UDTO_UpdateGonvertGroupCode(string idList, string Code)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            return baseResultBool;
        }


        #endregion

        #region ReaGoodsOrgLink
        //过滤当前供应商已维护并启用的货品信息
        public BaseResultDataValue ST_UDTO_SearchReaGoodsByCenOrgId(int page, int limit, string fields, string where, string sort, bool isPlanish, long cenOrgId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            try
            {
                string hqlWhere = string.Format("reagoodsorglink.Visible=1 and CenOrg.Id={0}", cenOrgId);
                IList<ReaGoodsOrgLink> tempList = IBReaGoodsOrgLink.SearchListByHQL(hqlWhere);
                StringBuilder strb = new StringBuilder();
                foreach (var item in tempList)
                {
                    strb.Append(item.ReaGoods.Id + ",");
                }
                if (strb.ToString().TrimEnd(',').Length > 0)
                {
                    string notHql = string.Format("reagoods.Id not in({0})", strb.ToString().TrimEnd(','));
                    if (string.IsNullOrEmpty(where))
                        where = notHql;
                    else
                        where = string.Format("{0} and {1}", where, notHql);
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
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
        //Add  ReaGoodsOrgLink
        public BaseResultDataValue ST_UDTO_AddReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    baseResultDataValue = IBReaGoodsOrgLink.AddReaGoodsOrgLink(empID, empName);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_UpdateReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    baseResultBool = IBReaGoodsOrgLink.EditReaGoodsOrgLink(empID, empName);
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
        //Update  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_UpdateReaGoodsOrgLinkByField(ReaGoodsOrgLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsOrgLink.Entity, fields);
                        if (tempArray != null)
                        {
                            long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                            baseResultBool = IBReaGoodsOrgLink.UpdateReaGoodsOrgLink(tempArray, empID, empName);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsOrgLink.Edit();
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
        //Delele  ReaGoodsOrgLink
        public BaseResultBool ST_UDTO_DelReaGoodsOrgLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsOrgLink.Entity = IBReaGoodsOrgLink.Get(id);
                if (IBReaGoodsOrgLink.Entity != null)
                {
                    baseResultBool.success = IBReaGoodsOrgLink.Remove(id);
                    //原维护的操作记录也需要删除
                    if (baseResultBool.success)
                    {
                        IList<SCOperation> tempList = IBSCOperation.SearchListByHQL(string.Format("scoperation.BobjectID={0}", id));
                        if (tempList.Count > 0)
                        {
                            foreach (var item in tempList)
                            {
                                IBSCOperation.Entity = item;
                                IBSCOperation.Remove();
                            }
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLink(ReaGoodsOrgLink entity)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsOrgLink.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsOrgLink.Search();
                    entityList.count = IBReaGoodsOrgLink.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsOrgLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsOrgLink>(entity);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkAndChildListByHQL(long orgId, string where, int page, int limit, string fields, string sort, bool isPlanish, bool isSearchChild, int orgType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                    sort = CommonServiceMethod.GetSortHQL(sort);

                entityList = IBReaGoodsOrgLink.SearchReaGoodsOrgLinkAndChildListByHQL(orgId, where, sort, page, limit, isSearchChild, orgType);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
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

        #region ReaGoodsBarcodeOperation
        public BaseResultDataValue ST_UDTO_SearchReaGoodsBarcodeOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsBarcodeOperation> tempEntityList = new EntityList<ReaGoodsBarcodeOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaGoodsBarcodeOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaGoodsBarcodeOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaGoodsBarcodeOperation>(tempEntityList);
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
        #endregion

        #region ReaBmsInDoc
        //Add  ReaBmsInDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsInDoc(ReaBmsInDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsInDoc.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaBmsInDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsInDoc(ReaBmsInDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsInDoc.Edit();
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
        //Update  ReaBmsInDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsInDocByField(ReaBmsInDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsInDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsInDoc.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsInDoc.Edit();
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
        //Delele  ReaBmsInDoc
        public BaseResultBool ST_UDTO_DelReaBmsInDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsInDoc.Entity = IBReaBmsInDoc.Get(id);
                if (IBReaBmsInDoc.Entity != null)
                {
                    long labid = IBReaBmsInDoc.Entity.LabID;
                    string entityName = IBReaBmsInDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsInDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDoc(ReaBmsInDoc entity)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsInDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsInDoc.Search();
                    entityList.count = IBReaBmsInDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsInDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsInDoc>(entity);
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

        #region ReaBmsInDtl
        //Add  ReaBmsInDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsInDtl(ReaBmsInDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsInDtl.Add();
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
        //Update  ReaBmsInDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsInDtl(ReaBmsInDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsInDtl.Edit();
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
        //Update  ReaBmsInDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsInDtlByField(ReaBmsInDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsInDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsInDtl.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsInDtl.Edit();
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
        //Delele  ReaBmsInDtl
        public BaseResultBool ST_UDTO_DelReaBmsInDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsInDtl.Entity = IBReaBmsInDtl.Get(id);
                if (IBReaBmsInDtl.Entity != null)
                {
                    long labid = IBReaBmsInDtl.Entity.LabID;
                    string entityName = IBReaBmsInDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsInDtl.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtl(ReaBmsInDtl entity)
        {
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsInDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsInDtl.Search();
                    entityList.count = IBReaBmsInDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDtl.SearchListByHQL(where, page, limit);
                }
                                
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsInDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsInDtl>(entity);
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

        #region ReaBmsOutDoc
        //Add  ReaBmsOutDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsOutDoc(ReaBmsOutDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsOutDoc.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaBmsOutDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsOutDoc(ReaBmsOutDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsOutDoc.Edit();
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
        //Update  ReaBmsOutDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsOutDocByField(ReaBmsOutDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!fields.Contains("DataUpdateTime"))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;

                if (entity.Status.ToString() == ReaBmsOutDocStatus.审核通过.Key || entity.Status.ToString() == ReaBmsOutDocStatus.审核退回.Key)
                {
                    entity.ConfirmTime = DateTime.Now;
                    if (!fields.Contains("ConfirmTime")) fields = fields + ",ConfirmTime";
                }
                else if (entity.Status.ToString() == ReaBmsOutDocStatus.审批通过.Key || entity.Status.ToString() == ReaBmsOutDocStatus.审批退回.Key)
                {
                    entity.ApprovalTime = DateTime.Now;
                    if (!fields.Contains("ApprovalTime")) fields = fields + ",ApprovalTime";
                }
                IBReaBmsOutDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsOutDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsOutDoc.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsOutDoc.Edit();
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
        //Delele  ReaBmsOutDoc
        public BaseResultBool ST_UDTO_DelReaBmsOutDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsOutDoc.Entity = IBReaBmsOutDoc.Get(id);
                if (IBReaBmsOutDoc.Entity != null)
                {
                    long labid = IBReaBmsOutDoc.Entity.LabID;
                    string entityName = IBReaBmsOutDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsOutDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDoc(ReaBmsOutDoc entity)
        {
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsOutDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsOutDoc.Search();
                    entityList.count = IBReaBmsOutDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsOutDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsOutDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsOutDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsOutDoc>(entity);
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

        #region ReaBmsOutDtl
        //Add  ReaBmsOutDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsOutDtl(ReaBmsOutDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsOutDtl.Add();
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
        //Update  ReaBmsOutDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsOutDtl(ReaBmsOutDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsOutDtl.Edit();
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
        //Update  ReaBmsOutDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsOutDtlByField(ReaBmsOutDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsOutDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsOutDtl.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsOutDtl.Edit();
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
        //Delele  ReaBmsOutDtl
        public BaseResultBool ST_UDTO_DelReaBmsOutDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsOutDtl.Entity = IBReaBmsOutDtl.Get(id);
                if (IBReaBmsOutDtl.Entity != null)
                {
                    long labid = IBReaBmsOutDtl.Entity.LabID;
                    string entityName = IBReaBmsOutDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsOutDtl.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDtl(ReaBmsOutDtl entity)
        {
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsOutDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsOutDtl.Search();
                    entityList.count = IBReaBmsOutDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsOutDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsOutDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsOutDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsOutDtl>(entity);
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

        #region ReaBmsReqDoc
        //Add  ReaBmsReqDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsReqDoc.Add();
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
        //Update  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsReqDoc.Edit();
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
        //Update  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocByField(ReaBmsReqDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsReqDoc.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsReqDoc.Edit();
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
        //Delele  ReaBmsReqDoc
        public BaseResultBool ST_UDTO_DelReaBmsReqDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsReqDoc.Entity = IBReaBmsReqDoc.Get(id);
                if (IBReaBmsReqDoc.Entity != null)
                {
                    long labid = IBReaBmsReqDoc.Entity.LabID;
                    string entityName = IBReaBmsReqDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsReqDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDoc(ReaBmsReqDoc entity)
        {
            EntityList<ReaBmsReqDoc> entityList = new EntityList<ReaBmsReqDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsReqDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsReqDoc.Search();
                    entityList.count = IBReaBmsReqDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDoc> entityList = new EntityList<ReaBmsReqDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsReqDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsReqDoc>(entity);
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

        #region ReaBmsReqDtl
        //Add  ReaBmsReqDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsReqDtl.Add();
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
        //Update  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsReqDtl.Edit();
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
        //Update  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtlByField(ReaBmsReqDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsReqDtl.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsReqDtl.Edit();
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
        //Delele  ReaBmsReqDtl
        public BaseResultBool ST_UDTO_DelReaBmsReqDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsReqDtl.Entity = IBReaBmsReqDtl.Get(id);
                if (IBReaBmsReqDtl.Entity != null)
                {
                    long labid = IBReaBmsReqDtl.Entity.LabID;
                    string entityName = IBReaBmsReqDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsReqDtl.RemoveByHQL(id);

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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtl(ReaBmsReqDtl entity)
        {
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsReqDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsReqDtl.Search();
                    entityList.count = IBReaBmsReqDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, page, limit);
                }

                //IList<ReaGoods> tempGoodsList = IBReaGoods.SearchListByHQL("Visible=1");
                //foreach (ReaBmsReqDtl dtl in entityList.list)
                //{
                //    var l = tempGoodsList.Where(p => p.Id == dtl.GoodsID).ToList();
                //    if (l.Count > 0)
                //    {
                //        dtl.GoodsSName = l[0].SName;
                //        dtl.GoodsEName = l[0].EName;
                //    }
                //}

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsReqDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsReqDtl>(entity);
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

        #region ReaBmsTransferDoc
        //Add  ReaBmsTransferDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsTransferDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsTransferDoc.Get(IBReaBmsTransferDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsTransferDoc.Entity);
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
        //Update  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDocByField(ReaBmsTransferDoc entity, string fields)
        {
            IBReaBmsTransferDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsTransferDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsTransferDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsTransferDoc.Edit();
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
        //Delele  ReaBmsTransferDoc
        public BaseResultBool ST_UDTO_DelReaBmsTransferDoc(long longReaBmsTransferDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDoc.Remove(longReaBmsTransferDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDoc(ReaBmsTransferDoc entity)
        {
            IBReaBmsTransferDoc.Entity = entity;
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsTransferDoc.Search();
                tempEntityList.count = IBReaBmsTransferDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsTransferDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsTransferDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsTransferDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsTransferDoc>(tempEntity);
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

        #region ReaBmsTransferDtl
        //Add  ReaBmsTransferDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsTransferDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsTransferDtl.Get(IBReaBmsTransferDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsTransferDtl.Entity);
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
        //Update  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsTransferDtlByField(ReaBmsTransferDtl entity, string fields)
        {
            IBReaBmsTransferDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsTransferDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsTransferDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsTransferDtl.Edit();
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
        //Delele  ReaBmsTransferDtl
        public BaseResultBool ST_UDTO_DelReaBmsTransferDtl(long longReaBmsTransferDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsTransferDtl.Remove(longReaBmsTransferDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtl(ReaBmsTransferDtl entity)
        {
            IBReaBmsTransferDtl.Entity = entity;
            EntityList<ReaBmsTransferDtl> tempEntityList = new EntityList<ReaBmsTransferDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsTransferDtl.Search();
                tempEntityList.count = IBReaBmsTransferDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDtl> tempEntityList = new EntityList<ReaBmsTransferDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsTransferDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsTransferDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsTransferDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsTransferDtl>(tempEntity);
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

        #region ReaDeptGoods
        //Add  ReaDeptGoods
        public BaseResultDataValue ST_UDTO_AddReaDeptGoods(ReaDeptGoods entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBReaDeptGoods.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaDeptGoods.Add();
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
        //Update  ReaDeptGoods
        public BaseResultBool ST_UDTO_UpdateReaDeptGoods(ReaDeptGoods entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaDeptGoods.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaDeptGoods.Edit();
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
        //Update  ReaDeptGoods
        public BaseResultBool ST_UDTO_UpdateReaDeptGoodsByField(ReaDeptGoods entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaDeptGoods.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaDeptGoods.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaDeptGoods.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaDeptGoods.Edit();
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
        //Delele  ReaDeptGoods
        public BaseResultBool ST_UDTO_DelReaDeptGoods(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaDeptGoods.Entity = IBReaDeptGoods.Get(id);
                if (IBReaDeptGoods.Entity != null)
                {
                    long labid = IBReaDeptGoods.Entity.LabID;
                    string entityName = IBReaDeptGoods.Entity.GetType().Name;
                    baseResultBool.success = IBReaDeptGoods.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoods(ReaDeptGoods entity)
        {
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaDeptGoods.Entity = entity;
                try
                {
                    entityList.list = IBReaDeptGoods.Search();
                    entityList.count = IBReaDeptGoods.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaDeptGoods.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaDeptGoods.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaDeptGoodsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaDeptGoods.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaDeptGoods>(entity);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsByHRDeptID(long deptID, string where, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                    sort = CommonServiceMethod.GetSortHQL(sort);

                entityList = IBReaDeptGoods.SearchReaGoodsByHRDeptID(deptID, where, sort, page, limit);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
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

        #region ReaGoodsLot
        //Add  ReaGoodsLot
        public BaseResultDataValue ST_UDTO_AddReaGoodsLot(ReaGoodsLot entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;

                try
                {
                    ReaGoodsLot reaGoodsLot = null;
                    BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
                    baseResultDataValue.success = baseResultBool.success;
                    baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;

                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaGoodsLot
        public BaseResultBool ST_UDTO_UpdateReaGoodsLot(ReaGoodsLot entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;
                try
                {
                    ReaGoodsLot reaGoodsLot = null;
                    baseResultBool = IBReaGoodsLot.EditValid(ref reaGoodsLot);
                    if (baseResultBool.success == false) return baseResultBool;

                    baseResultBool.success = IBReaGoodsLot.Edit();
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
        //Update  ReaGoodsLot
        public BaseResultBool ST_UDTO_UpdateReaGoodsLotByField(ReaGoodsLot entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsLot.Entity = entity;

                try
                {
                    ReaGoodsLot reaGoodsLot = null;
                    baseResultBool = IBReaGoodsLot.EditValid(ref reaGoodsLot);
                    if (baseResultBool.success == false) return baseResultBool;

                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsLot.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsLot.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsLot.Edit();
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
        public BaseResultBool ST_UDTO_UpdateReaGoodsLotByVerificationField(ReaGoodsLot entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            if (entity != null)
            {
                baseResultBool = IBReaGoodsLot.UpdateVerification(entity, fields);
                //货品批号性能处理后,需要更新对应的库存货品批号的性能验证结果
                if (baseResultBool.success == true)
                    baseResultBool = IBReaBmsQtyDtl.UpdateVerificationByReaGoodsLot(entity);
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ReaGoodsLot
        public BaseResultBool ST_UDTO_DelReaGoodsLot(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsLot.Entity = IBReaGoodsLot.Get(id);
                if (IBReaGoodsLot.Entity != null)
                {
                    long labid = IBReaGoodsLot.Entity.LabID;
                    string entityName = IBReaGoodsLot.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsLot.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLot(ReaGoodsLot entity)
        {
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsLot.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsLot.Search();
                    entityList.count = IBReaGoodsLot.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsLot>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLotByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsLot.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsLot.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsLot>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsLotById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ReaGoodsLot entity = IBReaGoodsLot.Get(id);
                if (fields.Contains("VerificationMemo"))
                {
                    entity = IBReaGoodsLot.GetVerificationMemo(entity);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsLot>(entity);
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

        #region ReaGoodsRegister
        public Stream ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile()
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();
            ReaGoodsRegister entity = null;
            string entityStr = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (file.FileName.Length > 0)
                {
                    string[] temp = file.FileName.Split('.');
                    if (temp[temp.Length - 1].ToLower() != "pdf")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "entity":
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            entityStr = HttpContext.Current.Request.Form["entity"].Trim();
                        break;
                    case "file":
                        break;
                    default:
                        break;
                }
            }
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string hrdeptID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID);
            string hrdeptCode = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptCode);
            // ZhiFang.Common.Log.Log.Debug("新增注册证信息机构编码:" + hrdeptCode);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "登录人信息为空!请登录后再操作!";
            }
            if (baseResultBool.success && String.IsNullOrEmpty(hrdeptID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "登录人机构信息为空!请登录后再操作!";
            }
            //if (baseResultBool.success && String.IsNullOrEmpty(hrdeptCode))
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "登录人机构编号信息为空!请登录后再操作!";
            //}
            if (baseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity为空!";
            }
            if (baseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增注册证信息出错:" + baseResultBool.ErrorInfo);
            }
            if (baseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ReaGoodsRegister>(entityStr);
                    entity.EmpID = long.Parse(employeeID);
                    entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "新增注册证信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增注册证信息序列化出错:" + ex.Message);
                }
            }

            if (baseResultBool.success)
            {
                entity.DataAddTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultBool = IBReaGoodsRegister.AddReaGoodsRegisterAndUploadRegisterFile(file);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增注册证信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            ReaGoodsRegister entity = null;
            string fields = "";
            string fFileEntity = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (!String.IsNullOrEmpty(file.FileName))
                {
                    string[] temp = file.FileName.Split('.');
                    if (temp[temp.Length - 1].ToLower() != "pdf")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传PDF格式的原件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }

            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "fields":
                        if (HttpContext.Current.Request.Form["fields"].Trim() != "")
                            fields = HttpContext.Current.Request.Form["fields"].Trim();
                        break;
                    case "entity"://Entity
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["entity"].Trim();
                        break;

                }
            }
            if (!string.IsNullOrEmpty(fFileEntity))
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ReaGoodsRegister>(fFileEntity);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            if (entity == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (baseResultBool.success)
            {
                entity.EmpID = long.Parse(employeeID);
                entity.EmpName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBReaGoodsRegister.UpdateReaGoodsRegisterAndUploadRegisterFileByField(tempArray, file);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + baseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新注册证信息出错:" + baseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新注册证信息出错1:" + baseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public Stream ST_UDTO_ReaGoodsRegisterPreviewPdf(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = "";
                fileStream = IBReaGoodsRegister.GetReaGoodsRegisterFileStream(id, ref filename);

                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "注册证文件不存在!请重新上传或联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                    return memoryStream;
                }
                else
                {
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                    return fileStream;
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "预览注册证文件错误!" + ex.Message;
                ZhiFang.Common.Log.Log.Error(errorInfo);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsRegister.SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsRegister.SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
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

        //Add  ReaGoodsRegister
        public BaseResultDataValue ST_UDTO_AddReaGoodsRegister(ReaGoodsRegister entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoodsRegister.Add();

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
        //Update  ReaGoodsRegister
        public BaseResultBool ST_UDTO_UpdateReaGoodsRegister(ReaGoodsRegister entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoodsRegister.Edit();

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
        //Update  ReaGoodsRegister
        public BaseResultBool ST_UDTO_UpdateReaGoodsRegisterByField(ReaGoodsRegister entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsRegister.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsRegister.Update(tempArray);

                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsRegister.Edit();
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
        //Delele  ReaGoodsRegister
        public BaseResultBool ST_UDTO_DelReaGoodsRegister(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsRegister.Entity = IBReaGoodsRegister.Get(id);
                if (IBReaGoodsRegister.Entity != null)
                {
                    long labid = IBReaGoodsRegister.Entity.LabID;
                    string entityName = IBReaGoodsRegister.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsRegister.RemoveByHQL(id);

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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegister(ReaGoodsRegister entity)
        {
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsRegister.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsRegister.Search();
                    entityList.count = IBReaGoodsRegister.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsRegister> entityList = new EntityList<ReaGoodsRegister>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsRegister.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsRegister.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsRegister>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsRegister.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsRegister>(entity);
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

        #region ReaGoodsUnit
        //Add  ReaGoodsUnit
        public BaseResultDataValue ST_UDTO_AddReaGoodsUnit(ReaGoodsUnit entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaGoodsUnit.Add();

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
        //Update  ReaGoodsUnit
        public BaseResultBool ST_UDTO_UpdateReaGoodsUnit(ReaGoodsUnit entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaGoodsUnit.Edit();

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
        //Update  ReaGoodsUnit
        public BaseResultBool ST_UDTO_UpdateReaGoodsUnitByField(ReaGoodsUnit entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaGoodsUnit.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaGoodsUnit.Update(tempArray);

                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaGoodsUnit.Edit();
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
        //Delele  ReaGoodsUnit
        public BaseResultBool ST_UDTO_DelReaGoodsUnit(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaGoodsUnit.Entity = IBReaGoodsUnit.Get(id);
                if (IBReaGoodsUnit.Entity != null)
                {
                    long labid = IBReaGoodsUnit.Entity.LabID;
                    string entityName = IBReaGoodsUnit.Entity.GetType().Name;
                    baseResultBool.success = IBReaGoodsUnit.RemoveByHQL(id);

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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnit(ReaGoodsUnit entity)
        {
            EntityList<ReaGoodsUnit> entityList = new EntityList<ReaGoodsUnit>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaGoodsUnit.Entity = entity;
                try
                {
                    entityList.list = IBReaGoodsUnit.Search();
                    entityList.count = IBReaGoodsUnit.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsUnit>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsUnit> entityList = new EntityList<ReaGoodsUnit>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsUnit.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsUnit.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsUnit>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaGoodsUnitById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaGoodsUnit.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsUnit>(entity);
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

        #region ReaPlace
        //Add  ReaPlace
        public BaseResultDataValue ST_UDTO_AddReaPlace(ReaPlace entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaPlace.Add();
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
        //Update  ReaPlace
        public BaseResultBool ST_UDTO_UpdateReaPlace(ReaPlace entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaPlace.Edit();
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
        //Update  ReaPlace
        public BaseResultBool ST_UDTO_UpdateReaPlaceByField(ReaPlace entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaPlace.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaPlace.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaPlace.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaPlace.Edit();
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
        //Delele  ReaPlace
        public BaseResultBool ST_UDTO_DelReaPlace(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaPlace.Entity = IBReaPlace.Get(id);
                if (IBReaPlace.Entity != null)
                {
                    long labid = IBReaPlace.Entity.LabID;
                    string entityName = IBReaPlace.Entity.GetType().Name;
                    baseResultBool.success = IBReaPlace.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaPlace(ReaPlace entity)
        {
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaPlace.Entity = entity;
                try
                {
                    entityList.list = IBReaPlace.Search();
                    entityList.count = IBReaPlace.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaPlace>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaPlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaPlace.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaPlace.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaPlace>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaPlaceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaPlace.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaPlace>(entity);
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

        #region ReaStorage
        //Add  ReaStorage
        public BaseResultDataValue ST_UDTO_AddReaStorage(ReaStorage entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaStorage.Add();
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
        //Update  ReaStorage
        public BaseResultBool ST_UDTO_UpdateReaStorage(ReaStorage entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaStorage.Edit();
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
        //Update  ReaStorage
        public BaseResultBool ST_UDTO_UpdateReaStorageByField(ReaStorage entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaStorage.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaStorage.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaStorage.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaStorage.Edit();
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
        //Delele  ReaStorage
        public BaseResultBool ST_UDTO_DelReaStorage(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaStorage.Entity = IBReaStorage.Get(id);
                if (IBReaStorage.Entity != null)
                {
                    long labid = IBReaStorage.Entity.LabID;
                    string entityName = IBReaStorage.Entity.GetType().Name;
                    baseResultBool.success = IBReaStorage.RemoveByHQL(id);

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

        public BaseResultDataValue ST_UDTO_SearchReaStorage(ReaStorage entity)
        {
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaStorage.Entity = entity;
                try
                {
                    entityList.list = IBReaStorage.Search();
                    entityList.count = IBReaStorage.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaStorage>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaStorageByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaStorage.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaStorage.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaStorage>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaStorageById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaStorage.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaStorage>(entity);
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

        #region ReaReqOperation
        //Add  ReaReqOperation
        public BaseResultDataValue ST_UDTO_AddReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaReqOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaReqOperation.Get(IBReaReqOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaReqOperation.Entity);
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
        //Update  ReaReqOperation
        public BaseResultBool ST_UDTO_UpdateReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaReqOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaReqOperation
        public BaseResultBool ST_UDTO_UpdateReaReqOperationByField(ReaReqOperation entity, string fields)
        {
            IBReaReqOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaReqOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaReqOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaReqOperation.Edit();
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
        //Delele  ReaReqOperation
        public BaseResultBool ST_UDTO_DelReaReqOperation(long longReaReqOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaReqOperation.Remove(longReaReqOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaReqOperation(ReaReqOperation entity)
        {
            IBReaReqOperation.Entity = entity;
            EntityList<ReaReqOperation> tempEntityList = new EntityList<ReaReqOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaReqOperation.Search();
                tempEntityList.count = IBReaReqOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaReqOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaReqOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaReqOperation> tempEntityList = new EntityList<ReaReqOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaReqOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaReqOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaReqOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaReqOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaReqOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaReqOperation>(tempEntity);
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

        #region ReaBmsQtyDtl
        //Add  ReaBmsQtyDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsQtyDtl.Add();

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
        //Update  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsQtyDtl.Edit();

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
        //Update  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlByField(ReaBmsQtyDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsQtyDtl.Update(tempArray);

                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsQtyDtl.Edit();
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
        //Delele  ReaBmsQtyDtl
        public BaseResultBool ST_UDTO_DelReaBmsQtyDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsQtyDtl.Entity = IBReaBmsQtyDtl.Get(id);
                if (IBReaBmsQtyDtl.Entity != null)
                {
                    long labid = IBReaBmsQtyDtl.Entity.LabID;
                    string entityName = IBReaBmsQtyDtl.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsQtyDtl.RemoveByHQL(id);

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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtl(ReaBmsQtyDtl entity)
        {
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsQtyDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsQtyDtl.Search();
                    entityList.count = IBReaBmsQtyDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsQtyDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsQtyDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsQtyDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsQtyDtl>(entity);
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

        #region ReaCheckInOperation
        //Add  ReaCheckInOperation
        public BaseResultDataValue ST_UDTO_AddReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaCheckInOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaCheckInOperation.Get(IBReaCheckInOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaCheckInOperation.Entity);
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
        //Update  ReaCheckInOperation
        public BaseResultBool ST_UDTO_UpdateReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaCheckInOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaCheckInOperation
        public BaseResultBool ST_UDTO_UpdateReaCheckInOperationByField(ReaCheckInOperation entity, string fields)
        {
            IBReaCheckInOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCheckInOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaCheckInOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaCheckInOperation.Edit();
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
        //Delele  ReaCheckInOperation
        public BaseResultBool ST_UDTO_DelReaCheckInOperation(long longReaCheckInOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaCheckInOperation.Remove(longReaCheckInOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperation(ReaCheckInOperation entity)
        {
            IBReaCheckInOperation.Entity = entity;
            EntityList<ReaCheckInOperation> tempEntityList = new EntityList<ReaCheckInOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaCheckInOperation.Search();
                tempEntityList.count = IBReaCheckInOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaCheckInOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCheckInOperation> tempEntityList = new EntityList<ReaCheckInOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaCheckInOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaCheckInOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaCheckInOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCheckInOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaCheckInOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaCheckInOperation>(tempEntity);
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

        #region ReaCenBarCodeFormat
        //Add  ReaCenBarCodeFormat
        public BaseResultDataValue ST_UDTO_AddReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaCenBarCodeFormat.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaCenBarCodeFormat.Edit();

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
        //Update  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormatByField(ReaCenBarCodeFormat entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaCenBarCodeFormat.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaCenBarCodeFormat.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaCenBarCodeFormat.Edit();
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
        //Delele  ReaCenBarCodeFormat
        public BaseResultBool ST_UDTO_DelReaCenBarCodeFormat(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaCenBarCodeFormat.Entity = IBReaCenBarCodeFormat.Get(id);
                if (IBReaCenBarCodeFormat.Entity != null)
                {
                    long labid = IBReaCenBarCodeFormat.Entity.LabID;
                    string entityName = IBReaCenBarCodeFormat.Entity.GetType().Name;
                    baseResultBool.success = IBReaCenBarCodeFormat.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormat(ReaCenBarCodeFormat entity)
        {
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaCenBarCodeFormat.Entity = entity;
                try
                {
                    entityList.list = IBReaCenBarCodeFormat.Search();
                    entityList.count = IBReaCenBarCodeFormat.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenBarCodeFormat>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaCenBarCodeFormat> entityList = new EntityList<ReaCenBarCodeFormat>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaCenBarCodeFormat.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaCenBarCodeFormat.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaCenBarCodeFormat>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaCenBarCodeFormat.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaCenBarCodeFormat>(entity);
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

        public Stream ST_UDTO_DownLoadReaCenBarCodeFormatOfPlatformOrgNo(string platformOrgNo, long operateType)
        {
            Stream stream = null;
            Encoding code = Encoding.GetEncoding("gb2312");
            System.Web.HttpContext.Current.Response.ContentEncoding = code;
            System.Web.HttpContext.Current.Response.HeaderEncoding = code;

            if (string.IsNullOrEmpty(platformOrgNo))
            {
                string errorInfo = "机构平台编码(platformOrgNo)信息为空!";
                stream = ResponseResultStream.GetResultInfoOfStream(errorInfo);
                operateType = 1;
            }
            string filename = platformOrgNo + ".json" + "." + FileExt.zf;
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                FileStream fileStream = IBReaCenBarCodeFormat.DownLoadReaCenBarCodeFormatOfPlatformOrgNo(platformOrgNo, labID);
                if (fileStream != null)
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";//application/octet-stream text/plain
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    return fileStream;
                }
                else
                {
                    operateType = 1;
                    string errorInfo = "获取条码规则信息为空!";
                    stream = ResponseResultStream.GetResultInfoOfStream(errorInfo);
                }
            }
            catch (Exception ex)
            {
                operateType = 1;
                string errorInfo = "导出条码规则信息错误。";
                ZhiFang.Common.Log.Log.Error("导出条码规则信息(某一供应商所属条码规则+公共条码规则信息):" + ex.Message);
                stream = ResponseResultStream.GetResultInfoOfStream(errorInfo);
            }

            if (operateType == 1)//直接打开文件
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
            }
            return stream;
        }
        public Message ST_UDTO_UploadReaCenBarCodeFormatOfAttachment()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                tempBaseResultBool.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "未检测到文件！";
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    string fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));
                    if (!fileExt.Equals("." + FileExt.zf))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "导入的条码规则附件文件后缀名必须是.zf";
                    }
                    else
                    {
                        long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                        tempBaseResultBool = IBReaCenBarCodeFormat.UploadReaCenBarCodeFormatOfAttachment(file, labID);
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("导入条码规则附件文件错误:" + ex.Message);
                tempBaseResultBool.ErrorInfo = ex.Message;
                tempBaseResultBool.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        #endregion

        #region ReaChooseGoodsTemplate
        //Add  ReaChooseGoodsTemplate
        public BaseResultDataValue ST_UDTO_AddReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                entity.IsUse = true;
                entity.DataAddTime = DateTime.Now;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaChooseGoodsTemplate.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_AddReaChooseGoodsTemplate.Error:" + ex.StackTrace);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaChooseGoodsTemplate.Edit();

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
        //Update  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplateByField(ReaChooseGoodsTemplate entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            if (entity != null)
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue) entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreatName)) entity.CreatName = empName;
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaChooseGoodsTemplate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaChooseGoodsTemplate.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaChooseGoodsTemplate.Edit();
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
        //Delele  ReaChooseGoodsTemplate
        public BaseResultBool ST_UDTO_DelReaChooseGoodsTemplate(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaChooseGoodsTemplate.Entity = IBReaChooseGoodsTemplate.Get(id);
                if (IBReaChooseGoodsTemplate.Entity != null)
                {
                    long labid = IBReaChooseGoodsTemplate.Entity.LabID;
                    string entityName = IBReaChooseGoodsTemplate.Entity.GetType().Name;
                    baseResultBool.success = IBReaChooseGoodsTemplate.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity)
        {
            EntityList<ReaChooseGoodsTemplate> entityList = new EntityList<ReaChooseGoodsTemplate>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaChooseGoodsTemplate.Entity = entity;
                try
                {
                    entityList.list = IBReaChooseGoodsTemplate.Search();
                    entityList.count = IBReaChooseGoodsTemplate.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaChooseGoodsTemplate>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaChooseGoodsTemplate> entityList = new EntityList<ReaChooseGoodsTemplate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaChooseGoodsTemplate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaChooseGoodsTemplate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaChooseGoodsTemplate>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaChooseGoodsTemplate.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaChooseGoodsTemplate>(entity);
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

        #region ReaBmsCenOrderDoc       
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenOrderDoc> entityList = new EntityList<ReaBmsCenOrderDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenOrderDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenOrderDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenOrderDoc>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenOrderDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenOrderDoc>(entity);
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
        public BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocByField(ReaBmsCenOrderDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenOrderDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenOrderDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsCenOrderDoc.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenOrderDoc.Edit();
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
        public BaseResultBool ST_UDTO_DelReaBmsCenOrderDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenOrderDoc.Entity = IBReaBmsCenOrderDoc.Get(id);
                if (IBReaBmsCenOrderDoc.Entity != null)
                {
                    long labid = IBReaBmsCenOrderDoc.Entity.LabID;
                    string entityName = IBReaBmsCenOrderDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsCenOrderDoc.RemoveByHQL(id);
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

        #endregion

        #region ReaBmsCenOrderDtl

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenOrderDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenOrderDtl>(entity);
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenOrderDtl> entityList = new EntityList<ReaBmsCenOrderDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenOrderDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenOrderDtl.SearchListByHQL(where, page, limit);
                }

                //IList<ReaGoods> tempGoodsList = IBReaGoods.SearchListByHQL("Visible=1");
                //foreach (ReaBmsCenOrderDtl dtl in entityList.list)
                //{
                //    var l = tempGoodsList.Where(p => p.Id == dtl.ReaGoodsID).ToList();
                //    if (l.Count > 0)
                //    {
                //        dtl.GoodSName = l[0].SName;
                //        dtl.GoodEName = l[0].EName;
                //    }
                //}

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenOrderDtl>(entityList);
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

        #region ReaBmsCenSaleDoc
        //Add  BmsCenSaleDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                if (entity.LabID <= 0)
                {
                    long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    entity.LabID = labID;
                }
                entity.DataAddTime = DateTime.Now;
                IBReaBmsCenSaleDoc.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsCenSaleDoc.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDoc.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsCenSaleDoc.Edit();
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
        //Update  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocByField(ReaBmsCenSaleDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDoc.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDoc.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsCenSaleDoc.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenSaleDoc.Edit();
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
        //Delele  BmsCenSaleDoc
        public BaseResultBool ST_UDTO_DelReaBmsCenSaleDoc(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenSaleDoc.Entity = IBReaBmsCenSaleDoc.Get(id);
                if (IBReaBmsCenSaleDoc.Entity != null)
                {
                    long labid = IBReaBmsCenSaleDoc.Entity.LabID;
                    string entityName = IBReaBmsCenSaleDoc.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsCenSaleDoc.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity)
        {
            EntityList<ReaBmsCenSaleDoc> entityList = new EntityList<ReaBmsCenSaleDoc>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsCenSaleDoc.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsCenSaleDoc.Search();
                    entityList.count = IBReaBmsCenSaleDoc.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDoc> entityList = new EntityList<ReaBmsCenSaleDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDoc>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenSaleDoc.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenSaleDoc>(entity);
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

        #region ReaBmsCenSaleDtl
        //Add  ReaBmsCenSaleDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                if (entity.LabID <= 0)
                {
                    long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    entity.LabID = labID;
                }
                entity.DataAddTime = DateTime.Now;
                IBReaBmsCenSaleDtl.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsCenSaleDtl.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBReaBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(IBReaBmsCenSaleDtl.Entity.ReaBmsCenSaleDoc.Id);
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
        //Update  ReaBmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDtl.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsCenSaleDtl.Edit();
                    if (baseResultBool.success)
                    {
                        ReaBmsCenSaleDtl saleDtl = IBReaBmsCenSaleDtl.Get(entity.Id);
                        //IBReaBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.ReaBmsCenSaleDoc.Id);
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
        //Update  ReaBmsCenSaleDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtlByField(ReaBmsCenSaleDtl entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                entity.LabID = labID;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDtl.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDtl.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsCenSaleDtl.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                ReaBmsCenSaleDtl saleDtl = IBReaBmsCenSaleDtl.Get(entity.Id);
                                //IBReaBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDtl.ReaBmsCenSaleDoc.Id);
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsCenSaleDtl.Edit();
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
        //Delele  ReaBmsCenSaleDtl
        public BaseResultBool ST_UDTO_DelReaBmsCenSaleDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenSaleDtl.Entity = IBReaBmsCenSaleDtl.Get(id);
                if (IBReaBmsCenSaleDtl.Entity != null)
                {
                    long labid = IBReaBmsCenSaleDtl.Entity.LabID;
                    string entityName = IBReaBmsCenSaleDtl.Entity.GetType().Name;
                    long saleDocID = IBReaBmsCenSaleDtl.Entity.SaleDocID.Value;
                    baseResultBool.success = IBReaBmsCenSaleDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBReaBmsCenSaleDoc.EditBmsCenSaleDocTotalPrice(saleDocID, id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity)
        {
            EntityList<ReaBmsCenSaleDtl> entityList = new EntityList<ReaBmsCenSaleDtl>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsCenSaleDtl.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsCenSaleDtl.Search();
                    entityList.count = IBReaBmsCenSaleDtl.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDtl> entityList = new EntityList<ReaBmsCenSaleDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenSaleDtl.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenSaleDtl>(entity);
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

        #region ReaBmsCenSaleDocConfirm
        //Add  ReaBmsCenSaleDocConfirm
        public BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDocConfirm(ReaBmsCenSaleDocConfirm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBReaBmsCenSaleDocConfirm.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
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
        //Update  ReaBmsCenSaleDocConfirm
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocConfirm(ReaBmsCenSaleDocConfirm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    baseResultBool.success = IBReaBmsCenSaleDocConfirm.Edit();
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

        //Delele  ReaBmsCenSaleDocConfirm
        public BaseResultBool ST_UDTO_DelReaBmsCenSaleDocConfirm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenSaleDocConfirm.Entity = IBReaBmsCenSaleDocConfirm.Get(id);
                if (IBReaBmsCenSaleDocConfirm.Entity != null)
                {
                    long labid = IBReaBmsCenSaleDocConfirm.Entity.LabID;
                    string entityName = IBReaBmsCenSaleDocConfirm.Entity.GetType().Name;
                    baseResultBool.success = IBReaBmsCenSaleDocConfirm.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocConfirm(ReaBmsCenSaleDocConfirm entity)
        {
            EntityList<ReaBmsCenSaleDocConfirm> entityList = new EntityList<ReaBmsCenSaleDocConfirm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    entityList.list = IBReaBmsCenSaleDocConfirm.Search();
                    entityList.count = IBReaBmsCenSaleDocConfirm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDocConfirm>(entityList);
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

        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocConfirmByField(ReaBmsCenSaleDocConfirm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsCenSaleDocConfirm.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenSaleDocConfirm.Edit();
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDocConfirm> entityList = new EntityList<ReaBmsCenSaleDocConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDocConfirm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDocConfirm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDocConfirm>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocConfirmById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenSaleDocConfirm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenSaleDocConfirm>(entity);
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

        #region ReaBmsCenSaleDtlConfirm
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtlConfirmByField(ReaBmsCenSaleDtlConfirm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsCenSaleDtlConfirm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDtlConfirm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBReaBmsCenSaleDtlConfirm.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBBmsCenSaleDtlConfirm.Edit();
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDtlConfirm> entityList = new EntityList<ReaBmsCenSaleDtlConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(where, page, limit);
                }

                //if (fields.IndexOf("ReaBmsCenSaleDtlConfirm_ProdOrgName") >= 0)
                //{
                //    //Rea_BmsCenSaleDtlConfirm，表里有冗余字段ProdOrgName，但是值都为NULL。
                //    //如果字段里包含厂商编码[ReaBmsCenSaleDtlConfirm_ProdOrgName]，则根据货品ID动态取货品表里的厂商编码
                //    if (entityList.list.Count > 0)
                //    {
                //        string reaGoodsIDs = string.Join(",", entityList.list.Where(p => p.ReaGoodsID != null).Select(p => p.ReaGoodsID.Value).ToArray());
                //        var goodsList = IBReaGoods.SearchListByHQL(string.Format("Id in ({0})", reaGoodsIDs));
                //        foreach (ReaBmsCenSaleDtlConfirm reaBmsCenSaleDtlConfirm in entityList.list)
                //        {
                //            var l = goodsList.Where(p => p.Id == reaBmsCenSaleDtlConfirm.ReaGoodsID).ToList();
                //            if (l.Count > 0)
                //            {
                //                reaBmsCenSaleDtlConfirm.ProdOrgName = l[0].ProdOrgName;
                //            }
                //        }
                //    }
                //}

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDtlConfirm>(entityList);
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
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBReaBmsCenSaleDtlConfirm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsCenSaleDtlConfirm>(entity);
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

        #region ReaEquipReagentLink
        //Add  ReaEquipReagentLink
        public BaseResultDataValue ST_UDTO_AddReaEquipReagentLink(ReaEquipReagentLink entity)
        {
            IBReaEquipReagentLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaEquipReagentLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaEquipReagentLink.Get(IBReaEquipReagentLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaEquipReagentLink.Entity);
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
        //Update  ReaEquipReagentLink
        public BaseResultBool ST_UDTO_UpdateReaEquipReagentLink(ReaEquipReagentLink entity)
        {
            IBReaEquipReagentLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaEquipReagentLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaEquipReagentLink
        public BaseResultBool ST_UDTO_UpdateReaEquipReagentLinkByField(ReaEquipReagentLink entity, string fields)
        {
            IBReaEquipReagentLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaEquipReagentLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaEquipReagentLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaEquipReagentLink.Edit();
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
        //Delele  ReaEquipReagentLink
        public BaseResultBool ST_UDTO_DelReaEquipReagentLink(long longReaEquipReagentLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaEquipReagentLink.Remove(longReaEquipReagentLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaEquipReagentLink(ReaEquipReagentLink entity)
        {
            IBReaEquipReagentLink.Entity = entity;
            EntityList<ReaEquipReagentLink> tempEntityList = new EntityList<ReaEquipReagentLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaEquipReagentLink.Search();
                tempEntityList.count = IBReaEquipReagentLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipReagentLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaEquipReagentLink> tempEntityList = new EntityList<ReaEquipReagentLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaEquipReagentLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaEquipReagentLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipReagentLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaEquipReagentLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaEquipReagentLink>(tempEntity);
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

        #region ReaTestEquipLab
        //Add  ReaTestEquipLab
        public BaseResultDataValue ST_UDTO_AddReaTestEquipLab(ReaTestEquipLab entity)
        {
            IBReaTestEquipLab.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaTestEquipLab.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaTestEquipLab.Get(IBReaTestEquipLab.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaTestEquipLab.Entity);
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
        //Update  ReaTestEquipLab
        public BaseResultBool ST_UDTO_UpdateReaTestEquipLab(ReaTestEquipLab entity)
        {
            IBReaTestEquipLab.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipLab.EditVerification();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("Lis仪器编码为{0},已存在,请不要重复维护!", entity.LisCode);
                    return tempBaseResultBool;
                }
                tempBaseResultBool.success = IBReaTestEquipLab.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaTestEquipLab
        public BaseResultBool ST_UDTO_UpdateReaTestEquipLabByField(ReaTestEquipLab entity, string fields)
        {
            IBReaTestEquipLab.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipLab.EditVerification();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("Lis仪器编码为{0},已存在,请不要重复维护!", entity.LisCode);
                    return tempBaseResultBool;
                }

                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaTestEquipLab.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaTestEquipLab.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaTestEquipLab.Edit();
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
        //Delele  ReaTestEquipLab
        public BaseResultBool ST_UDTO_DelReaTestEquipLab(long longReaTestEquipLabID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipLab.Remove(longReaTestEquipLabID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipLab(ReaTestEquipLab entity)
        {
            IBReaTestEquipLab.Entity = entity;
            EntityList<ReaTestEquipLab> tempEntityList = new EntityList<ReaTestEquipLab>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaTestEquipLab.Search();
                tempEntityList.count = IBReaTestEquipLab.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipLab>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipLabByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipLab> tempEntityList = new EntityList<ReaTestEquipLab>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipLab.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipLab.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipLab>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipLabById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaTestEquipLab.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaTestEquipLab>(tempEntity);
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

        #region ReaTestEquipProd
        //Add  ReaTestEquipProd
        public BaseResultDataValue ST_UDTO_AddReaTestEquipProd(ReaTestEquipProd entity)
        {
            IBReaTestEquipProd.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaTestEquipProd.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaTestEquipProd.Get(IBReaTestEquipProd.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaTestEquipProd.Entity);
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
        //Update  ReaTestEquipProd
        public BaseResultBool ST_UDTO_UpdateReaTestEquipProd(ReaTestEquipProd entity)
        {
            IBReaTestEquipProd.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipProd.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaTestEquipProd
        public BaseResultBool ST_UDTO_UpdateReaTestEquipProdByField(ReaTestEquipProd entity, string fields)
        {
            IBReaTestEquipProd.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaTestEquipProd.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaTestEquipProd.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaTestEquipProd.Edit();
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
        //Delele  ReaTestEquipProd
        public BaseResultBool ST_UDTO_DelReaTestEquipProd(long longReaTestEquipProdID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipProd.Remove(longReaTestEquipProdID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipProd(ReaTestEquipProd entity)
        {
            IBReaTestEquipProd.Entity = entity;
            EntityList<ReaTestEquipProd> tempEntityList = new EntityList<ReaTestEquipProd>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaTestEquipProd.Search();
                tempEntityList.count = IBReaTestEquipProd.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipProd>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipProdByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipProd> tempEntityList = new EntityList<ReaTestEquipProd>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipProd.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipProd.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipProd>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipProdById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaTestEquipProd.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaTestEquipProd>(tempEntity);
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

        #region ReaTestEquipType
        //Add  ReaTestEquipType
        public BaseResultDataValue ST_UDTO_AddReaTestEquipType(ReaTestEquipType entity)
        {
            IBReaTestEquipType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaTestEquipType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaTestEquipType.Get(IBReaTestEquipType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaTestEquipType.Entity);
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
        //Update  ReaTestEquipType
        public BaseResultBool ST_UDTO_UpdateReaTestEquipType(ReaTestEquipType entity)
        {
            IBReaTestEquipType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaTestEquipType
        public BaseResultBool ST_UDTO_UpdateReaTestEquipTypeByField(ReaTestEquipType entity, string fields)
        {
            IBReaTestEquipType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaTestEquipType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaTestEquipType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaTestEquipType.Edit();
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
        //Delele  ReaTestEquipType
        public BaseResultBool ST_UDTO_DelReaTestEquipType(long longReaTestEquipTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipType.Remove(longReaTestEquipTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipType(ReaTestEquipType entity)
        {
            IBReaTestEquipType.Entity = entity;
            EntityList<ReaTestEquipType> tempEntityList = new EntityList<ReaTestEquipType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaTestEquipType.Search();
                tempEntityList.count = IBReaTestEquipType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipType> tempEntityList = new EntityList<ReaTestEquipType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaTestEquipType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaTestEquipType>(tempEntity);
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

        #region ReaBmsQtyDtlOperation
        //Add  ReaBmsQtyDtlOperation
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity)
        {
            IBReaBmsQtyDtlOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsQtyDtlOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyDtlOperation.Get(IBReaBmsQtyDtlOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyDtlOperation.Entity);
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
        //Update  ReaBmsQtyDtlOperation
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity)
        {
            IBReaBmsQtyDtlOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyDtlOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsQtyDtlOperation
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlOperationByField(ReaBmsQtyDtlOperation entity, string fields)
        {
            IBReaBmsQtyDtlOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyDtlOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsQtyDtlOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsQtyDtlOperation.Edit();
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
        //Delele  ReaBmsQtyDtlOperation
        public BaseResultBool ST_UDTO_DelReaBmsQtyDtlOperation(long longReaBmsQtyDtlOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyDtlOperation.Remove(longReaBmsQtyDtlOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity)
        {
            IBReaBmsQtyDtlOperation.Entity = entity;
            EntityList<ReaBmsQtyDtlOperation> tempEntityList = new EntityList<ReaBmsQtyDtlOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsQtyDtlOperation.Search();
                tempEntityList.count = IBReaBmsQtyDtlOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyDtlOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtlOperation> tempEntityList = new EntityList<ReaBmsQtyDtlOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsQtyDtlOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsQtyDtlOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyDtlOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsQtyDtlOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsQtyDtlOperation>(tempEntity);
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

        #region 客户端入库
        public BaseResultDataValue ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isNoPrefEntity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaSaleDtlConfirmVO> entityList = new EntityList<ReaSaleDtlConfirmVO>();
            //ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL.where:" + where);
            //ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL.fields:" + fields);
            try
            {
                if (isNoPrefEntity == true)
                {
                    fields = fields.Replace("DtlFieldsVO_", "ReaSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirm_");
                    //ZhiFang.Common.Log.Log.Debug("fields:"+ fields);
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchReaDtlConfirmVOOfStoreInByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchReaDtlConfirmVOOfStoreInByHQL(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaSaleDtlConfirmVO>(entityList);
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
                    //ZhiFang.Common.Log.Log.Debug("SearchReaDtlConfirmVOOfStoreInByHQL.序列化错误:" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Debug("SearchReaDtlConfirmVOOfStoreInByHQL.HQL查询错误:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(serialNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "serialNo参数值为空!";
                return baseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                ReaGoodsScanCodeVO entity = IBReaGoodsBarcodeOperation.DecodingReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(reaCompID, serialNo, docConfirmID);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity);
                    //baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaGoodsScanCodeVO>(entity);
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }
            if (string.IsNullOrEmpty(dtlDocConfirmIDStr))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "本次入库的验收明细IDStr为空，不能入库！";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsInDoc.AddReaBmsInDocAndDtlOfVO(dtAddList, docConfirmID, dtlDocConfirmIDStr, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    //IBReaBmsCenSaleDocConfirm.Get(IBReaBmsCenSaleDocConfirm.Entity.Id);
                    IBReaBmsInDoc.Get(IBReaBmsInDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsInDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("验收入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtlOfManualInput(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsInDoc.AddReaBmsInDocAndDtlOfVO(dtAddList, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsInDoc.Get(IBReaBmsInDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsInDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("手工入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsInDocAndDtlOfManualInput(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, string codeScanningMode, string fields)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if ((dtAddList == null || dtAddList.Count <= 0) && (dtEditList == null || dtEditList.Count <= 0))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数dtAddList及dtEditList为空!";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                //IBReaBmsInDoc.Entity = entity;
                //ReaBmsInDoc serverEntity = IBReaBmsInDoc.Get(entity.Id);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsInDoc.EditReaBmsInDocAndDtlOfVO(entity, tempArray, dtAddList, dtEditList, fieldsDtl, codeScanningMode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("手工入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsInDocOfConfirmStock(long id, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                tempBaseResultBool = IBReaBmsInDoc.EditReaBmsInDocOfConfirmStock(id, codeScanningMode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("确认入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtlVO> entityList = new EntityList<ReaBmsInDtlVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDtl.SearchReaBmsInDtlVOByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDtl.SearchReaBmsInDtlVOByHQL(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtlVO>(entityList);
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

        #region ReaBmsCheckDoc

        //Update  ReaBmsCheckDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsCheckDoc(ReaBmsCheckDoc entity)
        {
            IBReaBmsCheckDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsCheckDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsCheckDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsCheckDocByField(ReaBmsCheckDoc entity, string fields)
        {
            IBReaBmsCheckDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCheckDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsCheckDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsCheckDoc.Edit();
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDoc(ReaBmsCheckDoc entity)
        {
            IBReaBmsCheckDoc.Entity = entity;
            EntityList<ReaBmsCheckDoc> tempEntityList = new EntityList<ReaBmsCheckDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsCheckDoc.Search();
                tempEntityList.count = IBReaBmsCheckDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCheckDoc> tempEntityList = new EntityList<ReaBmsCheckDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsCheckDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsCheckDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsCheckDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsCheckDoc>(tempEntity);
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

        //Delele  ReaBmsCheckDoc
        public BaseResultBool ST_UDTO_DelReaBmsCheckDoc(long longReaBmsCheckDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBReaBmsCheckDoc.DelReaBmsCheckDoc(longReaBmsCheckDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        #endregion

        #region ReaBmsCheckDtl
        //Add  ReaBmsCheckDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsCheckDtl(ReaBmsCheckDtl entity)
        {
            IBReaBmsCheckDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsCheckDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCheckDtl.Get(IBReaBmsCheckDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCheckDtl.Entity);
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
        //Update  ReaBmsCheckDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsCheckDtl(ReaBmsCheckDtl entity)
        {
            IBReaBmsCheckDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsCheckDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsCheckDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsCheckDtlByField(ReaBmsCheckDtl entity, string fields)
        {
            IBReaBmsCheckDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCheckDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsCheckDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsCheckDtl.Edit();
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
        //Delele  ReaBmsCheckDtl
        public BaseResultBool ST_UDTO_DelReaBmsCheckDtl(long longReaBmsCheckDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsCheckDtl.Remove(longReaBmsCheckDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtl(ReaBmsCheckDtl entity)
        {
            IBReaBmsCheckDtl.Entity = entity;
            EntityList<ReaBmsCheckDtl> tempEntityList = new EntityList<ReaBmsCheckDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsCheckDtl.Search();
                tempEntityList.count = IBReaBmsCheckDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCheckDtl> tempEntityList = new EntityList<ReaBmsCheckDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    //ZhiFang.Common.Log.Log.Debug("sort:" + sort+ ";GetSortHQL:"+ CommonServiceMethod.GetSortHQL(sort));
                    tempEntityList = IBReaBmsCheckDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsCheckDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsCheckDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsCheckDtl>(tempEntity);
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

        #region ReaBmsQtyBalanceDoc
        //Add  ReaBmsQtyBalanceDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity)
        {
            IBReaBmsQtyBalanceDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsQtyBalanceDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyBalanceDoc.Get(IBReaBmsQtyBalanceDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyBalanceDoc.Entity);
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
        //Update  ReaBmsQtyBalanceDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity)
        {
            IBReaBmsQtyBalanceDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyBalanceDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsQtyBalanceDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDocByField(ReaBmsQtyBalanceDoc entity, string fields)
        {
            IBReaBmsQtyBalanceDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyBalanceDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsQtyBalanceDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsQtyBalanceDoc.Edit();
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
        //Delele  ReaBmsQtyBalanceDoc
        public BaseResultBool ST_UDTO_DelReaBmsQtyBalanceDoc(long longReaBmsQtyBalanceDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyBalanceDoc.Remove(longReaBmsQtyBalanceDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity)
        {
            IBReaBmsQtyBalanceDoc.Entity = entity;
            EntityList<ReaBmsQtyBalanceDoc> tempEntityList = new EntityList<ReaBmsQtyBalanceDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsQtyBalanceDoc.Search();
                tempEntityList.count = IBReaBmsQtyBalanceDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyBalanceDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyBalanceDoc> tempEntityList = new EntityList<ReaBmsQtyBalanceDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsQtyBalanceDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsQtyBalanceDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyBalanceDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsQtyBalanceDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsQtyBalanceDoc>(tempEntity);
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

        #region ReaBmsQtyBalanceDtl
        //Add  ReaBmsQtyBalanceDtl
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity)
        {
            IBReaBmsQtyBalanceDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsQtyBalanceDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyBalanceDtl.Get(IBReaBmsQtyBalanceDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyBalanceDtl.Entity);
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
        //Update  ReaBmsQtyBalanceDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity)
        {
            IBReaBmsQtyBalanceDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyBalanceDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsQtyBalanceDtl
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDtlByField(ReaBmsQtyBalanceDtl entity, string fields)
        {
            IBReaBmsQtyBalanceDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyBalanceDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsQtyBalanceDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsQtyBalanceDtl.Edit();
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
        //Delele  ReaBmsQtyBalanceDtl
        public BaseResultBool ST_UDTO_DelReaBmsQtyBalanceDtl(long longReaBmsQtyBalanceDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyBalanceDtl.Remove(longReaBmsQtyBalanceDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity)
        {
            IBReaBmsQtyBalanceDtl.Entity = entity;
            EntityList<ReaBmsQtyBalanceDtl> tempEntityList = new EntityList<ReaBmsQtyBalanceDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsQtyBalanceDtl.Search();
                tempEntityList.count = IBReaBmsQtyBalanceDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyBalanceDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyBalanceDtl> tempEntityList = new EntityList<ReaBmsQtyBalanceDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsQtyBalanceDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsQtyBalanceDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyBalanceDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsQtyBalanceDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsQtyBalanceDtl>(tempEntity);
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

        #region ReaBmsQtyMonthBalanceDoc
        //Add  ReaBmsQtyMonthBalanceDoc
        public BaseResultDataValue ST_UDTO_AddReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity)
        {
            IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBmsQtyMonthBalanceDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyMonthBalanceDoc.Get(IBReaBmsQtyMonthBalanceDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyMonthBalanceDoc.Entity);
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
        //Update  ReaBmsQtyMonthBalanceDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity)
        {
            IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyMonthBalanceDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBmsQtyMonthBalanceDoc
        public BaseResultBool ST_UDTO_UpdateReaBmsQtyMonthBalanceDocByField(ReaBmsQtyMonthBalanceDoc entity, string fields)
        {
            IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsQtyMonthBalanceDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBmsQtyMonthBalanceDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBmsQtyMonthBalanceDoc.Edit();
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
        //Delele  ReaBmsQtyMonthBalanceDoc
        public BaseResultBool ST_UDTO_DelReaBmsQtyMonthBalanceDoc(long longReaBmsQtyMonthBalanceDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBmsQtyMonthBalanceDoc.Remove(longReaBmsQtyMonthBalanceDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity)
        {
            IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            EntityList<ReaBmsQtyMonthBalanceDoc> tempEntityList = new EntityList<ReaBmsQtyMonthBalanceDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBmsQtyMonthBalanceDoc.Search();
                tempEntityList.count = IBReaBmsQtyMonthBalanceDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyMonthBalanceDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyMonthBalanceDoc> tempEntityList = new EntityList<ReaBmsQtyMonthBalanceDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBmsQtyMonthBalanceDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBmsQtyMonthBalanceDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyMonthBalanceDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBmsQtyMonthBalanceDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBmsQtyMonthBalanceDoc>(tempEntity);
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

        #region ReaUserStorageLink
        //Add  ReaUserStorageLink
        public BaseResultDataValue ST_UDTO_AddReaUserStorageLink(ReaUserStorageLink entity)
        {
            IBReaUserStorageLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaUserStorageLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaUserStorageLink.Get(IBReaUserStorageLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaUserStorageLink.Entity);
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
        //Update  ReaUserStorageLink
        public BaseResultBool ST_UDTO_UpdateReaUserStorageLink(ReaUserStorageLink entity)
        {
            IBReaUserStorageLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaUserStorageLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaUserStorageLink
        public BaseResultBool ST_UDTO_UpdateReaUserStorageLinkByField(ReaUserStorageLink entity, string fields)
        {
            IBReaUserStorageLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaUserStorageLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaUserStorageLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaUserStorageLink.Edit();
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
        //Delele  ReaUserStorageLink
        public BaseResultBool ST_UDTO_DelReaUserStorageLink(long longReaUserStorageLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaUserStorageLink.Remove(longReaUserStorageLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaUserStorageLink(ReaUserStorageLink entity)
        {
            IBReaUserStorageLink.Entity = entity;
            EntityList<ReaUserStorageLink> tempEntityList = new EntityList<ReaUserStorageLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaUserStorageLink.Search();
                tempEntityList.count = IBReaUserStorageLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaUserStorageLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaUserStorageLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaUserStorageLink> tempEntityList = new EntityList<ReaUserStorageLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaUserStorageLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaUserStorageLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaUserStorageLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaUserStorageLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaUserStorageLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaUserStorageLink>(tempEntity);
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

        #region BReport
        //Add  BReport
        public BaseResultDataValue ST_UDTO_AddBReport(BReport entity)
        {
            IBBReport.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBReport.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBReport.Get(IBBReport.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBReport.Entity);
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
        //Update  BReport
        public BaseResultBool ST_UDTO_UpdateBReport(BReport entity)
        {
            IBBReport.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBReport.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BReport
        public BaseResultBool ST_UDTO_UpdateBReportByField(BReport entity, string fields)
        {
            IBBReport.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBReport.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBReport.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBReport.Edit();
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
        //Delele  BReport
        public BaseResultBool ST_UDTO_DelBReport(long longBReportID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBReport.Remove(longBReportID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBReport(BReport entity)
        {
            IBBReport.Entity = entity;
            EntityList<BReport> tempEntityList = new EntityList<BReport>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBReport.Search();
                tempEntityList.count = IBBReport.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BReport>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBReportByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BReport> tempEntityList = new EntityList<BReport>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBReport.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBReport.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BReport>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBReportById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBReport.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BReport>(tempEntity);
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

        #region BTemplate
        //Add  BTemplate
        public BaseResultDataValue ST_UDTO_AddBTemplate(BTemplate entity)
        {
            IBBTemplate.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTemplate.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTemplate.Get(IBBTemplate.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTemplate.Entity);
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
        //Update  BTemplate
        public BaseResultBool ST_UDTO_UpdateBTemplate(BTemplate entity)
        {
            IBBTemplate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTemplate.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTemplate
        public BaseResultBool ST_UDTO_UpdateBTemplateByField(BTemplate entity, string fields)
        {
            IBBTemplate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTemplate.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTemplate.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTemplate.Edit();
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
        //Delele  BTemplate
        public BaseResultBool ST_UDTO_DelBTemplate(long longBTemplateID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTemplate.Remove(longBTemplateID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBTemplate(BTemplate entity)
        {
            IBBTemplate.Entity = entity;
            EntityList<BTemplate> tempEntityList = new EntityList<BTemplate>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTemplate.Search();
                tempEntityList.count = IBBTemplate.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTemplate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTemplate> tempEntityList = new EntityList<BTemplate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTemplate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTemplate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTemplate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBTemplateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTemplate.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTemplate>(tempEntity);
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

        public Stream ST_UDTO_AddTemplateUploadFile()
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();
            BTemplate entity = null;
            string entityStr = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (file.FileName.Length > 0)
                {
                    string[] temp = file.FileName.Split('.');
                    string fileExt = temp[temp.Length - 1].ToLower();
                    if (fileExt != "frx" && fileExt != "xls" && fileExt != "xlsx")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传frx/xls/xlsx格式文件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }
            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "entity":
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            entityStr = HttpContext.Current.Request.Form["entity"].Trim();
                        break;
                    case "fields":
                        break;
                    default:
                        break;
                }
            }
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "登录人信息为空!请登录后再操作!";
            }
            if (baseResultBool.success && string.IsNullOrEmpty(entityStr))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity为空!";
            }
            if (baseResultBool.success == false)
            {
                ZhiFang.Common.Log.Log.Error("新增模板信息出错:" + baseResultBool.ErrorInfo);
            }
            if (baseResultBool.success)
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<BTemplate>(entityStr);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "新增模板信息序列化出错!";
                    ZhiFang.Common.Log.Log.Error("新增模板信息序列化出错:" + ex.Message);
                }
            }

            if (baseResultBool.success)
            {
                entity.DataAddTime = DateTime.Now;
                IBBTemplate.Entity = entity;
                try
                {
                    baseResultBool = IBBTemplate.AddTemplateUploadFile(file);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("新增注册证信息出错2:" + ex.Message);
                    //throw new Exception(ex.Message);
                }
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }

        public Stream ST_UDTO_UpdateTemplateAndUploadFileByField()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            BTemplate entity = null;
            string fields = "";
            string fFileEntity = "";
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            HttpPostedFile file = null;
            int iTotal = HttpContext.Current.Request.Files.Count;
            string strResult = "";
            if (iTotal > 0)
            {
                file = HttpContext.Current.Request.Files[0];
                if (!String.IsNullOrEmpty(file.FileName))
                {
                    string[] temp = file.FileName.Split('.');
                    string fileExt = temp[temp.Length - 1].ToLower();
                    if (fileExt != "frx" && fileExt != "xls" && fileExt != "xlsx")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：只能上传frx/xls/xlsx格式文件!";
                        ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                        strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
                        return ResponseResultStream.GetResultInfoOfStream(strResult);
                    }
                }
            }

            for (int i = 0; i < allkeys.Length; i++)
            {
                switch (allkeys[i])
                {
                    case "fields":
                        if (HttpContext.Current.Request.Form["fields"].Trim() != "")
                            fields = HttpContext.Current.Request.Form["fields"].Trim();
                        break;
                    case "entity"://Entity
                        if (HttpContext.Current.Request.Form["entity"].Trim() != "")
                            fFileEntity = HttpContext.Current.Request.Form["entity"].Trim();
                        break;

                }
            }
            if (!string.IsNullOrEmpty(fFileEntity))
            {
                try
                {
                    entity = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<BTemplate>(fFileEntity);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            if (entity == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity信息为空!";
            }

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (String.IsNullOrEmpty(employeeID))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "没能获取到登录人信息!请登录后再操作!";
            }
            if (baseResultBool.success)
            {
                // entity.DataUpdateTime = DateTime.Now;
                IBBTemplate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTemplate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBBTemplate.UpdateTemplateUploadFileByField(tempArray, file);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        ZhiFang.Common.Log.Log.Error("更新模板信息出错:" + baseResultBool.ErrorInfo);
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("更新模板信息出错:" + baseResultBool.ErrorInfo);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("更新模板信息出错1:" + baseResultBool.ErrorInfo);
            }
            strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }

        public Stream ST_UDTO_DownLoadTemplateFrx(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = "";
                fileStream = IBBTemplate.GetTemplateFileStream(id, ref filename);

                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "模板文件不存在!请重新上传或联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                    return memoryStream;
                }
                else
                {
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                    }
                    return fileStream;
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "预览模板文件错误!" + ex.Message;
                ZhiFang.Common.Log.Log.Error(errorInfo);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        #endregion

        #region CenOrgType
        //Add  CenOrgType
        public BaseResultDataValue ST_UDTO_AddCenOrgType(CenOrgType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBCenOrgType.Add();
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
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgType(CenOrgType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    baseResultBool.success = IBCenOrgType.Edit();
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
        //Update  CenOrgType
        public BaseResultBool ST_UDTO_UpdateCenOrgTypeByField(CenOrgType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBCenOrgType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCenOrgType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBCenOrgType.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBCenOrgType.Edit();
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
        //Delele  CenOrgType
        public BaseResultBool ST_UDTO_DelCenOrgType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBCenOrgType.Entity = IBCenOrgType.Get(id);
                if (IBCenOrgType.Entity != null)
                {
                    long labid = IBCenOrgType.Entity.LabID;
                    string entityName = IBCenOrgType.Entity.GetType().Name;
                    baseResultBool.success = IBCenOrgType.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgType(CenOrgType entity)
        {
            EntityList<CenOrgType> entityList = new EntityList<CenOrgType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBCenOrgType.Entity = entity;
                try
                {
                    entityList.list = IBCenOrgType.Search();
                    entityList.count = IBCenOrgType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<CenOrgType> entityList = new EntityList<CenOrgType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBCenOrgType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBCenOrgType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<CenOrgType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchCenOrgTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBCenOrgType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<CenOrgType>(entity);
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

        #region ReaBusinessInterface
        //Add  ReaBusinessInterface
        public BaseResultDataValue ST_UDTO_AddReaBusinessInterface(ReaBusinessInterface entity)
        {
            IBReaBusinessInterface.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBusinessInterface.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBusinessInterface.Get(IBReaBusinessInterface.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBusinessInterface.Entity);
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
        //Update  ReaBusinessInterface
        public BaseResultBool ST_UDTO_UpdateReaBusinessInterface(ReaBusinessInterface entity)
        {
            IBReaBusinessInterface.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBusinessInterface.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBusinessInterface
        public BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceByField(ReaBusinessInterface entity, string fields)
        {
            IBReaBusinessInterface.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBusinessInterface.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBusinessInterface.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBusinessInterface.Edit();
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
        //Delele  ReaBusinessInterface
        public BaseResultBool ST_UDTO_DelReaBusinessInterface(long longReaBusinessInterfaceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBusinessInterface.Remove(longReaBusinessInterfaceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterface(ReaBusinessInterface entity)
        {
            IBReaBusinessInterface.Entity = entity;
            EntityList<ReaBusinessInterface> tempEntityList = new EntityList<ReaBusinessInterface>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBusinessInterface.Search();
                tempEntityList.count = IBReaBusinessInterface.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBusinessInterface>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBusinessInterface> tempEntityList = new EntityList<ReaBusinessInterface>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBusinessInterface.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBusinessInterface.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBusinessInterface>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBusinessInterface.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBusinessInterface>(tempEntity);
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

        #region ReaBusinessInterfaceLink
        //Add  ReaBusinessInterfaceLink
        public BaseResultDataValue ST_UDTO_AddReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity)
        {
            IBReaBusinessInterfaceLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaBusinessInterfaceLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaBusinessInterfaceLink.Get(IBReaBusinessInterfaceLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBusinessInterfaceLink.Entity);
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
        //Update  ReaBusinessInterfaceLink
        public BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity)
        {
            IBReaBusinessInterfaceLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBusinessInterfaceLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaBusinessInterfaceLink
        public BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceLinkByField(ReaBusinessInterfaceLink entity, string fields)
        {
            IBReaBusinessInterfaceLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBusinessInterfaceLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaBusinessInterfaceLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaBusinessInterfaceLink.Edit();
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
        //Delele  ReaBusinessInterfaceLink
        public BaseResultBool ST_UDTO_DelReaBusinessInterfaceLink(long longReaBusinessInterfaceLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaBusinessInterfaceLink.Remove(longReaBusinessInterfaceLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity)
        {
            IBReaBusinessInterfaceLink.Entity = entity;
            EntityList<ReaBusinessInterfaceLink> tempEntityList = new EntityList<ReaBusinessInterfaceLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaBusinessInterfaceLink.Search();
                tempEntityList.count = IBReaBusinessInterfaceLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBusinessInterfaceLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBusinessInterfaceLink> tempEntityList = new EntityList<ReaBusinessInterfaceLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaBusinessInterfaceLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaBusinessInterfaceLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBusinessInterfaceLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaBusinessInterfaceLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaBusinessInterfaceLink>(tempEntity);
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

        #region ReaEquipTestItemReaGoodLink
        //Add  ReaEquipTestItemReaGoodLink
        public BaseResultDataValue ST_UDTO_AddReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity)
        {
            IBReaEquipTestItemReaGoodLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaEquipTestItemReaGoodLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaEquipTestItemReaGoodLink.Get(IBReaEquipTestItemReaGoodLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaEquipTestItemReaGoodLink.Entity);
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
        //Update  ReaEquipTestItemReaGoodLink
        public BaseResultBool ST_UDTO_UpdateReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity)
        {
            IBReaEquipTestItemReaGoodLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaEquipTestItemReaGoodLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaEquipTestItemReaGoodLink
        public BaseResultBool ST_UDTO_UpdateReaEquipTestItemReaGoodLinkByField(ReaEquipTestItemReaGoodLink entity, string fields)
        {
            IBReaEquipTestItemReaGoodLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaEquipTestItemReaGoodLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaEquipTestItemReaGoodLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaEquipTestItemReaGoodLink.Edit();
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
        //Delele  ReaEquipTestItemReaGoodLink
        public BaseResultBool ST_UDTO_DelReaEquipTestItemReaGoodLink(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaEquipTestItemReaGoodLink.Remove(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity)
        {
            IBReaEquipTestItemReaGoodLink.Entity = entity;
            EntityList<ReaEquipTestItemReaGoodLink> tempEntityList = new EntityList<ReaEquipTestItemReaGoodLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaEquipTestItemReaGoodLink.Search();
                tempEntityList.count = IBReaEquipTestItemReaGoodLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipTestItemReaGoodLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaEquipTestItemReaGoodLink> tempEntityList = new EntityList<ReaEquipTestItemReaGoodLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaEquipTestItemReaGoodLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaEquipTestItemReaGoodLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipTestItemReaGoodLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaEquipTestItemReaGoodLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaEquipTestItemReaGoodLink>(tempEntity);
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

        #region ReaTestEquipItem
        //Add  ReaTestEquipItem
        public BaseResultDataValue ST_UDTO_AddReaTestEquipItem(ReaTestEquipItem entity)
        {
            IBReaTestEquipItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaTestEquipItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaTestEquipItem.Get(IBReaTestEquipItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaTestEquipItem.Entity);
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
        //Update  ReaTestEquipItem
        public BaseResultBool ST_UDTO_UpdateReaTestEquipItem(ReaTestEquipItem entity)
        {
            IBReaTestEquipItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaTestEquipItem
        public BaseResultBool ST_UDTO_UpdateReaTestEquipItemByField(ReaTestEquipItem entity, string fields)
        {
            IBReaTestEquipItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaTestEquipItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaTestEquipItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaTestEquipItem.Edit();
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
        //Delele  ReaTestEquipItem
        public BaseResultBool ST_UDTO_DelReaTestEquipItem(long longReaTestEquipItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestEquipItem.Remove(longReaTestEquipItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipItem(ReaTestEquipItem entity)
        {
            IBReaTestEquipItem.Entity = entity;
            EntityList<ReaTestEquipItem> tempEntityList = new EntityList<ReaTestEquipItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaTestEquipItem.Search();
                tempEntityList.count = IBReaTestEquipItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipItem> tempEntityList = new EntityList<ReaTestEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestEquipItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaTestEquipItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaTestEquipItem>(tempEntity);
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

        #region ReaTestItem
        //Add  ReaTestItem
        public BaseResultDataValue ST_UDTO_AddReaTestItem(ReaTestItem entity)
        {
            IBReaTestItem.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaTestItem.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaTestItem.Get(IBReaTestItem.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaTestItem.Entity);
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
        //Update  ReaTestItem
        public BaseResultBool ST_UDTO_UpdateReaTestItem(ReaTestItem entity)
        {
            IBReaTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestItem.EditVerification();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("Lis项目编码为{0},已存在,请不要重复维护!", entity.LisCode);
                    return tempBaseResultBool;
                }
                tempBaseResultBool.success = IBReaTestItem.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaTestItem
        public BaseResultBool ST_UDTO_UpdateReaTestItemByField(ReaTestItem entity, string fields)
        {
            IBReaTestItem.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestItem.EditVerification();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("Lis项目编码为{0},已存在,请不要重复维护!", entity.LisCode);
                    return tempBaseResultBool;
                }
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaTestItem.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaTestItem.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaTestItem.Edit();
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
        //Delele  ReaTestItem
        public BaseResultBool ST_UDTO_DelReaTestItem(long longReaTestItemID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaTestItem.Remove(longReaTestItemID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaTestItem(ReaTestItem entity)
        {
            IBReaTestItem.Entity = entity;
            EntityList<ReaTestItem> tempEntityList = new EntityList<ReaTestItem>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaTestItem.Search();
                tempEntityList.count = IBReaTestItem.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestItem> tempEntityList = new EntityList<ReaTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestItem>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaTestItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaTestItem.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaTestItem>(tempEntity);
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

        public Message ST_UDTO_UploadTestItemByExcel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    //string deptID = HttpContext.Current.Request.Form["DeptID"];
                    string filepath = Path.Combine(parentPath, ZhiFang.Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    baseResultDataValue = IBReaTestItem.CheckTestItemExcelFormat(filepath, HttpContext.Current.Server.MapPath("~/"));
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue = IBReaTestItem.AddReaTestItemByExcel(filepath, HttpContext.Current.Server.MapPath("~/"));
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
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return System.ServiceModel.Web.WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //
          //检验项目导出
        public Message ST_UDTO_GetTestItemReportExcelPath()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string reportType = "";
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
                    case "reportType":
                        reportType = HttpContext.Current.Request.Form["reportType"];
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
                if (reportType == "1")
                {
                    if (string.IsNullOrEmpty(sort))
                        sort = "[{\"property\":\"ReaTestItem_DispOrder\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "检验项目信息列表";
                    ds = IBReaTestItem.GetReaTestItemInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_ReaTestItem.xml");
                }
                string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + CommonRS.GetExcelExtName();
                string tempFilePath = basePath + "\\TempExcelFile\\" + excelName;
                if (!Directory.Exists(basePath + "\\TempExcelFile"))
                {
                    Directory.CreateDirectory(basePath + "\\TempExcelFile");
                }
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string headerText = "";
                    if (isHeader == "1")
                        headerText = tempFileName;
                    if (!ExcelHelper.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
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

        #endregion

        #region ReaAlertInfoSettings
        //Add  ReaAlertInfoSettings
        public BaseResultDataValue ST_UDTO_AddReaAlertInfoSettings(ReaAlertInfoSettings entity)
        {
            IBReaAlertInfoSettings.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaAlertInfoSettings.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaAlertInfoSettings.Get(IBReaAlertInfoSettings.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaAlertInfoSettings.Entity);
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
        //Update  ReaAlertInfoSettings
        public BaseResultBool ST_UDTO_UpdateReaAlertInfoSettings(ReaAlertInfoSettings entity)
        {
            IBReaAlertInfoSettings.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaAlertInfoSettings.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaAlertInfoSettings
        public BaseResultBool ST_UDTO_UpdateReaAlertInfoSettingsByField(ReaAlertInfoSettings entity, string fields)
        {
            IBReaAlertInfoSettings.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaAlertInfoSettings.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaAlertInfoSettings.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaAlertInfoSettings.Edit();
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
        //Delele  ReaAlertInfoSettings
        public BaseResultBool ST_UDTO_DelReaAlertInfoSettings(long longReaAlertInfoSettingsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaAlertInfoSettings.Remove(longReaAlertInfoSettingsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettings(ReaAlertInfoSettings entity)
        {
            IBReaAlertInfoSettings.Entity = entity;
            EntityList<ReaAlertInfoSettings> tempEntityList = new EntityList<ReaAlertInfoSettings>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaAlertInfoSettings.Search();
                tempEntityList.count = IBReaAlertInfoSettings.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaAlertInfoSettings>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettingsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaAlertInfoSettings> tempEntityList = new EntityList<ReaAlertInfoSettings>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaAlertInfoSettings.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaAlertInfoSettings.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaAlertInfoSettings>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettingsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaAlertInfoSettings.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaAlertInfoSettings>(tempEntity);
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

        #region ReaMonthUsageStatisticsDoc
        //Add  ReaMonthUsageStatisticsDoc
        public BaseResultDataValue ST_UDTO_AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity)
        {
            IBReaMonthUsageStatisticsDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaMonthUsageStatisticsDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaMonthUsageStatisticsDoc.Get(IBReaMonthUsageStatisticsDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaMonthUsageStatisticsDoc.Entity);
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
        //Update  ReaMonthUsageStatisticsDoc
        public BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity)
        {
            IBReaMonthUsageStatisticsDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaMonthUsageStatisticsDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaMonthUsageStatisticsDoc
        public BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDocByField(ReaMonthUsageStatisticsDoc entity, string fields)
        {
            IBReaMonthUsageStatisticsDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaMonthUsageStatisticsDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaMonthUsageStatisticsDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaMonthUsageStatisticsDoc.Edit();
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
        //Delele  ReaMonthUsageStatisticsDoc
        public BaseResultBool ST_UDTO_DelReaMonthUsageStatisticsDoc(long longReaMonthUsageStatisticsDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaMonthUsageStatisticsDoc.Remove(longReaMonthUsageStatisticsDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity)
        {
            IBReaMonthUsageStatisticsDoc.Entity = entity;
            EntityList<ReaMonthUsageStatisticsDoc> tempEntityList = new EntityList<ReaMonthUsageStatisticsDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaMonthUsageStatisticsDoc.Search();
                tempEntityList.count = IBReaMonthUsageStatisticsDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaMonthUsageStatisticsDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaMonthUsageStatisticsDoc> tempEntityList = new EntityList<ReaMonthUsageStatisticsDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaMonthUsageStatisticsDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaMonthUsageStatisticsDoc.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaMonthUsageStatisticsDoc>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaMonthUsageStatisticsDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaMonthUsageStatisticsDoc>(tempEntity);
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

        #region ReaMonthUsageStatisticsDtl
        //Add  ReaMonthUsageStatisticsDtl
        public BaseResultDataValue ST_UDTO_AddReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity)
        {
            IBReaMonthUsageStatisticsDtl.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaMonthUsageStatisticsDtl.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaMonthUsageStatisticsDtl.Get(IBReaMonthUsageStatisticsDtl.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaMonthUsageStatisticsDtl.Entity);
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
        //Update  ReaMonthUsageStatisticsDtl
        public BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity)
        {
            IBReaMonthUsageStatisticsDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaMonthUsageStatisticsDtl.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaMonthUsageStatisticsDtl
        public BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDtlByField(ReaMonthUsageStatisticsDtl entity, string fields)
        {
            IBReaMonthUsageStatisticsDtl.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaMonthUsageStatisticsDtl.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaMonthUsageStatisticsDtl.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaMonthUsageStatisticsDtl.Edit();
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
        //Delele  ReaMonthUsageStatisticsDtl
        public BaseResultBool ST_UDTO_DelReaMonthUsageStatisticsDtl(long longReaMonthUsageStatisticsDtlID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaMonthUsageStatisticsDtl.Remove(longReaMonthUsageStatisticsDtlID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity)
        {
            IBReaMonthUsageStatisticsDtl.Entity = entity;
            EntityList<ReaMonthUsageStatisticsDtl> tempEntityList = new EntityList<ReaMonthUsageStatisticsDtl>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaMonthUsageStatisticsDtl.Search();
                tempEntityList.count = IBReaMonthUsageStatisticsDtl.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaMonthUsageStatisticsDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaMonthUsageStatisticsDtl> tempEntityList = new EntityList<ReaMonthUsageStatisticsDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaMonthUsageStatisticsDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaMonthUsageStatisticsDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaMonthUsageStatisticsDtl>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtlById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaMonthUsageStatisticsDtl.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaMonthUsageStatisticsDtl>(tempEntity);
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

        #region ReaLisTestStatisticalResults
        //Add  ReaLisTestStatisticalResults
        public BaseResultDataValue ST_UDTO_AddReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity)
        {
            IBReaLisTestStatisticalResults.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaLisTestStatisticalResults.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaLisTestStatisticalResults.Get(IBReaLisTestStatisticalResults.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaLisTestStatisticalResults.Entity);
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
        //Update  ReaLisTestStatisticalResults
        public BaseResultBool ST_UDTO_UpdateReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity)
        {
            IBReaLisTestStatisticalResults.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaLisTestStatisticalResults.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaLisTestStatisticalResults
        public BaseResultBool ST_UDTO_UpdateReaLisTestStatisticalResultsByField(ReaLisTestStatisticalResults entity, string fields)
        {
            IBReaLisTestStatisticalResults.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaLisTestStatisticalResults.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaLisTestStatisticalResults.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaLisTestStatisticalResults.Edit();
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
        //Delele  ReaLisTestStatisticalResults
        public BaseResultBool ST_UDTO_DelReaLisTestStatisticalResults(long longReaLisTestStatisticalResultsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaLisTestStatisticalResults.Remove(longReaLisTestStatisticalResultsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity)
        {
            IBReaLisTestStatisticalResults.Entity = entity;
            EntityList<ReaLisTestStatisticalResults> tempEntityList = new EntityList<ReaLisTestStatisticalResults>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaLisTestStatisticalResults.Search();
                tempEntityList.count = IBReaLisTestStatisticalResults.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaLisTestStatisticalResults>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResultsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaLisTestStatisticalResults> tempEntityList = new EntityList<ReaLisTestStatisticalResults>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaLisTestStatisticalResults.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaLisTestStatisticalResults.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaLisTestStatisticalResults>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResultsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaLisTestStatisticalResults.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaLisTestStatisticalResults>(tempEntity);
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

        #region BUserUIConfig
        //Add  BUserUIConfig
        public BaseResultDataValue ST_UDTO_AddBUserUIConfig(BUserUIConfig entity)
        {
            IBBUserUIConfig.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBUserUIConfig.Add();
                ZhiFang.Common.Log.Log.Info("ST_UDTO_AddBUserUIConfig:" + entity.Id);
                if (tempBaseResultDataValue.success)
                {
                    BUserUIConfig entity2 = IBBUserUIConfig.Get(entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity2);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddBUserUIConfig:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        //Update  BUserUIConfig
        public BaseResultBool ST_UDTO_UpdateBUserUIConfig(BUserUIConfig entity)
        {
            IBBUserUIConfig.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBUserUIConfig.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BUserUIConfig
        public BaseResultBool ST_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields)
        {
            ZhiFang.Common.Log.Log.Info("ST_UDTO_AddBUserUIConfig:" + entity.Id);
            IBBUserUIConfig.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBUserUIConfig.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBUserUIConfig.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBUserUIConfig.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_UpdateBUserUIConfigByField:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        //Delele  BUserUIConfig
        public BaseResultBool ST_UDTO_DelBUserUIConfig(long longBUserUIConfigID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBUserUIConfig.Remove(longBUserUIConfigID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBUserUIConfig(BUserUIConfig entity)
        {
            IBBUserUIConfig.Entity = entity;
            EntityList<BUserUIConfig> tempEntityList = new EntityList<BUserUIConfig>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBUserUIConfig.Search();
                tempEntityList.count = IBBUserUIConfig.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BUserUIConfig>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BUserUIConfig> tempEntityList = new EntityList<BUserUIConfig>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBUserUIConfig.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBUserUIConfig.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BUserUIConfig>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBUserUIConfig.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BUserUIConfig>(tempEntity);
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

        #region ReaStorageGoodsLink
        //Add  ReaStorageGoodsLink
        public BaseResultDataValue ST_UDTO_AddReaStorageGoodsLink(ReaStorageGoodsLink entity)
        {
            entity.DataUpdateTime = DateTime.Now;
            IBReaStorageGoodsLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaStorageGoodsLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaStorageGoodsLink.Get(IBReaStorageGoodsLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaStorageGoodsLink.Entity);
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
        //Update  ReaStorageGoodsLink
        public BaseResultBool ST_UDTO_UpdateReaStorageGoodsLink(ReaStorageGoodsLink entity)
        {
            IBReaStorageGoodsLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaStorageGoodsLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaStorageGoodsLink
        public BaseResultBool ST_UDTO_UpdateReaStorageGoodsLinkByField(ReaStorageGoodsLink entity, string fields)
        {
            IBReaStorageGoodsLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaStorageGoodsLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaStorageGoodsLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaStorageGoodsLink.Edit();
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
        //Delele  ReaStorageGoodsLink
        public BaseResultBool ST_UDTO_DelReaStorageGoodsLink(long longReaStorageGoodsLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaStorageGoodsLink.Remove(longReaStorageGoodsLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLink(ReaStorageGoodsLink entity)
        {
            IBReaStorageGoodsLink.Entity = entity;
            EntityList<ReaStorageGoodsLink> tempEntityList = new EntityList<ReaStorageGoodsLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaStorageGoodsLink.Search();
                tempEntityList.count = IBReaStorageGoodsLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaStorageGoodsLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaStorageGoodsLink> tempEntityList = new EntityList<ReaStorageGoodsLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaStorageGoodsLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaStorageGoodsLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaStorageGoodsLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaStorageGoodsLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaStorageGoodsLink>(tempEntity);
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
