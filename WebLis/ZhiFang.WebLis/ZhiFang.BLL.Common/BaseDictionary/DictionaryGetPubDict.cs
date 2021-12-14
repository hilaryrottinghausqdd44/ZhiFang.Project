using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;


namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class DictionaryGetPubDict :IBLL.Common.BaseDictionary.IBDictionaryGetPubDict
    {
        private readonly IDDictionaryGetPubDict dal = DalFactory<IDDictionaryGetPubDict>.GetDalByClassName("DictionaryGetPubDict");

        public DataSet DictionaryGet(string tablename, string fields, string labcode, string filervalue, string precisequery,int page,int rows)
        {
            return dal.DictionaryGet(tablename, fields, labcode, filervalue, precisequery,page,rows);
        }
        public int GetTotalCount(string tablename) {
            return dal.GetTotalCount(tablename);
        }
    }
}