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
	/// 数据访问类CLIENTELE。
	/// </summary>
    public class CLIENTELE : BaseDALLisDB, IDCLIENTELE,IDBatchCopy
    {
        public CLIENTELE(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public CLIENTELE()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ClIENTNO", "CLIENTELE"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(long ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CLIENTELE");
			strSql.Append(" where ClIENTNO="+ClIENTNO+"  ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CLIENTELE where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
                if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ClIENTNO != null)
			{
				strSql1.Append("ClIENTNO,");
				strSql2.Append(""+model.ClIENTNO+",");
			}
            if (model.CNAME != null && model.CNAME != "")
			{
				strSql1.Append("CNAME,");
				strSql2.Append("'"+model.CNAME+"',");
			}
            if (model.ENAME != null && model.ENAME != "")
			{
				strSql1.Append("ENAME,");
				strSql2.Append("'"+model.ENAME+"',");
			}
            if (model.SHORTCODE != null && model.SHORTCODE != "")
			{
				strSql1.Append("SHORTCODE,");
				strSql2.Append("'"+model.SHORTCODE+"',");
			}
			if (model.ISUSE != null)
			{
				strSql1.Append("ISUSE,");
				strSql2.Append(""+model.ISUSE+",");
			}
            if (model.LINKMAN != null && model.LINKMAN != "")
			{
				strSql1.Append("LINKMAN,");
				strSql2.Append("'"+model.LINKMAN+"',");
			}
            if (model.ADDRESS != null && model.ADDRESS != "")
			{
				strSql1.Append("ADDRESS,");
				strSql2.Append("'"+model.ADDRESS+"',");
			}
            if (model.MAILNO != null && model.MAILNO != "")
			{
				strSql1.Append("MAILNO,");
				strSql2.Append("'"+model.MAILNO+"',");
			}
            if (model.EMAIL != null && model.EMAIL != "")
			{
				strSql1.Append("EMAIL,");
				strSql2.Append("'"+model.EMAIL+"',");
			}
            if (model.PRINCIPAL != null && model.PRINCIPAL != "")
			{
				strSql1.Append("PRINCIPAL,");
				strSql2.Append("'"+model.PRINCIPAL+"',");
			}
            if (model.CLIENTTYPE != null)
			{
				strSql1.Append("CLIENTTYPE,");
				strSql2.Append(""+model.CLIENTTYPE+",");
			}
			if (model.bmanno != null)
			{
				strSql1.Append("bmanno,");
				strSql2.Append(""+model.bmanno+",");
			}
            if (model.romark != null && model.romark != "")
			{
				strSql1.Append("romark,");
				strSql2.Append("'"+model.romark+"',");
			}
			if (model.titletype != null)
			{
				strSql1.Append("titletype,");
				strSql2.Append(""+model.titletype+",");
			}
            if (model.LinkManPosition != null && model.LinkManPosition != "")
            {
                strSql1.Append("LinkManPosition,");
                strSql2.Append("'" + model.LinkManPosition + "',");
            }
            if (model.PHONENUM1 != null && model.PHONENUM1 != "")
            {
                strSql1.Append("PHONENUM1,");
                strSql2.Append("'" + model.PHONENUM1 + "',");
            }
            if (model.PHONENUM2 != null && model.PHONENUM2 != "")
            {
                strSql1.Append("PHONENUM2,");
                strSql2.Append("'" + model.PHONENUM2 + "',");
            }

            if (model.GroupName != null && model.GroupName != "")
            {
                strSql1.Append("GroupName,");
                strSql2.Append("'" + model.GroupName + "',");
            }
            if (model.FaxNo != null && model.FaxNo != "")
            {
                strSql1.Append("FaxNo,");
                strSql2.Append("'" + model.FaxNo + "',");
            }
            if (model.AutoFax != null)
            {
                strSql1.Append("AutoFax,");
                strSql2.Append("'" + model.AutoFax + "',");
            }
            if (model.CZDY1 != null && model.CZDY1 != "")
            {
                strSql1.Append("CZDY1,");
                strSql2.Append("'" + model.CZDY1 + "',");
            }
            if (model.CZDY2 != null && model.CZDY2 != "")
            {
                strSql1.Append("CZDY2,");
                strSql2.Append("'" + model.CZDY2 + "',");
            }
            if (model.CZDY3 != null && model.CZDY3 != "")
            {
                strSql1.Append("CZDY3,");
                strSql2.Append("'" + model.CZDY3 + "',");
            }
            if (model.CZDY4 != null && model.CZDY4 != "")
            {
                strSql1.Append("CZDY4,");
                strSql2.Append("'" + model.CZDY4 + "',");
            }
            if (model.CZDY5 != null && model.CZDY5 != "")
            {
                strSql1.Append("CZDY5,");
                strSql2.Append("'" + model.CZDY5 + "',");
            }
            if (model.CZDY6 != null && model.CZDY6 != "")
            {
                strSql1.Append("CZDY6,");
                strSql2.Append("'" + model.CZDY6 + "',");
            }
            if (model.clientarea != null && model.clientarea != "")
            {
                strSql1.Append("clientarea,");
                strSql2.Append("'" + model.clientarea + "',");
            }
            if (model.AreaID != null)
            {
                strSql1.Append("AreaID,");
                strSql2.Append("'" + model.AreaID + "',");
            }
            if (model.RelationName != null && model.RelationName != "")
            {
                strSql1.Append("RelationName,");
                strSql2.Append("'" + model.RelationName + "',");
            }
            if (model.WebLisSourceOrgId != null && model.WebLisSourceOrgId != "")
            {
                strSql1.Append("WebLisSourceOrgId,");
                strSql2.Append("'" + model.WebLisSourceOrgId + "',");
            }
            strSql1.Append("tstamp,");
            strSql2.Append("sysdate+ '1.1234',");
			
			strSql.Append("insert into CLIENTELE(");
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
		public int Update(ZhiFang.Model.CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CLIENTELE set ");
            if (model.CNAME != null && model.CNAME != "")
			{
				strSql.Append("CNAME='"+model.CNAME+"',");
			}
            if (model.ENAME != null && model.ENAME != "")
			{
				strSql.Append("ENAME='"+model.ENAME+"',");
			}
            if (model.SHORTCODE != null && model.SHORTCODE != "")
			{
				strSql.Append("SHORTCODE='"+model.SHORTCODE+"',");
			}
            if (model.ISUSE != null)
			{
				strSql.Append("ISUSE="+model.ISUSE+",");
			}
            if (model.LINKMAN != null && model.LINKMAN != "")
			{
				strSql.Append("LINKMAN='"+model.LINKMAN+"',");
			}
            if (model.PHONENUM1 != null && model.PHONENUM1 != "")
			{
				strSql.Append("PHONENUM1='"+model.PHONENUM1+"',");
			}
            if (model.ADDRESS != null && model.ADDRESS != "")
			{
				strSql.Append("ADDRESS='"+model.ADDRESS+"',");
			}
            if (model.MAILNO != null && model.MAILNO != "")
			{
				strSql.Append("MAILNO='"+model.MAILNO+"',");
			}
            if (model.EMAIL != null && model.EMAIL != "")
			{
				strSql.Append("EMAIL='"+model.EMAIL+"',");
			}
            if (model.PRINCIPAL != null && model.PRINCIPAL != "")
			{
				strSql.Append("PRINCIPAL='"+model.PRINCIPAL+"',");
			}
            if (model.PHONENUM2 != null && model.PHONENUM2 != "")
			{
				strSql.Append("PHONENUM2='"+model.PHONENUM2+"',");
			}
			if (model.bmanno != null)
			{
				strSql.Append("bmanno="+model.bmanno+",");
			}
            if (model.romark != null && model.romark != "")
			{
				strSql.Append("romark='"+model.romark+"',");
			}
            if (model.titletype != null)
			{
				strSql.Append("titletype="+model.titletype+",");
			}

            if (model.LinkManPosition != null && model.LinkManPosition != "")
            {
                strSql.Append("LinkManPosition='" + model.LinkManPosition + "',");
            }
            if (model.GroupName != null && model.GroupName != "")
            {
                strSql.Append("GroupName='" + model.GroupName + "',");
            }
            if (model.FaxNo != null && model.FaxNo != "")
            {
                strSql.Append("FaxNo='" + model.FaxNo + "',");
            }
            if (model.AutoFax != null)
            {
                strSql.Append("AutoFax='" + model.AutoFax + "',");
            }
            if (model.CZDY1 != null && model.CZDY1 != "")
            {
                strSql.Append("CZDY1='" + model.CZDY1 + "',");
            }
            if (model.CZDY2 != null && model.CZDY2 != "")
            {
                strSql.Append("CZDY2='" + model.CZDY2 + "',");
            }
            if (model.CZDY3 != null && model.CZDY3 != "")
            {
                strSql.Append("CZDY3='" + model.CZDY3 + "',");
            }
            if (model.CZDY4 != null && model.CZDY4 != "")
            {
                strSql.Append("CZDY4='" + model.CZDY4 + "',");
            }
            if (model.CZDY5 != null && model.CZDY5 != "")
            {
                strSql.Append("CZDY5='" + model.CZDY5 + "',");
            }
            if (model.CZDY6 != null && model.CZDY6 != "")
            {
                strSql.Append("CZDY6='" + model.CZDY6 + "',");
            }
            if (model.clientarea != null && model.clientarea != "")
            {
                strSql.Append("clientarea='" + model.clientarea + "',");
            }
            if (model.AreaID != null)
            {
                strSql.Append("AreaID='" + model.AreaID + "',");
            }
            if (model.RelationName != null && model.RelationName != "")
            {
                strSql.Append("RelationName='" + model.RelationName + "',");
            }
            if (model.WebLisSourceOrgId != null && model.WebLisSourceOrgId != "")
            {
                strSql.Append("WebLisSourceOrgId='" + model.WebLisSourceOrgId + "',");
            }
            strSql.Append("tstamp= sysdate+ '1.1234',");

			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ClIENTNO="+ model.ClIENTNO+" ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(long ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CLIENTELE ");
			strSql.Append(" where ClIENTNO="+ClIENTNO+"  " );
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ZhiFang.Model.CLIENTELE GetModel(long ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
            strSql.Append(" ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype,AreaID ");
			strSql.Append(" from CLIENTELE ");
            strSql.Append(" where ROWNUM <='1' and ClIENTNO=" + ClIENTNO + " ");
			Model.CLIENTELE model=new Model.CLIENTELE();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ClIENTNO"].ToString()!="")
				{
                    model.ClIENTNO = ds.Tables[0].Rows[0]["ClIENTNO"].ToString();
				}
				model.CNAME=ds.Tables[0].Rows[0]["CNAME"].ToString();
				model.ENAME=ds.Tables[0].Rows[0]["ENAME"].ToString();
				model.SHORTCODE=ds.Tables[0].Rows[0]["SHORTCODE"].ToString();
				if(ds.Tables[0].Rows[0]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
				}
				model.LINKMAN=ds.Tables[0].Rows[0]["LINKMAN"].ToString();
				model.PHONENUM1=ds.Tables[0].Rows[0]["PHONENUM1"].ToString();
				model.ADDRESS=ds.Tables[0].Rows[0]["ADDRESS"].ToString();
				model.MAILNO=ds.Tables[0].Rows[0]["MAILNO"].ToString();
				model.EMAIL=ds.Tables[0].Rows[0]["EMAIL"].ToString();
				model.PRINCIPAL=ds.Tables[0].Rows[0]["PRINCIPAL"].ToString();
				model.PHONENUM2=ds.Tables[0].Rows[0]["PHONENUM2"].ToString();
				if(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString()!="")
				{
					model.CLIENTTYPE=int.Parse(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["bmanno"].ToString()!="")
				{
					model.bmanno=int.Parse(ds.Tables[0].Rows[0]["bmanno"].ToString());
				}
				model.romark=ds.Tables[0].Rows[0]["romark"].ToString();
				if(ds.Tables[0].Rows[0]["titletype"].ToString()!="")
				{
					model.titletype=int.Parse(ds.Tables[0].Rows[0]["titletype"].ToString());
				}
                try
                {
                    model.AreaID = int.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
                }
                catch
                {
                    model.AreaID = 0;
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM CLIENTELE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by ShortCode ");
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(ZhiFang.Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * ");
            strSql.Append(" FROM CLIENTELE where 1=1 ");
            if (model.ClIENTNO != ""&&model.ClIENTNO !=null)
            {
                strSql.Append(" and ClIENTNO='" + model.ClIENTNO + "'");
            }
            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME like '%" + model.CNAME + "%'");
            }
            if (model.ENAME != null)
            {
                strSql.Append(" and ENAME='" + model.ENAME + "'");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "'");
            }
            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE=" + model.ISUSE + "");
            }
            if (model.LINKMAN != null)
            {
                strSql.Append(" and LINKMAN='" + model.LINKMAN + "'");
            }
            if (model.PHONENUM1 != null)
            {
                strSql.Append(" and PHONENUM1='" + model.PHONENUM1 + "'");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "'");
            }
            if (model.MAILNO != null)
            {
                strSql.Append(" and MAILNO='" + model.MAILNO + "'");
            }
            if (model.EMAIL != null)
            {
                strSql.Append(" and EMAIL='" + model.EMAIL + "'");
            }
            if (model.PRINCIPAL != null)
            {
                strSql.Append(" and PRINCIPAL='" + model.PRINCIPAL + "'");
            }
            if (model.PHONENUM2 != null)
            {
                strSql.Append(" and PHONENUM2='" + model.PHONENUM2 + "'");
            }
            if (model.bmanno != null)
            {
                strSql.Append(" and bmanno=" + model.bmanno + "");
            }
            if (model.romark != null)
            {
                strSql.Append(" and romark='" + model.romark + "'");
            }
            if (model.titletype != null)
            {
                strSql.Append(" and titletype=" + model.titletype + "");
            }

            if (model.WebLisSourceOrgId != null)
            {
                strSql.Append(" and WebLisSourceOrgId =" + model.WebLisSourceOrgId + "");
            }
            strSql.Append(" order by ShortCode ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetListLike(Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CLIENTELE.*,ClIENTNO as LabNo,concat(concat(ClIENTNO,'_'),CNAME) as LabNo_Value,concat(concat(concat(CName,'('),ClIENTNO),')') as LabNoAndName_Text ");
            strSql.Append(" FROM CLIENTELE  where 1=1 ");

            if (model.OutSideClientNo != null)
            {
                strSql.Append(" and ClIENTNO <> " + model.OutSideClientNo + " ");
            }

            StringBuilder sbWhere = new StringBuilder();
            if (model.CNAME != null)
            {
                sbWhere.Append(" and ( CNAME like '%" + model.CNAME + "%' ");
            }
            if (model.ClIENTNO != "" && model.ClIENTNO != null)
            {
                if (sbWhere.Length > 0)
                    sbWhere.Append(" or ClIENTNO like '%" + model.ClIENTNO + "%' ");
                else
                    sbWhere.Append(" and ( ClIENTNO like '%" + model.ClIENTNO + "%' ");
            }
            if (model.SHORTCODE != null)
            {
                if (sbWhere.Length > 0)
                    sbWhere.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
                else
                    sbWhere.Append(" and ( SHORTCODE like '%" + model.SHORTCODE + "%' ");
            }
            if (sbWhere.Length > 0)
                sbWhere.Append(" ) ");

            sbWhere.Append(" order by areaid");
            strSql.Append(sbWhere.ToString());

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
            strSql.Append(" *  ");
			strSql.Append(" FROM CLIENTELE ");
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

		#endregion  成员方法        

        #region IDCLIENTELE 成员

        public int DeleteList(string CLIENTIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CLIENTELE ");
            strSql.Append(" where ID in (" + CLIENTIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        #endregion

        #region IDataPage<CLIENTELE> 成员
        /// <summary>
        /// 利用主键分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.CLIENTELE model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                string likesql = "";
				if (model.ClienteleLikeKey != null && model.ClienteleLikeKey.Trim() != "")
                {
                    likesql = " and  ( CLIENTELE.ClIENTNO like '%" + model.ClienteleLikeKey + "%' or CLIENTELE.CNAME like '%" + model.ClienteleLikeKey + "%'  or CLIENTELE.ENAME like '%" + model.ClienteleLikeKey + "%'  or CLIENTELE.SHORTCODE like '%" + model.ClienteleLikeKey + "%') ";
                }
                strSql.Append(" select   * from CLIENTELE left join CLIENTELEControl on CLIENTELE.ClIENTNO=CLIENTELEControl.ClIENTNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and CLIENTELEControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and ClIENTNO not in ( ");
                strSql.Append("select ClIENTNO from  CLIENTELE left join CLIENTELEControl on CLIENTELE.ClIENTNO=CLIENTELEControl.ClIENTNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and CLIENTELEControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + " order by CLIENTELE." + model.OrderField + " ) " + likesql + " order by CLIENTELE." + model.OrderField + " ");
                Common.Log.Log.Info("GetListByPage" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                string likesql="";
                if(model.ClienteleLikeKey!=null&&model.ClienteleLikeKey.Trim()!="")
                {
                    likesql = " and  ( ClIENTNO like '%" + model.ClienteleLikeKey + "%' or CNAME like '%" + model.ClienteleLikeKey + "%'  or ENAME like '%" + model.ClienteleLikeKey + "%'  or SHORTCODE like '%" + model.ClienteleLikeKey + "%') ";
                }
                if (model.AreaID != null && model.AreaID != 0)
                {
                    likesql = likesql + " and AreaID=" + model.AreaID + " ";
                }

                strSql.Append("select   * from CLIENTELE where  ROWNUM <= '" + nowPageSize + "' and ClIENTNO not in  ");
                strSql.Append("(select  ClIENTNO from CLIENTELE where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + ") ");
                strSql.Append(likesql);
                strSql.Append("  ");
                Common.Log.Log.Info("GetListByPage" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        #endregion

        #region IDataBase<CLIENTELE> 成员      

        public DataSet GetList(int Top, Model.CLIENTELE model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  *  ");
            strSql.Append(" FROM CLIENTELE where 1=1 ");
            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "'");
            }
            if (model.ENAME != null)
            {
                strSql.Append(" and ENAME='" + model.ENAME + "'");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "'");
            }
            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE=" + model.ISUSE + "");
            }
            if (model.LINKMAN != null)
            {
                strSql.Append(" and LINKMAN='" + model.LINKMAN + "'");
            }
            if (model.PHONENUM1 != null)
            {
                strSql.Append(" and PHONENUM1='" + model.PHONENUM1 + "'");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "'");
            }
            if (model.MAILNO != null)
            {
                strSql.Append(" and MAILNO='" + model.MAILNO + "'");
            }
            if (model.EMAIL != null)
            {
                strSql.Append(" and EMAIL='" + model.EMAIL + "'");
            }
            if (model.PRINCIPAL != null)
            {
                strSql.Append(" and PRINCIPAL='" + model.PRINCIPAL + "'");
            }
            if (model.PHONENUM2 != null)
            {
                strSql.Append(" and PHONENUM2='" + model.PHONENUM2 + "'");
            }
            if (model.bmanno != null)
            {
                strSql.Append(" and bmanno=" + model.bmanno + "");
            }
            if (model.romark != null)
            {
                strSql.Append(" and romark='" + model.romark + "'");
            }
            if (model.titletype != null)
            {
                strSql.Append(" and titletype=" + model.titletype + "");
            }
            strSql.Append(" and ROWNUM <= '" + Top + "'");
            if (filedOrder != string.Empty && filedOrder.Trim() != "")
            {
                strSql.Append(" order by  " + filedOrder);
            }
            else
            {
                strSql.Append(" order by  ClIENTNO ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from CLIENTELE");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from CLIENTELE where 1=1 ");
            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "'");
            }
            if (model.ENAME != null)
            {
                strSql.Append(" and ENAME='" + model.ENAME + "'");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "'");
            }
            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE=" + model.ISUSE + "");
            }
            if (model.LINKMAN != null)
            {
                strSql.Append(" and LINKMAN='" + model.LINKMAN + "'");
            }
            if (model.PHONENUM1 != null)
            {
                strSql.Append(" and PHONENUM1='" + model.PHONENUM1 + "'");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "'");
            }
            if (model.MAILNO != null)
            {
                strSql.Append(" and MAILNO='" + model.MAILNO + "'");
            }
            if (model.EMAIL != null)
            {
                strSql.Append(" and EMAIL='" + model.EMAIL + "'");
            }
            if (model.PRINCIPAL != null)
            {
                strSql.Append(" and PRINCIPAL='" + model.PRINCIPAL + "'");
            }
            if (model.PHONENUM2 != null)
            {
                strSql.Append(" and PHONENUM2='" + model.PHONENUM2 + "'");
            }
            if (model.bmanno != null)
            {
                strSql.Append(" and bmanno=" + model.bmanno + "");
            }
            if (model.romark != null)
            {
                strSql.Append(" and romark='" + model.romark + "'");
            }           
            if (model.clientarea != null)
            {
                strSql.Append(" and clientarea='" + model.clientarea + "'");
            }
            if (model.clientstyle != null)
            {
                strSql.Append(" and clientstyle='" + model.clientstyle + "'");
            }
            if (model.FaxNo != null)
            {
                strSql.Append(" and FaxNo='" + model.FaxNo + "'");
            }
            if (model.AutoFax != null)
            {
                strSql.Append(" and AutoFax=" + model.AutoFax + "");
            }
            if (model.ClientReportTitle != null)
            {
                strSql.Append(" and ClientReportTitle='" + model.ClientReportTitle + "'");
            }
            if (model.IsPrintItem != null)
            {
                strSql.Append(" and IsPrintItem=" + model.IsPrintItem + "");
            }
            if (model.CZDY1 != null)
            {
                strSql.Append(" and CZDY1='" + model.CZDY1 + "'");
            }
            if (model.CZDY2 != null)
            {
                strSql.Append(" and CZDY2='" + model.CZDY2 + "'");
            }
            if (model.CZDY3 != null)
            {
                strSql.Append(" and CZDY3='" + model.CZDY3 + "'");
            }
            if (model.CZDY4 != null)
            {
                strSql.Append(" and CZDY4='" + model.CZDY4 + "'");
            }
            if (model.CZDY5 != null)
            {
                strSql.Append(" and CZDY5='" + model.CZDY5 + "'");
            }
            if (model.CZDY6 != null)
            {
                strSql.Append(" and CZDY6='" + model.CZDY6 + "'");
            }

            string likesql = "";
            if (model.ClienteleLikeKey != null && model.ClienteleLikeKey.Trim() != "")
            {
                likesql = " and  ( ClIENTNO like '%" + model.ClienteleLikeKey + "%' or CNAME like '%" + model.ClienteleLikeKey + "%'  or ENAME like '%" + model.ClienteleLikeKey + "%'  or SHORTCODE like '%" + model.ClienteleLikeKey + "%') ";
            }
            if (model.AreaID != null && model.AreaID != 0)
            {
                likesql = likesql + " and AreaID=" + model.AreaID + " ";
            }
            strSql.Append(likesql);
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        #endregion

        #region IDBatchCopy 成员

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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["ClIENTNO"].ToString().Trim())))
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.AddByDataRow(dr);
                        }
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
                strSql.Append("insert into CLIENTELE (");
                strSql.Append("ClIENTNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype,uploadtype,printtype,InputDataType,CNAME,reportpagetype,clientarea,clientstyle,RelationName,WebLisSourceOrgID,GroupName,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO");
                strSql.Append(") values (");

                if (dr.Table.Columns["ClIENTNO"] != null && dr.Table.Columns["ClIENTNO"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClIENTNO"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["EMAIL"] != null && dr.Table.Columns["EMAIL"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["EMAIL"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PRINCIPAL"] != null && dr.Table.Columns["PRINCIPAL"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PRINCIPAL"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PHONENUM2"] != null && dr.Table.Columns["PHONENUM2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PHONENUM2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CLIENTTYPE"] != null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CLIENTTYPE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["bmanno"] != null && dr.Table.Columns["bmanno"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["bmanno"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["romark"] != null && dr.Table.Columns["romark"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["romark"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["titletype"] != null && dr.Table.Columns["titletype"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["titletype"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["uploadtype"] != null && dr.Table.Columns["uploadtype"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["uploadtype"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["printtype"] != null && dr.Table.Columns["printtype"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["printtype"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["InputDataType"] != null && dr.Table.Columns["InputDataType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["InputDataType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CNAME"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["reportpagetype"] != null && dr.Table.Columns["reportpagetype"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["reportpagetype"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["clientarea"] != null && dr.Table.Columns["clientarea"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["clientarea"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["clientstyle"] != null && dr.Table.Columns["clientstyle"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["clientstyle"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["RelationName"] != null && dr.Table.Columns["RelationName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["RelationName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["WebLisSourceOrgID"] != null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["WebLisSourceOrgID"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["GroupName"] != null && dr.Table.Columns["GroupName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["GroupName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ENAME"] != null && dr.Table.Columns["ENAME"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ENAME"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SHORTCODE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ISUSE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LINKMAN"] != null && dr.Table.Columns["LINKMAN"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LINKMAN"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PHONENUM1"] != null && dr.Table.Columns["PHONENUM1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PHONENUM1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ADDRESS"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["MAILNO"] != null && dr.Table.Columns["MAILNO"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["MAILNO"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.CLIENTELE.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CLIENTELE set ");


                if (dr.Table.Columns["EMAIL"] != null && dr.Table.Columns["EMAIL"].ToString().Trim() != "")
                {
                    strSql.Append(" EMAIL = '" + dr["EMAIL"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PRINCIPAL"] != null && dr.Table.Columns["PRINCIPAL"].ToString().Trim() != "")
                {
                    strSql.Append(" PRINCIPAL = '" + dr["PRINCIPAL"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PHONENUM2"] != null && dr.Table.Columns["PHONENUM2"].ToString().Trim() != "")
                {
                    strSql.Append(" PHONENUM2 = '" + dr["PHONENUM2"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CLIENTTYPE"] != null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim() != "")
                {
                    strSql.Append(" CLIENTTYPE = '" + dr["CLIENTTYPE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["bmanno"] != null && dr.Table.Columns["bmanno"].ToString().Trim() != "")
                {
                    strSql.Append(" bmanno = '" + dr["bmanno"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["romark"] != null && dr.Table.Columns["romark"].ToString().Trim() != "")
                {
                    strSql.Append(" romark = '" + dr["romark"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["titletype"] != null && dr.Table.Columns["titletype"].ToString().Trim() != "")
                {
                    strSql.Append(" titletype = '" + dr["titletype"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["uploadtype"] != null && dr.Table.Columns["uploadtype"].ToString().Trim() != "")
                {
                    strSql.Append(" uploadtype = '" + dr["uploadtype"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["printtype"] != null && dr.Table.Columns["printtype"].ToString().Trim() != "")
                {
                    strSql.Append(" printtype = '" + dr["printtype"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["InputDataType"] != null && dr.Table.Columns["InputDataType"].ToString().Trim() != "")
                {
                    strSql.Append(" InputDataType = '" + dr["InputDataType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" CNAME = '" + dr["CNAME"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["reportpagetype"] != null && dr.Table.Columns["reportpagetype"].ToString().Trim() != "")
                {
                    strSql.Append(" reportpagetype = '" + dr["reportpagetype"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["clientarea"] != null && dr.Table.Columns["clientarea"].ToString().Trim() != "")
                {
                    strSql.Append(" clientarea = '" + dr["clientarea"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["clientstyle"] != null && dr.Table.Columns["clientstyle"].ToString().Trim() != "")
                {
                    strSql.Append(" clientstyle = '" + dr["clientstyle"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["RelationName"] != null && dr.Table.Columns["RelationName"].ToString().Trim() != "")
                {
                    strSql.Append(" RelationName = '" + dr["RelationName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["WebLisSourceOrgID"] != null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim() != "")
                {
                    strSql.Append(" WebLisSourceOrgID = '" + dr["WebLisSourceOrgID"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["GroupName"] != null && dr.Table.Columns["GroupName"].ToString().Trim() != "")
                {
                    strSql.Append(" GroupName = '" + dr["GroupName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ENAME"] != null && dr.Table.Columns["ENAME"].ToString().Trim() != "")
                {
                    strSql.Append(" ENAME = '" + dr["ENAME"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" SHORTCODE = '" + dr["SHORTCODE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" ISUSE = '" + dr["ISUSE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["LINKMAN"] != null && dr.Table.Columns["LINKMAN"].ToString().Trim() != "")
                {
                    strSql.Append(" LINKMAN = '" + dr["LINKMAN"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PHONENUM1"] != null && dr.Table.Columns["PHONENUM1"].ToString().Trim() != "")
                {
                    strSql.Append(" PHONENUM1 = '" + dr["PHONENUM1"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" ADDRESS = '" + dr["ADDRESS"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["MAILNO"] != null && dr.Table.Columns["MAILNO"].ToString().Trim() != "")
                {
                    strSql.Append(" MAILNO = '" + dr["MAILNO"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where ClIENTNO='" + dr["ClIENTNO"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.CLIENTELE .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["ClIENTNO"] != null && dr.Table.Columns["ClIENTNO"].ToString().Trim() != "")
                {
                    strSql.Append("delete from CLIENTELE ");
                    strSql.Append(" where ClIENTNO='" + dr["ClIENTNO"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.CLIENTELE.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public bool CopyToLab(List<string> lst)
        {
            return false;
        }

        #endregion


        public DataSet GetClientNo(string CLIENTIDlist,string CName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM CLIENTELE  where 1=1 ");
            strSql.Append(" and ClIENTNO not in (" + CLIENTIDlist + ")");
            if (CName != "")
            {
                strSql.Append(" and CName like '%" + CName + "%'");
            }

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }


        public int Add(List<Model.CLIENTELE> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.CLIENTELE> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

