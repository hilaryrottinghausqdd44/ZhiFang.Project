using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.BLL.Common.News;
using ZhiFang.Common.Public;
using ZhiFang.Model;
using ZhiFang.Tools;
using ZhiFang.WebLis.ServerContract;

namespace ZhiFang.WebLis.ServiceWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NewsService : INewsService
    {
        BNNews BNNews = new BNNews();
        BNNews_Attachment BNNews_Attachment = new BNNews_Attachment();
        BNNews_CopyUser BNNews_CopyUser = new BNNews_CopyUser();
        BNNews_Interaction BNNews_Interaction = new BNNews_Interaction();
        BNNews_Operation BNNews_Operation = new BNNews_Operation();
        BNNews_ReadingLog BNNews_ReadingLog = new BNNews_ReadingLog();
        BNNews_ReadingUser BNNews_ReadingUser = new BNNews_ReadingUser();
        BNNews_Type BNNews_Type = new BNNews_Type();
        BNNews_Area BNNews_Area = new BNNews_Area();
        BNNewsAreaClientLink BNNewsAreaClientLink = new BNNewsAreaClientLink();
        BNNewsAreaLink BNNewsAreaLink = new BNNewsAreaLink();

        #region NNews
        //Add  NNews
        public BaseResultDataValue ST_UDTO_AddNNews(N_News entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                DateTime StartDateTime;
                DateTime EndDateTime;
                if (DateTime.TryParse(entity.StartDateTime, out StartDateTime))
                    entity.BeginTime = StartDateTime;
                if (DateTime.TryParse(entity.EndDateTime, out EndDateTime))
                    entity.EndTime = EndDateTime;

                entity.FileID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.DrafterDateTime = DateTime.Now;
                entity.IsUse = 1;
                entity.Status = int.Parse(NNewsStatus.起草.Key);
                if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                entity.DrafterId = long.Parse(Cookie.CookieHelper.Read("ZhiFangUserID"));
                entity.DrafterCName = Cookie.CookieHelper.Read("EmployeeName");
                try
                {
                    baseResultDataValue.success = BNNews.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.FileID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNews
        public BaseResultBool ST_UDTO_UpdateNNews(N_News entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    entity.Status = int.Parse(NNewsStatus.起草.Key);
                    entity.StatusName = NNewsStatus.起草.Value.Name;
                    baseResultBool.success = BNNews.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  NNews

        //Delele  NNews
        public BaseResultBool ST_UDTO_DelNNews(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                N_News entity = BNNews.GetModel(id);
                if (entity != null)
                {
                    entity.IsUse = 0;
                    baseResultBool.success = BNNews.Update(entity);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }
        public BaseResultBool ST_UDTO_EnableNNews(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                N_News entity = BNNews.GetModel(id);
                if (entity != null)
                {
                    entity.IsUse = 1;
                    baseResultBool.success = BNNews.Update(entity);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News> entityList = new EntityListEasyUI<N_News>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews.GetModelList(where, page, rows);
                }

                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsByDrafter(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<N_News> entityList = new EntityListEasyUI<N_News>();
            string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
            string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
            if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
            {
                brdv.ErrorInfo = "请登录！";
                brdv.success = false;
                return brdv;
            }
            where = (where == null || where.Trim() == "") ? " DrafterId=" + ZhiFangUserID : where + " and DrafterId=" + ZhiFangUserID;
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews.GetModelList(where, page, rows);
                }

                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsByApproval(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<N_News> entityList = new EntityListEasyUI<N_News>();
            string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
            string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
            if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
            {
                brdv.ErrorInfo = "请登录！";
                brdv.success = false;
                return brdv;
            }
            where = (where == null || where.Trim() == "") ? " ApprovalId=" + ZhiFangUserID : where + " and Approval=" + ZhiFangUserID;
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews.GetModelList(where, page, rows);
                }

                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsByPublish(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<N_News> entityList = new EntityListEasyUI<N_News>();
            string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
            string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
            if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
            {
                brdv.ErrorInfo = "请登录！";
                brdv.success = false;
                return brdv;
            }
            where = (where == null || where.Trim() == "") ? " Status in (" + ZhiFang.Model.NNewsStatus.审批通过.Key + "," + ZhiFang.Model.NNewsStatus.发布.Key + "," + ZhiFang.Model.NNewsStatus.发布撤回.Key + ")" : where + " and  Status in (" + ZhiFang.Model.NNewsStatus.审批通过.Key + "," + ZhiFang.Model.NNewsStatus.发布.Key + "," + ZhiFang.Model.NNewsStatus.发布撤回.Key + ")";
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews.GetModelList(where, page, rows);
                }

                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsList(int page, int rows, string fields, string startdatetime, string enddatetime, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<N_News> entityList = new EntityListEasyUI<N_News>();
            try
            {
                string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
                string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
                if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
                {
                    brdv.ErrorInfo = "请登录！";
                    brdv.success = false;
                    return brdv;
                }

                ZhiFang.BLL.Common.BaseDictionary.BusinessLogicClientControl bcc = new BLL.Common.BaseDictionary.BusinessLogicClientControl();
                DataSet dsclientlist = bcc.GetList(new BusinessLogicClientControl() { Account = ZhiFangUser, SelectedFlag = true });
                List<string> clientnolist = new List<string>();
                if (dsclientlist != null && dsclientlist.Tables.Count > 0 && dsclientlist.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsclientlist.Tables[0].Rows.Count; i++)
                    {
                        clientnolist.Add(dsclientlist.Tables[0].Rows[i]["CLIENTNO"].ToString());
                    }
                }
                string tmpsql = "";
                if (clientnolist.Count > 0)
                {
                    BNNewsAreaClientLink bnarea = new BNNewsAreaClientLink();
                    var arealist = bnarea.GetModelList(" ClientNo in (" + string.Join(",", clientnolist) + ") ");
                    if (arealist != null && arealist.Count > 0)
                    {
                        List<string> areaidlist = new List<string>();
                        var alist = arealist.GroupBy(a => a.NewsAreaId);
                        foreach (var a in alist)
                        {
                            areaidlist.Add(a.Key.ToString());
                        }
                        tmpsql = " or ( FileID in ( select NewsId from N_NewsAreaLink where NewsAreaId in (" + string.Join(",", areaidlist) + ") ) ) ";
                    }
                }
                string tmpwhere = " Status=" + ZhiFang.Model.NNewsStatus.发布.Key + "  and (FileID not in (select NewsId from N_NewsAreaLink) " + tmpsql + ") and                     ((EndTime >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and BeginTime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') or (EndTime >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and BeginTime is null) or (EndTime is null and BeginTime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') or (EndTime is null and BeginTime is null )) ";

                if (startdatetime != null && startdatetime.Trim() != "")
                {
                    tmpwhere += " and PublisherDateTime>='" + startdatetime + "'";
                }

                if (enddatetime != null && enddatetime.Trim() != "")
                {
                    tmpwhere += " and PublisherDateTime<='" + enddatetime + " 23:59:59'";
                }

                where = (where == null || where.Trim() == "") ? tmpwhere : where + " and  " + tmpwhere;
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews.ST_UDTO_SearchNNewsList(where, sort, page, rows, ZhiFangUserID);
                }
                else
                {
                    entityList = BNNews.ST_UDTO_SearchNNewsList(where, null, page, rows, ZhiFangUserID);
                }

                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                return baseResultDataValue;
            }
            if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                return baseResultDataValue;
            }
            string empid = Cookie.CookieHelper.Read("ZhiFangUserID");
            string empname = Cookie.CookieHelper.Read("EmployeeName");
            try
            {
                var entity = BNNews.GetModel(id);
                if (entity.Status.ToString() == ZhiFang.Model.NNewsStatus.发布.Key)
                {
                    if (BNNews_ReadingLog.GetCount("FileID=" + id + " and ReaderID=" + empid) > 0)
                    {
                        BNNews_ReadingLog.UpdateTimes("FileID=" + id + " and ReaderID=" + empid);
                    }
                    else
                    {
                        BNNews_ReadingLog.Add(new N_News_ReadingLog() { FileReadingLogID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(), FileID = id, ReaderID = long.Parse(empid), ReaderName = empname, ReadTimes = 1, CreatorID = long.Parse(empid), CreatorName = empname, DataAddTime = DateTime.Now, IsUse = true });
                    }
                }

                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_NNewsApproval(string idlist, string Memo, bool ApprovalFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                string empid = Cookie.CookieHelper.Read("ZhiFangUserID");
                string empname = Cookie.CookieHelper.Read("EmployeeName");
                baseResultDataValue = BNNews.NNewsApproval(idlist, ApprovalFlag, Memo, empid, empname);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_NNewsPublish(string idlist, string Memo, bool PublishFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未能读取登录者信息！";
                    return baseResultDataValue;
                }
                string empid = Cookie.CookieHelper.Read("ZhiFangUserID");
                string empname = Cookie.CookieHelper.Read("EmployeeName");
                baseResultDataValue = BNNews.NNewsPublish(idlist, PublishFlag, Memo, empid, empname);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        #endregion

        #region NNewsAttachment
        //Add  NNewsAttachment
        public BaseResultDataValue ST_UDTO_AddNNewsAttachment(N_News_Attachment entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = BNNews_Attachment.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.FileAttachmentID.ToString();
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsAttachment
        public BaseResultBool ST_UDTO_UpdateNNewsAttachment(N_News_Attachment entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultBool.success = BNNews_Attachment.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  NNewsAttachment
        public BaseResultBool ST_UDTO_DelNNewsAttachment(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {

                if (id > 0)
                {

                    baseResultBool.success = BNNews_Attachment.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAttachmentByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Attachment> entityList = new EntityListEasyUI<N_News_Attachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_Attachment.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_Attachment.GetModelList(where, page, rows);
                }

                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_Attachment.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsCopyUser
        //Add  NNewsCopyUser
        public BaseResultDataValue ST_UDTO_AddNNewsCopyUser(N_News_CopyUser entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = BNNews_CopyUser.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.FileCopyUserID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsCopyUser
        public BaseResultBool ST_UDTO_UpdateNNewsCopyUser(N_News_CopyUser entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultBool.success = BNNews_CopyUser.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        //Delele  NNewsCopyUser
        public BaseResultBool ST_UDTO_DelNNewsCopyUser(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNews_CopyUser.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsCopyUserByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_CopyUser> entityList = new EntityListEasyUI<N_News_CopyUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_CopyUser.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_CopyUser.GetModelList(where, page, rows);
                }

                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsCopyUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_CopyUser.GetModel(id);
                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsInteraction
        //Add  NNewsInteraction
        public BaseResultDataValue ST_UDTO_AddNNewsInteraction(N_News_Interaction entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = BNNews_Interaction.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.InteractionID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsInteraction
        public BaseResultBool ST_UDTO_UpdateNNewsInteraction(N_News_Interaction entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                try
                {
                    baseResultBool.success = BNNews_Interaction.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  NNewsInteraction
        public BaseResultBool ST_UDTO_DelNNewsInteraction(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNews_Interaction.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsInteractionByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Interaction> entityList = new EntityListEasyUI<N_News_Interaction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_Interaction.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_Interaction.GetModelList(where, page, rows);
                }
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_Interaction.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsOperation
        //Add  NNewsOperation
        public BaseResultDataValue ST_UDTO_AddNNewsOperation(N_News_Operation entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = BNNews_Operation.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.FileOperationID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsOperation
        public BaseResultBool ST_UDTO_UpdateNNewsOperation(N_News_Operation entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultBool.success = BNNews_Operation.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  NNewsOperation
        public BaseResultBool ST_UDTO_DelNNewsOperation(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNews_Operation.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsOperationByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Operation> entityList = new EntityListEasyUI<N_News_Operation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_Operation.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_Operation.GetModelList(where, page, rows);
                }
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_Operation.GetModel(id);
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsReadingLog
        //Add  NNewsReadingLog
        //public BaseResultDataValue ST_UDTO_AddNNewsReadingLog(N_News_ReadingLog entity)
        //{
        //    BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
        //    if (entity != null)
        //    {
        //        entity.DataAddTime = DateTime.Now;
        //        entity.DataUpdateTime = DateTime.Now;
        //        try
        //        {
        //            baseResultDataValue.success = BNNews_ReadingLog.Add(entity);
        //            if (baseResultDataValue.success)
        //            {
        //                baseResultDataValue.ResultDataValue = entity.FileReadingLogID.ToString();
        //                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultDataValue.success = false;
        //            baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
        //            ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
        //            //throw new Exception(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        baseResultDataValue.success = false;
        //        baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultDataValue;
        //}
        ////Update  NNewsReadingLog
        //public BaseResultBool ST_UDTO_UpdateNNewsReadingLog(N_News_ReadingLog entity)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        entity.DataUpdateTime = DateTime.Now;
        //        try
        //        {
        //            baseResultBool.success = BNNews_ReadingLog.Update(entity);
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
        //            //throw new Exception(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultBool;
        //}
        ////Delele  NNewsReadingLog
        //public BaseResultBool ST_UDTO_DelNNewsReadingLog(long id)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            baseResultBool.success = BNNews_ReadingLog.Delete(id);
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
        //            }
        //        }
        //        else
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
        //        //throw new Exception(ex.ToString());
        //    }
        //    return baseResultBool;
        //}

        public BaseResultDataValue ST_UDTO_SearchNNewsReadingLogByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_ReadingLog> entityList = new EntityListEasyUI<N_News_ReadingLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_ReadingLog.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_ReadingLog.GetModelList(where, page, rows);
                }
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsReadingLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_ReadingLog.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsReadingUser
        //Add  NNewsReadingUser
        public BaseResultDataValue ST_UDTO_AddNNewsReadingUser(N_News_ReadingUser entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultDataValue.success = BNNews_ReadingUser.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.FileReadingUserID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsReadingUser
        public BaseResultBool ST_UDTO_UpdateNNewsReadingUser(N_News_ReadingUser entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                try
                {
                    baseResultBool.success = BNNews_ReadingUser.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        //Delele  NNewsReadingUser
        public BaseResultBool ST_UDTO_DelNNewsReadingUser(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNews_ReadingUser.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsReadingUserByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_ReadingUser> entityList = new EntityListEasyUI<N_News_ReadingUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_ReadingUser.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_ReadingUser.GetModelList(where, page, rows);
                }
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsReadingUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_ReadingUser.GetModel(id);
                try
                {

                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);

                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsType
        //Add  NNewsType
        public BaseResultDataValue ST_UDTO_AddNNewsType(N_News_Type entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.TypeID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                try
                {
                    baseResultDataValue.success = BNNews_Type.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.TypeID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsType
        public BaseResultBool ST_UDTO_UpdateNNewsType(N_News_Type entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                try
                {
                    baseResultBool.success = BNNews_Type.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }

        //Delele  NNewsType
        public BaseResultBool ST_UDTO_DelNNewsType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                N_News_Type entity = BNNews_Type.GetModel(id);
                if (entity != null)
                {
                    entity.IsUse = false;
                    baseResultBool.success = BNNews_Type.Update(entity);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsTypeByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Type> entityList = new EntityListEasyUI<N_News_Type>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_Type.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_Type.GetModelList(where, null, page, rows);
                }
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_Type.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region NNewsArea
        //Add  NNewsArea
        public BaseResultDataValue ST_UDTO_AddNNewsArea(N_News_Area entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.NewsAreaId = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                try
                {
                    baseResultDataValue.success = BNNews_Area.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.NewsAreaId.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  NNewsArea
        public BaseResultBool ST_UDTO_UpdateNNewsArea(N_News_Area entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                try
                {
                    baseResultBool.success = BNNews_Area.Update(entity);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  NNewsArea
        public BaseResultBool ST_UDTO_DelNNewsArea(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNews_Area.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaByHQL(int page, int rows, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Area> entityList = new EntityListEasyUI<N_News_Area>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNews_Area.GetModelList(where, sort, page, rows);
                }
                else
                {
                    entityList = BNNews_Area.GetModelList(where, page, rows);
                }
                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNews_Area.GetModel(id);
                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region N_NewsAreaClientLink
        //Add  N_NewsAreaClientLink
        public BaseResultDataValue ST_UDTO_AddNNewsAreaClientLink(N_NewsAreaClientLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.NewsAreaClientLinkId = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                try
                {
                    baseResultDataValue.success = BNNewsAreaClientLink.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.NewsAreaClientLinkId.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Delele  N_NewsAreaClientLink
        public BaseResultBool ST_UDTO_DelNNewsAreaClientLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNewsAreaClientLink.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaClientLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_NewsAreaClientLink> entityList = new EntityListEasyUI<N_NewsAreaClientLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNewsAreaClientLink.GetModelList(where, sort, page, limit);
                }
                else
                {
                    entityList = BNNewsAreaClientLink.GetModelList(where, page, limit);
                }

                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaClientLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNewsAreaClientLink.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
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

        public BaseResultDataValue ST_UDTO_SearchUnSelectListNNewsAreaClientLink(int page, int limit, string fields, string NewsAreaId, string where, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<CLIENTELE> entityList = new EntityListEasyUI<CLIENTELE>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNewsAreaClientLink.GetUnSelectList(NewsAreaId, where, sort, page, limit);
                }
                else
                {
                    entityList = BNNewsAreaClientLink.GetUnSelectList(NewsAreaId, where, null, page, limit);
                }

                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "ST_UDTO_SearchUnSelectListNNewsAreaClientLink.HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region N_NewsAreaLink
        //Add  N_NewsAreaLink
        public BaseResultDataValue ST_UDTO_AddNNewsAreaLink(N_NewsAreaLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                entity.NewsAreaLinkId = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                try
                {
                    baseResultDataValue.success = BNNewsAreaLink.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = entity.NewsAreaLinkId.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        //Delele  N_NewsAreaLink
        public BaseResultBool ST_UDTO_DelNNewsAreaLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    baseResultBool.success = BNNewsAreaLink.Delete(id);
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
                baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_NewsAreaLink> entityList = new EntityListEasyUI<N_NewsAreaLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNewsAreaLink.GetModelList(where, sort, page, limit);
                }
                else
                {
                    entityList = BNNewsAreaLink.GetModelList(where, page, limit);
                }
                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchUnSelectListNNewsAreaLink(int page, int limit, string fields, string NewsId, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityListEasyUI<N_News_Area> entityList = new EntityListEasyUI<N_News_Area>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = BNNewsAreaLink.GetUnSelectList(NewsId, sort, page, limit);
                }
                else
                {
                    entityList = BNNewsAreaLink.GetUnSelectList(NewsId, null, page, limit);
                }

                baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "ST_UDTO_SearchUnSelectListNNewsAreaLink.HQL查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchNNewsAreaLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = BNNewsAreaLink.GetModel(id);
                try
                {
                    baseResultDataValue.ResultDataValue = Common.Public.JsonSerializer.JsonDotNetSerializer(entity);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                    //throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.ToString();
                //throw new Exception(ex.ToString());
            }
            return baseResultDataValue;
        }


        #endregion

        public BaseResultDataValue GetApprovaList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            WSRBAC.WSRbac a = new WSRBAC.WSRbac();
            BaseResultDataSet brds = new BaseResultDataSet();
            //调服务
            //string strxml = a.getUserInfoListByPost("LogisticsOfficer,WebLisApplyInput");
            string strxml = a.getUserInfoListByPost("NewsApproval");
            DataSet ds = new DataSet();
            try
            {
                ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(strxml);
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug("读取审批角色人员信息列表出错！:" + eee.ToString());
            }
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (!ds.Tables[0].Columns.Contains("Name"))
                    {
                        ds.Tables[0].Columns.Add("Name");
                    }
                    DataSet logicDs = new DataSet();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dr["Name"] = dr["NameL"].ToString() + dr["NameF"].ToString();
                    }
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ds.Tables[0]);
                    brdv.success = true;
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "读取审批角色人员信息列表出错！";
                ZhiFang.Common.Log.Log.Debug("GetLogisticsDeliveryPerson.读取审批角色人员信息列表出错！:" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetHasReadNewsFlag()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能读取登录者信息！";
                return brdv;
            }
            if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "未能读取登录者信息！";
                return brdv;
            }
            string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
            string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
            try
            {
                if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
                {
                    brdv.ErrorInfo = "请登录！";
                    brdv.success = false;
                    return brdv;
                }

                ZhiFang.BLL.Common.BaseDictionary.BusinessLogicClientControl bcc = new BLL.Common.BaseDictionary.BusinessLogicClientControl();
                DataSet dsclientlist = bcc.GetList(new BusinessLogicClientControl() { Account = ZhiFangUser, SelectedFlag = true });
                List<string> clientnolist = new List<string>();
                if (dsclientlist != null && dsclientlist.Tables.Count > 0 && dsclientlist.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsclientlist.Tables[0].Rows.Count; i++)
                    {
                        clientnolist.Add(dsclientlist.Tables[0].Rows[i]["CLIENTNO"].ToString());
                    }
                }
                string tmpsql = "";
                if (clientnolist.Count > 0)
                {
                    BNNewsAreaClientLink bnarea = new BNNewsAreaClientLink();
                    var arealist = bnarea.GetModelList(" ClientNo in (" + string.Join(",", clientnolist) + ") ");
                    if (arealist != null && arealist.Count > 0)
                    {
                        List<string> areaidlist = new List<string>();
                        var alist = arealist.GroupBy(a => a.NewsAreaId);
                        foreach (var a in alist)
                        {
                            areaidlist.Add(a.Key.ToString());
                        }
                        tmpsql = " or ( FileID in ( select NewsId from N_NewsAreaLink where NewsAreaId in (" + string.Join(",", areaidlist) + ") ) ) ";

                    }
                }
                string where = " Status=" + ZhiFang.Model.NNewsStatus.发布.Key + "  and (FileID not in (select NewsId from N_NewsAreaLink) " + tmpsql + ") and                     ((EndTime >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and BeginTime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') or (EndTime >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and BeginTime is null) or (EndTime is null and BeginTime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "') or (EndTime is null and BeginTime is null )) and  (select count(*) from N_News_ReadingLog where FileID=N_News.FileID and ReaderID=" + ZhiFangUserID + ")<=0 and IsUse=1";
                ZhiFang.Common.Log.Log.Debug("GetLogisticsDeliveryPerson.where:" + where);
                brdv.ResultDataValue = BNNews.GetCount(where).ToString();
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "读取未读新闻列表出错！";
                ZhiFang.Common.Log.Log.Error("GetLogisticsDeliveryPerson.读取未读新闻列表出错！:" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetZhiFangLIIPMsg(string flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (Cookie.CookieHelper.Read("ZhiFangUserID") == null || Cookie.CookieHelper.Read("ZhiFangUserID").Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能读取登录者信息！";
                    return brdv;
                }
                if (Cookie.CookieHelper.Read("EmployeeName") == null || Cookie.CookieHelper.Read("EmployeeName").Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未能读取登录者信息！";
                    return brdv;
                }
                string ZhiFangUser = Cookie.CookieHelper.Read("ZhiFangUser");
                string ZhiFangUserID = Cookie.CookieHelper.Read("ZhiFangUserID");
                string ZhiFangPWD = Cookie.CookieHelper.Read("ZhiFangPwd");
                //ZhiFangPWD = ZhiFang.Common.Public.Base64Help.DecodingString(ZhiFangPWD);

                if (ZhiFangUser == null || ZhiFangUser.Trim() == "")
                {
                    brdv.ErrorInfo = "请登录！";
                    brdv.success = false;
                    return brdv;
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFangLIIPUrl") == null || ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFangLIIPUrl") == "")
                {
                    brdv.ErrorInfo = "集成平台地址未配置！";
                    brdv.success = false;
                    return brdv;
                }
                string result = WebRequestHelp.Get(ConfigHelper.GetConfigString("ZhiFangLIIPUrl") + "/ServerWCF/IMService.svc/GetUserMsgByPWD?Account=" + ZhiFangUser + "&PWD=" + ZhiFangPWD + "&flag=false");
                ZhiFang.Common.Log.Log.Debug("GetZhiFangLIIPMsg.result:" + result);
                BaseResultDataValue brdvresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<BaseResultDataValue>(result);
                if (brdvresult.success)
                {
                    var resultroot = JsonConvert.DeserializeObject(brdvresult.ResultDataValue) as JObject;
                    ZhiFang.Common.Log.Log.Debug("GetZhiFangLIIPMsg.brdvresult.ResultDataValue:" + brdvresult.ResultDataValue);
                    if (resultroot["count"] != null && long.Parse(resultroot["count"].ToString()) >= 1)
                    {
                        brdv.ResultDataValue = "{\"result\":\"true\"}";
                        brdv.success = true;
                        return brdv;
                    }
                    else
                    {
                        brdv.ResultDataValue = "{\"result\":\"false\"}";
                        brdv.success = true;
                        return brdv;
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "调用平台服务出错！";
                    ZhiFang.Common.Log.Log.Debug("GetZhiFangLIIPMsg.调用平台服务出错！:" + brdvresult.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                ZhiFang.Common.Log.Log.Debug("GetZhiFangLIIPMsg.程序异常！:" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetZhiFangLIIPUrl()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFangLIIPUrl") == null || ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFangLIIPUrl") == "")
            {
                brdv.ErrorInfo = "集成平台地址未配置！";
                brdv.success = false;
                return brdv;
            }
            brdv.ResultDataValue = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhiFangLIIPUrl");
            brdv.success = true;
            return brdv;
        }
    }
}
