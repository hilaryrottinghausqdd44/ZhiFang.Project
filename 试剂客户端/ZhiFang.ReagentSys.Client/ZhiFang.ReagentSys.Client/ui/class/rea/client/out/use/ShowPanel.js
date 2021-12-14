/**
 * 使用出库管理
 * @author longfc
 * @version 2019-03-14
 */
Ext.define('Shell.class.rea.client.out.use.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

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
	/**出库类型*/
	defaluteOutType: '1',
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
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.use.ShowDtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center'
		});
		me.DocForm = Ext.create("Shell.class.rea.client.out.search.Form", {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 185,			
			split: true,
			collapsible: true,
			collapseMode: 'mini',
			defaluteOutType: me.defaluteOutType
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	onSearch: function(id) {
		var me = this;
		me.DocForm.PK = id;
		me.DocForm.isShow(id);
		me.DtlGrid.defaultWhere = 'reabmsoutdtl.OutDocID=' + id;
		me.DtlGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		me.DtlGrid.clearData();
		me.DocForm.clearData();
	}
});