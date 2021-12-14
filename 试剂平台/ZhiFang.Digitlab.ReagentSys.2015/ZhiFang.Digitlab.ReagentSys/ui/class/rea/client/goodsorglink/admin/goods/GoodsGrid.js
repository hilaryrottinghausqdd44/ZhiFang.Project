/**
 * 产品采购供应维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.goods.GoodsGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '产品列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**默认加载数据*/
	defaultLoad: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reagoods.Visible=1';
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-'];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '产品名称/产品编码',
			fields: ['reagoods.CName', 'reagoods.GoodsNo']
		};
		items.push({
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			dataIndex: 'ReaGoods_CName',
			text: '产品名称',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '产品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			hidden: true,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ProdOrgName',
			text: '厂家',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdEara',
			text: '产地',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Price',
			text: '参考价',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_StorageType',
			text: '储藏条件',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			text: '二级分类',
			dataIndex: 'ReaGoods_GoodsClassType',
			width: 100,
			hidden: true,
			defaultRenderer: true
		});
		return columns;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			list[i].ReaGoods_IsRegister = list[i].ReaGoods_IsRegister == '1' ? true : false;
			list[i].ReaGoods_IsPrintBarCode = list[i].ReaGoods_IsPrintBarCode == '1' ? true : false;
		}
		data.list = list;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		me.internalWhere = params.join(' and ');
		return me.callParent(arguments);
	}
});