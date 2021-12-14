/**
 * 退供应商库存货品列表
 * @author longfc
 * @version 2019-03-27
 */
Ext.define('Shell.class.rea.client.out.retreatsuppliers.QtyDtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.QtyDtlGrid',

	/**表单选中的库房*/
	StorageObj: {},
	/**用户UI配置Key*/
	userUIKey: 'out.loss.QtyDtlGrid',
	/**用户UI配置Name*/
	userUIName: "退供应商库存货品列表",
	/**货品条码操作类型*/
	barcodeOperType: '10',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//扫码只有一行数据时，自动添加到明细列表
		me.store.on({
			load: function(com, records, successful, eOpts) {
				var buttonsToolbar = me.getComponent('buttonsToolbar');
				var txtScanCode = buttonsToolbar.getComponent('txtScanCode');
				//"\s"匹配任何不可见字符，包括空格、制表符、换页符等等
				var barCode = txtScanCode.getValue().trim().replace(/\s+/g, '');
				if(records && records.length == 1 && barCode) {
					me.fireEvent('dbitemclick', me, records[0]);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkchange', 'dbitemclick', 'NObarcode', 'dbselectclick', 'scanCodeClick');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		//columns.push();
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		return items;
	}
});