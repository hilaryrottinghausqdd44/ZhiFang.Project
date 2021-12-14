/**
 * 任务验收
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.check.Form',{
	extend:'Shell.class.wfm.task.execute.Form',

    title:'任务验收',
    
    /**处理中ID*/
	IngId:JShell.WFM.GUID.TaskStatus.CheckIng.GUID,
	/**处理中文字*/
	IngName:JShell.WFM.GUID.TaskStatus.CheckIng.text,
	
	/**通过ID*/
	OverId:JShell.WFM.GUID.TaskStatus.CheckOver.GUID,
	/**通过文字*/
	OverName:JShell.WFM.GUID.TaskStatus.CheckOver.text,
	
	/**退回ID*/
	BackId:JShell.WFM.GUID.TaskStatus.CheckBack.GUID,
	/**退回文字*/
	BackName:JShell.WFM.GUID.TaskStatus.CheckBack.text,
	
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = me.callParent(arguments);
		
		for(var i in items){
			items[i].readOnly = true;
			items[i].locked = true;
		}
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			params = {};
		
		params.entity = {
			Id:me.PK
		};
		//任务状态
		if(values.PTask_Status_Id){
			params.entity.Status = {
				Id:values.PTask_Status_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			params.entity.StatusName = values.PTask_Status_CName;
		}
		
		var fields = ['Id','Status_Id','StatusName'];
		
		params.fields = fields.join(',');
			
		return params;
	},
	/**@overwrite 是否显示处理按钮*/
	showIngButton:function(StatusId){
		//是否是执行完成状态
		return JShell.WFM.GUID.TaskStatus.ExecuteOver.GUID == StatusId;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isOver){
		var me = this,
			values = me.getForm().getValues();
			
//		//执行完成
//		var isExecuteOver = JShell.WFM.GUID.TaskStatus.ExecuteOver.GUID == values.PTask_Status_Id;
//		//验收中
//		var isCheckIng = JShell.WFM.GUID.TaskStatus.CheckIng.GUID == values.PTask_Status_Id;
//		
//		if(!isExecuteOver && !isCheckIng){
//			JShell.Msg.error('当前的任务状态错误，请刷新任务列表再进行操作！');
//			return;
//		}
		
		me.callParent(arguments);
	},
	/**@overwrite 通过按钮校验*/
	isOverValid:function(){
		return true;
	}
});