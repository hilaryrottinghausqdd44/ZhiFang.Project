using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_CLIENTELE	
	/// </summary>
    public interface IDLab_CLIENTELE : IDataBase<ZhiFang.Model.Lab_CLIENTELE>, IDataPage<ZhiFang.Model.Lab_CLIENTELE>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabClIENTNO);
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
        int Delete(string LabCode, int LabClIENTNO);

        int DeleteList(string ClIENTIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_CLIENTELE GetModel(string LabCode, int LabClIENTNO);


        DataSet GetListByLike(ZhiFang.Model.Lab_CLIENTELE model);
        #endregion  成员方法
    }
}