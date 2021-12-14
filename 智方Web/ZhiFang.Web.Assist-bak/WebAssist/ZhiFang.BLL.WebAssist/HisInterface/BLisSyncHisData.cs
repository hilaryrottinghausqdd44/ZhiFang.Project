using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.WebAssist.GKBarcode;
using ZhiFang.Entity.WebAssist.HisInterface;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.WebAssist.Common;

namespace ZhiFang.BLL.WebAssist
{
    public class BLisSyncHisData : IBLisSyncHisData
    {
        IBLL.RBAC.IBDepartment IBDepartment { get; set; }
        IBLL.RBAC.IBPUser IBPUser { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }

        public BaseResultBool SaveLisSyncHisDataInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            string code = ZhiFang.WebAssist.Common.JSONConfigHelp.GetString(SysConfig.HISSYS.Key, "LISSYNCHIS");
            if (string.IsNullOrEmpty(code))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "未配置GKConfig.json的LIS同步HIS数据的配置项！";
                ZhiFang.Common.Log.Log.Warn(baseResultBool.ErrorInfo);
                return baseResultBool;
            }

            SysRunHelp.LisSyncHisData = true;
            if (code == LisSyncHisType.百色市人民医院.Key)
            {
                baseResultBool = SaveSyncBSDeptUserInfo();
            }

            SysRunHelp.LisSyncHisData = false;
            return baseResultBool;
        }
        public BaseResultBool SaveLisSyncDpetHisData()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            string code = ZhiFang.WebAssist.Common.JSONConfigHelp.GetString(SysConfig.HISSYS.Key, "LISSYNCHIS");
            if (string.IsNullOrEmpty(code))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "未配置GKConfig.json的LIS同步HIS数据的配置项！";
                ZhiFang.Common.Log.Log.Warn(baseResultBool.ErrorInfo);
                return baseResultBool;
            }

            SysRunHelp.LisSyncHisData = true;
            if (code == LisSyncHisType.百色市人民医院.Key)
            {
                baseResultBool = SaveSyncBSDepInfo();
            }

            SysRunHelp.LisSyncHisData = false;
            return baseResultBool;
        }

        #region LIS同步HIS科室人员信息--百色市人民医院
        public BaseResultBool SaveSyncBSDepInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            IList<ZhiFang.Entity.RBAC.Department> lisDeptList = IBDepartment.LoadAll();

            //新增的科室集合信息
            IList<ZhiFang.Entity.RBAC.Department> lisAddDeptList = new List<ZhiFang.Entity.RBAC.Department>();

            //HIS科室人员关系
            IList<BSDeptUserVO> hisDeptUserList = DataAccess_SQL.CreateBSDeptUserVODao_SQL().GetListByHQL("");
            var groupByDeptUserList = hisDeptUserList.GroupBy(p => p.PERSONAL_DEPT);

            foreach (var groupByList in groupByDeptUserList)
            {
                string hisDeptCode = groupByList.Key;
                if (string.IsNullOrEmpty(hisDeptCode)) continue;
                if (string.IsNullOrEmpty(groupByList.ElementAt(0).PERSONAL_DEPT_NAME)) continue;

                //科室是否已经存在LIS
                var lisDeptList3 = lisDeptList.Where(p => p.CName == groupByList.ElementAt(0).PERSONAL_DEPT_NAME);
                if (lisDeptList3 != null && lisDeptList3.Count() > 0) continue;

                //科室是否已经存在LIS
                var lisDeptList2 = lisDeptList.Where(p => p.Code1 == hisDeptCode || p.Code2 == hisDeptCode || p.Code3 == hisDeptCode);
                Entity.RBAC.Department lisDept = null;
                if (lisDeptList2 == null || lisDeptList2.Count() <= 0)
                {
                    lisDept = GetDepartmentByBSHIS(groupByList.ElementAt(0));
                    lisAddDeptList.Add(lisDept);
                }

            }
            ZhiFang.Common.Log.Log.Debug("lisAddDeptList.Count:" + lisAddDeptList.Count);
            AddDeptList(lisAddDeptList);
            return baseResultBool;
        }

        public BaseResultBool SaveSyncBSDeptUserInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            IList<ZhiFang.Entity.RBAC.Department> lisDeptList = IBDepartment.LoadAll();
            IList<ZhiFang.Entity.RBAC.PUser> lisPUserList = IBPUser.LoadAll();
            IList<ZhiFang.Entity.RBAC.DepartmentUser> lisDeptPUserList = IBDepartmentUser.LoadAll();

            //新增的科室集合信息
            IList<ZhiFang.Entity.RBAC.Department> lisAddDeptList = new List<ZhiFang.Entity.RBAC.Department>();
            //新增的人员集合信息
            IList<ZhiFang.Entity.RBAC.PUser> lisAddPUserList = new List<ZhiFang.Entity.RBAC.PUser>();

            //新增的科室人员集合信息
            IList<ZhiFang.Entity.RBAC.DepartmentUser> lisAddDeptPUserList = new List<ZhiFang.Entity.RBAC.DepartmentUser>();

            //HIS科室人员关系
            IList<BSDeptUserVO> hisDeptUserList = DataAccess_SQL.CreateBSDeptUserVODao_SQL().GetListByHQL("");
            ZhiFang.Common.Log.Log.Debug("hisDeptUserList.Count:" + hisDeptUserList.Count);
            var groupByDeptUserList = hisDeptUserList.GroupBy(p => p.PERSONAL_DEPT);
            ZhiFang.Common.Log.Log.Debug("groupByDeptUserList.Count:" + groupByDeptUserList.Count());

            int curPuserId = IBPUser.GetMaxId();
            //需要新增的人员编码集合信息
            IList<string> userIdAddList = new List<string>();
            //需要新增的科室人员编码集合信息
            IList<string> deptUserIdAddList = new List<string>();

            foreach (var groupByList in groupByDeptUserList)
            {
                string hisDeptCode = groupByList.Key;
                if (string.IsNullOrEmpty(hisDeptCode)) continue;
                if (string.IsNullOrEmpty(groupByList.ElementAt(0).PERSONAL_DEPT_NAME)) continue;

                //科室对照码是否已经存在LIS
                var lisDeptList2 = lisDeptList.Where(p => p.Code1 == hisDeptCode || p.Code2 == hisDeptCode || p.Code3 == hisDeptCode);
                Entity.RBAC.Department lisDept = null;
                if (lisDeptList2 == null || lisDeptList2.Count() <= 0)
                {
                    lisDept = GetDepartmentByBSHIS(groupByList.ElementAt(0));
                    lisAddDeptList.Add(lisDept);
                }
                else
                {
                    lisDept = lisDeptList2.ElementAt(0);
                }

                if (lisDept == null)
                {
                    //科室名称是否已经存在LIS
                    var lisDeptList3 = lisDeptList.Where(p => p.CName == groupByList.ElementAt(0).PERSONAL_DEPT_NAME);
                    if (lisDeptList3 != null && lisDeptList3.Count() > 0)
                        lisDept = lisDeptList3.ElementAt(0);
                }

                if (lisDept == null) continue;

                #region 某一科室的人员集合信息处理
                foreach (BSDeptUserVO entityVO in groupByList)
                {
                    string hisPUserCode = entityVO.PERSONAL_CODE;
                    if (string.IsNullOrEmpty(hisPUserCode)) continue;
                    if (userIdAddList.Contains(hisPUserCode)) continue;

                    //人员是否已经存在LIS
                    var lisPUserList2 = lisPUserList.Where(p => p.ShortCode == hisPUserCode || p.Code1 == hisPUserCode || p.Code2 == hisPUserCode || p.Code3 == hisPUserCode);

                    Entity.RBAC.PUser lisPUser = null;
                    if (lisPUserList2 == null || lisPUserList2.Count() <= 0)
                    {
                        lisPUser = GetPUserByBSHIS(entityVO, lisDept, ref curPuserId);
                        lisAddPUserList.Add(lisPUser);
                        //如果科室及人员都不存在LIS,科室人员关系在新增人员时处理
                        string deptUserId = lisDept.Id.ToString() + "" + lisPUser.Id.ToString();
                        if (!deptUserIdAddList.Contains(deptUserId))
                        {
                            lisAddDeptPUserList.Add(GetDepartmentUserByBSHIS(lisDept, lisPUser));
                            deptUserIdAddList.Add(deptUserId);
                        }
                    }
                    else
                    {
                        lisPUser = lisPUserList2.ElementAt(0);
                        //科室人员关系是否存在
                        var lisDeptPUserList2 = lisDeptPUserList.Where(p => p.Department.Id == lisDept.Id && p.PUser.Id == lisPUser.Id);
                        if (lisDeptPUserList2 == null || lisDeptPUserList2.Count() <= 0)
                        {
                            //如果科室及人员都不存在LIS,科室人员关系在新增人员时处理
                            string deptUserId = lisDept.Id.ToString() + "" + lisPUser.Id.ToString();
                            if (!deptUserIdAddList.Contains(deptUserId))
                            {
                                lisAddDeptPUserList.Add(GetDepartmentUserByBSHIS(lisDept, lisPUser));
                                deptUserIdAddList.Add(deptUserId);
                            }
                        }
                    }
                    if (lisPUser == null) continue;

                }
                #endregion

            }
            ZhiFang.Common.Log.Log.Debug("lisAddDeptList.Count:" + lisAddDeptList.Count);
            AddDeptList(lisAddDeptList);

            ZhiFang.Common.Log.Log.Debug("lisAddPUserList.Count:" + lisAddPUserList.Count);
            AddPUserList(lisAddPUserList);

            //已经存在科室或人员信息，但没存在科室人员关系的处理
            ZhiFang.Common.Log.Log.Debug("lisAddDeptPUserList.Count:" + lisAddDeptPUserList.Count);
            AddDeptPUserList(lisAddDeptPUserList);

            return baseResultBool;
        }
        private Entity.RBAC.DepartmentUser GetDepartmentUserByBSHIS(Entity.RBAC.Department lisDept, Entity.RBAC.PUser lisPUser)
        {
            ZhiFang.Entity.RBAC.DepartmentUser deptUser = new Entity.RBAC.DepartmentUser();
            deptUser.Department = lisDept;
            deptUser.PUser = lisPUser;
            deptUser.IsUse = true;
            deptUser.DispOrder = deptUser.PUser.Id;

            return deptUser;
        }
        private Entity.RBAC.Department GetDepartmentByBSHIS(BSDeptUserVO vo)
        {
            int deptId = -1;
            int.TryParse(vo.PERSONAL_DEPT, out deptId);

            Entity.RBAC.Department entity = new Entity.RBAC.Department();
            if (deptId > -1)
                entity.Id = deptId;
            entity.CName = vo.PERSONAL_DEPT_NAME;
            entity.Code1 = vo.PERSONAL_DEPT;//住院对照码
            entity.Code2 = vo.PERSONAL_DEPT;//门诊对照码
            entity.Code4 = entity.Id.ToString();//院感申请对照码取LIS科室编码
            entity.DispOrder = entity.Id;
            entity.Visible = 1;
            entity.ShortCode = GetPinYin(entity.CName);
            entity.ShortName = entity.ShortCode;
            entity.MonitorType = 2;//科室监测
            entity.DataAddTime = DateTime.Now;

            return entity;
        }
        private Entity.RBAC.PUser GetPUserByBSHIS(BSDeptUserVO vo, Entity.RBAC.Department lisDept, ref int curPuserId)
        {
            Entity.RBAC.PUser entity = new Entity.RBAC.PUser();
            if (curPuserId != -1)
            {
                curPuserId = curPuserId + 1;
                entity.Id = curPuserId;
            }
            entity.Department = lisDept;
            entity.CName = vo.PERSONAL_NAME;
            entity.Usertype = vo.PERSONAL_CLASS;
            entity.Code1 = vo.PERSONAL_CODE;//住院对照码
            entity.Code2 = vo.PERSONAL_CODE;//门诊对照码
            entity.ShortCode = vo.PERSONAL_CODE; //GetPinYin(entity.CName);
            entity.Password = vo.PERSONAL_CODE;//先不用加密
            entity.Code4 = entity.Id.ToString();//院感申请对照码取LIS科室编码
            entity.DataAddTime = DateTime.Now;

            entity.DispOrder = entity.Id;
            entity.Visible = 1;

            return entity;
        }

        #endregion

        #region 公共部分
        private void AddDeptList(IList<ZhiFang.Entity.RBAC.Department> lisAddDeptList)
        {
            bool result = true;
            foreach (var entity in lisAddDeptList)
            {
                IBDepartment.Entity = entity;
                result = IBDepartment.Add();
                if (!result)
                {
                    ZhiFang.Common.Log.Log.Error(string.Format("同步科室失败:{0},{1}", entity.Id, entity.CName));
                }
            }
        }
        private void AddPUserList(IList<ZhiFang.Entity.RBAC.PUser> lisAddPUserList)
        {
            bool result = true;
            foreach (var entity in lisAddPUserList)
            {
                IBPUser.Entity = entity;
                result = IBPUser.Add();
                if (!result)
                {
                    ZhiFang.Common.Log.Log.Error(string.Format("同步人员失败:{0},{1},{2}", entity.Department.CName, entity.ShortCode, entity.CName));
                }
            }
        }
        //已经存在科室或人员信息，但没存在科室人员关系的处理
        private void AddDeptPUserList(IList<ZhiFang.Entity.RBAC.DepartmentUser> lisAddDeptPUserList)
        {
            bool result = true;
            foreach (var entity in lisAddDeptPUserList)
            {
                IBDepartmentUser.Entity = entity;
                result = IBDepartmentUser.Add();
                if (!result)
                {
                    ZhiFang.Common.Log.Log.Error(string.Format("同步科室人员失败:{0},{1},{2}", entity.Department.CName, entity.PUser.ShortCode, entity.PUser.CName));
                }
            }
        }
        private string GetPinYin(string chinese)
        {
            string pinYin = "";
            try
            {
                if (chinese != null && chinese.Length > 0)
                {
                    char[] tmpstr = chinese.ToCharArray();
                    foreach (char a in tmpstr)
                    {
                        pinYin += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
                    }
                }
                else
                {
                    return pinYin;
                }
            }
            catch (Exception e)
            {
                pinYin = "";
            }
            return pinYin;
        }

        #endregion

    }
}
