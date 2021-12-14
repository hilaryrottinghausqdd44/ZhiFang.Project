/**
 * 任务申请/一审
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.apply.oneaudit.Form',{
	extend:'Shell.class.wfm.task.apply.Form',

    title:'任务申请/一审',
    
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'CName','Contents','ExecutAddr','ReqEndTime',
			
			'TypeID','ExecutType_Id','Urgency_Id',
			'TypeName','ExecutTypeName','UrgencyName',
			
			'Status_Id','StatusName',
			'Id','PClient_Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PTask_Id;
		return entity;
	},
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		me.getForm().setValues({
			PTask_ApplyName:USERNAME,
			PTask_ApplyID:USERID,
			PTask_OneAuditName:USERNAME,
			PTask_OneAuditID:USERID,
			PTask_MTypeID:me.TaskMTypeId,
			PTask_MTypeName:me.TaskMTypeName
		});
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		
		if(isSubmit){//提交
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.OneAuditOver.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID;
			
			//新增服务地址，带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_PTaskAdd';
		}else{//暂存
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.Temporary.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.Temporary.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.Temporary.GUID;
			
			//新增服务地址，不带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTask';
		}
		me.onSaveClick();
	}
});