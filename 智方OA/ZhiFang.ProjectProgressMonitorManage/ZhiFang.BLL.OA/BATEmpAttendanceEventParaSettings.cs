using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.Entity.RBAC.ViewObject.Response;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    /// 员工考勤事件参数设置表
    /// </summary>
    public class BATEmpAttendanceEventParaSettings : BaseBLL<ATEmpAttendanceEventParaSettings>, ZhiFang.IBLL.OA.IBATEmpAttendanceEventParaSettings
    {
        ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public IList<ATEmpAttendanceEventParaSettings> SearchATEmpAttendanceEventParaSettingsByDeptId(long deptid, bool isincludesubdept)
        {
            string hql = " 1=1 ";
            IList<HREmployee> emplist = new List<HREmployee>();
            if (deptid > 0)
            {
                if (isincludesubdept)
                {
                    var deptidlist = IDHRDeptDao.GetSubDeptIdListByDeptId(deptid);
                    string deptidStr = "";
                    if (deptidlist.Count > 0)
                    {
                        deptidStr = string.Join(",", deptidlist.ToArray());
                    }
                    else
                    {
                        deptidStr = deptid.ToString();
                    }
                    if (!String.IsNullOrEmpty(deptidStr))
                        emplist = IDHREmployeeDao.GetListByHQL(" IsUse=true and HRDept.Id in ( " + deptidStr + " )");
                    List<long> empidlist = new List<long>();
                    foreach (Entity.RBAC.HREmployee emp in emplist)
                    {
                        empidlist.Add(emp.Id);
                    }
                    if (empidlist.Count > 0)
                    {
                        hql += " and EmpID in (" + string.Join(",", empidlist.ToArray()) + ") ";
                    }
                    else
                    {
                        emplist = null;
                    }
                }
                else
                {
                    emplist = IDHREmployeeDao.GetListByHQL(" IsUse=true and HRDept.Id = " + deptid + " ");
                    List<long> empidlist = new List<long>();
                    foreach (Entity.RBAC.HREmployee emp in emplist)
                    {
                        empidlist.Add(emp.Id);
                    }
                    if (emplist.Count > 0)
                    {
                        hql += " and EmpID in (" + string.Join(",", empidlist.ToArray()) + ") ";
                    }
                    else
                    {
                        emplist = null;
                    }
                }
            }
            else
            {
                emplist = null;
            }
            if (emplist != null)
            {
                ZhiFang.Common.Log.Log.Debug("SearchATEmpAttendanceEventParaSettingsByDeptId.HQL:" + hql);
                IList<ATEmpAttendanceEventParaSettings> ateaepslist = DBDao.GetListByHQL(hql);

                List<ATEmpAttendanceEventParaSettings> ateaepslistreturn = new List<ATEmpAttendanceEventParaSettings>();

                foreach (var emp in emplist)
                {
                    ATEmpAttendanceEventParaSettings ateaeps = new ATEmpAttendanceEventParaSettings();
                    ateaeps.EmpID = emp.Id;
                    ateaeps.EmpName = emp.CName;
                    ateaeps.ATEventParaSettingsType = -1;
                    if (ateaepslist != null && ateaepslist.Count > 0)
                    {
                        var ateaepstmp = ateaepslist.Where(a => a.EmpID == emp.Id);
                        if (ateaepstmp.Count() > 0)
                        {
                            ateaeps = ateaepstmp.ElementAt(0);
                        }
                    }
                    ateaepslistreturn.Add(ateaeps);
                }
                return ateaepslistreturn;
            }
            else
            {
                return null;
            }
        }
        public ATEmpAttendanceEventParaSettings SearchATEmpAttendanceEventParaSettingsByEmpId(long empId)
        {
            string hql = " IsUse=true and EmpID=" + empId;
            ATEmpAttendanceEventParaSettings entity = null;
            if (empId > 0)
            {
                //ZhiFang.Common.Log.Log.Debug("SearchATEmpAttendanceEventParaSettingsByEmpId.HQL:" + hql);
                IList<ATEmpAttendanceEventParaSettings> list = DBDao.GetListByHQL(hql);
                if (list.Count > 0)
                    entity = list[0];
            }
            return entity;
        }

    }
}