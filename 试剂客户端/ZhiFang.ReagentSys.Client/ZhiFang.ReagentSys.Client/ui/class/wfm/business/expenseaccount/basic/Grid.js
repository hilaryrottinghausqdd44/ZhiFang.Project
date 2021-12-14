/**
 * 报销基础列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.expenseaccount.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '报销基础列表',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 1200,
	height: 800,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchpexpenseaccountByHQL?isPlanish=true',

	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_Delpexpenseaccount',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PExpenseAccount_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: false,
	/**默认时间类型*/
	defaultDateType: 'DataAddTime',
	/**默认员工类型*/
	defaultUserType: 'ApplyID',

	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ApplyManID', '申请人'],
		['ReviewManID', '一审人'],
		['TwoReviewManID', '二审人'],
		['ThreeReviewManID', '三审人'],
		['FourReviewManID', '四审人'],
		['PayManID', '打款负责人'],
		['ReceiveManID', '领款人']
	],
	/**时间类型列表*/
	DateTypeList: [
		['DataAddTime', '创建时间'],
		['ApplyDate', '申请时间'],
		['ReviewDate', '一审时间'],
		['TwoReviewDate', '二审时间'],
		['ThreeReviewDate', '三审时间'],
		['CheckerDataTime', '四审时间'],
		['PayDate', '打款时间']
	],
	/**默认员工赋值*/
	hasDefaultUser: true,
	/**默认员工ID*/
	defaultUserID: null,
	/**默认员工名称*/
	defaultUserName: null,
	/**默认状态*/
	defaultStatusValue: '',
	/**是否只返回开启的数据*/
	IsUse: true,
	PKField: 'PExpenseAccount_Id',
	/*项目类别*/
	ProjectTypeName: 'ItemType',
	isShowDel: false,
	hiddenIsUse: true,
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
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
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
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: [{
				width: 180,
				labelWidth: 60,
				labelAlign: 'right',
				xtype: 'uxCheckTrigger',
				itemId: 'PClientName',
				fieldLabel: '费用类型',
				emptyText: '费用类型',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '费用类型选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectTypeName + "'"
				}
			}, {
				xtype: 'textfield',
				itemId: 'PClientID',
				fieldLabel: '客户主键ID',
				hidden: true
			}, '-', {
				width: 120,
				labelWidth: 45,
				labelAlign: 'right',
				xtype: 'uxSimpleComboBox',
				itemId: 'UserType',
				fieldLabel: '人员',
				data: me.UserTypeList,
				value: me.defaultUserType
			}, {
				width: 80,
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
				width: 150,
				labelWidth: 65,
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
			}]
		};
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '项目名称/项目类别/核算单位',
			isLike: true,
			itemId: 'search',
			fields: ['pexpenseaccount.ClientName', 'pexpenseaccount.ProjectTypeName', 'pexpenseaccount.AccountingDeptName']
		};
		buttonToolbarItems.unshift('refresh', '-', {
			boxLabel: '显示禁用',
			itemId: 'checkIsUse',
			checked: me.isShowDel,
			value: me.isShowDel,
			hidden: me.hiddenIsUse,
			inputValue: false,
			xtype: 'checkbox',
			style: {
				marginRight: '4px'
			}
		});
		buttonToolbarItems.push({
			width: 160,
			labelWidth: 60,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'StatusID',
			fieldLabel: '报销状态',
			value:me.defaultStatusValue
		}, '-', {
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '填报时间',
			dataIndex: 'PExpenseAccount_DataAddTime',
			width: 135,
			sortable: true,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '项目类别',
			dataIndex: 'PExpenseAccount_ProjectTypeName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '项目名称',
			dataIndex: 'PExpenseAccount_ClientName',
			width: 180,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '核算单位',
			dataIndex: 'PExpenseAccount_AccountingDeptName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '报销金额',
			dataIndex: 'PExpenseAccount_PExpenseAccounAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '核算年月',
			dataIndex: 'PExpenseAccount_AccountingDate',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '状态',
			dataIndex: 'PExpenseAccount_Status',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer:function(value,meta){
            	var v = value || '';
            	
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PExpenseAccountStatus',v);
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
			text: '所属公司',
			dataIndex: 'PExpenseAccount_ComponeName',
			width: 180,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '所属部门',
			dataIndex: 'PExpenseAccount_DeptName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '主键ID',
			dataIndex: 'PExpenseAccount_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: 'IsSpecially',
			dataIndex: 'PExpenseAccount_IsSpecially',
			hidden: true,
			hideable: false
		}, {
			text: '申请人',
			dataIndex: 'PExpenseAccount_ApplyMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '申请时间',
			dataIndex: 'PExpenseAccount_ApplyDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '一级科目',
			dataIndex: 'PExpenseAccount_OneLevelItemName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '二级科目',
			dataIndex: 'PExpenseAccount_TwoLevelItemName',
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '上级领导',
			dataIndex: 'PExpenseAccount_ReviewMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '审核时间',
			dataIndex: 'PExpenseAccount_ReviewDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '商务助理',
			dataIndex: 'PExpenseAccount_TwoReviewMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '商务核对时间',
			dataIndex: 'PExpenseAccount_TwoReviewDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '总经理',
			dataIndex: 'PExpenseAccount_ThreeReviewMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '特殊审批时间',
			dataIndex: 'PExpenseAccount_ThreeReviewDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '财务人员',
			dataIndex: 'PExpenseAccount_FourReviewMan',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '财务复核时间',
			dataIndex: 'PExpenseAccount_FourReviewDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '出纳打款人',
			dataIndex: 'PExpenseAccount_PayManName',
			width: 80,
			sortable: false,
			menuDisabled: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				var Status=record.get('PExpenseAccount_Status');
				if(value != "") {
					meta.style = me.getMetaStyle(colIndex,Status);
				}
				return value;
			}
		}, {
			text: '出纳打款时间',
			dataIndex: 'PExpenseAccount_PayDate',
			width: 135,
			sortable: false,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '1审核人意见',
			dataIndex: 'PExpenseAccount_ReviewInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '2审核人意见',
			dataIndex: 'PExpenseAccount_TwoReviewInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '3审核人意见',
			dataIndex: 'PExpenseAccount_ThreeReviewInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '4审核人意见',
			dataIndex: 'PExpenseAccount_FourReviewInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '打款负责人意见',
			dataIndex: 'PExpenseAccount_PayDateInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '领款人银行备注说明',
			dataIndex: 'PExpenseAccount_ReceiveBankInfo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			text: '报销单说明',
			dataIndex: 'PExpenseAccount_PExpenseAccounMemo',
			hidden: true,
			width: 100,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}];
		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PExpenseAccountStatus',function(){
			if(!JShell.System.ClassDict.PExpenseAccountStatus){
    			JShell.Msg.error('未获取到报销状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PExpenseAccountStatus;
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
    	});
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
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.basic.ShowTabPanel', {
			formtype: "show",
			PK: id //报销ID
		}).show();
	},

	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		//功能按钮栏1监听
		me.doButtonsToolbarListeners();
		//功能按钮栏2监听
		me.doButtonsToolbarListeners2();
	},
	/**功能按钮栏1监听*/
	doButtonsToolbarListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		var StatusID = buttonsToolbar.getComponent('StatusID');
		//报销状态
		if(StatusID) {
			StatusID.on({
				change: function() {
					me.onGridSearch();
				}
			});
		}
	},
	/**功能按钮栏2监听*/
	doButtonsToolbarListeners2: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar2');
		if(!buttonsToolbar) return;
		//客户
		var PClientName = buttonsToolbar.getComponent('PClientName'),
			PClientID = buttonsToolbar.getComponent('PClientID');
		if(PClientName) {
			PClientName.on({
				check: function(p, record) {
					PClientName.setValue(record ? record.get('BDict_CName') : '');
					PClientID.setValue(record ? record.get('BDict_Id') : '');
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
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			PClientID = null,
			UserType = null,
			UserID = null,
			DateType = null,
			BeginDate = null,
			EndDate = null,
			StatusID = null,
			search = null,
			checkIsUse = false,
			params = [];

		if(buttonsToolbar) {
			StatusID = buttonsToolbar.getComponent('StatusID').getValue();
			checkIsUse = buttonsToolbar.getComponent('checkIsUse').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
		}
		if(buttonsToolbar2) {
			PClientID = buttonsToolbar2.getComponent('PClientID').getValue();
			UserType = buttonsToolbar2.getComponent('UserType').getValue();
			UserID = buttonsToolbar2.getComponent('UserID').getValue();
			DateType = buttonsToolbar2.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
		}
		if(me.hiddenIsUse.toString() == 'false') {
			if(checkIsUse.toString() == 'false') {
				params.push("pexpenseaccount.IsUse=1");
			}
		}
		//项目类别
		if(PClientID) {
			params.push("pexpenseaccount.ProjectTypeID='" + PClientID + "'");
		}
		//状态
		if(StatusID) {
			params.push("pexpenseaccount.Status='" + StatusID + "'");
		}
		//员工
		if(UserType && UserID) {
			params.push("pexpenseaccount." + UserType + "='" + UserID + "'");
		}
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("pexpenseaccount." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("pexpenseaccount." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
			}
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	showQtipValue: function(meta, record) {
		var me = this;
		var qtipMemoValue = record.get("PExpenseAccount_PExpenseAccounMemo");
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>报销说明:</b>" + qtipMemoValue + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>报销审核意见:</b>" + record.get("PExpenseAccount_ReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>报销核对意见:</b>" + record.get("PExpenseAccount_TwoReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>特殊审批意见:</b>" + record.get("PExpenseAccount_ThreeReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>财务复核意见:</b>" + record.get("PExpenseAccount_FourReviewInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>出纳检查并打款意见:</b>" + record.get("PExpenseAccount_PayDateInfo") + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>领款人银行备注说明:</b>" + record.get("PExpenseAccount_ReceiveBankInfo") + "</p>";
		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	
	getMetaStyle: function(index, v) {
		var me = this;
		var style = 'font-weight:bold;';
		var info = JShell.System.ClassDict.getClassInfoById('PExpenseAccountStatus', v);
		if(info) {
			if(info.BGColor) {
				if(info.BGColor) {
					style = style + "background-color:" + info.BGColor + ";";
				}
				if(info.FontColor) {
					style = style + "color:" + info.FontColor + ";";
				}
			}
		}
		return style;
	}
});