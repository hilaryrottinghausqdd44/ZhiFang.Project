/**
 * 报销单检查并打款
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.expenseaccount.pay.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
	],
	title: '报销单检查并打款',
	width: 495,
	height: 300,
	bodyPadding: '20px 20px 10px 20px',
	formtype: "edit",
	autoScroll: false,
	hasButtontoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPExpenseAccount',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePExpenseAccountByField',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: '',
	/**收入分类字典*/
	IncomeTypeName: 'IncomeTypeName',
	Status: '',
	/**报销总金额*/
	PExpenseAccounAmount: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键',
			name: 'PExpenseAccounAmount_Id',
			hidden: true,
			itemId: 'PExpenseAccounAmount_Id'
		}, {
			fieldLabel: '申请人Id',
			name: 'PExpenseAccounAmount_ApplyManID',
			hidden: true,
			itemId: 'PExpenseAccounAmount_ApplyManID'
		}, {
			fieldLabel: '报销单总金额',
//			labelWidth:130,
			name: 'PExpenseAccounAmount_PExpenseAccounAmount',
			itemId: 'PExpenseAccounAmount_PExpenseAccounAmount',
			labelStyle: 'font-weight:bold;color:#0000FF;',
			//			style:'font-weight:bold;color:#0000FF',
			value: 0,
			fieldStyle: 'font-weight:bold;color:#0000FF;background:none;border:0;border-bottom:0px',
			colspan: 1,
			readOnly:true,
			locked:true,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '已分配金额',
//			labelWidth:120,
			readOnly:true,
			locked:true,
			fieldStyle: 'background:none;border:0;border-bottom:0px',
			name: 'PExpenseAccounAmount_Assigned',
			itemId: 'PExpenseAccounAmount_Assigned',
			labelStyle: 'font-weight:bold;color:#008B00;',
			fieldStyle: 'font-weight:bold;color:#008B00;background:none;border:0;border-bottom:0px',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '现金金额',
			name: 'PExpenseAccounAmount_CashAmount',
			itemId: 'PExpenseAccounAmount_CashAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '转账金额',
			name: 'PExpenseAccount_TransferAmount',
			itemId: 'PExpenseAccount_TransferAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '相抵金额',
			name: 'PExpenseAccount_LoadAmount',
			itemId: 'PExpenseAccount_LoadAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			fieldLabel: '领款人',
			itemId: 'PExpenseAccount_ReceiveManName',
			name: 'PExpenseAccount_ReceiveManName',
			colspan: 1,
			allowBlank: false,
			emptyText: '必填项',
			width: me.defaults.width * 1
		}, {
			fieldLabel: '领款人ID',
			name: 'PExpenseAccount_ReceiveManID',
			hidden: true,
			itemId: 'PExpenseAccount_ReceiveManID'
		}, {
			fieldLabel: '银行备注',
			colspan: 2,
			width: me.defaults.width * 2,
			//			height: 80,
			//			xtype: 'textarea',
			name: 'PExpenseAccounAmount_ReceiveBankInfo',
			itemId: 'PExpenseAccounAmount_ReceiveBankInfo'
		}, {
			fieldLabel: '处理意见',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 80,
			xtype: 'textarea',
			name: 'PExpenseAccounAmount_PayDateInfo',
			itemId: 'PExpenseAccounAmount_PayDateInfo'
		});
		return items;
	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Status: me.Status
		};
		//		entity.IsReceiveMoney = values.PExpenseAccounAmount_IsReceiveMoney ? true : false;
		if(values.PExpenseAccounAmount_CashAmount) {
			entity.CashAmount = values.PExpenseAccounAmount_CashAmount;
		}
		if(values.PExpenseAccounAmount_ApplyManID) {

		}
		if(values.PExpenseAccount_TransferAmount) {
			entity.TransferAmount = values.PExpenseAccount_TransferAmount;
		}
		if(values.PExpenseAccount_LoadAmount) {
			entity.LoadAmount = values.PExpenseAccount_LoadAmount;
		}
		if(values.PExpenseAccount_ReceiveManName) {
			entity.ReceiveManID = values.PExpenseAccount_ReceiveManID;
			entity.ReceiveManName = values.PExpenseAccount_ReceiveManName;
		}
		if(values.PExpenseAccounAmount_PayDateInfo) {
			entity.PayDateInfo = values.PExpenseAccounAmount_PayDateInfo.replace(/\\/g, '&#92');
		}
		if(values.PExpenseAccounAmount_ReceiveBankInfo) {
			entity.ReceiveBankInfo = values.PExpenseAccounAmount_ReceiveBankInfo.replace(/\\/g, '&#92');
		}
		if(me.PK) {
			entity.Id = me.PK;
		}
		//登录员工
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//登录员工名称
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.PayManID = userId;
		}
		if(userName) {
			entity.PayManName = userName;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.PayDate = ReviewDateStr;
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id', 'Status', 'CashAmount', 'TransferAmount', 'LoadAmount', 'ReceiveManName', 'ReceiveManID', 'ReceiveBankInfo', 'PayDate'];
		entity.fields = fields.join(',');
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if(!me.getForm().isValid()) return;

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		var PExpenseAccounAmount = me.getComponent('PExpenseAccounAmount_PExpenseAccounAmount').getValue();
		var Assigned = me.getComponent('PExpenseAccounAmount_Assigned').getValue();
		if(PExpenseAccounAmount && Assigned) {
			if(parseInt(PExpenseAccounAmount) != parseInt(Assigned)) {
				JShell.Msg.error('已分配金额不等于报销总金额,不能保存!');
				return;
			}
		}

		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		me.fireEvent('beforesave', me);
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}

				if(me.isReturnData) {
					me.fireEvent('save', me, me.returnData(id));
				} else {
					me.fireEvent('save', me, id);
				}

				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//领款人
		var ReceiveManName = me.getComponent('PExpenseAccount_ReceiveManName'),
			ReceiveManID = me.getComponent('PExpenseAccount_ReceiveManID');
		if(ReceiveManName) {
			ReceiveManName.on({
				check: function(p, record) {
					ReceiveManName.setValue(record ? record.get('HREmployee_CName') : '');
					ReceiveManID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			});
		}
		//已分配金额
		var Assigned = me.getComponent('PExpenseAccounAmount_Assigned');
		var CashAmount = me.getComponent('PExpenseAccounAmount_CashAmount');
		var TransferAmount = me.getComponent('PExpenseAccount_TransferAmount');
		var LoadAmount = me.getComponent('PExpenseAccount_LoadAmount');
		var AssignedCount = CashAmount.getValue() + TransferAmount.getValue() + LoadAmount.getValue();
		CashAmount.on({
			blur: function(com, The, eOpts) {
				var count = me.getAssignedCount(CashAmount, TransferAmount, LoadAmount);
				Assigned.setValue(count);
			}
		});
		TransferAmount.on({
			blur: function(com, The, eOpts) {
				var count = me.getAssignedCount(CashAmount, TransferAmount, LoadAmount);
				Assigned.setValue(count);
			}
		});
		LoadAmount.on({
			blur: function(com, The, eOpts) {
				var count = me.getAssignedCount(CashAmount, TransferAmount, LoadAmount);
				Assigned.setValue(count);
			}
		});
	},
	/**合计已分配金额*/
	getAssignedCount: function(CashAmount, TransferAmount, LoadAmount) {
		var me = this;
		var Count = 0;

		//现金金额
		var CashAmountValue = parseInt(CashAmount.getValue());
		//转账金额
		var TransferAmountValue = parseInt(TransferAmount.getValue());
		//相抵金额
		var LoadAmountValue = parseInt(LoadAmount.getValue());

		Count = 0;
		if(CashAmountValue) {
			Count = Count + CashAmountValue;
		}
		if(TransferAmountValue) {
			Count = Count + TransferAmountValue;
		}
		if(LoadAmountValue) {
			Count = Count + LoadAmountValue;
		}

		return Count;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var PExpenseAccounAmount = me.getComponent('PExpenseAccounAmount_PExpenseAccounAmount');
			PExpenseAccounAmount.setValue(me.PExpenseAccounAmount);
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});