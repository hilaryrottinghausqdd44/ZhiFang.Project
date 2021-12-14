/**
 * 入库明细列表
 * @author liangyl
 * @version 2018-10-23
 */
Ext.define('Shell.class.rea.client.stock.reconciliations.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '入库明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsInDtl',
	/**新增数据服务路径*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDtl',
	/**修改数据服务路径*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtlByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 50,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[50, 50],
		[100, 100],
		[500, 500],
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDtl_DispOrder',
		direction: 'ASC'
	},{
		property: 'ReaBmsInDtl_DataAddTime',
		direction: 'ASC'
	},{
		property: 'ReaBmsInDtl_GoodsCName',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**默认选中*/
	autoSelect: false,
	/**入库总单Id*/
	InDocID: null,
	/**用户UI配置Key*/
	userUIKey: 'stock.reconciliations.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "入库对帐明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '货品名称',width: 150,
			columnCountKey:me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsInDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsInDtl_GoodSName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsInDtl_BarCodeType',text: '条码类型',hidden:true,
			width: 85,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_GoodsQty',text: '入库数量',
			width: 85,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ProdOrgName',text: '厂家名称',
			width: 80,defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_ProdOrgName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsUnit',text: '单位',
			width: 80,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoods_UnitMemo',text: '规格',
			width: 80,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',text: '单价',width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',sortable: false,
			text: '总计金额',width: 100,
			type: 'number',
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_InDtlNo',sortable: false,
			text: '入库明细单号',width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_InvalidDate',text: '有效期至',
			isDate: true,width: 85,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',text: '批号',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_DataAddTime',text: '操作日期',isDate:true,
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_ProdDate',text: '生产日期',isDate: true,
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_StorageName',text: '库房名称',
			width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',text: '税率',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_CompanyName',text: '供应商',
			width: 150,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_PlaceName',text: '货架名称',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_ProdOrgName',text: '生产厂商',width: 100,defaultRenderer: true,
			doSort: function(state) {
				var field="ReaGoods_ProdOrgName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			dataIndex: 'ReaBmsInDtl_ReaGoods_ProdGoodsNo',text: '厂商物料编码',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_RegisterNo',text: '注册证号',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_RegisterInvalidDate',isDate: true,text: '注册证有效期',width: 85,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_ReaGoods_SuitableType',text: '适用机型',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_Id',sortable: false,text: '主键ID',hidden: true,hideable: false,isKey: true
		}];
		return columns;
	},
	changeDefaultWhere:function(){
		
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,InDocID=null,params = [];
			
		//改变默认条件
//		me.changeDefaultWhere();
			
		me.internalWhere = '';
			

		if(me.InDocID){
			params.push('reabmsindtl.InDocID='+me.InDocID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "("+me.getSearchWhere(search)+")";
			}
		}
		return me.callParent(arguments);
	}
});