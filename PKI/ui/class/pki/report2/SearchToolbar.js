/**
 * 报表查询条件
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.SearchToolbar', {
	//extend: 'Ext.toolbar.Toolbar',
	extend: 'Shell.class.pki.search.SearchParams',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	height: 105,

	/**是否包含财务*/
	hasFinanceLock: false,

	/**默认选中日期方式*/
	isDateRadio: false,

	/**时间类型默认值*/
	defaultDateTypeValue: '1',
	/**开票方类型默认值*/
	defaultBillingUnitTypeValue: 0,
	/**对账状态默认值*/
	defaultIsLockedValue: '3',

	/**时间类型列表*/
	DateTypeList: [
		['1', '录入时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'], //OperDate
		['2', '采样时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'], //CollectDate
		['3', '核收时间', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'] //ReceiveDate
	],
	/**开票方类型列表*/
	BillingUnitTypeList: JShell.PKI.Enum.getList('UnitType', true, true),
	/**对账状态列表*/
	IsLockedList: JShell.PKI.Enum.getList('IsLocked', true, true),

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
			y: 30,
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
			y: 55,
			fieldLabel: '检验项目',
			itemId: 'TestItem_CName',
			xtype: 'uxCheckTrigger',
			searchInfo: {
				width: 170,
				emptyText: '项目名称',
				isLike: true,
				fields: ['btestitem.CName']
			},
			className: 'Shell.class.pki.item.CheckGrid'
		}, {
			fieldLabel: '检验项目主键ID',
			itemId: 'TestItem_Id',
			hidden: true
		});
		//销售【勾选列表】
		items.push({
			x: 5,
			y: 80,
			fieldLabel: '销售',
			itemId: 'Seller_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.seller.CheckGrid'
		}, {
			fieldLabel: '销售主键ID',
			itemId: 'Seller_Id',
			hidden: true
		});

		//经销商【勾选列表】
		items.push({
			x: 205,
			y: 30,
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
			y: 55,
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

		//开票方类型（全部、个人、经销商、送检单位）
		items.push({
			x: 395,
			y: 30,
			width: 150,
			labelWidth: 70,
			fieldLabel: '开票方类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'BillingUnitType',
			hasStyle: true,
			value: me.defaultBillingUnitTypeValue,
			data: me.BillingUnitTypeList
		});
		//状态（待对账，对账中，对账错误，财务锁定）
		items.push({
			x: 395,
			y: 55,
			width: 150,
			labelWidth: 70,
			fieldLabel: '对帐状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'IsLocked',
			hasStyle: true,
			value: me.defaultIsLockedValue,
			data: me.IsLockedList
		});
		//是否免单（全部、是、否）
		items.push({
			x: 395,
			y: 80,
			labelWidth: 70,
			width: 150,
			fieldLabel: '是否免单',
			xtype: 'uxBoolComboBox',
			itemId: 'IsFree',
			hasAll: true,
			value: null
		});

		//预制条码
		items.push({
			x: 545,
			y: 30,
			fieldLabel: '预制条码',
			itemId: 'SerialNo'
		});
		//上机条码
		items.push({
			x: 545,
			y: 55,
			fieldLabel: '上机条码',
			itemId: 'BarCode'
		});
		//病人姓名
		items.push({
			x: 545,
			y: 80,
			fieldLabel: '病人姓名',
			itemId: 'NRequestForm_CName'
		});

		var timeY = 5;
		//时间条件
		items.push({
			x: 5,
			y: timeY,
			width: 170,
			fieldLabel: '时间类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			hasStyle: true,
			value: me.defaultDateTypeValue,
			data: me.DateTypeList
		}, {
			x: 185,
			y: timeY,
			width: 50,
			itemId: 'radio2',
			boxLabel: '年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		}, {
			x: 235,
			y: timeY,
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		}, {
			x: 330,
			y: timeY,
			width: 95,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio,
			margin: '0 2px 0 10px'
		}, {
			x: 445,
			y: timeY,
			width: 50,
			itemId: 'radio1',
			boxLabel: '日期',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		}, {
			x: 495,
			y: timeY,
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		}, {
			x: 590,
			y: timeY,
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
			x: 700,
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
			x: 770,
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
		}, {
			x: 840,
			y: 5,
			width: 22,
			iconCls: 'button-up',
			margin: '0 0 0 4px',
			xtype: 'button',
			tooltip: '<b>收缩</b>',
			itemId: 'up',
			handler: function() {
				this.hide();
				this.ownerCt.getComponent('down').show();
				me.setHeight(30);
			}
		}, {
			x: 840,
			y: 5,
			width: 22,
			iconCls: 'button-down',
			margin: '0 0 0 4px',
			xtype: 'button',
			hidden: true,
			itemId: 'down',
			tooltip: '<b>展开</b>',
			handler: function() {
				this.hide();
				this.ownerCt.getComponent('up').show();
				me.setHeight(105);
			}
		});

		return items;
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
			'BillingUnit_Name', 'Seller_Name'
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

				Id.setValue(record ? record.get('B' + idName) : '');
				Name.setValue(record ? record.get('B' + name) : '');
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

		var textList = ['SerialNo', 'BarCode', 'NRequestForm_CName'];
		var comboList = ['BillingUnitType', 'IsFree'];//'IsLocked', 
		var checkList = [
			'Laboratory_CName', 'TestItem_CName', 'Dealer_Name',
			'BillingUnit_Name', 'Seller_Name'
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
				params.StartDate = JShell.Date.toString(BeginDate.getValue(), true);
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

				params.StartDate = JShell.Date.getMonthFirstDate(year, month, true);
				params.EndDate = JShell.Date.getMonthLastDate(year, month, true);
			}
		}

		var textList = ['SerialNo', 'BarCode', 'NRequestForm_CName'];
		var comboList = ['BillingUnitType', 'IsLocked', 'IsFree'];
		var checkList = [
			'Laboratory_Id', 'TestItem_Id', 'Dealer_Id',
			'BillingUnit_Id', 'Seller_Id'
		];
		var booleanList = ['IsFree'];
		params = me.getSearchParams(params, textList, "textList");
		params = me.getSearchParams(params, comboList, "comboList");
		params = me.getSearchParams(params, checkList, "checkList");
		params = me.getSearchParams(params, booleanList, "booleanList", me);
		return params;
	}
});