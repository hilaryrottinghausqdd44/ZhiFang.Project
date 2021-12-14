/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.twoaudit.EditPanel', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.EditPanel',
	
	title: '单站点授权审批',
	width: 800,
	height: 500,
	
	/**通过按钮文字*/
	OverButtonName: '特批授权通过',
	/**不通过按钮文字*/
	BackButtonName: '特批授权退回',
	/**通过状态文字*/
	OverName: '特批授权通过',
	/**不通过状态文字*/
	BackName: '特批授权退回',
	/**处理意见字段*/
	OperMsgField: 'TwoAuditInfo',
	/**处理时间字段*/
	AuditDataTimeMsgField: 'TwoAuditDataTime',

	/**单站点授权ID*/
	PK: null,
	/**表单参数*/
	FormConfig: {
		/**需要保存的 信息*/
		Entity: {
			TwoAuditID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			TwoAuditName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	}
});