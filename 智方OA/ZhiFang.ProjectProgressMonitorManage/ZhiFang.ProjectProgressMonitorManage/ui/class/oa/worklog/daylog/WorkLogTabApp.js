/**
 * 日志工作计划
 * @author liangyl
 * @version 2016-08-04
 */
Ext.define('Shell.class.oa.worklog.daylog.WorkLogTabApp', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '日总结/日计划',
	border: true,
	closable: false,
	/**功能按钮栏位置*/
	buttonDock: 'top',
	/**对外公开:1:普通员工(能查看我的工作日志,@我的工作日志),2:普通管理员(能查看我的工作日志,@我的工作日志,我的下属的工作日志 );3:查看到所有的工作日志 */
	TYPE: '3',
	initComponent: function() {
		var me = this;
		me.items = [];
		me.bodyPadding = 1;
		me.title = me.title || "周工作计划";

		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//me.activeTab =me.activeTab;
	},
	createItems: function() {
		var me = this;
		me.MyWeekLogApp = Ext.create('Shell.class.oa.worklog.daylog.App', {
			itemId: 'MyWeekLogApp',
			border: false,
			title: '日总结/日计划',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'MEOWN',
			ownempid: '-1',
			PK: me.PK
		});
		me.SendForMyWeekLogGrid = Ext.create('Shell.class.oa.worklog.daylog.Grid', {
			itemId: 'SendForMyWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '@我',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'COPYFORME',
			ownempid: '-1',
			PK: me.PK
		});
		me.MyEmpWeekLogGrid = Ext.create('Shell.class.oa.worklog.daylog.Grid', {
			itemId: 'MyEmpWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '下属',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'SENDFORME',
			ownempid: '-1',
			PK: me.PK
		});

		me.AllWeekLogGrid = Ext.create('Shell.class.oa.worklog.daylog.Grid', {
			itemId: 'AllWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '全部',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'ALL',
			ownempid: '-1',
			PK: me.PK
		});
		var arr = [];
		switch(me.TYPE) {
			case '1':
				arr = [me.MyWeekLogApp, me.SendForMyWeekLogGrid];
				break;
			case '2':
				arr = [me.MyWeekLogApp, me.SendForMyWeekLogGrid, me.MyEmpWeekLogGrid];
				break;
			case '3':
				arr = [me.MyWeekLogApp, me.SendForMyWeekLogGrid, me.MyEmpWeekLogGrid, me.AllWeekLogGrid];
				break;
			default:
				arr = [me.MyWeekLogApp, me.SendForMyWeekLogGrid, me.MyEmpWeekLogGrid];
				break;
		}
		return arr;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		toolbar = null;
		return toolbar;
	}
})