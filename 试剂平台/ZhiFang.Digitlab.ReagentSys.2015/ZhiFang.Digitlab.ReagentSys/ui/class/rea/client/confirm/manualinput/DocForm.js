/**
 * 客户端手工录入验收
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.DocForm', {
	extend: 'Shell.class.rea.client.confirm.basic.DocForm',

	title: '验货单信息',

	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDocConfirm',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocConfirmByField',
	OTYPE: "manualinput",
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	Status: "0",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = me.callParent(arguments);
		//供货总单号隐藏
		items[3].hidden = true;

		items.splice(3, 0, {
			fieldLabel: '验收单号',
			name: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			itemId: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	}
});