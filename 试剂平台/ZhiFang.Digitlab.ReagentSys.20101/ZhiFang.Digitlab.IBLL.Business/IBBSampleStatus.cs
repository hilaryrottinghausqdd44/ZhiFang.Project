

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBSampleStatus : IBGenericManager<BSampleStatus>
	{
        IList<BSampleStatus> SerachSampleStatusByOperateObjectID(long operateObjectID);

        /// <summary>
        /// 批量增加状态记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operateObjectIDList">被记录状态的对象ID列表</param>
        /// <returns>操作成功的对象ID列表</returns>
        BaseResultDataValue AddMEPTSampleStatus(BSampleStatus entity, string objectIDList);

        /// <summary>
        /// 增加操作对象的样本状态
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="statusTypeCode">操作对象状态类型代码()例如:已复核状态是Review</param>
        /// <param name="comment">状态说明</param>
        /// <returns>样本状态实体</returns>
        BSampleStatus AddOperateObjectStatus(BaseEntity baseEntity, string objectTypeCode, string statusTypeCode, string comment);
        bool DeleteBSampleStatus(long? operateObjectID);
        /// <summary>
        /// 增加操作对象的样本状态
        /// </summary>
        /// <param name="operateObjectID">操作对象Id</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="statusTypeLevel">操作对象状态操作级别</param>
        /// <param name="comment">状态说明</param>
        /// <returns>样本状态实体</returns>
        BSampleStatus AddByStatusTypeLevel(string operateObjectID, string objectTypeCode, string statusTypeLevel, string comment);
	}
}