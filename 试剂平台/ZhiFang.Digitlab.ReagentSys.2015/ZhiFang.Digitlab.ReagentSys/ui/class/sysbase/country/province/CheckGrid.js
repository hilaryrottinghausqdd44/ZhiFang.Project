/**
 * 省份选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'省份选择列表',
    width:270,
    height:300,
    
    /**根据部门ID查询模式*/
    DeptTypeModel:true,
    /**部门ID*/
    DeptId:null,
	
	/**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bprovince.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称/简称',isLike:true,
			fields:['bprovince.CName','bprovince.SName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'省份名称',dataIndex:'BProvince_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'省份简称',dataIndex:'BProvince_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BProvince_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'BProvince_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	}
});