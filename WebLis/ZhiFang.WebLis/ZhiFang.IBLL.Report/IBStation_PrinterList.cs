using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Report
{
    /// <summary>
    /// 业务逻辑类IBStation_PrinterList 的摘要说明。
    /// </summary>
    public interface IBStation_PrinterList : IBLLBase<Model.Station_PrinterList>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(Model.Station_PrinterList model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.Station_PrinterList GetModel(int id);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.Station_PrinterList GetModelByCache(int id);
    }
}
