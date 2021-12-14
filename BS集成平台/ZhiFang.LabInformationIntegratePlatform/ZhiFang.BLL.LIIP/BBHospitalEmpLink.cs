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

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BBHospitalEmpLink : BaseBLL<BHospitalEmpLink>, IBBHospitalEmpLink
    {
        IDAO.LIIP.IDBHospitalDao IDBHospitalDao { get; set; }
        ZhiFang.IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { get; set; }
        public EntityList<BHospitalEmpLink> SearchSelectHospitalListByEmpId(string empId, string Sort, int page, int limit)
        {
            EntityList<BHospitalEmpLink> entitylist = new EntityList<BHospitalEmpLink>();
            entitylist.list = new List<BHospitalEmpLink>();
            entitylist = DBDao.GetListByHQL(" 1=1 and empId=" + empId, Sort, page, limit);
            if (entitylist != null && entitylist.list != null && entitylist.list.Count > 0)
            {
                List<long> hidlist = new List<long>();
                entitylist.list.ToList().ForEach(a =>
                {
                    if (a.HospitalID.HasValue)
                    {
                        hidlist.Add(a.HospitalID.Value);
                    }
                }
                );
                var hlist=IDBHospitalDao.GetListByHQL(" id in ("+string.Join(",",hidlist.ToArray())+") ");
                entitylist.list.ToList().ForEach(a =>
                {
                    if (hlist.Count(b=>b.Id==a.HospitalID.Value)>0)
                    {
                        a.HospitalName = hlist.Where(b => b.Id == a.HospitalID.Value).ElementAt(0).Name;
                    }
                }
                );
            }
            return entitylist;
        }

        public EntityList<BHospital> SearchUnSelectHospitalListByEmpId(string empId, string Sort, int page, int limit)
        {
            EntityList<BHospitalEmpLink> entityhospitalemplist = new EntityList<BHospitalEmpLink>();
            EntityList<BHospital> entitylist = new EntityList<BHospital>();
            entityhospitalemplist.list = new List<BHospitalEmpLink>();
            var bhospitalemplinkIlist = DBDao.GetListByHQL(" 1=1 and empId=" + empId);
            string hql = "";
            List<long> HospitalIDlist = new List<long>();
            if (bhospitalemplinkIlist != null && bhospitalemplinkIlist.Count > 0)
            {
                var bhospitalemplinklist = bhospitalemplinkIlist.ToList();

                bhospitalemplinklist.ForEach(a =>
                {
                    if (a.HospitalID.HasValue)
                        HospitalIDlist.Add(a.HospitalID.Value);
                });
                entitylist = IDBHospitalDao.GetListByHQL(" 1=1 and IsUse=1 and Id not in (" + string.Join(",", HospitalIDlist) + ") ", Sort, page, limit);

            }
            else
            {
                entitylist = IDBHospitalDao.GetListByHQL(" 1=1 and IsUse=1 ", Sort, page, limit);
            }


            return entitylist;
        }

        public override bool Add()
        {
            //检测是否重复
            var count = DBDao.GetListCountByHQL(" EmpID= " + Entity.EmpID + " and HospitalID=" + Entity.HospitalID);
            if (count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("BBHospitalEmpLink.Add:EmpID= " + Entity.EmpID + " and HospitalID=" + Entity.HospitalID + ".重复！");
                return false;
            }
            else
            {
                return base.Add();
            }
        }

        public BaseResultDataValue SaveByList(List<BHospitalEmpLink> entitylist)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            foreach (var entity in entitylist)
            {
                //检测是否重复
                var count = DBDao.GetListCountByHQL(" EmpID= " + Entity.EmpID + " and HospitalID=" + Entity.HospitalID);
                if (count > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("BBHospitalEmpLink.Add:EmpID= " + Entity.EmpID + " and HospitalID=" + Entity.HospitalID + ".重复！");
                    brdv.ErrorInfo += "EmpID= " + Entity.EmpID + " and HospitalID=" + Entity.HospitalID + ".重复！";
                }
                else
                {
                    base.Add();
                }
            }
            return brdv;
        }

        public bool BatchAddBHospitalEmpLink(List<BHospitalEmpLink> entitylist)
        {
            if (entitylist != null && entitylist.Count > 0)
            {
                foreach (var entity in entitylist)
                {
                    var count = DBDao.GetListCountByHQL(" Empid=" + entity.EmpID + " and HospitalID=" + entity.HospitalID + " ");
                    if (count > 0)
                    {
                        continue;
                    }
                    var hospital = IDBHospitalDao.Get(entity.HospitalID.Value);
                    if (hospital != null)
                    {
                        entity.HospitalCode = hospital.HospitalCode;
                        entity.HospitalName = hospital.Name;
                    }

                    var emp = IDHREmployeeDao.Get(entity.EmpID.Value);
                    {
                        entity.EmpName = emp.CName;
                    }

                    if (ZFHospitalEmpLinkType.GetStatusDic().Keys.Contains(entity.LinkTypeID.ToString()) && ZFHospitalEmpLinkType.GetStatusDic()[entity.LinkTypeID.ToString()] != null)
                    {
                        var linktype = ZFHospitalEmpLinkType.GetStatusDic()[entity.LinkTypeID.ToString()];
                        entity.LinkTypeID = long.Parse(linktype.Id);
                        entity.LinkTypeName = linktype.Name;
                    }
                    else
                    {
                        entity.LinkTypeID = long.Parse(ZFHospitalEmpLinkType.管理.Key);
                        entity.LinkTypeName = ZFHospitalEmpLinkType.管理.Value.Name;
                    }
                    entity.DataAddTime = DateTime.Now;
                    entity.IsUse = true;
                    DBDao.Save(entity);
                }
                return true;
            }
            return false;
        }

        public bool BHospitalEmpLinkSetLinkType(long id, long typeid)
        {
            try
            {
                var BHospitalEmpLink = DBDao.Get(id);
                if (BHospitalEmpLink == null)
                {
                    ZhiFang.Common.Log.Log.Error($"BHospitalEmpLinkSetLinkType.ID={id}未找到相关数据！");
                    return false;
                }
                if (!ZFHospitalEmpLinkType.GetStatusDic().ContainsKey(typeid.ToString()))
                {
                    ZhiFang.Common.Log.Log.Error($"BHospitalEmpLinkSetLinkType.typeid={typeid}类型ID错误！");
                    return false;
                }
                string typename = ZFHospitalEmpLinkType.GetStatusDic()[typeid.ToString()].Name;
                switch (typeid)
                {
                    case 1:
                        DBDao.UpdateByHql(" update BHospitalEmpLink set LinkTypeID=2 ,LinkTypeName='" + ZFHospitalEmpLinkType.GetStatusDic()["2"].Name + "' where  EmpID=" + BHospitalEmpLink.EmpID);
                        break;
                    case 2:
                        break;
                }
                DBDao.UpdateByHql(" update BHospitalEmpLink set LinkTypeID=" + typeid.ToString() + " ,LinkTypeName='" + typename + "' where id=" + id);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long GetLabIdByEmpId(long id)
        {
            var tmplist = DBDao.GetListByHQL(" EmpId=" + id);
            if (tmplist != null && tmplist.Count > 0 && tmplist.ElementAt(0).HospitalID.HasValue)
            {
                return tmplist.ElementAt(0).HospitalID.Value;
            }
            return -1;
        }
        public BHospitalEmpLink GetBHospitalEmpLinkByEmpId(long id)
        {
            var tmplist = DBDao.GetListByHQL(" EmpId=" + id);
            if (tmplist != null && tmplist.Count > 0 )
            {
                if (tmplist.Count(a => a.LinkTypeID.ToString() == ZFHospitalEmpLinkType.所属.Key) > 0)
                {
                    return  tmplist.Where(a => a.LinkTypeID.ToString() == ZFHospitalEmpLinkType.所属.Key).First();
                }
                else
                {
                    tmplist.First();
                }
            }
            return null;
        }
    }
}