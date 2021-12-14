 /**
 * 还款列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.prepayment.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '还款列表',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPSalesManClientLink',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PRepayment_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: false,
	/**默认时间类型*/
	defaultDateType: 'DataAddTime',
	/**默认员工类型*/
	defaultUserType: '',

	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ApplyManID', '申请人'],
		['ReviewManID', '审核人']
	],
	/**时间类型列表*/
	DateTypeList: [
		['DataAddTime', '创建时间'],
		['ApplyDate', '申请时间'],
		['ReviewDate', '一审时间']
	],
	/**默认员工赋值*/
	hasDefaultUser: true,
	/**默认员工ID*/
	defaultUserID: null,
	/**默认员工名称*/
	defaultUserName: null,
	/**默认状态*/
	defaultStatusValue: '',
	hasStatus: true,
	/**是否只返回开启的数据*/
	IsUse: true,
	PKField: 'PRepayment_Id',
	Status: {},
//	StatusBgcolor: {},
//	StatusFontcolor: {},
//	StatusList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.hasDefaultUser) {
			//默认员工ID
			me.defaultUserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			//默认员工名称
			me.defaultUserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		}
		//使用中的数据才显示
		if(me.IsUse) {
			if(me.defaultWhere) {
				me.defaultWhere += ' and ';
			}
			me.defaultWhere += 'prepayment.IsUse=1';
		}
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift('refresh', '-');

		//		if(me.hasStatus){
		buttonToolbarItems.push({
			width: 142,
			labelWidth: 65,
			labelAlign: 'right',
			hasStyle: true,
			hidden: !me.hasStatus,
			xtype: 'uxSimpleComboBox',
			itemId: 'StatusID',
			fieldLabel: '还款状态',
			value:me.defaultStatusValue
		});
		//		}
		buttonToolbarItems.push({
			fieldLabel: '部门',
			emptyText: '部门',
			name: 'DeptName',
			itemId: 'DeptName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.wfm.service.accept.CheckGrid',
			labelWidth: 35,
			width: 160,
			classConfig: {
				height: 300,
				title: '部门选择'
			}
		}, {
			fieldLabel: '部门',
			emptyText: '部门',
			name: 'DeptID',
			itemId: 'DeptID',
			hidden: true,
			xtype: 'textfield'
		}, {
			width: 110,
			labelWidth: 30,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'UserType',
			fieldLabel: '人员',
			data: me.UserTypeList,
			value: me.defaultUserType
		}, {
			width: 60,
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
			labelWidth: 60,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			itemId: 'DateType',
			fieldLabel: '时间范围',
			data: me.DateTypeList,
			value: me.defaultDateType
		}, {
			width: 95,
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
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
		var me = this;
		var columns = [{
			text: '填报时间',
			dataIndex: 'PRepayment_DataAddTime',
			width: 135,
			isDate: true,
			hasTime: true,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '部门',
			dataIndex: 'PRepayment_DeptName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '还款内容',
			dataIndex: 'PRepayment_PRepaymentContentTypeName',
			width: 150,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '还款金额',
			dataIndex: 'PRepayment_PRepaymentAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '状态',
			dataIndex: 'PRepayment_Status',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer:function(value,meta){
            	var v = value || '';
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PRepaymentStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
           }
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
					var id = rec.get(me.PKField);
					me.showInteractionById(id);
				}
			}]
		}, {
			text: '申请人',
			dataIndex: 'PRepayment_ApplyMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '申请时间',
			dataIndex: 'PRepayment_ApplyDate',
			width: 130,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '审核人',
			dataIndex: 'PRepayment_ReviewMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '审核时间',
			dataIndex: 'PRepayment_ReviewDate',
			width: 130,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PRepayment_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	/**根据报销ID查看报销交流*/
	showInteractionById: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			//resizable: false,
			PK: id //报销ID
		}).show();
	},
	/**显示报销信息*/
	openShowForm: function(id) {
		JShell.Win.open('Shell.class.wfm.business.prepayment.basic.ShowTabPanel', {
			formtype: "show",
			PK: id //报销ID
		}).show();
	},

	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		//功能按钮栏2监听
		me.doButtonsToolbarListeners();
	},
	/**功能按钮栏2监听*/
	doButtonsToolbarListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		var StatusID = buttonsToolbar.getComponent('StatusID');
		//还款状态
		if(StatusID) {
			StatusID.on({
				change: function(com,  newValue,  oldValue,  eOpts) {
					me.onGridSearch();
				}
			});
		}
		//部门
		var DeptName = buttonsToolbar.getComponent('DeptName'),
			DeptID = buttonsToolbar.getComponent('DeptID');
		if(DeptName) {
			DeptName.on({
				check: function(p, record) {
					DeptName.setValue(record ? record.get('HRDept_CName') : '');
					DeptID.setValue(record ? record.get('HRDept_Id') : '');
					p.close();
				},
				change: function() {
					me.onGridSearch();
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
					me.onGridSearch();
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
					me.onGridSearch();
				}
			});
		}
		//时间类型+时间
		var DateType = buttonsToolbar.getComponent('DateType'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
		if(DateType) {
			DateType.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(BeginDate.getValue() && EndDate.getValue())
						me.onGridSearch();
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && EndDate.getValue())
						me.onGridSearch();
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && BeginDate.getValue())
						me.onGridSearch();
				}
			});
		}
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			UserType = null,
			UserID = null,
			DateType = null,
			BeginDate = null,
			EndDate = null,
			search = null,
			DeptID = null,
			StatusID = null,
			params = [];
		if(buttonsToolbar) {
			StatusID = buttonsToolbar.getComponent('StatusID').getValue();
			UserType = buttonsToolbar.getComponent('UserType').getValue();
			UserID = buttonsToolbar.getComponent('UserID').getValue();
			DateType = buttonsToolbar.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			DeptID = buttonsToolbar.getComponent('DeptID').getValue();
		}
		//状态
		if(StatusID) {
			params.push("prepayment.Status='" + StatusID + "'");
		}
		//部门
		if(DeptID) {
			params.push("prepayment.DeptID=" + DeptID);
		}
		//员工
		if(UserType && UserID) {
			params.push("prepayment." + UserType + "=" + UserID);
		}
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("prepayment." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("prepayment." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
			}
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
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
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PRepaymentStatus',function(){
			if(!JShell.System.ClassDict.PRepaymentStatus){
    			JShell.Msg.error('未获取到还款状态，请刷新列表');
    			return;
    		}
            var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PRepaymentStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
    	});
	}
});