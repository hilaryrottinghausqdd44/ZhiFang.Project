using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBBPhysicalExamType : IBBase<ZhiFang.Model.BPhysicalExamType>, IBDataPage<ZhiFang.Model.BPhysicalExamType>
    {
        bool Exists(long id);
        int Delete(long id);
        Model.BPhysicalExamType GetModel(long id);
        List<ZhiFang.Model.BPhysicalExamType> DataTableToList(DataTable dt);
        //DataSet GetList(int p, int PageIndex, Model.BPhysicalExamType model);
        //int GetListCount(Model.BPhysicalExamType model);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
