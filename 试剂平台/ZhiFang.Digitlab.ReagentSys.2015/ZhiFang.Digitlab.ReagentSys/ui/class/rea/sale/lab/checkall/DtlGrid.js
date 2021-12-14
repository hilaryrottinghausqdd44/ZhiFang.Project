/**
 * 供货验收-供货明细列表-实验室专用
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.lab.checkall.DtlGrid', {
	extend: 'Shell.class.rea.sale.basic.DtlGrid',
	title: '供货验收-供货明细列表-实验室专用',
	
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**默认选中*/
	autoSelect:false,
	
	/**是否可编辑*/
	canEdit:true,
	
	/**排序字段*/
	defaultOrderBy:[
		{property:'BmsCenSaleDtl_DispOrder',direction:'ASC'}
	],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.store.on({
			update:function(store,record){
				me.onPriceOrGoodsQtyChanged(record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_Goods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			renderer:function(value, meta) {
				var v = "";
				if(value == "0"){
					v = "批条码";
					meta.style = "color:green;";
				}else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				}else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ShortCode',
			text: '产品简码',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',
			text: '厂商产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_LotNo',sortable: false,
			text:'<b style="color:blue;">产品批号</b>',
			width:100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_InvalidDate',sortable: false,
			text:'<b style="color:blue;">有效期</b>',
			width:100,type:'date',isDate:true,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_GoodsQty',sortable: false,
			text:'<b style="color:blue;">数量</b>',
			width:80,type:'int',align:'right',
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_Price',sortable: false,
			text:'<b style="color:blue;">单价</b>',
			width:80,type:'float',align:'right',
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_TempRange',sortable: false,
		    text:'<b style="color:blue;">温度范围</b>',
			width:100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdDate',sortable: false,
		    text:'<b style="color:blue;">生产日期</b>',
			width:100,type:'date',isDate:true,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_MixSerial',sortable: false,
			width: 200,
			text:'<b style="color:orange;">混合条码</b>',
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdOrgName',sortable: false,
		    text:'<b style="color:blue;">生产厂家</b>',
			width:100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BiddingNo',sortable: false,
		    text:'<b style="color:blue;">招标号</b>',
			width:100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_RegisterNo',sortable: false,
			text:'<b style="color:green;">注册证编号</b>',
			width:100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_RegisterInvalidDate',sortable: false,
			text:'<b style="color:green;">注册证有效期</b>',
			width:100,type:'date',isDate:true,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_TaxRate',sortable: false,
			text: '税率',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenSaleDtl_DataAddTime',sortable: false,
			text: '新增时间',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_EName',sortable: false,
			text: '产品英文名',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsClass',sortable: false,
			text: '一级分类',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo',sortable: false,
			text: '供货单号',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Prod_OrgNo',sortable: false,
			text: '产品品牌',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo',sortable: false,
			text: '供应商名称',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged:function(record){
		var me = this;
		var Price = record.get('BmsCenSaleDtl_Price');
		var GoodsQty = record.get('BmsCenSaleDtl_GoodsQty');
		
		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);
		
		record.set('BmsCenSaleDtl_SumTotal',SumTotal);
	},
	
	/**刷新数据*/
	onSearch:function(){
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead:function(){
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = false;
		this.load(null, true);
	},
	
	/**获取数据错误信息*/
	getDataErrorMsg:function(){
		var me = this,
			records = me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length,
			errorMsg = null;
			
		for(var i=0;i<len;i++){
			var rec = records[i],
				GoodsQty = rec.get('BmsCenSaleDtl_GoodsQty'),
				AcceptCount = rec.get('BmsCenSaleDtl_AcceptCount'),
				AccepterErrorMsg = rec.get('BmsCenSaleDtl_AccepterErrorMsg');
				
			if(GoodsQty == AcceptCount && AccepterErrorMsg){
				errorMsg = '验收数量与供货数量一致时，不能填写异常信息，请删除该条异常信息后再操作！';
				break;
			}else if(AcceptCount > GoodsQty){
				errorMsg = '验收数量不能大于供货数量，请修改后再操作！';
				break;
			}else if(AcceptCount < GoodsQty && !AccepterErrorMsg){
				errorMsg = '验收数量小于供货数量时，必须填写异常信息，请修改后再操作！';
				break;
			}
		}
		
		return errorMsg;
	}
});