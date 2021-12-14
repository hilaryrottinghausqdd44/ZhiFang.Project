/**
 * 平台客户级别选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.serviceclient.level.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'平台客户级别选择列表',
    width:270,
    height:300,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchSServiceClientlevelByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'sserviceclientlevel.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['sserviceclientlevel.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'名称',dataIndex:'SServiceClientlevel_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'SServiceClientlevel_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'SServiceClientlevel_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});