/**
 * 合同申请
 * @author Jcall
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.contract.apply.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '合同申请',
	
	requires:['Shell.ux.toolbar.Button'],
	
	width: 700,
	height: 480,

	autoScroll: false,

	/**合同ID*/
	PK: null,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			isValid:function(p){
				me.setActiveTab(me.Form);
			},
			beforesave: function(p) {
				me.showMask(me.saveText);
			},
			save: function(p, id) {
				me.hideMask();
				me.PK = id;
				me.onSaveAttachment(id);
				//保存收款计划
				me.Preceiveplan.AddSaveInfo(me.PK);               
			},
			saveerror:function(p){
				me.hideMask();
			}
		});
		me.Attachment.on({
			save: function(win,data) {
				if(data.success){
					me.fireEvent('save', me, me.PK);
				}else{
					JShell.Msg.error(data.msg);
				}
			}
		});
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'Preceiveplan':
						me.Preceiveplan.onSaveReceivePlan(me.Form,me.PK);
						break;
					default:
					
						break
				}
			},
			beforetabchange:function(tabPanel,  newCard, oldCard,  eOpts ){
				var edit = me.Preceiveplan.getPlugin('NewsGridEditing'); 
                edit.completeEdit();
                edit.cancelEdit();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;

		me.Form = Ext.create('Shell.class.wfm.business.contract.apply.Form', {
			formtype: 'add',
			hasLoadMask: false,//开启加载数据遮罩层
			title: '合同内容',
			hasButtontoolbar:false//带功能按钮栏
		});
		
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			title: '合同附件'
		});
		
	 	me.EquipInfoListForm = Ext.create('Shell.class.wfm.business.contract.apply.EquipInfoListForm', {
			title: '仪器清单',
			header: false,
			formtype: 'add',
			layout: 'fit',
			itemId: 'EquipInfoListForm',
			border: false,
			hasButtontoolbar:false//带功能按钮栏
		});
	
		me.PurchaseDescForm = Ext.create('Shell.class.wfm.business.contract.apply.PurchaseDescForm', {
			title: '采购说明',
			header: false,
			formtype: 'add',
			layout: 'fit',
			itemId: 'PurchaseDescForm',
			border: false,
			hasButtontoolbar:false//带功能按钮栏
		});

		me.Preceiveplan = Ext.create('Shell.class.wfm.business.contract.apply.PreceiveplanTree',{
			title:'收款计划',
			itemId: 'Preceiveplan',
			PContractID:me.PK
		});
		return [me.Form, me.Attachment,me.EquipInfoListForm,me.PurchaseDescForm,me.Preceiveplan];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'暂存',
				iconCls:'button-save',
				tooltip:'暂存',
				handler:function(){
					me.onSave(false);
				}
			},{
				text:'提交',
				iconCls:'button-save',
				tooltip:'提交',
				handler:function(){
					me.onSave(true);
				}
			},'reset']
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.Form.getForm().getValues();
		if(!me.Form.getForm().isValid()){
			me.Form.fireEvent('isValid',me);
			return;
		}
		var roonodes = me.Preceiveplan.getRootNode().childNodes; //获取主节点
		if(roonodes.length>0){
            me.Preceiveplan.Amount=values.PContract_Amount;
            //验证收款计划
	        if(!me.Preceiveplan.IsValid('1')) {
				return;
			}
		}
		var contentvalues = me.EquipInfoListForm.getForm().getValues();
		//采购说明
		var purchasedescvalues = me.PurchaseDescForm.getForm().getValues();
		//采购说明为空时，设置默认值
		var PurchaseDescHTML=purchasedescvalues.PContract_PurchaseDescHTML.replace(/\\/g, '&#92');
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}
			if(isSubmit){//提交
    			var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus','申请');
				me.Form.getForm().setValues({
					PContract_ContractStatus:info.Id,
					PContract_LinkEquipInfoListHTML:contentvalues.PContract_LinkEquipInfoListHTML.replace(/\\/g, '&#92'),
					PContract_PurchaseDescHTML:PurchaseDescHTML
				});
			}else{//暂存
				var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus','暂存');
				me.Form.getForm().setValues({
					PContract_ContractStatus:info.Id,
					PContract_LinkEquipInfoListHTML:contentvalues.PContract_LinkEquipInfoListHTML.replace(/\\/g, '&#92'),
					PContract_PurchaseDescHTML:PurchaseDescHTML
				});
			}
            //合同编号不为空，需要验证
            if(values.PContract_ContractNumber){
            	me.Form.isValidContractNumber(function(){
					me.Form.onSaveClick();
				});
            }else{
            	me.Form.onSaveClick();
            }
           
//			//新增时需要校验合同编号是否不重复
//			if(values.PContract_Id){
//				me.Form.onSaveClick();
//			}else{
//				me.Form.isValidContractNumber(function(){
//					me.Form.onSaveClick();
//				});
//			}
    	});
	},
	onSaveAttachment: function(id) {
		var me = this;
		
		me.Attachment.setValues({
			fkObjectId: id
		});
		me.Attachment.save();
	},
	onSaveLinkEquipInfoListHTML: function(id) {
		var me = this;
		me.EquipInfoListForm.save(id);
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});