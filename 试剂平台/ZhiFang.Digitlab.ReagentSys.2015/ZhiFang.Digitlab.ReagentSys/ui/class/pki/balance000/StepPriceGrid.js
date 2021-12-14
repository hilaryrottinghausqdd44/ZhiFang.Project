/**
 * 阶梯价格计算
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.StepPriceGrid', {
	extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title:'阶梯价格计算 ',
    width:800,
    height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/StatService.svc/Stat_UDTO_CalcStepPrice?isPlanish=true',
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	
	/**默认选中送检时间*/
	isDateRadio: false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		//初始化送检时间
		me.initDate();
		
		me.columns = [{
			dataIndex: 'DDealerCalcPrice_DealerName',
			text:'经销商',
			defaultRenderer: true
		},{
			dataIndex: 'DDealerCalcPrice_ItemName',
			text:'项目',
			defaultRenderer: true
		},{
			dataIndex: 'DDealerCalcPrice_ItemCount',
			text:'项目数量',
			defaultRenderer: true
		},{
			dataIndex: 'DDealerCalcPrice_StepPrice',
			text:'阶梯价格',
			defaultRenderer: true
		},{
			dataIndex: 'DDealerCalcPrice_StepPriceCount',
			text:'采用阶梯价格数量',
			width:110,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				iconCls:'button-show hand',
				tooltip:'查看明细',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.showInfo(rec);
				}
			}]
		}];
		//创建挂靠功能栏
		me.dockedItems = me.createFilterToolbar();
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
	/**查询*/
	onFilterSearch: function() {
		var me = this;
		me.onSearch();
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
			name: me.getId() + 'radioG1',
			checked: me.isDateRadio
		}, {
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.BeginDate,
			disabled: !me.isDateRadio
		}, {
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			value: me.EndDate,
			disabled: !me.isDateRadio
		}, {
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

		items.push({
			width: 70,
			itemId: 'radio2',
			boxLabel: '送检年月',
			xtype: 'radio',
			name: me.getId() + 'radioG1',
			checked: !me.isDateRadio
		});
		items.push({
			width: 95,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth,
			disabled: me.isDateRadio
		});
		items.push({
			width: 95,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth,
			disabled: me.isDateRadio,
			margin: '0 2px 0 10px'
		});
		items.push({
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
		});
		items.push({
			fieldLabel: '检验项目主键ID',
			itemId: 'TestItem_Id',
			hidden: true
		});
		
		items.push({
			x: 920,
			y: 10,
			width: 50,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '计算',
			tooltip: '<b>根据条件计算阶梯价格</b>',
			handler: function() {
				me.onFilterSearch();
			}
		});

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
			Name = me.getFilterComponent('Name'),
			IsLocked = me.getFilterComponent('IsLocked'),
			ItemPriceType = me.getFilterComponent('ItemPriceType');


		var params = {};
		if (radio1.checked) {
			if (BeginDate && BeginDate.getValue()) {
				params.startDate = JShell.Date.toString(BeginDate.getValue(), true);
			}
			if (EndDate && EndDate.getValue()) {
				params.endDate = JShell.Date.toString(EndDate.getValue(), true);
			}
		} else {
			if (YearMonth && MonthMonth) {
				var year = YearMonth.getValue();
				var month = MonthMonth.getValue();

				params.startDate = JShell.Date.getMonthFirstDate(year, month, true);
				params.endDate = JShell.Date.getMonthLastDate(year, month, true);
			}
		}

		if (Laboratory_Id) {
			params.labID = Laboratory_Id.getValue();
		}
		if (TestItem_Id) {
			params.itemID = TestItem_Id.getValue();
		}
		if (Dealer_Id) {
			params.dealerID = Dealer_Id.getValue();
		}
		if (BarCode) {
			params.barCode = BarCode.getValue();
		}
		if (Name) {
			params.userName = Name.getValue();
		}
		if (IsLocked) {
			params.isLocked = IsLocked.getValue();
		}
		if (ItemPriceType) {
			params.itemPriceType = ItemPriceType.getValue();
		}

		return params;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		me.doFilterParams();

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&strWhere=' + JShell.String.encode(where);
		}

		//做处理
		if (me.startDate) params.push("&startDate=" + me.startDate);
		if (me.endDate) params.push("&endDate=" + me.endDate);
		if (me.labID) params.push("&labID=" + me.labID);
		if (me.itemID) params.push("&itemID=" + me.itemID);
		if (me.deptID) params.push("&deptID=" + me.deptID);
		if (me.dealerID) params.push("&dealerID=" + me.dealerID);
		if (me.billingUnitID) params.push("&billingUnitID=" + me.billingUnitID);

		url += params.join("");

		return url;
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
		if (params.barCode) {
			where.push("nrequestitem.BarCode='" + params.barCode + "'");
		}
		if (params.userName) {
			where.push("nrequestitem.NRequestForm.CName='" + params.userName + "'");
		}
		
		if(params.isLocked){
			switch (params.isLocked) {
				case me.IsLockedList[1][1]:
					where.push("(nrequestitem.IsLocked=0 or nrequestitem.IsLocked=1)");
					break;
				case me.IsLockedList[2][1]:
					where.push("(nrequestitem.IsLocked=0)");
					break;
				case me.IsLockedList[3][1]:
					where.push("(nrequestitem.IsLocked=1)");
					break;
				case me.IsLockedList[4][1]:
					where.push("(nrequestitem.IsLocked=3)");
					break;
				default:
					break;
			}
		}

		if (params.itemPriceType) {
			var arr = me.ItemPriceTypeList;
			for (var i = 1; i < arr.length; i++) {
				if (params.itemPriceType == arr[i][1]) {
					where.push("(nrequestitem.ItemPriceType='" + me.ItemPriceTypeList[i][0] + "')");
					break;
				}
			}
		}

		me.internalWhere = where.join(" and ");
	},
	/**查看明细*/
	showInfo:function(record){
		var me = this;
		
		JShell.Win.open('Shell.class.pki.balance.StepPriceInfoGrid', {
			resizable: false,
			/**开始日期*/
			startDate: me.startDate,
			/**结束日期*/
			endDate: me.endDate,
			/**送检项目ID*/
			itemID: me.itemID,
			/**经销商ID*/
			dealerID: me.dealerID,
			/**阶梯价*/
			stepPrice: record.get('DDealerCalcPrice_StepPrice'),
		}).show();
	}
});