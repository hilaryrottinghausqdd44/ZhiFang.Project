using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using ZhiFang.InterfaceFactory;
using System.Collections;
using ZhiFang.ReagentSys.Client.ServerContract;

namespace ZhiFang.ReagentSys.Client
{
    /// <summary>
    /// 试剂定制接口
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaCustomInterface : IReaCustomInterface
    {
        IBHRDept IBHRDept { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaBmsOutDoc IBReaBmsOutDoc { get; set; }

        #region 四川大家NC接口

        /// <summary>
        /// 调用NC接口获取试剂货品信息并保存
        /// </summary>
        /// <param name="ts">时间戳</param>
        /// <param name="supplierId">供应商ID</param>
        /// <returns></returns>
        public BaseResultBool RS_GetReaGoodsByInterface(string ts, long supplierId)
        {
            Log.Info("调用NC接口获取货品开始，方法名称=RS_GetReaGoodsByInterface，参数ts=" + ts + "，supplierId=" + supplierId);
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                HRDept hRDept = GetRootHRDept();
                if (string.IsNullOrWhiteSpace(hRDept.MatchCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的物资对照码为空！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                //if (string.IsNullOrWhiteSpace(ts))
                //{
                //    baseResultBool.success = false;
                //    baseResultBool.ErrorInfo = "ts参数不能为空！";
                //    Log.Error(baseResultBool.ErrorInfo);
                //    return baseResultBool;
                //}

                Hashtable ht = new Hashtable();
                ht["MatchCodeDept"] = hRDept.MatchCode;
                ht["LabID"] = hRDept.LabID;
                ht["TS"] = ts;
                ht["SupplierId"] = supplierId;
                Log.Info("根部门ID=" + hRDept.Id + "，物资对照码(即NC分公司编码)=" + hRDept.MatchCode);

                string DllName = "NC.SCDJ";
                Log.Info("调用定制插件" + DllName.Trim());
                DllInterface obj = (DllInterface)System.Activator.CreateInstance(DllName, DllName + ".PlugInClass").Unwrap();
                baseResultBool = obj.GetReaGoodsByInterface(ht);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("调用NC接口获取货品结束，返回结果：" + baseResultBool.success + "|" + baseResultBool.ErrorInfo);
            return baseResultBool;
        }

        /// <summary>
        /// 调用NC接口上传订单
        /// </summary>
        /// <param name="orderDocId">订单主单ID</param>
        /// <returns></returns>
        public BaseResultBool RS_SendBmsCenOrderByInterface(long orderDocId)
        {
            Log.Info("调用NC接口上传订单开始，方法名称=RS_SendBmsCenOrderByInterface，参数orderDocId=" + orderDocId);
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                //获取订单
                ReaBmsCenOrderDoc orderDoc = IBReaBmsCenOrderDoc.Get(orderDocId);
                if (orderDoc != null)
                {
                    if (orderDoc.IsThirdFlag.ToString() == ReaBmsOrderDocThirdFlag.同步成功.Key)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "订货单已上传NC平台";
                        return baseResultBool;
                    }
                    if (orderDoc.Status != int.Parse(ReaBmsOrderDocStatus.供应商确认.Key))
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "只有单据状态=[供应商确认]的订货单才可以上传NC平台";
                        return baseResultBool;
                    }
                }
                //获取订单明细
                IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.GetReaBmsCenOrderDtlListByDocId(orderDocId);
                if (orderDtlList == null || orderDtlList.Count == 0)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "订货单同步NC平台失败：无法根据订货单ID" + orderDocId.ToString() + "获取订货单明细信息";
                    return baseResultBool;
                }
                //获取供应商的根级部门（即登录试剂平台用户的所属部门的根级部门）
                HRDept hRDept = GetRootHRDept();
                if (string.IsNullOrWhiteSpace(hRDept.MatchCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的物资对照码为空！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                if (string.IsNullOrWhiteSpace(hRDept.StandCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的销售组织编码为空！请在组织机构的“标准代码”里维护其销售组织编码！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }

                Hashtable ht = new Hashtable();
                ht["ReaBmsCenOrderDoc"] = orderDoc;
                ht["ReaBmsCenOrderDtlList"] = orderDtlList;
                ht["HRDept"] = hRDept;

                string DllName = "NC.SCDJ";
                Log.Info("调用定制插件" + DllName.Trim());
                DllInterface obj = (DllInterface)System.Activator.CreateInstance(DllName, DllName + ".PlugInClass").Unwrap();
                baseResultBool = obj.SendBmsCenOrderByInterface(ht);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("调用NC接口上传订单结束，返回结果：" + baseResultBool.success + "|" + baseResultBool.ErrorInfo);
            return baseResultBool;
        }

        /// <summary>
        /// 调用NC的出库单服务，获取出库单信息
        /// 数据写入到供货单表（Rea_BmsCenSaleDoc_供货总单表和Rea_BmsCenSaleDtl_供货明细表）
        /// </summary>
        /// <param name="ncBillNo">NC出库单号</param>
        /// <returns></returns>
        public BaseResultBool RS_GetOutOrderInfoByInterface(string ncBillNo)
        {
            Log.Info("调用NC接口获取出库单开始，方法名称=RS_GetOutOrderInfoByInterface，参数ncbillno=" + ncBillNo);
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "无法获取当前用户的ID信息,请重新登录！";
                    return baseResultBool;
                }

                //获取供应商的根级部门（即登录试剂平台用户的所属部门的根级部门）
                HRDept hRDept = GetRootHRDept();
              
                long empID = long.Parse(employeeID);
                string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                Hashtable ht = new Hashtable();
                ht["NcBillNo"] = ncBillNo;
                ht["EmpID"] = empID;
                ht["EmployeeName"] = employeeName;
                ht["HRDept"] = hRDept;

                string DllName = "NC.SCDJ";
                Log.Info("调用定制插件" + DllName.Trim());
                DllInterface obj = (DllInterface)System.Activator.CreateInstance(DllName, DllName + ".PlugInClass").Unwrap();
                baseResultBool = obj.GetOutOrderInfoByInterface(ht);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("调用NC接口获取出库单结束，返回结果：" + baseResultBool.success + "|" + baseResultBool.ErrorInfo);
            return baseResultBool;
        }

        /// <summary>
        /// 调用NC接口上传出库单
        /// </summary>
        /// <param name="outDocId">出库主单ID</param>
        /// <returns></returns>
        public BaseResultBool RS_SendOutInfoByInterface(long outDocId)
        {
            Log.Info("调用NC接口上传出库单开始，方法名称=RS_SendOutInfoByInterface，参数outDocId=" + outDocId);
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "无法获取当前用户的ID信息,请重新登录！";
                    return baseResultBool;
                }

                //获取供应商的根级部门（即登录试剂平台用户的所属部门的根级部门）
                HRDept hRDept = GetRootHRDept();
                if (string.IsNullOrWhiteSpace(hRDept.MatchCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的物资对照码为空！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                if (string.IsNullOrWhiteSpace(hRDept.StandCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的销售组织编码为空！请在组织机构的“标准代码”里维护其销售组织编码！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }
                if (string.IsNullOrWhiteSpace(hRDept.UseCode))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = string.Format("部门ID={0}，部门名称={1}的平台编码为空！请在组织机构的“平台编码”里维护！", hRDept.Id, hRDept.CName);
                    Log.Error(baseResultBool.ErrorInfo);
                    return baseResultBool;
                }

                //获取出库单
                ReaBmsOutDoc reaBmsOutDoc = IBReaBmsOutDoc.Get(outDocId);
                if (reaBmsOutDoc.IsThirdFlag.ToString() == ReaBmsOutDocThirdFlag.同步成功.Key)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "出库单已上传NC平台";
                    return baseResultBool;
                }
                if (reaBmsOutDoc.Status != int.Parse(ReaBmsOutDocStatus.供应商确认.Key))
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "只有单据状态=[供应商确认]的出库单才可以上传NC平台";
                    return baseResultBool;
                }

                long empID = long.Parse(employeeID);
                string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                Hashtable ht = new Hashtable();
                ht["OutDocId"] = outDocId;
                ht["EmpID"] = empID;
                ht["EmployeeName"] = employeeName;
                ht["HRDept"] = hRDept;
                ht["ReaBmsOutDoc"] = reaBmsOutDoc;

                string DllName = "NC.SCDJ";
                Log.Info("调用定制插件" + DllName.Trim());
                DllInterface obj = (DllInterface)System.Activator.CreateInstance(DllName, DllName + ".PlugInClass").Unwrap();
                baseResultBool = obj.SendOutInfoByInterface(ht);

            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "操作异常：" + ex.Message;
                Log.Error("操作异常->", ex);
            }
            Log.Info("调用NC接口上传出库单结束，返回结果：" + baseResultBool.success + "|" + baseResultBool.ErrorInfo);
            return baseResultBool;
        }

        /// <summary>
        /// 根据当前登录人员的部门，获取其根级部门
        /// </summary>
        /// <returns></returns>
        private HRDept GetRootHRDept()
        {
            long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
            List<long> allParent = IBHRDept.GetParentDeptIdListByDeptId(deptID);
            deptID = allParent[allParent.Count - 1];
            HRDept hRDept = IBHRDept.Get(deptID);
            return hRDept;
        }

        #endregion
    }
}
