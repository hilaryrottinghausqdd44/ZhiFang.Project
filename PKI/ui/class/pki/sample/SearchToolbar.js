/**
 * 样本查询条件
 * @author liangyl
 * @version 2017-10-24
 */
Ext.define('Shell.class.pki.sample.SearchToolbar', {
	//extend: 'Ext.toolbar.Toolbar',
	extend: 'Shell.class.pki.search.SearchParams',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**默认选中日期方式*/
	isDateRadio: false,

	/**时间类型默认值*/
	defaultDateTypeValue: '1',
//	/**对账状态默认值*/
//	defaultIsLockedValue: 0,
	/**样本项目状态默认值*/
	defaultSampleStateValue: '3',
	/**时间类型列表*/
	DateTypeList: [
		['1', '录入时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';']
	],
	
	/**对账状态列表*/
	IsLockedList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '待对账', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E1'] + ';'],
		['2', '销售锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E2'] + ';'],
		['3', '财务锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E3'] + ';'],
		['4', '待对账+销售锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E4'] + ';'],
		['5', '销售+财务锁定', 'font-weight:bold;color:' + JcallShell.PKI.Enum.IsLockedColor['E5'] + ';']
	],
	
	/**样本项目状态*/
	SampleStateList: [
		['1', ' 已删除', 'font-weight:bold;color:#FF3030;'],
		['2', '已退回', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['3', ' 已删除+已退回', 'font-weight:bold;color:#9B30FF;']
	],

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
		//初始化检索监听
		me.initFilterListeners();
	},

	initComponent: function() {
		var me = this;
		me.addEvents('search');
		//初始化送检时间
		me.initDate();
		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;

		var date = JShell.Date.getNextDate(new Date(), JShell.PKI.Balance.Dates);

		var year = date.getFullYear();
		var month = date.getMonth() + 1;

		me.BeginDate = JShell.Date.getMonthFirstDate(year, month);
		me.EndDate = JShell.Date.getMonthLastDate(year, month);

		me.YearMonth = year;
		me.MonthMonth = month;

	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		//送检单位【勾选列表】
		items.push({
			x: 5,
			y: 5,
			fieldLabel: '送检单位',
			itemId: 'Laboratory_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.laboratory.CheckGrid'
		}, {
			fieldLabel: '送检单位主键ID',
			itemId: 'Laboratory_Id',
			hidden: true
		});

		//预制条码
		items.push({
			x: 210,
			y: 5,
			fieldLabel: '预制条码',
			itemId: 'SerialNo'
		});
		//上机条码
		items.push({
			x: 415,
			y: 5,
			fieldLabel: '上机条码',
			itemId: 'BarCode'
		});
		//姓名（模糊查询）
		items.push({
			x: 620,
			y: 5,
			fieldLabel: '姓名',
			emptyText: '模糊查询',
			itemId: 'NRequestForm_CName'
		});

		//时间条件
		items.push({
			x: 5,
			y: 30,
			width: 160,
			fieldLabel: '时间类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			hasStyle: true,
			value: me.defaultDateTypeValue,
			data: me.getDateTypeList()
		}, {
			x: 175,
			y: 30,
			width: 50,
			itemId: 'radio2',
			boxLabel: '年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		}, {
			x: 220,
			y: 30,
			width: 70,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		}, {
			x: 285,
			y: 30,
			width: 60,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio,
			margin: '0 2px 0 10px'
		}, {
			x: 360,
			y: 30,
			width: 50,
			itemId: 'radio1',
			boxLabel: '日期',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		}, {
			x: 410,
			y: 30,
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		}, {
			x: 505,
			y: 30,
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.EndDate,
			disabled: !me.isDateRadio
		});

		//样本项目状态（一审,二审,退回,空(全部))
		items.push({
			x: 620,
			y: 30,
//			width: 140,
			labelWidth: 60,
			fieldLabel: '样本状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'SampleState',
			hasStyle: true,
			value: me.defaultSampleStateValue,
			data: me.getSampleStateList()
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
			x: 960,
			y: 30,
			width: 60,
			hidden:true,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		}, {
			x: 1020,
			y: 30,
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			hidden:true,
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onFilterSearch();
			}
		});

		return items;
	},
	/**获取时间类型列表*/
	getDateTypeList: function() {
		var me = this;

		var DateTypeList = me.DateTypeList,
			len = DateTypeList.length,
			list = [];

		for(var i = 0; i < len; i++) {
			if(DateTypeList[i][0] != '7') {
				list.push(DateTypeList[i]);
			}
		}

		return list;
	},
	/*获取样本项目状态*/
	getSampleStateList: function() {
		var me = this;

		var IsSampleItemStatusList = me.SampleStateList,
			len = IsSampleItemStatusList.length,
			list = [];

		for(var i = 0; i < len; i++) {
			list.push(IsSampleItemStatusList[i]);
		}
		return list;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			radio1 = me.getComponent('radio1'),
			radio2 = me.getComponent('radio2');

		radio1.on({
			change: function(field, newValue) {
				if(newValue) {
					me.changeDateType(1);
				}
			}
		});
		radio2.on({
			change: function(field, newValue) {
				if(newValue) {
					me.changeDateType(2);
				}
			}
		});

		var checkList = [
			'Laboratory_CName'
		];

		for(var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			com = me.getComponent(name);

		if(!com) return;

		var idName = name.split('_')[0] + '_Id';
		com.on({
			check: function(p, record) {
				Id = me.getComponent(idName),
					Name = me.getComponent(name);
				var idNameValue = '',
					nameValue = '';
				switch(idName) {
					case 'SampleSendPlace_Id':
//						idNameValue = idName;
//						nameValue = name;
						break;
					default:
						idNameValue = 'B' + idName;
						nameValue = 'B' + name;
						break;
				}
				Id.setValue(record ? record.get(idNameValue) : '');
				Name.setValue(record ? record.get(nameValue) : '');
				p.close();
			}
		});
	},
	/**更改时间方式*/
	changeDateType: function(value) {
		var me = this,
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate'),
			YearMonth = me.getComponent('YearMonth'),
			MonthMonth = me.getComponent('MonthMonth');

		switch(value) {
			case 1:
				BeginDate.enable();
				EndDate.enable();
				YearMonth.disable();
				MonthMonth.disable();
				break;
			case 2:
				YearMonth.enable();
				MonthMonth.enable();
				BeginDate.disable();
				EndDate.disable();
				break;
		}
	},
	/**清空查询内容*/
	onClearSearch: function() {
		var me = this,
			DateType = me.getComponent('DateType'),
			radio2 = me.getComponent('radio2'),
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate'),
			YearMonth = me.getComponent('YearMonth'),
			MonthMonth = me.getComponent('MonthMonth');
		var textList = [ 'SerialNo', 'BarCode', 'NRequestForm_CName'];

		var comboList = ['SampleState'];
		var checkList = ['Laboratory_CName'];

		for(var i in textList) {
			var text = me.getComponent(textList[i]);
			if(text) text.setValue('');
		}
		for(var i in comboList) {
			var combo = me.getComponent(comboList[i]);
			if(combo) combo.setValue(null);
		}
		for(var i in checkList) {
			var check = me.getComponent(checkList[i]);
			var check_Id = me.getComponent(checkList[i].split('_')[0] + '_Id');
			if(check) check.setValue('');
			if(check_Id) check_Id.setValue('');
		}

		if(DateType.getValue() != me.defaultDateTypeValue) {
			DateType.setValue(me.defaultDateTypeValue);
		}

		radio2.setValue(true);
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
			DateType = me.getComponent('DateType'),
			radio1 = me.getComponent('radio1'),

			params = {};

		if(DateType) {
			params.DateType = DateType.getValue();
		}

		if(radio1.getValue()) {
			var BeginDate = me.getComponent('BeginDate'),
				EndDate = me.getComponent('EndDate');
			if(BeginDate && BeginDate.getValue()) {
				params.BeginDate = JShell.Date.toString(BeginDate.getValue(), true);
			}
			if(EndDate && EndDate.getValue()) {
				params.EndDate = JShell.Date.toString(EndDate.getValue(), true);
			}
		} else {
			var YearMonth = me.getComponent('YearMonth'),
				MonthMonth = me.getComponent('MonthMonth');
			if(YearMonth && MonthMonth) {
				var year = YearMonth.getValue();
				var month = MonthMonth.getValue();

				params.BeginDate = JShell.Date.getMonthFirstDate(year, month, true);
				params.EndDate = JShell.Date.getMonthLastDate(year, month, true);
			}
		}
		var textList = [ 'SerialNo','BarCode', 'NRequestForm_CName'];
		var comboList = [ 'SampleState'];
//		var booleanList = ['IsStepPrice'];
		var checkList = [
			'Laboratory_Id'
		];
		params = me.getSearchParams(params, textList, "textList");
		params = me.getSearchParams(params, comboList, "comboList");
		params = me.getSearchParams(params, checkList, "checkList");

		return params;
	}
});