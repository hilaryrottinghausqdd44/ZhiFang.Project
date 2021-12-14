/**
 * 列表树
 * @author liangyl
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.change.GridTree', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',
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
	//=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
	   	columns = me.callParent(arguments);
		columns.splice(2, 0,  {
			text: '状态',
			dataIndex: 'Status',
			width: 65,
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
					var Status = record.get('Status');
					var ReceivePlanAmount = record.get('ReceivePlanAmount');
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
					var UnReceiveAmount = rec.get('UnReceiveAmount');
					//收款计划内容Id
					var ReceiveGradationID = rec.get('ReceiveGradationID');
                    //收款计划内容
					var ReceiveGradationName = rec.get('ReceiveGradationName');
					//收款金额
					var ReceivePlanAmount = rec.get('ReceivePlanAmount');
					//已付款
					var ReceiveAmount = rec.get('ReceiveAmount');
					me.openEditForm(id, ReceivePlanAmount,UnReceiveAmount,ReceiveAmount,ReceiveGradationID,ReceiveGradationName);
				}
			}]
		});
		return columns;
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
			Amount: UnReceiveAmount,
			ReceiveAmount:ReceiveAmount,
			UnReceiveAmount:UnReceiveAmount,
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
					me.load();
				}
			}
		}).show();
	}
});