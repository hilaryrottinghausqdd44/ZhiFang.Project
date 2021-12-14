/**
 * 经销商销售价格查询条件
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.stepprice.SearchToolbar', {
	//extend: 'Ext.toolbar.Toolbar',
	extend: 'Shell.class.pki.search.SearchParams',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	/**默认选中日期方式*/
	isDateRadio: false,

	/**开票方类型默认值*/
	defaultBillingUnitTypeValue: null,
	/**合作级别默认值*/
	defaultCoopLevelValue: null,
	/**合作级别*/
	CoopLevelList: JShell.PKI.Enum.getList('CoopLevel', true, true),
	/**开票方类型列表*/
	BillingUnitTypeList: JShell.PKI.Enum.getList('UnitType', true, true),

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
	/*价格类型选择默认值*/
	defaultItemPriceTypeValue: 0,
	/**价格类型选择*/
	ItemPriceTypeList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'],
		['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';']
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
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

		//经销商【勾选列表】
		items.push({
			x: 5,
			y: 5,
			width: 185,
			labelWidth: 45,
			fieldLabel: '经销商',
			itemId: 'Dealer_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.dealer.CheckGrid'
		}, {
			fieldLabel: '经销商主键ID',
			itemId: 'Dealer_Id',
			hidden: true
		});
		//销售【勾选列表】
		items.push({
			x: 190,
			y: 5,
			width: 180,
			labelWidth: 40,
			fieldLabel: '销售',
			itemId: 'Seller_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.seller.CheckGrid'
		}, {
			fieldLabel: '销售主键ID',
			itemId: 'Seller_Id',
			hidden: true
		});
		//销售区域（模糊查询）
		items.push({
			x: 370,
			y: 5,
			fieldLabel: '销售区域',
			emptyText: '模糊查询',
			itemId: 'Seller_AreaIn'
		});
		items.push({
			x: 560,
			y: 5,
			width: 150,
			labelWidth: 70,
			fieldLabel: '价格类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'ItemPriceType',
			hasStyle: true,
			hidden:true,
			value: me.defaultItemPriceTypeValue,
			data: me.ItemPriceTypeList
		});
		//时间条件
		items.push({
			x: 5,
			y: 30,
			width: 50,
			itemId: 'radio2',
			boxLabel: '年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		}, {
			x: 55,
			y: 30,
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		}, {
			x: 150,
			y: 30,
			width: 95,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio,
			margin: '0 2px 0 10px'
		}, {
			x: 265,
			y: 30,
			width: 50,
			itemId: 'radio1',
			boxLabel: '日期',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		}, {
			x: 315,
			y: 30,
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		}, {
			x: 410,
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

		//操作
		var buttons = me.createButtons();
		if (buttons) {
			items = items.concat(buttons);
		}

		return items;
	},
	/**创建功能按钮*/
	createButtons: function() {
		var me = this,
			items = [];

		items.push({
			x: 565,
			y: 30,
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
			x: 640,
			y: 30,
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

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			radio1 = me.getComponent('radio1'),
			radio2 = me.getComponent('radio2');

		radio1.on({
			change: function(field, newValue) {
				if (newValue) {
					me.changeDateType(1);
				}
			}
		});
		radio2.on({
			change: function(field, newValue) {
				if (newValue) {
					me.changeDateType(2);
				}
			}
		});

		var checkList = ['Dealer_Name', 'Seller_Name'];

		for (var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			com = me.getComponent(name);

		if (!com) return;

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

		switch (value) {
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
			radio2 = me.getComponent('radio2'),
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate'),
			YearMonth = me.getComponent('YearMonth'),
			MonthMonth = me.getComponent('MonthMonth');

		var textList = ['Seller_AreaIn'];
		var checkList = ['Dealer_Name', 'Seller_Name'];
		var comboList = ['ItemPriceType'];
		for (var i in textList) {
			var text = me.getComponent(textList[i]);
			if (text) text.setValue('');
		}
		for (var i in comboList) {
			var combo = me.getComponent(comboList[i]);
			if (combo) combo.setValue(null);
		}
		for (var i in checkList) {
			var check = me.getComponent(checkList[i]);
			var check_Id = me.getComponent(checkList[i].split('_')[0] + '_Id');
			if (check) check.setValue('');
			if (check_Id) check_Id.setValue('');
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

		if (DateType) {
			params.DateType = DateType.getValue();
		}

		if (radio1.getValue()) {
			var BeginDate = me.getComponent('BeginDate'),
				EndDate = me.getComponent('EndDate');
			if (BeginDate && BeginDate.getValue()) {
				params.StartDate = JShell.Date.toString(BeginDate.getValue(), true);
			}
			if (EndDate && EndDate.getValue()) {
				params.EndDate = JShell.Date.toString(EndDate.getValue(), true);
			}
		} else {
			var YearMonth = me.getComponent('YearMonth'),
				MonthMonth = me.getComponent('MonthMonth');
			if (YearMonth && MonthMonth) {
				var year = YearMonth.getValue();
				var month = MonthMonth.getValue();

				params.StartDate = JShell.Date.getMonthFirstDate(year, month, true);
				params.EndDate = JShell.Date.getMonthLastDate(year, month, true);
			}
		}

		var textList = ['Seller_AreaIn'];
		params=me.getSearchParams(params, textList, "textList");

		var checkList = ['Dealer_Id', 'Seller_Id'];
		params=me.getSearchParams(params, checkList, "checkList");

		var comboList = ['ItemPriceType'];
		params=me.getSearchParams(params, comboList, "comboList");

		return params;
	}
});