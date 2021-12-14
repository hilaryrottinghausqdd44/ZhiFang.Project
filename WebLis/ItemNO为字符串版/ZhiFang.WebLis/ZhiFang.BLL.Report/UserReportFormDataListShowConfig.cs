using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using ZhiFang.IBLL.Report;

namespace ZhiFang.BLL.Report
{
    public class UserReportFormDataListShowConfig : IBUserReportFormDataListShowConfig
    {
        public SortedList<string, string[]> ShowReportFormListHeadName(string PageName, int Sort)
        {
            SortedList<string, string[]> sl = new SortedList<string, string[]>();

            DataSet ds = new DataSet();
            ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("PrintDataListConfig")));
            
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               try
                {
                    DataRow[] dra = ds.Tables[0].Select(" PageName ='" + PageName + "'");
                    if (dra.Length > 0)
                    {
                        string[] Columns = dra[Sort]["Columns"].ToString().Trim().Split(',');
                        string[] ColumnsName = dra[Sort]["ColumnsName"].ToString().Trim().Split(',');
                        string[] ColumnsWidth = dra[Sort]["ColumnsWidth"].ToString().Trim().Split(',');
                        string[] ColumnsAlign = dra[Sort]["ColumnsAlign"].ToString().Trim().Split(',');
                        for (int i = 0; i < Columns.Length; i++)
                        {
                            string[] tmpht = new string[5];
                            ArrayList tmpidflag = new ArrayList();
                            try
                            {
                                tmpht[0] = ColumnsName[i];
                                tmpht[1] = ColumnsWidth[i];
                                tmpht[2] = Columns[i];
                                tmpht[3] = ColumnsAlign[i];
                                tmpht[4] = dra[Sort]["CheckBoxFlag"].ToString().Trim();
                            }
                            catch
                            {

                            }

                            sl.Add(i.ToString(), tmpht);
                        }
                    }
                }
                catch
                {
 
                }
            }
            else
            {
                
            }
            return sl;
        }
        public SortedList<string, string[]> ShowReportFormListHeadName(string PageName)
        {
            return ShowReportFormListHeadName(PageName, 0);
        }
        public DataSet ShowClassList(string ConfigSetName)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("PrintDataListConfig")));
                //ds.ReadXml(p.Server.MapPath(Common.ConfigHelper.GetConfigString("LocalhostURL") + Common.ConfigHelper.GetConfigString(ConfigSetName)));

                return ds;
            }
            catch
            {
                return new DataSet();
            }
        }
        public int ShowReportFormListPageSize(string PageName, int Sort)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("PrintDataListConfig")));

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        DataRow[] dra = ds.Tables[0].Select(" PageName ='" + PageName + "'");
                        if (dra.Length > 0)
                        {
                            return Convert.ToInt32(dra[Sort]["PageSize"].ToString().Trim());
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        public string ShowReportFormListOrderColumn(string PageName, int Sort)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("PrintDataListConfig")));

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        DataRow[] dra = ds.Tables[0].Select(" PageName ='" + PageName + "'");
                        if (dra.Length > 0)
                        {
                            try
                            {
                                if (dra[Sort]["DefaultOrder"].ToString().Trim().Length > 0)
                                {
                                    return dra[Sort]["DefaultOrder"].ToString().Trim();
                                }
                                else
                                {
                                    return dra[0]["Columns"].ToString().Trim().Split(',')[0].Trim();
                                }
                            }
                            catch
                            {
                                return dra[0]["Columns"].ToString().Trim().Split(',')[0].Trim();
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                    catch
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public DataSet ShowFormTypeList(string ConfigSetName)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XmlConfig")));                
                return ds;
            }
            catch
            {
                return new DataSet();
            }
        }
        
    }
}
