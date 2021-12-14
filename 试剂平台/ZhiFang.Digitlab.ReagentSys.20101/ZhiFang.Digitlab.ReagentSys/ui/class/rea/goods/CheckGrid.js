/**
 * 产品选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'产品选择列表',
    width:1260,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true',
    
    /**是否单选*/
	checkOne:false,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'goods.Visible=1';
		
		//查询框信息
		me.searchInfo = {width:160,emptyText:'产品编号/名称/简称',isLike:true,
			fields:['goods.GoodsNo','goods.CName','goods.ShortCode']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'Goods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			renderer:function(value, meta) {
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
			dataIndex: 'Goods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		},{
			dataIndex: 'Goods_CenOrg_CName',
			text: '机构',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_GoodsNo',
			text: '产品编码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_Comp_CName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_Prod_CName',
			text: '厂商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_CompGoodsNo',
			text: '供应商产品编码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_ProdGoodsNo',
			text: '厂商产品编码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_ShortCode',
			text: '代码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'Goods_CenOrg_Id',
			text: '机构ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Comp_Id',
			text: '供应商ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Prod_Id',
			text: '厂商ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_ProdEara',
			text: '产地',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_ProdOrgName',
			text: '生成厂家',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_GoodsClass',
			text: '一级分类',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_GoodsClassType',
			text: '二级分类',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_StorageType',
			text: '储藏条件',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_GoodsDesc',
			text: '货品描述',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_ApproveDocNo',
			text: '批准文号',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Standard',
			text: '国标',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_RegistNo',
			text: '注册号',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_RegistDate',
			text: '注册日期',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_RegistNoInvalidDate',
			text: '注册证有效期',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Purpose',
			text: '用途',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Constitute',
			text: '机构组成',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_Price',
			text: '单价',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'Goods_BiddingNo',
			text: '招标号',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	}
});