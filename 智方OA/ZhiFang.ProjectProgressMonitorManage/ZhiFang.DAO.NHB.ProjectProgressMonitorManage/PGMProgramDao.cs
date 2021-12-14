using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{	
	public class PGMProgramDao : BaseDaoNHB<PGMProgram, long>, IDPGMProgramDao
	{
        public bool UpdateCountsById(long id)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(id.ToString()))
            {
                string hql = "update PGMProgram pgmprogram set pgmprogram.Counts=(pgmprogram.Counts+1) where pgmprogram.Id=" + id;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;

        }
        public bool UpdateStatusByStrIds(string strIds, string status)
        {
            bool result = true;
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            if (!String.IsNullOrEmpty(strIds))
            {
                StringBuilder hql = new StringBuilder();
                hql.Append("update PGMProgram pgmprogram set pgmprogram.Status=" + status);
                hql.Append(",pgmprogram.PublisherID=" + employeeID);
                hql.Append(",pgmprogram.PublisherName='" + employeeName+"'");
                hql.Append(",pgmprogram.PublisherDateTime='" + DateTime.Now+"'");

                hql.Append("where pgmprogram.Id in(" + strIds+")");
                
                int counts = this.UpdateByHql(hql.ToString());
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 程序的禁用或启用操作
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public bool UpdateIsUseByStrIds(string strIds, bool isUse)
        {
            bool result = true;
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            if (!String.IsNullOrEmpty(strIds))
            {
                StringBuilder hql = new StringBuilder();
                int isUseValue = isUse == true ? 1 : 0;
                hql.Append("update PGMProgram pgmprogram set pgmprogram.IsUse=" + isUseValue);
                
                hql.Append("where pgmprogram.Id in(" + strIds + ")");

                int counts = this.UpdateByHql(hql.ToString());
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}