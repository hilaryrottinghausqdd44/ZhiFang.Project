using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using ZhiFang.IDAL;
using ZhiFang.Common.Log;

namespace ZhiFang.DAL.Oracle
{
    public class BaseDALLisDB : IDBaseDALLisDB
    {
        public DBUtility.IDBConnection DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        public int getInsertSQL(string tableName, Hashtable hashColumn, Hashtable hashContent)
        {
            //Log.Info("这是getInsertSQL()方法：ZhiFang.DAL.Oracle.getInsertSQL");
            //return 0;
            string sql = "";
            string insertModal = "INSERT INTO  {0} ({1}) VALUES({2})";
            string insertFieldNameSQL = "";
            string insertFieldValueSQL = "";
            int clientcount = 0;
            int itemrannumcount = 0;
            int itemrannum = 1000;
            //遍历字段内容
            System.Collections.IDictionaryEnumerator myEnumerator = hashContent.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string fieldName = myEnumerator.Key.ToString().ToUpper();
                if (hashColumn[fieldName] == null)
                {
                    string msg = "表 " + tableName + " 中不存在字段 " + fieldName + "!该字段内容将不被导入!";
                    Log.Info(msg);
                    continue;
                }
                string fieldValue = myEnumerator.Value.ToString();

                #region Clientno即为weblissourceorgid 服务端进行逻辑处理

                if (tableName == "ReportFormFull")
                {
                    if (hashContent["CLIENTNO"] == null && clientcount == 0)
                    {
                        clientcount = clientcount + 1;
                        if (insertFieldNameSQL != "")
                            insertFieldNameSQL += ",";
                        insertFieldNameSQL += "CLIENTNO";
                        if (insertFieldValueSQL != "")
                            insertFieldValueSQL += ",";
                        //对内容进行转义(如&lt;为<等)
                        string fieldValue_orgid = ZhiFang.Tools.Tools.convertESCToHtml(hashContent["WEBLISSOURCEORGID"].ToString());
                        insertFieldValueSQL += "'" + fieldValue_orgid + "'";

                        //CLIENTNO = WEBLISSOURCEORGID
                    }
                }
                #endregion

              

                if (fieldValue == "")//没有内容,不生成SQL脚本
                    continue;
                bool datetimeflag = false;
                if (insertFieldNameSQL != "")
                {
                    insertFieldNameSQL += ",";
                }
                if (fieldName.ToUpper().IndexOf("DATE") > 0 || (fieldName.ToUpper().IndexOf("TIME") > 0 && fieldName.ToUpper().IndexOf("TIMES") < 0) || fieldName.ToUpper().IndexOf("DATETIME") > 0 || fieldName.Trim().ToUpper() == "BIRTHDAY")
                {
                    datetimeflag = true;
                }
                insertFieldNameSQL += fieldName;
                if (insertFieldValueSQL != "")
                {
                    insertFieldValueSQL += ",";
                }
                //对内容进行转义(如&lt;为<等)
                fieldValue = ZhiFang.Tools.Tools.convertESCToHtml(fieldValue);
                if (datetimeflag)
                {
                    insertFieldValueSQL += "to_date('" + fieldValue + "','YYYY-MM-DD HH24:MI:ss')";
                }
                else
                {
                    insertFieldValueSQL += "'" + fieldValue + "'";
                }
            }
            if (insertFieldNameSQL != "")
            {
                //插入主表数据
                sql = string.Format(insertModal, tableName, insertFieldNameSQL, insertFieldValueSQL);
                ZhiFang.Common.Log.Log.Info("插入语句：" + tableName + sql);
            }
            ZhiFang.DAL.Oracle.weblis.ReportFormFull dalrff = new ZhiFang.DAL.Oracle.weblis.ReportFormFull(ZhiFang.Common.Dictionary.DBSource.LisDB());
            int count = dalrff.InsertSql(sql);
            return count;

        }
    }
}
