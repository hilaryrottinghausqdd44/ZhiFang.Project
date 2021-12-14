/**
 * 项目监控
 * @author longfc
 * @version 2017-04-01
 */
Ext.define('Shell.class.wfm.business.pproject.TabPanel', {
	extend: 'Ext.tab.Panel',
	title: '项目监控',

	width: 600,
	height: 400,

	/**ID*/
	ProjectID: null,
	PK:null,
	/**计划开始时间*/
	EstiStartTime: null,
	/**计划结束时间*/
	EstiEndTime: null,
	/**工具栏项的显示值*/
	initItemsValue: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.initItemsValue) {
			me.initButtonToolbarItemsValue(me.initItemsValue);
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		//创建功能按钮栏Items
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createDockedItems: function() {
		var me = this;
		var items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items.push(Ext.create('Ext.toolbar.Toolbar', {
				dock: 'top',
				itemId: 'buttonsToolbar',
				items: me.createButtontoolbar()
			}));
		}
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '项目名称',
			labelWidth: 65,
			readOnly: true,
			width: 285,
			xtype: 'displayfield',
			name: 'CName',
			itemId: 'CName'
		});
		items.push({
			fieldLabel: '进场时间',
			labelWidth: 65,
			readOnly: true,
			width: 145,
			xtype: 'displayfield',
			name: 'EntryTime',
			itemId: 'EntryTime'
		});
		items.push({
			fieldLabel: '进度状态',
			labelWidth: 65,
			readOnly: true,
			width: 120,
			xtype: 'displayfield',
			name: 'PaceEvalName',
			itemId: 'PaceEvalName'
		});
		items.push({
			fieldLabel: '实施负责人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'ProjectLeader',
			itemId: 'ProjectLeader'
		});
		items.push({
			fieldLabel: '销售负责人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'Principal',
			itemId: 'Principal'
		});
		items.push({
			fieldLabel: '进度监督人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'PhaseManager',
			itemId: 'PhaseManager'
		}, {
	        xtype: 'label',
	        text: '没有延期'
	   },{
	        xtype: 'label',
	        text: '    ',
	        width:20,
	        height:10,
	        style : 'background-color:#1A1AE6',
	        margin: '0 10 5 0'
	   },{
	        xtype: 'label',
	        text: '  延期'
	   },{
	        xtype: 'label',
	        text: '    ',
	        width:20,
	        height:10,
	        style : 'background-color:#FFFF00',
	        margin: '0 10 5 0'
	   },{
	        xtype: 'label',
	        text: '  严重延期'
	   },{
	        xtype: 'label',
	        text: '    ',
	        width:20,
	        height:10,
	        style : 'background-color:#EE0000',
	        margin: '0 10 5 0'
	   });
		return items;
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.ScheduleGrid = Ext.create('Shell.class.wfm.business.pproject.schedule.Grid', {
			itemId: 'ScheduleGrid',
			border: false,
			EstiStartTime: me.EstiStartTime,
			EstiEndTime: me.EstiEndTime,
			initItemsValue: me.initItemsValue,
			ProjectID: me.ProjectID
		});
		me.DocumentGrid = Ext.create('Shell.class.wfm.business.pproject.document.Grid', {
			itemId: 'DocumentGrid',
			border: false,
			hidden:true,
			initItemsValue: me.initItemsValue,
			ProjectID: me.ProjectID
		});
		return [me.ScheduleGrid, me.DocumentGrid];
	},
	/**
	 * 隐藏 tab
	 */
	hideTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.hide();
		tab.tab.hide();
	},
	/**
	 * 显示 tab
	 */
	showTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.show();
		tab.tab.show();
		me.setActiveTab(tab);
	},
	/**创建功能按钮栏Items*/
	initButtonToolbarItemsValue: function(data) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar && data) {
			var CName = buttonsToolbar.getComponent('CName');
			var EntryTime = buttonsToolbar.getComponent('EntryTime');
			var PaceEvalName = buttonsToolbar.getComponent('PaceEvalName');
			var ProjectLeader = buttonsToolbar.getComponent('ProjectLeader');
			var Principal = buttonsToolbar.getComponent('Principal');
			var PhaseManager = buttonsToolbar.getComponent('PhaseManager');

			if(CName && data.CName) CName.setValue(data.CName);
			if(EntryTime && data.EntryTime) {
				data.EntryTime = Ext.util.Format.date(data.EntryTime, 'Y-m-d');
				EntryTime.setValue(data.EntryTime)
			}

			if(PaceEvalName && data.PaceEvalName) PaceEvalName.setValue(data.PaceEvalName);
			if(ProjectLeader && data.ProjectLeader) ProjectLeader.setValue(data.ProjectLeader);
			if(Principal && data.Principal) Principal.setValue(data.Principal);
			if(PhaseManager && data.PhaseManager) Principal.setValue(data.PhaseManager);
		}
	}
});