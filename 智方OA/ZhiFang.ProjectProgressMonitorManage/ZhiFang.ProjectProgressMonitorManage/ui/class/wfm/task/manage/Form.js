/**
 * 任务详情管理
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.manage.Form',{
	extend:'Shell.class.wfm.task.basic.EditForm',

    title:'任务详情管理',
    
    /**处理中ID*/
	IngId:null,
	/**处理中文字*/
	IngName:null,
	
	/**通过ID*/
	OverId:null,
	/**通过文字*/
	OverName:null,
	
	/**退回ID*/
	BackId:null,
	/**退回文字*/
	BackName:null,
	
	/**初始化受理信息*/
	initIngInfo:function(data){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		buttonsToolbar.insert(1,{
			text:'保存修改内容',
			iconCls:'button-save',
			tooltip:'保存修改内容',
			handler:function(){
				//保存临时存储的内容
				me.onUpdateInfo();
			}
		},{
			xtype:'tbseparator'
		});
	}
});