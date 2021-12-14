using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;
using System.Linq;

namespace ZhiFang.BLL.Common.News
{
    //枚举
    //Type 文档类型（1、新闻；2、知识库；3、文档）
    //Status 状态（1、待审核；2、已审核；3、发布）	

    public class BNNews
    {

        private readonly ZhiFang.IDAL.IDNNews dal = DalFactory<IDNNews>.GetDalByClassName("DNNews");
        public BNNews()
        { }

        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long FileID)
        {

            return dal.Delete(FileID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News GetModel(long FileID)
        {

            return dal.GetModel(FileID);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_News> DataTableToList(DataTable dt)
        {
            List<Model.N_News> modelList = new List<Model.N_News>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_News model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_News();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["FileID"].ToString() != "")
                    {
                        model.FileID = long.Parse(dt.Rows[n]["FileID"].ToString());
                    }
                    model.Title = dt.Rows[n]["Title"].ToString();
                    model.No = dt.Rows[n]["No"].ToString();
                    model.Content = dt.Rows[n]["Content"].ToString();
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = long.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["ContentType"].ToString() != "")
                    {
                        model.ContentType = long.Parse(dt.Rows[n]["ContentType"].ToString());
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (model.Status > 0 && model.Status <= NNewsStatus.GetStatusDic().Count)
                    {
                        model.StatusName = NNewsStatus.GetStatusDic()[model.Status.ToString()].Name;
                    }
                    model.Keyword = dt.Rows[n]["Keyword"].ToString();
                    model.Summary = dt.Rows[n]["Summary"].ToString();
                    model.Source = dt.Rows[n]["Source"].ToString();
                    model.VersionNo = dt.Rows[n]["VersionNo"].ToString();
                    if (dt.Rows[n]["Pagination"].ToString() != "")
                    {
                        model.Pagination = int.Parse(dt.Rows[n]["Pagination"].ToString());
                    }
                    model.ReviseNo = dt.Rows[n]["ReviseNo"].ToString();
                    if (dt.Rows[n]["RevisorID"].ToString() != "")
                    {
                        model.RevisorID = long.Parse(dt.Rows[n]["RevisorID"].ToString());
                    }
                    model.ReviseReason = dt.Rows[n]["ReviseReason"].ToString();
                    model.ReviseContent = dt.Rows[n]["ReviseContent"].ToString();
                    if (dt.Rows[n]["ReviseTime"].ToString() != "")
                    {
                        model.ReviseTime = DateTime.Parse(dt.Rows[n]["ReviseTime"].ToString());
                    }
                    if (dt.Rows[n]["BeginTime"].ToString() != "")
                    {
                        model.BeginTime = DateTime.Parse(dt.Rows[n]["BeginTime"].ToString());
                    }
                    if (dt.Rows[n]["EndTime"].ToString() != "")
                    {
                        model.EndTime = DateTime.Parse(dt.Rows[n]["EndTime"].ToString());
                    }
                    if (dt.Rows[n]["NextExecutorID"].ToString() != "")
                    {
                        model.NextExecutorID = long.Parse(dt.Rows[n]["NextExecutorID"].ToString());
                    }
                    model.Memo = dt.Rows[n]["Memo"].ToString();
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[n]["IsUse"].ToString() != "")
                    {
                        model.IsUse = int.Parse(dt.Rows[n]["IsUse"].ToString());
                    }
                    if (dt.Rows[n]["CreatorID"].ToString() != "")
                    {
                        model.CreatorID = long.Parse(dt.Rows[n]["CreatorID"].ToString());
                    }
                    model.CreatorName = dt.Rows[n]["CreatorName"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Rows[n]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(dt.Rows[n]["DataUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["FTypeTreeId"].ToString() != "")
                    {
                        model.FTypeTreeId = long.Parse(dt.Rows[n]["FTypeTreeId"].ToString());
                    }
                    if (dt.Rows[n]["OriginalFileID"].ToString() != "")
                    {
                        model.OriginalFileID = long.Parse(dt.Rows[n]["OriginalFileID"].ToString());
                    }
                    if (dt.Rows[n]["CheckerId"].ToString() != "")
                    {
                        model.CheckerId = long.Parse(dt.Rows[n]["CheckerId"].ToString());
                    }
                    model.CheckerName = dt.Rows[n]["CheckerName"].ToString();
                    if (dt.Rows[n]["ApprovalId"].ToString() != "")
                    {
                        model.ApprovalId = long.Parse(dt.Rows[n]["ApprovalId"].ToString());
                    }
                    model.ApprovalName = dt.Rows[n]["ApprovalName"].ToString();
                    if (dt.Rows[n]["PublisherId"].ToString() != "")
                    {
                        model.PublisherId = long.Parse(dt.Rows[n]["PublisherId"].ToString());
                    }
                    model.PublisherName = dt.Rows[n]["PublisherName"].ToString();
                    if (dt.Rows[n]["DrafterId"].ToString() != "")
                    {
                        model.DrafterId = long.Parse(dt.Rows[n]["DrafterId"].ToString());
                    }
                    model.DrafterCName = dt.Rows[n]["DrafterCName"].ToString();
                    if (dt.Rows[n]["IsTop"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsTop"].ToString() == "1") || (dt.Rows[n]["IsTop"].ToString().ToLower() == "true"))
                        {
                            model.IsTop = true;
                        }
                        else
                        {
                            model.IsTop = false;
                        }
                    }
                    if (dt.Rows[n]["IsDiscuss"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsDiscuss"].ToString() == "1") || (dt.Rows[n]["IsDiscuss"].ToString().ToLower() == "true"))
                        {
                            model.IsDiscuss = true;
                        }
                        else
                        {
                            model.IsDiscuss = false;
                        }
                    }
                    if (dt.Rows[n]["Counts"].ToString() != "")
                    {
                        model.Counts = int.Parse(dt.Rows[n]["Counts"].ToString());
                    }
                    if (dt.Rows[n]["DrafterDateTime"].ToString() != "")
                    {
                        model.DrafterDateTime = DateTime.Parse(dt.Rows[n]["DrafterDateTime"].ToString());
                    }
                    if (dt.Rows[n]["CheckerDateTime"].ToString() != "")
                    {
                        model.CheckerDateTime = DateTime.Parse(dt.Rows[n]["CheckerDateTime"].ToString());
                    }
                    if (dt.Rows[n]["ApprovalDateTime"].ToString() != "")
                    {
                        model.ApprovalDateTime = DateTime.Parse(dt.Rows[n]["ApprovalDateTime"].ToString());
                    }
                    if (dt.Rows[n]["PublisherDateTime"].ToString() != "")
                    {
                        model.PublisherDateTime = DateTime.Parse(dt.Rows[n]["PublisherDateTime"].ToString());
                    }
                    if (dt.Rows[n]["IsSyncWeiXin"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsSyncWeiXin"].ToString() == "1") || (dt.Rows[n]["IsSyncWeiXin"].ToString().ToLower() == "true"))
                        {
                            model.IsSyncWeiXin = true;
                        }
                        else
                        {
                            model.IsSyncWeiXin = false;
                        }
                    }
                    model.WeiXinContent = dt.Rows[n]["WeiXinContent"].ToString();
                    model.ThumbnailsPath = dt.Rows[n]["ThumbnailsPath"].ToString();
                    model.ThumbnailsMemo = dt.Rows[n]["ThumbnailsMemo"].ToString();
                    model.Media_id = dt.Rows[n]["Media_id"].ToString();
                    model.Thumb_media_id = dt.Rows[n]["Thumb_media_id"].ToString();
                    model.WeiXinTitle = dt.Rows[n]["WeiXinTitle"].ToString();
                    model.WeiXinAuthor = dt.Rows[n]["WeiXinAuthor"].ToString();
                    model.WeiXinDigest = dt.Rows[n]["WeiXinDigest"].ToString();
                    model.WeiXinUrl = dt.Rows[n]["WeiXinUrl"].ToString();
                    model.WeiXinContent_source_url = dt.Rows[n]["WeiXinContent_source_url"].ToString();
                    if (dt.Columns.Contains("ReadCount") && dt.Rows[n]["ReadCount"].ToString() != "")
                    {
                        model.ReadCount = int.Parse(dt.Rows[n]["ReadCount"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public BaseResultDataValue NNewsApproval(string idlist, bool approvalFlag, string Memo, string empid, string empname)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            DataSet ds = dal.GetList(" IsUse=1 and FileID in (" + idlist + ") ");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                List<string> Idlist = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ApprovalId"] == null || dt.Rows[i]["ApprovalId"].ToString().Trim() != empid)
                    {
                        ZhiFang.Common.Log.Log.Debug("所审批的新闻审核者不是当前登录者！FileID:" + dt.Rows[i]["ApprovalId"].ToString() + ",empid:" + empid);
                    }
                    else
                    {
                        if (dt.Rows[i]["Status"] == null || (dt.Rows[i]["Status"].ToString().Trim() != ZhiFang.Model.NNewsStatus.起草.Key && dt.Rows[i]["Status"].ToString().Trim() != ZhiFang.Model.NNewsStatus.发布撤回.Key))
                        {
                            ZhiFang.Common.Log.Log.Debug("所审批的新闻审状态错误！FileID:" + dt.Rows[i]["ApprovalId"].ToString() + ",empid:" + empid);
                        }
                        else
                        {
                            Idlist.Add(dt.Rows[i]["FileID"].ToString().Trim());
                        }
                    }
                }
                brdv.success = dal.NNewsApproval(string.Join(",", Idlist), approvalFlag, Memo, empid, empname);
                return brdv;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("所审批的新闻不存在！idlist:" + idlist);
                brdv.ErrorInfo = "所审批的新闻不存在！";
                brdv.success = false;
                return brdv;
            }
        }

        public EntityListEasyUI<N_News> ST_UDTO_SearchNNewsList(string where, string sort, int page, int rows, string empid)
        {
            EntityListEasyUI<N_News> list = new EntityListEasyUI<N_News>();
            DataSet ds = new DataSet();
            ds = dal.GetList(where, page, rows, sort, empid);
            BNNews_Type bnt = new BNNews_Type();
            var typelist = bnt.GetModelList(" IsUse=1 ");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            {
                list.total = dal.GetCount(where);
                list.rows = DataTableToList(ds.Tables[0]);
                foreach (var n in list.rows)
                {
                    if (typelist != null && typelist.Count > 0 && typelist.Count(a => a.TypeID == n.ContentType) > 0)
                    {
                        n.ContentTypeName = typelist.Where(a => a.TypeID == n.ContentType).ElementAt(0).CName;
                    }
                }
            }
            else
            {
                list.total = 0;
                list.rows = new List<N_News>();
            }
            return list;
        }

        public BaseResultDataValue NNewsPublish(string idlist, bool publishFlag, string Memo, string empid, string empname)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            brdv.success = dal.NNewsPublish(idlist, publishFlag, Memo, empid, empname);
            return brdv;
        }

        public EntityListEasyUI<N_News> GetModelList(string where, int page, int limit)
        {
            return this.GetModelList(where, null, page, limit);
        }

        public EntityListEasyUI<N_News> GetModelList(string where, string sort, int page, int limit)
        {
            EntityListEasyUI<N_News> list = new EntityListEasyUI<N_News>();
            DataSet ds = new DataSet();
            ds = dal.GetList(where, page, limit, sort);
            BNNews_Type bnt = new BNNews_Type();
            var typelist = bnt.GetModelList(" IsUse=1 ");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            {
                list.total = dal.GetCount(where);
                list.rows = DataTableToList(ds.Tables[0]);
                foreach (var n in list.rows)
                {
                    if (typelist != null && typelist.Count > 0 && typelist.Count(a => a.TypeID == n.ContentType) > 0)
                    {
                        n.ContentTypeName = typelist.Where(a => a.TypeID == n.ContentType).ElementAt(0).CName;
                    }
                }
            }
            else
            {
                list.total = 0;
                list.rows = new List<N_News>();
            }
            return list;
        }

        public int GetCount(string where)
        {
            return dal.GetCount(where);
        }


        #endregion

    }
}