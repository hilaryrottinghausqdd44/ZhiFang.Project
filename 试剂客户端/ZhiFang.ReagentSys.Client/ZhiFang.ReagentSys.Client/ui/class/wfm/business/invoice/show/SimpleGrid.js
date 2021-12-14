/**
 * 发票查询
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.show.SimpleGrid', {
	extend: 'Shell.class.wfm.business.invoice.basic.Grid',
	title: '发票查询列表',
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceByHQL?isPlanish=true',
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
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,
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
    defaultWhere : 'pinvoice.IsUse=1 and pinvoice.Status!=1',
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
		buttonToolbarItems.unshift('refresh', '-',  {
			width: 170,
			labelWidth: 55,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			itemId: 'DeptName',
			fieldLabel: '部门',
			emptyText  : '部门',
			className: 'Shell.class.wfm.service.accept.CheckGrid',
			classConfig: {
				title: '部门选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'DeptID',
			fieldLabel: '部门主键ID',
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
		/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text: '状态',
			dataIndex: 'PInvoice_Status',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer:function(value,meta){
            	var v = value || '';
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PInvoiceStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
           }
		}, {
			text: '开票金额',
			dataIndex: 'PInvoice_InvoiceAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '要求开票时间',
			dataIndex: 'PInvoice_InvoiceDate',
			width: 130,
			isDate: true,
			hasTime: true
		});
		return columns;
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
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			params = [];
		if(!buttonsToolbar) {
			return;
		}
		var EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			DateType = buttonsToolbar.getComponent('DateType').getValue(),
			UserType = buttonsToolbar.getComponent('UserType').getValue(),
			UserID = buttonsToolbar.getComponent('UserID').getValue(),
			DeptID = buttonsToolbar.getComponent('DeptID').getValue();
		var EndDate2 = JcallShell.Date.toString(EndDate, true),
			BeginDate2 = JcallShell.Date.toString(BeginDate, true);
		//时间
		if(DateType) {
			if(BeginDate2 && EndDate2) {
				params.push("pinvoice." + DateType + " between '" + BeginDate2 + ' 00:00:00 ' + "' and '" + EndDate2 + " 23:59:59'");
			}
		}
		//付款单位
		if(DeptID) {
			params.push("pinvoice.DeptID='" + DeptID + "'");
		}
		//员工
		if(UserType && UserID) {
			params.push("pinvoice." + UserType + "='" + UserID + "'");
		}
			//默认员工ID
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(me.ISADMIN == 0) {
			params.push("pinvoice.ApplyManID=" + userId);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	paramsFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		
        var DeptName = buttonsToolbar.getComponent('DeptName'),
			DeptID = buttonsToolbar.getComponent('DeptID');
		if(DeptName) {
			DeptName.on({
				check: function(p, record) {
					DeptName.setValue(record ? record.get('HRDept_CName') : '');
					DeptID.setValue(record ? record.get('HRDept_Id') : '');
					me.onSearch();
					p.close();
				}
			});
		}
		
		//人员类型+人员
		var UserType = buttonsToolbar.getComponent('UserType'),
			UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserType) {
			UserType.on({
				change: function() {
					if(UserID.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change: function() {
					if(UserType.getValue() && UserID.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		//时间类型+时间

		var EndDate = buttonsToolbar.getComponent('EndDate');
		var BeginDate = buttonsToolbar.getComponent('BeginDate');
		var DateType = buttonsToolbar.getComponent('DateType');
		if(DateType) {
			DateType.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onGridSearch();
					}
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onSearch();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						me.onSearch();
					}
				}
			});
		}
	}
});