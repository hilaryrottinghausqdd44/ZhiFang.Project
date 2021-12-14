using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using ZhiFang.Model;
using ZhiFang.WebLis.ServerContract;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Model.UiModel;
using ZhiFang.Common.Dictionary;
using ZhiFang.WebLis.Class;
using System.Web;
using System.IO;
using ZhiFang.IBLL.Report;
using System.Text;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common;
using System.Web.Script.Serialization;
using ZhiFang.BLL.Common;
using System.Xml;
using System.ServiceModel.Web;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DictionaryService : IDictionaryService
    {
        IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        IBLL.Common.BaseDictionary.IBDoctor ibd = BLLFactory<IBDoctor>.GetBLL();
        IBLL.Common.BaseDictionary.IBPGroupPrint ibpgp = BLLFactory<IBPGroupPrint>.GetBLL();
        IBLL.Common.BaseDictionary.IBDictionaryGetPubDict ibdgpd = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBDictionaryGetPubDict>.GetBLL();
        //医疗机构字典
        IBLL.Common.BaseDictionary.IBLab_TestItem LabTestItem = BLLFactory<IBLab_TestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBLab_GroupItem LabGroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBSuperGroupControl ibsgc = BLLFactory<IBSuperGroupControl>.GetBLL();
        //中心字典
        IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBGroupItem CenterGroupItem = BLLFactory<IBGroupItem>.GetBLL();
        IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();

        public List<Model.CLIENTELE> GetClientListByRBAC(int page, int rows, string fields, string where, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<Model.CLIENTELE> lc = new List<Model.CLIENTELE>();
            try
            {
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    string alertStr = "未登录，请登陆后继续！";
                    ZhiFang.Common.Log.Log.Info(alertStr);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }

                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, page, rows, fields, where, sort);

                #region 增加默认权限
                string deptCode = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangdeptCode");
                IBLL.Common.BaseDictionary.IBCLIENTELE ibctl = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
                if (deptCode != "" && deptCode != null)
                {
                    Model.CLIENTELE model = ibctl.GetModel(long.Parse(deptCode));

                    if (model != null && cl.list != null)
                    {
                        cl.list.Add(model);
                    }
                }
                #endregion
                //brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(cl) ;
                lc = cl.list.ToList();
                return lc;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
            //return lc;
        }

        public List<Model.Doctor> GetCenterDoctorAllList()
        {
            List<Model.Doctor> doctorlist = new List<Model.Doctor>();
            try
            {
                DataSet ds = ibd.GetAllList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    doctorlist = ibd.DataTableToList(ds.Tables[0]);

                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("查询医生字典无数据！");
                    return null;
                }
                return doctorlist;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
        }

        public List<Model.Doctor> GetCenterDoctorList(int page, int rows, string fields, string jsonentity, string sort)
        {
            List<Model.Doctor> doctorlist = new List<Model.Doctor>();
            try
            {
                Model.Doctor doctor = JsonConvert.DeserializeObject<Doctor>(jsonentity);
                if (doctor != null && jsonentity != null)
                {
                    DataSet ds = ibd.GetListByPage(doctor, page, rows);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        doctorlist = ibd.DataTableToList(ds.Tables[0]);
                    }

                    else
                    {
                        ZhiFang.Common.Log.Log.Error("查询医生字典无数据！");
                        return null;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("查询医生字典无数据！");
                    return null;
                }
                return doctorlist;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }
        }

        #region 小组模版设置

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public BaseResultList<Model.PGroupFormat> GetAllReportGroupModelSet(int page, int rows, string itemkey, string sort)
        {
            ZhiFang.Common.Log.Log.Info("page:" + page + " |rows:" + rows + " |itemkey:" + itemkey + " |sort:" + sort);

            BaseResultList<Model.PGroupFormat> brls = new BaseResultList<Model.PGroupFormat>();
            try
            {
                Model.PGroupFormat pgp_m = new PGroupFormat();
                List<Model.PGroupFormat> lc = new List<Model.PGroupFormat>();
                if (page > 0)
                {
                    page = page - 1;
                }
                pgp_m.SectionName = itemkey;
                EntityList<Model.PGroupFormat> ELpgp = ibpgp.GetAllReportGroupModelSet(pgp_m, page, rows, "", "", "");
                lc = ELpgp.list.ToList();
                brls.list = ELpgp;
                brls.success = true;
                brls.ErrorInfo = "获取列表成功！";
                ZhiFang.Common.Log.Log.Info("获取列表成功！");
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("获取列表失败!" + ex.StackTrace + ex.ToString());
                brls.success = false;
                brls.ErrorInfo = "获取列表失败！";
            }
            return brls;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetReportGroupModelByID(string id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Info("id:" + id);

            try
            {
                Model.PGroupFormat pgp_m = new PGroupFormat();
                List<Model.PGroupFormat> lc = new List<Model.PGroupFormat>();
                Model.PGroupFormat Mpgf = ibpgp.GetModelByID(id);
                if (Mpgf != null)
                {
                    brdv.ErrorInfo = "查询成功！";
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(Mpgf);
                }
                else
                {
                    brdv.ErrorInfo = "查询失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(String.Format("GetReportGroupModelByID.查询失败！{0}{1}", ex.StackTrace, ex));
                brdv.ErrorInfo = "查询失败！";//String.Format("查询失败！{0}{1}", ex.StackTrace, ex);
                brdv.success = false;
            }
            return brdv;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public BaseResult UpdateReportGroupModelByID(Model.PGroupPrint jsonentity)
        {

            BaseResultForm<Model.PGroupPrint> brf = new BaseResultForm<PGroupPrint>();
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroupPrint ibpg = BLLFactory<IBPGroupPrint>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Update(jsonentity) == 1)
                {
                    br.ErrorInfo = "修改成功！";
                    br.success = true;


                }
                else
                {
                    br.ErrorInfo = "修改失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("UpdateReportGroupModelByID.修改失败！" + ex.StackTrace + ex.ToString());
                br.ErrorInfo = "修改失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
            }
            return br;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddReportGroupModel(Model.PGroupPrint jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroupPrint ibpg = BLLFactory<IBPGroupPrint>.GetBLL();
                int id = ibpg.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ErrorInfo = "保存成功！";
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "保存失败！";
                    ZhiFang.Common.Log.Log.Info("保存失败!" + brdv.ErrorInfo);
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("AddReportGroupModel.保存失败！" + ex.ToString());
                brdv.ErrorInfo = "保存失败！";// + ex.ToString();
                brdv.success = false;
            }
            return brdv;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelReportGroupModel(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroupPrint ibpg = BLLFactory<IBPGroupPrint>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                {
                    br.ErrorInfo = "删除成功！";
                    br.success = true;

                }
                else
                {
                    br.ErrorInfo = "删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("DelReportGroupModel.保存失败！" + ex.ToString());
                br.ErrorInfo = "删除失败！";
                br.success = false;
            }
            return br;
        }
        #endregion

        #region 公共服务
        public BaseResultDataValue GetPubDict(string tablename, string fields, string filervalue, string itemno, string selectedflag, string labcode, int page, int rows, string precisequery, string sort, string order)
        {
            //String tableName(表名);String fields(字段)[将需要的字段用逗号拼接],String filerValue(条件模糊匹配值，前台传一个值,后台自动匹配某三个字段(name、code、shortName)like这个值。)
            BaseResultDataValue brdv = new BaseResultDataValue();
            string tablenameParam = tablename;
            if ((!string.IsNullOrEmpty(fields) || !string.IsNullOrEmpty(precisequery)) && string.IsNullOrEmpty(selectedflag))
            {
                DataSet ds = ibdgpd.DictionaryGet(tablename, fields, labcode, filervalue, precisequery, page, rows);
                int total = 0;
                if (page > 0 && rows > 0)
                {
                    total = ibdgpd.GetTotalCount(tablename);
                }
                try
                {
                    ZhiFang.Common.Log.Log.Info("tableName:" + tablename + " |fields:" + fields + " |filerValue:" + filervalue);
                    brdv.ResultDataFormatType = "json";
                    brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                    BaseResultDataSet brds = new BaseResultDataSet();
                    string flagstr = tablename;
                    brds.total = ds.Tables[0].Rows.Count > total ? ds.Tables[0].Rows.Count : total;
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                    {
                        flagstr = "default";
                    }

                    switch (flagstr.ToLower())
                    {
                        case "testitem":
                            EntityListEasyUI<Model.TestItem> EntityLisTestItem = new EntityListEasyUI<Model.TestItem>();
                            IBLL.Common.BaseDictionary.IBTestItem ibDictionaryTestItem = BLLFactory<IBTestItem>.GetBLL();
                            EntityLisTestItem.rows = ibDictionaryTestItem.DataTableToList(ds.Tables[0]);
                            EntityLisTestItem.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisTestItem);
                            break;
                        case "sampletype":
                            EntityListEasyUI<Model.SampleType> EntityLisSampleType = new EntityListEasyUI<Model.SampleType>();
                            IBLL.Common.BaseDictionary.IBSampleType ibDictionarySampleType = BLLFactory<IBSampleType>.GetBLL();
                            EntityLisSampleType.rows = ibDictionarySampleType.DataTableToList(ds.Tables[0]);
                            EntityLisSampleType.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisSampleType);
                            break;
                        case "gendertype":
                            EntityListEasyUI<Model.GenderType> EntityLisGenderType = new EntityListEasyUI<Model.GenderType>();
                            IBLL.Common.BaseDictionary.IBGenderType ibDictionaryGenderType = BLLFactory<IBGenderType>.GetBLL();
                            EntityLisGenderType.rows = ibDictionaryGenderType.DataTableToList(ds.Tables[0]);
                            EntityLisGenderType.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisGenderType);
                            break;
                        case "folktype":
                            EntityListEasyUI<Model.FolkType> EntityLisFolkType = new EntityListEasyUI<Model.FolkType>();
                            IBLL.Common.BaseDictionary.IBFolkType ibDictionaryFolkType = BLLFactory<IBFolkType>.GetBLL();
                            EntityLisFolkType.rows = ibDictionaryFolkType.DataTableToList(ds.Tables[0]);
                            EntityLisFolkType.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisFolkType);
                            break;
                        case "supergroup":
                            EntityListEasyUI<Model.SuperGroup> EntityLisSuperGroup = new EntityListEasyUI<Model.SuperGroup>();
                            IBLL.Common.BaseDictionary.IBSuperGroup ibDictionarySuperGroup = BLLFactory<IBSuperGroup>.GetBLL();
                            EntityLisSuperGroup.rows = ibDictionarySuperGroup.DataTableToList(ds.Tables[0]);
                            EntityLisSuperGroup.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisSuperGroup);
                            break;
                        case "sicktype":
                            EntityListEasyUI<Model.SickType> EntityLisSickType = new EntityListEasyUI<Model.SickType>();
                            IBLL.Common.BaseDictionary.IBSickType ibDictionarySickType = BLLFactory<IBSickType>.GetBLL();
                            EntityLisSickType.rows = ibDictionarySickType.DataTableToList(ds.Tables[0]);
                            EntityLisSickType.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisSickType);
                            break;
                        case "clientelearea":
                            EntityListEasyUI<Model.ClientEleArea> EntityLisClientEleArea = new EntityListEasyUI<Model.ClientEleArea>();
                            IBLL.Common.BaseDictionary.IBClientEleArea ibDictionaryClientEleArea = BLLFactory<IBClientEleArea>.GetBLL();
                            EntityLisClientEleArea.rows = ibDictionaryClientEleArea.DataTableToList(ds.Tables[0]);
                            EntityLisClientEleArea.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisClientEleArea);
                            break;
                        case "district":
                            EntityListEasyUI<Model.District> EntityLisDistrict = new EntityListEasyUI<Model.District>();
                            IBLL.Common.BaseDictionary.IBDistrict ibDictionaryDistrict = BLLFactory<IBDistrict>.GetBLL();
                            EntityLisDistrict.rows = ibDictionaryDistrict.DataTableToList(ds.Tables[0]);
                            EntityLisDistrict.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisDistrict);
                            break;
                        case "wardtype":
                            EntityListEasyUI<Model.WardType> EntityLisWardType = new EntityListEasyUI<Model.WardType>();
                            IBLL.Common.BaseDictionary.IBWardType ibDictionaryWardType = BLLFactory<IBWardType>.GetBLL();
                            EntityLisWardType.rows = ibDictionaryWardType.DataTableToList(ds.Tables[0]);
                            EntityLisWardType.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisWardType);
                            break;
                        case "doctor":
                            EntityListEasyUI<Model.Doctor> EntityLisDoctor = new EntityListEasyUI<Model.Doctor>();
                            IBLL.Common.BaseDictionary.IBDoctor ibDictionaryDoctor = BLLFactory<IBDoctor>.GetBLL();
                            EntityLisDoctor.rows = ibDictionaryDoctor.DataTableToList(ds.Tables[0]);
                            EntityLisDoctor.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisDoctor);
                            break;
                        case "department":
                            EntityListEasyUI<Model.Department> EntityLisDepartment = new EntityListEasyUI<Model.Department>();
                            IBLL.Common.BaseDictionary.IBDepartment ibDictionaryDepartment = BLLFactory<IBDepartment>.GetBLL();
                            EntityLisDepartment.rows = ibDictionaryDepartment.DataTableToList(ds.Tables[0]);
                            EntityLisDepartment.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisDepartment);
                            break;
                        case "pgroup":
                            EntityListEasyUI<Model.PGroup> EntityLisPGroup = new EntityListEasyUI<Model.PGroup>();
                            IBLL.Common.BaseDictionary.IBPGroup ibDictionaryPGroup = BLLFactory<IBPGroup>.GetBLL();
                            EntityLisPGroup.rows = ibDictionaryPGroup.DataTableToList(ds.Tables[0]);
                            EntityLisPGroup.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisPGroup);
                            break;
                        case "clientele":
                            EntityListEasyUI<Model.CLIENTELE> EntityLisCLIENTELE = new EntityListEasyUI<Model.CLIENTELE>();
                            IBLL.Common.BaseDictionary.IBCLIENTELE ibDictionaryCLIENTELE = BLLFactory<IBCLIENTELE>.GetBLL();
                            EntityLisCLIENTELE.rows = ibDictionaryCLIENTELE.DataTableToList(ds.Tables[0]);
                            EntityLisCLIENTELE.total = ds.Tables[0].Rows.Count > total ? ds.Tables[0].Rows.Count : total;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisCLIENTELE);
                            break;
                        case "printformat":
                            EntityListEasyUI<Model.PrintFormat> EntityLisPrintFormat = new EntityListEasyUI<Model.PrintFormat>();
                            IBLL.Common.BaseDictionary.IBPrintFormat ibDictionaryPrintFormat = BLLFactory<IBPrintFormat>.GetBLL();
                            EntityLisPrintFormat.rows = ibDictionaryPrintFormat.DataTableToList(ds.Tables[0]);
                            EntityLisPrintFormat.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisPrintFormat);
                            break;//default
                        case "ageunit":
                            EntityListEasyUI<Model.AgeUnit> EntityLisAgeUnit = new EntityListEasyUI<Model.AgeUnit>();
                            IBLL.Common.BaseDictionary.IBAgeUnit ibDictionaryAgeUnit = BLLFactory<IBAgeUnit>.GetBLL();
                            EntityLisAgeUnit.rows = ibDictionaryAgeUnit.DataTableToList(ds.Tables[0]);
                            EntityLisAgeUnit.total = ds.Tables[0].Rows.Count;
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisAgeUnit);
                            break;
                        //case "groupitem":
                        //    EntityListEasyUI<Model.GroupItem> EntityLisGroupItem = new EntityListEasyUI<Model.GroupItem>();
                        //    IBLL.Common.BaseDictionary.IBGroupItem ibDictionaryGroupItem = BLLFactory<IBGroupItem>.GetBLL();
                        //    EntityLisGroupItem.rows = ibDictionaryGroupItem.DataTableToList(ds.Tables[0]);
                        //    EntityLisGroupItem.total = ds.Tables[0].Rows.Count;
                        //    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLisGroupItem);
                        //    break;
                        case "default":
                            //PKI 送检项目绑定TestItem中的ShortName
                            if (tablenameParam == "TestItem" && fields == "ShortName")
                            {
                                brds.rows = ds.Tables[0].DefaultView.ToTable(true, "ShortName");//shortName去重
                                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                            }
                            else
                            {
                                brds.rows = ds.Tables[0];
                                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                            }

                            break;
                        default:
                            brds.rows = ds.Tables[0];
                            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                            break;
                    }
                    brdv.ErrorInfo = "获取字典成功!";
                    brdv.success = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("GetPubDict.获取“" + tablename + "”失败!" + ex.StackTrace + ex.ToString());
                    brdv.ErrorInfo = "获取字典失败!";
                    brdv.success = false;
                }
            }
            else
            {
                switch (tablename.ToLower())
                {
                    #region 实验室
                    case "b_lab_testitem":
                        return GetLabTestItemModelManage(filervalue, labcode, page, rows, sort, order);
                        break;
                    case "b_lab_sampletype":
                        return GetLabSampleTypeModelManage(filervalue, labcode, page, rows);
                        break;
                    case "u_dic_lab_ageunit":
                        break;
                    case "b_lab_gendertype":
                        return GetLabGenderTypeModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_folktype":
                        return GetLabFolkTypeModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_supergroup":
                        return GetLabSuperGroupModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_sicktype":
                        return GetLabSickTypeModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_district":
                        return GetLabDistrictModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_doctor":
                        return GetLabDoctorModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_department":
                        return GetLabDepartmentModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_pgroup":
                        return GetLabPGroupModelManage(filervalue, labcode, page, rows);
                        break;
                    case "b_lab_groupitem":
                        return GetLabGroupItemModelManage(filervalue, labcode, itemno, selectedflag);
                        break;
                    #endregion

                    #region 中心字典
                    case "testitem":
                        return GetTestItemModelManage(filervalue, page, rows);
                        break;
                    case "sampletype":
                        return GetSampleTypeModelManage(filervalue, page, rows);
                        break;
                    case "ageunit":
                        return GetAgeUnitModelManage(filervalue, page, rows);
                        break;
                    case "gendertype":
                        return GetGenderTypeModelManage(filervalue, page, rows);
                        break;
                    case "folktype":
                        return GetFolkTypeModelManage(filervalue, page, rows);
                        break;
                    case "supergroup":
                        return GetSuperGroupModelManage(filervalue, labcode, page, rows);
                        break;
                    case "sicktype":
                        return GetSickTypeModelManage(filervalue, page, rows);
                        break;
                    case "clientelearea":
                        return GetClientEleAreaModelManage(filervalue, page, rows);
                        break;
                    case "clientele":
                        return GetCLIENTELEModelManage(filervalue, page, rows);
                        break;
                    case "district":
                        return GetDistrictModelManage(filervalue, page, rows);
                        break;
                    case "wardtype":
                        return GetWardTypeModelManage(filervalue, page, rows);
                        break;
                    case "doctor":
                        return GetDoctorModelManage(filervalue, page, rows);
                        break;
                    case "department":
                        return GetDepartmentModelManage(filervalue, page, rows);
                        break;
                    case "pgroup":
                        return GetPGroupModelManage(filervalue, page, rows);
                        break;
                    case "groupitem":
                        return GetGroupItemModelManage(filervalue, itemno, selectedflag);
                        break;
                    case "bphysicalexamtype":
                        return GetBPhysicalExamTypeModelManage(filervalue, page, rows);
                        break;
                    #endregion

                    #region 对照关系
                    //完
                    case "testitemcontrol": return GetTestItemControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_testitemcontrol": return GetTestItemControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_resulttestitemcontrol": return GetTestItemControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    //完
                    case "sampletypecontrol": return GetSampleTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_sampletypecontrol": return GetSampleTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "gendertypecontrol": return GetGenderTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_gendertypecontrol": return GetGenderTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "folktypecontrol": return GetFolkTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_folktypecontrol": return GetFolkTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    //完
                    case "supergroupcontrol": return GetSuperGroupControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_supergroupcontrol": return GetSuperGroupControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "sicktypecontrol": return GetSickTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_sicktypecontrol": return GetSickTypeControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "districtcontrol": return GetDistrictControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_districtcontrol": return GetDistrictControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "doctorcontrol": return GetDoctorControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_doctorcontrol": return GetDoctorControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    //完
                    case "departmentcontrol": return GetDepartmentControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_departmentcontrol": return GetDepartmentControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);

                    case "pgroupcontrol": return GetPGroupControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    case "b_lab_pgroupcontrol": return GetPGroupControlModelManage(filervalue, tablename, labcode, selectedflag, page, rows);
                    #endregion
                    default:
                        brdv.ErrorInfo = "无此字典信息！";
                        break;
                }

            }
            return brdv;
        }
        #endregion

        #region 物流人员服务
        #region 获取物流人员信息列表
        //1:page(当前页)2:rows(每行数量)3:presonname(模糊查询人员名称)
        public BaseResultDataValue GetLogisticsDeliveryPerson(int page, int rows, string presonname)
        {
            WSRBAC.WSRbac a = new WSRBAC.WSRbac();
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultDataSet brds = new BaseResultDataSet();
            //调服务
            //string strxml = a.getUserInfoListByPost("LogisticsOfficer,WebLisApplyInput");
            string strxml = a.getUserInfoListByPost("LogisticsOfficer");
            DataSet ds = new DataSet();
            try
            {
                ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(strxml);
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug("读取物流人员信息列表出错！:" + eee.ToString());
            }
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IBLL.Common.BaseDictionary.IBBusinessLogicClientControl ibbcc = BLLFactory.BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                    ZhiFang.Model.BusinessLogicClientControl l_m = new Model.BusinessLogicClientControl();

                    if (!ds.Tables[0].Columns.Contains("ClientNo"))
                    {
                        ds.Tables[0].Columns.Add("ClientNo");
                    }
                    if (!ds.Tables[0].Columns.Contains("ClientName"))
                    {
                        ds.Tables[0].Columns.Add("ClientName");
                    }
                    if (!ds.Tables[0].Columns.Contains("Name"))
                    {
                        ds.Tables[0].Columns.Add("Name");
                    }
                    DataSet logicDs = new DataSet();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        l_m.Account = dr["Account"].ToString().Trim();
                        l_m.SelectedFlag = true;
                        l_m.Flag = 1;
                        logicDs = ibbcc.GetList(l_m);
                        if (logicDs != null && logicDs.Tables.Count > 0 && logicDs.Tables[0].Rows.Count > 0)
                        {
                            dr["ClientNo"] = logicDs.Tables[0].Rows[0]["ClientNo"];
                            dr["ClientName"] = logicDs.Tables[0].Rows[0]["CName"];
                        }
                        dr["Name"] = dr["NameL"].ToString() + dr["NameF"].ToString();
                    }
                    DataRow[] dra = ds.Tables[0].Select("Name like '%" + presonname + "%'");

                    #region 分页
                    if (dra.CopyToDataTable() != null && dra.CopyToDataTable().Rows.Count > 0)
                        brds.total = dra.CopyToDataTable().Rows.Count;

                    if (page > 0 && rows > 0 && brds.total > rows)
                        brds.rows = Tools.PagePaging.PresentPage(dra.CopyToDataTable(), (page - 1), rows);
                    else
                        brds.rows = dra.CopyToDataTable();
                    #endregion

                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    brdv.success = true;
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "读取物流人员信息列表出错！";
                ZhiFang.Common.Log.Log.Debug("GetLogisticsDeliveryPerson.读取物流人员信息列表出错！:" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region 根据物流人员ID、已选或未选状态，查询客户信息列表
        //1:selectedflag(1已选或2未选0全部) 2:account(物流人员ID)3:page(当前页)4:rows(每行数量) 5:itemkey(name) 客户名字
        public BaseResultDataValue GetLogisticsCustomerByDeliveryIDAndType(int page, int rows, int selectedflag, string account, string itemkey)
        {

            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            BaseResultDataSet brds = new BaseResultDataSet();
            EntityListEasyUI<BusinessLogicClientControl> EntityLis = new EntityListEasyUI<BusinessLogicClientControl>();
            BaseResultDataValue brdv = new BaseResultDataValue();
            DataSet logisticsDs = null;
            try
            {
                if (selectedflag == 0)
                { //全部
                    logisticsDs = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { });
                }
                if (selectedflag == 1)
                {//已选
                    logisticsDs = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = account, Itemkey = itemkey, SelectedFlag = true, Flag = 0 });
                }
                else if (selectedflag == 2)
                {//未选
                    logisticsDs = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = account, Itemkey = itemkey, SelectedFlag = false });
                }

                #region 分页

                if (logisticsDs != null && logisticsDs.Tables.Count > 0 && logisticsDs.Tables[0].Rows.Count > 0)
                {
                    brds.total = logisticsDs.Tables[0].Rows.Count;



                    string dbtype = "";
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                    {
                        dbtype = "default";
                    }
                    if (dbtype == "default")
                    {

                        if (page > 0 && rows > 0 && brds.total > rows)
                            brds.rows = Tools.PagePaging.PresentPage(logisticsDs.Tables[0], (page - 1), rows);
                        else
                            brds.rows = logisticsDs.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = iblcc.DataTableToList(logisticsDs.Tables[0]);
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    brds.rows = null;
                    brds.total = 0;
                }
                #endregion

                brdv.success = true;
                brdv.ResultDataFormatType = "JSON";

                ZhiFang.Common.Log.Log.Debug("成功！");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "失败！";
                ZhiFang.Common.Log.Log.Debug("GetLogisticsCustomerByDeliveryIDAndType.失败！:" + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region 修改物流人员和客户的关系
        //1:account(物流人员ID)  2:clientlist(已选单位) 3:ClientNo(所属单位) 4:operatetype
        public BaseResult UpdateLogisticsDeliveryCustomer(LogisticsEntity strentity)
        {

            BaseResult br = new BaseResult();
            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            try
            {
                #region 验证必填
                try
                {
                    if (!string.IsNullOrEmpty(strentity.ClientNo))
                    {
                        br.success = false;
                        br.ErrorInfo = "请求失败！";
                        ZhiFang.Common.Log.Log.Debug("UpdateLogisticsDeliveryCustomer.请求失败！ClientNo：" + strentity.ClientNo);
                    }
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "请求失败！所属单位必填，当前已选：" + strentity.ClientNo;
                    ZhiFang.Common.Log.Log.Debug("UpdateLogisticsDeliveryCustomer.请求失败！所属单位必填：" + strentity.ClientNo + ex.ToString());
                    return br;
                }
                #endregion

                #region 删除单位
                DataSet tmpds = iblcc.GetList(new ZhiFang.Model.BusinessLogicClientControl() { Account = strentity.Account, SelectedFlag = true, Flag = 1 });
                if (tmpds != null && tmpds.Tables.Count > 0 && tmpds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < tmpds.Tables[0].Rows.Count; i++)
                    {
                        iblcc.Delete(tmpds.Tables[0].Rows[i]["Id"].ToString().Trim());
                    }
                }
                #endregion

                #region 所属单位
                if (!string.IsNullOrEmpty(strentity.ClientNo))
                {
                    iblcc.Add(new Model.BusinessLogicClientControl() { Account = strentity.Account, ClientNo = strentity.ClientNo.ToString(), Flag = 1 });
                }
                #endregion



                #region 已选单位

                try
                {
                    if (strentity.ClientList.Count > 0)
                    {
                        br.success = true;
                        br.ErrorInfo = "";
                        ZhiFang.Common.Log.Log.Info("ClientList条数：" + strentity.ClientList.Count);
                    }
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "无法设置已选单位！ClientList条数：" + strentity.ClientList;
                    ZhiFang.Common.Log.Log.Debug("无法设置已选单位！ClientList条数：" + strentity.ClientList + ex.ToString());
                    return br;
                }

                List<Model.BusinessLogicClientControl> listLogic = new List<Model.BusinessLogicClientControl>();
                if (strentity.ClientList.Count >= 0)
                {
                    for (int i = 0; i < strentity.ClientList.Count; i++)
                    {
                        Model.BusinessLogicClientControl lcc_m = new Model.BusinessLogicClientControl() { Account = strentity.Account, ClientNo = strentity.ClientList[i] };
                        listLogic.Add(lcc_m);
                    }
                }
                else
                {
                    Model.BusinessLogicClientControl lcc_m = new Model.BusinessLogicClientControl() { Account = strentity.Account, ClientNo = "" };
                    listLogic.Add(lcc_m);
                }
                #endregion
                if (iblcc.Add(listLogic))
                {
                    br.success = true;
                    br.ErrorInfo = "请求成功！";
                    ZhiFang.Common.Log.Log.Debug("请求成功！");
                }
                else
                {
                    br.success = false;
                    br.ErrorInfo = "请求失败！";
                    ZhiFang.Common.Log.Log.Debug("请求失败！");
                }
            }
            catch (Exception ex)
            {
                br.success = false;
                br.ErrorInfo = "请求失败！";
                ZhiFang.Common.Log.Log.Debug("UpdateLogisticsDeliveryCustomer.异常信息" + ex.ToString());
            }
            return br;
        }
        #endregion
        #endregion

        #region 导出报告
        public string DownReportExcel(string id)
        {
            IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
            Model.VIEW_ReportItemFull model = new VIEW_ReportItemFull();
            DataSet das = ibvtif.GetViewItemFull(id);

            string FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
            HttpContext.Current.Response.AddHeader("content-type", "application / ms - excel");//设置输出文件的类型

            //定义一个输出流
            StringWriter tw = new StringWriter();//System.IO
            //打印表头
            string columns = "送检医院,核收时间,病人姓名,样本号,项目中文,项目英文,项目结果,项目单位,项目参考值,项目高低状态";
            for (int k = 0; k < columns.Split(',').Length; k++)
                tw.Write(columns.Split(',')[k] + '\t');
            tw.WriteLine();
            //打印数据行
            for (int i = 0; i < das.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < das.Tables[0].Columns.Count; j++)
                {
                    tw.Write(das.Tables[0].Rows[i][j].ToString() + '\t');
                }
                tw.WriteLine();
            }
            HttpContext.Current.Response.Write(tw.ToString());
            return null;
        }

        #endregion

        #region 财务对账单
        #region 获取对账单列表
        //1:page(当前页)2:rows(每行数量) 3:monthname(对账月) 4:clientname(客户名称)5:status(确认状态)
        public BaseResultDataValue GetBill(int page, int rows, string monthname, string clientname, string status)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultDataSet brds = new BaseResultDataSet();
            IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            IBTB_CheckClientAccount ibtcca = BLLFactory<IBTB_CheckClientAccount>.GetBLL();

            Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
            try
            {
                if (page < 1 && rows < 1)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "操作失败! page=" + page + ",rows=" + rows;
                    return brdv;
                }
                string clientList = "";

                //ZhiFang.IBLL.Common.BaseDictionary.
                User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                BusinessLogicClientControl.Account = u.Account;
                BusinessLogicClientControl.Flag = 1;
                BusinessLogicClientControl.SelectedFlag = true;
                DataSet ds = iblcc.GetList(BusinessLogicClientControl);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        clientList += ds.Tables[0].Rows[i]["ClientNo"].ToString() + ",";
                    }
                }
                clientList = clientList.Remove(clientList.Length - 1);
                string strWhere = "";
                if (clientList != "")
                {
                    strWhere = " clientno in (" + clientList + ")";
                }
                DataSet tbccDs = null;
                TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();

                #region 验证并赋值
                if (!string.IsNullOrEmpty(monthname))
                    tbcc_m.monthname = monthname.Trim();
                if (!string.IsNullOrEmpty(clientname))
                    tbcc_m.clientname = clientname.Trim();
                if (!string.IsNullOrEmpty(status))
                    tbcc_m.status = status.Trim();
                #endregion

                tbccDs = ibtcca.GetListByPage(strWhere, tbcc_m, page, rows);

                if (!tbccDs.Tables[0].Columns.Contains("url"))
                {
                    tbccDs.Tables[0].Columns.Add("url");
                }
                if (!tbccDs.Tables[0].Columns.Contains("urlitem"))
                {
                    tbccDs.Tables[0].Columns.Add("urlitem");
                }
                DataSet logicDs = new DataSet();
                foreach (DataRow dr in tbccDs.Tables[0].Rows)
                {
                    string httpUrl = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.Url.Segments[0] + HttpContext.Current.Request.Url.Segments[1];
                    httpUrl = httpUrl.Replace(":80", "");
                    dr["url"] = httpUrl + dr["filepath"];
                    dr["urlitem"] = httpUrl + dr["filepathitem"];
                }

                brds.rows = tbccDs.Tables[0];
                brds.total = ibtcca.GetRecordCount(strWhere, tbcc_m);
                brdv.success = true;
                brdv.ErrorInfo = "操作成功!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "操作失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                ZhiFang.Common.Log.Log.Error("GetBill" + ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
            }
            return brdv;
        }
        #endregion

        #region 修改对账单
        //1:id(序号) 2:status(确认状态) 3:remark(备注)
        public BaseResult UpdateBill(TB_CheckClientAccount jsonentity)
        {
            BaseResult br = new BaseResult();
            IBTB_CheckClientAccount ibtcca = BLLFactory<IBTB_CheckClientAccount>.GetBLL();
            TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();
            #region 验证或赋值
            if (jsonentity.id <= 0 && (string.IsNullOrEmpty(jsonentity.status) || string.IsNullOrEmpty(jsonentity.remark)))
            {
                br.success = false;
                ZhiFang.Common.Log.Log.Debug("请求失败！id:" + jsonentity.id + "|status:" + jsonentity.status + "|remark:" + jsonentity.remark);
                br.ErrorInfo = "请求失败！id:" + jsonentity.id + "|status:" + jsonentity.status + "|remark:" + jsonentity.remark;
            }
            else
            {
                tbcc_m.id = jsonentity.id;
                tbcc_m.status = jsonentity.status;
                tbcc_m.remark = jsonentity.remark;
            }
            #endregion

            #region 修改对账单
            try
            {
                if (ibtcca.Update(tbcc_m))
                {
                    br.success = true;
                    br.ErrorInfo = "请求成功！";
                    ZhiFang.Common.Log.Log.Debug("请求成功！");
                }
                else
                {
                    br.success = false;
                    br.ErrorInfo = "请求失败！";
                    ZhiFang.Common.Log.Log.Debug("请求失败！");
                }
            }
            #endregion
            catch (Exception ex)
            {
                br.success = false;
                br.ErrorInfo = "请求失败！";// + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("UpdateBill.异常信息" + ex.ToString());
            }
            return br;
        }
        #endregion

        #region 下载对账单
        public string DownLoadExcel(int id, int type)
        {
            IBTB_CheckClientAccount ibtcca = BLLFactory<IBTB_CheckClientAccount>.GetBLL();
            TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();
            string httpUrlHerd = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.Url.Segments[0] + HttpContext.Current.Request.Url.Segments[1];
            string url = "";
            try
            {

                #region 验证或赋值
                if (id <= 0)
                    ZhiFang.Common.Log.Log.Debug("请求失败！id:" + id + "|type:" + type);
                #endregion

                #region 获取路径(先判断type类型)
                tbcc_m = ibtcca.GetModel(id);
                if (type == 0)
                    url = httpUrlHerd + tbcc_m.filepath;
                if (type == 1)
                    url = httpUrlHerd + tbcc_m.filepathitem;
                #endregion
                if (!Directory.Exists(@"\TotalExcel"))
                    Directory.CreateDirectory(@"\TotalExcel");
                if (!File.Exists(url))
                {
                    string path = type == 0 ? tbcc_m.filepath : tbcc_m.filepathitem;
                    #region 如果文件不存在，请求获取并保存本地
                    //下载
                    //流
                    FileStream loadFile = new FileStream(HttpContext.Current.Request.PhysicalApplicationPath + path, FileMode.Open);
                    byte[] byteData = new byte[loadFile.Length];
                    loadFile.Read(byteData, 0, byteData.Length);
                    loadFile.Close();
                    //路径
                    string folder = HttpContext.Current.Request.PhysicalApplicationPath + path;
                    if (File.Exists(folder))
                    {
                        FileInfo DownloadFile = new FileInfo(folder);
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.Buffer = false;
                        HttpContext.Current.Response.ContentType = "application/octet-stream";

                        if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
                            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + tbcc_m.monthname + tbcc_m.clientname + (type == 1 ? "项目对帐" : "") + "." + folder.Split('.')[1] + "\"");
                        else
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(tbcc_m.monthname + tbcc_m.clientname + (type == 1 ? "项目对帐" : "") + "." + folder.Split('.')[1]));
                        HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                        HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                        return null;
                    }
                    else
                        return "{\"ErrorInfo\": \"请求失败,文件不存在。\",\"success\": true,\"filepath\": \"" + url + "\" }";

                    #endregion
                }
                else
                    return "{\"ErrorInfo\": \"请求失败,路径不存在。\",\"success\": true,\"filepath\": \"" + url + "\" }";

            }
            catch (Exception ex)
            {
                //return null;
                return "{\"ErrorInfo\": \"请求失败!+" + ex.ToString() + "\",\"success\": false,\"filepath\": \"" + url + "\" }";
            }
        }

        #endregion

        #region 上传对账单
        internal static ServiceHost myServiceHost = null;
        public bool UpLoadBll(DataSet ds, List<string> filesname, List<byte[]> filebyte, List<string> fileitemsname, List<byte[]> fileitemsbyte, out string errorinfo, out string strfailidforlist)
        {

            //设置BasicHttpBinding绑定
            BasicHttpBinding myBinding = new BasicHttpBinding();
            //安全模式None
            myBinding.Security.Mode = BasicHttpSecurityMode.None;
            Uri baseAddress = new Uri("http://localhost:80/WCFService/");
            errorinfo = "";
            strfailidforlist = "";
            IBTB_CheckClientAccount ibtcca = BLLFactory<IBTB_CheckClientAccount>.GetBLL();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();
                        tbcc_m.id = (int)ds.Tables[0].Rows[i]["cid"];
                        tbcc_m.monthid = (int)ds.Tables[0].Rows[i]["yid"];
                        tbcc_m.monthname = ds.Tables[0].Rows[i]["yname"].ToString();
                        tbcc_m.clientname = ds.Tables[0].Rows[i]["cclientname"].ToString();
                        tbcc_m.status = ds.Tables[0].Rows[i]["cstatus"].ToString();
                        tbcc_m.remark = ds.Tables[0].Rows[i]["cremark"].ToString();
                        tbcc_m.checkdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ccheckdate"].ToString());
                        tbcc_m.filepath = ds.Tables[0].Rows[i]["cfilepath"].ToString();
                        tbcc_m.createdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ccreatedate"].ToString());
                        tbcc_m.reply = ds.Tables[0].Rows[i]["creply"].ToString();
                        tbcc_m.clientno = ds.Tables[0].Rows[i]["cclientno"].ToString();
                        tbcc_m.auditstatus = ds.Tables[0].Rows[i]["cauditstatus"].ToString();
                        tbcc_m.downloadfile = ds.Tables[0].Rows[i]["downloadfile"].ToString();
                        tbcc_m.count = ds.Tables[0].Rows[i]["ccount"].ToString();
                        tbcc_m.sumprice = ds.Tables[0].Rows[i]["csumprice"].ToString();
                        tbcc_m.filepathitem = ds.Tables[0].Rows[i]["cfilepathitem"].ToString();
                        tbcc_m.downloadfileitem = ds.Tables[0].Rows[i]["downloadfileitem"].ToString();
                        ibtcca.Delete(tbcc_m.id);
                        if (ibtcca.Add(tbcc_m) != 0)
                            strfailidforlist += tbcc_m.id + ",";
                    }
                    catch (Exception ex)
                    {
                        errorinfo = "";
                        ZhiFang.Common.Log.Log.Error("UpLoadBll,异常" + ex.ToString());
                        return false;
                    }
                }
                strfailidforlist = strfailidforlist.TrimEnd(',');
                return true;
            }
            else
                errorinfo = "无对账单信息";

            return true;
        }

        #endregion
        #endregion

        #region 中心字典服务

        #region CLIENTELE 中心医疗机构字典
        #region CLIENTELE 字典表_查询
        public BaseResultDataValue GetCLIENTELEModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBCLIENTELE ibDictionary = BLLFactory<IBCLIENTELE>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.CLIENTELE> EntityLis = new EntityListEasyUI<Model.CLIENTELE>();
            try
            {
                if (page > 0)
                    page = page - 1;
                DataSet ds = ibDictionary.GetListByPage(new CLIENTELE { ClienteleLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);

                    EntityLis.total = ibDictionary.GetTotalCount(new CLIENTELE { ClienteleLikeKey = itemkey, OrderField = "DTimeStampe" });

                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求成功,字典无数据！");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region CLIENTELE 字典表_增加
        public BaseResultDataValue AddCLIENTELEModel(Model.CLIENTELE jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBCLIENTELE ibDictionary = BLLFactory<IBCLIENTELE>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "CLIENTELE 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "CLIENTELE 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("CLIENTELE 增加请求失败！" + ex.ToString() + ex.StackTrace);
            }
            return brdv;
        }
        #endregion

        #region CLIENTELE 字典表_修改
        public BaseResult UpdateCLIENTELEModelByID(Model.CLIENTELE jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBCLIENTELE ibDictionary = BLLFactory<IBCLIENTELE>.GetBLL();
                Model.CLIENTELE m_pgp = new CLIENTELE();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "CLIENTELE 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "CLIENTELE 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error("CLIENTELE 修改请求失败！" + ex.StackTrace + ex.ToString());
            }
            return br;
        }
        #endregion

        #region  CLIENTELE 字典表_删除
        public BaseResult DeleteCLIENTELEModelByID(string clinetNo)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBCLIENTELE ibDictionary = BLLFactory<IBCLIENTELE>.GetBLL();

                long tempClinetNo = long.Parse(clinetNo);
                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "clinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region ClientEleArea 中心区域字典
        #region ClientEleArea 字典表_查询
        public BaseResultDataValue GetClientEleAreaModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBClientEleArea ibDictionary = BLLFactory<IBClientEleArea>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.ClientEleArea> EntityLis = new EntityListEasyUI<Model.ClientEleArea>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new ClientEleArea { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new ClientEleArea { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    brdv.ErrorInfo = "ClientEleArea 查询获取成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("ClientEleArea 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "ClientEleArea 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error("ClientEleArea 查询请求失败!" + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region ClientEleArea 字典表_增加
        public BaseResultDataValue AddClientEleAreaModel(Model.ClientEleArea jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBClientEleArea ibDictionary = BLLFactory<IBClientEleArea>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "ClientEleArea 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "ClientEleArea 增加请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("ClientEleArea 增加请求失败！" + ex.StackTrace + ex.ToString());
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region ClientEleArea 字典表_修改
        public BaseResult UpdateClientEleAreaModelByID(Model.ClientEleArea jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBClientEleArea ibDictionary = BLLFactory<IBClientEleArea>.GetBLL();
                Model.ClientEleArea m_pgp = new ClientEleArea();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "ClientEleArea 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "ClientEleArea 修改请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("ClientEleArea 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region ClientEleArea 字典表_删除
        public BaseResult DeleteClientEleAreaModelByID(string areaID)
        {

            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBClientEleArea ibDictionary = BLLFactory<IBClientEleArea>.GetBLL();
                int intareaID = int.Parse(areaID);
                if (ibDictionary.Exists(intareaID))
                {
                    if (ibDictionary.Delete(intareaID) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "areaID不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        //AreaID
        #endregion

        #region AgeUnit 中心年龄字典
        #region AgeUnit 字典表_查询
        public BaseResultDataValue GetAgeUnitModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBAgeUnit ibDictionary = BLLFactory<IBAgeUnit>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.AgeUnit> EntityLis = new EntityListEasyUI<Model.AgeUnit>();
            try
            {
                if (page > 0)
                    page = page - 1;

                //DataSet ds = ibDictionary.GetListByPage(new AgeUnit { SearchLikeKey = itemkey }, page, rows);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                //    EntityLis.total = ibDictionary.GetTotalCount(new AgeUnit { SearchLikeKey = itemkey });
                //    brdv.success = true;
                //    ZhiFang.Common.Log.Log.Info("AgeUnit 查询请求成功!");
                //    brdv.ResultDataFormatType = "JSON";
                //    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                //}
                //else
                //{
                //    ZhiFang.Common.Log.Log.Info("AgeUnit 查询请求成功,字典无数据!");
                //    brdv.success = true;
                //    brdv.ResultDataFormatType = "JSON";
                //    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                //}
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "AgeUnit 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
        }
        #endregion

        #region AgeUnit 字典表_增加
        public BaseResultDataValue AddAgeUnitModel(Model.AgeUnit jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBAgeUnit ibDictionary = BLLFactory<IBAgeUnit>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "AgeUnit 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "AgeUnit增加请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("AgeUnit增加请求失败！" + ex.StackTrace + ex.ToString());
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region AgeUnit 字典表_修改
        public BaseResult UpdateAgeUnitModelByID(Model.AgeUnit jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBAgeUnit ibDictionary = BLLFactory<IBAgeUnit>.GetBLL();
                Model.AgeUnit m_pgp = new AgeUnit();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "AgeUnit 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "AgeUnit 修改请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("AgeUnit 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region AgeUnit 字典表_删除
        public BaseResult DeleteAgeUnitModelByID(string AgeUnitNo)
        {

            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBAgeUnit ibDictionary = BLLFactory<IBAgeUnit>.GetBLL();
                int intAgeUnitNo = int.Parse(AgeUnitNo);
                if (ibDictionary.Exists(intAgeUnitNo))
                {
                    if (ibDictionary.Delete(intAgeUnitNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "AgeUnitNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }


        #endregion
        #endregion

        #region Doctor 中心医生字典
        #region Doctor 字典表_查询
        public BaseResultDataValue GetDoctorModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDoctor ibDictionary = BLLFactory<IBDoctor>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Doctor> EntityLis = new EntityListEasyUI<Model.Doctor>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Doctor { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Doctor { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region Doctor 字典表_增加
        public BaseResultDataValue AddDoctorModel(Model.Doctor jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBDoctor ibDictionary = BLLFactory<IBDoctor>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    ;
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "Doctor 增加请求失败,未保存成功!";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "Doctor 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                ZhiFang.Common.Log.Log.Error("Doctor 增加请求失败！" + ex.ToString() + ex.StackTrace);
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region Doctor 字典表_修改
        public BaseResult UpdateDoctorModelByID(Model.Doctor jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDoctor ibDictionary = BLLFactory<IBDoctor>.GetBLL();
                Model.Doctor m_pgp = new Doctor();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "Doctor 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "Doctor 修改请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("Doctor 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region Doctor 字典表_删除
        public BaseResult DeleteDoctorModelByID(string DoctorNo)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDoctor ibDictionary = BLLFactory<IBDoctor>.GetBLL();
                int tempDoctorNo = int.Parse(DoctorNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDoctorControl ibdictionarycontrol = BLLFactory<IBDoctorControl>.GetBLL();
                Model.DoctorControl modeldoctor = new DoctorControl();
                modeldoctor.DoctorNo = tempDoctorNo;
                modeldoctor.ControlDoctorNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldoctor);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + DoctorNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempDoctorNo))
                {
                    if (ibDictionary.Delete(tempDoctorNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "tempDoctorNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }

        #endregion

        #endregion

        #region District 中心病区字典
        #region District 字典表_查询
        public BaseResultDataValue GetDistrictModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDistrict ibDictionary = BLLFactory<IBDistrict>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.District> EntityLis = new EntityListEasyUI<Model.District>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new District { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new District { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region District 字典表_增加
        public BaseResultDataValue AddDistrictModel(Model.District jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBDistrict ibDictionary = BLLFactory<IBDistrict>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "District 增加请求失败，保存失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "District 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                ZhiFang.Common.Log.Log.Error("District 增加请求失败！" + ex.ToString() + ex.StackTrace);
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region District 字典表_修改
        public BaseResult UpdateDistrictModelByID(Model.District jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDistrict ibDictionary = BLLFactory<IBDistrict>.GetBLL();
                Model.District m_pgp = new District();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "District 修改请求失败,修改错误！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "District 修改请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("District 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region District 字典表_删除
        public BaseResult DeleteDistrictModelByID(string DistrictNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBDistrict ibDictionary = BLLFactory<IBDistrict>.GetBLL();
                int tempClinetNo = int.Parse(DistrictNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDistrictControl ibdictionarycontrol = BLLFactory<IBDistrictControl>.GetBLL();
                Model.DistrictControl modeldistrict = new DistrictControl();
                modeldistrict.DistrictNo = tempClinetNo;
                modeldistrict.ControlDistrictNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + DistrictNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "DistrictNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;

        }
        #endregion
        #endregion

        #region PGroup 中心检验小组字典
        #region PGroup 字典表_查询
        public BaseResultDataValue GetPGroupModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBPGroup ibDictionary = BLLFactory<IBPGroup>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.PGroup> EntityLis = new EntityListEasyUI<Model.PGroup>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new PGroup { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    if (!string.IsNullOrEmpty(itemkey))
                        EntityLis.total = ibDictionary.GetTotalCount(new PGroup { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    else
                        EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region PGroup 字典表_增加
        public BaseResultDataValue AddPGroupModel(Model.PGroup jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroup ibDictionary = BLLFactory<IBPGroup>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "PGroup 增加请求失败，保存出错！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {

                brdv.ErrorInfo = "PGroup 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                ZhiFang.Common.Log.Log.Error("PGroup 增加请求失败！" + ex.ToString() + ex.StackTrace);
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region PGroup 字典表_修改
        public BaseResult UpdatePGroupModelByID(Model.PGroup jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroup ibDictionary = BLLFactory<IBPGroup>.GetBLL();
                Model.PGroup m_pgp = new PGroup();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "PGroup 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "PGroup 修改请求失败！" + ex.StackTrace + ex.ToString();
                br.success = false;
            }
            ZhiFang.Common.Log.Log.Error(br.ErrorInfo);
            return br;
        }
        #endregion

        #region PGroup 字典表_删除
        public BaseResult DeletePGroupModelByID(string SectionNo)
        {

            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBPGroup ibDictionary = BLLFactory<IBPGroup>.GetBLL();
                int tempClinetNo = int.Parse(SectionNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBPGroupControl ibdictionarycontrol = BLLFactory<IBPGroupControl>.GetBLL();
                Model.PGroupControl modeldistrict = new PGroupControl();
                modeldistrict.SectionNo = tempClinetNo;
                modeldistrict.ControlSectionNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + SectionNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "SectionNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }

        #endregion
        #endregion

        #region SampleType 中心样本类型字典
        #region SampleType 字典表_查询
        public BaseResultDataValue GetSampleTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSampleType ibDictionary = BLLFactory<IBSampleType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SampleType> EntityLis = new EntityListEasyUI<Model.SampleType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new SampleType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    if (!string.IsNullOrEmpty(itemkey))
                        EntityLis.total = ibDictionary.GetTotalCount(new SampleType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    else
                        EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region SampleType 字典表_增加
        public BaseResultDataValue AddSampleTypeModel(Model.SampleType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBSampleType ibDictionary = BLLFactory<IBSampleType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "SampleType 增加请求失败，保存出错！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "SampleType 增加请求失败！";// + ex.ToString();
                ZhiFang.Common.Log.Log.Error("SampleType 增加请求失败！" + ex.ToString());
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region SampleType 字典表_修改
        public BaseResult UpdateSampleTypeModelByID(Model.SampleType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSampleType ibDictionary = BLLFactory<IBSampleType>.GetBLL();
                Model.SampleType m_pgp = new SampleType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SampleType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "SampleType 修改请求失败！";// + ex.StackTrace + ex.ToString();
                ZhiFang.Common.Log.Log.Error("SampleType 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region SampleType 字典表_删除

        public BaseResult DeleteSampleTypeModelByID(string SampleTypeNo)
        {

            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBSampleType ibDictionary = BLLFactory<IBSampleType>.GetBLL();
                int tempClinetNo = int.Parse(SampleTypeNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSampleTypeControl ibdictionarycontrol = BLLFactory<IBSampleTypeControl>.GetBLL();
                Model.SampleTypeControl modeldistrict = new SampleTypeControl();
                modeldistrict.SampleTypeNo = tempClinetNo;
                modeldistrict.ControlSampleTypeNo = "-1";
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + SampleTypeNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "SampleTypeNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region TestItem 中心项目字典
        #region TestItem 字典表_查询
        public BaseResultDataValue GetTestItemModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBTestItem ibDictionary = BLLFactory<IBTestItem>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.TestItem> EntityLis = new EntityListEasyUI<Model.TestItem>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new TestItem { TestItemLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    if (!string.IsNullOrEmpty(itemkey))
                        EntityLis.total = ibDictionary.GetTotalCount(new TestItem { TestItemLikeKey = itemkey, OrderField = "DTimeStampe" });
                    else
                        EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region TestItem 字典表_增加
        public BaseResultDataValue AddTestItemModel(Model.TestItem jsonentity)
        {
            //因为涉及到微信开单所以要加Session验证
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBTestItem ibDictionary = BLLFactory<IBTestItem>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "TestItem 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "TestItem 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                ZhiFang.Common.Log.Log.Error("TestItem 增加请求失败！" + ex.ToString() + ex.StackTrace);
                brdv.success = false;
            }

            return brdv;
        }
        #endregion

        #region TestItem 字典表_修改
        public BaseResult UpdateTestItemModelByID(Model.TestItem jsonentity)
        {
            //因为涉及到微信开单所以要加Session验证
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBTestItem ibDictionary = BLLFactory<IBTestItem>.GetBLL();
                Model.TestItem m_pgp = new TestItem();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "TestItem 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "TestItem 修改请求失败！";
                ZhiFang.Common.Log.Log.Error("TestItem 修改请求失败！" + ex.StackTrace + ex.ToString());
                br.success = false;
            }

            return br;
        }
        #endregion

        #region TestItem 字典表_删除
        public BaseResult DeleteTestItemModelByID(string ItemNo)
        {
            //因为涉及到微信开单所以要加Session验证
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBTestItem ibDictionary = BLLFactory<IBTestItem>.GetBLL();
                //int tempClinetNo = int.Parse(ItemNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBTestItemControl ibdictionarycontrol = BLLFactory<IBTestItemControl>.GetBLL();
                Model.TestItemControl modeldistrict = new TestItemControl();
                modeldistrict.ItemNo = ItemNo;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + ItemNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                #region 删除组套关系
                IBLL.Common.BaseDictionary.IBGroupItem ibGroupItem = BLLFactory<IBGroupItem>.GetBLL();
                Model.GroupItem groupitemModel = new Model.GroupItem();
                groupitemModel.PItemNo = ItemNo;
                groupitemModel.ItemNo = null;
                ibGroupItem.Delete(groupitemModel, "");
                #endregion

                if (ibDictionary.Exists(ItemNo))
                {
                    if (ibDictionary.Delete(ItemNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "ItemNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region FolkType 中心民族类型字典
        #region FolkType 字典表_查询
        public BaseResultDataValue GetFolkTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBFolkType ibDictionary = BLLFactory<IBFolkType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.FolkType> EntityLis = new EntityListEasyUI<Model.FolkType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new FolkType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    if (!string.IsNullOrEmpty(itemkey))
                        EntityLis.total = ibDictionary.GetTotalCount(new FolkType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    else
                        EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region FolkType 字典表_增加
        public BaseResultDataValue AddFolkTypeModel(Model.FolkType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBFolkType ibDictionary = BLLFactory<IBFolkType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "FolkType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "FolkType 增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("FolkType 增加请求失败！" + ex.ToString() + ex.StackTrace);
            }

            return brdv;
        }
        #endregion

        #region FolkType 字典表_修改
        public BaseResult UpdateFolkTypeModelByID(Model.FolkType jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBFolkType ibDictionary = BLLFactory<IBFolkType>.GetBLL();
                Model.FolkType m_pgp = new FolkType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "FolkType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "FolkType 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error("FolkType 修改请求失败！" + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region FolkType 字典表_删除
        public BaseResult DeleteFolkTypeModelByID(string FolkNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBFolkType ibDictionary = BLLFactory<IBFolkType>.GetBLL();
                int tempClinetNo = int.Parse(FolkNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBFolkTypeControl ibdictionarycontrol = BLLFactory<IBFolkTypeControl>.GetBLL();
                Model.FolkTypeControl modeldistrict = new FolkTypeControl();
                modeldistrict.FolkNo = tempClinetNo;
                modeldistrict.ControlFolkNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + FolkNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "FolkNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;


        }
        #endregion
        #endregion

        #region SuperGroup 中心检验大组字典
        #region SuperGroup 字典表_查询
        public BaseResultDataValue GetSuperGroupModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSuperGroup ibDictionary = BLLFactory<IBSuperGroup>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SuperGroup> EntityLis = new EntityListEasyUI<Model.SuperGroup>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new SuperGroup { SearchLikeKey = itemkey, LabCode = labcode, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(labcode))
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        //if(!string.IsNullOrEmpty(itemkey))                                      
                        EntityLis.total = ibDictionary.GetTotalCount(new SuperGroup { SearchLikeKey = itemkey, LabCode = labcode, OrderField = "DTimeStampe" });
                        //else
                        //EntityLis.total = ibDictionary.GetTotalCount();
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";

                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region SuperGroup 字典表_增加
        public BaseResultDataValue AddSuperGroupModel(Model.SuperGroup jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBSuperGroup ibDictionary = BLLFactory<IBSuperGroup>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "SuperGroup 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "SuperGroup 增加请求失败！";// + ex.ToString() + ex.StackTrace;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("SuperGroup 增加请求失败！" + ex.ToString() + ex.StackTrace);
            }

            return brdv;
        }
        #endregion

        #region SuperGroup 字典表_修改
        public BaseResult UpdateSuperGroupModelByID(Model.SuperGroup jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSuperGroup ibDictionary = BLLFactory<IBSuperGroup>.GetBLL();
                Model.SuperGroup m_pgp = new SuperGroup();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SuperGroup 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "SuperGroup 修改请求失败！" + ex.StackTrace + ex.ToString();
                br.success = false;
            }
            ZhiFang.Common.Log.Log.Error(br.ErrorInfo);
            return br;
        }
        #endregion

        #region SuperGroup 字典表_删除

        public BaseResult DeleteSuperGroupModelByID(string SuperGroupNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBSuperGroup ibDictionary = BLLFactory<IBSuperGroup>.GetBLL();
                int tempClinetNo = int.Parse(SuperGroupNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSuperGroupControl ibdictionarycontrol = BLLFactory<IBSuperGroupControl>.GetBLL();
                Model.SuperGroupControl modeldistrict = new SuperGroupControl();
                modeldistrict.SuperGroupNo = tempClinetNo;
                modeldistrict.ControlSuperGroupNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + SuperGroupNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "SuperGroupNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region GenderType 中心性别字典
        #region GenderType 字典表_查询
        public BaseResultDataValue GetGenderTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBGenderType ibDictionary = BLLFactory<IBGenderType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.GenderType> EntityLis = new EntityListEasyUI<Model.GenderType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new GenderType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    //if(!string.IsNullOrEmpty(itemkey))                                      
                    EntityLis.total = ibDictionary.GetTotalCount(new GenderType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    //else
                    //EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region GenderType 字典表_增加
        public BaseResultDataValue AddGenderTypeModel(Model.GenderType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBGenderType ibDictionary = BLLFactory<IBGenderType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "GenderType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "GenderType 增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("GenderType 增加请求失败！" + ex.ToString() + ex.StackTrace);
            }

            return brdv;
        }
        #endregion

        #region GenderType 字典表_修改
        public BaseResult UpdateGenderTypeModelByID(Model.GenderType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBGenderType ibDictionary = BLLFactory<IBGenderType>.GetBLL();
                Model.GenderType m_pgp = new GenderType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "GenderType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "GenderType 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error("GenderType 修改请求失败！" + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region GenderType 字典表_删除
        public BaseResult DeleteGenderTypeModelByID(string GenderNo)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBGenderType ibDictionary = BLLFactory<IBGenderType>.GetBLL();
                int tempClinetNo = int.Parse(GenderNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBGenderTypeControl ibdictionarycontrol = BLLFactory<IBGenderTypeControl>.GetBLL();
                Model.GenderTypeControl modeldistrict = new GenderTypeControl();
                modeldistrict.GenderNo = tempClinetNo;
                modeldistrict.ControlGenderNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + GenderNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "GenderNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region SickType 中心就诊类型字典
        #region SickType 字典表_查询
        public BaseResultDataValue GetSickTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSickType ibDictionary = BLLFactory<IBSickType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SickType> EntityLis = new EntityListEasyUI<Model.SickType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new SickType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new SickType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    brdv.ErrorInfo = "SickType 查询获取成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("SickType 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "SickType 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error("SickType 查询请求失败!" + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region SickType 字典表_增加
        public BaseResultDataValue AddSickTypeModel(Model.SickType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBSickType ibDictionary = BLLFactory<IBSickType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "SickType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "SickType 增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("SickType 增加请求失败！" + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region SickType 字典表_修改
        public BaseResult UpdateSickTypeModelByID(Model.SickType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSickType ibDictionary = BLLFactory<IBSickType>.GetBLL();
                Model.SickType m_pgp = new SickType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SickType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "SickType 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error("SickType 修改请求失败！" + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region SickType 字典表_删除
        public BaseResult DeleteSickTypeModelByID(string SickTypeNo)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSickType ibDictionary = BLLFactory<IBSickType>.GetBLL();
                int tempClinetNo = int.Parse(SickTypeNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSickTypeControl ibdictionarycontrol = BLLFactory<IBSickTypeControl>.GetBLL();
                Model.SickTypeControl modeldistrict = new SickTypeControl();
                modeldistrict.SickTypeNo = tempClinetNo;
                modeldistrict.ControlSickTypeNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + SickTypeNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempClinetNo))
                {
                    if (ibDictionary.Delete(tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "SickTypeNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region WardType 中心病房字典
        #region WardType 字典表_查询
        public BaseResultDataValue GetWardTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBWardType ibDictionary = BLLFactory<IBWardType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.WardType> EntityLis = new EntityListEasyUI<Model.WardType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new WardType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new WardType { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("WardType 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "WardType 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error("WardType 查询请求失败!" + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region WardType 字典表_增加
        public BaseResultDataValue AddWardTypeModel(Model.WardType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBWardType ibDictionary = BLLFactory<IBWardType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "WardType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "WardType增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("WardType增加请求失败！" + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region WardType 字典表_修改
        public BaseResult UpdateWardTypeModelByID(Model.WardType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBWardType ibDictionary = BLLFactory<IBWardType>.GetBLL();
                Model.WardType m_pgp = new WardType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "WardType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "WardType 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error("WardType 修改请求失败！" + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region WardType 字典表_删除
        public BaseResult DeleteWardTypeModelByID(string DistrictNo, string WardNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBWardType ibDictionary = BLLFactory<IBWardType>.GetBLL();
                int tempClinetNo = int.Parse(DistrictNo);
                int wardNo = int.Parse(WardNo);



                if (ibDictionary.Exists(tempClinetNo, wardNo))
                {
                    if (ibDictionary.Delete(tempClinetNo, wardNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "DistrictNo/WardNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region Department 中心科室字典
        #region Department 字典表_查询
        public BaseResultDataValue GetDepartmentModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDepartment ibDictionary = BLLFactory<IBDepartment>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Department> EntityLis = new EntityListEasyUI<Model.Department>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Department { SearchLikeKey = itemkey, OrderField = "DTimeStampe" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Department { SearchLikeKey = itemkey, OrderField = "DTimeStampe" });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("Department 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("Department 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Department 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region Department 字典表_增加
        public BaseResultDataValue AddDepartmentModel(Model.Department jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBDepartment ibDictionary = BLLFactory<IBDepartment>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "Department 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "Department增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("Department增加请求失败！" + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region Department 字典表_修改
        public BaseResult UpdateDepartmentModelByID(Model.Department jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDepartment ibDictionary = BLLFactory<IBDepartment>.GetBLL();
                Model.Department m_pgp = new Department();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "Department 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "Department 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error("Department 修改请求失败！" + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region Department 字典表_删除
        public BaseResult DeleteDepartmentModelByID(string DeptNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBDepartment ibDictionary = BLLFactory<IBDepartment>.GetBLL();
                int tempDeptNo = int.Parse(DeptNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDepartmentControl ibdictionarycontrol = BLLFactory<IBDepartmentControl>.GetBLL();
                Model.DepartmentControl modeldistrict = new DepartmentControl();
                modeldistrict.DeptNo = tempDeptNo;
                modeldistrict.ControlDeptNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + DeptNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(tempDeptNo))
                {
                    if (ibDictionary.Delete(tempDeptNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "DeptNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region GroupItem 中心组套字典
        #region GroupItem 字典表_查询


        #region 循环
        public DataTable ReturnGroupItemDataTable(DataSet ds)
        {
            int total = ds.Tables[0].Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemNoName", typeof(string));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow row = dt.NewRow();
                row["ItemNoName"] = dr["ItemNoName"];
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion



        public BaseResultDataValue GetGroupItemModelManage(string itemkey, string itemno, string selectedflag)
        {
            IBLL.Common.BaseDictionary.IBTestItem ibti = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBTestItem>.GetBLL("BaseDictionary.TestItem");
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.TestItem> EntityLis = new EntityListEasyUI<Model.TestItem>();
            try
            {
                DataSet ds = new DataSet();
                if (itemno != "" && itemno != null)
                    GetParentItemNo(itemno);
                Model.TestItem testitemModel = new Model.TestItem();
                testitemModel.ItemNo = itemno;
                testitemModel.SearchKey = itemkey;
                ds = ibti.GetList(testitemModel, selectedflag);

                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                brdv.ResultDataFormatType = "json";
                if (dbtype == "default")
                {
                    brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                    BaseResultDataSet brds = new BaseResultDataSet();
                    brds.total = ds.Tables[0].Rows.Count;
                    //brds.rows = ds.Tables[0];
                    brds.rows = ReturnGroupItemDataTable(ds);
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                }
                else
                {
                    EntityLis.rows = ibti.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ds.Tables[0].Rows.Count;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                brdv.success = true;
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GroupItem 查询请求失败!"; // + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region GroupItem 字典表_增加
        public BaseResultDataValue AddGroupItemModel(Model.GroupItem jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                #region 验证此项目在其它组套中已存在

                List<TestItemDetail> ttdList = new List<TestItemDetail>();
                GetSubLabItem(jsonentity.ItemNo, null, ref ttdList);

                if (ttdList != null && ttdList.Find(p => p.ItemNo == jsonentity.ItemNo) != null)
                {

                    brdv.success = false;
                    brdv.ErrorInfo = "此项目在其它组套中已存在!";
                    return brdv;
                }
                #endregion
                IBLL.Common.BaseDictionary.IBGroupItem ibDictionary = BLLFactory<IBGroupItem>.GetBLL();

                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "GroupItem 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "GroupItem 增加请求失败！"; // + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("GroupItem 增加请求失败！" + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region GroupItem 字典表_修改
        public BaseResult UpdateGroupItemModelByID(Model.UiModel.GroupItemEntity jsonentity)
        {
            BaseResult br = new BaseResult();
            IBLL.Common.BaseDictionary.IBGroupItem ibDictionary = BLLFactory<IBGroupItem>.GetBLL();
            try
            {
                #region 验证必填
                try
                {
                    if (string.IsNullOrEmpty(jsonentity.itemno))
                    {
                        br.success = false;
                        br.ErrorInfo = "GroupItem 验证失败！itemno 为空：" + jsonentity.itemno;
                        return br;
                    }
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "GroupItem 字典表_修改 验证失败！当前已选：" + jsonentity.itemno + ex.ToString();
                    return br;
                }


                #region 验证此项目在其它组套中已存在
                List<TestItemDetail> ttdList = new List<TestItemDetail>();
                for (int i = 0; i < jsonentity.itemnolist.Count; i++)
                {
                    GetSubLabItem(jsonentity.itemnolist[i], null, ref ttdList);
                    if (ttdList != null && ttdList.Find(p => p.ItemNo == jsonentity.itemno) != null)
                    {
                        br.success = false;
                        br.ErrorInfo = jsonentity.itemnolist[i] + "此项目已选中当前项目为组套细项，不能继续添加!";
                        return br;
                    }
                }
                #endregion

                #endregion

                #region 删除组套项目
                try
                {
                    Model.GroupItem groupitemModel = new Model.GroupItem();
                    groupitemModel.PItemNo = jsonentity.itemno;
                    groupitemModel.ItemNo = null;
                    ibDictionary.Delete(groupitemModel, "");
                    br.success = true;
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = " GroupItem 字典表_修改 删除失败！：";// + ex.ToString() + ex.StackTrace;
                    ZhiFang.Common.Log.Log.Debug(" GroupItem 字典表_修改 删除失败！：" + ex.ToString() + ex.StackTrace);
                }
                #endregion
                #region 已选组套
                try
                {
                    if (jsonentity.itemnolist.Count > 0)
                    {
                        for (int i = 0; i < jsonentity.itemnolist.Count(); i++)
                        {
                            Model.GroupItem groupitemModel = new Model.GroupItem();
                            groupitemModel.PItemNo = jsonentity.itemno;
                            groupitemModel.ItemNo = jsonentity.itemnolist[i];
                            ibDictionary.Add(groupitemModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "GroupItem 字典表_修改 请求失败！:";// + ex.StackTrace + ex.ToString();
                    ZhiFang.Common.Log.Log.Debug("GroupItem 字典表_修改 请求失败！:" + ex.StackTrace + ex.ToString());
                    return br;
                }
                #endregion
            }
            catch (Exception ex)
            {
                br.success = false;
                br.ErrorInfo = "GroupItem 字典表_修改 请求失败！";// + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(" GroupItem 字典表_修改 请求失败！" + ex.ToString());
            }

            return br;
        }
        #endregion

        #region GroupItem 字典表_删除
        public BaseResult DeleteGroupItemModelByID(string PItemNo, string ItemNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBGroupItem ibDictionary = BLLFactory<IBGroupItem>.GetBLL();
                //int tempClinetNo = int.Parse(PItemNo);
                //int itemNo = int.Parse(ItemNo);
                if (ibDictionary.Exists(PItemNo, ItemNo))
                {
                    if (ibDictionary.Delete(PItemNo, ItemNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "clinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion
        string strPItemNos = "";
        #region 获取中心当前项目编码的所有上级，赋值给变量strPItemNos 以逗号分隔
        /// <summary>
        /// 获取当前项目编码的所有上级，赋值给变量strPItemNos 以逗号分隔
        /// </summary>
        /// <param name="ItemNo">项目编码</param>
        public void GetParentItemNo(string ItemNo)
        {
            IBLL.Common.BaseDictionary.IBGroupItem ibgi = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBGroupItem>.GetBLL("BaseDictionary.GroupItem");
            Model.GroupItem groupitemModel = new Model.GroupItem();
            groupitemModel.ItemNo = ItemNo.Trim();
            DataSet ds = ibgi.GetList(groupitemModel);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (strPItemNos.Length > 0)
                    {
                        strPItemNos += "," + ds.Tables[0].Rows[i]["PItemNo"].ToString();
                    }
                    else
                    {
                        strPItemNos = ds.Tables[0].Rows[i]["PItemNo"].ToString();
                    }
                    GetParentItemNo(ds.Tables[0].Rows[i]["PItemNo"].ToString());
                }
            }

        }
        #endregion


        #region ItemColorDict 项目颜色字典

        #region 查询
        public BaseResultDataValue GetAllItemColorDict()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.ItemColorDict> EntityLis = new EntityListEasyUI<Model.ItemColorDict>();
            try
            {

                IBLL.Common.BaseDictionary.IBItemColorDict ibDictionary = BLLFactory<IBItemColorDict>.GetBLL();

                DataSet ds = ibDictionary.GetAllList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = EntityLis.rows.Count;
                    brdv.success = true;

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    //ZhiFang.Common.Log.Log.Info("Department 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }

            }
            catch (Exception ex)
            {

                brdv.success = false;
                brdv.ErrorInfo = "ItemColorDict 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
            return brdv;
        }
        #endregion

        #region 增加
        public BaseResultDataValue AddItemColorDict(Model.ItemColorDict jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBItemColorDict ibDictionary = BLLFactory<IBItemColorDict>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{ColorID:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "ItemColorDict 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "ItemColorDict增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("ItemColorDict增加请求失败！" + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion


        #region 修改
        public BaseResult UpdateItemColorDictByID(Model.ItemColorDict jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBItemColorDict ibDictionary = BLLFactory<IBItemColorDict>.GetBLL();
                Model.ItemColorDict m_pgp = new ItemColorDict();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "ItemColorDict 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "ItemColorDict 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            //
            return br;
        }
        #endregion


        #region 删除
        public BaseResult DeleteItemColorDictByID(string ColorId)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBItemColorDict ibDictionary = BLLFactory<IBItemColorDict>.GetBLL();
                int tempColorId = int.Parse(ColorId);
                if (ibDictionary.Exists(tempColorId))
                {
                    if (ibDictionary.Delete(tempColorId) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "ColorId不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region BPhysicalExamType 体检类型字典

        #region 查询
        public BaseResultDataValue GetBPhysicalExamTypeModelManage(string itemkey, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBBPhysicalExamType ibDictionary = BLLFactory<IBBPhysicalExamType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.BPhysicalExamType> EntityLis = new EntityListEasyUI<Model.BPhysicalExamType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new BPhysicalExamType { SearchLikeKey = itemkey, OrderField = "DispOrder" }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    if (!string.IsNullOrEmpty(itemkey))
                        EntityLis.total = ibDictionary.GetTotalCount(new BPhysicalExamType { SearchLikeKey = itemkey, OrderField = "DispOrder" });
                    else
                        EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        public BaseResultDataValue GetAllBPhysicalExamType()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.BPhysicalExamType> EntityLis = new EntityListEasyUI<Model.BPhysicalExamType>();
            try
            {

                IBLL.Common.BaseDictionary.IBBPhysicalExamType ibDictionary = BLLFactory<IBBPhysicalExamType>.GetBLL();

                DataSet ds = ibDictionary.GetAllList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = EntityLis.rows.Count;
                    brdv.success = true;

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    //ZhiFang.Common.Log.Log.Info("Department 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }

            }
            catch (Exception ex)
            {

                brdv.success = false;
                brdv.ErrorInfo = "BPhysicalExamType 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
            return brdv;
        }
        #endregion

        #region 增加
        public BaseResultDataValue AddBPhysicalExamType(Model.BPhysicalExamType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBBPhysicalExamType ibDictionary = BLLFactory<IBBPhysicalExamType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{Id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "BPhysicalExamType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "BPhysicalExamType增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("BPhysicalExamType增加请求失败！" + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion


        #region 修改
        public BaseResult UpdateBPhysicalExamTypeByID(Model.BPhysicalExamType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBBPhysicalExamType ibDictionary = BLLFactory<IBBPhysicalExamType>.GetBLL();
                Model.BPhysicalExamType m_pgp = new BPhysicalExamType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "BPhysicalExamType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "BPhysicalExamType 修改请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            //
            return br;
        }
        #endregion


        #region 删除
        public BaseResult DeleteBPhysicalExamTypeByID(string Id)
        {
            BaseResult br = new BaseResult();
            if (string.IsNullOrEmpty(Id))
            {
                br.ErrorInfo = "Id为空!";
                br.success = false;
                return br;
            }
            try
            {

                IBLL.Common.BaseDictionary.IBBPhysicalExamType ibDictionary = BLLFactory<IBBPhysicalExamType>.GetBLL();
                long tempId = long.Parse(Id);
                if (ibDictionary.Exists(tempId))
                {
                    if (ibDictionary.Delete(tempId) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "Id不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion
        #endregion

        #region 实验室字典服务

        #region B_Lab_CLIENTELE 实验室医疗机构字典

        #region B_Lab_CLIENTELE 字典表_查询
        public BaseResultDataValue GetLabCLIENTELEModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_CLIENTELE ibDictionary = BLLFactory<IBLab_CLIENTELE>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_CLIENTELE> EntityLis = new EntityListEasyUI<Model.Lab_CLIENTELE>();
            BaseResultDataSet brds = new BaseResultDataSet();
            try
            {
                if (page > 0)
                    page = page - 1;
                int blabclientno = -1;
                int outi = 0;
                bool b = int.TryParse(itemkey, out outi);
                if (b)
                    blabclientno = int.Parse(itemkey.Trim());

                DataSet ds = ibDictionary.GetListByPage(new Lab_CLIENTELE
                {
                    SHORTCODE = itemkey,
                    CNAME = itemkey,
                    LabClIENTNO = blabclientno,
                    LabCode = labcode,
                    ISUSE = -1,
                    UploadType = -1,
                    InputDataType = -1,
                    ReportPageType = -1,
                    IsPrintItem = -1
                }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBCLIENTELEControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBCLIENTELEControl>.GetBLL("BaseDictionary.CLIENTELEControl");
                        Model.CLIENTELEControl controlModel = new Model.CLIENTELEControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlClIENTNO = int.Parse(ds.Tables[0].Rows[i]["LabClIENTNO"].ToString().Trim());
                        controlModel.ClIENTNO = 0;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    brds.rows = ds.Tables[0];
                    brds.total = ibDictionary.GetTotalCount(new Lab_CLIENTELE { SHORTCODE = itemkey, CNAME = itemkey, LabCode = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("B_Lab_CLIENTELE 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_Lab_CLIENTELE 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_CLIENTELE 查询请求失败!";// + ex.StackTrace + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_CLIENTELE 字典表_增加
        public BaseResultDataValue AddLabCLIENTELEModel(Model.Lab_CLIENTELE jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_CLIENTELE ibDictionary = BLLFactory<IBLab_CLIENTELE>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_CLIENTELE 增加请求失败,增加失败！";
                    brdv.success = true;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_CLIENTELE 增加请求失败！";// + ex.StackTrace + ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_CLIENTELE 字典表_修改
        public BaseResult UpdateLabCLIENTELEModelByID(Model.Lab_CLIENTELE jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_CLIENTELE ibDictionary = BLLFactory<IBLab_CLIENTELE>.GetBLL();
                Model.Lab_CLIENTELE m_pgp = new Lab_CLIENTELE();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_CLIENTELE 修改请求失败,增加失败！";
                    br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_CLIENTELE 修改请求失败！"; // + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_CLIENTELE 字典表_删除
        public BaseResult DeleteLabCLIENTELEModelByID(string labCode, string labClientNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_CLIENTELE ibDictionary = BLLFactory<IBLab_CLIENTELE>.GetBLL();
                int tempClinetNo = int.Parse(labClientNo);



                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "clinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_Lab_TestItem 实验室项目字典

        #region B_Lab_TestItem 字典表_查询
        public BaseResultDataValue GetLabTestItemModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_TestItem> EntityLis = new EntityListEasyUI<Model.Lab_TestItem>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_TestItem { LabCode = labcode, TestItemLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBTestItemControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
                        Model.TestItemControl controlModel = new Model.TestItemControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlItemNo = ds.Tables[0].Rows[i]["LabItemNo"].ToString().Trim();

                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }

                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    //if (!string.IsNullOrEmpty(itemkey))
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_TestItem { LabCode = labcode, TestItemLikeKey = itemkey });
                    //else
                    //    EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        public BaseResultDataValue GetLabTestItemModelManage(string itemkey, string labcode, int page, int rows, string sort, string order)
        {
            IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_TestItem> EntityLis = new EntityListEasyUI<Model.Lab_TestItem>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_TestItem { LabCode = labcode, TestItemLikeKey = itemkey }, page, rows, sort, order);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBTestItemControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
                        Model.TestItemControl controlModel = new Model.TestItemControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlItemNo = ds.Tables[0].Rows[i]["LabItemNo"].ToString().Trim();

                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }

                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    //if (!string.IsNullOrEmpty(itemkey))
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_TestItem { LabCode = labcode, TestItemLikeKey = itemkey });
                    //else
                    //    EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region B_Lab_TestItem 字典表_增加
        public BaseResultDataValue AddLabTestItemModel(Model.Lab_TestItem jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_TestItem 字典表_增加 请求失败,增加失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_TestItem 字典表_增加 请求失败！";// + ex.ToString() + ex.StackTrace;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("B_Lab_TestItem 字典表_增加 请求失败！" + ex.ToString() + ex.StackTrace);
            }

            return brdv;
        }
        #endregion

        #region B_Lab_TestItem 字典表_修改
        public BaseResult UpdateLabTestItemModelByID(Model.Lab_TestItem jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
                Model.Lab_TestItem m_pgp = new Lab_TestItem();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_TestItem 字典表_修改 请求失败,增加失败！";
                    br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_TestItem 字典表_修改 请求失败！";// + ex.StackTrace + ex.ToString();
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }

        //修改项目颜色 ganwh add 2015-12-14
        public BaseResult UpdateLabTestItemColorByID(Model.Lab_TestItem jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();

                if (ibDictionary.UpdateColor(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_TestItem 字典表_修改 请求失败,增加失败！";
                    br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_TestItem 字典表_修改 请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_TestItem 字典表_删除
        public BaseResult DeleteLabTestItemModelByID(string labCode, string labItemNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
                //int tempClinetNo = int.Parse(SectionNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBTestItemControl ibdictionarycontrol = BLLFactory<IBTestItemControl>.GetBLL();
                Model.TestItemControl modeldistrict = new TestItemControl();
                modeldistrict.ControlItemNo = labItemNo;
                modeldistrict.ControlLabNo = labCode;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labItemNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                #region 删除组套项目
                IBLL.Common.BaseDictionary.IBLab_GroupItem ibLab_GroupItem = BLLFactory<IBLab_GroupItem>.GetBLL();
                try
                {
                    Model.Lab_GroupItem labgroupitemModel = new Model.Lab_GroupItem();
                    labgroupitemModel.PItemNo = labItemNo;
                    labgroupitemModel.LabCode = labCode;
                    labgroupitemModel.ItemNo = null;
                    ibLab_GroupItem.Delete(labgroupitemModel, "");
                    br.success = true;
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = " B_Lab_GroupItem 字典表_修改 删除失败！：";// + ex.ToString() + ex.StackTrace;
                    ZhiFang.Common.Log.Log.Error(" B_Lab_GroupItem 字典表_修改 删除失败！：" + ex.ToString() + ex.StackTrace);
                }
                #endregion

                if (ibDictionary.Exists(labCode, labItemNo))
                {
                    if (ibDictionary.Delete(labCode, labItemNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode/labItemNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_Lab_FolkType 实验室民族字典

        #region B_Lab_FolkType 字典表_查询
        public BaseResultDataValue GetLabFolkTypeModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_FolkType ibDictionary = BLLFactory<IBLab_FolkType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_FolkType> EntityLis = new EntityListEasyUI<Model.Lab_FolkType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_FolkType { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBFolkTypeControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBFolkTypeControl>.GetBLL("BaseDictionary.FolkTypeControl");
                        Model.FolkTypeControl controlModel = new Model.FolkTypeControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlFolkNo = int.Parse(ds.Tables[0].Rows[i]["LabFolkNo"].ToString().Trim());
                        controlModel.FolkNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_FolkType { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region B_Lab_FolkType 字典表_增加
        public BaseResultDataValue AddLabFolkTypeModel(Model.Lab_FolkType jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_FolkType ibDictionary = BLLFactory<IBLab_FolkType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_FolkType 字典表_增加 请求失败,增加失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_FolkType 字典表_增加 请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_FolkType 字典表_修改
        public BaseResult UpdateLabFolkTypeModelByID(Model.Lab_FolkType jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_FolkType ibDictionary = BLLFactory<IBLab_FolkType>.GetBLL();
                Model.Lab_FolkType m_pgp = new Lab_FolkType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_FolkType 字典表_修改 请求失败,增加失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_FolkType 字典表_修改 请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_FolkType 字典表_删除

        public BaseResult DeleteLabFolkTypeModelByID(string labCode, string labClientNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_FolkType ibDictionary = BLLFactory<IBLab_FolkType>.GetBLL();
                int tempClinetNo = int.Parse(labClientNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBFolkTypeControl ibdictionarycontrol = BLLFactory<IBFolkTypeControl>.GetBLL();
                Model.FolkTypeControl modeldistrict = new FolkTypeControl();
                modeldistrict.ControlFolkNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.FolkNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labClientNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_Lab_Doctor 实验室医生字典

        #region B_Lab_Doctor 字典表_查询
        public BaseResultDataValue GetLabDoctorModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_Doctor ibDictionary = BLLFactory<IBLab_Doctor>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_Doctor> EntityLis = new EntityListEasyUI<Model.Lab_Doctor>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_Doctor { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBDoctorControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBDoctorControl>.GetBLL("BaseDictionary.DoctorControl");

                        Model.DoctorControl controlModel = new Model.DoctorControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlDoctorNo = int.Parse(ds.Tables[0].Rows[i]["LabDoctorNo"].ToString().Trim());
                        controlModel.DoctorNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_Doctor { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region B_Lab_Doctor 字典表_增加
        public BaseResultDataValue AddLabDoctorModel(Model.Lab_Doctor jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_Doctor ibDictionary = BLLFactory<IBLab_Doctor>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_Doctor 字典表_增加 请求失败,增加失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_Doctor 字典表_增加 请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_Doctor 字典表_修改
        public BaseResult UpdateLabDoctorModelByID(Model.Lab_Doctor jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_Doctor ibDictionary = BLLFactory<IBLab_Doctor>.GetBLL();
                Model.Lab_Doctor m_pgp = new Lab_Doctor();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_Doctor 字典表_修改 请求失败,增加失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_Doctor 字典表_修改 请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_Doctor 字典表_删除
        public BaseResult DeleteLabDoctorModelByID(string labCode, string labDoctorNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_Doctor ibDictionary = BLLFactory<IBLab_Doctor>.GetBLL();
                int tempClinetNo = int.Parse(labDoctorNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDoctorControl ibdictionarycontrol = BLLFactory<IBDoctorControl>.GetBLL();
                Model.DoctorControl modeldoctor = new DoctorControl();
                modeldoctor.ControlDoctorNo = tempClinetNo;
                modeldoctor.ControlLabNo = labCode;
                modeldoctor.DoctorNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldoctor);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labDoctorNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode,tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_Lab_SampleType 实验室样本类型字典

        #region B_Lab_SampleType 字典表_查询
        public BaseResultDataValue GetLabSampleTypeModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_SampleType ibDictionary = BLLFactory<IBLab_SampleType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_SampleType> EntityLis = new EntityListEasyUI<Model.Lab_SampleType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_SampleType { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBSampleTypeControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
                        Model.SampleTypeControl controlModel = new Model.SampleTypeControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlSampleTypeNo = ds.Tables[0].Rows[i]["LabSampleTypeNo"].ToString().Trim();
                        controlModel.SampleTypeNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    //if (!string.IsNullOrEmpty(itemkey))
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_SampleType { LabCode = labcode, SearchLikeKey = itemkey });
                    //else
                    //    EntityLis.total = ibDictionary.GetTotalCount();
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region B_Lab_SampleType 字典表_增加
        public BaseResultDataValue AddLabSampleTypeModel(Model.Lab_SampleType jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SampleType ibDictionary = BLLFactory<IBLab_SampleType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_SampleType 字典表_增加 请求失败,增加失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_SampleType 字典表_增加 请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_SampleType 字典表_修改
        public BaseResult UpdateLabSampleTypeModelByID(Model.Lab_SampleType jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SampleType ibDictionary = BLLFactory<IBLab_SampleType>.GetBLL();
                Model.Lab_SampleType m_pgp = new Lab_SampleType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_SampleType 字典表_修改请求失败,增加失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_SampleType 字典表_修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_SampleType 字典表_删除
        public BaseResult DeleteLabSampleTypeModelByID(string labCode, string labSampleTypeNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_SampleType ibDictionary = BLLFactory<IBLab_SampleType>.GetBLL();
                int tempClinetNo = int.Parse(labSampleTypeNo.ToString());

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSampleTypeControl ibdictionarycontrol = BLLFactory<IBSampleTypeControl>.GetBLL();
                Model.SampleTypeControl modeldistrict = new SampleTypeControl();
                modeldistrict.ControlSampleTypeNo = tempClinetNo.ToString();
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.SampleTypeNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labSampleTypeNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }

        #endregion
        #endregion

        #region B_Lab_SickType 实验室就诊类型字典

        #region B_Lab_SickType 字典表_查询
        public BaseResultDataValue GetLabSickTypeModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_SickType ibDictionary = BLLFactory<IBLab_SickType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_SickType> EntityLis = new EntityListEasyUI<Model.Lab_SickType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_SickType { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBSickTypeControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBSickTypeControl>.GetBLL("BaseDictionary.SickTypeControl");
                        Model.SickTypeControl controlModel = new Model.SickTypeControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlSickTypeNo = int.Parse(ds.Tables[0].Rows[i]["LabSickTypeNo"].ToString().Trim());
                        controlModel.SickTypeNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_SickType { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("请求失败,字典无数据！");
                    brdv.success = true;
                    brdv.ErrorInfo = "请求成功!";
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "请求失败!";// + ex.ToString();
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(ex.Message + "--" + ex.ToString() + "--" + ex.StackTrace + "--" + ex.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return brdv;
            }
        }
        #endregion

        #region B_Lab_SickType 字典表_增加
        public BaseResultDataValue AddLabSickTypeModel(Model.Lab_SickType jsonentity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SickType ibDictionary = BLLFactory<IBLab_SickType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_SickType 字典表_增加 请求失败,增加失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_SickType 字典表_增加 请求失败！";// ;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_SickType 字典表_修改
        public BaseResult UpdateLabSickTypeModelByID(Model.Lab_SickType jsonentity)
        {

            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SickType ibDictionary = BLLFactory<IBLab_SickType>.GetBLL();
                Model.Lab_SickType m_pgp = new Lab_SickType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_SickType 字典表_修改 请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_SickType 字典表_修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion


        #region B_Lab_SickType 字典表_删除

        public BaseResult DeleteLabSickTypeModelByID(string labCode, string labSickTypeNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_SickType ibDictionary = BLLFactory<IBLab_SickType>.GetBLL();
                int tempClinetNo = int.Parse(labSickTypeNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSickTypeControl ibdictionarycontrol = BLLFactory<IBSickTypeControl>.GetBLL();
                Model.SickTypeControl modeldistrict = new SickTypeControl();
                modeldistrict.ControlSickTypeNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.SickTypeNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labSickTypeNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_Lab_SuperGroup 实验室检验大组字典

        #region B_Lab_SuperGroup 字典表_查询
        public BaseResultDataValue GetLabSuperGroupModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_SuperGroup ibDictionary = BLLFactory<IBLab_SuperGroup>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_SuperGroup> EntityLis = new EntityListEasyUI<Model.Lab_SuperGroup>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_SuperGroup { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBSuperGroupControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBSuperGroupControl>.GetBLL("BaseDictionary.SuperGroupControl");
                        Model.SuperGroupControl controlModel = new Model.SuperGroupControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlSuperGroupNo = int.Parse(ds.Tables[0].Rows[i]["LabSuperGroupNo"].ToString().Trim());
                        controlModel.SuperGroupNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_SuperGroup { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("B_Lab_SuperGroup 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_Lab_SuperGroup 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_SuperGroup 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_SuperGroup 字典表_增加
        public BaseResultDataValue AddLabSuperGroupModel(Model.Lab_SuperGroup jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SuperGroup ibDictionary = BLLFactory<IBLab_SuperGroup>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_SuperGroup 增加请求失败！";
                    ZhiFang.Common.Log.Log.Info(brdv.ErrorInfo);
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_SuperGroup 增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_SuperGroup 字典表_修改
        public BaseResult UpdateLabSuperGroupModelByID(Model.Lab_SuperGroup jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_SuperGroup ibDictionary = BLLFactory<IBLab_SuperGroup>.GetBLL();
                Model.Lab_SuperGroup m_pgp = new Lab_SuperGroup();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_SuperGroup 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_SuperGroup 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_SuperGroup 字典表_删除

        public BaseResult DeleteLabSuperGroupModelByID(string labCode, string labSuperGroupNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_SuperGroup ibDictionary = BLLFactory<IBLab_SuperGroup>.GetBLL();
                int tempClinetNo = int.Parse(labSuperGroupNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBSuperGroupControl ibdictionarycontrol = BLLFactory<IBSuperGroupControl>.GetBLL();
                Model.SuperGroupControl modeldistrict = new SuperGroupControl();
                modeldistrict.ControlSuperGroupNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.SuperGroupNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labSuperGroupNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_Lab_District 实验室病区字典

        #region B_Lab_District 字典表_查询
        public BaseResultDataValue GetLabDistrictModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_District ibDictionary = BLLFactory<IBLab_District>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_District> EntityLis = new EntityListEasyUI<Model.Lab_District>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_District { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBDistrictControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBDistrictControl>.GetBLL("BaseDictionary.DistrictControl");
                        Model.DistrictControl controlModel = new Model.DistrictControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlDistrictNo = int.Parse(ds.Tables[0].Rows[i]["LabDistrictNo"].ToString().Trim());
                        controlModel.DistrictNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_District { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("B_Lab_District 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_Lab_District 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_District 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_District 字典表_增加
        public BaseResultDataValue AddLabDistrictModel(Model.Lab_District jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_District ibDictionary = BLLFactory<IBLab_District>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_District 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_District增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_District 字典表_修改
        public BaseResult UpdateLabDistrictModelByID(Model.Lab_District jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_District ibDictionary = BLLFactory<IBLab_District>.GetBLL();
                Model.Lab_District m_pgp = new Lab_District();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_District 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_District 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_District 字典表_删除
        public BaseResult DeleteLabDistrictModelByID(string labCode, string labDistrictNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_District ibDictionary = BLLFactory<IBLab_District>.GetBLL();
                int tempClinetNo = int.Parse(labDistrictNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDistrictControl ibdictionarycontrol = BLLFactory<IBDistrictControl>.GetBLL();
                Model.DistrictControl modeldistrict = new DistrictControl();
                modeldistrict.ControlDistrictNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.DistrictNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labDistrictNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_Lab_PGroup 实验室检验小组字典

        #region B_Lab_PGroup 字典表_查询
        public BaseResultDataValue GetLabPGroupModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_PGroup ibDictionary = BLLFactory<IBLab_PGroup>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_PGroup> EntityLis = new EntityListEasyUI<Model.Lab_PGroup>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_PGroup { LabCode = labcode, Searchlikekey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBPGroupControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBPGroupControl>.GetBLL("BaseDictionary.PGroupControl");
                        Model.PGroupControl controlModel = new Model.PGroupControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.SectionNo = -1;
                        controlModel.ControlSectionNo = int.Parse(ds.Tables[0].Rows[i]["LabSectionNo"].ToString().Trim());

                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_PGroup { LabCode = labcode, Searchlikekey = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("B_Lab_PGroup 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_Lab_PGroup 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_PGroup 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_PGroup 字典表_增加
        public BaseResultDataValue AddLabPGroupModel(Model.Lab_PGroup jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_PGroup ibDictionary = BLLFactory<IBLab_PGroup>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_PGroup 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_PGroup增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_PGroup 字典表_修改
        public BaseResult UpdateLabPGroupModelByID(Model.Lab_PGroup jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_PGroup ibDictionary = BLLFactory<IBLab_PGroup>.GetBLL();
                Model.Lab_PGroup m_pgp = new Lab_PGroup();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_PGroup 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_PGroup 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_PGroup 字典表_删除
        public BaseResult DeleteLabPGroupModelByID(string labCode, string labSectionNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_PGroup ibDictionary = BLLFactory<IBLab_PGroup>.GetBLL();
                int tempClinetNo = int.Parse(labSectionNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBPGroupControl ibdictionarycontrol = BLLFactory<IBPGroupControl>.GetBLL();
                Model.PGroupControl modeldistrict = new PGroupControl();
                modeldistrict.ControlSectionNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.SectionNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labSectionNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode, tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_Lab_Department 实验室科室字典

        #region B_Lab_Department 字典表_查询
        public BaseResultDataValue GetLabDepartmentModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_Department ibDictionary = BLLFactory<IBLab_Department>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_Department> EntityLis = new EntityListEasyUI<Model.Lab_Department>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_Department { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        IBLL.Common.BaseDictionary.IBDepartmentControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBDepartmentControl>.GetBLL("BaseDictionary.DepartmentControl");

                        Model.DepartmentControl controlModel = new Model.DepartmentControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlDeptNo = int.Parse(ds.Tables[0].Rows[i]["LabDeptNo"].ToString().Trim());
                        controlModel.DeptNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_Department { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("B_Lab_Department 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_Lab_Department 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_Department 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_Department 字典表_增加
        public BaseResultDataValue AddLabDepartmentModel(Model.Lab_Department jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_Department ibDictionary = BLLFactory<IBLab_Department>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_Department 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_Department增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_Department 字典表_修改
        public BaseResult UpdateLabDepartmentModelByID(Model.Lab_Department jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_Department ibDictionary = BLLFactory<IBLab_Department>.GetBLL();
                Model.Lab_Department m_pgp = new Lab_Department();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_Lab_Department 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_Lab_Department 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region B_Lab_Department 字典表_删除

        public BaseResult DeleteLabDepartmentModelByID(string labCode, string labDeptNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_Department ibDictionary = BLLFactory<IBLab_Department>.GetBLL();
                int tempClinetNo = int.Parse(labDeptNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBDepartmentControl ibdictionarycontrol = BLLFactory<IBDepartmentControl>.GetBLL();
                Model.DepartmentControl modeldistrict = new DepartmentControl();
                modeldistrict.ControlDeptNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.DeptNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labDeptNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode,tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region b_lab_GenderType 实验室性别字典

        #region b_lab_GenderType 字典表_查询
        public BaseResultDataValue GetLabGenderTypeModelManage(string itemkey, string labcode, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBLab_GenderType ibDictionary = BLLFactory<IBLab_GenderType>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_GenderType> EntityLis = new EntityListEasyUI<Model.Lab_GenderType>();
            try
            {
                if (page > 0)
                    page = page - 1;

                DataSet ds = ibDictionary.GetListByPage(new Lab_GenderType { LabCode = labcode, SearchLikeKey = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("ControlStatus");//对照状态：未对照/已对照
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        IBLL.Common.BaseDictionary.IBGenderTypeControl controlBLL = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
                        Model.GenderTypeControl controlModel = new Model.GenderTypeControl();
                        controlModel.ControlLabNo = labcode;
                        controlModel.ControlGenderNo = int.Parse(ds.Tables[0].Rows[i]["LabGenderNo"].ToString().Trim());
                        controlModel.GenderNo = -1;
                        DataSet dsControl = controlBLL.GetList(controlModel);
                        if (dsControl != null && dsControl.Tables.Count > 0 && dsControl.Tables[0].Rows.Count > 0)
                            ds.Tables[0].Rows[i]["ControlStatus"] = "已对照";
                        else
                            ds.Tables[0].Rows[i]["ControlStatus"] = "未对照";
                    }
                    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                    EntityLis.total = ibDictionary.GetTotalCount(new Lab_GenderType { LabCode = labcode, SearchLikeKey = itemkey });
                    brdv.success = true;
                    ZhiFang.Common.Log.Log.Info("b_lab_GenderType 查询请求成功!");
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("b_lab_GenderType 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "b_lab_GenderType 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region b_lab_GenderType 字典表_增加
        public BaseResultDataValue AddLabGenderTypeModel(Model.Lab_GenderType jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_GenderType ibDictionary = BLLFactory<IBLab_GenderType>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "b_lab_GenderType 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "b_lab_GenderType增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region b_lab_GenderType 字典表_修改
        public BaseResult UpdateLabGenderTypeModelByID(Model.Lab_GenderType jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_GenderType ibDictionary = BLLFactory<IBLab_GenderType>.GetBLL();
                Model.Lab_GenderType m_pgp = new Lab_GenderType();
                if (ibDictionary.Update(jsonentity) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "b_lab_GenderType 修改请求失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "b_lab_GenderType 修改请求失败！";
                br.success = false;
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return br;
        }
        #endregion

        #region b_lab_GenderType 字典表_删除

        public BaseResult DeleteLabGenderTypeModelByID(string labCode, string labGenderNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_GenderType ibDictionary = BLLFactory<IBLab_GenderType>.GetBLL();
                int tempClinetNo = int.Parse(labGenderNo);

                #region 是否存在对照关系
                IBLL.Common.BaseDictionary.IBGenderTypeControl ibdictionarycontrol = BLLFactory<IBGenderTypeControl>.GetBLL();
                Model.GenderTypeControl modeldistrict = new GenderTypeControl();
                modeldistrict.ControlGenderNo = tempClinetNo;
                modeldistrict.ControlLabNo = labCode;
                modeldistrict.GenderNo = -1;
                DataSet ds = ibdictionarycontrol.GetList(modeldistrict);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    br.ErrorInfo = "（" + labGenderNo + "）此项已对照,不能删除!";
                    br.success = false;
                    return br;
                }
                #endregion

                if (ibDictionary.Exists(labCode, tempClinetNo))
                {
                    if (ibDictionary.Delete(labCode, tempClinetNo) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "labCode,tempClinetNo不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_Lab_GroupItem 实验室组套字典

        #region B_Lab_GroupItem 字典表_查询

        #region 循环
        public DataTable ReturnDataTableLabGroupItem(DataSet ds)
        {
            int total = ds.Tables[0].Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemNoName", typeof(string));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow row = dt.NewRow();
                row["ItemNoName"] = dr["ItemNoName"];
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        public BaseResultDataValue GetLabGroupItemModelManage(string itemkey, string labcode, string itemno, string selectedflag)
        {
            IBLL.Common.BaseDictionary.IBLab_TestItem iblti = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBLab_TestItem>.GetBLL("BaseDictionary.Lab_TestItem");
            IBLL.Common.BaseDictionary.IBGroupItem ibDictionary = BLLFactory<IBGroupItem>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.Lab_TestItem> EntityLis = new EntityListEasyUI<Model.Lab_TestItem>();
            try
            {
                DataSet ds = new DataSet();
                if (itemno != "" && itemno != null)
                    GetParentItemNo(itemno, labcode);
                Model.Lab_TestItem labtestitemModel = new Model.Lab_TestItem();
                labtestitemModel.LabItemNo = itemno;
                labtestitemModel.SearchKey = itemkey;
                labtestitemModel.PItemNos = strPItemNos;
                labtestitemModel.LabCode = labcode.Trim();
                ds = iblti.GetList(labtestitemModel, selectedflag);
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                brdv.ResultDataFormatType = "json";
                if (dbtype == "default")
                {
                    //brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                    BaseResultDataSet brds = new BaseResultDataSet();
                    brds.total = ds.Tables[0].Rows.Count;
                    //brds.rows = ds.Tables[0];
                    brds.rows = ReturnDataTableLabGroupItem(ds);
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                }
                else
                {
                    EntityLis.rows = iblti.DataTableToList(ds.Tables[0]);
                    //EntityLis.rows = ReturnDataTableLabGroupItem(ds);
                    EntityLis.total = ds.Tables[0].Rows.Count;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                brdv.success = true;
                return brdv;

                //DataSet ds = ibDictionary.GetListByPage(new Lab_GroupItem { SearchLikeKey = itemkey }, page, rows);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                //    EntityLis.total = ibDictionary.GetTotalCount(new Lab_GroupItem { SearchLikeKey = itemkey });
                //    brdv.success = true;
                //    ZhiFang.Common.Log.Log.Info("B_Lab_GroupItem 查询请求成功!");
                //    brdv.ResultDataFormatType = "JSON";
                //    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                //}
                //else
                //{
                //    ZhiFang.Common.Log.Log.Info("B_Lab_GroupItem 查询请求成功,字典无数据!");
                //    brdv.success = true;
                //    brdv.ResultDataFormatType = "JSON";
                //    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                //}
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_Lab_GroupItem 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_Lab_GroupItem 字典表_增加
        public BaseResultDataValue AddLabGroupItemModel(Model.Lab_GroupItem jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBLab_GroupItem ibDictionary = BLLFactory<IBLab_GroupItem>.GetBLL();
                int id = ibDictionary.Add(jsonentity);
                if (id > 0)
                {
                    brdv.ResultDataValue = "{id:" + id + "}";
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "B_Lab_GroupItem 增加请求失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "B_Lab_GroupItem增加请求失败！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }

            return brdv;
        }
        #endregion

        #region B_Lab_GroupItem 字典表_修改
        public BaseResult UpdateLabGroupItemModelByID(Model.UiModel.GroupItemEntity jsonentity)
        {
            BaseResult br = new BaseResult();
            IBLL.Common.BaseDictionary.IBLab_GroupItem ibDictionary = BLLFactory<IBLab_GroupItem>.GetBLL();
            try
            {
                #region 验证必填
                try
                {
                    if (string.IsNullOrEmpty(jsonentity.itemno))
                    {
                        br.success = false;
                        br.ErrorInfo = "B_Lab_GroupItem 请求失败！itemno 为空：" + jsonentity.itemno;
                        return br;
                    }

                    #region 验证此项目在其它组套中已存在
                    List<TestItemDetail> ttdList = new List<TestItemDetail>();
                    for (int i = 0; i < jsonentity.itemnolist.Count; i++)
                    {
                        GetSubLabItem(jsonentity.itemnolist[i], jsonentity.labcode, ref ttdList);
                        if (ttdList != null && ttdList.Find(p => p.ItemNo == jsonentity.itemno) != null)
                        {
                            br.success = false;
                            br.ErrorInfo = jsonentity.itemnolist[i] + "此项目已选中当前项目为组套细项，不能继续添加!";
                            return br;
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "B_Lab_GroupItem 字典表_修改 请求失败！当前已选：" + jsonentity.itemno;
                    ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                    return br;
                }
                #endregion

                #region 删除组套项目
                try
                {
                    Model.Lab_GroupItem labgroupitemModel = new Model.Lab_GroupItem();
                    labgroupitemModel.PItemNo = jsonentity.itemno;
                    labgroupitemModel.LabCode = jsonentity.labcode;
                    labgroupitemModel.ItemNo = null;
                    ibDictionary.Delete(labgroupitemModel, "");
                    br.success = true;
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = " B_Lab_GroupItem 字典表_修改 删除失败！：";
                    ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString() + ex.StackTrace);
                }
                #endregion


                #region 已选组套

                try
                {
                    if (jsonentity.itemnolist.Count > 0)
                    {
                        for (int i = 0; i < jsonentity.itemnolist.Count(); i++)
                        {
                            Model.Lab_GroupItem labgroupitemModel = new Model.Lab_GroupItem();
                            labgroupitemModel.PItemNo = jsonentity.itemno;
                            labgroupitemModel.ItemNo = jsonentity.itemnolist[i];
                            labgroupitemModel.LabCode = jsonentity.labcode;
                            ibDictionary.Add(labgroupitemModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    br.success = false;
                    br.ErrorInfo = "B_Lab_GroupItem 字典表_修改 请求失败！:";// + ex.StackTrace + ex.ToString();
                    ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                    return br;
                }
                #endregion
                ZhiFang.Common.Log.Log.Debug(br.ErrorInfo);
            }
            catch (Exception ex)
            {
                br.success = false;
                br.ErrorInfo = "B_Lab_GroupItem 字典表_修改 请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
            }
            return br;
        }
        #endregion


        #region B_Lab_GroupItem 字典表_删除

        public BaseResult DeleteLabGroupItemModelByID(string labCode, string pItemNo, string ItemNo)
        {
            BaseResult br = new BaseResult();
            try
            {

                IBLL.Common.BaseDictionary.IBLab_GroupItem ibDictionary = BLLFactory<IBLab_GroupItem>.GetBLL();
                //int tempClinetNo = int.Parse(SectionNo);
                if (ibDictionary.Exists(pItemNo, ItemNo, labCode))
                {
                    if (ibDictionary.Delete(pItemNo, ItemNo, labCode) > 0)
                    {
                        br.success = true;
                    }
                    else
                        br.success = false;
                }
                else
                {
                    br.ErrorInfo = "pItemNo, ItemNo, labCode不存在";
                    br.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return br;
        }
        #endregion
        #endregion

        #region 实验室获取当前项目编码的所有上级，赋值给变量strPItemNos 以逗号分隔
        /// <summary>
        /// 获取当前项目编码的所有上级，赋值给变量strPItemNos 以逗号分隔
        /// </summary>
        /// <param name="ItemNo">项目编码</param>
        public void GetParentItemNo(string ItemNo, string LabCode)
        {
            IBLL.Common.BaseDictionary.IBLab_GroupItem iblgi = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBLab_GroupItem>.GetBLL("BaseDictionary.Lab_GroupItem");
            Model.Lab_GroupItem labgroupitemModel = new Model.Lab_GroupItem();
            labgroupitemModel.ItemNo = ItemNo.Trim();
            labgroupitemModel.LabCode = LabCode.Trim();
            DataSet ds = iblgi.GetList(labgroupitemModel);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (strPItemNos.Length > 0)
                    {
                        strPItemNos += "," + ds.Tables[0].Rows[i]["PItemNo"].ToString();
                    }
                    else
                    {
                        strPItemNos = ds.Tables[0].Rows[i]["PItemNo"].ToString();
                    }
                    GetParentItemNo(ds.Tables[0].Rows[i]["PItemNo"].ToString(), LabCode.Trim());
                }
            }
        }
        #endregion

        #endregion

        #region 对照关系字典



        #region B_SampleTypeControl 样本类型字典
        #region B_SampleTypeControl 字典表_查询
        public BaseResultDataValue GetSampleTypeControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSampleTypeControl ibDictionary = BLLFactory<IBSampleTypeControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SampleTypeControl> EntityLis = new EntityListEasyUI<Model.SampleTypeControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }

                if (page > 0)
                    page = page - 1;
                DataSet ds = new DataSet();
                if (tablename.ToLower() == "sampletypecontrol")
                    ds = ibDictionary.GetListByPage(new SampleTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_sampletypecontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new SampleTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("SampleTypeControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_SuperGroupControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_SuperGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_SampleTypeControl 字典表_修改
        public BaseResult UpdateOrAddSampleTypeControlModelByID(Model.SampleTypeControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSampleTypeControl ibDictionary = BLLFactory<IBSampleTypeControl>.GetBLL();
                Model.SampleTypeControl modelControl = new Model.SampleTypeControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlSampleTypeNo = jsonentity.ControlSampleTypeNo;

                //modelControl.SampleTypeNo = -1;
                //DataSet dsTemp = ibDictionary.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    //string strSampleTypeNo = dsTemp.Tables[0].Rows[0]["SampleTypeNo"].ToString().Trim();
                //    //if (strSampleTypeNo != jsonentity.SampleTypeNo.ToString())
                //    br.success = false;
                //    br.ErrorInfo = "Repeat";
                //    return br;
                //}
                modelControl.SampleTypeNo = jsonentity.SampleTypeNo;
                modelControl.SampleTypeControlNo = jsonentity.ControlLabNo + "_" + jsonentity.SampleTypeNo + "_" + jsonentity.ControlSampleTypeNo;
                modelControl.UseFlag = 1;
                bool isExit = ibDictionary.Exists(jsonentity.ControlLabNo + "_" + jsonentity.SampleTypeNo + "_" + jsonentity.ControlSampleTypeNo);
                if (isExit)
                { //已经存在                    
                    if (ibDictionary.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibDictionary.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_SampleTypeControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region SampleTypeControl 字典表 删除
        /// <summary>
        /// SampleTypeControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelSampleTypeControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSampleTypeControl ibpg = BLLFactory<IBSampleTypeControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SampleTypeControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "SampleTypeControl 字典表 删除失败！";
                ZhiFang.Common.Log.Log.Info(String.Format("SampleTypeControl 字典表 删除失败！{0}{1}", ex.StackTrace, ex));
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_TestItemControl 项目字典

        #region B_TestItemControl 字典表_查询

        #region 循环
        public DataTable ReturnDataTableTestItemControl(DataSet ds)
        {
            int total = ds.Tables[0].Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("LabItemNo", typeof(string));
            dt.Columns.Add("CName", typeof(string));
            dt.Columns.Add("ShortCode", typeof(string));
            dt.Columns.Add("CenterItemNo", typeof(string));
            dt.Columns.Add("CenterCName", typeof(string));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow row = dt.NewRow();
                row["LabItemNo"] = dr["LabItemNo"];
                row["CName"] = dr["CName"];
                row["ShortCode"] = dr["ShortCode"];
                row["CenterItemNo"] = dr["CenterItemNo"];
                row["CenterCName"] = dr["CenterCName"];
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        public BaseResultDataValue GetTestItemControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBTestItemControl ibDictionary = BLLFactory<IBTestItemControl>.GetBLL();
            IBLL.Common.BaseDictionary.IBResultTestItemControl ibTesultControl = BLLFactory<IBResultTestItemControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.TestItemControl> EntityLis = new EntityListEasyUI<Model.TestItemControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;
                DataSet ds = new DataSet();
                if (tablename.ToLower() == "testitemcontrol")
                    ds = ibDictionary.GetListByPage(new TestItemControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_testitemcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new TestItemControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_resulttestitemcontrol")
                    ds = ibTesultControl.B_lab_GetResultListByPage(new ResultTestItemControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (dbtype == "default")
                    {
                        brdv.ResultDataFormatType = "JSON";
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;

                        //brds.rows = ds.Tables[0];

                        brds.rows = ReturnDataTableTestItemControl(ds);

                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        //EntityLis.total = ibDictionary.GetTotalCount(new TestItemControl { SearchLikeKey = itemkey });
                        brdv.success = true;
                        ZhiFang.Common.Log.Log.Info("B_TestItemControl 查询请求成功!");
                        brdv.ResultDataFormatType = "JSON";
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_TestItemControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_TestItemControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());

            }
            return brdv;
        }
        #endregion

        #region B_TestItemControl 字典表_修改
        public BaseResult UpdateOrAddTestItemControlModelByID(Model.TestItemControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL();
                Model.TestItemControl ticontrolModel = new Model.TestItemControl(); //ibtic.GetModel(ItemNo.Trim(), LabCode.Trim(), LabItemNo.Trim());
                ticontrolModel.ControlLabNo = jsonentity.ControlLabNo;
                ticontrolModel.ControlItemNo = jsonentity.ControlItemNo;
                //ticontrolModel.ItemNo = "-1"; 
                //DataSet dsTemp = ibtic.GetList(ticontrolModel);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                ////    string strItemNo = dsTemp.Tables[0].Rows[0]["ItemNo"].ToString().Trim();
                ////    if (strItemNo != jsonentity.ItemNo.Trim())
                //br.success = false;
                //br.ErrorInfo = "Repeat";
                //return br;
                //}
                ticontrolModel.ItemNo = jsonentity.ItemNo;
                ticontrolModel.ItemControlNo = jsonentity.ControlLabNo + "_" + jsonentity.ItemNo + "_" + jsonentity.ControlItemNo;
                ticontrolModel.UseFlag = 1;
                bool isExit = ibtic.Exists(jsonentity.ControlLabNo + "_" + jsonentity.ItemNo + "_" + jsonentity.ControlItemNo);
                if (isExit)
                { //已经存在                    
                    if (ibtic.Update(ticontrolModel) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibtic.Add(ticontrolModel) > 0)
                        br.success = true;
                }

            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_TestItemControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region B_ResultTestItemControl 新增
        /// <summary>
        /// B_ResultTestItemControl 结果项目字典对照关系表,此表用于报告下载进行对照 ganwh add 2015-9-6
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResult UpdateOrAddResultTestItemControlModelByID(Model.TestItemControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBResultTestItemControl ibtic = BLLFactory<IBResultTestItemControl>.GetBLL();
                Model.ResultTestItemControl ticontrolModel = new Model.ResultTestItemControl();
                ticontrolModel.ControlLabNo = jsonentity.ControlLabNo;
                ticontrolModel.ControlItemNo = jsonentity.ControlItemNo;

                ticontrolModel.ItemNo = jsonentity.ItemNo;
                ticontrolModel.ItemControlNo = jsonentity.ControlLabNo + "_" + jsonentity.ItemNo + "_" + jsonentity.ControlItemNo;
                ticontrolModel.UseFlag = 1;
                bool isExit = ibtic.Exists(jsonentity.ItemNo, jsonentity.ControlLabNo);
                if (isExit)
                { //已经存在                    
                    //if (ibtic.Update(ticontrolModel) > 0)
                    br.ErrorInfo = "此中心项目已存在对照关系,不能进行一对多操作";
                    br.success = false;
                }
                else
                {
                    if (ibtic.Add(ticontrolModel) > 0)
                        br.success = true;
                }

            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_ResultTestItemControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region B_ResultTestItemControl  删除
        /// <summary>
        /// B_ResultTestItemControl 结果项目字典对照关系表,此表用于报告下载进行对照 ganwh add 2015-9-6
        /// </summary>
        /// <returns></returns>
        public BaseResult DelResultTestItemControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBResultTestItemControl ibpg = BLLFactory<IBResultTestItemControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "B_ResultTestItemControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("B_ResultTestItemControl 字典表 删除失败！");
                ZhiFang.Common.Log.Log.Info(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region TestItemControl 字典表 删除
        /// <summary>
        /// TestItemControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelTestItemControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBTestItemControl ibpg = BLLFactory<IBTestItemControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "TestItemControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("TestItemControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_SuperGroupControl 检验大组字典
        #region B_SuperGroupControl 字典表_查询
        public BaseResultDataValue GetSuperGroupControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSuperGroup ibll = ZhiFang.BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBSuperGroup>.GetBLL("BaseDictionary.SuperGroup");
            IBLL.Common.BaseDictionary.IBSuperGroupControl ibDictionary = BLLFactory<IBSuperGroupControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SuperGroupControl> EntityLis = new EntityListEasyUI<Model.SuperGroupControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "supergroupcontrol")
                    ds = ibDictionary.GetListByPage(new SuperGroupControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_supergroupcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new SuperGroupControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_SuperGroupControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_SuperGroupControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_SuperGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
                return brdv;
            }
        }
        #endregion

        #region B_SuperGroupControl 字典表_修改
        public BaseResult UpdateOrAddSuperGroupControlModelByID(Model.SuperGroupControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSuperGroupControl ibllControl = BLLFactory<IBSuperGroupControl>.GetBLL();
                Model.SuperGroupControl modelControl = new Model.SuperGroupControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlSuperGroupNo = jsonentity.ControlSuperGroupNo;
                //modelControl.SuperGroupNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strSuperGroupNo = dsTemp.Tables[0].Rows[0]["SuperGroupNo"].ToString().Trim();
                //    if (strSuperGroupNo != jsonentity.SuperGroupNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}

                modelControl.SuperGroupNo = jsonentity.SuperGroupNo;
                modelControl.SuperGroupControlNo = jsonentity.ControlLabNo + "_" + jsonentity.SuperGroupNo + "_" + jsonentity.ControlSuperGroupNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.SuperGroupNo + "_" + jsonentity.ControlSuperGroupNo);
                if (isExit)
                { //已经存在       
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_SuperGroupControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region SuperGroupControlModelByID 字典表 删除
        /// <summary>
        /// SuperGroupControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelSuperGroupControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSuperGroupControl ibpg = BLLFactory<IBSuperGroupControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SuperGroupControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("SuperGroupControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_SickTypeControl 就诊类型字典
        #region B_SickTypeControl 字典表_查询
        public BaseResultDataValue GetSickTypeControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBSickTypeControl ibDictionary = BLLFactory<IBSickTypeControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.SickTypeControl> EntityLis = new EntityListEasyUI<Model.SickTypeControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "sicktypecontrol")
                    ds = ibDictionary.GetListByPage(new SickTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_sicktypecontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new SickTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("SampleTypeControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_SickTypeControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_SuperGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_SickTypeControl 字典表_修改
        public BaseResult UpdateOrAddSickTypeControlModelByID(Model.SickTypeControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSickTypeControl ibllControl = BLLFactory<IBSickTypeControl>.GetBLL();
                Model.SickTypeControl modelControl = new Model.SickTypeControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlSickTypeNo = jsonentity.ControlSickTypeNo;
                modelControl.SickTypeNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strSickTypeNo = dsTemp.Tables[0].Rows[0]["SickTypeNo"].ToString().Trim();
                //    if (strSickTypeNo != jsonentity.SickTypeNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}
                modelControl.SickTypeNo = jsonentity.SickTypeNo;
                modelControl.SickTypeControlNo = jsonentity.ControlLabNo + "_" + jsonentity.SickTypeNo + "_" + jsonentity.ControlSickTypeNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.SickTypeNo + "_" + jsonentity.ControlSickTypeNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_SickTypeControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region SickTypeControl 字典表 删除
        /// <summary>
        /// SickTypeControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelSickTypeControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBSickTypeControl ibpg = BLLFactory<IBSickTypeControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "SickTypeControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("SickTypeControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_GenderTypeControl 性别字典
        #region B_GenderTypeControl 字典表_查询
        public BaseResultDataValue GetGenderTypeControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBGenderTypeControl ibDictionary = BLLFactory<IBGenderTypeControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.GenderTypeControl> EntityLis = new EntityListEasyUI<Model.GenderTypeControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "gendertypecontrol")
                    ds = ibDictionary.GetListByPage(new GenderTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_gendertypecontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new GenderTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("GenderTypecontrol 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("b_lab_GenderTypecontrol 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "b_lab_gendertypecontrol 查询请求失败!";//;
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_GenderTypeControl 字典表_修改
        public BaseResult UpdateOrAddGenderTypeControlModelByID(Model.GenderTypeControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBGenderTypeControl ibllControl = BLLFactory<IBGenderTypeControl>.GetBLL();
                Model.GenderTypeControl modelControl = new Model.GenderTypeControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlGenderNo = jsonentity.ControlGenderNo;
                //modelControl.GenderNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strGenderNo = dsTemp.Tables[0].Rows[0]["GenderNo"].ToString().Trim();
                //    if (strGenderNo != jsonentity.GenderNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}
                modelControl.GenderNo = jsonentity.GenderNo;
                modelControl.GenderControlNo = jsonentity.ControlLabNo + "_" + jsonentity.GenderNo + "_" + jsonentity.ControlGenderNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.GenderNo + "_" + jsonentity.ControlGenderNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_GenderTypeControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region GenderTypeControl 字典表 删除
        /// <summary>
        /// GenderTypeControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelGenderTypeControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBGenderTypeControl ibpg = BLLFactory<IBGenderTypeControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "GenderTypeControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("GenderTypeControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #endregion

        #region B_FolkTypeControl 民族字典
        #region B_FolkTypeControl 字典表_查询
        public BaseResultDataValue GetFolkTypeControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBFolkTypeControl ibDictionary = BLLFactory<IBFolkTypeControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.FolkTypeControl> EntityLis = new EntityListEasyUI<Model.FolkTypeControl>();
            try
            {
                if (page > 0)
                    page = page - 1;

                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "folktypecontrol")
                    ds = ibDictionary.GetListByPage(new FolkTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_folktypecontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new FolkTypeControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "JSON";
                    brdv.success = true;
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_SuperGroupControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_SuperGroupControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_SuperGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_FolkTypeControl 字典表_修改
        public BaseResult UpdateOrAddFolkTypeControlModelByID(Model.FolkTypeControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBFolkTypeControl ibllControl = BLLFactory<IBFolkTypeControl>.GetBLL();
                Model.FolkTypeControl modelControl = new Model.FolkTypeControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlFolkNo = jsonentity.ControlFolkNo;
                //modelControl.FolkNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strItemNo = dsTemp.Tables[0].Rows[0]["FolkNo"].ToString().Trim();
                //    if (strItemNo != jsonentity.FolkNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}
                modelControl.FolkNo = jsonentity.FolkNo;
                modelControl.FolkControlNo = jsonentity.ControlLabNo + "_" + jsonentity.FolkNo + "_" + jsonentity.ControlFolkNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.FolkNo + "_" + jsonentity.ControlFolkNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_FolkTypeControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region FolkTypeControl 字典表 删除
        /// <summary>
        /// FolkTypeControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelFolkTypeControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBFolkTypeControl ibpg = BLLFactory<IBFolkTypeControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "FolkTypeControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("FolkTypeControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_DistrictControl 病区字典
        #region B_DistrictControl 字典表_查询
        public BaseResultDataValue GetDistrictControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDistrictControl ibDictionary = BLLFactory<IBDistrictControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.DistrictControl> EntityLis = new EntityListEasyUI<Model.DistrictControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "districtcontrol")
                    ds = ibDictionary.GetListByPage(new DistrictControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_districtcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new DistrictControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_DistrictControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }

                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_DistrictControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_SuperGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_DistrictControl 字典表_修改
        public BaseResult UpdateOrAddDistrictControlModelByID(Model.DistrictControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDistrictControl ibllControl = BLLFactory<IBDistrictControl>.GetBLL();
                Model.DistrictControl modelControl = new Model.DistrictControl();
                bool isExit = ibllControl.Exists(jsonentity.LabCode + "_" + jsonentity.DistrictNo + "_" + jsonentity.DistrictNo);
                if (isExit)
                { //已经存在
                    modelControl.DistrictNo = jsonentity.DistrictNo;
                    modelControl.ControlLabNo = jsonentity.ControlLabNo;
                    modelControl.DistrictControlNo = jsonentity.ControlLabNo + "_" + jsonentity.DistrictNo + "_" + jsonentity.ControlDistrictNo;
                    modelControl.ControlDistrictNo = jsonentity.ControlDistrictNo;
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    modelControl = new Model.DistrictControl();
                    modelControl.DistrictControlNo = jsonentity.ControlLabNo + "_" + jsonentity.DistrictNo + "_" + jsonentity.ControlDistrictNo;
                    modelControl.DistrictNo = jsonentity.DistrictNo;
                    modelControl.ControlLabNo = jsonentity.ControlLabNo;


                    modelControl.ControlDistrictNo = jsonentity.ControlDistrictNo;

                    modelControl.UseFlag = 1;
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_DistrictControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region B_DistrictControl 字典表 删除
        /// <summary>
        /// DistrictControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelDistrictControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDistrictControl ibpg = BLLFactory<IBDistrictControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "DistrictControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("DistrictControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_DoctorControl 医生字典
        #region B_DoctorControl 字典表_查询
        public BaseResultDataValue GetDoctorControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDoctorControl ibDictionary = BLLFactory<IBDoctorControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.DoctorControl> EntityLis = new EntityListEasyUI<Model.DoctorControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "doctorcontrol")
                    ds = ibDictionary.GetListByPage(new DoctorControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_doctorcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new DoctorControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_DoctorControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_DoctorControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_DoctorControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_DoctorControl 字典表_修改
        public BaseResult UpdateOrAddDoctorControlModelByID(Model.DoctorControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDoctorControl ibllControl = BLLFactory<IBDoctorControl>.GetBLL();
                Model.DoctorControl m_pgp = new DoctorControl();
                Model.DoctorControl modelControl = new Model.DoctorControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlDoctorNo = jsonentity.ControlDoctorNo;
                //modelControl.DoctorNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strSampleTypeNo = dsTemp.Tables[0].Rows[0]["DoctorNo"].ToString().Trim();
                //    if (strSampleTypeNo != jsonentity.DoctorNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}
                modelControl.DoctorNo = jsonentity.DoctorNo;
                modelControl.DoctorControlNo = jsonentity.ControlLabNo + "_" + jsonentity.DoctorNo + "_" + jsonentity.ControlDoctorNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.DoctorNo + "_" + jsonentity.ControlDoctorNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_DoctorControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region DoctorControl 字典表 删除
        /// <summary>
        /// DoctorControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelDoctorControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDoctorControl ibpg = BLLFactory<IBDoctorControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "DoctorControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("DoctorControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_DepartmentControl 科室字典
        #region B_DepartmentControl 字典表_查询
        public BaseResultDataValue GetDepartmentControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBDepartmentControl ibDictionary = BLLFactory<IBDepartmentControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.DepartmentControl> EntityLis = new EntityListEasyUI<Model.DepartmentControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "departmentcontrol")
                    ds = ibDictionary.GetListByPage(new DepartmentControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_departmentcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new DepartmentControl { SearchLikeKey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_DoctorControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_DepartmentControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_DepartmentControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_DepartmentControl 字典表_修改
        public BaseResult UpdateOrAddDepartmentControlModelByID(Model.DepartmentControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDepartmentControl ibllControl = BLLFactory<IBDepartmentControl>.GetBLL();
                Model.DepartmentControl modelControl = new Model.DepartmentControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlDeptNo = jsonentity.ControlDeptNo;

                //modelControl.DeptNo = -1;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    //string strSampleTypeNo = dsTemp.Tables[0].Rows[0]["DeptNo"].ToString().Trim();
                //    //if (strSampleTypeNo != jsonentity.DeptNo.ToString().Trim())
                //    br.success = false;
                //    br.ErrorInfo = "Repeat";
                //    return br;
                //}
                modelControl.DeptNo = jsonentity.DeptNo;
                modelControl.DepartmentControlNo = jsonentity.ControlLabNo + "_" + jsonentity.DeptNo + "_" + jsonentity.ControlDeptNo;
                modelControl.UseFlag = 1;

                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.DeptNo + "_" + jsonentity.ControlDeptNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_DepartmentControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region DepartmentControl 字典表 删除
        /// <summary>
        /// DepartmentControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelDepartmentControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBDepartmentControl ibpg = BLLFactory<IBDepartmentControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "DepartmentControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("DepartmentControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region B_PGroupControl 检验小组字典
        #region B_PGroupControl 字典表_查询
        public BaseResultDataValue GetPGroupControlModelManage(string itemkey, string tablename, string labcode, string selectedflag, int page, int rows)
        {
            IBLL.Common.BaseDictionary.IBPGroupControl ibDictionary = BLLFactory<IBPGroupControl>.GetBLL();
            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.PGroupControl> EntityLis = new EntityListEasyUI<Model.PGroupControl>();
            try
            {
                string dbtype = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("mssql") >= 0)
                {
                    dbtype = "default";
                }
                if (page > 0)
                    page = page - 1;

                DataSet ds = new DataSet();
                if (tablename.ToLower() == "pgroupcontrol")
                    ds = ibDictionary.GetListByPage(new PGroupControl { Searchlikekey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                else if (tablename.ToLower() == "b_lab_pgroupcontrol")
                    ds = ibDictionary.B_lab_GetListByPage(new PGroupControl { Searchlikekey = itemkey, ControlLabNo = labcode, ControlState = selectedflag }, page, rows);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataFormatType = "json";
                    if (dbtype == "default")
                    {
                        brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                        BaseResultDataSet brds = new BaseResultDataSet();
                        brds.total = ds.Tables[0].Rows.Count;
                        brds.rows = ds.Tables[0];
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                    }
                    else
                    {
                        EntityLis.rows = ibDictionary.DataTableToList(ds.Tables[0]);
                        EntityLis.total = ds.Tables[0].Rows.Count;
                        ZhiFang.Common.Log.Log.Info("B_PGroupControl 查询请求成功!");
                        brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("B_PGroupControl 查询请求成功,字典无数据!");
                    brdv.success = true;
                    brdv.ResultDataFormatType = "JSON";
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                }
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "B_PGroupControl 查询请求失败!";
                brdv.ResultDataFormatType = "JSON";
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(EntityLis);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.StackTrace + ex.ToString());
            }
            return brdv;
        }
        #endregion

        #region B_PGroupControl 字典表_修改
        public BaseResult UpdateOrAddPGroupControlModelByID(Model.PGroupControl jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroupControl ibllControl = BLLFactory<IBPGroupControl>.GetBLL();
                Model.PGroupControl modelControl = new Model.PGroupControl();
                modelControl.ControlLabNo = jsonentity.ControlLabNo;
                modelControl.ControlSectionNo = jsonentity.ControlSectionNo;
                //DataSet dsTemp = ibllControl.GetList(modelControl);
                //if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    string strSampleTypeNo = dsTemp.Tables[0].Rows[0]["SectionNo"].ToString().Trim();
                //    if (strSampleTypeNo != jsonentity.SectionNo.ToString().Trim())
                //        br.success = false;
                //    return br;
                //}
                modelControl.SectionNo = jsonentity.SectionNo;
                modelControl.SectionControlNo = jsonentity.ControlLabNo + "_" + jsonentity.SectionNo + "_" + jsonentity.ControlSectionNo;
                modelControl.UseFlag = 1;
                bool isExit = ibllControl.Exists(jsonentity.ControlLabNo + "_" + jsonentity.SectionNo + "_" + jsonentity.ControlSectionNo);
                if (isExit)
                { //已经存在
                    if (ibllControl.Update(modelControl) > 0)
                        br.success = true;
                }
                else
                {
                    if (ibllControl.Add(modelControl) > 0)
                        br.success = true;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "B_PGroupControl 修改请求失败！";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.StackTrace + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion

        #region PGroupControl 字典表 删除
        /// <summary>
        /// PGroupControlModelByID 字典表 删除
        /// </summary>
        /// <returns></returns>
        public BaseResult DelPGroupControlModelByID(string id)
        {
            ZhiFang.Common.Log.Log.Info("id:" + id);
            BaseResult br = new BaseResult();
            try
            {
                IBLL.Common.BaseDictionary.IBPGroupControl ibpg = BLLFactory<IBPGroupControl>.GetBLL();
                Model.PGroupPrint m_pgp = new PGroupPrint();
                if (ibpg.Delete(id) == 1)
                    br.success = true;
                else
                {
                    br.ErrorInfo = "PGroupControl字典表 删除失败！";
                    br.success = false;
                }
            }
            catch (Exception ex)
            {
                br.ErrorInfo = String.Format("PGroupControl 字典表 删除失败！", ex.StackTrace, ex);
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion


        #region ItemColorAndSampleTypeDetail

        /// <summary>
        /// 通过颜色ID获取对应的样本类型
        /// </summary>
        /// <param name="ColorId"></param>
        /// <returns></returns>
        public BaseResultDataValue GetItemColorAndSampleDetail(string ColorId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                IBLL.Common.BaseDictionary.IBItemColorAndSampleTypeDetail ibDictionary = BLLFactory<IBItemColorAndSampleTypeDetail>.GetBLL();

                Model.UiModel.UiItemColorSampleTypeNo model = ibDictionary.GetItemColorAndSampleDetail(ColorId);
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(model);

            }
            catch (Exception ex)
            {

                brdv.success = false;
                brdv.ErrorInfo = "异常";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + ex.ToString());
            }
            return brdv;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResult SaveToItemColorAndSampleTypeDetail(Model.UiModel.UiItemColorAndSampleTypeDetail jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {

                #region 取值
                List<Model.ItemColorAndSampleTypeDetail> ListModel = new List<ItemColorAndSampleTypeDetail>();
                Model.ItemColorAndSampleTypeDetail icatd = null;
                foreach (UiItemColorSampleTypeNo icstn in jsonentity.UiItemColorSampleTypeNo)
                {
                    foreach (var sampleTypeNo in icstn.SampleTypeNoList)
                    {
                        icatd = new ItemColorAndSampleTypeDetail();
                        icatd.ColorID = icstn.ColorId;
                        icatd.SampleTypeNo = sampleTypeNo;
                        ListModel.Add(icatd);
                    }
                }
                #endregion


                IBLL.Common.BaseDictionary.IBItemColorAndSampleTypeDetail ibDictionary = BLLFactory<IBItemColorAndSampleTypeDetail>.GetBLL();

                #region 删除
                foreach (UiItemColorSampleTypeNo icstn in jsonentity.UiItemColorSampleTypeNo)
                {
                    ibDictionary.DeleteItemColorAndSampleTypeDetailByColorId(icstn.ColorId);
                }
                #endregion

                if (ibDictionary.SaveToItemColorAndSampleTypeDetail(ListModel))
                {
                    br.success = true;
                }
                else
                    br.success = false;
            }
            catch (Exception ex)
            {
                br.ErrorInfo = "异常";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                br.success = false;
            }
            return br;
        }
        #endregion
        #endregion

        #region 字典批量复制
        /// <summary>
        /// 字典批量复制
        /// </summary>
        /// <param name="DicTable"></param>
        /// <param name="fromLab">源实验室</param>
        /// <param name="toLab">目标实验室</param>
        /// <returns></returns>
        public BaseResultDataValue CopyAllToLabs(string DicTable, string fromLab, string toLab)
        {
            BaseResultDataValue br = new BaseResultDataValue();

            try
            {
                IBLL.Common.IBBatchCopy ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary." + DicTable);
                List<string> LabCodeLst = new List<string>();
                if (fromLab != null)
                {
                    LabCodeLst.Add(fromLab + '&' + toLab);
                }
                else
                {
                    LabCodeLst.Add(toLab);
                }
                //LabCodeLst.Add(LabCodeNo);
                if (ibCopy.CopyToLab(LabCodeLst))
                {
                    br.success = true;

                }
                else
                {
                    br.success = false;
                    //return br;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.DictionaryInfoSys.DicCenterManager.CopyAllToLabs--批量复制到客户端时出错：" + ex.ToString());
                br.success = false;
                br.ErrorInfo = "异常";
                ZhiFang.Common.Log.Log.Error(br.ErrorInfo + ex.ToString());
                return br;
            }

            return br;

        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="DicTable"></param>
        /// <param name="LabCodeNo"></param>
        /// <returns></returns>
        public BaseResult ExistLabsData(string DicTable, string LabCodeNo)
        {
            BaseResult br = new BaseResult();
            IBLL.Common.IBBatchCopy ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary." + DicTable);
            br.success = ibCopy.IsExist(LabCodeNo);
            return br;
        }
        /// <summary>
        /// 删除字典表和关系中LabCode的记录
        /// </summary>
        /// <param name="DicTable"></param>
        /// <param name="LabCodeNo"></param>
        /// <returns></returns>
        public BaseResult DeleteByLabCode(string DicTable, string LabCodeNo)
        {
            BaseResult br = new BaseResult();
            IBLL.Common.IBBatchCopy ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary." + DicTable);
            br.success = ibCopy.DeleteByLabCode(LabCodeNo);
            return br;
        }

        /// <summary>
        /// 项目批量复制
        /// </summary>
        /// <param name="ItemNos">项目号</param>
        /// <param name="fromLabCodeNo">源实验室</param>
        /// <param name="LabCodeNo">目标实验室</param>
        /// <param name="ItemKey"></param>
        /// <returns></returns>
        public BaseResult BatchCopyItemsToLab(string ItemNos, string fromLabCodeNo, string LabCodeNo, string ItemKey)
        {
            BaseResult br = new BaseResult();

            List<string> lst = new List<string>();
            try
            {
                IBLL.Common.IBBatchCopy ibCopy = null;
                if (fromLabCodeNo != null)//实验室到实验室间的复制
                {
                    string items = fromLabCodeNo + '|' + LabCodeNo + "|(" + ItemNos + ")";
                    lst.Add(items);
                    lst.Insert(0, "CopyToLab_LabFirstSelect#" + LabCodeNo);
                    ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary.Lab_TestItem");
                }
                else//中心到实验室间的复制
                {
                    string items = LabCodeNo + "|(" + ItemNos + ")";
                    lst.Add(items);
                    lst.Insert(0, "CopyToLab_LabFirstSelect");
                    ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary.TestItem");
                }

                if (ibCopy.CopyToLab(lst))
                    br.success = true;
                else
                    br.success = false;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("BatchCopyItemsToLab异常", ex);
            }

            return br;

        }
        #endregion

        #region 报告模板管理

        IBLL.Common.BaseDictionary.IBPrintFormat ibPrintFormat = BLLFactory<IBPrintFormat>.GetBLL();
        /// <summary>
        /// 查询所有报告模板信息
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetAllReportModelManage(string itemkey, int page, int rows)
        {
            BaseResultDataValue resultObj = new BaseResultDataValue();
            EntityListEasyUI<Model.PrintFormat> printFormatlist = new EntityListEasyUI<PrintFormat>();
            try
            {
                if (page > 0)
                {
                    page = page - 1;
                }
                DataSet ds = ibPrintFormat.GetListByPage(new PrintFormat { PrintFormatName = itemkey }, page, rows);
                if (ds != null && ds.Tables.Count > 0)
                {
                    printFormatlist.rows = ibPrintFormat.DataTableToList(ds.Tables[0]);
                    printFormatlist.total = ibPrintFormat.GetTotalCount();

                    resultObj.success = true;
                    resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(printFormatlist);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("查询模板管理字典无数据！");
                    return null;
                }
                return resultObj;
            }
            catch (Exception e)
            {
                resultObj.ErrorInfo = "异常";
                resultObj.success = false;
                resultObj.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(resultObj.ErrorInfo + e.ToString());
                return null;
            }
        }
        ///// <summary>
        ///// 根据ID查询报告模板
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public BaseResultDataValue GetReportModelManageByID(string id)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    try
        //    {
        //        Model.PrintFormat pf = ibPrintFormat.GetModel(id);
        //        if (pf != null)
        //        {
        //            brdv.ErrorInfo = "查询成功！";
        //            brdv.success = true;
        //            brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(pf);
        //        }
        //        else
        //        {
        //            brdv.ErrorInfo = "查询失败！";
        //            brdv.success = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Error(ex.ToString());
        //        brdv.ErrorInfo = String.Format("查询失败！{0}{1}", ex.StackTrace, ex);
        //        brdv.success = false;
        //    }
        //    return brdv;
        //}
        /// <summary>
        /// 根据ID删除一个报告模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResult DelReportModel(string id)
        {
            BaseResult br = new BaseResult();
            try
            {
                #region 验证
                //是否存在
                if (!ibPrintFormat.Exists(id))
                {
                    ZhiFang.Common.Log.Log.Info("执行DelReportModel操作,Id不存在");
                    return null;
                }
                #endregion

                if (ibPrintFormat.Delete(id) > 0)
                    br.success = true;
                else
                    br.success = false;

                return br;
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return null;
            }

        }

        /// <summary>
        /// 增加一个模板
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public void AddReportModel()
        {
            HttpContext.Current.Response.ContentType = "text/html";

            // HttpContext.Current.Response.AddHeader("content-type", "text/html");
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                Model.PrintFormat jsonentity = new PrintFormat();
                string strjsonentity = HttpContext.Current.Request.Form["jsonentity"];
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jsonentity = jss.Deserialize<Model.PrintFormat>(strjsonentity);
                if (jsonentity != null)
                {
                    int id = ibPrintFormat.Add(jsonentity);
                    if (id > 0)
                    {
                        string tmpfilename = "";
                        HttpPostedFile file = HttpContext.Current.Request.Files["FileUpload1"];
                        int len = file.ContentLength;
                        if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                        {
                            tmpfilename = file.FileName;
                            ZhiFang.Common.Log.Log.Info("模版文件名tmpfilename：" + tmpfilename);
                            string fileclass = System.IO.Path.GetExtension(tmpfilename).ToUpper().Trim();
                            if (!string.IsNullOrEmpty(tmpfilename) && (fileclass == ".XSL" || fileclass == ".XSLT" || fileclass == ".FRX" || fileclass == ".FR3"))
                            {
                                string savepath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + jsonentity.PintFormatAddress + "\\" + id + "\\";
                                ZhiFang.Common.Log.Log.Info("savepath:" + savepath);
                                if (ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(savepath))
                                {
                                    string[] filename = tmpfilename.Split('.');

                                    string filepath = System.IO.Path.Combine(savepath, id + "." + filename[1]);
                                    // file.ContentType = "text/plain;charset=UTF-8";

                                    file.SaveAs(filepath);

                                    ZhiFang.Common.Log.Log.Info("filepath:" + filepath);
                                    ZhiFang.Common.Log.Log.Info("filename:" + tmpfilename);

                                    brdv.success = true;

                                }
                            }
                            else { brdv.success = false; ZhiFang.Common.Log.Log.Info("模版文件名tmpfilename：" + tmpfilename); }
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "保存失败！";
                        ZhiFang.Common.Log.Log.Info("保存失败!" + brdv.ErrorInfo);
                        brdv.success = false;
                    }
                }
                else
                {
                    brdv.ErrorInfo = "json参数反序列化错误！";
                    ZhiFang.Common.Log.Log.Info("json参数反序列化错误！");
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("保存失败！" + ex.ToString());
                brdv.ErrorInfo = "保存失败！";
                brdv.success = false;
            }
            //return brdv;
        }

        /// <summary>
        /// 修改单个报告模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResult UpdateReportModelByID(Model.PrintFormat jsonentity)
        {
            BaseResult br = new BaseResult();
            try
            {
                string strPicture = "";
                strPicture = HttpContext.Current.Request.Form["FileUpload1"];

                #region 验证
                if (ibPrintFormat.GetModel(jsonentity.Id.ToString()) == null)
                {
                    ZhiFang.Common.Log.Log.Info("模板不存在");
                    return null;
                }
                #endregion

                if (jsonentity != null)
                {
                    if (ibPrintFormat.Update(jsonentity) > 0)
                        br.success = true;
                    else
                        br.success = false;
                }
                return br;
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("UpdateReportModelByID.异常:" + e.ToString());
                return br;
            }
        }

        public BaseResult UploadTempleteFile()
        {
            BaseResult br = new BaseResult();
            //Picture图片字符串,ExpandName图片扩展名
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string strPicture = "";
            try
            {
                strPicture = HttpContext.Current.Request.Form["FileUpload1"];
                if (!string.IsNullOrEmpty(strPicture))
                {
                    byte[] tempBuf = Convert.FromBase64String(strPicture);//把字符串读到字节数组中
                    MemoryStream tempMemoryStream = new MemoryStream(tempBuf);
                    System.Drawing.Image tempImge = System.Drawing.Image.FromStream(tempMemoryStream);
                    string tempPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UpLoadPicturePath");

                    //if (tmpfilename.Length > 0 && this.FileUpload1.FileBytes.Count() > 0 && (tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".XSL" || tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".XSLT" || (tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".FRX" || tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".FR3")))
                    //{
                    //    pf_m.PintFormatFileName = this.FileUpload1.FileName.ToString();
                    //}

                    ////if (string.IsNullOrEmpty(strExpandName))
                    //string tempFileName = "";
                    //if (string.IsNullOrEmpty(tempPath))
                    //    tempFileName = "Images\\" + GUIDHelp.GetGUIDString() + ".jpg";
                    //else
                    //    tempFileName = tempPath + "\\" + GUIDHelp.GetGUIDString() + ".jpg";
                    //tempPath = System.AppDomain.CurrentDomain.BaseDirectory + tempFileName;
                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(tempPath)))
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempPath));
                    tempImge.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //tempBaseResultDataValue.ResultDataValue = "{ filepath:" + tempFileName + " }";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.ErrorInfo = "异常";// ex.Message;
                tempBaseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("UploadTempleteFile.异常:" + ex.ToString());
            }
            return tempBaseResultDataValue;

            // string tmpfiledir = ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL");
            //file.SaveAs(
            return br;
        }
        #endregion

        #region 申请录入字典
        IBLL.Common.BaseDictionary.IBSuperGroup sg = BLLFactory<IBSuperGroup>.GetBLL();
        /// <summary>
        /// 项目过滤
        /// </summary>
        /// <param name="SuperGroupNo">检验大组</param>
        /// <param name="ItemKey">联想输入</param>
        ///<param name="rows">每页行数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="labcode">机构</param>
        /// <returns></returns>
        public BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int pageindex, string labcode)
        {

            BaseResultDataValue resultObj = new BaseResultDataValue();
            //EntityList<ApplyInputItemEntity> testItemList = new EntityList<ApplyInputItemEntity>();
            EntityListEasyUI<ApplyInputItemEntity> testItemList = new EntityListEasyUI<ApplyInputItemEntity>();
            DataSet ds;
            int AllItemCount = 0;
            try
            {
                #region 如果医疗机构编码不存在 按照中心项目字典表显示项目
                switch ((TestItemSuperGroupClass)Enum.Parse(typeof(TestItemSuperGroupClass), supergroupno.ToUpper()))
                {
                    case TestItemSuperGroupClass.ALL:

                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, Visible = 1 });
                        }

                        break;

                    case TestItemSuperGroupClass.COMBI:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 });
                            //AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI, UseFlag = 1, Visible = 1 });

                        }
                        break;
                    //case TestItemSuperGroupClass.OFTEN:
                    //    if (labcode != "")
                    //    {
                    //        ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                    //        AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                    //    }
                    //    else
                    //    {
                    //        ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                    //        AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.OFTEN, IsDoctorItem = 1, Visible = 1 });
                    //    }
                    //    break;
                    case TestItemSuperGroupClass.CHARGE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.CHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    case TestItemSuperGroupClass.COMBIITEMPROFILE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBIITEMPROFILE, Visible = 1 });
                        }
                        break;
                    case TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.DOCTORCOMBICHARGE, IsDoctorItem = 1, Visible = 1 });
                        }
                        break;
                    default:
                        if (labcode != null && labcode != "")
                        {
                            ds = LabTestItem.GetListByPage(new Model.Lab_TestItem { LabCode = labcode, CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = LabTestItem.GetTotalCount(new Model.Lab_TestItem { LabCode = labcode, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });
                        }
                        else
                        {
                            ds = CenterTestItem.GetListByPage(new Model.TestItem { CName = itemkey, EName = itemkey, ShortCode = itemkey, ShortName = itemkey, TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 }, pageindex - 1, rows);
                            AllItemCount = CenterTestItem.GetTotalCount(new Model.TestItem { TestItemSuperGroupClass = TestItemSuperGroupClass.COMBI, UseFlag = 1, IsDoctorItem = 1, Visible = 1 });

                        }
                        break;

                }
                #endregion

                if (ds != null)
                {
                    if (labcode != null && labcode != "")
                    {
                        //testItemList.list = LabTestItem.ItemEntityDataTableToList(ds.Tables[0]);
                        //testItemList.count = testItemList.list.Count;
                        testItemList.rows = LabTestItem.ItemEntityDataTableToList(ds.Tables[0]);
                        testItemList.total = AllItemCount;//testItemList.rows.Count;
                        resultObj.success = true;
                        resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(testItemList);
                    }
                    else
                    {
                        testItemList.rows = LabTestItem.ItemEntityDataTableToList(ds.Tables[0]);
                        testItemList.total = AllItemCount;//testItemList.rows.Count;
                        resultObj.success = true;
                        resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(testItemList);
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("GetTestItem.异常" + e.ToString());
                resultObj.ErrorInfo = "异常";// e.ToString();
                resultObj.ResultDataValue = "";
                resultObj.success = false;
            }
            return resultObj;
        }

        /// <summary>
        /// 查询检验类型项目列表
        /// </summary>
        /// <param name="typestate">1-组合,2-检验大组,3-组合+检验大组都存在</param>
        /// <returns></returns>
        public BaseResultDataValue GetSuperGroupList(int typestate)
        {
            BaseResultDataValue resultObj = new BaseResultDataValue();

            //EntityList<SuperGroupEntity> superGroupEntityList = new EntityList<SuperGroupEntity>();
            EntityListEasyUI<SuperGroupEntity> superGroupEntityList = new EntityListEasyUI<SuperGroupEntity>();
            //SuperGroupEntity superGroupEntity = new SuperGroupEntity();
            List<SuperGroupEntity> SuperGroupList = null;
            try
            {
                #region 验证
                if (typestate != 1 && typestate != 2 && typestate != 3)
                {
                    return null;
                }
                #endregion

                if (typestate == 1)
                {
                    SuperGroupList = GetCombiSuperGroupList(); ;
                    //superGroupEntityList.list = SuperGroupList;
                    //superGroupEntityList.count = SuperGroupList.Count;
                    superGroupEntityList.rows = SuperGroupList;
                    superGroupEntityList.total = SuperGroupList.Count;
                    resultObj.success = true;
                    resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(superGroupEntityList);
                }
                else if (typestate == 2)
                {
                    SuperGroupList = GetItemSuperGroupList();
                    //superGroupEntityList.list = SuperGroupList;
                    //superGroupEntityList.count = SuperGroupList.Count;
                    superGroupEntityList.rows = SuperGroupList;
                    superGroupEntityList.total = SuperGroupList.Count;
                    resultObj.success = true;
                    resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(superGroupEntityList);

                }
                else if (typestate == 3)
                {
                    SuperGroupList = new List<SuperGroupEntity>();
                    SuperGroupList.AddRange(GetCombiSuperGroupList());
                    SuperGroupList.AddRange(GetItemSuperGroupList());

                    //superGroupEntityList.list = SuperGroupList;
                    //superGroupEntityList.count = SuperGroupList.Count;
                    superGroupEntityList.rows = SuperGroupList;
                    superGroupEntityList.total = SuperGroupList.Count;
                    resultObj.success = true;
                    resultObj.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(superGroupEntityList);
                }

            }
            catch (Exception e)
            {
                resultObj.success = false;
                resultObj.ErrorInfo = "异常";// e.ToString();
                resultObj.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error("GetSuperGroupList.异常" + e.ToString());
            }

            return resultObj;
        }

        /// <summary>
        /// 取出组合中枚举的值
        /// </summary>
        /// <returns></returns>
        private List<SuperGroupEntity> GetCombiSuperGroupList()
        {
            List<SuperGroupEntity> CombiSuperGroupList = new List<SuperGroupEntity>();
            SuperGroupEntity sge = new SuperGroupEntity();

            foreach (TestItemSuperGroupClass cmb in (TestItemSuperGroupClass[])Enum.GetValues(typeof(TestItemSuperGroupClass)))
            {

                sge = new SuperGroupEntity();
                switch (Enum.GetName(typeof(TestItemSuperGroupClass), cmb).ToUpper())
                {
                    case "ALL":
                        sge.CName = "全部";
                        sge.SuperGroupNo = cmb.ToString();
                        break;

                    case "DOCTORCOMBICHARGE":
                        sge.CName = "医生组套收费";
                        sge.SuperGroupNo = cmb.ToString();
                        break;

                    //case "OFTEN":
                    //    sge.CName = "普通";
                    //    sge.SuperGroupNo = cmb.ToString();
                    //    break;
                    case "COMBI":
                        sge.CName = "组合组套";
                        sge.SuperGroupNo = cmb.ToString();
                        break;
                    case "COMBIITEMPROFILE":
                        sge.CName = "组套收费";
                        sge.SuperGroupNo = cmb.ToString();
                        break;
                    case "CHARGE":
                        sge.CName = "收费";
                        sge.SuperGroupNo = cmb.ToString();
                        break;

                }
                if (sge.CName != null && sge.SuperGroupNo != null)
                    CombiSuperGroupList.Add(sge);
            }
            return CombiSuperGroupList;

        }

        /// <summary>
        /// 从数据库表中取出所有检验大组子项目
        /// </summary>     
        /// <returns></returns>
        private List<SuperGroupEntity> GetItemSuperGroupList()
        {
            List<SuperGroupEntity> SuperGroupList = null;
            SuperGroupEntity superGroupEntity = null;
            DataSet ds = sg.GetAllList();
            if (ds != null)
            {
                SuperGroupList = new List<SuperGroupEntity>();
                List<SuperGroup> superGroupList = sg.DataTableToList(ds.Tables[0]);
                foreach (SuperGroup superGroup in superGroupList)
                {
                    superGroupEntity = new SuperGroupEntity();
                    superGroupEntity.CName = superGroup.CName;
                    superGroupEntity.SuperGroupNo = superGroup.SuperGroupNo.ToString();
                    SuperGroupList.Add(superGroupEntity);
                }
            }
            return SuperGroupList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemid">项目号</param>
        /// <param name="labcode">机构号</param>
        /// <returns></returns>
        public BaseResultDataValue GetTestDetailByItemID(string itemid, string labcode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<TestItemDetail> ttdList = new List<TestItemDetail>();
            SampleTypeDetail sampleDetail = new SampleTypeDetail();
            //List<string> itemColorList = new List<string>();
            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();
            try
            {
                GetSubLabItem(itemid, labcode, ref ttdList);
                if (ttdList.Count > 0)
                {
                    brdv.success = true;

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.Json<TestItemDetail>(ttdList); //.JsonDotNetSerializer(ttdList);

                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error("GetTestDetailByItemID.异常" + e.ToString());
            }
            return brdv;
        }

        /// <summary>
        /// 非组套项目，子项是它本身,构造一个假的组套外壳 
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="labcode"></param>
        /// <returns></returns>
        public BaseResultDataValue GetParentItemAsChildItemByItemID(string itemid, string labcode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<TestItemDetail> ttdList = new List<TestItemDetail>();
            SampleTypeDetail sampleDetail = new SampleTypeDetail();
            //List<string> itemColorList = new List<string>();
            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();
            try
            {
                GetItemAsSubLabItem(itemid, labcode, ref ttdList);
                if (ttdList.Count > 0)
                {
                    brdv.success = true;

                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.Json<TestItemDetail>(ttdList); //.JsonDotNetSerializer(ttdList);

                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error("GetParentItemAsChildItemByItemID.异常" + e.ToString());
            }
            return brdv;
        }


        public void GetItemAsSubLabItem(string itemid, string labcode, ref List<TestItemDetail> listtestitem)
        {
            try
            {
                IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
                DataSet dsitem = null;
                dsitem = ibDictionary.GetListByPage(new Lab_TestItem { LabCode = labcode, LabItemNo = itemid, ItemNo = itemid }, 0, 0);

                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsitem.Tables[0].Rows.Count; i++)
                    {
                        GetSubLabItem(dsitem.Tables[0].Rows[i]["ItemNo"].ToString(), labcode, ref listtestitem);
                        TestItemDetail ttd = new TestItemDetail();
                        ttd.CName = dsitem.Tables[0].Rows[i]["CName"].ToString();
                        ttd.ItemNo = dsitem.Tables[0].Rows[i]["ItemNo"].ToString();
                        ttd.EName = dsitem.Tables[0].Rows[i]["EName"].ToString();
                        ttd.ColorName = dsitem.Tables[0].Rows[i]["Color"].ToString();
                        ttd.Prices = dsitem.Tables[0].Rows[i]["price"].ToString();
                        if (ttd.ColorName != "")
                        {
                            var aa = icd.GetModelByColorName(ttd.ColorName);
                            if (aa != null)
                                ttd.ColorValue = aa.ColorValue;
                            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();
                            foreach (ZhiFang.Model.SampleType sampletype in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(ttd.ColorName)) //ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].SampleType)
                            {
                                SampleTypeDetail sampleDetail = new SampleTypeDetail();
                                sampleDetail.CName = sampletype.CName;
                                sampleDetail.SampleTypeID = sampletype.SampleTypeID.ToString();
                                sampleDetailList.Add(sampleDetail);
                            }
                            ttd.SampleTypeDetail = sampleDetailList;
                        }
                        listtestitem.Add(ttd);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BaseResultDataValue GetInputBaseDic(string[] tablename, string[] fields, string labcode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (tablename == null || tablename.Length <= 0)
            {
                brdv.ErrorInfo = "字典表数组为空！";
                brdv.success = false;
                return brdv;
            }
            if (fields == null || fields.Length <= 0)
            {
                brdv.ErrorInfo = "字段数组为空！";
                brdv.success = false;
                return brdv;
            }
            if (tablename.Length != fields.Length)
            {
                brdv.ErrorInfo = "字典表数组同字段数组不一致！";
                brdv.success = false;
                return brdv;
            }
            for (int i = 0; i < tablename.Length; i++)
            {
                BaseResultDataSet brds = new BaseResultDataSet();
                DataSet ds = ibdgpd.DictionaryGet(tablename[i], fields[i], labcode, null, null, 0, 0);
                brds.rows = ds.Tables[0];
                if (brdv.ResultDataValue != null && brdv.ResultDataValue.Trim() != "")
                {
                    brdv.ResultDataValue += "," + tablename + ":" + ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                }
                else
                {
                    brdv.ResultDataValue += tablename + ":" + ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                }
            }
            //this.GetPubDict()
            return brdv;
        }

        #endregion        

        #region 条码打印配置表增删改查服务

        IBLL.Common.BaseDictionary.IBLocationbarCodePrintPamater LocationBarCodePrint = BLLFactory<IBLocationbarCodePrintPamater>.GetBLL();
        //IBLL.Common.BaseDictionary.IBLab_TestItem ibDictionary = BLLFactory<IBLab_TestItem>.GetBLL();
        /// <summary>
        /// 根据账户ID查询打印配置的参数信息
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public BaseResultDataValue GetLocationBarCodePrintPamater(string AccountId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                Model.LocationbarCodePrintPamater model = LocationBarCodePrint.GetModel(AccountId);

                if (model == null)
                    return null;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(model);

            }
            catch (Exception e)
            {
                brdv.ErrorInfo = "异常";// e.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("GetLocationBarCodePrintPamater.异常" + e.ToString());
            }
            return brdv;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue AddLocationBarCodePrintPamater(Model.LocationbarCodePrintPamater jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {

                if (jsonentity.AccountId == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "AccountId不能为空";
                    return brdv;
                }
                Model.LocationbarCodePrintPamater locationBarCode = jsonentity;

                LocationbarCodePrintPamater LocationbarCodePrintPamater = LocationBarCodePrint.GetAdminPara();
                if (LocationbarCodePrintPamater != null)
                {
                    locationBarCode.UpdateDateTime = DateTime.Now;
                    locationBarCode.ParaMeter = jsonentity.ParaMeter;
                    locationBarCode.Id = LocationbarCodePrintPamater.Id;
                    if (LocationBarCodePrint.Update(locationBarCode))
                    {
                        brdv.success = true;

                        #region 配置参数写入Cookie

                        Cookie.CookieHelper.Write("BarcodeModel", locationBarCode.ParaMeter.Trim());
                        #endregion
                    }
                    else
                        brdv.success = false;
                }
                else
                {
                    locationBarCode.CreateDateTime = DateTime.Now;
                    locationBarCode.Id = Common.Public.GUIDHelp.GetGUIDLong();
                    if (LocationBarCodePrint.Add(locationBarCode))
                    {
                        brdv.success = true;

                        #region 配置参数写入Cookie
                        Cookie.CookieHelper.Write("BarcodeModel", locationBarCode.ParaMeter.Trim());
                        #endregion
                    }
                    else
                        brdv.success = false;
                }

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("AddLocationBarCodePrintPamater.异常" + e.ToString());
            }

            return brdv;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue UpdateLocationBarCodePrintPamater(Model.LocationbarCodePrintPamater jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                //验证
                if (jsonentity.AccountId == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "AccountId不能为空";
                    return brdv;
                }
                Model.LocationbarCodePrintPamater locationBarCode = jsonentity;

                LocationbarCodePrintPamater LocationbarCodePrintPamater = LocationBarCodePrint.GetAdminPara();
                if (LocationbarCodePrintPamater != null)
                {

                    locationBarCode.UpdateDateTime = DateTime.Now;
                    locationBarCode.ParaMeter = jsonentity.ParaMeter;
                    locationBarCode.Id = LocationbarCodePrintPamater.Id;
                    if (LocationBarCodePrint.Update(locationBarCode))
                    {
                        brdv.success = true;

                        #region 配置参数写入Cookie

                        Cookie.CookieHelper.Write("BarcodeModel", locationBarCode.ParaMeter.Trim());
                        #endregion
                    }
                    else
                        brdv.success = false;
                }
                else
                {
                    locationBarCode.CreateDateTime = DateTime.Now;
                    locationBarCode.Id = Common.Public.GUIDHelp.GetGUIDLong();
                    if (LocationBarCodePrint.Add(locationBarCode))
                    {
                        brdv.success = true;

                        #region 配置参数写入Cookie
                        Cookie.CookieHelper.Write("BarcodeModel", locationBarCode.ParaMeter.Trim());
                        #endregion
                    }
                    else
                        brdv.success = false;
                }
                //Model.LocationbarCodePrintPamater locationBarCode = LocationBarCodePrint.GetModel(jsonentity.AccountId);
                //if (locationBarCode == null)
                //    return null;

                ////赋值
                ////locationBarCode = jsonentity;
                //locationBarCode.UpdateDateTime = DateTime.Now;
                //locationBarCode.ParaMeter = jsonentity.ParaMeter;
                //if (LocationBarCodePrint.Update(locationBarCode))
                //{
                //    brdv.success = true;

                //    #region 配置参数写入Cookie

                //    Cookie.CookieHelper.Write("BarcodeModel", locationBarCode.ParaMeter.Trim());
                //    #endregion
                //}
                //else
                //    brdv.success = false;


            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("UpdateLocationBarCodePrintPamater.异常" + e.ToString());
            }

            return brdv;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        public BaseResultDataValue DelLocationBarCodePrintPamater(string AccountId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                Model.LocationbarCodePrintPamater model = LocationBarCodePrint.GetModel(AccountId);
                if (model == null)
                    return null;

                if (LocationBarCodePrint.Delete(model.Id))
                    brdv.success = true;
                else
                    brdv.success = false;

            }
            catch (Exception e)
            {
                brdv.ErrorInfo = "异常";// e.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("DelLocationBarCodePrintPamater.异常" + e.ToString());
            }
            return brdv;
        }
        #endregion

        public void GetSubLabItem(string itemid, string labcode, ref List<TestItemDetail> listtestitem)
        {
            try
            {
                DataSet dsitem = null;
                if (labcode != null && labcode != "")
                {
                    dsitem = LabGroupItem.GetGroupItemList(itemid, labcode);
                }
                else
                    dsitem = CenterGroupItem.GetGroupItemList(itemid);

                if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsitem.Tables[0].Rows.Count; i++)
                    {
                        GetSubLabItem(dsitem.Tables[0].Rows[i]["ItemNo"].ToString(), labcode, ref listtestitem);
                        TestItemDetail ttd = new TestItemDetail();
                        ttd.CName = dsitem.Tables[0].Rows[i]["CName"].ToString();
                        ttd.ItemNo = dsitem.Tables[0].Rows[i]["ItemNo"].ToString();
                        ttd.EName = dsitem.Tables[0].Rows[i]["EName"].ToString();
                        ttd.ColorName = dsitem.Tables[0].Rows[i]["Color"].ToString();
                        ttd.Prices = dsitem.Tables[0].Rows[i]["price"].ToString();
                        if (ttd.ColorName != "")
                        {
                            var aa = icd.GetModelByColorName(ttd.ColorName);
                            if (aa != null)
                                ttd.ColorValue = aa.ColorValue; //ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].ColorValue;
                            //List<SampleType> listsampletype = ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].SampleType;
                            List<SampleTypeDetail> sampleDetailList = new List<SampleTypeDetail>();
                            foreach (ZhiFang.Model.SampleType sampletype in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(ttd.ColorName)) //ZhiFang.BLL.Common.Lib.ItemColor()[ttd.ColorName].SampleType)
                            {
                                SampleTypeDetail sampleDetail = new SampleTypeDetail();
                                sampleDetail.CName = sampletype.CName;
                                sampleDetail.SampleTypeID = sampletype.SampleTypeID.ToString();
                                sampleDetailList.Add(sampleDetail);
                            }
                            ttd.SampleTypeDetail = sampleDetailList;
                        }
                        listtestitem.Add(ttd);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region 客户参数
        public BaseResultDataValue AddBBClientPara(Model.B_ClientPara jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (jsonentity == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则不能为空！";
                    return brdv;
                }
                if (jsonentity.Name == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则Name不能为空！";
                    return brdv;
                }
                if (jsonentity.ParaDesc == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaDesc不能为空！";
                    return brdv;
                }
                if (jsonentity.LabID < 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则LabID不能为空！";
                    return brdv;
                }
                if (jsonentity.ParaNo == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaNo不能为空";
                    return brdv;
                }
                if (jsonentity.ParaValue == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaValue不能为空";
                    return brdv;
                }
                IBLL.Common.BaseDictionary.IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                if (IBBClientPara.Add(jsonentity) > 0)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "新增未成功！";
                }

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("AddBBClientPara.异常" + e.ToString());
            }

            return brdv;
        }

        public BaseResultDataValue EditBBClientPara(Model.B_ClientPara jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (jsonentity == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则不能为空！";
                    return brdv;
                }
                if (jsonentity.Name == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则Name不能为空！";
                    return brdv;
                }
                if (jsonentity.ParaDesc == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaDesc不能为空！";
                    return brdv;
                }
                if (jsonentity.LabID < 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则LabID不能为空！";
                    return brdv;
                }
                if (jsonentity.ParaNo == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaNo不能为空";
                    return brdv;
                }
                if (jsonentity.ParaValue == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "规则ParaValue不能为空";
                    return brdv;
                }
                IBLL.Common.BaseDictionary.IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                if (IBBClientPara.Add(jsonentity) > 0)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "新增未成功！";
                }

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("EditBBClientPara.异常" + e.ToString());
            }

            return brdv;
        }

        public BaseResultDataValue DeleteBBClientPara(long Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (Id <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "Id不能为空！";
                    return brdv;
                }
                IBLL.Common.BaseDictionary.IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                if (IBBClientPara.Delete(Id) > 0)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "删除未成功！";
                }

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("DeleteBBClientPara.异常" + e.ToString());
            }

            return brdv;
        }

        public BaseResultDataValue SearchBBClientParaByParaNo(string ParaNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (ParaNo == null || ParaNo.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数不能为空！";
                    return brdv;
                }
                IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                List<B_ClientPara> list = IBBClientPara.SearchByParaNo(ParaNo);
                if (list != null && list.Count > 0)
                {
                    EntityListEasyUI<B_ClientPara> tmplist = new EntityListEasyUI<B_ClientPara>();
                    tmplist.rows = list;
                    tmplist.total = list.Count;
                    brdv.ResultDataValue = Common.Public.JsonHelp.JsonDotNetSerializer(tmplist);
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "为查询到相关参数列表！";
                }

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("SearchBBClientParaByParaNo.异常" + e.ToString());
            }

            return brdv;
        }

        public BaseResultDataValue SearchBBClientParaGroupByName(string Name)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                //if (Name == null || Name.Trim() == "")
                //{
                //    brdv.success = false;
                //    brdv.ErrorInfo = "参数不能为空！";
                //    return brdv;
                //}
                IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                List<B_ClientPara> list = IBBClientPara.SearchBBClientParaGroupByName(Name);
                if (list != null && list.Count > 0)
                {
                    EntityListEasyUI<B_ClientPara> tmplist = new EntityListEasyUI<B_ClientPara>();
                    tmplist.rows = list;
                    tmplist.total = list.Count;
                    brdv.ResultDataValue = Common.Public.JsonHelp.JsonDotNetSerializer(tmplist);
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "为查询到相关参数列表！";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("SearchBBClientParaGroupByName.异常" + e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue SearchBBClientParaByParaNoAndLabIDAndLabName(string ParaNo, string LabID, string LabName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (ParaNo == null || ParaNo.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "ParaNo参数不能为空！";
                    return brdv;
                }
                IBBClientPara IBBClientPara = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                List<B_ClientPara> list = IBBClientPara.SearchBBClientParaByParaNoAndLabIDAndLabName(ParaNo, LabID, LabName);
                if (list != null && list.Count > 0)
                {
                    EntityListEasyUI<B_ClientPara> tmplist = new EntityListEasyUI<B_ClientPara>();
                    tmplist.rows = list;
                    tmplist.total = list.Count;
                    brdv.ResultDataValue = Common.Public.JsonHelp.JsonDotNetSerializer(tmplist);
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "为查询到相关参数列表！";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "异常";// e.ToString();
                ZhiFang.Common.Log.Log.Error("SearchBBClientParaByParaNoAndLabIDAndLabName.异常" + e.ToString());
            }
            return brdv;
        }

        #endregion

        #region 无用&未知
        /*
        //public BaseResultDataValue CopyAllToLabs(string DicTable, string LabCodeNo)
        //{
        //    BaseResultDataValue br = new BaseResultDataValue();

        //    try
        //    {
        //        IBLL.Common.IBBatchCopy ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary." + DicTable);
        //        List<string> LabCodeLst = new List<string>();
        //        if (LabCodeNo.Trim() != "")
        //        {
        //            LabCodeLst.Add(LabCodeNo);
        //        }
        //        //LabCodeLst.Add(LabCodeNo);
        //        if (ibCopy.CopyToLab(LabCodeLst))
        //        {
        //            br.success = true;

        //        }
        //        else
        //        {
        //            br.success = false;
        //            //return br;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Debug("ZhiFang.DictionaryInfoSys.DicCenterManager.CopyAllToLabs--批量复制到客户端时出错：" + ex.ToString());
        //        br.success = false;
        //        br.ErrorInfo = ex.ToString();
        //        return br;
        //    }

        //    return br;

        //}

         /// <summary>
        /// 选择项目复制
        /// </summary>
        /// <param name="ItemNos"></param>
        /// <param name="LabCodeForm"></param>
        /// <param name="ItemKey">拼音检索</param>
        /// <returns></returns>
        public BaseResult BatchCopyItemsToLab(string ItemNos, string LabCodeNo, string ItemKey)
        {
            BaseResult br = new BaseResult();

            List<string> lst = new List<string>();
            try
            {
                if (ItemKey != null)
                {
                    DataSet ds = CenterTestItem.GetListLike(new TestItem() { CName = ItemKey, ItemNo = ItemKey, ShortCode = ItemKey, ShortName = ItemKey });

                    if (ds != null)
                    {
                        DataRowCollection drs = ds.Tables[0].Rows;
                        string tempA = string.Empty;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (tempA == string.Empty)
                            {
                                tempA += "" + LabCodeNo + "";
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    tempA += "|('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'";
                                }
                                else
                                {
                                    tempA += ",'" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'";
                                }

                            }
                        }
                        lst.Add("" + tempA + "");

                    }
                }
                else
                {
                    //string tempA = string.Empty;

                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    if (tempA == string.Empty)
                    //    {
                    //        tempA += "" + LabCodeNo + "";
                    //    }
                    //    else
                    //    {
                    //        if (i == 1)
                    //        {
                    //            tempA += "|('" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'";
                    //        }
                    //        else
                    //        {
                    //            tempA += ",'" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'";
                    //        }

                    //    }
                    //}

                    string items = LabCodeNo + "|(" + ItemNos + ")";
                    lst.Add(items);
                }

                IBLL.Common.IBBatchCopy ibCopy = null;
                if (LabCodeNo.Trim() == "")
                {
                    lst.Insert(0, "CopyToLab_LabFirstSelect#" + LabCodeNo);
                    ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary.Lab_TestItem");
                }
                else
                {
                    lst.Insert(0, "CopyToLab_LabFirstSelect");
                    ibCopy = ZhiFang.BLLFactory.BLLFactory<IBBatchCopy>.GetBLL("BaseDictionary.TestItem");
                }

                if (ibCopy.CopyToLab(lst))
                    br.success = true;
                else
                    br.success = false;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("BatchCopyItemsToLab异常", ex);
            }

            return br;

        }

        #region 配置报告查询打印
        public BaseResultDataValue SaveReportConfig(string xmlName, string selFieldName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (xmlName.Contains("Sel"))
                {
                    xmlName = "SelReportPrint";
                }
                else
                    xmlName = "ReportPrintDataList";
                string xmlData = selFieldName;
                string xmlFileName = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("xmlPath") + "\\"
                      + xmlName + ".txt";
                ZhiFang.Tools.Tools.writeStringToLocalFile(xmlFileName, xmlData);
                brdv.success = true;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;

            }
            return brdv;
        }

        public BaseResultDataValue ReadReportConfig(string xmlName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {

                if (xmlName.Contains("Sel"))
                {
                    xmlName = "SelReportPrint";
                }
                else
                    xmlName = "ReportPrintDataList";
                string xmlFileName = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("xmlPath") + "\\"
                      + xmlName + ".txt";

                StreamReader sr = new StreamReader(xmlFileName, Encoding.Default);
                string line;
                line = sr.ReadToEnd();
                sr.Close();
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(line);
                brdv.success = true;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;

            }
            return brdv;
        }
        #endregion
        */
        #endregion

        #region 客户端密钥生成服务及下载
        /// <summary>
        /// 按AES加密生成并保存密钥文件(WebLis用)
        /// 生成的密钥文件名为ZhiFangUserID
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue CreatAESEncryptFile(UIAESEntity jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            StringBuilder errorInfo = new StringBuilder();
            if (jsonentity == null)
            {
                errorInfo.Append("传入参数jsonentity的值不能为空!");
                brdv.success = false;
            }
            if (string.IsNullOrEmpty(jsonentity.UserID))
            {
                errorInfo.Append("传入参数UserID的值不能为空!");
                brdv.success = false;
            }
            if (string.IsNullOrEmpty(jsonentity.Account))
            {
                errorInfo.Append("传入参数Account的值不能为空!");
                brdv.success = false;
            }
            if (string.IsNullOrEmpty(jsonentity.LabCode))
            {
                errorInfo.Append("传入参数LabCode的值不能为空!");
                brdv.success = false;
            }

            if (brdv.success == false)
            {
                brdv.ErrorInfo = errorInfo.ToString();
                ZhiFang.Common.Log.Log.Debug("CreatAESEncryptFile:LabCode为" + jsonentity.LabCode + "的客户端密钥生成不能保存!" + brdv.ErrorInfo);
                return brdv;
            }

            string clientNo = jsonentity.LabCode;
            string msgStr = "用户帐号为:" + jsonentity.Account + ",所属单位编码为:" + jsonentity.LabCode;
            if (string.IsNullOrEmpty(jsonentity.Version))
            {
                DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
                long timeStamp = Convert.ToInt64((DateTime.Now - dateStart).TotalSeconds);
                jsonentity.Version = timeStamp.ToString();
            }
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            string lawsStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(jsonentity);
            string key = Tools.AESHelper.Password;
            ZhiFang.Common.Log.Log.Debug(msgStr + "的客户端密钥文件生成开始:被加密的明文为:" + lawsStr + ",约定密钥值为:" + key);
            string aesKey = "";
            try
            {
                aesKey = ZhiFang.Tools.AESHelper.AESEncryptOf128(lawsStr, key);
                ZhiFang.Common.Log.Log.Debug(msgStr + "的客户端密钥文件生成的加密信息为:" + aesKey + ",约定密钥值为:" + key);

                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\AESAttachment";
                string fileName = jsonentity.UserID + ".txt";
                brdv.success = FilesHelper.CreatDirFile(filePath, fileName, aesKey);
            }
            catch (Exception ee)
            {
                ZhiFang.Common.Log.Log.Error(msgStr + "的客户端密钥生成失败!" + ee.Message);
            }

            if (brdv.success)
            {
                ZhiFang.Common.Log.Log.Debug(msgStr + "的客户端密钥生成成功!");
                brdv.ResultDataValue = "{entity:" + lawsStr + "}";
            }
            else
            {
                brdv.ErrorInfo = msgStr + "的客户端密钥文件生成失败!";
                brdv.ResultDataValue = "{entity:''}";
            }
            return brdv;
        }
        /// <summary>
        /// 依fileName获取加密的密钥文件并解密还原为UIAESEntity(WebLis用)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BaseResultDataValue GetAESDecryptFileByFileName(string fileName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\AESAttachment";
            fileName = fileName + ".txt";
            string aesKey = "", decryptStr = "";
            string key = Tools.AESHelper.Password;
            if (!string.IsNullOrEmpty(filePath))
            {
                filePath = filePath + "\\" + fileName;
                if (!File.Exists(filePath))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "密钥文件不存在!";
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "密钥文件不存在!";
            }

            if (brdv.success == true)
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                aesKey = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                fs.Dispose();
                //将加密后的密文解密
                decryptStr = ZhiFang.Tools.AESHelper.AESDecryptOf128(aesKey, key);
                //ZhiFang.Common.Log.Log.Debug(fileName + "的客户端密钥解密后为:" + decryptStr);
            }
            brdv.ResultDataValue = decryptStr;//"{entity:" + decryptStr + "}";
            return brdv;
        }
        /// <summary>
        /// 依fileName下载按AES加密生成的密文文件(WebLis用)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream DownLoadAESEncryptFile(string fileName)
        {
            Stream ms = new MemoryStream();
            FileStream fileStream = null;
            //获取错误提示信息
            if (string.IsNullOrEmpty(fileName))
            {
                string errorInfo = "传入参数fileName不能为空!";
                MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                return fileStream;
            }
            try
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\AESAttachment";
                fileName = fileName + ".txt";
                if (!string.IsNullOrEmpty(filePath))
                {
                    filePath = filePath + "\\" + fileName;
                    if (!File.Exists(filePath))
                    {
                        string errorInfo = "fileName为" + fileName + "的客户端密钥文件不存在!请重新生成或联系管理员。";
                        ZhiFang.Common.Log.Log.Debug(errorInfo);
                        MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                        return fileStream;
                    }
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "fileName为" + fileName + "的客户端密钥文件不存在!请重新生成或联系管理员。";
                        ZhiFang.Common.Log.Log.Debug(errorInfo);
                        MemoryStream memoryStream = GetErrMemoryStreamInfo(errorInfo);
                        return fileStream;
                    }
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    System.Web.HttpContext.Current.Response.ContentType = "text/plain";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else {
                    string errorInfo = "文件路径为" + filePath + "不存在!";
                    ZhiFang.Common.Log.Log.Error(errorInfo);
                    MemoryStream stream = GetErrMemoryStreamInfo(errorInfo);
                    return fileStream;
                }
            }
            catch (Exception ex)
            {
                string errorInfo = "获取fileName为" + fileName + "的客户端密钥文件失败!错误信息:" + ex.StackTrace;
                ZhiFang.Common.Log.Log.Error(errorInfo);
                MemoryStream stream = GetErrMemoryStreamInfo(errorInfo);
                return fileStream;
            }
            return fileStream;
        }
        /// <summary>
        /// 获取附件文件不存在时的错误提示文件处理
        /// </summary>
        /// <param name="errorInfo">错误提示信息</param>
        /// <returns></returns>
        private MemoryStream GetErrMemoryStreamInfo(string errorInfo)
        {
            MemoryStream memoryStream = null;
            string fileName = "ErrFile.html";
            if (String.IsNullOrEmpty(errorInfo))
                errorInfo = "文件不存在!请联系管理员。";
            StringBuilder strb = new StringBuilder("<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'><h4>错误提示信息</h4><p style='color: red; padding: 5px; word -break:break-all; word - wrap:break-word; '>");
            strb.Append(errorInfo);
            strb.Append("</p></div>");
            byte[] infoByte = Encoding.UTF8.GetBytes(strb.ToString());
            memoryStream = new MemoryStream(infoByte);
            Encoding code = Encoding.GetEncoding("UTF-8");
            System.Web.HttpContext.Current.Response.ContentEncoding = code;
            //fileName = EncodeFileName.ToEncodeFileName(fileName);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html; charset=UTF-8";
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
            return memoryStream;
        }
        #endregion

    }

}




