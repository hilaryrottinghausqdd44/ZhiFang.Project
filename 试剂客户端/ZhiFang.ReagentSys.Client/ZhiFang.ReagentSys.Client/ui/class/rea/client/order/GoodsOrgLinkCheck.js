/**
 * 机构货品选择列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.order.GoodsOrgLinkCheck', {
	extend: 'Shell.class.rea.client.goodsorglink.CheckGrid',
	title: '货品选择列表',
	width: 875,
	height: 540,
	/**是否单选*/
	checkOne: false,
	/**编辑前的行选中集合*/
	SelectionRecords: null,

	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			listeners: {
					beforeedit: function(editor, e, eOpts) {
						me.SelectionRecords = me.getSelectionModel().getSelection();
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
						}
					},
					canceledit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					},
					edit: function(editor, e, eOpts) {
						if(me.SelectionRecords && me.SelectionRecords.length > 0) {
							me.getSelectionModel().select(me.SelectionRecords, false);
							me.SelectionRecords = null;
						}
					}
				}
		});	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns =me.callParent(arguments);
		columns.splice(1, 0,{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_MonthlyUsage',
			text: '理论月用量',
			width: 75,
			defaultRenderer: true
		},{
			dataIndex: 'CurrentQty',
			text: '当前库存数',
			width: 75,
			renderer: function(value, meta, record) {
				return value;
			}
		},{
			dataIndex: 'GoodsQty',
			text: '<b style="color:blue;">申请数</b>',
			width: 65,
			editor: {
				xtype: 'numberfield',
				allowBlank: false,
				minValue: 0,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('GoodsQty', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		});
		return columns;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records && records.length > 0) {
			me.loadCurrentQty();
		}
	},
	/**@description 获取申请货品明细的库存数量*/
	loadCurrentQty: function() {
		var me = this;
		var idStr = "",
			goodIdStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaGoodsOrgLink_ReaGoods_Id");
			goodIdStr += goodId + ",";
		});
		if(!goodIdStr) return;
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		idStr = idStr.substring(0, idStr.length - 1);
		var url = "/ReaManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?goodIdStr=" + goodIdStr + "&idStr=" + idStr;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value;
				if(list && list.length > 0) {
					me.store.each(function(record) {
						for(var i = 0; i < list.length; i++) {
							if(record.get("ReaGoodsOrgLink_ReaGoods_Id") == list[i]["CurGoodsId"]) {
								var currentQty = list[i]["GoodsQty"];
								if(!parseFloat(currentQty))
									currentQty = 0;
								record.set("CurrentQty", currentQty);
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