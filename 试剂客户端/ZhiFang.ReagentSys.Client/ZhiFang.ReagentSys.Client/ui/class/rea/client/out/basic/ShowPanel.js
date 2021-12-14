/**
 * 出库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.basic.ShowPanel', {
	extend: 'Ext.panel.Panel',

	title: '出库信息',
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
    defaluteOutType:'1',
    /**出库总单*/
	formPanel:'Shell.class.rea.client.out.basic.Form',
	
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.basic.ShowDtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center'
		});
		me.DocForm = Ext.create(me.formPanel, {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 145,
			defaluteOutType:me.defaluteOutType,
            split: true,
			collapsible: true,
			PK:me.PK,
			collapseMode:'mini'	
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	onSearch:function(id){
		var me =this;
		me.DocForm.PK=id;
	    me.DocForm.isShow(id);
		me.DtlGrid.defaultWhere='reabmsoutdtl.OutDocID='+id;
	    me.DtlGrid.onSearch();
	},
	clearData:function(){
		var me = this;
		me.DtlGrid.clearData();	
		me.DocForm.clearData();
	}
});