using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCMsg : IBGenericManager<SCMsg>
	{
        BaseResultDataValue AddAndGetDeptId(SCMsg entity);

        BaseResultDataValue SaveAndGetDeptId_OTTH(SCMsg_OTTH entity);

        SCMsg MsgHandleSearchById_OTTH(SCMsg_OTTH_Search entity);

        bool SCMsgByWarningUpload(string[] tempArray);
        BaseResultDataValue SCMsgByConfirm(SCMsg scmsg);
        BaseResultDataValue SCMsgByConfirmNotify(SCMsg scmsg);
        BaseResultDataValue SCMsgByTimeOutReSend(SCMsg entity);
        EntityList<SCMsg> SearchSCMsgByHQLAndLabCode(string where, int page, int limit, string empid);
        bool LISSendMessage(LISSendMessageVO lismsg);
        EntityList<SCMsg> SearchSCMsgByHQLAndAccountPWD(string v1, int v2, int v3, string account, string pWDbase);
        string BatchConfirmMsg(string where, string empid,string ip);
        /// <summary>
        /// 根据实验室编码进行消息查询
        /// </summary>
        /// <param name="where">HQL</param>
        /// <param name="page">起始页</param>
        /// <param name="limit">每页大小</param>
        /// <param name="labCodeList">实验室编码列表，逗号分隔</param>
        /// <returns></returns>
        EntityList<SCMsg> SearchLabMsgByLabCodeList(string where, int page, int limit, string labCodeList);
        BaseResultDataValue AddSCMsgZF_LAB_START_CV(SCMsg entity);
        EntityList<SCMsg> SearchSCMsgAndSCMsgHandleByHQL(string where, string v, int page, int limit);
    }
}