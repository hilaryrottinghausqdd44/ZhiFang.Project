/**
 * 出库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.refund.check.ShowGrid', {
	extend: 'Shell.class.rea.client.out.basic.ShowDtlGrid',

	title: '出库明细列表',
	width: 800,
	height: 500,
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**查询数据*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDtlByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsOutDtl_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	loadDataById: function(id) {
		var me = this;
		if(!id) {
			me.store.removeAll();
			return;
		} else {
			me.defaultWhere = 'reabmsoutdtl.OutDocID=' + id;
			me.onSearch();
		}
	},
	/**选择明细*/
	getDtlInfo: function() {
		var me = this;
		var records = me.store.data.items;
		return records;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: '出库关联的库存IDStr',
			hidden: true,
			width: 100,
			defaultRenderer: true
		});
		return columns;
	}
});