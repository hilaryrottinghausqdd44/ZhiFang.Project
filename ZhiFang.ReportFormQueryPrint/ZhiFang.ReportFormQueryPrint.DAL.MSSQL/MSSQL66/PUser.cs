using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.IO;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类PUser。
	/// </summary>
	public class PUser:IDPUser
	{
        public PUser()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserNo", "PUser");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserNo, string ShortCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PUser");
            strSql.Append(" where UserNo=" + UserNo + " and ShortCode='" + ShortCode + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.PUser model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            
            strSql1.Append("UserNo,");
            strSql2.Append("" + Common.GUIDHelp.GetGUIDInt() + ",");
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.Password != null)
            {
                strSql1.Append("Password,");
                strSql2.Append("'" + model.Password + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.Gender != null)
            {
                strSql1.Append("Gender,");
                strSql2.Append("" + model.Gender + ",");
            }
            if (model.Birthday != null)
            {
                strSql1.Append("Birthday,");
                strSql2.Append("'" + model.Birthday + "',");
            }
            if (model.Role != null)
            {
                strSql1.Append("Role,");
                strSql2.Append("'" + model.Role + "',");
            }
            if (model.Resume != null)
            {
                strSql1.Append("Resume,");
                strSql2.Append("'" + model.Resume + "',");
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
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            if (model.userimage != null)
            {
                strSql1.Append("userimage,");
                strSql2.Append("" + model.userimage + ",");
            }
            if (model.usertype != null)
            {
                strSql1.Append("usertype,");
                strSql2.Append("'" + model.usertype + "',");
            }
            if (model.SectorTypeNo != null)
            {
                strSql1.Append("SectorTypeNo,");
                strSql2.Append("" + model.SectorTypeNo + ",");
            }
            if (model.UserImeName != null)
            {
                strSql1.Append("UserImeName,");
                strSql2.Append("'" + model.UserImeName + "',");
            }
            if (model.IsManager != null)
            {
                strSql1.Append("IsManager,");
                strSql2.Append("" + model.IsManager + ",");
            }
            if (model.PassWordS != null)
            {
                strSql1.Append("PassWordS,");
                strSql2.Append("'" + model.PassWordS + "',");
            }
            
            strSql.Append("insert into PUser(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.PUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PUser set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.Password != null)
            {
                strSql.Append("Password='" + model.Password + "',");
            }
            if (model.Gender != null)
            {
                strSql.Append("Gender=" + model.Gender + ",");
            }
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            if (model.Role != null)
            {
                strSql.Append("Role='" + model.Role + "',");
            }
            if (model.Resume != null)
            {
                strSql.Append("Resume='" + model.Resume + "',");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.userimage != null)
            {
                strSql.Append("userimage=" + model.userimage + ",");
            }
            if (model.usertype != null)
            {
                strSql.Append("usertype='" + model.usertype + "',");
            }
            if (model.SectorTypeNo != null)
            {
                strSql.Append("SectorTypeNo=" + model.SectorTypeNo + ",");
            }
            if (model.UserImeName != null)
            {
                strSql.Append("UserImeName='" + model.UserImeName + "',");
            }
            if (model.IsManager != null)
            {
                strSql.Append("IsManager=" + model.IsManager + ",");
            }
            if (model.PassWordS != null)
            {
                strSql.Append("PassWordS='" + model.PassWordS + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where UserNo=" + model.UserNo + " and ShortCode='" + model.ShortCode + "' ");
            ZhiFang.Common.Log.Log.Debug("PUser.update: sql="+strSql.ToString());
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int UserNo, string ShortCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PUser ");
            strSql.Append(" where UserNo=" + UserNo + " and ShortCode='" + ShortCode + "' ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.PUser GetModel(int UserNo, string ShortCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,SectorTypeNo,UserImeName,IsManager,PassWordS ");
            strSql.Append(" from PUser ");
            strSql.Append(" where UserNo=" + UserNo + " and ShortCode='" + ShortCode + "' ");
            Model.PUser model = new Model.PUser();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserNo"].ToString() != "")
                {
                    model.UserNo = int.Parse(ds.Tables[0].Rows[0]["UserNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    model.Gender = int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.Role = ds.Tables[0].Rows[0]["Role"].ToString();
                model.Resume = ds.Tables[0].Rows[0]["Resume"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                if (ds.Tables[0].Rows[0]["userimage"].ToString() != "")
                {
                    model.userimage = (byte[])ds.Tables[0].Rows[0]["userimage"];
                }
                model.usertype = ds.Tables[0].Rows[0]["usertype"].ToString();
                if (ds.Tables[0].Rows[0]["SectorTypeNo"].ToString() != "")
                {
                    model.SectorTypeNo = int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
                }
                model.UserImeName = ds.Tables[0].Rows[0]["UserImeName"].ToString();
                if (ds.Tables[0].Rows[0]["IsManager"].ToString() != "")
                {
                    model.IsManager = int.Parse(ds.Tables[0].Rows[0]["IsManager"].ToString());
                }
                model.PassWordS = ds.Tables[0].Rows[0]["PassWordS"].ToString();
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
            strSql.Append("select UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,SectorTypeNo,UserImeName,IsManager,PassWordS ");
            strSql.Append(" FROM PUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
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
            strSql.Append(" UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,SectorTypeNo,UserImeName,IsManager,PassWordS ");
            strSql.Append(" FROM PUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */

        public DataSet GetList(Model.PUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,SectorTypeNo,UserImeName,IsManager,PassWordS ");
            strSql.Append(" FROM PUser where 1=1 ");
            if (model.UserNo != null)
            {
                strSql.Append(" and UserNo=" + model.UserNo + "");
            }
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.Password != null)
            {
                strSql.Append(" and Password='" + model.Password + "'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.Gender != null)
            {
                strSql.Append(" and Gender=" + model.Gender + "");
            }
            if (model.Birthday != null)
            {
                strSql.Append(" and Birthday='" + model.Birthday + "'");
            }
            if (model.Role != null)
            {
                strSql.Append(" and Role='" + model.Role + "'");
            }
            if (model.Resume != null)
            {
                strSql.Append(" and Resume='" + model.Resume + "'");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.userimage != null)
            {
                strSql.Append(" and userimage=" + model.userimage + "");
            }
            if (model.usertype != null)
            {
                strSql.Append(" and usertype='" + model.usertype + "'");
            }
            if (model.SectorTypeNo != null)
            {
                strSql.Append(" and SectorTypeNo=" + model.SectorTypeNo + "");
            }
            if (model.UserImeName != null)
            {
                strSql.Append(" and UserImeName='" + model.UserImeName + "'");
            }
            if (model.IsManager != null)
            {
                strSql.Append(" and IsManager=" + model.IsManager + "");
            }
            if (model.PassWordS != null)
            {
                strSql.Append(" and PassWordS='" + model.PassWordS + "'");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion


        public DataSet GetListByPUserIdList(string puseridlist)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPUserIdList(string[] puseridlist)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPUserNameList(List<string> puseridlist)
        {
            if (puseridlist == null || puseridlist.Count <= 0)
            {
                return null;
            }
            string strSql = " select * FROM PUser where  1=2  or CName in ('" + string.Join("\',\'", puseridlist.ToArray()) + "')";
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            ZhiFang.Common.Log.Log.Debug("6.6.PUser.GetListByPUserNameList:sql=" + strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("FilePath");
              
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    byte[] imagebytes = null;
                    if (ds.Tables[0].Rows[i]["userimage"] != DBNull.Value)
                    {
                        imagebytes = (byte[])ds.Tables[0].Rows[i]["userimage"];

                        string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormImagesURL") + "Puser\\";// + Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString()).ToString("yyyy-MM-dd") + "\\";
                        string filename ="_" + ds.Tables[0].Rows[i]["CName"].ToString() + "_" + ds.Tables[0].Rows[i]["UserNo"].ToString()+ ".jpg";
                        if (File.Exists(path + "\\" + filename))//原有是否覆盖
                        {
                            ds.Tables[0].Rows[i]["FilePath"] = path + filename;
                        }
                        else {
                            
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                if(!File.Exists(path + "\\" + filename))
                                {
                                    //FileStream fs = new FileStream(path + "\\" + filename, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                                    FileStream fs = new FileStream(path + "\\" + filename, FileMode.OpenOrCreate);
                                    fs.Write(imagebytes, 0, imagebytes.Count());
                                    fs.Close();
                                }
                            
                        }
                           
                      
                    }
                }
                for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if (ds.Tables[0].Rows[i]["userimage"] == DBNull.Value)
                    {
                        ds.Tables[0].Rows.RemoveAt(i);
                    }
                }
                return ds;
            }
            return null;
        }
        public DataSet GetOperatorChecker(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CName ");
            strSql.Append(" FROM PUser ");
            if (where.Trim() != "")
            {
                strSql.Append(" where " + where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetIsPUser(long UserNo)
        {
            string sql = "select count(*) from PUser where UserNo=" + UserNo;
            ZhiFang.Common.Log.Log.Debug("GetIsPUser:sql=" + sql);
            return int.Parse(DbHelperSQL.Query(sql).Tables[0].Rows[0][0].ToString());        
        }

        public int GetCreatePUserESignature(string SqlWhere)
        {
            string strSql = " select * FROM PUser ";
            if (null != SqlWhere && !"".Equals(SqlWhere)) {
                strSql += "where " + SqlWhere;
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            ZhiFang.Common.Log.Log.Debug("6.6.PUser.GetListByPUserNameList:sql=" + strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("FilePath");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    byte[] imagebytes = null;
                    if (ds.Tables[0].Rows[i]["userimage"] != DBNull.Value)
                    {
                        imagebytes = (byte[])ds.Tables[0].Rows[i]["userimage"];

                        string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormImagesURL") + "Puser\\";// + Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString()).ToString("yyyy-MM-dd") + "\\";
                        string filename = "_" + ds.Tables[0].Rows[i]["CName"].ToString() + "_" + ds.Tables[0].Rows[i]["UserNo"].ToString() + ".jpg";
                        if (File.Exists(path + "\\" + filename))//原有是否覆盖
                        {
                            ds.Tables[0].Rows[i]["FilePath"] = path + filename;
                        }
                        else
                        {

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            if (!File.Exists(path + "\\" + filename))
                            {
                                //FileStream fs = new FileStream(path + "\\" + filename, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                                FileStream fs = new FileStream(path + "\\" + filename, FileMode.OpenOrCreate);
                                fs.Write(imagebytes, 0, imagebytes.Count());
                                fs.Close();
                            }

                        }


                    }
                }
                for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if (ds.Tables[0].Rows[i]["userimage"] == DBNull.Value)
                    {
                        ds.Tables[0].Rows.RemoveAt(i);
                    }
                }
                return 1;
            }
            return 0;
        }
    }
}

