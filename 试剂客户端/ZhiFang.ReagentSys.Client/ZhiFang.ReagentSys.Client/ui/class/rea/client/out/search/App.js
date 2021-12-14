/**
 * 出库查询
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.search.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库查询',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**@description 新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.DocGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.isShow(record);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.isShow(record);
				},null,500);
			},
			nodata:function(p){
				me.ShowPanel.clearData();
			}
		});
		
		me.DocGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.out.search.DocGrid', {
			header: false,
			title: '出库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.out.search.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			border:false,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
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
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
		me.ShowPanel.DtlPanel.formtype = formtype;
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.DocGrid);
	}
});