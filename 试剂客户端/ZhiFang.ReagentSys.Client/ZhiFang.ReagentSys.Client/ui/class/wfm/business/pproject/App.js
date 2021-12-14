/**
 * 项目监控
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.App', {
	extend: 'Ext.panel.Panel',
	title: '项目管理',

	layout: 'border',
	bodyPadding: 1,

	width: 1200,
	height: 600,
	/**默认加载*/
	defaultLoad: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();		
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = [{
			dock: 'top',
			itemId: 'SearchToolbar',
			border: false,
			items: [
				Ext.create('Shell.class.wfm.business.pproject.SearchToolbar', {
					border: false,
					itemId: 'SearchToolbar'
				})
			]
		}];
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push(Ext.create('Shell.class.wfm.business.pproject.Grid', {
			region: 'center',
			border: false,
			header: false,
			itemId: 'Grid'
		}));
		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var SearchToolbar = me.getComponent('SearchToolbar').getComponent('SearchToolbar');
		var Grid = me.getComponent('Grid');

		SearchToolbar.on({
			onSearchClick: function(p, params) {
				Grid.clearData();
				Grid.params =params;
				Grid.onSearch();
			},
			save: function(form, toolbar) {
				Grid.onSearch();
			},
			onCopyClick:function(){
				Grid.onSaveCopyProject();
			}
		});
		Grid.store.on({
			load: function(store, records, successful) {
				if(!successful || !records || records.length <= 0) {
					Grid.clearData();
				}
			}
		});
        Grid.on({
        	savecopy:function(grid,win){
        		grid.onSearch();
        	}
        });
		if(me.defaultLoad) {
			JShell.Action.delay(function() {
				SearchToolbar.onSearch();
			});
		}
	}
});