/**
 * 任务分配列表
 * @author liangyl
 * @version 2017-06-09
 */
Ext.define('Shell.class.wfm.task.new.publisher.Grid',{
    extend: 'Shell.class.wfm.task.new.basic.Grid',
    
    title:'任务分配列表',
    
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
				var name = record.get('PTask_CName');
				//二审通过
				var isTwoAuditOver = JShell.WFM.GUID.TaskStatus.TwoAuditOver.GUID == record.get('PTask_Status_Id');
				//分配中
				var isPublisherIng = JShell.WFM.GUID.TaskStatus.PublisherIng.GUID == record.get('PTask_Status_Id');
				//不执行
				var isNoExecute = JShell.WFM.GUID.TaskStatus.NoExecute.GUID == record.get('PTask_Status_Id');
				
				if(isTwoAuditOver || isPublisherIng || isNoExecute){
					//me.openEditForm(id,name);
					
					//分配人存在+分配人不是自己的数据只能看
					var PublisherID = record.get('PTask_PublisherID');//分配人ID
					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
					if(PublisherID && PublisherID != userId){
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
	
	
	/**加载符合的任务类型数据*/
	loadTaskTypeData:function(ids){
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//分配人=用户 or (分配人为空 and 任务状态=二审通过 ) and 任务类型 in (用户能选择的任务类型)
		me.defaultWhere = 
			"ptask.IsUse=1 and (ptask.PublisherID=" + userId +
			" or (ptask.Status.Id=" + JShell.WFM.GUID.TaskStatus.TwoAuditOver.GUID + "))"+
			" and ptask.TypeID in(" + ids + ")";
			
		me.onSearch();
	},
	openEditForm:function(id,name){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.publisher.EditPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			//resizable: false,
			TaskId:id,//任务ID
			TaskName:name,//任务名称
			listeners: {
				save: function(p,id,checkedBackButton) {
					p.close();
					//me.onSearch();
					if(checkedBackButton){
						me.onSearch();
					}else{
						//打开分配页面
						me.openExecuteWin(id);
					}
				}
			}
		}).show();
	},
	
	/**
	 * 分配完毕判断执行人是否是当前用户自己，
	 * 如果不是，关闭分配页面后直接刷新列表，
	 * 如果是，则关闭分配页面后直接打开执行页面
	 */
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