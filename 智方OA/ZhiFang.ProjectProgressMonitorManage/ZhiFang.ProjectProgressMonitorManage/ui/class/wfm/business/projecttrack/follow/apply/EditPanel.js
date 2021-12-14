/**
 * 项目跟踪申请
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.EditPanel', {
	extend: 'Ext.tab.Panel',
	title: '项目跟踪申请',

	width: 700,
	height: 480,
	
	/**项目跟踪ID*/
	PK: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Form.on({
			save: function(p, id) {
				me.fireEvent('save', me, id);
			}
		});
		
		me.Attachment.on({
			selectedfilerender: function(p) {
				me.Attachment.save();
			},
			save: function(p) {
				if(me.Attachment.progressMsg!="")
				JShell.Msg.alert(me.Attachment.progressMsg);
			}
		});
//		me.Interaction.on({
//			save:function(p){
//				me.fireEvent('saveInteraction', p);
//			}
//		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
//		me.addEvents('saveInteraction');
		me.items = me.createItems();
			//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		me.Form = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.Form',{
			title:'项目内容',
			formtype: 'edit',
			hasButtontoolbar:false,//带功能按钮栏
			hasLoadMask: false,//开启加载数据遮罩层
			PK:me.PK
		});
		me.Interaction = Ext.create('Shell.class.wfm.business.projecttrack.interaction.App',{
			title:'跟踪记录',
			FormPosition:'s',
			PK:me.PK
		});
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment',{
			title:'项目附件',
			PK:me.PK
		});
		me.EquipInfoListForm = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.EquipInfoListForm', {
			title: '仪器清单',
			header: false,
			formtype: 'edit',
			PK:me.PK,
			itemId: 'EquipInfoListForm',
			border: false,
			/**带功能按钮栏*/
        	hasButtontoolbar: false
		});
		me.PurchaseDescForm = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.PurchaseDescForm', {
			title: '采购说明',
			header: false,
			formtype: 'edit',
			PK:me.PK,
			itemId: 'PurchaseDescForm',
			border: false,
			/**带功能按钮栏*/
        	hasButtontoolbar: false
		});
		return [me.Form,me.Interaction,me.Attachment,me.EquipInfoListForm,me.PurchaseDescForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'保存',
				iconCls:'button-save',
				tooltip:'保存',
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
		var contentvalues = me.EquipInfoListForm.getForm().getValues();
		//采购说明
		var purchasedescvalues = me.PurchaseDescForm.getForm().getValues();
		//采购说明为空时，设置默认值
		var PurchaseDescHTML=purchasedescvalues.PContractFollow_PurchaseDescHTML.replace(/\\/g, '&#92');
		me.Form.getForm().setValues({
			PContractFollow_LinkEquipInfoListHTML:contentvalues.PContractFollow_LinkEquipInfoListHTML.replace(/\\/g, '&#92'),
			PContractFollow_PurchaseDescHTML:PurchaseDescHTML
		});
         me.Form.onSaveClick();
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	}
});