/**
 * 选择出库单
 * @author liangyl
 * @version 2018-03-19
 */
Ext.define('Shell.class.rea.client.out.refund.check.App', {
	extend: 'Ext.panel.Panel',

	title: '出库单选择',
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.DocGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('ReaBmsOutDoc_Id');
					me.ShowGrid.loadDataById(id);
				},null,200);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('ReaBmsOutDoc_Id');
					me.ShowGrid.loadDataById(id);
				},null,200);
			},
			accept:function(p,records){
				JShell.Action.delay(function(){
					var docId = records.get('ReaBmsOutDoc_Id'); 
	                var recs = me.ShowGrid.store.data.items;
					me.fireEvent('accept',me,recs,docId);
				},null,500);
			},
			nodata:function(p){
				me.ShowGrid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.out.refund.check.DocGrid', {
			header: false,
			title: '出库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		me.ShowGrid = Ext.create('Shell.class.rea.client.out.refund.check.ShowGrid', {
			header: false,
			itemId: 'ShowGrid',
			region: 'center',
			layout:'fit',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.ShowGrid];
		return appInfos;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});