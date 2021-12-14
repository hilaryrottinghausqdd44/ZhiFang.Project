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