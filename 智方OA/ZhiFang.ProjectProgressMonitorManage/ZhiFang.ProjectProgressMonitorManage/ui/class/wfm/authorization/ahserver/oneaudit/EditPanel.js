/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.oneaudit.EditPanel', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.EditPanel',
	
	width: 800,
	height: 500,
	title: '服务器授权审核',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**通过按钮文字*/
	OverButtonName: '商务授权通过',
	/**不通过按钮文字*/
	BackButtonName: '商务授权退回',
	/**通过状态文字*/
	OverName: '商务授权通过',
	/**不通过状态文字*/
	BackName: '商务授权退回',
	/**处理意见字段*/
	OperMsgField: 'OneAuditInfo',
	/**处理时间字段*/
	AuditDataTimeMsgField: 'OneAuditDataTime',
	/**是否包含是否特批复选框(只有审核时才显示)*/
	hasIsSpecially: true,
	/**是否特批复选框选择值*/
	IsSpeciallyValue: false,
	/**授权ID*/
	PK: null,
	PClientID:null,
	ProgramGrid: 'Shell.class.wfm.authorization.ahserver.oneaudit.ProgramLicenceGrid',
	EquipGrid: 'Shell.class.wfm.authorization.ahserver.oneaudit.EquipLicenceGrid',

	/**上传的授权申请文件的主要信息*/
	AHServerLicence: null,
	/**表单参数*/
	FormConfig: {
		/**需要保存的 信息*/
		Entity: {
			OneAuditID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			OneAuditName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	}
});