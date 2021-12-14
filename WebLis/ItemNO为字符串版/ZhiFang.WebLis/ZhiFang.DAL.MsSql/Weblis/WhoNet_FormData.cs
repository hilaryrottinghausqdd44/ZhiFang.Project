using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //WhoNet_FormData
    public partial class WhoNet_FormData : BaseDALLisDB, IWhoNet_FormData
    {



        D_LogInfo d_log = new D_LogInfo();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.WhoNet_FormData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WhoNet_FormData(");
            strSql.Append("LabID,FormID,country_a,laboratory,patient_id,last_name,first_name,sex,age,pat_type,ward,department,ward_type,date_brith,institut,SPEC_NUM,SPEC_DATE,SPEC_TYPE,SPEC_CODE,SPEC_REAS,DATE_ADMIS,DATE_DISCH,DATE_OPER,DATE_WARD,DIAGNOSIS,DATE_INFEC,SITEINFECT,OPERATION,ORDER_MD,CLNOUTCOME,PHYSICIAN,PRIOR_ABX,RESP_TO_TX,SURGEON,STORAGELOC,STORAGENUM,RESID_TYPE,OCCUPATION,ETHNIC,DataAddTime,DataUpdateTime");
            strSql.Append(") values (");
            strSql.Append("@LabID,@FormID,@country_a,@laboratory,@patient_id,@last_name,@first_name,@sex,@age,@pat_type,@ward,@department,@ward_type,@date_brith,@institut,@SPEC_NUM,@SPEC_DATE,@SPEC_TYPE,@SPEC_CODE,@SPEC_REAS,@DATE_ADMIS,@DATE_DISCH,@DATE_OPER,@DATE_WARD,@DIAGNOSIS,@DATE_INFEC,@SITEINFECT,@OPERATION,@ORDER_MD,@CLNOUTCOME,@PHYSICIAN,@PRIOR_ABX,@RESP_TO_TX,@SURGEON,@STORAGELOC,@STORAGENUM,@RESID_TYPE,@OCCUPATION,@ETHNIC,@DataAddTime,@DataUpdateTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@country_a", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@laboratory", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@patient_id", SqlDbType.VarChar,12) ,            
                        new SqlParameter("@last_name", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@first_name", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sex", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@age", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@pat_type", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@ward", SqlDbType.VarChar,6) ,            
                        new SqlParameter("@department", SqlDbType.VarChar,6) ,            
                        new SqlParameter("@ward_type", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@date_brith", SqlDbType.DateTime) ,            
                        new SqlParameter("@institut", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@SPEC_NUM", SqlDbType.VarChar,12) ,            
                        new SqlParameter("@SPEC_DATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@SPEC_TYPE", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@SPEC_CODE", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@SPEC_REAS", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@DATE_ADMIS", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_DISCH", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_OPER", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_WARD", SqlDbType.DateTime) ,            
                        new SqlParameter("@DIAGNOSIS", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DATE_INFEC", SqlDbType.DateTime) ,            
                        new SqlParameter("@SITEINFECT", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ORDER_MD", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CLNOUTCOME", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PHYSICIAN", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PRIOR_ABX", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RESP_TO_TX", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SURGEON", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@STORAGELOC", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@STORAGENUM", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RESID_TYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OCCUPATION", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ETHNIC", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)           
              
            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FormID;
            parameters[2].Value = model.country_a;
            parameters[3].Value = model.laboratory;
            parameters[4].Value = model.patient_id;
            parameters[5].Value = model.last_name;
            parameters[6].Value = model.first_name;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.age;
            parameters[9].Value = model.pat_type;
            parameters[10].Value = model.ward;
            parameters[11].Value = model.department;
            parameters[12].Value = model.ward_type;
            parameters[13].Value = model.date_brith;
            parameters[14].Value = model.institut;
            parameters[15].Value = model.SPEC_NUM;
            parameters[16].Value = model.SPEC_DATE;
            parameters[17].Value = model.SPEC_TYPE;
            parameters[18].Value = model.SPEC_CODE;
            parameters[19].Value = model.SPEC_REAS;
            parameters[20].Value = model.DATE_ADMIS;
            parameters[21].Value = model.DATE_DISCH;
            parameters[22].Value = model.DATE_OPER;
            parameters[23].Value = model.DATE_WARD;
            parameters[24].Value = model.DIAGNOSIS;
            parameters[25].Value = model.DATE_INFEC;
            parameters[26].Value = model.SITEINFECT;
            parameters[27].Value = model.OPERATION;
            parameters[28].Value = model.ORDER_MD;
            parameters[29].Value = model.CLNOUTCOME;
            parameters[30].Value = model.PHYSICIAN;
            parameters[31].Value = model.PRIOR_ABX;
            parameters[32].Value = model.RESP_TO_TX;
            parameters[33].Value = model.SURGEON;
            parameters[34].Value = model.STORAGELOC;
            parameters[35].Value = model.STORAGENUM;
            parameters[36].Value = model.RESID_TYPE;
            parameters[37].Value = model.OCCUPATION;
            parameters[38].Value = model.ETHNIC;
            parameters[39].Value = model.DataAddTime;
            parameters[40].Value = model.DataUpdateTime;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("WhoNet_FormData", "", "", DateTime.Now, 1);
            }
            else
                return -1;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_FormData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WhoNet_FormData set ");

            strSql.Append(" LabID = @LabID , ");
            strSql.Append(" FormID = @FormID , ");
            strSql.Append(" country_a = @country_a , ");
            strSql.Append(" laboratory = @laboratory , ");
            strSql.Append(" patient_id = @patient_id , ");
            strSql.Append(" last_name = @last_name , ");
            strSql.Append(" first_name = @first_name , ");
            strSql.Append(" sex = @sex , ");
            strSql.Append(" age = @age , ");
            strSql.Append(" pat_type = @pat_type , ");
            strSql.Append(" ward = @ward , ");
            strSql.Append(" department = @department , ");
            strSql.Append(" ward_type = @ward_type , ");
            strSql.Append(" date_brith = @date_brith , ");
            strSql.Append(" institut = @institut , ");
            strSql.Append(" SPEC_NUM = @SPEC_NUM , ");
            strSql.Append(" SPEC_DATE = @SPEC_DATE , ");
            strSql.Append(" SPEC_TYPE = @SPEC_TYPE , ");
            strSql.Append(" SPEC_CODE = @SPEC_CODE , ");
            strSql.Append(" SPEC_REAS = @SPEC_REAS , ");
            strSql.Append(" DATE_ADMIS = @DATE_ADMIS , ");
            strSql.Append(" DATE_DISCH = @DATE_DISCH , ");
            strSql.Append(" DATE_OPER = @DATE_OPER , ");
            strSql.Append(" DATE_WARD = @DATE_WARD , ");
            strSql.Append(" DIAGNOSIS = @DIAGNOSIS , ");
            strSql.Append(" DATE_INFEC = @DATE_INFEC , ");
            strSql.Append(" SITEINFECT = @SITEINFECT , ");
            strSql.Append(" OPERATION = @OPERATION , ");
            strSql.Append(" ORDER_MD = @ORDER_MD , ");
            strSql.Append(" CLNOUTCOME = @CLNOUTCOME , ");
            strSql.Append(" PHYSICIAN = @PHYSICIAN , ");
            strSql.Append(" PRIOR_ABX = @PRIOR_ABX , ");
            strSql.Append(" RESP_TO_TX = @RESP_TO_TX , ");
            strSql.Append(" SURGEON = @SURGEON , ");
            strSql.Append(" STORAGELOC = @STORAGELOC , ");
            strSql.Append(" STORAGENUM = @STORAGENUM , ");
            strSql.Append(" RESID_TYPE = @RESID_TYPE , ");
            strSql.Append(" OCCUPATION = @OCCUPATION , ");
            strSql.Append(" ETHNIC = @ETHNIC , ");
            strSql.Append(" DataAddTime = @DataAddTime , ");
            strSql.Append(" DataUpdateTime = @DataUpdateTime ");
            strSql.Append(" where FormID=@FormID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@LabID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormID", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@country_a", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@laboratory", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@patient_id", SqlDbType.VarChar,12) ,            
                        new SqlParameter("@last_name", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@first_name", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@sex", SqlDbType.VarChar,1) ,            
                        new SqlParameter("@age", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@pat_type", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@ward", SqlDbType.VarChar,6) ,            
                        new SqlParameter("@department", SqlDbType.VarChar,6) ,            
                        new SqlParameter("@ward_type", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@date_brith", SqlDbType.DateTime) ,            
                        new SqlParameter("@institut", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@SPEC_NUM", SqlDbType.VarChar,12) ,            
                        new SqlParameter("@SPEC_DATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@SPEC_TYPE", SqlDbType.VarChar,2) ,            
                        new SqlParameter("@SPEC_CODE", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@SPEC_REAS", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@DATE_ADMIS", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_DISCH", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_OPER", SqlDbType.DateTime) ,            
                        new SqlParameter("@DATE_WARD", SqlDbType.DateTime) ,            
                        new SqlParameter("@DIAGNOSIS", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DATE_INFEC", SqlDbType.DateTime) ,            
                        new SqlParameter("@SITEINFECT", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OPERATION", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ORDER_MD", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CLNOUTCOME", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PHYSICIAN", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@PRIOR_ABX", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RESP_TO_TX", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SURGEON", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@STORAGELOC", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@STORAGENUM", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RESID_TYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@OCCUPATION", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ETHNIC", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DataAddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@DataUpdateTime", SqlDbType.DateTime)           
              
            };

            parameters[0].Value = model.LabID;
            parameters[1].Value = model.FormID;
            parameters[2].Value = model.country_a;
            parameters[3].Value = model.laboratory;
            parameters[4].Value = model.patient_id;
            parameters[5].Value = model.last_name;
            parameters[6].Value = model.first_name;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.age;
            parameters[9].Value = model.pat_type;
            parameters[10].Value = model.ward;
            parameters[11].Value = model.department;
            parameters[12].Value = model.ward_type;
            parameters[13].Value = model.date_brith;
            parameters[14].Value = model.institut;
            parameters[15].Value = model.SPEC_NUM;
            parameters[16].Value = model.SPEC_DATE;
            parameters[17].Value = model.SPEC_TYPE;
            parameters[18].Value = model.SPEC_CODE;
            parameters[19].Value = model.SPEC_REAS;
            parameters[20].Value = model.DATE_ADMIS;
            parameters[21].Value = model.DATE_DISCH;
            parameters[22].Value = model.DATE_OPER;
            parameters[23].Value = model.DATE_WARD;
            parameters[24].Value = model.DIAGNOSIS;
            parameters[25].Value = model.DATE_INFEC;
            parameters[26].Value = model.SITEINFECT;
            parameters[27].Value = model.OPERATION;
            parameters[28].Value = model.ORDER_MD;
            parameters[29].Value = model.CLNOUTCOME;
            parameters[30].Value = model.PHYSICIAN;
            parameters[31].Value = model.PRIOR_ABX;
            parameters[32].Value = model.RESP_TO_TX;
            parameters[33].Value = model.SURGEON;
            parameters[34].Value = model.STORAGELOC;
            parameters[35].Value = model.STORAGENUM;
            parameters[36].Value = model.RESID_TYPE;
            parameters[37].Value = model.OCCUPATION;
            parameters[38].Value = model.ETHNIC;
            parameters[39].Value = model.DataAddTime;
            parameters[40].Value = model.DataUpdateTime;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("WhoNet_FormData", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WhoNet_FormData ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回关联表数据
        /// </summary>
        /// <returns></returns>
        public DataSet JoinCount(string LABORATORY, DateTime? SPEC_DATE, string SPEC_TYPE, string ORGANISM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" from WhoNet_MicroData a,WhoNet_FormData b");
            strSql.Append(" where 1=1");
            strSql.Append(" and a.[FomID] =b.[FormID]");
            strSql.Append(" and b.LABORATORY ='" + LABORATORY + "' and b.SPEC_DATE='" + SPEC_DATE + "'");
            strSql.Append(" and b.SPEC_TYPE='" + SPEC_TYPE + "' and a.ORGANISM='" + ORGANISM + "'");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_FormData GetModel()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LabID, FormID, country_a, laboratory, patient_id, last_name, first_name, sex, age, pat_type, ward, department, ward_type, date_brith, institut, SPEC_NUM, SPEC_DATE, SPEC_TYPE, SPEC_CODE, SPEC_REAS, DATE_ADMIS, DATE_DISCH, DATE_OPER, DATE_WARD, DIAGNOSIS, DATE_INFEC, SITEINFECT, OPERATION, ORDER_MD, CLNOUTCOME, PHYSICIAN, PRIOR_ABX, RESP_TO_TX, SURGEON, STORAGELOC, STORAGENUM, RESID_TYPE, OCCUPATION, ETHNIC, DataAddTime, DataUpdateTime, DataTimeStamp  ");
            strSql.Append("  from WhoNet_FormData ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
			};


            ZhiFang.Model.WhoNet_FormData model = new ZhiFang.Model.WhoNet_FormData();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(ds.Tables[0].Rows[0]["LabID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FormID"].ToString() != "")
                {
                    model.FormID = long.Parse(ds.Tables[0].Rows[0]["FormID"].ToString());
                }
                model.country_a = ds.Tables[0].Rows[0]["country_a"].ToString();
                model.laboratory = ds.Tables[0].Rows[0]["laboratory"].ToString();
                model.patient_id = ds.Tables[0].Rows[0]["patient_id"].ToString();
                model.last_name = ds.Tables[0].Rows[0]["last_name"].ToString();
                model.first_name = ds.Tables[0].Rows[0]["first_name"].ToString();
                model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                model.age = ds.Tables[0].Rows[0]["age"].ToString();
                model.pat_type = ds.Tables[0].Rows[0]["pat_type"].ToString();
                model.ward = ds.Tables[0].Rows[0]["ward"].ToString();
                model.department = ds.Tables[0].Rows[0]["department"].ToString();
                model.ward_type = ds.Tables[0].Rows[0]["ward_type"].ToString();
                if (ds.Tables[0].Rows[0]["date_brith"].ToString() != "")
                {
                    model.date_brith = DateTime.Parse(ds.Tables[0].Rows[0]["date_brith"].ToString());
                }
                model.institut = ds.Tables[0].Rows[0]["institut"].ToString();
                model.SPEC_NUM = ds.Tables[0].Rows[0]["SPEC_NUM"].ToString();
                if (ds.Tables[0].Rows[0]["SPEC_DATE"].ToString() != "")
                {
                    model.SPEC_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["SPEC_DATE"].ToString());
                }
                model.SPEC_TYPE = ds.Tables[0].Rows[0]["SPEC_TYPE"].ToString();
                model.SPEC_CODE = ds.Tables[0].Rows[0]["SPEC_CODE"].ToString();
                model.SPEC_REAS = ds.Tables[0].Rows[0]["SPEC_REAS"].ToString();
                if (ds.Tables[0].Rows[0]["DATE_ADMIS"].ToString() != "")
                {
                    model.DATE_ADMIS = DateTime.Parse(ds.Tables[0].Rows[0]["DATE_ADMIS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DATE_DISCH"].ToString() != "")
                {
                    model.DATE_DISCH = DateTime.Parse(ds.Tables[0].Rows[0]["DATE_DISCH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DATE_OPER"].ToString() != "")
                {
                    model.DATE_OPER = DateTime.Parse(ds.Tables[0].Rows[0]["DATE_OPER"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DATE_WARD"].ToString() != "")
                {
                    model.DATE_WARD = DateTime.Parse(ds.Tables[0].Rows[0]["DATE_WARD"].ToString());
                }
                model.DIAGNOSIS = ds.Tables[0].Rows[0]["DIAGNOSIS"].ToString();
                if (ds.Tables[0].Rows[0]["DATE_INFEC"].ToString() != "")
                {
                    model.DATE_INFEC = DateTime.Parse(ds.Tables[0].Rows[0]["DATE_INFEC"].ToString());
                }
                model.SITEINFECT = ds.Tables[0].Rows[0]["SITEINFECT"].ToString();
                model.OPERATION = ds.Tables[0].Rows[0]["OPERATION"].ToString();
                model.ORDER_MD = ds.Tables[0].Rows[0]["ORDER_MD"].ToString();
                model.CLNOUTCOME = ds.Tables[0].Rows[0]["CLNOUTCOME"].ToString();
                model.PHYSICIAN = ds.Tables[0].Rows[0]["PHYSICIAN"].ToString();
                model.PRIOR_ABX = ds.Tables[0].Rows[0]["PRIOR_ABX"].ToString();
                model.RESP_TO_TX = ds.Tables[0].Rows[0]["RESP_TO_TX"].ToString();
                model.SURGEON = ds.Tables[0].Rows[0]["SURGEON"].ToString();
                model.STORAGELOC = ds.Tables[0].Rows[0]["STORAGELOC"].ToString();
                model.STORAGENUM = ds.Tables[0].Rows[0]["STORAGENUM"].ToString();
                model.RESID_TYPE = ds.Tables[0].Rows[0]["RESID_TYPE"].ToString();
                model.OCCUPATION = ds.Tables[0].Rows[0]["OCCUPATION"].ToString();
                model.ETHNIC = ds.Tables[0].Rows[0]["ETHNIC"].ToString();
                if (ds.Tables[0].Rows[0]["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataAddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataUpdateTime"].ToString() != "")
                {
                    model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DataUpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[0]["DataTimeStamp"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM WhoNet_FormData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append(" * ");
            strSql.Append(" FROM WhoNet_FormData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }



        #region IDataBase<WhoNet_FormData> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(Model.WhoNet_FormData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM WhoNet_FormData where 1=1 ");
            if (model.LabID != null)
            {
                strSql.Append(" and LabID=" + model.LabID + " ");
            }
            if (model.FormID != null)
            {
                strSql.Append(" and FormID=" + model.FormID + " ");
            }
            if (model.country_a != null && model.country_a != "")
            {
                strSql.Append(" and country_a='" + model.country_a + "' ");
            }
            if (model.laboratory != null && model.laboratory != "")
            {
                strSql.Append(" and laboratory=" + model.laboratory + " ");
            }
            if (model.patient_id != null && model.patient_id != "")
            {
                strSql.Append(" and patient_id='" + model.patient_id + "' ");
            }
            if (model.last_name != null && model.last_name != "")
            {
                strSql.Append(" and last_name='" + model.last_name + "' ");
            }
            if (model.first_name != null && model.first_name != "")
            {
                strSql.Append(" and first_name='" + model.first_name + "' ");
            }
            if (model.sex != null && model.sex != "")
            {
                strSql.Append(" and sex='" + model.sex + "' ");
            }
            if (model.age != null && model.age != "")
            {
                strSql.Append(" and age='" + model.age + "' ");
            }
            if (model.pat_type != null && model.pat_type != "")
            {
                strSql.Append(" and pat_type='" + model.pat_type + "' ");
            }
            if (model.ward != null && model.ward != "")
            {
                strSql.Append(" and ward='" + model.ward + "' ");
            }
            if (model.department != null && model.department != "")
            {
                strSql.Append(" and department='" + model.department + "' ");
            }
            if (model.ward_type != null && model.ward_type != "")
            {
                strSql.Append(" and ward_type='" + model.ward_type + "' ");
            }

            if (model.date_brith != null)
            {
                strSql.Append(" and date_brith='" + model.date_brith + "' ");
            }
            if (model.institut != null && model.institut != "")
            {
                strSql.Append(" and institut='" + model.institut + "' ");
            }
            if (model.SPEC_NUM != null && model.SPEC_NUM != "")
            {
                strSql.Append(" and SPEC_NUM='" + model.SPEC_NUM + "' ");
            }
            if (model.SPEC_DATE != null)
            {
                strSql.Append(" and SPEC_DATE='" + model.SPEC_DATE + "' ");
            }
            if (model.SPEC_TYPE != null && model.SPEC_TYPE != "")
            {
                strSql.Append(" and SPEC_TYPE='" + model.SPEC_TYPE + "' ");
            }

            if (model.SPEC_CODE != null && model.SPEC_CODE != "")
            {
                strSql.Append(" and SPEC_CODE='" + model.SPEC_CODE + "' ");
            }
            if (model.SPEC_REAS != null && model.SPEC_REAS != "")
            {
                strSql.Append(" and SPEC_REAS='" + model.SPEC_REAS + "' ");
            }
            if (model.DATE_ADMIS != null)
            {
                strSql.Append(" and DATE_ADMIS='" + model.DATE_ADMIS + "' ");
            }
            if (model.DATE_DISCH != null)
            {
                strSql.Append(" and DATE_DISCH='" + model.DATE_DISCH + "' ");
            }
            if (model.DATE_OPER != null)
            {
                strSql.Append(" and DATE_OPER='" + model.DATE_OPER + "' ");
            }

            if (model.DATE_WARD != null)
            {
                strSql.Append(" and DATE_WARD='" + model.DATE_WARD + "' ");
            }
            if (model.DIAGNOSIS != null && model.DIAGNOSIS != "")
            {
                strSql.Append(" and DIAGNOSIS='" + model.DIAGNOSIS + "' ");
            }
            if (model.DATE_INFEC != null)
            {
                strSql.Append(" and DATE_INFEC='" + model.DATE_INFEC + "' ");
            }
            if (model.SITEINFECT != null && model.SITEINFECT != "")
            {
                strSql.Append(" and SITEINFECT='" + model.SITEINFECT + "' ");
            }
            if (model.OPERATION != null && model.OPERATION != "")
            {
                strSql.Append(" and OPERATION='" + model.OPERATION + "' ");
            }

            if (model.ORDER_MD != null && model.OPERATION != "")
            {
                strSql.Append(" and ORDER_MD='" + model.ORDER_MD + "' ");
            }
            if (model.CLNOUTCOME != null && model.CLNOUTCOME != "")
            {
                strSql.Append(" and CLNOUTCOME='" + model.CLNOUTCOME + "' ");
            }
            if (model.PHYSICIAN != null && model.PHYSICIAN != "")
            {
                strSql.Append(" and PHYSICIAN='" + model.PHYSICIAN + "' ");
            }
            if (model.PRIOR_ABX != null && model.PRIOR_ABX != "")
            {
                strSql.Append(" and PRIOR_ABX='" + model.PRIOR_ABX + "' ");
            }
            if (model.RESP_TO_TX != null && model.RESP_TO_TX != "")
            {
                strSql.Append(" and RESP_TO_TX='" + model.RESP_TO_TX + "' ");
            }

            if (model.SURGEON != null && model.SURGEON != "")
            {
                strSql.Append(" and SURGEON='" + model.SURGEON + "' ");
            }
            if (model.STORAGELOC != null && model.STORAGELOC != "")
            {
                strSql.Append(" and STORAGELOC='" + model.STORAGELOC + "' ");
            }
            if (model.STORAGENUM != null && model.STORAGENUM != "")
            {
                strSql.Append(" and STORAGENUM='" + model.STORAGENUM + "' ");
            }
            if (model.RESID_TYPE != null && model.RESID_TYPE != "")
            {
                strSql.Append(" and RESID_TYPE='" + model.RESID_TYPE + "' ");
            }
            if (model.OCCUPATION != null && model.OCCUPATION != "")
            {
                strSql.Append(" and OCCUPATION='" + model.OCCUPATION + "' ");
            }

            if (model.ETHNIC != null && model.ETHNIC != "")
            {
                strSql.Append(" and ETHNIC='" + model.ETHNIC + "' ");
            }
            if (model.DataAddTime != null)
            {
                strSql.Append(" and DataAddTime='" + model.DataAddTime + "' ");
            }
            if (model.DataUpdateTime != null)
            {
                strSql.Append(" and DataUpdateTime='" + model.DataUpdateTime + "' ");
            }
            if (model.DataTimeStamp != null)
            {
                strSql.Append(" and DataTimeStamp='" + model.DataTimeStamp + "' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, Model.WhoNet_FormData t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.WhoNet_FormData t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

