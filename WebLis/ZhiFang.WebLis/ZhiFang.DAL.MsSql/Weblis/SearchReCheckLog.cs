using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public class SearchReCheckLog : BaseDALLisDB, IDSearchReCheckLog
    {
		public SearchReCheckLog()
		{
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
		#region  Method


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SearchReCheckLog");
			strSql.Append(" where Id="+Id+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZhiFang.Model.SearchReCheckLog model)
		{
            string flag = DbHelperSQL.ExecuteScalar("select count(*) from SearchReCheckLog");
            if (flag != null && flag.Trim() != "")
            {
                if (Convert.ToInt32(flag) > 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder strSql1 = new StringBuilder();
                    StringBuilder strSql2 = new StringBuilder();
                    if (model.Id != null)
                    {
                        strSql1.Append("Id,");
                        strSql2.Append("" + model.Id + ",");
                    }

                    if (model.BATCH_NUM != null)
                    {
                        strSql1.Append("BATCH_NUM,");
                        strSql2.Append("'" + model.BATCH_NUM + "',");
                    }
                    if (model.UPLOAD_TIME != null)
                    {
                        strSql1.Append("UPLOAD_TIME,");
                        strSql2.Append("'" + model.UPLOAD_TIME + "',");
                    }
                    if (model.LOCAL_ID != null)
                    {
                        strSql1.Append("LOCAL_ID,");
                        strSql2.Append("'" + model.LOCAL_ID + "',");
                    }
                    if (model.RESULTDESC != null)
                    {
                        strSql1.Append("RESULTDESC,");
                        strSql2.Append("'" + model.RESULTDESC + "',");
                    }
                    if (model.RESULTCODE != null)
                    {
                        strSql1.Append("RESULTCODE,");
                        strSql2.Append("'" + model.RESULTCODE + "',");
                    }
                    if (model.SERIALNUM_ID != null)
                    {
                        strSql1.Append("SERIALNUM_ID,");
                        strSql2.Append("'" + model.SERIALNUM_ID + "',");
                    }
                    if (model.UNIQUEID != null)
                    {
                        strSql1.Append("UNIQUEID,");
                        strSql2.Append("(select '" + model.UNIQUEID + "'+dbo.GetSortNo(Convert(nvarchar(10),max(Sortid)+1)) from SearchReCheckLog),");
                    }
                    if (model.PID != null)
                    {
                        strSql1.Append("PID,");
                        strSql2.Append("'" + model.PID + "',");
                    }
                    if (model.BUSINESS_RELATION_ID != null)
                    {
                        strSql1.Append("BUSINESS_RELATION_ID,");
                        strSql2.Append("'" + model.BUSINESS_RELATION_ID + "',");
                    }
                    if (model.BUSINESS_ACTIVE_TYPE != null)
                    {
                        strSql1.Append("BUSINESS_ACTIVE_TYPE,");
                        strSql2.Append("'" + model.BUSINESS_ACTIVE_TYPE + "',");
                    }
                    if (model.BUSINESS_ACTIVE_DES != null)
                    {
                        strSql1.Append("BUSINESS_ACTIVE_DES,");
                        strSql2.Append("'" + model.BUSINESS_ACTIVE_DES + "',");
                    }
                    if (model.BUSINESS_ID != null)
                    {
                        strSql1.Append("BUSINESS_ID,");
                        strSql2.Append("'" + model.BUSINESS_ID + "',");
                    }
                    if (model.BASIC_ACTIVE_TYPE != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_TYPE,");
                        strSql2.Append("'" + model.BASIC_ACTIVE_TYPE + "',");
                    }
                    if (model.BASIC_ACTIVE_DES != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_DES,");
                        strSql2.Append("'" + model.BASIC_ACTIVE_DES + "',");
                    }
                    if (model.BASIC_ACTIVE_ID != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_ID,");
                        strSql2.Append("(select '" + model.BASIC_ACTIVE_ID + "'+dbo.GetSortNo(Convert(nvarchar(10),max(Sortid)+1)) from SearchReCheckLog),");
                    }
                    if (model.ORGANIZATION_CODE != null)
                    {
                        strSql1.Append("ORGANIZATION_CODE,");
                        strSql2.Append("'" + model.ORGANIZATION_CODE + "',");
                    }
                    if (model.ORGANIZATION_NAME != null)
                    {
                        strSql1.Append("ORGANIZATION_NAME,");
                        strSql2.Append("'" + model.ORGANIZATION_NAME + "',");
                    }
                    if (model.DOMAIN_CODE != null)
                    {
                        strSql1.Append("DOMAIN_CODE,");
                        strSql2.Append("'" + model.DOMAIN_CODE + "',");
                    }
                    if (model.DOMAIN_NAME != null)
                    {
                        strSql1.Append("DOMAIN_NAME,");
                        strSql2.Append("'" + model.DOMAIN_NAME + "',");
                    }
                    if (model.VER != null)
                    {
                        strSql1.Append("VER,");
                        strSql2.Append("'" + model.VER + "',");
                    }
                    if (model.VERDES != null)
                    {
                        strSql1.Append("VERDES,");
                        strSql2.Append("'" + model.VERDES + "',");
                    }
                    if (model.REGION_IDEN != null)
                    {
                        strSql1.Append("REGION_IDEN,");
                        strSql2.Append("'" + model.REGION_IDEN + "',");
                    }
                    if (model.DATA_SECURITY != null)
                    {
                        strSql1.Append("DATA_SECURITY,");
                        strSql2.Append("'" + model.DATA_SECURITY + "',");
                    }
                    if (model.RECORD_IDEN != null)
                    {
                        strSql1.Append("RECORD_IDEN,");
                        strSql2.Append("'" + model.RECORD_IDEN + "',");
                    }
                    if (model.CREATE_DATE != null)
                    {
                        strSql1.Append("CREATE_DATE,");
                        strSql2.Append("'" + model.CREATE_DATE + "',");
                    }
                    if (model.UPDATE_DATE != null)
                    {
                        strSql1.Append("UPDATE_DATE,");
                        strSql2.Append("'" + model.UPDATE_DATE + "',");
                    }
                    if (model.DATAGENERATE_DATE != null)
                    {
                        strSql1.Append("DATAGENERATE_DATE,");
                        strSql2.Append("'" + model.DATAGENERATE_DATE + "',");
                    }
                    if (model.SYS_CODE != null)
                    {
                        strSql1.Append("SYS_CODE,");
                        strSql2.Append("'" + model.SYS_CODE + "',");
                    }
                    if (model.SYS_NAME != null)
                    {
                        strSql1.Append("SYS_NAME,");
                        strSql2.Append("'" + model.SYS_NAME + "',");
                    }
                    if (model.ORG_CODE != null)
                    {
                        strSql1.Append("ORG_CODE,");
                        strSql2.Append("'" + model.ORG_CODE + "',");
                    }
                    if (model.ORG_NAME != null)
                    {
                        strSql1.Append("ORG_NAME,");
                        strSql2.Append("'" + model.ORG_NAME + "',");
                    }
                    if (model.TASK_TYPE != null)
                    {
                        strSql1.Append("TASK_TYPE,");
                        strSql2.Append("'" + model.TASK_TYPE + "',");
                    }
                    if (model.PERSON_NAME != null)
                    {
                        strSql1.Append("PERSON_NAME,");
                        strSql2.Append("'" + model.PERSON_NAME + "',");
                    }
                    if (model.CERT_TYPE != null)
                    {
                        strSql1.Append("CERT_TYPE,");
                        strSql2.Append("'" + model.CERT_TYPE + "',");
                    }
                    if (model.CERT_NAME != null)
                    {
                        strSql1.Append("CERT_NAME,");
                        strSql2.Append("'" + model.CERT_NAME + "',");
                    }
                    if (model.CERT_NUMBER != null)
                    {
                        strSql1.Append("CERT_NUMBER,");
                        strSql2.Append("'" + model.CERT_NUMBER + "',");
                    }
                    if (model.PERSON_TEL != null)
                    {
                        strSql1.Append("PERSON_TEL,");
                        strSql2.Append("'" + model.PERSON_TEL + "',");
                    }
                    if (model.TASK_TIME != null)
                    {
                        strSql1.Append("TASK_TIME,");
                        strSql2.Append("'" + model.TASK_TIME + "',");
                    }
                    if (model.TASK_DESC != null)
                    {
                        strSql1.Append("TASK_DESC,");
                        strSql2.Append("'" + model.TASK_DESC + "',");
                    }
                    if (model.DOCTOR_ID != null)
                    {
                        strSql1.Append("DOCTOR_ID,");
                        strSql2.Append("'" + model.DOCTOR_ID + "',");
                    }
                    if (model.DOCTOR_NAME != null)
                    {
                        strSql1.Append("DOCTOR_NAME,");
                        strSql2.Append("'" + model.DOCTOR_NAME + "',");
                    }
                    if (model.BUS_RESULT_CODE != null)
                    {
                        strSql1.Append("BUS_RESULT_CODE,");
                        strSql2.Append("'" + model.BUS_RESULT_CODE + "',");
                    }
                    if (model.BUS_RESULT_DESC != null)
                    {
                        strSql1.Append("BUS_RESULT_DESC,");
                        strSql2.Append("'" + model.BUS_RESULT_DESC + "',");
                    }
                    if (model.UpLoadFlag != null)
                    {
                        strSql1.Append("UpLoadFlag,");
                        strSql2.Append("" + (model.UpLoadFlag ? 1 : 0) + ",");
                    }
                    if (model.AddDateTime != null)
                    {
                        strSql1.Append("AddDateTime,");
                        strSql2.Append("'" + model.AddDateTime + "',");
                    }
                    strSql.Append("insert into SearchReCheckLog(");
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
                else
                {
                    #region 初始第一次
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder strSql1 = new StringBuilder();
                    StringBuilder strSql2 = new StringBuilder();
                    if (model.Id != null)
                    {
                        strSql1.Append("Id,");
                        strSql2.Append("" + model.Id + ",");
                    }
                    if (model.BATCH_NUM != null)
                    {
                        strSql1.Append("BATCH_NUM,");
                        strSql2.Append("'" + model.BATCH_NUM + "',");
                    }
                    if (model.UPLOAD_TIME != null)
                    {
                        strSql1.Append("UPLOAD_TIME,");
                        strSql2.Append("'" + model.UPLOAD_TIME + "',");
                    }
                    if (model.LOCAL_ID != null)
                    {
                        strSql1.Append("LOCAL_ID,");
                        strSql2.Append("'" + model.LOCAL_ID + "',");
                    }
                    if (model.RESULTDESC != null)
                    {
                        strSql1.Append("RESULTDESC,");
                        strSql2.Append("'" + model.RESULTDESC + "',");
                    }
                    if (model.RESULTCODE != null)
                    {
                        strSql1.Append("RESULTCODE,");
                        strSql2.Append("'" + model.RESULTCODE + "',");
                    }
                    if (model.SERIALNUM_ID != null)
                    {
                        strSql1.Append("SERIALNUM_ID,");
                        strSql2.Append("'" + model.SERIALNUM_ID + "',");
                    }
                    if (model.UNIQUEID != null)
                    {
                        strSql1.Append("UNIQUEID,");
                        strSql2.Append("'" + model.UNIQUEID + "'+dbo.GetSortNo('1'),");
                    }
                    if (model.PID != null)
                    {
                        strSql1.Append("PID,");
                        strSql2.Append("'" + model.PID + "',");
                    }
                    if (model.BUSINESS_RELATION_ID != null)
                    {
                        strSql1.Append("BUSINESS_RELATION_ID,");
                        strSql2.Append("'" + model.BUSINESS_RELATION_ID + "',");
                    }
                    if (model.BUSINESS_ACTIVE_TYPE != null)
                    {
                        strSql1.Append("BUSINESS_ACTIVE_TYPE,");
                        strSql2.Append("'" + model.BUSINESS_ACTIVE_TYPE + "',");
                    }
                    if (model.BUSINESS_ACTIVE_DES != null)
                    {
                        strSql1.Append("BUSINESS_ACTIVE_DES,");
                        strSql2.Append("'" + model.BUSINESS_ACTIVE_DES + "',");
                    }
                    if (model.BUSINESS_ID != null)
                    {
                        strSql1.Append("BUSINESS_ID,");
                        strSql2.Append("'" + model.BUSINESS_ID + "',");
                    }
                    if (model.BASIC_ACTIVE_TYPE != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_TYPE,");
                        strSql2.Append("'" + model.BASIC_ACTIVE_TYPE + "',");
                    }
                    if (model.BASIC_ACTIVE_DES != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_DES,");
                        strSql2.Append("'" + model.BASIC_ACTIVE_DES + "',");
                    }
                    if (model.BASIC_ACTIVE_ID != null)
                    {
                        strSql1.Append("BASIC_ACTIVE_ID,");
                        strSql2.Append("'" + model.BASIC_ACTIVE_ID + "',");
                    }
                    if (model.ORGANIZATION_CODE != null)
                    {
                        strSql1.Append("ORGANIZATION_CODE,");
                        strSql2.Append("'" + model.ORGANIZATION_CODE + "',");
                    }
                    if (model.ORGANIZATION_NAME != null)
                    {
                        strSql1.Append("ORGANIZATION_NAME,");
                        strSql2.Append("'" + model.ORGANIZATION_NAME + "',");
                    }
                    if (model.DOMAIN_CODE != null)
                    {
                        strSql1.Append("DOMAIN_CODE,");
                        strSql2.Append("'" + model.DOMAIN_CODE + "',");
                    }
                    if (model.DOMAIN_NAME != null)
                    {
                        strSql1.Append("DOMAIN_NAME,");
                        strSql2.Append("'" + model.DOMAIN_NAME + "',");
                    }
                    if (model.VER != null)
                    {
                        strSql1.Append("VER,");
                        strSql2.Append("'" + model.VER + "',");
                    }
                    if (model.VERDES != null)
                    {
                        strSql1.Append("VERDES,");
                        strSql2.Append("'" + model.VERDES + "',");
                    }
                    if (model.REGION_IDEN != null)
                    {
                        strSql1.Append("REGION_IDEN,");
                        strSql2.Append("'" + model.REGION_IDEN + "',");
                    }
                    if (model.DATA_SECURITY != null)
                    {
                        strSql1.Append("DATA_SECURITY,");
                        strSql2.Append("'" + model.DATA_SECURITY + "',");
                    }
                    if (model.RECORD_IDEN != null)
                    {
                        strSql1.Append("RECORD_IDEN,");
                        strSql2.Append("'" + model.RECORD_IDEN + "',");
                    }
                    if (model.CREATE_DATE != null)
                    {
                        strSql1.Append("CREATE_DATE,");
                        strSql2.Append("'" + model.CREATE_DATE + "',");
                    }
                    if (model.UPDATE_DATE != null)
                    {
                        strSql1.Append("UPDATE_DATE,");
                        strSql2.Append("'" + model.UPDATE_DATE + "',");
                    }
                    if (model.DATAGENERATE_DATE != null)
                    {
                        strSql1.Append("DATAGENERATE_DATE,");
                        strSql2.Append("'" + model.DATAGENERATE_DATE + "',");
                    }
                    if (model.SYS_CODE != null)
                    {
                        strSql1.Append("SYS_CODE,");
                        strSql2.Append("'" + model.SYS_CODE + "',");
                    }
                    if (model.SYS_NAME != null)
                    {
                        strSql1.Append("SYS_NAME,");
                        strSql2.Append("'" + model.SYS_NAME + "',");
                    }
                    if (model.ORG_CODE != null)
                    {
                        strSql1.Append("ORG_CODE,");
                        strSql2.Append("'" + model.ORG_CODE + "',");
                    }
                    if (model.ORG_NAME != null)
                    {
                        strSql1.Append("ORG_NAME,");
                        strSql2.Append("'" + model.ORG_NAME + "',");
                    }
                    if (model.TASK_TYPE != null)
                    {
                        strSql1.Append("TASK_TYPE,");
                        strSql2.Append("'" + model.TASK_TYPE + "',");
                    }
                    if (model.PERSON_NAME != null)
                    {
                        strSql1.Append("PERSON_NAME,");
                        strSql2.Append("'" + model.PERSON_NAME + "',");
                    }
                    if (model.CERT_TYPE != null)
                    {
                        strSql1.Append("CERT_TYPE,");
                        strSql2.Append("'" + model.CERT_TYPE + "',");
                    }
                    if (model.CERT_NAME != null)
                    {
                        strSql1.Append("CERT_NAME,");
                        strSql2.Append("'" + model.CERT_NAME + "',");
                    }
                    if (model.CERT_NUMBER != null)
                    {
                        strSql1.Append("CERT_NUMBER,");
                        strSql2.Append("'" + model.CERT_NUMBER + "',");
                    }
                    if (model.PERSON_TEL != null)
                    {
                        strSql1.Append("PERSON_TEL,");
                        strSql2.Append("'" + model.PERSON_TEL + "',");
                    }
                    if (model.TASK_TIME != null)
                    {
                        strSql1.Append("TASK_TIME,");
                        strSql2.Append("'" + model.TASK_TIME + "',");
                    }
                    if (model.TASK_DESC != null)
                    {
                        strSql1.Append("TASK_DESC,");
                        strSql2.Append("'" + model.TASK_DESC + "',");
                    }
                    if (model.DOCTOR_ID != null)
                    {
                        strSql1.Append("DOCTOR_ID,");
                        strSql2.Append("'" + model.DOCTOR_ID + "',");
                    }
                    if (model.DOCTOR_NAME != null)
                    {
                        strSql1.Append("DOCTOR_NAME,");
                        strSql2.Append("'" + model.DOCTOR_NAME + "',");
                    }
                    if (model.BUS_RESULT_CODE != null)
                    {
                        strSql1.Append("BUS_RESULT_CODE,");
                        strSql2.Append("'" + model.BUS_RESULT_CODE + "',");
                    }
                    if (model.BUS_RESULT_DESC != null)
                    {
                        strSql1.Append("BUS_RESULT_DESC,");
                        strSql2.Append("'" + model.BUS_RESULT_DESC + "',");
                    }
                    if (model.UpLoadFlag != null)
                    {
                        strSql1.Append("UpLoadFlag,");
                        strSql2.Append("" + (model.UpLoadFlag ? 1 : 0) + ",");
                    }
                    if (model.AddDateTime != null)
                    {
                        strSql1.Append("AddDateTime,");
                        strSql2.Append("'" + model.AddDateTime + "',");
                    }
                    strSql.Append("insert into SearchReCheckLog(");
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
                    #endregion
                }
            }
            else
            {
                return false;
            }
			
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZhiFang.Model.SearchReCheckLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SearchReCheckLog set ");
			if (model.BATCH_NUM != null)
			{
				strSql.Append("BATCH_NUM='"+model.BATCH_NUM+"',");
			}
			else
			{
				strSql.Append("BATCH_NUM= null ,");
			}
			if (model.UPLOAD_TIME != null)
			{
				strSql.Append("UPLOAD_TIME='"+model.UPLOAD_TIME+"',");
			}
			else
			{
				strSql.Append("UPLOAD_TIME= null ,");
			}
			if (model.LOCAL_ID != null)
			{
				strSql.Append("LOCAL_ID='"+model.LOCAL_ID+"',");
			}
			else
			{
				strSql.Append("LOCAL_ID= null ,");
			}
			if (model.RESULTDESC != null)
			{
				strSql.Append("RESULTDESC='"+model.RESULTDESC+"',");
			}
			else
			{
				strSql.Append("RESULTDESC= null ,");
			}
			if (model.RESULTCODE != null)
			{
				strSql.Append("RESULTCODE='"+model.RESULTCODE+"',");
			}
			else
			{
				strSql.Append("RESULTCODE= null ,");
			}
			if (model.SERIALNUM_ID != null)
			{
				strSql.Append("SERIALNUM_ID='"+model.SERIALNUM_ID+"',");
			}
			else
			{
				strSql.Append("SERIALNUM_ID= null ,");
			}
			if (model.UNIQUEID != null)
			{
				strSql.Append("UNIQUEID='"+model.UNIQUEID+"',");
			}
			if (model.PID != null)
			{
				strSql.Append("PID='"+model.PID+"',");
			}
			else
			{
				strSql.Append("PID= null ,");
			}
			if (model.BUSINESS_RELATION_ID != null)
			{
				strSql.Append("BUSINESS_RELATION_ID='"+model.BUSINESS_RELATION_ID+"',");
			}
			else
			{
				strSql.Append("BUSINESS_RELATION_ID= null ,");
			}
			if (model.BUSINESS_ACTIVE_TYPE != null)
			{
				strSql.Append("BUSINESS_ACTIVE_TYPE='"+model.BUSINESS_ACTIVE_TYPE+"',");
			}
			else
			{
				strSql.Append("BUSINESS_ACTIVE_TYPE= null ,");
			}
			if (model.BUSINESS_ACTIVE_DES != null)
			{
				strSql.Append("BUSINESS_ACTIVE_DES='"+model.BUSINESS_ACTIVE_DES+"',");
			}
			else
			{
				strSql.Append("BUSINESS_ACTIVE_DES= null ,");
			}
			if (model.BUSINESS_ID != null)
			{
				strSql.Append("BUSINESS_ID='"+model.BUSINESS_ID+"',");
			}
			if (model.BASIC_ACTIVE_TYPE != null)
			{
				strSql.Append("BASIC_ACTIVE_TYPE='"+model.BASIC_ACTIVE_TYPE+"',");
			}
			else
			{
				strSql.Append("BASIC_ACTIVE_TYPE= null ,");
			}
			if (model.BASIC_ACTIVE_DES != null)
			{
				strSql.Append("BASIC_ACTIVE_DES='"+model.BASIC_ACTIVE_DES+"',");
			}
			else
			{
				strSql.Append("BASIC_ACTIVE_DES= null ,");
			}
			if (model.BASIC_ACTIVE_ID != null)
			{
				strSql.Append("BASIC_ACTIVE_ID='"+model.BASIC_ACTIVE_ID+"',");
			}
			if (model.ORGANIZATION_CODE != null)
			{
				strSql.Append("ORGANIZATION_CODE='"+model.ORGANIZATION_CODE+"',");
			}
			if (model.ORGANIZATION_NAME != null)
			{
				strSql.Append("ORGANIZATION_NAME='"+model.ORGANIZATION_NAME+"',");
			}
			if (model.DOMAIN_CODE != null)
			{
				strSql.Append("DOMAIN_CODE='"+model.DOMAIN_CODE+"',");
			}
			if (model.DOMAIN_NAME != null)
			{
				strSql.Append("DOMAIN_NAME='"+model.DOMAIN_NAME+"',");
			}
			if (model.VER != null)
			{
				strSql.Append("VER='"+model.VER+"',");
			}
			else
			{
				strSql.Append("VER= null ,");
			}
			if (model.VERDES != null)
			{
				strSql.Append("VERDES='"+model.VERDES+"',");
			}
			else
			{
				strSql.Append("VERDES= null ,");
			}
			if (model.REGION_IDEN != null)
			{
				strSql.Append("REGION_IDEN='"+model.REGION_IDEN+"',");
			}
			if (model.DATA_SECURITY != null)
			{
				strSql.Append("DATA_SECURITY='"+model.DATA_SECURITY+"',");
			}
			else
			{
				strSql.Append("DATA_SECURITY= null ,");
			}
			if (model.RECORD_IDEN != null)
			{
				strSql.Append("RECORD_IDEN='"+model.RECORD_IDEN+"',");
			}
			else
			{
				strSql.Append("RECORD_IDEN= null ,");
			}
			if (model.CREATE_DATE != null)
			{
				strSql.Append("CREATE_DATE='"+model.CREATE_DATE+"',");
			}
			else
			{
				strSql.Append("CREATE_DATE= null ,");
			}
			if (model.UPDATE_DATE != null)
			{
				strSql.Append("UPDATE_DATE='"+model.UPDATE_DATE+"',");
			}
			else
			{
				strSql.Append("UPDATE_DATE= null ,");
			}
			if (model.DATAGENERATE_DATE != null)
			{
				strSql.Append("DATAGENERATE_DATE='"+model.DATAGENERATE_DATE+"',");
			}
			if (model.SYS_CODE != null)
			{
				strSql.Append("SYS_CODE='"+model.SYS_CODE+"',");
			}
			if (model.SYS_NAME != null)
			{
				strSql.Append("SYS_NAME='"+model.SYS_NAME+"',");
			}
			if (model.ORG_CODE != null)
			{
				strSql.Append("ORG_CODE='"+model.ORG_CODE+"',");
			}
			if (model.ORG_NAME != null)
			{
				strSql.Append("ORG_NAME='"+model.ORG_NAME+"',");
			}
			if (model.TASK_TYPE != null)
			{
				strSql.Append("TASK_TYPE='"+model.TASK_TYPE+"',");
			}
			if (model.PERSON_NAME != null)
			{
				strSql.Append("PERSON_NAME='"+model.PERSON_NAME+"',");
			}
			if (model.CERT_TYPE != null)
			{
				strSql.Append("CERT_TYPE='"+model.CERT_TYPE+"',");
			}
			if (model.CERT_NAME != null)
			{
				strSql.Append("CERT_NAME='"+model.CERT_NAME+"',");
			}
			if (model.CERT_NUMBER != null)
			{
				strSql.Append("CERT_NUMBER='"+model.CERT_NUMBER+"',");
			}
			if (model.PERSON_TEL != null)
			{
				strSql.Append("PERSON_TEL='"+model.PERSON_TEL+"',");
			}
			else
			{
				strSql.Append("PERSON_TEL= null ,");
			}
			if (model.TASK_TIME != null)
			{
				strSql.Append("TASK_TIME='"+model.TASK_TIME+"',");
			}
			if (model.TASK_DESC != null)
			{
				strSql.Append("TASK_DESC='"+model.TASK_DESC+"',");
			}
			if (model.DOCTOR_ID != null)
			{
				strSql.Append("DOCTOR_ID='"+model.DOCTOR_ID+"',");
			}
			else
			{
				strSql.Append("DOCTOR_ID= null ,");
			}
			if (model.DOCTOR_NAME != null)
			{
				strSql.Append("DOCTOR_NAME='"+model.DOCTOR_NAME+"',");
			}
			else
			{
				strSql.Append("DOCTOR_NAME= null ,");
			}
			if (model.BUS_RESULT_CODE != null)
			{
				strSql.Append("BUS_RESULT_CODE='"+model.BUS_RESULT_CODE+"',");
			}
			if (model.BUS_RESULT_DESC != null)
			{
				strSql.Append("BUS_RESULT_DESC='"+model.BUS_RESULT_DESC+"',");
			}
			if (model.UpLoadFlag != null)
			{
				strSql.Append("UpLoadFlag="+ (model.UpLoadFlag? 1 : 0) +",");
			}
			else
			{
				strSql.Append("UpLoadFlag= null ,");
			}
			if (model.AddDateTime != null)
			{
				strSql.Append("AddDateTime='"+model.AddDateTime+"',");
			}
			else
			{
				strSql.Append("AddDateTime= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where Id="+ model.Id+" ");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
		public bool Delete(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SearchReCheckLog ");
			strSql.Append(" where Id="+Id+" " );
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SearchReCheckLog ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.SearchReCheckLog GetModel(long Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Id,BATCH_NUM,UPLOAD_TIME,LOCAL_ID,RESULTDESC,RESULTCODE,SERIALNUM_ID,UNIQUEID,PID,BUSINESS_RELATION_ID,BUSINESS_ACTIVE_TYPE,BUSINESS_ACTIVE_DES,BUSINESS_ID,BASIC_ACTIVE_TYPE,BASIC_ACTIVE_DES,BASIC_ACTIVE_ID,ORGANIZATION_CODE,ORGANIZATION_NAME,DOMAIN_CODE,DOMAIN_NAME,VER,VERDES,REGION_IDEN,DATA_SECURITY,RECORD_IDEN,CREATE_DATE,UPDATE_DATE,DATAGENERATE_DATE,SYS_CODE,SYS_NAME,ORG_CODE,ORG_NAME,TASK_TYPE,PERSON_NAME,CERT_TYPE,CERT_NAME,CERT_NUMBER,PERSON_TEL,TASK_TIME,TASK_DESC,DOCTOR_ID,DOCTOR_NAME,BUS_RESULT_CODE,BUS_RESULT_DESC,UpLoadFlag,AddDateTime ");
			strSql.Append(" from SearchReCheckLog ");
			strSql.Append(" where Id="+Id+" " );
			ZhiFang.Model.SearchReCheckLog model=new ZhiFang.Model.SearchReCheckLog();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=long.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BATCH_NUM"]!=null && ds.Tables[0].Rows[0]["BATCH_NUM"].ToString()!="")
				{
					model.BATCH_NUM=ds.Tables[0].Rows[0]["BATCH_NUM"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UPLOAD_TIME"]!=null && ds.Tables[0].Rows[0]["UPLOAD_TIME"].ToString()!="")
				{
					model.UPLOAD_TIME=ds.Tables[0].Rows[0]["UPLOAD_TIME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LOCAL_ID"]!=null && ds.Tables[0].Rows[0]["LOCAL_ID"].ToString()!="")
				{
					model.LOCAL_ID=ds.Tables[0].Rows[0]["LOCAL_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RESULTDESC"]!=null && ds.Tables[0].Rows[0]["RESULTDESC"].ToString()!="")
				{
					model.RESULTDESC=ds.Tables[0].Rows[0]["RESULTDESC"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RESULTCODE"]!=null && ds.Tables[0].Rows[0]["RESULTCODE"].ToString()!="")
				{
					model.RESULTCODE=ds.Tables[0].Rows[0]["RESULTCODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SERIALNUM_ID"]!=null && ds.Tables[0].Rows[0]["SERIALNUM_ID"].ToString()!="")
				{
					model.SERIALNUM_ID=ds.Tables[0].Rows[0]["SERIALNUM_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UNIQUEID"]!=null && ds.Tables[0].Rows[0]["UNIQUEID"].ToString()!="")
				{
					model.UNIQUEID=ds.Tables[0].Rows[0]["UNIQUEID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PID"]!=null && ds.Tables[0].Rows[0]["PID"].ToString()!="")
				{
					model.PID=ds.Tables[0].Rows[0]["PID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUSINESS_RELATION_ID"]!=null && ds.Tables[0].Rows[0]["BUSINESS_RELATION_ID"].ToString()!="")
				{
					model.BUSINESS_RELATION_ID=ds.Tables[0].Rows[0]["BUSINESS_RELATION_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_TYPE"]!=null && ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_TYPE"].ToString()!="")
				{
					model.BUSINESS_ACTIVE_TYPE=ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_TYPE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_DES"]!=null && ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_DES"].ToString()!="")
				{
					model.BUSINESS_ACTIVE_DES=ds.Tables[0].Rows[0]["BUSINESS_ACTIVE_DES"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUSINESS_ID"]!=null && ds.Tables[0].Rows[0]["BUSINESS_ID"].ToString()!="")
				{
					model.BUSINESS_ID=ds.Tables[0].Rows[0]["BUSINESS_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BASIC_ACTIVE_TYPE"]!=null && ds.Tables[0].Rows[0]["BASIC_ACTIVE_TYPE"].ToString()!="")
				{
					model.BASIC_ACTIVE_TYPE=ds.Tables[0].Rows[0]["BASIC_ACTIVE_TYPE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BASIC_ACTIVE_DES"]!=null && ds.Tables[0].Rows[0]["BASIC_ACTIVE_DES"].ToString()!="")
				{
					model.BASIC_ACTIVE_DES=ds.Tables[0].Rows[0]["BASIC_ACTIVE_DES"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BASIC_ACTIVE_ID"]!=null && ds.Tables[0].Rows[0]["BASIC_ACTIVE_ID"].ToString()!="")
				{
					model.BASIC_ACTIVE_ID=ds.Tables[0].Rows[0]["BASIC_ACTIVE_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ORGANIZATION_CODE"]!=null && ds.Tables[0].Rows[0]["ORGANIZATION_CODE"].ToString()!="")
				{
					model.ORGANIZATION_CODE=ds.Tables[0].Rows[0]["ORGANIZATION_CODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ORGANIZATION_NAME"]!=null && ds.Tables[0].Rows[0]["ORGANIZATION_NAME"].ToString()!="")
				{
					model.ORGANIZATION_NAME=ds.Tables[0].Rows[0]["ORGANIZATION_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DOMAIN_CODE"]!=null && ds.Tables[0].Rows[0]["DOMAIN_CODE"].ToString()!="")
				{
					model.DOMAIN_CODE=ds.Tables[0].Rows[0]["DOMAIN_CODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DOMAIN_NAME"]!=null && ds.Tables[0].Rows[0]["DOMAIN_NAME"].ToString()!="")
				{
					model.DOMAIN_NAME=ds.Tables[0].Rows[0]["DOMAIN_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["VER"]!=null && ds.Tables[0].Rows[0]["VER"].ToString()!="")
				{
					model.VER=ds.Tables[0].Rows[0]["VER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["VERDES"]!=null && ds.Tables[0].Rows[0]["VERDES"].ToString()!="")
				{
					model.VERDES=ds.Tables[0].Rows[0]["VERDES"].ToString();
				}
				if(ds.Tables[0].Rows[0]["REGION_IDEN"]!=null && ds.Tables[0].Rows[0]["REGION_IDEN"].ToString()!="")
				{
					model.REGION_IDEN=ds.Tables[0].Rows[0]["REGION_IDEN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DATA_SECURITY"]!=null && ds.Tables[0].Rows[0]["DATA_SECURITY"].ToString()!="")
				{
					model.DATA_SECURITY=ds.Tables[0].Rows[0]["DATA_SECURITY"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RECORD_IDEN"]!=null && ds.Tables[0].Rows[0]["RECORD_IDEN"].ToString()!="")
				{
					model.RECORD_IDEN=ds.Tables[0].Rows[0]["RECORD_IDEN"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CREATE_DATE"]!=null && ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()!="")
				{
					model.CREATE_DATE=ds.Tables[0].Rows[0]["CREATE_DATE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UPDATE_DATE"]!=null && ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString()!="")
				{
					model.UPDATE_DATE=ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DATAGENERATE_DATE"]!=null && ds.Tables[0].Rows[0]["DATAGENERATE_DATE"].ToString()!="")
				{
					model.DATAGENERATE_DATE=ds.Tables[0].Rows[0]["DATAGENERATE_DATE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SYS_CODE"]!=null && ds.Tables[0].Rows[0]["SYS_CODE"].ToString()!="")
				{
					model.SYS_CODE=ds.Tables[0].Rows[0]["SYS_CODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SYS_NAME"]!=null && ds.Tables[0].Rows[0]["SYS_NAME"].ToString()!="")
				{
					model.SYS_NAME=ds.Tables[0].Rows[0]["SYS_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ORG_CODE"]!=null && ds.Tables[0].Rows[0]["ORG_CODE"].ToString()!="")
				{
					model.ORG_CODE=ds.Tables[0].Rows[0]["ORG_CODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ORG_NAME"]!=null && ds.Tables[0].Rows[0]["ORG_NAME"].ToString()!="")
				{
					model.ORG_NAME=ds.Tables[0].Rows[0]["ORG_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TASK_TYPE"]!=null && ds.Tables[0].Rows[0]["TASK_TYPE"].ToString()!="")
				{
					model.TASK_TYPE=ds.Tables[0].Rows[0]["TASK_TYPE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PERSON_NAME"]!=null && ds.Tables[0].Rows[0]["PERSON_NAME"].ToString()!="")
				{
					model.PERSON_NAME=ds.Tables[0].Rows[0]["PERSON_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CERT_TYPE"]!=null && ds.Tables[0].Rows[0]["CERT_TYPE"].ToString()!="")
				{
					model.CERT_TYPE=ds.Tables[0].Rows[0]["CERT_TYPE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CERT_NAME"]!=null && ds.Tables[0].Rows[0]["CERT_NAME"].ToString()!="")
				{
					model.CERT_NAME=ds.Tables[0].Rows[0]["CERT_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CERT_NUMBER"]!=null && ds.Tables[0].Rows[0]["CERT_NUMBER"].ToString()!="")
				{
					model.CERT_NUMBER=ds.Tables[0].Rows[0]["CERT_NUMBER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PERSON_TEL"]!=null && ds.Tables[0].Rows[0]["PERSON_TEL"].ToString()!="")
				{
					model.PERSON_TEL=ds.Tables[0].Rows[0]["PERSON_TEL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TASK_TIME"]!=null && ds.Tables[0].Rows[0]["TASK_TIME"].ToString()!="")
				{
					model.TASK_TIME=ds.Tables[0].Rows[0]["TASK_TIME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TASK_DESC"]!=null && ds.Tables[0].Rows[0]["TASK_DESC"].ToString()!="")
				{
					model.TASK_DESC=ds.Tables[0].Rows[0]["TASK_DESC"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DOCTOR_ID"]!=null && ds.Tables[0].Rows[0]["DOCTOR_ID"].ToString()!="")
				{
					model.DOCTOR_ID=ds.Tables[0].Rows[0]["DOCTOR_ID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DOCTOR_NAME"]!=null && ds.Tables[0].Rows[0]["DOCTOR_NAME"].ToString()!="")
				{
					model.DOCTOR_NAME=ds.Tables[0].Rows[0]["DOCTOR_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUS_RESULT_CODE"]!=null && ds.Tables[0].Rows[0]["BUS_RESULT_CODE"].ToString()!="")
				{
					model.BUS_RESULT_CODE=ds.Tables[0].Rows[0]["BUS_RESULT_CODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BUS_RESULT_DESC"]!=null && ds.Tables[0].Rows[0]["BUS_RESULT_DESC"].ToString()!="")
				{
					model.BUS_RESULT_DESC=ds.Tables[0].Rows[0]["BUS_RESULT_DESC"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UpLoadFlag"]!=null && ds.Tables[0].Rows[0]["UpLoadFlag"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["UpLoadFlag"].ToString()=="1")||(ds.Tables[0].Rows[0]["UpLoadFlag"].ToString().ToLower()=="true"))
					{
						model.UpLoadFlag=true;
					}
					else
					{
						model.UpLoadFlag=false;
					}
				}
				if(ds.Tables[0].Rows[0]["AddDateTime"]!=null && ds.Tables[0].Rows[0]["AddDateTime"].ToString()!="")
				{
					model.AddDateTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddDateTime"].ToString());
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
			strSql.Append("select Id,BATCH_NUM,UPLOAD_TIME,LOCAL_ID,RESULTDESC,RESULTCODE,SERIALNUM_ID,UNIQUEID,PID,BUSINESS_RELATION_ID,BUSINESS_ACTIVE_TYPE,BUSINESS_ACTIVE_DES,BUSINESS_ID,BASIC_ACTIVE_TYPE,BASIC_ACTIVE_DES,BASIC_ACTIVE_ID,ORGANIZATION_CODE,ORGANIZATION_NAME,DOMAIN_CODE,DOMAIN_NAME,VER,VERDES,REGION_IDEN,DATA_SECURITY,RECORD_IDEN,CREATE_DATE,UPDATE_DATE,DATAGENERATE_DATE,SYS_CODE,SYS_NAME,ORG_CODE,ORG_NAME,TASK_TYPE,PERSON_NAME,CERT_TYPE,CERT_NAME,CERT_NUMBER,PERSON_TEL,TASK_TIME,TASK_DESC,DOCTOR_ID,DOCTOR_NAME,BUS_RESULT_CODE,BUS_RESULT_DESC,UpLoadFlag,AddDateTime ");
			strSql.Append(" FROM SearchReCheckLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,BATCH_NUM,UPLOAD_TIME,LOCAL_ID,RESULTDESC,RESULTCODE,SERIALNUM_ID,UNIQUEID,PID,BUSINESS_RELATION_ID,BUSINESS_ACTIVE_TYPE,BUSINESS_ACTIVE_DES,BUSINESS_ID,BASIC_ACTIVE_TYPE,BASIC_ACTIVE_DES,BASIC_ACTIVE_ID,ORGANIZATION_CODE,ORGANIZATION_NAME,DOMAIN_CODE,DOMAIN_NAME,VER,VERDES,REGION_IDEN,DATA_SECURITY,RECORD_IDEN,CREATE_DATE,UPDATE_DATE,DATAGENERATE_DATE,SYS_CODE,SYS_NAME,ORG_CODE,ORG_NAME,TASK_TYPE,PERSON_NAME,CERT_TYPE,CERT_NAME,CERT_NUMBER,PERSON_TEL,TASK_TIME,TASK_DESC,DOCTOR_ID,DOCTOR_NAME,BUS_RESULT_CODE,BUS_RESULT_DESC,UpLoadFlag,AddDateTime ");
			strSql.Append(" FROM SearchReCheckLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SearchReCheckLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from SearchReCheckLog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}


		#endregion  Method
	}
}
