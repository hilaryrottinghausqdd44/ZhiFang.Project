/**
 * 字典类型列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dicttype.SimpleGrid',{
    extend: 'Shell.ux.grid.Panel',
    
    title:'字典类型列表',
    width: 205,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictTypeByHQL?isPlanish=true',
  	
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
	
	defaultOrderBy:[{property:'BDictType_DispOrder',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:220,emptyText:'编码/名称',isLike:true,
			fields:['bdicttype.DictTypeCode','bdicttype.CName']};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'编码',dataIndex:'BDictType_DictTypeCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'BDictType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
//		},{
//			text:'次序',dataIndex:'BDictType_DispOrder',width:100,
//			defaultRenderer:true,align:'center',type:'int'
		},{
			text:'主键ID',dataIndex:'BDictType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});