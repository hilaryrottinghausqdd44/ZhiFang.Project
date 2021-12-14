using System;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LabStarCommService : ILabStarCommService
    {
        #region

        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquip IBLBEquip { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSampleType IBLBSampleType { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCItem IBLBQCItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCMaterial IBLBQCMaterial { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipForm IBLisEquipForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipItem IBLisEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipComFile IBLisEquipComFile { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipComLog IBLisEquipComLog { get; set; }

        #endregion

        #region LisEquipForm
        //Add  LisEquipForm
        public BaseResultDataValue LS_UDTO_AddLisEquipForm(LisEquipForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipForm.Add();
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
        //Update  LisEquipForm
        public BaseResultBool LS_UDTO_UpdateLisEquipForm(LisEquipForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipForm.Edit();
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
        //Update  LisEquipForm
        public BaseResultBool LS_UDTO_UpdateLisEquipFormByField(LisEquipForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipForm.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipForm.Edit();
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
        //Delele  LisEquipForm
        public BaseResultBool LS_UDTO_DelLisEquipForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipForm.Entity = IBLisEquipForm.Get(id);
                if (IBLisEquipForm.Entity != null)
                {
                    long labid = IBLisEquipForm.Entity.LabID;
                    string entityName = IBLisEquipForm.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipForm.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipForm(LisEquipForm entity)
        {
            EntityList<LisEquipForm> entityList = new EntityList<LisEquipForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipForm.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipForm.Search();
                    entityList.count = IBLisEquipForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipForm> entityList = new EntityList<LisEquipForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipForm>(entity);
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

        #region LisEquipItem
        //Add  LisEquipItem
        public BaseResultDataValue LS_UDTO_AddLisEquipItem(LisEquipItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipItem.Add();
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
        //Update  LisEquipItem
        public BaseResultBool LS_UDTO_UpdateLisEquipItem(LisEquipItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipItem.Edit();
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
        //Update  LisEquipItem
        public BaseResultBool LS_UDTO_UpdateLisEquipItemByField(LisEquipItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipItem.Edit();
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
        //Delele  LisEquipItem
        public BaseResultBool LS_UDTO_DelLisEquipItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipItem.Entity = IBLisEquipItem.Get(id);
                if (IBLisEquipItem.Entity != null)
                {
                    long labid = IBLisEquipItem.Entity.LabID;
                    string entityName = IBLisEquipItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItem(LisEquipItem entity)
        {
            EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipItem.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipItem.Search();
                    entityList.count = IBLisEquipItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipItem>(entity);
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

        #region 定制服务

        /// <summary>
        /// 上传仪器参数文件
        /// </summary>
        /// <returns></returns>
        public Message LS_UDTO_UpLoadEquipCommParaFile()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string equipID = "";
                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "equipID":
                            if (HttpContext.Current.Request.Form["equipID"].Trim() != "")
                                equipID = HttpContext.Current.Request.Form["equipID"].Trim();
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(equipID))
                {
                    LBEquip equip = IBLBEquip.Get(long.Parse(equipID));

                    if (equip != null)
                    {
                        int iTotal = HttpContext.Current.Request.Files.Count;
                        if (iTotal > 0)
                        {
                            HttpPostedFile file = HttpContext.Current.Request.Files[0];
                            int len = file.ContentLength;
                            if (len > 0)
                            {
                                string equipName = equip.CName;
                                string fileDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "\\TempFile\\UploadFiles\\CommParaFile";
                                if (!Directory.Exists(fileDirectory))
                                {
                                    Directory.CreateDirectory(fileDirectory);
                                }
                                string fileName = equipName + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                                string filePathName = Path.Combine(fileDirectory, Path.GetFileName(fileName));
                                file.SaveAs(filePathName);
                                using (FileStream fs = File.OpenRead(filePathName))
                                {
                                    string strTxtContent = "";
                                    byte[] tempByte = new byte[fs.Length];
                                    UTF8Encoding tempUTF8 = new UTF8Encoding(true);
                                    while (fs.Read(tempByte, 0, tempByte.Length) > 0)
                                    {
                                        strTxtContent += tempUTF8.GetString(tempByte);
                                    }
                                    brdv = IBLBEquip.EditLBEquipCommPara(equip.Id, strTxtContent);
                                }
                            }
                            else
                            {
                                brdv.ErrorInfo = "上传的仪器参数文件无效！";
                                brdv.success = false;
                            }
                        }
                        else
                        {
                            brdv.ErrorInfo = "无法获取相应的仪器信息！";
                            brdv.success = false;
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "仪器ID参数不能为空！";
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "上传仪器参数文件时发生错误，原因为：<br>" + ex.Message;
                brdv.success = false;
                ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_UpLoadEquipCommParaFile：" + ex.ToString());
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 下载仪器参数文件
        /// </summary>
        /// <param name="equipID">仪器ID</param>
        /// <param name="contentType">参数内容类型</param>
        /// <param name="operateType">0为下载，1为打开预览</param>
        /// <returns></returns>
        public Stream LS_UDTO_DownLoadEquipCommParaFile(long equipID, long contentType, int operateType)
        {
            try
            {
                FileStream tempFileStream = null;
                string empD = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                ///if (!string.IsNullOrEmpty(empD))
                {
                    string fileDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "\\TempFile\\CommParaFile";
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    LBEquip equip = IBLBEquip.Get(equipID);

                    if (equip != null)
                    {
                        string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        string filePathName = Path.Combine(fileDirectory, Path.GetFileName(fileName));
                        using (FileStream fsTXT = new FileStream(filePathName, FileMode.Create))
                        {
                            using (StreamWriter sw = new StreamWriter(fsTXT))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append("{");
                                switch (contentType)
                                {
                                    case 1:
                                        sb.Append("\"CommInfo\":" + (string.IsNullOrWhiteSpace(equip.CommInfo) ? "\"\"" : equip.CommInfo));
                                        break;
                                    case 2:
                                        sb.Append("\"CommPara\":" + (string.IsNullOrWhiteSpace(equip.CommPara) ? "\"\"" : equip.CommPara));
                                        break;
                                    case 3:
                                        sb.Append("\"CommSys\":" + (string.IsNullOrWhiteSpace(equip.CommSys) ? "\"\"" : equip.CommSys));
                                        break;
                                    default:
                                        sb.Append("\"CommInfo\":" + (string.IsNullOrWhiteSpace(equip.CommInfo) ? "\"\"" : equip.CommInfo) + ",");
                                        sb.Append("\"CommPara\":" + (string.IsNullOrWhiteSpace(equip.CommPara) ? "\"\"" : equip.CommPara) + ",");
                                        sb.Append("\"CommSys\":" + (string.IsNullOrWhiteSpace(equip.CommSys) ? "\"\"" : equip.CommSys));
                                        break;
                                }
                                sb.Append("}");
                                sw.Write(sb.ToString());
                            }

                            string tempFileName = "EquipPara.txt";
                            tempFileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read);

                            Encoding code = Encoding.GetEncoding("gb2312");
                            System.Web.HttpContext.Current.Response.ContentEncoding = code;
                            System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                            if (operateType == 0) //下载文件
                            {
                                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                                //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                                //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                            }
                            else if (operateType == 1)//直接打开PDF文件
                            {
                                WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                                //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                                //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                            }
                        }
                    }
                }
                //else
                //{
                //    ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_GetEquipCommParaInfo：登录超时，请重新登录！");
                //    throw new Exception("登录超时，请重新登录！");
                //}
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "LS_UDTO_GetEquipCommParaInfo异常：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_GetEquipCommParaInfo异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        /// <summary>
        /// 仪器检验结果数据上传服务
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_UpLoadEquipResultInfo()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //实验室ID
                string labID = "";
                //仪器文件结果类型
                string equipResultFileName = "";
                //仪器文件结果类型
                string equipResultType = "";
                //仪器文件结果记录数
                string equipResultCount = "";
                //仪器文件结果
                string equipResultInfo = "";
                //客户端信息
                string clientInfo = "";

                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                if (allkeys != null && allkeys.Length > 0)
                {
                    for (int i = 0; i < allkeys.Length; i++)
                    {
                        switch (allkeys[i])
                        {
                            case "labID":
                                if (HttpContext.Current.Request.Form["labID"].Trim() != "")
                                    labID = HttpContext.Current.Request.Form["labID"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数labID:" + labID);
                                break;
                            case "clientInfo":
                                if (HttpContext.Current.Request.Form["clientInfo"].Trim() != "")
                                    clientInfo = HttpContext.Current.Request.Form["clientInfo"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数clientInfo:" + clientInfo);
                                break;
                            case "equipResultFileName":
                                if (HttpContext.Current.Request.Form["equipResultFileName"].Trim() != "")
                                    equipResultFileName = HttpContext.Current.Request.Form["equipResultFileName"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数equipResultFileName:" + equipResultFileName);
                                break;
                            case "equipResultType":
                                if (HttpContext.Current.Request.Form["equipResultType"].Trim() != "")
                                    equipResultType = HttpContext.Current.Request.Form["equipResultType"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数equipResultType:" + equipResultType);
                                break;
                            case "equipResultCount":
                                if (HttpContext.Current.Request.Form["equipResultCount"].Trim() != "")
                                    equipResultCount = HttpContext.Current.Request.Form["equipResultCount"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数equipResultCount:" + equipResultCount);
                                break;
                            case "equipResultInfo":
                                if (HttpContext.Current.Request.Form["equipResultInfo"].Trim() != "")
                                    equipResultInfo = HttpContext.Current.Request.Form["equipResultInfo"].Trim();
                                ZhiFang.LabStar.Common.LogHelp.Info("LS_UDTO_UpLoadEquipResultInfo服务参数equipResultInfo:" + equipResultInfo);
                                break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(labID))
                        labID = "0";
                    if (long.Parse(labID) > 0)
                    {
                        HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = labID;//实验室ID
                        HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "1";

                        if (equipResultInfo != null && equipResultInfo.Length > 0)
                        {
                            //baseResultDataValue = IBLisEquipForm.AppendLisEquipItemResultByUpLoadInfo(long.Parse(labID), equipResultType, equipResultInfo, SendCommDataDelegate);
                            baseResultDataValue = AppendLisEquipItemResultByUpLoadInfo(long.Parse(labID), equipResultFileName, equipResultType, equipResultInfo, equipResultCount, clientInfo);
                        }
                        else
                        {
                            baseResultDataValue.ErrorInfo = "上传的通讯检验结果信息为空！";
                            baseResultDataValue.success = false;
                            ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                        }
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "实验室ID信息不正确，请正确配置ID后再上传文件！";
                        baseResultDataValue.success = false;
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "无法获取仪器检验结果信息参数！";
                    baseResultDataValue.success = false;
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = "上传仪器检验结果信息时发生错误：" + ex.Message;
                baseResultDataValue.success = false;
                ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_UpLoadEquipResultInfo：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 仪器检验结果文件上传
        /// </summary>
        /// <param name="labID">实验室ID</param>
        /// <param name="equipResultFileName">文件名称</param>
        /// <param name="equipResultType">检验结果类型</param>
        /// <param name="equipResultInfo">检验结果文件内容Json字符串</param>>
        /// <param name="equipResultCount">检验结果记录数</param>
        /// <param name="clientInfo">客户端相关信息（计算机名、网卡号，IP等信息）</param>
        /// <returns></returns>
        public BaseResultDataValue AppendLisEquipItemResultByUpLoadInfo(long labID, string equipResultFileName, string equipResultType, string equipResultInfo, string equipResultCount, string clientInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            ClientComputerInfo computerInfo = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ClientComputerInfo>(clientInfo);
            computerInfo.ComFileName = equipResultFileName;

            if (string.IsNullOrWhiteSpace(equipResultInfo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -1;
                baseResultDataValue.ErrorInfo = CommFileHintVar.Hint_CommFileInfoIsEmpty;
                IBLisEquipComLog.AddLisEquipComLog(0, CommFileHintVar.Hint_CommFileInfoIsEmpty, computerInfo);
                return baseResultDataValue;
            }

            int resultCount = 0;
            int.TryParse(equipResultCount, out resultCount);

            BaseResultDataValue baseResult = IBLisEquipComFile.AddLisEquipComFile(equipResultType, equipResultInfo, resultCount, computerInfo);
            long comFileID = 0;
            if (baseResult.success)
            {
                long.TryParse(baseResult.ResultDataValue, out comFileID);
                computerInfo.SComFileID = comFileID;
                IBLisEquipComLog.AddLisEquipComLog(0, CommFileHintVar.Hint_AddCommFile + "ID:" + comFileID, computerInfo);
            }
            
            if (equipResultType == "Common_Result")
            {
                IList<EquipResult> listEquipResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipResult>>(equipResultInfo);
                baseResultDataValue = IBLisEquipForm.AddLisEquipItemResult_Common(labID, listEquipResult, computerInfo, SendSysMessage.SendSysMessageDelegate);
            }
            else if (equipResultType == "Memo_Result")
            {
                IList<EquipMemoResult> listEquipMemoResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipMemoResult>>(equipResultInfo);
                baseResultDataValue = IBLisEquipForm.AddLisEquipItemResult_Memo(comFileID, labID, listEquipMemoResult, computerInfo, SendSysMessage.SendSysMessageDelegate);
            }
            else if (equipResultType == "Graph_Result")
            {
                IList<EquipGraphResult> listEquipGraphResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipGraphResult>>(equipResultInfo);
                baseResultDataValue = IBLisEquipForm.AppendLisEquipItemResult_Graph(comFileID, labID, listEquipGraphResult, computerInfo, SendSysMessage.SendSysMessageDelegate);
            }
            else if (equipResultType == "QC_Result")
            {
                IList<EquipQCResult> listEquipQCResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipQCResult>>(equipResultInfo);
                baseResultDataValue = IBLisEquipForm.AddLisEquipItemResult_QC(comFileID, labID, listEquipQCResult, computerInfo, SendSysMessage.SendSysMessageDelegate);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 通讯管理程序基础数据同步服务

        public BaseResultDataValue LS_Sync_SearchLBEquipByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquip> entityList = new EntityList<LBEquip>();
            try
            {
                SetLabIDCookie(labID);
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquip.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquip.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquip>(entityList);
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

        public BaseResultDataValue LS_Sync_QueryLBEquipItemByHQL(string labID, string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBEquipItem> entityList = new EntityList<LBEquipItem>();
            try
            {
                SetLabIDCookie(labID);
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBEquipItem.QueryLBEquipItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBEquipItem.QueryLBEquipItem(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBEquipItem>(entityList);
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

        public BaseResultDataValue LS_Sync_SearchLBSampleTypeByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSampleType> entityList = new EntityList<LBSampleType>();
            try
            {
                SetLabIDCookie(labID);
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSampleType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSampleType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSampleType>(entityList);
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

        public BaseResultDataValue QC_Sync_QueryLBQCItemByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItem> entityList = new EntityList<LBQCItem>();
            try
            {
                SetLabIDCookie(labID);
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItem.QueryLBQCItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItem.QueryLBQCItem(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItem>(entityList);
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

        public BaseResultDataValue QC_Sync_SearchLBQCMaterialByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCMaterial> entityList = new EntityList<LBQCMaterial>();
            try
            {
                SetLabIDCookie(labID);
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCMaterial.QueryLBQCMaterial(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCMaterial.QueryLBQCMaterial(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCMaterial>(entityList);
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

        private void SetLabIDCookie(string labID)
        {
            if (string.IsNullOrWhiteSpace(labID))
                labID = "0";
            if (long.Parse(labID) > 0)
            {
                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = labID;//实验室ID
                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "1";
            }
        }
        #endregion

    }
}
