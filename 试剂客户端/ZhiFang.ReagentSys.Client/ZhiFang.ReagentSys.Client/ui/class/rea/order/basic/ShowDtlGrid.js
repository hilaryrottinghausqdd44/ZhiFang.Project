/**
 * 订货明细列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.order.basic.ShowDtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '订货明细列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDtl',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDtl',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtlByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**序号列宽度*/
	rowNumbererWidth: 35,
	
	/**供应商ID*/
	CompId:'',
	/**机构ID*/
	CenOrgId:'',
	
	/**排序字段*/
	//defaultOrderBy:[{property:'BmsCenOrderDtl_DataAddTime',direction:'ASC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenOrderDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_ProdGoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenOrderDtl_GoodsQty',sortable: false,
			text:'订购数',
			width:50,type:'float',align:'right'
		},{
			dataIndex:'BmsCenOrderDtl_Price',sortable: false,
			text:'单价',
			width:70,type:'float',align:'right'
		},{
			dataIndex: 'TotalPrice',sortable: false,
			text: '金额',align:'right',width: 80,
			renderer:function(value, meta, record, rowIndex, colIndex) {
				var TotalPrice = record.get('BmsCenOrderDtl_GoodsQty') * record.get('BmsCenOrderDtl_Price');
				TotalPrice = TotalPrice ? TotalPrice.toFixed(2) : 0;
				meta.tdAttr = 'data-qtip="<b>' + TotalPrice + '</b>"';
				return TotalPrice;
			}
		},{
			dataIndex: 'BmsCenOrderDtl_GoodsUnit',sortable: false,
			text: '单位',
			width: 50,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_ProdOrgName',sortable: false,
			text: '厂商',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});