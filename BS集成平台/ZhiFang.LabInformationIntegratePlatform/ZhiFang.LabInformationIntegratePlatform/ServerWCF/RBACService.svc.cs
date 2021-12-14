using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.LabInformationIntegratePlatform.BusinessObject;
using ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : RBACServiceCommon, ServerContract.IRBACService
    {
        public IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink { get; set; }
        public IBLL.LIIP.IBBHospitalEmpLink IBBHospitalEmpLink { get; set; }

        public BaseResultDataValue RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID(string id)
        {
            throw new NotImplementedException();
        }

        public BaseResultDataValue RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID()
        {
            throw new NotImplementedException();
        }

        #region 电子签名图片处理
        /// <summary>
        /// 上传电子签名图片
        /// </summary>
        /// <returns></returns>
        public Message SC_UDTO_UploadEmpSignByEmpId()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            brdv.ResultDataValue = "{id:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到电子签名图片信息！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //员工Id
                string empId = HttpContext.Current.Request.Form["EmpId"];
                if (!long.TryParse(empId, out var empid))
                {
                    brdv.ErrorInfo = "员工Id值非法！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }

                HREmployee emp = new HREmployee();
                emp.Id = empid;

                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                if (brdv.success && fileExt.ToLower() != ".png")
                {
                    brdv.ErrorInfo = "上传的图片格式需是png！";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }

                string fileName = empId + ".png";

                if (brdv.success && file != null && !string.IsNullOrEmpty(fileName))
                {
                    //上传电子签名保存路径
                    string parentPath = System.AppDomain.CurrentDomain.BaseDirectory + SystemFilePath.EmpImages + "/";

                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);

                    string filepath = Path.Combine(parentPath, fileName);
                    file.SaveAs(filepath);

                    byte[] buffer = new byte[file.InputStream.Length];
                    int offset = 0;
                    int cnt = 0;
                    while ((cnt = file.InputStream.Read(buffer, offset, 10)) > 0)
                    {
                        offset += cnt;
                    }

                    string base64 = Convert.ToBase64String(buffer);
                    //string ByteStr = System.Text.Encoding.UTF8.GetString(buffer);
                    //ZhiFang.Common.Log.Log.Debug(System.Text.Encoding.UTF8.GetString(buffer));
                    //ZhiFang.Common.Log.Log.Debug(Convert.ToBase64String(buffer));
                    emp.SignatureImage = base64;
                    IBHREmployee.TUpdate(emp);
                }
            }
            catch (Exception ex)
            {
                Log.Error("上传电子签名图片错误:" + ex.ToString());
                brdv.ErrorInfo = "上传电子签名图片异常!";
                brdv.ResultDataValue = "{id:''}";
                brdv.success = false;
            }

            string strResult = Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载电子签名图片
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>       
        public Stream PGM_UDTO_DownLoadEmpSignByEmpId(long empId, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                //上传电子签名保存路径
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + SystemFilePath.EmpImages + "/";
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = empId + ".png";

                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");

                    }

                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                Common.Log.Log.Error("电子签名图片下载错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }

        /// <summary>
        /// 获得电子签名的bate64字符串 根据EmpId
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public BaseResultDataValue GetEmpSignatureImageByEmpID(long EmpId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + "/" + SystemFilePath.EmpImages + "/" + EmpId + ".png";
                if (File.Exists(filepath))
                {
                    System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(filepath);
                    MemoryStream ms1 = new MemoryStream();
                    bmp2.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arr1 = new byte[ms1.Length];
                    ms1.Position = 0;
                    ms1.Read(arr1, 0, (int)ms1.Length);
                    ms1.Close();
                    baseResultDataValue.ResultDataValue = Convert.ToBase64String(arr1);
                    baseResultDataValue.success = true;
                    bmp2.Dispose();
                }
                else
                {
                    baseResultDataValue.success = false;
                }

            }
            catch (Exception e)
            {
                baseResultDataValue.ErrorInfo = e.Message;
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Debug("CommonService.svc.GetEmpElectronicSignatureByEmpID:" + e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 危急值

        public BaseResultDataValue CV_AddDoctorOrNurse(CV_AddDoctorOrNurseVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                RBACUser rbacuser;
                brdv = IBCVCriticalValueEmpIdDeptLink.CV_SearchAndAddDoctorOrNurse(entity, out rbacuser);
                if (brdv.success)
                {
                    base.SetUserSession(rbacuser);
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBACService.CV_AddDoctorOrNurse:异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！" + e.Message;
            }
            return brdv;
        }
        public BaseResultDataValue CV_AddTech(CV_AddTechVO entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                RBACUser rbacuser;
                brdv = IBCVCriticalValueEmpIdDeptLink.CV_SearchAndAddTech(entity, out rbacuser);
                if (brdv.success)
                {
                    base.SetUserSession(rbacuser);
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBACService.CV_AddDoctorOrNurse:异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！" + e.Message;
            }
            return brdv;
        }

        #endregion

        #region 权限服务

        public BaseResultDataValue RBAC_RJ_GetSubDeptIdListByDeptId(string id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long tempID;
                if (id != null && long.TryParse(id, out tempID))
                {
                    IList<long> list = IBHRDept.GetSubDeptIdListByDeptId(tempID);
                    if (list != null && list.Count > 0)
                        baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(list);
                }
                else
                {
                    baseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_RJ_GetSubDeptIdListByDeptId：未能找到数据！Id=" + id);
                    baseResultDataValue.ErrorInfo = "未能找到数据！Id = " + id;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_RJ_GetSubDeptIdListByDeptId,异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RBAC_RJ_GetParentDeptIdListByDeptId(string id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long tempID;
                if (id != null && long.TryParse(id, out tempID))
                {
                    IList<long> hrdeptList = base.IBHRDept.GetParentDeptIdListByDeptId(tempID);
                    if (hrdeptList != null && hrdeptList.Count > 0)
                    {
                        tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(hrdeptList);
                        tempBaseResultDataValue.success = true;
                        return tempBaseResultDataValue;
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        ZhiFang.Common.Log.Log.Debug("RBAC_RJ_GetParentDeptIdListByDeptId：未能找到数据！Id=" + id);
                        tempBaseResultDataValue.ErrorInfo = "未能找到数据！Id = " + id;
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_RJ_GetParentDeptIdListByDeptId,参数错误！");
                    tempBaseResultDataValue.ErrorInfo = "参数错误！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_RJ_GetParentDeptIdListByDeptId,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_GetRBACRolesListByEmpId(string id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long tmpid;
                if (id != null && long.TryParse(id, out tmpid))
                {
                    IList<RBACRole> rbacrolelist = base.IBRBACRole.SearchRoleByHREmpID(tmpid);
                    if (rbacrolelist != null && rbacrolelist.Count > 0)
                    {
                        tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacrolelist);
                        tempBaseResultDataValue.success = true;
                        return tempBaseResultDataValue;
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACRolesListByEmpId：未能找到数据！Id=" + id);
                        tempBaseResultDataValue.ErrorInfo = "未能找到数据！Id = " + id;
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACRolesListByEmpId,参数错误！");
                    tempBaseResultDataValue.ErrorInfo = "参数错误！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACRolesListByEmpId,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_GetRBACEmpRolesListByRoleIdList(string idlist)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (idlist != null && idlist.Trim() != "")
                {
                    IList<RBACEmpRoles> rbacemprolesList = base.IBRBACEmpRoles.GetRBACEmpRolesListByRoleIdList(idlist);
                    if (rbacemprolesList != null && rbacemprolesList.Count > 0)
                    {
                        tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(rbacemprolesList);
                        tempBaseResultDataValue.success = true;
                        return tempBaseResultDataValue;
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACEmpRolesListByRoleIdList：未能找到数据！idlist=" + idlist);
                        tempBaseResultDataValue.ErrorInfo = "未能找到数据！idlist = " + idlist;
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACEmpRolesListByRoleIdList,参数错误！");
                    tempBaseResultDataValue.ErrorInfo = "参数错误！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetRBACEmpRolesListByRoleIdList,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_GetHREmployeeAllList()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<HREmployee> empList = base.IBHREmployee.GetHREmployeeAllList();
                if (empList != null && empList.Count > 0)
                {
                    tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(empList);
                    tempBaseResultDataValue.success = true;
                    return tempBaseResultDataValue;
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeAllList：未能找到数据");
                    tempBaseResultDataValue.ErrorInfo = "未能找到数据！";
                }

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeAllList,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_GetHREmployeeListByDeptIdList(string idlist)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (idlist != null && idlist.Trim() != "")
                {
                    IList<HREmployee> empList = base.IBHREmployee.GetHREmployeeListByDeptIdList(idlist);
                    if (empList != null && empList.Count > 0)
                    {
                        tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(empList);
                        tempBaseResultDataValue.success = true;
                        return tempBaseResultDataValue;
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeListByDeptIdList：未能找到数据！idlist=" + idlist);
                        tempBaseResultDataValue.ErrorInfo = "未能找到数据！idlist = " + idlist;
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeListByDeptIdList,参数错误！");
                    tempBaseResultDataValue.ErrorInfo = "参数错误！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeListByDeptIdList,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_GetHREmployeeById(string id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long tmpid;
                if (id != null && long.TryParse(id, out tmpid))
                {
                    HREmployee emp = base.IBHREmployee.Get(tmpid);
                    if (emp != null)
                    {
                        tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(emp);
                        tempBaseResultDataValue.success = true;
                        return tempBaseResultDataValue;
                    }
                    else
                    {
                        tempBaseResultDataValue.success = false;
                        ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeById：未能找到数据！Id=" + id);
                        tempBaseResultDataValue.ErrorInfo = "未能找到数据！Id = " + id;
                    }
                }
                else
                {
                    tempBaseResultDataValue.success = false;
                    ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeById,参数错误！");
                    tempBaseResultDataValue.ErrorInfo = "参数错误！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeById,异常：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }

        //public BaseResultDataValue RBAC_UDTO_GetHREmployeeAndRbacUserInfoList()
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    try
        //    {
        //        IList<HREmployee> empList = IBHREmployee.GetHREmployeeAllList();
        //        if (empList != null && empList.Count > 0)
        //        {
        //            tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(empList);
        //            tempBaseResultDataValue.success = true;
        //            return tempBaseResultDataValue;
        //        }
        //        else
        //        {
        //            tempBaseResultDataValue.success = false;
        //            ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeAllList：未能找到数据");
        //            tempBaseResultDataValue.ErrorInfo = "未能找到数据！";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultDataValue.success = false;
        //        tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
        //        ZhiFang.Common.Log.Log.Debug("RBAC_UDTO_GetHREmployeeAllList,异常：" + ex.ToString());
        //    }
        //    return tempBaseResultDataValue;
        //}       
        public BaseResultDataValue CheckAccountPWDByInterFace(string Account, string PWD)
        {
            ZhiFang.Common.Log.Log.Debug("CheckAccountPWDByInterFace.Account:" + Account + ",PWD:" + PWD);
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Account == null || Account.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号参数为空！";
                return brdv;
            }
            if (PWD == null || PWD.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "密码参数为空！";
                return brdv;
            }
            try
            {
                bool flag = base.RBAC_BA_Login(Account, PWD, true);
                if (!flag)
                {
                    var flagkey = ConfigHelper.GetConfigString(Entity.LIIP.SystemParameter_LIIP.是否开启HIS接口用户密码验证.Value.Code);
                    var urlkey = ConfigHelper.GetConfigString(Entity.LIIP.SystemParameter_LIIP.HIS接口地址.Value.Code);
                    if (flagkey != null && flagkey.Trim() != "" && flagkey.Trim() == "1" && urlkey != null && urlkey.Trim() != "")
                    {
                        var isliipuser = false;
                        if (base.IBRBACUser.IsExistUserAccount(Account))//存在账户名
                        {
                            isliipuser = true;
                        }
                        var resultstr = ZhiFang.LIIP.Common.RestfullHelper.InvkerRestServicePost("{\"Account\": \"" + Account + "\",\"PWD\": \"" + PWD + "\"}", "TEXT", urlkey, 10);
                        //var resultstr = "{\"success\": \"True\",\"message\": \"\",\"data\": {\"Account\":\"617\",\"PWD\": \"1234\",\"Name\": \"翁荷英\",\"Sex\":\"\",\"Phone\": \"18918288411\",\"Type\": \"护士\",\"DeptName\": \"肾内科\",\"DeptHISCode\": \"2170\"}}";
                        ZhiFang.Common.Log.Log.Debug("CheckAccountPWDByInterFace.调用接口返回值:" + resultstr);
                        //{
                        //    "success": "True",
                        //"message": "",
                        //    "resultcode":"",
                        //"data": {
                        //        "Account": "617",
                        //  "PWD": "1234",
                        //  "Name": "翁荷英",
                        //  "Sex": "",
                        //  "Phone": "18918288411",
                        //        "HISCode":"123456789",
                        //  "Type": "护士",
                        //  "DeptName": "肾内科",
                        //  "DeptHISCode": "2170"
                        //}
                        //}
                        var resultroot = JsonConvert.DeserializeObject(resultstr) as JObject;
                        if (resultroot["success"].ToString().Trim().ToUpper() == "TRUE")
                        {
                            if (isliipuser)
                            {
                                //更新LIS平台用户名密码
                                if (IBRBACUser.SetPWDbyAccount(Account, PWD))
                                {
                                    ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:HIS平台验证通过！");
                                    brdv.success = true;
                                    return brdv;
                                }
                            }
                            else
                            {
                                var empstr = JsonConvert.DeserializeObject(resultroot["data"].ToString().Trim()) as JObject;
                                //新增员工，帐号
                                CV_AddDoctorOrNurseVO entity = new CV_AddDoctorOrNurseVO();
                                entity.Name = empstr["Name"].ToString();
                                entity.Sex = (empstr["Sex"] != null && empstr["Sex"].ToString().Trim() != "") ? (empstr["Sex"].ToString().Trim() == "1" && empstr["Sex"].ToString().Trim() == "男") ? "男" : "女" : "男";
                                entity.PWD = PWD;
                                entity.Account = Account;
                                entity.DeptHISCode = empstr["DeptHISCode"].ToString();
                                entity.DeptName = empstr["DeptName"].ToString();
                                entity.Type = empstr["Type"].ToString();
                                entity.Phone = empstr["Phone"] != null ? empstr["Phone"].ToString() : "";
                                entity.HISCode = empstr["HISCode"] != null ? empstr["HISCode"].ToString() : "";
                                Entity.RBAC.RBACUser rbacuser;
                                brdv = IBCVCriticalValueEmpIdDeptLink.CV_SearchAndAddDoctorOrNurse(entity, out rbacuser);
                                return brdv;
                            }
                        }
                        else
                        {
                            var resultcode = resultroot["resultcode"].ToString().Trim();
                            switch (resultcode.Trim())
                            {
                                case "2":
                                    if (isliipuser)
                                    {
                                        ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:用户名密码错误！HIS无法验证用户名密码！");
                                        brdv.success = false;
                                        brdv.ErrorInfo = "用户名密码错误！";
                                        return brdv;
                                    }
                                    else
                                    {
                                        //新增员工,并提示是否创建帐号
                                        var empstr = JsonConvert.DeserializeObject(resultroot["data"].ToString().Trim()) as JObject;
                                        //新增员工，帐号
                                        CV_AddDoctorOrNurseVO entity = new CV_AddDoctorOrNurseVO();
                                        entity.Name = empstr["Name"].ToString();
                                        entity.Sex = (empstr["Sex"] != null && empstr["Sex"].ToString().Trim() != "") ? (empstr["Sex"].ToString().Trim() == "1" && empstr["Sex"].ToString().Trim() == "男") ? "男" : "女" : "男";
                                        entity.PWD = PWD;
                                        entity.Account = Account;
                                        entity.DeptHISCode = empstr["DeptHISCode"].ToString();
                                        entity.DeptName = empstr["DeptName"].ToString();
                                        entity.Type = empstr["Type"].ToString();
                                        entity.Phone = empstr["Phone"] != null ? empstr["Phone"].ToString() : "";

                                        var result = IBCVCriticalValueEmpIdDeptLink.CV_AddDoctorOrNurseToEmp(entity);
                                        ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:LIS平台不存在此账户，用户名密码错误！HIS存在此用户单无法验证用户名密码！");
                                        brdv.success = false;
                                        if (result.success)
                                        {
                                            brdv.ErrorInfo = "HIS存在此用户，LIS平台新增员工成功,并提示是否创建帐号！";
                                            brdv.ResultCode = "1";
                                            brdv.ResultDataValue = result.ResultDataValue;
                                        }
                                        else
                                        {
                                            brdv.ErrorInfo = "HIS存在此用户，LIS平台新增员工失败！请重试！";
                                            brdv.ResultCode = "";
                                        }
                                        return brdv;
                                    }
                                case "3":
                                    ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:用户名密码错误！HIS验证用户名密码错误！");
                                    brdv.success = false;
                                    brdv.ErrorInfo = "用户名密码错误！";
                                    return brdv;
                                case "4":
                                    ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:用户名不存在！HIS验证用户名不存在！");
                                    brdv.success = false;
                                    brdv.ErrorInfo = "用户名密码错误！";
                                    return brdv;
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:用户名密码错误！并且未配置HIS验证接口。");
                        brdv.success = false;
                        brdv.ErrorInfo = "用户名密码错误！";
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:LIS平台验证通过！");
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBACService.CheckAccountPWDByInterFace:异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！" + e.Message;
            }
            return brdv;
        }

        public BaseResultDataValue RestRBACUserPWD()
        {

            BaseResultDataValue brdv = new BaseResultDataValue();

            string Empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            if (Empid == null || Empid.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取登录者信息！";
                return brdv;
            }
            string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (EmpName == null || EmpName.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取登录者信息！";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Debug("RestRBACUserPWD,Empid:" + Empid + ",EmpName:" + EmpName);
            if (ConfigHelper.GetConfigString("RestRBACUserPWD") == null || ConfigHelper.GetConfigString("RestRBACUserPWD").Trim() == "")
            {
                brdv.success = false;
                return brdv;
            }
            try
            {
                brdv = IBRBACUser.RestRBACUserPWD();
                if (brdv.success)
                {
                    ZhiFang.Common.Log.Log.Debug("RBACService.RestRBACUserPWD:完成！");
                }
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBACService.RestRBACUserPWD:异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！" + e.Message;
            }
            return brdv;
        }
        public BaseResultDataValue GetEmpIDNameByAccount(string Account)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(Account))
            {
                ZhiFang.Common.Log.Log.Error("RBACService.GetEmpIDNameByAccount:参数为空！IP:" + LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP());
                brdv.success = false;
                brdv.ErrorInfo = "参数为空!";
                return brdv;
            }
            ZhiFang.Common.Log.Log.Debug("GetEmpIDNameByAccount,Account:" + Account + ",IP:" + LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP());

            try
            {
                brdv = IBRBACUser.GetEmpIDNameByAccount(Account);
                if (brdv.success)
                {
                    //ZhiFang.Common.Log.Log.Error("RBACService.GetEmpIDByAccount:完成！EmpId:" + brdv.ResultDataValue);
                }
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("RBACService.GetEmpIDByAccount:异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！" + e.Message;
            }
            return brdv;
        }
        public BaseResultDataValue RBAC_UDTO_SearchModuleByHREmpID(long id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<RBACModule> rBACModules = IBRBACModule.SearchModuleByHREmpID(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACModule>(rBACModules);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleToTreeByWhere(string where)
        {
            BaseResultDataValue result = new BaseResultDataValue();
            result.success = true;
            try
            {
                BaseResultTree<RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
                ListTreeRoot = IBRBACModule.SearchRBACModuleToTree(where, false);
                result.success = true;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.Indented, settings);
                string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.None, settings);//去掉回车和空格
                result.ResultDataValue = resulta;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            return result;
        }

        public BaseResultDataValue RBAC_UDTO_SearchRBACModuleIncludePModuleByWhere(string where)
        {
            BaseResultDataValue result = new BaseResultDataValue();
            result.success = true;
            try
            {
                List<Ttree<RBACModule>> ListTreeRoot = new List<Ttree<RBACModule>>();
                ListTreeRoot = IBRBACModule.SearchRBACModule_IncludePModule(where, false);
                result.success = true;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.Indented, settings);
                string resulta = JsonConvert.SerializeObject(ListTreeRoot, Formatting.None, settings);//去掉回车和空格
                result.ResultDataValue = resulta;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = "程序异常!";
                ZhiFang.Common.Log.Log.Error("RBAC_UDTO_SearchRBACModuleIncludePModuleByWhere.异常:" + ex.ToString());
            }
            return result;
        }
        public override bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate)
        {
            try
            {
                return base.RBAC_BA_Login(strUserAccount, strPassWord, isValidate);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".RBAC_BA_Login:异常：" + e.ToString() + ".IP:" + IPHelper.GetClientIP());
                return false;
            }
        }
        public override void SetUserSession(RBACUser rbacUser)
        {
            ZhiFang.Common.Log.Log.Error("RBACService.SetUserSession.开始.");
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                object BBHospitalEmpLink = context.GetObject("BBHospitalEmpLink");
                object BRBACModule = context.GetObject("BRBACModule");
                IBLL.LIIP.IBBHospitalEmpLink IBBHospitalEmpLinkEntity = (IBLL.LIIP.IBBHospitalEmpLink)BBHospitalEmpLink;
                IBLL.RBAC.IBRBACModule IBRBACModuleEntity = (IBLL.RBAC.IBRBACModule)BRBACModule;

                if (rbacUser != null)
                {
                    //foreach(var key in HttpContext.Current.Request.Cookies)
                    //{
                    //    HttpContext.Current.Request.Cookies.Remove(key.ToString());
                    //}

                    HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = "";
                    HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "";
                    if (!string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsMultiLabFlag")) && Common.Public.ConfigHelper.GetConfigString("IsMultiLabFlag").Trim() == "1")
                    {
                        //long HospitalId = IBBHospitalEmpLink.GetLabIdByEmpId(rbacUser.HREmployee.Id);
                        BHospitalEmpLink hosemplink = IBBHospitalEmpLinkEntity.GetBHospitalEmpLinkByEmpId(rbacUser.HREmployee.Id);
                        if (hosemplink != null)
                        {
                            //////////////////////////////////////改为Hospital
                            SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, hosemplink.HospitalID.ToString());//实验室ID
                            HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = hosemplink.HospitalID.ToString();//实验室ID
                            HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabName].Value = hosemplink.HospitalName.Trim();//实验室名称
                            HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabCode].Value = hosemplink.HospitalCode.Trim();//实验室编码
                            HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "1";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsSetLabFlagByAccount")) && Common.Public.ConfigHelper.GetConfigString("IsSetLabFlagByAccount").Trim() == "1" && rbacUser.LabID > 0)
                            {
                                SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, rbacUser.LabID.ToString());//实验室ID
                                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = rbacUser.LabID.ToString();//实验室ID
                                HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.IsLabFlag].Value = "1";
                            }
                        }
                    }

                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.UserAccount, rbacUser.Account);//员工账户名
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.UseCode, rbacUser.UseCode);//员工代码

                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UserID].Value = rbacUser.Id.ToString();//账户ID
                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UserAccount].Value = rbacUser.Account;//账户名
                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UseCode].Value = rbacUser.UseCode;//账户代码

                    //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID

                    if (rbacUser.HREmployee != null)
                    {
                        SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeID, rbacUser.HREmployee.Id); //员工ID
                        SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);//员工姓名 

                        SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);//员工代码 



                        //员工时间戳
                        //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                        Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.EmployeeID, rbacUser.HREmployee.Id.ToString());// 员工ID
                        HttpContext.Current.Request.Cookies[Entity.RBAC.DicCookieSession.EmployeeID].Value = rbacUser.HREmployee.Id.ToString();// 员工ID


                        Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.EmployeeName, rbacUser.HREmployee.CName);// 员工姓名
                        Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.EmployeeUseCode, rbacUser.HREmployee.UseCode);// 员工代码
                        if (rbacUser.HREmployee.HRDept != null)
                        {
                            SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id);//部门ID
                            SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                            Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.HRDeptID, rbacUser.HREmployee.HRDept.Id.ToString());//部门ID
                            Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.HRDeptName, rbacUser.HREmployee.HRDept.CName);//部门名称
                            Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.HRDeptCode, rbacUser.HREmployee.HRDept.UseCode);//部门名称
                        }

                        //获取员工具有权限的模块列表
                        IList<RBACModule> tempRBACModuleList = IBRBACModuleEntity.SearchModuleByHREmpID(rbacUser.HREmployee.Id);
                        if (tempRBACModuleList != null && tempRBACModuleList.Count > 0)
                        {
                            Dictionary<string, string> tempRBACModuleDic = new Dictionary<string, string>();
                            foreach (RBACModule tempRBACModule in tempRBACModuleList)
                            {
                                if (!tempRBACModuleDic.ContainsKey(tempRBACModule.Id.ToString()))
                                    tempRBACModuleDic.Add(tempRBACModule.Id.ToString(), tempRBACModule.Url);
                            }
                            SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.OldModuleID, tempRBACModuleDic);
                        }
                        //获取员工具有权限的模块操作列表
                        //IList<RBACModuleOper> tempRBACModuleOperList = IBRBACModuleOper.SearchModuleOperByHREmpID(rbacUser.HREmployee.Id);
                        //if (tempRBACModuleOperList != null && tempRBACModuleOperList.Count > 0)
                        //{
                        //    Dictionary<string, string> tempRBACModuleOperDic = new Dictionary<string, string>();
                        //    foreach (RBACModuleOper tempRBACModuleOper in tempRBACModuleOperList)
                        //    {
                        //        if (!tempRBACModuleOperDic.ContainsKey(tempRBACModuleOper.Id.ToString()))
                        //            tempRBACModuleOperDic.Add(tempRBACModuleOper.Id.ToString(), tempRBACModuleOper.OperateURL);
                        //    }
                        //    SessionHelper.SetSessionValue(DicCookieSession.CurModuleOperID, tempRBACModuleOperDic);
                        //}
                    }
                }
                else
                {
                    SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, "");//实验室ID
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.UserAccount, Entity.RBAC.DicCookieSession.SuperUser);//账户名
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.UseCode, "");//员工代码
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeID, ""); //员工ID
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeName, Entity.RBAC.DicCookieSession.SuperUserName);//员工姓名 
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.HRDeptID, "");//部门ID
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.HRDeptName, "");//部门名称
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.OldModuleID, "");
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.CurModuleID, "");

                    HttpContext.Current.Response.Cookies[SysPublicSet.SysDicCookieSession.LabID].Value = "";//实验室ID
                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UserID].Value = "";//账户ID
                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UserAccount].Value = Entity.RBAC.DicCookieSession.SuperUser;//账户名
                    HttpContext.Current.Response.Cookies[Entity.RBAC.DicCookieSession.UseCode].Value = "";//账户代码

                    Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.EmployeeID, "");// 员工ID
                    Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.EmployeeName, Entity.RBAC.DicCookieSession.SuperUserName);// 员工姓名
                    SessionHelper.SetSessionValue(Entity.RBAC.DicCookieSession.EmployeeUseCode, "");//员工代码 
                    Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.HRDeptID, "");//部门ID
                    Cookie.CookieHelper.Write(Entity.RBAC.DicCookieSession.HRDeptName, "");//部门名称            
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("SetUserSession.异常：" + ex.ToString());
            }
        }

        public BaseResultDataValue ImportDeptByExcel()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                var tmpfilelist = HttpContext.Current.Request.Files;
                if (tmpfilelist == null || tmpfilelist.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ImportDeptByExcel.异常：未能找到上传的文件!IP=" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "程序异常", success = false };
                }
                if (System.IO.Path.GetExtension(tmpfilelist[0].FileName).ToUpper() != ".XLS" && System.IO.Path.GetExtension(tmpfilelist[0].FileName).ToUpper() != ".XLSX")
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". ImportDeptByExcel.异常：文件类型错误,文件全名{tmpfilelist[0].FileName}!IP=" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "程序异常", success = false };
                }
                string tmpexcelfilename = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + tmpfilelist[0].FileName;
                string tmpexcelpath = System.AppDomain.CurrentDomain.BaseDirectory + SystemFilePath.UPDBFilePath_Dept + "/";

                if (!Directory.Exists(tmpexcelpath))
                    Directory.CreateDirectory(tmpexcelpath);

                string tmpexcelpathall = tmpexcelpath + tmpexcelfilename;
                tmpfilelist[0].SaveAs(tmpexcelpathall);

                

                var tmpdt = ZhiFang.LIIP.Common.NPOIHelper.ImportExceltoDt(tmpexcelpathall);

                brdv = IBHRDept.TransformDeptByExcel(tmpdt);

                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ImportDeptByExcel.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }
        }
        public BaseResultDataValue ImportDeptEmpByExcel()
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                var tmpfilelist = HttpContext.Current.Request.Files;
                if (tmpfilelist == null || tmpfilelist.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ImportDeptEmpByExcel.异常：未能找到上传的文件!IP=" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "程序异常", success = false };
                }
                if (System.IO.Path.GetExtension(tmpfilelist[0].FileName).ToUpper() != ".XLS" && System.IO.Path.GetExtension(tmpfilelist[0].FileName).ToUpper() != ".XLSX")
                {
                    ZhiFang.Common.Log.Log.Error(this.GetType().FullName + $". ImportDeptEmpByExcel.异常：文件类型错误,文件全名{tmpfilelist[0].FileName}!IP=" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "程序异常", success = false };
                }
                string tmpexcelfilename = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + tmpfilelist[0].FileName;
                string tmpexcelpath = System.AppDomain.CurrentDomain.BaseDirectory + SystemFilePath.UPDBFilePath_Emp + "/";

                if (!Directory.Exists(tmpexcelpath))
                    Directory.CreateDirectory(tmpexcelpath);

                string tmpexcelpathall = tmpexcelpath + tmpexcelfilename;
                tmpfilelist[0].SaveAs(tmpexcelpathall);



                var tmpdt = ZhiFang.LIIP.Common.NPOIHelper.ImportExceltoDt(tmpexcelpathall);

                brdv = IBHREmployee.TransformEmpByExcel(tmpdt);

                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". ImportDeptEmpByExcel.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public Stream GetValidateCode()
        {
            try
            {
                int ValidateCodeLength = 6;
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ValidateCodeLength") > 0)
                {
                    ValidateCodeLength = Common.Public.ConfigHelper.GetConfigInt("ValidateCodeLength");
                }
                string checkCode = CreateRandomCode(ValidateCodeLength);
                ZhiFang.Common.Public.SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.ValidateCode, checkCode);
                ZhiFang.Common.Public.SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.IPAddress, IPHelper.GetClientIP());
                ZhiFang.Common.Public.SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.ValidateCodeDateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                return CreateImage(checkCode);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetValidateCode.异常:" + e.ToString());
                return null;
            }
        }
        private string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1; Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        private Stream CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 13);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 23);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Bold));        // 前景色       
            Brush b = new System.Drawing.SolidBrush(Color.Black);        // 背景色      
            g.Clear(Color.White);        // 填充文字       
            g.DrawString(checkCode, f, b, 0, 1);        // 随机线条       
            Pen linePen = new Pen(Color.Gray, 0); Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x1 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int x2 = rand.Next(image.Width);
                int y2 = rand.Next(image.Height);
                g.DrawLine(linePen, x1, y1, x2, y2);
            }        // 随机点       
            for (int i = 0; i < 30; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                image.SetPixel(x, y, Color.Gray);
            }        // 边框      
            g.DrawRectangle(new Pen(Color.Gray), 0, 0, image.Width - 1, image.Height - 1);        // 输出图片 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
            return ms;
        }

        public BaseResultDataValue CheckValidateCode(string ValidateCode)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            int MaxValidateErrorCount = 10;
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("MaxValidateErrorCount") > 0)
            {
                MaxValidateErrorCount = ConfigHelper.GetConfigInt("MaxValidateErrorCount");
            }
            int MaxValidateErrorCountStopSecond = 300;
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("MaxValidateErrorCountStopSecond") > 0)
            {
                MaxValidateErrorCountStopSecond = ConfigHelper.GetConfigInt("MaxValidateErrorCountStopSecond");
            }
            try
            {
                ZhiFang.Common.Log.Log.Error("验证码服务被调用：IP" + IPHelper.GetClientIP());
                #region 验证超过最大次数后的间隔时间是否已到

                if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(Entity.LIIP.DicCookieSession.MaxValidateErrorCountDateTime)))
                {
                    if (DateTime.TryParse(SessionHelper.GetSessionValue(Entity.LIIP.DicCookieSession.MaxValidateErrorCountDateTime), out var tmpdate))
                    {
                        if (DateTime.Now <= tmpdate)
                        {
                            var tmpStopSecond = Math.Round((tmpdate - DateTime.Now).TotalSeconds);
                            ZhiFang.Common.Log.Log.Error("因为验证码错误次数达到上限，所以在" + tmpStopSecond + "秒内，禁止登陆！IP" + IPHelper.GetClientIP());
                            return new BaseResultDataValue() { ErrorInfo = "因为验证码错误次数达到上限，在" + MaxValidateErrorCountStopSecond + "秒内，禁止登陆！还剩" + tmpStopSecond + "秒", success = false, ResultCode = "105" };
                        }
                    }
                }
                #endregion

                if (string.IsNullOrEmpty(ValidateCode))
                {
                    ZhiFang.Common.Log.Log.Error("验证码服务被调用：验证码为空！IP" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "验证码错误！", success = false, ResultCode = "101" };
                }
                #region 验证码有效期验证--暂时注释
                //if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue("ValidateCodeDateTime")))
                //{
                //    ZhiFang.Common.Log.Log.Error("验证码服务被调用：验证码时效为空！IP" + ZhiFang.Tools.IPHelper.GetClientIP());
                //    return new BaseResultDataValue() { ErrorInfo = "验证码过期！", success = false,ResultDataValue="102" };
                //}
                //string ValidateCodeDateTimeStr = SessionHelper.GetSessionValue("ValidateCodeDateTime");
                //DateTime ValidateCodeDateTime = DateTime.Parse(ValidateCodeDateTimeStr);
                //if (ValidateCodeDateTime.AddSeconds(300)<=DateTime.Now)
                //{
                //    ZhiFang.Common.Log.Log.Error("验证码服务被调用：验证码过期！IP" + ZhiFang.Tools.IPHelper.GetClientIP());
                //    return new BaseResultDataValue() { ErrorInfo = "验证码过期！", success = false, ResultDataValue = "102" };
                //}
                #endregion
                ZhiFang.Common.Log.Log.Error("验证码服务被调用：ValidateCode=" + ValidateCode + ",IP" + IPHelper.GetClientIP());

                if (string.IsNullOrEmpty(SessionHelper.GetSessionValue(Entity.LIIP.DicCookieSession.ValidateCode)))
                {
                    ZhiFang.Common.Log.Log.Error("验证码服务被调用：Session验证码为空！IP" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "验证码丢失，请刷新！", success = false, ResultCode = "103" };
                }

                if (ValidateCode != SessionHelper.GetSessionValue(Entity.LIIP.DicCookieSession.ValidateCode))
                {
                    if (int.TryParse(SessionHelper.GetSessionValue(Entity.LIIP.DicCookieSession.ValidateErrorCount), out int aaa))
                    {

                        int tmpcount = aaa + 1;
                        if (tmpcount >= MaxValidateErrorCount)
                        {
                            SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.MaxValidateErrorCountDateTime, DateTime.Now.AddSeconds(MaxValidateErrorCountStopSecond).ToString("yyyy-MM-dd HH:mm:ss"));
                            SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.ValidateErrorCount, 0);
                        }
                        else
                        {
                            SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.ValidateErrorCount, tmpcount);
                        }
                    }
                    else
                    {
                        SessionHelper.SetSessionValue(Entity.LIIP.DicCookieSession.ValidateErrorCount, 1);
                    }
                    ZhiFang.Common.Log.Log.Error("验证码服务被调用：验证码错误！IP" + IPHelper.GetClientIP());
                    return new BaseResultDataValue() { ErrorInfo = "验证码错误！请重新输入！", success = false, ResultCode = "104" };
                }
                brdv.success = true;
                brdv.ResultCode = "200";
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". CheckValidateCode.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
        #endregion

        #region 统计构建定制,后期可能作废
        /// <summary>
        /// 新增模块 根据传递的模块id
        /// </summary>
        /// <param name="bModuleId"></param>
        /// <param name="bModule"></param>
        /// <returns></returns>  
        public BaseResultBool BModuleGetAndAdd(long bModuleId, string bModule)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                long id = 0;
                baseResultBool.success = IBRBACModule.BModuleGetAndAdd(bModuleId, bModule, ref id);
                baseResultBool.BoolInfo = id.ToString();
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.Message;
            }
            return baseResultBool;
        }
        #endregion
    }
}
