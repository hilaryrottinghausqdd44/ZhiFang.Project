/**
 * 统计来源组成明细
 * @author liangyl
 * @version 2017-11-14
 */
Ext.define('Shell.class.rea.client.seach.bmsqtydtl.BmsInDtGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '统计来源组成明细',
	width: 730,
	height: 450,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '',
	/**修改服务地址*/
    editUrl:'',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**货品id*/
    goodsId:null,
	/**默认加载数据*/
	defaultLoad: true,
	
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsInDtl_GoodsCName',text: '货品名称',
			width: 150,defaultRenderer:true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsQty',text: '数量',
			width: 100,defaultRenderer:true
		}, {
			dataIndex: 'ReaBmsInDtl_UnitName',text: '单位',
			width: 100,defaultRenderer:true
		}, {
			dataIndex: 'ReaBmsInDtl_Price',text: '单价',
			width: 100,defaultRenderer:true
		},{
			dataIndex: 'ReaBmsInDtl_税率',text: '加权平均价',
			width: 100,defaultRenderer:true
		},{
			dataIndex: 'ReaBmsInDtl_SumTotal',text: '总计',
			width: 100,defaultRenderer:true
		},{
			dataIndex: 'ReaBmsInDtl_Id',text: '主键ID',hidden: true,hideable: false,
			isKey: true,defaultRenderer:true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		//查询框信息
		me.searchInfo = {
			width:150,isLike:true,itemId: 'Search',
			emptyText:'货品名称/单位',
			fields:['reabmsindtl.GoodsCName','reagoods.UnitName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
            goodsId = null,search = null
			params = [];
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('Search').getValue();
		}
		
		if(me.goodsId){
			params.push("reabmsindtl.ReaGoods.Id=" + me.goodsId );
		}
		
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		
		return me.callParent(arguments);
	}
	
});