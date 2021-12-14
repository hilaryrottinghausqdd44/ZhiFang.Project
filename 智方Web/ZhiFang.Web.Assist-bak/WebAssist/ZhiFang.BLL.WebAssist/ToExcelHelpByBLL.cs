using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using ZhiFang.WebAssist.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
    public class ToExcelHelpByBLL
    {
        /// <summary>
        /// 获取当前Excel填充的公共填充数据项信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static ExportExcelCommand CreateExportExcelCommand(string startDate, string endDate)
        {
            ExportExcelCommand eec = new ExportExcelCommand();
            eec.EEC_StartDate = startDate;
            eec.EEC_EndDate = endDate;
            eec.EEC_EmployeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            eec.EEC_DeptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
            eec.EEC_LabName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(Entity.Base.SysPublicSet.SysDicCookieSession.LabName);
            if (string.IsNullOrEmpty(eec.EEC_LabName))
                eec.EEC_LabName = JSONConfigHelp.GetString(SysConfig.LISSYS.Key, "EECLabName");
            //ZhiFang.Common.Log.Log.Error("CreateExportExcelCommand.部门名称:" + eec.EEC_DeptName);
            return eec;
        }
    }
}
