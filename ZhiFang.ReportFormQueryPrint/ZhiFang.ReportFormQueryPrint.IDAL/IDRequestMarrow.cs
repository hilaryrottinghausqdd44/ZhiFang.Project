using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层RequestMarrow
	/// </summary>
    public interface IDRequestMarrow : IDataBase<Model.RequestMarrow>
	{
        DataTable GetRequestMarrowItemList(string FormNo);

        DataSet GetRequestMarrowFullList(string p);
    } 
}
