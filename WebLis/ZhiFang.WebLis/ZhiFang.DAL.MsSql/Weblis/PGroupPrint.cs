using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;
using ZhiFang.Common.Public;
namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类PGroupPrint。
    /// </summary>
    public class PGroupPrint : BaseDALLisDB, IDPGroupPrint
    {
        public PGroupPrint(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public PGroupPrint()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
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
        public bool Exists(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PGroupPrint");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PGroupPrint model)
        {
            try
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
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    if (model.ModelTitleType != null)
                    {
                        strSql1.Append("ModelTitleType,");
                        strSql2.Append("" + model.ModelTitleType + ",");
                    }
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
                Common.Log.Log.Info("添加小组模板" + strSql.ToString());
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
            catch (Exception ex)
            {
                Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.PGroupPrint model)
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                if (model.ModelTitleType != null)
                {
                    strSql.Append("ModelTitleType=" + model.ModelTitleType + ",");
                }
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PGroupPrint ");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.PGroupPrint GetModel(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",ModelTitleType");
            }
            strSql.Append(" from PGroupPrint ");
            strSql.Append(" where Id=" + Id + " ");
            ZhiFang.Model.PGroupPrint model = new ZhiFang.Model.PGroupPrint();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                    model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
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
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    if (ds.Tables[0].Rows[0]["ModelTitleType"].ToString() != "")
                    {
                        model.ModelTitleType = int.Parse(ds.Tables[0].Rows[0]["ModelTitleType"].ToString());
                    }
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",ModelTitleType");
            }
            strSql.Append(" FROM PGroupPrint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.PGroupPrint model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    strSql.Append(",ModelTitleType");
                }
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
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    if (model.ModelTitleType != null)
                    {
                        strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                    }
                }
                strSql.Append(" order by Sort asc,Id desc ");
                Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.ToString());
                return null;

            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_No_Name(ZhiFang.Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT dbo.PGroup.CName AS SectionName, dbo.PGroupPrint.Id, dbo.PGroupPrint.SectionNo, dbo.PGroupPrint.PrintFormatNo, dbo.PGroupPrint.AddTime, dbo.PGroupPrint.ClientNo, dbo.PGroupPrint.Sort, dbo.PGroupPrint.SpecialtyItemNo, dbo.PGroupPrint.UseFlag, dbo.PGroupPrint.ItemMinNumber, dbo.PGroupPrint.ItemMaxNumber, dbo.PGroupPrint.SickTypeNo,dbo.CLIENTELE.CNAME AS ClientName, dbo.PrintFormat.PrintFormatName, dbo.TestItem.CName AS SpecialtyItemName   ");
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",dbo.PGroupPrint.ModelTitleType");
            }
            strSql.Append(" FROM dbo.PrintFormat INNER JOIN dbo.PGroupPrint ON dbo.PrintFormat.Id = dbo.PGroupPrint.PrintFormatNo INNER JOIN dbo.PGroup ON dbo.PGroupPrint.SectionNo = dbo.PGroup.SectionNo LEFT OUTER JOIN dbo.CLIENTELE ON dbo.PGroupPrint.ClientNo = dbo.CLIENTELE.ClIENTNO LEFT OUTER JOIN dbo.TestItem ON dbo.PGroupPrint.SpecialtyItemNo = dbo.TestItem.ItemNo where 1=1 ");
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                if (model.ModelTitleType != null)
                {
                    strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                }
            }
            strSql.Append(" order by Sort asc,dbo.PGroupPrint.Id desc ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",ModelTitleType");
            }
            strSql.Append(" FROM PGroupPrint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        #endregion  成员方法

        #region IDataBase<PGroupPrint> 成员

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PGroupPrint");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.PGroupPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PGroupPrint where 1=1 RIGHT OUTER JOIN dbo.PGroupPrint ON dbo.PGroup.SectionNo = dbo.PGroupPrint.SectionNo LEFT OUTER JOIN dbo.PrintFormat ON dbo.PGroupPrint.PrintFormatNo = dbo.PrintFormat.Id LEFT OUTER JOIN dbo.CLIENTELE ON dbo.PGroupPrint.ClientNo = dbo.CLIENTELE.ClIENTNO LEFT OUTER JOIN dbo.TestItem ON dbo.PGroupPrint.SpecialtyItemNo = dbo.TestItem.ItemNo ");
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
            if (model.PrintFormatName != null)
            {
                strSql.Append(" and  PrintFormatName like '%" + model.PrintFormatName + "%'");
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                if (model.ModelTitleType != null)
                {
                    strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                }
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        #endregion

        #region IDataBase<PGroupPrint> 成员


        public DataSet GetList(int Top, Model.PGroupPrint model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",ModelTitleType");
            }
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
            if (model.ModelTitleType != null)
            {
                strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
            }
            strSql.Append(" order by Sort asc,Id desc ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo ");
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",ModelTitleType");
            }
            strSql.Append(" FROM PGroupPrint ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDPGroupPrint 成员
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.PGroupPrint model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from PGroupPrint left join PGroupPrintControl on PGroupPrint.Id=PGroupPrintControl.Id ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and PGroupPrintControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where Id not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " Id from  PGroupPrint left join PGroupPrintControl on PGroupPrint.Id=PGroupPrintControl.Id ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and PGroupPrintControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by PGroupPrint.Id ) order by PGroupPrint.Id ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from PGroupPrint where Id not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " Id from PGroupPrint order by Id) order by Id  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }
        #endregion

        #region IDataBase<PGroupPrint> 成员
        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["Id"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PGroupPrint (");
                strSql.Append("SectionNo,PrintFormatNo,AddTime,ClientNo,Sort,SpecialtyItemNo,UseFlag,ImageFlag,AntiFlag,ItemMinNumber,ItemMaxNumber,BatchPrint,SickTypeNo");
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    strSql.Append(",ModelTitleType");
                }
                strSql.Append(") values (");
                strSql.Append("'" + dr["SectionNo"].ToString().Trim() + "','" + dr["PrintFormatNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["ClientNo"].ToString().Trim() + "','" + dr["Sort"].ToString().Trim() + "','" + dr["SpecialtyItemNo"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "','" + dr["ImageFlag"].ToString().Trim() + "','" + dr["AntiFlag"].ToString().Trim() + "','" + dr["ItemMinNumber"].ToString().Trim() + "','" + dr["ItemMaxNumber"].ToString().Trim() + "','" + dr["BatchPrint"].ToString().Trim() + "','" + dr["SickTypeNo"].ToString().Trim() + "'");
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    strSql.Append(",'" + dr["ModelTitleType"].ToString().Trim() + "'");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PGroupPrint set ");

                strSql.Append(" SectionNo = '" + dr["SectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" PrintFormatNo = '" + dr["PrintFormatNo"].ToString().Trim() + "' , ");
                strSql.Append(" ClientNo = '" + dr["ClientNo"].ToString().Trim() + "' , ");
                strSql.Append(" Sort = '" + dr["Sort"].ToString().Trim() + "' , ");
                strSql.Append(" SpecialtyItemNo = '" + dr["SpecialtyItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                strSql.Append(" ImageFlag = '" + dr["ImageFlag"].ToString().Trim() + "' , ");
                strSql.Append(" AntiFlag = '" + dr["AntiFlag"].ToString().Trim() + "' , ");
                strSql.Append(" ItemMinNumber = '" + dr["ItemMinNumber"].ToString().Trim() + "' , ");
                strSql.Append(" ItemMaxNumber = '" + dr["ItemMaxNumber"].ToString().Trim() + "' , ");
                strSql.Append(" BatchPrint = '" + dr["BatchPrint"].ToString().Trim() + "' , ");
                strSql.Append(" SickTypeNo = '" + dr["SickTypeNo"].ToString().Trim() + "'  ");
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    strSql.Append(", ModelTitleType = '" + dr["ModelTitleType"].ToString().Trim() + "'  ");
                }
                strSql.Append(" where Id='" + dr["Id"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }


        #endregion


        #region 小组模版设置
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_No_Name(ZhiFang.Model.PGroupFormat model, int nowpagenum, int nowpagesize)
        {
            string strtop = "";
            if (nowpagesize != 0)
            {
                strtop = " top " + nowpagesize;
            }

            string strpage = "";
            if (nowpagenum < 0)
            {
                nowpagenum = 0;
                
            }
            strpage = "top " + (nowpagesize * nowpagenum);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + strtop + " PGroup.CName AS SectionName, PGroupPrint.Id,PrintFormat.id PrintFormatNo,PGroupPrint.AntiFlag,PGroupPrint.BatchPrint,PGroupPrint.ImageFlag, PGroupPrint.SectionNo, PGroupPrint.PrintFormatNo, PGroupPrint.AddTime, PGroupPrint.ClientNo, PGroupPrint.Sort, PGroupPrint.SpecialtyItemNo, PGroupPrint.UseFlag, PGroupPrint.ItemMinNumber, PGroupPrint.ItemMaxNumber, PGroupPrint.SickTypeNo,CLIENTELE.CNAME AS ClientName, PrintFormat.PrintFormatName, TestItem.CName AS SpecialtyItemName,PGroupPrint.ModelTitleType   ");
            //if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            //{
            //    strSql.Append("");
            //}
            strSql.Append(" FROM dbo.PGroup RIGHT OUTER JOIN dbo.PGroupPrint ON dbo.PGroup.SectionNo = dbo.PGroupPrint.SectionNo LEFT OUTER JOIN dbo.PrintFormat ON dbo.PGroupPrint.PrintFormatNo = dbo.PrintFormat.Id LEFT OUTER JOIN dbo.CLIENTELE ON dbo.PGroupPrint.ClientNo = dbo.CLIENTELE.ClIENTNO LEFT OUTER JOIN dbo.TestItem ON dbo.PGroupPrint.SpecialtyItemNo = dbo.TestItem.ItemNo where 1=1 ");
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

            if (model.SectionName != null)
            {
                strSql.Append(" and  PGroup.Cname like '%" + model.SectionName + "%'");
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                if (model.ModelTitleType != null)
                {
                    strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                }
            }
            if (nowpagesize != 0)
            {
                strSql.Append(" and  PGroupPrint.id not in ( ");

                strSql.Append("SELECT " + strpage + "  PGroupPrint.Id   ");
                strSql.Append(" FROM dbo.PGroup RIGHT OUTER JOIN dbo.PGroupPrint ON dbo.PGroup.SectionNo = dbo.PGroupPrint.SectionNo LEFT OUTER JOIN dbo.PrintFormat ON dbo.PGroupPrint.PrintFormatNo = dbo.PrintFormat.Id LEFT OUTER JOIN  dbo.CLIENTELE ON dbo.PGroupPrint.ClientNo = dbo.CLIENTELE.ClIENTNO LEFT OUTER JOIN dbo.TestItem ON dbo.PGroupPrint.SpecialtyItemNo = dbo.TestItem.ItemNo where 1=1 ");
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
                if (model.SectionName != null)
                {
                    strSql.Append(" and  PGroup.Cname like '%" + model.SectionName + "%'");
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
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    if (model.ModelTitleType != null)
                    {
                        strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                    }
                }
                strSql.Append(" order by PGroupPrint.Id desc ) order by PGroupPrint.Id desc ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public int GetTotalCount(Model.PGroupFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PGroupPrint  left OUTER JOIN PGroup ON PGroup.SectionNo = PGroupPrint.SectionNo LEFT OUTER JOIN PrintFormat ON PGroupPrint.PrintFormatNo = PrintFormat.Id LEFT OUTER JOIN CLIENTELE ON PGroupPrint.ClientNo = CLIENTELE.ClIENTNO LEFT OUTER JOIN TestItem ON PGroupPrint.SpecialtyItemNo = TestItem.ItemNo where 1=1 ");
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
            if (model.SectionName != null)
            {
                strSql.Append(" and  PGroup.CName like '%" + model.SectionName + "%'");
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
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                if (model.ModelTitleType != null)
                {
                    strSql.Append(" and ModelTitleType=" + model.ModelTitleType + "");
                }
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }


        public DataSet GetModelByID(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PGroup.CName AS SectionName, PGroupPrint.Id,PGroupPrint.AntiFlag,PGroupPrint.BatchPrint,PGroupPrint.ImageFlag, PGroupPrint.SectionNo, PGroupPrint.PrintFormatNo, PGroupPrint.AddTime, PGroupPrint.ClientNo, PGroupPrint.Sort, PGroupPrint.SpecialtyItemNo, PGroupPrint.UseFlag, PGroupPrint.ItemMinNumber, PGroupPrint.ItemMaxNumber, PGroupPrint.SickTypeNo,CLIENTELE.CNAME AS ClientName, PrintFormat.PrintFormatName, TestItem.CName AS SpecialtyItemName   ");
            if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
            {
                strSql.Append(",PGroupPrint.ModelTitleType");
            }
            strSql.Append(" FROM PrintFormat INNER JOIN PGroupPrint ON PrintFormat.Id = PGroupPrint.PrintFormatNo INNER JOIN PGroup ON PGroupPrint.SectionNo = PGroup.SectionNo LEFT OUTER JOIN CLIENTELE ON PGroupPrint.ClientNo = CLIENTELE.ClIENTNO LEFT OUTER JOIN TestItem ON PGroupPrint.SpecialtyItemNo = TestItem.ItemNo where 1=1 ");
            strSql.Append(" and PGroupPrint.id ='" + id + "'");

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCountJoin()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT   COUNT(*) FROM PrintFormat INNER JOIN PGroupPrint ON PrintFormat.Id = PGroupPrint.PrintFormatNo INNER JOIN PGroup ON PGroupPrint.SectionNo = PGroup.SectionNo LEFT OUTER JOIN CLIENTELE ON PGroupPrint.ClientNo = CLIENTELE.ClIENTNO LEFT OUTER JOIN TestItem ON PGroupPrint.SpecialtyItemNo = TestItem.ItemNo where 1=1  ");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }
        #endregion
    }
}