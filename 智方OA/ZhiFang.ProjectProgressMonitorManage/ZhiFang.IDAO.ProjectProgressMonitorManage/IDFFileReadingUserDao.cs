using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
	public interface IDFFileReadingUserDao : IDBaseDao<FFileReadingUser, long>
	{
        bool DeleteByFFileId(long fFileId);
        
    } 
}