/**
 * 任务分配
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.publisher.Form',{
	extend:'Shell.class.wfm.task.basic.EditForm',

    title:'任务分配',
    
    /**处理中ID*/
	IngId:JShell.WFM.GUID.TaskStatus.PublisherIng.GUID,
	/**处理中文字*/
	IngName:JShell.WFM.GUID.TaskStatus.PublisherIng.text,
	
	/**通过ID*/
	OverId:JShell.WFM.GUID.TaskStatus.PublisherOver.GUID,
	/**通过文字*/
	OverName:JShell.WFM.GUID.TaskStatus.PublisherOver.text,
	
	/**退回ID*/
	BackId:JShell.WFM.GUID.TaskStatus.PublisherBack.GUID,
	/**退回文字*/
	BackName:JShell.WFM.GUID.TaskStatus.PublisherBack.text,
	
	initComponent:function(){
		var me = this;
		
		me.addEvents('accept');
		
		me.callParent(arguments);
	},
	
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = me.callParent(arguments);
			
		items[0].colspan = 2;
		items[0].width = me.defaults.width * items[0].colspan;
		
		me.PTask_ExecutorName.colspan = 1;
		items.splice(1,0,me.PTask_ExecutorName);
		
		return items;
	},
	
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			params = me.callParent(arguments);	
		
		if(!params.entity.PTask_PublisherID){
			var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
				USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			
			params.entity.PublisherID = USERID;
			params.entity.PublisherName = USERNAME;
			params.fields += ',PublisherID,PublisherName';
		}
		
		//执行人
		params.entity.ExecutorID = values.PTask_ExecutorID || null ;
		params.entity.ExecutorName = values.PTask_ExecutorName || null;
		params.fields += ',ExecutorID,ExecutorName';
		
		return params;
	},
	
	/**@overwrite 通过按钮校验*/
	isOverValid:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!values.PTask_ExecutorID){
			JShell.Msg.error('请选择执行人！');
			return false;
		}
		
		return true;
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
				PublisherID:USERID,
				PublisherName:USERNAME,
				Status:{
					Id:me.IngId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				},
				StatusName:me.IngName
			},
			fields:'Id,PublisherID,PublisherName,Status_Id,StatusName'
		};
		
		var id = params.entity.Id;
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('accept',me);
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
		//是否是二审通过状态
		return JShell.WFM.GUID.TaskStatus.TwoAuditOver.GUID == StatusId;
	}
});