using System;
using System.Collections.Generic;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.LIIP;
using ZhiFang.IDAO.LIIP;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BBHospitalArea : BaseBLL<BHospitalArea>, IBBHospitalArea
    {
        IDAO.LIIP.IDBHospitalDao IDBHospitalDao { get; set; }
        IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        IDAO.LIIP.IDBHospitalTypeDao IDBHospitalTypeDao { get; set; }

        public List<long> SearchListFiltrationById(List<long> ids)
        {

            string strwhere = " PHospitalAreaID in (";

            for (int i = 0; i < ids.Count; i++)
            {
                if (i == 0)
                {
                    strwhere += ids[i];
                }
                else
                {
                    strwhere += "," + ids[i];
                }
            }
            strwhere += ")";

            IList<BHospitalArea> entityList = SearchListByHQL(strwhere);
            List<long> sonids = new List<long>();
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    sonids.Add(item.Id);
                }
                List<long> grandsonIds = SearchListFiltrationById(sonids);
                foreach (var item in grandsonIds)
                {
                    sonids.Add(item);
                }
            }
            else
            {
                return new List<long>();
            }
            return sonids;
        }

        public IList<BHospitalArea> SearchBHospitalAreaLevelNameTreeByID(long id)
        {
            string strwhere = "Id=" + id;
            IList<BHospitalArea> entityList = new List<BHospitalArea>();
            IList<BHospitalArea> oneself = SearchListByHQL(strwhere);
            foreach (var item in oneself)
            {
                entityList.Add(item);
                if (item.PHospitalAreaID == null || item.PHospitalAreaID == 0)
                {
                    return entityList;
                }
                else
                {
                    IList<BHospitalArea> balist = SearchBHospitalAreaLevelNameTreeByID(long.Parse(item.PHospitalAreaID.ToString()));
                    entityList.Add(balist[0]);
                }
            }
            return entityList;
        }

        public BaseResultTree<BHospitalArea> SearchBHospitalAreaListTree()
        {
            BaseResultTree<BHospitalArea> bHospitalAreaTree = new BaseResultTree<BHospitalArea>();
            EntityList<BHospitalArea> bhaEntityList = new EntityList<BHospitalArea>();
            try
            {
                string tempWhereStr = " IsUse = 1 and (PHospitalAreaID = 0 or PHospitalAreaID is null)";
                List<tree<BHospitalArea>> tempListTree = new List<tree<BHospitalArea>>();
                bhaEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);
                if ((bhaEntityList != null) && (bhaEntityList.list != null) && (bhaEntityList.list.Count > 0))
                {
                    Dictionary<string, BHospitalArea> keyValuePairs = new Dictionary<string, BHospitalArea>();
                    foreach (BHospitalArea bHospitalArea in bhaEntityList.list)
                    {
                        tree<BHospitalArea> tempTree = new tree<BHospitalArea>();
                        tempTree.expanded = true;
                        tempTree.text = bHospitalArea.Name;
                        tempTree.tid = bHospitalArea.Id.ToString();
                        tempTree.pid = bHospitalArea.Id.ToString();
                        tempTree.objectType = "BHospitalArea";
                        tempTree.value = bHospitalArea;
                        if (!keyValuePairs.ContainsKey(bHospitalArea.Id.ToString()))
                            keyValuePairs.Add(bHospitalArea.Id.ToString(), bHospitalArea);
                        tempTree.Tree = GetChildTreeList(bHospitalArea.Id.ToString(), keyValuePairs);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempListTree.Add(tempTree);
                    }
                }
                bHospitalAreaTree.Tree = tempListTree;
                bHospitalAreaTree.success = true;
            }
            catch (Exception ex)
            {
                bHospitalAreaTree.success = false;
                bHospitalAreaTree.ErrorInfo = ex.Message;
            }
            return bHospitalAreaTree;
        }

        public tree<BHospitalArea>[] GetChildTreeList(string bHospitalAreaID, Dictionary<string, BHospitalArea> keyValuePairs)
        {
            List<tree<BHospitalArea>> tempListTree = new List<tree<BHospitalArea>>();
            if (bHospitalAreaID != null && bHospitalAreaID != "")
            {
                try
                {
                    EntityList<BHospitalArea> bhaEntityList = new EntityList<BHospitalArea>();
                    bhaEntityList = this.SearchListByHQL(" IsUse = 1 and PHospitalAreaID = " + bHospitalAreaID, -1, -1);

                    foreach (BHospitalArea bHospitalAreaEntity in bhaEntityList.list)
                    {
                        tree<BHospitalArea> tempTree = new tree<BHospitalArea>();
                        BHospitalArea tempEntity = null;
                        if (keyValuePairs.ContainsKey(bHospitalAreaEntity.Id.ToString()))
                            tempEntity = keyValuePairs[bHospitalAreaEntity.Id.ToString()];
                        else
                            tempEntity = this.Get((long)bHospitalAreaEntity.Id);
                        if (tempEntity != null)
                        {
                            tempTree.expanded = true;
                            tempTree.text = bHospitalAreaEntity.Name;
                            tempTree.tid = bHospitalAreaEntity.Id.ToString();
                            tempTree.pid = bHospitalAreaEntity.PHospitalAreaID.ToString();
                            tempTree.objectType = "BHospitalArea";
                            tempTree.value = tempEntity;
                            tempTree.Tree = GetChildTreeList(bHospitalAreaEntity.Id.ToString(), keyValuePairs);
                            tempTree.leaf = (tempTree.Tree.Length <= 0);
                            tempListTree.Add(tempTree);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return tempListTree.ToArray();
        }

        public string GetBHospitalAreaCenterHospitalLabCodeByLabCode(string labCode)
        {
            var hosplist = IDBHospitalDao.GetListByHQL(" HospitalCode='" + labCode + "' ");
            if (hosplist == null || hosplist.Count <= 0)
            {
                throw new Exception($"GetBHospitalAreaCenterHospitalLabCodeByLabCode.未能找到医院，labcode:{labCode}");
            }
            if (!hosplist[0].AreaID.HasValue)
            {
                throw new Exception($"GetBHospitalAreaCenterHospitalLabCodeByLabCode.医院编码labcode:{labCode}，不归属与任何区域！");
            }

            var area = DBDao.Get(hosplist[0].AreaID.Value);
            if (area == null)
            {
                throw new Exception($"GetBHospitalAreaCenterHospitalLabCodeByLabCode.未能找到区域！区域ID:{hosplist[0].AreaID.Value}！");
            }
            if (!area.CenterHospitalID.HasValue)
            {
                throw new Exception($"GetBHospitalAreaCenterHospitalLabCodeByLabCode.区域ID:{hosplist[0].AreaID.Value},还未设置区域中心医院！");
            }
            var centerhospital = IDBHospitalDao.Get(area.CenterHospitalID.Value);
            if (centerhospital == null)
            {
                throw new Exception($"GetBHospitalAreaCenterHospitalLabCodeByLabCode.未能找到区域中心医院！区域中心医院ID:{area.CenterHospitalID.Value}！");
            }
            return centerhospital.HospitalCode;
        }
        public EntityList<BHospitalAreaVO> ST_UDTO_SearchTreeGridBHospitalAreaByHQL(string where, string v, int page, int limit)
        {
            EntityList<BHospitalAreaVO> entityListvo = new EntityList<BHospitalAreaVO>();
            EntityList<BHospitalArea> entityList = new EntityList<BHospitalArea>();
            if (v != null)
            {
                entityList = DBDao.GetListByHQL(where, v, page, limit);
            }
            else
            {
                entityList = DBDao.GetListByHQL(where, page, limit);
            }
            if (entityList.count > 0)
            {
                foreach (var item in entityList.list)
                {
                    BHospitalAreaVO bHospitalAreaVO = new BHospitalAreaVO();
                    bHospitalAreaVO = ZhiFang.LIIP.Common.ClassMapperHelp.GetMapper<BHospitalAreaVO, BHospitalArea>(item);
                    bHospitalAreaVO.ChildHosps = cildeTreeHosptial(item.Id);
                    bHospitalAreaVO.IsChild = bHospitalAreaVO.ChildHosps.Count > 0;
                    if (entityListvo.list == null)
                    {
                        entityListvo.list = new List<BHospitalAreaVO>();
                    }
                    entityListvo.list.Add(bHospitalAreaVO);
                }
            }
            entityListvo.count = entityList.count;
            return entityListvo;
        }

        private List<BHospitalAreaVO> cildeTreeHosptial(long Id)
        {
            var count = DBDao.GetListByHQL(" IsUse = 1 and PHospitalAreaID =" + Id);
            List<BHospitalAreaVO> bHospitalAreaVOs = new List<BHospitalAreaVO>();
            if (count.Count < 0)
            {
                return bHospitalAreaVOs;
            }
            foreach (var item in count)
            {
                BHospitalAreaVO bHospitalAreaVO = new BHospitalAreaVO();
                bHospitalAreaVO = ZhiFang.LIIP.Common.ClassMapperHelp.GetMapper<BHospitalAreaVO, BHospitalArea>(item);
                bHospitalAreaVO.ChildHosps = cildeTreeHosptial(item.Id);
                bHospitalAreaVO.IsChild = bHospitalAreaVO.ChildHosps.Count > 0;
                bHospitalAreaVOs.Add(bHospitalAreaVO);
            }

            return bHospitalAreaVOs;
        }

        public EntityList<BHospitalAreaVO> ST_UDTO_SearchTreeGridBHospitalAreaByHQL(string where, int page, int limit)
        {
            return ST_UDTO_SearchTreeGridBHospitalAreaByHQL(where, null, page, limit);
        }

        public bool ST_UDTO_UpdateBHospitalAreaByWhere(string[] pardata, string where)
        {
            string strEntityName = "BHospitalArea";
            string strEntityNameLower = "BHospitalArea".ToLower();
            StringBuilder sb = new StringBuilder();

            sb.Append("update " + strEntityName + " " + strEntityNameLower + " set ");
            foreach (string s in pardata)
            {
                if (s.Trim().IndexOf("Id=") >= 0 && s.Trim().IndexOf(".Id=") < 0)
                {
                }
                else
                {
                    if (s.Trim().IndexOf("DataTimeStamp=") >= 0 || s.Trim().IndexOf(".DataTimeStamp=") >= 0)
                    {
                    }
                    else
                    {
                        sb.Append(strEntityNameLower + "." + s + ", ");
                    }
                }
            }
            int n = sb.ToString().LastIndexOf(",");
            sb.Remove(n, 1);
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append("where " + where);
            }
            ZhiFang.Common.Log.Log.Debug(sb.ToString());
            int row = DBDao.UpdateByHql(sb.ToString());
            if (row == 1)
                return true;
            else
                return false;
        }

        public BaseResultBool ReceiveAndAdd(List<AreaVO> tmpo)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (tmpo != null)
                {
                    tmpo.ForEach(a =>
                    {
                        if (this.ReceiveAndAddEntity(a))
                        {
                            if (a.ChildAreaList != null)
                            {
                                ReceiveAndAdd(a.ChildAreaList);
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

        public bool ReceiveAndAddEntity(AreaVO tmpo)
        {
            var tmparea = DBDao.Get(tmpo.Id);
            if (tmparea != null)
            {
                tmparea.LabID = tmpo.LabID;
                if (tmpo.AreaTypeID.HasValue)
                {
                    var areatype = IDBHospitalTypeDao.Get(tmpo.AreaTypeID.Value);
                    if (areatype != null)
                    {
                        tmparea.AreaTypeID = tmpo.AreaTypeID;
                        tmparea.AreaTypeName = tmpo.AreaTypeName;
                    }
                    else
                    {
                        if (IDBHospitalTypeDao.Save(new BHospitalType()
                        {
                            LabID=tmpo.LabID,
                            Id=tmpo.AreaTypeID.Value,
                            Name= tmpo.AreaTypeName
                        }))
                        {
                            tmparea.AreaTypeID = tmpo.AreaTypeID;
                            tmparea.AreaTypeName = tmpo.AreaTypeName;
                        }
                        else
                        {
                            throw new Exception($"同步医院区域，新增类型失败！AreaTypeID:{tmpo.AreaTypeID.Value},AreaTypeName:{tmpo.AreaTypeName}.");
                        }
                    }
                }
                

              
                //tmparea.CenterHospitalID = tmpo.CenterHospitalID;
                //tmparea.CenterHospitalName = tmpo.CenterHospitalName;
                tmparea.Code = tmpo.Code;
                tmparea.Comment = tmpo.Comment;
                tmparea.DataUpdateTime = DateTime.Now;
                if (tmpo.DeptID.HasValue)
                {
                    var tmpdept = IDHRDeptDao.Get(tmpo.DeptID.Value);
                    if (tmpdept != null)
                    {
                        tmparea.DeptID = tmpo.DeptID.Value;
                        tmparea.DeptName = tmpo.DeptName;
                    }
                    else
                    {
                        ZhiFang.Entity.RBAC.HRDept dept = new Entity.RBAC.HRDept();
                        dept.Id = tmpo.DeptID.Value;
                        dept.CName = tmpo.DeptName;
                        dept.IsUse = true;
                        dept.DataAddTime = DateTime.Now;
                        if (IDHRDeptDao.Save(dept))
                        {
                            tmparea.LabID = tmpo.LabID;
                            tmparea.DeptID = tmpo.DeptID.Value;
                            tmparea.DeptName = tmpo.DeptName;
                        }
                        else
                        {
                            throw new Exception($"同步医院区域，新增部门（分公司）失败！DeptID:{tmpo.DeptID.Value},DeptName:{tmpo.DeptName}.");
                        }
                    }
                }
               
                tmparea.DispOrder = tmpo.DispOrder;
                tmparea.HospitalAreaLevelName = tmpo.HospitalAreaLevelName;
                tmparea.IsUse = tmpo.IsUse;
                tmparea.Name = tmpo.Name;
                tmparea.PHospitalAreaCode = tmpo.PHospitalAreaCode;
                tmparea.PHospitalAreaID = tmpo.PHospitalAreaID;
                tmparea.PHospitalAreaName = tmpo.PHospitalAreaName;
                tmparea.PinYinZiTou = tmpo.Shortcode;
                tmparea.SName = tmpo.SName;

                bool flag = DBDao.Update(tmparea);
                if (flag)
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospitalArea.ReceiveAndAddEntity,Update成功！.areaid:{tmparea.Id},areaName:{tmparea.Name}");
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospitalArea.ReceiveAndAddEntity,Update失败！.areaid:{tmparea.Id},areaName:{tmparea.Name}");
                return flag;
            }
            else
            {


                BHospitalArea area = new BHospitalArea();
                area.Id = tmpo.Id;
                area.LabID = tmpo.LabID;
                if (tmpo.AreaTypeID.HasValue)
                {
                    var areatype = IDBHospitalTypeDao.Get(tmpo.AreaTypeID.Value);
                    if (areatype != null)
                    {
                        area.AreaTypeID = tmpo.AreaTypeID;
                        area.AreaTypeName = tmpo.AreaTypeName;
                    }
                    else
                    {
                        if (IDBHospitalTypeDao.Save(new BHospitalType()
                        {
                            LabID = tmpo.LabID,
                            Id = tmpo.AreaTypeID.Value,
                            Name = tmpo.AreaTypeName
                        }))
                        {
                            area.AreaTypeID = tmpo.AreaTypeID;
                            area.AreaTypeName = tmpo.AreaTypeName;
                        }
                        else
                        {
                            throw new Exception($"同步医院区域，新增类型失败！AreaTypeID:{tmpo.AreaTypeID.Value},AreaTypeName:{tmpo.AreaTypeName}.");
                        }
                    }
                }
                //area.CenterHospitalID = tmpo.CenterHospitalID;
                //area.CenterHospitalName = tmpo.CenterHospitalName;
                area.Code = tmpo.Code;
                area.Comment = tmpo.Comment;
                area.DataAddTime = DateTime.Now;
                if (tmpo.DeptID.HasValue)
                {
                    var tmpdept = IDHRDeptDao.Get(tmpo.DeptID.Value);
                    if (tmpdept != null)
                    {
                        area.DeptID = tmpo.DeptID.Value;
                        area.DeptName = tmpo.DeptName;
                    }
                    else
                    {
                        ZhiFang.Entity.RBAC.HRDept dept = new Entity.RBAC.HRDept();
                        dept.Id = tmpo.DeptID.Value;
                        dept.CName = tmpo.DeptName;
                        dept.IsUse = true;
                        dept.DataAddTime = DateTime.Now;
                        if (IDHRDeptDao.Save(dept))
                        {
                            area.DeptID = tmpo.DeptID.Value;
                            area.DeptName = tmpo.DeptName;
                        }
                        else
                        {
                            throw new Exception($"同步医院区域，新增部门（分公司）失败！DeptID:{tmpo.DeptID.Value},DeptName:{tmpo.DeptName}.");
                        }
                    }
                }
                area.DispOrder = tmpo.DispOrder;
                area.HospitalAreaLevelName = tmpo.HospitalAreaLevelName;
                area.IsUse = tmpo.IsUse;
                area.Name = tmpo.Name;
                area.PHospitalAreaCode = tmpo.PHospitalAreaCode;
                area.PHospitalAreaID = tmpo.PHospitalAreaID;
                area.PHospitalAreaName = tmpo.PHospitalAreaName;
                area.PinYinZiTou = tmpo.Shortcode;
                area.SName = tmpo.SName;

                bool flag = DBDao.SaveOrUpdate(area);
                if (flag)
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospitalArea.ReceiveAndAddEntity,Add成功！.areaid:{area.Id},areaName:{area.Name}");
                else
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.BLL.LIIP.BBHospitalArea.ReceiveAndAddEntity,Add失败！.areaid:{area.Id},areaName:{area.Name}");
                return flag;
            }
        }
    }
}