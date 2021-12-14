using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;

namespace ZhiFang.BLL.Report
{
    public partial class DictionaryGetPubDict
    {
        private readonly IDDictionaryGetPubDict dal = DalFactory<IDDictionaryGetPubDict>.GetDalByClassName("DictionaryGetPubDict");

        public DataSet DictionaryGet(string tablename, string fields, string labcode, string filervalue, string orderbyfields)
        {
            return dal.DictionaryGet(tablename, fields, labcode, filervalue, orderbyfields);
        }
    }
}
