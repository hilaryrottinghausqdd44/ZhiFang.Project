using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BSCIMInfomationContent : BaseBLL<SCIMInfomationContent>, ZhiFang.IBLL.LIIP.IBSCIMInfomationContent
    {
        public BaseResultBool SCIMInfomationContentReBack(string EmpId, string EmpName, long IMID)
        {
            BaseResultBool brb = new BaseResultBool();
            var tmpentity=DBDao.Get(IMID);
            if (tmpentity == null)
            {
                ZhiFang.Common.Log.Log.Error("BSCIMInfomationContent.SCIMInfomationContentReBack.未找到相关通讯信息数据！IMID:"+ IMID);
                brb.ErrorInfo = "未找到相关通讯信息数据！";
                brb.success = false;
                return brb;
            }

            if (tmpentity.SenderID.ToString().Trim() != EmpId)
            {
                ZhiFang.Common.Log.Log.Error("BSCIMInfomationContent.SCIMInfomationContentReBack.无权撤销改通讯信息数据！IMID:" + IMID+ ";EmpId:"+ EmpId+ ";tmpentity.SenderID:"+ tmpentity.SenderID.ToString().Trim());
                brb.ErrorInfo = "无权撤销改通讯信息数据！";
                brb.success = false;
                return brb;
            }

            brb.success=DBDao.UpdateByHql(" update SCIMInfomationContent set BackFlag=1 where  Id=" + IMID+ " and SenderID= "+EmpId)>0;
            return brb;
        }

        public BaseResultBool SCIMInfomationContentReRead(string EmpId, string EmpName, long IMID)
        {
            BaseResultBool brb = new BaseResultBool();
            var tmpentity = DBDao.Get(IMID);
            if (tmpentity == null)
            {
                ZhiFang.Common.Log.Log.Error("BSCIMInfomationContent.SCIMInfomationContentReRead.未找到相关通讯信息数据！IMID:" + IMID);
                brb.ErrorInfo = "未找到相关通讯信息数据！";
                brb.success = false;
                return brb;
            }

            if (tmpentity.ReceiverID.ToString().Trim() != EmpId)
            {
                ZhiFang.Common.Log.Log.Error("BSCIMInfomationContent.SCIMInfomationContentReRead.无权阅读改通讯信息数据！IMID:" + IMID + ";EmpId:" + EmpId + ";tmpentity.ReceiverID:" + tmpentity.ReceiverID.ToString().Trim());
                brb.ErrorInfo = "无权撤销改通讯信息数据！";
                brb.success = false;
                return brb;
            }

            brb.success = DBDao.UpdateByHql(" update SCIMInfomationContent set ReadFlag=1 where  Id=" + IMID + " and ReceiverID= " + EmpId) > 0;
            return brb;
        }
    }
}