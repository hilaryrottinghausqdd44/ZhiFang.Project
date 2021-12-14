/**
 * 任务执行
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.execute.Form',{
	extend:'Shell.class.wfm.task.basic.EditForm',

    title:'任务执行',
    
    /**处理中ID*/
	IngId:JShell.WFM.GUID.TaskStatus.ExecuteIng.GUID,
	/**处理中文字*/
	IngName:JShell.WFM.GUID.TaskStatus.ExecuteIng.text,
	
	/**通过ID*/
	OverId:JShell.WFM.GUID.TaskStatus.ExecuteOver.GUID,
	/**通过文字*/
	OverName:JShell.WFM.GUID.TaskStatus.ExecuteOver.text,
	
	/**退回ID*/
	BackId:JShell.WFM.GUID.TaskStatus.NoExecute.GUID,
	/**退回文字*/
	BackName:JShell.WFM.GUID.TaskStatus.NoExecute.text,
	
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		//信息行
		me.InfoLabel.colspan = 2;
		me.InfoLabel.style = "text-align:center;";
		me.InfoLabel.width = me.defaults.width * me.InfoLabel.colspan;
		items.push(me.InfoLabel);
		//执行人
		me.PTask_ExecutorName.colspan = 1;
		items.push(me.PTask_ExecutorName);
			
		//任务名称
		me.PTask_CName.colspan = 2;
		me.PTask_CName.width = me.defaults.width * me.PTask_CName.colspan;
		items.push(me.PTask_CName);
		//任务类别
		me.PTask_TypeName.colspan = 1;
		items.push(me.PTask_TypeName);
		
		//任务内容
		me.PTask_Contents.colspan = 3;
		me.PTask_Contents.width = me.defaults.width * me.PTask_Contents.colspan;
		items.push(me.PTask_Contents);
		
		//--------------------------------------------
		//客户选择
		me.PTask_PClient_Name.colspan = 1;
		items.push(me.PTask_PClient_Name);
		//紧急程度
		me.PTask_Urgency_CName.colspan = 1;
		items.push(me.PTask_Urgency_CName);
		//执行方式
		me.TaskExecutType.colspan = 1;
		items.push(me.TaskExecutType);
		
		//要求完成时间
		me.PTask_ReqEndTime.colspan = 1;
		items.push(me.PTask_ReqEndTime);
		
		// 添加任务分类查询项   @author liangyl @version 2017-07-13
	    me.PTask_PClassName.colspan = 1;
		items.push(me.PTask_PClassName);
		//任务进度  
		me.PTask_Pace_CName.colspan = 1;
		items.push(me.PTask_Pace_CName);
		
		
		//执行地点  隐藏执行地点 @author liangyl @version 2017-07-13
		me.PTask_ExecutAddr.colspan = 2;
		me.PTask_ExecutAddr.hidden = true;
		me.PTask_ExecutAddr.width = me.defaults.width * me.PTask_ExecutAddr.colspan;
		items.push(me.PTask_ExecutAddr);

		//任务状态
		me.PTask_Status_CName.hidden = true;
		items.push(me.PTask_Status_CName);
		
		for(var i in items){
			items[i].readOnly = true;
			items[i].locked = true;
		}
		
		me.TaskExecutType.readOnly = false;
		me.TaskExecutType.locked = false;
		
		me.PTask_ExecutAddr.readOnly = false;
		me.PTask_ExecutAddr.locked = false;
		
		me.PTask_Pace_CName.readOnly = false;
		me.PTask_Pace_CName.locked = false;
		
		//计划开始时间
		me.PTask_EstiStartTime.colspan = 1;
		items.push(me.PTask_EstiStartTime);
		//计划完成时间
		me.PTask_EstiEndTime.colspan = 1;
		items.push(me.PTask_EstiEndTime);
		//预计工作量
		me.PTask_EstiWorkload.colspan = 1;
		items.push(me.PTask_EstiWorkload);
		//实际开始时间
		me.PTask_StartTime.colspan = 1;
		items.push(me.PTask_StartTime);
		//实际完成时间
		me.PTask_EndTime.colspan = 1;
		items.push(me.PTask_EndTime);
		//实际工作量
		me.PTask_Workload.colspan = 1;
		items.push(me.PTask_Workload);
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			params = {};
			
		params.entity = {
			Id:me.PK,
			EstiStartTime:JShell.Date.toServerDate(values.PTask_EstiStartTime),
			EstiEndTime:JShell.Date.toServerDate(values.PTask_EstiEndTime),
			EstiWorkload:values.PTask_EstiWorkload,
			
			StartTime:JShell.Date.toServerDate(values.PTask_StartTime),
			EndTime:JShell.Date.toServerDate(values.PTask_EndTime),
			Workload:values.PTask_Workload
		};
		//任务状态
		if(values.PTask_Status_Id){
			params.entity.Status = {
				Id:values.PTask_Status_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			params.entity.StatusName = values.PTask_Status_CName;
		}
		//执行方式
		if(values.PTask_ExecutType_Id){
			params.entity.ExecutType = {
				Id:values.PTask_ExecutType_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			params.entity.ExecutTypeName = values.PTask_ExecutType_CName;
		}
		
		//任务进度
		if(values.PTask_Pace_Id){
			params.entity.Pace = {
				Id:values.PTask_Pace_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			params.entity.PaceName = values.PTask_Pace_CName;
		}
		var fields = [
			'Id','Status_Id','StatusName',
			'EstiStartTime','EstiEndTime','EstiWorkload',
			'StartTime','EndTime','Workload',
			'ExecutAddr','ExecutType_Id','ExecutTypeName',
			'Pace_Id','PaceName'
		];
		
		params.fields = fields.join(',');
			
		return params;
	},
	/**@overwrite 是否显示处理按钮*/
	showIngButton:function(StatusId){
		//是否是分配通过状态
		return JShell.WFM.GUID.TaskStatus.PublisherOver.GUID == StatusId;
	},
	/**@overwrite 通过按钮校验*/
	isOverValid:function(){
		var me = this,
			values = me.getForm().getValues(),
			isValid = true;
		
		if(!values.PTask_StartTime){isValid = false;}
		if(!values.PTask_EndTime){isValid = false;}
		if(!values.PTask_Workload){isValid = false;}
		
		if(!isValid){
			JShell.Msg.error('必须填写实际开始时间、实际完成时间、实际工作量！');
		}
		
		return isValid;
	}
});