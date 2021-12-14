/**
 * 区域选择列表
 * @author liangyl	
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.hospital.area.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'区域选择列表',
    width:320,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'区域名称/简称/代码',isLike:true,
			fields:['clientelearea.AreaCName','clientelearea.AreaShortName','clientelearea.ClientNo']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'区域名称',dataIndex:'ClientEleArea_AreaCName',width:130,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'ClientEleArea_AreaShortName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'代码',dataIndex:'ClientEleArea_ClientNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'ClientEleArea_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});