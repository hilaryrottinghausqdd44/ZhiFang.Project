/**
 * 学位选择列表
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.degree.CheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'学位选择列表',
    width:550,
    height:350,
	/**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBDegreeByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bdegree.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:420,emptyText:'学位名称',isLike:true,
			fields:['bdegree.Name']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'学位名称',dataIndex:'BDegree_Name',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BDegree_Shortcode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BDegree_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'BDegree_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
	
	}
});