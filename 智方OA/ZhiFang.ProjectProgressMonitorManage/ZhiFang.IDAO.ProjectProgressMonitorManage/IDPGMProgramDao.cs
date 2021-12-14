using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
	public interface IDPGMProgramDao : IDBaseDao<PGMProgram, long>
	{
        bool UpdateCountsById(long id);
        bool UpdateStatusByStrIds(string strIds, string status);
        bool UpdateIsUseByStrIds(string strIds, bool isUse);

    } 
}