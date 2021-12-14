/**
 * 对账快捷锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.LockAQuick', {
	extend: 'Ext.panel.Panel',

	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '对账快捷锁定',
	bodyPadding: 10,
	autoScroll: true,
	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_QuickReconciliationLocking',
	/**显示成功信息*/
	showSuccessInfo: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**消息框消失时间*/
	hideTimes:3000,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;

		//初始化送检时间
		me.initDate();
		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		
		var date = JShell.Date.getNextDate(new Date(),JShell.PKI.Balance.Dates);
		
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		
		me.BeginDate = JShell.Date.getMonthFirstDate(year,month);
		me.EndDate = JShell.Date.getMonthLastDate(year,month);

		me.YearMonth = year;
		me.MonthMonth = month;
		
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			xtype: 'fieldset',
			items: [{
				xtype: 'label',
				html: '<a>根据选择条件，将符合条件的所有待对账项目进行锁定，并计算合同价格。</a>' +
					'</br><a>可能需要一些时间，请耐心等待。</a>'
			}]
		});

		items.push({
			xtype: 'fieldset',
			itemId: 'lockFilter',
			title: '锁定范围',
			collapsible: true,
			height: 100,
			defaultType: 'textfield',
			defaults: {
				width: 200,
				labelWidth: 60,
				labelAlign: 'right'
			},
			autoScroll: true,
			layout: 'absolute',
			items: me.createFilterItems()
		});

		items.push({
			xtype: 'button',
			text: '锁定',
			tooltip: '<b>对符合条件的项目进行锁定</b>',
			iconCls: 'button-lock',
			handler: function() {
				me.doCheckedLock();
			}
		});
		//		items.push({
		//			xtype:'button',
		//			text:'终止',
		//			tooltip:'<b>终止锁定</b>',
		//			margin:'0 0 0 10',
		//			iconCls:'button-lock-break',
		//			handler:function(){}
		//		});

		return items;
	},
	/**创建锁定范围检索框*/
	createFilterItems: function() {
		var me = this,
			items = [];

		//送检日期
		items.push({
			x: 0,
			y: 0,
			width: 80,
			boxLabel: '送检日期',
			itemId: 'radio1',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		});
		//送检年月
		items.push({
			x: 0,
			y: 30,
			width: 80,
			boxLabel: '送检年月',
			itemId: 'radio2',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		});

		//开始日期
		items.push({
			x: 80,
			y: 0,
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		});
		//截止日期
		items.push({
			x: 175,
			y: 0,
			width: 110,
			labelWidth: 10,
			fieldLabel: '-',
			labelAlign: 'right',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.EndDate,
			disabled: !me.isDateRadio
		});
		//年月选择
		items.push({
			x: 80,
			y: 30,
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		});
		items.push({
			x: 190,
			y: 30,
			width: 95,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio
		});

		//送检单位
		items.push({
			x: 300,
			y: 0,
			fieldLabel: '送检单位',
			itemId: 'Laboratory_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.laboratory.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onLaboratoryAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '送检单位主键ID',
			itemId: 'Laboratory_Id',
			hidden: true
		});
		//经销商
		items.push({
			x: 500,
			y: 0,
			fieldLabel: '经销商',
			itemId: 'Dealer_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.dealer.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onDealerAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '经销商主键ID',
			itemId: 'Dealer_Id',
			hidden: true
		});
		//送检项目
		items.push({
			x: 300,
			y: 30,
			fieldLabel: '送检项目',
			itemId: 'TestItem_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.item.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTestItemAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '检验项目主键ID',
			itemId: 'TestItem_Id',
			hidden: true
		});
		//开票方
		items.push({
			x: 500,
			y: 30,
			fieldLabel: '开票方',
			itemId: 'Seller_BillingUnit_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.billingunit.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onBillingUnitAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '开票方主键ID',
			itemId: 'Seller_BillingUnit_Id',
			hidden: true
		});

		return items;
	},
	/**送检单位选择确认处理*/
	onLaboratoryAccept: function(record) {
		var me = this,
			filter = me.getComponent('lockFilter'),
			Id = filter.getComponent('Laboratory_Id'),
			Name = filter.getComponent('Laboratory_CName');

		Id.setValue(record ? record.get('BLaboratory_Id') : '');
		Name.setValue(record ? record.get('BLaboratory_CName') : '');
	},
	/**经销商选择确认处理*/
	onDealerAccept: function(record) {
		var me = this,
			filter = me.getComponent('lockFilter'),
			Id = filter.getComponent('Dealer_Id'),
			Name = filter.getComponent('Dealer_Name');

		Id.setValue(record ? record.get('BDealer_Id') : '');
		Name.setValue(record ? record.get('BDealer_Name') : '');
	},
	/**送检项目选择确认处理*/
	onTestItemAccept: function(record) {
		var me = this,
			filter = me.getComponent('lockFilter'),
			Id = filter.getComponent('TestItem_Id'),
			Name = filter.getComponent('TestItem_Name');

		Id.setValue(record ? record.get('BTestItem_Id') : '');
		Name.setValue(record ? record.get('BTestItem_CName') : '');
	},
	/**开票方选择确认处理*/
	onBillingUnitAccept: function(record) {
		var me = this,
			filter = me.getComponent('lockFilter'),
			Id = filter.getComponent('Seller_BillingUnit_Id'),
			Name = filter.getComponent('Seller_BillingUnit_Name');

		Id.setValue(record ? record.get('BBillingUnit_Id') : '');
		Name.setValue(record ? record.get('BBillingUnit_Name') : '');
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			filter = me.getComponent('lockFilter'),
			radio1 = filter.getComponent('radio1'),
			radio2 = filter.getComponent('radio2');

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
	},
	/**更改时间方式*/
	changeDateType: function(value) {
		var me = this,
			filter = me.getComponent('lockFilter'),
			BeginDate = filter.getComponent('BeginDate'),
			EndDate = filter.getComponent('EndDate'),
			YearMonth = filter.getComponent('YearMonth'),
			MonthMonth = filter.getComponent('MonthMonth');

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
	/**锁定选中的数据*/
	doCheckedLock: function() {
		var me = this;

		JShell.Msg.confirm("确定要锁定吗", function(but) {
			if (but != "ok") return;

			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.lockUrl;

			url += "?" + me.getParamStr() + "&isLock=true";

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**获取参数串*/
	getParamStr: function() {
		var me = this,
			params = me.getParams();

		//startDate={startDate}&endDate={endDate}&labID={labID}&itemID={itemID}&
		//deptID={deptID}&dealerID={dealerID}&billingUnitID={billingUnitID}&
		//strWhere={strWhere}&isLock={isLock}

		var paramStr = [];

		//做处理
		if (params.startDate) paramStr.push("startDate=" + params.startDate);
		if (params.endDate) paramStr.push("endDate=" + params.endDate);
		if (params.labID) paramStr.push("labID=" + params.labID);
		if (params.itemID) paramStr.push("itemID=" + params.itemID);
		if (params.deptID) paramStr.push("deptID=" + params.deptID);
		if (params.dealerID) paramStr.push("dealerID=" + params.dealerID);
		if (params.billingUnitID) paramStr.push("billingUnitID=" + params.billingUnitID);

		return paramStr.join("&");
	},
	/**获取条件参数*/
	getParams: function() {
		var me = this,
			radio1 = me.getFilterComponent('radio1'),
			radio2 = me.getFilterComponent('radio2'),
			BeginDate = me.getFilterComponent('BeginDate'),
			EndDate = me.getFilterComponent('EndDate'),
			YearMonth = me.getFilterComponent('YearMonth'),
			MonthMonth = me.getFilterComponent('MonthMonth'),

			Laboratory_CName = me.getFilterComponent('Laboratory_CName'),
			Dealer_Name = me.getFilterComponent('Dealer_Name'),
			TestItem_Name = me.getFilterComponent('TestItem_Name'),
			Seller_BillingUnit_Name = me.getFilterComponent('Seller_BillingUnit_Name'),

			Laboratory_Id = me.getFilterComponent('Laboratory_Id'),
			Dealer_Id = me.getFilterComponent('Dealer_Id'),
			TestItem_Id = me.getFilterComponent('TestItem_Id'),
			Seller_BillingUnit_Id = me.getFilterComponent('Seller_BillingUnit_Id');

		var params = {};
		if (radio1.checked) {
			params.startDate = JShell.Date.toString(BeginDate.getValue(), true);
			params.endDate = JShell.Date.toString(EndDate.getValue(), true);
		} else {
			var year = YearMonth.getValue();
			var month = MonthMonth.getValue();

			params.startDate = JShell.Date.getMonthFirstDate(year, month, true);
			params.endDate = JShell.Date.getMonthLastDate(year, month, true);
		}

		params.labID = Laboratory_Id.getValue();
		params.itemID = TestItem_Id.getValue();
		params.dealerID = Dealer_Id.getValue();
		params.billingUnitID = Seller_BillingUnit_Id.getValue();

		return params;
	},
	/**获取条件组件*/
	getFilterComponent: function(itemId) {
		var me = this,
			com = null;

		com = me.getComponent('lockFilter').getComponent(itemId);
		if (com) return com;

		return null;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});