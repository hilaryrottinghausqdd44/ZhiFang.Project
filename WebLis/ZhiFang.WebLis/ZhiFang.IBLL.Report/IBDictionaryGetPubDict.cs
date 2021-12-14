using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBDictionaryGetPubDict
    {
        DataSet DictionaryGet(string tablename, string fields, string filervalue, string orderbyfields);
    }
}
