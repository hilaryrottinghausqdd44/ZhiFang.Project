using ZhiFang.IDAO.Base;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	public interface IDMEGroupSampleFormDao : IDBaseDao<MEGroupSampleForm, long>
	{
        /// <summary>
        /// 获取检验小组的下一样本号信息
        /// </summary>
        /// <param name="sectionNo"></param>
        /// <param name="testTime"></param>
        /// <returns></returns>
        string GetNextGSampleNo(int sectionNo, string testTime);
    } 
}