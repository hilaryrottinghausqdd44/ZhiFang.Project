using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBHostTypeUser : BaseBLL<BHostTypeUser>, ZhiFang.IBLL.LabStar.IBBHostTypeUser
    {
        public BaseResultDataValue copyBHostTypeUserByEmpId(long pasteuser, string Copyusers)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (pasteuser == 0 || string.IsNullOrEmpty(Copyusers))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "复制人ID和被复制人ID不可为空！";
                return baseResultDataValue;
            }
            IList<BHostTypeUser> original = DBDao.GetListByHQL("EmpID in ("+ Copyusers+","+ pasteuser+")");
            int successnum = 0;
            int errornum = 0;
            if (original.Count > 0)
            {
                var copyhost = original.Where(w => w.EmpID != pasteuser).ToList();
                if (copyhost.Count > 0)
                {
                    List<long> hostids = new List<long>();
                    foreach (var host in copyhost)
                    {
                        if (!hostids.Contains(long.Parse(host.HostTypeID.ToString())))
                            hostids.Add(long.Parse(host.HostTypeID.ToString()));
                    }
                    var pastehost = original.Where(w => w.EmpID == pasteuser).ToList();
                    foreach (var id in hostids)
                    {
                        if (pastehost.Where(w => w.HostTypeID == id).Count() == 0)
                        {

                            BHostTypeUser bHostTypeUser = new BHostTypeUser();
                            bHostTypeUser.HostTypeID = id;
                            bHostTypeUser.EmpID = pasteuser;
                            bHostTypeUser.IsUse = true;
                            bHostTypeUser.DataAddTime = DateTime.Now;
                            this.Entity = bHostTypeUser;
                            if (this.Save())
                                successnum++;
                            else
                                errornum++;
                        }
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "没有站点类型可以复制！";
                    return baseResultDataValue;
                }
            }
            else 
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "没有站点类型可以复制！";
                return baseResultDataValue;
            }
            baseResultDataValue.success = true;
            baseResultDataValue.ResultDataValue = "复制成功:" + successnum + "个，复制失败:" + errornum + "个！";
            return baseResultDataValue;

        }
    }
}