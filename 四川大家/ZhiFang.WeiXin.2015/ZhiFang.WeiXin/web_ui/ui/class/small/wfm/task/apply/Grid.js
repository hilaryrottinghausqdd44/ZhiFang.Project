/**
 * 我申请的任务
 * @author Jcall
 * @version 2016-09-18
 */
Ext.define('Shell.class.small.wfm.task.apply.Grid',{
    extend: 'Shell.class.small.basic.Grid',
    
    title:'我申请的任务',
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'PTask_DataAddTime', direction: 'ASC' }],
	
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
				if(isTemporary || isOneAuditBack){
					me.openEditForm(id);
				}else{
					me.openShowForm(id);
				}
			}
		});
	},
  	initComponent:function(){
		var me = this;
		
		me.defaultWhere = "ptask.IsUse=1";
		
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere += " and ptask.ApplyID=" + userId;
		
		//已验收、已终止的数据不显示
		me.defaultWhere += " and ptask.Status.Id <>" + JShell.WFM.GUID.TaskStatus.CheckOver.GUID;
		me.defaultWhere += " and ptask.Status.Id <>" + JShell.WFM.GUID.TaskStatus.IsStop.GUID;
		
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
			text:'状态',dataIndex:'PTask_StatusName',width:70,
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
		JShell.Win.open('Shell.class.wfm.task.apply.EditPanel', {
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
	/**显示任务信息*/
	openShowForm:function(id){
		JShell.Win.open('Shell.class.wfm.task.basic.ShowTabPanel', {
			//resizable: false,
			TaskId:id//任务ID
		}).show();
	}
});