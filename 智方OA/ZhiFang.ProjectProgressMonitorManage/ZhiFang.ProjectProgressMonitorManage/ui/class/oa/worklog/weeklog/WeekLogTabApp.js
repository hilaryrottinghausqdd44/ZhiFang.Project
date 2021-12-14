/**
 * 周工作计划
 * @author longfc
 * @version 2016-08-01
 */
Ext.define('Shell.class.oa.worklog.weeklog.WeekLogTabApp', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '周总结/周计划',
	border: false,
	closable: false,
	/**功能按钮栏位置*/
	buttonDock: 'top',
	/**对外公开:1:普通员工(能查看我的工作日志,@我的工作日志),2:普通管理员(能查看我的工作日志,@我的工作日志,我的下属的工作日志 );3:查看到所有的工作日志 */
	TYPE: '3',
	tabPosition:'top',
	MyWeekLogAppIsLoaded: false,
	SendForMyWeekLogGridIsLoaded: false,
	MyEmpWeekLogGridIsLoaded: false,
	AllWeekLogGridIsLoaded: false,
	initComponent: function() {
		var me = this;
		me.items = [];
		me.bodyPadding = 1;
		me.title = me.title || "周工作计划";
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
	createItems: function() {
		var me = this;
		var app="Shell.class.oa.worklog.weeklog.MyWeekLogApp";
		me.MyWeekLogApp = Ext.create(app, {
			itemId: 'MyWeekLogApp',
			border: false,
			title: '周总结/周计划',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'MEOWN',
			defaultPageSize: 15,
			hasAdd: true,
			hasEidt: true,
			defaultLoad: true,
			PK: me.PK
		});
		me.SendForMyWeekLogGrid = Ext.create(app, {
			itemId: 'SendForMyWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '@我',
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'COPYFORME',
			defaultLoad: false,
			PK: me.PK
		});
		me.MyEmpWeekLogGrid = Ext.create(app, {
			itemId: 'MyEmpWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '下属',
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'SENDFORME',
			defaultLoad: false,
			PK: me.PK
		});

		me.AllWeekLogGrid = Ext.create(app, {
			itemId: 'AllWeekLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '全部',
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'ALL',
			defaultLoad: false,
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
					case 'MyWeekLogApp':
						if(me.MyWeekLogAppIsLoaded == false) {
							me.MyWeekLogAppIsLoaded = true;
							me.MyWeekLogApp.load();
						}
						break;
					case 'SendForMyWeekLogGrid':
						if(me.SendForMyWeekLogGridIsLoaded == false) {
							me.SendForMyWeekLogGridIsLoaded = true;
							me.SendForMyWeekLogGrid.load();
						}
						break;
					case 'MyEmpWeekLogGrid':
						if(me.MyEmpWeekLogGridIsLoaded == false) {
							me.MyEmpWeekLogGridIsLoaded = true;
							me.MyEmpWeekLogGrid.load();
						}
						break;
					case 'AllWeekLogGrid':
						if(me.AllWeekLogGridIsLoaded == false) {
							me.AllWeekLogGridIsLoaded = true;
							me.AllWeekLogGrid.load();
						}
						break;
					default:

						break
				}
			}
		});
	}
})