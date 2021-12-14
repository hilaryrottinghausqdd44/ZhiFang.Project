using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBEquip : BaseBLL<LBEquip>, ZhiFang.IBLL.LabStar.IBLBEquip
    {
        ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipResultTH IBLBEquipResultTH { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipSection IBLBEquipSection { get; set; }

        public BaseResultDataValue EditLBEquipCommPara(long equipID, string jsonCommPara)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (jsonCommPara == null || jsonCommPara.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "参数jsonCommPara的值不能为空！";
                return baseResultDataValue;
            }
            LBEquip equip = this.Get(equipID);
            if (equip != null)
            {
                JObject jsonObject = JObject.Parse(jsonCommPara);
                if (jsonObject.Property("CommInfo") != null)
                    equip.CommInfo = jsonObject["CommInfo"].ToString();
                if (jsonObject.Property("CommPara") != null)
                    equip.CommPara = jsonObject["CommPara"].ToString();
                if (jsonObject.Property("CommSys") != null)
                    equip.CommSys = jsonObject["CommSys"].ToString();
                this.Entity = equip;
                baseResultDataValue.success = this.Edit();
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取相应的仪器信息！";
            }

            return baseResultDataValue;
        }

        public BaseResultDataValue AddNewLBEquipByLBEquip(long equipID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LBEquip equip = this.Get(equipID);
            if (equip != null)
            {
                LBEquip newEquip = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBEquip, LBEquip>(equip);
                newEquip.CName += "_复制";
                this.Entity = newEquip;
                this.Add();
                IList<LBEquipItem> listLBEquipItem = IBLBEquipItem.SearchListByHQL(" lbequipitem.LBEquip.Id=" + equipID);
                if (listLBEquipItem != null && listLBEquipItem.Count > 0)
                {
                    foreach (LBEquipItem equipItem in listLBEquipItem)
                    {
                        LBEquipItem newEquipItem = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBEquipItem, LBEquipItem>(equipItem);
                        newEquipItem.LBEquip = newEquip;
                        IBLBEquipItem.Entity = newEquipItem;
                        IBLBEquipItem.Add();
                    }
                }

                IList<LBEquipResultTH> listLBEquipResultTH = IBLBEquipResultTH.SearchListByHQL(" lbequipresultth.LBEquip.Id=" + equipID);
                if (listLBEquipResultTH != null && listLBEquipResultTH.Count > 0)
                {
                    foreach (LBEquipResultTH equipResultTH in listLBEquipResultTH)
                    {
                        LBEquipResultTH newEquipResultTH = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBEquipResultTH, LBEquipResultTH>(equipResultTH);
                        newEquipResultTH.LBEquip = newEquip;
                        IBLBEquipResultTH.Entity = newEquipResultTH;
                        IBLBEquipResultTH.Add();
                    }
                }

                IList<LBEquipSection> listLBEquipSection = IBLBEquipSection.SearchListByHQL(" lbequipsection.LBEquip.Id=" + equipID);
                if (listLBEquipSection != null && listLBEquipSection.Count > 0)
                {
                    foreach (LBEquipSection equipSection in listLBEquipSection)
                    {
                        LBEquipSection newEquipSection = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBEquipSection, LBEquipSection>(equipSection);
                        newEquipSection.LBEquip = newEquip;
                        IBLBEquipSection.Entity = newEquipSection;
                        IBLBEquipSection.Add();
                    }
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法根据仪器ID获取仪器信息";
            }
            return brdv;
        }

        public BaseResultDataValue AddCopyLBEquipItemByLBEquip(long fromEquipID, long toEquipID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LBEquip fromEquip = this.Get(fromEquipID);
            if (fromEquip != null)
            {
                LBEquip toEquip = this.Get(toEquipID);
                if (toEquip != null)
                {
                    IList<LBEquipItem> listLBEquipItem = IBLBEquipItem.SearchListByHQL(" lbequipitem.LBEquip.Id=" + fromEquipID);
                    IList<LBEquipItem> listOldLBEquipItem = IBLBEquipItem.SearchListByHQL(" lbequipitem.LBEquip.Id=" + toEquipID);
                    if (listLBEquipItem != null && listLBEquipItem.Count > 0)
                    {
                        foreach (LBEquipItem equipItem in listLBEquipItem)
                        {
                            IList<LBEquipItem> temmList = listOldLBEquipItem.Where(p => p.LBItem.Id == equipItem.LBItem.Id).ToList();
                            if (temmList == null || temmList.Count == 0)
                            {
                                LBEquipItem newEquipItem = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LBEquipItem, LBEquipItem>(equipItem);
                                newEquipItem.LBEquip = toEquip;
                                IBLBEquipItem.Entity = newEquipItem;
                                if (IBLBEquipItem.Add())
                                    listOldLBEquipItem.Add(newEquipItem);
                            }
                        }
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "无法根据目标仪器ID获取仪器信息";
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法根据源仪器ID获取仪器信息";
            }
            return brdv;
        }
    }
}