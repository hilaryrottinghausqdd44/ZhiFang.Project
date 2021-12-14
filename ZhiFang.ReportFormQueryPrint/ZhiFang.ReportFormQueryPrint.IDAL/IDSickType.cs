using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDSickType : IDataBase<Model.SickType>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SickTypeNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SickTypeNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.SickType GetModel(int SickTypeNo);
    }
}
