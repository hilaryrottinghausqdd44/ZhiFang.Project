/**
 * 授权申请
 * @author longfc
 * @version 2016-12-18
 */
Ext.define('Shell.class.wfm.authorization.apply.TabPanel', {
	extend: 'Ext.tab.Panel',
	title: '授权申请',

	width: 1200,
	height: 400,
	padding:1,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				switch(newCard.itemId) {
					case "SingleGrid":
						me.SingleGrid.clearData();
						me.SingleGrid.onSearch();
						break;
					case "ServerGrid":
						me.ServerGrid.clearData();
						me.ServerGrid.onSearch();
						break;
					default:
						break;
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.SingleGrid = Ext.create('Shell.class.wfm.authorization.ahsingle.apply.Grid', {
			title: '单站点授权申请',
			itemId: 'SingleGrid'
		});
		//Shell.class.wfm.authorization.ahserver.apply.App
		me.ServerGrid = Ext.create('Shell.class.wfm.authorization.ahserver.apply.ServerGrid', {
			title: '服务器授权申请',
			itemId: 'ServerGrid'
		});
		return [me.SingleGrid, me.ServerGrid];
	}
});