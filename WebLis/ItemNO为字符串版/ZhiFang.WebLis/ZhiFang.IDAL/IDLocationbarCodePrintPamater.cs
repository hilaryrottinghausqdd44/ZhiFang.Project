using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
   public interface IDLocationbarCodePrintPamater
    {
       bool Exists(long Id);
       bool Add(Model.LocationbarCodePrintPamater model);
       bool Update(Model.LocationbarCodePrintPamater model);
       bool Delete(long Id);
       bool Delete(string AccountId);
       Model.LocationbarCodePrintPamater GetModel(long Id);
       Model.LocationbarCodePrintPamater GetModel(string AccountId);
       DataSet GetList(string strWhere);
       DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
