
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaTestEquipItem : BaseBLL<ReaTestEquipItem>, ZhiFang.IBLL.ReagentSys.Client.IBReaTestEquipItem
    {
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }
        IDReaTestItemDao IDReaTestItemDao { get; set; }
        /// <summary>
        /// 客户端同步LIS的仪器项目关系信息
        /// </summary>
        /// <returns></returns>
        public BaseResultBool SaveSyncLisReaTestEquipItemInfo(string equipId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IList<ReaTestEquipItem> reaList = new List<ReaTestEquipItem>();
            IList<ReaTestEquipLab> equipLabList = new List<ReaTestEquipLab>();
            IList<ReaTestItem> itemList = IDReaTestItemDao.LoadAll();
            ReaTestEquipLab testEquipLab1 = null;
            if (!string.IsNullOrEmpty(equipId))
            {
                reaList = this.SearchListByHQL("TestEquipID=" + equipId);
                testEquipLab1 = IDReaTestEquipLabDao.Get(long.Parse(equipId));
                if (testEquipLab1 != null) equipLabList.Add(testEquipLab1);
            }
            else
            {
                reaList = this.LoadAll();
                equipLabList = IDReaTestEquipLabDao.LoadAll();
            }
            if (!string.IsNullOrEmpty(equipId) && testEquipLab1 == null)
            {
                baseResultBool.success = false;
                baseResultBool.BoolInfo = "获取仪器ID为:" + equipId + ",仪器信息为空!";
                return baseResultBool;
            }
            if (!string.IsNullOrEmpty(equipId) && string.IsNullOrEmpty(testEquipLab1.LisCode))
            {
                baseResultBool.success = false;
                baseResultBool.BoolInfo = "获取仪器ID为:" + equipId + ",lis仪器对照码信息为空!";
                return baseResultBool;
            }

            StringBuilder lisHave = new StringBuilder();
            //foreach (ReaTestEquipItem model in reaList)
            //{
            //    lisHave.Append(" (TestEquipID!=" + model.TestEquipID + " and TestItemID!=" + model.TestItemID + ") and");
            //}
            string hql = "";
            if (lisHave.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                hql = " (" + lisHave.ToString().TrimEnd(trimChars) + ")";
            }
            if (!string.IsNullOrEmpty(equipId) && !string.IsNullOrEmpty(hql))
            {
                hql = hql + " and TestEquipID=" + testEquipLab1.LisCode;
            }
            else if (!string.IsNullOrEmpty(equipId) && string.IsNullOrEmpty(hql))
            {
                hql = " TestEquipID=" + testEquipLab1.LisCode;
            }
            ZhiFang.Common.Log.Log.Debug("SaveSyncLisReaTestEquipItemInfo.hql(从LIS获取仪器项目查询条件为):" + hql);
            IList<ReaTestEquipItem> lisList = DataAccess_SQL.CreateReaTestEquipItemDao_SQL().GetListByHQL(hql);

            if (lisList != null && lisList.Count > 0)
            {
                try
                {
                    int dispOrder = 1;
                    foreach (var entity in lisList)
                    {
                        ReaTestEquipLab testEquipLab2 = null;
                        ReaTestItem testItem2 = null;

                        //先判断是否在仪器信息表及存在LIS对照关系
                        var testEquipLabList2 = equipLabList.Where(p => p.Id == entity.TestEquipID || p.LisCode == entity.TestEquipID.ToString());
                        if (testEquipLabList2 == null || testEquipLabList2.Count() <= 0)
                        {
                            ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:LIS仪器编码为:" + entity.TestEquipID + "不存在检验仪器信息里,仪器项目关系不导入!");
                            continue;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(testEquipLabList2.ElementAt(0).LisCode))
                            {
                                ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:仪器编码为:" + entity.TestEquipID + ",没维护好LIS对照码,仪器项目关系不导入!");
                                continue;
                            }
                            testEquipLab2 = testEquipLabList2.ElementAt(0);
                        }
                        if (testEquipLab2 == null)
                        {
                            ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:LIS仪器编码为:" + entity.TestEquipID + "不存在检验仪器信息里,仪器项目关系不导入!");
                            continue;
                        }

                        //判断检验项目是否存在及维护了LIS对照关系
                        var testItemList2 = itemList.Where(p => p.Id == entity.TestItemID || p.LisCode == entity.TestItemID.ToString());
                        if (testItemList2 == null || testItemList2.Count() <= 0)
                        {
                            ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:项目编码为:" + entity.TestItemID + "不存在检验项目信息里,仪器项目关系不导入!");
                            continue;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(testItemList2.ElementAt(0).LisCode))
                            {
                                ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:项目编码为:" + entity.TestItemID + ",没维护好LIS对照码,仪器项目关系不导入!");
                                continue;
                            }
                            testItem2 = testItemList2.ElementAt(0);
                        }
                        if (testItem2 == null)
                        {
                            ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:项目编码为:" + entity.TestItemID + "不存在检验项目信息里,仪器项目关系不导入!");
                            continue;
                        }

                        var tempList1 = reaList.Where(p => p.TestEquipID == testEquipLab2.Id && p.TestItemID == testItem2.Id);
                        if (tempList1 != null && tempList1.Count() > 0)
                        {
                            //ZhiFang.Common.Log.Log.Error("同步LIS的仪器项目关系信息提示:仪器编码为:" + entity.TestEquipID + "项目编码为:" + entity.TestItemID + ",已存在!");
                            continue;
                        }
                        entity.Id= ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        entity.TestEquipID = testEquipLab2.Id;
                        entity.TestItemID = testItem2.Id;
                        entity.Visible = 1;
                        entity.DataUpdateTime = DateTime.Now;
                        entity.DispOrder = dispOrder;
                        this.Entity = entity;
                        baseResultBool.success = DBDao.Save(this.Entity);//this.Add();
                        if (baseResultBool.success == false)
                        {
                            baseResultBool.ErrorInfo = "同步LIS的仪器项目关系信息保存时出错!";
                            break;
                        }
                        dispOrder = dispOrder + 1;
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.BoolInfo = "同步LIS的仪器项目关系信息失败!";
                    ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo + ex.Message);
                }
            }
            if (baseResultBool.success == true)
            {
                baseResultBool.BoolInfo = "同步LIS的仪器项目关系信息成功!";
                baseResultBool.ErrorInfo = baseResultBool.BoolInfo;
            }
            return baseResultBool;
        }
        public IList<ReaTestEquipItem> SearchNewListByHQL(string where, string reatestitemHql, string sort, int page, int limit)
        {
            IList<ReaTestEquipItem> entityList = new List<ReaTestEquipItem>();
            entityList = ((IDReaTestEquipItemDao)base.DBDao).SearchReaTestEquipItemListByJoinHql(where, reatestitemHql, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaTestEquipItem> SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit)
        {
            EntityList<ReaTestEquipItem> entityList = new EntityList<ReaTestEquipItem>();
            entityList = ((IDReaTestEquipItemDao)base.DBDao).SearchReaTestEquipItemEntityListByJoinHql(where, reatestitemHql, sort, page, limit);
            return entityList;
        }
    }
}