/**
 * 医生奖金结算记录列表
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger',
		'Ext.ux.CheckColumn'
	],

	title: '医生奖金结算记录信息',
	width: 1200,
	height: 800,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusByHQL?isPlanish=false',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusByField',
	downLoadExcelUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelOSDoctorBonusDetail',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/ServerWCF/SystemCommonService.svc/GetClassDicList',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'DoctorAccountID',
		direction: 'ASC'
	}],
	/**删除标志字段*/
	DelField: 'delState',
	checkOne: true,
	multiSelect: true,
	defaultWhere: '',
	autoScroll: true,
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**默认加载数据*/
	defaultLoad: false,
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasRefresh: false,
	hasDel: false,
	/**导出excel*/
	hasExportExcel: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	isAllowEditing: true,
	/**后台排序*/
	remoteSort: false,
	BankIDList: [],
	BankIDEnum: [],

	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},

	PaymentMethodList: [],
	PaymentMethodEnum: {},
	PaymentMethodFColorEnum: {},
	PaymentMethodGColorEnum: {},
	/**编辑前的行选中集合*/
	SelectionRecords: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(me.hiddenbuttonsToolbar && buttonsToolbar) {
			buttonsToolbar.setVisible(false);
		}
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 680
		});
	},
	initComponent: function() {
		var me = this;
		me.getBankIDList();
		me.getStatusListData();
		me.getPaymentMethodListData();
		if(!me.checkOne) me.setSelModel();
		if(me.isAllowEditing == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				editing: true,
				clicksToEdit: 1,
				listeners: {
					beforeedit: function(editor, e, eOpts) {
						me.SelectionRecords = me.getSelectionModel().getSelection();
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
						}
					},
					canceledit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					},
					edit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					}
				}
			});
		}
		//创建数据列
		me.columns = me.createGridColumns();
		//初始化默认条件
		me.initDefaultWhere();
		me.callParent(arguments);
	},
	setSelModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//只能点击复选框才能选中
		me.selModel = new Ext.selection.CheckboxModel({
			checkOnly: true
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	createGridColumns: function() {
		var me = this;
		//创建数据列
		var columns = [];
		columns = me.createDefaultColumns();
		return columns;
	},
	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = [];
		//自定义复选框
		columns.splice(0, 0, {
			xtype: 'checkcolumn',
			type: 'boolean',
			text : '&#160;',
			align: 'center',
			hidden:true,
			cls:  Ext.baseCSSPrefix + 'column-header-checkbox ',
			width: 26,
			sortable: false,
            draggable: false,
            resizable: false,
            hideable: false,
            menuDisabled: true,
            dataIndex: 'IsCheck'
		});
		columns.push({
			text: '医生姓名',
			dataIndex: 'DoctorName',
			width: 80,
			sortable: true,
			hideable: false
		}, {
			text: '状态',
			dataIndex: 'Status',
			width: 105,
			align: 'center',
			sortable: true,
			menuDisabled: true,
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
				var style = 'font-weight:bold;';
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
		}, {
			text: '医生奖金结算单ID',
			dataIndex: 'OSDoctorBonusFormID',
			width: 85,
			hidden: true,
			hideable: false
		}, {
			text: '医生账户信息ID',
			dataIndex: 'DoctorAccountID',
			width: 85,
			hidden: true,
			hideable: false
		}, {
			text: '医生微信ID',
			dataIndex: 'WeiXinUserID',
			width: 85,
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '医生奖金结算记录的用户消费单ID字符串',
			dataIndex: 'OSUConsumerFormIDStr',
			hidden: true,
			width: 85,
			hideable: true
		}, {
			text: '开单数量',
			dataIndex: 'OrderFormCount',
			width: 65,
			sortable: true,
			hideable: false
		}, {
			text: '开单金额(¥)',
			dataIndex: 'OrderFormAmount',
			width: 85,
			hideable: false
		}, {
			text: '奖金金额(¥)',
			text: '<b style="color:blue;">奖金金额(¥)</b>',
			dataIndex: 'Amount',
			width: 85,
			hideable: false,
			sortable: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				minValue: 0,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						if(newValue < 0) newValue = 0;
						var record = com.ownerCt.editingPlugin.context.record;
						var percent = 0;
						if(newValue) {
							var orderFormAmount = record.get("OrderFormAmount");
							if(orderFormAmount && parseFloat(orderFormAmount) > 0) {
								//结算比率值等于医生的奖金金额除以医生的开单金额
								percent = parseFloat(parseFloat(newValue) / parseFloat(orderFormAmount)) * 100;
								if(percent > 0) percent = percent.toFixed(2);
							}
						}
						JShell.Action.delay(function() {
							record.set('Percent', percent);
							record.set('Amount', newValue);
							me.getView().refresh();
						}, null, 500);
					}
				}
			}
		}, {
			text: '<b style="color:blue;">结算比率(%)</b>',
			dataIndex: 'Percent',
			width: 85,
			hideable: false,
			sortable: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				minValue: 0,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						if(newValue < 0) newValue = 0;
						var record = com.ownerCt.editingPlugin.context.record;
						var amount = 0;
						if(newValue) {
							var orderFormAmount = record.get("OrderFormAmount");
							if(orderFormAmount) {
								//医生的奖金金额等于医生的开单金额*计算比率值
								amount = parseFloat(parseFloat(orderFormAmount) * parseFloat(newValue) * 0.01);
								if(amount > 0) amount = amount.toFixed(2);
							}
						}
						JShell.Action.delay(function() {
							record.set('Amount', amount);
							record.set('Percent', newValue);
							me.getView().refresh();
						}, null, 500);
					}
				}
			}
		});
		columns.push({
			text: '说明',
			dataIndex: 'Memo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '<b style="color:blue;">发放方式</b>',
			dataIndex: 'PaymentMethod',
			sortable: true,
			width: 75,
			hideable: false,
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'text',
				valueField: 'value',
				value: '1',
				listClass: 'x-combo-list-small',
				store: new Ext.data.SimpleStore({
					fields: ['value', 'text'],
					data: (me.PaymentMethodList != null ? me.PaymentMethodList : [
						["", "请选择"],
						["1", "微信支付"],
						["2", "银行转账"]
					])
				}),
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('PaymentMethod', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}),
			renderer: function(value, meta) {
				var v = value;
				if(me.PaymentMethodEnum != null) {
					v = me.PaymentMethodEnum[value];
				}
				return v;
			}
		}, {
			text: '<b style="color:blue;">银行种类</b>',
			dataIndex: 'BankID',
			sortable: true,
			width: 90,
			hideable: false,
			renderer: function(value, meta) {
				var v = value;
				if(me.BankIDEnum != null) {
					v = me.BankIDEnum[value];
				}
				return v;
			},
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'text',
				valueField: 'value',
				listClass: 'x-combo-list-small',
				store: new Ext.data.SimpleStore({
					fields: ['value', 'text'],
					data: me.BankIDList
				}),
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('BankID', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			})
		}, {
			text: '银行帐号',
			dataIndex: 'BankAccount',
			hidden: false,
			width: 95,
			hideable: false
		}, {
			text: '<b style="color:blue;">手机号</b>',
			dataIndex: 'MobileCode',
			sortable: true,
			width: 90,
			hideable: false,
			editor: {
				xtype: 'textfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('MobileCode', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '<b style="color:blue;">身份证号</b>',
			dataIndex: 'IDNumber',
			width: 120,
			hideable: false,
			editor: {
				xtype: 'textfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('IDNumber', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '<b style="color:blue;">银行开户行地址</b>',
			dataIndex: 'BankAddress',
			hidden: false,
			width: 95,
			hideable: false,
			editor: {
				xtype: 'textfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('BankAddress', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '<b style="color:blue;">银行转账单号</b>',
			dataIndex: 'BankTransFormCode',
			minWidth: 90,
			width: 90,
			//flex: 1,
			hideable: false,
			editor: {
				xtype: 'textfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('BankTransFormCode', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '结算记录单号',
			dataIndex: 'BonusCode',
			hidden: true,
			width: 85,
			hideable: false
		}, {
			text: '结算单号',
			dataIndex: 'BonusFormCode',
			hidden: true,
			width: 80,
			hideable: false
		});
		return columns;
	},
	/**奖金发放方式*/
	getPaymentMethodParams: function() {
		var me = this;
		var params = {
			"jsonpara": [{
				"classname": "OSDoctorBonusPaymentMethod",
				"classnamespace": "ZhiFang.WeiXin.Entity"
			}]
		};
		return params;
	},
	/**奖金发放方式*/
	getPaymentMethodListData: function(callback) {
		var me = this,
			params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);
		params = Ext.encode(me.getPaymentMethodParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].OSDoctorBonusPaymentMethod.length > 0) {
						me.PaymentMethodList = [];
						me.PaymentMethodEnum = {};
						me.PaymentMethodFColorEnum = {};
						me.PaymentMethodBGColorEnum = {};
						var tempArr = [];
						me.PaymentMethodList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].OSDoctorBonusPaymentMethod, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								//style.push('color:' + obj.FontColor);
								me.PaymentMethodFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.PaymentMethodBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.PaymentMethodEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.PaymentMethodList.push(tempArr);

						});
					}
				}
			}
		}, false);
	},
	/**获取申请状态参数*/
	getStatusParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "OSDoctorBonusFormStatus",
				"classnamespace": "ZhiFang.WeiXin.Entity"
			}]
		};
		return params;
	},
	/**获取申请状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList && me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(me.classdicSelectUrl);
		params = Ext.encode(me.getStatusParams());
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].OSDoctorBonusFormStatus.length > 0) {
						me.StatusList = [];
						me.StatusEnum = {};
						me.StatusFColorEnum = {};
						me.StatusBGColorEnum = {};
						var tempArr = [];
						me.StatusList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].OSDoctorBonusFormStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								//style.push('color:' + obj.FontColor);
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.StatusBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.StatusEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);

						});
					}
				}
			}
		}, false);
	},
	/**获取银行种类信息*/
	getBankIDList: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true";
		url += "&fields=BDict_Id,BDict_CName&where=bdict.BDictType.DictTypeCode='SYS1002'";
		me.BankIDList = [];

		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						var tempArr = [obj.BDict_Id, obj.BDict_CName];
						me.BankIDEnum[obj.BDict_Id] = obj.BDict_CName;
						me.BankIDList.push(tempArr);
					});
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = [];
		params = me.getSearchWhereHQL();
		return me.callParent(arguments);
	},
	getSearchWhereHQL: function() {
		var me = this;
		buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = null;
		var params = [];

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			params.push(me.defaultWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			params.push(me.externalWhere);
		}
		return params;
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onDelClick: function() {
		var me = this;
		me.fireEvent('onDelClick', me);
	}
});