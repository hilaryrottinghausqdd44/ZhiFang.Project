using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IDAL
{
    public interface IDDownloadDictHelp// : IDataBase<Model.DownloadDict.D_DictionaryInfo>
    {
        string GetMaxDataTimeStamp(string labcode);
    }
}
