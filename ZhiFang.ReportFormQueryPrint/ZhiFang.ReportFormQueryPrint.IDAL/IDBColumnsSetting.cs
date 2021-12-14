using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层NRequestForm
	/// </summary>
    public interface IDBColumnsSetting : IDAL.IDataBase<Model.BColumnsSetting>
	{
        int deleteByAppType(string appType);
        int deleteById(long id);
        DataSet GetList(string strWhere, string order);

    } 
}
