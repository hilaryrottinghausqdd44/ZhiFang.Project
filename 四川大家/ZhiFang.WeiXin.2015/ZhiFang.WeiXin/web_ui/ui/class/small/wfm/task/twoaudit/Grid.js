/**
 * 我二审的任务
 * @author Jcall
 * @version 2016-09-18
 */
Ext.define('Shell.class.small.wfm.task.twoaudit.Grid',{
    extend: 'Shell.class.small.basic.Grid',
    
    title:'我二审的任务',
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskByHQL?isPlanish=true',
	/**获取任务类型人员服务*/
    getPTaskTypeEmpLinkUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkByHQL',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'PTask_DataAddTime', direction: 'ASC' }],
	
	/**是否默认触发boxready事件*/
	defaultFireBoxready:false,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.on({
			boxready:function(){
				//加载符合的任务类型数据
				me.loadTaskTypeData();
			}
		});
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
					me.openEditForm(id);
					
//					//二审人存在+二审人不是自己的数据只能看
//					var TwoAuditID = record.get('PTask_TwoAuditID');//二审人ID
//					var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
//					if(TwoAuditID && TwoAuditID != userId){
//					    me.openShowForm(id);
//					}else{
//						me.openEditForm(id);
//					}
				}else{
					me.openShowForm(id);
				}
			}
		});
	},
	
  	initComponent:function(){
		var me = this;
		
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'任务类型',dataIndex:'PTask_TypeName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'任务名称',dataIndex:'PTask_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'一审人',dataIndex:'PTask_OneAuditName',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'任务进度',dataIndex:'PTask_PaceName',width:120,resizable:false,
			sortable:false,menuDisabled:true,renderer:function(value,meta){
				value = value || '0%';
				var templet = 
	                '<div class="progress progress-mini" style="float:left;width:67%;height:6px;margin:0;margin-top:3px;">'+
	                    '<div style="width: {PaceName};" class="progress-bar"></div>'+
	                '</div><div style="float:left;width:33%;">&nbsp;{PaceName}</div>';
	                
	            var v = templet.replace(/{PaceName}/g,value);
				return v;
			}
		},{
			text:'要求完成时间',dataIndex:'PTask_ReqEndTime',width:85,isDate:true
		},{
			text:'主键ID',dataIndex:'PTask_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'任务状态主键ID',dataIndex:'PTask_Status_Id',hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**查看处理面板*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.twoaudit.EditPanel', {
			//resizable: false,
			TaskId:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**加载符合的任务类型数据*/
	loadTaskTypeData:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.getPTaskTypeEmpLinkUrl);
			
		var fields = [
			'PTaskTypeEmpLink_Id',
			'PTaskTypeEmpLink_TaskTypeID',
			'PTaskTypeEmpLink_TwoAudit'
		];
		
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		
		url += '?isPlanish=true&fields=' + fields.join(',');
		url += '&where=ptasktypeemplink.TwoAudit=1 and ptasktypeemplink.EmpID=' + userId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				var ids = [];
				if(data.value && data.value.list){
					var list = data.value.list,
						len = list.length;
						
					for(var i=0;i<len;i++){
						ids.push(list[i].PTaskTypeEmpLink_TaskTypeID);
					}
				}
				if(ids.length > 0){
					me.onLoadTaskTypeData(ids);
				}else{
					var error = me.errorFormat.replace(/{msg}/, '该用户没有分配任何任务类型的二审权限！');
					me.getView().update(error);
				}
			}else{
				var error = me.errorFormat.replace(/{msg}/, data.msg);
				me.getView().update(error);
			}
		});
	},
	/***/
	onLoadTaskTypeData:function(ids){
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		
		//(二审人=用户&&状态为一审通过、二审中、分配退回) or (二审人为空 and 任务状态=一审通过 and 任务类型 in (用户能二审的任务类型))
		me.defaultWhere = 
		"ptask.IsUse=1 and (" +
			"(" +
				"ptask.TwoAuditID=" + userId + " and " +
				"ptask.Status.Id in(" +
					JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID + "," +
					JShell.WFM.GUID.TaskStatus.TwoAuditIng.GUID + "," +
					JShell.WFM.GUID.TaskStatus.PublisherBack.GUID +
				")" +
			") or " +
			"(ptask.TwoAuditID is null and " +
				"ptask.Status.Id=" + JShell.WFM.GUID.TaskStatus.OneAuditOver.GUID + " and " +
				"ptask.PTypeID in(" + ids.join(',') + ")" +
			")" +
		")";
		
		me.onSearch();
	},
	/**显示任务信息*/
	openShowForm:function(id){
		JShell.Win.open('Shell.class.wfm.task.basic.ShowTabPanel', {
			//resizable: false,
			TaskId:id//任务ID
		}).show();
	}
});