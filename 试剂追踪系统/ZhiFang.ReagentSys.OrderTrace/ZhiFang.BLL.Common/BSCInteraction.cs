using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IBLL.Common;
using ZhiFang.IDAO.Common;

namespace ZhiFang.BLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public class BSCInteraction : BaseBLL<SCInteraction>, IBSCInteraction
    {
        /// <summary>
        /// 扩展交流内容的新增服务(支持新增话题或新增交流内容)
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddSCInteractionExtend()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (this.Entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "entity信息为空!不能新增保存!";
                return tempBaseResultDataValue;
            }
            if (!this.Entity.BobjectID.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "entity的BobjectID信息为空!不能新增保存!";
            }
            //tempBaseResultDataValue.success = this.Add();
            tempBaseResultDataValue.success = ((IDSCInteractionDao)base.DBDao).Save(this.Entity);
            if (tempBaseResultDataValue.success)
            {
                //新增的是交流信息
                if (this.Entity.IsCommunication == false)
                {
                    ((IDSCInteractionDao)base.DBDao).UpdateReplyCountAndLastReplyDateTimeOfId(this.Entity.BobjectID);
                }
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 依某一业务对象ID获取该业务对象ID下的所有交流内容信息
        /// </summary>
        /// <param name="bobjectID"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<SCInteraction> SearchAllSCInteractionByBobjectID(string bobjectID, int page, int count)
        {
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            if (String.IsNullOrEmpty(bobjectID))
                return tempEntityList;
            string strbIds = GetIDStr(bobjectID);
            if (!String.IsNullOrEmpty(strbIds))
            {
                string whereHql = "IsCommunication=0 and BobjectID in(" + strbIds+ ")";
                tempEntityList = this.SearchListByHQL(whereHql, page, count);
            }
            return tempEntityList;
        }

        /// <summary>
        /// 依某一业务对象ID获取该业务对象ID下的所有交流内容信息
        /// </summary>
        /// <param name="bobjectID"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<SCInteraction> SearchAllSCInteractionByBobjectID(string bobjectID, string sort, int page, int count)
        {
            EntityList<SCInteraction> tempEntityList = new EntityList<SCInteraction>();
            if (String.IsNullOrEmpty(bobjectID))
                return tempEntityList;
            string strbIds = GetIDStr(bobjectID);
            if (!String.IsNullOrEmpty(strbIds))
            {
                string whereHql = "IsCommunication=0 and BobjectID in(" + strbIds + ")";
                tempEntityList = this.SearchListByHQL(whereHql, sort, page, count);
            }
            return tempEntityList;
        }
        private string GetIDStr(string bobjectID)
        {
            StringBuilder strbIds = new StringBuilder();
            if (String.IsNullOrEmpty(bobjectID))
                return strbIds.ToString();
            IList<SCInteraction> tempList = this.SearchListByHQL("IsCommunication=1 and BobjectID=" + bobjectID);
            var tempIdList = tempList.GroupBy(p => p.Id);
            foreach (var item in tempIdList)
            {
                if (!strbIds.ToString().Contains(item.Key.ToString()))
                    strbIds.Append(item.Key.ToString() + ",");
            }
            return strbIds.ToString().TrimEnd(',');
        }
    }
}