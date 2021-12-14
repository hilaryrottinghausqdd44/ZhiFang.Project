using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.SA
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBDictTree : IBGenericManager<BDictTree>
	{
        /// <summary>
        /// 根据传入的节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="id">默认传的id</param>
        /// <param name="idListStr">124,222</param>
        /// <param name="maxLevelStr">最大层数参数</param>
        /// <returns></returns>
        BaseResultTree<BDictTree> SearchBDictTreeListTree(string id, string idListStr, string maxLevelStr);
        /// <summary>
        /// 根据传入的一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="idListStr">124,222</param>
        /// <param name="maxLevelStr">最大层数参数</param>
        /// <returns></returns>
        BaseResultTree SearchBDictTree(string idListStr, string maxLevelStr);
        EntityList<BDictTree> SearchBDictTreeAndChildTreeByHQL(int page, int limit, string where, string sort, bool isSearchChild);
    }
}