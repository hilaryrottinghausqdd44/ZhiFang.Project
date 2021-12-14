using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class RBACRowFilterDao : Base.BaseDaoNHBService<RBACRowFilter, long>, IDRBACRowFilterDao
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
        /// <summary>
        /// 行数据条件需要重写,以防在修改保存时相关的宏命令会被替换为具体的值
        /// </summary>
        /// <param name="hql"></param>
        /// <returns></returns>
        public override int UpdateByHql(string hql)
        {
            //ZhiFang.LabStar.Common.LogHelp.Debug("UpdateByHql:" + hql);
            var result = this.HibernateTemplate.Execute<int>(new Base.DaoNHBHqlAction(hql));
            return result;
        }
    }
}