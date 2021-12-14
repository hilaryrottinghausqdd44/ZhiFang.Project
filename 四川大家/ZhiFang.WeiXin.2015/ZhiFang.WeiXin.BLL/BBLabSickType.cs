
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Log;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BBLabSickType : BaseBLL<BLabSickType>, IBLL.IBBLabSickType
    {
        IDBSickTypeControlDao IDBSickTypeControlDao { get; set; }
        IDSickTypeDao IDSickTypeDao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceLabCode">源实验室id</param>
        /// <param name="labCodeList">复制到实验室id列表</param>
        /// <param name="OverRideType">状态 0 ,1,2 全部删除重写，覆盖，智能对比</param>
        /// <returns></returns>
        public BaseResultDataValue BLabSickTypeCopyAll(string sourceLabCode, List<string> labCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (labCodeList == null || labCodeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室列表为空";
                Log.Debug("BBLabSickType.BLabSickTypeCopy.labCodeList 实验室列表为空");
                return brdv;
            }

            IList<BLabSickType> bLabSickTypeList = DBDao.GetListByHQL(" labcode = " + sourceLabCode);

            if (bLabSickTypeList == null || bLabSickTypeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "就诊类型列表为空";
                Log.Debug("BBLabSickType.BLabSickTypeCopy 就诊类型列表为空");
                return brdv;
            }

            IList<BSickTypeControl> controlList = IDBSickTypeControlDao.GetListByHQL(" controllabno =" + sourceLabCode);
            
            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql(" From BLabSickType  where labcode in ('" + String.Join(",", labCodeList.ToArray()) + "')");
                    IDBSickTypeControlDao.DeleteByHql(" From BSickTypeControl  where controllabno in ('" + String.Join(",", labCodeList.ToArray()) + "')");
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            BLabSickType bLSickType = new BLabSickType();
                            bLSickType.LabCode = labCode;
                            bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                            bLSickType.CName = bLabSickType.CName;
                            bLSickType.ShortCode = bLabSickType.ShortCode;
                            bLSickType.DispOrder = bLabSickType.DispOrder;
                            bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                            bLSickType.StandCode = bLabSickType.StandCode;
                            bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                            bLSickType.UseFlag = bLabSickType.UseFlag;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSickTypeControl bSicktTypeControl in controlList)
                        {
                            BSickTypeControl bSTControl = new BSickTypeControl();
                            bSTControl.SickTypeNo = bSicktTypeControl.SickTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSickTypeNo = bSicktTypeControl.ControlSickTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            IList<BSickTypeControl> flagBSickTYpeCOntrol = IDBSickTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSickTypeNo =" + bLabSickType.LabSickTypeNo);
                            if (flagBSickTYpeCOntrol != null || flagBSickTYpeCOntrol.Count > 0)
                            {
                                IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where controllabno ='" + labCode + "' and controlsicktypeno='" + bLabSickType.LabSickTypeNo + "'");
                            }
                            DBDao.DeleteByHql(" from  BLabSickType where labcode = '" + labCode + "' and LabSickTypeNo ='" + bLabSickType.LabSickTypeNo + "'");
                            BLabSickType bLSickType = new BLabSickType();
                            bLSickType.LabCode = labCode;
                            bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                            bLSickType.CName = bLabSickType.CName;
                            bLSickType.ShortCode = bLabSickType.ShortCode;
                            bLSickType.DispOrder = bLabSickType.DispOrder;
                            bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                            bLSickType.StandCode = bLabSickType.StandCode;
                            bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                            bLSickType.UseFlag = bLabSickType.UseFlag;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSickTypeControl bSicktTypeControl in controlList)
                        {
                            BSickTypeControl bSTControl = new BSickTypeControl();
                            bSTControl.SickTypeNo = bSicktTypeControl.SickTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSickTypeNo = bSicktTypeControl.ControlSickTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            int flag = DBDao.GetListCountByHQL(" labcode ='" + labCode + "'and LabSickTypeNo ='" + bLabSickType.LabSickTypeNo + "'");
                            if (flag <= 0)
                            {
                                BLabSickType bLSickType = new BLabSickType();
                                bLSickType.LabCode = labCode;
                                bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                                bLSickType.CName = bLabSickType.CName;
                                bLSickType.ShortCode = bLabSickType.ShortCode;
                                bLSickType.DispOrder = bLabSickType.DispOrder;
                                bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                                bLSickType.StandCode = bLabSickType.StandCode;
                                bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                                bLSickType.UseFlag = bLabSickType.UseFlag;
                                DBDao.Save(bLSickType);

                                IList<BSickTypeControl> flagBSickTYpeCOntrol = IDBSickTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSickTypeNo =" + bLabSickType.LabSickTypeNo);
                                if (flagBSickTYpeCOntrol == null || flagBSickTYpeCOntrol.Count <= 0)
                                {
                                    var flagBLabSickType = controlList.Where(a => a.ControlSickTypeNo == bLabSickType.LabSickTypeNo);
                                    if (flagBLabSickType != null || flagBLabSickType.Count() > 0)
                                        foreach (var item in flagBLabSickType)
                                        {
                                            BSickTypeControl bSTControl = new BSickTypeControl();
                                            bSTControl.SickTypeNo = item.SickTypeNo;
                                            bSTControl.ControlLabNo = labCode;
                                            bSTControl.ControlSickTypeNo = item.ControlSickTypeNo;
                                            bSTControl.UseFlag = item.UseFlag;
                                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                                            IDBSickTypeControlDao.Save(bSTControl);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
            return brdv;
        }

        public BaseResultDataValue BLabSickTypeCopy(string sourceLabCode, List<string> labCodeList, List<string> ItemNoList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (labCodeList == null || labCodeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室列表为空";
                Log.Debug("BBLabSickType.BLabSickTypeCopy.labCodeList 实验室列表为空");
                return brdv;
            }

            if (ItemNoList == null || ItemNoList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "实验室列表为空";
                Log.Debug("BBLabSickType.BLabSickTypeCopy.ItemNoList 就诊项列表为空");
                return brdv;
            }

            IList<BLabSickType> bLabSickTypeList = DBDao.GetListByHQL(" labcode = " + sourceLabCode+" and id in ("+ string.Join(",",ItemNoList.ToArray()) + ")");

            if (bLabSickTypeList == null || bLabSickTypeList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "就诊类型列表为空";
                Log.Debug("BBLabSickType.BLabSickTypeCopy 就诊类型列表为空");
                return brdv;
            }
            List<int> ControlNo=new List<int>();
            foreach (var item in bLabSickTypeList)
            {
                ControlNo.Add(item.LabSickTypeNo);
            }

            IList<BSickTypeControl> controlList = IDBSickTypeControlDao.GetListByHQL(" controllabno =" + sourceLabCode+ " and controlsicktypeno in ("+string.Join(",",ControlNo.ToArray())+")");
            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql(" From BLabSickType  where labcode in ('" + String.Join(",", labCodeList.ToArray()) + "')");
                    IDBSickTypeControlDao.DeleteByHql(" From BSickTypeControl  where controllabno in ('" + String.Join(",", labCodeList.ToArray()) + "')");
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            BLabSickType bLSickType = new BLabSickType();
                            bLSickType.LabCode = labCode;
                            bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                            bLSickType.CName = bLabSickType.CName;
                            bLSickType.ShortCode = bLabSickType.ShortCode;
                            bLSickType.DispOrder = bLabSickType.DispOrder;
                            bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                            bLSickType.StandCode = bLabSickType.StandCode;
                            bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                            bLSickType.UseFlag = bLabSickType.UseFlag;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSickTypeControl bSicktTypeControl in controlList)
                        {
                            BSickTypeControl bSTControl = new BSickTypeControl();
                            bSTControl.SickTypeNo = bSicktTypeControl.SickTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSickTypeNo = bSicktTypeControl.ControlSickTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            IList<BSickTypeControl> flagBSickTYpeCOntrol = IDBSickTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSickTypeNo =" + bLabSickType.LabSickTypeNo);
                            if (flagBSickTYpeCOntrol != null || flagBSickTYpeCOntrol.Count > 0)
                            {
                                IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where controllabno ='" + labCode + "' and controlsicktypeno='" + bLabSickType.LabSickTypeNo + "'");
                            }
                            DBDao.DeleteByHql(" from  BLabSickType where labcode = '" + labCode + "' and LabSickTypeNo ='" + bLabSickType.LabSickTypeNo + "'");
                            BLabSickType bLSickType = new BLabSickType();
                            bLSickType.LabCode = labCode;
                            bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                            bLSickType.CName = bLabSickType.CName;
                            bLSickType.ShortCode = bLabSickType.ShortCode;
                            bLSickType.DispOrder = bLabSickType.DispOrder;
                            bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                            bLSickType.StandCode = bLabSickType.StandCode;
                            bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                            bLSickType.UseFlag = bLabSickType.UseFlag;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSickTypeControl bSicktTypeControl in controlList)
                        {
                            BSickTypeControl bSTControl = new BSickTypeControl();
                            bSTControl.SickTypeNo = bSicktTypeControl.SickTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSickTypeNo = bSicktTypeControl.ControlSickTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (BLabSickType bLabSickType in bLabSickTypeList)
                        {
                            int flag = DBDao.GetListCountByHQL(" labcode ='" + labCode + "'and LabSickTypeNo ='" + bLabSickType.LabSickTypeNo + "'");
                            if (flag <= 0)
                            {
                                BLabSickType bLSickType = new BLabSickType();
                                bLSickType.LabCode = labCode;
                                bLSickType.LabSickTypeNo = bLabSickType.LabSickTypeNo;
                                bLSickType.CName = bLabSickType.CName;
                                bLSickType.ShortCode = bLabSickType.ShortCode;
                                bLSickType.DispOrder = bLabSickType.DispOrder;
                                bLSickType.HisOrderCode = bLabSickType.HisOrderCode;
                                bLSickType.StandCode = bLabSickType.StandCode;
                                bLSickType.ZFStandCode = bLabSickType.ZFStandCode;
                                bLSickType.UseFlag = bLabSickType.UseFlag;
                                DBDao.Save(bLSickType);

                                IList<BSickTypeControl> flagBSickTYpeCOntrol = IDBSickTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSickTypeNo =" + bLabSickType.LabSickTypeNo);
                                if (flagBSickTYpeCOntrol == null || flagBSickTYpeCOntrol.Count <= 0)
                                {
                                    var flagBLabSickType = controlList.Where(a => a.ControlSickTypeNo == bLabSickType.LabSickTypeNo);
                                    if (flagBLabSickType != null || flagBLabSickType.Count() > 0)
                                        foreach (var item in flagBLabSickType)
                                        {
                                            BSickTypeControl bSTControl = new BSickTypeControl();
                                            bSTControl.SickTypeNo = item.SickTypeNo;
                                            bSTControl.ControlLabNo = labCode;
                                            bSTControl.ControlSickTypeNo = item.ControlSickTypeNo;
                                            bSTControl.UseFlag = item.UseFlag;
                                            bSTControl.SickTypeControlNo = labCode + "_" + bSTControl.SickTypeNo + "_" + bSTControl.ControlSickTypeNo;
                                            IDBSickTypeControlDao.Save(bSTControl);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
            return brdv;
        }

        public EntityList<BLabSickTypeVO> BLabSickTypeAndControl(string labCode,int type, int page, int limit, string where)
        {
            EntityList<BLabSickTypeVO> BLabSickTypeVO = new EntityList<BLabSickTypeVO>();
            List<BLabSickTypeVO> bLabSickTypeVO = new List<BLabSickTypeVO>();
            EntityList<BLabSickType> bLabSickTypes =new EntityList<BLabSickType>();
            if (where != null && where.Length > 0)
            {
                bLabSickTypes= DBDao.GetListByHQL(where+" and labcode = " + labCode, page, limit);
            }else
            {
                bLabSickTypes = DBDao.GetListByHQL(" labcode = " + labCode, page, limit);
            }

            if(bLabSickTypes == null || bLabSickTypes.count <= 0)
            {
                return null;
            }
            BLabSickTypeVO.count = bLabSickTypes.count;
            IList<BSickTypeControl> controlList = IDBSickTypeControlDao.GetListByHQL(" controllabno =" + labCode);
            switch (type)
            {
                case 0: //全部
                    
                    foreach (BLabSickType bLabSickType in bLabSickTypes.list)
                    {
                        BLabSickTypeVO sickTypeVO = new BLabSickTypeVO();
                        sickTypeVO = labSickTypeTOVO(bLabSickType);
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSickTypeNo == bLabSickType.LabSickTypeNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            sickTypeVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<SickType> sickType = IDSickTypeDao.GetListByHQL(" sicktypeno=" + flag.ElementAt(0).SickTypeNo);
                            if(sickType !=null && sickType.Count >0)
                            {
                                sickTypeVO.sickTypeId = sickType[0].Id.ToString();
                                sickTypeVO.sickTypeCname = sickType[0].CName;
                            }
                        }
                        else
                        {
                            sickTypeVO.isContrast = null; //对照不存在
                        }
                        
                        bLabSickTypeVO.Add(sickTypeVO);
                    }
                    BLabSickTypeVO.list = bLabSickTypeVO;
                    break;
                case 1: //已对照
                   
                    foreach (BLabSickType bLabSickType in bLabSickTypes.list)
                    {

                        BLabSickTypeVO sickTypeVO = new BLabSickTypeVO();
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSickTypeNo == bLabSickType.LabSickTypeNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            sickTypeVO = labSickTypeTOVO(bLabSickType);
                            sickTypeVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<SickType> sickType = IDSickTypeDao.GetListByHQL(" sicktypeno=" + flag.ElementAt(0).SickTypeNo);
                            if (sickType != null && sickType.Count > 0)
                            {
                                sickTypeVO.sickTypeId = sickType[0].Id.ToString();
                                sickTypeVO.sickTypeCname = sickType[0].CName;
                            }
                            bLabSickTypeVO.Add(sickTypeVO);
                        }
                    }
                    BLabSickTypeVO.list= bLabSickTypeVO;
                    break;
                case 2: //未对照
                    foreach (BLabSickType bLabSickType in bLabSickTypes.list)
                    {
                        BLabSickTypeVO sickTypeVO = new BLabSickTypeVO();

                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSickTypeNo == bLabSickType.LabSickTypeNo);
                        if (!(flag != null && flag.Count() > 0))
                        {
                            sickTypeVO = labSickTypeTOVO(bLabSickType);
                            sickTypeVO.isContrast = null;
                            bLabSickTypeVO.Add(sickTypeVO);
                        }
                    }
                    BLabSickTypeVO.list = bLabSickTypeVO;
                    break;
            }
            return BLabSickTypeVO;
        }

        BLabSickTypeVO labSickTypeTOVO(BLabSickType bLabSickType)
        {
            BLabSickTypeVO sickTypeVO = new BLabSickTypeVO();
            sickTypeVO.Id = bLabSickType.Id;
            sickTypeVO.LabCode = bLabSickType.LabCode;
            sickTypeVO.LabSickTypeNo = bLabSickType.LabSickTypeNo;
            sickTypeVO.ShortCode = bLabSickType.ShortCode;
            sickTypeVO.StandCode = bLabSickType.StandCode;
            sickTypeVO.ZFStandCode = bLabSickType.ZFStandCode;
            sickTypeVO.UseFlag = bLabSickType.UseFlag;
            sickTypeVO.CName = bLabSickType.CName;
            sickTypeVO.DispOrder = bLabSickType.DispOrder;
            return sickTypeVO;
        }

        public bool RemoveLabSickTypeAndControl(long id)
        {
            bool flag = false;
            IList<BLabSickType> labSickType = DBDao.GetListByHQL(" sicktypeid = "+id);
            flag = DBDao.Delete(id);
            int count = IDBSickTypeControlDao.GetListCountByHQL(" ControlSickTypeNo =" + labSickType[0].LabSickTypeNo + " and ControlLabNo=" + labSickType[0].LabCode);
            if(count > 0)
            {
                IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where ControlSickTypeNo =" + labSickType[0].LabSickTypeNo + " and ControlLabNo=" + labSickType[0].LabCode);
            }
            return flag;
        }
    }
}