using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
    /// <summary>
    /// 数据访问类PGroup。
    /// </summary>
    public class PGroup : BaseDALLisDB, IDPGroup, IDBatchCopy
    {
        public PGroup(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public PGroup()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SectionNo", "PGroup");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SectionNo, int Visible)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PGroup");
            strSql.Append(" where SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SectionNo != null)
            {
                strSql1.Append("SectionNo,");
                strSql2.Append(" " + model.SectionNo + ",");
            }
            //if (model.SuperGroupNo != null)
            //{
            //    strSql1.Append("SuperGroupNo,");
            //    strSql2.Append(""+model.SuperGroupNo+",");
            //}
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortName != null)
            {
                strSql1.Append("ShortName,");
                strSql2.Append("'" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.SectionDesc != null)
            {
                strSql1.Append("SectionDesc,");
                strSql2.Append("'" + model.SectionDesc + "',");
            }
            if (model.SectionType != null)
            {
                strSql1.Append("SectionType,");
                strSql2.Append("" + model.SectionType + ",");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.SuperGroupNo != null)
            {
                strSql1.Append("SuperGroupNo,");
                strSql2.Append("" + model.SuperGroupNo + ",");
            }
            strSql1.Append("tstamp,");
            strSql2.Append("Systimestamp,");

            #region

            //if (model.OnlineTime != null)
            //{
            //    strSql1.Append("onlinetime,");
            //    strSql2.Append("" + model.OnlineTime + ",");
            //}
            //if (model.KeyDispOrder != null)
            //{
            //    strSql1.Append("KeyDispOrder,");
            //    strSql2.Append(""+model.KeyDispOrder+",");
            //}
            //if (model.DummyGroup != null)
            //{
            //    strSql1.Append("dummygroup,");
            //    strSql2.Append("" + model.DummyGroup + ",");
            //}
            //if (model.UnionType != null)
            //{
            //    strSql1.Append("uniontype,");
            //    strSql2.Append("" + model.UnionType + ",");
            //}
            //if (model.SectorTypeNo != null)
            //{
            //    strSql1.Append("SectorTypeNo,");
            //    strSql2.Append(""+model.SectorTypeNo+",");
            //}
            //if (model.SampleRule != null)
            //{
            //    strSql1.Append("SampleRule,");
            //    strSql2.Append(""+model.SampleRule+",");
            //}
            #endregion
            strSql.Append("insert into PGroup(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PGroup set ");
            if (model.SuperGroupNo != null)
            {
                strSql.Append("SuperGroupNo=" + model.SuperGroupNo + ",");
            }
            else
            {
                strSql.Append("SuperGroupNo=null,");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.ShortName != null)
            {
                strSql.Append("ShortName='" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.SectionDesc != null)
            {
                strSql.Append("SectionDesc='" + model.SectionDesc + "',");
            }
            if (model.SectionType != null)
            {
                strSql.Append("SectionType=" + model.SectionType + ",");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }

            strSql.Append("tstamp= Systimestamp,");

            //if (model.Visible != null)
            //{
            //    strSql.Append("Visible=" + model.Visible + ",");
            //}
            //if (model.OnlineTime != null)
            //{
            //    strSql.Append("onlinetime="+model.OnlineTime+",");
            //}
            //if (model.KeyDispOrder != null)
            //{
            //    strSql.Append("KeyDispOrder="+model.KeyDispOrder+",");
            //}
            //if (model.DummyGroup != null)
            //{
            //    strSql.Append("dummygroup="+model.DummyGroup+",");
            //}
            //if (model.UnionType != null)
            //{
            //    strSql.Append("uniontype="+model.UnionType+",");
            //}
            //if (model.SectorTypeNo != null)
            //{
            //    strSql.Append("SectorTypeNo="+model.SectorTypeNo+",");
            //}
            //if (model.SampleRule != null)
            //{
            //    strSql.Append("SampleRule="+model.SampleRule+",");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where SectionNo=" + model.SectionNo);
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int SectionNo, int Visible)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PGroup ");
            strSql.Append(" where SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.PGroup GetModel(int SectionNo, int Visible)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" from PGroup ");
            strSql.Append(" where ROWNUM <='1' and SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
            Model.PGroup model = new Model.PGroup();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.SectionDesc = ds.Tables[0].Rows[0]["SectionDesc"].ToString();
                if (ds.Tables[0].Rows[0]["SectionType"].ToString() != "")
                {
                    model.SectionType = int.Parse(ds.Tables[0].Rows[0]["SectionType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["onlinetime"].ToString() != "")
                {
                    model.OnlineTime = int.Parse(ds.Tables[0].Rows[0]["onlinetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KeyDispOrder"].ToString() != "")
                {
                    model.KeyDispOrder = int.Parse(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["dummygroup"].ToString() != "")
                {
                    model.DummyGroup = int.Parse(ds.Tables[0].Rows[0]["dummygroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["uniontype"].ToString() != "")
                {
                    model.UnionType = int.Parse(ds.Tables[0].Rows[0]["uniontype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectorTypeNo"].ToString() != "")
                {
                    model.SectorTypeNo = int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleRule"].ToString() != "")
                {
                    model.SampleRule = int.Parse(ds.Tables[0].Rows[0]["SampleRule"].ToString());
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
            strSql.Append("select SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" FROM PGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" FROM PGroup where 1=1");
            if (model != null)
            {
                if (model.SectionNo != null)
                {
                    strSql.Append(" and SectionNo=" + model.SectionNo + "");
                }
                if (model.SuperGroupNo != null)
                {
                    strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + "");
                }
                if (model.CName != null)
                {
                    strSql.Append(" and CName='" + model.CName + "'");
                }
                if (model.ShortName != null)
                {
                    strSql.Append(" and ShortName='" + model.ShortName + "'");
                }
                if (model.ShortCode != null)
                {
                    strSql.Append(" and ShortCode='" + model.ShortCode + "'");
                }
                if (model.SectionDesc != null)
                {
                    strSql.Append(" and SectionDesc='" + model.SectionDesc + "'");
                }
                if (model.SectionType != null)
                {
                    strSql.Append(" and SectionType=" + model.SectionType + "");
                }
                if (model.DispOrder != null)
                {
                    strSql.Append(" and DispOrder=" + model.DispOrder + "");
                }
                if (model.OnlineTime != null)
                {
                    strSql.Append(" and onlinetime=" + model.OnlineTime + "");
                }
                if (model.KeyDispOrder != null)
                {
                    strSql.Append(" and KeyDispOrder=" + model.KeyDispOrder + "");
                }
                if (model.DummyGroup != null)
                {
                    strSql.Append(" and dummygroup=" + model.DummyGroup + "");
                }
                if (model.UnionType != null)
                {
                    strSql.Append(" and uniontype=" + model.UnionType + "");
                }
                if (model.SectorTypeNo != null)
                {
                    strSql.Append(" and SectorTypeNo=" + model.SectorTypeNo + "");
                }
                if (model.SampleRule != null)
                {
                    strSql.Append(" and SampleRule=" + model.SampleRule + "");
                }
            }
            Common.Log.Log.Info("GetList:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" FROM PGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /*
        */

        #endregion  成员方法

        #region IDPGroup 成员
        public bool Exists(int SectionNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PGroup");
            strSql.Append(" where SectionNo=" + SectionNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        public int Delete(int SectionNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PGroup ");
            strSql.Append(" where SectionNo=" + SectionNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public int DeleteList(string SectionIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PGroup ");
            strSql.Append(" where ID in (" + SectionIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public Model.PGroup GetModel(int SectionNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" from PGroup ");
            strSql.Append(" where ROWNUM <='1' and SectionNo=" + SectionNo + " ");
            Model.PGroup model = new Model.PGroup();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.SectionDesc = ds.Tables[0].Rows[0]["SectionDesc"].ToString();
                if (ds.Tables[0].Rows[0]["SectionType"].ToString() != "")
                {
                    model.SectionType = int.Parse(ds.Tables[0].Rows[0]["SectionType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["onlinetime"].ToString() != "")
                {
                    model.OnlineTime = int.Parse(ds.Tables[0].Rows[0]["onlinetime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["KeyDispOrder"].ToString() != "")
                {
                    model.KeyDispOrder = int.Parse(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["dummygroup"].ToString() != "")
                {
                    model.DummyGroup = int.Parse(ds.Tables[0].Rows[0]["dummygroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["uniontype"].ToString() != "")
                {
                    model.UnionType = int.Parse(ds.Tables[0].Rows[0]["uniontype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectorTypeNo"].ToString() != "")
                {
                    model.SectorTypeNo = int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleRule"].ToString() != "")
                {
                    model.SampleRule = int.Parse(ds.Tables[0].Rows[0]["SampleRule"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region IDataBase<PGroup> 成员


        public DataSet GetList(int Top, Model.PGroup model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" FROM PGroup where 1=1");
            if (model != null)
            {
                if (model.SuperGroupNo != null)
                {
                    strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + "");
                }
                if (model.CName != null)
                {
                    strSql.Append(" and CName='" + model.CName + "'");
                }
                if (model.ShortName != null)
                {
                    strSql.Append(" and ShortName='" + model.ShortName + "'");
                }
                if (model.ShortCode != null)
                {
                    strSql.Append(" and ShortCode='" + model.ShortCode + "'");
                }
                if (model.SectionDesc != null)
                {
                    strSql.Append(" and SectionDesc='" + model.SectionDesc + "'");
                }
                if (model.SectionType != null)
                {
                    strSql.Append(" and SectionType=" + model.SectionType + "");
                }
                if (model.DispOrder != null)
                {
                    strSql.Append(" and DispOrder=" + model.DispOrder + "");
                }
                if (model.OnlineTime != null)
                {
                    strSql.Append(" and onlinetime=" + model.OnlineTime + "");
                }
                if (model.KeyDispOrder != null)
                {
                    strSql.Append(" and KeyDispOrder=" + model.KeyDispOrder + "");
                }
                if (model.DummyGroup != null)
                {
                    strSql.Append(" and dummygroup=" + model.DummyGroup + "");
                }
                if (model.UnionType != null)
                {
                    strSql.Append(" and uniontype=" + model.UnionType + "");
                }
                if (model.SectorTypeNo != null)
                {
                    strSql.Append(" and SectorTypeNo=" + model.SectorTypeNo + "");
                }
                if (model.SampleRule != null)
                {
                    strSql.Append(" and SampleRule=" + model.SampleRule + "");
                }
            }

            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleRule ");
            strSql.Append(" FROM PGroup ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PGroup");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PGroup where 1=1  ");
            if (model.SearchLikeKey != null && model.SearchLikeKey.Trim() != "")
            {
                strSql.Append("  and  (SectionNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%'  or ShortName like '%" + model.SearchLikeKey + "%'  or ShortCode like '%" + model.SearchLikeKey + "%') ");
            }
            //if (model != null)
            //{
            //    if (model.SuperGroupNo != null)
            //    {
            //        strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + "");
            //    }
            //    if (model.CName != null)
            //    {
            //        strSql.Append(" and CName='" + model.CName + "'");
            //    }
            //    if (model.ShortName != null)
            //    {
            //        strSql.Append(" and ShortName='" + model.ShortName + "'");
            //    }
            //    if (model.ShortCode != null)
            //    {
            //        strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            //    }
            //    if (model.SectionDesc != null)
            //    {
            //        strSql.Append(" and SectionDesc='" + model.SectionDesc + "'");
            //    }
            //    if (model.SectionType != null)
            //    {
            //        strSql.Append(" and SectionType=" + model.SectionType + "");
            //    }
            //    if (model.DispOrder != null)
            //    {
            //        strSql.Append(" and DispOrder=" + model.DispOrder + "");
            //    }
            //    if (model.OnlineTime != null)
            //    {
            //        strSql.Append(" and onlinetime=" + model.OnlineTime + "");
            //    }
            //    if (model.KeyDispOrder != null)
            //    {
            //        strSql.Append(" and KeyDispOrder=" + model.KeyDispOrder + "");
            //    }
            //    if (model.DummyGroup != null)
            //    {
            //        strSql.Append(" and dummygroup=" + model.DummyGroup + "");
            //    }
            //    if (model.UnionType != null)
            //    {
            //        strSql.Append(" and uniontype=" + model.UnionType + "");
            //    }
            //    if (model.SectorTypeNo != null)
            //    {
            //        strSql.Append(" and SectorTypeNo=" + model.SectorTypeNo + "");
            //    }
            //    if (model.SampleRule != null)
            //    {
            //        strSql.Append(" and SampleRule=" + model.SampleRule + "");
            //    }
            //}
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SectionNo"].ToString().Trim())))
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
                strSql.Append("insert into PGroup (");
                strSql.Append("SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,SampleRule");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SectionNo"].ToString().Trim() + "','" + dr["SuperGroupNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["SectionDesc"].ToString().Trim() + "','" + dr["SectionType"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["onlinetime"].ToString().Trim() + "','" + dr["KeyDispOrder"].ToString().Trim() + "','" + dr["dummygroup"].ToString().Trim() + "','" + dr["uniontype"].ToString().Trim() + "','" + dr["SectorTypeNo"].ToString().Trim() + "','" + dr["SampleNoType"].ToString().Trim() + "','" + dr["SampleRule"].ToString().Trim() + "'");
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
                strSql.Append("update PGroup set ");

                strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" SectionDesc = '" + dr["SectionDesc"].ToString().Trim() + "' , ");
                strSql.Append(" SectionType = '" + dr["SectionType"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" onlinetime = '" + dr["onlinetime"].ToString().Trim() + "' , ");
                strSql.Append(" KeyDispOrder = '" + dr["KeyDispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" dummygroup = '" + dr["dummygroup"].ToString().Trim() + "' , ");
                strSql.Append(" uniontype = '" + dr["uniontype"].ToString().Trim() + "' , ");
                strSql.Append(" SectorTypeNo = '" + dr["SectorTypeNo"].ToString().Trim() + "' , ");
                strSql.Append(" SampleNoType = '" + dr["SampleNoType"].ToString().Trim() + "' , ");
                strSql.Append(" SampleRule = '" + dr["SampleRule"].ToString().Trim() + "'  ");
                strSql.Append(" where SectionNo='" + dr["SectionNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region IDataPage<PGroup> 成员
        /// <summary>
        /// 利用主键分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.PGroup model, int nowPageNum, int nowPageSize)
        {
            string likesql = "";
            StringBuilder strSql = new StringBuilder();
            #region
            //#region
            //if (model != null && model.LabCode != null)
            //{
            //    strSql.Append(" select top " + nowPageSize + "  * from PGroup left join B_PGroupControl on PGroup.SectionNo=B_PGroupControl.SectionControlNo ");
            //    if (model.LabCode != null)
            //    {
            //        strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
            //    }
            //    strSql.Append("where PGroup.SectionNo not in ( ");
            //    strSql.Append("select top " + (nowPageSize * nowPageNum) + " PGroup.SectionNo from  PGroup left join B_PGroupControl on PGroup.SectionNo=B_PGroupControl.SectionControlNo ");
            //    if (model.LabCode != null)
            //    {
            //        strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
            //    }
            //    strSql.Append("order by PGroup.SectionNo ) order by PGroup.SectionNo ");
            //    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //}
            //else
            //{
            //    string strLike = "";
            //    if (model.SearchLikeKey != null)
            //    {

            //        //likesql = " and  ( SectionNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%'  or ShortName like '%" + model.SearchLikeKey + "%'  or ShortCode like '%" + model.SearchLikeKey + "%') ";



            //        strLike = " and (SectionNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%' or ShortName like '%" + model.SearchLikeKey + "%') ";
            //    }
            //    strSql.Append("select top " + nowPageSize + "  * from PGroup where SectionNo not in  ");
            //    strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SectionNo from PGroup where 1=1  " + strLike + " order by " + model.OrderField + ") " + strLike + " order by " + model.OrderField + "  ");

            //    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //}
            //#endregion
            #endregion
            if (model != null && model.LabCode != null)
            {
                if (model.SearchLikeKey != null)
                {
                    likesql = " and (PGroup.SectionNo like '%" + model.SearchLikeKey + "%' or PGroup.CName like '%" + model.SearchLikeKey + "%' or PGroup.ShortCode like '%" + model.SearchLikeKey + "%') ";
                }

                string strOrderBy = "";
                if (model.OrderField == "SectionNo")
                {
                    strOrderBy = "PGroup.SectionNo";
                }
                else if (model.OrderField.ToLower().IndexOf("control") >= 0)
                {
                    strOrderBy = "B_PGroupControl." + model.OrderField;
                }
                else
                {
                    strOrderBy = "PGroup." + model.OrderField;
                }

                strSql.Append(" select  * from PGroup left join B_PGroupControl on PGroup.SectionNo=B_PGroupControl.SectionNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and PGroup.SectionNo not in ( ");
                strSql.Append("select PGroup.SectionNo from  PGroup left join B_PGroupControl on PGroup.SectionNo=B_PGroupControl.SectionNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + " ) " + likesql + " order by " + strOrderBy + " ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {

                if (model.SearchLikeKey != null)
                {
                    likesql = " and  ( SectionNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%'  or ShortName like '%" + model.SearchLikeKey + "%'  or ShortCode like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select * from PGroup where  ROWNUM <= '" + nowPageSize + "' and SectionNo not in  ");
                // strSql.Append("(select top " + (nowPageSize * nowPageNum ) + " DeptNo from Department  where 1=1 " + likesql + "  order by DeptNo)");
                strSql.Append("(select SectionNo from PGroup where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + " ) " + likesql + " order by " + model.OrderField + "  ");
                //strSql.Append(likesql);
                //strSql.Append(" order by DeptNo    ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }

        }

        #endregion


        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    string str = GetControlItems(lst[i].Trim());

                    strSql.Append("insert into B_Lab_PGroup ( LabCode,");
                    strSql.Append(" LabSectionNo , onlinetime , KeyDispOrder , dummygroup , uniontype , SectorTypeNo , SampleNoType , SampleRule ,  UpLoadTimeL , LabSuperGroupNo , CName , ShortName , ShortCode , SectionDesc , SectionType , Visible , DispOrder ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("SectionNo,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,SampleRule,UpLoadTimeL,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder");
                    strSql.Append(" from PGroup ");
                    if (str.Trim() != "")
                        strSql.Append(" where SectionNo not in (" + str + ")");

                    strSqlControl.Append("insert into B_PGroupControl ( ");
                    strSqlControl.Append(" SectionControlNo,SectionNo,ControlLabNo,ControlSectionNo,UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  concat(concat(concat(concat('" + lst[i].Trim() + "','_'),SectionNo),'_'),SectionNo) as SectionControlNo,SectionNo,'" + lst[i].Trim() + "' as ControlLabNo,SectionNo,1 as UseFlag ");
                    strSqlControl.Append(" from PGroup ");
                    if (str.Trim() != "")
                        strSqlControl.Append(" where SectionNo not in (" + str + ")");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("PGroup", lst[i].Trim(), "", "", DateTime.Now, 1);
                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetControlItems(string strLabCode)
        {
            string str = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SectionNo from B_PGroupControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["SectionNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["SectionNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }


        public bool IsExist(string labCodeNo)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT(1) from B_Lab_PGroup ");
            strSql.Append(" where LabCode='" + labCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" select COUNT(1) from B_PGroupControl ");
            strSql2.Append(" where ControlLabNo='" + labCodeNo + "' ");

            if (DbHelperSQL.Exists(strSql.ToString()))
            {
                result = true;
            }
            return result;
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            bool result = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from B_Lab_PGroup ");
            strSql.Append(" where LabCode='" + LabCodeNo + "' ");

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" delete from B_PGroupControl ");
            strSql2.Append(" where ControlLabNo='" + LabCodeNo + "' ");

            int i = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            int j = DbHelperSQL.ExecuteNonQuery(strSql2.ToString());
            if (i > 0 || j > 0)
                result = true;
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0 || DbHelperSQL.ExecuteNonQuery(strSql2.ToString()) > 0)
            //{
            //    result = true;
            //}
            return result;
        }


        public int Add(List<Model.PGroup> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.PGroup> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

