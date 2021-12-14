
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public class BRBACPreconditions : BaseBLL<RBACPreconditions>, ZhiFang.IBLL.RBAC.IBRBACPreconditions
    {
        /// <summary>
        /// 将选择的模块服务的预置条件项新增复制到指定的模块服务
        /// </summary>
        /// <param name="moduleoperId">待复制预置条件项所属的模块服务ID</param>
        /// <param name="copyModuleOpeIdStr">选择需要复制的模块服务Id字符串(123,222)</param>
        /// <returns></returns>
        public BaseResultBool CopyPreconditionsOfRBACModuleOper(long moduleoperId, string copyModuleOpeIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(copyModuleOpeIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "选择复制的模块服务为空!";
                return tempBaseResultBool;
            }
            string errorInfo = "";
            copyModuleOpeIdStr = copyModuleOpeIdStr.TrimEnd(',');
            string[] copyIdArr = copyModuleOpeIdStr.Split(',');
            IList<RBACPreconditions> list = this.SearchListByHQL("rbacpreconditions.RBACModuleOper.Id=" + moduleoperId);
            foreach (var copuId in copyIdArr)
            {
                RBACModuleOper moduleOper = new RBACModuleOper();
                moduleOper.Id = long.Parse(copuId);
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                moduleOper.DataTimeStamp = arrDataTimeStamp;
                foreach (var entity in list)
                {
                    entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    entity.RBACModuleOper = moduleOper;
                    this.Entity = entity;
                    if (this.Add() == false)
                        errorInfo += "预置条件项名称为:" + entity.CName + "复制新增失败;";
                }
            }
            if (!string.IsNullOrEmpty(errorInfo))
            {
                ZhiFang.Common.Log.Log.Error("CopyPreconditionsOfRBACModuleOper错误:" + errorInfo);
                tempBaseResultBool.ErrorInfo = errorInfo;
                tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
    }
}