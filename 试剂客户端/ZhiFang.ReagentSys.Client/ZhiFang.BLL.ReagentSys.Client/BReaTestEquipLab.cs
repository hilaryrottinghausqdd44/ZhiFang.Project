
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaTestEquipLab : BaseBLL<ReaTestEquipLab>, ZhiFang.IBLL.ReagentSys.Client.IBReaTestEquipLab
    {

        /// <summary>
        /// 客户端同步LIS的检验仪器信息
        /// </summary>
        /// <returns></returns>
        public BaseResultBool SaveSyncLisTestEquipLabInfo(long labid)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IList<ReaTestEquipLab> reaList = this.LoadAll();

            StringBuilder lisCode = new StringBuilder();
            foreach (ReaTestEquipLab model in reaList)
            {
                if (!string.IsNullOrEmpty(model.LisCode))
                {
                    lisCode.Append(model.LisCode + ",");
                }
            }
            string hql = "";
            if (lisCode.Length > 0)
                hql = " LisCode not in(" + lisCode.ToString().TrimEnd(',') + ")";
            IList<ReaTestEquipLab> lisList = DataAccess_SQL.CreateReaTestEquipLabDao_SQL().GetListByHQL(hql);
            if (lisList != null && lisList.Count > 0)
            {
                try
                {
                    //DBDao.BatchSaveVO(lisList);
                    foreach (var entity in lisList)
                    {
                        this.Entity = entity;
                        baseResultBool.success = DBDao.Save(this.Entity);//this.Add();
                        if (baseResultBool.success == false)
                        {
                            baseResultBool.ErrorInfo = "同步LIS检验仪器信息保存时出错!";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo + ex.Message);
                }
            }
            if (baseResultBool.success == true)
            {
                baseResultBool.BoolInfo = "同步LIS检验仪器信息成功!";
                baseResultBool.ErrorInfo = baseResultBool.BoolInfo;
            }
            return baseResultBool;
        }
        public override bool Add()
        {
            bool result = EditVerification();
            if (result == false)
            {
                return false;
            }
            result = DBDao.Save(this.Entity);
            return result;
        }
        public bool EditVerification()
        {
            bool result = true;
            if (this.Entity != null)
            {
                //Lis仪器编码不能重复
                if (!string.IsNullOrEmpty(this.Entity.LisCode))
                {
                    IList<ReaTestEquipLab> tempList = this.SearchListByHQL(string.Format("reatestequiplab.Visible=1 and reatestequiplab.LisCode='{0}' and reatestequiplab.Id!={1} and reatestequiplab.LabID={2}", this.Entity.LisCode, this.Entity.Id, this.Entity.LabID));
                    if (tempList != null && tempList.Count > 0)
                    {
                        result = false;
                        ZhiFang.Common.Log.Log.Error(string.Format("Lis仪器编码为{0},已存在,请不要重复维护!", this.Entity.LisCode));
                        return result;
                    }
                }
            }
            return result;
        }
    }
}