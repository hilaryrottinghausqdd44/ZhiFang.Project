using System;
using ZhiFang.Common.Public;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.LabStar.Common
{
    public class SysCookieValueHelper
    {
        public static SysCookieValue GetSysCookieValue()
        {
            SysCookieValue sysCookieValue = new SysCookieValue();
            string tempID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            if (tempID != null && tempID.Trim().Length > 0)
                sysCookieValue.EmpID = long.Parse(tempID);  //操作人员ID
            sysCookieValue.EmpName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);  //操作人员

            tempID = Cookie.CookieHelper.Read(DicCookieSession.HRDeptID);
            if (tempID != null && tempID.Trim().Length > 0)
                sysCookieValue.DeptID = long.Parse(tempID); //操作部门科室ID
            sysCookieValue.DeptName = Cookie.CookieHelper.Read(DicCookieSession.HRDeptName); //操作部门科室

            //sysCookieValue.HostName = Cookie.CookieHelper.Read(""); //操作站点
            //sysCookieValue.HostAddress = Cookie.CookieHelper.Read(""); //操作站点地址
            //sysCookieValue.HostType = Cookie.CookieHelper.Read(""); //操作站点类型

            return sysCookieValue;
        }

        /// <summary>
        /// 获取cookie中LabFlag、LabID数据项的值
        /// </summary>
        /// <returns></returns>
        public static string[] GetLabIDInfoBySysCookie()
        {

            string labID = "";
            string IsLabFlag = "";
            try
            {
                if (!HttpContextHelper.httpcontextFlag())
                    HttpContextHelper.Sethttpcontext();
                IsLabFlag = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag);
                if (!string.IsNullOrWhiteSpace(IsLabFlag) && IsLabFlag.Trim() == "1")
                {
                    labID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                    if (labID == null || labID.Trim() == "")
                    {
                        ZhiFang.Common.Log.Log.Debug("未找到所属实验室标识！");
                        throw new Exception("未找到所属实验室标识！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("未找到所属实验室标记");
                    throw new Exception("未找到所属实验室标记");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new string[2] { IsLabFlag, labID };
        }
    }

}
