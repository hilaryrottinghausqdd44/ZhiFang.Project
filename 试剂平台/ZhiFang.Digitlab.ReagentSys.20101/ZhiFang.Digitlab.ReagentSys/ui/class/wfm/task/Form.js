/**
 * 任务信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.Form',{
	extend:'Shell.ux.model.ExtraForm',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'任务信息',
    
    /**其他信息模板地址*/
	OtherMsgUrl:null,
    
    /**项目表主体名*/
    PrimaryName:null,
    /**项目表数据ID*/
	PrimaryID:null,
	/**项目名称*/
	ProjectName:null,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchFTaskById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddFTask',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateFTaskByField', 
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		
		me.on({
			load:function(p,data){
				if(data.success){
					me.OtherMsgContent = data.value.FTask_OtherMsg;
					me.changeOtherMsg();
				}else{
					me.getComponent('OtherMsg').hide();
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//任务名称
		items.push({
			fieldLabel:'任务名称',name:'FTask_CName',
			emptyText:'必填项',allowBlank:false
		});
		
		//任务类别
		items.push({
			fieldLabel:'任务类别',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Type_CName',
			itemId:'FTask_Type_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'任务类别选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskType + "'"
			}
		},{
			fieldLabel:'任务类别主键ID',hidden:true,
			name:'FTask_Type_Id',
			itemId:'FTask_Type_Id'
		});
		
		//指派人
		items.push({
			fieldLabel:'指派人',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Publisher_CName',
			itemId:'FTask_Publisher_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'指派人主键ID',hidden:true,
			name:'FTask_Publisher_Id',
			itemId:'FTask_Publisher_Id'
		},{
			fieldLabel:'指派人时间戳',hidden:true,
			name:'FTask_Publisher_DataTimeStamp',
			itemId:'FTask_Publisher_DataTimeStamp'
		});
		//执行人
		items.push({
			fieldLabel:'执行人',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Executor_CName',
			itemId:'FTask_Executor_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'执行人主键ID',hidden:true,
			name:'FTask_Executor_Id',
			itemId:'FTask_Executor_Id'
		},{
			fieldLabel:'执行人时间戳',hidden:true,
			name:'FTask_Executor_DataTimeStamp',
			itemId:'FTask_Executor_DataTimeStamp'
		});
		//检查人
		items.push({
			fieldLabel:'检查人',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Checker_CName',
			itemId:'FTask_Checker_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'检查人主键ID',hidden:true,
			name:'FTask_Checker_Id',
			itemId:'FTask_Checker_Id'
		},{
			fieldLabel:'检查人时间戳',hidden:true,
			name:'FTask_Checker_DataTimeStamp',
			itemId:'FTask_Checker_DataTimeStamp'
		});
		
		//执行方式
		items.push({
			fieldLabel:'执行方式',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_ExecutType_CName',
			itemId:'FTask_ExecutType_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'执行方式选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskExecutType + "'"
			}
		},{
			fieldLabel:'执行方式主键ID',hidden:true,
			name:'FTask_ExecutType_Id',
			itemId:'FTask_ExecutType_Id'
		});
		//执行地点
		items.push({
			fieldLabel:'执行地点',name:'FTask_ExecutAddr'
		});
		//紧急程度
		items.push({
			fieldLabel:'紧急程度',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Urgency_CName',
			itemId:'FTask_Urgency_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'紧急程度选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.Urgency + "'"
			}
		},{
			fieldLabel:'紧急程度主键ID',hidden:true,
			name:'FTask_Urgency_Id',
			itemId:'FTask_Urgency_Id'
		});
		//任务状态
		items.push({
			fieldLabel:'任务状态',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Status_CName',
			itemId:'FTask_Status_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'任务状态选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskStatus + "'"
			}
		},{
			fieldLabel:'任务状态主键ID',hidden:true,
			name:'FTask_Status_Id',
			itemId:'FTask_Status_Id'
		});
		//任务进度
		items.push({
			fieldLabel:'任务进度',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_Pace_CName',
			itemId:'FTask_Pace_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'任务进度选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskPace + "'"
			}
		},{
			fieldLabel:'任务进度主键ID',hidden:true,
			name:'FTask_Pace_Id',
			itemId:'FTask_Pace_Id'
		});
		
		//计划开始时间
		items.push({
			fieldLabel:'计划开始时间',name:'FTask_EstiStartTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//计划完成时间
		items.push({
			fieldLabel:'计划完成时间',name:'FTask_EstiEndTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//预计工作量
		items.push({
			fieldLabel:'预计工作量',name:'FTask_EstiWorkload',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		
		//实际开始时间
		items.push({
			fieldLabel:'实际开始时间',name:'FTask_StartTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//实际完成时间
		items.push({
			fieldLabel:'实际完成时间',name:'FTask_EndTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//实际工作量
		items.push({
			fieldLabel:'实际工作量',name:'FTask_Workload',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		
		//协作评估
		items.push({
			fieldLabel:'协作评估',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_TeamworkEval_CName',
			itemId:'FTask_TeamworkEval_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'协作评估选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskEval + "'"
			}
		},{
			fieldLabel:'协作评估主键ID',hidden:true,
			name:'FTask_TeamworkEval_Id',
			itemId:'FTask_TeamworkEval_Id'
		});
		//进度评估
		items.push({
			fieldLabel:'进度评估',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_PaceEval_CName',
			itemId:'FTask_PaceEval_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'进度评估选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskEval + "'"
			}
		},{
			fieldLabel:'进度评估主键ID',hidden:true,
			name:'FTask_PaceEval_Id',
			itemId:'FTask_PaceEval_Id'
		});
		//总体评估
		items.push({
			fieldLabel:'总体评估',
			//emptyText:'必填项',allowBlank:false,
			name:'FTask_EfficiencyEval_CName',
			itemId:'FTask_EfficiencyEval_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'总体评估选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.TaskEval + "'"
			}
		},{
			fieldLabel:'总体评估主键ID',hidden:true,
			name:'FTask_EfficiencyEval_Id',
			itemId:'FTask_EfficiencyEval_Id'
		});
		
		
		items.push({
			fieldLabel:'备注',height:85,
			name:'FTask_Memo',xtype:'textarea'
		},{
			boxLabel:'是否使用',name:'FTask_IsUse',
			xtype:'checkbox',checked:true
		},{
			fieldLabel:'主键ID',name:'FTask_Id',hidden:true
		},{
			fieldLabel:'附加信息',name:'FTask_ExtraMsg',hidden:true
		},{
			fieldLabel:'其他信息',name:'FTask_OtherMsg',hidden:true
		});
		
		//其他信息
		items.push({
			xtype:'button',
			itemId:'OtherMsg',
			text:'其他信息',
			hidden:true,
			handler:function(){
				me.openMsgForm('OtherMsg',me.OtherMsgContent);
			}
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.FTask_CName,
			
			ExecutAddr:values.FTask_ExecutAddr,
			
			EstiStartTime:JShell.Date.toServerDate(values.FTask_EstiStartTime),
			EstiEndTime:JShell.Date.toServerDate(values.FTask_EstiEndTime),
			EstiWorkload:values.FTask_EstiWorkload,
			
			StartTime:JShell.Date.toServerDate(values.FTask_StartTime),
			EndTime:JShell.Date.toServerDate(values.FTask_EndTime),
			Workload:values.FTask_Workload,
			
			IsUse:values.FTask_IsUse ? true : false,
			Memo:values.FTask_Memo,
			
			/**项目表主体名*/
		    PrimaryName:me.PrimaryName,
		    /**项目表数据ID*/
			PrimaryID:me.PrimaryID,
			/**项目名称*/
			ProjectName:me.ProjectName
		};
		
		if(values.FTask_Type_Id){
			entity.Type = {Id:values.FTask_Type_Id};
		}
		if(values.FTask_Publisher_Id){
			entity.Publisher = {
				Id:values.FTask_Publisher_Id,
				DataTimeStamp:values.FTask_Publisher_DataTimeStamp.split(',')
			};
		}
		if(values.FTask_Executor_Id){
			entity.Executor = {
				Id:values.FTask_Executor_Id,
				DataTimeStamp:values.FTask_Executor_DataTimeStamp.split(',')
			};
		}
		if(values.FTask_Checker_Id){
			entity.Checker = {
				Id:values.FTask_Checker_Id,
				DataTimeStamp:values.FTask_Checker_DataTimeStamp.split(',')
			};
		}
		if(values.FTask_ExecutType_Id){
			entity.ExecutType = {Id:values.FTask_ExecutType_Id};
		}
		if(values.FTask_Status_Id){
			entity.Status = {Id:values.FTask_Status_Id};
		}
		if(values.FTask_Pace_Id){
			entity.Pace = {Id:values.FTask_Pace_Id};
		}
		if(values.FTask_TeamworkEval_Id){
			entity.TeamworkEval = {Id:values.FTask_TeamworkEval_Id};
		}
		if(values.FTask_PaceEval_Id){
			entity.PaceEval = {Id:values.FTask_PaceEval_Id};
		}
		if(values.FTask_EfficiencyEval_Id){
			entity.EfficiencyEval = {Id:values.FTask_EfficiencyEval_Id};
		}
		if(values.FTask_Urgency_Id){
			entity.Urgency = {Id:values.FTask_Urgency_Id};
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		var fields = [
			'CName','Type_Id','Publisher_Id','Executor_Id','Checker_Id',
			'ExecutType_Id','ExecutAddr','Status_Id','Pace_Id','Urgency_Id',
			'EstiStartTime','EstiEndTime','EstiWorkload',
			'StartTime','EndTime','Workload',
			'TeamworkEval_Id','PaceEval_Id','EfficiencyEval_Id',
			'IsUse','Memo,Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.FTask_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.FTask_EstiStartTime = JShell.Date.getDate(data.FTask_EstiStartTime);
		data.FTask_EstiEndTime = JShell.Date.getDate(data.FTask_EstiEndTime);
		data.FTask_StartTime = JShell.Date.getDate(data.FTask_StartTime);
		data.FTask_EndTime = JShell.Date.getDate(data.FTask_EndTime);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//字典监听
		var dictList = ['Type','ExecutType','Status','Pace',
			'Urgency','TeamworkEval','PaceEval','EfficiencyEval'];
		
		for(var i=0;i<dictList.length;i++){
			me.doDictListeners(dictList[i]);
		}
		
		//员工监听
		var dictList = ['Publisher','Executor','Checker'];
		
		for(var i=0;i<dictList.length;i++){
			me.doUserListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners:function(name){
		var me = this;
		var CName = me.getComponent('FTask_' + name + '_CName');
		var Id = me.getComponent('FTask_' + name + '_Id');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('FDict_CName') : '');
				Id.setValue(record ? record.get('FDict_Id') : '');
				p.close();
			}
		});
	},
	/**员工监听*/
	doUserListeners:function(name){
		var me = this;
		var CName = me.getComponent('FTask_' + name + '_CName');
		var Id = me.getComponent('FTask_' + name + '_Id');
		var DataTimeStamp = me.getComponent('FTask_' + name + '_DataTimeStamp');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('HREmployee_CName') : '');
				Id.setValue(record ? record.get('HREmployee_Id') : '');
				DataTimeStamp.setValue(record ? record.get('HREmployee_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	afterSaveOtherMsg:function(){
		var me = this;
		me.changeOtherMsg();
	},
	changeOtherMsg:function(){
		var me = this;
		var OtherMsg = me.getComponent('OtherMsg');
		
		var msg = me.OtherMsgContent ? 
			'<b style="color:green">(存在)</b>' : '<b style="color:red">(无)</b>';
		OtherMsg.setText('其他信息' + msg);
		OtherMsg.show();
	}
});