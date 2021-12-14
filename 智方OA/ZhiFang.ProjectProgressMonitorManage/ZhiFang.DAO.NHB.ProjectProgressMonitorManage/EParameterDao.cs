using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{	
	public class EParameterDao : BaseDaoNHBService<EParameter, long>, IDEParameterDao
	{
        public string QueryParaDao(string paraType, string paraNo)
        {
            IList<EParameter> listEParameter = this.GetListByHQL(" eparameter.ParaType=\'" + paraType + "\' and eparameter.ParaNo=\'" + paraNo + "\'");
            if (listEParameter != null && listEParameter.Count > 0)
                return listEParameter[0].ParaValue;
            else
                return "";
        }
    } 
}