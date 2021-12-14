using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.IBLL.RBAC;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.BLL.Business
{
    public class BEPBEquip : ZhiFang.Digitlab.BLL.BaseBLL<EPBEquip>, IBEPBEquip
    {
        public IBRBACModule IBRBACModule { get; set; }
        public IBRBACModuleOper IBRBACModuleOper { get; set; }
        public IBEPEquipType IBEPEquipType { get; set; }
        public IBEPEquipTypeOperate IBEPEquipTypeOperate { get; set; }
        public IDEPBEquipDao DBDaoA { get; set; }
        public override bool Add()
        {
            bool a = DBDao.Save(this.Entity);
            bool b=false;
            if (this.Entity.EPEquipType != null && this.Entity.IsUse)
            {
                b = SaveRBACModule(this.Entity.EPEquipType.Id);
            }
            else
            {
                return b;
            }

            return a&&b;
        }

        private bool SaveRBACModule(long equiptypeid)
        {
            EPEquipType et = IBEPEquipType.Get(equiptypeid);

            RBACModule TestGroupEquipAll_Module = IBRBACModule.SearchModuleByUseCode("TestEquipAll");
            long TestGroupEquipAll_Moduleid = 0;
            #region 判断、新增检验组合仪器模块的跟模块
            if (TestGroupEquipAll_Module == null)
            {
                RBACModule groupequipallmodule = new RBACModule();
                groupequipallmodule.CName = "检验仪器";//固定值
                groupequipallmodule.IsUse = true;
                groupequipallmodule.UseCode = "TestEquipAll";//固定值
                groupequipallmodule.Url = et.Url;
                groupequipallmodule.Comment = "检验仪器";//固定值
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
                    throw new Exception("添加检验仪器根模块失败！");
                }
            }
            else
            {
                TestGroupEquipAll_Moduleid = TestGroupEquipAll_Module.Id;
            }
            #endregion
            RBACModule rbacmodule = new RBACModule();
            rbacmodule.CName = this.Entity.CName;
            rbacmodule.IsUse = true;
            rbacmodule.ModuleType = 3;
            rbacmodule.UseCode = "EquipID_" + this.Entity.Id;
            rbacmodule.Url = et.Url;
            rbacmodule.Para = "{EquipType:'" + et.UseCode + "',EquipID:" + this.Entity.Id + "}";
            rbacmodule.ParentID = TestGroupEquipAll_Moduleid;
            rbacmodule.Owner = "1";
            IBRBACModule.Entity = rbacmodule;
            if (IBRBACModule.Add())
            {
                EntityList<EPEquipTypeOperate> gmgto = IBEPEquipTypeOperate.SearchListByHQL(" epequiptypeoperate.EPEquipType.Id= " + et.Id, 0, 100);
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
                            throw new Exception("添加检验仪器模块操作失败！");
                        }
                    }
                }
            }
            else
            {
                return false;
                throw new Exception("添加检验仪器模块失败！");
            }

            return true;
        }
        public override bool Remove(long longID)
        {
            if (!IBRBACModule.DeleteModuleByUseCode("EquipID_" + longID + " "))
            {
                return false;
                throw new Exception("删除检验仪器模块失败！");
            }
            if (!DBDao.Delete(longID))
            {
                return false;
                throw new Exception("删除检验仪器失败！");
            }
            return true;
        }
        public override bool Update(string[] strParas)
        {
            long equipid = 0;
            long equiptypeid = 0;
            string equipname = "";
            string equipisuse = "";
            EPBEquip epbequip = null;
            for (int i = 0; i < strParas.Length; i++)
            {
                if (strParas[i].Split('=')[0].Trim() == "Id")
                {
                    equipid = Convert.ToInt64(strParas[i].Split('=')[1]);
                }
                if (strParas[i].Split('=')[0].Trim() == "EPEquipType.Id")
                {
                    equiptypeid = Convert.ToInt64(strParas[i].Split('=')[1]);
                }
                if (strParas[i].Split('=')[0].Trim() == "CName")
                {
                    equipname = strParas[i].Split('=')[1];
                }
                if (strParas[i].Split('=')[0].Trim() == "IsUse")
                {
                    equipisuse = strParas[i].Split('=')[1];
                }
            }
            if (equipid != 0)
            {
                epbequip = DBDao.Get(equipid);
                EPEquipType et = IBEPEquipType.Get(equiptypeid);

                RBACModule rbacm = IBRBACModule.SearchModuleByUseCode("EquipID_" + equipid);
                if (rbacm != null)
                {
                    //rbacm.CName = equipname.Replace("\'", "");
                    //rbacm.UseCode = "EquipID_" + this.Entity.Id;
                    //rbacm.Url = et.Url;
                    //rbacm.Para = "EquipType:" + et.UseCode + ",EquipID:" + this.Entity.Id;
                    if (equipisuse.Trim() != "" && equipisuse.Trim().ToLower() == "false")
                    {
                        IBRBACModule.DeleteModuleByUseCode("EquipID_" + equipid + " ");
                    }
                    if (equipisuse.Trim() != "" && equipisuse.Trim().ToLower() == "true")
                    {
                        string[] p = new string[5];
                        p[0] = "CName=" + equipname;
                        p[1] = "UseCode='" + "EquipID_" + this.Entity.Id + "'";
                        p[2] = "Url='" + et.Url + "'";
                        p[3] = "Para='{" + "EquipType:''" + et.UseCode + "'',EquipID:" + this.Entity.Id + "}'";
                        p[4] = "Id=" + rbacm.Id;

                        //IBRBACModule.Entity = rbacm;

                        if (!IBRBACModule.UpdateSingleFields(p))
                        {
                            return false;
                            throw new Exception("更新检验仪器模块失败！");
                        }

                        if (epbequip != null)
                        {
                            if (epbequip.EPEquipType.Id.ToString() != equiptypeid.ToString() && equiptypeid != 0)
                            {
                                if (rbacm != null)
                                {
                                    IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id);
                                }

                                EntityList<EPEquipTypeOperate> gmgto = IBEPEquipTypeOperate.SearchListByHQL(" epequiptypeoperate.EPEquipType.Id= " + et.Id, 0, 100);
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
                                            throw new Exception("添加检验仪器模块操作失败！");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return false;
                            throw new Exception("获取检验仪器信息失败！");
                        }
                    }
                }
                else
                {
                    this.Entity = epbequip;
                    this.Entity.EPEquipType = et;
                    SaveRBACModule(equiptypeid);
                }

            }
            else
            {
                return false;
                throw new Exception("获取检验仪器ID失败！");
            }
            bool a = DBDao.Update(strParas);
            return a;
        }

        public bool ST_UDTO_RestEPBEquipModule()
        {
            var Equiplist = this.LoadAll();
            try
            {
                foreach (var Equip in Equiplist)
                {
                    this.Entity = Equip;
                    if (Equip.EPEquipType != null && this.Entity.IsUse)
                    {
                        EPEquipType et = Equip.EPEquipType;

                        RBACModule rbacm = IBRBACModule.SearchModuleByUseCode("EquipID_" + Equip.Id);
                        if (rbacm != null)
                        {
                            //rbacm.CName = equipname.Replace("\'", "");
                            //rbacm.UseCode = "EquipID_" + this.Entity.Id;
                            //rbacm.Url = et.Url;
                            //rbacm.Para = "EquipType:" + et.UseCode + ",EquipID:" + this.Entity.Id;
                            string[] p = new string[5];
                            p[0] = "CName='" + Equip.CName+"'";
                            p[1] = "UseCode='" + "EquipID_" + this.Entity.Id + "'";
                            p[2] = "Url='" + et.Url + "'";
                            p[3] = "Para='{" + "EquipType:''" + et.UseCode + "'',EquipID:" + this.Entity.Id + "}'";
                            p[4] = "Id=" + rbacm.Id;

                            //IBRBACModule.Entity = rbacm;

                            if (!IBRBACModule.UpdateSingleFields(p))
                            {
                                return false;
                                throw new Exception("更新检验仪器模块失败！");
                            }
                            if (Equip.EPEquipType.Url.ToLower() != rbacm.Url.ToLower() )
                            {
                                if (rbacm != null)
                                {
                                    IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id);
                                }
                                EntityList<EPEquipTypeOperate> gmgto = IBEPEquipTypeOperate.SearchListByHQL(" epequiptypeoperate.EPEquipType.Id= " + et.Id, 0, 100);
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
                                            throw new Exception("添加检验仪器模块操作失败！");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.Entity = Equip;
                            this.Entity.EPEquipType = et;
                            SaveRBACModule(Equip.EPEquipType.Id);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_RestEPBEquipModule：" + e.ToString());
                throw new Exception("添加检验仪器模块失败！");
            }
        }
    }
}
