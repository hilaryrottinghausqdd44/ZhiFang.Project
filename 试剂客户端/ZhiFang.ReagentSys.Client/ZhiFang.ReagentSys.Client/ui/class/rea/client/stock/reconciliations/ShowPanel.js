/**
 * 入库对帐
 * @author liangyl
 * @version 2018-10-22
 */
Ext.define('Shell.class.rea.client.stock.reconciliations.ShowPanel', {
	extend: 'Ext.panel.Panel',

	title: '入库对帐',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.reconciliations.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.stock.reconciliations.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 145,
            split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	onSeach:function(id){
		var me =this;
		if(!id)return;
	    me.DtlGrid.InDocID=id;
	    me.DocForm.PK=id;
	    me.DocForm.isShow(id);
	    me.DtlGrid.onSearch();
	},
	clearData:function(){
		var me = this;
		me.DtlGrid.clearData();	
		me.DocForm.clearData();
	}
});