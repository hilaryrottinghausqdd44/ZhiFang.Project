/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.twoaudit.EditPanel', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.EditPanel',
	width: 800,
	height: 500,
	title: '服务器授权申请',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
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
	/**是否包含是否特批复选框(只有审核时才显示)*/
	hasIsSpecially: false,
	/**是否特批复选框选择值*/
	IsSpeciallyValue: false,
	/**授权ID*/
	PK: null,
	ProgramGrid: 'Shell.class.wfm.authorization.ahserver.twoaudit.ProgramLicenceGrid',
	EquipGrid: 'Shell.class.wfm.authorization.ahserver.twoaudit.EquipLicenceGrid',

	/**上传的授权申请文件的主要信息*/
	AHServerLicence: null,
	/**表单参数*/
	FormConfig: {
		/**需要保存的 信息*/
		Entity: {
			TwoAuditID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			TwoAuditName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	}
});