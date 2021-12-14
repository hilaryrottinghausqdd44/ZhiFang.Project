using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBPGroup	
    /// </summary>
    public interface IBPGroup : IBBase<ZhiFang.Model.PGroup>, IBDataPage<ZhiFang.Model.PGroup>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int SectionNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int SectionNo);

        int DeleteList(string SectionIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.PGroup GetModel(int SectionNo);
        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.PGroup> DataTableToList(DataTable dt);
        #endregion  成员方法

        Model.PGroup GetModel(int SectionNo, int p);

        int Add(List<ZhiFang.Model.PGroup> modelList);

        int Update(List<Model.PGroup> modelList);
    }
}