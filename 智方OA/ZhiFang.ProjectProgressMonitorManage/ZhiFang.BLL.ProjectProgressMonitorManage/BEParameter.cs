using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using System.Web;
using ZhiFang.Common.Public;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BEParameter : BaseBLL<EParameter>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBEParameter
    {
        public BaseResultDataValue AddEParameter(EParameter entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<EParameter> listEParameter = this.SearchListByHQL(" eparameter.ParaType=\'" + entity.ParaType + "\' and eparameter.ParaNo=\'" + entity.ParaNo + "\'");
            if (listEParameter != null && listEParameter.Count > 0)
            {
                listEParameter[0].ParaValue = entity.ParaValue;
                listEParameter[0].ParaDesc = entity.ParaDesc;
                listEParameter[0].IsUse = entity.IsUse;
                listEParameter[0].IsUserSet = entity.IsUserSet;
                listEParameter[0].DispOrder = entity.DispOrder;
                this.Entity = listEParameter[0];
                brdv.success = this.Edit();
            }
            else
            {
                this.Entity = entity;
                brdv.success = this.Add();
            }
            return brdv;
        }

        public string QueryPara(string paraType, string paraNo)
        {
            return (DBDao as IDEParameterDao).QueryParaDao(paraType, paraNo);
        }
    }
}