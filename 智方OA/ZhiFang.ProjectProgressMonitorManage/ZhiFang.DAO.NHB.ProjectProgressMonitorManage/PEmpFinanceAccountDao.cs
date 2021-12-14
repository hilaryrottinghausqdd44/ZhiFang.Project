using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{
    public class PEmpFinanceAccountDao : BaseDaoNHB<PEmpFinanceAccount, long>, IDPEmpFinanceAccountDao
    {
        public bool PExpenseAccount(double ExpenseAmount, long EmpId)
        {
            IList<PEmpFinanceAccount> empfa = this.GetListByHQL(" EmpID= " + EmpId);
            if (empfa != null && empfa.Count > 0)
            {
                empfa[0].RepaymentAmount = empfa[0].RepaymentAmount + ExpenseAmount;
                empfa[0].UnRepaymentAmount = empfa[0].UnRepaymentAmount - ExpenseAmount;
                return this.Update(empfa[0]);
            }
            else
            {
                return false;
                //PEmpFinanceAccount pefa = new PEmpFinanceAccount();
                //pefa.EmpID = empID;
                //pefa.Name = empName;
                //pefa.LoanAmount = 0;
                //pefa.UnRepaymentAmount = 0;
                //pefa.RepaymentAmount = 0;
                //pefa.LoanAmount = 0;
                //pefa.IsUse = true;
                //IDPEmpFinanceAccountDao.Save(pefa);
            }
        }
        public bool PLoanBill(double LoanBillAmount, long EmpId)
        {
            IList<PEmpFinanceAccount> empfa = this.GetListByHQL(" EmpID= " + EmpId);
            if (empfa != null && empfa.Count > 0)
            {
                empfa[0].LoanAmount = empfa[0].LoanAmount + LoanBillAmount;
                empfa[0].UnRepaymentAmount = empfa[0].UnRepaymentAmount + LoanBillAmount;
                return this.Update(empfa[0]);
            }
            else
            {
                return false;
                //PEmpFinanceAccount pefa = new PEmpFinanceAccount();
                //pefa.EmpID = empID;
                //pefa.Name = empName;
                //pefa.LoanAmount = 0;
                //pefa.UnRepaymentAmount = 0;
                //pefa.RepaymentAmount = 0;
                //pefa.LoanAmount = 0;
                //pefa.IsUse = true;
                //IDPEmpFinanceAccountDao.Save(pefa);
            }
        }
    }
}