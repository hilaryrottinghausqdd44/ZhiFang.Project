/**
 * 财务状态列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance.Grid',{
    extend:'Shell.ux.grid.Panel',
    
    requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title:'财务状态列表 ',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchNRequestItemByHQL?isPlanish=true',
    /**带功能按钮栏*/
	hasButtontoolbar:false,
    /**显示对账状态下拉框*/
	showIsLockedCombobox: true,
	/**显示价格类型下拉框*/
	showItemPriceTypeCombobox: true,
    
    /**对账状态列表*/
	IsLockedList: [
		[0, '全部', 'font-weight:bold;color:black;'],
		[1, '待对账/对账中', 'font-weight:bold;color:green;'],
		[2, '待对账', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E0'] + ';'],
		[3, '对账中', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		[4, '对账错误', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';']
	],
    /**价格类型列表*/
	ItemPriceTypeList:[
		[0, '全部', 'font-weight:bold;color:black;'],
		['1', '合同价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E1'] + ';'],
		['2', '阶梯价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E2'] + ';'],
		['3', '免单价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E3'] + ';'],
		['4', '终端价', 'font-weight:bold;color:' + JcallShell.PKI.Enum.Color['E4'] + ';']
	],
    
    /**默认加载*/
	defaultLoad:true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl:false,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**是否启用序号列*/
	hasRownumberer:true,
	
	/**开始日期*/
	startDate:null,
	/**结束日期*/
	endDate:null,
	/**送检单位ID*/
	labID:null,
	/**送检项目ID*/
	itemID:null,
	/**科室ID*/
	deptID:null,
	/**经销商ID*/
	dealerID:null,
	/**开票方ID*/
	billingUnitID:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		
		//初始化送检时间
		me.initDate();
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
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '开票方信息',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价格',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'NRequestItem_ItemPrice',
			text: '设定价格',
			width: 60,
			type: 'float'
		}, {
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
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
			width: 150,
			labelWidth: 40,
			fieldLabel: '状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'IsLocked',
			hasStyle: true,
			value: 0,
			data: me.IsLockedList
		},{
			width: 110,
			labelWidth: 40,
			fieldLabel: '价格',
			xtype: 'uxSimpleComboBox',
			itemId: 'ItemPriceType',
			hasStyle: true,
			value: 0,
			data: me.ItemPriceTypeList
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
			width: 180,
			labelWidth: 50,
			itemId: 'Name',
			fieldLabel: '姓名'
		});
		items.push({
			width: 160,
			labelWidth: 50,
			itemId: 'BarCode',
			fieldLabel: '条码号'
		});
		
		items.push({
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
			if(BeginDate && BeginDate.getValue()){
				params.startDate = JShell.Date.toString(BeginDate.getValue(), true);
			}
			if(EndDate && EndDate.getValue()){
				params.endDate = JShell.Date.toString(EndDate.getValue(), true);
			}
		} else {
			if(YearMonth && MonthMonth){
				var year = YearMonth.getValue();
				var month = MonthMonth.getValue();
	
				params.startDate = JShell.Date.getMonthFirstDate(year, month, true);
				params.endDate = JShell.Date.getMonthLastDate(year, month, true);
			}
		}
		
		if(Laboratory_Id){
			params.labID = Laboratory_Id.getValue();
		}
		if(TestItem_Id){
			params.itemID = TestItem_Id.getValue();
		}
		if(Dealer_Id){
			params.dealerID = Dealer_Id.getValue();
		}
		if(BarCode){
			params.barCode = BarCode.getValue();
		}
		if(Name){
			params.userName = Name.getValue();
		}
		if(IsLocked){
			params.isLocked = IsLocked.getValue();
		}
		if(ItemPriceType){
			params.itemPriceType = ItemPriceType.getValue();
		}

		return params;
	},
	/**@overwrite 条件处理*/
	doFilterParams:function(){
		var me = this,
			params = me.getParams();
		
		//内部数据条件
		var where = [];
		if (params.barCode) {
			where.push("nrequestitem.BarCode='" + params.barCode + "'");
		}
		if (params.userName) {
			where.push("nrequestitem.NRequestForm.CName='" + params.userName + "'");
		}

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
		
		if(params.itemPriceType){
			var arr = me.ItemPriceTypeList;
			for(var i=1;i<arr.length;i++){
				if(params.itemPriceType == arr[i][1]){
					where.push("(nrequestitem.ItemPriceType='" + me.ItemPriceTypeList[i][0] + "')");
					break;
				}
			}
		}
		
		if(params.startDate) where.push("nrequestitem.NRequestForm.OperDate>'" + params.startDate + "'");
		if(params.endDate) where.push("nrequestitem.NRequestForm.OperDate<='" + JShell.Date.toString(JShell.Date.getNextDate(params.endDate),true) + "'");
		if(params.labID) where.push("nrequestitem.NRequestForm.BLaboratory.Id=" + params.labID);
		if(params.itemID) where.push("nrequestitem.BTestItem.Id=" + params.itemID);
		if(params.deptID) where.push("nrequestitem.NRequestForm.DeptID=" + params.deptID);
		if(params.dealerID) where.push("nrequestitem.BDealer.Id=" + params.dealerID);
		if(params.billingUnitID) where.push("nrequestitem.BBillingUnit.Id=" + params.billingUnitID);

		me.internalWhere = where.join(" and ");
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		
		return me.callParent(arguments);
	}
});