/**
 * 月工作计划
 * @author longfc
 * @version 2016-08-01
 */
Ext.define('Shell.class.oa.worklog.monthlog.MonthLogTabApp', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '月总结/月计划',
	border: false,
	closable: false,
	/**功能按钮栏位置*/
	buttonDock: 'top',
	/**对外公开:1:普通员工(能查看我的工作日志,@我的工作日志),2:普通管理员(能查看我的工作日志,@我的工作日志,我的下属的工作日志 );3:查看到所有的工作日志 */
	TYPE: '3',
	MyMonthLogAppIsLoaded: false,
	SendForMyMonthLogGridIsLoaded: false,
	MyEmpMonthLogGridIsLoaded: false,
	AllMonthLogGridIsLoaded: false,
	initComponent: function() {
		var me = this;
		me.items = [];
		me.bodyPadding = 1;
		me.title = me.title || "月工作计划";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ontabchange();
		me.activeTab = 0;
	},
	createItems: function() {
		var me = this;
		var app='Shell.class.oa.worklog.monthlog.MyMonthLogApp';
		me.MyMonthLogApp = Ext.create(app, {
			itemId: 'MyMonthLogApp',
			border: false,
			title: '月总结/月计划',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			sendtype: 'MEOWN',
			hasAdd: true,
			hasEidt: true,
			defaultLoad: true,
			PK: me.PK
		});
		me.SendForMyMonthLogGrid = Ext.create(app, {
			itemId: 'SendForMyMonthLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '@我',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'COPYFORME',
			PK: me.PK
		});
		me.MyEmpMonthLogGrid = Ext.create(app, {
			itemId: 'MyEmpMonthLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '下属',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'SENDFORME',
			PK: me.PK
		});

		me.AllMonthLogGrid = Ext.create('Shell.class.oa.worklog.monthlog.MyMonthLogApp', {
			itemId: 'AllMonthLogGrid',
			border: false,
			/**是否启用新增按钮*/
			hasAdd: false,
			hasEidt: false,
			title: '全部',
			FTYPE: me.FTYPE,
			height: me.height,
			width: me.width,
			defaultPageSize: 50,
			sendtype: 'ALL',
			PK: me.PK
		});
		var arr = [];
		switch(me.TYPE) {
			case '1':
				arr = [me.MyMonthLogApp, me.SendForMyMonthLogGrid];
				break;
			case '2':
				arr = [me.MyMonthLogApp, me.SendForMyMonthLogGrid, me.MyEmpMonthLogGrid];
				break;
			case '3':
				arr = [me.MyMonthLogApp, me.SendForMyMonthLogGrid, me.MyEmpMonthLogGrid, me.AllMonthLogGrid];
				break;
			default:
				arr = [me.MyMonthLogApp, me.SendForMyMonthLogGrid, me.MyEmpMonthLogGrid];
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
					case 'MyMonthLogApp':
						if(me.MyMonthLogAppIsLoaded == false) {
							me.MyMonthLogAppIsLoaded = true;
							me.MyMonthLogApp.load();
						}
						break;
					case 'SendForMyMonthLogGrid':
						if(me.SendForMyMonthLogGridIsLoaded == false) {
							me.SendForMyMonthLogGridIsLoaded = true;
							me.SendForMyMonthLogGrid.load();
						}
						break;
					case 'MyEmpMonthLogGrid':
						if(me.MyEmpMonthLogGridIsLoaded == false) {
							me.MyEmpMonthLogGridIsLoaded = true;
							me.MyEmpMonthLogGrid.load();
						}
						break;
					case 'AllMonthLogGrid':
						if(me.AllMonthLogGridIsLoaded == false) {
							me.AllMonthLogGridIsLoaded = true;
							me.AllMonthLogGrid.load();
						}
						break;
					default:
						break
				}
			}
		});
	}
})