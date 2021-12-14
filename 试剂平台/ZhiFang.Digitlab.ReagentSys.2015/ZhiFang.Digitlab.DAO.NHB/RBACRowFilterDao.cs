using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class RBACRowFilterDao : BaseDaoNHB<RBACRowFilter, long>, IDRBACRowFilterDao
	{
        //public override bool Update(string[] strParas)
        //{
        //    string strEntityName = "RBACRowFilter";
        //    string strEntityNameLower = "RBACRowFilter".ToLower();
        //    string strWhere = "";
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("update " + strEntityName + " " + strEntityNameLower + " set ");
        //    foreach (string s in strParas)
        //    {
        //        if (s.Trim().IndexOf("Id=") >= 0 && s.Trim().IndexOf(".Id=") < 0)
        //        {
        //            if (s.Trim().Split('=')[0].Trim() == "Id")
        //                strWhere = s;
        //            else
        //                sb.Append(strEntityNameLower + "." + s + ", ");
        //        }
        //        else
        //        {
        //            if (s.Trim().IndexOf("DataTimeStamp=") >= 0 || s.Trim().IndexOf(".DataTimeStamp=") >= 0)
        //            {

        //            }
        //            else
        //            {
        //                if (s.Count(c => c == '\'') > 2)
        //                {
        //                    string tmphql = s.Substring(s.IndexOf('\'') + 1, s.LastIndexOf('\'') - s.IndexOf('\'') - 1);
        //                    tmphql = tmphql.Replace("'", "''");
        //                    tmphql = s.Substring(0, s.IndexOf('\'') + 1) + tmphql + s.Substring(s.LastIndexOf('\''), s.Length - s.LastIndexOf('\''));
        //                    sb.Append(strEntityNameLower + "." + tmphql + ", ");
        //                }
        //                else
        //                {
        //                    sb.Append(strEntityNameLower + "." + s + ", ");
        //                }
        //            }
        //        }
        //    }
        //    int n = sb.ToString().LastIndexOf(",");
        //    sb.Remove(n, 1);
        //    sb.Append("where " + strEntityNameLower + "." + strWhere);
        //    int row = this.UpdateByHql(sb.ToString());
        //    if (row > 0)
        //        return true;
        //    else
        //        return false;
        //}
	} 
}