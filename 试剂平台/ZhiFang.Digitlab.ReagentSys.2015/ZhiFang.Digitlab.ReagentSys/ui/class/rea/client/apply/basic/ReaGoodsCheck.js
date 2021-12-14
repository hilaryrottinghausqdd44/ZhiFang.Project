/**
 * 产品待选列表
 * @author longfc
 * @version 2016-10-24
 */
Ext.define('Shell.class.rea.client.apply.basic.ReaGoodsCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '部门货品待选列表',
	width: 360,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**默认每页数量*/
	defaultPageSize: 20,
	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'readeptgoods.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'Search',
			emptyText: '中文名/产品编号/英文名/同系列码',
			fields: ['readeptgoods.ReaGoods.GoodsCName', 'readeptgoods.ReaGoods.GoodsNo', 'readeptgoods.ReaGoods.EName', 'readeptgoods.ReaGoods.ShortCode']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['->'];
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaDeptGoods_ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			hidden: true,
			renderer: function(value, meta) {
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
			dataIndex: 'ReaDeptGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_Id',
			text: '货品名Id',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_GoodsCName',
			text: '货品名',
			width: 170,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_EName',
			text: '英文名',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ReaGoodsUnit_Id',
			text: '单位Id',
			hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitName',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitMemo',
			text: '规格',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ShortCode',
			text: '系列码',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsOtherQty',
			text: '货品对应同系列的库存数量',
			width: 75,
			hidden: true,
			defaultRenderer: true
		}];
		columns.push(me.createCurrentQtyColumn());
		return columns;
	},
	/**创建当前库存数数据列*/
	createCurrentQtyColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'CurrentQty',
			text: '库存数',
			width: 75,
			renderer: function(value, meta, record) {
				var goodsOtherQty = "";
				goodsOtherQty = record.get("GoodsOtherQty");
				if(goodsOtherQty) goodsOtherQty = "<p border=0 style='vertical-align:top;font-size:12px;'>同系列库存为:" + goodsOtherQty + "</p>";
				if(value) goodsOtherQty = "<p border=0 style='vertical-align:top;font-size:12px;'>当前库存数为:" + value + "<br />" + goodsOtherQty + "</p>";
				meta.tdAttr = 'data-qtip="<b>' + goodsOtherQty + '</b>"';
				return value;
			}
		};
		return column;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;	
		me.fireEvent('onBeforeSearch', me);
		me.load(null, true, autoSelect);
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
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records&&records.length>0){
			me.loadCurrentQty();
		}
	},
	/**@description 获取申请货品明细的库存数量*/
	loadCurrentQty: function() {
		var me = this;
		var idStr = "",
			goodIdStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaDeptGoods_ReaGoods_Id");
			goodIdStr += goodId + ",";
		});
		if(!goodIdStr) return;
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		idStr = idStr.substring(0, idStr.length - 1);
		var url = "/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?goodIdStr=" + goodIdStr + "&idStr=" + idStr;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value;
				if(list && list.length > 0) {
					me.store.each(function(record) {
						for(var i = 0; i < list.length; i++) {
							if(record.get("ReaDeptGoods_ReaGoods_Id") == list[i]["CurGoodsId"]) {
								record.set("CurrentQty", list[i]["CurrentQty"]);
								record.set("GoodsOtherQty", list[i]["GoodsOtherQty"]);
								record.commit();
								break;
							}
						}
					});
				}
			} 
		});
	}
});