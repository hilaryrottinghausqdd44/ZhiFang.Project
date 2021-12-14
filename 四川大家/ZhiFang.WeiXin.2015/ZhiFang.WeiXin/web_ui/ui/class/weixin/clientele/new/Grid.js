/**
 * 实验室列表
 * @author GHX
 * @version 2021-01-11
 */
Ext.define('Shell.class.weixin.clientele.new.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	
	title: '实验室列表 ',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelCLIENTELE',
	
    /**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用保存按钮*/
	hasSave:false,
	/**默认加载*/
	defaultLoad: true,
	//排序字段
	defaultOrderBy: [{property:'CLIENTELE_Id',direction:'ASC'}],
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
		
		var columns = [/* {
			text:'编号',dataIndex:'LabNo',width:100,
		}, */{
			text:'中文名',dataIndex:'CLIENTELE_CNAME',width:260,
			sortable:false
		},{
			text:'英文名',dataIndex:'CLIENTELE_ENAME',width:200
		},{
			text:'快捷码',dataIndex:'CLIENTELE_SHORTCODE',width:150
		},{
			text:'使用',dataIndex:'CLIENTELE_ISUSE',//isBool:true,
			width:40,renderer: function(value){
				if (value == 1 || value=="1") {
					return '是';
				}else{
					return '否';
				}
				
			}
		},{
			text:'主键ID',dataIndex:'CLIENTELE_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});