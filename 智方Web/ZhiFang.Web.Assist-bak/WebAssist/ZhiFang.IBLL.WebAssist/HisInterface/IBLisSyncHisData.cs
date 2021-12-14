using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.WebAssist
{
    public interface IBLisSyncHisData
    {
        /// <summary>
        /// 仅同步HIS科室信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveLisSyncDpetHisData();

        /// <summary>
        /// LIS同步HIS科室人员信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveLisSyncHisDataInfo();

    }
}
