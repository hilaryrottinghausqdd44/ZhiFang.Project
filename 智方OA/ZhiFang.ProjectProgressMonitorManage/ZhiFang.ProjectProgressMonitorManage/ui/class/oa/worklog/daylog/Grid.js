/**
 * 工作日志--日报信息
 * @author liangyl
 * @version 2016-08-02
 */
Ext.define('Shell.class.oa.worklog.daylog.Grid', {
	extend: 'Shell.class.oa.worklog.basic.Grid',
	title: '日报信息',
	width: 360,
	height: 500,
		/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DataAddTime',
		direction: 'DESC'
	}],
	defaultPageSize: 50,
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,

	hasPagingtoolbar: true,
	/**周默认值*/
	defaultDateTypeValue: '1',
	hasRownumberer: false,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**查询栏包含周选项*/
	hasWeeked: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	columnLines: true,
	/**查询对象*/
	objectEName: 'WorkLogDay',
	worklogtype: 'WorkLogDay',
	
	/**日志外键名称
	 * @author Jcall
	 * @version 2016-08-19
	 */
	LogName:'WorkDayLog',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	sendtype:'ALL',
	initComponent: function() {
		var me = this;
		me.initData();
		me.callParent(arguments);
		
	},
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = [];

		me.buttonToolbarItems.push({
			width: 155,
			labelWidth: 60,
			labelAlign: 'right',
			fieldLabel: '时间',
			itemId: 'BeginDate',
			value: me.BeginDate,
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			value: me.EndDate,
			xtype: 'datefield',
			format: 'Y-m-d'
		});
		me.buttonToolbarItems.push({
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onSearch();
			}
		}, '-', '->');
		if(me.hasRefresh) {
			me.buttonToolbarItems.push('refresh');
		}
		if(me.hasAdd) {
			me.buttonToolbarItems.push('add');
		}
		if(me.hasEidt) {
			me.buttonToolbarItems.push('edit');
		}
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			StartDate = "",
			EndDate = "",
			params = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar.getComponent('EndDate').getValue();
//		var BeginDate = Year + '-' + Month + '-01';
//		EndDate = JcallShell.Date.getMonthLastDate(Year, Month);

		
		params.push("&sendtype=" + me.sendtype);
		params.push("&worklogtype=" + me.worklogtype);
		params.push("&empid=" + me.ownempid);
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(EmpID != "-1" && EmpID != "") {
			params.push("&ownempid=" + EmpID);
			
		}
	
		if(BeginDate!=null && EndDate!=null) {
			params.push("&sd=" + JShell.Date.toString(BeginDate, true) );
			params.push("&ed=" + JShell.Date.toString(EndDate, true) );
		}
		
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
		/**初始化数据*/
	initData: function() {
		var me = this;
		//时间处理
		var date = JShell.Date.getNextDate(new Date(),0);

		var year = date.getFullYear();
		var month = date.getMonth() + 1;

		me.BeginDate = JShell.Date.getMonthFirstDate(year,month);
		me.EndDate = JShell.Date.getMonthLastDate(year,month);
		
	}
});