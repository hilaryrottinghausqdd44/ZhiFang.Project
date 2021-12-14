using System;
using System.Data;
using ZhiFang.IDAL;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDLab_Department	
    /// </summary>
    public interface IDLab_Department : IDataBase<ZhiFang.Model.Lab_Department>, IDataPage<ZhiFang.Model.Lab_Department>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabDeptNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string LabCode, int LabDeptNo);


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_Department GetModel(string LabCode, int LabDeptNo);

        DataSet GetListByLike(ZhiFang.Model.Lab_Department model);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);

        #endregion  成员方法
    }
}