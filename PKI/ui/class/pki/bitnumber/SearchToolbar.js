/**
 * 财务查询条件
 * @author liangy1
 * @version 2017-12-01
 */
Ext.define('Shell.class.pki.bitnumber.SearchToolbar', {
	//extend: 'Ext.toolbar.Toolbar',
	extend: 'Shell.class.pki.search.SearchParams',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],

	/**是否包含财务*/
	hasFinanceLock: false,

	/**默认选中日期方式*/
	isDateRadio: false,

	/**时间类型默认值*/
	defaultDateTypeValue: '1',
	/**开票方类型默认值*/
	defaultBillingUnitTypeValue: 0,
	/**对账状态默认值*/
	defaultIsLockedValue: 0,
	/**价格类型默认值*/
	defaultItemPriceTypeValue: 0,
	/**匹配合同价格默认值*/
	defaultIsGetPriceValue: 0,
	/**样本项目状态默认值*/
	defaultSampleStateValue: 0,
	/**时间类型列表*/
	DateTypeList: [
		['1', '录入时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'], //OperDate
		['2', '采样时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'], //CollectDate
		['3', '核收时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'], //ReceiveDate
		['4', '审定时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';'], //CheckDate
		['5', '二审时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';'], //SenderTime2
		['6', '对账锁定时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E5'] + ';'], //FirstLockedDate
		['7', '财务锁定时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E6'] + ';'] //IsLockedDate
	],
	/**开票方类型列表*/
	BillingUnitTypeList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '送检单位', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2', '经销商', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['3', '个人', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
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
	/**匹配状态*/
	IsGetPriceList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['0', '未匹配', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'],
		['1', '已匹配', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['-1', '匹配错误', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
	],
	/**价格类型列表*/
	ItemPriceTypeList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['3', '免单价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';'],
		['4', '终端价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']
	],
	/**样本项目状态*/
	SampleStateList: [
		[0, '全部', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';'],
		['Report', '一审', 'font-weight:bold;color:black;'],
		['Report_Second', '二审', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['Refuse', '已退回', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';']
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
		//检验项目【勾选列表】
		items.push({
			x: 5,
			y: 30,
			fieldLabel: '检验项目',
			itemId: 'TestItem_CName',
			xtype: 'uxCheckTrigger',
			classConfig: {
				title: '本公司名称选择',
				defaultWhere:'btestitem.IsSanger=1',
			},
			className: 'Shell.class.pki.item.CheckGrid'
		}, {
			fieldLabel: '检验项目主键ID',
			itemId: 'TestItem_Id',
			hidden: true
		});
		//销售区域（模糊查询）
		items.push({
			x: 5,
			y: 55,
			fieldLabel: '销售区域',
			emptyText: '模糊查询',
			itemId: 'Seller_AreaIn'
		});

		//经销商【勾选列表】
		items.push({
			x: 205,
			y: 5,
			width: 190,
			labelWidth: 50,
			fieldLabel: '经销商',
			itemId: 'Dealer_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.dealer.CheckGrid'
		}, {
			fieldLabel: '经销商主键ID',
			itemId: 'Dealer_Id',
			hidden: true
		});
		//开票方【勾选列表】
		items.push({
			x: 205,
			y: 30,
			width: 190,
			labelWidth: 50,
			fieldLabel: '开票方',
			itemId: 'BillingUnit_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.billingunit.CheckGrid'
		}, {
			fieldLabel: '开票方主键ID',
			itemId: 'BillingUnit_Id',
			hidden: true
		});
		//销售【勾选列表】
		items.push({
			x: 205,
			y: 55,
			width: 190,
			labelWidth: 50,
			fieldLabel: '销售',
			itemId: 'Seller_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.seller.CheckGrid'
		}, {
			fieldLabel: '销售主键ID',
			itemId: 'Seller_Id',
			hidden: true
		});

		//开票方类型（全部、个人、经销商、送检单位）
		items.push({
			x: 395,
			y: 5,
			width: 150,
			labelWidth: 70,
			fieldLabel: '开票方类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'BillingUnitType',
			hasStyle: true,
			value: me.defaultBillingUnitTypeValue,
			data: me.BillingUnitTypeList
		});
		//价格类型（合同价、免单价、终端价格、阶梯价）
		items.push({
			x: 395,
			y: 30,
			width: 150,
			labelWidth: 70,
			fieldLabel: '价格类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'ItemPriceType',
			hasStyle: true,
			value: me.defaultItemPriceTypeValue,
			data: me.ItemPriceTypeList
		});
		//状态（待对账，对账中，对账错误，财务锁定）
		items.push({
			x: 395,
			y: 55,
			width: 150,
			labelWidth: 70,
			fieldLabel: '对账状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'IsLocked',
			hasStyle: true,
			value: me.defaultIsLockedValue,
			data: me.getIsLockedList()
		});

		//预制条码
		items.push({
			x: 545,
			y: 5,
			fieldLabel: '预制条码',
			itemId: 'SerialNo'
		});
		//上机条码
		items.push({
			x: 545,
			y: 30,
			fieldLabel: '上机条码',
			itemId: 'BarCode'
		});
		//姓名（模糊查询）
		items.push({
			x: 545,
			y: 55,
			fieldLabel: '姓名',
			emptyText: '模糊查询',
			itemId: 'NRequestForm_CName'
		});

		//时间条件
		items.push({
			x: 5,
			y: 80,
			width: 160,
			fieldLabel: '时间类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			hasStyle: true,
			value: me.defaultDateTypeValue,
			data: me.getDateTypeList()
		}, {
			x: 175,
			y: 80,
			width: 50,
			itemId: 'radio2',
			boxLabel: '年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		}, {
			x: 220,
			y: 80,
			width: 70,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		}, {
			x: 285,
			y: 80,
			width: 55,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio,
			margin: '0 2px 0 10px'
		}, {
			x: 355,
			y: 80,
			width: 50,
			itemId: 'radio1',
			boxLabel: '日期',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		}, {
			x: 405,
			y: 80,
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		}, {
			x: 500,
			y: 80,
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

		items.push({
			x: 605,
			y: 80,
			width: 140,
			labelWidth: 50,
			fieldLabel: '<b>对帐人</b>',
			emptyText: '模糊查询',
			itemId: 'NRequestItem_FirstLocker'
		});

		//委托单位【勾选列表】
		items.push({
			x: 760,
			y: 5,
			width: 200,
			fieldLabel: '委托单位',
			itemId: 'SampleSendPlace_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.laboratory.SampleSendPlaceGrid'
		}, {
			fieldLabel: '委托单位主键ID',
			itemId: 'SampleSendPlace_Id',
			hidden: true
		});
		//匹配合同价格（全部,未匹配已匹配, 匹配错误)
		items.push({
			x: 740,
			y: 30,
			width: 220,
			labelWidth: 80,
			fieldLabel: '匹配状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'IsGetPrice',
			hasStyle: true,
			value: me.defaultIsGetPriceValue,
			data: me.IsGetPriceList
		});
		//样本项目状态（一审,二审,退回,空(全部))
		items.push({
			x: 740,
			y: 55,
			width: 220,
			labelWidth: 80,
			fieldLabel: '样本状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'SampleState',
			hasStyle: true,
			value: me.defaultSampleStateValue,
			data: me.getSampleStateList()
		});
		items.push({
			x: 740,
			y: 80,
			width: 220,
			labelWidth: 80,
			fieldLabel: '<b>对帐批次号</b>',
			emptyText: '模糊查询',
			itemId: 'NRequestItem_LockBatchNumber'
		});
		if(me.hasFinanceLock) {
			//是否返差价（全部、是、否）
			items.push({
				x: 960,
				y: 5,
				labelWidth: 70,
				width: 130,
				fieldLabel: '是否返差价',
				xtype: 'uxBoolComboBox',
				itemId: 'IsSpread',
				hasAll: true,
				value: null
			});
			//取阶梯价（全部、是、否）
			items.push({
				x: 960,
				y: 30,
				labelWidth: 70,
				width: 130,
				fieldLabel: '取阶梯价',
				xtype: 'uxBoolComboBox',
				itemId: 'IsStepPrice',
				hasAll: true,
				value: null
			});
			//是否免单（全部、是、否）
			items.push({
				x: 960,
				y: 55,
				labelWidth: 70,
				width: 130,
				fieldLabel: '是否免单',
				xtype: 'uxBoolComboBox',
				itemId: 'IsFree',
				hasAll: true,
				value: null
			});
		}

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
			y: 80,
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
			x: 1020,
			y: 80,
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
	/**获取时间类型列表*/
	getDateTypeList: function() {
		var me = this;
		if(me.hasFinanceLock) return me.DateTypeList;

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
	/**获取对账状态列表*/
	getIsLockedList: function() {
		var me = this;
		if(me.hasFinanceLock) return me.IsLockedList;

		var IsLockedList = me.IsLockedList,
			len = IsLockedList.length,
			list = [];

		for(var i = 0; i < len; i++) {
			if(IsLockedList[i][0] != '3') {
				list.push(IsLockedList[i]);
			}
		}

		return list;
	},
	/*获取样本项目状态*/
	getSampleStateList: function() {
		var me = this;
		if(me.hasFinanceLock) return me.SampleStateList;

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
			'Laboratory_CName', 'TestItem_CName', 'Dealer_Name',
			'BillingUnit_Name', 'Seller_Name', 'SampleSendPlace_CName'
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
						idNameValue = idName;
						nameValue = name;
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
			FirstLocker = me.getComponent('NRequestItem_FirstLocker'),
			BatchNumber = me.getComponent('NRequestItem_LockBatchNumber'),
			MonthMonth = me.getComponent('MonthMonth');
		var textList = ['Seller_AreaIn', 'SerialNo', 'BarCode', 'NRequestForm_CName', 'NRequestItem_FirstLocker', 'NRequestItem_LockBatchNumber'];

		var comboList = ['BillingUnitType', 'ItemPriceType', //'IsLocked',
			'IsSpread', 'IsStepPrice', 'IsFree', 'IsGetPrice', 'SampleState'
		];
		var checkList = [
			'Laboratory_CName', 'TestItem_CName', 'Dealer_Name',
			'BillingUnit_Name', 'Seller_Name', 'SampleSendPlace_CName'
		];

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
		var textList = ['Seller_AreaIn', 'SerialNo', 'BarCode', 'NRequestForm_CName', 'NRequestItem_FirstLocker', 'NRequestItem_LockBatchNumber'];
		var comboList = ['BillingUnitType', 'ItemPriceType', 'IsLocked', 'IsGetPrice', 'SampleState'];
		var booleanList = ['IsFree', 'IsSpread', 'IsStepPrice'];
		var checkList = [
			'Laboratory_Id', 'TestItem_Id', 'Dealer_Id',
			'BillingUnit_Id', 'Seller_Id', 'SampleSendPlace_Id'
		];
		params = me.getSearchParams(params, textList, "textList");
		params = me.getSearchParams(params, comboList, "comboList");
		params = me.getSearchParams(params, booleanList, "booleanList");
		params = me.getSearchParams(params, checkList, "checkList");

		return params;
	}
});