using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDSamplingGroup : IDataBase<ZhiFang.Model.SamplingGroup>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string SampleTypeControlNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string SampleTypeControlNo);

        int DeleteList(string Idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.SamplingGroup GetModel(string SampleTypeControlNo);

        #endregion  成员方法
    }
}
