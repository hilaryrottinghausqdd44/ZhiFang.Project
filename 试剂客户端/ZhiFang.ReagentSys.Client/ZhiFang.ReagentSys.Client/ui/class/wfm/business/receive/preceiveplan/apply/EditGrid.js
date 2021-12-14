/**
 * “收款计划”页签(第一个入口,商务人员对合同进行“正式签署”)
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.apply.EditGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.apply.Grid',
	title: '收款计划列表',
	/**通过文字*/
	OverName: '',
	/**退回文字*/
	BackName: '',
	/**使用中*/
	Status: 3,
	/**编辑状态*/
	EditStatus: 3,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**底部工具栏*/
	hasBottomToolbar: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**通过文字*/
	OverName: '同意',
	/**退回文字*/
	BackName: '不同意',
	/**合同总金额*/
	Amount: 0,
	/**合同ID*/
	PContractID: null,
	/**合同名称*/
	PContractName: '',
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
	defaultLoad: false,
	/**是否是合同签署页*/
	IsContractPanel: true,
	IsCheckStatus:true,
	IsEdit:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.onSearch();
		}, null, 500);
		me.changeAmountText(me.Amount, '合同金额:');
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
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-', {
			text: '新增',
			tooltip: '新增',
			iconCls: 'button-add',
			itemId: 'Add',
			handler: function(but, e) {
				me.IsEdit=true;
				me.changeEdit();
				me.onAddClick();
			}
		}, '-', {
			xtype: 'label',
			text: '',
			itemId: 'AmountText',
			name: 'AmountText',
			style: "font-weight:bold;color:red;",
			margin: '0 10 0 10'
		},'-',{
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
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this,
			i = 0;
		me.focus(false);
		if(isSubmit) {
			if(me.store.getCount() > 0) {
				if(!me.IsValid('1')) {
					return;
				}
			}
			me.store.each(function(record) {
				i = i + 1;
				//新增
				if(record.get('add') == '1') {
					me.onSave(record, i);
				}
			});
		} else {
			JShell.Action.delay(function() {
				me.onSearch();
			}, null, 500);
		}
	},
	changeEdit:function(){
		var me=this;
		  me.on({
		    beforeedit : function(editor, e) { 
		    	if(me.IsEdit){
		    		return true;
		    	}
		    	else{
		    		 if(e.colIdx== 1 && e.record.data.direct == 0){ 
			            return true; 
			        }else if(e.colIdx == 2 && e.record.data.direct == 1){ 
			            return true; 
			        }else{ 
			            return false; 
			        } 
		    	}
		    } 
		}); 
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records.length > 0) {
			me.IsEdit=false;
		}
	    me.changeEdit();
	}
});