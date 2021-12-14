/**
 * 收款计划列表
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.change.Grid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.Grid',
	title: '收款计划列表',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**合同ID*/
	PContractID: null,
	/**合同名称*/
	PContractName: '',
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
	defaultLoad: false,
	/**合同总金额*/
	Amount: 0,
	/**编辑状态*/
	EditStatus: 3,
	BackEditStatus: 7,
	/**有效计划*/
	IsCheckStatus:true,
	/**付款单位*/
	PayOrgID:null,
	/**付款单位*/
	PayOrg:null,
	/**客户*/
	PClientID:null,
	/**客户*/
	PClientName:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
		/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
		buttonToolbarItems.push('-',{
			boxLabel: '有效计划',
			itemId: 'CheckStatus',
			checked: true,
			value: true,
			inputValue: true,
			xtype: 'checkbox',
			style: {
				marginRight: '8px'
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue == true) {
						me.IsCheckStatus=true;
					} else {
						me.IsCheckStatus=false;
					}
					me.onSearch();
				}
			}
		});
		return buttonToolbarItems;
	},
	/**收款计划变更*/
	openEditForm: function(id, ReceivePlanAmount,UnReceiveAmount,ReceiveAmount,ReceiveGradationID,ReceiveGradationName) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.receive.preceiveplan.change.EditGrid', {
			//resizable:false,
			title: '收款计划变更',
			PK: id,
			SUB_WIN_NO: '1',
			PContractID: me.PContractID,
			PContractName: me.PContractName,
			Amount: ReceivePlanAmount,
			ReceiveAmount:ReceiveAmount,
			UnReceiveAmount:UnReceiveAmount,
//			ReceivePlanAmountS:ReceivePlanAmount,
			PrincipalID: me.PrincipalID,
			Principal: me.Principal,
			GradationID:ReceiveGradationID,
			GradationName:ReceiveGradationName,
			/**付款单位*/
			PayOrgID:me.PayOrgID,
			/**付款单位*/
			PayOrg:me.PayOrg,
			/**客户*/
			PClientID:me.PClientID,
			/**客户*/
			PClientName:me.PClientName,
			listeners: {
				close:function(){
					me.onSearch();
				}
			}
		}).show();
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
		
//			me.defaultWhere = "preceiveplan.Status=3";
			me.load(null, true, autoSelect);
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			params = [],
			search = null;
		me.internalWhere = '';
		if(me.PContractID) {
			params.push("preceiveplan.PContractID=" + me.PContractID);
		}
		if(me.IsCheckStatus==true){
			params.push("preceiveplan.Status in(3,5,7)" );	
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
		/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(5, 0, {
			text: '已收',
			dataIndex: 'PReceivePlan_ReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '待收',
			dataIndex: 'PReceivePlan_UnReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '状态',
			dataIndex: 'PReceivePlan_Status',
			width: 85,
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('PReceivePlanStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		},{
			xtype: 'actioncolumn',
			text: '变更',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var Status = record.get('PReceivePlan_Status');
					var ReceivePlanAmount = record.get('PReceivePlan_ReceivePlanAmount');
					//变更审核退回和审核通过显示变更按钮					
					if(Status.toString() == me.EditStatus.toString() || Status.toString() == me.BackEditStatus.toString() ) {
						meta.tdAttr = 'data-qtip="<b>变更</b>"';
						return 'button-edit hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var UnReceiveAmount = rec.get('PReceivePlan_UnReceiveAmount');
					//收款计划内容Id
					var ReceiveGradationID = rec.get('PReceivePlan_ReceiveGradationID');
                    //收款计划内容
					var ReceiveGradationName = rec.get('PReceivePlan_ReceiveGradationName');
					//收款金额
					var ReceivePlanAmount = rec.get('PReceivePlan_ReceivePlanAmount');
					//已付款
					var ReceiveAmount = rec.get('PReceivePlan_ReceiveAmount');
					me.openEditForm(id, ReceivePlanAmount,UnReceiveAmount,ReceiveAmount,ReceiveGradationID,ReceiveGradationName);
				}
			}]
		});
		return columns;
	}
});