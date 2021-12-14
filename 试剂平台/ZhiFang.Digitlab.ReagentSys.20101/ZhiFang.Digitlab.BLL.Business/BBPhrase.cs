
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public class BBPhrase : BaseBLL<BPhrase>, ZhiFang.Digitlab.IBLL.Business.IBBPhrase
    {
        /// <summary>
        /// 根传入的小组类型Url获取小组结果短语及小组项目结果短语(过滤相同的)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        public IList<BPhrase> SearchBPhrasetByGMGroupTypeUrl(string gmgroupTypeUrl)
        {
            return ((IDAO.IDBPhraseDao)DBDao).SearchListByGMGroupTypeUrl(gmgroupTypeUrl);
        }
    }
}