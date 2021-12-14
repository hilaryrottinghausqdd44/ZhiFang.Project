using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
namespace ZhiFang.ReportFormQueryPrint.BLL
{
	/// <summary>
	/// ReportMarrow
	/// </summary>
    public partial class BRequestMarrow 
	{
        private readonly IDRequestMarrow dal = DalFactory<IDRequestMarrow>.GetDal("RequestMarrow");
        internal DataTable GetRequestMarrowItemList(string FormNo)
        {
            return dal.GetRequestMarrowItemList(FormNo);
        }

        public DataTable GetRequestItemList_DataTable(string FormNo)
        {
            return dal.GetRequestMarrowFullList(FormNo).Tables[0];
        }
    }
}

