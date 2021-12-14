using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDCLIENTELEControl	
	/// </summary>
    public interface IDCLIENTELEControl : IDataBase<ZhiFang.Model.CLIENTELEControl>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ClIENTControlNo);
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
        int Delete(string ClIENTControlNo);

        int DeleteList(string Idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.CLIENTELEControl GetModel(string ClIENTControlNo);
        
        #endregion  成员方法
    }
}