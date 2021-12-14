using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPFinanceReceive : BaseBLL<PFinanceReceive>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPFinanceReceive
    {
        public bool RemoveByFinance(long PFinanceReceiveID)
        {
            PFinanceReceive pfr = DBDao.Get(PFinanceReceiveID);
            if (pfr != null && pfr.SplitAmount == 0)
            {
                return DBDao.UpdateByHql(" update PFinanceReceive set IsUse=false where Id = " + PFinanceReceiveID) > 0;
            }
            else
            {
                throw new Exception("收款记录已被分配！不能删除！");
            }
        }

        public BaseResultDataValue SearchListTotalByHQL(string where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            object result=DBDao.GetTotalByHQL(where, fields);
            brdv.ResultDataValue= result==null ? "": result.ToString();
            return brdv;
        }

        public bool UpdateByFinance(string[] strParas, PFinanceReceive entity,string fields)
        {
            PFinanceReceive pfr=DBDao.Get(entity.Id);
            if (pfr != null)
            {
                if (entity.ReceiveAmount <= 0)
                {
                    throw new Exception("收款金额不能低于或等于0！");
                }
                if ((entity.ReceiveAmount - pfr.SplitAmount) < 0)
                {
                    throw new Exception("收款金额不能低于已分配金额！");
                }
                return base.Update(strParas);
            }
            else
            {
                return false;
            }
        }

    }
}