/**
 * 审批状态选择
 * @author liangyl	
 * @version 2017-01-24
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.basic.ApproveStatusCheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'审批状态选择列表',
    width:300,
    height:280,
    /**获取数据服务路径*/
	selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchATApproveStatusByHQL?isPlanish=true',
    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'ATApproveStatus_Name',
		direction: 'ASC'
	}],
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'atapprovestatus.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['atapprovestatus.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'名称',dataIndex:'ATApproveStatus_Name',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'ATApproveStatus_Shortcode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'ATApproveStatus_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'ATApproveStatus_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	}
});