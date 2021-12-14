using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBDictionaryGetPubDict
    {
        DataSet DictionaryGet(string tablename, string fields, string labcode, string filervalue, string precisequery,int page,int rows);
        int GetTotalCount(string tablename);
    }
}
