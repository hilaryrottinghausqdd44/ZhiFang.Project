using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPReceive : BaseBLL<PReceive>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPReceive
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IDPContractDao IDPContractDao { get; set; }
        public IDPFinanceReceiveDao IDPFinanceReceiveDao { get; set; }
        public IDPReceivePlanDao IDPReceivePlanDao { get; set; }
        public BaseResultDataValue AddPReceive()
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (this.Entity.PReceivePlan == null)
            {
                throw new Exception("商务收款所属收款计划为空！");
            }
            if (this.Entity.PReceivePlan.UnReceiveAmount< this.Entity.ReceiveAmount)
            {
                throw new Exception("商务收款金额大于所属收款计划待收金额！");
            }
            if (this.Entity.PFinanceReceive == null)
            {
                throw new Exception("商务收款所属财务收款为空！");
            }
            if (this.Entity.PFinanceReceive.ReceiveAmount - this.Entity.PFinanceReceive.SplitAmount - this.Entity.ReceiveAmount<0)
            {
                throw new Exception("商务收款金额大于所属财务收款待分配金额！");
            }
            if (base.Add())
            {
                IDPContractDao.UpdateByHql("update PContract set PayedMoney=PayedMoney+"+Entity.ReceiveAmount + ",LeftMoney=Amount-PayedMoney-" + Entity.ReceiveAmount + " where Id=" + Entity.PContractID);

                IDPFinanceReceiveDao.UpdateByHql("update PFinanceReceive set SplitAmount=SplitAmount+" + Entity.ReceiveAmount +" where Id=" + Entity.PFinanceReceive.Id);

                IDPReceivePlanDao.UpdateByHql("update PReceivePlan set ReceiveAmount=ReceiveAmount+" + Entity.ReceiveAmount + ",UnReceiveAmount=UnReceiveAmount-" + Entity.ReceiveAmount + " where Id=" + Entity.PReceivePlan.Id);

                AddOperationEntityStatus(this.Entity,1,"新增");
                brb.success = true;
                return brb;
            }
            else
            {
                brb.success = false;
                return brb;
            }
        }
        public BaseResultDataValue AddBackPReceive()
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (this.Entity.PReceivePlan == null)
            {
                throw new Exception("商务收款所属收款计划为空！");
            }
            if (this.Entity.PReceivePlan.ReceiveAmount < this.Entity.ReceiveAmount)
            {
                throw new Exception("商务收款金额大于所属收款计划已金额！");
            }
            if (this.Entity.PFinanceReceive == null)
            {
                throw new Exception("商务收款所属财务收款为空！");
            }
            if (this.Entity.PFinanceReceive.SplitAmount<this.Entity.ReceiveAmount)
            {
                throw new Exception("商务收款金额大于所属财务收款已分配金额！");
            }
            if (DBDao.UpdateByHql("update PReceive set IsUse=false where Id="+Entity.Id)>0)
            {
                IDPContractDao.UpdateByHql("update PContract set PayedMoney=PayedMoney-" + Entity.ReceiveAmount + ",LeftMoney=Amount-PayedMoney+" + Entity.ReceiveAmount + " where Id=" + Entity.PContractID);

                IDPFinanceReceiveDao.UpdateByHql("update PFinanceReceive set SplitAmount=SplitAmount-" + Entity.ReceiveAmount + " where Id=" + Entity.PFinanceReceive.Id);

                IDPReceivePlanDao.UpdateByHql("update PReceivePlan set ReceiveAmount=ReceiveAmount-" + Entity.ReceiveAmount + ",UnReceiveAmount=UnReceiveAmount+" + Entity.ReceiveAmount + " where Id=" + Entity.PReceivePlan.Id);

                AddOperationEntityStatus(this.Entity, 0, "撤回");
                brb.success = true;
                return brb;
            }
            else
            {
                brb.success = false;
                return brb;
            }
        }
        void AddOperationEntityStatus(PReceive pr,long type,string typeName)
        {
            SCOperation sco = new SCOperation();
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PReceive";
            sco.Memo = "合同名称："+ pr.PContractName + "，收款计划ID："+ pr.PReceivePlan.Id + "，商务收款ID："+ pr.PFinanceReceive.Id;

            sco.Type = type;
            sco.TypeName = typeName;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        public override EntityList<PReceive> SearchListByHQL(string strHqlWhere, string Order, int page, int count)
        {
            EntityList<PReceive> tmplist=DBDao.GetListByHQL(strHqlWhere, Order, page, count);
            if (tmplist != null && tmplist.list != null && tmplist.list.Count > 0)
            {
                List<string> PContractIDlist = new List<string>();
                foreach (var e in tmplist.list)
                {
                    PContractIDlist.Add(e.PContractID.ToString());
                }
                IList<PContract> PContractList = IDPContractDao.GetListByHQL(" PContractID in (" + string.Join(",", PContractIDlist.ToArray()) + ") ");
                if (PContractList == null || PContractList.Count <= 0)
                {
                    return null;
                }
                foreach (var e in tmplist.list)
                {
                    var pcontract = PContractList.Where(a => a.Id == e.PContractID);
                    if (pcontract != null && pcontract.Count() > 0)
                        e.ContractSignDateTime = pcontract.ElementAt(0).SignDate;
                }
            }
            return tmplist;
        }
        public override EntityList<PReceive> SearchListByHQL(string strHqlWhere, int page, int count)
        {
            EntityList<PReceive> tmplist = DBDao.GetListByHQL(strHqlWhere, page, count);
            if (tmplist != null && tmplist.list != null && tmplist.list.Count > 0)
            {
                List<string> PContractIDlist = new List<string>();
                foreach (var e in tmplist.list)
                {
                    PContractIDlist.Add(e.PContractID.ToString());
                }
                IList<PContract> PContractList = IDPContractDao.GetListByHQL(" PContractID in (" + string.Join(",", PContractIDlist.ToArray()) + ") ");
                if (PContractList == null || PContractList.Count <= 0)
                {
                    return null;
                }
                foreach (var e in tmplist.list)
                {
                    var pcontract = PContractList.Where(a => a.Id == e.PContractID);
                    if (pcontract != null && pcontract.Count() > 0)
                        e.ContractSignDateTime = pcontract.ElementAt(0).SignDate;
                }
            }
            return tmplist;
        }

        public BaseResultDataValue SearchListTotalByHQL(string where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object result = DBDao.GetTotalByHQL(where, fields);
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(result);
            return brdv;
        }
    }
}