using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.LIIP;
using ZhiFang.Entity.LIIP;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.LIIP;
using System.Collections.Generic;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.LIIP
{
    /// <summary>
    ///
    /// </summary>
    public class BWXMessageSendTask : BaseBLL<WXMessageSendTask>, IBWXMessageSendTask
    {
        IDWXMsgSendLogDao IDWXMsgSendLogDao { get; set; }
        IDBHospitalEmpLinkDao IDBHospitalEmpLinkDao { get; set; }
        IDHRDeptDao IDHRDeptDao { get; set; }
        public bool Add_WXMessageSendOut(long id, long peopleId, string empID, string empName)
        {
            WXMessageSendTask wXMessageSendTask = DBDao.Get(id);
            switch (wXMessageSendTask.SendTypeID)
            {
                case 1: //人员
                    IList<BHospitalEmpLink> bHospitalEmpLink = IDBHospitalEmpLinkDao.GetListByHQL("HospitalID=" + id);
                    bHospitalEmpLink.ToList().ForEach(a =>
                    {
                        WXMsgSendLog wXMsgSendLog1 = new WXMsgSendLog();
                        wXMsgSendLog1.BobjectID = id;
                        wXMsgSendLog1.SenderID = long.Parse(empID);
                        wXMsgSendLog1.SenderName = empName;
                        wXMsgSendLog1.TypeName = "人员";
                        wXMsgSendLog1.ReceiveObjectID = a.EmpID.Value;
                        IDWXMsgSendLogDao.Save(wXMsgSendLog1);
                    });
                    break;
                case 2://部门
                    IList<HRDept> hRDepts = IDHRDeptDao.LoadAll();
                    hRDepts.ToList().ForEach(a =>
                    {
                        WXMsgSendLog wXMsgSendLog1 = new WXMsgSendLog();
                        wXMsgSendLog1.BobjectID = id;
                        wXMsgSendLog1.SenderID = long.Parse(empID);
                        wXMsgSendLog1.SenderName = empName;
                        wXMsgSendLog1.TypeName = "部门";
                        wXMsgSendLog1.ReceiveObjectID = a.Id;
                        IDWXMsgSendLogDao.Save(wXMsgSendLog1);
                    });
                    break;
                case 3://未指定
                    WXMsgSendLog wXMsgSendLog = new WXMsgSendLog();
                    wXMsgSendLog.BobjectID = id;
                    wXMsgSendLog.SenderID = long.Parse(empID);
                    wXMsgSendLog.SenderName = empName;
                    wXMsgSendLog.TypeName = "未指定";
                    wXMsgSendLog.ReceiveObjectID = peopleId;
                    IDWXMsgSendLogDao.Save(wXMsgSendLog);
                    break;
            }

            wXMessageSendTask.Count = wXMessageSendTask.Count + 1;
            DBDao.Update(wXMessageSendTask);
            return true;
        }
    }
}