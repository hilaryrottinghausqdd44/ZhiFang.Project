using System;
using System.Data;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDLab_BUSINESSMAN	
	/// </summary>
    public interface IDLab_BUSINESSMAN : IDataBase<ZhiFang.Model.Lab_BUSINESSMAN>, IDataPage<ZhiFang.Model.Lab_BUSINESSMAN>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabBMANNO);
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
        int Delete(string LabCode, int LabBMANNO);

        int DeleteList(string BNANIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_BUSINESSMAN GetModel(string LabCode, int LabBMANNO);

        DataSet GetListByLike(ZhiFang.Model.Lab_BUSINESSMAN model);
        #endregion  成员方法
    }
}