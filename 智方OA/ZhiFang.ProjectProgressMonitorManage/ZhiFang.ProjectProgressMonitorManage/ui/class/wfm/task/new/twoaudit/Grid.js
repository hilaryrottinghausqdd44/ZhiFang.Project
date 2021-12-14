/**
 * 任务二审列表
 * @author liangyl
 * @version 2017-06-09
 */
Ext.define('Shell.class.wfm.task.new.twoaudit.Grid',{
    extend: 'Shell.class.wfm.task.new.basic.Grid',
    
    title:'任务二审列表',
    
    /**获取任务类型人员服务*/
    getPTaskTypeEmpLinkUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkByHQL',
	
	/**默认加载数据*/
	defaultLoad: false,
	
	/**默认员工类型*/
	defaultUserType:'',
    /**是否按部门查询*/
	hasDept:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//一审通过
				var isOneAuditOver = JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID == record.get('PTask_Status_Id');
				//二审中
				var isTwoAuditIng = JShell.WFM.GUID.TaskStatus.TwoAuditIng.GUID == record.get('PTask_Status_Id');
				//分配退回
				var isPublisherBack = JShell.WFM.GUID.TaskStatus.PublisherBack.GUID == record.get('PTask_Status_Id');
				if(isOneAuditOver || isTwoAuditIng || isPublisherBack){
					//me.openEditForm(id);

					//二审人存在+二审人不是自己的数据只能看
					var TwoAuditID = record.get('PTask_TwoAuditID');//二审人ID
					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
					if(TwoAuditID && TwoAuditID != userId){
					    me.openShowForm(id);
					}else{
						me.openEditForm(id);
					}
				}else{
					me.openShowForm(id);
				}
			}
		});
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		
		columns.push({text:'二审人ID',dataIndex:'PTask_TwoAuditID',hidden:true,hideable:false});
		columns.push({text:'任务类别ID',dataIndex:'PTask_PTypeID',hidden:true,hideable:false});
		
		return columns;
	},
	/**加载符合的任务类型数据*/
	loadTaskTypeData:function(ids){
	    var me=this;
	    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
	    //二审人=用户 or (任务状态=一审通过 ) and 任务类型 in (用户能二审的任务类型)
		me.defaultWhere = 
			"ptask.IsUse=1 and (ptask.TwoAuditID=" + userId +
			" or (ptask.Status.Id=" + JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID +"))"+
			" and ptask.TypeID in(" + ids + ")";
		me.onSearch();
	},
	
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.twoaudit.EditPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			//resizable: false,
			TaskId:id,
			listeners: {
				save: function(p,id,checkedBackButton) {
					p.close();
					//me.onSearch();
					if(checkedBackButton){
						me.onSearch();
					}else{
						//打开分配页面
						me.openPublisherWin(id);
					}
				}
			}
		}).show();
	},
	
	/**
	 * 二审完毕判断当前用户是否有分配权限，
	 * 如果没有，关闭二审页面后直接刷新列表，
	 * 如果有，则关闭二审页面后直接打开分配页面，
	 * 分配完毕判断执行人是否是当前用户自己，
	 * 如果不是，关闭分配页面后直接刷新列表，
	 * 如果是，则关闭分配页面后直接打开执行页面
	 */
	/**打开分配页面*/
	openPublisherWin:function(id){
		var me = this,
			record = me.store.findRecord(me.PKField,id),
			typeId = record.get('PTask_PTypeID');
		
		me.canPublishedByTaskTypeId(typeId,function(bo){
			if(bo){
				JShell.Win.open('Shell.class.wfm.task.publisher.EditPanel', {
					SUB_WIN_NO:'2',//内部窗口编号
					//resizable: false,
					TaskId:id,
					listeners: {
						save: function(p,id,checkedBackButton) {
							p.close();
							// me.onSearch();
							if(checkedBackButton){
								me.onSearch();
							}else{
								//打开执行页面
								me.openExecuteWin(id);
							}
						}
					}
				}).show();
			}else{
				me.onSearch();
			}
		});
	},
	/**判断该任务类型是否有分配的权限*/
	canPublishedByTaskTypeId:function(taskTypeID,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.getPTaskTypeEmpLinkUrl);
			
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		
		url += '?isPlanish=true&fields=PTaskTypeEmpLink_Id';
		url += '&where=ptasktypeemplink.Publish=1 and ptasktypeemplink.EmpID=' + userId + 
			' and ptasktypeemplink.TaskTypeID=' + taskTypeID ;
		
		JShell.Server.get(url,function(data){
			var canPublished = null;
			if(data.success){
				if(data.value && data.value.list && data.value.list.length > 0){
					canPublished = true;
				}else{
					canPublished = false;
				}
			}
			callback(canPublished);
		});
	},
	/**打开执行页面*/
	openExecuteWin:function(id){
		var me = this;
		
		me.canExecutedByTaskId(id,function(bo){
			if(bo){
				JShell.Win.open('Shell.class.wfm.task.execute.EditPanel', {
					SUB_WIN_NO:'3',//内部窗口编号
					//resizable: false,
					TaskId:id,
					listeners: {
						save: function(p,id) {
							p.close();
							me.onSearch();
						}
					}
				}).show();
			}else{
				me.onSearch();
			}
		});
	},
	/**判断该任务是否有执行的权限*/
	canExecutedByTaskId:function(taskId,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
			
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		
		url += '&fields=PTaskTypeEmpLink_Id';
		url += '&where=ptask.Id=' + taskId + ' and ptask.ExecutorID=' + userId;
		
		JShell.Server.get(url,function(data){
			var canExecuted = null;
			if(data.success){
				if(data.value && data.value.list && data.value.list.length > 0){
					canExecuted = true;
				}else{
					canExecuted = false;
				}
			}
			callback(canExecuted);
		});
	}
});