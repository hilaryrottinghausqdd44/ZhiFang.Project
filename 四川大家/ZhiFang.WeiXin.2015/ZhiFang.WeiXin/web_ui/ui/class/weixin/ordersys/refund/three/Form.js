/**
 * 退款发放表单
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.three.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 2
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 95,
		width: 160,
		labelAlign: 'right'
	},
	border: false,
	formtype: "add",
	title: '退款申请',
	width: 480,
	height: 245,
	PK: '',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**获取数据服务路径(编辑时不需要更新总阅读数)*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormById?isPlanish=false',
	/**新增服务地址*/
	addUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSUserOrderFormRefundE',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormThreeReview',
	/**显示成功信息*/
	showSuccessInfo: false,

	autoScroll: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createUserName("用户姓名");
		me.createPrice("实际金额");

		me.createRefundType("退款方式");
		//在退款发放调用时,退款方式为银行退款时,银行转账单号需要显示
		me.createBankName("银行种类");
		me.createBankAccount("银行帐号");

		me.createBankTransFormCode("转账单号");

		me.createRefundPrice("退款金额");
		me.createRefundApplyTime("申请时间");

		me.createRefundReason("退费原因");
		me.createMemo("备注说明");
	},
	//用户姓名
	createUserName: function(fieldLabel) {
		var me = this;
		me.UserName = {
			xtype: 'displayfield',
			hidden: true,
			fieldLabel: fieldLabel,
			name: 'UserName',
			itemId: 'UserName'
		};
	},
	//实际金额
	createPrice: function(fieldLabel) {
		var me = this;
		me.Price = {
			xtype: 'displayfield',
			hidden: true,
			fieldLabel: fieldLabel,
			name: 'Price',
			itemId: 'Price'
		};
	},
	//银行种类
	createBankName: function(fieldLabel) {
		var me = this;
		me.BankName = {
			fieldLabel: fieldLabel,
			name: 'BankName',
			itemId: 'BankName',
			allowBlank: false,
			emptyText: "银行种类",
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 380,
				defaultLoad: true,
				defaultWhere: "bdict.BDictType.DictTypeCode='SYS1002'"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('BankName');
					var Id = me.getComponent('BankID');
					CName.setValue(record ? record.get('BDict_CName') : '');
					Id.setValue(record ? record.get('BDict_Id') : '');
					p.close();
				}
			}
		};
	},
	//银行帐号
	createBankAccount: function(fieldLabel) {
		var me = this;
		me.BankAccount = {
			fieldLabel: fieldLabel,
			name: 'BankAccount',
			itemId: 'BankAccount'
		};
	},
	//银行转账单号
	createBankTransFormCode: function(fieldLabel) {
		var me = this;
		me.BankTransFormCode = {
			hidden: me.formtype == "add" ? true : false,
			emptyText: '银行转账单号',
			fieldLabel: fieldLabel,
			name: 'BankTransFormCode',
			itemId: 'BankTransFormCode'
		};
	},
	/**申请时间*/
	createRefundApplyTime: function(fieldLabel) {
		var me = this;
		var serverTime = JcallShell.System.Date.getDate();
		me.RefundApplyTime = {
			fieldLabel: fieldLabel,
			locked: true,
			readOnly: true,
			name: 'RefundApplyTime',
			itemId: 'RefundApplyTime',
			xtype: 'datefield',
			value: serverTime,
			format: 'Y-m-d h:m'
		};
	},
	/**退费原因*/
	createRefundReason: function(fieldLabel) {
		var me = this;
		me.RefundReason = {
			fieldLabel: fieldLabel,
			xtype: 'textarea',
			border: false,
			name: 'RefundReason',
			minHeight: 20,
			height: 50,
			style: {
				marginBottom: '5px'
			}
		};
	},
	createRefundPrice: function(fieldLabel) {
		var me = this;
		me.RefundPrice = {
			xtype: 'numberfield',
			minValue: 0,
			minLengthText: '最小值只能输入0',
			maxValue: 999999999999,
			maxLengthText: '最大值只能输入999999999999',
			allowBlank: false,
			emptyText: "退款金额不能为空",
			allowDecimals: true,
			decimalPrecision: 2,
			itemId: 'RefundPrice',
			fieldLabel: fieldLabel,
			name: 'RefundPrice'
		};
	},
	createRefundType: function(fieldLabel) {
		var me = this;
		var items = [];
		items.push({
			checked: true,
			name: "rgpRefundType",
			boxLabel: '银行转账',
			inputValue: 3
		}, {
			checked: false,
			name: "rgpRefundType",
			boxLabel: '微信退回',
			inputValue: 1
		}, {
			checked: false,
			hidden: true,
			name: "rgpRefundType",
			boxLabel: '企业付款',
			inputValue: 2
		});
		me.RefundType = {
			xtype: 'radiogroup',
			fieldLabel: fieldLabel,
			tooltip: fieldLabel,
			emptyText: fieldLabel,
			itemId: 'RefundType',
			name: 'RefundType',
			allowBlank: false,
			columnWidth: 60,
			columns: 3,
			vertical: false,
			items: items,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue != null) {
						//在退款发放调用时,退款方式为银行退款时,银行转账单号需要显示
						var checkValue = "" + com.getValue().rgpRefundType;
						var BankTransFormCode = me.getComponent('BankTransFormCode');
						var BankName = me.getComponent('BankName');
						var BankAccount = me.getComponent('BankAccount');
						switch(checkValue) {
							case "1": //微信退回
								BankName.allowBlank = true;
								BankName.setVisible(false);
								BankAccount.setVisible(false);
								BankTransFormCode.setVisible(false);
								break;
							default:
								BankName.allowBlank = false;
								BankName.setVisible(true);
								BankAccount.setVisible(true);
								if(BankTransFormCode && me.formtype == "edit")
									BankTransFormCode.setVisible(true);
								break;
						}
					}
				}
			}
		};
	},
	/**备注说明*/
	createMemo: function(fieldLabel) {
		var me = this;
		me.Memo = {
			fieldLabel: fieldLabel,
			xtype: 'textarea',
			locked: true,
			readOnly: true,
			border: false,
			name: 'Memo',
			minHeight: 20,
			height: 60,
			maxLengthText: "备注说明",
			style: {
				marginBottom: '2px'
			}
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		var width = 85;
		var colspanWidth = parseInt(me.width / 2) - 10;
		//实际金额
		me.UserName.colspan = 1;
		me.UserName.width = colspanWidth;
		items.push(me.UserName);
		//实际金额
		me.Price.colspan = 1;
		me.Price.width = colspanWidth;
		items.push(me.Price);

		//退款方式
		me.RefundType.colspan = 2;
		me.RefundType.width = 360;
		items.push(me.RefundType);

		//在退款发放调用时,退款方式为银行退款时,银行转账单号需要显示
		//银行种类
		me.BankName.colspan = 1;
		me.BankName.width = colspanWidth;
		items.push(me.BankName);
		//银行帐号
		me.BankAccount.colspan = 1;
		me.BankAccount.width = colspanWidth;
		items.push(me.BankAccount);

		//银行转账单号
		me.BankTransFormCode.colspan = 2;
		me.BankTransFormCode.width = me.width - 20;
		items.push(me.BankTransFormCode);

		//退费金额
		me.RefundPrice.colspan = 1;
		me.RefundPrice.width = colspanWidth;
		items.push(me.RefundPrice);
		//退费申请时间
		me.RefundApplyTime.colspan = 1;
		me.RefundApplyTime.width = colspanWidth;
		items.push(me.RefundApplyTime);

		me.RefundReason.colspan = 2;
		me.RefundReason.width = me.width - 20;
		items.push(me.RefundReason);

		me.Memo.colspan = 2;
		me.Memo.width = me.width - 20;
		items.push(me.Memo);
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		me.BankNameValue = "";
		if(me.formtype == "edit" && data.Price != null) {
			var money = JcallShell.Number.getMoney(parseFloat(data.Price));
			me.Price.value = money;
		}
		if(me.formtype == "edit" && data.RefundApplyTime != null) {
			data.RefundApplyTime = JShell.Date.getDate(data.RefundApplyTime);
		}

		var reg = new RegExp("<br />", "g");
		if(data.RefundReason != null)
			data.RefundReason = data.RefundReason.replace(reg, "\r\n");
		if(data.Memo != null)
			data.Memo = data.Memo.replace(reg, "\r\n");

		var RefundType = me.getComponent('RefundType');
		if(RefundType && data.RefundType) {
			var tempArr = [];
			tempArr.push(data.RefundType);
			var arrJson = {
				rgpRefundType: [tempArr]
			};
			RefundType.setValue(arrJson);
			if("" + data.RefundType == "3") {
				var BankTransFormCode = me.getComponent('BankTransFormCode');
				BankTransFormCode.setVisible(true);
			}
		}
		if(data.BankID && data.BankID != null && data.BankID != "") {
			var url = JShell.System.Path.getRootUrl("/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictById");
			url = url + "?isPlanish=false&fields=CName,Id&id=" + data.BankID;
			JcallShell.Server.get(url, function(data2) {
				if(data2.success) {
					if(data2.value) {
						me.BankNameValue = data2.value.CName;
					}
				}
			}, false);
		}
		if(me.BankNameValue) data.BankName = me.BankNameValue;
		return data;
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'Id'
		}, {
			fieldLabel: '银行种类ID',
			hidden: true,
			name: 'BankID',
			itemId: 'BankID'
		});

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var serverTime = JcallShell.System.Date.getDate();
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		me.getForm().setValues({
			RefundApplyTime: serverTime
		});
	},
	isEdit: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			RefundPrice: values.RefundPrice
		};

		if(values.Id) entity.Id = values.Id;
		if(values.Price) entity.Price = values.Price;
		if(values.BankAccount) entity.BankAccount = values.BankAccount;
		//退款方式
		var com = me.getComponent('RefundType');
		var checkbox = com.getChecked();
		if(checkbox != null) {
			entity.RefundType = checkbox[0].inputValue;
		}
		var checkValue = com.getValue().rgpRefundType;
		switch(checkValue) {
			case 3:
				//银行转帐
				if(values.BankTransFormCode) {
					entity.BankTransFormCode = values.BankTransFormCode;
				}
				if(values.BankID) {
					entity.BankID = values.BankID;
				}
				if(values.BankAccount) {
					entity.BankAccount = values.BankAccount;
				}
				break;
			default:
				break;
		}
		//退款原因
		if(values.RefundReason) {
			entity.Reason = values.RefundReason.replace(/\\/g, '&#92');
			entity.Reason = entity.Reason.replace(/[\r\n]/g, '<br />');
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
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if(!me.getForm().isValid()) return;
		//退款方式
		var com = me.getComponent('RefundType');
		var checkValue = com.getValue().rgpRefundType;
		switch(checkValue) {
			case 3://银行转帐
				if(!values.BankID) {
					JcallShell.Msg.alert("退款方式为银行转帐时,银行种类不能为空!", null, 1500);
					return;
				}
				break;
			default:
				break;
		}
		me.fireEvent('save', me, me.getAddParams());
	}
});