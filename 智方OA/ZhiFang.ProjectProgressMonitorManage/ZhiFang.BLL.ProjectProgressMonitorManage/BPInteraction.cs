using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BPInteraction : BaseBLL<PInteraction>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPInteraction
	{
        public IDAO.ProjectProgressMonitorManage.IDPTaskDao IDPTaskDao { get; set; }
        public override bool Add()
        {
            if (base.Add())
            {
                IDPTaskDao.UpdateByHql("update PTask set InteractionCount = (InteractionCount + 1) where Id = " + Entity.PTask.Id);
                return true;
            }
            return false;
        }
    }
}