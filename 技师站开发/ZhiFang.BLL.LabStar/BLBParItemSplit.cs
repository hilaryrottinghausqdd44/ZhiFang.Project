using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBParItemSplit : BaseBLL<LBParItemSplit>, ZhiFang.IBLL.LabStar.IBLBParItemSplit
    {
        IBLBItemGroup IBLBItemGroup { get; set; }
        IDLBSamplingItemDao IDLBSamplingItemDao { get; set; }

        public EntityList<LBParItemSplit> SearchAddLBParItemSplitListByParItemId(string where, string sort, int page, int limit, string parItemId)
        {
            EntityList<LBParItemSplit> entityList = new EntityList<LBParItemSplit>();
            entityList.list = new List<LBParItemSplit>();
            if (string.IsNullOrEmpty(where))
                where = "1=1 ";
            if (!where.Contains("lbitemgroup.LBGroup.Id"))
                where = where + " and lbitemgroup.LBGroup.Id=" + parItemId;
            IList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(where, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
            if (listLBItemGroup.Count <= 0) return entityList;

            foreach (LBItemGroup item in listLBItemGroup)
            {
                LBParItemSplit entity = new LBParItemSplit();
                entity.NewBarCode = 0;
                entity.IsAutoUnion = false;
                entity.ParItem = item.LBGroup;
                entity.LBItem = item.LBItem;
                //子项目的采样组项目关系信息
                LBSamplingGroup defalutLBSamplingGroup = null;
                entity.LBSamplingGroupListStr = SearchLBSamplingGroupListStr(item.LBItem.Id, ref defalutLBSamplingGroup);
                entity.LBSamplingGroup = defalutLBSamplingGroup;
                entityList.list.Add(entity);
            }
            entityList.count = entityList.list.Count;
            return entityList;
        }
        public EntityList<LBParItemSplit> SearchEditLBParItemSplitByHQL(string where, string order, int page, int limit)
        {
            EntityList<LBParItemSplit> el = this.SearchListByHQL(where, order, page, limit);
            for (int i = 0; i < el.list.Count; i++)
            {
                //子项目的采样组项目关系信息
                LBSamplingGroup defalutLBSamplingGroup = null;
                el.list[i].LBSamplingGroupListStr = SearchLBSamplingGroupListStr(el.list[i].LBItem.Id, ref defalutLBSamplingGroup);
                //if (el.list[i].LBSamplingGroup == null)
                //    el.list[i].LBSamplingGroup = defalutLBSamplingGroup;
            }
            return el;
        }
        private string SearchLBSamplingGroupListStr(long lbitemId, ref LBSamplingGroup defalutLBSamplingGroup)
        {
            string samplingGroupListStr = "";
            string where2 = " lbsamplingitem.LBItem.Id=" + lbitemId;
            IList<LBSamplingItem> entityList1 = IDLBSamplingItemDao.GetListByHQL(where2);
            if (entityList1 == null || entityList1.Count <= 0) return samplingGroupListStr;
            if (entityList1.Count > 0)
                defalutLBSamplingGroup = entityList1[0].LBSamplingGroup;
            JArray samplingGroupList = new JArray();
            foreach (var item2 in entityList1)
            {
                JObject jobj1 = new JObject();
                jobj1.Add("Id", item2.LBSamplingGroup.Id.ToString());
                jobj1.Add("CName", item2.LBSamplingGroup.CName);
                samplingGroupList.Add(jobj1);
            }
            samplingGroupListStr = samplingGroupList.ToString().Replace(Environment.NewLine, "");
            //ZhiFang.LabStar.Common.LogHelp.Debug("LBSamplingGroupListStr:" + entity.LBSamplingGroupListStr);
            return samplingGroupListStr;
        }
        /// <summary>
        /// 定制新增保存组合项目拆分
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public BaseResultDataValue AddLBParItemSplitList(IList<LBParItemSplit> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //先判断组合项目ID是否已经进行过组合项目拆分
            foreach (LBParItemSplit entity in entityList)
            {
                if (entity.ParItem == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("组合项目信息为空,新增保存组合项目拆分关系失败!");
                    return baseResultDataValue;
                }
                if (entity.LBItem == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("检验子项目信息为空,新增保存组合项目拆分关系失败!");
                    return baseResultDataValue;
                }
                if (entity.LBSamplingGroup == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("采样组信息为空,新增保存组合项目拆分关系失败!");
                    return baseResultDataValue;
                }
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                this.Entity = entity;
                baseResultDataValue.success = this.Add();
                if (baseResultDataValue.success == false)
                {
                    baseResultDataValue.ErrorInfo = string.Format("组合项目名称为:{0},子项目名称为:{1},采样组为:{2},新增保存组合项目拆分关系失败!", entity.ParItem.CName, entity.LBItem.CName, entity.LBSamplingGroup.CName);
                    break;
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 定制编辑保存组合项目拆分
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public BaseResultBool EditLBParItemSplitList(IList<LBParItemSplit> entityList)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            foreach (LBParItemSplit entity in entityList)
            {
                LBParItemSplit serverEntity = this.Get(entity.Id);
                if (serverEntity == null)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("获取组合项目拆分关系ID为:{0},组合项目信息为空,编辑保存组合项目拆分关系失败!", entity.Id);
                    break;
                }
                if (entity.LBSamplingGroup == null)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("采样组信息为空,编辑保存组合项目拆分关系失败!");
                    break;
                }
                serverEntity.NewBarCode = entity.NewBarCode;
                serverEntity.LBSamplingGroup = entity.LBSamplingGroup;
                serverEntity.IsAutoUnion = entity.IsAutoUnion;
                serverEntity.DataUpdateTime = DateTime.Now;
                this.Entity = serverEntity;
                baseResultBool.success = this.Edit();
                if (baseResultBool.success == false)
                {
                    baseResultBool.ErrorInfo = string.Format("组合项目名称为:{0},子项目名称为:{1},采样组为:{2},编辑保存组合项目拆分关系失败!", serverEntity.ParItem.CName, serverEntity.LBItem.CName, serverEntity.LBSamplingGroup.CName);
                    break;
                }
            }
            return baseResultBool;
        }

        /// <summary>
        /// 定制按组合项目ID删除组合项目拆分关系
        /// </summary>
        /// <param name="parItemId">组合项目Id</param>
        /// <returns></returns>
        public BaseResultBool DelLBParItemSplitByParItemId(long parItemId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            int result = this.DeleteByHql("From LBParItemSplit lbparitemsplit where lbparitemsplit.ParItem.Id=" + parItemId);
            if (result > 0)
            {
                baseResultBool.success = true;
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "按组合项目ID删除组合项目拆分关系记录数为:" + result;
            }
            return baseResultBool;
        }
    }
}