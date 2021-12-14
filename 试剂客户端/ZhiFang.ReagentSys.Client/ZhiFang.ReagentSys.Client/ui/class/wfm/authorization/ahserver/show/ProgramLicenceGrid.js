/**
 * 服务器程序授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.show.ProgramLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ProgramLicenceGrid',
	title: '服务器程序授权明细',
	hasButtontoolbar: false,
	/**是否有删除列*/
	hasDeleteColumn: false,
	isAllowEditing: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});