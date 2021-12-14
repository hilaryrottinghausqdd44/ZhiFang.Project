using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ZhiFang.DAL.MsSql.Weblis
{
    public partial class ItemMergeRule : BaseDALLisDB, IDItemMergeRule
    {
       public ItemMergeRule(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
       public ItemMergeRule()
		{
        }
		D_LogInfo d_log = new D_LogInfo();
        public DataSet GetList(Model.ItemMergeRule model)
        {

            StringBuilder strSql = new StringBuilder();
            if (model.MergeRuleName != null && model.MergeRuleName != "")
            {
                strSql.Append("select * from ItemMergeRule");
                strSql.Append(" where MergeRuleName='" + model.MergeRuleName + "'");
            }
            else
            {
                strSql.Append("select distinct MergeRuleName from ItemMergeRule");
            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ItemMerge ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }
        public DataSet GetListItem(Model.ItemMergeRule model) 
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select distinct ItemMerge.ItemNo,TestItem.CName from ItemMerge");
                strSql.Append(" INNER JOIN TestItem On ItemMerge.ItemNo=TestItem.ItemNo");
                //if (model.ClientNo != null)
                //{
                //    strSql.Append(" and dbo.ItemMerge.ClientNo='" + model.ClientNo + "' ");
                //}
                //if (model.SuperGroupNo != null)
                //{
                //    strSql.Append(" and dbo.ItemMerge.SuperGroupNo=" + model.SuperGroupNo + " ");
                //}
                if (model.ItemNo != null)
                {
                    strSql.Append(" and dbo.ItemMerge.ItemNo in (" + model.ItemNo + ")");
                }
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                return null;
            }
        }
        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        public bool Add(List<Model.ItemMergeRule> l)
        {
            try
            {
                ArrayList al = new ArrayList();
                if (l != null && l.Count > 0)
                {
                    foreach (Model.ItemMergeRule lcc_m in l)
                    {
                        if (lcc_m.MergeRuleName != null && lcc_m.ItemNo != null&&lcc_m.SectionNo!=null)
                        {
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("insert into ItemMergeRule(");
                            strSql.Append("MergeRuleName,ItemNo,SectionNo");
                            strSql.Append(") values (");
                            strSql.Append("'" + lcc_m.MergeRuleName + "'," + lcc_m.ItemNo + "," + lcc_m.SectionNo);
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
        public bool AddList(List<Model.ItemMergeRule> l)
        {
            try
            {
                ArrayList al = new ArrayList();
                al.Add(" delete from ItemMergeRule where MergeRuleName='" + l.ElementAt(0).itemMergeCName + "'");
                if (l != null && l.Count > 0)
                {
                    foreach (Model.ItemMergeRule lcc_m in l)
                    {
                        if (lcc_m.MergeRuleName != null && lcc_m.ItemNo != null && lcc_m.SectionNo != null)
                        {
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("insert into ItemMergeRule(");
                            strSql.Append("MergeRuleName,ItemNo,SectionNo");
                            strSql.Append(") values (");
                            strSql.Append("'" + lcc_m.MergeRuleName + "'," + lcc_m.ItemNo + "," + lcc_m.SectionNo);
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
        public int Add(Model.ItemMergeRule model)
        {
            throw new NotImplementedException();
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into ItemMerge(");
            //strSql.Append("ItemNo,ClientNo,SuperGroupNo");
            //strSql.Append(") values (");
            //strSql.Append("@ItemNo,@ClientNo,@SuperGroupNo");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@ItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4)             
              
            //};
            //parameters[0].Value = model.ItemNo;
            //parameters[1].Value = model.ClientNo;
            //parameters[2].Value = model.SuperGroupNo;
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        public int Update(Model.ItemMergeRule t)
        {
            throw new NotImplementedException();
        }
        public DataSet GetList(string SuperGroupNo,string ClientNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select Id, ItemNo from ItemMerge where 1=1");
            if (SuperGroupNo != "")
            {
                strSql.Append(" and SuperGroupNo='" + SuperGroupNo + "'");
            }
            if (ClientNo != "")
            {
                strSql.Append(" and ClientNo in (" + ClientNo + ")");
            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public System.Data.DataSet GetList(int Top, Model.ItemMergeRule t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(System.Data.DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ItemMergeRule t)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetListByPage(Model.ItemMergeRule model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                //string sql = "";
                //if (model.SuperGroupNoLikeKey != null && model.SuperGroupNoLikeKey.Trim() != "")
                //{
                //    sql += " and  (  SuperGroupNo.CNAME like '%" + model.SuperGroupNoLikeKey + "%'  or CLIENTELE.ENAME like '%" + model.ClienteleLikeKey + "%'  or CLIENTELE.SHORTCODE like '%" + model.ClienteleLikeKey + "%') ";
                //}
                //strSql.Append("select top " + nowPageSize + " dbo.CLIENTELE.* from  dbo.BusinessLogicClientControl INNER JOIN  dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo not in  ");
                //strSql.Append("(select top " + (nowPageSize * nowPageNum) + " BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo from  dbo.BusinessLogicClientControl INNER JOIN   dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where 1=2 " + sql + "  order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo)");
                //strSql.Append(sql);
                //strSql.Append(" order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo  ");
                //return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                //strSql.Append("select top " + nowPageSize + "  dbo.BusinessLogicClientControl.Account, dbo.CLIENTELE.*  from dbo.BusinessLogicClientControl INNER JOIN  dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO where BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo not in  ");
                //strSql.Append("(select top " + (nowPageSize * nowPageNum) + "BusinessLogicClientControl.Account+'_'+BusinessLogicClientControl.ClientNo from dbo.BusinessLogicClientControl INNER JOIN   dbo.CLIENTELE ON dbo.BusinessLogicClientControl.ClientNo = dbo.CLIENTELE.ClIENTNO order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo) order by BusinessLogicClientControl.Account,BusinessLogicClientControl.ClientNo  ");
                //return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            return null;
        }


        public DataSet GetItemNo(Model.ClientProfile ClientProfile, Model.ItemMergeRule ItemMergeRule)
        {
               StringBuilder strSql = new StringBuilder();
               strSql.Append("select ItemMergeRule.* from ItemMergeRule join ClientProfile on  ItemMergeRule.MergeRuleName= ClientProfile.MergeRuleName where 1=1");
               if (ClientProfile.ClientNo != null)
               {
                   strSql.Append(" and ClientProfile.ClientNo=" + ClientProfile.ClientNo);
               }
               if (ItemMergeRule.SectionNo != null)
               {
                   strSql.Append(" and ItemMergeRule.SectionNo=" + ItemMergeRule.SectionNo);
               }
               return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
    }
}
 