using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WeiXin.ServerWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConstructionService : ConstructionServiceCommon, ZhiFang.WeiXin.ServerContract.IConstructionService
    {
        #region 服务成员
        protected override string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        protected override IList<string> InterfaceList
        {
            get
            {
                return new List<string> { "IConstructionService", "IMEPTService", "IMEService", "IQCService", "IRBACService", "ISingleTableService" };
            }
        }

        public override BaseResultDataValue GetPinYin(string chinese)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (chinese != null && chinese.Length > 0)
                {
                    char[] tmpstr = chinese.ToCharArray();
                    foreach (char a in tmpstr)
                    {
                        //brdv.ResultDataValue += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
                        brdv.ResultDataValue += Common.PinYinConverter.GetFirst(a.ToString());
                    }
                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    brdv.ErrorInfo = "字符串格式不正确！";
                    brdv.success = false;
                    return brdv;
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message;
                brdv.success = false;
                return brdv;
            }
        }

        #endregion
    }
}
