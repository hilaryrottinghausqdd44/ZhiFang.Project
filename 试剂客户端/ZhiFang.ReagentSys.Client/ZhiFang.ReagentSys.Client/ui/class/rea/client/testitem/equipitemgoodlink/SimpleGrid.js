/**
 * 仪器列表
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.SimpleGrid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
    title:'仪器列表',
    width:800,
    height:500,
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaTestEquipLab',
	 /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipLabByField',
    /**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaTestEquipLab',
	/**获取获取供应商数据服务路径*/
	selectCenOrgUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',

    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaTestEquipLab_DispOrder',direction:'ASC'}],

    /**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**原始行数*/
	oldCount: 0,
	CenOrgList:[],
    CenOrgEnum:null,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:155,emptyText:'仪器名称/英文名称/代码',isLike:true,itemId:'Search',
			fields:['reatestequiplab.CName','reatestequiplab.EName','reatestequiplab.ShortCode']
		};		
		me.buttonToolbarItems = ['refresh','->',{
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipLab_LisCode',
			text: '仪器编号',
			width: 100,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipLab_CName',
			text: '仪器名称',
			flex: 1,
			defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipLab_EName',
			text: '英文名称',
			width: 100,hidden:true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipLab_ShortCode',
			text: '代码',width: 100,hidden:true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipLab_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		return columns;
	}
	
});