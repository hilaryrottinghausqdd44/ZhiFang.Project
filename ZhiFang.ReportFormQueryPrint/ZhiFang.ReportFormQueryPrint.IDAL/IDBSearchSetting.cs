using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层NRequestForm
	/// </summary>
    public interface IDBSearchSetting : IDAL.IDataBase<Model.BSearchSetting>
	{
        int deleteByAppType(string appType);
        int deleteById(int id);
        DataSet GetList(string strWhere, string order);
        //查询要添加的高级查询项是否存在
        int GetIsSenior(long STID);
    } 
}
