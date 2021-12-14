/**
 * 操作记录日志查看查询条件
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.search.SearchBSysOperateToolbar', {
	extend: 'Ext.toolbar.Toolbar',
	//extend: 'Shell.class.pki.search.SearchParams',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	/**默认选中日期方式*/
	isDateRadio: false,
	/**布局方式*/
	layout: 'absolute',
	/**默认组件*/
	defaultType: 'textfield',
	/** 每个组件的默认属性*/
	defaults: {
		width: 200,
		labelWidth: 60,
		labelAlign: 'right'
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},

	initComponent: function() {
		var me = this;
		me.addEvents('search');
		//初始化数据
		me.initData();
		me.items = me.createItems();

		me.callParent(arguments);
	},

	/**初始化数据*/
	initData: function() {
		var me = this;
		//时间处理
		//		var date = JShell.Date.getNextDate(new Date(), JShell.PKI.Balance.Dates);
		//
		//		var year = date.getFullYear();
		//		var month = date.getMonth() + 1;
		//		
		//		me.BeginDate = JShell.Date.getMonthFirstDate(year, month);
		//		me.EndDate = JShell.Date.getMonthLastDate(year, month);

		me.BeginDate = JShell.Date.getNextDate(new Date(), 0);
		me.EndDate = JShell.Date.getNextDate(new Date(), 0);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//时间条件
		items.push({
			x: 5,
			y: 5,
			width: 95,
			allowBlank: false,
			emptyText: '必填项',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate
		}, {
			x: 110,
			y: 5,
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false,
			emptyText: '必填项',
			value: me.EndDate
		});

		//操作
		var buttons = me.createButtons();
		if(buttons) {
			items = items.concat(buttons);
		}

		return items;
	},
	/**创建功能按钮*/
	createButtons: function() {
		var me = this,
			items = [];

		items.push({
			x: 215,
			y: 5,
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		}, {
			x: 285,
			y: 5,
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onFilterSearch();
			}
		});

		return items;
	},

	/**清空查询内容*/
	onClearSearch: function() {
		var me = this,
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate');
		BeginDate.setValue("");
		EndDate.setValue("");
	},
	/**查询处理*/
	onFilterSearch: function() {
		var me = this;
		var params = me.getParams();
		me.fireEvent('search', me, params);
	},
	/**获取参数*/
	getParams: function() {
		var me = this,
			params = {};
		
		var BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate');
		if(BeginDate && BeginDate.getValue()) {
			params.BeginDate = JShell.Date.toString(BeginDate.getValue(), true);
		}
		if(EndDate && EndDate.getValue()) {
			params.EndDate = JShell.Date.toString(EndDate.getValue(), true);
		}
		return params;
	}
});