/**
 * 任务一审
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.oneaudit.Form',{
	extend:'Shell.class.wfm.task.basic.EditForm',

    title:'任务一审',
    
    /**处理中ID*/
	IngId:JShell.WFM.GUID.TaskStatus.OneAuditIng.GUID,
	/**处理中文字*/
	IngName:JShell.WFM.GUID.TaskStatus.OneAuditIng.text,
	
	/**通过ID*/
	OverId:JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID,
	/**通过文字*/
	OverName:JShell.WFM.GUID.TaskStatus.OneAuditOver.text,
	
	/**退回ID*/
	BackId:JShell.WFM.GUID.TaskStatus.OneAuditBack.GUID,
	/**退回文字*/
	BackName:JShell.WFM.GUID.TaskStatus.OneAuditBack.text,
	
	/**@overwrite 是否显示处理按钮*/
	showIngButton:function(StatusId){
		var me = this;
		
		//是否是申请状态
		//return JShell.WFM.GUID.TaskStatus.Apply.GUID == StatusId;
		
		if(JShell.WFM.GUID.TaskStatus.Apply.GUID == StatusId){
			me.getForm().setValues({
				PTask_Status_CName:me.IngName,
				PTask_Status_Id:me.IngId
			});
			
			me.STATUS_ID = me.IngId;
			me.onAccept(function(){
				//任务操作记录
				me.onSavePTaskOperLog(me.PK,me.STATUS_ID,function(data){
					if(data.success){
						//创建受理中文字
						me.createIngLabel();
					}else{
						JShell.Msg.error(data.msg);
					}
				});
			});
		}else if(JShell.WFM.GUID.TaskStatus.OneAuditIng.GUID == StatusId || 
			JShell.WFM.GUID.TaskStatus.TwoAuditBack.GUID == StatusId){
			//创建受理中文字
			me.createIngLabel();
		}
		
		return null;
	}
});