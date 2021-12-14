using System;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IB_Lab_GroupItem
    /// </summary>
    public interface IBLab_GroupItem:IBBase<Model.Lab_GroupItem>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PItemNo, string ItemNo, string LabCode);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string PItemNo, string ItemNo, string LabCode);
        int Delete(ZhiFang.Model.Lab_GroupItem model, string flag);
        int DeleteAll(string LabCode);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_GroupItem GetModel(string PItemNo, string ItemNo, string LabCode);
        
        #endregion  成员方法

        DataSet GetGroupItemList(string p, string labcode);

        string GetSubItemList_No_CName(string p, string labcode);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        DataSet GetGroupItemListBySubItemNo(string subitemno, string labcode);
    }
}