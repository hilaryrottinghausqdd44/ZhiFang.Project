using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层NRequestForm
	/// </summary>
    public interface IDNRequestItem : IDAL.IDataBase<Model.NRequestItem>
	{
        #region  成员方法

        int Delete(string SerialNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.NRequestForm GetModel(string SerialNo);
        #endregion  成员方法

        int GetCountFormFull(string where);

        DataSet GetList_FormFull(string urlModel, string urlWhere);
    } 
}
