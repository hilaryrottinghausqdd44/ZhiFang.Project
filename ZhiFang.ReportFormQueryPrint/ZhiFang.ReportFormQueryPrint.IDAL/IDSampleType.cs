using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDSampleType : IDataBase<Model.SampleType> 
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SampleTypeNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SampleTypeNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.SampleType GetModel(int SampleTypeNo);
    }
}
