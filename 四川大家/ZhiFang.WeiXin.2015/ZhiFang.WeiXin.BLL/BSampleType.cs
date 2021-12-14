
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.DAO.NHB;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BSampleType : BaseBLL<SampleType>, IBSampleType
    {
        IDBLabSampleTypeDao BLabSampleTypeDao { get; set; }
        IDBSampleTypeControlDao BSampleTypeControlDao { get; set; }

        public bool RemoveAndControl(long id)
        {
            bool flag = DBDao.Delete(id);
            if (flag)
            {
                int count = BSampleTypeControlDao.GetListCountByHQL("sampletypeno=" + id);
                if (count > 0)
                {
                        BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where sampletypeno=" + id);
                }
            }
            return flag;
        }

        public BaseResultDataValue SampleTypeCopy(List<string> SampleTypeNolist, List<string> LabCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<SampleType> sampleTypelist = DBDao.GetListByHQL(" SampleTypeNo in (" + string.Join(",", SampleTypeNolist.ToArray()) + ")");
            if(sampleTypelist ==null && sampleTypelist.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "没有找到数据";
                return brdv;
            }

            switch (OverRideType)
            {
                case 0:
                    BLabSampleTypeDao.DeleteByHql("from BLabSampleType where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where controllabno in("+string.Join(",", LabCodeList.ToArray()) +")");
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
                case 1:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            int count = BLabSampleTypeDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + sampleType.Id);
                            if(count > 0)
                            {
                                int flagcount = BSampleTypeControlDao.GetListCountByHQL("controllabno=" + labCode + " and sampletypeno=" + sampleType.Id);
                                if(flagcount > 0)
                                {
                                    BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where controllabno=" + labCode + " and sampletypeno=" + sampleType.Id);
                                }
                                BLabSampleTypeDao.DeleteByHql("from BLabSampleType where labcode=" + labCode+ " and LabSampleTypeNo=" + sampleType.Id);
                            }
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
                case 2:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            int count = BLabSampleTypeDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + sampleType.Id);
                            if (count > 0)
                            {
                                continue;
                            }
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
            }

            return brdv;
        }


        public BaseResultDataValue SampleTypeCopyAll(List<string> LabCodeList, int OverRideType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<SampleType> sampleTypelist = DBDao.GetListByHQL("");
            if (sampleTypelist == null && sampleTypelist.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "没有找到数据";
                return brdv;
            }

            switch (OverRideType)
            {
                case 0:
                    BLabSampleTypeDao.DeleteByHql("from BLabSampleType where labcode in (" + string.Join(",", LabCodeList.ToArray()) + ")");
                    BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where controllabno in(" + string.Join(",", LabCodeList.ToArray()) + ")");
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
                case 1:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            int count = BLabSampleTypeDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + sampleType.Id);
                            if (count > 0)
                            {
                                int flagcount = BSampleTypeControlDao.GetListCountByHQL("controllabno=" + labCode + " and sampletypeno=" + sampleType.Id);
                                if (flagcount > 0)
                                {
                                    BSampleTypeControlDao.DeleteByHql("from BSampleTypeControl where controllabno=" + labCode + " and sampletypeno=" + sampleType.Id);
                                }
                                BLabSampleTypeDao.DeleteByHql("from BLabSampleType where labcode=" + labCode + " and LabSampleTypeNo=" + sampleType.Id);
                            }
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
                case 2:
                    foreach (var labCode in LabCodeList)
                    {
                        foreach (var sampleType in sampleTypelist)
                        {
                            int count = BLabSampleTypeDao.GetListCountByHQL(" labcode=" + labCode + " and LabSampleTypeNo=" + sampleType.Id);
                            if (count > 0)
                            {
                                continue;
                            }
                            BLabSampleType bLabSampleType = new BLabSampleType();
                            bLabSampleType.LabCode = labCode;
                            bLabSampleType.CName = sampleType.CName;
                            bLabSampleType.LabSampleTypeNo = (Int32)sampleType.Id;
                            bLabSampleType.ShortCode = sampleType.ShortCode;
                            bLabSampleType.Visible = sampleType.Visible;
                            bLabSampleType.DispOrder = sampleType.DispOrder;
                            bLabSampleType.HisOrderCode = sampleType.HisOrderCode;
                            bLabSampleType.UseFlag = 1;
                            BLabSampleTypeDao.Save(bLabSampleType);
                        }
                    }
                    break;
            }

            return brdv;
        }
    }
}