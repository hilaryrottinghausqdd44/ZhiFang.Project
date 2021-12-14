/**
 * 颜色维护
 * @author GHX
 * @version 2021-01-29
 */
Ext.define('Shell.class.weixin.itemColorDict.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '颜色列表',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorDictByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelItemColorDict',
	
	/**是否使用字段名*/
    IsUseField:'',
	hasSave:false,
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	IsUseType:'int',
	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['itemcolordict.ColorName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'颜色名称',dataIndex:'ItemColorDict_ColorName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'颜色值',dataIndex:'ItemColorDict_ColorValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'ItemColorDict_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});