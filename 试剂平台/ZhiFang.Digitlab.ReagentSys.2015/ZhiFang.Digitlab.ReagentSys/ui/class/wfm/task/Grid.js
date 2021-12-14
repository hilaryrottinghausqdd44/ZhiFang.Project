/**
 * 任务列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.Grid',{
    extend: 'Shell.ux.model.ExtraGrid',
	requires: ['Ext.ux.CheckColumn'],
    
    title:'任务列表',
    
    /**是否使用字段*/
	IsUseField:'FTask_IsUse',
	/**其他信息模板路径*/
	OtherMsgModelUrl:'',
	/**信息字段*/
	MsgField:'OtherMsg',
	
  	/**获取数据服务路径*/
	selectUrl:'/BaseService.svc/ST_UDTO_SearchFTaskByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/BaseService.svc/ST_UDTO_UpdateFTaskByField',
	/**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelFTask',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'FTask_DataAddTime', direction: 'ASC' }],
  	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'项目名称',dataIndex:'FTask_ProjectName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'任务名称',dataIndex:'FTask_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行人',dataIndex:'FTask_Executor_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'任务状态',dataIndex:'FTask_Status_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'任务进度',dataIndex:'FTask_Pace_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'FTask_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'创建时间',dataIndex:'FTask_DataAddTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'主键ID',dataIndex:'FTask_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});