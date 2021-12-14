using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBDDictionaryInfo
    {
        /// <summary>
        /// 获得所有数据列表
        /// </summary>
        Model.DownloadDict.D_DictionaryInfo GetAllListByLabCode(string labcode);
    }
}
