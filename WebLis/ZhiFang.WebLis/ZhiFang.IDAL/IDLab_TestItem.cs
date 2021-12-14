using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层B_Lab_TestItem
    /// </summary>
    public interface IDLab_TestItem : IDataBase<Model.Lab_TestItem>, IDataPage<Model.Lab_TestItem>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, string LabItemNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string LabCode, string LabItemNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_TestItem GetModel(string LabCode, string LabItemNo);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);

        DataSet GetList(ZhiFang.Model.Lab_TestItem model, string flag);


        DataSet GetLabTestItemByItemNo(string labCode, string ItemNo);
        int UpdateColor(ZhiFang.Model.Lab_TestItem model);

        DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model, int nowPageNum, int nowPageSize, string sort, string order);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        #endregion  成员方法
    }
}