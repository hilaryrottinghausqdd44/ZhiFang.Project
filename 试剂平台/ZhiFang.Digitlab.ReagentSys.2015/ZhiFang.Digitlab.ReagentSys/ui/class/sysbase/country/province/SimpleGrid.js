/**
 * 国家列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.SimpleGrid',{
    extend: 'Shell.ux.grid.Panel',
    
    title:'国家列表',
    width: 220,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	defaultOrderBy:[{property:'BProvince_DataAddTime',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:155,emptyText:'名称/简称',isLike:true,
			fields:['province.Name','province.SName']};
		
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		  
		var me = this;
		var columns = [{
			text:'名称',dataIndex:'BProvince_Name',defaultRenderer:true
		},{
			text:'简称',dataIndex:'BProvince_SName',defaultRenderer:true
		},{
			text:'创建时间',dataIndex:'BProvince_DataAddTime',width:130,
			isDate:true,hasTime:true,hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BProvince_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'BProvince_DataTimeStamp',hidden:true,hideable:false
		}];
		
		return columns;
	}
});