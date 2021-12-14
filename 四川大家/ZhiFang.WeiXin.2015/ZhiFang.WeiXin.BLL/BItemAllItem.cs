
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.Common.Log;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BItemAllItem : ZhiFang.BLL.Base.BaseBLL<TestItem>, ZhiFang.WeiXin.IBLL.IBItemAllItem
    {
        IDAO.IDBItemConDao IDBItemConDao { get; set; }
        IDAO.IDBTestItemControlDao IDBTestItemControlDao { get; set; }
        IDAO.IDBLabTestItemDao IDBLabTestItemDao { get; set; }
        IDAO.IDBLabGroupItemDao IDBLabGroupItemDao { get; set; }
        public EntityList<GroupItemVO> SearchGroupItemSubItemByPItemNo(string pitemNo, int page, int limit, string sort)
        {
            EntityList<TestItem> tempList = new EntityList<TestItem>();
            EntityList<GroupItemVO> tempLGroupItemVOist = new EntityList<GroupItemVO>();
            List<string> tempSubItemNoList = GetGroupItemSubItemNoByPItemNo(pitemNo);
            if (tempSubItemNoList != null && tempSubItemNoList.Count > 0)
            {

                string sqlwhere = " Id in('" + string.Join("','", tempSubItemNoList.ToArray()) + "')";
                tempList = DBDao.GetListByHQL(sqlwhere, sort, page, limit);
            }

            if (tempList != null && tempList.list == null)
            {
                tempLGroupItemVOist.count = 0;
                tempLGroupItemVOist.list = new List<GroupItemVO>();
            }
            else
            {
                tempLGroupItemVOist.count = tempList.count;
                tempLGroupItemVOist.list = new List<GroupItemVO>();
                foreach (var t in tempList.list)
                {
                    GroupItemVO giv = new GroupItemVO();
                    giv.TestItem = t;
                    giv.PItemNo = long.Parse(pitemNo);
                    giv.ItemNo = t.Id;
                    tempLGroupItemVOist.list.Insert(0, giv);
                }
            }
            return tempLGroupItemVOist;
        }

        public List<string> GetGroupItemSubItemNoByPItemNo(string pItemNo)
        {
            List<string> tempSubItemNoList = new List<string>();

            EntityList<GroupItem> tmpGroupItemlist = new EntityList<GroupItem>();
            string tmpwhere = " PItemNo='" + pItemNo + "' ";
            tmpGroupItemlist = IDBItemConDao.GetListByHQL(tmpwhere, -1, -1);
            if (tmpGroupItemlist != null && tmpGroupItemlist.list != null && tmpGroupItemlist.count > 0)
            {
                foreach (var item in tmpGroupItemlist.list)
                {
                    if (tmpGroupItemlist.list.Count(a => a.PItemNo == item.ItemNo) <= 0)
                    {
                        var subitemno = GetGroupItemSubItemNoByPItemNo(item.ItemNo.ToString());
                        if (subitemno != null && subitemno.Count > 0)
                        {
                            tempSubItemNoList.InsertRange(0, subitemno);
                        }
                        else
                        {
                            tempSubItemNoList.Insert(0, item.ItemNo.ToString());
                        }
                    }
                }
            }
            return tempSubItemNoList;
        }
        public bool AddByTestItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.TestItemVO entity)
        {
            this.Entity = entity.TransVO(entity);
            if (DBDao.Save(this.Entity))
            {
                if (entity != null && entity.SubTestItemVO != null && entity.SubTestItemVO.Count > 0)
                {
                    foreach (var gi in entity.SubTestItemVO)
                    {
                        GroupItem blgi = new GroupItem();
                        blgi.ItemNo = gi.ItemNo;
                        blgi.PItemNo = entity.Id;
                        IDBItemConDao.Save(blgi);
                    }
                }
                return true;
            }
            return false;
        }

        public bool UpdateTestItemByFieldVO(string[] strParas, Entity.ViewObject.Request.TestItemVO entity)
        {
            this.Entity = entity.TransVO(entity);
            if (DBDao.Update(strParas))
            {
                if (IDBItemConDao.DeleteByHql(" From GroupItem where PItemNo=" + entity.Id + "") >= 0)
                {
                    if (entity != null && entity.SubTestItemVO != null && entity.SubTestItemVO.Count > 0)
                    {
                        foreach (var gi in entity.SubTestItemVO)
                        {
                            GroupItem blgi = new GroupItem();
                            blgi.ItemNo = gi.ItemNo;
                            blgi.PItemNo = entity.Id;
                            IDBItemConDao.Save(blgi);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public BaseResultDataValue TestItemCopy(List<string> labCodeList, List<string> itemNoList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (labCodeList == null || labCodeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室项目列表为空！";
                Log.Debug("BItemAllItem.TestItemCopy.实验室项目列表为空！");
                return brdv;
            }
            if (itemNoList == null || itemNoList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "项目项目列表为空！";
                Log.Debug("BItemAllItem.TestItemCopy.项目项目列表为空！");
                return brdv;
            }
            var testitemlist = DBDao.GetListByHQL(" ItemNo in (" + String.Join(",", itemNoList.ToArray()) + ") ");
            IList<GroupItem> groupitemlist = IDBItemConDao.GetListByHQL(" PItemNo in (" + String.Join(",", itemNoList.ToArray()) + ")  ");
            //List<BLabTestItem> BLabTestItemList = new List<BLabTestItem>();
            //if (testitemlist != null && testitemlist.Count > 0)
            //{
            //    foreach (var testitem in testitemlist)
            //    {
            //        var tmpsubitemNolist = GetGroupItemSubItemNoByPItemNo(testitem.Id.ToString());
            //        var tmpsubitemlist = DBDao.GetListByHQL(" ItemNo in (" + String.Join(",", tmpsubitemNolist.ToArray()) + ") ");
            //        BLabTestItem blti = SetValue(testitem);
            //        foreach (var subitem in tmpsubitemlist)
            //        {
            //            BLabTestItem bltisub = SetValue(subitem);
            //            blti.SubBLabTestItem.Add(bltisub);
            //        }
            //        BLabTestItemList.Add(blti);
            //    }
            //}
            switch (OverRideType)
            {
                case 0:
                    #region 克隆
                    ZhiFang.Common.Log.Log.Debug("BItemAllItem.TestItemCopy.克隆.细项删除动作.From BLabGroupItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where ControlLabNo in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBLabTestItemDao.DeleteByHql(" From BLabTestItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);
                        }

                        foreach (var groupitem in groupitemlist)
                        {
                            BLabGroupItem blgi = new BLabGroupItem();
                            blgi.ItemNo = groupitem.ItemNo.ToString();
                            blgi.LabCode = labcode;
                            blgi.PItemNo = groupitem.PItemNo.ToString();
                            blgi.DataAddTime = DateTime.Now;
                            IDBLabGroupItemDao.Save(blgi);
                        }
                    }
                    #endregion
                    break;
                case 1:
                    #region 覆盖
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            var testitemcon = IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                            if (testitemcon != null && testitemcon.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("BItemAllItem.TestItemCopy.覆盖.细项删除动作.From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where  ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                                IDBLabTestItemDao.DeleteByHql(" From BLabTestItem  where LabCode ='" + labcode + "'and ItemNo='" + testitemcon[0].ControlItemNo + "' ");

                            }

                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.Id);
                            if (tmp != null && tmp.Count() > 0)
                            {
                                foreach (var groupitem in tmp)
                                {
                                    BLabGroupItem blgi = new BLabGroupItem();
                                    blgi.ItemNo = groupitem.ItemNo.ToString();
                                    blgi.LabCode = labcode;
                                    blgi.PItemNo = groupitem.PItemNo.ToString();
                                    blgi.DataAddTime = DateTime.Now;
                                    IDBLabGroupItemDao.Save(blgi);
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 2:
                    #region 差异
                    string info = "";
                    foreach (string labcode in labCodeList)
                    {
                        string tmpinfo = "实验室编码："+ labcode+"@";
                        foreach (var testitem in testitemlist)
                        {
                            var testitemcon = IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                            if (testitemcon != null && testitemcon.Count > 0)
                            {
                                Log.Debug("BItemAllItem.TestItemCopy.差异复制.实验室编码:'" + labcode + "',中心项目编码:'" + testitem.Id + "',对照关系已存在！不复制！");
                                tmpinfo += "'" + testitem.Id + "'";
                                continue;

                            }

                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.Id);
                            if (tmp != null && tmp.Count() > 0)
                            {
                                foreach (var groupitem in tmp)
                                {
                                    BLabGroupItem blgi = new BLabGroupItem();
                                    blgi.ItemNo = groupitem.ItemNo.ToString();
                                    blgi.LabCode = labcode;
                                    blgi.PItemNo = groupitem.PItemNo.ToString();
                                    blgi.DataAddTime = DateTime.Now;
                                    IDBLabGroupItemDao.Save(blgi);
                                }
                            }
                        }
                        info += tmpinfo+";";
                    }
                    brdv.ErrorInfo = info;
                    #endregion
                    break;
            }
           
            return brdv;
        }

        private BLabTestItem SetValue(TestItem subitem)
        {
            BLabTestItem blti = new BLabTestItem();
            blti.Price = subitem.Price;
            blti.BonusPercent = subitem.BonusPercent;
            blti.CName = subitem.CName;
            blti.Color = subitem.Color;
            blti.CostPrice = subitem.CostPrice;
            blti.Cuegrade = subitem.Cuegrade;
            blti.DataAddTime =DateTime.Now;
            blti.DiagMethod = subitem.DiagMethod;
            blti.DispOrder = subitem.DispOrder;
            blti.EName = subitem.EName;
            blti.FWorkLoad = subitem.FWorkLoad;
            blti.InspectionPrice= subitem.InspectionPrice;
            blti.IsCalc = subitem.IsCalc;
            blti.IschargeItem = subitem.IschargeItem;
            blti.IsCombiItem = subitem.IsCombiItem;
            blti.IsDoctorItem = subitem.IsDoctorItem;
            blti.IsProfile = subitem.IsProfile;
            blti.ItemDesc= subitem.ItemDesc;
            blti.ItemNo = subitem.Id.ToString();
            blti.OrderNo = subitem.OrderNo;
            blti.Prec = subitem.Prec;
            blti.Secretgrade = subitem.Secretgrade;
            blti.ShortCode = subitem.ShortCode;
            blti.ShortName = subitem.ShortName;
            blti.StandardCode = subitem.StandardCode;
            blti.Unit = subitem.Unit;
            blti.UseFlag = subitem.Visible;
            blti.Visible = subitem.Visible;
            blti.ZFStandCode = subitem.StandardCode;
            return blti;
        }

        public BaseResultDataValue TestItemCopyAll(List<string> labCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (labCodeList == null || labCodeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室列表为空！";
                Log.Debug("BItemAllItem.TestItemCopyAll.labCodeList.实验室列表为空！");
                return brdv;
            }
            IList<TestItem> testitemlist = DBDao.GetListByHQL(" Visible=1 ");
            if (testitemlist == null || testitemlist.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "中心项目列表为空！";
                Log.Debug("BItemAllItem.TestItemCopyAll.中心项目列表为空！");
                return brdv;
            }
            IList<GroupItem> groupitemlist = IDBItemConDao.GetListByHQL(" 1=1 ");

            switch (OverRideType)
            {
                case 0:
                    #region 克隆
                    ZhiFang.Common.Log.Log.Debug("BItemAllItem.TestItemCopyAll.克隆.细项删除动作.From BLabGroupItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where ControlLabNo in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBLabTestItemDao.DeleteByHql(" From BLabTestItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);
                        }

                        foreach (var groupitem in groupitemlist)
                        {
                            BLabGroupItem blgi = new BLabGroupItem();
                            blgi.ItemNo = groupitem.ItemNo.ToString();
                            blgi.LabCode = labcode;
                            blgi.PItemNo = groupitem.PItemNo.ToString();
                            blgi.DataAddTime = DateTime.Now;
                            IDBLabGroupItemDao.Save(blgi);
                        }
                    }
                    #endregion
                    break;
                case 1:
                    #region 覆盖
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            var testitemcon = IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                            if (testitemcon != null && testitemcon.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("BItemAllItem.TestItemCopyAll.覆盖.细项删除动作.From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where  ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                                IDBLabTestItemDao.DeleteByHql(" From BLabTestItem  where LabCode ='" + labcode + "'and ItemNo='" + testitemcon[0].ControlItemNo + "' ");

                            }

                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.Id);
                            if (tmp != null && tmp.Count() > 0)
                            {
                                foreach (var groupitem in tmp)
                                {
                                    BLabGroupItem blgi = new BLabGroupItem();
                                    blgi.ItemNo = groupitem.ItemNo.ToString();
                                    blgi.LabCode = labcode;
                                    blgi.PItemNo = groupitem.PItemNo.ToString();
                                    blgi.DataAddTime = DateTime.Now;
                                    IDBLabGroupItemDao.Save(blgi);
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 2:
                    #region 差异
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            var testitemcon = IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                            if (testitemcon != null && testitemcon.Count > 0)
                            {
                                continue;

                            }

                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.ItemNo = testitem.Id.ToString();
                            blti.LabCode = labcode;
                            IDBLabTestItemDao.Save(blti);
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitem.Id.ToString();
                            btic.ControlItemNo = blti.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.Id);
                            if (tmp != null && tmp.Count() > 0)
                            {
                                foreach (var groupitem in tmp)
                                {
                                    BLabGroupItem blgi = new BLabGroupItem();
                                    blgi.ItemNo = groupitem.ItemNo.ToString();
                                    blgi.LabCode = labcode;
                                    blgi.PItemNo = groupitem.PItemNo.ToString();
                                    blgi.DataAddTime = DateTime.Now;
                                    IDBLabGroupItemDao.Save(blgi);
                                }
                            }
                        }
                    }
                    #endregion
                    break;
            }
            return brdv;
        }
    }
}