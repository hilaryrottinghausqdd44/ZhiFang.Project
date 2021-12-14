
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
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
    public class BBLabDoctor : BaseBLL<BLabDoctor>, IBBLabDoctor
    {
        IDDoctorDao DoctorDao {get;set;}
        IDBDoctorControlDao BDoctorControlDao {get;set;}
        public EntityList<BLabDoctorVO> BLabDoctorAndControl(string labCode, int controlType, int page, int limit, string where)
        {
            EntityList<BLabDoctorVO> BLabDoctorVO = new EntityList<BLabDoctorVO>();
            List<BLabDoctorVO> BLabDoctorVOList = new List<BLabDoctorVO>();
            EntityList<BLabDoctor> bLabDoctors = new EntityList<BLabDoctor>();
            if (where != null && where.Length > 0)
            {
                bLabDoctors = DBDao.GetListByHQL(where + " and labcode = " + labCode, page, limit);
            }
            else
            {
                bLabDoctors = DBDao.GetListByHQL(" labcode = " + labCode, page, limit);
            }

            if (bLabDoctors == null || bLabDoctors.count <= 0)
            {
                return null;
            }
            BLabDoctorVO.count = bLabDoctors.count;
            IList<BDoctorControl> controlList = BDoctorControlDao.GetListByHQL(" controllabno =" + labCode);
            switch (controlType)
            {
                case 0: //全部

                    foreach (BLabDoctor bLabDoctor in bLabDoctors.list)
                    {
                        BLabDoctorVO doctorVO = new BLabDoctorVO();
                        doctorVO = DoctorTOVO(bLabDoctor);
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlDoctorNo == bLabDoctor.LabDoctorNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            doctorVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<Doctor> doctor = DoctorDao.GetListByHQL(" DoctorNo=" + flag.ElementAt(0).DoctorNo);
                            if (doctor != null && doctor.Count > 0)
                            {
                                doctorVO.doctorId = doctor[0].Id.ToString();
                                doctorVO.doctorCname = doctor[0].CName;
                            }
                        }
                        else
                        {
                            doctorVO.isContrast = null; //对照不存在
                        }

                        BLabDoctorVOList.Add(doctorVO);
                    }
                    BLabDoctorVO.list = BLabDoctorVOList;
                    break;
                case 1: //已对照
                    
                    foreach (BLabDoctor bLabDoctor in bLabDoctors.list)
                    {
                        
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlDoctorNo == bLabDoctor.LabDoctorNo);
                        if (flag != null && flag.Count() > 0)
                        {
                            BLabDoctorVO doctorVO = new BLabDoctorVO();
                            doctorVO = DoctorTOVO(bLabDoctor);
                            doctorVO.isContrast = flag.ElementAt(0).Id.ToString(); //对照存在
                            IList<Doctor> doctor = DoctorDao.GetListByHQL(" DoctorNo=" + flag.ElementAt(0).DoctorNo);
                            if (doctor != null && doctor.Count > 0)
                            {
                                doctorVO.doctorId = doctor[0].Id.ToString();
                                doctorVO.doctorCname = doctor[0].CName;
                            }
                            BLabDoctorVOList.Add(doctorVO);
                        }
                        
                    }
                    BLabDoctorVO.list = BLabDoctorVOList;
                    break;
                case 2: //未对照
                    foreach (BLabDoctor bLabDoctor in bLabDoctors.list)
                    {
                       
                        var flag = controlList.Where(a => a.ControlLabNo == labCode && a.ControlDoctorNo == bLabDoctor.LabDoctorNo);
                        if (!(flag != null && flag.Count() > 0))
                        {
                                BLabDoctorVO doctorVO = new BLabDoctorVO();
                                doctorVO = DoctorTOVO(bLabDoctor);
                                doctorVO.isContrast = null;
                                BLabDoctorVOList.Add(doctorVO);
                        }
                    }
                    BLabDoctorVO.list = BLabDoctorVOList;
                    break;
            }
            return BLabDoctorVO;
        }
        public bool RemoveAndControl(long id)
        {
            BLabDoctor labDoctor = DBDao.Get(id);
            bool flag = DBDao.Delete(id);
            int count = BDoctorControlDao.GetListCountByHQL(" controllabno=" + labDoctor.LabCode + " and ControlDoctorNo=" + labDoctor.LabDoctorNo);
            if(count > 0)
            {
                BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno=" + labDoctor.LabCode + " and ControlDoctorNo=" + labDoctor.LabDoctorNo);
            }
            return flag;
        }

        BLabDoctorVO DoctorTOVO(BLabDoctor bLabDoctor)
        {
            BLabDoctorVO vo = new BLabDoctorVO();
            vo.Id = bLabDoctor.Id;
            vo.CName = bLabDoctor.CName;
            vo.LabDoctorNo = bLabDoctor.LabDoctorNo;
            //vo.DispOrder = bLabDoctor.DispOrder;
            vo.ShortCode = bLabDoctor.ShortCode;
            vo.StandCode = bLabDoctor.StandCode;
            vo.ZFStandCode = bLabDoctor.ZFStandCode;
            vo.UseFlag = bLabDoctor.UseFlag;
            vo.Visible = bLabDoctor.Visible;
            return vo;
        }

        public void LabDoctorCopyAll(string originalLabCode, List<string> LabCodeList, int OverRideType)
        {
            IList<BLabDoctor> bLabDoctors = DBDao.GetListByHQL("labcode="+originalLabCode);
            IList<BDoctorControl> DoctorControl = BDoctorControlDao.GetListByHQL("controllabno ="+originalLabCode);
            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql("from BLabDoctor where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno in ("+string.Join(",",LabCodeList.ToArray())+")");
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                            labdoctor.LabCode = labCode;
                            DBDao.Save(labdoctor);
                        }

                        foreach (var bDoctorControl in DoctorControl)
                        {
                            BDoctorControl control = new BDoctorControl();
                            control.DoctorNo = bDoctorControl.DoctorNo;
                            control.ControlLabNo = labCode;
                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                            control.UseFlag = bDoctorControl.UseFlag;
                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                            BDoctorControlDao.Save(control);
                        }
                    }
                    break;
                case 1:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            IList<BDoctorControl> flagdoctorCOntrol = BDoctorControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlDoctorNo =" + bLabDoctor.LabDoctorNo);
                            if (flagdoctorCOntrol != null && flagdoctorCOntrol.Count > 0)
                            {
                                BDoctorControlDao.Delete(flagdoctorCOntrol[0].Id);
                            }
                            DBDao.DeleteByHql(" from  BLabDoctor where labcode = '" + labCode + "' and LabDoctorNo ='" + bLabDoctor.LabDoctorNo + "'");

                            BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                            labdoctor.LabCode = labCode;
                            DBDao.Save(labdoctor);
                        }

                        foreach (var bDoctorControl in DoctorControl)
                        {
                            BDoctorControl control = new BDoctorControl();
                            control.DoctorNo = bDoctorControl.DoctorNo;
                            control.ControlLabNo = labCode;
                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                            control.UseFlag = bDoctorControl.UseFlag;
                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                            BDoctorControlDao.Save(control);
                        }
                    }
                    break;
                case 2:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            int labDoctorFlag = DBDao.GetListCountByHQL("labcode = "+labCode+ " and LabDoctorNo="+bLabDoctor.LabDoctorNo);
                            if (labDoctorFlag <=0)
                            {
                                BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                                labdoctor.LabCode = labCode;
                                DBDao.Save(labdoctor);

                                IList<BDoctorControl> flagdoctorCOntrol = BDoctorControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlDoctorNo =" + bLabDoctor.LabDoctorNo);
                                if (flagdoctorCOntrol == null || flagdoctorCOntrol.Count <= 0)
                                {
                                    var flagBLabdoctor = DoctorControl.Where(a => a.ControlDoctorNo == bLabDoctor.LabDoctorNo);
                                    if (flagBLabdoctor != null || flagBLabdoctor.Count() > 0)
                                        foreach (var bDoctorControl in flagBLabdoctor)
                                        {
                                            BDoctorControl control = new BDoctorControl();
                                            control.DoctorNo = bDoctorControl.DoctorNo;
                                            control.ControlLabNo = labCode;
                                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                                            control.UseFlag = bDoctorControl.UseFlag;
                                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                                            BDoctorControlDao.Save(control);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void LabDoctorCopy(string originalLabCode, List<string> ItemNoList, List<string> LabCodeList, int OverRideType)
        {
            IList<BLabDoctor> bLabDoctors = DBDao.GetListByHQL("labcode=" + originalLabCode+" and id in ("+string.Join(",",ItemNoList.ToArray())+")");

            List<long> ControlNo = new List<long>();

            foreach (var item in bLabDoctors)
            {
                ControlNo.Add(item.LabDoctorNo);
            }

            IList<BDoctorControl> DoctorControl = BDoctorControlDao.GetListByHQL("controllabno ="+originalLabCode+ " and ControlDoctorNo in (" + string.Join(",",ControlNo.ToArray())+")");
            switch (OverRideType)
            {
                case 0:
                    DBDao.DeleteByHql("from BLabDoctor where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                            labdoctor.LabCode = labCode;
                            DBDao.Save(labdoctor);
                        }

                        foreach (var bDoctorControl in DoctorControl)
                        {
                            BDoctorControl control = new BDoctorControl();
                            control.DoctorNo = bDoctorControl.DoctorNo;
                            control.ControlLabNo = labCode;
                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                            control.UseFlag = bDoctorControl.UseFlag;
                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                            BDoctorControlDao.Save(control);
                        }
                    }
                    break;
                case 1:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            IList<BDoctorControl> flagdoctorCOntrol = BDoctorControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlDoctorNo =" + bLabDoctor.LabDoctorNo);
                            if (flagdoctorCOntrol != null && flagdoctorCOntrol.Count > 0)
                            {
                                BDoctorControlDao.Delete(flagdoctorCOntrol[0].Id);
                            }
                            DBDao.DeleteByHql(" from  BLabDoctor where labcode = '" + labCode + "' and LabDoctorNo ='" + bLabDoctor.LabDoctorNo + "'");

                            BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                            labdoctor.LabCode = labCode;
                            DBDao.Save(labdoctor);
                        }

                        foreach (var bDoctorControl in DoctorControl)
                        {
                            BDoctorControl control = new BDoctorControl();
                            control.DoctorNo = bDoctorControl.DoctorNo;
                            control.ControlLabNo = labCode;
                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                            control.UseFlag = bDoctorControl.UseFlag;
                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                            BDoctorControlDao.Save(control);
                        }
                    }
                    break;
                case 2:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var bLabDoctor in bLabDoctors)
                        {
                            int labDoctorFlag = DBDao.GetListCountByHQL("labcode = " + labCode + " and LabDoctorNo=" + bLabDoctor.LabDoctorNo);
                            if (labDoctorFlag <= 0)
                            {
                                BLabDoctor labdoctor = BLabDoctorTO(bLabDoctor);
                                labdoctor.LabCode = labCode;
                                DBDao.Save(labdoctor);

                                IList<BDoctorControl> flagdoctorCOntrol = BDoctorControlDao.GetListByHQL("ControlLabNo ='" + labCode + "' and ControlDoctorNo =" + bLabDoctor.LabDoctorNo);
                                if (flagdoctorCOntrol == null || flagdoctorCOntrol.Count <= 0)
                                {
                                    var flagBLabdoctor = DoctorControl.Where(a => a.ControlDoctorNo == bLabDoctor.LabDoctorNo);
                                    if (flagBLabdoctor != null || flagBLabdoctor.Count() > 0)
                                        foreach (var bDoctorControl in flagBLabdoctor)
                                        {
                                            BDoctorControl control = new BDoctorControl();
                                            control.DoctorNo = bDoctorControl.DoctorNo;
                                            control.ControlLabNo = labCode;
                                            control.ControlDoctorNo = bDoctorControl.ControlDoctorNo;
                                            control.UseFlag = bDoctorControl.UseFlag;
                                            control.DoctorControlNo = labCode + "_" + bDoctorControl.DoctorNo + "_" + bDoctorControl.ControlDoctorNo;
                                            BDoctorControlDao.Save(control);
                                        }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        BLabDoctor BLabDoctorTO(BLabDoctor bLabDoctor)
        {
            BLabDoctor labDoctor = new BLabDoctor();
            labDoctor.Id = bLabDoctor.Id;
            labDoctor.CName = bLabDoctor.CName;
            labDoctor.ShortCode = bLabDoctor.ShortCode;
            labDoctor.LabDoctorNo = bLabDoctor.LabDoctorNo;
            labDoctor.Visible = bLabDoctor.Visible;
            labDoctor.UseFlag = bLabDoctor.UseFlag;
            labDoctor.ZFStandCode = bLabDoctor.ZFStandCode;
            labDoctor.StandCode = bLabDoctor.StandCode;
            //labDoctor.DispOrder = bLabDoctor.DispOrder;
            return labDoctor;
        }
    }
}