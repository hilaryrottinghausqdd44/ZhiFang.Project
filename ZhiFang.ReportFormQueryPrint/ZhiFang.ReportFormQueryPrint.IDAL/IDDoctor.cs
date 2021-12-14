using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    /// <summary>
    /// 接口层IDoctor 的摘要说明。
    /// </summary>
    public interface IDDoctor:IDataBase<Model.Doctor>
    {
        #region  成员方法        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DoctorNo);
                /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DoctorNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.Doctor GetModel(int DoctorNo);        
        #endregion  成员方法
    }
}
