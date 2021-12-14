/**
 * 仪器试剂
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.EquipReagentGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '仪器试剂列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLinkByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 1000,
	/**带分页栏*/
	hasPagingtoolbar: false,
	TestItemList: [],
	TestItemEnum: null,
	/**仪器项目ID*/
	TestEquipItemID: null,
	/**检验项目ID*/
	TestItemID: null,
	/**仪器ID*/
	EquipID: null,
	/**后台排序*/
	remoteSort: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl();
	},
	initComponent: function() {
		var me = this;
		me.getGoodsInfo();
		me.buttonToolbarItems = [{
			xtype: 'label',
			text: '仪器试剂',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;"
		}, '-', 'refresh'];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaEquipReagentLink_ReaGoodsNo',
			text: '货品编码',
			width: 150,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_GoodsCName',
			text: '货品名称',
			minWidth: 150,
			flex: 1,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_UnitName',
			text: '单位',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_UnitMemo',
			text: '规格',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_TestCount',
			text: '测试数',
			minWidth: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_GoodsID',
			text: '货品ID',
			width: 150,
			sortable: false,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_TestEquipID',
			text: '仪器id',
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipReagentLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			EquipID = null,
			search = null,
			params = [];
		me.internalWhere = '';
		if(me.EquipID) {
			params.push('reaequipreagentlink.TestEquipID=' + me.EquipID);
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
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.callParent(arguments);
	},
	/**获取货品信息*/
	getGoodsInfo: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectGoodsUrl;
		url += '&fields=ReaGoods_CName,ReaGoods_Id,ReaGoods_ReaGoodsNo,ReaGoods_UnitName,ReaGoods_UnitMemo,ReaGoods_TestCount';
		me.GoodsEnum = {}, me.GoodsList = [];
		me.GoodsNoEnum = {}, me.GoodsNoList = [];
		me.GoodsUnitEnum = {}, me.GoodsTestCount = {};
		me.GoodsUnitMemo = {};
		var obj = {};
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						var tempArr = [obj.ReaGoods_Id, obj.ReaGoods_CName];
						me.GoodsEnum[obj.ReaGoods_Id] = obj.ReaGoods_CName;
						var tempArr2 = [obj.ReaGoods_Id, obj.ReaGoods_ReaGoodsNo];
						me.GoodsNoEnum[obj.ReaGoods_Id] = obj.ReaGoods_ReaGoodsNo;
						me.GoodsUnitEnum[obj.ReaGoods_Id] = obj.ReaGoods_UnitName;
						me.GoodsUnitMemo[obj.ReaGoods_Id] = obj.ReaGoods_UnitMemo;
						me.GoodsTestCount[obj.ReaGoods_Id] = obj.ReaGoods_TestCount;
					});
					obj = {
						GoodsID: me.GoodsEnum,
						UnitName: me.GoodsUnitEnum,
						UnitMemo: me.GoodsUnitMemo,
						ReaGoodsNo: me.GoodsNoEnum,
						GoodsTestCount: me.GoodsTestCount
					}
					me.ReaGoodsArr = obj;
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
			result = {},
			list = [],
			arr = [];
		for(var i = 0; i < data.list.length; i++) {
			var GoodsID = data.list[i].ReaEquipReagentLink_GoodsID;
			var GoodsCName = me.ReaGoodsArr.GoodsID[GoodsID];
			var GoodsNo = me.ReaGoodsArr.ReaGoodsNo[GoodsID];
			var Unit = me.ReaGoodsArr.UnitName[GoodsID];
			var UnitMemo = me.ReaGoodsArr.UnitMemo[GoodsID];
			var GoodsTestCount = me.ReaGoodsArr.GoodsTestCount[GoodsID];
			var obj1 = {
				ReaEquipReagentLink_ReaGoodsNo: GoodsNo,
				ReaEquipReagentLink_GoodsCName: GoodsCName,
				ReaEquipReagentLink_UnitName: Unit,
				ReaEquipReagentLink_UnitMemo: UnitMemo,
				ReaEquipReagentLink_TestCount: GoodsTestCount
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	}
});