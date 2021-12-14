
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BBLabTestItem : ZhiFang.BLL.Base.BaseBLL<BLabTestItem>, ZhiFang.WeiXin.IBLL.IBBLabTestItem
    {
        IDAO.IDClientEleArea IDClientEleArea { get; set; }
        IDAO.IDBLabGroupItemDao IDBLabGroupItemDao { get; set; }
        IDAO.IDBTestItemControlDao IDBTestItemControlDao { get; set; }
        IDAO.IDItemAllItemDao IDItemAllItemDao { get; set; }
        IDAO.IDItemColorDictDao IDItemColorDictDao { get; set; }
        IDAO.IDItemColorAndSampleTypeDetailDao IDItemColorAndSampleTypeDetailDao { get; set; }
        IDAO.IDSampleTypeDao IDSampleTypeDao { get; set; }

        //IDAO.IDClientEleArea IDClientEleArea { get; set; }
        //IDAO.IDClientEleArea IDClientEleArea { get; set; }
        public EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchOSBLabTestItemByAreaID(string AreaID, int page, int limit, string sort, string where)
        {
            ClientEleArea cea = IDClientEleArea.Get(long.Parse(AreaID));
            if (cea != null && cea.ClientNo > 0 && cea.ClientNo.ToString().Trim() != "")
            {
                return SearchOSBLabTestItemByLabID(cea.ClientNo.ToString(), page, limit, sort, where);
            }
            else
            {
                return null;
            }
        }
        public EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchOSBLabTestItemByLabID(string LabID, int page, int limit, string sort, string where)
        {
            if (LabID != null && LabID.Trim() != "")
            {
                //添加上只查询为主套的标记且价格大于0的条件
                string defalutWhere = " LabCode='" + LabID + "' and IsCombiItem=1 and Price>0 and UseFlag=1 ";
                string tmpwhere = (where != null && where.Trim() != "") ? defalutWhere + " and " + where : defalutWhere;
                var tmplist = DBDao.GetListByHQL(tmpwhere, sort, page, limit);
                return TransVO(tmplist);
            }
            else
            {
                return null;
            }
        }
        public EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchBLabTestItemVOList(int page, int limit, string sort, string where)
        {
            //添加上只查询为主套的标记且价格大于0的条件
            string defalutWhere = "IsCombiItem=1 and Price>0 and UseFlag=1 ";
            string tmpwhere = (where != null && where.Trim() != "") ? defalutWhere + " and " + where : defalutWhere;
            var tmplist = DBDao.GetListByHQL(tmpwhere, sort, page, limit);
            return TransVO(tmplist);
        }
        
        EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> TransVO(EntityList<BLabTestItem> entity)
        {
            EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> tmplist = new EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO>();
            tmplist.list = new List<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO>();
            if (entity != null && entity.list != null && entity.count > 0)
            {
                tmplist.count = entity.count;
                foreach (var e in entity.list)
                {
                    ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO tmp = new WeiXin.Entity.ViewObject.Response.BLabTestItemVO();
                    tmp= tmp.TransVO(e);
                    tmplist.list.Add(tmp);
                }
                return tmplist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取某一组套项目的项目明细信息
        /// </summary>
        /// <param name="pItemNo"></param>
        /// <param name="areaID"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<BLabTestItem> SearchBLabGroupItemByPItemNoAndAreaID(string pItemNo, string areaID, int page, int limit, string sort)
        {
            EntityList<BLabTestItem> tempList = new EntityList<BLabTestItem>();
            ClientEleArea cea = IDClientEleArea.Get(long.Parse(areaID));
            if (cea != null && cea.ClientNo > 0 && cea.ClientNo.ToString().Trim() != "" && !string.IsNullOrEmpty(pItemNo))
            {
                EntityList<BLabGroupItem> tmpGroupItemlist = new EntityList<BLabGroupItem>();
                string tmpwhere = "LabCode='" + cea.ClientNo + "' and PItemNo='" + pItemNo + "'";
                tmpGroupItemlist = IDBLabGroupItemDao.GetListByHQL(tmpwhere, -1, -1);
                StringBuilder strb = new StringBuilder();
                if (tmpGroupItemlist != null && tmpGroupItemlist.list != null && tmpGroupItemlist.count > 0)
                {
                    foreach (var item in tmpGroupItemlist.list)
                    {
                        if (!string.IsNullOrEmpty(item.ItemNo))
                            strb.Append("'" + item.ItemNo + "',");
                    }
                    if (!string.IsNullOrEmpty(strb.ToString()))
                    {
                        string itemNoStr = strb.ToString().TrimEnd(',');
                        string sqlwhere = "LabCode='" + cea.ClientNo + "' and ItemNo in(" + itemNoStr + ")";
                        tempList = DBDao.GetListByHQL(sqlwhere, sort, page, limit);
                    }
                }
            }
            if (tempList != null && tempList.list == null)
            {
                tempList.count = 0;
                tempList.list = new List<BLabTestItem>();
            }
            return tempList;
        }

        public EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchAllBLabTestItemByAreaID(string AreaID, int page, int limit, string sort, string where)
        {
            ClientEleArea cea = IDClientEleArea.Get(long.Parse(AreaID));
            if (cea != null && cea.ClientNo > 0 && cea.ClientNo.ToString().Trim() != "")
            {
                return SearchAllBLabTestItemByLabID(cea.ClientNo.ToString(), page, limit, sort, where);
            }
            else
            {
                return null;
            }
        }

        private EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchAllBLabTestItemByLabID(string LabID, int page, int limit, string sort, string where)
        {
            if (LabID != null && LabID.Trim() != "")
            {
                //添加上只查询为主套的标记且价格大于0的条件
                string defalutWhere = " LabCode='" + LabID + "' ";
                string tmpwhere = (where != null && where.Trim() != "") ? defalutWhere + " and " + where : defalutWhere;
                var tmplist = DBDao.GetListByHQL(tmpwhere, sort, page, limit);
                if (tmplist != null && tmplist.list != null && tmplist.list.Count > 0)
                {
                    List<string> tmpItemNolist = new List<string>();
                    foreach (var i in tmplist.list)
                    {
                        tmpItemNolist.Add(i.ItemNo);
                    }
                    if (tmpItemNolist.Count > 0)
                    {
                        var itemcontrollist=IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + LabID + "' and ControlItemNo in ('" + string.Join("','", tmpItemNolist.ToArray()) + "')  ");
                        if (itemcontrollist != null && itemcontrollist.Count > 0)
                        {
                            foreach (var i in tmplist.list)
                            {
                                i.IsMappingFlag = (itemcontrollist.Count(a => a.ControlItemNo == i.ItemNo) > 0) ? true : false;
                            }
                        }
                    }

                    return TransVO(tmplist);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某一组套项目的项目明细信息(最小项目单元)
        /// </summary>
        /// <param name="pItemNo"></param>
        /// <param name="areaID"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<BLabTestItem> SearchBLabGroupItemSubItemByPItemNoAndAreaID(string pItemNo, string areaID, int page, int limit, string sort)
        {
            EntityList<BLabTestItem> tempList = new EntityList<BLabTestItem>();
            ClientEleArea cea = IDClientEleArea.Get(long.Parse(areaID));
            if (cea != null && cea.ClientNo > 0 && cea.ClientNo.ToString().Trim() != "" && !string.IsNullOrEmpty(pItemNo))
            {
                List<BLabGroupItem> tempSubItemList= GetBLabGroupItemSubItemNoByPItemNoAndClientNo(pItemNo, cea.ClientNo.ToString().Trim());
                if (tempSubItemList != null && tempSubItemList.Count > 0)
                {
                    List<string> tempSubItemNoList = new List<string>();
                    foreach (var blgi in tempSubItemList)
                    {
                        tempSubItemNoList.Add(blgi.ItemNo);
                    }
                    string sqlwhere = "LabCode='" + cea.ClientNo + "' and ItemNo in('" + string.Join("','", tempSubItemNoList.ToArray()) + "')";
                    tempList = DBDao.GetListByHQL(sqlwhere, sort, page, limit);
                }
            }
            if (tempList != null && tempList.list == null)
            {
                tempList.count = 0;
                tempList.list = new List<BLabTestItem>();
            }
            return tempList;
        }

        public List<BLabGroupItem> GetBLabGroupItemSubItemNoByPItemNoAndClientNo(string pItemNo, string ClientNo)
        {
            List<BLabGroupItem> tempSubItemList = new List<BLabGroupItem>();
            if (ClientNo.ToString().Trim() != "" && !string.IsNullOrEmpty(pItemNo))
            {
                EntityList<BLabGroupItem> tmpGroupItemlist = new EntityList<BLabGroupItem>();
                string tmpwhere = "LabCode='" + ClientNo + "' and PItemNo='" + pItemNo + "'";
                tmpGroupItemlist = IDBLabGroupItemDao.GetListByHQL(tmpwhere, -1, -1);
                if (tmpGroupItemlist != null && tmpGroupItemlist.list != null && tmpGroupItemlist.count > 0)
                {
                    foreach (var item in tmpGroupItemlist.list)
                    {
                        if (!string.IsNullOrEmpty(item.ItemNo)&& tmpGroupItemlist.list.Count(a=>a.PItemNo== item.ItemNo)<=0)
                        {
                           var subitemno= GetBLabGroupItemSubItemNoByPItemNoAndClientNo(item.ItemNo, ClientNo);
                            if (subitemno != null && subitemno.Count > 0)
                            {
                                tempSubItemList.InsertRange(0, subitemno);
                            }
                            else
                            {
                                tempSubItemList.Insert(0, item);
                            }
                        }
                            
                    }
                  
                }
            }
            
            return tempSubItemList;
        }

        public EntityList<ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO> SearchAllBLabTestItemByLabCode(string labCode, int page, int limit, string sort, string where)
        {
            return SearchAllBLabTestItemByLabID(labCode, page, limit, sort, where);
        }

        public EntityList<BLabTestItem> SearchBLabGroupItemSubItemByPItemNoAndLabCode(string pItemNo, string LabCode, int page, int limit, string sort)
        {
            EntityList<BLabTestItem> tempList = new EntityList<BLabTestItem>();
            List<BLabGroupItem> tempSubItemList = GetBLabGroupItemSubItemNoByPItemNoAndClientNo(pItemNo, LabCode.Trim());
            if (tempSubItemList != null && tempSubItemList.Count > 0)
            {
                List<string> tempSubItemNoList = new List<string>();
                foreach (var blgi in tempSubItemList)
                {
                    tempSubItemNoList.Add(blgi.ItemNo);
                }
                string sqlwhere = "LabCode='" + LabCode + "' and ItemNo in('" + string.Join("','", tempSubItemNoList.ToArray()) + "')";
                tempList = DBDao.GetListByHQL(sqlwhere, sort, page, limit);
            }
            if (tempList != null && tempList.list == null)
            {
                tempList.count = 0;
                tempList.list = new List<BLabTestItem>();
            }
            return tempList;
        }

        public EntityList<Entity.ViewObject.Response.BLabGroupItemVO> SearchBLabGroupItemSubItemVOByPItemNoAndLabCode(string pItemNo, string LabCode, int page, int limit, string sort)
        {
            EntityList<Entity.ViewObject.Response.BLabGroupItemVO> tempListVO = new EntityList<Entity.ViewObject.Response.BLabGroupItemVO>();

            EntityList<BLabTestItem> tempList = new EntityList<BLabTestItem>();

            List<BLabGroupItem> tempSubItemList = GetBLabGroupItemSubItemNoByPItemNoAndClientNo(pItemNo, LabCode.Trim());
            if (tempSubItemList != null && tempSubItemList.Count > 0)
            {
                List<string> tempSubItemNoList = new List<string>();
                foreach (var blgi in tempSubItemList)
                {
                    tempSubItemNoList.Add(blgi.ItemNo);
                }
                string sqlwhere = "LabCode='" + LabCode + "' and ItemNo in('" + string.Join("','", tempSubItemNoList.ToArray()) + "')";
                tempList = DBDao.GetListByHQL(sqlwhere, sort, page, limit);
            }
            if (tempList != null && tempList.list == null)
            {
                tempListVO.count = 0;
                tempListVO.list = new List<Entity.ViewObject.Response.BLabGroupItemVO>();
            }
            else
            {
                tempListVO.count = tempList.count;
                tempListVO.list = new List<Entity.ViewObject.Response.BLabGroupItemVO>();
                foreach (var item in tempList.list)
                {
                    ZhiFang.WeiXin.Entity.ViewObject.Response.BLabGroupItemVO blgiVO = new WeiXin.Entity.ViewObject.Response.BLabGroupItemVO();
                    blgiVO.ItemNo = item.ItemNo;
                    blgiVO.LabCode =LabCode;
                    blgiVO.PItemNo = pItemNo;
                    ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO tmp = new WeiXin.Entity.ViewObject.Response.BLabTestItemVO();
                    blgiVO.BLabTestItemVO = tmp.TransVO(item);
                    var gi = tempSubItemList.Where(a => a.PItemNo == pItemNo && a.ItemNo == item.ItemNo);
                    if (gi != null && gi.Count() > 0)
                    {
                        blgiVO.Id = gi.ElementAt(0).Id;                       
                        blgiVO.Price = gi.ElementAt(0).Price;
                        blgiVO.DataAddTime = gi.ElementAt(0).DataAddTime;
                    }
                    tempListVO.list.Add(blgiVO);
                }
            }
            return tempListVO;
        }

        public BaseResultDataValue AddByBLabTestItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity==null)
            {
                brdv.success= false;
                brdv.ErrorInfo = "参数为空！";
                return brdv;
            }
            if (entity.LabSubTestItem != null && entity.LabSubTestItem.Count > 0)
            {
                if (entity.SubBLabTestItem != null && entity.SubBLabTestItem.Count > 0 && entity.SubBLabTestItem.Count(a => a.ItemNo == entity.ItemNo) > 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "细项列表中ItemNo同主项目ItemNo重复！";
                    return brdv;
                }
            }
            ZhiFang.WeiXin.Entity.BLabTestItem blti = new BLabTestItem();
            blti.BonusPercent = entity.BonusPercent;
            blti.CName = entity.CName;
            blti.Color = entity.Color;
            blti.CostPrice = entity.BonusPercent;
            blti.Cuegrade = entity.Cuegrade;
            blti.DataAddTime = entity.DataAddTime;
            blti.DiagMethod = entity.DiagMethod;
            blti.DispOrder = entity.DispOrder;
            blti.EName = entity.EName;
            blti.FWorkLoad = entity.FWorkLoad;
            blti.GreatMasterPrice = entity.GreatMasterPrice;
            blti.InspectionPrice = entity.InspectionPrice;
            blti.IsCalc = entity.IsCalc;
            blti.IschargeItem = entity.IschargeItem;
            blti.IsCombiItem = entity.IsCombiItem;
            blti.IsDoctorItem = entity.IsDoctorItem;
            blti.IsMappingFlag = entity.IsMappingFlag;
            blti.IsProfile = entity.IsProfile;
            blti.ItemDesc = entity.ItemDesc;
            blti.ItemNo = entity.ItemNo;
            blti.LabCode = entity.LabCode;
            blti.LabID = entity.LabID;
            blti.LabSuperGroupNo = entity.LabSuperGroupNo;
            blti.MarketPrice = entity.MarketPrice;
            blti.OrderNo = entity.OrderNo;
            blti.Pic = entity.Pic;
            blti.Prec = entity.Prec;
            blti.Price = entity.Price;
            blti.Secretgrade = entity.Secretgrade;
            blti.ShortCode = entity.ShortCode;
            blti.ShortName = entity.ShortName;
            blti.StandardCode = entity.StandardCode;
            blti.StandCode = entity.StandCode;
            blti.Unit = entity.Unit;
            blti.UseFlag = entity.UseFlag;
            blti.Visible = entity.Visible;
            blti.ZFStandCode = entity.ZFStandCode;
            if (DBDao.Save(blti))
            {
                if (entity.LabSubTestItem != null && entity.LabSubTestItem.Count > 0)
                {
                    foreach (var gi in entity.LabSubTestItem)
                    {
                        BLabGroupItem blgi = new BLabGroupItem();
                        blgi.LabCode = entity.LabCode;
                        blgi.ItemNo = gi.ItemNo;
                        blgi.PItemNo = entity.ItemNo;
                        blgi.Price = gi.Price;
                        IDBLabGroupItemDao.Save(blgi);
                    }
                }
                brdv.success = true;
                return brdv;
            }
            brdv.success = false;
            brdv.ErrorInfo = "新增主项目错误！";
            return brdv;
        }

        public BaseResultBool UpdateBLabTestItemByFieldVO(string[] strParas, Entity.ViewObject.Request.BLabTestItemVO entity)
        {
            BaseResultBool brb = new BaseResultBool();
            if (entity == null)
            {
                brb.success = false;
                brb.ErrorInfo = "参数为空！";
                return brb;
            }
            if (entity.LabSubTestItem != null && entity.LabSubTestItem.Count > 0)
            {
                if (entity.SubBLabTestItem != null && entity.SubBLabTestItem.Count > 0 && entity.SubBLabTestItem.Count(a => a.ItemNo == entity.ItemNo) > 0)
                {
                    brb.success = false;
                    brb.ErrorInfo = "细项列表中ItemNo同主项目ItemNo重复！";
                    return brb;
                }
            }

            this.Entity = (ZhiFang.WeiXin.Entity.BLabTestItem)entity;
            if (DBDao.Update(strParas))
            {
                ZhiFang.Common.Log.Log.Debug("BBLabTestItem.UpdateBLabTestItemByFieldVO.细项删除动作.From BLabGroupItem where PItemNo='" + entity.ItemNo + "' and  LabCode='" + entity.LabCode + "' ");
                if (IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem where PItemNo='" + entity.ItemNo + "' and  LabCode='" + entity.LabCode + "' ") >= 0)
                {
                    if (entity != null && entity.LabSubTestItem != null && entity.LabSubTestItem.Count > 0)
                    {
                        foreach (var gi in entity.LabSubTestItem)
                        {
                            BLabGroupItem blgi = new BLabGroupItem();
                            blgi.LabCode = entity.LabCode;
                            blgi.ItemNo = gi.ItemNo;
                            blgi.PItemNo = entity.ItemNo;
                            blgi.Price = gi.Price;
                            IDBLabGroupItemDao.Save(blgi);
                        }
                    }
                    brb.success = true;
                    return brb;
                }
            }
            brb.success = false;
            brb.ErrorInfo = "修改主项目错误！";
            return brb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0全部、1已对照、2未对照</param>
        /// <param name="LabCode"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchBLabTestItemByLabCodeAndType(string type, string LabCode, string where, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (sort == null || sort.Trim() == "")
            {
                sort = " ItemNo asc ";
            }
            EntityList<BLabTestItem> bltilist = DBDao.GetListByHQL(where + " and LabCode='" + LabCode + "'" , sort,0, 10000 );
            if (bltilist != null&& bltilist.count>0)
            {
                List<string> LabTestItemNoList = new List<string>();
                foreach (var i in bltilist.list)
                {
                    LabTestItemNoList.Add(i.ItemNo);
                }
                IList<BTestItemControl> bticlist = IDBTestItemControlDao.GetListByHQL(" ControlLabNo = '" + LabCode + "'");
                IList<TestItem> ticlist = new List<TestItem>();
                if (bticlist != null && bticlist.Count > 0)
                {
                    List<string> TestItemNoList = new List<string>();
                    foreach (var i in bticlist)
                    {
                        TestItemNoList.Add(i.ItemNo);
                    }
                    ticlist = IDItemAllItemDao.GetListByHQL(" 1=1  ");
                }
                List<BTestItemControlVO> bltivolist = new List<BTestItemControlVO>();
                if (type.Trim() == "0")
                {
                    for (int i = 0; i < bltilist.count; i++)
                    {
                        BTestItemControlVO bltivo = new BTestItemControlVO();
                        bltivo.BLabTestItemCName = (bltilist.list[i].CName != null && bltilist.list[i].CName.Trim() != "") ? bltilist.list[i].CName : "";
                        bltivo.ControlItemNo = bltilist.list[i].ItemNo;
                        bltivo.ControlLabNo = bltilist.list[i].LabCode;
                        bltivo.UseFlag = bltilist.list[i].UseFlag;
                        var bttc = bticlist.Where(a => a.ControlLabNo == LabCode && a.ControlItemNo == bltilist.list[i].ItemNo);
                        if (bttc != null && bttc.Count() > 0)
                        {
                            //bltivo.BLabTestItem.IsMappingFlag = true;
                            bltivo.ControlItemNo = bttc.ElementAt(0).ControlItemNo;
                            bltivo.ControlLabNo = bttc.ElementAt(0).ControlLabNo;
                            bltivo.DataAddTime = bttc.ElementAt(0).DataAddTime;
                            bltivo.Id = bttc.ElementAt(0).Id;
                            bltivo.ItemControlNo = bttc.ElementAt(0).ItemControlNo;
                            bltivo.ItemNo = bttc.ElementAt(0).ItemNo;
                            

                            var testitem = ticlist.Where(a => a.Id == long.Parse(bltivo.ItemNo));
                            if (testitem != null && testitem.Count() > 0)
                            {
                                //bltivo.TestItem = testitem.ElementAt(0);
                                bltivo.TestItemCName = (testitem.ElementAt(0).CName != null && testitem.ElementAt(0).CName.Trim() != "") ? testitem.ElementAt(0).CName : "";
                            }
                        }
                        
                        bltivolist.Add(bltivo);
                    }
                    //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    //brdv.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTestItemControlVO>(bltivolist);
                    brdv.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializer(bltivolist);
                    return brdv;
                }

                if (type.Trim() == "1")
                {
                    for (int i = 0; i < bltilist.count; i++)
                    {
                        BTestItemControlVO bltivo = new BTestItemControlVO();
                        bltivo.BLabTestItemCName = (bltilist.list[i].CName != null && bltilist.list[i].CName.Trim() != "") ? bltilist.list[i].CName : "";
                        bltivo.ControlItemNo = bltilist.list[i].ItemNo;
                        bltivo.ControlLabNo = bltilist.list[i].LabCode;
                        bltivo.UseFlag = bltilist.list[i].UseFlag;
                        var bttc = bticlist.Where(a => a.ControlLabNo == LabCode && a.ControlItemNo == bltilist.list[i].ItemNo);
                        if (bttc != null && bttc.Count() > 0)
                        {
                            //bltivo.BLabTestItem.IsMappingFlag = true;
                            bltivo.ControlItemNo = bttc.ElementAt(0).ControlItemNo;
                            bltivo.ControlLabNo = bttc.ElementAt(0).ControlLabNo;
                            bltivo.DataAddTime = bttc.ElementAt(0).DataAddTime;
                            bltivo.Id = bttc.ElementAt(0).Id;
                            bltivo.ItemControlNo = bttc.ElementAt(0).ItemControlNo;
                            bltivo.ItemNo = bttc.ElementAt(0).ItemNo;

                            var testitem = ticlist.Where(a => a.Id == long.Parse(bltivo.ItemNo));
                            if (testitem != null && testitem.Count() > 0)
                            {
                                //bltivo.TestItem = testitem.ElementAt(0);
                                bltivo.TestItemCName = (testitem.ElementAt(0).CName != null && testitem.ElementAt(0).CName.Trim() != "") ? testitem.ElementAt(0).CName : "";
                            }
                            bltivolist.Add(bltivo);
                        }                       
                    }
                    brdv.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializer(bltivolist);
                    return brdv;
                }

                if (type.Trim() == "2")
                {
                    for (int i = 0; i < bltilist.count; i++)
                    {
                        BTestItemControlVO bltivo = new BTestItemControlVO();
                        bltivo.BLabTestItemCName = (bltilist.list[i].CName != null && bltilist.list[i].CName.Trim() != "") ? bltilist.list[i].CName : "";
                        bltivo.ControlItemNo = bltilist.list[i].ItemNo;
                        bltivo.ControlLabNo = bltilist.list[i].LabCode;
                        bltivo.UseFlag = bltilist.list[i].UseFlag;
                        var bttc = bticlist.Where(a => a.ControlLabNo == LabCode && a.ControlItemNo == bltilist.list[i].ItemNo);
                        if (!(bttc != null && bttc.Count() > 0))
                        {
                            bltivolist.Add(bltivo);
                        }
                    }
                    brdv.ResultDataValue = ZhiFang.WeiXin.Common.JsonSerializer.JsonDotNetSerializer(bltivolist);
                    return brdv;
                }
            }
            //IDBTestItemControlDao.
            return brdv;
        }

        public BaseResultDataValue TestItemCopyAll(string sourceLabCode, List<string> labCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (labCodeList == null || labCodeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室列表为空！";
                Log.Debug("BBLabTestItem.TestItemCopyAll.labCodeList.实验室列表为空！");
                return brdv;
            }
            IList<BLabTestItem> testitemlist = DBDao.GetListByHQL(" Visible=1 and labcode=" + sourceLabCode);
            if (testitemlist == null || testitemlist.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "中心项目列表为空！";
                Log.Debug("BBLabTestItem.TestItemCopyAll.中心项目列表为空！");
                return brdv;
            }
            IList<BLabGroupItem> groupitemlist = IDBLabGroupItemDao.GetListByHQL(" 1=1  and labcode='" + sourceLabCode + "'");

            IList<BTestItemControl> testitemcontrollist = IDBTestItemControlDao.GetListByHQL(" 1=1  and ControlLabNo='" + sourceLabCode + "'");

            switch (OverRideType)
            {
                case 0:
                    #region 克隆
                    ZhiFang.Common.Log.Log.Debug("BBLabTestItem.TestItemCopyAll.克隆.细项删除动作.From BLabGroupItem  where LabCode in ('" + String.Join("', '", labCodeList.ToArray()) + "')");
                    IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where ControlLabNo in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    DBDao.DeleteByHql(" From BLabTestItem  where LabCode in ('" + String.Join("','", labCodeList.ToArray()) + "')");
                    foreach (string labcode in labCodeList)
                    {
                        foreach (var testitem in testitemlist)
                        {
                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.LabCode = labcode;
                            DBDao.Save(blti);
                            //BTestItemControl btic = new BTestItemControl();
                            //btic.ItemNo = testitem.Id.ToString();
                            //btic.ControlItemNo = blti.ItemNo;
                            //btic.ControlLabNo = labcode;
                            //btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            //btic.DataAddTime = DateTime.Now;
                            //IDBTestItemControlDao.Save(btic);
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
                        foreach (var testitemcontrol in testitemcontrollist)
                        {
                            BTestItemControl btic = new BTestItemControl();
                            btic.ItemNo = testitemcontrol.ItemNo;
                            btic.ControlLabNo = labcode;
                            btic.ControlItemNo = testitemcontrol.ControlItemNo;
                            btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            btic.UseFlag = testitemcontrol.UseFlag;
                            btic.DataAddTime = DateTime.Now;
                            IDBTestItemControlDao.Save(btic);
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
                            var testitemcon = IDBTestItemControlDao.GetListByHQL(" ControlLabNo='" + labcode + "' and ItemNo='" + testitem.ItemNo + "' ");
                            if (testitemcon != null && testitemcon.Count > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("BBLabTestItem.TestItemCopyAll.覆盖.细项删除动作.From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBLabGroupItemDao.DeleteByHql(" From BLabGroupItem  where LabCode ='" + labcode + "' and PItemNo='" + testitemcon[0].ControlItemNo + "'");
                                IDBTestItemControlDao.DeleteByHql(" From BTestItemControl  where  ControlLabNo='" + labcode + "' and ItemNo='" + testitem.Id + "' ");
                                DBDao.DeleteByHql(" From BLabTestItem  where LabCode ='" + labcode + "'and ItemNo='" + testitemcon[0].ControlItemNo + "' ");
                            }

                            BLabTestItem blti = new BLabTestItem();
                            blti = SetValue(testitem);
                            blti.LabCode = labcode;
                            DBDao.Save(blti);
                            //BTestItemControl btic = new BTestItemControl();
                            //btic.ItemNo = testitem.Id.ToString();
                            //btic.ControlItemNo = blti.ItemNo;
                            //btic.ControlLabNo = labcode;
                            //btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            //btic.DataAddTime = DateTime.Now;
                            //IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.ItemNo);
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
                            var ttclist = testitemcontrollist.Where(a => a.ItemControlNo == testitem.ItemNo);
                            if (ttclist != null && ttclist.Count() > 0)
                            {
                                foreach (var ttc in ttclist)
                                {
                                    BTestItemControl btic = new BTestItemControl();
                                    btic.ItemNo = ttc.ItemNo;
                                    btic.ControlItemNo = ttc.ControlItemNo;
                                    btic.ControlLabNo = labcode;
                                    btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                                    btic.DataAddTime = DateTime.Now;
                                    IDBTestItemControlDao.Save(btic);
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
                            blti.LabCode = labcode;
                            DBDao.Save(blti);
                            //BTestItemControl btic = new BTestItemControl();
                            //btic.ItemNo = testitem.Id.ToString();
                            //btic.ControlItemNo = blti.ItemNo;
                            //btic.ControlLabNo = labcode;
                            //btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                            //btic.DataAddTime = DateTime.Now;
                            //IDBTestItemControlDao.Save(btic);

                            var tmp = groupitemlist.Where(a => a.PItemNo == testitem.ItemNo);
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

                            var ttclist = testitemcontrollist.Where(a => a.ItemControlNo == testitem.ItemNo);
                            if (ttclist != null && ttclist.Count() > 0)
                            {
                                foreach (var ttc in ttclist)
                                {
                                    BTestItemControl btic = new BTestItemControl();
                                    btic.ItemNo = ttc.ItemNo;
                                    btic.ControlItemNo = ttc.ControlItemNo;
                                    btic.ControlLabNo = labcode;
                                    btic.ItemControlNo = btic.ControlLabNo + "_" + btic.ItemNo + "_" + btic.ControlItemNo;
                                    btic.DataAddTime = DateTime.Now;
                                    IDBTestItemControlDao.Save(btic);
                                }
                            }
                        }
                    }
                    #endregion
                    break;
            }
            return brdv;
        }

        public BaseResultDataValue TestItemCopy(string sourceLabCode, List<string> labCodeList, List<string> itemNoList, int OverRideType)
        {
            throw new NotImplementedException();
        }
        private BLabTestItem SetValue(BLabTestItem subitem)
        {
            BLabTestItem blti = new BLabTestItem();
            blti.Price = subitem.Price;
            blti.BonusPercent = subitem.BonusPercent;
            blti.CName = subitem.CName;
            blti.Color = subitem.Color;
            blti.CostPrice = subitem.CostPrice;
            blti.Cuegrade = subitem.Cuegrade;
            blti.DataAddTime = DateTime.Now;
            blti.DiagMethod = subitem.DiagMethod;
            blti.DispOrder = subitem.DispOrder;
            blti.EName = subitem.EName;
            blti.FWorkLoad = subitem.FWorkLoad;
            blti.InspectionPrice = subitem.InspectionPrice;
            blti.IsCalc = subitem.IsCalc;
            blti.IschargeItem = subitem.IschargeItem;
            blti.IsCombiItem = subitem.IsCombiItem;
            blti.IsDoctorItem = subitem.IsDoctorItem;
            blti.IsProfile = subitem.IsProfile;
            blti.ItemDesc = subitem.ItemDesc;
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
            blti.ItemNo = subitem.ItemNo;            
            blti.CostPrice = subitem.CostPrice;
            blti.GreatMasterPrice = subitem.GreatMasterPrice;
            blti.InspectionPrice = subitem.InspectionPrice;
            blti.MarketPrice = subitem.MarketPrice;
            blti.BonusPercent = subitem.BonusPercent;
            return blti;
        }

        public override bool Remove(long longID)
        {           
            var blti= DBDao.Get(longID);
            ZhiFang.Common.Log.Log.Debug("BBLabTestItem.Remove.细项删除动作.from BLabGroupItem where LabCode='" + blti.LabCode + "' and PItemNo='" + blti.ItemNo + "' ");
            IDBLabGroupItemDao.DeleteByHql(" from BLabGroupItem where LabCode='" + blti.LabCode + "' and PItemNo='" + blti.ItemNo + "' ");
            IDBTestItemControlDao.DeleteByHql(" from BTestItemControl where ControlLabNo='" + blti.LabCode + "' and ControlItemNo='" + blti.ItemNo + "' ");
            return DBDao.Delete(longID);
        }

        public List<ApplyInputItemEntity> ItemEntityDataTableToList(List<BLabTestItem> BLabTestItemlist, List<TestItem> TestItemlist)
        {
            List<ApplyInputItemEntity> modelList = new List<ApplyInputItemEntity>();
            if (BLabTestItemlist.Count > 0) {
                int rowsCount = BLabTestItemlist.Count;
                if (rowsCount > 0)
                {
                    ApplyInputItemEntity model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new ApplyInputItemEntity();
                        model.ItemNo = BLabTestItemlist[n].ItemNo.ToString();

                        model.CName = BLabTestItemlist[n].CName.ToString();
                        model.EName = BLabTestItemlist[n].EName.ToString();

                        model.ColorName = BLabTestItemlist[n].Color.ToString();
                        model.isCombiItem = BLabTestItemlist[n].IsCombiItem.ToString();
                        if (BLabTestItemlist[n].Color.ToString() != "")
                        {
                            IList<ItemColorDict> ItemColorDictList = IDItemColorDictDao.GetListByHQL("itemcolordict.id=" + BLabTestItemlist[n].Color.ToString() + " or itemcolordict.ColorName='" + BLabTestItemlist[n].Color.ToString() + "'");
                            if (ItemColorDictList  == null || ItemColorDictList.Count <= 0)
                                model.ColorValue = "";
                            else
                                model.ColorValue = ItemColorDictList[0].ColorValue;

                           
                            List<SampleType> sampleTypeList = new List<SampleType>();
                            foreach (var item in ItemColorDictList)
                            {
                                IList<ItemColorAndSampleTypeDetail> ItemColorAndSampleTypeDetailList = IDItemColorAndSampleTypeDetailDao.GetListByHQL("itemcolorandsampletypedetail.ColorId="+item.Id);
                                foreach (var item2 in ItemColorAndSampleTypeDetailList)
                                {
                                    IList<SampleType> SampleTypeList = IDSampleTypeDao.GetListByHQL("sampletype.Id="+ item2.SampleTypeNo);
                                    sampleTypeList.AddRange(SampleTypeList);
                                }
                            }
                            
                            List<SampleTypeDetail> UisampleTypeDetailList = new List<SampleTypeDetail>();
                            SampleTypeDetail UisampleTypeDetail = new SampleTypeDetail();
                            foreach (var sampleType in sampleTypeList)
                            {
                                UisampleTypeDetail = new SampleTypeDetail();
                                UisampleTypeDetail.CName = sampleType.CName;
                                UisampleTypeDetail.SampleTypeID = sampleType.Id.ToString();
                                UisampleTypeDetailList.Add(UisampleTypeDetail);
                            }
                            model.SampleTypeDetail = UisampleTypeDetailList;
                        }
                        model.Prices = BLabTestItemlist[n].Price.ToString();
                        modelList.Add(model);
                    }
                }
            } 
            else if (TestItemlist.Count>0) {
                int rowsCount = TestItemlist.Count;
                if (rowsCount > 0)
                {
                    ApplyInputItemEntity model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new ApplyInputItemEntity();
                        model.ItemNo = TestItemlist[n].Id.ToString();

                        model.CName = TestItemlist[n].CName.ToString();
                        model.EName = TestItemlist[n].EName.ToString();

                        model.ColorName = TestItemlist[n].Color.ToString();
                        model.isCombiItem = TestItemlist[n].IsCombiItem.ToString();
                        if (TestItemlist[n].Color.ToString() != "")
                        {
                            IList<ItemColorDict> ItemColorDictList = IDItemColorDictDao.GetListByHQL("itemcolordict.id=" + TestItemlist[n].Color.ToString() + " or itemcolordict.ColorName='" + TestItemlist[n].Color.ToString() + "'");
                            if (ItemColorDictList == null || ItemColorDictList.Count <= 0)
                                model.ColorValue = "";
                            else
                                model.ColorValue = ItemColorDictList[0].ColorValue;


                            List<SampleType> sampleTypeList = new List<SampleType>();
                            foreach (var item in ItemColorDictList)
                            {
                                IList<ItemColorAndSampleTypeDetail> ItemColorAndSampleTypeDetailList = IDItemColorAndSampleTypeDetailDao.GetListByHQL("itemcolorandsampletypedetail.ColorId=" + item.Id);
                                foreach (var item2 in ItemColorAndSampleTypeDetailList)
                                {
                                    IList<SampleType> SampleTypeList = IDSampleTypeDao.GetListByHQL("sampletype.Id=" + item2.SampleTypeNo);
                                    sampleTypeList.AddRange(SampleTypeList);
                                }
                            }

                            List<SampleTypeDetail> UisampleTypeDetailList = new List<SampleTypeDetail>();
                            SampleTypeDetail UisampleTypeDetail = new SampleTypeDetail();
                            foreach (var sampleType in sampleTypeList)
                            {
                                UisampleTypeDetail = new SampleTypeDetail();
                                UisampleTypeDetail.CName = sampleType.CName;
                                UisampleTypeDetail.SampleTypeID = sampleType.Id.ToString();
                                UisampleTypeDetailList.Add(UisampleTypeDetail);
                            }
                            model.SampleTypeDetail = UisampleTypeDetailList;
                        }
                        model.Prices = TestItemlist[n].Price.ToString();
                        modelList.Add(model);
                    }
                }
            }
            
            return modelList;
        }

        public BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int page, string labcode)
        {
            BaseResultDataValue resultObj = new BaseResultDataValue();
            EntityList<ApplyInputItemEntity> entityList = new EntityList<ApplyInputItemEntity>();
            EntityList<BLabTestItem> ds = new EntityList<BLabTestItem>();
            EntityList<TestItem> tyds = new EntityList<TestItem>();
            int AllItemCount = 0;
           
            #region 如果医疗机构编码不存在 按照中心项目字典表显示项目
            switch ((TestItemSuperGroupClass)Enum.Parse(typeof(TestItemSuperGroupClass), supergroupno.ToUpper()))
            {
                case TestItemSuperGroupClass.ALL:
                    if (labcode != null && labcode != "")
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = "blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.CName=" + itemkey + "" + " and blabtestitem.EName=" + itemkey + "" + " and blabtestitem.ShortCode=" + itemkey + "" + " and blabtestitem.ShortName=" + itemkey + "" + " and blabtestitem.Visible=1";
                        }
                        else
                        {
                            hbw = "blabtestitem.LabCode=" + labcode + " and blabtestitem.Visible=1";
                        }

                        ds = SearchListByHQL(hbw, page, rows);
                        AllItemCount = SearchListByHQL(hbw).Count;
                    }
                    else
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = "testitem.CName=" + itemkey + "" + " and testitem.EName=" + itemkey + "" + " and testitem.ShortCode=" + itemkey + "" + " and testitem.ShortName=" + itemkey + "" + " and testitem.Visible=1";
                        }
                        else
                        {
                            hbw = "testitem.Visible=1";
                        }
                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw).Count;
                    }
                    break;

                case TestItemSuperGroupClass.COMBI:
                    if (labcode != null && labcode != "")
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = " blabtestitem.LabCode=" + labcode + " and blabtestitem.CName=" + itemkey + "" + " and blabtestitem.EName=" + itemkey + "" + " and blabtestitem.ShortCode=" + itemkey + "" + " and blabtestitem.ShortName=" + itemkey + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "' and blabtestitem.Visible=1" + " and blabtestitem.UseFlag=1";
                        }
                        else
                        {
                            hbw = " blabtestitem.LabCode=" + labcode  + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "' and blabtestitem.Visible=1" + " and blabtestitem.UseFlag=1";
                        }

                        ds = SearchListByHQL(hbw, page, rows);
                        AllItemCount = SearchListByHQL(hbw).Count;
                    }
                    else
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = "testitem.CName=" + itemkey + "" + " and testitem.EName=" + itemkey + "" + " and testitem.ShortCode=" + itemkey + "" + " and testitem.ShortName=" + itemkey + "" + " and testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "' and testitem.UseFlag=1" + " and testitem.Visible=1";
                        }
                        else
                        {
                            hbw = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "' and testitem.UseFlag=1" + " and testitem.Visible=1";
                        }

                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        string hbw2 = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "' and testitem.UseFlag=1" + " and testitem.Visible=1";
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw2).Count;
                    }
                    break;
                case TestItemSuperGroupClass.CHARGE:
                    if (labcode != null && labcode != "")
                    {

                        string hbw = " blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.CHARGE + "' and blabtestitem.Visible=1";
                        ds = SearchListByHQL(hbw, page, rows);
                        AllItemCount = SearchListByHQL(hbw).Count;
                    }
                    else
                    {

                        string hbw = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.CHARGE + "' and testitem.IsDoctorItem=1" + " and testitem.Visible=1";
                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw).Count;
                    }
                    break;
                case TestItemSuperGroupClass.COMBIITEMPROFILE:
                    if (labcode != null && labcode != "")
                    {

                        string hbw = " blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBIITEMPROFILE + "' and blabtestitem.Visible=1";
                        ds = SearchListByHQL(hbw, page, rows);
                        AllItemCount = SearchListByHQL(hbw).Count;
                    }
                    else
                    {
                        string hbw = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBIITEMPROFILE + "' and testitem.Visible=1";
                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw).Count;
                    }
                    break;
                case TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                    if (labcode != null && labcode != "")
                    {
                        string hbw = " blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.DOCTORCOMBICHARGE + "' and blabtestitem.IsDoctorItem=1" + " and blabtestitem.Visible=1";
                        ds = SearchListByHQL(hbw, page, rows);
                        AllItemCount = SearchListByHQL(hbw).Count;
                    }
                    else
                    {
                        string hbw = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.DOCTORCOMBICHARGE + "' and testitem.IsDoctorItem=1" + " and testitem.Visible=1";
                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw).Count;
                    }
                    break;
                default:
                    if (labcode != null && labcode != "")
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = "blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.CName=" + itemkey + "" + " and blabtestitem.EName=" + itemkey + "" + " and blabtestitem.ShortCode=" + itemkey + "" + " and blabtestitem.ShortName=" + itemkey + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and blabtestitem.UseFlag=1" + " and blabtestitem.Visible=1";
                        }
                        else
                        {
                            hbw = "blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and blabtestitem.UseFlag=1" + " and blabtestitem.Visible=1";
                        }

                        ds = SearchListByHQL(hbw, page, rows);
                        string hbw2 = "blabtestitem.LabCode=" + labcode + "" + " and blabtestitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and blabtestitem.IsDoctorItem=1" + " and blabtestitem.UseFlag=1" + " and blabtestitem.Visible=1";
                        AllItemCount = SearchListByHQL(hbw2).Count;
                    }
                    else
                    {
                        string hbw = "";
                        if (!string.IsNullOrEmpty(itemkey))
                        {
                            hbw = "testitem.CName=" + itemkey + "" + " and testitem.EName=" + itemkey + "" + " and testitem.ShortCode=" + itemkey + "" + " and testitem.ShortName=" + itemkey + "" + " and testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and testitem.IsDoctorItem=1" + " and testitem.UseFlag=1" + " and testitem.Visible=1";
                        }
                        else
                        {
                            hbw = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and testitem.IsDoctorItem=1" + " and testitem.UseFlag=1" + " and testitem.Visible=1";
                        }
                        tyds = IDItemAllItemDao.GetListByHQL(hbw, page, rows);
                        string hbw2 = " testitem.TestItemSuperGroupClass='" + TestItemSuperGroupClass.COMBI + "'" + " and testitem.IsDoctorItem=1" + " and testitem.UseFlag=1" + " and testitem.Visible=1";
                        AllItemCount = IDItemAllItemDao.GetListByHQL(hbw2).Count;
                    }
                    break;

            }
            #endregion

            if (ds.count > 0 || tyds.count > 0)
            {
                entityList.list = ItemEntityDataTableToList(ds.list.ToList(), tyds.list.ToList());
                entityList.count = AllItemCount;
                resultObj.success = true;
                resultObj.ResultDataValue = JsonHelp.JsonDotNetSerializer(entityList);
            }
            return resultObj;
        }
    }
}