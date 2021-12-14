using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPTaskOperLog : BaseBLL<PTaskOperLog>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTaskOperLog
	{
        public IDAO.ProjectProgressMonitorManage.IDPTaskDao IDPTaskDao { get; set; }
        public override bool Add()
        {
            if (base.Add())
            {
                IDPTaskDao.UpdateByHql("update PTask set OperLogCount = (OperLogCount + 1) where Id = " + Entity.PTaskID);
                return true;
            }
            return false;
        }
    }
}