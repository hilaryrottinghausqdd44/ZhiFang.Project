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
    public class BLBQCMaterial : BaseBLL<LBQCMaterial>, ZhiFang.IBLL.LabStar.IBLBQCMaterial
    {
        ZhiFang.IBLL.LabStar.IBLBEquip IBLBEquip { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCItem IBLBQCItem { get; set; }

        IDLBEquipDao IDLBEquipDao { get; set; }

        /// <summary>
        /// 查询质控物信息（FetchJoin）
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<LBQCMaterial> QueryLBQCMaterial(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBQCMaterialDao).QueryLBQCMaterialDao(strHqlWhere, Order, start, count);
        }

        public BaseResultTree GetEquipMaterialTree(long equipID, long matID)
        {
            BaseResultTree baseResultTree = new BaseResultTree();
            baseResultTree.Tree = new List<tree>();
            IList<LBEquip> listEquip = null;
            if (equipID == 0)
                listEquip = IBLBEquip.LoadAll();
            else
                listEquip = IBLBEquip.SearchListByHQL(" lbequip.Id=" + equipID);

            foreach (LBEquip equip in listEquip.OrderBy(a => a.DispOrder))
            {
                tree treeEquip = new tree();
                treeEquip.tid = equip.Id.ToString();
                treeEquip.text = equip.CName.ToString();
                //treeEquip.pid = "";
                treeEquip.expanded = true;
                treeEquip.leaf = true;
                treeEquip.value = "Equip";
                treeEquip.iconCls = treeEquip.leaf ? "orgImg16" : "orgsImg16";
                treeEquip.Tree = GetEquipMatTree(equip);
                baseResultTree.Tree.Add(treeEquip);
            }
            return baseResultTree;
        }

        public List<tree> GetEquipMatTree(LBEquip equip)
        {
            List<tree> equipMatTree = new List<tree>();
            IList<LBQCMaterial> listMat = this.SearchListByHQL(" lbqcmaterial.LBEquip.Id=" + equip.Id);
            if (listMat != null && listMat.Count > 0)
            {
                foreach (var mat in listMat.OrderBy(a => a.DispOrder))
                {
                    //当前货架节点
                    tree treeMat = new tree();
                    treeMat.tid = mat.Id.ToString();
                    treeMat.text = mat.CName.ToString();
                    treeMat.pid = equip.Id.ToString();
                    treeMat.expanded = true;
                    treeMat.leaf = true;
                    treeMat.value = "Mat";
                    treeMat.Para = mat.EquipModule;
                    treeMat.iconCls = treeMat.leaf ? "orgImg16" : "orgsImg16";
                    equipMatTree.Add(treeMat);
                }
            }
            return equipMatTree;
        }

        public BaseResultDataValue AddCopyLBQCItemByMatID(long fromMatID, long toMatID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //LBQCMaterial fromMat = this.Get(fromMatID);
            LBQCMaterial toMat = this.Get(toMatID);
            if (toMat != null)
            {
                IList<LBQCItem> listFormLBQCItem = IBLBQCItem.SearchListByHQL(" lbqcitem.LBQCMaterial.Id=" + fromMatID.ToString());
                if (listFormLBQCItem != null && listFormLBQCItem.Count > 0)
                {
                    EntityList<LBEquipItem> listLBEquipItem = IBLBEquipItem.QueryLBEquipItem(" lbequipitem.LBEquip.Id=" + toMat.LBEquip.Id.ToString(), "", 0, 0);
                    if (listLBEquipItem != null && listLBEquipItem.count > 0)
                    {
                        IList<LBQCItem> listToLBQCItem = IBLBQCItem.SearchListByHQL(" lbqcitem.LBQCMaterial.Id=" + toMatID.ToString());
                        foreach (LBQCItem qcItem in listFormLBQCItem)
                        {
                            var listForm = listLBEquipItem.list.Where(p => p.LBItem.Id == qcItem.LBItem.Id).ToList();
                            var listTo = listToLBQCItem.Where(p => p.LBItem.Id == qcItem.LBItem.Id).ToList();
                            bool isAdd = (listForm != null && listForm.Count > 0 && (listTo == null || listTo.Count == 0));
                            if (isAdd)
                            {
                                LBQCItem addEntity = new LBQCItem();
                                addEntity.LabID = qcItem.LabID;
                                addEntity.LBItem = qcItem.LBItem;
                                addEntity.LBQCMaterial = toMat;
                                addEntity.QCDataType = qcItem.QCDataType;
                                addEntity.QCDataTypeName = qcItem.QCDataTypeName;
                                //addEntity = qcItem;
                                //addEntity = qcItem;
                                //addEntity = qcItem;
                                //addEntity = qcItem;
                                //addEntity = qcItem;
                                IBLBQCItem.Entity = addEntity;
                                IBLBQCItem.Add();
                            }
                            else
                            {
                                //baseResultDataValue.success = false;
                                string errorInfo = "复制质控物项目：质控物【" + toMat.CName + "】所属仪器【" + toMat.LBEquip.CName + "】没有设置【" + qcItem.LBItem.CName + "】项目信息或该质控物已设置此项目信息";
                                ZhiFang.LabStar.Common.LogHelp.Info(errorInfo);
                            }
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "复制质控物项目：质控物【" + toMat.CName + "】所属仪器【" + toMat.LBEquip.CName + "】没有设置任何项目信息";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "复制质控物项目：无法根据质控物ID【" + toMatID + "】获取相关质控物信息";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 根据小组找仪器质控物
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public List<LBQCMaterial> SearchQCMaterialbySectionEquip(long sectionId)
        {
            List<LBQCMaterial> lBQCMaterials = new List<LBQCMaterial>();
            IList<LBEquip> lBEquips = IDLBEquipDao.GetListByHQL("LBSection.Id=" + sectionId);
            if (lBEquips.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLBQCMaterial.SearchQCMaterialbySectionEquip 未找到小组Id为:" + sectionId + "的仪器");
                return lBQCMaterials;
            }
            List<long> qcmids = new List<long>();
            lBEquips.ToList().ForEach(a => qcmids.Add(a.Id));
            lBQCMaterials = DBDao.GetListByHQL("LBEquip.Id in (" + string.Join(",", qcmids) + ")").ToList();
            return lBQCMaterials;
        }
    }
}