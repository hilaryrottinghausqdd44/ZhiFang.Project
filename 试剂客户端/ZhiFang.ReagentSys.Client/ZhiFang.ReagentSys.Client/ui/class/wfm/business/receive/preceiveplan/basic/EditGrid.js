/**
 * 可编辑列表
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.EditGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.Grid',
	title: '收款计划列表',
	width: 600,
	height: 380,
	defaultLoad: true,
	/**合同总金额*/
	Amount: 0,
	/**通过文字*/
	OverName: '同意',
	/**退回文字*/
	BackName: '不同意',
	/**关闭按钮*/
	CloseName: '',
	/**编辑状态*/
	EditStatus: 1,
	PK: null,
	/**底部工具栏*/
	hasBottomToolbar: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
    /**合同ID*/
	PContractID: null,
	/**合同名称*/
	PContractName: '',
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
    /**收款分期ID*/
	GradationID: null,
	/**收款分期*/
	GradationName: '',
	/**已收款*/
	ReceiveAmount: 0,
	/**有效计划*/
	IsCheckStatus:true,
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function() {
				me.store.removeAll();
			}
		});
		
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);

		return columns
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
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasBottomToolbar) items.push(me.createBottomtoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
		buttonToolbarItems.push({
			text: '新增',
			tooltip: '新增',
			iconCls: 'button-add',
			itemId: 'Add',
			handler: function(but, e) {
				me.onAddClick();
			}
		}, '-',{
			boxLabel: '有效计划',
			itemId: 'CheckStatus',
			checked: true,
			value: true,
			inputValue: true,
			hidden:!me.IsCheckStatus,
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
		},'-',{
			xtype: 'label',
			text: '',
			itemId: 'AmountText',
			name:'AmountText',
			style: "font-weight:bold;color:red;",
			margin: '0 10 0 10'
		});
		return buttonToolbarItems;
	},
	/**工具栏*/
	createBottomtoolbar: function() {
		var me = this,
			items = [];

		if(items.length == 0) {
			items.push('->');
			//通过按钮
			if(me.OverName) {
				items.push({
					text: me.OverName,
					tooltip: me.OverName,
					iconCls: 'button-save',
					itemId: 'Save',
					handler: function() {
						me.onSaveClick(true);
					}
				});
			}
			//退回按钮
			if(me.BackName) {
				items.push({
					text: me.BackName,
					tooltip: me.BackName,
					iconCls: 'button-save',
					handler: function() {
						me.onSaveClick(false);
					}
				});
			}
			//关闭按钮
			if(me.CloseName) {
				items.push({
					text: me.CloseName,
					tooltip: me.CloseName,
					iconCls: 'button-del',
					handler: function() {
						me.close();
					}
				});
			}
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	onAddClick: function() {
		var me = this;
		me.createAddRec();
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this;
		me.store.each(function(record) {
			//新增
			if(record.get('add') == '1') {
				me.onSave(record);
			}
		});
	},
	onSave: function(rec) {
		var me = this,
			msgInfo = '',
			url = '';
	},
	/**增加一行*/
	createAddRec: function() {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var obj = {
			PReceivePlan_ReceiveGradationID: me.GradationID,
			PReceivePlan_ReceiveGradationName: me.GradationName,
			PReceivePlan_ContractNumber: '',
			PReceivePlan_ReceivePlanAmount: 0,
			PReceivePlan_ExpectReceiveDate: '',
			PReceivePlan_ReceiveManName: me.Principal,
			PReceivePlan_ReceiveManID: me.PrincipalID,
			PReceivePlan_ReceiveAmount:me.ReceiveAmount,
			PReceivePlan_Id: '',
			InputerID: userId,
			InputerName: userName,
			add: '1'
		};
		me.store.insert(me.store.getCount(), obj);
	},
	/**删除本行*/
	createDelRec: function(i, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		if(id) {
			me.delOneById(i, id);
			me.store.remove(rec);
		} else {
			me.store.remove(rec);
		}
	},
	/**验证*/
	IsValid: function(str) {
		var me = this,PlanAmount=0,isbool=true,Msg='',Count=0;
		me.store.each(function(rec) {
		    var ReceivePlanAmount = rec.get('PReceivePlan_ReceivePlanAmount');
	        PlanAmount=PlanAmount+ parseFloat(ReceivePlanAmount);
            Count=Number(PlanAmount.toFixed(2));
			var ReceiveGradationName = rec.get('PReceivePlan_ReceiveGradationName');
			ReceiveGradationName.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			if(!ReceiveGradationName) {
//				Msg=Msg+'收款分期不能为空!'+'<br/>'
				JShell.Msg.error('收款分期不能为空!');
				isbool=false;
				return;
			}
			var ExpectReceiveDate = rec.get('PReceivePlan_ExpectReceiveDate');
			if(!ExpectReceiveDate) {
//				Msg=Msg+'收款时间不能为空!'+'<br/>'
				JShell.Msg.error('收款时间不能为空!');
				isbool=false;
				return;
			}
			if(ReceivePlanAmount.toString()=='0' || ReceivePlanAmount.toString()=='0.00' ) {
//				Msg=Msg+'收款金额不能等于0!'+'<br/>'
				JShell.Msg.error('收款金额不能等于0!');
				isbool=false;
				return;
			}
		});
	   	if(me.Amount.toString() != Count.toString()) {
	   		if(str!='1'){
	   			Msg='新收款计划总额:'+Count+'不等于原始收款计划未收总额:'+me.Amount+",请校验!";
	   		}else{
	   			Msg='收款总额:'+Count+'不等于合同金额:'+me.Amount+",请校验!";
	   		}
			JShell.Msg.error(Msg);
			isbool=false;
			return;
		}
//	   	if(!isbool){
//	   		JShell.Msg.error(Msg);
//	   	}
		return isbool;
	},
	//显示合同金额
	changeAmountText:function(Amount,str){
		var me=this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		var AmountText = buttonsToolbar.getComponent('AmountText');

		AmountText.setText(str+Amount);
		
	}
});