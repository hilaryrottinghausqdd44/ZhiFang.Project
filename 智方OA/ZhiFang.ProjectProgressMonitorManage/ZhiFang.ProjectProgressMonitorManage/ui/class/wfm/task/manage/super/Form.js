/**
 * 任务详情管理
 * @author Jcall
 * @version 2018-08-03
 */
Ext.define('Shell.class.wfm.task.manage.super.Form',{
	extend:'Shell.class.wfm.task.basic.Form',
    title:'任务修改页面',
    width:790,
	height:370,
	/**基础修改服务地址*/
    basicEditUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskByField',
	/**每个组件的默认属性*/
    defaults:{
        labelWidth:85,
        width:240,//200,
        labelAlign:'right'
    },
	
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
    initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems = ['->',{
			text:'保存修改内容',
			iconCls:'button-save',
			tooltip:'保存修改内容',
			handler:function(){
				//保存临时存储的内容
				me.onUpdateInfo();
			}
		},'reset'];
		
		me.callParent(arguments);
	},
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		//信息行
		me.InfoLabel.colspan = 3;
		me.InfoLabel.style = "text-align:center;";
		me.InfoLabel.width = me.defaults.width * me.InfoLabel.colspan;
		items.push(me.InfoLabel);
		
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
		//任务分类
	    me.PTask_PClassName.colspan = 1;
	    me.PTask_PClassName.emptyText = '';
	    me.PTask_PClassName.allowBlank = true;
		items.push(me.PTask_PClassName);
		//任务进度  
		me.PTask_Pace_CName.colspan = 1;
		items.push(me.PTask_Pace_CName);
		
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
		
		//每个环节时间
		items.push(
			{fieldLabel:'创建时间',name:'PTask_DataAddTime',colspan:1},
			{fieldLabel:'申请时间',name:'PTask_ApplyDataTime',colspan:1},
			{fieldLabel:'一审时间',name:'PTask_OneAuditDataTime',colspan:1},
			{fieldLabel:'二审时间',name:'PTask_TwoAuditDataTime',colspan:1},
			{fieldLabel:'分配时间',name:'PTask_PublisherDataTime',colspan:1},
			{fieldLabel:'验收时间',name:'PTask_CheckerDataTime',colspan:1}
		);
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Id:values.PTask_Id,//主键ID
			CName:values.PTask_CName,//标题
			Contents:values.PTask_Contents.replace(/\\/g,'&#92'),//内容
			ReqEndTime:JShell.Date.toServerDate(values.PTask_ReqEndTime),//要求完成时间
			
			EstiStartTime:JShell.Date.toServerDate(values.PTask_EstiStartTime),
			EstiEndTime:JShell.Date.toServerDate(values.PTask_EstiEndTime),
			EstiWorkload:values.PTask_EstiWorkload,
			
			StartTime:JShell.Date.toServerDate(values.PTask_StartTime),
			EndTime:JShell.Date.toServerDate(values.PTask_EndTime),
			Workload:values.PTask_Workload
		};
		
		//任务类别
		if(values.PTask_TypeID){
			entity.TypeID = values.PTask_TypeID;
			entity.TypeName = values.PTask_TypeName;
		}
		//任务大类
		if(values.PTask_PTypeID){
			entity.PTypeID = values.PTask_PTypeID;
			entity.PTypeName = values.PTask_PTypeName;
		}
		
		//客户选择
		if(values.PTask_PClient_Id){
			entity.PClient= {
				Id:values.PTask_PClient_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
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
		
		//任务分类    @author liangyl @version 2017-07-13
		if(values.PTask_PClassID){
			entity.PClassID = values.PTask_PClassID;
			entity.PClassName = values.PTask_PClassName;
		}
		//任务进度
		if(values.PTask_Pace_Id){
			entity.Pace = {
				Id:values.PTask_Pace_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
			entity.PaceName = values.PTask_Pace_CName;
		}
		
		//每个环节时间 
		entity.DataAddTime = JShell.Date.toServerDate(values.PTask_DataAddTime);
		entity.ApplyDataTime = JShell.Date.toServerDate(values.PTask_ApplyDataTime);
		entity.OneAuditDataTime = JShell.Date.toServerDate(values.PTask_OneAuditDataTime);
		entity.TwoAuditDataTime = JShell.Date.toServerDate(values.PTask_TwoAuditDataTime);
		entity.PublisherDataTime = JShell.Date.toServerDate(values.PTask_PublisherDataTime);
		entity.CheckerDataTime = JShell.Date.toServerDate(values.PTask_CheckerDataTime);
		
		var fields = [
			'CName','Contents','ReqEndTime',
			
			'PTypeID','TypeID','ExecutType_Id','Urgency_Id',
			'PTypeName','TypeName','ExecutTypeName','UrgencyName',
			
			'EstiStartTime','EstiEndTime','EstiWorkload',
			'StartTime','EndTime','Workload',
			
			'Id','PClient_Id',
			'PClassID','PClassName',
			'Pace_Id','PaceName',
			
			'DataAddTime','ApplyDataTime','OneAuditDataTime',
			'TwoAuditDataTime','PublisherDataTime','CheckerDataTime'
		];
		
		return {
			entity:entity,
			fields:fields.join(',')
		};
	},
	/**保存临时存储的内容*/
	onUpdateInfo:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.basicEditUrl
		
		if(!me.getForm().isValid()) return;
		
		var params = Ext.JSON.encode(me.getEditParams());
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});