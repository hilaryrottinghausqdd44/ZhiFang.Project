using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDItemRefeRangeDao : IDBaseDao<ItemRefeRange, long>
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="longItemID"></param>
        /// <param name="OrderField"></param>
        /// <param name="isASC"></param>
        /// <returns></returns>
        IList<ItemRefeRange> SearchItemRefeRangeByItemID(long longItemID, string OrderField, bool isASC);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="longItemID"></param>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        IList<ItemRefeRange> SearchItemRefeRangeByItemID(long longItemID, string strOrder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strItemIDList"></param>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        IList<ItemRefeRange> SearchItemRefeRangeByItemID(string strItemIDList, string strOrder);
	} 
}