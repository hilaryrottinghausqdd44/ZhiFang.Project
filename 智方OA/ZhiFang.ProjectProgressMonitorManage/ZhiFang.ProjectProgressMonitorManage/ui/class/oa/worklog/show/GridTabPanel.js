/**
 * 日报统计tab列表
 * @author longfc
 * @version 2016-10-21
 */
Ext.define('Shell.class.oa.worklog.show.GridTabPanel', {
	extend: 'Ext.tab.Panel',
	header: false,
	activeTab: 0,
	hideHeaders: false,
	title: '日报统计',
	border: false,
	closable: true,
	/**是否重置按钮*/
	hasReset: false,
	/**是否启用取消按钮*/
	hasCancel: false,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	isloadEmpGrid: false,

	IsIncludeSubDept: false,
	EMPID: null,
	DeptId: null,
	initComponent: function() {
		var me = this;
		me.width = me.width;
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//页签切换处理
		me.ontabchange();
		me.activeTab = 0;
	},
	/**
	 * 隐藏 tab
	 * @param tabPanel
	 * @param tab
	 * @returns {boolean}
	 */
	hideTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.hide();
		tab.tab.hide();
	},
	/**
	 * 显示 tab
	 * @param tabPanel
	 * @param tab
	 */
	showTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.show();
		tab.tab.show();
		me.setActiveTab(tab);
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
	},

	/**加载部门员工日报统计内容信息*/
	loadDeptEmpGrid: function() {
		var me = this;
	},
	/**加载员工日报统计内容信息*/
	loadEmpGrid: function() {
		var me = this;
		me.isloadEmpGrid = true;
	},

	createItems: function() {
		var me = this;
		me.DeptEmpGrid = Ext.create("Shell.class.oa.worklog.show.DeptEmpGrid", {
			region: 'center',
			header: false,
			title: "部门员工日报总结统计信息",
			itemId: 'DeptEmpGrid'
		});
		me.EmpGrid = Ext.create("Shell.class.oa.worklog.show.EmpGrid", {
			region: 'center',
			header: false,
			hidden: true,
			title: "员工日报总结统计信息",
			itemId: 'EmpGrid'
		});
		return [me.DeptEmpGrid, me.EmpGrid];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'top',
			hidden: true,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return items;
	},

	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'DeptEmpGrid':
						me.DeptEmpGrid.onSearch();
						break;
					case 'EmpGrid':
						me.EmpGrid.GridSearch();
						break;
					default:
						break
				}
			}
		});
	}

});