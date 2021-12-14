/**
 * 职务选择列表
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.position.CheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'职务选择列表',
    width:550,
    height:350,
	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHRPositionByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'hrposition.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:420,emptyText:'职务名称',isLike:true,
			fields:['hrposition.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'职务名称',dataIndex:'HRPosition_CName',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'代码',dataIndex:'HRPosition_UseCode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'HRPosition_Shortcode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'HRPosition_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'HRPosition_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
	
	}
});