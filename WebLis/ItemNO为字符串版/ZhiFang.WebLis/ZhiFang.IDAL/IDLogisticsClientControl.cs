using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data;

namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDBusinessLogicClientControl

    /// </summary>
    public interface IDBusinessLogicClientControl : IDataBase<ZhiFang.Model.BusinessLogicClientControl>, IDataPage<ZhiFang.Model.BusinessLogicClientControl>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string Account, string ClientNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string Account, string ClientNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BusinessLogicClientControl GetModel(string Account, string ClientNo);
        #endregion  成员方法

        bool Add(List<Model.BusinessLogicClientControl> l);
    }
}
