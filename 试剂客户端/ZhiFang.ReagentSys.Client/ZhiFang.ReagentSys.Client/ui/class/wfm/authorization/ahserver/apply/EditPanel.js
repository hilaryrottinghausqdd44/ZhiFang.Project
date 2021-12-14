/**
 * 服务器授权
 * @author liangyl
 * @version 2016-12-15
 */
Ext.define('Shell.class.wfm.authorization.ahserver.apply.EditPanel', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.EditPanel',
	title: '服务器授权',
	/**通过按钮文字*/
	OverButtonName: '暂存',
	/**不通过按钮文字*/
	BackButtonName: '提交申请',
	/**通过状态文字*/
	OverName: '暂存',
	/**不通过状态文字*/
	BackName: '申请',
	/**处理意见字段*/
	OperMsgField: '',
	/**处理时间字段*/
	AuditDataTimeMsgField: '',
	/**是否包含是否特批复选框(只有审核时才显示)*/
	hasIsSpecially: false,
	/**是否特批复选框选择值*/
	IsSpeciallyValue: false,
	/**单站点授权ID*/
	PK: null,
	ProgramGrid: 'Shell.class.wfm.authorization.ahserver.apply.ProgramLicenceGrid',
	EquipGrid: 'Shell.class.wfm.authorization.ahserver.apply.EquipLicenceGrid',

	/**表单参数*/
	FormConfig: null
});