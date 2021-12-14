/**
 * 子任务列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.publisher.children.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'子任务列表',
    
    /**获取任务类型服务地址*/
    getTaskTypeUrl:'/ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true',
	
    /**父任务ID*/
	ParentTaskId:null,
	/**父任务名称*/
	ParentTaskName:null,
    
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//暂存
				var isTemporary = JShell.WFM.GUID.TaskStatus.Temporary.GUID == record.get('PTask_Status_Id');
				//一审退回
				var isOneAuditBack = JShell.WFM.GUID.TaskStatus.OneAuditBack.GUID == record.get('PTask_Status_Id');
				//分配中
				var isPublisherIng = JShell.WFM.GUID.TaskStatus.PublisherIng.GUID == record.get('PTask_Status_Id');
				//不执行
				var isNoExecute = JShell.WFM.GUID.TaskStatus.NoExecute.GUID == record.get('PTask_Status_Id');
				
				if(isTemporary || isOneAuditBack){
					me.openEditForm(id);
				}else if(isPublisherIng || isNoExecute){
					me.openEditPublisherForm(id);
				}else{
					me.openShowForm(id);
				}
			}
		});
		
		me.initAddButtons();
	},
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = 'ptask.PTaskID=' + me.ParentTaskId;
		
		me.buttonToolbarItems = [{
			xtype:'button',
			iconCls:'button-add',
			text:'任务分配',
			tooltip:'任务分配',
			handler:function(but){
				me.onAddPublisherTask();
			}
		}];
		
		me.callParent(arguments);
	},
	initAddButtons:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.getTaskTypeUrl);
		
		url += '&fields=BDictTree_Id,BDictTree_CName';
		url += '&where=bdicttree.IsUse=1 and bdicttree.ParentID=' + JShell.WFM.GUID.DictTree.TaskType.GUID;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.onAddButtons((data.value || {}).list);
			}
		});
	},
	onAddButtons:function(list){
		var me = this,
			arr = list || [],
			len = arr.length,
			items = [];
			
		for(var i=0;i<len;i++){
			var obj = list[i];
			items.push({
				iconCls:'button-add',
				tooltip:'新增"' + obj.BDictTree_CName + '"类型的任务',
				text:obj.BDictTree_CName,
				TaskMTypeId:obj.BDictTree_Id,
				TaskMTypeName:obj.BDictTree_CName,
				handler:function(but){
					me.onAddClick(but.TaskMTypeId,but.TaskMTypeName);
				}
			});
		}
		
		if(items.length > 0){
			var buttonsToolbar = me.getComponent('buttonsToolbar');
			buttonsToolbar.insert(1,['-',{
				xtype:'button',
				iconCls:'button-add',
				text:'任务申请',
				tooltip:'任务申请',
				menu:items
			}]);
		}
	},
	/**新增申请任务*/
	onAddClick:function(TaskMTypeId,TaskMTypeName){
		var me = this;
		
		var config = {
			resizable:false,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		};
		
		if(TaskMTypeId){
			config.title = '【' + TaskMTypeName + '】任务信息新增页面';
			config.FormConfig = {
				TaskMTypeId:TaskMTypeId,//任务主类别ID
				TaskMTypeName:TaskMTypeName//任务主类别名称
			};
		}else{
			config.title = '任务分配';
		}
		config.FormConfig.ParentTaskId = me.ParentTaskId;//父任务ID
		config.FormConfig.ParentTaskName = me.ParentTaskName;//父任务名称
		
		JShell.Win.open('Shell.class.wfm.task.publisher.children.AddPanel',{
			resizable:false,
			title:'【' + TaskMTypeName + '】任务信息新增页面',
			isApplyTask:true,//是否是申请任务
			width:670,
			height:370,
			FormConfig:{
				TaskMTypeId:TaskMTypeId,//任务主类别ID
				TaskMTypeName:TaskMTypeName,//任务主类别名称
				ParentTaskId:me.ParentTaskId,//父任务ID
				ParentTaskName:me.ParentTaskName,//父任务名称
			},
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**新增分配任务*/
	onAddPublisherTask:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.wfm.task.publisher.children.AddPanel',{
			resizable:false,
			title:'任务分配',
    		isApplyTask:false,//是否是申请任务
			FormConfig:{
				ParentTaskId:me.ParentTaskId,//父任务ID
				ParentTaskName:me.ParentTaskName,//父任务名称
			},
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**修改任务*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.publisher.children.EditPanel', {
			//resizable: false,
			isApplyTask:true,//是否是申请任务
			TaskId:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**修改任务*/
	openEditPublisherForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.publisher.children.EditPanel', {
			//resizable: false,
			isApplyTask:false,//是否是申请任务
			TaskId:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});