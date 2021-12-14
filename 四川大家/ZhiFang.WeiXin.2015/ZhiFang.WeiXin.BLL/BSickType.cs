
using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BSickType : BaseBLL<SickType>, IBLL.IBSickType
    {
        IDBLabSickTypeDao BLabSickTypeDao { get; set; }
        IDBSickTypeControlDao IDBSickTypeControlDao { get; set; }

        public bool RemoveSickTypeAndControl(long id)
        {
            bool flag = false;
            flag = DBDao.Delete(id);
            int count = IDBSickTypeControlDao.GetListCountByHQL(" SickTypeNo=" + id);
            if(count > 0) {
                IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where SickTypeNo=" + id);
            }
            
            return flag;
        }

        public BaseResultDataValue SickTypeCopy(List<string> LabCodeList, List<string> ItemNoList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<SickType> sickType = DBDao.GetListByHQL(" SickTypeNo in ("+string.Join(",", ItemNoList.ToArray()) +")");
            switch (OverRideType)
            {
                case 0:
                    BLabSickTypeDao.DeleteByHql(" from BLabSickType where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where controllabno in (" + string.Join(",", LabCodeList.ToArray()) + ")");

                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            BLabSickType bLabSickType = new BLabSickType();
                            bLabSickType.CName = sType.CName;
                            bLabSickType.LabCode = labCode;
                            bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                            bLabSickType.ShortCode = sType.ShortCode;
                            bLabSickType.DispOrder = sType.DispOrder;
                            bLabSickType.HisOrderCode = sType.HisOrderCode;
                            bLabSickType.UseFlag = 1;
                            BLabSickTypeDao.Save(bLabSickType);

                            BSickTypeControl bSickTypeControl = new BSickTypeControl();
                            bSickTypeControl.ControlLabNo = labCode;
                            bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                            bSickTypeControl.UseFlag = 1;
                            bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                            bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSickTypeControl);
                        }
                    }

                    break;
                case 1:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            IList<BLabSickType> flagBLSickType = BLabSickTypeDao.GetListByHQL(" labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                            if (flagBLSickType != null || flagBLSickType.Count > 0)
                            {
                                BLabSickTypeDao.DeleteByHql("from BLabSickType where labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                                IDBSickTypeControlDao.DeleteByHql("from BSickTypeControl where controllabno=" + labCode + " and controlSickTypeNo =" + sType.Id);
                            }
                            BLabSickType bLabSickType = new BLabSickType();
                            bLabSickType.CName = sType.CName;
                            bLabSickType.LabCode = labCode;
                            bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                            bLabSickType.ShortCode = sType.ShortCode;
                            bLabSickType.DispOrder = sType.DispOrder;
                            bLabSickType.HisOrderCode = sType.HisOrderCode;
                            bLabSickType.UseFlag = 1;
                            BLabSickTypeDao.Save(bLabSickType);

                            BSickTypeControl bSickTypeControl = new BSickTypeControl();
                            bSickTypeControl.ControlLabNo = labCode;
                            bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                            bSickTypeControl.UseFlag = 1;
                            bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                            bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSickTypeControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            IList<BLabSickType> flagBLSickType = BLabSickTypeDao.GetListByHQL(" labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                            if (flagBLSickType == null || flagBLSickType.Count <= 0)
                            {
                                BLabSickType bLabSickType = new BLabSickType();
                                bLabSickType.CName = sType.CName;
                                bLabSickType.LabCode = labCode;
                                bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                                bLabSickType.ShortCode = sType.ShortCode;
                                bLabSickType.DispOrder = sType.DispOrder;
                                bLabSickType.HisOrderCode = sType.HisOrderCode;
                                bLabSickType.UseFlag = 1;
                                BLabSickTypeDao.Save(bLabSickType);
                                int flagSickTypeControl = IDBSickTypeControlDao.GetListCountByHQL(" controllabno =" + labCode + " and controlSickTypeNo =" + sType.Id);
                                if (flagSickTypeControl <= 0)
                                {
                                    BSickTypeControl bSickTypeControl = new BSickTypeControl();
                                    bSickTypeControl.ControlLabNo = labCode;
                                    bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                                    bSickTypeControl.UseFlag = 1;
                                    bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                                    bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                                    IDBSickTypeControlDao.Save(bSickTypeControl);
                                }
                            }

                        }
                    }
                    break;
            }
            return brdv;
        }

        public BaseResultDataValue SickTypeCopyAll(List<string> LabCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<SickType> sickType = DBDao.GetListByHQL("");
            switch (OverRideType)
            {
                case 0:
                    BLabSickTypeDao.DeleteByHql(" from BLabSickType where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    IDBSickTypeControlDao.DeleteByHql(" from BSickTypeControl where controllabno in (" + string.Join(",", LabCodeList.ToArray()) + ")");

                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            BLabSickType bLabSickType = new BLabSickType();
                            bLabSickType.CName = sType.CName;
                            bLabSickType.LabCode = labCode;
                            bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                            bLabSickType.ShortCode = sType.ShortCode;
                            bLabSickType.DispOrder = sType.DispOrder;
                            bLabSickType.HisOrderCode = sType.HisOrderCode;
                            bLabSickType.UseFlag = 1;
                            BLabSickTypeDao.Save(bLabSickType);

                            BSickTypeControl bSickTypeControl = new BSickTypeControl();
                            bSickTypeControl.ControlLabNo = labCode;
                            bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                            bSickTypeControl.UseFlag = 1;
                            bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                            bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSickTypeControl);
                        }
                    }

                    break;
                case 1:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            IList<BLabSickType> flagBLSickType = BLabSickTypeDao.GetListByHQL(" labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                            if (flagBLSickType != null || flagBLSickType.Count > 0)
                            {
                                BLabSickTypeDao.DeleteByHql("from BLabSickType where labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                                IDBSickTypeControlDao.DeleteByHql("from BSickTypeControl where controllabno=" + labCode + " and controlSickTypeNo =" + sType.Id);
                            }
                            BLabSickType bLabSickType = new BLabSickType();
                            bLabSickType.CName = sType.CName;
                            bLabSickType.LabCode = labCode;
                            bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                            bLabSickType.ShortCode = sType.ShortCode;
                            bLabSickType.DispOrder = sType.DispOrder;
                            bLabSickType.HisOrderCode = sType.HisOrderCode;
                            bLabSickType.UseFlag = 1;
                            BLabSickTypeDao.Save(bLabSickType);

                            BSickTypeControl bSickTypeControl = new BSickTypeControl();
                            bSickTypeControl.ControlLabNo = labCode;
                            bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                            bSickTypeControl.UseFlag = 1;
                            bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                            bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                            IDBSickTypeControlDao.Save(bSickTypeControl);
                        }
                    }
                    break;
                case 2:
                    foreach (string labCode in LabCodeList)
                    {
                        foreach (SickType sType in sickType)
                        {
                            IList<BLabSickType> flagBLSickType = BLabSickTypeDao.GetListByHQL(" labcode = " + labCode + " and LabSickTypeNo =" + sType.Id);
                            if (flagBLSickType == null || flagBLSickType.Count <= 0)
                            {
                                BLabSickType bLabSickType = new BLabSickType();
                                bLabSickType.CName = sType.CName;
                                bLabSickType.LabCode = labCode;
                                bLabSickType.LabSickTypeNo = (Int32)sType.Id;
                                bLabSickType.ShortCode = sType.ShortCode;
                                bLabSickType.DispOrder = sType.DispOrder;
                                bLabSickType.HisOrderCode = sType.HisOrderCode;
                                bLabSickType.UseFlag = 1;
                                BLabSickTypeDao.Save(bLabSickType);
                                int flagSickTypeControl = IDBSickTypeControlDao.GetListCountByHQL(" controllabno =" + labCode + " and controlSickTypeNo =" + sType.Id);
                                if (flagSickTypeControl <= 0)
                                {
                                    BSickTypeControl bSickTypeControl = new BSickTypeControl();
                                    bSickTypeControl.ControlLabNo = labCode;
                                    bSickTypeControl.ControlSickTypeNo = bLabSickType.LabSickTypeNo;
                                    bSickTypeControl.UseFlag = 1;
                                    bSickTypeControl.SickTypeNo = (Int32)sType.Id;
                                    bSickTypeControl.SickTypeControlNo = labCode + "_" + bSickTypeControl.SickTypeNo + "_" + bSickTypeControl.ControlSickTypeNo;
                                    IDBSickTypeControlDao.Save(bSickTypeControl);
                                }
                            }

                        }
                    }
                    break;
            }
            return brdv;
        }
    }
}