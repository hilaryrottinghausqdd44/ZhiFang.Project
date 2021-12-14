using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDBUSINESSMANControl	
	/// </summary>
    public interface IDBUSINESSMANControl : IDataBase<ZhiFang.Model.BUSINESSMANControl>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string BMANControlNo);
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
        int Delete(string BMANControlNo);

        int DeleteList(string Idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BUSINESSMANControl GetModel(string BMANControlNo);

        #endregion  成员方法
    }
}