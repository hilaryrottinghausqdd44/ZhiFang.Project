using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Report
{
    public interface IBPatNo_Passwords : IBLLBase<Model.PatNo_Passwords>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int Id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int Id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.PatNo_Passwords GetModel(int Id);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.PatNo_Passwords GetModelByCache(int Id);
    }
}
