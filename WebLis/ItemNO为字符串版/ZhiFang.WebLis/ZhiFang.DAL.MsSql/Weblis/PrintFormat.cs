using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;
namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类PrintFormat。
    /// </summary>
    public class PrintFormat : BaseDALLisDB, IDPrintFormat
    {
        public PrintFormat(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public PrintFormat()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "PrintFormat");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PrintFormat");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PrintFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.PrintFormatName != null)
            {
                strSql1.Append("PrintFormatName,");
                strSql2.Append("'" + model.PrintFormatName + "',");
            }
            if (model.PintFormatAddress != null)
            {
                strSql1.Append("PintFormatAddress,");
                strSql2.Append("'" + model.PintFormatAddress + "',");
            }
            if (model.PintFormatFileName != null)
            {
                strSql1.Append("PintFormatFileName,");
                strSql2.Append("'" + model.PintFormatFileName + "',");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql1.Append("ItemParaLineNum,");
                strSql2.Append("" + model.ItemParaLineNum + ",");
            }
            if (model.PaperSize != null)
            {
                strSql1.Append("PaperSize,");
                strSql2.Append("'" + model.PaperSize + "',");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql1.Append("PrintFormatDesc,");
                strSql2.Append("'" + model.PrintFormatDesc + "',");
            }
            if (model.BatchPrint != null)
            {
                strSql1.Append("BatchPrint,");
                strSql2.Append("" + model.BatchPrint + ",");
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
            strSql.Append("insert into PrintFormat(");
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
        public int Update(ZhiFang.Model.PrintFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PrintFormat set ");
            if (model.PrintFormatName != null)
            {
                strSql.Append("PrintFormatName='" + model.PrintFormatName + "',");
            }
            if (model.PintFormatAddress != null)
            {
                strSql.Append("PintFormatAddress='" + model.PintFormatAddress + "',");
            }
            if (model.PintFormatFileName != null)
            {
                strSql.Append("PintFormatFileName='" + model.PintFormatFileName + "',");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql.Append("ItemParaLineNum=" + model.ItemParaLineNum + ",");
            }
            if (model.PaperSize != null)
            {
                strSql.Append("PaperSize='" + model.PaperSize + "',");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql.Append("PrintFormatDesc='" + model.PrintFormatDesc + "',");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append("BatchPrint=" + model.BatchPrint + ",");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append("ImageFlag=" + model.ImageFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append("AntiFlag=" + model.AntiFlag + ",");
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
            strSql.Append("delete from PrintFormat ");
            strSql.Append(" where Id=" + Id + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.PrintFormat GetModel(string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(" Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" from PrintFormat ");
            strSql.Append(" where Id=" + Id + " ");
            Model.PrintFormat model = new Model.PrintFormat();
            Common.Log.Log.Info(strSql.ToString());
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                Common.Log.Log.Info("a1");
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PrintFormatName = ds.Tables[0].Rows[0]["PrintFormatName"].ToString();
                model.PintFormatAddress = ds.Tables[0].Rows[0]["PintFormatAddress"].ToString();
                model.PintFormatFileName = ds.Tables[0].Rows[0]["PintFormatFileName"].ToString();
                if (ds.Tables[0].Rows[0]["ItemParaLineNum"].ToString() != "")
                {
                    model.ItemParaLineNum = int.Parse(ds.Tables[0].Rows[0]["ItemParaLineNum"].ToString());
                }
                model.PaperSize = ds.Tables[0].Rows[0]["PaperSize"].ToString();
                model.PrintFormatDesc = ds.Tables[0].Rows[0]["PrintFormatDesc"].ToString();
                if (ds.Tables[0].Rows[0]["BatchPrint"].ToString() != "")
                {
                    model.BatchPrint = int.Parse(ds.Tables[0].Rows[0]["BatchPrint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImageFlag"].ToString() != "")
                {
                    model.ImageFlag = int.Parse(ds.Tables[0].Rows[0]["ImageFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AntiFlag"].ToString() != "")
                {
                    model.AntiFlag = int.Parse(ds.Tables[0].Rows[0]["AntiFlag"].ToString());
                }
                Common.Log.Log.Info("a2");
                return model;
            }
            else
            {
                Common.Log.Log.Info("a3");
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.PrintFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat where 1=1 ");
            if (model.PrintFormatName != null)
            {
                strSql.Append(" and PrintFormatName='" + model.PrintFormatName + "'");
            }
            if (model.PintFormatAddress != null)
            {
                strSql.Append(" and PintFormatAddress='" + model.PintFormatAddress + "'");
            }
            if (model.PintFormatFileName != null)
            {
                strSql.Append(" and PintFormatFileName='" + model.PintFormatFileName + "'");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql.Append(" and ItemParaLineNum=" + model.ItemParaLineNum + "");
            }
            if (model.PaperSize != null)
            {
                strSql.Append(" and PaperSize='" + model.PaperSize + "'");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql.Append(" and PrintFormatDesc='" + model.PrintFormatDesc + "'");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
            }
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
            strSql.Append(" Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat ");
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



        #region IDataBase<PrintFormat> 成员

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PrintFormat");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.PrintFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PrintFormat where 1=1 ");
            if (model.PrintFormatName != null)
            {
                strSql.Append(" and PrintFormatName='" + model.PrintFormatName + "'");
            }
            if (model.PintFormatAddress != null)
            {
                strSql.Append(" and PintFormatAddress='" + model.PintFormatAddress + "'");
            }
            if (model.PintFormatFileName != null)
            {
                strSql.Append(" and PintFormatFileName='" + model.PintFormatFileName + "'");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql.Append(" and ItemParaLineNum=" + model.ItemParaLineNum + "");
            }
            if (model.PaperSize != null)
            {
                strSql.Append(" and PaperSize='" + model.PaperSize + "'");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql.Append(" and PrintFormatDesc='" + model.PrintFormatDesc + "'");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        #endregion


        #region IDataBase<PrintFormat> 成员


        public DataSet GetList(int Top, Model.PrintFormat model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat where 1=1 ");
            if (model.PrintFormatName != null)
            {
                strSql.Append(" and PrintFormatName='" + model.PrintFormatName + "'");
            }
            if (model.PintFormatAddress != null)
            {
                strSql.Append(" and PintFormatAddress='" + model.PintFormatAddress + "'");
            }
            if (model.PintFormatFileName != null)
            {
                strSql.Append(" and PintFormatFileName='" + model.PintFormatFileName + "'");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql.Append(" and ItemParaLineNum=" + model.ItemParaLineNum + "");
            }
            if (model.PaperSize != null)
            {
                strSql.Append(" and PaperSize='" + model.PaperSize + "'");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql.Append(" and PrintFormatDesc='" + model.PrintFormatDesc + "'");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDPrintFormat 成员

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.PrintFormat model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from PrintFormat left join PrintFormatControl on PrintFormat.Id=PrintFormatControl.Id ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and PrintFormatControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where Id not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " Id from  PrintFormat left join PrintFormatControl on PrintFormat.Id=PrintFormatControl.Id ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and PrintFormatControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by PrintFormat.Id ) order by PrintFormat.Id desc");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from PrintFormat where Id not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " Id from PrintFormat order by Id desc)  ");

                if (model.PrintFormatName != null)
                {
                    strSql.Append(" and ( PrintFormatName like '%" + model.PrintFormatName + "%')");
                }
                strSql.Append("order by Id  desc");

               
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        #endregion

        #region IDataBase<PrintFormat> 成员
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
                strSql.Append("insert into PrintFormat (");
                strSql.Append("PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["PrintFormatName"].ToString().Trim() + "','" + dr["PintFormatAddress"].ToString().Trim() + "','" + dr["PintFormatFileName"].ToString().Trim() + "','" + dr["ItemParaLineNum"].ToString().Trim() + "','" + dr["PaperSize"].ToString().Trim() + "','" + dr["PrintFormatDesc"].ToString().Trim() + "','" + dr["BatchPrint"].ToString().Trim() + "','" + dr["ImageFlag"].ToString().Trim() + "','" + dr["AntiFlag"].ToString().Trim() + "'");
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
                strSql.Append("update PrintFormat set ");

                strSql.Append(" PrintFormatName = '" + dr["PrintFormatName"].ToString().Trim() + "' , ");
                strSql.Append(" PintFormatAddress = '" + dr["PintFormatAddress"].ToString().Trim() + "' , ");
                strSql.Append(" PintFormatFileName = '" + dr["PintFormatFileName"].ToString().Trim() + "' , ");
                strSql.Append(" ItemParaLineNum = '" + dr["ItemParaLineNum"].ToString().Trim() + "' , ");
                strSql.Append(" PaperSize = '" + dr["PaperSize"].ToString().Trim() + "' , ");
                strSql.Append(" PrintFormatDesc = '" + dr["PrintFormatDesc"].ToString().Trim() + "' , ");
                strSql.Append(" BatchPrint = '" + dr["BatchPrint"].ToString().Trim() + "' , ");
                strSql.Append(" ImageFlag = '" + dr["ImageFlag"].ToString().Trim() + "' , ");
                strSql.Append(" AntiFlag = '" + dr["AntiFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where Id='" + dr["Id"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}

