/**
 * 库房人员权限关系维护:出库申请权限
 * @author longfc	
 * @version 2019-04-02
 */
Ext.define('Shell.class.rea.client.shelves.storagelink.OfOutApplyGrid', {
	extend: 'Shell.class.rea.client.shelves.storagelink.BasicGrid',

	title: '出库申请权限',
	/**库房人员关系类型:
	 * 1:库房管理权限;
	 * 2:移库申请源库房;
	 * 3:出库申请权限;
	 * 4:直接移库源库房;
	 * */
	operType: "3",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	}
});