using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEMicroDSTValueDao : IDBaseDao<MEMicroDSTValue, long>
	{
        /// <summary>
        /// 允许传入同一检验单的多个药敏结果Id
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        bool UpdateDeleteFlag(string strIds, bool deleteFlag);
	} 
}