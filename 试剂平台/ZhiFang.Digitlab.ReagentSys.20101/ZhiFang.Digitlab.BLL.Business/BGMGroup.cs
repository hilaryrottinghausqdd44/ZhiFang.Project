
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.RBAC;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BGMGroup : BaseBLL<GMGroup>, ZhiFang.Digitlab.IBLL.Business.IBGMGroup
	{
        public IBRBACModule IBRBACModule { get; set; }
        public IBRBACModuleOper IBRBACModuleOper { get; set; }
        public IBGMGroupType IBGMGroupType { get; set; }
        public IBGMGroupTypeOperate IBGMGroupTypeOperate { get; set; }
        public IBRBACRoleRight IBRBACRoleRight { get; set; }
        public override bool Add()
        {
            bool a = DBDao.Save(this.Entity);
            bool b = true;
            if (this.Entity.GMGroupType != null && this.Entity.IsUse )
            {
                b = SaveRBACModule(this.Entity.GMGroupType.Id);
            }           

            return a && b;
        }

        public bool SaveRBACModule(long gmgrouptypeid)
        {
            GMGroupType gt = IBGMGroupType.Get(this.Entity.GMGroupType.Id);
            RBACModule TestGroupEquipAll_Module = IBRBACModule.SearchModuleByUseCode("TestGroupAll");
            long TestGroupEquipAll_Moduleid = 0;
            #region 判断、新增检验组合仪器模块的跟模块
            if (TestGroupEquipAll_Module == null)
            {
                RBACModule groupequipallmodule = new RBACModule();
                groupequipallmodule.CName = "检验小组";//固定值
                groupequipallmodule.IsUse = true;
                groupequipallmodule.UseCode = "TestGroupAll";//固定值
                groupequipallmodule.Url = gt.Url;
                groupequipallmodule.Comment = "检验小组";//固定值
                groupequipallmodule.ParentID = 0;
                groupequipallmodule.Owner = "1";
                IBRBACModule.Entity = groupequipallmodule;
                if (IBRBACModule.Add())
                {
                    TestGroupEquipAll_Moduleid = IBRBACModule.Entity.Id;
                }
                else
                {
                    return false;
                    throw new Exception("添加检验小组根模块失败！");
                }
            }
            else
            {
                TestGroupEquipAll_Moduleid = TestGroupEquipAll_Module.Id;
            }
            #endregion
            RBACModule rbacmodule = new RBACModule();
            rbacmodule.CName = this.Entity.Name;
            rbacmodule.IsUse = true;
            rbacmodule.ModuleType = 2;
            rbacmodule.UseCode = "GroupID_" + this.Entity.Id;
            rbacmodule.Url = gt.Url;
            rbacmodule.Para = "{GroupType:'" + gt.UseCode + "',GroupID:" + this.Entity.Id + "}";
            rbacmodule.ParentID = TestGroupEquipAll_Moduleid;
            rbacmodule.Owner = "1";
            IBRBACModule.Entity = rbacmodule;
            if (IBRBACModule.Add())
            {
                EntityList<GMGroupTypeOperate> gmgto = IBGMGroupTypeOperate.SearchListByHQL(" gmgrouptypeoperate.GMGroupType.Id= " + gt.Id, 0, 100);
                if (gmgto != null && gmgto.list != null)
                {
                    foreach (var tmpo in gmgto.list)
                    {
                        RBACModuleOper rbacmo = new RBACModuleOper();
                        rbacmo.CName = tmpo.Name;
                        rbacmo.RBACModule = IBRBACModule.Entity;
                        rbacmo.UseCode = tmpo.UseCode;
                        rbacmo.IsUse = true;
                        rbacmo.InvisibleOrDisable = 0;
                        rbacmo.DefaultChecked = false;
                        rbacmo.RowFilterBase = tmpo.RowFilterBase;
                        rbacmo.DataAddTime = DateTime.Now;
                        IBRBACModuleOper.Entity = rbacmo;
                        if (!IBRBACModuleOper.Add())
                        {
                            return false;
                            throw new Exception("添加检验小组模块操作失败！");
                        }
                    }
                }
            }
            else
            {
                return false;
                throw new Exception("添加检验小组模块失败！");
            }
            return true;
        }

        public override bool Remove(long longID)
        {
            if (!IBRBACModule.DeleteModuleByUseCode("GroupID_" + longID + " "))
            {
                return false;
                throw new Exception("删除检验小组模块失败！");
            }
            if (!DBDao.Delete(longID))
            {
                return false;
                throw new Exception("删除检验小组失败！");
            }
            return true;
        }

        public override bool Update(string[] strParas)
        {
            long groupid=0;
            long grouptypeid=0;
            string groupname="";
            string groupisuse="";
            GMGroup bmgroup = null;
            for (int i = 0; i < strParas.Length; i++)
            {
                if (strParas[i].Split('=')[0].Trim() == "Id")
                {
                    groupid = Convert.ToInt64(strParas[i].Split('=')[1]);
                }
                if (strParas[i].Split('=')[0].Trim() == "GMGroupType.Id")
                {
                    grouptypeid = Convert.ToInt64(strParas[i].Split('=')[1]);
                }
                if (strParas[i].Split('=')[0].Trim() == "Name")
                {
                    groupname = strParas[i].Split('=')[1];
                }
                if (strParas[i].Split('=')[0].Trim() == "IsUse")
                {
                    groupisuse = strParas[i].Split('=')[1];
                }
            }
            if (groupid != 0)
            {
                bmgroup = DBDao.Get(groupid);
                GMGroupType gt = IBGMGroupType.Get(grouptypeid);

                RBACModule rbacm = IBRBACModule.SearchModuleByUseCode("GroupID_" + groupid);
                if (rbacm != null)
                {
                    if (groupisuse.Trim() != "" && groupisuse.Trim().ToLower() == "false")
                    {
                        IBRBACModule.DeleteModuleByUseCode("GroupID_" + groupid + " ");
                    }
                    if (groupisuse.Trim() != "" && groupisuse.Trim().ToLower() == "true")
                    {
                        string[] p = new string[5];
                        p[0] = "CName=" + groupname;
                        p[1] = "UseCode='" + "GroupID_" + this.Entity.Id + "'";
                        p[2] = "Url='" + gt.Url + "'";
                        p[3] = "Para='{" + "GroupType:''" + gt.UseCode + "'',GroupID:" + this.Entity.Id + "}'";
                        p[4] = "Id=" + rbacm.Id;


                        if (!IBRBACModule.UpdateSingleFields(p))
                        {
                            return false;
                            throw new Exception("更新检验小组模块失败！");
                        }

                        if (bmgroup != null)
                        {
                            if (bmgroup.GMGroupType.Id.ToString() != grouptypeid.ToString() && grouptypeid != 0)
                            {
                                if (rbacm != null)
                                {
                                    IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id);
                                }

                                EntityList<GMGroupTypeOperate> gmgto = IBGMGroupTypeOperate.SearchListByHQL(" gmgrouptypeoperate.GMGroupType.Id= " + gt.Id, 0, 100);
                                if (gmgto != null && gmgto.list != null)
                                {
                                    foreach (var tmpo in gmgto.list)
                                    {
                                        RBACModuleOper rbacmo = new RBACModuleOper();
                                        rbacmo.CName = tmpo.Name;
                                        rbacmo.RBACModule = rbacm;
                                        rbacmo.UseCode = tmpo.UseCode;
                                        rbacmo.IsUse = true;
                                        rbacmo.InvisibleOrDisable = 0;
                                        rbacmo.DefaultChecked = false;
                                        rbacmo.RowFilterBase = tmpo.RowFilterBase;
                                        rbacmo.DataAddTime = DateTime.Now;
                                        IBRBACModuleOper.Entity = rbacmo;
                                        if (!IBRBACModuleOper.Add())
                                        {
                                            return false;
                                            throw new Exception("添加检验小组模块操作失败！");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return false;
                            throw new Exception("获取检验小组信息失败！");
                        }
                    }
                }
                else
                {
                    this.Entity = bmgroup;
                    this.Entity.GMGroupType = gt;
                    if (bmgroup.IsUse)
                    {
                        SaveRBACModule(grouptypeid);
                    }
                }
            }
            else
            {
                return false;
                throw new Exception("获取检验小组ID失败！");
            }
            bool a = DBDao.Update(strParas);
            return a;
        }

        public bool ST_UDTO_RestGMGroupModule()
        {
           var grouplist= this.LoadAll();
           try
           {
               foreach (var group in grouplist)
               {
                   this.Entity = group;
                   if (group.GMGroupType != null && this.Entity.IsUse)
                   {
                       GMGroupType gt = group.GMGroupType;

                       RBACModule rbacm = IBRBACModule.SearchModuleByUseCode("GroupID_" + group.Id);
                       if (rbacm != null)
                       {
                           string[] p = new string[5];
                           p[0] = "CName='" + group.Name + "'";
                           p[1] = "UseCode='" + "GroupID_" + this.Entity.Id + "'";
                           p[2] = "Url='" + gt.Url + "'";
                           p[3] = "Para='{" + "GroupType:''" + gt.UseCode + "'',GroupID:" + this.Entity.Id + "}'";
                           p[4] = "Id=" + rbacm.Id;


                           if (!IBRBACModule.UpdateSingleFields(p))
                           {
                               return false;
                               throw new Exception("更新检验小组模块失败！");
                           }

                           if (group.GMGroupType.Url.ToLower() != rbacm.Url.ToLower())
                           {
                               if (rbacm != null)
                               {
                                   IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id);
                               }

                               EntityList<GMGroupTypeOperate> gmgto = IBGMGroupTypeOperate.SearchListByHQL(" gmgrouptypeoperate.GMGroupType.Id= " + gt.Id, 0, 100);
                               if (gmgto != null && gmgto.list != null)
                               {
                                   foreach (var tmpo in gmgto.list)
                                   {
                                       RBACModuleOper rbacmo = new RBACModuleOper();
                                       rbacmo.CName = tmpo.Name;
                                       rbacmo.RBACModule = rbacm;
                                       rbacmo.UseCode = tmpo.UseCode;
                                       rbacmo.IsUse = true;
                                       rbacmo.InvisibleOrDisable = 0;
                                       rbacmo.DefaultChecked = false;
                                       rbacmo.RowFilterBase = tmpo.RowFilterBase;
                                       rbacmo.DataAddTime = DateTime.Now;
                                       IBRBACModuleOper.Entity = rbacmo;
                                       if (!IBRBACModuleOper.Add())
                                       {
                                           return false;
                                           throw new Exception("添加检验小组模块操作失败！");
                                       }
                                   }
                               }
                           }
                       }
                       else
                       {
                           this.Entity = group;
                           this.Entity.GMGroupType = gt;
                           SaveRBACModule(group.GMGroupType.Id);
                       }
                   }
               }
               return true;
           }
           catch (Exception e)
           {
               return false;
               ZhiFang.Common.Log.Log.Error("ST_UDTO_RestGMGroupModule："+e.ToString());
               throw new Exception("添加检验小组模块失败！");
           }
        }
    }
}