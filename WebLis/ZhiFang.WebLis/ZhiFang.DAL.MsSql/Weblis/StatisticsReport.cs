using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class StatisticsReport : BaseDALLisDB, IDStatisticsReport
    {
        public StatisticsReport(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public StatisticsReport()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        public DataTable ReportFormGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            #region PROCEDURE
            //            CREATE PROCEDURE[dbo].[Statistics_ReportFormGroupByClientNoAndReportDate]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(150), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int

            //    AS
            //BEGIN
            //declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null and WeblisSourceOrgId in ('+@clientNoList+') ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null  ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //if(@clientNoList='')
            //begin
            //set @sqlstr='select StatisticsDate,WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from 
            //(select SUBSTRING(Convert(varchar, UploadDate,120),0,11) as StatisticsDate ,WeblisSourceOrgId,WebLisSourceOrgName from ReportFormFull
            // where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null  ) rf
            // group by StatisticsDate, WeblisSourceOrgId, WebLisSourceOrgName';
            //end
            //    exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Statistics_ReportFormGroupByClientNoAndReportDate", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable ReportFormGroupByClientNo(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            #region PROCEDURE
            //            CREATE PROCEDURE[dbo].[Statistics_ReportFormGroupByClientNo]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(150), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int

            //    AS
            //BEGIN
            //declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null and WeblisSourceOrgId in ('+@clientNoList+') group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //if(@clientNoList='')
            //begin
            //set @sqlstr='select WeblisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount  from ReportFormFull
            //where UploadDate>='''+@startDateTime+''' and UploadDate<='''+@endDateTime+''' and WeblisSourceOrgId is not null group by WeblisSourceOrgId,WebLisSourceOrgName';
            //end
            //    exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Statistics_ReportFormGroupByClientNo", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsRequestItemCenter(string startDateTime, string endDateTime, string clientNoList, int dateType = 0)
        {
            #region PROCEDURE
            //            CREATE PROCEDURE[dbo].[StatisticsRequestItemCenter]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(50), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int
            //    AS
            //BEGIN
            //    declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin

            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.CombiItemNo) as CombiItemName,
            //				dbo.NRequestItem.ParItemNo, 
            //(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo) as ParItemName,
            //(select top 1 price from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemPrice,
            //(select top 1 lowprice from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemlowPrice
            //FROM      dbo.NRequestForm INNER JOIN
            //                dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.WebLisSourceOrgID in ('+@clientNoList+')  and NRequestForm.OperDate>='''+@startDateTime+''' and NRequestForm.OperDate<='''+@endDateTime+'''

            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.CombiItemNo) as CombiItemName,
            //				dbo.NRequestItem.ParItemNo, 
            //(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo) as ParItemName,
            //(select top 1 price from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemPrice,
            //(select top 1 lowprice from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemlowPrice
            //FROM      dbo.NRequestForm INNER JOIN
            //                dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.OperDate>='''+@startDateTime+''' and NRequestForm.OperDate<='''+@endDateTime+'''

            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList= '')
            //begin
            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.CombiItemNo) as CombiItemName,
            //				dbo.NRequestItem.ParItemNo, 
            //(select top 1 CName from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo) as ParItemName,
            //(select top 1 price from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemPrice,
            //(select top 1 lowprice from TestItem where TestItem.ItemNo= NRequestItem.ParItemNo ) as ParItemlowPrice
            //FROM      dbo.NRequestForm INNER JOIN
            //                dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.OperDate>='''+@startDateTime+''' and NRequestForm.OperDate<='''+@endDateTime+'''

            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end
            //    exec (@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsRequestItemCenter", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsRequestItemClient(string startDateTime, string endDateTime, string clientNoList, int dateType = 0)
        {
            #region PROCEDURE
            //            ALTER PROCEDURE[dbo].[StatisticsRequestItemClient]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(50), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int
            //    AS
            //BEGIN
            //    declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin

            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemName,
            //dbo.NRequestItem.ParItemNo, 
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemName,


            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN


            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.WebLisSourceOrgID in ('+@clientNoList+')  and NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''



            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemName,
            //dbo.NRequestItem.ParItemNo, 
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemName,


            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN


            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''



            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList = '')
            //begin
            //set @sqlstr = 'SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,


            //dbo.NRequestItem.CombiItemNo,


            //[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo) AS CombiItemName,
            //dbo.NRequestItem.ParItemNo,


            //[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo) AS ParItemName,


            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN


            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''



            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo, NRequestItem.ParItemNo';
            //end


            //exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsRequestItemClient", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsRequestDetailItemLab(string startDateTime, string endDateTime, string clientNoList)
        {
            #region PROCEDURE
            //            USE[weblis]
            //GO
            ///****** Object:  StoredProcedure [dbo].[StatisticsRequestDetailItemLab]    Script Date: 2019-11-11 18:10:00 ******/
            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO

            //ALTER PROCEDURE[dbo].[StatisticsRequestDetailItemLab]

            //    @startDateTime varchar(50), --开始时间
            //@endDateTime varchar(50), --结束时间
            //@clientNoList varchar(5000), --单位ID数组
            //@dateType int
            //AS
            //BEGIN

            //    declare @sqlstr varchar(5000);
            //            if (@clientNoList <> '')
            //                begin

            //                set @sqlstr = 'SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,

            //                dbo.NRequestItem.ParItemNo, 

            //                [dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.WebLisSourceOrgID in ('+@clientNoList+')  and NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //				dbo.NRequestItem.ParItemNo, 
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo';
            //end
            //if(@clientNoList = '')
            //begin
            //set @sqlstr = 'SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,

            //dbo.NRequestItem.ParItemNo,

            //[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo) AS ParItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo) AS ParItemPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.ParItemNo)) as ParItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.ParItemNo';
            //end

            //exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = 0;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsRequestDetailItemLab", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsRequestCombiItemLab(string startDateTime, string endDateTime, string clientNoList)
        {
            #region PROCEDURE
            /****** Object:  StoredProcedure [dbo].[StatisticsRequestCombiItemLab]    Script Date: 2019-11-30 21:42:12 ******/
            //            SET ANSI_NULLS ON
            //            GO
            //SET QUOTED_IDENTIFIER ON
            //GO

            //ALTER PROCEDURE[dbo].[StatisticsRequestCombiItemLab]
            //        @startDateTime varchar(50), --开始时间
            //    @endDateTime varchar(50), --结束时间
            //    @clientNoList varchar(5000), --单位ID数组
            //    @dateType int
            //    AS
            //BEGIN
            //    declare @sqlstr varchar(5000);
            //if(@clientNoList<>'')
            //begin

            //set @sqlstr='SELECT   dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemPrice,

            //[dbo].[GetBLabTestItemMarketPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemMarketPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo)) as CombiItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where NRequestForm.WebLisSourceOrgID in ('+@clientNoList+')  and NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo';
            //end
            //if(@clientNoList is null)
            //begin
            //set @sqlstr='SELECT   dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,
            //                dbo.NRequestItem.CombiItemNo,
            //				[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemPrice,

            //[dbo].[GetBLabTestItemMarketPrice](NRequestForm.WebLisSourceOrgID,dbo.NRequestItem.CombiItemNo) AS CombiItemMarketPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo)) as CombiItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo';
            //end
            //if(@clientNoList = '')
            //begin
            //set @sqlstr = 'SELECT    dbo.NRequestForm.WebLisSourceOrgID, dbo.NRequestForm.WebLisSourceOrgName,

            //dbo.NRequestItem.CombiItemNo,

            //[dbo].[GetBLabTestItemName](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo) AS CombiItemName,

            //[dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo) AS CombiItemPrice,

            //[dbo].[GetBLabTestItemMarketPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo) AS CombiItemMarketPrice,
            //count(*) as StatisticsCount,
            //count(*) * ([dbo].[GetBLabTestItemPrice](NRequestForm.WebLisSourceOrgID, dbo.NRequestItem.CombiItemNo)) as CombiItemPriceCount
            //FROM      dbo.NRequestForm INNER JOIN

            //dbo.NRequestItem ON dbo.NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo
            //where  NRequestForm.OperDate >= '''+@startDateTime+''' and NRequestForm.OperDate <= '''+@endDateTime+'''


            //group by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo
            //order by NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName, NRequestItem.CombiItemNo';
            //end

            //exec(@sqlstr);
            //    print(@sqlstr);
            //        END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = 0;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsRequestCombiItemLab", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsBarCodeCountGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            #region PROCEDURE
            //            CREATE  PROCEDURE [dbo].[StatisticsBarCodeCountGroupByClientNoAndReportDate]
            //            @startDateTime varchar(50), --开始时间
            //@endDateTime varchar(150), --结束时间
            //@clientNoList varchar(5000), --单位ID数组
            //@dateType int

            //AS
            //BEGIN
            //declare @sqlstr varchar(5000);
            //            if (@clientNoList <> '')
            //                begin
            //                set @sqlstr = 'select SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10) as StatisticsDate,WebLisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount from BarCodeForm
            //where CollectDate>= '''+@startDateTime+''' and CollectDate<= '''+@endDateTime+''' and WeblisSourceOrgId is not null and WeblisSourceOrgId in ('+@clientNoList+') ) bf
            //  group by SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10),WebLisSourceOrgId,WebLisSourceOrgName';
            //end
            //if (@clientNoList is null)
            //                begin
            //                set @sqlstr = 'select SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10) as StatisticsDate,WebLisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount from BarCodeForm
            //where CollectDate>= '''+@startDateTime+''' and CollectDate<= '''+@endDateTime+''' and WeblisSourceOrgId is not null  ) bf
            //  group by SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10),WebLisSourceOrgId,WebLisSourceOrgName';
            //end
            //if (@clientNoList = '')
            //                begin
            //                set @sqlstr = 'select SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10) as StatisticsDate,WebLisSourceOrgId,WebLisSourceOrgName,count(*) as StatisticsCount from BarCodeForm
            //where CollectDate>= '''+@startDateTime+''' and CollectDate<= '''+@endDateTime+''' and WeblisSourceOrgId is not null  ) bf
            //  group by SUBSTRING(CONVERT(VARCHAR, CollectDate,120),1,10),WebLisSourceOrgId,WebLisSourceOrgName';
            //end

            //    exec(@sqlstr);
            //            print(@sqlstr);
            //            END
            #endregion

            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@dateType", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = dateType;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsBarCodeCountGroupByClientNoAndReportDate", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public Dictionary<string, int> StatisticsGetTestFinishCount(string startDateTime, string endDateTime, string clientNoList)
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            //检验总数
            string BarCodeCountSqlStr = "select COUNT(*) from BarCodeForm where CollectTime >= '" + startDateTime + "' and CollectTime <= '" + endDateTime + "' and " + (clientNoList != "" && clientNoList != null ? "ClientNo in (" + String.Join(",", clientNoList) + ")" : "1=1");
            //检验完成数
            string BarCodeTestFinishCountSqlStr = "select SERIALNO from ReportFormFull where CollectTime >= '" + startDateTime + "' and CollectTime <= '" + endDateTime + "' and " + (clientNoList != "" && clientNoList != null ? "ClientNo in (" + String.Join(",", clientNoList) + ")" : "1=1") + " Group by SERIALNO";
            //检验完成+检验中
            string BarCodeWebLisFlag5SqlStr = "select BarCode from BarCodeForm where WebLisFlag = 5 and CollectTime >= '" + startDateTime + "' and CollectTime <= '" + endDateTime + "' and " + (clientNoList != "" && clientNoList != null ? "ClientNo in (" + String.Join(",", clientNoList) + ")" : "1=1");

            string BarCodeCount = DbHelperSQL.ExecuteScalar(BarCodeCountSqlStr);
            DataSet BarCodeTestFinishCountds = DbHelperSQL.ExecuteDataSet(BarCodeTestFinishCountSqlStr);
            DataSet BarCodeWebLisFlag5ds = DbHelperSQL.ExecuteDataSet(BarCodeWebLisFlag5SqlStr);

            tmp.Add("BarCodeCount", int.Parse(BarCodeCount));//检验总数
            tmp.Add("BarCodeTestFinishCount", BarCodeTestFinishCountds.Tables[0].Rows.Count);//检验完成数
            tmp.Add("BarCodeTestingCount", BarCodeWebLisFlag5ds.Tables[0].Rows.Count - tmp["BarCodeTestFinishCount"]);//检验中
            tmp.Add("BarCodeUnTestCount", tmp["BarCodeCount"] - BarCodeWebLisFlag5ds.Tables[0].Rows.Count);//待检验

            return tmp;
        }

        public DataTable StatisticsGetTestFinish(string startDateTime, string endDateTime, string clientNoList, int Limit)
        {
            string SqlStr = "select top " + Limit + " barcode.ClientNo as LabCode,barcode.ClientName as LabName," +
                                        "count(barcode.ClientNo) as ReceiveBarCodeCount," +
                                        "count(report.CLIENTNO) as FinishBarCodeCount " +
                                        "from BarCodeForm barcode " +
                                        "left join ReportFormFull report on report.SERIALNO = barcode.BarCode " +
                                        "where barcode.weblisflag = 5 and barcode.CollectDate>='" + startDateTime + "' and barcode.CollectDate<='" + endDateTime + "'" +
                                        "and barcode.WeblisSourceOrgId is not null and " + (clientNoList != "" && clientNoList != null ? "barcode.WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "1=1") +
                                        " group by barcode.ClientNo,barcode.ClientName" +
                                        " order by ReceiveBarCodeCount Desc";
            DataSet ds = DbHelperSQL.ExecuteDataSet(SqlStr);
            List<string> IdList = new List<string>();
            DataTable dt = ds.Tables[0].Copy();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        IdList.Add(ds.Tables[0].Rows[i]["LabCode"].ToString());
            //    }
            //}
            //if (IdList.Count() > 0)
            //{
            //    string SqlStr2 = "select HospitalCode,Name from B_Hospital where HospitalID not in (" + string.Join(",", IdList) + ")";
            //    DataSet ds2 = DbHelperSQL.ExecuteDataSet(SqlStr2);
            //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            //    {
            //        for (var i = 0; i < ds2.Tables[0].Rows.Count; i++)
            //        {
            //            if (dt.Rows.Count == Limit)
            //            {
            //                break;
            //            }
            //            DataRow dr = dt.NewRow();
            //            dr["LabCode"] = ds2.Tables[0].Rows[i]["HospitalCode"].ToString();
            //            dr["LabName"] = ds2.Tables[0].Rows[i]["Name"].ToString();
            //            dr["ReceiveBarCodeCount"] = 0;
            //            dr["FinishBarCodeCount"] = 0;
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}


            return dt;
        }

        public DataTable StatisticsGetBarCodeDeliveryInfo(string startDateTime, string endDateTime, string clientNoList, int Limit)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@Limit", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime;
            p[1].Value = endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = Limit;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsGetBarCodeDeliveryInfo", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }
        public DataTable StatisticsGetTestFinishCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@limit", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = limit;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsGetTestFinishCountTop", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsGetBarCodeSendCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@limit", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = limit;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsGetBarCodeSendCountTop", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable StatisticsGetTestItemCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@clientNoList", SqlDbType.VarChar,5000) ,
                        new SqlParameter("@limit", SqlDbType.Int,4)

            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            p[2].Value = clientNoList == null ? "" : clientNoList;
            p[3].Value = limit;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("StatisticsGetTestItemCountTop", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }
        public DataTable StatisticsGetTestFinishCountByYear(string startDateTime, string endDateTime, string clientNoList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReceiveBarCodeCount");
            dt.Columns.Add("TestBarCodeCount");
            dt.Columns.Add("TestFinishRate");
            var year = DateTime.Today.Year;
            string StartDate = year + "-01-01 00:00:00";
            string EndDate = year + "-12-31 23:59:59";
            //检验量
            string SqlStr1 = "select COLLECTDATE from BarCodeForm where WebLisFlag = 5 and CollectDate>='" + StartDate + "' and CollectDate<='" + EndDate + "' and " + (clientNoList != "" && clientNoList != null ? "barcode.WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "1=1");
            //检验完成量
            string SqlStr2 = "select COLLECTDATE from ReportFormFull where CollectDate>='" + StartDate + "' and CollectDate<='" + EndDate + "'  and " + (clientNoList != "" && clientNoList != null ? "barcode.WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "1=1")+" group by SERIALNO,COLLECTDATE";

            DataSet ds1 = DbHelperSQL.ExecuteDataSet(SqlStr1);
            DataSet ds2 = DbHelperSQL.ExecuteDataSet(SqlStr2);

            for (var i = 1; i <= 12; i++)
            {
                int ReceiveBarCodeCount = 0;
                int TestBarCodeCount = 0;
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    for (var j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        if (DateTime.Parse(ds1.Tables[0].Rows[j]["COLLECTDATE"].ToString()).Month == i) ReceiveBarCodeCount++;
                    }
                }
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    for (var a = 0; a < ds2.Tables[0].Rows.Count; a++)
                    {
                        if (DateTime.Parse(ds2.Tables[0].Rows[a]["COLLECTDATE"].ToString()).Month == i) TestBarCodeCount++;
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ReceiveBarCodeCount"] = ReceiveBarCodeCount;
                dr["TestBarCodeCount"] = TestBarCodeCount;
                dr["TestFinishRate"] = (ReceiveBarCodeCount != 0 ? TestBarCodeCount / ReceiveBarCodeCount * 100 : 0);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable StatisticsGetReportCountByYear(string startDateTime, string endDateTime, string clientNoList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SendBarCodeCount");
            dt.Columns.Add("ReportCount");
            var year = DateTime.Today.Year;
            string StartDate = year + "-01-01 00:00:00";
            string EndDate = year + "-12-31 23:59:59";
            //检验量
            string SqlStr1 = "select COLLECTDATE from BarCodeForm where CollectDate>='" + StartDate + "' and CollectDate<='" + EndDate + "' and " + (clientNoList != "" && clientNoList != null ? "barcode.WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "1=1");
            //检验完成量
            string SqlStr2 = "select COLLECTDATE from ReportFormFull where CollectDate>='" + StartDate + "' and CollectDate<='" + EndDate + "' and " + (clientNoList != "" && clientNoList != null ? "barcode.WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "1=1") + " group by SERIALNO,COLLECTDATE";

            DataSet ds1 = DbHelperSQL.ExecuteDataSet(SqlStr1);
            DataSet ds2 = DbHelperSQL.ExecuteDataSet(SqlStr2);

            for (var i = 1; i <= 12; i++)
            {
                int SendBarCodeCount = 0;
                int ReportCount = 0;
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    for (var j = 0; j < ds1.Tables[0].Rows.Count; j++)
                    {
                        if (DateTime.Parse(ds1.Tables[0].Rows[j]["COLLECTDATE"].ToString()).Month == i) SendBarCodeCount++;
                    }
                }
                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    for (var a = 0; a < ds2.Tables[0].Rows.Count; a++)
                    {
                        if (DateTime.Parse(ds2.Tables[0].Rows[a]["COLLECTDATE"].ToString()).Month == i) ReportCount++;
                    }
                }
                DataRow dr = dt.NewRow();
                dr["SendBarCodeCount"] = SendBarCodeCount;
                dr["ReportCount"] = ReportCount;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable StatisticsGetReportCountTop(string startDateTime, string endDateTime, string clientNoList, int Limit)
        {
            var year = DateTime.Today.Year;
            string StartDate = year + "-01-01 00:00:00";
            string EndDate = year + "-12-31 23:59:59";
            //全年报告量排名
            string SqlStr = "select WeblisSourceOrgId as LabCode,WeblisSourceOrgName as LabName,count(*) as ReportCount from ReportFormFull where WeblisSourceOrgId is not null and CHECKDate>='" + StartDate + "' and CHECKDate<='" + EndDate + "'";
            SqlStr += clientNoList != "" && clientNoList != null ? "and WeblisSourceOrgId in (" + String.Join(",", clientNoList) + ")" : "";
            SqlStr += " group by WeblisSourceOrgId,WeblisSourceOrgName";

            SqlStr = " select  top " + Limit + " tmp.LabCode,tmp.LabName,tmp.ReportCount from (" + SqlStr + ") tmp order by tmp.ReportCount desc ";
            DataSet ds = DbHelperSQL.ExecuteDataSet(SqlStr);
            DataTable dt = ds.Tables[0].Copy();
            return dt;
        }

        public DataTable Wuhu_StatisticsGender(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)

            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsGender", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsAge(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsAge", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsInspectionData(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsInspectionData", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsHosptalGrade(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsHosptalGrade", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsUrbanRuralGrade(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsUrbanRuralGrade", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsAreaDetectionQuantity(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsAreaDetectionQuantity", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataTable Wuhu_StatisticsPopInspectionFee(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsPopInspectionFee", p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return dt;
        }

        public DataSet Wuhu_StatisticsDataAnalysis(string startDateTime, string endDateTime)
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = {
                        new SqlParameter("@startDateTime", SqlDbType.VarChar,50) ,
                        new SqlParameter("@endDateTime", SqlDbType.VarChar,50)
            };
            p[0].Value = startDateTime == null ? "" : startDateTime;
            p[1].Value = endDateTime == null ? "" : endDateTime;
            DataSet ds = DbHelperSQL.ExecDataSetStoredProcedure("Wuhu_StatisticsDataAnalysis", p);
            
            return ds;
        }
    }
}
