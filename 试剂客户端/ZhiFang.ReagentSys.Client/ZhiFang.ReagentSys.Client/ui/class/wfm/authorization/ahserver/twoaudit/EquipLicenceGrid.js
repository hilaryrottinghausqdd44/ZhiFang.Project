/**
 * 服务器仪器授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.twoaudit.EquipLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.EquipLicenceGrid',
	title: '服务器仪器授权明细',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});