/**
 * 任务申请
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.apply.Form',{
	extend:'Shell.class.wfm.task.basic.Form',

    title:'任务申请',
    width:670,
	height:370,
	
	/**获取员工信息*/
	selectEmpUrl:'/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true',
	
	/**每个组件的默认属性*/
    defaults:{
        labelWidth:85,
        width:200,
        labelAlign:'right'
    },
    
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
	
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			save:function(p,id){
				//暂存不记录任务操作记录
				if(me.STATUS_ID == JShell.WFM.GUID.TaskStatus.Temporary.GUID){
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
		
		if(me.hasButtontoolbar){
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
		}
		
		me.callParent(arguments);
	},
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		if(me.PK){
			//信息行
			me.InfoLabel.colspan = 3;
			me.InfoLabel.style = "text-align:center;";
			me.InfoLabel.width = '100%';
			items.push(me.InfoLabel);
		}
			
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
		
		// 任务分类  @author liangyl  @version 2017-07-13  添加任务分类，隐藏执行地点
		me.PTask_PClassName.colspan = 2;
		items.push(me.PTask_PClassName);
		
		//执行地点
		me.PTask_ExecutAddr.colspan = 2;
		me.PTask_ExecutAddr.hidden = true;
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
		//验收人
		if(values.PTask_CheckerID){
			entity.CheckerID = values.PTask_CheckerID;
			entity.CheckerName = values.PTask_CheckerName;
		}
		//一审人
		if(values.PTask_OneAuditID){
			entity.OneAuditID = values.PTask_OneAuditID;
			entity.OneAuditName = values.PTask_OneAuditName;
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
		// 任务分类 @author liangyl  @version 2017-07-13
		if(values.PTask_PClassID){
			entity.PClassID = values.PTask_PClassID;
			entity.PClassName = values.PTask_PClassName;
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
			
			'PTypeID','TypeID','ExecutType_Id','Urgency_Id',
			'PTypeName','TypeName','ExecutTypeName','UrgencyName',
			
			'Status_Id','StatusName',
			'OneAuditID','OneAuditName',
			'PClassID','PClassName',
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
			PTask_MTypeID:me.TaskMTypeId,
			PTask_MTypeName:me.TaskMTypeName,
			PTask_CheckerID:USERID,
			PTask_CheckerName:USERNAME
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
			//暂存
			var isTemporary = JShell.WFM.GUID.TaskStatus.Temporary.GUID == values.PTask_Status_Id;
			//一审退回
			var isOneAuditBack = JShell.WFM.GUID.TaskStatus.OneAuditBack.GUID == values.PTask_Status_Id;
			
			if(values.PTask_Status_Id && !isTemporary && !isOneAuditBack){
				JShell.Msg.error('当前的任务状态错误，请刷新任务列表再进行操作！');
				return;
			}
			
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.Apply.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.Apply.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.Apply.GUID;
			
			//新增服务地址，带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_PTaskAdd';
    		
			me.getManager(function(){
				me.onSaveClick();
			});
		}else{//暂存
			me.getForm().setValues({
				PTask_Status_CName:JShell.WFM.GUID.TaskStatus.Temporary.text,
				PTask_Status_Id:JShell.WFM.GUID.TaskStatus.Temporary.GUID
			});
			me.STATUS_ID = JShell.WFM.GUID.TaskStatus.Temporary.GUID;
			
			//新增服务地址，不带推送的新增服务
    		me.addUrl = '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTask';
    		
			me.onSaveClick();
		}
	},
	/**获取直接上级*/
	getManager:function(callback){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectEmpUrl);
		url += '&fields=HREmployee_ManagerID,HREmployee_ManagerName&id=' + 
			JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.HREmployee_ManagerID){
					me.getForm().setValues({
						PTask_OneAuditID:data.value.HREmployee_ManagerID,
						PTask_OneAuditName:data.value.HREmployee_ManagerName
					});
					callback();
				}else{
					JShell.Msg.error('没有找到该用户的直接领导，不能申请任务!');
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});