/**
 * 选择锁定
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.LockACheck', {
	extend: 'Shell.class.pki.balance.ItemBasicGrid',

	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '选择锁定',

	/**锁定服务*/
	lockUrl: '/StatService.svc/Stat_UDTO_SelectReconciliationLocking',
	/**显示成功信息*/
	showSuccessInfo: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;

		//自定义按钮功能栏
		me.buttonToolbarItems = ['lock', '-', 'lockopen'];
		//创建挂靠功能栏
		me.dockedItems = me.createFilterToolbar();

		me.columns = [{
			dataIndex: 'NRequestItem_NRequestForm_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '姓名',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BarCode',
			text: '条码号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_IsLocked',
			text: '状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style='background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if (record.get('NRequestItem_IsLocked') == '1') {
						meta.tdAttr = 'data-qtip="<b>解除锁定</b>"';
						meta.style = 'background-color:red;';
						return 'button-lock-open hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>对账一次锁定</b>"';
						meta.style = 'background-color:green;';
						return 'button-lock hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isOpen = rec.get('NRequestItem_IsLocked') == '1' ? true : false;
					me.doLock(id, isOpen);
				}
			}]
		}, {
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, ];

		me.callParent(arguments);
	},
	createFilterToolbar: function() {
		var me = this,
			items = [];

		items.push(me.createFilterToolbar1());
		items.push(me.createFilterToolbar2());

		return items;
	},
	createFilterToolbar1: function() {
		var me = this,
			items = [];

		items = [{
			width: 70,
			itemId: 'radio1',
			boxLabel: '送检日期',
			checked: true,
			xtype: 'radio',
			name: me.getId() + 'radioG1'
		}, {
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: '2010-10-01'
		}, {
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: '2010-10-01'
		}, {
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
		}, {
			fieldLabel: '经销商',
			itemId: 'Dealer_Name',
			width: 190,
			labelWidth: 50,
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
		}, {
			width: 180,
			labelWidth: 40,
			itemId: 'Name',
			fieldLabel: '姓名'
		}];

		return {
			xtype: 'toolbar',
			defaultType: 'textfield',
			padding: 5,
			defaults: {
				width: 200,
				labelWidth: 60,
				labelAlign: 'right'
			},
			itemId: 'filterToolbar1',
			items: items,
			isLocked: true
		};
	},
	createFilterToolbar2: function() {
		var me = this,
			items = [];

		items = [{
			width: 70,
			itemId: 'radio2',
			boxLabel: '送检年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1'
		}, {
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			disabled: true
		}, {
			width: 95,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			disabled: true,
			margin: '0 2px 0 10px'
		}, {
			x: 300,
			y: 0,
			fieldLabel: '检验项目',
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
		}, {
			width: 190,
			labelWidth: 50,
			itemId: 'BarCode',
			fieldLabel: '条码号'
		}, {
			x: 920,
			y: 10,
			width: 50,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.clearValues();
			}
		}, {
			x: 920,
			y: 10,
			width: 50,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onFilterSearch();
			}
		}];

		return {
			xtype: 'toolbar',
			defaultType: 'textfield',
			padding: 5,
			defaults: {
				width: 200,
				labelWidth: 60,
				labelAlign: 'right'
			},
			itemId: 'filterToolbar2',
			items: items,
			isLocked: true
		};
	},

	/**获取条件组件*/
	getFilterComponent: function(itemId) {
		var me = this,
			com = null;

		com = me.getComponent('filterToolbar1').getComponent(itemId);
		if (com) return com;

		com = me.getComponent('filterToolbar2').getComponent(itemId);
		if (com) return com;

		return null;
	},
	/**送检单位选择确认处理*/
	onLaboratoryAccept: function(record) {
		var me = this,
			Id = me.getFilterComponent('Laboratory_Id'),
			Name = me.getFilterComponent('Laboratory_CName');

		Id.setValue(record ? record.get('BLaboratory_Id') : '');
		Name.setValue(record ? record.get('BLaboratory_CName') : '');
	},
	/**经销商选择确认处理*/
	onDealerAccept: function(record) {
		var me = this,
			Id = me.getFilterComponent('Dealer_Id'),
			Name = me.getFilterComponent('Dealer_Name');

		Id.setValue(record ? record.get('BDealer_Id') : '');
		Name.setValue(record ? record.get('BDealer_Name') : '');
	},
	/**检验项目选择确认处理*/
	onTestItemAccept: function(record) {
		var me = this,
			Id = me.getFilterComponent('TestItem_Id'),
			Name = me.getFilterComponent('TestItem_Name');

		Id.setValue(record ? record.get('BTestItem_Id') : '');
		Name.setValue(record ? record.get('BTestItem_CName') : '');
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			radio1 = me.getFilterComponent('radio1'),
			radio2 = me.getFilterComponent('radio2');

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
			BeginDate = me.getFilterComponent('BeginDate'),
			EndDate = me.getFilterComponent('EndDate'),
			YearMonth = me.getFilterComponent('YearMonth'),
			MonthMonth = me.getFilterComponent('MonthMonth');

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
	/**清空查询条件*/
	clearValues: function() {
		var me = this,
			BeginDate = me.getFilterComponent('BeginDate'),
			EndDate = me.getFilterComponent('EndDate'),
			YearMonth = me.getFilterComponent('YearMonth'),
			MonthMonth = me.getFilterComponent('MonthMonth'),

			Laboratory_CName = me.getFilterComponent('Laboratory_CName'),
			Dealer_Name = me.getFilterComponent('Dealer_Name'),
			TestItem_Name = me.getFilterComponent('TestItem_Name'),

			Laboratory_Id = me.getFilterComponent('Laboratory_Id'),
			Dealer_Id = me.getFilterComponent('Dealer_Id'),
			TestItem_Id = me.getFilterComponent('TestItem_Id'),

			BarCode = me.getFilterComponent('BarCode'),
			Name = me.getFilterComponent('Name');

		BeginDate.setValue('');
		EndDate.setValue('');
		YearMonth.setValue(new Date().getYear() + 1900);
		MonthMonth.setValue(1);

		Laboratory_CName.setValue('');
		Dealer_Name.setValue('');
		TestItem_Name.setValue('');

		Laboratory_Id.setValue('');
		Dealer_Id.setValue('');
		TestItem_Id.setValue('');

		BarCode.setValue('');
		Name.setValue('');
	},

	/**锁定按钮点击处理*/
	onLockClick: function() {
		this.doCheckedLock(false);
	},
	/**解锁按钮点击处理*/
	onLockOpenClick: function() {
		this.doCheckedLock(true);
	},
	/**锁定选中的数据*/
	doCheckedLock: function(isOpen) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var ids = [];
		for (var i = 0; i < len; i++) {
			ids.push(records[i].get(me.PKField));
		}

		me.doLock(ids.join(","), isOpen);
	},
	/**锁定一条数据*/
	doLock: function(ids, isOpen) {
		var me = this;
		var msg = isOpen ? "确定要解锁吗" : "确定要锁定吗";

		JShell.Msg.confirm(msg, function(but) {
			if (but != "ok") return;

			var url = (me.lockUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.lockUrl;

			url += "?idList=" + ids + "&isLock=" + (isOpen ? false : true);

			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 5000);
					me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**查询*/
	onFilterSearch: function() {
		var me = this;
		me.onSearch();
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.getParams();

		me.startDate = params.startDate;
		me.endDate = params.endDate;
		me.labID = params.labID;
		me.itemID = params.itemID;
		me.deptID = params.deptID;
		me.dealerID = params.dealerID;
		me.billingUnitID = params.billingUnitID;

		//内部数据条件
		var where = [];
		if (params.barCode) where.push("nrequestitem.BarCode='" + params.barCode + "'");
		if (params.userName) where.push("nrequestitem.NRequestForm.CName='" + params.userName + "'");

		me.internalWhere = where.join(" and ");
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

			Laboratory_Id = me.getFilterComponent('Laboratory_Id'),
			Dealer_Id = me.getFilterComponent('Dealer_Id'),
			TestItem_Id = me.getFilterComponent('TestItem_Id'),

			BarCode = me.getFilterComponent('BarCode'),
			Name = me.getFilterComponent('Name');

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

		params.barCode = BarCode.getValue();
		params.userName = Name.getValue();

		return params;
	}
});