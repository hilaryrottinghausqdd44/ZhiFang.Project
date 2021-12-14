/**
 * 基础任务表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.basic.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'基础任务表单',
    width:790,
	height:400,
    
   /**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskById?isPlanish=true',
    /**新增服务地址*/
    addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_PTaskAdd',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField',
    /**新增任务操作记录服务地址*/
    addPTaskOperLogUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog',
    
	bodyPadding:'20px 20px 10px 20px',
	
    layout:{
        type:'table',
        columns:3//每行有几列
    },
    /**每个组件的默认属性*/
    defaults:{
        labelWidth:85,
        width:240,
        labelAlign:'right'
    },
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:false,
	
    /**任务主类别ID*/
	TaskMTypeId:null,
	/**任务主类别名称*/
	TaskMTypeName:null,
	
	/**操作记录-处理意见*/
    OperMsg:'',
    /**任务分类-字典*/
    TaskClassification:'TaskClassification',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		me.addEvents('aftersave');
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		//创建可见组件
		me.createShowItems();
		//创建隐形组件
		items = items.concat(me.createHideItems());
		
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		
		return items;
	},
	/**创建可见组件*/
	createShowItems:function(){
		var me = this;
		
		//创建人员选择组件
		me.createUserItems();
		//创建评估选择组件
		me.createEvalItems();
		//创建任务其他选择组件
		me.createOtherItems();
		//创建时间+工作量组件
		me.createDateAndWorklogItems();
		
		me.InfoLabel = {xtype:'displayfield',name:'InfoLabel',margin:'0 0 10px 0'};//信息行
		me.PTask_CName = {fieldLabel:'任务名称',name:'PTask_CName',emptyText:'必填项',allowBlank:false};
		//me.PTask_Contents = {fieldLabel:'任务内容',name:'PTask_Contents',xtype:'textarea',grow:true,minHeight:80};
		me.PTask_Contents = {
			fieldLabel:'任务内容',name:'PTask_Contents',//resizable:true,resizeHandles:'s',
			minHeight:160,style:{marginBottom:'10px'},xtype:'textarea'//xtype:'htmleditor'
		};
		me.PTask_ExecutAddr = {fieldLabel:'执行地点',name:'PTask_ExecutAddr'};
		me.PTask_IsUse = {boxLabel:'是否使用',name:'PTask_IsUse',xtype:'checkbox',checked:true,style:{marginLeft:'30px'}};
	},
	
	/**创建人员选择组件*/
	createUserItems:function(){
		var me = this;
		
//		me.PTask_ApplyName = {
//			fieldLabel:'申请人',name:'PTask_ApplyName',itemId:'PTask_ApplyName',
//			xtype:'displayfield'
//		};
//		me.PTask_OneAuditName = {
//			fieldLabel:'一审人',name:'PTask_OneAuditName',itemId:'PTask_OneAuditName',
//			xtype:'displayfield'
//		};
//		me.PTask_TwoAuditName = {
//			fieldLabel:'二审人',name:'PTask_TwoAuditName',itemId:'PTask_TwoAuditName',
//			xtype:'displayfield'
//		};
//		me.PTask_PublisherName = {
//			fieldLabel:'指派人',name:'PTask_PublisherName',itemId:'PTask_PublisherName',
//			xtype:'displayfield'
//		};
		me.PTask_ExecutorName = {
			fieldLabel:'执行人',name:'PTask_ExecutorName',itemId:'PTask_ExecutorName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp'
		};
//		me.PTask_CheckerName = {
//			fieldLabel:'检查人',name:'PTask_CheckerName',itemId:'PTask_CheckerName',
//			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp'
//		};
	},
	/**创建时间+工作量组件*/
	createDateAndWorklogItems:function(){
		var me = this;
		
		me.PTask_ReqEndTime = {fieldLabel:'要求完成时间',name:'PTask_ReqEndTime',xtype:'datefield',format:'Y-m-d'};
		me.PTask_EstiStartTime = {fieldLabel:'计划开始时间',name:'PTask_EstiStartTime',xtype:'datefield',format:'Y-m-d'};
		me.PTask_EstiEndTime = {fieldLabel:'计划完成时间',name:'PTask_EstiEndTime',xtype:'datefield',format:'Y-m-d'};
		me.PTask_EstiWorkload = {fieldLabel:'预计工作量',name:'PTask_EstiWorkload',xtype:'numberfield',value:0,emptyText:'必填项',allowBlank:false};
		
		me.PTask_StartTime = {fieldLabel:'实际开始时间',name:'PTask_StartTime',xtype:'datefield',format:'Y-m-d'};
		me.PTask_EndTime = {fieldLabel:'实际完成时间',name:'PTask_EndTime',xtype:'datefield',format:'Y-m-d'};
		me.PTask_Workload = {fieldLabel:'实际工作量',name:'PTask_Workload',xtype:'numberfield',value:0,emptyText:'必填项',allowBlank:false};
	},
	/**创建评估选择组件*/
	createEvalItems:function(){
		var me = this;
		//协作评估
		me.PTask_TeamworkEval_CName = {
			fieldLabel:'协作评估',name:'PTask_TeamworkEval_CName',itemId:'PTask_TeamworkEval_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'协作评估选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.TeamworkEval + "'"
			}
		};
		//进度评估
		me.PTask_PaceEval_CName = {
			fieldLabel:'进度评估',name:'PTask_PaceEval_CName',itemId:'PTask_PaceEval_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'进度评估选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.PaceEval + "'"
			}
		};
		//效率评估
		me.PTask_EfficiencyEval_CName = {
			fieldLabel:'效率评估',name:'PTask_EfficiencyEval_CName',itemId:'PTask_EfficiencyEval_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'效率评估选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.EfficiencyEval + "'"
			}
		};
		//质量评估
		me.PTask_QualityEval_CName = {
			fieldLabel:'质量评估',name:'PTask_QualityEval_CName',itemId:'PTask_QualityEval_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'质量评估选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.QualityEval + "'"
			}
		};
		//总体评估
		me.PTask_TotalityEval_CName = {
			fieldLabel:'总体评估',name:'PTask_TotalityEval_CName',itemId:'PTask_TotalityEval_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'总体评估选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.TotalityEval + "'"
			}
		};
	},
	/**创建任务其他选择组件*/
	createOtherItems:function(){
		var me = this;
		
		//任务类别
		me.PTask_TypeName = {
			fieldLabel:'任务类别',name:'PTask_TypeName',itemId:'PTask_TypeName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.task.type.CheckTree',
			emptyText:'必填项',allowBlank:false,
			classConfig:{
				title:'任务类别选择',
				/**是否单选*/
            	checkOne: true,
				rootVisible:false,
				IDS:me.TaskMTypeId
			}
		};
		//执行方式
		me.TaskExecutType = {
			fieldLabel:'执行方式',name:'PTask_ExecutType_CName',itemId:'PTask_ExecutType_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'执行方式选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.TaskExecutType + "'"
			}
		};
		//紧急程度
		me.PTask_Urgency_CName = {
			fieldLabel:'紧急程度',name:'PTask_Urgency_CName',itemId:'PTask_Urgency_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'紧急程度选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.Urgency + "'"
			}
		};
		//任务状态
		me.PTask_Status_CName = {
			fieldLabel:'任务状态',name:'PTask_Status_CName',itemId:'PTask_Status_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'任务状态选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.TaskStatus + "'"
			}
		};
		//任务进度
		me.PTask_Pace_CName = {
			fieldLabel:'任务进度',name:'PTask_Pace_CName',itemId:'PTask_Pace_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'任务进度选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JShell.WFM.DictTypeCode.TaskPace + "'"
			}
		};
		//客户选择
		me.PTask_PClient_Name = {
			fieldLabel:'客户选择',name:'PTask_PClient_Name',itemId:'PTask_PClient_Name',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid'
		};
		
		// 添加任务分类   @author liangyl @version 2017-07-13
		me.PTask_PClassName = {
			fieldLabel:'任务分类',name:'PTask_PClassName',itemId:'PTask_PClassName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',emptyText:'必填项',allowBlank:false,
			classConfig:{
				title:'任务分类选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + me.TaskClassification + "'"
			}
		};
	},
	
	/**创建隐形组件*/
	createHideItems:function(){
		var me = this,
			items = [];
			
		items.push({fieldLabel:'主键ID',hidden:true,name:'PTask_Id'});
		items.push({fieldLabel:'客户主键ID',hidden:true,name:'PTask_PClient_Id',itemId:'PTask_PClient_Id'});
		
		items.push({fieldLabel:'申请人主键ID',hidden:true,name:'PTask_ApplyID',itemId:'PTask_ApplyID'});
		items.push({fieldLabel:'一审人主键ID',hidden:true,name:'PTask_OneAuditID',itemId:'PTask_OneAuditID'});
		items.push({fieldLabel:'二审人主键ID',hidden:true,name:'PTask_TwoAuditID',itemId:'PTask_TwoAuditID'});
		items.push({fieldLabel:'指派人主键ID',hidden:true,name:'PTask_PublisherID',itemId:'PTask_PublisherID'});
		items.push({fieldLabel:'执行人主键ID',hidden:true,name:'PTask_ExecutorID',itemId:'PTask_ExecutorID'});
		items.push({fieldLabel:'检查人主键ID',hidden:true,name:'PTask_CheckerID',itemId:'PTask_CheckerID'});
		
		items.push({fieldLabel:'申请人名称',hidden:true,name:'PTask_ApplyName',itemId:'PTask_ApplyName'});
		items.push({fieldLabel:'一审人名称',hidden:true,name:'PTask_OneAuditName',itemId:'PTask_OneAuditName'});
		items.push({fieldLabel:'二审人名称',hidden:true,name:'PTask_TwoAuditName',itemId:'PTask_TwoAuditName'});
		items.push({fieldLabel:'指派人名称',hidden:true,name:'PTask_PublisherName',itemId:'PTask_PublisherName'});
		//items.push({fieldLabel:'执行人名称',hidden:true,name:'PTask_ExecutorName',itemId:'PTask_ExecutorName'});
		items.push({fieldLabel:'检查人名称',hidden:true,name:'PTask_CheckerName',itemId:'PTask_CheckerName'});
		
		items.push({fieldLabel:'任务主类别主键ID',hidden:true,name:'PTask_MTypeID',itemId:'PTask_MTypeID'});
		items.push({fieldLabel:'任务父类别主键ID',hidden:true,name:'PTask_PTypeID',itemId:'PTask_PTypeID'});
		items.push({fieldLabel:'任务类别主键ID',hidden:true,name:'PTask_TypeID',itemId:'PTask_TypeID'});
		
		items.push({fieldLabel:'任务主类别名称',hidden:true,name:'PTask_MTypeName',itemId:'PTask_MTypeName'});
		items.push({fieldLabel:'任务父类别名称',hidden:true,name:'PTask_PTypeName',itemId:'PTask_PTypeName'});
		
		items.push({fieldLabel:'父任务主键ID',hidden:true,name:'PTask_PTaskID',itemId:'PTask_PTaskID'});
		items.push({fieldLabel:'父任务名称',hidden:true,name:'PTask_PTaskCName',itemId:'PTask_PTaskCName'});

		// 任务分类ID   @author liangyl @version 2017-07-13
		items.push({fieldLabel:'任务分类ID',hidden:true,name:'PTask_PClassID',itemId:'PTask_PClassID'});

		//执行方式+紧急程度+任务状态+任务进度+
		var names = [
			'PTask_ExecutType','PTask_Urgency','PTask_Status','PTask_Pace'
		];
		
		for(var i in names){
			items = items.concat(me.createOneHideItem(names[i]));
		}
		
		return items;
	},
	/**创建一个隐形组件*/
	createOneHideItem:function(name){
		return [{
			fieldLabel:'主键ID',hidden:true,
			name:name+'_Id',itemId:name+'_Id'
		},{
			fieldLabel:'时间戳',hidden:true,
			name:name+'_DataTimeStamp',itemId:name+'_DataTimeStamp'
		}];
	},
	
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//客户选择监听
		var PTask_PClient_Name = me.getComponent('PTask_PClient_Name'),
			PTask_PClient_Id = me.getComponent('PTask_PClient_Id');
		if(PTask_PClient_Name){
			PTask_PClient_Name.on({
				check: function(p, record) {
					PTask_PClient_Name.setValue(record ? record.get('PClient_Name') : '');
					PTask_PClient_Id.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			});
		}
		
		//任务类型选择监听
		var PTask_TypeName = me.getComponent('PTask_TypeName'),
			PTask_TypeID = me.getComponent('PTask_TypeID');
		if(PTask_TypeName){
			PTask_TypeName.on({
				check: function(p, record) {
					//不能选择非叶子节点
					if(!record.data.leaf){
						return false;
					}
					me.changeTaskType(p, record);
				}
			});
		}
		// 任务分类选择监听   @author liangyl @version 2017-07-13
		var PTask_PClassName = me.getComponent('PTask_PClassName'),
			PTask_PClassID = me.getComponent('PTask_PClassID');
		if(PTask_PClassName){
			PTask_PClassName.on({
				check: function(p, record) {
					PTask_PClassName.setValue(record ? record.get('PDict_CName') : '');
					PTask_PClassID.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			});
		}
		
		//字典监听
		var dictList = [
			'ExecutType','Urgency','Status','Pace',
			'TeamworkEval','PaceEval','EfficiencyEval','QualityEval','TotalityEval'
		];
		
		for(var i=0;i<dictList.length;i++){
			me.doDictListeners(dictList[i]);
		}
		
		//员工监听
		var dictList = ['Apply','OneAudit','TwoAudit','Publisher','Executor','Checker'];
		
		for(var i=0;i<dictList.length;i++){
			me.doUserListeners(dictList[i]);
		}
	},
	changeTaskType:function(p,node){
		var me = this,
			PTask_PTypeName = me.getComponent('PTask_PTypeName'),
			PTask_PTypeID = me.getComponent('PTask_PTypeID'),
			PTask_TypeName = me.getComponent('PTask_TypeName'),
			PTask_TypeID = me.getComponent('PTask_TypeID');
		
		PTask_PTypeName.setValue(node.parentNode.data.text);
		PTask_PTypeID.setValue(node.parentNode.data.tid);
		PTask_TypeName.setValue(node.data.text);
		PTask_TypeID.setValue(node.data.tid);
		p.close();
	},
	/**字典监听*/
	doDictListeners:function(name){
		var me = this;
		var CName = me.getComponent('PTask_' + name + '_CName');
		var Id = me.getComponent('PTask_' + name + '_Id');
		var DataTimeStamp = me.getComponent('PTask_' + name + '_DataTimeStamp');
		
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				DataTimeStamp.setValue(record ? record.get('PDict_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**员工监听*/
	doUserListeners:function(name){
		var me = this;
		var CName = me.getComponent('PTask_' + name + 'Name');
		var Id = me.getComponent('PTask_' + name + 'ID');
		
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('HREmployee_CName') : '');
				Id.setValue(record ? record.get('HREmployee_Id') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult:function(data){
		data.PTask_ReqEndTime = JShell.Date.getDate(data.PTask_ReqEndTime);
		data.PTask_EstiStartTime = JShell.Date.getDate(data.PTask_EstiStartTime);
		data.PTask_EstiEndTime = JShell.Date.getDate(data.PTask_EstiEndTime);
		data.PTask_StartTime = JShell.Date.getDate(data.PTask_StartTime);
		data.PTask_EndTime = JShell.Date.getDate(data.PTask_EndTime);
		
		data.InfoLabel = [];
		
		if(data.PTask_ApplyName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>申请人：<b>' + 
				data.PTask_ApplyName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		if(data.PTask_OneAuditName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>一审人：<b>' + 
				data.PTask_OneAuditName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		if(data.PTask_TwoAuditName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>二审人：<b>' + 
				data.PTask_TwoAuditName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		if(data.PTask_PublisherName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>分配人：<b>' + 
				data.PTask_PublisherName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		if(data.PTask_ExecutorName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>执行人：<b>' + 
				data.PTask_ExecutorName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		if(data.PTask_CheckerName){
			data.InfoLabel.push('<span style="color:#e0e0e0;">【</span>检查人：<b>' + 
				data.PTask_CheckerName + '</b><span style="color:#e0e0e0;">】</span>');
		}
		data.InfoLabel = data.InfoLabel.join('&nbsp;');
		
		var me = this;
		me.TaskMTypeId = data.PTask_MTypeID;//任务主类别ID
		me.TaskMTypeName = data.PTask_MTypeName;//任务主类别名称
		
		me.getComponent('PTask_TypeName').changeClassConfig({
			title:'任务类别选择',
			rootVisible:false,
			IDS:me.TaskMTypeId,
			selectId:data.PTask_TypeID
		});

		return data;
	},
	/**更改标题*/
	changeTitle:function(){
		//不做处理
	},
	
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
		
		return items;
	},
	
	/**任务操作记录*/
	onSavePTaskOperLog:function(PTaskID,StatusID,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addPTaskOperLogUrl);
			
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		var entity = {
			PTaskID:PTaskID,
			PTaskOperTypeID:StatusID,
			OperaterID:USERID,
			OperaterName:USERNAME,
			OperateMemo:me.OperMsg ? me.OperMsg.trim() : ''
		};
		var params = Ext.JSON.encode({entity:entity});
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			callback(data);
		});
	}
});