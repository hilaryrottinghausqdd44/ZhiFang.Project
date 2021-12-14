/**
 * 发票查询
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.show.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.BasicGrid',
	title: '发票查询列表',
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
//	ExportType: 5,
	OperMsg: '',
	defaultStatusValue: '',
	/**默认员工类型*/
	defaultUserType: '',
	/**是否是管理员,不是管理员ISADMIN==0  是管理员ISADMIN=1*/
	ISADMIN: 0,
	IsBasic:true,
	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ApplyManID', '申请人'],
		['ReviewManID', '一审人'],
		['TwoReviewManID', '二审人'],
		['InvoiceManID', '开票人']
	],
	 /**付款单位*/
    PClientClassName:'Shell.class.wfm.client.CheckGrid',
	initComponent: function() {
		var me = this;
		if(me.ISADMIN == 0) {
			/**员工类型列表*/
			me.UserTypeList = [
				['', '不过滤'],
				['ReviewManID', '一审人'],
				['TwoReviewManID', '二审人'],
				['InvoiceManID', '开票人']
			];
		}
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get('PInvoice_Id');
				var ContractID = record.get('PInvoice_ContractID');
				var Status= record.get('PInvoice_Status');
				//管理员
				if(me.ISADMIN==1){
					me.openEditForm(id, ContractID,Status);
				}else{
					me.openShowForm(id, ContractID);
				}
				
			}
		});
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		if(buttonToolbarItems.length > 0) {
			buttonToolbarItems.unshift('-');
		}
		//管理员
		if(me.ISADMIN==1){
			me.PClientClassName='Shell.class.wfm.client.CheckGrid';
		}else{
			//普通查询
			me.PClientClassName='Shell.class.wfm.business.invoice.basic.CheckGrid';
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
	/**发票查看*/
	openEditForm: function(id, ContractID,Status) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.show.ShowTabPanel', {
			resizable: true,
			PK: id,
			Status:Status,
			formtype: 'show',
			hasButtontoolbar: false,
			hasSave: false,
			VAT: me.VAT,
			hasDisSave: false,
			ContractID: ContractID,
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});