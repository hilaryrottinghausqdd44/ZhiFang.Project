
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Log;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BBLabSampleType : BaseBLL<BLabSampleType>, IBBLabSampleType
    {
        IDBSampleTypeControlDao BSampleTypeControlDao { get; set; }
        IDSampleTypeDao SampleTypeDao { get; set; }
        public EntityList<BLabSampleTypeVO> BLabSampleTypeAndControl(string labCode, int controlType, int page, int limit, string where)
        {
            EntityList<BLabSampleTypeVO> BLabSampleTypeVO = new EntityList<BLabSampleTypeVO>();
            List<BLabSampleTypeVO> BLabSampleTypeVOList = new List<BLabSampleTypeVO>();
            EntityList<BLabSampleType> bLabSampleTypes = new EntityList<BLabSampleType>();
            if (where != null && where.Length > 0)
            {
                bLabSampleTypes = DBDao.GetListByHQL(where + " and labcode = " + labCode, page, limit);
            }
            else
            {
                bLabSampleTypes = DBDao.GetListByHQL(" labcode = " + labCode, page, limit);
            }

            if (bLabSampleTypes == null || bLabSampleTypes.count <= 0)
            {
                return null;
            }
            BLabSampleTypeVO.count = bLabSampleTypes.count;
            IList<BSampleTypeControl> controlList = BSampleTypeControlDao.GetListByHQL(" controllabno =" + labCode);
            switch (controlType)
            {
                case 0: //全部

                    foreach (BLabSampleType bLabSampleType in bLabSampleTypes.list)
                    {
                        BLabSampleTypeVO sempleTypeVO = new BLabSampleTypeVO();
                        sempleTypeVO = labSampleTypeTOVO(bLabSampleType);
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSampleTypeNo == bLabSampleType.LabSampleTypeNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            sempleTypeVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<SampleType> sickType = SampleTypeDao.GetListByHQL(" sampletypeno=" + flag.ElementAt(0).SampleTypeNo);
                            if (sickType != null && sickType.Count > 0)
                            {
                                sempleTypeVO.sampleTypeId = sickType[0].Id.ToString();
                                sempleTypeVO.sampleTypeCname = sickType[0].CName;
                            }
                        }
                        else
                        {
                            sempleTypeVO.isContrast = null; //对照不存在
                        }

                        BLabSampleTypeVOList.Add(sempleTypeVO);
                    }
                    BLabSampleTypeVO.list = BLabSampleTypeVOList;
                    break;
                case 1: //已对照

                    foreach (BLabSampleType bLabSampleType in bLabSampleTypes.list)
                    {

                        BLabSampleTypeVO sempleTypeVO = new BLabSampleTypeVO();
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSampleTypeNo == bLabSampleType.LabSampleTypeNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            sempleTypeVO = labSampleTypeTOVO(bLabSampleType);
                            sempleTypeVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<SampleType> sickType = SampleTypeDao.GetListByHQL(" sampletypeno=" + flag.ElementAt(0).SampleTypeNo);
                            if (sickType != null && sickType.Count > 0)
                            {
                                sempleTypeVO.sampleTypeId = sickType[0].Id.ToString();
                                sempleTypeVO.sampleTypeCname = sickType[0].CName;
                            }
                            BLabSampleTypeVOList.Add(sempleTypeVO);
                        }
                    }
                    BLabSampleTypeVO.list = BLabSampleTypeVOList;
                    break;
                case 2: //未对照
                    foreach (BLabSampleType bLabSampleType in bLabSampleTypes.list)
                    {
                        BLabSampleTypeVO sempleTypeVO = new BLabSampleTypeVO();

                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlSampleTypeNo == bLabSampleType.LabSampleTypeNo);
                        if (!(flag != null && flag.Count() > 0))
                        {
                            sempleTypeVO = labSampleTypeTOVO(bLabSampleType);
                            sempleTypeVO.isContrast = null;
                            BLabSampleTypeVOList.Add(sempleTypeVO);
                        }
                    }
                    BLabSampleTypeVO.list = BLabSampleTypeVOList;
                    break;
            }
            return BLabSampleTypeVO;
        }

        public BaseResultDataValue LabSampleTypeCopy(string originalLabCode, List<string> ItemNoList, List<string> LabCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<BLabSampleType> bLabSampleTypeList = DBDao.GetListByHQL(" labcode = " + originalLabCode+" and id in ("+string.Join(",",ItemNoList.ToArray())+")");

            List<int> ControlNo = new List<int>();
            foreach (var item in bLabSampleTypeList)
            {
                ControlNo.Add(item.LabSampleTypeNo);
            }

            IList<BSampleTypeControl> controlList = BSampleTypeControlDao.GetListByHQL(" controllabno =" + originalLabCode+ " and ControlSampleTypeNo in ("+string.Join(",",ControlNo.ToArray())+")");

            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql(" From BLabSampleType  where labcode in ('" + String.Join(",", LabCodeList.ToArray()) + "')");
                    BSampleTypeControlDao.DeleteByHql(" From BSampleTypeControl  where controllabno in ('" + String.Join(",", LabCodeList.ToArray()) + "')");
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            BLabSampleType bLSickType = labSampleTypeTO(bLabSickType);
                            bLSickType.LabCode = labCode;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSampleTypeControl bSicktTypeControl in controlList)
                        {
                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                            bSTControl.SampleTypeNo = bSicktTypeControl.SampleTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSampleTypeNo = bSicktTypeControl.ControlSampleTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                            BSampleTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            IList<BSampleTypeControl> flagBSickTYpeCOntrol = BSampleTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSampleTypeNo =" + bLabSickType.LabSampleTypeNo);
                            if (flagBSickTYpeCOntrol != null || flagBSickTYpeCOntrol.Count > 0)
                            {
                                BSampleTypeControlDao.DeleteByHql(" from BSampleTypeControl where controllabno ='" + labCode + "' and ControlSampleTypeNo='" + bLabSickType.LabSampleTypeNo + "'");
                            }
                            DBDao.DeleteByHql(" from  BLabSampleType where labcode = '" + labCode + "' and LabSampleTypeNo ='" + bLabSickType.LabSampleTypeNo + "'");
                            BLabSampleType bLSickType = labSampleTypeTO(bLabSickType);
                            bLSickType.LabCode = labCode;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSampleTypeControl bSicktTypeControl in controlList)
                        {
                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                            bSTControl.SampleTypeNo = bSicktTypeControl.SampleTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSampleTypeNo = bSicktTypeControl.ControlSampleTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                            BSampleTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            int flag = DBDao.GetListCountByHQL(" labcode ='" + labCode + "'and LabSampleTypeNo =" + bLabSickType.LabSampleTypeNo);
                            
                            if (flag <= 0)
                            {
                                BLabSampleType bLSickType = labSampleTypeTO(bLabSickType);
                                bLSickType.LabCode = labCode;
                                DBDao.Save(bLSickType);

                                IList<BSampleTypeControl> flagBSickTYpeCOntrol = BSampleTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSampleTypeNo =" + bLabSickType.LabSampleTypeNo);
                                if (flagBSickTYpeCOntrol == null || flagBSickTYpeCOntrol.Count <= 0)
                                {
                                    var flagBLabSickType = controlList.Where(a => a.ControlSampleTypeNo == bLabSickType.LabSampleTypeNo);
                                    if (flagBLabSickType != null || flagBLabSickType.Count() > 0)
                                        foreach (var item in flagBLabSickType)
                                        {
                                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                                            bSTControl.SampleTypeNo = item.SampleTypeNo;
                                            bSTControl.ControlLabNo = labCode;
                                            bSTControl.ControlSampleTypeNo = item.ControlSampleTypeNo;
                                            bSTControl.UseFlag = item.UseFlag;
                                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                                            BSampleTypeControlDao.Save(bSTControl);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
            return brdv; ;
        }

        public BaseResultDataValue LabSampleTypeCopyAll(string originalLabCode, List<string> LabCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<BLabSampleType> bLabSampleTypeList = DBDao.GetListByHQL(" labcode = " + originalLabCode);
            IList<BSampleTypeControl> controlList = BSampleTypeControlDao.GetListByHQL(" controllabno =" + originalLabCode);

            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql(" From BLabSampleType  where labcode in ('" + String.Join(",", LabCodeList.ToArray()) + "')");
                    BSampleTypeControlDao.DeleteByHql(" From BSampleTypeControl  where controllabno in ('" + String.Join(",", LabCodeList.ToArray()) + "')");
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            BLabSampleType bLSickType  = labSampleTypeTO(bLabSickType);
                            bLSickType.LabCode = labCode;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSampleTypeControl bSicktTypeControl in controlList)
                        {
                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                            bSTControl.SampleTypeNo = bSicktTypeControl.SampleTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSampleTypeNo = bSicktTypeControl.ControlSampleTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                            BSampleTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            IList<BSampleTypeControl> flagBSickTYpeCOntrol = BSampleTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSampleTypeNo =" + bLabSickType.LabSampleTypeNo);
                            if (flagBSickTYpeCOntrol != null || flagBSickTYpeCOntrol.Count > 0)
                            {
                                BSampleTypeControlDao.DeleteByHql(" from BSampleTypeControl where controllabno ='" + labCode + "' and ControlSampleTypeNo='" + bLabSickType.LabSampleTypeNo + "'");
                            }
                            DBDao.DeleteByHql(" from  BLabSampleType where labcode = '" + labCode + "' and LabSampleTypeNo ='" + bLabSickType.LabSampleTypeNo + "'");
                            BLabSampleType bLSickType = labSampleTypeTO(bLabSickType);
                            bLSickType.LabCode = labCode;
                            DBDao.Save(bLSickType);
                        }

                        foreach (BSampleTypeControl bSicktTypeControl in controlList)
                        {
                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                            bSTControl.SampleTypeNo = bSicktTypeControl.SampleTypeNo;
                            bSTControl.ControlLabNo = labCode;
                            bSTControl.ControlSampleTypeNo = bSicktTypeControl.ControlSampleTypeNo;
                            bSTControl.UseFlag = bSicktTypeControl.UseFlag;
                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                            BSampleTypeControlDao.Save(bSTControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (BLabSampleType bLabSickType in bLabSampleTypeList)
                        {
                            int flag = DBDao.GetListCountByHQL(" labcode ='" + labCode + "'and LabSampleTypeNo ='" + bLabSickType.LabSampleTypeNo + "'");
                            if (flag <= 0)
                            {
                                BLabSampleType bLSickType = labSampleTypeTO(bLabSickType);
                                bLSickType.LabCode = labCode;
                                DBDao.Save(bLSickType);

                                IList<BSampleTypeControl> flagBSickTYpeCOntrol = BSampleTypeControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlSampleTypeNo =" + bLabSickType.LabSampleTypeNo);
                                if (flagBSickTYpeCOntrol == null || flagBSickTYpeCOntrol.Count <= 0)
                                {
                                    var flagBLabSickType = controlList.Where(a => a.ControlSampleTypeNo == bLabSickType.LabSampleTypeNo);
                                    if (flagBLabSickType != null || flagBLabSickType.Count() > 0)
                                        foreach (var item in flagBLabSickType)
                                        {
                                            BSampleTypeControl bSTControl = new BSampleTypeControl();
                                            bSTControl.SampleTypeNo = item.SampleTypeNo;
                                            bSTControl.ControlLabNo = labCode;
                                            bSTControl.ControlSampleTypeNo = item.ControlSampleTypeNo;
                                            bSTControl.UseFlag = item.UseFlag;
                                            bSTControl.SampleTypeControlNo = labCode + "_" + bSTControl.SampleTypeNo + "_" + bSTControl.ControlSampleTypeNo;
                                            BSampleTypeControlDao.Save(bSTControl);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
            return brdv;
        }

        public bool RemoveAndControl(long BLabSampleTypeID)
        {
            BLabSampleType bLabSampleType = DBDao.Get(BLabSampleTypeID);
            bool flag = DBDao.Delete(BLabSampleTypeID);
            if (flag)
            {
                int count = BSampleTypeControlDao.GetListCountByHQL("controllabno=" + bLabSampleType.LabCode + " and controlsampletypeno=" + bLabSampleType.LabSampleTypeNo);
                if (count >0)
                {
                        BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where controllabno=" + bLabSampleType.LabCode + " and controlsampletypeno=" + bLabSampleType.LabSampleTypeNo);
                }
            }
            return flag;
        }

        BLabSampleTypeVO labSampleTypeTOVO(BLabSampleType bLabSampleType)
        {
            BLabSampleTypeVO vo = new BLabSampleTypeVO();
            vo.Id = bLabSampleType.Id;
            vo.LabCode = bLabSampleType.LabCode;
            vo.CName = bLabSampleType.CName;
            vo.DispOrder = bLabSampleType.DispOrder;
            vo.Code1 = bLabSampleType.Code1;
            vo.Code2 = bLabSampleType.Code2;
            vo.Code3 = bLabSampleType.Code3;
            vo.HisOrderCode = bLabSampleType.HisOrderCode;
            vo.LabSampleTypeNo = bLabSampleType.LabSampleTypeNo;
            vo.ShortCode = bLabSampleType.ShortCode;
            vo.ZFStandCode = bLabSampleType.ZFStandCode;
            vo.Visible = bLabSampleType.Visible;
            vo.StandCode = bLabSampleType.StandCode;
            vo.UseFlag = bLabSampleType.UseFlag;
            return vo;
        }

        BLabSampleType labSampleTypeTO(BLabSampleType bLabSampleType)
        {
            BLabSampleType labSampleType = new BLabSampleType();
            labSampleType.CName = bLabSampleType.CName;
            labSampleType.LabSampleTypeNo = bLabSampleType.LabSampleTypeNo;
            labSampleType.DispOrder = bLabSampleType.DispOrder;
            labSampleType.HisOrderCode = bLabSampleType.HisOrderCode;
            labSampleType.ShortCode = bLabSampleType.ShortCode;
            labSampleType.StandCode = bLabSampleType.StandCode;
            labSampleType.ZFStandCode = bLabSampleType.ZFStandCode;
            labSampleType.UseFlag = bLabSampleType.UseFlag;
            labSampleType.Visible = bLabSampleType.Visible;
            labSampleType.Code1 = bLabSampleType.Code1;
            labSampleType.Code2 = bLabSampleType.Code2;
            labSampleType.Code3 = bLabSampleType.Code3;
            return labSampleType;
        }
    }
}