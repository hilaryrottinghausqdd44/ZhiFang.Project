using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBLocationbarCodePrintPamater
    {
        bool Exists(long id);
        bool Add(Model.LocationbarCodePrintPamater model);
        bool Update(Model.LocationbarCodePrintPamater model);
        bool Delete(long id);
        bool Delete(string AccountId);
        Model.LocationbarCodePrintPamater GetModel(long id);
        Model.LocationbarCodePrintPamater GetModel(string AccountId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        List<ZhiFang.Model.LocationbarCodePrintPamater> DataTableToList(DataTable dt);

        Model.LocationbarCodePrintPamater GetAdminPara();
    }
}
