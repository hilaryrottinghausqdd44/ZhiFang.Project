using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层NRequestForm
	/// </summary>
    public interface IDNRequestForm : IDAL.IDataBase<Model.NRequestForm>
	{
        #region  成员方法

        int Delete(string SerialNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.NRequestForm GetModel(string SerialNo);
        #endregion  成员方法

        int GetCountForm(string strWhere);
        DataSet GetList_FormFull(string fields, string strWhere);

    } 
}
