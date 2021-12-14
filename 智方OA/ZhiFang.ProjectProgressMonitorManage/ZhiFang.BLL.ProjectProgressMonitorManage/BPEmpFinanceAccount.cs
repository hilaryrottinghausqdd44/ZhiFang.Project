using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPEmpFinanceAccount : BaseBLL<PEmpFinanceAccount>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPEmpFinanceAccount
    {
        ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public IList<PEmpFinanceAccount> SearchPEmpFinanceAccountByDeptId(long deptid, bool isincludesubdept)
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
                    else {
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
                ZhiFang.Common.Log.Log.Debug("SearchPEmpFinanceAccountByDeptIdOrEmpId.HQL:" + hql);
                IList<PEmpFinanceAccount> ateaepslist = DBDao.GetListByHQL(hql);

                List<PEmpFinanceAccount> ateaepslistreturn = new List<PEmpFinanceAccount>();

                foreach (var emp in emplist)
                {
                    PEmpFinanceAccount ateaeps = new PEmpFinanceAccount();
                    ateaeps.IsExist = false;
                  
                    ateaeps.EmpID = emp.Id;
                    ateaeps.Name = emp.CName;
                    ateaeps.PinYinZiTou = emp.PinYinZiTou;
                    ateaeps.Shortcode = emp.Shortcode;
                    ateaeps.SName = emp.SName;
                    if (ateaepslist != null && ateaepslist.Count > 0)
                    {
                        var ateaepstmp = ateaepslist.Where(a => a.EmpID == emp.Id);
                        if (ateaepstmp.Count() > 0)
                        {
                            ateaeps = ateaepstmp.ElementAt(0);
                            ateaeps.IsExist = true;
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
    }
}