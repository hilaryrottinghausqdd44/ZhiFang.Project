/**
 * 服务受理
 * @author Jcall
 * @version 2016-11-03
 */
Ext.define('Shell.class.wfm.service.accept.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '服务受理',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceByHQL?isPlanish=true',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**删除数据服务路径*/
	delUrl: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PCustomerService_ServiceAcceptanceDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用查询框*/
	hasSearch: true,
	defaultStatusValue:'',
	defaultDateType: 'ServiceRegisterDate',
	StatusList: [],
	/**是否是管理员,不是管理员ISADMIN==0  是管理员ISADMIN=1*/
	ISADMIN: 0,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	Status: {},
	defaultApplyDate: null,
	defaultEndDate: null,
	isYdata: false,
		/**序号列宽度*/
	rowNumbererWidth: 35,
	/**默认员工类型*/
	defaultUserType:'',
	/**员工类型列表*/
	UserTypeList:[
		['','不过滤'],['RequestMan','请求人'],['ServiceAcceptanceManID','受理人'],['ServiceRegisterManID','登记人'],
		['ServiceOperationCompleteManID','处理人'],['ServiceReturnManID','回访人']
	],
	/**时间类型列表*/
	DateTypeList: [
		['ServiceRegisterDate', '登记时间'],
		['ServiceAcceptanceDate', '受理时间'],
		['ServiceOperationDate', '处理时间'],
		['ServiceFinishDate', '完成时间'],
		['ServiceReturnDate', '回访时间']
	],
	StatusList: null,
	StatusEnum: null,
	StatusFColorEnum: null,
	StatusBGColorEnum: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    Ext.override(Ext.ToolTip, {
			maxWidth: 380
		});
		me.initFilterListeners();
		me.on({
			save: function() {
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//初始化申请时间
		me.initDate();
		me.regStr = new RegExp('"', "g");
		
		me.searchInfo = {
			width: 140,
			emptyText: '问题名称',
			isLike: true,
			itemId: 'search',
			fields: ['pcustomerservice.ProblemMemo']
		};
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate, true);
		var lastdays = JShell.Date.getNextDate(Sysdate, -7);

		me.defaultApplyDate = lastdays;
		me.defaultEndDate = ApplyDate;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift('refresh', 'add', '-');
		buttonToolbarItems.push({
			width: 130,
			labelWidth: 35,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
//			data:me.getStatusData(),
			itemId: 'StatusID',
			fieldLabel: '状态',
			value:me.defaultStatusValue
		},{
			fieldLabel: '医院',
			emptyText: '医院',
			labelWidth: 35,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid',
			name: 'PCustomerService_ClientName',
			itemId: 'PCustomerService_ClientName',
			width: 160
		}, {
			fieldLabel: '省份',
			emptyText: '省份',
			name: 'PCustomerService_ClientID',
			itemId: 'PCustomerService_ClientID',
			hidden: true,
			xtype: 'textfield'
		}, '-', {
			fieldLabel: '省份',
			emptyText: '省份',
			name: 'PCustomerService_ProvinceName',
			itemId: 'PCustomerService_ProvinceName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.sysbase.country.province.CheckGrid',
			labelWidth: 35,
			width: 160
		}, {
			fieldLabel: '省份',
			emptyText: '省份',
			name: 'PCustomerService_ProvinceID',
			itemId: 'PCustomerService_ProvinceID',
			hidden: true,
			xtype: 'textfield'
		}, '-', {
			type: 'search',
			info: me.searchInfo
		});
		if(me.ISADMIN == 1) {
			buttonToolbarItems.push('-', {
				text: '禁用',
				tooltip: '将选中的记录进行批量禁用',
				iconCls: 'button-lock',
				handler: function() {
					me.onChangeUseField(false);
				}
			}, {
				text: '撤销禁用',
				tooltip: '撤销禁用',
				iconCls: 'button-back',
				handler: function() {
					me.onChangeUseField(true);
				}
			});
		}
		return buttonToolbarItems;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: [{
				fieldLabel: '部门',
				emptyText: '部门',
				name: 'PCustomerService_DeptName',
				itemId: 'PCustomerService_DeptName',
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
				name: 'PCustomerService_DeptID',
				itemId: 'PCustomerService_DeptID',
				hidden: true,
				xtype: 'textfield'
			}, '-',{
				width:110,labelWidth:30,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'UserType',fieldLabel:'人员',
				data:me.UserTypeList,
				value:me.defaultUserType
			},{
				width:60,xtype:'uxCheckTrigger',itemId:'UserName',
				className:'Shell.class.sysbase.user.CheckApp',
				value:me.defaultUserName
			},{
				xtype:'textfield',itemId:'UserID',fieldLabel:'人员主键ID',hidden:true,
				value:me.defaultUserID
			},'-',{
				width: 140,
				labelWidth: 60,
				labelAlign: 'right',
				xtype: 'uxSimpleComboBox',
				itemId: 'DateType',
				fieldLabel: '时间范围',
				data: me.DateTypeList,
				value: me.defaultDateType
			}, {
				text: '今日',
				tooltip: '今日',
				xtype: 'button',
				width: 45,
				name: 'Today',
				itemId: 'Today'
			}, {
				text: '昨日',
				tooltip: '昨日',
				xtype: 'button',
				width: 45,
				name: 'Yesterday',
				itemId: 'Yesterday'
			}, {
				text: '本周',
				tooltip: '本周',
				xtype: 'button',
				width: 45,
				name: 'Thisweek',
				itemId: 'Thisweek'
			}, {
				text: '本月',
				tooltip: '本月',
				xtype: 'button',
				width: 45,
				name: 'Thismonth',
				itemId: 'Thismonth'
			}, {
				text: '上月',
				tooltip: '上月',
				xtype: 'button',
				width: 45,
				name: 'LastMonth',
				itemId: 'LastMonth'
			}, {
				text: '最近7天',
				tooltip: '最近7天',
				xtype: 'button',
				width: 55,
				name: 'Last7days',
				itemId: 'Last7days'
			}, {
				text: '最近30天',
				tooltip: '最近30天',
				xtype: 'button',
				width: 62,
				name: 'Onemonth',
				itemId: 'Onemonth'
			}, {
				width: 90,
				itemId: 'BeginDate',
				xtype: 'datefield',
				format: 'Y-m-d',
				value: me.defaultApplyDate
			}, {
				width: 100,
				labelWidth: 5,
				fieldLabel: '-',
				labelSeparator: '',
				itemId: 'EndDate',
				xtype: 'datefield',
				format: 'Y-m-d',
				value: me.defaultEndDate
			}]
		};
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '受理时间',
			dataIndex: 'PCustomerService_ServiceAcceptanceDate',
			width: 85,
			isDate: true,
			renderer: function(value, meta, record) {
				value=me.changeOrderShowText(value, meta, record,'受理时间');
	            return value;
			}
		}, {
			text: '用户名称',
			dataIndex: 'PCustomerService_ClientName',
			width: 200,
			sortable: false,
			renderer: function(value, meta, record) {
				value=me.changeOrderShowText(value, meta, record,'用户名称');
				return value;
			}
		}, {
			text: '省份',
			dataIndex: 'PCustomerService_ProvinceName',
			width: 55,
			sortable: false,
			renderer: function(value, meta, record) {
				value=me.changeOrderShowText(value, meta, record,'省份');
	            return value;
			}
		}, {
			text: '代理',
			dataIndex: 'PCustomerService_IsProxy',
			width: 40,
			isBool: true,
			type: 'bool'
		}, {
			text: '使用',
			dataIndex: 'PCustomerService_IsUse',
			width: 40,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			text: '状态',
			dataIndex: 'PCustomerService_Status',
			width: 60,
			sortable: false,
			renderer: function(value, meta) {
				var v = value;
				if(me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if(me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if(me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
				var style = '';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
//			renderer: function(value, meta) {
//				var v = value || '';
//				if(v) {
//					var info = JShell.System.ClassDict.getClassInfoById('PCustomerServiceStatus', v);
//					if(info) {
//						v = info.Name;
//						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
//					}
//				}
//				return v;
//			}
		}, {
			xtype: 'actioncolumn',
			text: '编辑',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PCustomerService_Id');
					me.onOpenEditForm(id);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '处理',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PCustomerService_Id');
					me.onOpenExecuteApp(id);
				}
			}]
		}, {
			text: '问题原始描述',
			dataIndex: 'PCustomerService_ProblemMemo',
			width: 320,
			//hidden: true,
			sortable: false,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			text: '受理人',
			dataIndex: 'PCustomerService_ServiceAcceptanceMan',
			width: 55,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '登记人',
			dataIndex: 'PCustomerService_ServiceRegisterMan',
			width: 55,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '请求人',
			dataIndex: 'PCustomerService_RequestMan',
			width: 55,
			sortable: false,
			renderer: function(value, meta, record) {
				value=me.changeOrderShowText(value, meta, record,'请求人');
	            return value;
			}
		}, {
			text: '请求人联系电话',
			dataIndex: 'PCustomerService_RequestManPhone',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record) {
				value=me.changeOrderShowText(value, meta, record,'联系电话');
	            return value;
			}
		}, {
			text: '登记时间',
			dataIndex: 'PCustomerService_ServiceRegisterDate',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			text: '主键ID',
			dataIndex: 'PCustomerService_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '处理说明',
			dataIndex: 'PCustomerService_ServiceOperationCompleteMemo',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**新增*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.service.accept.AddPanel', {
			formtype: 'add',
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**编辑*/
	onOpenEditForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.service.accept.AddPanel', {
			formtype: 'edit',
			PK: id,
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**处理*/
	onOpenExecuteApp: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.service.accept.ExecuteApp', {
			//resizable:false,
			Status: me.Status,
			PK: id
		}).show();
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2'),
			buttonsToolbar1 = me.getComponent('buttonsToolbar');
		//医院
		var ClientName = buttonsToolbar1.getComponent('PCustomerService_ClientName'),
			ClientID = buttonsToolbar1.getComponent('PCustomerService_ClientID');
		if(ClientName) {
			ClientName.on({
				check: function(p, record) {
					ClientName.setValue(record ? record.get('PClient_Name') : '');
					ClientID.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				},
				change: function() {
					me.onGridSearch();
				}
			});
		}
		//区域
		var ProvinceName = buttonsToolbar1.getComponent('PCustomerService_ProvinceName'),
			ProvinceID = buttonsToolbar1.getComponent('PCustomerService_ProvinceID');
		if(ProvinceName) {
			ProvinceName.on({
				check: function(p, record) {
					ProvinceName.setValue(record ? record.get('BProvince_Name') : '');
					ProvinceID.setValue(record ? record.get('BProvince_Id') : '');
					p.close();
				},
				change: function() {
					me.onGridSearch();
				}
			});
		}
		//部门
		var DeptName = buttonsToolbar.getComponent('PCustomerService_DeptName'),
			DeptID = buttonsToolbar.getComponent('PCustomerService_DeptID');
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
		if(UserType){
			UserType.on({change:function(){me.onGridSearch();}});
		}
		if(UserName){
			UserName.on({
				check:function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
		
		//状态
		var StatusID = buttonsToolbar1.getComponent('StatusID');
        if(StatusID){
        	StatusID.on({
				change: function(com, newValue, oldValue, eOpts) {
					me.onGridSearch();
				}
			});
        }
		//今日
		var Today = buttonsToolbar.getComponent('Today'),
			//昨日
			Yesterday = buttonsToolbar.getComponent('Yesterday'),
			//本周
			Thisweek = buttonsToolbar.getComponent('Thisweek'),
			//本月
			Thismonth = buttonsToolbar.getComponent('Thismonth'),
			//上月
			LastMonth = buttonsToolbar.getComponent('LastMonth'),
			//最近7天
			Last7days = buttonsToolbar.getComponent('Last7days'),
			//最近30天
			Onemonth = buttonsToolbar.getComponent('Onemonth');
		//开始时间
		var BeginDate = me.getComponent('BeginDate');
		var EndDate = me.getComponent('EndDate');
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate, true);
		Today.on({
			click: function() {
				me.isYdata = false;
				BeginDate.setValue(ApplyDate);
				EndDate.setValue(ApplyDate);
			}
		});
		Yesterday.on({
			click: function() {
				me.isYdata = true;
				var Yesterdaydate = JShell.Date.getNextDate(Sysdate, -1);
				BeginDate.setValue(Yesterdaydate);
				EndDate.setValue(Yesterdaydate);
			}
		});
		var nowDayOfWeek = Sysdate.getDay(); //今天本周的第几天
		var nowDay = Sysdate.getDate(); //当前日     
		var LastMonthValue = Sysdate.getMonth(); //上月 
		var nowYear = Sysdate.getYear(); //当前年   
		nowYear += (nowYear < 2000) ? 1900 : 0; // 
		Thisweek.on({
			click: function() {
				me.isYdata = false;
				//获得本周的开始日期
				var getWeekStartDate = new Date(nowYear, LastMonthValue, nowDay - nowDayOfWeek);
				var getWeekStartDate = me.formatDate(getWeekStartDate);
				//获得本周的结束日期
				var getWeekEndDate = new Date(nowYear, LastMonthValue, nowDay + (6 - nowDayOfWeek));
				var getWeekEndDate = me.formatDate(getWeekEndDate);
				BeginDate.setValue(getWeekStartDate);
				EndDate.setValue(getWeekEndDate);
			}
		});
		//当月
		Thismonth.on({
			click: function() {
				me.isYdata = false;
				//获得本月的开始日期
				var getMonthStartDate = new Date(nowYear, LastMonthValue, 1);
				var getMonthStartDate = me.formatDate(getMonthStartDate);
				//获得本月的结束日期
				var myDate = JcallShell.Date.toString(Sysdate, true);
				var dayCount = JcallShell.Date.getCountDays(myDate); //该月天数
				var getMonthEndDate = new Date(nowYear, LastMonthValue, dayCount);
				var getMonthEndDate = me.formatDate(getMonthEndDate);
				BeginDate.setValue(getMonthStartDate);
				EndDate.setValue(getMonthEndDate);
			}
		});
		//上月
		LastMonth.on({
			click: function() {
				me.isYdata = false;
				//获得本月的上一个月的日期
				var yearValue = Sysdate.getFullYear();
				var monthValue = Sysdate.getMonth();
				if(monthValue == 0) {
					monthValue = 12;
					yearValue = yearValue - 1;
				}
				if(monthValue < 10) {
					monthValue = "0" + monthValue;
				}
				var startDate = "" + yearValue + "-" + monthValue + "-" + "01";
				var getMonthStartDate = JcallShell.Date.getDate(startDate);
				var getMonthStartDate = me.formatDate(getMonthStartDate);
				//获得本月的结束日期
				var myDate = JcallShell.Date.toString(getMonthStartDate, true);
				var dayCount = JcallShell.Date.getCountDays(myDate); //该月天数

				var getMonthEndDate = JShell.Date.getNextDate(startDate, dayCount - 1);
				var getMonthEndDate = me.formatDate(getMonthEndDate);
				BeginDate.setValue(getMonthStartDate);
				EndDate.setValue(getMonthEndDate);
			}
		});
		Last7days.on({
			click: function() {
				me.isYdata = false;
				var lastdays = JShell.Date.getNextDate(Sysdate, -7);
				BeginDate.setValue(lastdays);
				EndDate.setValue(ApplyDate);
			}
		});
		Onemonth.on({
			click: function() {
				me.isYdata = false;
				var monthdays = JShell.Date.getNextDate(Sysdate, -30);
				BeginDate.setValue(monthdays);
				EndDate.setValue(ApplyDate);
			}
		});

		//客户
		var PClientName = buttonsToolbar.getComponent('PCustomerService_PClientName'),
			PClientID = buttonsToolbar.getComponent('PCustomerService_PClientID');
		if(PClientName) {
			PClientName.on({
				check: function(p, record) {
					PClientName.setValue(record ? record.get('PClient_Name') : '');
					PClientID.setValue(record ? record.get('PClient_Id') : '');
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
	//格式化日期：yyyy-MM-dd     
	formatDate: function formatDate(date) {
		var myyear = date.getFullYear();
		var mymonth = date.getMonth() + 1;
		var myweekday = date.getDate();

		if(mymonth < 10) {
			mymonth = "0" + mymonth;
		}
		if(myweekday < 10) {
			myweekday = "0" + myweekday;
		}
		return(myyear + "-" + mymonth + "-" + myweekday);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar2'),
			buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			DeptID = null,
			ProvinceID = null,
			ClientID = null,
			search = null,
			DateType = null,
			BeginDate = null,
			EndDate = null,
			UserType = null,
			UserID = null,
			Status=null,
			params = [];
		if(buttonsToolbar) {
			DeptID = buttonsToolbar.getComponent('PCustomerService_DeptID').getValue();
			DateType = buttonsToolbar.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar.getComponent('EndDate').getValue();
			UserType = buttonsToolbar.getComponent('UserType').getValue();
			UserID = buttonsToolbar.getComponent('UserID').getValue();
		}
		if(buttonsToolbar1) {
			Status = buttonsToolbar1.getComponent('StatusID').getValue();
			ClientID = buttonsToolbar1.getComponent('PCustomerService_ClientID').getValue();
			ProvinceID = buttonsToolbar1.getComponent('PCustomerService_ProvinceID').getValue();
			search = buttonsToolbar1.getComponent('search').getValue();
		}
		//部门
		if(DeptID) {
			params.push("pcustomerservice.DeptID='" + DeptID + "'");
		}
		//省份
		if(ProvinceID) {
			params.push("pcustomerservice.ProvinceID='" + ProvinceID + "'");
		}
		//客户
		if(ClientID) {
			params.push("pcustomerservice.ClientID='" + ClientID + "'");
		}
		//员工
		if(UserType && UserID){
			//请求人只能通过文字匹配
			if(UserType == 'RequestMan'){
				var UserName = buttonsToolbar.getComponent('UserName').getValue();
				params.push("pcustomerservice." + UserType + "='" + UserName + "'");
			}else{
				params.push("pcustomerservice." + UserType + "='" + UserID + "'");
			}
		}
		//状态
		if(Status) {
			params.push("pcustomerservice.Status='" + Status + "'");
		}
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("pcustomerservice." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + " 00:00:00'");
			}
			if(EndDate) {
				params.push("pcustomerservice." + DateType + "<'" + JShell.Date.toString(EndDate, true) + " 23:59:59'");
			}

		}
		if(me.ISADMIN == 0) {
			params.push('pcustomerservice.IsUse=1');
		}
		if(search) {
			params.push("pcustomerservice.ProblemMemo" + " like '%" + search + "%'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	/**批量修改使用字段值*/
	onChangeUseField: function(IsUse) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var Status = rec.get('PCustomerService_Status');
			me.updateOneByIsUse(i, id, IsUse, Status);
		}
	},
	updateOneByIsUse: function(index, id, IsUse, Status) {
		var me = this;
		var url = "/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceByField";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		//是否使用的类型不同处理
		if(me.IsUseType == 'int') {
			IsUse = IsUse ? "1" : "0";
		}
		var params = {
			entity: {
				Id: id,
				IsUse: IsUse,
				Status: Status
			},
			fields: 'Id,IsUse'
		};

		setTimeout(function() {
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.getStatusList(function(data){
        	if(data.value.list){
	             var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
				if(StatusID.store.data.items.length==0){
				     StatusID.loadData(me.getStatusData(data.value.list));
				     StatusID.setValue(me.defaultStatusValue);
				}
        	}
        });
		me.load(null, true, autoSelect);
	},
	/**获取状态列表*/
	getStatusData:function(Status){
		var me = this,
			data = [];
		me.StatusList = [];
		me.StatusEnum = {};
		me.StatusFColorEnum = {};
		me.StatusBGColorEnum = {};	
		data.push(['','=全部=','font-weight:bold;text-align:center']);//font-weight:bold;
		for(var i in Status){
			var obj = Status[i];

			switch (obj.PDict_Id){
				//未处理
				case '5306931351097424780':
				    BGColor='#bfbfbf';
				    FontColor='#ffffff';
					break;
				//处理中
				case '4913864673941805116':
				    BGColor='#f4c600';
				    FontColor='#ffffff';
					break;
				//已完成
				case '4868388252853122720':
				    BGColor='#7cba59';
				    FontColor='#ffffff';
					break;
				//有遗留
				case '5118676910026877787':
				    BGColor='#FF0000';
				    FontColor='#ffffff';
					break;
			    //不好处理
				case '5476597464546245138':
				    BGColor='#FF0000';
				    FontColor='#ffffff';
					break;
				//可不处理
				case '5313840044079446861':
				    BGColor='#FF0000';
				    FontColor='#ffffff';
					break;
				default:
				   BGColor='';
				   FontColor='#1E1E1E';
					break;
			}			
			if(FontColor){
				me.StatusFColorEnum[obj.PDict_Id] = FontColor;
			}
			var style = ['font-weight:bold;text-align:center'];
			if(BGColor){style.push('color:' + BGColor);
			me.StatusBGColorEnum[obj.PDict_Id] = BGColor;
			}
			me.StatusEnum[obj.PDict_Id] = obj.PDict_CName;
			data.push([obj.PDict_Id,obj.PDict_CName,style.join(';')]);
		}
	
		return data;
	},
	showMemoText:function(value, meta, record){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 30 ? v.substring(0, 30) : v);
		if(value.length>30){
			v= v+"...";
		}
		var problemMemo = "" + record.get("PCustomerService_ProblemMemo");
		problemMemo = problemMemo.replace(me.regStr, "'");
		var completeMemo = "" + record.get("PCustomerService_ServiceOperationCompleteMemo");
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>问题原始描述:</b>" + value + "</p>";
		qtipValue += "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>处理信息:</b>" + completeMemo + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	
	/**显示*/
	changeOrderShowText:function(value, meta, record,Name){
		var me=this;
	    var problemMemo = "" + record.get("PCustomerService_ProblemMemo");
		problemMemo = problemMemo.replace(me.regStr, "'");
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>"+Name+":</b>" + value + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>问题原始描述:</b>" + problemMemo + "</p>";
	    var completeMemo = "" + record.get("PCustomerService_ServiceOperationCompleteMemo");
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>处理信息:</b>" + completeMemo + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return value;
	},

	/**获取字典状态信息*/
	getStatusList:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true';
		url += "&fields=PDict_CName,PDict_Id&where=	((pdict.PDictType.DictTypeCode='ServiceStatus') and pdict.IsUse=1)";
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false, 500, false);
	}
});