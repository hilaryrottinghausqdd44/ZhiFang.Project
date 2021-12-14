/**
 * 合同列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.wfm.business.invoice.apply.ContractGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '合同列表',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
	/**获取客户数据服务路径*/
	selectPClientUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PContract_SignDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**后台排序*/
	remoteSort: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	PayOrgID: '',
	PKField: 'PContract_Id',
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
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl(); //启用所有的操作功能
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		//		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//		me.defaultWhere += "pcontract.PrincipalID=" + userId;
		//创建数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.addEvents('addclick');
		me.callParent(arguments);
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		//客户
		var PClientName = buttonsToolbar.getComponent('PClientName'),
			PClientID = buttonsToolbar.getComponent('PClientID');
		if(PClientName) {
			PClientName.on({
				check: function(p, record) {
					PClientName.setValue(record ? record.get('PSalesManClientLink_PClientName') : '');
					PClientID.setValue(record ? record.get('PSalesManClientLink_PClientID') : '');
					var PayOrgID = record ? record.get('PSalesManClientLink_PClientID') : '';
					var PayOrgName = record ? record.get('PSalesManClientLink_PClientName') : '';
					if(PayOrgID) {
						me.onSearch();
					} else {
						me.clearData();
					}
					me.getPClientById(PayOrgID);
					me.fireEvent('onAcceptClick', me, PayOrgID, PayOrgName, me.VAT);
					p.close();
				}
			});
		}
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		if(!buttonsToolbar2) return;
		//签署时间
		var EndDate = buttonsToolbar2.getComponent('EndDate');
		var BeginDate = buttonsToolbar2.getComponent('BeginDate');
		if(EndDate) {
			EndDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						if(!PClientID.getValue()){
							JShell.Msg.alert('请选择付款单位!');
				            return;
						}
						me.onSearch();
					}
				}
			});
		}
		if(BeginDate) {
			BeginDate.on({
				change: function() {
					if(EndDate.getValue() && BeginDate.getValue()) {
						if(!PClientID.getValue()){
							JShell.Msg.alert('请选择付款单位!');
				            return;
						}
						me.onSearch();
					}
				}
			});
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',
			dataIndex: 'PContract_Id',
			width: 170,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '签署日期',
			dataIndex: 'PContract_SignDate',
			width: 80,
			sortable: true,
			defaultRenderer: true,
			isDate: true
		}, {
			text: '合同名称',
			dataIndex: 'PContract_Name',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '合同金额',
			dataIndex: 'PContract_Amount',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '收款金额',
			dataIndex: 'PContract_PayedMoney',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '已开票金额',
			dataIndex: 'PContract_InvoiceMoney',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}];
		return columns;
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		var me = this;
		var PClientID = null;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			PClientID = buttonsToolbar.getComponent('PClientID').getValue();
			if(PClientID) {
				me.onSearch();
			} else {
				JShell.Msg.alert('请选择付款单位!');
				return;
			}
		}
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
				width: 150,
				labelWidth: 55,
				labelAlign: 'right',
				fieldLabel: '签署时间',
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
			}]
		};

		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		if(buttonToolbarItems.length > 0) {
			buttonToolbarItems.unshift('-');
		}
		buttonToolbarItems.unshift('refresh', '-', {
			width: 189,
			labelWidth: 55,
			labelAlign: 'right',
			tooltip: '选择付款单位',
			xtype: 'uxCheckTrigger',
			itemId: 'PClientName',
			fieldLabel: '付款单位',
			className: 'Shell.class.wfm.business.invoice.basic.CheckGrid',
			classConfig: {
				title: '付款单位选择'
			}
		}, {
			xtype: 'textfield',
			itemId: 'PClientID',
			fieldLabel: '选择付款单位主键ID',
			hidden: true
		});
		return buttonToolbarItems;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			BeginDate = null,
			PClientID=null,
			EndDate = null,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			params = [];
				//改变默认条件
		me.changeDefaultWhere();
		if(buttonsToolbar) {
			PClientID = buttonsToolbar.getComponent('PClientID').getValue();
		}
		if(buttonsToolbar2) {
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
		}
		//付款单位
		if(PClientID) {
			params.push("pcontract.PayOrgID='" + PClientID + "'");
		}
		//签署时间
		var EndDate2 = JcallShell.Date.toString(EndDate, true),
			BeginDate2 = JcallShell.Date.toString(BeginDate, true);
		if(BeginDate2 && EndDate2) {
			params.push("pcontract.SignDate between '" + BeginDate2 + ' 00:00:00 ' + "' and '" + EndDate2 + " 23:59:59'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**改变默认条件*/
	changeDefaultWhere:function(){
		var me = this;
		
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('pcontract.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and pcontract.IsUse=1 and pcontract.ContractStatus>1';
			}
		}else{
			me.defaultWhere = 'pcontract.IsUse=1 and pcontract.ContractStatus>1';
		}
	},
	/**根据付款单位查询增值税税号，增值税开户行、增值税账号*/
	getPClientById: function(Id) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectPClientUrl);

		var fields = [
			'PClient_Id', 'PClient_VATNumber', 'PClient_VATBank', 'PClient_VATAccount', 'PClient_PhoneNum', 'PClient_Address'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=pclient.Id=' + Id;

		JShell.Server.get(url, function(data) {
			if(data.success) {
				var value = data.value.list;
				me.VAT.VATNumber = value[0].PClient_VATNumber;
				me.VAT.VATBank = value[0].PClient_VATBank;
				me.VAT.VATAccount = value[0].PClient_VATAccount;
				me.VAT.PhoneNum = value[0].PClient_PhoneNum;
				me.VAT.Address = value[0].PClient_Address;
			}
		});
	}
});