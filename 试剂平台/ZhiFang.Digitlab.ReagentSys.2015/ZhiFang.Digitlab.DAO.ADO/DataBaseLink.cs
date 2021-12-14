using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.DAO.ADO
{
    public class DataBaseLink
    {
        public static string MainDBConnectStr = ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString");
        public static Dictionary<string, string> DicDataBaseLink = new Dictionary<string, string>();

        public static BaseResultDataValue GetDataBaseLinkByOrgNo(string orgNo, string orgName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //if (DicDataBaseLink.ContainsKey(orgNo))
            //{
            //    brdv.ResultDataValue = DicDataBaseLink[orgNo];
            //}
            //else
            {
                //从配置文件中获取数据库连接
                brdv = _getDataBaseLinkByJsonConfig(orgNo, orgName);
                if (brdv.success && (!string.IsNullOrEmpty(brdv.ResultDataValue)))
                {
                    if (DicDataBaseLink.ContainsKey(orgNo))
                        DicDataBaseLink[orgNo] = brdv.ResultDataValue;
                    else
                        DicDataBaseLink.Add(orgNo, brdv.ResultDataValue);
                }
            }
            return brdv;
        }

        private static string _getConnectionString(DataRow dr)
        {
            string connectStr = "";
            if (dr["ServerName"] != null && (!string.IsNullOrEmpty(dr["ServerName"].ToString())))
                connectStr = "Server=" + dr["ServerName"] + ";";
            else
                return "";
            if (dr["DatabaseName"] != null && (!string.IsNullOrEmpty(dr["DatabaseName"].ToString())))
                connectStr += "database=" + dr["DatabaseName"] + ";";
            else
                return "";
            if (dr["UserName"] != null && (!string.IsNullOrEmpty(dr["UserName"].ToString())))
                connectStr += "uid=" + dr["UserName"] + ";";
            else
                return "";
            if (dr["Password"] != null && (!string.IsNullOrEmpty(dr["Password"].ToString())))
                connectStr += "pwd=" + dr["Password"] + ";";
            else
                return "";
            return connectStr;
        }

        private static BaseResultDataValue _getDataBaseLinkByJsonConfig(string orgNo, string orgName)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string connectStr = "";
            DataSet ds = SqlServerHelper.QuerySql("Select * from B_Parameter where ParaNo=\'LabADODBLinkInfo\'", MainDBConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ParaValue"] != null)
            {
                string jsonConfig = ds.Tables[0].Rows[0]["ParaValue"].ToString();
                DataTable dt = ZhiFang.Common.Public.JsonHelp.JsonConfigToDataTable(jsonConfig);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] rows = dt.Select(" OrgNo=\'" + orgNo + "\'");
                    if (rows != null && rows.Length > 0)
                    {
                        connectStr = _getConnectionString(rows[0]);
                        if (string.IsNullOrEmpty(connectStr))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "机构【" + orgName + "】的数据库连接配置信息为空！";
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "找不到机构【" + orgName + "】的数据库连接配置信息！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无数据库连接配置信息！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "机构【" + orgName + "】的数据库连接配置信息为空！";
            }
            if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            baseResultDataValue.ResultDataValue = connectStr;
            return baseResultDataValue;
        }
    }
}
