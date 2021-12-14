
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public class BBSampleTypeAndCultureMedium : BaseBLL<BSampleTypeAndCultureMedium>, ZhiFang.Digitlab.IBLL.Business.IBBSampleTypeAndCultureMedium
    {
        public IDMEGroupSampleFormDao IDMEGroupSampleFormDao { get; set; }
        public IDMEPTSampleItemDao IDMEPTSampleItemDao { get; set; }
        public IDBSampleCharacterDao IDBSampleCharacterDao { get; set; }
        public IDMEMicroBCBottleManageInfoDao IDMEMicroBCBottleManageInfoDao { get; set; }
        public IDMEMicroInoculantDao IDMEMicroInoculantDao { get; set; }
        /// <summary>
        /// 依依医嘱项目(和样本类型)找到样本单的检验医嘱项目和样本类型，查询出默认培养基信息
        /// </summary>
        /// <param name="itemIdListsStr"></param>
        /// <param name="bsampleTypeId"></param>
        /// <param name="inoculantType">（接种类型），枚举型：1-初次接种；2-分纯接种</param>
        /// <returns></returns>
        public EntityList<BSampleTypeAndCultureMedium> GetDefaultCultureMedium(string itemIdListsStr, string bsampleTypeId, string inoculantType)
        {
            EntityList<BSampleTypeAndCultureMedium> tempEntityList = new EntityList<BSampleTypeAndCultureMedium>();
            string strHqlWhere = "";
            if (!String.IsNullOrEmpty(bsampleTypeId))
            {
                strHqlWhere = "bsampletypeandculturemedium.BSampleType.Id=" + bsampleTypeId;
            }
            if (!String.IsNullOrEmpty(itemIdListsStr))
            {
                if (!String.IsNullOrEmpty(strHqlWhere))
                {
                    strHqlWhere = strHqlWhere + " and bsampletypeandculturemedium.ItemAllItem.Id in (" + itemIdListsStr + ")";
                }
                else
                {
                    strHqlWhere = " bsampletypeandculturemedium.ItemAllItem.Id in (" + itemIdListsStr + ")";
                }
            }
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHqlWhere += " and bsampletypeandculturemedium.IsDefault=1 and bsampletypeandculturemedium.InoculantType=" + inoculantType;
                tempEntityList = this.SearchListByHQL(strHqlWhere, 0, 0);
            }
            return tempEntityList;
        }

        /// <summary>
        /// 依条码号获取到样本信息的默认培养基信息(包括样本信息+血培养瓶条码+标本性状信息+默认培养基+标本量)
        /// 如果是初次接种登记,先判断当前医嘱条码是否已经与血培养瓶条码关联签收,如果是,通过关联的血培养瓶的血培养瓶类型取出默认的培养基信息,如果没有默认的培养基信息,再按取常规取默认培养基信息
        /// 传种：仅对培养瓶这种情况,取默认培养基,是先依医嘱条码号判断是否签收有血培养瓶，然后根据瓶类型去取对应的默认培养基
        /// </summary>
        /// <param name="gbarCode">条码号</param>
        /// <param name="bsampleTypeId">样本类型Id</param>
        /// <param name="inoculantType">（接种类型），枚举型：1-初次接种；2-分纯接种:3-分纯</param>
        /// <param name="isExec"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public Dictionary<string, object> SearchDefaultCultureMediumByGBarCodeAndInoculantType(string gbarCode, string bsampleTypeId, string inoculantType, ref bool isExec, ref string info)
        {
            Dictionary<string, object> entityList = new Dictionary<string, object>();
            entityList.Add("Amount", "");
            entityList.Add("BSampleCharacterId", "");
            entityList.Add("BottleSerialNo", "");
            entityList.Add("DefaultCultureMediumList", "");
            gbarCode = gbarCode.Replace("'", "");
            EntityList<MEGroupSampleForm> tempMEGroupSampleFormList = IDMEGroupSampleFormDao.GetListByHQL("megroupsampleform.MainState=" + (int)ZhiFang.Common.Public.MEGroupSampleFormMainStateEnum.检验中 + " and megroupsampleform.GBarCode='" + gbarCode + "'", 1, 10);
            if (tempMEGroupSampleFormList.list != null && tempMEGroupSampleFormList.list.Count > 0)
            {
                IList<MEPTSampleItem> tempMEPTSampleItemEntityList = new List<MEPTSampleItem>();
                EntityList<BSampleTypeAndCultureMedium> tempEntityList = new EntityList<BSampleTypeAndCultureMedium>();
                EntityList<BSampleCharacter> tempBSampleCharacterList = new EntityList<BSampleCharacter>();

                entityList.Add("MEGroupSampleFormList", tempMEGroupSampleFormList);

                #region 标本性状处理
                if (String.IsNullOrEmpty(bsampleTypeId))
                {
                    bsampleTypeId = tempMEGroupSampleFormList.list[0].GSampleTypeID.ToString();
                }

                if (!String.IsNullOrEmpty(bsampleTypeId.ToString()))
                {
                    tempBSampleCharacterList = IDBSampleCharacterDao.GetListByHQL("bsamplecharacter.BSampleType.Id=" + bsampleTypeId, 0, 0);

                }
                else
                {
                    tempBSampleCharacterList = IDBSampleCharacterDao.GetListByHQL("", 0, 0);
                }
                entityList.Add("BSampleCharacterList", tempBSampleCharacterList);
                #endregion

                IList<MEMicroBCBottleManageInfo> tempMEMicroBCBottleManageInfoList = new List<MEMicroBCBottleManageInfo>();
                //初次接种需要判断医嘱条码是否已经关联血培养瓶
                if (inoculantType == "1")
                {
                    #region 获取血培养瓶的默认培养基信息
                    tempMEMicroBCBottleManageInfoList = IDMEMicroBCBottleManageInfoDao.GetListByHQL("memicrobcbottlemanageinfo.ReturnSerialNo='" + gbarCode + "'");
                    #endregion
                }
                else
                {
                    //传种或分纯,需要获取原接种的标本量及标本性状
                    IList<MEMicroInoculant> tempMEMicroInoculantList = IDMEMicroInoculantDao.GetListByHQL("memicroinoculant.BarCode='" + gbarCode + "'");
                    if (tempMEMicroInoculantList.Count > 0)
                    {
                        entityList["Amount"] = tempMEMicroInoculantList[0].Amount.ToString();
                        if (tempMEMicroInoculantList[0].BSampleCharacter != null)
                        {
                            entityList["BSampleCharacterId"] = tempMEMicroInoculantList[0].BSampleCharacter.Id.ToString();
                        }
                    }
                }
                #region 默认培养基信息
                if (tempMEMicroBCBottleManageInfoList == null || tempMEMicroBCBottleManageInfoList.Count == 0)
                {
                    tempMEPTSampleItemEntityList = IDMEPTSampleItemDao.GetListByHQL("meptsampleitem.SampleFrom.BarCode='" + gbarCode + "'");
                    string itemIdListsStr = "", itemCNameListsStr = "";
                    foreach (MEPTSampleItem model in tempMEPTSampleItemEntityList)
                    {
                        itemIdListsStr = itemIdListsStr + model.ItemAllItem.Id + ",";
                        itemCNameListsStr = itemCNameListsStr + model.ItemAllItem.CName + ",";
                    }
                    if (!String.IsNullOrEmpty(itemIdListsStr))
                    {
                        itemIdListsStr = itemIdListsStr.TrimEnd(',');
                        itemCNameListsStr = itemCNameListsStr.TrimEnd(',');
                    }
                    if (!String.IsNullOrEmpty(itemIdListsStr))
                    {
                        tempEntityList = this.GetDefaultCultureMedium(itemIdListsStr, bsampleTypeId.ToString(), inoculantType);
                    }
                    if (tempEntityList.count < 1)
                    {
                        isExec = false;
                        info = "检验项目为:[" + itemCNameListsStr + "],没有默认培养基信息";
                    }
                    else
                    {
                        entityList["DefaultCultureMediumList"] = tempEntityList;
                    }
                }
                else
                {
                    //取血培养瓶的默认培养基
                    MEMicroBCBottleManageInfo tempModel = tempMEMicroBCBottleManageInfoList[0];
                    if (tempModel != null && !String.IsNullOrEmpty(tempModel.BottleSerialNo))
                    {
                        entityList["BottleSerialNo"] = tempModel.BottleSerialNo;
                    }
                    if (tempModel.BMicroBloodCultureBottleType != null && tempModel.BMicroBloodCultureBottleType.BCultureMedium != null)
                    {
                        BSampleTypeAndCultureMedium model = new BSampleTypeAndCultureMedium();
                        //model.Id = 10000;
                        model.BCultureMedium = tempModel.BMicroBloodCultureBottleType.BCultureMedium;
                        tempEntityList.list = new List<BSampleTypeAndCultureMedium>();
                        tempEntityList.list.Add(model);
                        tempEntityList.count = 1;
                        entityList["DefaultCultureMediumList"] = tempEntityList;
                    }
                }
                #endregion
            }
            else
            {
                isExec = false;
                info = "条码号为:" + gbarCode + ",没有检验单信息";
            }
            return entityList;
        }
    }
}