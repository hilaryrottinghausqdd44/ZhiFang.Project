using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaCenOrgDao : IDBaseDao<ReaCenOrg, long>
	{
        /// <summary>
        ///机构编号=最大值+1
        /// </summary>
        int GetMaxOrgNo();

        /// <summary>
        /// 获取机构对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="hasLabId">是否按机构LabId过滤</param>
        /// <returns></returns>
        ReaCenOrg Get(long id, bool hasLabId);
    } 
}