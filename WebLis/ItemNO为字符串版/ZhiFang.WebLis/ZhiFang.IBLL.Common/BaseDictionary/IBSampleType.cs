using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBSampleType	
    /// </summary>
    public interface IBSampleType : IBBase<ZhiFang.Model.SampleType>, IBDataPage<ZhiFang.Model.SampleType>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SampleTypeNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SampleTypeNo);

        int DeleteList(string SampleTypeIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.SampleType GetModel(int SampleTypeNo);
        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.SampleType> DataTableToList(DataTable dt);
        #endregion  成员方法

        DataSet GetList(int p, Model.SampleType st);

        DataSet GetSampleTypeByColorName(string colorName);

        int Add(List<ZhiFang.Model.SampleType> modelList);

        int Update(List<Model.SampleType> modelList);
    }
}