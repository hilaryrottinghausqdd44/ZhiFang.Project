/**
 * 发票基础列表
 * @author Jcall
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '发票基础列表',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 1200,
	height: 800,
	defaultUserType:'',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByExportType?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPInvoice',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByField',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PInvoice_ApplyDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	hasRefresh: true,
	defaultStatusValue: '',
	StatusList: [],
	ExportType: '0',
	/**默认员工赋值*/
	hasDefaultUser: true,
	/**默认员工ID*/
	defaultUserID: null,
	/**默认员工名称*/
	defaultUserName: null,
	/**时间类型列表*/
	DateTypeList: [
		['DataAddTime', '创建时间'],
		['ApplyDate', '申请时间'],
		['ReviewDate', '一审时间'],
		['TwoReviewDate', '二审时间'],
		['InvoiceDate', '开票时间']
	],
	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ApplyManID', '申请人'],
		['ReviewManID', '一审人'],
		['TwoReviewManID', '二审人'],
		['InvoiceManID', '开票人']
	],
	VAT: {
		/**增值税税号*/
		VATNumber: '',
		/**增值税开户行*/
		VATBank: '',
		/**增值税账号*/
		VATAccount: '',
		/**电话*/
		PhoneNum: '',
		/**地址*/
		Address: ''
	},
	Status: {},
	
	/**默认时间类型*/
	defaultDateType: 'DataAddTime',
	PKField: 'PInvoice_Id',
		/**是否是管理员,不是管理员ISADMIN==0  是管理员ISADMIN=1*/
	ISADMIN: 0,
	/**是否使用查询基础服务*/
	IsBasic:false,
	/**付款单位*/
    PClientClassName:'Shell.class.wfm.business.invoice.basic.CheckGrid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initFilterListeners();
		me.paramsFilterListeners();
	},
	paramsFilterListeners: function() {
		var me = this;
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	initComponent: function() {
		var me = this;
		if(me.IsBasic==false){
			me.selectUrl = me.selectUrl + '&ExportType=' + me.ExportType;
		}
		if(me.hasDefaultUser) {
			//默认员工ID
			me.defaultUserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			//默认员工名称
			me.defaultUserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		}
		//创建数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '申请人',
			dataIndex: 'PInvoice_ApplyMan',
			width: 70,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '申请时间',
			dataIndex: 'PInvoice_ApplyDate',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			text: '执行公司',
			dataIndex: 'PInvoice_ComponeName',
			flex: 1,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '付款单位',
			dataIndex: 'PInvoice_PayOrgName',
			flex: 1,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '状态',
			dataIndex: 'PInvoice_StatusName',
			width: 80,
			hidden: true,
			sortable: false,
			menuDisabled: false
		}, {
			text: '创建时间',
			dataIndex: 'PInvoice_DataAddTime',
			width: 130,
			isDate: true,
			hasTime: true,
			hidden: true
		}, {
			text: '主键ID',
			dataIndex: 'PInvoice_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '合同ID',
			dataIndex: 'PInvoice_ContractID',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				iconCls: 'button-interact hand',
				tooltip: '<b>交流</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PInvoice_Id');
					me.showInteractionById(id);
				}
			}]
		}];

		return columns;
	},
	/**根据ID获取信息*/
	getInfoByID: function(value) {
		var me = this;
		for(var i in me.StatusList) {
			var obj = me.StatusList[i];
			if(obj.Id == value) {
				return obj;
			}
		}
		return null;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		if(buttonToolbarItems.length > 0) {
			buttonToolbarItems.unshift('-');
		}
		buttonToolbarItems.unshift('refresh', '-', {
			width: 132,
			labelWidth: 55,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'StatusID',
			fieldLabel: '发票状态',
			value: me.defaultStatusValue
//			data: me.getStatusData()
		}, '-', {
			width: 170,
			labelWidth: 55,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			itemId: 'PClientName',
			fieldLabel: '付款单位',
			className: me.PClientClassName,
			classConfig: {
				title: '付款单位选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'PClientID',
			fieldLabel: '选择付款单位主键ID',
			hidden: true
		}, '-', {
			width: 103,
			labelWidth: 30,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'UserType',
			fieldLabel: '人员',
			data: me.UserTypeList,
			value: me.defaultUserType
		}, {
			width: 70,
			xtype: 'uxCheckTrigger',
			itemId: 'UserName',
			className: 'Shell.class.sysbase.user.CheckApp',
			value: me.defaultUserName
		}, {
			xtype: 'textfield',
			itemId: 'UserID',
			fieldLabel: '申请人主键ID',
			hidden: true,
			value: me.defaultUserID
		}, '-', {
			width: 140,
			labelWidth: 55,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			fieldLabel: '时间范围',
			data: me.DateTypeList,
			value: me.defaultDateType
		}, {
			width: 90,
			labelWidth: 5,
			labelAlign: 'right',
			fieldLabel: '',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				change: function(field, newValue, oldValue) {

				}
			}
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		});

		return buttonToolbarItems;
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
	},

	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**发票查看*/
	openShowForm: function(id, ContractID) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.basic.ShowTabPanel', {
			resizable: true,
			PK: id,
			formtype: 'show',
			hasButtontoolbar: false,
			hasSave: false,
			VAT: me.VAT,
			hasDisSave: false,
			ContractID: ContractID,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**根据发票ID查看任务交流*/
	showInteractionById: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			//resizable: false,
			PK: id //任务ID
		}).show();
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PInvoiceStatus',function(){
			if(!JShell.System.ClassDict.PInvoiceStatus){
    			JShell.Msg.error('未获取到发票状态，请刷新列表');
    			return;
    		}
			 me.load(null, true, autoSelect);
    	});
	}
});