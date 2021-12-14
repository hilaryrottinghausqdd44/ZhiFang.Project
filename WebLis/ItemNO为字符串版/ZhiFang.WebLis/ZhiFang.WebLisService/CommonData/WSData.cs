using System.Data;
using System.Xml;
using DBUtility;
using System;
using ECDS.Common.Collections;
using ECDS.Common.SysInfo;
using ECDS.DALFactory;
using System.Collections;
using ECDS.Common;
using System.Data.SqlClient;
using System.IO;
using ECDS.Common.Security;

    public class WSData
    {
        #region 私有变量

        //调用事例SignFromCenterLis("BarCode", "X1909150002", "WebLisSourceOrgID='001'")

        private IDBConnection lisdb;      //兰卫数据库连接
        IDBConnection hisdb;
        DataTable dsLisBarCode = new DataTable();     //兰卫条码单表记录      
        DataTable dtLisNRForm = new DataTable();     //兰卫申请单表记录      
        DataSet NRForm = new DataSet();          //长宁申请单表记录
        public string BarCodeFormNo = "";
        string SickTypeName = "";
        string NRequestFormNo = "";

        #endregion

        #region 构造函数

        public WSData()
        {
            lisdb = LIS.DataConn.CreateLisDB();
            Log.Info(String.Format("字符串连接", lisdb.ToString()));
            BarCodeFormNo = GetNo(3).ToString();   //初始化条码单号
            Log.Info(String.Format("", BarCodeFormNo));
            NRequestFormNo = GetNo(2).ToString();   //初始化申请单号
            Log.Info(String.Format("", NRequestFormNo));
        }

        #endregion

        #region 保存申请单，项目，条码号等数据集

        /// <summary>
        /// 保存获取到的申请单项目条码数据
        /// </summary>
        /// <param name="BarCodeNo">条码号</param>
        /// <param name="wsBarCode">条码数据集</param>
        /// <param name="wsNRequestItem">项目数据集</param>
        /// <param name="wsNRequestForm">申请单数据集</param>
        /// <returns>0 成功 -1失败</returns>
        public int SaveWebLisData(string BarCodeNo, DataSet wsBarCode, DataSet wsNRequestItem, DataSet wsNRequestForm)
        {
            //====================================
            //独立实验室中条码
            DataSet lisBarCode = lisdb.ExecDS(GetSql("barcode", BarCodeNo, "", "barcode", true));

            //独立实验室中申请单              
            DataSet lisNRequestForm = lisdb.ExecDS(GetSql("barcode", BarCodeNo, "", "form", true));

            //独立实验室中项目
            DataSet dtLisNRItem = lisdb.ExecDS(GetSql("barcode", BarCodeNo, "", "item", true));

            ArrayList arr = new ArrayList();

            if (ECDS.Common.Security.FormatTools.CheckDataSet(lisBarCode))
            {
                //更新条码表

                string strSql = CreateUpdateSql(wsBarCode.Tables[0].Rows[0], lisBarCode.Tables[0].Rows[0], "BarCodeForm");
                arr.Add(strSql);
                
            }
            else
            {
                //插入条码表
                foreach (DataRow dr in wsBarCode.Tables[0].Rows)
                {
                    string strSql = CreatInsertSql(dr, lisBarCode.Tables[0].Columns, "BarCodeForm");
                    arr.Add(strSql);
                }
            }

            if (ECDS.Common.Security.FormatTools.CheckDataSet(lisNRequestForm))
            {
                //更新申请单
                string strSql = CreateUpdateSql(wsNRequestForm.Tables[0].Rows[0], lisNRequestForm.Tables[0].Rows[0], "NRequestForm");
                arr.Add(strSql);
            }
            else
            {
                //插入申请单
                foreach (DataRow dr in wsNRequestForm.Tables[0].Rows)
                {
                    string strSql = CreatInsertSql(dr, lisNRequestForm.Tables[0].Columns, "NRequestForm");
                    arr.Add(strSql);
                }
            }

            if (ECDS.Common.Security.FormatTools.CheckDataSet(dtLisNRItem))
            {
                //更新项目
            }
            else
            {
                //插入项目
                foreach (DataRow dr in wsNRequestItem.Tables[0].Rows)
                {
                    string strSql = CreatInsertSql(dr, dtLisNRItem.Tables[0].Columns, "NRequestItem");
                    arr.Add(strSql);
                }
            }

            lisdb.BatchUpdateWithoutTransaction(arr);

            for (int i = 0; i < arr.Count; i++)
            {
                Log.Info("执行语句" + arr[i].ToString());
            }

            return 0;

        }

        #endregion

        #region 工具函数

        /// <summary>
        /// 生成查询语句
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="fieldvalue"></param>
        /// <param name="whereclause"></param>
        /// <param name="type"></param>
        /// <param name="flag">true 兰卫false长宁</param>
        /// <returns></returns>
        private string GetSql(string fieldname, string fieldvalue, string whereclause, string type, bool flag)
        {
            //
            string sql = "";
            string sqlwhere = "";
            if (whereclause.Length > 0)
            {
                sqlwhere = " and " + whereclause;
            }
            switch (type)
            {
                case "barcode":
                    if (flag)
                    {
                        sql = "select * from BarCodeForm where " + fieldname + "='" + fieldvalue + "'";
                    }
                    else
                    {
                        sql = "select * from BarCodeForm where " + fieldname + "='" + fieldvalue + "'" + sqlwhere;
                    }
                    break;
                case "form":
                    if (flag)
                    {
                        sql = "select * from NRequestForm where exists(select distinct NRequestFormNo from NRequestItem " +
                              " where NRequestFormNo=NRequestForm.NRequestFormNo and exists(select barcodeformno from barcodeform where BarCodeFormNo=NRequestItem.BarCodeFormNo and " + fieldname + "='" + fieldvalue + "'))";
                    }
                    else
                    {
                        sql = "select * from NRequestForm where exists(select distinct NRequestFormNo from NRequestItem " +
                           " where NRequestFormNo=NRequestForm.NRequestFormNo and exists(select barcodeformno from barcodeform where BarCodeFormNo=NRequestItem.BarCodeFormNo and " + fieldname + "='" + fieldvalue + "'" + sqlwhere + "))";
                    }
                    break;
                case "item":
                    if (flag)
                    {
                        sql = "select * from NRequestItem where exists(select barcodeformno from barcodeform " +
                          " where BarCodeFormNo=NRequestItem.BarCodeFormNo and " + fieldname + "='" + fieldvalue + "')";
                    }
                    else
                    {
                        sql = "select * from NRequestItem where exists(select barcodeformno from barcodeform " +
                              " where BarCodeFormNo=NRequestItem.BarCodeFormNo and " + fieldname + "='" + fieldvalue + "'" + sqlwhere + ")";
                    }
                    break;
            }
            return sql;
        }



        /// <summary>
        /// 得到条码号或申请单号 2申请单 3条码号
        /// </summary>
        /// <param name="type">2申请单 3条码号</param>
        /// <returns></returns>
        private int GetNo(int type)
        {
            try
            {
                SqlParameter[] sp = new SqlParameter[2];
                SqlParameter p1 = new SqlParameter("@GetNoType", SqlDbType.VarChar, 2);
                p1.Direction = ParameterDirection.Input;
                p1.Value = type.ToString();

                SqlParameter p2 = new SqlParameter("@SN", SqlDbType.VarChar, 20);
                p2.Direction = ParameterDirection.Output;

                sp[0] = p1;
                sp[1] = p2;
                int i = LIS.DataConn.CreateLisDB().ExecStoredProcedure("P_GetMaxFormNo", sp);
                if (i < 0)
                {
                    return 0;
                }
                return Convert.ToInt32(p2.Value);
            }
            catch { throw; }
        }

        #endregion

        #region 签收打标志

        /// <summary>
        /// 打签收标志
        /// </summary>
        /// <param name="BarCode">条码号</param>
        /// <param name="ISSignData">true 签收标志 false 取消签收</param>
        /// <returns>0成功 -1失败</returns>
        public int SetSingDataFlag(string BarCode, string Clientno, bool ISSignData)
        {
            int flag = 0;
            if (ISSignData)
            {
                flag = 5;
            }
            else
            {
                flag = 0;
            }

            string strsql = String.Format("update barcodeform set WebLisFlag={0} where 2>1 ", flag);
            if (Clientno != null && Clientno != "")
            {
                strsql += String.Format("and WebLisSourceOrgID = '{0}'", Clientno);
            }
            if (BarCode != null && BarCode != "")
            {
                strsql += String.Format("and barcode = '{0}'", BarCode);
            }

            try
            {
                hisdb.ExecuteNonQuery(strsql);
            }
            catch
            {
                return -1;
            }
            Log.Info("签收打标志：" + strsql);
            return 0;
        }

        /// <summary>
        /// 打审定报告标志
        /// </summary>
        /// <param name="BarCode">条码号</param>
        /// <param name="ISSignData">true 审定标志 false 反审定</param>
        /// <returns>0成功 -1失败</returns>
        public int SetSendReportFlag(string SourceOrgID, string BarCode, bool ISSignData, out string ReturnDescription)
        {
            int flag = 0;
            ReturnDescription = "";
            string strsql = "";
            if (ISSignData)
            {
                flag = 9;

            }
            else
            {
                flag = 0;
            }
            try
            {
                strsql = String.Format("update barcodeform set WebLisFlag={0} where WebLisSourceOrgID = '{1}' and barcode = '{2}'", flag, SourceOrgID, BarCode);
            }
            catch
            {
                return -1;
            }

            hisdb.ExecuteNonQuery(strsql);
            Log.Info("审定报告打标志：" + strsql);
            return 0;
        }

        #endregion

        #region 数据库更新逻辑

        /// <summary>
        ///  构造主键条件
        /// </summary>
        /// <param name="DataTypeName">字段类型</param>
        /// <param name="FieldName">字段名称</param>
        /// <returns></returns>
        private string CreateSql(string DataTypeName, string FieldName)
        {
            string strwhere = "";
            switch (DataTypeName.ToUpper())
            {
                case "INT32":
                case "FLOAT":
                case "MONEY":
                case "DECIMAL":
                    strwhere += FieldName + "={#}";
                    break;
                default:
                    strwhere += FieldName + "='{#}'";
                    break;
            }
            return strwhere;
        }

        /// <summary>
        /// 构造字段表的主键组成的查询条件
        /// </summary>
        /// <param name="dr">记录</param>
        /// <param name="dc">主键数组</param>
        /// <returns></returns>
        private string CreateWhere(DataRow dr, DataColumn[] dc)
        {
            //组织条件，格式为where ItemNo={0} and CName='{1}' 
            string strwhere = " where 2>1 ";
            int i = 0;
            foreach (DataColumn dcn in dc)
            {
                string temp = "";
                string DataTypeName = dcn.DataType.Name;
                string FieldName = dcn.ColumnName;
                temp = CreateSql(DataTypeName, FieldName);
                temp = temp.Replace("{#}", dr[FieldName].ToString());
                temp = " and " + temp;
                strwhere += temp;
                i++;
            }
            return strwhere;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr">列集合</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        private string CreatInsertSql(DataRow dr, DataColumnCollection dcc, string TableName)
        {
            string strsql = " insert into " + TableName + " ";

            string fields = "";
            string values = "";
            foreach (DataColumn dc in dcc)
            {                
                if (dr.Table.Columns.Contains(dc.ColumnName) && dr[dc.ColumnName]!=null)
                {
                    fields += "," + dc.ColumnName;
                    if (dc.ColumnName.ToLower() == "operdate")
                    {
                        values += "," + "'" + DateTime.Now.ToString("yyyy-MM-dd hh:ss:mm") + "'";
                    }
                    else
                    {
                        values += "," + CreateValues(dc.DataType.ToString(), dr[dc.ColumnName].ToString().Trim());
                    }
                }
            }
            fields = fields.Substring(1);

            values = values.Substring(1);

            strsql = strsql + "(" + fields + ")values(" + values + ")";

            return strsql;
        }

        /// <summary>
        ///  构造主键条件
        /// </summary>
        /// <param name="DataTypeName">字段类型</param>
        /// <param name="FieldName">字段名称</param>
        /// <returns></returns>
        private string CreateValues(string DataTypeName, string FieldValue)
        {
            string Value = "";
            switch (DataTypeName)
            {
                case "System.Int32":
                case "System.Double":
                case "System.Decimal":
                    if (FieldValue == "")
                    {
                        Value += "null";
                    }
                    else
                    {
                        Value += FieldValue;
                    }
                    break;
                case "SYSTEM.BYTE[]":
                    break;
                case "System.DateTime":
                    if (FieldValue == "")
                    {
                        Value += "null";
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(FieldValue);
                        Value += "'" + dt.ToString("yyyy-MM-dd hh:ss:mm") + "'";
                    }
                    
                    break;
                case "System.String":
                    Value += "'" + FieldValue + "'";
                    break;
                default:
                    Value += "'" + FieldValue + "'";
                    break;
            }
            return Value;
        }

        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="wsdr">weblis列</param>
        /// <param name="lisdr">lis列</param>
        /// <param name="TableName">更新的表名</param>
        /// <returns></returns>
        private string CreateUpdateSql(DataRow wsdr, DataRow lisdr, string TableName)
        {
            string strsql = " update " + TableName + " set ";
            string strwhere = CreateWhere(lisdr, lisdr.Table.PrimaryKey);

            string strupdate = "";
            foreach (DataColumn dc in lisdr.Table.Columns)
            {
                if (wsdr.Table.Columns.Contains(dc.ColumnName) && wsdr[dc.ColumnName] != null)  //只更新核收过来的字段
                {
                    if (dc.ColumnName == "BarCodeFormNo")   //主键列跳过
                    {
                        this.BarCodeFormNo = lisdr["BarCodeFormNo"].ToString();
                        continue;
                    }
                    if (dc.ColumnName == "NRequestFormNo")   //主键列跳过
                    {
                        this.NRequestFormNo = lisdr["NRequestFormNo"].ToString();
                        continue;
                    }
                    strupdate += "," + dc.ColumnName + "=" + CreateValues(dc.DataType.ToString(), wsdr[dc.ColumnName].ToString());
                }
            }
            strupdate = strupdate.Substring(1);

            string strTemp = strsql + strupdate + strwhere;

            return strTemp;
        }



        #endregion

        #region 更新数据集

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wsBarCode"></param>
        public void UpdateBarCode(DataSet wsBarCode, string WebLisSourceOrgId, string WebLisOrgID)
        {
            Log.Info(String.Format("申请单数据{0}条", wsBarCode.Tables[0].Rows.Count));
            Log.Info(String.Format("送检单位ID", WebLisSourceOrgId));
            if (!wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgId"))
            {
                wsBarCode.Tables[0].Columns.Add("WebLisSourceOrgId");
            }
            if (!wsBarCode.Tables[0].Columns.Contains("WebLisOrgID"))
            {
                wsBarCode.Tables[0].Columns.Add("WebLisOrgID");
            }
            if (wsBarCode.Tables[0].Columns.Contains("BarCodeFormNo"))
            {
                wsBarCode.Tables[0].Columns.Add("BarCodeFormNo");
            }

            for (int i = 0; i < wsBarCode.Tables[0].Rows.Count; i++)
            {
                wsBarCode.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;
                wsBarCode.Tables[0].Rows[i]["WebLisOrgID"] = WebLisOrgID;
                wsBarCode.Tables[0].Rows[i]["BarCodeFormNo"] = BarCodeFormNo;

                //if (wsBarCode.Tables[0].Columns.Contains("BarCodeFormNo"))
                //{
                //    wsBarCode.Tables[0].Rows[i]["BarCodeFormNo"] = BarCodeFormNo;
                //}

                //if (wsBarCode.Tables[0].Columns.Contains("BarCodeSource"))
                //{
                //    wsBarCode.Tables[0].Rows[i]["BarCodeSource"] = 4;
                //}
                //else
                //{
                //    wsBarCode.Tables[0].Columns.Add("BarCodeSource");
                //    wsBarCode.Tables[0].Rows[i]["BarCodeSource"] = 4;
                //}

                //if (wsBarCode.Tables[0].Columns.Contains("IsAffirm"))
                //{
                //    wsBarCode.Tables[0].Rows[i]["IsAffirm"] = 1;
                //}

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wsNRequestForm"></param>
        public void UpdateNRequestForm(DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID)
        {

            if (!wsNRequestForm.Tables[0].Columns.Contains("NRequestFormNo"))
            {
                wsNRequestForm.Tables[0].Columns.Add("NRequestFormNo");
            }
            if (!wsNRequestForm.Tables[0].Columns.Contains("WebLisSourceOrgId"))
            {
                wsNRequestForm.Tables[0].Columns.Add("WebLisSourceOrgId");
            }
            if (!wsNRequestForm.Tables[0].Columns.Contains("ClientNo"))
            {
                wsNRequestForm.Tables[0].Columns.Add("ClientNo");
            }



            for (int i = 0; i < wsNRequestForm.Tables[0].Rows.Count; i++)
            {
                wsNRequestForm.Tables[0].Rows[i]["NRequestFormNo"] = NRequestFormNo;
                wsNRequestForm.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;
                wsNRequestForm.Tables[0].Rows[i]["ClientNo"] = WebLisSourceOrgId;

                if (wsNRequestForm.Tables[0].Rows[i]["jztype"].ToString() == "")
                {
                    wsNRequestForm.Tables[0].Rows[i]["jztype"] = wsNRequestForm.Tables[0].Rows[i]["sicktypeno"].ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wsNRequestItem"></param>
        public void UpdateNRequestItem(DataSet wsNRequestItem, DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID)
        {
            if (wsNRequestItem.Tables[0].Columns.Contains("nrequestitemno"))
            {
                wsNRequestItem.Tables[0].Columns.Remove("nrequestitemno");
            }

            if (!wsNRequestItem.Tables[0].Columns.Contains("WebLisSourceOrgId"))
            {
                wsNRequestItem.Tables[0].Columns.Add("WebLisSourceOrgId");
            }

            if (!wsNRequestItem.Tables[0].Columns.Contains("NRequestFormNo"))
            {
                wsNRequestItem.Tables[0].Columns.Add("NRequestFormNo");
            }

            if (!wsNRequestItem.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                DataColumn dc = new DataColumn("SampleTypeNo",DbType.Int32.GetType());
                wsNRequestItem.Tables[0].Columns.Add(dc);
            }

            for (int i = 0; i < wsNRequestItem.Tables[0].Rows.Count; i++)
            {
                if (wsNRequestItem.Tables[0].Columns.Contains("NRequestFormNo"))
                {
                    wsNRequestItem.Tables[0].Rows[i]["NRequestFormNo"] = NRequestFormNo;
                }

                if (wsNRequestItem.Tables[0].Columns.Contains("BarCodeFormNo"))
                {
                    wsNRequestItem.Tables[0].Rows[i]["BarCodeFormNo"] = BarCodeFormNo;
                }

                wsNRequestItem.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;

                wsNRequestItem.Tables[0].Rows[i]["SampleTypeNo"] = wsNRequestForm.Tables[0].Rows[0]["SampleTypeNo"];
            }
        }

        #endregion
    }

