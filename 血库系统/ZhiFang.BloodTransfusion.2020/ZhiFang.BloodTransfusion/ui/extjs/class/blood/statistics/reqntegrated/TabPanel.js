/**
 * 输血申请综合查询:按不同的类型显示
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '',
	bodyPadding: 1,
	//当前选中的申请单
	record: null,
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on({
			tabchange: function (tabPanel, newCard, oldCard, eOpts) {
				me.loadOfTabChange(tabPanel, newCard, oldCard);
			}
		});
		me.activeTab = 0;
	},
	initComponent: function () {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function () {
		var me = this;
		//血袋跟踪按拆分查看
		me.OfSplitPanel = Ext.create("Shell.class.blood.statistics.reqntegrated.OfSplitPanel", {
			title:"血袋跟踪查看(按记录列表)",
			header: true,
			itemId: 'OfSplitPanel'
		});
		//血袋跟踪按汇总查看
		me.OfMergePanel = Ext.create("Shell.class.blood.statistics.reqntegrated.OfMergePanel", {
			header: true,
			hidden:true,
			title:"血袋跟踪按汇总查看",
			itemId: 'OfMergePanel'
		});
		return [me.OfSplitPanel, me.OfMergePanel];
	},
	loadData: function(record) {
		var me = this;
		me.record=record;
		var gridPanel = me.getActiveTab();
		gridPanel.loadData(record);
	},
	onNodata: function() {
		var me = this;
		me.record=null;
		me.OfSplitPanel.onNodata();
	},
	//页签改变后加载对应的列表内容
	loadOfTabChange: function (tabPanel, newCard, oldCard) {
		var me = this;
		newCard.loadData(me.record);
	}
});
