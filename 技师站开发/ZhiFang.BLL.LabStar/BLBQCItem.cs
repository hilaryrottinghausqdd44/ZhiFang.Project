using FastReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBQCItem : BaseBLL<LBQCItem>, ZhiFang.IBLL.LabStar.IBLBQCItem
    {
        IDLBEquipItemDao IDLBEquipItemDao { get; set; }
        IDLisQCDataDao IDLisQCDataDao { get; set; }
        IDLBQCItemTimeDao IDLBQCItemTimeDao { get; set; }
        IDLBQCMaterialDao IDLBQCMaterialDao { get; set; }
        IDLBEquipDao IDLBEquipDao { get; set; }
        IDLBQCMatTimeDao IDLBQCMatTimeDao { get; set; }
        IDLBQCPrintTemplateDao IDLBQCPrintTemplateDao { get; set; }
        IDLisQCCommentsDao IDLisQCCommentsDao { get; set; }
        IDLBItemDao IDLBItemDao { get; set; }

        public static FastReport.EnvironmentSettings eSet = new EnvironmentSettings();

        /// <summary>
        /// 查询质控项目信息（FetchJoin）
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<LBQCItem> QueryLBQCItem(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBQCItemDao).QueryLBQCItemDao(strHqlWhere, Order, start, count);
        }

        /// <summary>
        /// 多浓度质控查询
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="qCMModule"></param>
        /// <param name="qCMGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public List<MultipleConcentrationQCM> GetMultipleConcentrationQCM(long EquipId, long itemId, string qCMModule, string qCMGroup, DateTime startDate, DateTime endDate)
        {
            List<MultipleConcentrationQCM> multipleConcentrationQCMs = new List<MultipleConcentrationQCM>();
            string dbwhere = "LBItem.Id=" + itemId + " and LBQCMaterial.EquipModule=" + (qCMModule == null ? "''" : qCMModule) + " and LBQCMaterial.QCGroup=" + qCMGroup + " and LBQCMaterial.LBEquip.Id=" + EquipId + " and IsUse=1";
            IList<LBQCItem> lBQCItems = DBDao.GetListByHQL(dbwhere);
            if (lBQCItems.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 未找到质控项目 itemId=" + itemId);
                return multipleConcentrationQCMs;
            }
            List<long> qcItemIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                qcItemIds.Add(a.Id);

            });

            //查询质控项目数据
            string lisQCDataWhere = "QCItemID in (" + string.Join(",", qcItemIds) + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            IList<LisQCData> lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count);
            //查询 质控项目时效
            string qcItemTimeWhere = "QCItemID in (" + string.Join(",", qcItemIds) + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);
            foreach (var item in lBQCItems.OrderBy(a => a.LBItem.DispOrder))
            {
                MultipleConcentrationQCM multipleConcentrationQCM = new MultipleConcentrationQCM();
                multipleConcentrationQCM = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCM, LBQCItem>(item);
                //项目数据计算
                var itemDatas = lisQCDatas.Where(a => a.LBQCItem.Id == item.Id);
                if (itemDatas.Count() > 0)
                {
                    multipleConcentrationQCM.CalculationTarget = itemDatas.Average(a => a.QuanValue);
                    if (itemDatas.Count() >= 3)
                    {
                        //平均值
                        double valueAvg = itemDatas.Average(p => p.QuanValue);
                        //求和
                        double valueSum = itemDatas.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
                        //取平方根 
                        double SD = Math.Sqrt(valueSum / (itemDatas.Count() - 1));
                        multipleConcentrationQCM.CalculationSD = SD;
                        if (multipleConcentrationQCM.CalculationTarget != 0)
                        {
                            int CCCV = (int)(SD / multipleConcentrationQCM.CalculationTarget * 1) * 100;
                            multipleConcentrationQCM.CalculationCCV = (CCCV / 100.0).ToString();
                        }
                    }
                    else
                    {
                        ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 质控项目数据小于3不计算标准差");
                    }
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 质控项目数据为空");
                }
                //时效
                var qcitemTimes = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.Id);
                if (qcitemTimes.Count() <= 0)
                {
                    multipleConcentrationQCMs.Add(multipleConcentrationQCM);
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCM 质控项目id=" + item.Id + " 没有质控时效");
                }
                foreach (var qcitemTime in qcitemTimes)
                {
                    var multipleConcentrationQCM1 = new MultipleConcentrationQCM();
                    multipleConcentrationQCM1 = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCM, MultipleConcentrationQCM>(multipleConcentrationQCM);
                    multipleConcentrationQCM1.CCV = qcitemTime.CCV;
                    multipleConcentrationQCM1.SD = qcitemTime.SD;
                    multipleConcentrationQCM1.Target = qcitemTime.Target;
                    multipleConcentrationQCM1.LotNo = qcitemTime.LBQCMatTime.LotNo;
                    multipleConcentrationQCM1.StartDate = qcitemTime.StartDate;
                    multipleConcentrationQCM1.EndDate = qcitemTime.EndDate;
                    multipleConcentrationQCM1.Manu = qcitemTime.LBQCMatTime.Manu;
                    multipleConcentrationQCM1.LBQCItemTime = qcitemTime;
                    multipleConcentrationQCMs.Add(multipleConcentrationQCM1);
                }

            }
            return multipleConcentrationQCMs.OrderBy(a => a.LBQCMaterial.DispOrder).ToList();
        }
        /// <summary>
        /// 多浓度质控树列表
        /// </summary>
        public BaseResultTree<MultipleConcentrationQCMTree> GetMultipleConcentrationQCMTree()
        {
            BaseResultTree<MultipleConcentrationQCMTree> baseResultTree = new BaseResultTree<MultipleConcentrationQCMTree>() { Tree = new List<tree<MultipleConcentrationQCMTree>>() };
            var lBQCItems = DBDao.GetListByHQL("IsUse=1");
            List<long> equipIDs = new List<long>();
            List<long> qcmIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                equipIDs.Add(a.LBQCMaterial.LBEquip.Id);
                qcmIds.Add(a.LBQCMaterial.Id);
            });
            //仪器项目查询
            var lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
            if (lBEquipItems.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器项目 where : EquipID in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //仪器查询
            var lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrder);
            if (lBEquips.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器查询 where : Id in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //质控查询
            var lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmIds) + ")").OrderBy(a => a.DispOrder);
            if (lBQCMaterials.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到质控查询 where : Id in (" + string.Join(", ", qcmIds) + ")");
                return baseResultTree;
            }
            foreach (var item1 in lBEquips.OrderBy(a => a.DispOrder))
            {
                var lBQCMaterial = lBQCMaterials.Where(a => a.LBEquip.Id == item1.Id).OrderBy(a => a.DispOrder).GroupBy(a => new { a.EquipModule, a.QCGroup });
                foreach (var item in lBQCMaterial)
                {
                    tree<MultipleConcentrationQCMTree> tree = new tree<MultipleConcentrationQCMTree>();
                    tree.tid = item1.Id.ToString();
                    tree.text = item1.CName;
                    tree.expanded = true;
                    tree.leaf = true;
                    tree.iconCls = tree.leaf ? "orgImg16" : "orgsImg16";
                    List<tree<MultipleConcentrationQCMTree>> listtree = new List<tree<MultipleConcentrationQCMTree>>();
                    List<long> itemids = new List<long>(); //记录同一组的质控物项目 加入过树的
                    foreach (var qcm in item)
                    {
                        foreach (var equipItem in lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder))
                        {
                            var lbQcitem = lBQCItems.Where(a => a.LBItem.Id == equipItem.LBItem.Id && a.LBQCMaterial.Id == qcm.Id && a.LBQCMaterial.EquipModule == qcm.EquipModule && a.LBQCMaterial.QCGroup == qcm.QCGroup);
                            //排除加入过的项目
                            if (lbQcitem.Count() > 0 && itemids.Count(a => a == lbQcitem.First().LBItem.Id) <= 0)
                            {
                                itemids.Add(lbQcitem.First().LBItem.Id);
                                tree<MultipleConcentrationQCMTree> tree1 = new tree<MultipleConcentrationQCMTree>();
                                tree1.tid = lbQcitem.First().LBItem.Id.ToString();
                                tree1.text = lbQcitem.First().LBItem.CName;
                                tree1.pid = qcm.LBEquip.Id.ToString();
                                tree1.expanded = true;
                                tree1.leaf = true;
                                tree1.value = new MultipleConcentrationQCMTree() { EquipId = lbQcitem.First().LBQCMaterial.LBEquip.Id, EquipModule = lbQcitem.First().LBQCMaterial.EquipModule, QCGroup = lbQcitem.First().LBQCMaterial.QCGroup };
                                tree1.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                                listtree.Add(tree1);
                            }
                        }
                    }
                    tree.Tree = listtree.ToArray();
                    baseResultTree.Tree.Add(tree);
                }
            }
            return baseResultTree;
        }

        /// <summary>
        /// 日指控查询
        /// </summary>
        /// <param name="equipID"></param>
        /// <param name="qCMatID"></param>
        /// <param name="dateTime"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<LBQCItemVO> GetQCDays(long equipID, long qCMatID, DateTime dateTime, int page, int limit)
        {
            List<LBQCItemVO> entityList = new List<LBQCItemVO>();
            //查询质控物的项目
            IList<LBQCItem> entityListLBQCItem = DBDao.GetListByHQL("QCMatID=" + qCMatID + " and IsUse=1");
            if (entityListLBQCItem.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCDays 未找到质控项目 QCMatId=" + qCMatID);
                return entityList;
            }
            //查询仪器项目
            List<long> itemids = new List<long>(); //项目id
            List<long> QCItemIDs = new List<long>();//质控项目id
            entityListLBQCItem.ToList().ForEach(a =>
            {
                itemids.Add(a.LBItem.Id);
                QCItemIDs.Add(a.Id);
            });
            IList<LBEquipItem> lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID=" + equipID + " and ItemID in (" + string.Join(",", itemids) + ")");
            if (lBEquipItems.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCDays 未找到仪器项目 where : EquipID = " + equipID + " and ItemID in (" + string.Join(", ", itemids) + ")");
                return entityList;
            }
            var equipItems = lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder);
            //按照排好的顺序放入数据
            List<LBQCItemVO> lBQCItemVOs = new List<LBQCItemVO>();
            foreach (var item in equipItems)
            {
                var lbQcitem = entityListLBQCItem.Where(a => a.LBItem.Id == item.LBItem.Id);
                if (lbQcitem.Count() > 0)
                {
                    LBQCItemVO lBQCItemVO = new LBQCItemVO();
                    lBQCItemVO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItemVO, LBQCItem>(lbQcitem.First());
                    lBQCItemVO.ItemName = lbQcitem.First().LBItem.CName;
                    lBQCItemVO.ItemId = lbQcitem.First().LBItem.Id.ToString();
                    lBQCItemVO.ItemSName = lbQcitem.First().LBItem.SName;
                    lBQCItemVO.Prec = lbQcitem.First().LBItem.Prec;
                    lBQCItemVOs.Add(lBQCItemVO);
                }
            }

            //查询质控项目数据
            string lisQCDataWhere = "QCItemID in (" + string.Join(",", QCItemIDs) + ") and ReceiveTime>='" + dateTime.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + dateTime.ToString("yyyy-MM-dd") + " 23:59:59'";
            IList<LisQCData> lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCDays 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count);
            //查询 质控项目时效
            string qcItemTimeWhere = "QCItemID in (" + string.Join(",", QCItemIDs) + ") and StartDate<='" + dateTime.ToString("yyyy-MM-dd") + "' and (EndDate>='" + dateTime.ToString("yyyy-MM-dd") + " 23:59:59' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCDays 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);

            foreach (var item in lBQCItemVOs)
            {
                //时效
                var lBQCItemTime = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.Id);
                if (lBQCItemTime.Count() > 0)
                {
                    item.lBQCItemTime = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItemTimeVO, LBQCItemTime>(lBQCItemTime.First());
                }
            }
            foreach (var item in lBQCItemVOs)
            {
                LBQCItemVO lBQCItemVO = new LBQCItemVO();
                lBQCItemVO = item;
                //数据
                var lisQCData = lisQCDatas.Where(a => a.LBQCItem.Id == item.Id);
                if (lisQCData.Count() > 0)
                {
                    lisQCData.OrderBy(a => a.ReceiveTime).ToList().ForEach(a =>
                    {
                        lBQCItemVO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItemVO, LBQCItemVO>(item);
                        lBQCItemVO.lisQCData = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCDataVO, LisQCData>(a);
                        entityList.Add(lBQCItemVO);
                    });
                }
                else
                {
                    entityList.Add(lBQCItemVO);
                }
            }


            return entityList;
        }
        /// <summary>
        /// 月质控树列表
        /// </summary>
        /// <returns></returns>
        public BaseResultTree GetQCMothTree()
        {
            BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
            var lBQCItems = DBDao.GetListByHQL("IsUse=1");
            List<long> equipIDs = new List<long>();
            List<long> qcmIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                equipIDs.Add(a.LBQCMaterial.LBEquip.Id);
                qcmIds.Add(a.LBQCMaterial.Id);
            });
            //仪器项目查询
            var lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder);
            if (lBEquipItems.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothTree 未找到仪器项目 where : EquipID in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //仪器查询
            var lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrder);
            if (lBEquips.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothTree 未找到仪器查询 where : Id in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //质控查询
            var lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmIds) + ")").OrderBy(a => a.DispOrder);
            if (lBQCMaterials.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothTree 未找到质控查询 where : Id in (" + string.Join(", ", qcmIds) + ")");
                return baseResultTree;
            }
            foreach (var item in lBEquips)
            {
                tree tree = new tree();
                tree.tid = item.Id.ToString();
                tree.text = item.CName;
                tree.expanded = true;
                tree.leaf = true;
                tree.iconCls = tree.leaf ? "orgImg16" : "orgsImg16";
                List<tree> qcmtrees = new List<tree>();

                foreach (var qcmtree in lBQCMaterials.Where(a => a.LBEquip.Id == item.Id))
                {
                    tree tree1 = new tree();
                    tree1.tid = qcmtree.Id.ToString();
                    tree1.text = qcmtree.CName;
                    tree1.pid = item.Id.ToString();
                    tree1.expanded = true;
                    tree1.leaf = true;
                    tree1.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                    List<tree> itemtree = new List<tree>();
                    List<long> itemids = new List<long>();
                    foreach (var equipItem in lBEquipItems)
                    {
                        var lbQcitem = lBQCItems.Where(a => a.LBItem.Id == equipItem.LBItem.Id && a.LBQCMaterial.Id == qcmtree.Id && a.LBQCMaterial.LBEquip.Id == item.Id);
                        if (lbQcitem.Count() > 0 && itemids.Count(a => a == lbQcitem.First().LBItem.Id) <= 0)
                        {
                            itemids.Add(lbQcitem.First().LBItem.Id);
                            tree tree2 = new tree();
                            tree2.tid = lbQcitem.First().Id.ToString();
                            tree2.text = lbQcitem.First().LBItem.CName;
                            tree2.pid = item.Id.ToString();
                            tree2.expanded = true;
                            tree2.leaf = true;
                            tree2.Para = lbQcitem.First().QCDataType;
                            tree2.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                            itemtree.Add(tree2);
                        }
                    }
                    tree1.Tree = itemtree;
                    qcmtrees.Add(tree1);
                }
                tree.Tree = qcmtrees;
                baseResultTree.Tree.Add(tree);
            }
            return baseResultTree;
        }
        /// <summary>
        /// 多浓度质控树筛选列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public IList<LBQCItem> SearchLBQCItemByHQL(IList<LBQCItem> list)
        {
            List<LBQCItem> lBQCItems = new List<LBQCItem>();
            List<LBQCItem> lBQCItemsOrd = new List<LBQCItem>();
            list.GroupBy(a => new { a.LBQCMaterial.LBEquip.Id, a.LBQCMaterial.EquipModule, a.LBQCMaterial.QCGroup, dd = a.LBItem.Id }).ToList()
                .ForEach(a =>
                {
                    lBQCItems.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItem, LBQCItem>(a.First()));
                });
            list.ToList().ForEach(w =>
            {
                if (lBQCItems.Count(a => a.Id == w.Id) > 0)
                {
                    lBQCItemsOrd.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItem, LBQCItem>(lBQCItems.Where(a => a.Id == w.Id).First()));
                }
            });

            return lBQCItemsOrd;
        }
        /// <summary>
        /// 月质控项目查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="itemId"></param>
        /// <param name="qCMModule"></param>
        /// <param name="qCMGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<MultipleConcentrationQCM> GetQCMothItem(long QCItemId, DateTime startDate, DateTime endDate)
        {
            List<MultipleConcentrationQCM> multipleConcentrationQCMs = new List<MultipleConcentrationQCM>();
            LBQCItem lBQCItem = DBDao.Get(QCItemId);
            if (lBQCItem == null)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 未找到质控项目 Id=" + QCItemId);
                return multipleConcentrationQCMs;
            }
            //查询质控项目数据
            string lisQCDataWhere = "QCItemID =" + lBQCItem.Id + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            IList<LisQCData> lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count);
            //查询 质控项目时效
            string qcItemTimeWhere = "QCItemID=" + lBQCItem.Id + " and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);
            MultipleConcentrationQCM multipleConcentrationQCM = new MultipleConcentrationQCM();
            multipleConcentrationQCM = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCM, LBQCItem>(lBQCItem);
            //项目数据计算
            var itemDatas = lisQCDatas.Where(a => a.LBQCItem.Id == lBQCItem.Id);
            if (itemDatas.Count() > 0)
            {
                multipleConcentrationQCM.CalculationTarget = itemDatas.Average(a => a.QuanValue);
                if (itemDatas.Count() >= 3)
                {
                    //平均值
                    double valueAvg = itemDatas.Average(p => p.QuanValue);
                    //求和
                    double valueSum = itemDatas.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
                    //取平方根 
                    double SD = Math.Sqrt(valueSum / (itemDatas.Count() - 1));
                    multipleConcentrationQCM.CalculationSD = SD;
                    if (multipleConcentrationQCM.CalculationTarget != 0)
                    {
                        int CCCV = (int)(SD / multipleConcentrationQCM.CalculationTarget * 1) * 100;
                        multipleConcentrationQCM.CalculationCCV = (CCCV / 100.0).ToString();
                    }
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 质控项目数据小于3不计算标准差");
                }
            }
            else
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 质控项目数据为空");
            }
            //时效
            var qcitemTimes = lBQCItemTimes.Where(a => a.LBQCItem.Id == lBQCItem.Id).OrderBy(a => a.StartDate);
            if (qcitemTimes.Count() <= 0)
            {
                multipleConcentrationQCMs.Add(multipleConcentrationQCM);
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMothItem 质控项目id=" + lBQCItem.Id + " 没有质控时效");
            }
            foreach (var qcitemTime in qcitemTimes)
            {
                var multipleConcentrationQCM1 = new MultipleConcentrationQCM();
                multipleConcentrationQCM1 = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCM, MultipleConcentrationQCM>(multipleConcentrationQCM);
                multipleConcentrationQCM1.CCV = qcitemTime.CCV;
                multipleConcentrationQCM1.SD = qcitemTime.SD;
                multipleConcentrationQCM1.Target = qcitemTime.Target;
                multipleConcentrationQCM1.LotNo = qcitemTime.LBQCMatTime.LotNo;
                multipleConcentrationQCM1.StartDate = qcitemTime.StartDate;
                multipleConcentrationQCM1.EndDate = qcitemTime.EndDate;
                multipleConcentrationQCM1.Manu = qcitemTime.LBQCMatTime.Manu;
                multipleConcentrationQCM1.LBQCItemTime = qcitemTime;
                multipleConcentrationQCMs.Add(multipleConcentrationQCM1);
            }

            return multipleConcentrationQCMs;
        }
        /// <summary>
        /// 多浓度质控对比信息
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetMultipleConcentrationQCMCompareInfo(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            string lisQCDataWhere = "LBQCItem.Id in (" + QCItemIds + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            var lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMCompareInfo 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ReceiveDate");
            dataTable.Columns.Add("ReceiveTime");
            dataTable.Columns.Add("QCDataIds");
            dataTable.Columns.Add("LoseTypes");
            foreach (var item in lisQCDatas.GroupBy(a => a.LBQCItem.Id))
            {
                dataTable.Columns.Add(item.Key.ToString());
            }
            List<List<List<LisQCData>>> lbqciteminfos = new List<List<List<LisQCData>>>();//记录需要追加到表格的其他批次数据 [需要加入到表格的第几行][第几个批次的][数据]
            SortedList<DateTime, List<long>> lbqcitemiDs = new SortedList<DateTime, List<long>>();//记录已经添加过的项目
            //lbqciteminfos.Add(new List<LisQCData>());
            List<int> dataTableinsertindex = new List<int>();//在哪一行添加新数据
            int index = 0;
            for (; startDate < endDate.AddDays(1); startDate = startDate.AddDays(1))
            {
                //如果质控项目数据有匹配的数据加入
                var lisQCData = lisQCDatas.Where(a => a.ReceiveTime >= startDate && a.ReceiveTime <= DateTime.Parse(startDate.ToString("yyyy-MM-dd 23:59:59")));
                DataRow dataRow = dataTable.NewRow();
                if (lisQCData.Count() > 0)
                {
                    var datagroup = lisQCData.GroupBy(gp => new { gp.LBQCItem.LBQCMaterial.LBEquip.Id, gp.LBQCItem.LBQCMaterial.EquipModule, gp.LBQCItem.LBQCMaterial.QCGroup });
                    foreach (var data in datagroup)
                    {
                        index++;
                        dataRow["ReceiveDate"] = data.OrderBy(ord => ord.ReceiveTime).First().ReceiveTime.ToString("yyyy-MM-dd");
                        dataRow["ReceiveTime"] = data.OrderBy(ord => ord.ReceiveTime).First().ReceiveTime.ToString("HH:mm:ss");
                        List<string> qcids = new List<string>();
                        List<string> LoseTypes = new List<string>();
                        bool flag = false;
                        List<List<LisQCData>> piciQcs = new List<List<LisQCData>>();
                        foreach (var item in data.OrderBy(od => od.DispOrder).ThenBy(od1 => od1.ReceiveTime))
                        {
                            //如果这天记录过这个质控项目则代表，当前这个数据有多个批次的
                            if (lbqcitemiDs.Count(f => f.Key == startDate && f.Value.Count(idscount => idscount == item.LBQCItem.Id) > 0) > 0)
                            {
                                //这天这个项目已经加入过几次了，加入一次代表一个批次
                                var count = lbqcitemiDs[startDate].Count(idc => idc == item.LBQCItem.Id);
                                //检查插入表格数组的 数组个数是否能够满足批次个数
                                if (piciQcs.Count < count)
                                {
                                    for (int i = 0; i < count - piciQcs.Count; i++)
                                    {
                                        piciQcs.Add(new List<LisQCData>());
                                    }
                                }
                                flag = true; //批次标记
                                //把这个项目这次批次的数据放入需要插入表格的数组 数组下标对应为第几批次
                                piciQcs[count - 1].Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item));

                                lbqcitemiDs[startDate].Add(item.LBQCItem.Id);
                                continue;
                            }
                            //记录插入的数据
                            if (!lbqcitemiDs.ContainsKey(startDate))
                            {
                                lbqcitemiDs.Add(startDate, new List<long>());
                            }
                            lbqcitemiDs[startDate].Add(item.LBQCItem.Id);
                            //插入当前行
                            dataRow[item.LBQCItem.Id.ToString()] = item.ReportValue;
                            qcids.Add("\"" + item.LBQCItem.Id.ToString() + "\":\"" + item.Id + "\"");
                            LoseTypes.Add("\"" + item.LBQCItem.Id.ToString() + "\":\"" + item.LoseType + "\"");
                        }
                        //如果为true 则表示当前数据为批次数据
                        if (flag)
                        {
                            if (lbqciteminfos.Count <= index)
                            {
                                for (int i = 0; i <= index; i++)
                                {
                                    lbqciteminfos.Add(new List<List<LisQCData>>());
                                }
                            }
                            //记录需要插入行的批次数据
                            lbqciteminfos[index] = piciQcs;
                            //记录需要插入的行
                            dataTableinsertindex.Add(index);
                        }
                        dataRow["QCDataIds"] = "{" + string.Join(",", qcids) + "}";
                        dataRow["LoseTypes"] = "{" + string.Join(",", LoseTypes) + "}";
                        dataTable.Rows.Add(dataRow.ItemArray);
                    }
                }
                else
                {
                    index++;
                    dataRow["ReceiveDate"] = startDate.ToString("yyyy-MM-dd");
                    dataTable.Rows.Add(dataRow);
                }
            }
            //批次数据插入表格
            int addindex = 0; //表格行自增索引
            for (int i = 0; i < dataTableinsertindex.Count; i++)
            {
                foreach (var item in lbqciteminfos[dataTableinsertindex[i]])
                {
                    List<string> qcids = new List<string>();
                    List<string> LoseTypes = new List<string>();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReceiveDate"] = lbqciteminfos[dataTableinsertindex[i]].First().First().ReceiveTime.ToString("yyyy-MM-dd");
                    dataRow["ReceiveTime"] = lbqciteminfos[dataTableinsertindex[i]].First().First().ReceiveTime.ToString("HH:mm:ss");
                    foreach (var qc in item)
                    {
                        dataRow[qc.LBQCItem.Id.ToString()] = qc.ReportValue;
                        qcids.Add('"' + qc.LBQCItem.Id.ToString() + "\":\"" + qc.Id + '"');
                        LoseTypes.Add('"' + qc.LBQCItem.Id.ToString() + "\":\"" + qc.LoseType + '"');
                    }

                    dataRow["QCDataIds"] = "{" + string.Join(",", qcids) + "}";
                    dataRow["LoseTypes"] = "{" + string.Join(",", LoseTypes) + "}";
                    dataTable.Rows.InsertAt(dataRow, dataTableinsertindex[i] + addindex);
                    addindex++;
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 多浓度质控详细信息
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<MultipleConcentrationQCMInfoFull> GetMultipleConcentrationQCMInfoFull(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            string lisQCDataWhere = "LBQCItem.Id in (" + QCItemIds + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            var lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere)
                .OrderBy(a => a.ReceiveTime).ThenBy(a => a.LBQCItem.LBQCMaterial.DispOrder);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMInfoFull 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count());
            //查询 质控项目时效
            string qcItemTimeWhere = "LBQCItem.Id in (" + QCItemIds + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMInfoFull 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);
            List<MultipleConcentrationQCMInfoFull> multipleConcentrationQCMInfoFulls = new List<MultipleConcentrationQCMInfoFull>();
            foreach (var item in lisQCDatas)
            {
                MultipleConcentrationQCMInfoFull multipleConcentrationQCMInfoFull = new MultipleConcentrationQCMInfoFull();
                multipleConcentrationQCMInfoFull = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCMInfoFull, LisQCData>(item);
                var lBQCItemTime = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.LBQCItem.Id && a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || a.EndDate.HasValue));
                if (lBQCItemTime.Count() > 0)
                {
                    multipleConcentrationQCMInfoFull.lBQCItemTime = lBQCItemTime.First();
                }
                multipleConcentrationQCMInfoFulls.Add(multipleConcentrationQCMInfoFull);
            }

            return multipleConcentrationQCMInfoFulls;
        }
        /// <summary>
        /// 仪器日指控仪器列表查询
        /// </summary>
        public DataTable SearchEquipDayQCM()
        {
            IList<LBEquip> lBEquips = IDLBEquipDao.LoadAll();
            List<long> eqids = new List<long>();
            lBEquips.ToList().ForEach(a => eqids.Add(a.Id));
            IList<LBQCItem> lBQCItems = DBDao.GetListByHQL("LBQCMaterial.LBEquip.Id in (" + string.Join(",", eqids) + ") and IsUse=1");
            List<LBQCMaterial> lBQCMaterials = new List<LBQCMaterial>();
            foreach (var item in lBQCItems.GroupBy(a => a.LBQCMaterial.Id))
            {
                lBQCMaterials.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCMaterial, LBQCMaterial>(item.First().LBQCMaterial));
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("EquipId");
            dataTable.Columns.Add("EquipName");
            dataTable.Columns.Add("EquipGroup");
            dataTable.Columns.Add("EquipModel");
            foreach (var item in lBEquips.OrderBy(a => a.DispOrder))
            {
                var gpEqs = lBQCMaterials.Where(a => a.LBEquip.Id == item.Id).GroupBy(gp => new { gp.EquipModule, gp.QCGroup });
                foreach (var qcm in gpEqs)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["EquipId"] = item.Id;
                    dataRow["EquipName"] = item.CName;
                    dataRow["EquipGroup"] = qcm.First().QCGroup;
                    dataRow["EquipModel"] = qcm.First().EquipModule;
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
        /// <summary>
        /// 仪器日指控对比信息查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="EquipModel"></param>
        /// <param name="EquipGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable SearchEquipDayQCMList(long EquipId, string EquipModel, string EquipGroup, DateTime startDate, DateTime endDate)
        {
            string lbqcmaterwhere = "LBEquip.Id=" + EquipId + " and EquipModule='" + EquipModel + "' and QCGroup='" + EquipGroup + "'";
            IList<LBQCMaterial> lBQCMaterials = IDLBQCMaterialDao.GetListByHQL(lbqcmaterwhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCMList 质控物查询 where = " + lbqcmaterwhere + " count=" + lBQCMaterials.Count);

            List<long> qcmids = new List<long>();
            lBQCMaterials.ToList().ForEach(a => qcmids.Add(a.Id));
            IList<LBQCItem> lBQCItems = DBDao.GetListByHQL("LBQCMaterial.Id in (" + string.Join(",", qcmids) + ") and IsUse=1");
            List<long> qcitemids = new List<long>();
            List<long> itemids = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                qcitemids.Add(a.Id);
                itemids.Add(a.LBItem.Id);
            });
            //仪器项目排序
            IList<LBEquipItem> lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID=" + EquipId + " and ItemID in (" + string.Join(",", itemids) + ")");
            if (lBEquipItems.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCMList 未找到仪器项目 where : EquipID = " + EquipId + " and ItemID in (" + string.Join(", ", itemids) + ")");
            }
            var equipItems = lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
            List<LBQCItem> lBQCItems1 = new List<LBQCItem>();
            foreach (var item in equipItems)
            {
                foreach (var itemqc in lBQCItems.Where(a => a.LBItem.Id == item.LBItem.Id))
                {
                    lBQCItems1.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItem, LBQCItem>(itemqc));
                }
            }
            return SearchEquipDayQCMList(string.Join(",", qcitemids), lBQCItems1, startDate, endDate);
        }
        /// <summary>
        /// 仪器日指控对比信息查询 根据质控项目id查询
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable SearchEquipDayQCMList(string QCItemIds, List<LBQCItem> lBQCItems, DateTime startDate, DateTime endDate)
        {
            string lisQCDataWhere = "LBQCItem.Id in (" + QCItemIds + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            var lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere).OrderBy(a => a.LBQCItem.LBQCMaterial.DispOrder);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMCompareInfo 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count());
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ReceiveDate");
            dataTable.Columns.Add("ReceiveTime");
            dataTable.Columns.Add("QCDataIds");
            dataTable.Columns.Add("LoseTypes");
            dataTable.Columns.Add("ItemId");
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemSame");
            dataTable.Columns.Add("ItemPrec");
            foreach (var item in lisQCDatas.GroupBy(a => a.LBQCItem.LBQCMaterial.Id))
            {
                dataTable.Columns.Add(item.Key.ToString());
            }
            foreach (var item in lBQCItems.GroupBy(igp => igp.LBQCMaterial.Id))
            {
                dataTable.Columns.Add(item.Key.ToString() + "_LBQCItemID");
                dataTable.Columns.Add(item.Key.ToString() + "_QCType");
            }
            List<List<List<LisQCData>>> lbqciteminfos = new List<List<List<LisQCData>>>();//记录需要追加到表格的其他批次数据 [需要加入到表格的第几行][第几个批次的][数据]
            SortedList<long, List<long>> lbqcitemiDs = new SortedList<long, List<long>>();//记录已经添加过的项目
            //lbqciteminfos.Add(new List<LisQCData>());
            List<int> dataTableinsertindex = new List<int>();//在哪一行添加新数据
            int index = 0;
            foreach (var lBQCItem in lBQCItems.GroupBy(igp => igp.LBItem.Id))
            {
                bool isqcdata = false;
                //按照项目分组
                DataRow dataRow = dataTable.NewRow();
                List<string> qcids = new List<string>();
                List<string> LoseTypes = new List<string>();
                //如果质控项目数据有匹配的数据加入
                var lisQCData = lisQCDatas.Where(a => a.LBQCItem.LBItem.Id == lBQCItem.Key && a.ReceiveTime >= startDate && a.ReceiveTime <= DateTime.Parse(endDate.ToString("yyyy-MM-dd 23:59:59")));

                if (lisQCData.Count() > 0)
                {
                    isqcdata = true;
                    index++;
                    //  foreach (var QCItem in lBQCItem)
                    //{
                    dataRow["ReceiveDate"] = lisQCData.OrderBy(ord => ord.ReceiveTime).First().ReceiveTime.ToString("yyyy-MM-dd");
                    dataRow["ReceiveTime"] = lisQCData.OrderBy(ord => ord.ReceiveTime).First().ReceiveTime.ToString("HH:mm:ss");
                    dataRow["ItemId"] = lisQCData.First().LBQCItem.LBItem.Id;
                    dataRow["ItemName"] = lisQCData.First().LBQCItem.LBItem.CName;
                    dataRow["ItemSame"] = lisQCData.First().LBQCItem.LBItem.SName;
                    dataRow["ItemPrec"] = lisQCData.First().LBQCItem.LBItem.Prec;

                    bool flag = false;
                    List<List<LisQCData>> piciQcs = new List<List<LisQCData>>();
                    foreach (var item in lisQCData.OrderBy(od => od.DispOrder).ThenBy(a => a.ReceiveTime))
                    {
                        //如果这天记录过这个质控项目则代表，当前这个数据有多个批次的
                        if (lbqcitemiDs.Count(f => f.Key == item.LBQCItem.LBQCMaterial.Id && f.Value.Count(idscount => idscount == item.LBQCItem.Id) > 0) > 0)
                        {
                            //这天这个项目已经加入过几次了，加入一次代表一个批次
                            var count = lbqcitemiDs[item.LBQCItem.LBQCMaterial.Id].Count(idc => idc == item.LBQCItem.Id);
                            //检查插入表格数组的 数组个数是否能够满足批次个数
                            if (piciQcs.Count < count)
                            {
                                for (int i = 0; i < count - piciQcs.Count; i++)
                                {
                                    piciQcs.Add(new List<LisQCData>());
                                }
                            }
                            flag = true; //批次标记
                                         //把这个项目这次批次的数据放入需要插入表格的数组 数组下标对应为第几批次
                            piciQcs[count - 1].Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item));

                            lbqcitemiDs[item.LBQCItem.LBQCMaterial.Id].Add(item.LBQCItem.Id);
                            continue;
                        }
                        //记录插入的数据
                        if (!lbqcitemiDs.ContainsKey(item.LBQCItem.LBQCMaterial.Id))
                        {
                            lbqcitemiDs.Add(item.LBQCItem.LBQCMaterial.Id, new List<long>());
                        }
                        lbqcitemiDs[item.LBQCItem.LBQCMaterial.Id].Add(item.LBQCItem.Id);
                        //插入当前行
                        dataRow[item.LBQCItem.LBQCMaterial.Id.ToString()] = item.ReportValue;
                        qcids.Add("\"" + item.LBQCItem.Id.ToString() + "\":\"" + item.Id + "\"");
                        LoseTypes.Add("\"" + item.LBQCItem.LBQCMaterial.Id.ToString() + "\":\"" + item.LoseType + "\"");

                    }
                    foreach (var qcItem in lBQCItem)
                    {
                        dataRow[qcItem.LBQCMaterial.Id.ToString() + "_LBQCItemID"] = qcItem.Id;
                        dataRow[qcItem.LBQCMaterial.Id.ToString() + "_QCType"] = qcItem.QCDataType;
                    }

                    //如果为true 则表示当前数据为批次数据
                    if (flag)
                    {
                        if (lbqciteminfos.Count <= index)
                        {
                            for (int i = 0; i <= index; i++)
                            {
                                lbqciteminfos.Add(new List<List<LisQCData>>());
                            }
                        }
                        //记录需要插入行的批次数据
                        lbqciteminfos[index] = piciQcs;
                        //记录需要插入的行
                        dataTableinsertindex.Add(index);
                    }
                    dataRow["QCDataIds"] = "{" + string.Join(",", qcids) + "}";
                    dataRow["LoseTypes"] = "{" + string.Join(",", LoseTypes) + "}";
                    dataTable.Rows.Add(dataRow.ItemArray);
                }

                //   }


                if (!isqcdata)
                {
                    DataRow dataRow1 = dataTable.NewRow();
                    index++;
                    dataRow1["ReceiveDate"] = startDate.ToString("yyyy-MM-dd");
                    foreach (var QCItem in lBQCItem)
                    {
                        dataRow1[QCItem.LBQCMaterial.Id.ToString() + "_LBQCItemID"] = QCItem.Id;
                        dataRow1[QCItem.LBQCMaterial.Id.ToString() + "_QCType"] = QCItem.QCDataType;
                    }
                    dataRow1["ItemId"] = lBQCItem.First().LBItem.Id;
                    dataRow1["ItemName"] = lBQCItem.First().LBItem.CName;
                    dataRow1["ItemSame"] = lBQCItem.First().LBItem.SName;
                    dataRow1["ItemPrec"] = lBQCItem.First().LBItem.Prec;
                    dataTable.Rows.Add(dataRow1);
                }
            }

            //批次数据插入表格
            int addindex = 0; //表格行自增索引
            for (int i = 0; i < dataTableinsertindex.Count; i++)
            {
                foreach (var item in lbqciteminfos[dataTableinsertindex[i]])
                {
                    List<string> qcids = new List<string>();
                    List<string> LoseTypes = new List<string>();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReceiveDate"] = lbqciteminfos[dataTableinsertindex[i]].First().First().ReceiveTime.ToString("yyyy-MM-dd");
                    dataRow["ReceiveTime"] = lbqciteminfos[dataTableinsertindex[i]].First().First().ReceiveTime.ToString("HH:mm:ss");
                    dataRow["ItemId"] = lbqciteminfos[dataTableinsertindex[i]].First().First().LBQCItem.LBItem.Id;
                    dataRow["ItemName"] = lbqciteminfos[dataTableinsertindex[i]].First().First().LBQCItem.LBItem.CName;
                    dataRow["ItemSame"] = lbqciteminfos[dataTableinsertindex[i]].First().First().LBQCItem.LBItem.SName;
                    dataRow["ItemPrec"] = lbqciteminfos[dataTableinsertindex[i]].First().First().LBQCItem.LBItem.Prec;
                    foreach (var qc in item)
                    {
                        dataRow[qc.LBQCItem.LBQCMaterial.Id.ToString()] = qc.ReportValue;
                        qcids.Add('"' + qc.LBQCItem.Id.ToString() + "\":\"" + qc.Id + '"');
                        LoseTypes.Add('"' + qc.LBQCItem.LBQCMaterial.Id.ToString() + "\":\"" + qc.LoseType + '"');
                    }
                    foreach (var QCItem in lBQCItems.Where(qcid => qcid.LBItem.Id == lbqciteminfos[dataTableinsertindex[i]].First().First().LBQCItem.LBItem.Id))
                    {
                        dataRow[QCItem.LBQCMaterial.Id.ToString() + "_LBQCItemID"] = QCItem.Id;
                        dataRow[QCItem.LBQCMaterial.Id.ToString() + "_QCType"] = QCItem.QCDataType;
                    }

                    dataRow["QCDataIds"] = "{" + string.Join(",", qcids) + "}";
                    dataRow["LoseTypes"] = "{" + string.Join(",", LoseTypes) + "}";
                    dataTable.Rows.InsertAt(dataRow, dataTableinsertindex[i] + addindex);
                    addindex++;
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 仪器日指控详细信息查询
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<MultipleConcentrationQCMInfoFull> SearchEquipDayQCMFull(string QCMIDs, DateTime startDate, DateTime endDate)
        {
            string lisQCDataWhere = "LBQCItem.LBQCMaterial.Id in(" + QCMIDs + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            var lisQCDatas = IDLisQCDataDao.GetListByHQL(lisQCDataWhere).OrderBy(a => a.LBQCItem.LBQCMaterial.DispOrder).GroupBy(gp => gp.LBQCItem.LBQCMaterial.Id);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMInfoFull 质控项目数据:where = " + lisQCDataWhere + " count= " + lisQCDatas.Count());
            //查询 质控项目时效
            string qcItemTimeWhere = "LBQCItem.LBQCMaterial.Id in(" + QCMIDs + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMInfoFull 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);
            List<MultipleConcentrationQCMInfoFull> multipleConcentrationQCMInfoFulls = new List<MultipleConcentrationQCMInfoFull>();

            foreach (var itemgp in lisQCDatas)
            {
                //仪器项目排序
                List<long> itemids = new List<long>();
                itemgp.ToList().ForEach(a => itemids.Add(a.LBQCItem.LBItem.Id));
                string eqwhere = "EquipID=" + itemgp.First().LBQCItem.LBQCMaterial.LBEquip.Id + " and ItemID in (" + string.Join(",", itemids) + ")";
                IList<LBEquipItem> lBEquipItems = IDLBEquipItemDao.GetListByHQL(eqwhere);
                if (lBEquipItems.Count <= 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCMFull 未找到仪器项目 where :" + eqwhere);
                }
                var equipItems = lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
                foreach (var eitem in equipItems)
                {
                    foreach (var item in itemgp.Where(b => b.LBQCItem.LBItem.Id == eitem.LBItem.Id))
                    {
                        MultipleConcentrationQCMInfoFull multipleConcentrationQCMInfoFull = new MultipleConcentrationQCMInfoFull();
                        multipleConcentrationQCMInfoFull = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<MultipleConcentrationQCMInfoFull, LisQCData>(item);
                        var lBQCItemTime = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.LBQCItem.Id && a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || a.EndDate.HasValue));
                        if (lBQCItemTime.Count() > 0)
                        {
                            multipleConcentrationQCMInfoFull.lBQCItemTime = lBQCItemTime.First();
                        }
                        multipleConcentrationQCMInfoFulls.Add(multipleConcentrationQCMInfoFull);
                    }
                }
            }

            return multipleConcentrationQCMInfoFulls;
        }

        /// <summary>
        /// 仪器日指控单个仪器查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="EquipModel"></param>
        /// <param name="EquipGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<LisQCDataMonthVO> SearchEquipDayQCData(long qCMId, DateTime startDate, DateTime endDate)
        {
            List<LisQCDataMonthVO> lisQCDataMonthVOs = new List<LisQCDataMonthVO>();
            IList<LisQCData> lisQCDatas = IDLisQCDataDao.GetListByHQL("LBQCItem.LBQCMaterial.Id=" + qCMId + " and ReceiveTime>='" + startDate + "' and ReceiveTime<='" + endDate + "'");
            if (lisQCDatas.Count > 0)
            {
                List<long> itemids = new List<long>();
                foreach (var item in lisQCDatas.GroupBy(a => a.LBQCItem.LBItem.Id))
                {
                    itemids.Add(item.Key);
                }
                //仪器项目排序
                string eqwhere = "EquipID=" + lisQCDatas.First().LBQCItem.LBQCMaterial.LBEquip.Id + " and ItemID in (" + string.Join(",", itemids) + ")";
                IList<LBEquipItem> lBEquipItems = IDLBEquipItemDao.GetListByHQL(eqwhere);
                if (lBEquipItems.Count <= 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCData 未找到仪器项目 where :" + eqwhere);
                }
                var equipItems = lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
                List<LisQCData> LisQCDatas = new List<LisQCData>(); //排序之后的数据
                foreach (var item in equipItems)
                {
                    foreach (var itemqc in lisQCDatas.Where(a => a.LBQCItem.LBItem.Id == item.LBItem.Id))
                    {
                        LisQCDatas.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(itemqc));
                    }
                }
                if (LisQCDatas.Count <= 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCData 排序之后的数据为空 LisQCDatas :" + LisQCDatas.Count + "   lisQCDatas= " + lisQCDatas);
                }
                //时效
                List<long> qcitemids = new List<long>();
                DBDao.GetListByHQL("LBQCMaterial.Id=" + qCMId).ToList().ForEach(fe => qcitemids.Add(fe.Id));
                string qcItemTimeWhere = "LBQCItem.Id in (" + string.Join(",", qcitemids) + ") and StartDate<='" + startDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + endDate + "' or EndDate is null)";
                IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCData 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);

                foreach (var item in LisQCDatas)
                {
                    LisQCDataMonthVO lisQCDataMonthVO = new LisQCDataMonthVO();
                    lisQCDataMonthVO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCDataMonthVO, LisQCData>(item);
                    var itemtime = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.LBQCItem.Id);
                    if (itemtime.Count() > 0)
                    {
                        lisQCDataMonthVO.lBQCItemTime = itemtime.First();
                    }
                    lisQCDataMonthVOs.Add(lisQCDataMonthVO);
                }
            }

            return lisQCDataMonthVOs;
        }

        public List<LBQCMaterial> SearchEquipDayQCM(long EquipId, string EquipModel, string EquipGroup)
        {

            string where = "LBQCMaterial.LBEquip.Id=" + EquipId + " and LBQCMaterial.EquipModule='" + EquipModel + "' and LBQCMaterial.QCGroup='" + EquipGroup + "' and IsUse=1";
            IList<LBQCItem> lBQCItems = DBDao.GetListByHQL(where);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.SearchEquipDayQCM 查询质控项目 where=" + where + " count=" + lBQCItems.Count);
            List<LBQCMaterial> lBQCMaterials = new List<LBQCMaterial>();
            foreach (var item in lBQCItems.GroupBy(a => a.LBQCMaterial.Id))
            {
                lBQCMaterials.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCMaterial, LBQCMaterial>(item.First().LBQCMaterial));
            }
            return lBQCMaterials.OrderBy(a => a.DispOrder).ToList();
        }
        /// <summary>
        /// 失控处理树 仪器-质控物
        /// </summary>
        /// <returns></returns>
        public BaseResultTree GetOutControlEQ_QCMTree()
        {
            BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
            var lBQCItems = DBDao.GetListByHQL("IsUse=1");
            List<long> equipIDs = new List<long>();
            List<long> qcmIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                equipIDs.Add(a.LBQCMaterial.LBEquip.Id);
                qcmIds.Add(a.LBQCMaterial.Id);
            });
            //仪器项目查询
            var lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
            if (lBEquipItems.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器项目 where : EquipID in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //仪器查询
            var lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrder);
            if (lBEquips.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器查询 where : Id in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //质控查询
            var lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmIds) + ")").OrderBy(a => a.DispOrder);
            if (lBQCMaterials.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到质控查询 where : Id in (" + string.Join(", ", qcmIds) + ")");
                return baseResultTree;
            }
            foreach (var item in lBEquips.OrderBy(a => a.DispOrder))
            {
                tree tree = new tree();
                tree.tid = item.Id.ToString();
                tree.text = item.CName;
                tree.objectType = "仪器";
                tree.expanded = true;
                tree.leaf = true;
                tree.iconCls = tree.leaf ? "orgImg16" : "orgsImg16";
                List<tree> qcmtrees = new List<tree>();

                foreach (var qcmtree in lBQCMaterials.Where(a => a.LBEquip.Id == item.Id).OrderBy(a => a.DispOrder))
                {
                    tree tree1 = new tree();
                    tree1.tid = qcmtree.Id.ToString();
                    tree1.text = qcmtree.CName;
                    tree1.objectType = "质控物";
                    tree1.pid = item.Id.ToString();
                    tree1.expanded = true;
                    tree1.leaf = true;
                    tree1.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                    List<tree> itemtree = new List<tree>();
                    List<long> itemids = new List<long>();
                    foreach (var equipItem in lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder))
                    {
                        var lbQcitem = lBQCItems.Where(a => a.LBItem.Id == equipItem.LBItem.Id && a.LBQCMaterial.Id == qcmtree.Id && a.LBQCMaterial.LBEquip.Id == item.Id);
                        if (lbQcitem.Count() > 0 && itemids.Count(a => a == lbQcitem.First().LBItem.Id) <= 0)
                        {
                            itemids.Add(lbQcitem.First().LBItem.Id);
                            tree tree2 = new tree();
                            tree2.tid = lbQcitem.First().LBItem.Id.ToString();
                            tree2.text = lbQcitem.First().LBItem.CName;
                            tree2.pid = item.Id.ToString();
                            tree2.objectType = "项目";
                            tree2.expanded = true;
                            tree2.leaf = true;
                            tree2.Para = lbQcitem.First().QCDataType;
                            tree2.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                            itemtree.Add(tree2);
                        }
                    }
                    tree1.Tree = itemtree;
                    qcmtrees.Add(tree1);
                }
                tree.Tree = qcmtrees;
                baseResultTree.Tree.Add(tree);
            }
            return baseResultTree;
        }
        /// <summary>
        /// 失控处理树 仪器-项目-质控
        /// </summary>
        /// <returns></returns>
        public BaseResultTree GetOutControlEQ_ITEMTree()
        {
            BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
            var lBQCItems = DBDao.GetListByHQL("IsUse=1");
            List<long> equipIDs = new List<long>();
            List<long> qcmIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                equipIDs.Add(a.LBQCMaterial.LBEquip.Id);
                qcmIds.Add(a.LBQCMaterial.Id);
            });
            //仪器项目查询
            var lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
            if (lBEquipItems.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器项目 where : EquipID in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //仪器查询
            var lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrder);
            if (lBEquips.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器查询 where : Id in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //质控查询
            //var lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmIds) + ")").OrderBy(a => a.DispOrder);
            //if (lBQCMaterials.Count() <= 0)
            //{
            //    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到质控查询 where : Id in (" + string.Join(", ", qcmIds) + ")");
            //    return baseResultTree;
            //}
            foreach (var item in lBEquips.OrderBy(a => a.DispOrder))
            {
                tree tree = new tree();
                tree.tid = item.Id.ToString();
                tree.text = item.CName;
                tree.objectType = "仪器";
                tree.expanded = true;
                tree.leaf = true;
                tree.iconCls = tree.leaf ? "orgImg16" : "orgsImg16";
                List<tree> qcmtrees = new List<tree>();

                foreach (var qcmtree in lBEquipItems.Where(a => a.LBEquip.Id == item.Id).OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder))
                {
                    tree tree1 = new tree();
                    tree1.tid = qcmtree.LBItem.Id.ToString();
                    tree1.text = qcmtree.LBItem.CName;
                    tree1.objectType = "项目";
                    tree1.pid = item.Id.ToString();
                    tree1.expanded = true;
                    tree1.leaf = true;
                    tree1.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                    List<tree> itemtree = new List<tree>();
                    foreach (var equipItem in lBQCItems.Where(b => b.LBItem.Id == qcmtree.LBItem.Id && b.LBQCMaterial.LBEquip.Id == item.Id).OrderBy(a => a.LBItem.DispOrder))
                    {
                        tree tree2 = new tree();
                        tree2.tid = equipItem.LBQCMaterial.Id.ToString();
                        tree2.text = equipItem.LBQCMaterial.CName;
                        tree2.objectType = "质控物";
                        tree2.pid = item.Id.ToString();
                        tree2.expanded = true;
                        tree2.leaf = true;
                        tree2.value = equipItem.LBQCMaterial.DispOrder;
                        tree2.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                        itemtree.Add(tree2);
                    }
                    if (itemtree.Count > 0)
                    {
                        tree1.Tree = itemtree.OrderBy(a => a.value).ToList();
                        qcmtrees.Add(tree1);
                    }
                }
                if (qcmtrees.Count > 0)
                {
                    tree.Tree = qcmtrees;
                    baseResultTree.Tree.Add(tree);
                }
            }
            return baseResultTree;
        }
        /// <summary>
        /// 失控处理 项目-仪器-质控物
        /// </summary>
        /// <returns></returns>
        public BaseResultTree GetOutControlITEM_QC_QCMTree()
        {
            BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
            var lBQCItems = DBDao.GetListByHQL("IsUse=1");
            List<long> equipIDs = new List<long>();
            List<long> qcmIds = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                equipIDs.Add(a.LBQCMaterial.LBEquip.Id);
                qcmIds.Add(a.LBQCMaterial.Id);
            });
            //仪器项目查询
            var lBEquipItems = IDLBEquipItemDao.GetListByHQL("EquipID in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder);
            if (lBEquipItems.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器项目 where : EquipID in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //仪器查询
            var lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", equipIDs) + ")").OrderBy(a => a.DispOrder);
            if (lBEquips.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到仪器查询 where : Id in (" + string.Join(", ", equipIDs) + ")");
                return baseResultTree;
            }
            //质控查询
            var lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmIds) + ")").OrderBy(a => a.DispOrder);
            if (lBQCMaterials.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetMultipleConcentrationQCMTree 未找到质控查询 where : Id in (" + string.Join(", ", qcmIds) + ")");
                return baseResultTree;
            }
            foreach (var item in lBEquipItems.OrderBy(a => a.DispOrderQC).ThenBy(a => a.DispOrder).ThenBy(a => a.LBItem.DispOrder).GroupBy(a => a.LBItem.Id))
            {
                tree tree = new tree();
                tree.tid = item.First().LBItem.Id.ToString();
                tree.text = item.First().LBItem.CName;
                tree.objectType = "项目";
                tree.expanded = true;
                tree.leaf = true;
                tree.iconCls = tree.leaf ? "orgImg16" : "orgsImg16";
                List<tree> qcmtrees = new List<tree>();
                List<LBEquip> equips = new List<LBEquip>();
                foreach (var eq in item)
                {
                    equips.AddRange(lBEquips.Where(a => a.Id == eq.LBEquip.Id));
                }
                foreach (var qcmtree in equips.OrderBy(a => a.DispOrder))
                {
                    tree tree1 = new tree();
                    tree1.tid = qcmtree.Id.ToString();
                    tree1.text = qcmtree.CName;
                    tree1.pid = item.First().Id.ToString();
                    tree1.objectType = "仪器";
                    tree1.expanded = true;
                    tree1.leaf = true;
                    tree1.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                    List<tree> itemtree = new List<tree>();
                    foreach (var equipItem in lBQCItems.Where(b => b.LBQCMaterial.LBEquip.Id == qcmtree.Id && b.LBItem.Id == item.Key).OrderBy(a => a.LBItem.DispOrder))
                    {
                        tree tree2 = new tree();
                        tree2.tid = equipItem.LBQCMaterial.Id.ToString();
                        tree2.text = equipItem.LBQCMaterial.CName;
                        tree2.objectType = "质控物";
                        tree2.pid = qcmtree.Id.ToString();
                        tree2.expanded = true;
                        tree2.leaf = true;
                        tree2.value = equipItem.LBQCMaterial.DispOrder;
                        tree2.iconCls = tree1.leaf ? "orgImg16" : "orgsImg16";
                        itemtree.Add(tree2);
                    }
                    if (itemtree.Count > 0)
                    {
                        tree1.Tree = itemtree.OrderBy(a => a.value).ToList();
                        qcmtrees.Add(tree1);
                    }
                }
                if (qcmtrees.Count > 0)
                {
                    tree.Tree = qcmtrees;
                    baseResultTree.Tree.Add(tree);
                }
            }
            return baseResultTree;
        }

        #region 质控打印

        /// <summary>
        /// 质控报告打印
        /// </summary>
        /// <param name="tempath">模板路径</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="stream">质控图片信息</param>
        /// <returns></returns>
        public string QCMReportFormPrint(Dictionary<string, string> keyValuePairs,
            DateTime StartDate, DateTime EndDate, string printType,
            string tempath, SortedList<string, System.IO.Stream> streams)
        {
            tempath = System.AppDomain.CurrentDomain.BaseDirectory + "/" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("QCPrintTempleUrl") + tempath + ".frx";
            #region 数据获取
            DataTable reportitem = new DataTable();
            DataTable reportform = new DataTable();
            DataTable reportOutControl = new DataTable();
            // int QCMcount = 0;//有多少个浓度
            // LBQCItem lBQCItem = new LBQCItem(); //查找模板时使用 仪器 项目 质控物 信息
            if (printType == QC_PrintType.月质控.Key)
            {
                long QCMatID = long.Parse(keyValuePairs["QCMatID"]);
                long QCItemId = long.Parse(keyValuePairs["QCItemId"]);
                //质控数据
                IList<LisQCData> lisQCDatas = GetQCMReprotItem(QCItemId, StartDate, EndDate);
                reportitem = ZhiFang.LabStar.Common.ObjectToDataSet.EntityConvetDataTable(lisQCDatas);
                //质控项目信息
                reportform = GetQCMReprotFrom(QCMatID, QCItemId, StartDate, EndDate, lisQCDatas);
                //有多少个浓度
                //QCMcount = 1;
                //lBQCItem = DBDao.Get(QCItemId);
            }
            if (printType == QC_PrintType.日质控.Key)
            {
                long QCMatID = long.Parse(keyValuePairs["QCMatID"]);
                //查询质控物下使用的质控项目
                IList<LBQCItem> lBQCItems = DBDao.GetListByHQL("LBQCMaterial.Id=" + QCMatID + " and IsUse=true");
                List<long> QCItemIds = new List<long>();
                if (lBQCItems.Count > 0)
                {
                    foreach (var item in lBQCItems)
                    {
                        QCItemIds.Add(item.Id);
                    }
                }
                //质控数据
                reportitem = GetDailyQCMReprotItem(String.Join(",", QCItemIds), StartDate, EndDate);
                //质控质控物信息
                reportform = GetDailyQCMReprotFrom(QCMatID, StartDate, EndDate);
            }
            if (printType == QC_PrintType.多浓度质控.Key)
            {
                long EquipId = long.Parse(keyValuePairs["EquipId"]);
                long ItemId = long.Parse(keyValuePairs["ItemId"]);
                string EquipGroup = keyValuePairs["EquipGroup"];
                string EquipModel = keyValuePairs["EquipModel"];
                IList<LisQCData> lisQCDatas = GetConcentrationReprotItem(EquipId, EquipGroup, EquipModel, StartDate, EndDate);
                reportitem = ZhiFang.LabStar.Common.ObjectToDataSet.EntityConvetDataTable(lisQCDatas);
                reportform = GetConcentrationReportFrom(EquipId, ItemId, EquipGroup, EquipModel, StartDate, EndDate, lisQCDatas);
            }
            if (printType == QC_PrintType.仪器日质控.Key)
            {
                string QCMIDs = keyValuePairs["QCMatIDs"];
                IList<LisQCDataPO> lisQCDatas = GetEquipDayReprotItem(QCMIDs, StartDate, EndDate);
                //reportitem = ZhiFang.LabStar.Common.ObjectToDataSet.EntityConvetDataTable(lisQCDatas);
                reportform = GetEquipDayReportFrom(QCMIDs, StartDate, EndDate);
                //结果数据转化
                reportitem = EquipDayQCDataConvetTable(reportform, lisQCDatas.Where(a => a.LoseType != "失控").ToList());
                //失控数据
                reportOutControl = EquipDayQCDataOutControl(lisQCDatas.Where(a => a.LoseType == "失控").ToList());
            }
            #endregion
            //查找模板
            //string tempath = @"E:\智方项目\检验之星BS版\ZhiFang.LabStar.TechnicianStation\ZhiFang.LabStar.TechnicianStation\QCReprotTemple\月质控.frx";// HttpContext.Current.Request.Form.GetValues("tempath")[0];
            //string tempath = FindTemplate(QCMcount, lBQCItem,printType,lBQCItem.QCDataType.ToString());
            //生成报告
            reportform.TableName = "QCfrom";
            reportitem.TableName = "QCData";
            reportOutControl.TableName = "QCDataOut";
            List<QCMReportFormFilesVO> qCMReportFormFilesVOs = CreatReportFormFilesByFRX(reportform, reportitem, reportOutControl, streams, tempath);
            return qCMReportFormFilesVOs.First().PDFPath;
        }
        /// <summary>
        /// 仪器日指控-失控数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable EquipDayQCDataOutControl(List<LisQCDataPO> list)
        {
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemSName");
            dataTable.Columns.Add("QCMName");
            dataTable.Columns.Add("loserule");
            dataTable.Columns.Add("LoseReason");
            dataTable.Columns.Add("CorrectMeasure");
            dataTable.Columns.Add("CorrectValue");
            dataTable.Columns.Add("LoseOperator");
            dataTable.Columns.Add("ReceiveTime");
            dataTable.Columns.Add("Operator");
            dataTable.Columns.Add("QuanValue");
            foreach (var qcmdata in list)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ItemName"] = qcmdata.ItemName;
                dataRow["ItemSName"] = qcmdata.ItemSName;
                dataRow["QCMName"] = qcmdata.QCMName;
                dataRow["loserule"] = qcmdata.Loserule;
                dataRow["LoseReason"] = qcmdata.LoseReason;
                dataRow["CorrectMeasure"] = qcmdata.CorrectMeasure;
                dataRow["CorrectValue"] = qcmdata.CorrectValue;
                dataRow["LoseOperator"] = qcmdata.LoseOperator;
                dataRow["ReceiveTime"] = qcmdata.ReceiveTime;
                dataRow["Operator"] = qcmdata.Operator;
                dataRow["QuanValue"] = qcmdata.QuanValue;

                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        /// <summary>
        /// 仪器日指控 - 项目信息转化
        /// </summary>
        /// <param name="qCMIDs"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private DataTable EquipDayQCDataConvetTable(DataTable reportform, IList<LisQCDataPO> lisQCDatas)
        {
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemSName");
            for (int i = 0; i < 3; i++)
            {
                dataTable.Columns.Add("QCMName" + i);
                dataTable.Columns.Add("loserule" + i);
                dataTable.Columns.Add("LoseReason" + i);
                dataTable.Columns.Add("CorrectMeasure" + i);
                dataTable.Columns.Add("CorrectValue" + i);
                dataTable.Columns.Add("LoseOperator" + i);
                dataTable.Columns.Add("ReceiveTime" + i);
                dataTable.Columns.Add("Operator" + i);
                dataTable.Columns.Add("QuanValue" + i);
            }


            foreach (var item in lisQCDatas.GroupBy(a => a.LBQCItem.LBItem.Id))
            {
                //质控物对应的质控数据
                SortedList<long, List<LisQCDataPO>> keyValuePairs = new SortedList<long, List<LisQCDataPO>>();
                //记录数据 最大的数量
                int MaxQCMcount = 0;
                foreach (var item1 in item.GroupBy(b => b.QCMId))
                {
                    keyValuePairs.Add(item1.Key, item1.ToList());
                    MaxQCMcount = MaxQCMcount < item1.Count() ? item1.Count() : MaxQCMcount;
                }
                for (int i = 0; i < MaxQCMcount; i++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    int rfidnex = 0;
                    foreach (DataRow Qcm in reportform.Rows)
                    {
                        //如果当前质控物对应的质控数据数量大于总数量 则为存在质控数据
                        if (keyValuePairs.ContainsKey(long.Parse(Qcm["QCMId"].ToString())) && keyValuePairs[long.Parse(Qcm["QCMId"].ToString())].Count > i && rfidnex < 3)
                        {
                            var qcmdata = keyValuePairs[long.Parse(Qcm["QCMId"].ToString())][i];
                            dataRow["ItemName"] = qcmdata.ItemName;
                            dataRow["ItemSName"] = qcmdata.ItemSName;
                            dataRow["QCMName" + rfidnex] = qcmdata.QCMName;
                            dataRow["loserule" + rfidnex] = qcmdata.Loserule;
                            dataRow["LoseReason" + rfidnex] = qcmdata.LoseReason;
                            dataRow["CorrectMeasure" + rfidnex] = qcmdata.CorrectMeasure;
                            dataRow["CorrectValue" + rfidnex] = qcmdata.CorrectValue;
                            dataRow["LoseOperator" + rfidnex] = qcmdata.LoseOperator;
                            dataRow["ReceiveTime" + rfidnex] = qcmdata.ReceiveTime;
                            dataRow["Operator" + rfidnex] = qcmdata.Operator;
                            dataRow["QuanValue" + rfidnex] = qcmdata.QuanValue;
                        }
                        rfidnex++;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 仪器日指控 - 项目信息
        /// </summary>
        /// <param name="qCMIDs"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private DataTable GetEquipDayReportFrom(string qCMIDs, DateTime startDate, DateTime endDate)
        {
            //创建表结构
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("Target");
            dataTable.Columns.Add("SD");
            dataTable.Columns.Add("CCV");
            //质控物信息
            dataTable.Columns.Add("QCMName");
            dataTable.Columns.Add("QCMId");
            dataTable.Columns.Add("QCMNo");//批号
            dataTable.Columns.Add("QCMManu");//厂家名称
            dataTable.Columns.Add("ConcLevel");//浓度水平
            //质控物时效
            dataTable.Columns.Add("Begindate"); //开始时间
            dataTable.Columns.Add("NotUseDate");//时效时间
            // 质控评语和审核
            dataTable.Columns.Add("QCMComment"); //质控评语
            dataTable.Columns.Add("CommentOperator");//评语人
            dataTable.Columns.Add("CommentDate"); //评语时间
            dataTable.Columns.Add("CheckComment");//质控审核
            dataTable.Columns.Add("CheckOperator"); //审核人
            dataTable.Columns.Add("CheckDate");//审核时间

            var QCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + qCMIDs + ")");
            //查询 质控项目时效
            string qcItemTimeWhere = "LBQCItem.LBQCMaterial.Id in(" + qCMIDs + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            var lisQCComments = IDLisQCCommentsDao.GetListByHQL("(TypeName='仪器日指控评语' or TypeName='仪器日指控审核')" +
                " and EquipID=" + QCMaterials.First().LBEquip.Id + " and BeginDate='" + startDate + "'");

            foreach (var qcmid in QCMaterials)
            {
                DataRow dataRow = dataTable.NewRow();
                #region 时效信息 （如果有多个先使用第一个时效）
                var qcitemtime = lBQCItemTimes.Where(a => DBDao.Get(a.LBQCItem.Id).LBQCMaterial.Id == qcmid.Id);
                if (qcitemtime.Count() > 0)
                {
                    dataRow["Target"] = qcitemtime.First().Target;
                    dataRow["SD"] = qcitemtime.First().SD;
                    dataRow["CCV"] = qcitemtime.First().CCV;
                    var qcmtime = IDLBQCMatTimeDao.Get(qcitemtime.First().LBQCMatTime.Id);
                    dataRow["QCMNo"] = qcmtime.LotNo;
                    dataRow["QCMManu"] = qcmtime.Manu;
                    dataRow["QCMName"] = QCMaterials.First(a => a.Id == DBDao.Get(qcitemtime.First().LBQCItem.Id).LBQCMaterial.Id).CName;
                    dataRow["QCMId"] = DBDao.Get(qcitemtime.First().LBQCItem.Id).LBQCMaterial.Id;
                    dataRow["ConcLevel"] = QCMaterials.First(a => a.Id == DBDao.Get(qcitemtime.First().LBQCItem.Id).LBQCMaterial.Id).ConcLevel;
                    dataRow["Begindate"] = qcmtime.Begindate;
                    dataRow["NotUseDate"] = qcmtime.NotUseDate;
                }
                #endregion
                //过滤仪器评语 和审核
                LisQCComments lisQCComment = new LisQCComments();
                LisQCComments lisQCCommentsCheck = new LisQCComments();
                foreach (var item in lisQCComments)
                {
                    var arry = item.QCInfo.Split(',');
                    string EquipModule = null;
                    string QCGroup = null;
                    foreach (var item1 in arry)
                    {
                        if (item1.IndexOf("EquipModule") >= 0) EquipModule = item1.Split('=')[1];
                        if (item1.IndexOf("QCGroup") >= 0) QCGroup = item1.Split('=')[1];
                    }
                    if (EquipModule != QCMaterials.First().EquipModule || QCGroup != QCMaterials.First().QCGroup)
                    {
                        continue;
                    }
                    if (item.TypeName == "仪器日指控评语")
                    {
                        dataRow["QCMComment"] = item.QCComment;
                        dataRow["CommentOperator"] = item.Operator;
                        dataRow["CommentDate"] = item.DataAddTime;
                    }
                    if (item.TypeName == "仪器日指控审核")
                    {
                        dataRow["CheckComment"] = item.QCComment;
                        dataRow["CheckOperator"] = item.Operator;
                        dataRow["CheckDate"] = item.DataAddTime;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// 仪器日指控 - 项目数据
        /// </summary>
        /// <param name="qCMIDs"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<LisQCDataPO> GetEquipDayReprotItem(string qCMIDs, DateTime startDate, DateTime endDate)
        {
            List<LisQCDataPO> lisQCDataPOs = new List<LisQCDataPO>();
            var lbQCMats = IDLBQCMaterialDao.GetListByHQL("Id in (" + qCMIDs + ")");
            var lbqcitems = DBDao.GetListByHQL("LBQCMaterial.Id in (" + qCMIDs + ")");
            List<long> qcitemids = new List<long>();
            List<long> itemids = new List<long>();
            lbqcitems.ToList().ForEach(a =>
            {
                qcitemids.Add(a.Id);
                itemids.Add(a.LBItem.Id);
            });
            var items = IDLBItemDao.GetListByHQL("Id in (" + string.Join(",", itemids) + ")");
            var lisqcdatas = IDLisQCDataDao.GetListByHQL("LBQCItem.Id in (" + string.Join(",", qcitemids) + ") and ReceiveTime>='" + startDate + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
            foreach (var item in lisqcdatas)
            {
                LisQCDataPO lisQCDataPO = new LisQCDataPO();
                lisQCDataPO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCDataPO, LisQCData>(item);
                lisQCDataPO.QCItemId = item.LBQCItem.Id;
                var itemname = items.Where(a => a.Id == item.LBQCItem.LBItem.Id);
                if (itemname.Count() > 0)
                {
                    lisQCDataPO.ItemName = itemname.First().CName;
                    lisQCDataPO.ItemSName = itemname.First().SName;
                }
                var qcmname = lbQCMats.Where(a => item.LBQCItem.LBQCMaterial.Id == a.Id);
                if (qcmname.Count() > 0)
                {
                    lisQCDataPO.QCMName = qcmname.First().CName;
                    lisQCDataPO.QCMId = qcmname.First().Id;
                }
                lisQCDataPOs.Add(lisQCDataPO);
            }
            return lisQCDataPOs;
        }

        /// <summary>
        /// 查找模板  --不使用
        /// </summary>
        /// <param name="drReportForm"></param>
        /// <param name="dtReportItem"></param>
        /// <param name="QCDataType">质控类型</param>
        /// <returns></returns>
        public string FindTemplate(int QCMCount, LBQCItem lBQCItem, string printType, string QCDataType)
        {
            try
            {
                string TemplateFullPath;
                LBQCPrintTemplate lBQCPrintTemplate = new LBQCPrintTemplate();

                //查找模板列表
                IList<LBQCPrintTemplate> lBQCPrintTemplates = IDLBQCPrintTemplateDao.GetListByHQL("TypeName='" + printType + "'");
                if (lBQCPrintTemplates.Count <= 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("FindTemplate.TypeName = '" + printType + "' and QCDataType = " + QCDataType + "的模版.");
                    return null;
                }
                else
                {
                    //仪器过滤
                    if (lBQCPrintTemplates.Count(a => a.EquipID == lBQCItem.LBQCMaterial.LBEquip.Id) > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.Where(a => a.EquipID == lBQCItem.LBQCMaterial.LBEquip.Id).ToList();
                        lBQCPrintTemplates = lBQCPrintTemplates.Count(a => a.EquipModule == lBQCItem.LBQCMaterial.EquipModule) > 0 ?
                            lBQCPrintTemplates.Where(a => a.EquipModule == lBQCItem.LBQCMaterial.EquipModule).ToList() : lBQCPrintTemplates;
                    }
                    //质控类型
                    if (lBQCPrintTemplates.Count(a => a.QCDataType.Value == int.Parse(QCDataType)) > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.Where(a => a.QCDataType.Value == int.Parse(QCDataType)).ToList();
                    }
                    //浓度数量过滤
                    if (lBQCPrintTemplates.Count(a => a.LevelCount == QCMCount) > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.Where(a => a.LevelCount == QCMCount).ToList();
                    }
                    //项目过滤
                    if (lBQCPrintTemplates.Count(a => a.ItemID == lBQCItem.LBItem.Id) > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.Where(a => a.ItemID == lBQCItem.LBItem.Id).ToList();
                    }
                    //质控物过滤
                    if (lBQCPrintTemplates.Count(a => a.QCMatID == lBQCItem.LBQCMaterial.Id) > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.Where(a => a.QCMatID == lBQCItem.LBQCMaterial.Id).ToList();
                    }
                    if (lBQCPrintTemplates != null && lBQCPrintTemplates.Count() > 0)
                    {
                        lBQCPrintTemplates = lBQCPrintTemplates.OrderBy(a => a.DispOrder).ToList();
                        TemplateFullPath = lBQCPrintTemplates.First().PrintTemplateName.ToString() + ".frx";
                        TemplateFullPath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("QCPrintTempleUrl") + TemplateFullPath.Trim();
                    }
                    else
                    {
                        ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType() + "FindModel.未找到匹配的模版.");
                        return null;
                    }
                    ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType() + "FindModel.找到匹配的模版，路径：" + TemplateFullPath);
                    return TemplateFullPath;
                }

            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("FindModel.查找报告模版异常：" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 多浓度-项目信息
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="ItemId"></param>
        /// <param name="EquipGroup"></param>
        /// <param name="EquipModel"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="lisQCDatas"></param>
        /// <returns></returns>
        public DataTable GetConcentrationReportFrom(long EquipId, long ItemId, string EquipGroup, string EquipModel, DateTime startDate, DateTime endDate, IList<LisQCData> lisQCDatas)
        {
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("JuTarget");//计算靶值
            dataTable.Columns.Add("JuSD");//计算标准差
            dataTable.Columns.Add("JuCCV");//计算cv
            dataTable.Columns.Add("Target");
            dataTable.Columns.Add("SD");
            dataTable.Columns.Add("CCV");
            dataTable.Columns.Add("Unit");//单位
            //项目信息
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemSName");
            dataTable.Columns.Add("EqName");
            //质控物信息
            dataTable.Columns.Add("QCMName");
            dataTable.Columns.Add("QCMNo");//批号
            dataTable.Columns.Add("QCMManu");//厂家名称

            var lbItem = IDLBItemDao.Get(ItemId);
            var lbQCItems = DBDao.GetListByHQL(" LBItem.Id=" + ItemId + " and LBQCMaterial.LBEquip.Id=" + EquipId + " and LBQCMaterial.EquipModule='" + EquipModel + "' and LBQCMaterial.QCGroup='" + EquipGroup + "'  and IsUse=1");
            List<long> qcitemids = new List<long>();
            lbQCItems.ToList().ForEach(a => qcitemids.Add(a.Id));
            //查询 质控项目时效
            string qcItemTimeWhere = "QCItemID in (" + string.Join(",", qcitemids) + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);

            foreach (var item in lbQCItems)
            {
                DataRow dataRow = dataTable.NewRow();
                //项目信息
                dataRow["ItemName"] = lbItem.CName;
                dataRow["ItemSName"] = lbItem.SName;
                dataRow["EqName"] = IDLBEquipDao.Get(item.LBQCMaterial.LBEquip.Id).CName;

                #region 时效信息 （如果有多个先使用第一个时效）
                var lbqcitemtiems = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.Id);
                if (lbqcitemtiems.Count() > 0)
                {
                    dataRow["Target"] = lbqcitemtiems.First().Target;
                    dataRow["SD"] = lbqcitemtiems.First().SD;
                    dataRow["CCV"] = lbqcitemtiems.First().CCV;
                    dataRow["Unit"] = lbqcitemtiems.First().Unit;
                    var qcmtime = IDLBQCMatTimeDao.Get(lbqcitemtiems.First().LBQCMatTime.Id);
                    dataRow["QCMNo"] = qcmtime.LotNo;
                    dataRow["QCMManu"] = qcmtime.Manu;
                    dataRow["QCMName"] = IDLBQCMaterialDao.Get(item.LBQCMaterial.Id).CName;
                }
                #endregion
                //计算 靶值 标准差 cv
                var itemDatas = lisQCDatas.Where(a => a.LBQCItem.Id == item.Id);
                if (itemDatas.Count() > 0)
                {
                    var CalculationTarget = itemDatas.Average(a => a.QuanValue);
                    dataRow["JuTarget"] = CalculationTarget;
                    if (itemDatas.Count() >= 3)
                    {
                        //平均值
                        double valueAvg = itemDatas.Average(p => p.QuanValue);
                        //求和
                        double valueSum = itemDatas.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
                        //取平方根 
                        double SD = Math.Sqrt(valueSum / (itemDatas.Count() - 1));
                        dataRow["JuSD"] = SD;
                        if (CalculationTarget != 0)
                        {
                            int CCCV = (int)(SD / CalculationTarget * 1) * 100;
                            dataRow["JuCCV"] = (CCCV / 100.0).ToString();
                        }
                    }
                    else
                    {
                        ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetConcentrationReportFrom 质控项目数据小于3不计算标准差");
                    }
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetConcentrationReportFrom 质控项目数据为空");
                }
                dataTable.Rows.Add(dataRow);
            }


            return dataTable;
        }
        /// <summary>
        /// 多浓度-项目数据信息
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="equipGroup"></param>
        /// <param name="equipModel"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private IList<LisQCData> GetConcentrationReprotItem(long EquipId, string equipGroup, string equipModel, DateTime startDate, DateTime endDate)
        {

            string qcdatawhere = "LBQCItem.LBQCMaterial.LBEquip.Id=" + EquipId +
                " and LBQCItem.LBQCMaterial.EquipModule='" + equipGroup + "'" +
                " and LBQCItem.LBQCMaterial.QCGroup='" + equipModel + "'" +
                " and ReceiveTime>='" + startDate + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetConcentrationReprotItem 质控项目查询条件  where=" + qcdatawhere);
            IList<LisQCData> lisQCDatas = IDLisQCDataDao.GetListByHQL(qcdatawhere);
            if (lisQCDatas.Count > 0)
            {
                lisQCDatas.OrderBy(a => a.ReceiveTime);
            }
            return lisQCDatas;
        }
        /// <summary>
        /// 月质控-质控项目信息
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <returns></returns>
        private DataTable GetQCMReprotFrom(long QCMatID, long qCItemId, DateTime startDate, DateTime endDate, IList<LisQCData> lisQCDatas)
        {
            //创建表结构
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("JuTarget");//计算靶值
            dataTable.Columns.Add("JuSD");//计算标准差
            dataTable.Columns.Add("JuCCV");//计算cv
            dataTable.Columns.Add("Target");
            dataTable.Columns.Add("SD");
            dataTable.Columns.Add("CCV");
            //项目信息
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemSName");
            dataTable.Columns.Add("EqName");
            //质控物信息
            dataTable.Columns.Add("QCMName");
            dataTable.Columns.Add("QCMNo");//批号
            dataTable.Columns.Add("QCMManu");//厂家名称
            dataTable.Columns.Add("JuALLJu"); //计算全距
            //查询 质控项目时效
            string qcItemTimeWhere = "QCItemID=" + qCItemId + " and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);

            DataRow dataRow = dataTable.NewRow();
            #region 计算靶值，计算标准差，计算ccv
            if (lisQCDatas.Count() > 0)
            {
                dataRow["JuTarget"] = lisQCDatas.Average(a => a.QuanValue);
                if (lisQCDatas.Count() >= 3)
                {
                    //平均值
                    double valueAvg = lisQCDatas.Average(p => p.QuanValue);
                    //求和
                    double valueSum = lisQCDatas.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
                    //取平方根 
                    double SD = Math.Sqrt(valueSum / (lisQCDatas.Count() - 1));
                    dataRow["JuSD"] = SD;
                    if (dataRow["JuTarget"].ToString() != "0")
                    {
                        int CCCV = (int)(SD / double.Parse(dataRow["JuTarget"].ToString()) * 1) * 100;
                        dataRow["JuSD"] = (CCCV / 100.0).ToString();
                    }
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMReprotFrom 质控项目数据小于3不计算标准差");
                }
            }
            else
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMReprotFrom 质控项目数据为空");
            }
            #endregion

            var QCMaterial = IDLBQCMaterialDao.Get(QCMatID);

            # region 时效信息 （如果有多个先使用第一个时效）
            if (lBQCItemTimes.Count > 0)
            {
                dataRow["Target"] = lBQCItemTimes.First().Target;
                dataRow["SD"] = lBQCItemTimes.First().SD;
                dataRow["CCV"] = lBQCItemTimes.First().CCV;
                var qcmtime = IDLBQCMatTimeDao.Get(lBQCItemTimes.First().LBQCMatTime.Id);
                dataRow["QCMNo"] = qcmtime.LotNo;
                dataRow["QCMManu"] = qcmtime.Manu;
                dataRow["QCMName"] = QCMaterial.CName;
            }
            #endregion

            #region 项目和仪器信息
            LBQCItem lBQCItem = DBDao.Get(qCItemId);
            dataRow["ItemName"] = lBQCItem.LBItem.CName;
            dataRow["ItemSName"] = lBQCItem.LBItem.SName;

            dataRow["EqName"] = IDLBEquipDao.Get(QCMaterial.LBEquip.Id).CName;
            #endregion
            dataTable.Rows.Add(dataRow);
            return dataTable;
        }
        /// <summary>
        /// 月质控-获得质控数据
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <returns></returns>
        private IList<LisQCData> GetQCMReprotItem(long qCItemId, DateTime startDate, DateTime endDate)
        {
            string qcdatawhere = "LBQCItem.Id =" + qCItemId + " and ReceiveTime>='" + startDate + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
            ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCItem.GetQCMReprotItem 质控项目查询条件  where=" + qcdatawhere);
            return IDLisQCDataDao.GetListByHQL(qcdatawhere);
        }

        /// <summary>
        /// 日质控报告--质控物信息
        /// </summary>
        /// <param name="QCMatID"></param>
        /// <returns></returns>
        private DataTable GetDailyQCMReprotFrom(long QCMatID, DateTime startDate, DateTime endDate)
        {
            //创建表结构
            DataTable dataTable = new DataTable();
            //时效信息
            dataTable.Columns.Add("NotUseDate");//失效日期
            dataTable.Columns.Add("Begindate");//开始日期
            dataTable.Columns.Add("EndDate");//截止日期
            //质控物信息
            dataTable.Columns.Add("QCMName");
            dataTable.Columns.Add("QCMNo");//批号
            dataTable.Columns.Add("QCMManu");//厂家名称
            dataTable.Columns.Add("ConcLevel");//浓度水平
            dataTable.Columns.Add("EquipName");//仪器
            dataTable.Columns.Add("EquipModule");//仪器模块
            dataTable.Columns.Add("DailyqcDate");//日期
            //日质控评语
            dataTable.Columns.Add("QCComment");//日质控评语

            DataRow dataRow = dataTable.NewRow();

            dataRow["DailyqcDate"] = startDate.ToString("yyyy-MM-dd");
            # region 质控物信息
            var QCMaterial = IDLBQCMaterialDao.Get(QCMatID);
            if (QCMaterial.CName != null && QCMaterial.CName != "undefined")
            {
                dataRow["QCMName"] = QCMaterial.CName;
                dataRow["ConcLevel"] = QCMaterial.ConcLevel;
                dataRow["EquipModule"] = QCMaterial.EquipModule;
            }
            #endregion

            # region 仪器信息
            var LBEquip = IDLBEquipDao.Get(QCMaterial.LBEquip.Id);
            if (LBEquip.CName != null && LBEquip.CName != "undefined")
            {
                dataRow["EquipName"] = LBEquip.CName;
            }
            #endregion

            #region 质控物时效信息 （如果有多个先使用第一个时效）
            //查询 质控项目时效
            string qcMatTimeWhere = "QCMatID=" + QCMatID + " and Begindate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCMatTime> lBQCMatTime = IDLBQCMatTimeDao.GetListByHQL(qcMatTimeWhere);
            if (lBQCMatTime.Count > 0)
            {
                dataRow["QCMNo"] = lBQCMatTime.First().LotNo;
                dataRow["QCMManu"] = lBQCMatTime.First().Manu;
                dataRow["NotUseDate"] = lBQCMatTime.First().NotUseDate != null ? lBQCMatTime.First().NotUseDate.Value.ToString("yyyy-MM-dd") : "";
                dataRow["Begindate"] = lBQCMatTime.First().Begindate != null ? lBQCMatTime.First().Begindate.Value.ToString("yyyy-MM-dd") : "";
                dataRow["EndDate"] = lBQCMatTime.First().EndDate != null ? lBQCMatTime.First().EndDate.Value.ToString("yyyy-MM-dd") : "";
            }
            #endregion

            #region 日质控评语
            //查询日质控评语
            string qcCommentWhere = "TypeName='日质控评语' and EquipID=" + QCMaterial.LBEquip.Id + " and QCMatID=" + QCMatID + " and BeginDate<='" + endDate.ToString("yyyy-MM-dd") + "' and BeginDate>='" + startDate.ToString("yyyy-MM-dd") + "'";
            IList<LisQCComments> lisQCComments = IDLisQCCommentsDao.GetListByHQL(qcCommentWhere);
            if (lisQCComments.Count > 0)
            {
                dataRow["QCComment"] = lisQCComments.First().QCComment;
            }
            #endregion

            dataTable.Rows.Add(dataRow);
            return dataTable;
        }

        /// <summary>
        /// 日质控报告--质控数据信息
        /// </summary>
        /// <param name="QCMatID"></param>
        /// <returns></returns>
        private DataTable GetDailyQCMReprotItem(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            //创建表结构
            DataTable dataTable = new DataTable();
            //质控项目信息
            dataTable.Columns.Add("ItemID");
            dataTable.Columns.Add("ItemCName");
            dataTable.Columns.Add("ItemSName");
            //质控项目时效信息
            dataTable.Columns.Add("Target");
            dataTable.Columns.Add("SD");
            //质控数据信息
            dataTable.Columns.Add("ReceiveTime");//质控时间
            dataTable.Columns.Add("ReportValue");//报告值
            dataTable.Columns.Add("EResultStatus");//状态
            dataTable.Columns.Add("Operator");//操作人

            if (QCItemIds != "")
            {
                #region 质控数据
                string qcdatawhere = "LBQCItem.Id in (" + QCItemIds + ") and ReceiveTime>='" + startDate + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
                ZhiFang.LabStar.Common.LogHelp.Debug("日质控报告查询质控数据条件  where=" + qcdatawhere);
                var QCDataInfo = IDLisQCDataDao.GetListByHQL(qcdatawhere);

                if (QCDataInfo.Count > 0)
                {
                    foreach (var item in QCDataInfo)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ItemID"] = item.LBQCItem.LBItem.Id;
                        dataRow["ItemCName"] = item.LBQCItem.LBItem.CName;
                        dataRow["ItemSName"] = item.LBQCItem.LBItem.SName;
                        dataRow["ReceiveTime"] = item.ReceiveTime.ToLongTimeString().ToString();
                        dataRow["ReportValue"] = item.ReportValue;
                        dataRow["EResultStatus"] = item.EResultStatus;
                        dataRow["Operator"] = item.Operator;
                        //查询该项目该时间的质控项目时效
                        string qcItemTimeWhere = "QCItemID=" + item.LBQCItem.Id + " and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
                        IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
                        if (lBQCItemTimes.Count > 0)
                        {
                            dataRow["Target"] = lBQCItemTimes.First().Target;
                            dataRow["SD"] = lBQCItemTimes.First().SD;
                        }

                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            #endregion

            return dataTable;
        }

        /// <summary>
        /// 生成报告文件(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dr)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// /// <param name="reportgraph">图片数据</param>
        /// <param name="templatefullpath">模版路径</param>
        /// <returns></returns>
        /// 
        public List<QCMReportFormFilesVO> CreatReportFormFilesByFRX(DataTable reportform, DataTable reportitem, DataTable reportOutControl, SortedList<string, System.IO.Stream> streams, string templatefullpath)
        {
            List<QCMReportFormFilesVO> reportformfileslist = new List<QCMReportFormFilesVO>();
            FastReport.Report report = new FastReport.Report();
            ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.读取模版开始");
            report.Load(templatefullpath);
            ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册数据开始");
            //RegeditDataFRX(reportitem, ref report);
            //ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册图片数据开始");
            if (streams.Count > 0)
            {
                RegeditImageFRX(streams, ref report);
            }
            ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册报告单数据开始");
            //转换为dataset
            DataSet ds = new DataSet();
            ds.Tables.Add(reportform);
            report.RegisterData(ds);
            DataSet ds1 = new DataSet();
            ds1.Tables.Add(reportitem);
            report.RegisterData(ds1);
            DataSet ds2 = new DataSet();
            ds2.Tables.Add(reportOutControl);
            report.RegisterData(ds2);
            eSet.ReportSettings.ShowProgress = false;
            ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.生成报告文件开始");
            report.Prepare();

            string tmppath = "QCMPDF/" + DateTime.Now.ToString("yyyy-MM-dd");//报告路径 不包含文件名
            QCMReportFormFilesVO tmpvo = new QCMReportFormFilesVO();
            if (ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(System.AppDomain.CurrentDomain.BaseDirectory + "/" + tmppath))
            {
                #region PDF
                ReportFormPDFExport tmppdf = new ReportFormPDFExport();
                //随机数
                string reportpath = System.AppDomain.CurrentDomain.BaseDirectory + "/" + tmppath;//报告路径 不包含文件名 (和只差根目录tmppath) 带有根目录的
                string reportformfileallpath = reportpath + "/" + ZhiFang.Common.Public.GUIDHelp.GetGUIDInt().ToString() + ".pdf";//全路径+文件名 (reportpath后加文件名 )
                try
                {
                    report.Export(tmppdf, reportformfileallpath);
                }
                catch (Exception e)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                }

                tmpvo.PDFPath = reportformfileallpath;
                tmpvo.PageCount = tmppdf.PageCount.ToString();

                float h = ((FastReport.ReportPage)report.FindObject("Page1")).PaperHeight;
                float w = ((FastReport.ReportPage)report.FindObject("Page1")).PaperWidth;
                tmpvo.PageName = PageTypeCheck(h, w);
                reportformfileslist.Add(tmpvo);
                report.Dispose();
                tmppdf.Dispose();
                #endregion
            }
            ZhiFang.LabStar.Common.LogHelp.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.释放报告模版开始");
            report.Dispose();
            return reportformfileslist;
        }

        /// <summary>
        /// 模板注入数据
        /// </summary>
        /// <param name="drreportform"></param>
        /// <param name="dtreportitem"></param>
        /// <param name="report"></param>
        /// <param name="TemplateFullPath"></param>
        public void RegeditDataFRX(DataTable dtreportitem, ref FastReport.Report report)
        {
            try
            {
                #region 普通
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dtreportitem);
                report.RegisterData(dataSet); //注入数据
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("RegeditDataFRX.异常:" + ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }

        #region 向模版图片信息
        public void RegeditImageFRX(SortedList<string, System.IO.Stream> streams, ref FastReport.Report report)
        {
            ZhiFang.LabStar.Common.LogHelp.Info("RegeditImageFRX.开始");
            try
            {
                #region 电子签名
                //GeneResultForm geneResultForm = IDGeneResultFormDao.Get(long.Parse(drreportform["ResultFormID"].ToString()));
                //if (geneResultForm != null)
                //{
                //    PictureObject NameImageTechnician = (PictureObject)report.FindObject("NameImageTechnician");
                //    PictureObject NameImageChecker = (PictureObject)report.FindObject("NameImageChecker");
                //    ZhiFang.LabStar.Common.LogHelp.Info("RegeditImageFRX.NameImage图片NameImageTechnician");
                //    if (NameImageTechnician != null)
                //    {
                //        //后缀名暂定为.bmp 2018 - 11 - 27
                //        string url = System.AppDomain.CurrentDomain.BaseDirectory + "EmpImages/" + geneResultForm.InputerID + ".bmp"; ;
                //        NameImageTechnician.ImageLocation = url;
                //        NameImageTechnician.Visible = true;
                //        ZhiFang.LabStar.Common.LogHelp.Info("RegeditImageFRX.NameImageTechnician" + geneResultForm.InputerName + ",图片路径：" + url);
                //    }
                //    if (NameImageChecker != null)
                //    {
                //        //后缀名暂定为.bmp 2018 - 11 - 27
                //        string url = System.AppDomain.CurrentDomain.BaseDirectory + "EmpImages/" + geneResultForm.TwoCheckerID + ".bmp"; ;
                //        NameImageChecker.ImageLocation = url;
                //        NameImageChecker.Visible = true;
                //        ZhiFang.LabStar.Common.LogHelp.Info("RegeditImageFRX.NameImageTechnician" + geneResultForm.TwoCheckerName + ",图片路径：" + url);
                //    }
                //}
                #endregion
                #region 报告图片
                for (int i = 0; i < streams.Keys.Count; i++)
                {
                    PictureObject p = (PictureObject)report.FindObject("QCMImg" + (i + 1));
                    ZhiFang.LabStar.Common.LogHelp.Info("RegeditImageFRX.RFGraphData图片" + "QCMImg" + (i + 1));
                    if (p != null)
                    {
                        System.Drawing.Image ia = Image.FromStream(streams[streams.Keys[i]]);
                        p.Image = ia;
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("RegeditImageFRX.异常" + e.ToString() + "---------" + e.StackTrace.ToString());
            }
        }
        #endregion
        public class ReportFormPDFExport : FastReport.Export.Pdf.PDFExport
        {
            public int PageCount
            {
                get { return base.Pages.Count(); }
            }
        }
        /// <summary>
        /// 报告页面类型 A4 A5
        /// </summary>
        /// <param name="height">高</param>
        /// <param name="width">宽</param>
        /// <returns></returns>
        public string PageTypeCheck(float height, float width)
        {
            string pagetype = "";
            if ((Convert.ToInt32(height) >= 205 || Convert.ToInt32(height) <= 215) && (Convert.ToInt32(width) >= 292 || Convert.ToInt32(width) <= 312))
                pagetype = "A4";
            if (Convert.ToInt32(height) == 297 && Convert.ToInt32(width) == 210)
                pagetype = "A4";
            if (Convert.ToInt32(height) == 210 && Convert.ToInt32(width) == 148)
                pagetype = "A5";
            if (Convert.ToInt32(height) == 148 && Convert.ToInt32(width) == 210)
                pagetype = "A5";
            return pagetype;
        }
        #endregion
    }
}