using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBSearchReCheckLog
    {
        bool Exists(long Id);
        
        bool Add(ZhiFang.Model.SearchReCheckLog model);
        
        bool Update(ZhiFang.Model.SearchReCheckLog model);
        
        bool Delete(long Id);
        
        bool DeleteList(string Idlist);
        
        ZhiFang.Model.SearchReCheckLog GetModel(long Id);
        
        DataSet GetList(string strWhere);
        
        DataSet GetList(int Top, string strWhere, string filedOrder);
        
        List<ZhiFang.Model.SearchReCheckLog> GetModelList(string strWhere);
        
        List<ZhiFang.Model.SearchReCheckLog> DataTableToList(DataTable dt);
        
        DataSet GetAllList();
        
        int GetRecordCount(string strWhere);
        
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
    }
}
