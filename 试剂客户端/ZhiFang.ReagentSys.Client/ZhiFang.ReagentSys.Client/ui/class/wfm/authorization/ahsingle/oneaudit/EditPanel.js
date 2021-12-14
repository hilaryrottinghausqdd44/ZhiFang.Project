/**
 * 单站点授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.oneaudit.EditPanel', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.EditPanel',
	width: 800,
	height: 500,
	title: '单站点商务授权',
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
	/**单站点授权ID*/
	PK: null,
	/**是否使用编辑表单*/
	isUseEditForm: true,
	/**是否新授权(授权码为纯数字)*/
	IsNumLicenceByMACValue: true,
	/**是否显示新授权选择项(授权码为纯数字)*/
	hasIsNumLicenceByMAC: true,
	/**表单参数*/
	FormConfig: {
		/**需要保存的 信息*/
		Entity: {
			OneAuditID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			OneAuditName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		}
	},
	/**保存前验证方法*/
	verificationSubmit: function() {
		var me = this;
		var basicForm = me.getComponent("EditTabPanel").getComponent("basicForm");
		var result = basicForm.verificationSubmit();
		return result;
	},
	/**overwrite获取编辑实体参数*/
	getEditParams: function(status) {
		var me = this;
		var basicForm = me.getComponent("EditTabPanel").getComponent("basicForm");
		var entity = basicForm.getEditParams();
		entity.entity.Id = me.PK;
		entity.entity.Status = status;
		/**
		 * IsNumLicenceByMACValue:是否新授权(授权码为纯数字)
		 * IsCharLicenceByMAC:是否旧授权(授权码为字母+数字)
		 * */
		entity.entity.IsCharLicenceByMAC = !me.IsNumLicenceByMACValue;

		var editParams = {
			entity: entity.entity,
			fields: entity.fields
		};
		return editParams;
	}
});