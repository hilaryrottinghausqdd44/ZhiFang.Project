/**
 * 货品选择
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.apply.StockApp', {
	extend: 'Shell.class.rea.client.out.stock.App',
	title: '货品选择',
	/**表单选中的库房*/
	StorageObj: {},

	createItems: function() {
		var me = this;
		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.out.apply.QtyDtlGrid', {
			header: false,
			itemId: 'QtyDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false,
			StorageObj: me.StorageObj
		});
		me.NearTermPeriodGrid = Ext.create('Shell.class.rea.client.out.stock.Grid', {
			header: false,
			title: '近效期库存货品',
			itemId: 'NearTermPeriodGrid',
			region: 'east',
			width: 280,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.NearTermPeriodGrid, me.QtyDtlGrid];
		return appInfos;
	},
	clearData: function() {
		var me = this;
		me.NearTermPeriodGrid.store.removeAll();
		me.QtyDtlGrid.store.removeAll();
	}
});
