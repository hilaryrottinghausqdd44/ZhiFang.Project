using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDDistrict : IDataBase<Model.District>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DistrictNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DistrictNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.District GetModel(int DistrictNo);
        #endregion  成员方法
    }
}
