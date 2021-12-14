/**
 * 服务器仪器授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.show.EquipLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.EquipLicenceGrid',
	title: '服务器仪器授权明细',
	/**是否有删除列*/
	hasDeleteColumn: false,
	isAllowEditing: false,
	/**带功能按钮栏*/
    hasButtontoolbar: false,
    /**是否单选*/
	checkOne: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});