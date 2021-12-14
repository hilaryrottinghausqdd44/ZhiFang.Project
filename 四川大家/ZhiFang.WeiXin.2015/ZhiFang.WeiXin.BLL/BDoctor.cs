using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BDoctor : BaseBLL<Doctor>, IBDoctor
    {
        IDBDoctorControlDao BDoctorControlDao { get; set; }
        IDBLabDoctorDao BLabDoctorDao {get;set;}
        public void DoctorCopy(List<string> itemNoList, List<string> labCodeList, int type)
        {
            IList<Doctor> doctorList = DBDao.GetListByHQL("DoctorNo in ("+string.Join(",",itemNoList.ToArray())+")");
            switch (type)
            {
                case 0:
                    BLabDoctorDao.DeleteByHql("from BLabDoctor where LabCode in (" + string.Join(",", labCodeList.ToArray()) + ")");
                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno in(" + string.Join(",", labCodeList.ToArray()) + ")");
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            int count = BLabDoctorDao.GetListCountByHQL(" labcode=" + labCode + " and LabDoctorNo=" + doctor.Id);
                            if (count > 0)
                            {
                                int flagcount = BDoctorControlDao.GetListCountByHQL("controllabno=" + labCode + " and DoctorNo=" + doctor.Id);
                                if (flagcount > 0)
                                {
                                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno=" + labCode + " and DoctorNo=" + doctor.Id);
                                }
                                BLabDoctorDao.DeleteByHql("from BLabDoctor where labcode=" + labCode + " and LabDoctorNo=" + doctor.Id);
                            }
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            int count = BLabDoctorDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + doctor.Id);
                            if (count > 0)
                            {
                                continue;
                            }
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;
            }
        }

        public void DoctorCopyAll(List<string> labCodeList, int type)
        {
            IList<Doctor> doctorList=DBDao.GetListByHQL("");
            switch (type)
            {
                case 0:
                    BLabDoctorDao.DeleteByHql("from BLabDoctor where LabCode in (" + string.Join(",",labCodeList.ToArray())+")");
                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno in(" + string.Join(",", labCodeList.ToArray()) + ")");
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;
                case 1:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            int count = BLabDoctorDao.GetListCountByHQL(" labcode=" + labCode + " and LabDoctorNo=" + doctor.Id);
                            if (count > 0)
                            {
                                int flagcount = BDoctorControlDao.GetListCountByHQL("controllabno=" + labCode + " and DoctorNo=" + doctor.Id);
                                if (flagcount > 0)
                                {
                                    BDoctorControlDao.DeleteByHql("from BDoctorControl where controllabno=" + labCode + " and DoctorNo=" + doctor.Id);
                                }
                                BLabDoctorDao.DeleteByHql("from BLabDoctor where labcode=" + labCode + " and LabDoctorNo=" + doctor.Id);
                            }
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in labCodeList)
                    {
                        foreach (Doctor doctor in doctorList)
                        {
                            int count = BLabDoctorDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + doctor.Id);
                            if (count > 0)
                            {
                                continue;
                            }
                            BLabDoctor bLabDoctor = new BLabDoctor();
                            bLabDoctor = DoctorTOBDoctor(doctor);
                            bLabDoctor.LabCode = labCode;
                            BLabDoctorDao.Save(bLabDoctor);
                        }
                    }
                    break;             
            }
        }

        public bool RemoveAndControl(long id)
        {
            Doctor doctor = DBDao.Get(id);
            bool flag = DBDao.Delete(id);
            int isCount = BDoctorControlDao.GetListCountByHQL("DoctorNo=" + id);
            if (isCount > 0)
            {
                BDoctorControlDao.DeleteByHql("from BDoctorControl where DoctorNo="+id);
            }
            return flag;
        }

        BLabDoctor DoctorTOBDoctor(Doctor doctor)
        {
            BLabDoctor bLabDoctor = new BLabDoctor();
            bLabDoctor.CName = doctor.CName;
            bLabDoctor.LabDoctorNo = doctor.Id;
            bLabDoctor.ShortCode = doctor.ShortCode;
            bLabDoctor.Visible = doctor.Visible;
            bLabDoctor.UseFlag = 1;
            return bLabDoctor;
        }
    }
}