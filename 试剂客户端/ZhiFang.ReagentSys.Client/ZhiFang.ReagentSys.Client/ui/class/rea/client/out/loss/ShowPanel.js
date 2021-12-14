/**
 * 报损出库
 * @author longfc
 * @version 2019-03-20
 */
Ext.define('Shell.class.rea.client.out.loss.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '报损出库',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},

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
		me.ShowDtlGrid = Ext.create('Shell.class.rea.client.out.basic.ShowDtlGrid', {
			header: false,
			itemId: 'ShowDtlGrid',
			region: 'center'
		});
		me.DocForm = Ext.create("Shell.class.rea.client.out.search.Form", {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 185,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.ShowDtlGrid, me.DocForm];
		return appInfos;
	},
	/**根据入库id加载*/
	onSearch: function(id) {
		var me = this;
		me.DocForm.PK = id;
		me.DocForm.isShow(id);
		me.ShowDtlGrid.defaultWhere = 'reabmsoutdtl.OutDocID=' + id;
		me.ShowDtlGrid.onSearch();
	},
	clearData: function() {
		var me = this;
		me.DocForm.PK = null;
		me.ShowDtlGrid.clearData();
		me.DocForm.clearData();
	}
});
