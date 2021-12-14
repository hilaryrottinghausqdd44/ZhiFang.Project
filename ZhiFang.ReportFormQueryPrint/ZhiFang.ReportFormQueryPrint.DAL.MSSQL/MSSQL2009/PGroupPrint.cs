using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
	/// <summary>
	/// 数据访问类PGroupPrint。
	/// </summary>
    public class PGroupPrint : IDPGroupPrint
    {
        public PGroupPrint()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "PGroupPrint");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PGroupPrint");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SectionNo != null)
            {
                strSql1.Append("SectionNo,");
                strSql2.Append("" + model.SectionNo + ",");
            }
            if (model.PrintFormatNo != null)
            {
                strSql1.Append("PrintFormatNo,");
                strSql2.Append("" + model.PrintFormatNo + ",");
            }
            if (model.ClientNo != null)
            {
                strSql1.Append("ClientNo,");
                strSql2.Append("" + model.ClientNo + ",");
            }
            if (model.Sort != null)
            {
                strSql1.Append("Sort,");
                strSql2.Append("" + model.Sort + ",");
            }
            if (model.SpecialtyItemNo != null)
            {
                strSql1.Append("SpecialtyItemNo,");
                strSql2.Append("" + model.SpecialtyItemNo + ",");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            if (model.ImageFlag != null)
            {
                strSql1.Append("ImageFlag,");
                strSql2.Append("" + model.ImageFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql1.Append("AntiFlag,");
                strSql2.Append("" + model.AntiFlag + ",");
            }
            if (model.ItemMinNumber != null)
            {
                strSql1.Append("ItemMinNumber,");
                strSql2.Append("" + model.ItemMinNumber + ",");
            }
            if (model.ItemMaxNumber != null)
            {
                strSql1.Append("ItemMaxNumber,");
                strSql2.Append("" + model.ItemMaxNumber + ",");
            }
            if (model.BatchPrint != null)
            {
                strSql1.Append("BatchPrint,");
                strSql2.Append("" + model.BatchPrint + ",");
            }
            if (model.SickTypeNo != null)
            {
                strSql1.Append("SickTypeNo,");
                strSql2.Append("" + model.SickTypeNo + ",");
            }
            strSql.Append("insert into PGroupPrint(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PGroupPrint set ");
            if (model.SectionNo != null)
            {
                strSql.Append("SectionNo=" + model.SectionNo + ",");
            }
            if (model.PrintFormatNo != null)
            {
                strSql.Append("PrintFormatNo=" + model.PrintFormatNo + ",");
            }
            if (model.ClientNo != null)
            {
                strSql.Append("ClientNo=" + model.ClientNo + ",");
            }
            if (model.Sort != null)
            {
                strSql.Append("Sort=" + model.Sort + ",");
            }
            if (model.SpecialtyItemNo != null)
            {
                strSql.Append("SpecialtyItemNo=" + model.SpecialtyItemNo + ",");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append("ImageFlag=" + model.ImageFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append("AntiFlag=" + model.AntiFlag + ",");
            }
            if (model.ItemMinNumber != null)
            {
                strSql.Append("ItemMinNumber=" + model.ItemMinNumber + ",");
            }
            if (model.ItemMaxNumber != null)
            {
                strSql.Append("ItemMaxNumber=" + model.ItemMaxNumber + ",");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append("BatchPrint=" + model.BatchPrint + ",");
            }
            if (model.SickTypeNo != null)
            {
                strSql.Append("SickTypeNo=" + model.SickTypeNo + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PGroupPrint ");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.PGroupPrint GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            strSql.Append(" from PGroupPrint ");
            strSql.Append(" where Id=" + Id + " ");
            Model.PGroupPrint model = new Model.PGroupPrint();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PrintFormatNo"].ToString() != "")
                {
                    model.PrintFormatNo = int.Parse(ds.Tables[0].Rows[0]["PrintFormatNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClientNo"].ToString() != "")
                {
                    model.ClientNo = int.Parse(ds.Tables[0].Rows[0]["ClientNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SpecialtyItemNo"].ToString() != "")
                {
                    model.SpecialtyItemNo = int.Parse(ds.Tables[0].Rows[0]["SpecialtyItemNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImageFlag"].ToString() != "")
                {
                    model.ImageFlag = int.Parse(ds.Tables[0].Rows[0]["ImageFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AntiFlag"].ToString() != "")
                {
                    model.AntiFlag = int.Parse(ds.Tables[0].Rows[0]["AntiFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemMinNumber"].ToString() != "")
                {
                    model.ItemMinNumber = int.Parse(ds.Tables[0].Rows[0]["ItemMinNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemMaxNumber"].ToString() != "")
                {
                    model.ItemMaxNumber = int.Parse(ds.Tables[0].Rows[0]["ItemMaxNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BatchPrint"].ToString() != "")
                {
                    model.BatchPrint = int.Parse(ds.Tables[0].Rows[0]["BatchPrint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SickTypeNo"].ToString() != "")
                {
                    model.SickTypeNo = int.Parse(ds.Tables[0].Rows[0]["SickTypeNo"].ToString());
                }
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
            strSql.Append("select Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            strSql.Append(" FROM PGroupPrint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            strSql.Append(" FROM PGroupPrint where 1=1 ");
            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + "");
            }
            if (model.PrintFormatNo != null)
            {
                strSql.Append(" and PrintFormatNo=" + model.PrintFormatNo + "");
            }
            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo=" + model.ClientNo + "");
            }
            if (model.Sort != null)
            {
                strSql.Append(" and Sort=" + model.Sort + "");
            }
            if (model.SpecialtyItemNo != null)
            {
                strSql.Append(" and SpecialtyItemNo=" + model.SpecialtyItemNo + "");
            }
            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + "");
            }

            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
            }


            if (model.ItemMinNumber != null)
            {
                strSql.Append(" and ItemMinNumber<=" + model.ItemMinNumber + "");
            }
            if (model.ItemMaxNumber != null)
            {
                strSql.Append(" and ItemMaxNumber>=" + model.ItemMaxNumber + "");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.SickTypeNo != null)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + "");
            }

            strSql.Append(" order by Sort asc,Id desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_No_Name(Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT     dbo.PGroup.CName AS SectionName, dbo.PGroupPrint.Id, dbo.PGroupPrint.SectionNo, dbo.PGroupPrint.PrintFormatNo, dbo.PGroupPrint.AddTime, dbo.PGroupPrint.ClientNo, dbo.PGroupPrint.Sort, dbo.PGroupPrint.SpecialtyItemNo, dbo.PGroupPrint.UseFlag, dbo.PGroupPrint.ItemMinNumber, dbo.PGroupPrint.ItemMaxNumber, dbo.CLIENTELE.CNAME AS ClientName, dbo.PrintFormat.PrintFormatName,dbo.PrintFormat.SickTypeNo, dbo.TestItem.CName AS SpecialtyItemName   ");

            strSql.Append(" FROM         dbo.PrintFormat INNER JOIN dbo.PGroupPrint ON dbo.PrintFormat.Id = dbo.PGroupPrint.PrintFormatNo INNER JOIN dbo.PGroup ON dbo.PGroupPrint.SectionNo = dbo.PGroup.SectionNo LEFT OUTER JOIN dbo.CLIENTELE ON dbo.PGroupPrint.ClientNo = dbo.CLIENTELE.ClIENTNO LEFT OUTER JOIN dbo.TestItem ON dbo.PGroupPrint.SpecialtyItemNo = dbo.TestItem.ItemNo where 1=1 ");
            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + "");
            }
            if (model.PrintFormatNo != null)
            {
                strSql.Append(" and PrintFormatNo=" + model.PrintFormatNo + "");
            }
            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo=" + model.ClientNo + "");
            }
            if (model.Sort != null)
            {
                strSql.Append(" and Sort=" + model.Sort + "");
            }
            if (model.SpecialtyItemNo != null)
            {
                strSql.Append(" and SpecialtyItemNo=" + model.SpecialtyItemNo + "");
            }
            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + "");
            }

            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
            }


            if (model.ItemMinNumber != null)
            {
                strSql.Append(" and ItemMinNumber<=" + model.ItemMinNumber + "");
            }
            if (model.ItemMaxNumber != null)
            {
                strSql.Append(" and ItemMaxNumber>=" + model.ItemMaxNumber + "");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.SickTypeNo != null)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + "");
            }
            strSql.Append(" order by Sort asc,dbo.PGroupPrint.Id desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            strSql.Append(" FROM PGroupPrint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */

        #endregion  成员方法
    }
}

