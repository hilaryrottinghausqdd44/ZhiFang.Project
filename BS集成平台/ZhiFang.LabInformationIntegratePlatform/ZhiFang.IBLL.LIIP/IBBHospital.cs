using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBHospital : IBGenericManager<BHospital>
    {
        BaseResultBool ReceiveAndAdd(List<AreaVO> hospitalsvolist);
        bool SaveEntity(BHospital entity);
    }
}