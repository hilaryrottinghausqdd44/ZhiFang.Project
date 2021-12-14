

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
	public  interface IBBSampleTypeAndCultureMedium : IBGenericManager<BSampleTypeAndCultureMedium>
	{
        /// <summary>
        /// 依依医嘱项目(和样本类型)找到样本单的检验医嘱项目和样本类型，查询出默认培养基信息
        /// </summary>
        /// <param name="itemIdListsStr"></param>
        /// <param name="bsampleTypeId"></param>
        /// <param name="inoculantType">（接种类型），枚举型：1-初次接种；2-分纯接种</param>
        /// <returns></returns>
        EntityList<BSampleTypeAndCultureMedium> GetDefaultCultureMedium(string itemIdListsStr, string bsampleTypeId, string inoculantType);
        /// <summary>
        /// 依条码号获取到样本信息的默认培养基信息(包括样本信息+血培养瓶条码+标本性状信息+默认培养基+标本量)
        /// </summary>
        /// <param name="gbarCode">条码号</param>
        /// <param name="bsampleTypeId">样本类型Id</param>
        /// <param name="inoculantType">（接种类型），枚举型：1-初次接种；2-分纯接种</param>
        /// <param name="isExec"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        Dictionary<string, object> SearchDefaultCultureMediumByGBarCodeAndInoculantType(string gbarCode, string bsampleTypeId, string inoculantType, ref bool isExec, ref string info);
	}
}