using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using System.Collections;

namespace ZhiFang.DAL.Oracle.weblis
{
    public class ClientProfile : BaseDALLisDB, IDClientProfile, IDBatchCopy
    {
          public ClientProfile(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
          public ClientProfile()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

          public bool Exists(string ClIENTControlNo)
          {
              throw new NotImplementedException();
          }

          public bool Exists(System.Collections.Hashtable ht)
          {
              throw new NotImplementedException();
          }

          public int AddByDataRow(System.Data.DataRow dr)
          {
              throw new NotImplementedException();
          }

          public int UpdateByDataRow(System.Data.DataRow dr)
          {
              throw new NotImplementedException();
          }

          public int Delete(string ClIENTControlNo)
          {
              throw new NotImplementedException();
          }

          public int DeleteList(string Idlist)
          {
              throw new NotImplementedException();
          }

          public Model.ClientProfile GetModel(string ClientProfile)
          {
              throw new NotImplementedException();
          }

          public int GetMaxId()
          {
              throw new NotImplementedException();
          }

          public int Add(Model.ClientProfile t)
          {
              throw new NotImplementedException();
          }

          public int Update(Model.ClientProfile t)
          {
              throw new NotImplementedException();
          }

          public System.Data.DataSet GetList(Model.ClientProfile t)
          {
              StringBuilder strSql = new StringBuilder();
              strSql.Append("select ClientProfile.*, CLIENTELE.CNAME");
              strSql.Append(" FROM ClientProfile join  CLIENTELE on ClientProfile.ClientNo=CLIENTELE.ClIENTNO");
              if (t.ClientProfileCName != null)
              {
                  strSql.Append(" where ClientProfile.ClientProfileCName='" + t .ClientProfileCName+ "'");
              }
              return DbHelperSQL.ExecuteDataSet(strSql.ToString());
          }

          public System.Data.DataSet GetList(int Top, Model.ClientProfile t, string filedOrder)
          {
              throw new NotImplementedException();
          }

          public System.Data.DataSet GetAllList()
          {
              StringBuilder strSql = new StringBuilder();
              strSql.Append("select distinct ClientProfileCName ");
              strSql.Append(" FROM ClientProfile ");
              return DbHelperSQL.ExecuteDataSet(strSql.ToString());
          }

          public int AddUpdateByDataSet(System.Data.DataSet ds)
          {
              throw new NotImplementedException();
          }

          public int GetTotalCount()
          {
              throw new NotImplementedException();
          }

          public int GetTotalCount(Model.ClientProfile t)
          {
              throw new NotImplementedException();
          }

          public bool CopyToLab(List<string> lst)
          {
              throw new NotImplementedException();
          }

          public int DeleteByDataRow(System.Data.DataRow dr)
          {
              throw new NotImplementedException();
          }


          public DataSet GetClientNo()
          {
              StringBuilder strSql = new StringBuilder();
              strSql.Append("select  ClientNo ");
              strSql.Append(" FROM ClientProfile ");
              return DbHelperSQL.ExecuteDataSet(strSql.ToString());
          }


          public bool Add(List<Model.ClientProfile> l)
          {
              try
              {
                  ArrayList al = new ArrayList();
                  if (l != null && l.Count > 0)
                  {
                      foreach (Model.ClientProfile lcc_m in l)
                      {
                          if (lcc_m.ClientProfileCName != null && lcc_m.ClientNo != null)
                          {
                              StringBuilder strSql = new StringBuilder();
                              strSql.Append("insert into ClientProfile(");
                              strSql.Append("ClientProfileCName,ClientNo,MergeRuleName");
                              strSql.Append(") values (");
                              strSql.Append("'" + lcc_m.ClientProfileCName + "'," + lcc_m.ClientNo + ",'" + lcc_m.MergeRuleName + "'");
                              strSql.Append(") ");
                              Common.Log.Log.Info("Add:" + strSql.ToString());
                              al.Add(strSql.ToString());
                              ZhiFang.Common.Log.Log.Info(strSql.ToString());
                          }
                      }

                      DbHelperSQL.BatchUpdateWithTransaction(al);
                      return true;
                  }
                  else
                  {
                      DbHelperSQL.BatchUpdateWithTransaction(al);
                      return true;
                  }
              }
              catch (Exception e)
              {
                  Common.Log.Log.Error(e.ToString());
                  return false;
              }
          }


          public bool AddList(List<Model.ClientProfile> l)
          {
              try
              {
                  ArrayList al = new ArrayList();
                  al.Add(" delete from ClientProfile where ClientProfileCName='" + l.ElementAt(0).ProfileCName + "'");
                  if (l != null && l.Count > 0)
                  {
                      foreach (Model.ClientProfile lcc_m in l)
                      {
                          if (lcc_m.ClientProfileCName != null && lcc_m.ClientNo != null)
                          {
                              StringBuilder strSql = new StringBuilder();
                              strSql.Append("insert into ClientProfile(");
                              strSql.Append("ClientProfileCName,ClientNo,MergeRuleName");
                              strSql.Append(") values (");
                              strSql.Append("'" + lcc_m.ClientProfileCName + "'," + lcc_m.ClientNo + ",'" + lcc_m.MergeRuleName+"'");
                              strSql.Append(") ");
                              Common.Log.Log.Info("Add:" + strSql.ToString());
                              al.Add(strSql.ToString());
                              ZhiFang.Common.Log.Log.Info(strSql.ToString());
                          }
                      }
                      DbHelperSQL.BatchUpdateWithTransaction(al);
                      return true;
                  }
                  else
                  {
                      DbHelperSQL.BatchUpdateWithTransaction(al);
                      return true;
                  }
              }
              catch (Exception e)
              {
                  Common.Log.Log.Error(e.ToString());
                  return false;
              }
          }


          public bool IsExist(string labCodeNo)
          {
              throw new NotImplementedException();
          }

          public bool DeleteByLabCode(string LabCodeNo)
          {
              throw new NotImplementedException();
          }
    }
}
