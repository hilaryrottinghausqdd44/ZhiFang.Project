using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDLab_SickType	
    /// </summary>
    public interface IDLab_SickType : IDataBase<ZhiFang.Model.Lab_SickType>, IDataPage<ZhiFang.Model.Lab_SickType>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabSickTypeNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string LabCode, int LabSickTypeNo);

        int DeleteList(string SickTypeIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_SickType GetModel(string LabCode, int LabSickTypeNo);

        DataSet GetListByLike(ZhiFang.Model.Lab_SickType model);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        DataSet GetLabCodeNo(string LabCode, List<string> LabCnameList);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    }
}