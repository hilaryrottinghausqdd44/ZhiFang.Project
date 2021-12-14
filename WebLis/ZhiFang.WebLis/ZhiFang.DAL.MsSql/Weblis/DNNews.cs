using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //枚举
    //Type 文档类型（1、新闻；2、知识库；3、文档）
    //Status 状态（1、待审核；2、已审核；3、发布）	

    public class DNNews : BaseDALLisDB,ZhiFang.IDAL.IDNNews
    {
        public DNNews(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public DNNews()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabID != null)
            {
                strSql1.Append("LabID,");
                strSql2.Append("" + model.LabID + ",");
            }
            if (model.FileID != null)
            {
                strSql1.Append("FileID,");
                strSql2.Append("" + model.FileID + ",");
            }
            if (model.Title != null)
            {
                strSql1.Append("Title,");
                strSql2.Append("'" + model.Title + "',");
            }
            if (model.No != null)
            {
                strSql1.Append("No,");
                strSql2.Append("'" + model.No + "',");
            }
            if (model.Content != null)
            {
                strSql1.Append("Content,");
                strSql2.Append("'" + model.Content + "',");
            }
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("" + model.Type + ",");
            }
            if (model.ContentType != null)
            {
                strSql1.Append("ContentType,");
                strSql2.Append("" + model.ContentType + ",");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("" + model.Status + ",");
            }
            if (model.Keyword != null)
            {
                strSql1.Append("Keyword,");
                strSql2.Append("'" + model.Keyword + "',");
            }
            if (model.Summary != null)
            {
                strSql1.Append("Summary,");
                strSql2.Append("'" + model.Summary + "',");
            }
            if (model.Source != null)
            {
                strSql1.Append("Source,");
                strSql2.Append("'" + model.Source + "',");
            }
            if (model.VersionNo != null)
            {
                strSql1.Append("VersionNo,");
                strSql2.Append("'" + model.VersionNo + "',");
            }
            if (model.Pagination != null)
            {
                strSql1.Append("Pagination,");
                strSql2.Append("" + model.Pagination + ",");
            }
            if (model.ReviseNo != null)
            {
                strSql1.Append("ReviseNo,");
                strSql2.Append("'" + model.ReviseNo + "',");
            }
            if (model.RevisorID != null)
            {
                strSql1.Append("RevisorID,");
                strSql2.Append("" + model.RevisorID + ",");
            }
            if (model.ReviseReason != null)
            {
                strSql1.Append("ReviseReason,");
                strSql2.Append("'" + model.ReviseReason + "',");
            }
            if (model.ReviseContent != null)
            {
                strSql1.Append("ReviseContent,");
                strSql2.Append("'" + model.ReviseContent + "',");
            }
            if (model.ReviseTime != null)
            {
                strSql1.Append("ReviseTime,");
                strSql2.Append("'" + model.ReviseTime + "',");
            }
            if (model.BeginTime != null)
            {
                strSql1.Append("BeginTime,");
                strSql2.Append("'" + model.BeginTime + "',");
            }
            if (model.EndTime != null)
            {
                strSql1.Append("EndTime,");
                strSql2.Append("'" + model.EndTime + "',");
            }
            if (model.NextExecutorID != null)
            {
                strSql1.Append("NextExecutorID,");
                strSql2.Append("" + model.NextExecutorID + ",");
            }
            if (model.Memo != null)
            {
                strSql1.Append("Memo,");
                strSql2.Append("'" + model.Memo + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("" + model.IsUse + ",");
            }
            if (model.CreatorID != null)
            {
                strSql1.Append("CreatorID,");
                strSql2.Append("" + model.CreatorID + ",");
            }
            if (model.CreatorName != null)
            {
                strSql1.Append("CreatorName,");
                strSql2.Append("'" + model.CreatorName + "',");
            }
            if (model.DataAddTime != null)
            {
                strSql1.Append("DataAddTime,");
                strSql2.Append("'" + model.DataAddTime + "',");
            }
            if (model.DataUpdateTime != null)
            {
                strSql1.Append("DataUpdateTime,");
                strSql2.Append("'" + model.DataUpdateTime + "',");
            }
            if (model.FTypeTreeId != null)
            {
                strSql1.Append("FTypeTreeId,");
                strSql2.Append("" + model.FTypeTreeId + ",");
            }
            if (model.OriginalFileID != null)
            {
                strSql1.Append("OriginalFileID,");
                strSql2.Append("" + model.OriginalFileID + ",");
            }
            if (model.CheckerId != null)
            {
                strSql1.Append("CheckerId,");
                strSql2.Append("" + model.CheckerId + ",");
            }
            if (model.CheckerName != null)
            {
                strSql1.Append("CheckerName,");
                strSql2.Append("'" + model.CheckerName + "',");
            }
            if (model.ApprovalId != null)
            {
                strSql1.Append("ApprovalId,");
                strSql2.Append("" + model.ApprovalId + ",");
            }
            if (model.ApprovalName != null)
            {
                strSql1.Append("ApprovalName,");
                strSql2.Append("'" + model.ApprovalName + "',");
            }
            if (model.PublisherId != null)
            {
                strSql1.Append("PublisherId,");
                strSql2.Append("" + model.PublisherId + ",");
            }
            if (model.PublisherName != null)
            {
                strSql1.Append("PublisherName,");
                strSql2.Append("'" + model.PublisherName + "',");
            }
            if (model.DrafterId != null)
            {
                strSql1.Append("DrafterId,");
                strSql2.Append("" + model.DrafterId + ",");
            }
            if (model.DrafterCName != null)
            {
                strSql1.Append("DrafterCName,");
                strSql2.Append("'" + model.DrafterCName + "',");
            }
            if (model.IsTop != null)
            {
                strSql1.Append("IsTop,");
                strSql2.Append("" + (model.IsTop ? 1 : 0) + ",");
            }
            if (model.IsDiscuss != null)
            {
                strSql1.Append("IsDiscuss,");
                strSql2.Append("" + (model.IsDiscuss ? 1 : 0) + ",");
            }
            if (model.Counts != null)
            {
                strSql1.Append("Counts,");
                strSql2.Append("" + model.Counts + ",");
            }
            if (model.DrafterDateTime != null)
            {
                strSql1.Append("DrafterDateTime,");
                strSql2.Append("'" + model.DrafterDateTime + "',");
            }
            if (model.CheckerDateTime != null)
            {
                strSql1.Append("CheckerDateTime,");
                strSql2.Append("'" + model.CheckerDateTime + "',");
            }
            if (model.ApprovalDateTime != null)
            {
                strSql1.Append("ApprovalDateTime,");
                strSql2.Append("'" + model.ApprovalDateTime + "',");
            }
            if (model.PublisherDateTime != null)
            {
                strSql1.Append("PublisherDateTime,");
                strSql2.Append("'" + model.PublisherDateTime + "',");
            }
            if (model.IsSyncWeiXin != null)
            {
                strSql1.Append("IsSyncWeiXin,");
                strSql2.Append("" + (model.IsSyncWeiXin ? 1 : 0) + ",");
            }
            if (model.WeiXinContent != null)
            {
                strSql1.Append("WeiXinContent,");
                strSql2.Append("'" + model.WeiXinContent + "',");
            }
            if (model.ThumbnailsPath != null)
            {
                strSql1.Append("ThumbnailsPath,");
                strSql2.Append("'" + model.ThumbnailsPath + "',");
            }
            if (model.ThumbnailsMemo != null)
            {
                strSql1.Append("ThumbnailsMemo,");
                strSql2.Append("'" + model.ThumbnailsMemo + "',");
            }
            if (model.Media_id != null)
            {
                strSql1.Append("Media_id,");
                strSql2.Append("'" + model.Media_id + "',");
            }
            if (model.Thumb_media_id != null)
            {
                strSql1.Append("Thumb_media_id,");
                strSql2.Append("'" + model.Thumb_media_id + "',");
            }
            if (model.WeiXinTitle != null)
            {
                strSql1.Append("WeiXinTitle,");
                strSql2.Append("'" + model.WeiXinTitle + "',");
            }
            if (model.WeiXinAuthor != null)
            {
                strSql1.Append("WeiXinAuthor,");
                strSql2.Append("'" + model.WeiXinAuthor + "',");
            }
            if (model.WeiXinDigest != null)
            {
                strSql1.Append("WeiXinDigest,");
                strSql2.Append("'" + model.WeiXinDigest + "',");
            }
            if (model.WeiXinUrl != null)
            {
                strSql1.Append("WeiXinUrl,");
                strSql2.Append("'" + model.WeiXinUrl + "',");
            }
            if (model.WeiXinContent_source_url != null)
            {
                strSql1.Append("WeiXinContent_source_url,");
                strSql2.Append("'" + model.WeiXinContent_source_url + "',");
            }
            strSql.Append("insert into N_News(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News model)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("update N_News set ");
			if (model.LabID != null)
			{
				strSql.Append("LabID="+model.LabID+",");
			}
			if (model.Title != null)
			{
				strSql.Append("Title='"+model.Title+"',");
			}
			else
			{
				strSql.Append("Title= null ,");
			}
			if (model.No != null)
			{
				strSql.Append("No='"+model.No+"',");
			}
			else
			{
				strSql.Append("No= null ,");
			}
			if (model.Content != null)
			{
				strSql.Append("Content='"+model.Content+"',");
			}
			else
			{
				strSql.Append("Content= null ,");
			}
			if (model.Type != null)
			{
				strSql.Append("Type="+model.Type+",");
			}
			else
			{
				strSql.Append("Type= null ,");
			}
			if (model.ContentType != null)
			{
				strSql.Append("ContentType="+model.ContentType+",");
			}
			else
			{
				strSql.Append("ContentType= null ,");
			}
			if (model.Status != null&& model.Status >0)
			{
				strSql.Append("Status="+model.Status+",");
			}

			if (model.Keyword != null)
			{
				strSql.Append("Keyword='"+model.Keyword+"',");
			}
			else
			{
				strSql.Append("Keyword= null ,");
			}
			if (model.Summary != null)
			{
				strSql.Append("Summary='"+model.Summary+"',");
			}
			else
			{
				strSql.Append("Summary= null ,");
			}
			if (model.Source != null)
			{
				strSql.Append("Source='"+model.Source+"',");
			}
			//else
			//{
			//	strSql.Append("Source= null ,");
			//}
			if (model.VersionNo != null)
			{
				strSql.Append("VersionNo='"+model.VersionNo+"',");
			}
			//else
			//{
			//	strSql.Append("VersionNo= null ,");
			//}
			if (model.Pagination != null)
			{
				strSql.Append("Pagination="+model.Pagination+",");
			}
			//else
			//{
			//	strSql.Append("Pagination= null ,");
			//}
			if (model.ReviseNo != null)
			{
				strSql.Append("ReviseNo='"+model.ReviseNo+"',");
			}
			//else
			//{
			//	strSql.Append("ReviseNo= null ,");
			//}
			if (model.RevisorID != null)
			{
				strSql.Append("RevisorID="+model.RevisorID+",");
			}
			//else
			//{
			//	strSql.Append("RevisorID= null ,");
			//}
			if (model.ReviseReason != null)
			{
				strSql.Append("ReviseReason='"+model.ReviseReason+"',");
			}
			//else
			//{
			//	strSql.Append("ReviseReason= null ,");
			//}
			if (model.ReviseContent != null)
			{
				strSql.Append("ReviseContent='"+model.ReviseContent+"',");
			}
			//else
			//{
			//	strSql.Append("ReviseContent= null ,");
			//}
			if (model.ReviseTime != null)
			{
				strSql.Append("ReviseTime='"+model.ReviseTime+"',");
			}
			//else
			//{
			//	strSql.Append("ReviseTime= null ,");
			//}
			if (model.BeginTime != null)
			{
				strSql.Append("BeginTime='"+model.BeginTime+"',");
			}
			//else
			//{
			//	strSql.Append("BeginTime= null ,");
			//}
			if (model.EndTime != null)
			{
				strSql.Append("EndTime='"+model.EndTime+"',");
			}
			//else
			//{
			//	strSql.Append("EndTime= null ,");
			//}
			if (model.NextExecutorID != null)
			{
				strSql.Append("NextExecutorID="+model.NextExecutorID+",");
			}
			//else
			//{
			//	strSql.Append("NextExecutorID= null ,");
			//}
			if (model.Memo != null)
			{
				strSql.Append("Memo='"+model.Memo+"',");
			}
			//else
			//{
			//	strSql.Append("Memo= null ,");
			//}
			if (model.DispOrder != null&& model.DispOrder>=0)
			{
				strSql.Append("DispOrder="+model.DispOrder+",");
			}
			//else
			//{
			//	strSql.Append("DispOrder= null ,");
			//}
			if (model.IsUse != null)
			{
				strSql.Append("IsUse="+model.IsUse+",");
			}
			//else
			//{
			//	strSql.Append("IsUse= null ,");
			//}
			//if (model.CreatorID != null && model.CreatorID >= 0)
			//{
			//	strSql.Append("CreatorID="+model.CreatorID+",");
			//}
			//else
			//{
			//	strSql.Append("CreatorID= null ,");
			//}
			//if (model.CreatorName != null)
			//{
			//	strSql.Append("CreatorName='"+model.CreatorName+"',");
			//}
			//else
			//{
			//	strSql.Append("CreatorName= null ,");
			//}
			if (model.DataUpdateTime != null)
			{
				strSql.Append("DataUpdateTime='"+model.DataUpdateTime+"',");
			}
			else
			{
				strSql.Append("DataUpdateTime= null ,");
			}
			//if (model.FTypeTreeId != null)
			//{
			//	strSql.Append("FTypeTreeId="+model.FTypeTreeId+",");
			//}
			//else
			//{
			//	strSql.Append("FTypeTreeId= null ,");
			//}
			//if (model.OriginalFileID != null)
			//{
			//	strSql.Append("OriginalFileID="+model.OriginalFileID+",");
			//}
			//else
			//{
			//	strSql.Append("OriginalFileID= null ,");
			//}
			if (model.CheckerId != null)
			{
				strSql.Append("CheckerId="+model.CheckerId+",");
			}
			else
			{
				strSql.Append("CheckerId= null ,");
			}
			if (model.CheckerName != null)
			{
				strSql.Append("CheckerName='"+model.CheckerName+"',");
			}
			else
			{
				strSql.Append("CheckerName= null ,");
			}
			if (model.ApprovalId != null)
			{
				strSql.Append("ApprovalId="+model.ApprovalId+",");
			}
			else
			{
				strSql.Append("ApprovalId= null ,");
			}
			if (model.ApprovalName != null)
			{
				strSql.Append("ApprovalName='"+model.ApprovalName+"',");
			}
			else
			{
				strSql.Append("ApprovalName= null ,");
			}
			if (model.PublisherId != null)
			{
				strSql.Append("PublisherId="+model.PublisherId+",");
			}
			else
			{
				strSql.Append("PublisherId= null ,");
			}
			if (model.PublisherName != null)
			{
				strSql.Append("PublisherName='"+model.PublisherName+"',");
			}
			else
			{
				strSql.Append("PublisherName= null ,");
			}
			//if (model.DrafterId != null)
			//{
			//	strSql.Append("DrafterId="+model.DrafterId+",");
			//}
			//else
			//{
			//	strSql.Append("DrafterId= null ,");
			//}
			//if (model.DrafterCName != null)
			//{
			//	strSql.Append("DrafterCName='"+model.DrafterCName+"',");
			//}
			//else
			//{
			//	strSql.Append("DrafterCName= null ,");
			//}
			if (model.IsTop != null)
			{
				strSql.Append("IsTop="+ (model.IsTop? 1 : 0) +",");
			}
			if (model.IsDiscuss != null)
			{
				strSql.Append("IsDiscuss="+ (model.IsDiscuss? 1 : 0) +",");
			}
			if (model.Counts != null)
			{
				strSql.Append("Counts="+model.Counts+",");
			}
			
			//if (model.DrafterDateTime != null)
			//{
			//	strSql.Append("DrafterDateTime='"+model.DrafterDateTime+"',");
			//}
			//else
			//{
			//	strSql.Append("DrafterDateTime= null ,");
			//}
			if (model.CheckerDateTime != null)
			{
				strSql.Append("CheckerDateTime='"+model.CheckerDateTime+"',");
			}
			else
			{
				strSql.Append("CheckerDateTime= null ,");
			}
			if (model.ApprovalDateTime != null)
			{
				strSql.Append("ApprovalDateTime='"+model.ApprovalDateTime+"',");
			}
			else
			{
				strSql.Append("ApprovalDateTime= null ,");
			}
			if (model.PublisherDateTime != null)
			{
				strSql.Append("PublisherDateTime='"+model.PublisherDateTime+"',");
			}
			else
			{
				strSql.Append("PublisherDateTime= null ,");
			}
			//if (model.IsSyncWeiXin != null)
			//{
			//	strSql.Append("IsSyncWeiXin="+ (model.IsSyncWeiXin? 1 : 0) +",");
			//}
			//else
			//{
			//	strSql.Append("IsSyncWeiXin= null ,");
			//}
			//if (model.WeiXinContent != null)
			//{
			//	strSql.Append("WeiXinContent='"+model.WeiXinContent+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinContent= null ,");
			//}
			//if (model.ThumbnailsPath != null)
			//{
			//	strSql.Append("ThumbnailsPath='"+model.ThumbnailsPath+"',");
			//}
			//else
			//{
			//	strSql.Append("ThumbnailsPath= null ,");
			//}
			//if (model.ThumbnailsMemo != null)
			//{
			//	strSql.Append("ThumbnailsMemo='"+model.ThumbnailsMemo+"',");
			//}
			//else
			//{
			//	strSql.Append("ThumbnailsMemo= null ,");
			//}
			//if (model.Media_id != null)
			//{
			//	strSql.Append("Media_id='"+model.Media_id+"',");
			//}
			//else
			//{
			//	strSql.Append("Media_id= null ,");
			//}
			//if (model.Thumb_media_id != null)
			//{
			//	strSql.Append("Thumb_media_id='"+model.Thumb_media_id+"',");
			//}
			//else
			//{
			//	strSql.Append("Thumb_media_id= null ,");
			//}
			//if (model.WeiXinTitle != null)
			//{
			//	strSql.Append("WeiXinTitle='"+model.WeiXinTitle+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinTitle= null ,");
			//}
			//if (model.WeiXinAuthor != null)
			//{
			//	strSql.Append("WeiXinAuthor='"+model.WeiXinAuthor+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinAuthor= null ,");
			//}
			//if (model.WeiXinDigest != null)
			//{
			//	strSql.Append("WeiXinDigest='"+model.WeiXinDigest+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinDigest= null ,");
			//}
			//if (model.WeiXinUrl != null)
			//{
			//	strSql.Append("WeiXinUrl='"+model.WeiXinUrl+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinUrl= null ,");
			//}
			//if (model.WeiXinContent_source_url != null)
			//{
			//	strSql.Append("WeiXinContent_source_url='"+model.WeiXinContent_source_url+"',");
			//}
			//else
			//{
			//	strSql.Append("WeiXinContent_source_url= null ,");
			//}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where FileID="+ model.FileID+" ");
			int rowsAffected=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long FileID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from N_News ");
            strSql.Append(" where FileID=@FileID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileID", SqlDbType.BigInt,8)         };
            parameters[0].Value = FileID;


            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News GetModel(long FileID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, FileID, Title, No, Content, Type, ContentType, Status, Keyword, Summary, Source, VersionNo, Pagination, ReviseNo, RevisorID, ReviseReason, ReviseContent, ReviseTime, BeginTime, EndTime, NextExecutorID, Memo, DispOrder, IsUse, CreatorID, CreatorName, DataAddTime, DataUpdateTime,  FTypeTreeId, OriginalFileID, CheckerId, CheckerName, ApprovalId, ApprovalName, PublisherId, PublisherName, DrafterId, DrafterCName, IsTop, IsDiscuss, Counts, DrafterDateTime, CheckerDateTime, ApprovalDateTime, PublisherDateTime, IsSyncWeiXin, WeiXinContent, ThumbnailsPath, ThumbnailsMemo, Media_id, Thumb_media_id, WeiXinTitle, WeiXinAuthor, WeiXinDigest, WeiXinUrl, WeiXinContent_source_url  ");
            strSql.Append("  from N_News ");
            strSql.Append(" where FileID=@FileID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FileID", SqlDbType.BigInt,8)         };
            parameters[0].Value = FileID;


            Model.N_News model = new Model.N_News();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileID"].ToString() != "")
                {
                    model.FileID = long.Parse(ds.Tables[0].Rows[0]["FileID"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.No = ds.Tables[0].Rows[0]["No"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = long.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContentType"].ToString() != "")
                {
                    model.ContentType = long.Parse(ds.Tables[0].Rows[0]["ContentType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.Keyword = ds.Tables[0].Rows[0]["Keyword"].ToString();
                model.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                model.Source = ds.Tables[0].Rows[0]["Source"].ToString();
                model.VersionNo = ds.Tables[0].Rows[0]["VersionNo"].ToString();
                if (ds.Tables[0].Rows[0]["Pagination"].ToString() != "")
                {
                    model.Pagination = int.Parse(ds.Tables[0].Rows[0]["Pagination"].ToString());
                }
                model.ReviseNo = ds.Tables[0].Rows[0]["ReviseNo"].ToString();
                if (ds.Tables[0].Rows[0]["RevisorID"].ToString() != "")
                {
                    model.RevisorID = long.Parse(ds.Tables[0].Rows[0]["RevisorID"].ToString());
                }
                model.ReviseReason = ds.Tables[0].Rows[0]["ReviseReason"].ToString();
                model.ReviseContent = ds.Tables[0].Rows[0]["ReviseContent"].ToString();
                if (ds.Tables[0].Rows[0]["ReviseTime"].ToString() != "")
                {
                    model.ReviseTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReviseTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BeginTime"].ToString() != "")
                {
                    model.BeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NextExecutorID"].ToString() != "")
                {
                    model.NextExecutorID = long.Parse(ds.Tables[0].Rows[0]["NextExecutorID"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUse"].ToString() != "")
                {
                    model.IsUse = int.Parse(ds.Tables[0].Rows[0]["IsUse"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorID"].ToString() != "")
                {
                    model.CreatorID = long.Parse(ds.Tables[0].Rows[0]["CreatorID"].ToString());
                }
                model.CreatorName = ds.Tables[0].Rows[0]["CreatorName"].ToString();
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataUpdateTime"].ToString());
                }
                //if (ds.Tables[0].Rows[0]["DataTimeStamp"].ToString() != "")
                //{
                //    model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["DataTimeStamp"].ToString());
                //}
                if (ds.Tables[0].Rows[0]["FTypeTreeId"].ToString() != "")
                {
                    model.FTypeTreeId = long.Parse(ds.Tables[0].Rows[0]["FTypeTreeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OriginalFileID"].ToString() != "")
                {
                    model.OriginalFileID = long.Parse(ds.Tables[0].Rows[0]["OriginalFileID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckerId"].ToString() != "")
                {
                    model.CheckerId = long.Parse(ds.Tables[0].Rows[0]["CheckerId"].ToString());
                }
                model.CheckerName = ds.Tables[0].Rows[0]["CheckerName"].ToString();
                if (ds.Tables[0].Rows[0]["ApprovalId"].ToString() != "")
                {
                    model.ApprovalId = long.Parse(ds.Tables[0].Rows[0]["ApprovalId"].ToString());
                }
                model.ApprovalName = ds.Tables[0].Rows[0]["ApprovalName"].ToString();
                if (ds.Tables[0].Rows[0]["PublisherId"].ToString() != "")
                {
                    model.PublisherId = long.Parse(ds.Tables[0].Rows[0]["PublisherId"].ToString());
                }
                model.PublisherName = ds.Tables[0].Rows[0]["PublisherName"].ToString();
                if (ds.Tables[0].Rows[0]["DrafterId"].ToString() != "")
                {
                    model.DrafterId = long.Parse(ds.Tables[0].Rows[0]["DrafterId"].ToString());
                }
                model.DrafterCName = ds.Tables[0].Rows[0]["DrafterCName"].ToString();
                if (ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsTop"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsTop"].ToString().ToLower() == "true"))
                    {
                        model.IsTop = true;
                    }
                    else
                    {
                        model.IsTop = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsDiscuss"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDiscuss"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDiscuss"].ToString().ToLower() == "true"))
                    {
                        model.IsDiscuss = true;
                    }
                    else
                    {
                        model.IsDiscuss = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Counts"].ToString() != "")
                {
                    model.Counts = int.Parse(ds.Tables[0].Rows[0]["Counts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DrafterDateTime"].ToString() != "")
                {
                    model.DrafterDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DrafterDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckerDateTime"].ToString() != "")
                {
                    model.CheckerDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckerDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApprovalDateTime"].ToString() != "")
                {
                    model.ApprovalDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApprovalDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PublisherDateTime"].ToString() != "")
                {
                    model.PublisherDateTime = DateTime.Parse(ds.Tables[0].Rows[0]["PublisherDateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSyncWeiXin"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSyncWeiXin"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSyncWeiXin"].ToString().ToLower() == "true"))
                    {
                        model.IsSyncWeiXin = true;
                    }
                    else
                    {
                        model.IsSyncWeiXin = false;
                    }
                }
                model.WeiXinContent = ds.Tables[0].Rows[0]["WeiXinContent"].ToString();
                model.ThumbnailsPath = ds.Tables[0].Rows[0]["ThumbnailsPath"].ToString();
                model.ThumbnailsMemo = ds.Tables[0].Rows[0]["ThumbnailsMemo"].ToString();
                model.Media_id = ds.Tables[0].Rows[0]["Media_id"].ToString();
                model.Thumb_media_id = ds.Tables[0].Rows[0]["Thumb_media_id"].ToString();
                model.WeiXinTitle = ds.Tables[0].Rows[0]["WeiXinTitle"].ToString();
                model.WeiXinAuthor = ds.Tables[0].Rows[0]["WeiXinAuthor"].ToString();
                model.WeiXinDigest = ds.Tables[0].Rows[0]["WeiXinDigest"].ToString();
                model.WeiXinUrl = ds.Tables[0].Rows[0]["WeiXinUrl"].ToString();
                model.WeiXinContent_source_url = ds.Tables[0].Rows[0]["WeiXinContent_source_url"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM N_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public DataSet GetList(string where, int page, int limit, string Sort,string EmpId)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " DispOrder asc ,DataAddTime desc " : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();//select count(*) from N_News_ReadingLog where FileID=N_News.FileID and ReaderID=" + ZhiFangUserID + "
            strSql.Append("select top " + limit + "  *,(select count(*) from N_News_ReadingLog  where FileID=N_News.FileID and ReaderID=" + EmpId + ") as ReadCount from N_News where FileID not in  ");
            strSql.Append("(select top " + (limit * (page - 1)) + " FileID from N_News where 1=1 and " + where + "and IsUse=1 order by " + Sort + "  ) and " + where + " and IsUse=1 order by " + Sort + " ");
            ZhiFang.Common.Log.Log.Debug("ZhiFang.DAL.MsSql.Weblis.DNNews.GetList.sql:"+ strSql.ToString());

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string where, int page, int limit, string Sort)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            Sort = (Sort == null || Sort.Trim() == "") ? " DispOrder asc ,DataAddTime desc " : Sort;
            page = page < 1 ? 1 : page;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + limit + "  * from N_News where FileID not in  ");
            strSql.Append("(select top " + (limit * (page - 1)) + " FileID from N_News where 1=1 and " + where + " order by " + Sort + "  ) and " + where + " order by " + Sort + " ");
            ZhiFang.Common.Log.Log.Debug("ZhiFang.DAL.MsSql.Weblis.DNNews.GetList.sql:" + strSql.ToString());

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetCount(string where)
        {
            if (where == null || where.Trim() == "")
            {
                where = "1=1";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from N_News where 1=1 and " + where + "  ");
            int count;
            if (int.TryParse(DbHelperSQL.ExecuteScalar(strSql.ToString()), out count))
            {
                return count;
            }
            else
            {
                return 0;
            }
        }

        public bool NNewsApproval(string idlist, bool approvalFlag, string Memo,string empid, string empname)
        {
            string where = " FileId in (" + idlist + ") and IsUse=1 and ApprovalId=" + empid + " and ( Status=" + ZhiFang.Model.NNewsStatus.起草.Key + " or Status=" + ZhiFang.Model.NNewsStatus.发布撤回.Key + " )";
            StringBuilder strSql = new StringBuilder();
            string status = Model.NNewsStatus.审批通过.Key;
            if(!approvalFlag)
                status = Model.NNewsStatus.审批退回.Key;
            strSql.Append("update N_News set status=" + status + "  ,ApprovalDateTime='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ "' , Memo='"+ Memo + "'  where 1=1 and " + where + "  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString())>0;
        }

        public bool NNewsPublish(string idlist, bool PublishFlag, string Memo, string empid, string empname)
        {
            string where = " FileId in (" + idlist + ") and  Status in (" + ZhiFang.Model.NNewsStatus.审批通过.Key + "," + Model.NNewsStatus.发布撤回.Key + ")";
            StringBuilder strSql = new StringBuilder();
            string status = Model.NNewsStatus.发布.Key;
            if (!PublishFlag)
            {
                status = Model.NNewsStatus.发布撤回.Key;
                where = " FileId in (" + idlist + ") and  Status in (" + ZhiFang.Model.NNewsStatus.审批通过.Key + "," + Model.NNewsStatus.发布.Key + ")";
            }
            strSql.Append("update N_News set status=" + status + "  ,PublisherId='" + empid+ "',PublisherName='" + empname + "',PublisherDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , ReviseReason='" + Memo + "'  where 1=1 and " + where + "  ");
            ZhiFang.Common.Log.Log.Debug("NNewsPublish.sql:"+ strSql.ToString());
             DbHelperSQL.ExecuteNonQuery(strSql.ToString()) ;
            return true;
        }
    }
}

