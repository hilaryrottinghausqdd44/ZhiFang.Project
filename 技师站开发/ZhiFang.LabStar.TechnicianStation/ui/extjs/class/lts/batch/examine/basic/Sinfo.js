/**
 * 成功消息
 * @author liangyl	
 * @version 2021-03-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.Sinfo',{
    extend:'Shell.ux.form.Panel',
    title:'成功消息',
    layout:'fit',
    /**功能按钮栏位置*/
	buttonDock:'top',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.buttonToolbarItems = [{xtype: 'label',text: '成功消息',margin: '5 0 0 10',style: "font-weight:bold;color:blue;"}];
		//创建挂靠功能栏
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		return  items =[{xtype: 'textareafield',itemId:'msg',fieldStyle	:"border: none; outline: none;box-shadow: none;"}];
	},
	isShow:function(id){
		var me = this;
		me.setReadOnly(true);
		me.formtype = 'show';
		me.changeTitle();//标题更改
		me.disableControl();
		me.load(id);
	},
	//赋值
	setValue : function(msg){
		var me = this;
		me.getComponent('msg').setValue(msg);
	}
});