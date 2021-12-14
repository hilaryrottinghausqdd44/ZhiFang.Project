using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
	public interface IDPEmpFinanceAccountDao : IDBaseDao<PEmpFinanceAccount, long>
	{
        bool PExpenseAccount(double ExpenseAmount, long EmpId);
        bool PLoanBill(double ExpenseAmount, long EmpId);

    } 
}