using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //GroupItem
    public partial class GroupItem : IDGroupItem, IDBatchCopy, IDGetListByTimeStampe
    {
        DBUtility.IDBConnection idb;
        public GroupItem(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public GroupItem()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GroupItem(");
            strSql.Append("PItemNo,ItemNo");
            strSql.Append(") values (");
            strSql.Append("@PItemNo,@ItemNo");
            strSql.Append(") ");
            //		
            SqlParameter[] parameters = {
			            new SqlParameter("@PItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.PItemNo;
            parameters[1].Value = model.ItemNo;
            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GroupItem set ");

            strSql.Append(" PItemNo = @PItemNo , ");
            strSql.Append(" ItemNo = @ItemNo  ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@PItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ItemNo", SqlDbType.VarChar,50)             
              
            };

            if (model.Id != null)
            {
                parameters[0].Value = model.Id;
            }

            if (model.PItemNo != null)
            {
                parameters[1].Value = model.PItemNo;
            }

            if (model.ItemNo != null)
            {
                parameters[2].Value = model.ItemNo;
            }

            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string PItemNo, string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GroupItem ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@PItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = PItemNo;
            parameters[1].Value = ItemNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }
        public int Delete(ZhiFang.Model.GroupItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GroupItem where 1=1 ");
            if (model.PItemNo != null)
            {
                strSql.Append(" and PItemNo='" + model.PItemNo + "' ");
            }
            if (model.ItemNo != null)
            {
                if (flag == "0")
                    strSql.Append(" and ItemNo not in (" + model.ItemNo + ") ");
                else if (flag == "1")
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                else
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }
            return idb.ExecuteNonQuery(strSql.ToString());

        }
        public int DeleteAll()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GroupItem ");
            return idb.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.GroupItem GetModel(string PItemNo, string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, PItemNo, ItemNo  ");
            strSql.Append("  from GroupItem ");
            strSql.Append(" where PItemNo=@PItemNo and ItemNo=@ItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@PItemNo", SqlDbType.VarChar,50),
					new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = PItemNo;
            parameters[1].Value = ItemNo;


            ZhiFang.Model.GroupItem model = new ZhiFang.Model.GroupItem();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.PItemNo = ds.Tables[0].Rows[0]["PItemNo"].ToString();
                model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();

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
            strSql.Append(" FROM GroupItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
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
            strSql.Append(" FROM GroupItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM GroupItem  INNER JOIN   dbo.TestItem ON dbo.GroupItem.ItemNo =  dbo.TestItem.ItemNo   where 1=1 ");

            if (model.PItemNo != null)
            {
                strSql.Append(" and GroupItem.PItemNo='" + model.PItemNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and GroupItem.ItemNo='" + model.ItemNo + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode from GroupItem where 1=1 ");
            //if (dTimeStampe != -999999)
            //{
            //    strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            //}
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select * from B_Lab_GroupItem where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            //if (dTimeStampe != -999999)
            //{
            //    strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            //}
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            //StringBuilder strSql3 = new StringBuilder();
            //strSql3.Append("select * from GroupItemControl where 1=1 ");
            //if (LabCode.Trim() != "")
            //{
            //    strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            //}
            //if (dTimeStampe != -999999)
            //{
            //    strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            //}
            //DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            //dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            //dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion

        #region IDBatchCopy 成员

        //public bool CopyToLab(List<string> lst)
        //{
        //    ///////////////////////
        //    try
        //    {
        //        System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
        //        StringBuilder strSql = new StringBuilder();
        //        if (lst.Count > 0)
        //        {
        //            if (lst[0].Trim() == "CopyToLab_LabFirstSelect")
        //            {
        //                //项目选择性批量复制到客户端
        //                if (lst.Count == 1)
        //                    return true;

        //                for (int i = 1; i < lst.Count; i++)
        //                {
        //                    strSql.Append("insert into B_Lab_GroupItem (PItemNo,ItemNo,LabCode) ");
        //                    strSql.Append(" select CONVERT(decimal(50),PItemNo),CONVERT(decimal(50),ItemNo),'" + lst[i].Trim().Split('|')[0].Trim() + "' as LabCode from GroupItem where PItemNo in " + lst[i].Trim().Split('|')[1].Trim() + " ");

        //                    arrySql.Add(strSql.ToString());
        //                    strSql = new StringBuilder();
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; i < lst.Count; i++)
        //                {
        //                    string strExistItemNos = this.GetExistItemNos(lst[i].Trim());
        //                    strSql.Append("insert into B_Lab_GroupItem (PItemNo,ItemNo,LabCode) ");
        //                    strSql.Append(" select CONVERT(decimal(50),PItemNo),CONVERT(decimal(50),ItemNo),'" + lst[i].Trim() + "' as LabCode from GroupItem ");
        //                    if (strExistItemNos.Trim() != "")
        //                        strSql.Append(" where PItemNo not in (" + strExistItemNos + ")");

        //                    arrySql.Add(strSql.ToString());
        //                    strSql = new StringBuilder();
        //                }
        //            }
        //        }

        //        idb.BatchUpdateWithTransaction(arrySql);
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.GropuItem.CopyToLab异常 ", ex);
        //        return false;
        //    }
        //}

        /// <summary>
        /// ganwh edit 2015-4-23
        /// </summary>
        /// <param name="strLabCode"></param>
        /// <returns></returns>
        public bool CopyToLab(List<string> lst)
        {
            try
            {
                System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
                StringBuilder strSql = new StringBuilder();
              
                if (lst.Count > 0)
                {
                    if (lst[0].Trim() == "CopyToLab_LabFirstSelect")
                    {
                        //项目选择性批量复制到实验室
                        if (lst.Count == 1)
                            return true;
                       
                        for (int i = 1; i < lst.Count; i++)
                        {
                            string[] labcodeAndItemno = lst[i].Split('|');
                            string labcode = labcodeAndItemno[0];
                            string listItemno = labcodeAndItemno[1].Substring(1, labcodeAndItemno[1].Length - 2);
                            string[] arryItemno = listItemno.Split(',');
                            for (int j = 0; j < arryItemno.Length; j++)
                            {
                                DataSet ds = idb.ExecuteDataSet("select * from GroupItem where PItemNo =" + arryItemno[j]);
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    for(int k=0;k<ds.Tables[0].Rows.Count;k++)
                                    {
                                        if (!this.Exists(labcode, arryItemno[j], ds.Tables[0].Rows[k]["ItemNo"].ToString()))
                                        {
                                            strSql.Append("insert into B_Lab_GroupItem (PItemNo,ItemNo,LabCode) ");
                                            strSql.Append("values(" + arryItemno[j] + ",'" + ds.Tables[0].Rows[k]["ItemNo"].ToString() + "','" + labcode+"')");
                                            arrySql.Add(strSql.ToString());
                                            strSql = new StringBuilder();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else//批量复制
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            string[] arryLabs = lst[i].Split(',');
                            for (int j = 0; j < arryLabs.Length; j++) 
                            {
                                string strExistItemNos = this.GetExistItemNos(arryLabs[j].Trim());
                                strSql.Append("insert into B_Lab_GroupItem (PItemNo,ItemNo,LabCode) ");
                                strSql.Append(" select CONVERT(decimal(50),PItemNo),CONVERT(decimal(50),ItemNo),'" + arryLabs[j].Trim() + "' as LabCode from GroupItem ");
                                if (strExistItemNos.Trim() != "")
                                    strSql.Append(" where PItemNo not in (" + strExistItemNos + ")");

                                arrySql.Add(strSql.ToString());
                                strSql = new StringBuilder();
                            }
                            
                        }
                    }
                }

                idb.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.GropuItem.CopyToLab异常 ", ex);
                return false;
            }
        }
        public string GetExistItemNos(string strLabCode)
        {
            string str = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct PItemNo from B_Lab_GroupItem where LabCode='" + strLabCode+"'");
            DataSet ds = idb.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["PItemNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["PItemNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }

        #endregion

        public bool Exists(string PItemNo, string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GroupItem ");
            strSql.Append(" where PItemNo ='" + PItemNo + "' and ItemNo='" + ItemNo + "'");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ganwh add 2015-4-23
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public bool Exists(string labcode, string PItemNo, string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_GroupItem ");
            strSql.Append(" where PItemNo =" + PItemNo + " and ItemNo='" + ItemNo + "' and LabCode='"+labcode+"'");
            return idb.Exists(strSql.ToString());
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GroupItem where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = idb.ExecuteScalar(strSql.ToString());
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

        #region IDataBase<GroupItem> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.GroupItem t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM GroupItem");
            return Convert.ToInt32(idb.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.GroupItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM GroupItem where 1=1 ");
            if (model.PItemNo != null)
            {
                strSql.Append(" and PItemNo='" + model.PItemNo + "' ");
            }

            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }
            return Convert.ToInt32(idb.ExecuteScalar(strSql.ToString()));
        }

        #endregion

        #region IDataBase<GroupItem> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim(), ds.Tables[0].Rows[i]["PItemNo"].ToString().Trim()))
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
                strSql.Append("insert into GroupItem (");
                strSql.Append("PItemNo,ItemNo,AddTime");
                strSql.Append(") values (");
                strSql.Append("'" + dr["PItemNo"].ToString().Trim() + "','" + dr["ItemNo"].ToString().Trim() + "','" + dr["AddTime"].ToString().Trim() + "'");
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
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
                strSql.Append("update GroupItem set ");

                strSql.Append(" where PItemNo='" + dr["PItemNo"].ToString().Trim() + "' and ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


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

