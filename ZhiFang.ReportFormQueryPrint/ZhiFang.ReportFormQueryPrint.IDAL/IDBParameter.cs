using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层NRequestForm
	/// </summary>
    public interface IDBParameter : IDAL.IDataBase<Model.BParameter>
	{
        int deleteBySName(string appType);
        int GetCount(string strwhere);
        bool Exists(string strWhere);

        DataSet GetSeniorPublicSetting(string SName, string ParaNo);
    } 
}
