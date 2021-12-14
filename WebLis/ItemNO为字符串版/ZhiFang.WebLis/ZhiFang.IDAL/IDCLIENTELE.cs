using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层ICLIENTELE 的摘要说明。
	/// </summary>
    public interface IDCLIENTELE : IDataBase<ZhiFang.Model.CLIENTELE>,IDataPage<ZhiFang.Model.CLIENTELE>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(long ClIENTNO);
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
        int Delete(long ClIENTNO);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.CLIENTELE GetModel(long ClIENTNO);
        DataSet GetClientNo(string CLIENTIDlist, string CName);

        DataSet GetListLike(ZhiFang.Model.CLIENTELE model);  
		#endregion  成员方法

        int DeleteList(string CLIENTIDlist);

        int Add(List<ZhiFang.Model.CLIENTELE> modelList);
        int Update(List<ZhiFang.Model.CLIENTELE> modelList);
    }
}
