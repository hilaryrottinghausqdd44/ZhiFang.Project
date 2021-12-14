/**
 * 环境卫生学及消毒灭菌效果监测--按全部科室
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.statistics.infection.App', {
	extend: 'Ext.tab.Panel',

	title: '环境卫生学及消毒灭菌效果监测',
	header: false,

	afterRender: function() {
		var me = this; 
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'OfDeptGrid':
						me.OfDeptGrid.onSearch();
						break;
					case 'OfDateTime':
						me.OfDateTime.onSearch();
						break;
					case 'OfEvaluationDate':
						me.OfEvaluationDate.onSearch();
						break;
					default:
						
						break
				}
			}
		});
		me.activeTab = 0;
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.OfHRegistrChecklist = Ext.create('Shell.class.assist.statistics.infection.ofalldept.OfHRegistrChecklist', {
			title: '院感登记清单',
			header: true,
			itemId: 'OfHRegistrChecklist'
		});
		me.OfDeptGrid = Ext.create('Shell.class.assist.statistics.infection.ofalldept.OfDeptGrid', {
			title: '按科室',
			header: true,
			itemId: 'OfDeptGrid'
		});
		me.OfDateTime = Ext.create('Shell.class.assist.statistics.infection.ofalldept.OfDateTimeGrid', {
			title: '按季度',
			header: true,
			itemId: 'OfDateTime'
		});
		me.OfEvaluationDate = Ext.create('Shell.class.assist.statistics.infection.ofalldept.OfEvaluationDate', {
			title: '评价报告表',
			header: true,
			itemId: 'OfEvaluationDate'
		});
		var appInfos = [me.OfHRegistrChecklist,me.OfDeptGrid, me.OfDateTime, me.OfEvaluationDate];
		return appInfos;
	},
	clearData: function() {
		var me = this;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.checkRecord = null;
		me.setFormType("show");
		me.OfDeptGrid.clearData();
		me.OfDateTime.clearData();
		me.OfEvaluationDate.clearData();
		me.clearData();
	}
});