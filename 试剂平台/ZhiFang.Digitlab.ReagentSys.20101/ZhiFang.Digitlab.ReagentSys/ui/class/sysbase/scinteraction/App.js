/**
 * 公共交流互动信息
 * @author longfc
 * @version 2016-10-24
 */
Ext.define('Shell.class.sysbase.scinteraction.App', {
	extend: 'Shell.class.sysbase.interaction.App',
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/SC_UDTO_SearchSCInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReagentSysService.svc/SC_UDTO_AddSCInteraction',
	/**交流对象名*/
	objectName: 'SCInteraction',
	/**附件关联对象名*/
	fObejctName: 'Bobject',
	/**附件关联对象主键*/
	fObjectValue: '',
	FormPosition: 's',
	/**ID*/
	PK: '',
	fObjectIsID: true,
	initComponent: function() {
		var me = this;
		me.fObjectValue = me.PK;
		me.callParent(arguments);
	}
});