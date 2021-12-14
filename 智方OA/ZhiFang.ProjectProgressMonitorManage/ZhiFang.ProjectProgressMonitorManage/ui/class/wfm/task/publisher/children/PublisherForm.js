/**
 * 新增任务分配
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.publisher.children.PublisherForm',{
	extend:'Shell.class.wfm.task.basic.Form',

    title:'新增任务分配',
	
    /**父任务ID*/
	ParentTaskId:null,
	/**父任务名称*/
	ParentTaskName:null,
	
	/**任务父类型ID*/
	TaskMTypeId:null,
	/**任务父类型名称*/
	TaskMTypeName:null,
	
	/**当前状态*/
	STATUS_ID:null,
	
	/**基础修改服务地址*/
    basicEditUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskByField',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(!me.PK){
			//获取父任务信息
			me.getTaskInfo();
		}
		
		me.on({
			save:function(p,id){
				//暂存不记录任务操作记录
				if(me.STATUS_ID == JShell.WFM.GUID.TaskStatus.PublisherIng.GUID){
					me.fireEvent('aftersave',me,id);
				}else{
					//任务操作记录
					me.onSavePTaskOperLog(id,me.STATUS_ID,function(data){
						if(data.success){
							me.fireEvent('aftersave',me,id);
						}else{
							JShell.Msg.error(data.msg);
						}
					});
				}
			}
		});
	},
    initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems = ['->',{
			text:'暂存',
			iconCls:'button-save',
			tooltip:'暂存',
			handler:function(){
				me.onSave(false);
			}
		},{
			text:'提交',
			iconCls:'button-save',
			tooltip:'提交',
			handler:function(){
				me.onSave(true);
			}
		},'reset'];
		
		me.callParent(arguments);
	},
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		//信息行
		me.InfoLabel.colspan = 2;
		me.InfoLabel.style = "text-align:center;";
		me.InfoLabel.width = '100%';
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
		me.PTask_TypeName.readOnly = true;
		me.PTask_TypeName.locked = true;
		items.push(me.PTask_TypeName);
		
		//任务内容
		me.PTask_Contents.colspan = 3;
		me.PTask_Contents.width = me.defaults.width * me.PTask_Contents.colspan;
		items.push(me.PTask_Contents);
		
		//--------------------------------------------
		//客户选择
		me.PTask_PClient_Name.colspan = 1;
		me.PTask_PClient_Name.readOnly = true;
		me.PTask_PClient_Name.locked = true;
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
		//执行地点
		me.PTask_ExecutAddr.colspan = 2;
		me.PTask_ExecutAddr.width = me.defaults.width * me.PTask_ExecutAddr.colspan;
		items.push(me.PTask_ExecutAddr);
		
		//任务状态
		me.PTask_Status_CName.hidden = true;
		items.push(me.PTask_Status_CName);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.PTask_CName,//标题
			Contents:values.PTask_Contents.replace(/\\/g,'&#92'),//内容
			ExecutAddr:values.PTask_ExecutAddr,//执行地点
			IsUse:true,//是否使用
			ReqEndTime:JShell.Date.toServerDate(values.PTask_ReqEndTime)//要求完成时间
		};
		
		//父任务
		if(me.ParentTaskId){
			entity.PTaskID = me.ParentTaskId;
			entity.PTaskCName = me.ParentTaskName;
		}
		
		//任务类别
		if(values.PTask_TypeID){
			entity.TypeID = values.PTask_TypeID;
			entity.TypeName = values.PTask_TypeName;
		}
		//任务父类别
		if(values.PTask_PTypeID){
			entity.PTypeID = values.PTask_PTypeID;
			entity.PTypeName = values.PTask_PTypeName;
		}
		//任务主类别
		if(values.PTask_MTypeID){
			entity.MTypeID = values.PTask_MTypeID;
			entity.MTypeName = values.PTask_MTypeName;
		}
		
		//任务状态
		if(values.PTask_Status_Id){
			entity.Status = {
				Id:values.PTask_Status_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.StatusName = values.PTask_Status_CName;
		}
		//申请人
		if(values.PTask_ApplyID){
			entity.ApplyID = values.PTask_ApplyID;
			entity.ApplyName = values.PTask_ApplyName;
		}
		//一审人
		if(values.PTask_OneAuditID){
			entity.OneAuditID = values.PTask_OneAuditID;
			entity.OneAuditName = values.PTask_OneAuditName;
		}
		//二审人
		if(values.PTask_TwoAuditID){
			entity.TwoAuditID = values.PTask_TwoAuditID;
			entity.TwoAuditName = values.PTask_TwoAuditName;
		}
		//分配人
		if(values.PTask_PublisherID){
			entity.PublisherID = values.PTask_PublisherID;
			entity.PublisherName = values.PTask_PublisherName;
		}
		//执行人
		if(values.PTask_ExecutorID){
			entity.ExecutorID = values.PTask_ExecutorID;
			entity.ExecutorName = values.PTask_ExecutorName;
		}
		//验收人
		if(values.PTask_CheckerID){
			entity.CheckerID = values.PTask_CheckerID;
			entity.CheckerName = values.PTask_CheckerName;
		}
		
		//执行方式
		if(values.PTask_ExecutType_Id){
			entity.ExecutType = {
				Id:values.PTask_ExecutType_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.ExecutTypeName = values.PTask_ExecutType_CName;
		}
		//紧急程度
		if(values.PTask_Urgency_Id){
			entity.Urgency = {
				Id:values.PTask_Urgency_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.UrgencyName = values.PTask_Urgency_CName;
		}
		//客户选择
		if(values.PTask_PClient_Name){
			entity.PClient= {
				Id:values.PTask_PClient_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			//fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		var fields = [
			'CName','Contents','ExecutAddr','ReqEndTime',
			
			'TypeID','ExecutType_Id','Urgency_Id',
			'TypeName','ExecutTypeName','UrgencyName',
			'ExecutorID','ExecutorName',
			'Status_Id','StatusName',
			'Id','PClient_Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PTask_Id;
		return entity;
	},
	
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()) return;
		
		if(isSubmit){//提交
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.PublisherOver.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.PublisherOver.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.PublisherOver.GUID;
			
			//新增服务地址，带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_PTaskAdd';
			
			if(values.PTask_ExecutorID){
				me.onSaveClick();
			}else{
				JShell.Msg.error('请选择执行人！');
			}
		}else{//暂存
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.PublisherIng.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.PublisherIng.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.PublisherIng.GUID;
			
			//新增服务地址，不带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTask';
    		
			//保存临时存储的内容
			me.onUpdateInfo();
		}
	},
	/**获取父任务信息*/
	getTaskInfo:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl),
			USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		//任务主类别、任务父类别、任务类别、客户
		var fields = [
			'MTypeID','MTypeName','PTypeID','PTypeName','TypeID','TypeName',
			'PClient_Id','PClient_Name'
		];
		
		url += '&fields=PTask_' + fields.join(',PTask_') + '&id=' + me.ParentTaskId;
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(data.value){
					me.getForm().setValues({
						PTask_MTypeID:data.value.PTask_MTypeID,
						PTask_MTypeName:data.value.PTask_MTypeName,
						PTask_PTypeID:data.value.PTask_PTypeID,
						PTask_PTypeName:data.value.PTask_PTypeName,
						PTask_TypeID:data.value.PTask_TypeID,
						PTask_TypeName:data.value.PTask_TypeName,
						
						PTask_PClient_Id:data.value.PTask_PClient_Id,
						PTask_PClient_Name:data.value.PTask_PClient_Name,
						
						PTask_ApplyID:USERID,
						PTask_ApplyName:USERNAME,
						PTask_OneAuditID:USERID,
						PTask_OneAuditName:USERNAME,
						PTask_TwoAuditID:USERID,
						PTask_TwoAuditName:USERNAME,
						PTask_PublisherID:USERID,
						PTask_PublisherName:USERNAME,
						PTask_CheckerID:USERID,
						PTask_CheckerName:USERNAME
					});
					if(Ext.typeOf(callback) == 'function') callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**保存临时存储的内容*/
	onUpdateInfo:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = !me.PK ? me.addUrl : me.basicEditUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = !me.PK ? me.getAddParams() : me.getEditParams();
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				var id = me.formtype == 'add' ? data.value : me.PK;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});