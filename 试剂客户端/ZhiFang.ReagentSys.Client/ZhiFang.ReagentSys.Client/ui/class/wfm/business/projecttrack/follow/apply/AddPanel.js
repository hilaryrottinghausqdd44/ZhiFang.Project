/**
 * 项目跟踪
 * @author liangyl	
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '项目跟踪申请',
	
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

		me.Form = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.Form', {
			formtype: 'add',
			hasLoadMask: false,//开启加载数据遮罩层
			title: '项目跟踪内容',
			hasButtontoolbar:false//带功能按钮栏
		});
		
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			title: '项目附件'
		});
		
	 	me.EquipInfoListForm = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.EquipInfoListForm', {
			title: '仪器清单',
			header: false,
			formtype: 'add',
			layout: 'fit',
			itemId: 'EquipInfoListForm',
			border: false,
			hasButtontoolbar:false//带功能按钮栏
		});
	
		me.PurchaseDescForm = Ext.create('Shell.class.wfm.business.projecttrack.follow.apply.PurchaseDescForm', {
			title: '采购说明',
			header: false,
			formtype: 'add',
			layout: 'fit',
			itemId: 'PurchaseDescForm',
			border: false,
			hasButtontoolbar:false//带功能按钮栏
		});
		return [me.Form, me.Attachment,me.EquipInfoListForm,me.PurchaseDescForm];
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