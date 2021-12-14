using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// ���ݷ�����SickType��
	/// </summary>
	public class SickType:IDSickType
	{
        public SickType()
        { }
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SickTypeNo", "SickType");
        }


        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int SickTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SickType");
            strSql.Append(" where SickTypeNo=" + SickTypeNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.SickType model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SickTypeNo != null)
            {
                strSql1.Append("SickTypeNo,");
                strSql2.Append("" + model.SickTypeNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            strSql.Append("insert into SickType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Model.SickType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SickType set ");
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where SickTypeNo=" + model.SickTypeNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public int Delete(int SickTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SickType ");
            strSql.Append(" where SickTypeNo=" + SickTypeNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.SickType GetModel(int SickTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode ");
            strSql.Append(" from SickType ");
            strSql.Append(" where SickTypeNo=" + SickTypeNo + " ");
            Model.SickType model = new Model.SickType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SickTypeNo"].ToString() != "")
                {
                    model.SickTypeNo = int.Parse(ds.Tables[0].Rows[0]["SickTypeNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode ");
            strSql.Append(" FROM SickType ");
            if (!"".Equals(strWhere) && null != strWhere)
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug("GetSickType:"+strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode ");
            strSql.Append(" FROM SickType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */

        public DataSet GetList(Model.SickType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SickTypeNo,CName,ShortCode,DispOrder,HisOrderCode ");
            strSql.Append(" FROM SickType where 1=1 ");
            if (model.SickTypeNo != null)
            {
                strSql.Append(" and SickTypeNo=" + model.SickTypeNo + ",");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "',");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "',");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

