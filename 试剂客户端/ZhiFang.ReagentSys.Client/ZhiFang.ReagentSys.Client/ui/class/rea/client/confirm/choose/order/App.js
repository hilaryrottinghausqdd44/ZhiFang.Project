/**
 * 订单选择
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.confirm.choose.order.App', {
	extend: 'Ext.panel.Panel',
	
	title: '订单选择',
	width: 700,
	height: 480,
	
	autoScroll: false,
	layout: {
		type: 'border'
	},
	/**内容周围距离*/
	bodyPadding:'1px',
	/**供应商ID*/
	ReaCompID:null,
	ReaCompCName: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.OrderCheck.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaBmsCenOrderDoc_Id');
					me.CenOrderDtlGrid.OrderDocID=id;
					me.CenOrderDtlGrid.onSearch();
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('ReaBmsCenOrderDoc_Id');
					me.CenOrderDtlGrid.OrderDocID=id;
					me.CenOrderDtlGrid.onSearch();
				}, null, 500);
			},
			nodata: function(p) {
				me.CenOrderDtlGrid.clearData();
			},
			accept:function(p, record){
				me.fireEvent('accept', me, record);
			}
		});	
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		//内部组件
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.OrderCheck = Ext.create('Shell.class.rea.client.confirm.choose.order.OrderCheck', {
			region: 'west',
			width: 820,
			header: false,
			itemId: 'OrderCheck',
			ReaCompID: me.ReaCompID,
	        ReaCompCName: me.ReaCompCName,
	        autoSelect:true,
	        defaultLoad: true,
		    split: true,
			collapsible: true,
			collapseMode:'mini'
		});

		me.CenOrderDtlGrid = Ext.create('Shell.class.rea.client.confirm.choose.order.CenOrderDtlGrid', {
			region: 'center',
			header: false,
			itemId: 'CenOrderDtlGrid'
		});
		return [me.OrderCheck,me.CenOrderDtlGrid];
	}
});