using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBSamplingGroup : IBBase<ZhiFang.Model.SamplingGroup>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string SampleTypeControlNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string SampleTypeControlNo);

        int DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.SamplingGroup GetModel(string SamplingGroupNo);
        #endregion  成员方法
    }
}
