/**
 * 项目文档
 * @author liangyl
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.pproject.document.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '项目文档',
	
	requires:['Shell.ux.toolbar.Button'],
	
	width: 640,
	height: 425,

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

		me.Form = Ext.create('Shell.class.wfm.business.pproject.document.ContentForm', {
			formtype: 'edit',
			PK:me.PK,
			hasLoadMask: false,//开启加载数据遮罩层
			title: '内容',
			hasButtontoolbar:false//带功能按钮栏
		});
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment',{
			title:'附件',
			PK:me.PK,
			formtype:'edit'
		});
		return [me.Form, me.Attachment];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
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
		me.Form.onSaveClick();
	},
	onSaveAttachment: function(id) {
		var me = this;
		
		me.Attachment.setValues({
			fkObjectId: id
		});
		me.Attachment.save();
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