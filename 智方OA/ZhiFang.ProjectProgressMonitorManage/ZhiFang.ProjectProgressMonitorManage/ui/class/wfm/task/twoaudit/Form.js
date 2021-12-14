/**
 * 任务二审
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.twoaudit.Form',{
	extend:'Shell.class.wfm.task.basic.EditForm',

    title:'任务二审',
    
    /**处理中ID*/
	IngId:JShell.WFM.GUID.TaskStatus.TwoAuditIng.GUID,
	/**处理中文字*/
	IngName:JShell.WFM.GUID.TaskStatus.TwoAuditIng.text,
	
	/**通过ID*/
	OverId:JShell.WFM.GUID.TaskStatus.TwoAuditOver.GUID,
	/**通过文字*/
	OverName:JShell.WFM.GUID.TaskStatus.TwoAuditOver.text,
	
	/**退回ID*/
	BackId:JShell.WFM.GUID.TaskStatus.TwoAuditBack.GUID,
	/**退回文字*/
	BackName:JShell.WFM.GUID.TaskStatus.TwoAuditBack.text,
	
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			params = me.callParent(arguments);	
		
		if(!params.entity.PTask_TwoAuditID){
			var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
				USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				
			params.entity.TwoAuditID = USERID;
			params.entity.TwoAuditName = USERNAME;
			params.fields += ',TwoAuditID,TwoAuditName';
		}
		
//		//二审通过时，清空分配人+分配时间
//		if(params.entity.Status.Id == JShell.WFM.GUID.TaskStatus.TwoAuditOver.GUID){
//			params.entity.PublisherID = null;
//			params.entity.PublisherName = null;
//			params.entity.PublisherDataTime = null;
//			params.fields += ',PublisherID,PublisherName,PublisherDataTime';
//		}
		
		return params;
	},
	/**@overwrite 受理*/
	onAccept:function(callback){
		var me = this,
			values = me.getForm().getValues(),
			url = JShell.System.Path.getRootUrl(me.editUrl);
		
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			
		var params = {
			entity:{
				Id:values.PTask_Id,
				TwoAuditID:USERID,
				TwoAuditName:USERNAME,
				Status:{
					Id:me.IngId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				StatusName:me.IngName
			},
			fields:'Id,TwoAuditID,TwoAuditName,Status_Id,StatusName'
		};
		
		var id = params.entity.Id;
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(Ext.typeOf(callback) == 'function'){
					callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 是否显示处理按钮*/
	showIngButton:function(StatusId){
		//是否是一审通过状态
		return JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID == StatusId;
	}
});