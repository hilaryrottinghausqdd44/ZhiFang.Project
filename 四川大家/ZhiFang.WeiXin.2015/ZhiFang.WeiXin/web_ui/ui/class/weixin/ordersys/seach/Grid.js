/**
 * 用户订单列表
 * @author liangyl
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.seach.Grid', {
	extend: 'Shell.ux.grid.Panel',

	title: '用户订单列表',
	width: 800,
	height: 500,
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderFormByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserOrderForm',
	/**获取医院数据服务路径*/
	hospitalUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
	
	/**
	 * 订单解除锁定服务地址
	 * @author Jcall
	 * @version 2018-03-23
	 */
	UnLockOSUserOrderFormByIdUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UnLockOSUserOrderFormById',
	
	/**默认加载*/
	defaultLoad: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	defaultOrderBy: [{
		property: 'OSUserOrderForm_DataAddTime',
		direction: 'DESC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**时间类型列表*/
	DateTypeList: [
		['DataAddTime', '创建时间'],
		['PayTime', '缴费时间'],
		['CancelApplyTime', '取消申请时间'],
		['CancelFinishedTime', '取消完成时间'],
		['ConsumerStartTime', '消费开始时间'],
		['ConsumerFinishedTime', '消费完成时间'],
		['CancelApplyTime', '退费申请时间'],
		['RefundOneReviewStartTime', '退款处理开始时间'],
		['RefundOneReviewFinishTime', '退款处理完成时间'],
		['RefundTwoReviewStartTime', '退款审批开始时间'],
		['RefundTwoReviewFinishTime', '退款审批完成时间'],
		['RefundThreeReviewStartTime', '退款发放开始时间'],
		['RefundThreeReviewFinishTime', '退款发放完成时间']
	],

	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['RefundOneReviewManID', '退款处理人'],
		['RefundTwoReviewManID', '退款审批人'],
		['RefundThreeReviewManID', '退款发放人']
	],
	/**默认时间类型*/
	defaultDateType: 'DataAddTime',
	/**默认员工类型*/
	defaultUserType: '',
	/**默认状态*/
	defaultStatusValue: '',
	/**后台排序*/
	remoteSort: true,
	/**医院*/
	HospitalEnum: {},
	/**本地数据存储*/
	LocalStorage: {
		set: function(name, value) {
			localStorage.setItem(name, value);
		},
		get: function(name) {
			return localStorage.getItem(name);
		},
		remove: function(name) {
			localStorage.removeItem(name);
		}
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getHospitalInfo();
		me.doButtonsToolbarListeners();
		me.doButtonsToolbarListeners2();
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.getHospitalInfo();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
		me.searchInfo = {
			width: 163,
			emptyText: '订单编号/消费单号/消费码',
			isLike: true,
			itemId: 'search',
			tooltip: '订单编号/消费单号/消费码',
			fields: ['osuserorderform.UOFCode', 'osuserorderform.UserName', 'osuserorderform.PayCode']
		};
		buttonToolbarItems.unshift('refresh', '-');

		buttonToolbarItems.push({
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: '设置',
			handler: function() {
				me.openColForm();
			}
		}, '-');
		buttonToolbarItems.push({
			width: 125,
			labelWidth: 33,
			labelAlign: 'right',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'StatusID',
			fieldLabel: '状态',
			value: me.defaultStatusValue
		}, {
			fieldLabel: '医生',
			emptyText: '医生名称',
			name: 'DoctorName',
			itemId: 'DoctorName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.weixin.doctor.CheckGrid',
			labelWidth: 33,
			width: 125,
			classConfig: {
				title: '医生选择',
				checkOne: true
			}
		}, {
			xtype: 'textfield',
			itemId: 'DoctorID',
			fieldLabel: '医生主键ID',
			hidden: true
		}, {
			fieldLabel: '用户',
			emptyText: '用户名称',
			name: 'AccountName',
			itemId: 'AccountName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.weixin.ordersys.seach.weixinaccount.CheckGrid',
			labelWidth: 33,
			width: 125,
			classConfig: {
				title: '用户选择',
				checkOne: true
			}
		}, {
			xtype: 'textfield',
			itemId: 'AccountID',
			fieldLabel: '用户主键ID',
			hidden: true
		});

		buttonToolbarItems.push('-', {
			type: 'search',
			info: me.searchInfo
		});

		return buttonToolbarItems;
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
				width: 122,
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
				width: 162,
				labelWidth: 55,
				labelAlign: 'right',
				xtype: 'uxSimpleComboBox',
				itemId: 'DateType',
				fieldLabel: '时间范围',
				data: me.DateTypeList,
				value: me.defaultDateType
			}, {
				width: 91,
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.createDefalutColumns();
		var temcolumns = [];
		if(me.LocalStorage.get('OSUserOrderFormGrid')) {
			temcolumns = Ext.JSON.decode(me.LocalStorage.get('OSUserOrderFormGrid'));
			for (var i in columns) {
				for (var j in temcolumns) {
					if(columns[i].dataIndex==temcolumns[j]){
						columns[i].hidden=false;
						break;
					}
				}
			}
		}
		return columns;
	},
	/**创建数据列*/
	createDefalutColumns: function() {
		var me = this;
		var columns = [{
				text: '订单状态',
				dataIndex: 'OSUserOrderForm_Status',
				width: 70,
				sortable: false,
				hideable: false,
				renderer: function(value, meta) {
					var v = value || '';
					if(v) {
						var info = JShell.System.ClassDict.getClassInfoById('UserOrderFormStatus', v);
						if(info) {
							v = info.Name;
							meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
						}
					}
					return v;
				}
			}, {
				xtype: 'actioncolumn',
				text: '退费',
				align: 'center',
				width: 40,
				style: 'font-weight:bold;color:white;background:orange;',
				sortable: false,
				hideable: false,
				items: [{
					getClass: function(v, meta, record) {
						var Status = record.get('OSUserOrderForm_Status');
						//状态为已缴费状态可以退费,完全使用2018-01-08(longfc添加上对状态为完全使用的支持)					
						if (Status == '2' || Status == '4' || Status == '10') {
							meta.tdAttr = 'data-qtip="<b>退费</b>"';
							return 'button-back hand';
						} else {
							return '';
						}
					},
					handler: function(grid, rowIndex, colIndex) {
						var rec = grid.getStore().getAt(rowIndex);
						var id = rec.get('OSUserOrderForm_Id');
						me.openRefundForm(id);
					}
				}]
			}, {
				text: '订单编号',
				dataIndex: 'OSUserOrderForm_UOFCode',
				width: 100,
				sortable: false,
				hideable: false,
				defaultRenderer: true
			}, {
				text: '订单时间',
				dataIndex: 'OSUserOrderForm_DataAddTime',
				width: 135,
				sortable: false,
				hideable: false,
				isDate: true,
				hasTime: true,
				defaultRenderer: true
			}, {
				xtype: 'actioncolumn',
				text: '解锁',
				align: 'center',
				width: 40,
				style: 'font-weight:bold;color:white;background:orange;',
				sortable: false,
				hideable: false,
				items: [{
					getClass: function(v, meta, record) {
						var Status = record.get('OSUserOrderForm_Status');
						//状态为"使用中"可以解锁 @author Jcall @version 2018-03-23					
						if(Status == '31') {
							meta.tdAttr = 'data-qtip="<b>' + 
								'采血站点：' + record.get('OSUserOrderForm_WeblisSourceOrgName')+ '</br>' +
								'采血人员：' + record.get('OSUserOrderForm_EmpAccount')+ '</br>' +
								'是否需要解除消费锁定？</b>"';
							return 'button-lock-open hand';
						} else {
							return '';
						}
					},
					handler: function(grid, rowIndex, colIndex) {
						var rec = grid.getStore().getAt(rowIndex);
						me.unLockOSUserOrderForm(rec);
					}
				}]
			},
			{
				text: '医院',
				dataIndex: 'OSUserOrderForm_HospitalID',
				width: 140,
				sortable: false,
				hideable: false,
				renderer: function(value, meta) {
					var v = value;
					if(me.HospitalEnum != null) {
						v = me.HospitalEnum[value];
					}
					return v;
				}
			}, {
				text: '医生姓名',
				dataIndex: 'OSUserOrderForm_DoctorName',
				width: 70,
				sortable: false,
				hideable: false,
				defaultRenderer: true
			}, {
			    text: '医生电话',
			    dataIndex: 'OSUserOrderForm_DoctMobileCode',
			    width: 110,
			    sortable: false,
			    hideable: false,
			    defaultRenderer: true
			}, {
				text: '用户姓名',
				dataIndex: 'OSUserOrderForm_UserName',
				width: 70,
				sortable: false,
				hideable: false,
				defaultRenderer: true
			}, {
			    text: '用户电话',
			    dataIndex: 'OSUserOrderForm_UserMobileCode',
			    width: 110,
			    sortable: false,
			    hideable: false,
			    defaultRenderer: true
			}, {
				text: '消费码',
				dataIndex: 'OSUserOrderForm_PayCode',
				width: 80,
				sortable: false,
				hideable: false,
				defaultRenderer: true
			}, {
				text: '实际金额',
				dataIndex: 'OSUserOrderForm_Price',
				width: 80,
				sortable: false,
				hideable: false,
				hideable: false,
				xtype: 'numbercolumn',
				type: 'float',
				renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
					value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
					meta.style = 'font-weight:bold;';
					return value;
				}
			}, {
				xtype: 'actioncolumn',
				text: '操作记录',
				align: 'center',
				width: 60,
				hideable: false,
				sortable: false,
				menuDisabled: true,
				style: 'font-weight:bold;color:white;background:orange;',
				items: [{
					iconCls: 'button-show hand',
					handler: function(grid, rowIndex, colIndex) {
						var rec = grid.getStore().getAt(rowIndex);
						var id = rec.get(me.PKField);
						me.openShowForm(id);
					}
				}]
			},
			 {text: '医嘱单号', dataIndex: 'OSUserOrderForm_DOFID', width: 100,hidden:true, sortable: false },
			 {text: '消费单号', dataIndex: 'OSUserOrderForm_OSUserConsumerFormID',hidden:true,  width: 100,sortable: false},
			 {text: '大家价格', dataIndex: 'OSUserOrderForm_GreatMasterPrice',hidden:true,  width: 75,sortable: false, defaultRenderer: true },
			 {text: '折扣价格', dataIndex: 'OSUserOrderForm_DiscountPrice',hidden:true,  width: 75, sortable: false, defaultRenderer: true},
			{
				text: '主键ID',
				dataIndex: 'OSUserOrderForm_Id',
				isKey: true,
				hidden: true,
				hideable: false
			},
			
			{text: '采血站点', dataIndex: 'OSUserOrderForm_WeblisSourceOrgName', width: 100,sortable: false },
			{text: '采血人员', dataIndex: 'OSUserOrderForm_EmpAccount', width: 80,sortable: false }
		];
		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.WeiXin.Entity', 'UserOrderFormStatus', function() {
			if(!JShell.System.ClassDict.UserOrderFormStatus) {
				JShell.Msg.error('未获取到订单状态，请刷新列表');
				return;
			}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List = JShell.System.ClassDict.UserOrderFormStatus;
			if(StatusID.store.data.items.length == 0) {
				StatusID.loadData(me.getStatusData(List));
				StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			DoctorID = null,
			UserType = null,
			AccountID = null,
			DateType = null,
			BeginDate = null,
			EndDate = null,
			StatusID = null,
			search = null,
			UserID = null,
			params = [];
		//改变默认条件
		me.changeDefaultWhere();
		if(buttonsToolbar) {
			StatusID = buttonsToolbar.getComponent('StatusID').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			DoctorID = buttonsToolbar.getComponent('DoctorID').getValue();
			AccountID = buttonsToolbar.getComponent('AccountID').getValue();
		}
		if(buttonsToolbar2) {
			UserType = buttonsToolbar2.getComponent('UserType').getValue();
			UserID = buttonsToolbar2.getComponent('UserID').getValue();
			DateType = buttonsToolbar2.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
		}
		//医生
		if(DoctorID) {
			params.push("osuserorderform.DoctorAccountID='" + DoctorID + "'");
		}
		//用户
		if(AccountID) {
			params.push("osuserorderform.UserWeiXinUserID='" + AccountID + "'");
		}
		//状态
		if(StatusID) {
			params.push("osuserorderform.Status='" + StatusID + "'");
		}
		//员工
		if(UserType && UserID) {
			params.push("osuserorderform." + UserType + "='" + UserID + "'");
		}
		//时间
		if(DateType) {
			if(BeginDate) {
				params.push("osuserorderform." + DateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push("osuserorderform." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
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
	/**改变默认条件*/
	changeDefaultWhere: function() {
		var me = this;

		//defaultWhere追加上IsUse约束
		if(me.defaultWhere) {
			var index = me.defaultWhere.indexOf('osuserorderform.IsUse=1');
			if(index == -1) {
				me.defaultWhere += ' and osuserorderform.IsUse=1';
			}
		} else {
			me.defaultWhere = 'osuserorderform.IsUse=1';
		}
	},

	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#0A0A0A;text-align:center']);
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
	/**功能按钮栏监听*/
	doButtonsToolbarListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		//医生
		var DoctorName = buttonsToolbar.getComponent('DoctorName'),
			DoctorID = buttonsToolbar.getComponent('DoctorID');
		if(DoctorName) {
			DoctorName.on({
				check: function(p, record) {
					DoctorName.setValue(record ? record.get('BDoctorAccount_Name') : '');
					DoctorID.setValue(record ? record.get('BDoctorAccount_Id') : '');
					me.onGridSearch();
					p.close();
				}
			});
		}

		//用户
		var AccountName = buttonsToolbar.getComponent('AccountName'),
			AccountID = buttonsToolbar.getComponent('AccountID');
		if(AccountName) {
			AccountName.on({
				check: function(p, record) {
					AccountName.setValue(record ? record.get('BWeiXinAccount_UserName') : '');
					AccountID.setValue(record ? record.get('BWeiXinAccount_Id') : '');
					me.onGridSearch();
					p.close();
				}
			});
		}
		var StatusID = buttonsToolbar.getComponent('StatusID');
		//状态
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
				change: function() {
					me.onGridSearch();
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					me.onGridSearch();
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function() {
					me.onGridSearch();
				}
			});
		}
	},
	/**退费页面*/
	openRefundForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.weixin.ordersys.seach.Form', {
			SUB_WIN_NO: '1', //内部窗口编号
			resizable: false,
			formtype: 'add',
			/**是否启用保存按钮*/
			hasSave: true,
			/**是否重置按钮*/
			hasReset: true,
			/**带功能按钮栏*/
			hasButtontoolbar: true,
			PK: id,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**自定义列显示隐藏*/
	openColForm: function() {
		var me = this;
		JShell.Win.open('Shell.class.weixin.ordersys.seach.ColForm', {
			SUB_WIN_NO: '1', //内部窗口编号
			resizable: false,
			//			formtype:'edit',
			/**是否启用保存按钮*/
			hasSave: true,
			/**是否重置按钮*/
			hasReset: true,
			/**带功能按钮栏*/
			hasButtontoolbar: true,
			listeners: {
				save: function(p, values) {
                    var arr=[];                  
					if(values.rb) {
						 arr=arr.concat(values.rb);
						for (var i in me.columns) {
							if(me.columns[i].dataIndex=='OSUserOrderForm_DOFID'){
					    		me.columns[i].hide();
					    	}
					    	if(me.columns[i].dataIndex=='OSUserOrderForm_OSUserConsumerFormID'){
					    		me.columns[i].hide();
					    	}
					    	if(me.columns[i].dataIndex=='OSUserOrderForm_GreatMasterPrice'){
					    		me.columns[i].hide();
					    	}
					    	if(me.columns[i].dataIndex=='OSUserOrderForm_DiscountPrice'){
					    		me.columns[i].hide();
					    	}
							for (var j in arr) {
								if(me.columns[i].dataIndex==arr[j]){
									me.columns[i].show();
									break;
								}
							}
						}
					}
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 500);
					me.onSearch();
					p.close();
				}
			}
		}).show();
	},
	/**查询操作记录*/
	openShowForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.weixin.ordersys.seach.HistoryPanel', {
			SUB_WIN_NO: '101', //内部窗口编号
			//resizable:false,
			title: '操作记录信息',
			PK: id
		}).show();
	},

	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	/**获取医院信息*/
	getHospitalInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.hospitalUrl;
		url += '&fields=BHospital_Name,BHospital_Id';
		me.HospitalEnum = {};
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						me.HospitalEnum[obj.BHospital_Id] = obj.BHospital_Name;
					});
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**
	 * 解除订单锁定状态
	 * @author Jcall
	 * @version 2018-03-23
	 * @param {Object} recode
	 */
	unLockOSUserOrderForm:function(recode){
		var me = this,
			url = JShell.System.Path.ROOT + me.UnLockOSUserOrderFormByIdUrl;
			
		//不是"使用中"状态的单据不让解锁
		if(recode.get("OSUserOrderForm_Status") != "31") return;
			
		var params = {Id:recode.get("OSUserOrderForm_Id")};
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask(); //隐藏遮罩层
			if(data.success){
				JShell.Msg.alert("解锁成功！",null,1000);
				recode.set({OSUserOrderForm_Status:'2'});
				recode.commit();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});