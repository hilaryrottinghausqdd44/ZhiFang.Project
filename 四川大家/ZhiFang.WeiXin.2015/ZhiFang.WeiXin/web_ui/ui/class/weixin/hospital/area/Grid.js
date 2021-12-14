/**
 * 区域列表
 * @author liangyl
 * @version 2017-02-24
 */
Ext.define('Shell.class.weixin.hospital.area.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '区域列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateClientEleAreaByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelClientEleArea',
	/**下载医生相片*/
	DownLoadImageUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadClientEleAreaImageByAccountID',
	/**默认加载*/
	defaultLoad: true,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,

	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称/代码',isLike:true,
			fields:['clientelearea.AreaCName','clientelearea.ClientNo']};
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
			text:'简称',dataIndex:'ClientEleArea_ShortName',width:100,hidden:true,
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