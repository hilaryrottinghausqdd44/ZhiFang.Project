using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    public interface IBUpdateLabDataBase
    {
        /// <summary>
        /// 帐号登录成功后,依帐号所属的LabID初始化或更新实验室在系统运行所需信息
        /// </summary>
        /// <param name="labId"></param>
        /// <returns></returns>
        bool EditDataBaseByLabId(long labId);
    }
}
