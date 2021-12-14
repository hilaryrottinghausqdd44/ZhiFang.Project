/**
 * 移库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.transfer.apply.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '移库信息',
	header: false,
	border: false,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	/**当前选择的主单Id*/
	PK: null,

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
		me.DocForm = Ext.create('Shell.class.rea.client.transfer.show.Form', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 145,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.DtlGrid = Ext.create('Shell.class.rea.client.transfer.show.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center'
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	loadData: function(id) {
		var me = this;
		me.PK = id;
		me.DocForm.PK = id;
		me.DocForm.isShow(id);
		me.DtlGrid.defaultWhere = 'reabmstransferdtl.TransferDocID=' + id;
		me.DtlGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		me.PK = null;
		me.DtlGrid.clearData();
		me.DocForm.clearData();
	}
});