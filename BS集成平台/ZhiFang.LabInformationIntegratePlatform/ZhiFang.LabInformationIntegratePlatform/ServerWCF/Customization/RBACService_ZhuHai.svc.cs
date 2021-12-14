using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.LabInformationIntegratePlatform.ServerContract.Customization;
using ZhiFang.LIIP.Common;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization
{
   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService_ZhuHai: RBACService, IRBACService_ZhuHai
    {
        string APP_ID = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_ID");
        string APP_KEY = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_KEY");
        string APP_SECRET = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_SECRET");
        string GetALLHospital_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetALLHospital_Url");
        string GetEmpByHospitalCode_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetEmpByHospitalCode_Url");
        string GetUserByHospitalCode_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetUserByHospitalCode_Url");
        string GetUserInfoByVerifyCode_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetUserInfoByVerifyCode_Url");

        public BaseResultDataValue GetUserInfoAndLogin(string verifyCode)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                object BHospitalDao = context.GetObject("BHospitalDao");
                object BHospitalEmpLinkDao = context.GetObject("BHospitalEmpLinkDao");
                object HREmployeeDao = context.GetObject("HREmployeeDao");
                object RBACUserDao = context.GetObject("RBACUserDao");

                IDAO.LIIP.IDBHospitalDao IDBHospitalDao = (IDAO.LIIP.IDBHospitalDao)BHospitalDao;
                IDAO.LIIP.IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao = (IDAO.LIIP.IDBHospitalEmpLinkDao)BHospitalEmpLinkDao;
                IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao = (IDAO.RBAC.IDHREmployeeDao)HREmployeeDao;
                IDAO.RBAC.IDRBACUserDao IDRBACUserDao = (IDAO.RBAC.IDRBACUserDao)RBACUserDao;

                if (string.IsNullOrEmpty(verifyCode))
                {
                    return new BaseResultDataValue() { ErrorInfo = "验证码为空！", success = false, ResultCode = "9001" };
                }

                #region 准备身份信息
                //检查票据_暂时不检查，暂时每次都获取最新，不存储票据，不验证票据是否过期

                //获取票据
                TicketResult ticketresult = TicketResult.GetTicket();

                //获取头信息
                Dictionary<string, string> headlist = Sign_Decode_Encode.CreatHeaderInfo(APP_ID, ticketresult.ticket, ticketresult.ticketSecret, APP_SECRET, APP_KEY);
                #endregion
                #region 调用服务
                //获取机构
                string postjsonstr = "{\\\"appId\\\":\\\"" + APP_ID + "\\\",\\\"verifyCode\\\":\\\"" + verifyCode + "\\\"}";
                ZhiFang.Common.Log.Log.Debug("GetUserInfoAndLogin.postjsonstr:" + postjsonstr);
                //加密
                string postjsonstr_Encode = Sign_Decode_Encode.Encode(APP_SECRET, postjsonstr, 100);
                ZhiFang.Common.Log.Log.Debug("GetUserInfoAndLogin.postjsonstr,Encode:" + postjsonstr_Encode);
                //调用服务
                string postjsonstr_encodeData = "{\"encodeData\":\"" + postjsonstr_Encode + "\"}";
                ZhiFang.Common.Log.Log.Debug("GetUserInfoAndLogin.postjsonstr_encodeData:" + postjsonstr_encodeData);
                string VerifyCodeUserResultStr = RestfullHelper.InvkerRestServicePost(postjsonstr_encodeData, "JSON", GetUserInfoByVerifyCode_Url, 100, headlist);
                ZhiFang.Common.Log.Log.Debug("GetUserInfoAndLogin.VerifyCodeUserResultStr:" + VerifyCodeUserResultStr);
                Result_ZhuHai<string> VerifyCodeUserResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Result_ZhuHai<string>>(VerifyCodeUserResultStr);

                #endregion


                if (VerifyCodeUserResult.success)
                {
                    #region 解密
                    string VerifyCodeUserResult_Decode = Sign_Decode_Encode.Decode(APP_SECRET, VerifyCodeUserResult.data, 100);
                    ZhiFang.Common.Log.Log.Debug("GetUserInfoAndLogin.VerifyCodeUserResult.Decode:" + VerifyCodeUserResult_Decode);
                    #endregion
                    //序列化
                    verifyCodeUser tmp = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<verifyCodeUser>(VerifyCodeUserResult_Decode);



                    #region 模拟登陆
                    IList<RBACUser> rbacuserList = IDRBACUserDao.GetListByHQL(" Account='" + tmp.userInfo.accountName + "' ");
                    if (rbacuserList == null || rbacuserList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetUserInfoAndLogin.未能找到用户，账户：" + tmp.userInfo.accountName);
                        brdv.success = false;
                        brdv.ErrorInfo = "未能找到用户！";
                        return brdv;
                    }
                    RBACUser rbacuser = rbacuserList.First();
                    base.SetUserSession(rbacuser);

                    brdv.success = true;
                    return brdv;
                    #endregion
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetUserInfoAndLogin.调用地第三方服务错误：verifyCode：" + verifyCode);
                    brdv.success = false;
                    brdv.ResultCode = VerifyCodeUserResult.retCode;
                    brdv.ErrorInfo = VerifyCodeUserResult.retMsg;
                    return brdv;
                }             
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetUserInfoAndLogin.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
       
        public BaseResultDataValue GetAndAddHospital(string StartTime)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = "2021-01-01";
                }
                if (!DateTime.TryParse(StartTime, out DateTime t))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数不合法！", success = false, ResultCode = "9001" };
                }

                #region 准备身份信息
                //检查票据_暂时不检查，暂时每次都获取最新，不存储票据，不验证票据是否过期

                //获取票据
                TicketResult ticketresult = TicketResult.GetTicket();

                //获取头信息
                Dictionary<string, string> headlist = Sign_Decode_Encode.CreatHeaderInfo(APP_ID, ticketresult.ticket, ticketresult.ticketSecret, APP_SECRET, APP_KEY);
                #endregion

                #region 调用服务
                //获取机构
                string postjsonstr = "{\\\"appId\\\":\\\"" + APP_ID + "\\\",\\\"pageSize\\\":1000,\\\"pageNum\\\":1,\\\"startTime\\\":\\\"" + DateTime.Parse(StartTime).ToString("yyyy-MM-dd HH:mm:ss") + "\\\"}";
                ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.postjsonstr:" + postjsonstr);
                //加密
                string postjsonstr_Encode = Sign_Decode_Encode.Encode(APP_SECRET, postjsonstr, 100);
                ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.postjsonstr,Encode:" + postjsonstr_Encode);
                //调用服务
                string postjsonstr_encodeData = "{\"encodeData\":\"" + postjsonstr_Encode + "\"}";
                ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.postjsonstr_encodeData:" + postjsonstr_encodeData);
                string hosptiallistresultstr = RestfullHelper.InvkerRestServicePost(postjsonstr_encodeData, "JSON", GetALLHospital_Url, 100, headlist);
                ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.hosptiallistresultstr:" + hosptiallistresultstr);
                Result_ZhuHai<string> hosptiallistresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Result_ZhuHai<string>>(hosptiallistresultstr);
                #endregion

                if (hosptiallistresult.success)
                {
                    //解密
                    string hosptiallistresult_Decode = Sign_Decode_Encode.Decode(APP_SECRET, hosptiallistresult.data, 100);
                    ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.hosptiallistresult.Decode:" + hosptiallistresult_Decode);
                    //序列化
                    PageResult_ZhuHai<hospital> tmp = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PageResult_ZhuHai<hospital>>(hosptiallistresult_Decode);

                    #region 保存
                    if (tmp != null && tmp.list != null && tmp.list.Count > 0)
                    {
                        IApplicationContext context = ContextRegistry.GetContext();
                        object BHospitalDao = context.GetObject("BHospitalDao");
                        IDAO.LIIP.IDBHospitalDao IDBHospitalDao = (IDAO.LIIP.IDBHospitalDao)BHospitalDao;

                        for (int i = 0; i < tmp.list.Count; i++)
                        {
                            int tmpcount = IDBHospitalDao.GetListCountByHQL(" HospitalCode='" + tmp.list[i].tenantCode + "'");
                            if (tmpcount > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug($"GetAndAddHospital.hosptiallistresult.机构已存在！tenantCode:{tmp.list[i].tenantCode},tenantId:{tmp.list[i].tenantId},tenantName:{tmp.list[i].tenantName}");
                                List<string> para = new List<string>();
                                para.Add("Name='" + tmp.list[i].tenantName + "' ");
                                para.Add("Shortcode='" + tmp.list[i].tenantId + "' ");
                                if (tmp.list[i].disabled == 1)
                                    para.Add("IsUse=0 ");
                                else
                                    para.Add("IsUse=1 ");

                                para.Add("Address='" + tmp.list[i].tenantAddress + "' ");
                                para.Add("LinkMan='" + tmp.list[i].adminName + "' ");

                                if (DateTime.TryParse(tmp.list[i].addDate, out var addtime))
                                {
                                    para.Add("DataAddTime='" + addtime.ToString("yyyy-MM-dd HH:mm:ss") + "' "); ;
                                }
                                if (DateTime.TryParse(tmp.list[i].modifyDate, out var edittime))
                                {
                                    para.Add("DataUpdateTime='" + edittime.ToString("yyyy-MM-dd HH:mm:ss") + "' "); ;
                                }
                                para.Add("LevelName='" + tmp.list[i].gradeName + "' ");
                                string updatehql = " update BHospital set  " + string.Join(",", para) + "  where HospitalCode='" + tmp.list[i].tenantCode + "'";
                                ZhiFang.Common.Log.Log.Debug("updatehql:" + updatehql);
                                brdv.success = (IDBHospitalDao.UpdateByHql(updatehql) > 0);
                            }
                            else
                            {
                                BHospital entity = new BHospital();
                                entity.Name = tmp.list[i].tenantName;
                                entity.HospitalCode = tmp.list[i].tenantCode;
                                entity.Shortcode = tmp.list[i].tenantId;
                                entity.IsUse = tmp.list[i].disabled == 1 ? false : true;
                                entity.Address = tmp.list[i].tenantAddress;
                                entity.LinkMan = tmp.list[i].adminName;
                                if (DateTime.TryParse(tmp.list[i].addDate, out var addtime))
                                {
                                    entity.DataAddTime = addtime;
                                }
                                if (DateTime.TryParse(tmp.list[i].modifyDate, out var edittime))
                                {
                                    entity.DataUpdateTime = edittime;
                                }
                                entity.LevelName = tmp.list[i].gradeName;

                                //固定值
                                entity.AreaID = 1;
                                entity.AreaCode = "1001";
                                entity.AreaName = "珠海区域";
                                //
                                if (IDBHospitalDao.Save(entity))
                                {
                                    ZhiFang.Common.Log.Log.Debug($"GetAndAddHospital.hosptiallistresult.机构保存成功！tenantCode:{tmp.list[i].tenantCode},tenantId:{tmp.list[i].tenantId},tenantName:{tmp.list[i].tenantName}");
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug($"GetAndAddHospital.hosptiallistresult.机构保存失败！tenantCode:{tmp.list[i].tenantCode},tenantId:{tmp.list[i].tenantId},tenantName:{tmp.list[i].tenantName}");
                                }
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("GetAndAddHospital.hosptiallistresult.获取机构数据为空！");
                        brdv.success = false;
                        brdv.ErrorInfo = "获取机构数据为空！";
                        return brdv;
                    }

                    #endregion
                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    brdv.success = hosptiallistresult.success;
                    brdv.ResultCode = hosptiallistresult.retCode;
                    brdv.ErrorInfo = hosptiallistresult.retMsg;
                    return brdv;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetAndAddHospital.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
       
        public BaseResultDataValue GetAndAddEmp_UserByHospitalCode(string HospitalCode, string StartTime)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            try
            {
                if (string.IsNullOrEmpty(HospitalCode))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数为空！", success = false, ResultCode = "9001" };
                }

                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = "2021-01-01";
                }

                if (!DateTime.TryParse(StartTime, out DateTime t))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数不合法！", success = false, ResultCode = "9001" };
                }

                #region 准备身份信息
                //检查票据_暂时不检查，暂时每次都获取最新，不存储票据，不验证票据是否过期

                //获取票据
                TicketResult ticketresult = TicketResult.GetTicket();
                //获取头信息
                Dictionary<string, string> headlist = Sign_Decode_Encode.CreatHeaderInfo(APP_ID, ticketresult.ticket, ticketresult.ticketSecret, APP_SECRET, APP_KEY);
                #endregion

                //获取所有机构
                IApplicationContext context = ContextRegistry.GetContext();
                object BHospitalDao = context.GetObject("BHospitalDao");
                object BHospitalEmpLinkDao = context.GetObject("BHospitalEmpLinkDao");
                object HREmployeeDao = context.GetObject("HREmployeeDao");
                object RBACUserDao = context.GetObject("RBACUserDao");

                IDAO.LIIP.IDBHospitalDao IDBHospitalDao = (IDAO.LIIP.IDBHospitalDao)BHospitalDao;
                IDAO.LIIP.IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao = (IDAO.LIIP.IDBHospitalEmpLinkDao)BHospitalEmpLinkDao;
                IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao = (IDAO.RBAC.IDHREmployeeDao)HREmployeeDao;
                IDAO.RBAC.IDRBACUserDao IDRBACUserDao = (IDAO.RBAC.IDRBACUserDao)RBACUserDao;
                List<HREmployee> emplist = new List<HREmployee>();
                List<RBACUser> userlist = new List<RBACUser>();

                var tmpemplist = IDHREmployeeDao.GetListByHQL(" 1=1 ");
                var tmpuserlist = IDRBACUserDao.GetListByHQL(" 1=1 ");

                if (tmpemplist != null)
                    emplist = tmpemplist.ToList();

                if (tmpuserlist != null)
                    userlist = tmpuserlist.ToList();

                IList<BHospital> hospitallist = IDBHospitalDao.GetListByHQL(" IsUse=1 ");
                if (hospitallist != null && hospitallist.Count > 0)
                {
                    hospitallist.ToList().ForEach(hospital =>
                    {
                        if (!string.IsNullOrEmpty(hospital.Shortcode))
                        {
                            var linkemplist = IDBHospitalEmpLinkDao.GetListByHQL(" HospitalCode='" + hospital.HospitalCode + "' ");
                            //List<HREmployee> emplist = new List<HREmployee>();
                            //List<RBACUser> userlist = new List<RBACUser>();
                            //if (linkemplist != null && linkemplist.Count > 0)
                            //{
                            //    List<long> empidlist = new List<long>();
                            //    linkemplist.ToList().ForEach(b =>
                            //    {
                            //        empidlist.Add(b.EmpID.Value);
                            //    });
                            //    var tmpemplist = IDHREmployeeDao.GetListByHQL(" Id in (" + string.Join(",", empidlist) + ") ").ToList();
                            //    if (tmpemplist != null && tmpemplist.Count > 0)
                            //        emplist = tmpemplist.ToList();

                            //    var tmpuserlist = IDRBACUserDao.GetListByHQL(" HREmployee.Id in (" + string.Join(",", empidlist) + ") ").ToList();
                            //    if (tmpuserlist != null && tmpuserlist.Count > 0)
                            //        userlist = tmpuserlist.ToList();

                            //}

                            #region 获取用户
                            PageResult_ZhuHai<User> tmpuserlist_zhuhai = new PageResult_ZhuHai<User>();
                            string postjsonstr_user = "{\\\"appId\\\":\\\"" + APP_ID + "\\\",\\\"tenantId\\\":\\\"" + hospital.Shortcode + "\\\",\\\"pageSize\\\":1000,\\\"pageNum\\\":1,\\\"startTime\\\":\\\"" + DateTime.Parse(StartTime).ToString("yyyy-MM-dd HH:mm:ss") + "\\\"}";
                            ZhiFang.Common.Log.Log.Debug($"GetAndAddEmp_UserByHospitalCode.postjsonstr_user:{postjsonstr_user}");
                            //加密
                            string postjsonstr_user_Encode = Sign_Decode_Encode.Encode(APP_SECRET, postjsonstr_user, 100);
                            ZhiFang.Common.Log.Log.Debug($"GetAndAddEmp_UserByHospitalCode.postjsonstr_user,Encode:{postjsonstr_user_Encode}");
                            //调用服务
                            string postjsonstr_user_encodeData = "{\"encodeData\":\"" + postjsonstr_user_Encode + "\"}";
                            ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.postjsonstr_user_encodeData:" + postjsonstr_user_encodeData);
                            string userlistresultstr = RestfullHelper.InvkerRestServicePost(postjsonstr_user_encodeData, "JSON", GetUserByHospitalCode_Url, 100, headlist);
                            ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.userlistresultstr:" + userlistresultstr);
                            Result_ZhuHai<string> userlistresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Result_ZhuHai<string>>(userlistresultstr);

                            if (userlistresult.success)
                            {
                                //解密
                                string userlistresult_Decode = Sign_Decode_Encode.Decode(APP_SECRET, userlistresult.data, 100);
                                ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.userlistresult_Decode:" + userlistresult_Decode);
                                //序列化
                                tmpuserlist_zhuhai = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PageResult_ZhuHai<User>>(userlistresult_Decode);

                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.获取用户列表失败！retCode:{userlistresult.retCode},retMsg:{userlistresult.retMsg},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                            }
                            #endregion

                            #region 获取员工
                            PageResult_ZhuHai<Emp> tmpemplist_zhuhai = new PageResult_ZhuHai<Emp>();
                            string postjsonstr_Emp = "{\\\"appId\\\":\\\"" + APP_ID + "\\\",\\\"tenantId\\\":\\\"" + hospital.Shortcode + "\\\",\\\"pageSize\\\":1000,\\\"pageNum\\\":1,\\\"startTime\\\":\\\"" + DateTime.Parse(StartTime).ToString("yyyy-MM-dd HH:mm:ss") + "\\\"}";
                            ZhiFang.Common.Log.Log.Debug($"GetAndAddEmp_UserByHospitalCode.postjsonstr_Emp:{postjsonstr_Emp}");
                            //加密
                            string postjsonstr_Emp_Encode = Sign_Decode_Encode.Encode(APP_SECRET, postjsonstr_Emp, 100);
                            ZhiFang.Common.Log.Log.Debug($"GetAndAddEmp_UserByHospitalCode.postjsonstr_Emp_Encode:{postjsonstr_Emp_Encode}");
                            //调用服务
                            string postjsonstr_Emp_encodeData = "{\"encodeData\":\"" + postjsonstr_Emp_Encode + "\"}";
                            ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.postjsonstr_Emp_encodeData:" + postjsonstr_Emp_encodeData);
                            string emplistresultstr = RestfullHelper.InvkerRestServicePost(postjsonstr_Emp_encodeData, "JSON", GetEmpByHospitalCode_Url, 100, headlist);
                            ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.emplistresultstr:" + emplistresultstr);
                            Result_ZhuHai<string> emplistresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Result_ZhuHai<string>>(emplistresultstr);

                            if (emplistresult.success)
                            {
                                //解密
                                string emplistresult_Decode = Sign_Decode_Encode.Decode(APP_SECRET, emplistresult.data, 100);
                                ZhiFang.Common.Log.Log.Debug("GetAndAddEmp_UserByHospitalCode.emplistresult_Decode:" + emplistresult_Decode);
                                //序列化
                                tmpemplist_zhuhai = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<PageResult_ZhuHai<Emp>>(emplistresult_Decode);
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.获取员工列表失败！retCode:{emplistresult.retCode},retMsg:{emplistresult.retMsg},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                            }
                            #endregion

                            if (tmpuserlist_zhuhai != null && tmpuserlist_zhuhai.list != null && tmpuserlist_zhuhai.list.Count > 0)
                            {
                                if (tmpemplist_zhuhai != null && tmpemplist_zhuhai.list != null && tmpemplist_zhuhai.list.Count > 0)//员工和用户是否保持一致？
                                {
                                    foreach (var user in tmpuserlist_zhuhai.list)
                                    {
                                        if (tmpemplist_zhuhai.list.Count(tmpemp => tmpemp.userId == user.userId) > 0)//员工和用户是否保持一致？
                                        {
                                            var tmpemp_zhuhai = tmpemplist_zhuhai.list.Where(tmpemp => tmpemp.userId == user.userId).First();
                                            if (tmpemp_zhuhai.employeeCode.IndexOf("and") < 0 && tmpemp_zhuhai.employeeCode.IndexOf("=") < 0)
                                            {
                                                //检测是否存在
                                                if (emplist.Count(emp => emp.UseCode == tmpemp_zhuhai.employeeId) > 0)//UseCode存放employeeId,Shortcode存employeeCode
                                                {
                                                    HREmployee tmpemp = emplist.Where(emp => emp.UseCode == tmpemp_zhuhai.employeeId).First();
                                                    if (DateTime.TryParse(tmpemp_zhuhai.modifyDate, out var tmpmodifydate))
                                                    {
                                                        if (!tmpemp.DataUpdateTime.HasValue || tmpmodifydate > tmpemp.DataUpdateTime)
                                                        {
                                                            //更新
                                                            IDHREmployeeDao.UpdateByHql(" update HREmployee set  CName='" + tmpemp_zhuhai.employeeName + "'  where UseCode='" + tmpemp_zhuhai.employeeId + "'");
                                                        }
                                                    }
                                                    else
                                                        ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.保存员工失败更新日期为空！employeeId:{tmpemp_zhuhai.employeeId},employeeName:{tmpemp_zhuhai.employeeName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                                }
                                                else
                                                {
                                                    //新增
                                                    HREmployee emp = new HREmployee();
                                                    emp.UseCode = tmpemp_zhuhai.employeeId; //UseCode存放employeeId,
                                                    emp.Shortcode = tmpemp_zhuhai.employeeCode;//Shortcode存employeeCode
                                                    emp.NameF = tmpemp_zhuhai.employeeName;
                                                    emp.NameL = tmpemp_zhuhai.employeeName;
                                                    emp.CName = tmpemp_zhuhai.employeeName;
                                                    emp.IsUse = tmpemp_zhuhai.disabled == 1 ? false : true;
                                                    emp.IsEnabled = tmpemp_zhuhai.disabled == 1 ? 0 : 1;
                                                    if (DateTime.TryParse(tmpemp_zhuhai.addDate, out var d1))
                                                    {
                                                        emp.DataAddTime = d1;
                                                    }
                                                    if (DateTime.TryParse(tmpemp_zhuhai.modifyDate, out var d2))
                                                    {
                                                        emp.DataUpdateTime = d2;
                                                    }
                                                    emp.HRDept = new HRDept() { Id = 5748186376948619101, DataTimeStamp = arrDataTimeStamp };
                                                    if (IDHREmployeeDao.Save(emp))
                                                    {
                                                        ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.保存员工成功！employeeId:{tmpemp_zhuhai.employeeId},employeeName:{tmpemp_zhuhai.employeeName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");

                                                        RBACUser rbacuser = new RBACUser();
                                                        rbacuser.UseCode = user.userId;  //UseCode存放userId
                                                        rbacuser.Account = user.accountName;
                                                        rbacuser.PWD = "694CDA783583A8A1";
                                                        rbacuser.EnMPwd = true;
                                                        rbacuser.PwdExprd = true;
                                                        rbacuser.AccExprd = false;
                                                        rbacuser.AccLock = false;
                                                        rbacuser.AuUnlock = 0;
                                                        rbacuser.CName = emp.CName;
                                                        rbacuser.Shortcode = user.nickName;//Shortcode存nickName
                                                        rbacuser.HREmployee = new HREmployee() { Id = emp.Id, DataTimeStamp = arrDataTimeStamp };
                                                        rbacuser.IsUse = tmpemp_zhuhai.delFlag == 1 ? false : true;
                                                        if (IDRBACUserDao.Save(rbacuser))
                                                        {
                                                            ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.保存用户成功！userId:{user.userId},accountName:{user.accountName},employeeId:{tmpemp_zhuhai.employeeId},employeeName:{tmpemp_zhuhai.employeeName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                                        }
                                                        else
                                                        {
                                                            IDHREmployeeDao.Delete(emp.Id);
                                                            ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.保存用户失败！userId:{user.userId},accountName:{user.accountName},employeeId:{tmpemp_zhuhai.employeeId},employeeName:{tmpemp_zhuhai.employeeName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");

                                                        }

                                                        //保存医院员工关系
                                                    }
                                                    else
                                                        ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.保存员工失败！employeeId:{tmpemp_zhuhai.employeeId},employeeName:{tmpemp_zhuhai.employeeName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                                }
                                            }
                                            else
                                            {
                                                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.数据中存在非法字符！UserID:{user.userId},realName:{user.realName},accountName:{user.accountName},nickName:{user.nickName},employeeCode:{tmpemp_zhuhai.employeeCode},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                            }
                                        }
                                        else
                                        {
                                            ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.未能找到UserID:{user.userId}的员工！realName:{user.realName},accountName:{user.accountName},nickName:{user.nickName},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                        }
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.获取员工列表为空！retCode:{userlistresult.retCode},retMsg:{userlistresult.retMsg},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                                }

                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.获取用户列表为空！retCode:{userlistresult.retCode},retMsg:{userlistresult.retMsg},Name:{hospital.Name},Shortcode:{hospital.Shortcode},HospitalCode:{hospital.HospitalCode}");
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". GetAndAddEmp_UserByHospitalCode.Shortcode为空！Name:{hospital.Name},HospitalCode:{hospital.HospitalCode}");
                        }
                    });

                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetAndAddEmp_UserByHospitalCode.在用医疗机构为空！");
                    brdv.success = false;
                    brdv.ErrorInfo = "在用医疗机构为空！";
                    return brdv;
                }



                
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". GetAndAddEmp_UserByHospitalCode.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue ExceptionInfoTest()
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                throw new Exception("异常测试！");
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ExceptionInfoTest.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
    }
    public class Result_ZhuHai<T>
    {
        public string retCode { get; set; }
        private string _retMsg = "";
        public string retMsg
        {
            get
            {
                if (string.IsNullOrEmpty(_retMsg))
                {
                    switch (retCode)
                    {
                        case "999999": return "成功";
                        case "GW0000": return "服务器开小差，请稍后重试";
                        case "GW0001": return "应用未注册";
                        case "GW0002": return "参数错误";
                        case "GW0003": return "ticket已失效";
                        case "GW0004": return "签名校验不通过";
                        case "GW0005": return "该接口无访问权限";
                        case "GW0006": return "接口404 NOT FOUND";
                        case "GW0007": return "refreshTicket已失效";
                        default: return "";
                    }
                }
                else
                {
                    return _retMsg;
                }
            }
            set
            {
                _retMsg = value;
            }
        }
        public bool success { get; set; }
        public T data { get; set; }
    }
    public class PageResult_ZhuHai<T>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public int pages { get; set; }
        public int total { get; set; }

        public List<T> list { get; set; }
    }
    public class hospital
    {
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public string tenantCode { get; set; }
        public string realCode { get; set; }
        public int organizationType { get; set; }
        public string gradeName { get; set; }
        public string adminUser { get; set; }
        public string adminName { get; set; }
        public string devisionType { get; set; }
        public string provinceCode { get; set; }
        public string cityCode { get; set; }
        public string districtCode { get; set; }
        public string tenantAddress { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public int disabled { get; set; }
        public int delFlag { get; set; }
        public string addDate { get; set; }
        public string modifyDate { get; set; }
    }
    public class User
    {
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public string userId { get; set; }
        public string accountName { get; set; }
        public string realName { get; set; }
        public int gender { get; set; }
        public string nickName { get; set; }
        public string headerIcon { get; set; }
        public string isAdmin { get; set; }
        public int disabled { get; set; }
        public int delFlag { get; set; }
        public string addDate { get; set; }
        public string modifyDate { get; set; }
    }

    public class verifyCodeUser
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public int accessExpireDate { get; set; }
        public int refreshExpireDate { get; set; }
        public verifyCodeUserInfo userInfo { get; set; }
    }
    public class verifyCodeUserInfo
    {
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public string userId { get; set; }
        public string accountName { get; set; }
        public string realName { get; set; }
        public int gender { get; set; }
        public string nickName { get; set; }
    }
    public class Emp
    {
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int gender { get; set; }
        public string postName { get; set; }
        public string postCode { get; set; }
        public string titleCode { get; set; }
        public string titleName { get; set; }
        public string headerIcon { get; set; }
        public string userId { get; set; }
        public string accountName { get; set; }
        public string departmentId { get; set; }
        public string departmentName { get; set; }
        public int disabled { get; set; }
        public int delFlag { get; set; }
        public string addDate { get; set; }
        public string modifyDate { get; set; }
    }
    public class TicketResult
    {
        public string refreshTicket { get; set; }
        public string ticket { get; set; }
        public string ticketSecret { get; set; }

        public static TicketResult GetTicket()
        {
            string GetTicket_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("GetTicket_Url");
            string APP_ID = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_ID");
            string APP_KEY = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_KEY");
            string APP_SECRET = ZhiFang.Common.Public.ConfigHelper.GetConfigString("APP_SECRET");
            string ticketdataResult = RestfullHelper.InvkerRestServicePost("{\"appId\":\"" + APP_ID + "\",\"appSecret\":\"" + APP_SECRET + "\"}", "JSON", GetTicket_Url, 100);
            ZhiFang.Common.Log.Log.Debug("GetTicket.ticketdataResult:" + ticketdataResult);
            Result_ZhuHai<string> tmp = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<Result_ZhuHai<string>>(ticketdataResult);
            string ticketdata = Sign_Decode_Encode.Decode(APP_SECRET, tmp.data, 100);
            ZhiFang.Common.Log.Log.Debug("TicketResult.GetTicket.ticketdata:" + ticketdata);

            TicketResult ticketresult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<TicketResult>(ticketdata);
            return ticketresult;
        }
    }
    public class Sign_Decode_Encode
    {
        public static string Sign(string SignType, string data, int timeout)
        {
            string ZhuHai_Sign_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhuHai_Sign_Url");
            string jsonstr = "{\"appSecret\":\"" + SignType + "\",\"data\":\"" + data + "\"}";
            ZhiFang.Common.Log.Log.Debug("Sign.ZhuHai_Sign_Url：" + ZhuHai_Sign_Url + ",jsonstr:" + jsonstr);
            string SignResult = RestfullHelper.InvkerRestServicePost(jsonstr, "JSON", ZhuHai_Sign_Url, 100);
            ZhiFang.Common.Log.Log.Debug("Sign.SignResult：" + SignResult);
            return SignResult;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="appSecret"></param>
        /// <param name="Data"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Decode(string appSecret, string Data, int timeout)
        {
            string ZhuHai_Decode_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhuHai_Decode_Url");
            string jsonstr = "{\"appSecret\":\"" + appSecret + "\",\"data\":\"" + Data + "\"}";
            ZhiFang.Common.Log.Log.Debug("Decode.ZhuHai_Decode_Url：" + ZhuHai_Decode_Url + ",jsonstr:" + jsonstr);
            string DecodeResult = RestfullHelper.InvkerRestServicePost(jsonstr, "JSON", ZhuHai_Decode_Url, timeout);
            ZhiFang.Common.Log.Log.Debug("Decode.DecodeResult：" + DecodeResult);
            return DecodeResult;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="PostData"></param>
        /// <param name="DataType"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Encode(string appSecret, string Data, int timeout)
        {
            string ZhuHai_Encode_Url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ZhuHai_Encode_Url");
            string jsonstr = "{\"appSecret\":\"" + appSecret + "\",\"data\":\"" + Data + "\"}";
            ZhiFang.Common.Log.Log.Debug("Encode.ZhuHai_Encode_Url：" + ZhuHai_Encode_Url + ",jsonstr:" + jsonstr);
            string EncodeResult = RestfullHelper.InvkerRestServicePost(jsonstr, "JSON", ZhuHai_Encode_Url, timeout);
            ZhiFang.Common.Log.Log.Debug("Encode.EncodeResult：" + EncodeResult);
            return EncodeResult;
        }

        public static Dictionary<string, string> CreatHeaderInfo(string APP_ID, string ticket, string ticketSecret, string APP_SECRET, string APP_KEY, string signType = "MD5")
        {
            //准备头信息和签名
            string timestamp = DateTimeHelp.DateTimeToTimeStamp(DateTime.Now).ToString();
            string signcontext = APP_ID + ticket + ticketSecret + timestamp + APP_SECRET + APP_KEY + signType;
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.APP_ID:" + APP_ID);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.ticket:" + ticket);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.ticketSecret:" + ticketSecret);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.timestamp:" + timestamp);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.APP_SECRET:" + APP_SECRET);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.APP_KEY:" + APP_KEY);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr.signType:" + signType);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signcontext:" + signcontext);
            string signstr = Sign_Decode_Encode.Sign(signType, signcontext, 100);
            ZhiFang.Common.Log.Log.Debug("CreatHeaderInfo.signstr:" + signstr);
            Dictionary<string, string> headlist = new Dictionary<string, string>();
            headlist.Add("requestId", ZhiFang.Common.Public.GUIDHelp.GetGUIDString());
            headlist.Add("timeStamp", timestamp);
            headlist.Add("appId", APP_ID);
            headlist.Add("sign", signstr);
            headlist.Add("signType", signType);
            headlist.Add("ticket", ticket);
            return headlist;
        }
    }
    public static class retCodeDic
    {
        public static KeyValuePair<string, BaseClassDicEntity> 成功 = new KeyValuePair<string, BaseClassDicEntity>("999999", new BaseClassDicEntity() { Id = "999999", Name = "成功", Code = "999999", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 服务器开小差_请稍后重试 = new KeyValuePair<string, BaseClassDicEntity>("GW0000", new BaseClassDicEntity() { Id = "GW0000", Name = "服务器开小差_请稍后重试", Code = "GW0000", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 应用未注册 = new KeyValuePair<string, BaseClassDicEntity>("GW0001", new BaseClassDicEntity() { Id = "GW0001", Name = "应用未注册", Code = "GW0001", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 参数错误 = new KeyValuePair<string, BaseClassDicEntity>("GW0002", new BaseClassDicEntity() { Id = "GW0002", Name = "参数错误", Code = "GW0002", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> ticket已失效 = new KeyValuePair<string, BaseClassDicEntity>("GW0003", new BaseClassDicEntity() { Id = "GW0003", Name = "ticket已失效", Code = "GW0003", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 签名校验不通过 = new KeyValuePair<string, BaseClassDicEntity>("GW0004", new BaseClassDicEntity() { Id = "GW0004", Name = "签名校验不通过", Code = "GW0004", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 该接口无访问权限 = new KeyValuePair<string, BaseClassDicEntity>("GW0005", new BaseClassDicEntity() { Id = "GW0005", Name = "该接口无访问权限", Code = "GW0005", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 接口404 = new KeyValuePair<string, BaseClassDicEntity>("GW0006", new BaseClassDicEntity() { Id = "GW0006", Name = "接口404", Code = "GW0006", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> refreshTicket已失效 = new KeyValuePair<string, BaseClassDicEntity>("GW0007", new BaseClassDicEntity() { Id = "GW0007", Name = "refreshTicket已失效", Code = "GW0007", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(成功.Key, 成功.Value);
            dic.Add(服务器开小差_请稍后重试.Key, 服务器开小差_请稍后重试.Value);
            dic.Add(应用未注册.Key, 应用未注册.Value);
            dic.Add(参数错误.Key, 参数错误.Value);
            dic.Add(ticket已失效.Key, ticket已失效.Value);
            dic.Add(签名校验不通过.Key, 签名校验不通过.Value);
            dic.Add(该接口无访问权限.Key, 该接口无访问权限.Value);
            dic.Add(接口404.Key, 接口404.Value);
            dic.Add(refreshTicket已失效.Key, refreshTicket已失效.Value);
            return dic;
        }
    }
}
