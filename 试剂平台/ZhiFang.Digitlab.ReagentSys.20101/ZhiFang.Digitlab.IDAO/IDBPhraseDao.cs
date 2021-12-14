using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDBPhraseDao : IDBaseDao<BPhrase, long>
	{
        /// <summary>
        /// 根传入的小组类型Url获取小组结果短语及小组项目结果短语(过滤相同的)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        IList<BPhrase> SearchListByGMGroupTypeUrl(string gmgroupTypeUrl);
	} 
}