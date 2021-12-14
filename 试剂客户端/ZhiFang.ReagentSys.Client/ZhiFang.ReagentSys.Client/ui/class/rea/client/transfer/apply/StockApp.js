/**
 * 货品选择
 * @author liangyl
 * @version 2018-11-05
 */
Ext.define('Shell.class.rea.client.transfer.apply.StockApp', {
	extend: 'Shell.class.rea.client.transfer.stock.App',
	title: '货品选择',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.QtyDtlGrid = Ext.create('Shell.class.rea.client.transfer.apply.QtyDtlGrid', {
			header: false,
			itemId: 'QtyDtlGrid',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		me.NearTermPeriodGrid = Ext.create('Shell.class.rea.client.out.stock.Grid', {
			header: false,
			title: '近效期库存',
			itemId: 'NearTermPeriodGrid',
			region: 'east',
			width: 280,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.QtyDtlGrid, me.NearTermPeriodGrid];
		return appInfos;
	}
});