/**
 * 仪器项目试剂关系
 * Rea_EquipTestItemReaGoodLink_仪器项目试剂关系
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '仪器项目试剂关系列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaEquipTestItemReaGoodLinkNewEntityListByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaEquipTestItemReaGoodLink',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaEquipTestItemReaGoodLinkByField',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaEquipTestItemReaGoodLink',
	/**获取获取供应商数据服务路径*/
	selectGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
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
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaEquipTestItemReaGoodLink_DispOrder',
		direction: 'ASC'
	}],
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**项目ID*/
	TestItemID: null,
	/**仪器ID*/
	TestEquipID: null,
	/**后台排序*/
	remoteSort: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.buttonToolbarItems = [{
			xtype: 'label',
			text: '项目试剂',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;"
		}, '-', 'refresh', '-', 'add', 'del', '-', 'save', '-', {
			xtype: 'checkboxfield',
			margin: '0 0 0 0',
			boxLabel: '仅显示当前仪器项目的试剂关系',
			name: 'Check',
			itemId: 'Check',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaEquipTestItemReaGoodLink_TestItemID',
			text: '项目Id',
			sortable: false,
			width: 150,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaEquipTestItemReaGoodLink_TestItemCode',
			text: 'LIS项目编码',
			sortable: false,
			width: 150,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_EquipCName',
			text: '仪器名称',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_GoodsCName',
			text: '货品名称',
			sortable: false,
			minWidth: 100,
			flex: 1,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_TestCount',
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			sortable: false,
			text: '<b style="color:blue;">测试数</b>',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_UnitName',
			text: '单位',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_UnitMemo',
			text: '规格',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_ZX1',
			text: 'ZX1',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_ZX2',
			text: 'ZX2',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_ZX3',
			text: 'ZX3',
			width: 100,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaEquipTestItemReaGoodLink_GoodsID',
			text: '货品ID',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaEquipTestItemReaGoodLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length > 32) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		if(!me.TestEquipID) {
			JShell.Msg.alert('请选择仪器后再操作！');
			return;
		}
		var maxWidth = document.body.clientWidth * 0.60;
		var height = document.body.clientHeight * 0.78;
		var ids = me.getGoodsId();
		var defaultWhere = 'reaequipreagentlink.TestEquipID='+me.TestEquipID;
		if(ids) {
			defaultWhere = defaultWhere+' and reaequipreagentlink.GoodsID not in(' + ids + ')';// and reaequipreagentlink.Visible=1
		}

		JShell.Win.open('Shell.class.rea.client.testitem.equipitemgoodlink.CheckGrid', {
			title: '试剂选择',
			width: maxWidth,
			height: height,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**获取当前页的货品ID*/
	getGoodsId: function() {
		var me = this;
		var records = me.store.data.items;
		var len = records.length;
		var ids =[];
		for(var i = 0; i < len; i++) {
			ids.push(records[i].get('ReaEquipTestItemReaGoodLink_GoodsID'));
		}
		return ids.join(",");
	},
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this,
			ids = [],
			addIds = [];

		if(records.length == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for(var i in records) {
			ids.push(records[i].get('ReaEquipReagentLink_GoodsID'));
		}
		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids, function(list) {
			addIds = [];
			for(var i in records) {
				var GoodsID = records[i].get('ReaEquipReagentLink_GoodsID');
				var CName = records[i].get('ReaEquipReagentLink_ReaGoods_GoodsCName');
				var UnitName = records[i].get('ReaEquipReagentLink_ReaGoods_UnitName');
				var UnitMemo = records[i].get('ReaEquipReagentLink_ReaGoods_UnitMemo');
				var TestCount = records[i].get('ReaEquipReagentLink_ReaGoods_TestCount');
				//				var ZX1=records[i].get('ReaGoods_ZX1');
				var hasLink = false;
				for(var j in list) {
					if(GoodsID == list[j].GoodsID) {
						hasLink = true;
						break;
					}
				}
				if(!hasLink) {
					var obj = {
						GoodsID: GoodsID,
						UnitName: UnitName,
						UnitMemo: UnitMemo,
						TestCount: TestCount
					};
					addIds.push(obj);
				}
				if(hasLink) {
					me.hideMask(); //隐藏遮罩层
					p.close();
				}
			}
			//循环保存数据
			for(var i in addIds) {
				me.saveLength = addIds.length;
				me.onAddOneLink(addIds[i], function() {
					JShell.Msg.alert("保存成功", null, 1000);
					p.close();
					me.onSearch();
				});
			}
		});
	},
	/**新增关系数据*/
	onAddOneLink: function(addIds, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var entity = {
			TestItemID: me.TestItemID,
			GoodsID: addIds.GoodsID,
			TestEquipID: me.TestEquipID,
			Visible: 1
		};
		if(addIds.TestCount) {
			entity.TestCount = addIds.TestCount;
		}
		var params = {
			entity: entity
		};

		//提交数据到后台
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					callback();
				}
			}
		});
	},

	/**根据IDS获取关系数据，用于验证勾选的货品是否已经存在于关系中*/
	getLinkByIds: function(ids, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl.split('?')[0] +
			'?fields=ReaEquipTestItemReaGoodLink_GoodsID' +
			'&where=reaequiptestitemreagoodlink.GoodsID in(' + ids.join(',') + ')  and reaequiptestitemreagoodlink.TestItemID=' + me.TestItemID +
			' and reaequiptestitemreagoodlink.TestEquipID=' + me.TestEquipID;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		me.internalWhere = '';
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var Check = buttonsToolbar.getComponent('Check');
		if(me.TestItemID) {
			params.push('reaequiptestitemreagoodlink.TestItemID=' + me.TestItemID);
		}
		if(Check.getValue() && me.TestEquipID) {
			params.push('reaequiptestitemreagoodlink.TestEquipID=' + me.TestEquipID);
		}
		//		if(me.TestEquipID){
		//			params.push('reaequiptestitemreagoodlink.TestEquipID='+me.TestEquipID);
		//		}
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
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;
		if(len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			me.updateOne(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateOne: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var entity = {
			Id: record.get('ReaEquipTestItemReaGoodLink_Id'),
			TestCount: record.get('ReaEquipTestItemReaGoodLink_TestCount')

		};
		fields = 'Id,TestCount';
		var params = Ext.JSON.encode({
			entity: entity,
			fields: fields
		});

		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	}
});