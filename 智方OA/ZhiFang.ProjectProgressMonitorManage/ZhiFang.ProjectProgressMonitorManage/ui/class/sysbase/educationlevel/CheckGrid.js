/**
 * 学历选择列表
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.educationlevel.CheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'学历选择列表',
    width:550,
    height:350,
	/**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEducationLevelByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'beducationlevel.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:420,emptyText:'学历名称',isLike:true,
			fields:['beducationlevel.Name']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'学历名称',dataIndex:'BEducationLevel_Name',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BEducationLevel_Shortcode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BEducationLevel_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'BEducationLevel_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
	
	}
});