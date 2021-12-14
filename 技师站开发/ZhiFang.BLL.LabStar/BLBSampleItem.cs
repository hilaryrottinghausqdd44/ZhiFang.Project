using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBSampleItem : BaseBLL<LBSampleItem>, ZhiFang.IBLL.LabStar.IBLBSampleItem
    {
        public BaseResultDataValue AddLBSampleItemList(IList<LBSampleItem> entityList, string ofType, bool isHasDel)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数(entityList)不能为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(ofType))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：设置类型参数(ofType)不能为空！";
                return baseResultDataValue;
            }
            string hql = "";
            if (ofType == "of_sampletype")
            {
                hql = "lbsampleitem.LBSampleType.Id=" + entityList[0].LBSampleType.Id;
            }
            else if (ofType == "of_testitem")
            {
                hql = "lbsampleitem.LBItem.Id=" + entityList[0].LBItem.Id;
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：未识别传入参数(ofType)值！";
                return baseResultDataValue;
            }
            IList<LBSampleItem> serverList = ((IDLBSampleItemDao)this.DBDao).GetListByHQL(hql);//this.SearchListByHQL(hql);
            //IList<LBSampleItem> addList = new List<LBSampleItem>();
            //IList<LBSampleItem> delList = new List<LBSampleItem>();
            //找出本次新增的关系集合
            bool result = true;
            foreach (LBSampleItem entity in entityList)
            {
                var tempList = serverList.Where(p => p.LBSampleType.Id == entity.LBSampleType.Id && p.LBItem.Id == entity.LBItem.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    entity.DataAddTime = DateTime.Now;
                    entity.DataUpdateTime = DateTime.Now;
                    this.Entity = entity;
                    result = this.Add();
                    if (result == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = string.Format("新增样本类型名称为:{0},检验项目名称为:{1},的项目样本类型关系失败!", entity.LBSampleType.CName, entity.LBItem.CName);
                        break;
                    }
                    //addList.Add(entity);
                }
            }
            if (baseResultDataValue.success == false)
                return baseResultDataValue;

            //找出本次需要被删除的关系集合
            foreach (LBSampleItem entity in serverList)
            {
                var tempList = entityList.Where(p => p.LBSampleType.Id == entity.LBSampleType.Id && p.LBItem.Id == entity.LBItem.Id);
                if (tempList == null || tempList.Count() <= 0)
                {
                    this.Entity = entity;
                    result = this.Remove(entity.Id);
                    if (result == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = string.Format("删除样本类型名称为:{0},检验项目名称为:{1},关系Id为:{2},的项目样本类型关系失败!", entity.LBSampleType.CName, entity.LBItem.CName, entity.Id);
                        break;
                    }
                    //delList.Add(entity);
                }
            }
            return baseResultDataValue;
        }

    }
}