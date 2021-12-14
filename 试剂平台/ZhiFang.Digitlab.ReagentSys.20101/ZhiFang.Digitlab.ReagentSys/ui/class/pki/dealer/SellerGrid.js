/**
 * 经销商销售列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.SellerGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'经销商销售列表',
    width:300,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDDealSellerByHQL?isPlanish=true',
    /**删除数据服务路径*/
    delUrl:'/BaseService.svc/ST_UDTO_DelDDealSeller',
    /**新增数据服务路径*/
   	saveUrl:'/BaseService.svc/ST_UDTO_AddDDealSeller',
    /**默认加载*/
	defaultLoad:false,
	/**后台排序*/
	remoteSort:true,
	/**排序字段*/
	defaultOrderBy:[
		{property:'DDealSeller_BeginDate',direction:'DESC'}
	],
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**默认每页数量*/
	defaultPageSize:500,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**复选框*/
	multiSelect:true,
	selType:'checkboxmodel',
	hasDel:true,
	
	/**经销商ID*/
	DealerId:null,
	/**经销商时间戳*/
	DealerDataTimeStamp:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:165,emptyText:'销售',isLike:true,fields:['ddealseller.BSeller.Name']};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','add','del','->',{type:'search',info:me.searchInfo}];
		//数据列
		me.columns = [{
			dataIndex:'DDealSeller_BeginDate',text:'开始日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DDealSeller_EndDate',text:'截止日期',width:80,isDate:true,sortable:false
		},{
			dataIndex:'DDealSeller_BSeller_Name',text:'销售',width:100,sortable:false
		},{
			dataIndex:'DDealSeller_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		}];
		
		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		JShell.Win.open('Shell.class.pki.dealer.SellerForm',{
			formtype:'add',
			resizable:false,
			formtype:'add',
			showSuccessInfo:false,
			DealerId:me.DealerId,
			DealerDataTimeStamp:me.DealerDataTimeStamp,
			listeners:{
				save:function(win){me.onSearch();}
			}
		}).show();
	},
	/**根据经销商ID获取数据*/
	loadByDealerId:function(id){
		var me = this;
		me.DealerId = id;
		me.defaultWhere = 'ddealseller.BDealer.Id=' + id;
		me.onSearch();
	}
});