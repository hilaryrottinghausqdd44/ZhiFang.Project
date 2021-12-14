/**
 * 实验室列表
 * @author GHX
 * @version 2021-01-08
 */
Ext.define('Shell.class.weixin.clientele.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室列表 ',	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelCLIENTELE',
	
	/**是否使用字段名*/
    IsUseField:'CLIENTELE_ISUSE',
	IsUseType:'int',
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	
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
			fields:['clientele.CNAME']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'实验室名称',dataIndex:'CLIENTELE_CNAME',width:250,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室简称',dataIndex:'CLIENTELE_ENAME',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'CLIENTELE_SHORTCODE',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'CLIENTELE_ISUSE',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'主键ID',dataIndex:'CLIENTELE_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});