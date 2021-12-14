using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCOperation : IBGenericManager<SCOperation>
	{
        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        void AddOperation(long bobjectID, long labID, int type, string operationMemo);

        /// <summary>
        /// 操作记录登记---增加实体的状态操作
        /// </summary>
        /// <param name="operEntity"></param>
        void AddOperationEntityStatus(BaseEntity operEntity);

    }
}