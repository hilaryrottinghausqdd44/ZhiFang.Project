/**
 * 服务器程序授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.oneaudit.ProgramLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ProgramLicenceGrid',
	title: '服务器程序授权明细',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	}
});