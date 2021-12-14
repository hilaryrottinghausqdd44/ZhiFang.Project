using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.LIIP;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using System.Collections.Generic;
using ZhiFang.Common.Public;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.IDAO.RBAC;
using ZhiFang.LIIP.Common;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BBHospital : BaseBLL<BHospital>, IBBHospital
    {
        IDBHospitalAreaDao IDBHospitalAreaDao { get; set; }
        IDBHospitalLevelDao IDBHospitalLevelDao { get; set; }
        IDBHospitalTypeDao IDBHospitalTypeDao { get; set; }
        IDBProvinceDao IDBProvinceDao { get; set; }
        IDBCityDao IDBCityDao { get; set; }
        IDBCountyDao IDBCountyDao { get; set; }
        public BaseResultBool ReceiveAndAdd(List<AreaVO> areavolist)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (areavolist != null)
                {
                    areavolist.ForEach(a =>
                    {
                        if (a.ChildHospitalList != null && a.ChildHospitalList.Count > 0)
                        {
                            a.ChildHospitalList.ForEach(b =>
                            {
                                this.ReceiveAndAddEntity(b, a.Id, a.Name, a.DeptID);
                            });
                        }
                        if (a.CenterHospitalID.HasValue)
                        {
                            var centerhospital = DBDao.Get(a.CenterHospitalID.Value);
                            if (centerhospital != null)
                            {
                                IDBHospitalAreaDao.UpdateByHql($" update BHospitalArea set CenterHospitalID={centerhospital.Id},CenterHospitalName='{centerhospital.Name}' where  Id=" + a.Id);
                            }
                        }
                    });
                }
                brb.success = true;
                return brb;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error($"ZhiFang.BLL.LIIP.BBHospitalArea.ReceiveAndAdd,异常！:{e.ToString()}");
                throw e;
            }
        }
        public bool ReceiveAndAddEntity(HospitalVO tmpo, long areaid, string areaname, long? DeptId)
        {
            var tmphospitallist = DBDao.GetListByHQL($" HospitalCode='{tmpo.HospitalCode}' ");

            if (tmphospitallist!=null && tmphospitallist.Count>0)
            {
                var tmphospital = tmphospitallist.First();
                tmphospital.Address = tmpo.Address;
                tmphospital.AreaCode = tmpo.AreaCode;
                tmphospital.AreaID = areaid;
                tmphospital.AreaName = areaname;
                tmphospital.AutoFax = tmpo.AutoFax;
                tmphospital.BillNumber = tmpo.BillNumber;
                tmphospital.BranchId = DeptId;
                tmphospital.BusinessAnalysisDate = tmpo.BusinessAnalysisDate;
                tmphospital.BusinessAnalysisDateType = tmpo.BusinessAnalysisDateType;

                if (tmpo.ProvinceID.HasValue)
                {
                    var Province = IDBProvinceDao.Get(tmpo.ProvinceID.Value);
                    if (Province != null)
                    {
                        tmphospital.ProvinceID = tmpo.ProvinceID;
                        tmphospital.ProvinceName = tmpo.ProvinceName;
                    }
                    else
                    {
                        if (IDBProvinceDao.Save(new BProvince()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.ProvinceID.Value,
                            Name = tmpo.ProvinceName
                        }))
                        {
                            tmphospital.ProvinceID = tmpo.ProvinceID;
                            tmphospital.ProvinceName = tmpo.ProvinceName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增省份失败！ProvinceID:{tmpo.ProvinceID.Value},ProvinceName:{tmpo.ProvinceName}.");
                        }
                    }
                }
                if (tmpo.CityID.HasValue)
                {
                    var City = IDBCityDao.Get(tmpo.CityID.Value);
                    if (City != null)
                    {
                        tmphospital.CityID = tmpo.CityID;
                        tmphospital.CityName = tmpo.CityName;
                    }
                    else
                    {
                        BCity city = new BCity();
                        city.LabID = tmpo.LabID;
                        city.Id = tmpo.CityID.Value;
                        city.Name = tmpo.CityName;
                        if (tmpo.ProvinceID.HasValue)
                        {
                            var Province = IDBProvinceDao.Get(tmpo.ProvinceID.Value);
                            if (Province != null)
                            {
                                city.BProvince = Province;
                            }
                        }
                        if (IDBCityDao.Save(city))
                        {
                            tmphospital.CityID = tmpo.CityID;
                            tmphospital.CityName = tmpo.CityName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增城市失败！CityID:{tmpo.CityID.Value},CityName:{tmpo.CityName}.");
                        }
                    }
                }
                if (tmpo.CountyID.HasValue)
                {
                    var County = IDBCountyDao.Get(tmpo.CountyID.Value);
                    if (County != null)
                    {
                        tmphospital.CountyID = tmpo.CountyID;
                        tmphospital.CountyName = tmpo.CountyName;
                    }
                    else
                    {
                        BCounty county = new BCounty();
                        county.LabID = tmpo.LabID;
                        county.Id = tmpo.CountyID.Value;
                        county.Name = tmpo.CountyName;

                        if (tmpo.CityID.HasValue)
                        {
                            var City = IDBCityDao.Get(tmpo.CityID.Value);
                            if (City != null)
                            {
                                county.BCity = City;
                            }
                        }

                        if (IDBCountyDao.Save(county))
                        {
                            tmphospital.CountyID = tmpo.CountyID;
                            tmphospital.CountyName = tmpo.CountyName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增区县失败！CountyID:{tmpo.CountyID.Value},CountyName:{tmpo.CountyName}.");
                        }
                    }
                }
                if (tmpo.HTypeID.HasValue)
                {
                    var HospitalType = IDBHospitalTypeDao.Get(tmpo.HTypeID.Value);
                    if (HospitalType != null)
                    {
                        tmphospital.HTypeID = tmpo.HTypeID;
                        tmphospital.HTypeName = tmpo.HTypeName;
                    }
                    else
                    {
                        if (IDBHospitalTypeDao.Save(new BHospitalType()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.HTypeID.Value,
                            Name = tmpo.HTypeName
                        }))
                        {
                            tmphospital.HTypeID = tmpo.HTypeID;
                            tmphospital.HTypeName = tmpo.HTypeName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增医院类别失败！HTypeID:{tmpo.HTypeID.Value},HTypeName:{tmpo.HTypeName}.");
                        }
                    }
                }
                if (tmpo.LevelID.HasValue)
                {
                    var HospitalLevel = IDBHospitalLevelDao.Get(tmpo.LevelID.Value);
                    if (HospitalLevel != null)
                    {
                        tmphospital.LevelID = tmpo.LevelID;
                        tmphospital.LevelName = tmpo.LevelName;
                    }
                    else
                    {
                        if (IDBHospitalLevelDao.Save(new BHospitalLevel()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.LevelID.Value,
                            Name = tmpo.LevelName
                        }))
                        {
                            tmphospital.LevelID = tmpo.LevelID;
                            tmphospital.LevelName = tmpo.LevelName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增医院级别失败！LevelID:{tmpo.LevelID.Value},LevelName:{tmpo.LevelName}.");
                        }
                    }
                }

                tmphospital.Comment = tmpo.Comment;
                tmphospital.DataUpdateTime = DateTime.Now;
                tmphospital.DealerID = tmpo.DealerID;
                tmphospital.EMAIL = tmpo.EMAIL;
                tmphospital.EName = tmpo.EName;
                tmphospital.FaxNo = tmpo.FaxNo;
                tmphospital.HospitalCode = tmpo.HospitalCode;


                tmphospital.IconsID = tmpo.IconsID;
                tmphospital.IsBloodSamplingPoint = tmpo.IsBloodSamplingPoint;
                tmphospital.IsCooperation = tmpo.IsCooperation;
                tmphospital.IsReceiveSamplePoint = tmpo.IsReceiveSamplePoint;
                tmphospital.IsUse = tmpo.IsUse;
                tmphospital.LabID = tmpo.LabID;
                tmphospital.LabIdentityTypeID = tmpo.LabIdentityTypeID;


                tmphospital.LinkMan = tmpo.LinkMan;
                tmphospital.LinkManPosition = tmpo.LinkManPosition;
                tmphospital.MAILNO = tmpo.MAILNO;
                tmphospital.Name = tmpo.Name;
                tmphospital.PersonFixedCharges = tmpo.PersonFixedCharges;
                tmphospital.PhoneNum1 = tmpo.PhoneNum1;
                tmphospital.PhoneNum2 = tmpo.PhoneNum2;
                tmphospital.PinYinZiTou = tmpo.PinYinZiTou;
                tmphospital.Postion = tmpo.Postion;

                tmphospital.Shortcode = tmpo.Shortcode;
                tmphospital.SName = tmpo.SName;
                tmphospital.TINumber = tmpo.TINumber;

                bool flag = DBDao.Update(tmphospital);
                if (flag)
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospital.ReceiveAndAddEntity,Update成功！.hospitalid:{tmphospital.Id},hospitalName:{tmphospital.Name}");
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospital.ReceiveAndAddEntity,Update失败！.hospitalid:{tmphospital.Id},hospitalName:{tmphospital.Name}");
                return flag;
            }
            else
            {
                BHospital Bhospital = new BHospital();
                Bhospital.Id = tmpo.Id;
                Bhospital.Address = tmpo.Address;
                Bhospital.AreaCode = tmpo.AreaCode;
                Bhospital.AreaID = areaid;
                Bhospital.AreaName = areaname;
                Bhospital.AutoFax = tmpo.AutoFax;
                Bhospital.BillNumber = tmpo.BillNumber;
                Bhospital.BranchId = DeptId;
                Bhospital.BusinessAnalysisDate = tmpo.BusinessAnalysisDate;
                Bhospital.BusinessAnalysisDateType = tmpo.BusinessAnalysisDateType;
                if (tmpo.ProvinceID.HasValue)
                {
                    var Province = IDBProvinceDao.Get(tmpo.ProvinceID.Value);
                    if (Province != null)
                    {
                        Bhospital.ProvinceID = tmpo.ProvinceID;
                        Bhospital.ProvinceName = tmpo.ProvinceName;
                    }
                    else
                    {
                        if (IDBProvinceDao.Save(new BProvince()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.ProvinceID.Value,
                            Name = tmpo.ProvinceName
                        }))
                        {
                            Bhospital.ProvinceID = tmpo.ProvinceID;
                            Bhospital.ProvinceName = tmpo.ProvinceName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增省份失败！ProvinceID:{tmpo.ProvinceID.Value},ProvinceName:{tmpo.ProvinceName}.");
                        }
                    }
                }
                if (tmpo.CityID.HasValue)
                {
                    var City = IDBCityDao.Get(tmpo.CityID.Value);
                    if (City != null)
                    {
                        Bhospital.CityID = tmpo.CityID;
                        Bhospital.CityName = tmpo.CityName;
                    }
                    else
                    {
                        BCity city = new BCity();
                        city.LabID = tmpo.LabID;
                        city.Id = tmpo.CityID.Value;
                        city.Name = tmpo.CityName;
                        if (tmpo.ProvinceID.HasValue)
                        {
                            var Province = IDBProvinceDao.Get(tmpo.ProvinceID.Value);
                            if (Province != null)
                            {
                                city.BProvince = Province;
                            }
                        }
                        if (IDBCityDao.Save(city))
                        {
                            Bhospital.CityID = tmpo.CityID;
                            Bhospital.CityName = tmpo.CityName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增城市失败！CityID:{tmpo.CityID.Value},CityName:{tmpo.CityName}.");
                        }
                    }
                }
                if (tmpo.CountyID.HasValue)
                {
                    var County = IDBCountyDao.Get(tmpo.CountyID.Value);
                    if (County != null)
                    {
                        Bhospital.CountyID = tmpo.CountyID;
                        Bhospital.CountyName = tmpo.CountyName;
                    }
                    else
                    {
                        BCounty county = new BCounty();
                        county.LabID = tmpo.LabID;
                        county.Id = tmpo.CountyID.Value;
                        county.Name = tmpo.CountyName;

                        if (tmpo.CityID.HasValue)
                        {
                            var City = IDBCityDao.Get(tmpo.CityID.Value);
                            if (City != null)
                            {
                                county.BCity= City;
                            }
                        }

                        if (IDBCountyDao.Save(county))
                        {
                            Bhospital.CountyID = tmpo.CountyID;
                            Bhospital.CountyName = tmpo.CountyName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增区县失败！CountyID:{tmpo.CountyID.Value},CountyName:{tmpo.CountyName}.");
                        }
                    }
                }
                if (tmpo.HTypeID.HasValue)
                {
                    var HospitalType = IDBHospitalTypeDao.Get(tmpo.HTypeID.Value);
                    if (HospitalType != null)
                    {
                        Bhospital.HTypeID = tmpo.HTypeID;
                        Bhospital.HTypeName = tmpo.HTypeName;
                    }
                    else
                    {
                        if (IDBHospitalTypeDao.Save(new BHospitalType()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.HTypeID.Value,
                            Name = tmpo.HTypeName
                        }))
                        {
                            Bhospital.HTypeID = tmpo.HTypeID;
                            Bhospital.HTypeName = tmpo.HTypeName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增医院类别失败！HTypeID:{tmpo.HTypeID.Value},HTypeName:{tmpo.HTypeName}.");
                        }
                    }
                }
                if (tmpo.LevelID.HasValue)
                {
                    var HospitalLevel = IDBHospitalLevelDao.Get(tmpo.LevelID.Value);
                    if (HospitalLevel != null)
                    {
                        Bhospital.LevelID = tmpo.LevelID;
                        Bhospital.LevelName = tmpo.LevelName;
                    }
                    else
                    {
                        if (IDBHospitalLevelDao.Save(new BHospitalLevel()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.LevelID.Value,
                            Name = tmpo.LevelName
                        }))
                        {
                            Bhospital.LevelID = tmpo.LevelID;
                            Bhospital.LevelName = tmpo.LevelName;
                        }
                        else
                        {
                            throw new Exception($"同步医院，新增医院级别失败！LevelID:{tmpo.LevelID.Value},LevelName:{tmpo.LevelName}.");
                        }
                    }
                }
                Bhospital.Comment = tmpo.Comment;
                Bhospital.DataUpdateTime = DateTime.Now;
                Bhospital.DealerID = tmpo.DealerID;
                Bhospital.EMAIL = tmpo.EMAIL;
                Bhospital.EName = tmpo.EName;
                Bhospital.FaxNo = tmpo.FaxNo;
                Bhospital.HospitalCode = tmpo.HospitalCode;
                Bhospital.IconsID = tmpo.IconsID;
                Bhospital.IsBloodSamplingPoint = tmpo.IsBloodSamplingPoint;
                Bhospital.IsCooperation = tmpo.IsCooperation;
                Bhospital.IsReceiveSamplePoint = tmpo.IsReceiveSamplePoint;
                Bhospital.IsUse = tmpo.IsUse;
                Bhospital.LabID = tmpo.LabID;
                Bhospital.LabIdentityTypeID = tmpo.LabIdentityTypeID;
                Bhospital.LinkMan = tmpo.LinkMan;
                Bhospital.LinkManPosition = tmpo.LinkManPosition;
                Bhospital.MAILNO = tmpo.MAILNO;
                Bhospital.Name = tmpo.Name;
                Bhospital.PersonFixedCharges = tmpo.PersonFixedCharges;
                Bhospital.PhoneNum1 = tmpo.PhoneNum1;
                Bhospital.PhoneNum2 = tmpo.PhoneNum2;
                Bhospital.PinYinZiTou = tmpo.PinYinZiTou;
                Bhospital.Postion = tmpo.Postion;

                Bhospital.Shortcode = tmpo.Shortcode;
                Bhospital.SName = tmpo.SName;
                Bhospital.TINumber = tmpo.TINumber;

                bool flag = DBDao.SaveOrUpdate(Bhospital);
                if (flag)
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospital.ReceiveAndAddEntity,Add成功！.hospitalid:{Bhospital.Id},hospitalName:{Bhospital.Name}");
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospital.ReceiveAndAddEntity,Add失败！.hospitalid:{Bhospital.Id},hospitalName:{Bhospital.Name}");
                return flag;
            }
        }

        public bool SaveEntity(BHospital entity)
        {
            if (!string.IsNullOrEmpty(entity.Name))
            {
                entity.PinYinZiTou = ZhiFang.LIIP.Common.PinYinConverter.GetFirst(entity.Name);
                entity.Shortcode = ZhiFang.LIIP.Common.PinYinConverter.GetFirst(entity.Name);
            }
            return DBDao.Save(entity);
        }
    }
}