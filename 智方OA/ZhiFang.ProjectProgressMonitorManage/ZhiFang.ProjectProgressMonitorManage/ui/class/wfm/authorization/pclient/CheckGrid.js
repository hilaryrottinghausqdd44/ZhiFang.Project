/**
 * 客户选择列表
 * @author longfc
 * @version 2017-01-07
 */
Ext.define('Shell.class.wfm.authorization.pclient.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'客户选择',
    width:410,
    height:400,
    
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'PClient_ProvinceName',
		direction: 'ASC'
	},{
		property: 'PClient_Name',
		direction: 'ASC'
	},{
		property: 'PClient_LicenceCode',
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
		me.defaultWhere += 'pclient.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:"65%",emptyText:'名称/客户服务编号',isLike:true,
			fields:['pclient.Name','pclient.LicenceCode']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'客户名称',dataIndex:'PClient_Name',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'授权名称',dataIndex:'PClient_LicenceClientName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'客户服务编号',dataIndex:'PClient_LicenceCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'PClient_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'PClient_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	}
});