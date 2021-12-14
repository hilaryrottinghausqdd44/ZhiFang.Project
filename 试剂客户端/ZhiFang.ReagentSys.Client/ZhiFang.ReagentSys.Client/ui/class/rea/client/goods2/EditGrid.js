/**
 * 库存上下限维护
 * 货品效期报警预警天数维护
 * @author liangyl
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.goods2.EditGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '货品列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoods',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**是否启用保存按钮*/
	hasSave: true,

	isRowEdit: true,
	editText: '按行编辑',
	isCycle: false,
	isRowCycle: false,
	isColCycle: false,
	rowIdx: null,
	colIdx: null,
	isSpecialkey: false,

	specialkeyArr: [{
		key: Ext.EventObject.ENTER,
		replaceKey: Ext.EventObject.TAB
	}, {
		key: Ext.EventObject.LEFT,
		type: 'left',
		ctrlKey: true
	}, {
		key: Ext.EventObject.RIGHT,
		type: 'right',
		ctrlKey: true
	}],
	defaultWhere: 'reagoods.GonvertQty=1',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoods_ReaGoodsNo',
		direction: 'ASC'
	}],
	hideTimes: 2000,
	/**用户UI配置Key*/
	userUIKey: 'goods2.EditGrid',
	/**用户UI配置Name*/
	userUIName: "机构货品预警设置列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.createCellEditListeners();
		me.on({
			validateedit: function(editor, e) {
				var bo = false;
				var edit = me.getPlugin('NewsGridEditing');

				bo = me.fireEvent('cellAvailable', editor, e) === false ? false : true;

				if (me.rowIdx == null && me.colIdx == null) {
					me.colIdx = e.colIdx;
					me.rowIdx = e.rowIdx;
				}
				if (me.rowIdx != null && me.colIdx != null) {
					if (me.isSpecialkey) {
						edit.startEditByPosition({
							row: me.rowIdx,
							column: me.colIdx
						})
					}
					me.rowIdx = null;
					me.colIdx = null;
					me.isSpecialkey = false
				}
				return bo
			}
		});
	},
	initComponent: function() {
		var me = this;
		if (me.isCycle) {
			me.isRowCycle = true;
			me.isColCycle = true
		}
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_EName',
			text: '英文名称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_SName',
			text: '简称',
			hidden:true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_StoreUpper',
			text: '<b style="color:blue;">库存上限</b>',
			width: 85,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_StoreLower',
			text: '<b style="color:blue;">库存下限</b>',
			width: 85,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_BeforeWarningDay',
			text: '<b style="color:blue;">效期报警天数</b>',
			width: 100,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_IsNeedBOpen',
			text: '<b style="color:blue;">需要开瓶管理</b>',
			width: 85,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_BOpenDays',
			text: '<b style="color:blue;">开瓶后有效天数</b>',
			width: 100,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'save'];
		items.push({
			xtype: 'radiogroup',
			fieldLabel: '',
			width: 180,
			columns: 2,
			hidden: true,
			vertical: true,
			items: [{
					boxLabel: '行编辑模式',
					name: 'rb',
					inputValue: '1',
					checked: true
				},
				{
					boxLabel: '列编辑模式',
					name: 'rb',
					inputValue: '2'
				}
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var val = newValue.rb;
					if (val == '1') {
						me.specialkeyArr = [{
							key: Ext.EventObject.ENTER,
							replaceKey: Ext.EventObject.TAB
						}];
					} else {
						me.specialkeyArr = [{
							key: Ext.EventObject.ENTER,
							type: 'down'
						}];
					}
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品编码/中文名/英文名/简称',
			fields: ['reagoods.ReaGoodsNo', 'reagoods.CName', 'reagoods.EName', 'reagoods.ShortCode']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if (len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for (var i = 0; i < len; i++) {
			me.updateInfo(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateInfo: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var entity = {
			Id: record.get('ReaGoods_Id'),
			ReaGoodsNo: record.get('ReaGoods_ReaGoodsNo')
		}
		var fields = 'Id,ReaGoodsNo';
		if (record.get('ReaGoods_StoreUpper')) {
			entity.StoreUpper = record.get('ReaGoods_StoreUpper');
			fields += ',StoreUpper';
		}
		if (record.get('ReaGoods_StoreLower')) {
			entity.StoreLower = record.get('ReaGoods_StoreLower');
			fields += ',StoreLower';
		}
		if (record.get('ReaGoods_BeforeWarningDay')) {
			entity.BeforeWarningDay = record.get('ReaGoods_BeforeWarningDay');
			fields += ',BeforeWarningDay';
		}
		entity.BOpenDays=record.get('ReaGoods_BOpenDays');
		if(!entity.BOpenDays)entity.BOpenDays=null;
		
		entity.IsNeedBOpen=record.get('ReaGoods_IsNeedBOpen') ? 1 : 0;
		fields += ',IsNeedBOpen,BOpenDays';
		
		var params = Ext.JSON.encode({
			entity: entity,
			fields: fields
		});
		JShell.Server.post(url, params, function(data) {
			if (data.success) {
				me.saveCount++;
				if (record) {
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if (record) {
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
					me.onSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	createCellEditListeners: function() {
		var me = this,
			columns = me.columns;
		for (var i in columns) {
			var column = columns[i];
			if (column.editor) {
				column.editor.listeners = column.editor.listeners || {};
				column.editor.listeners.specialkey = function(textField, e) {
					me.doSpecialkey(textField, e);
				};
				column.hasEditor = true
			} else if (column.columns) {
				for (var j in column.columns) {
					var c = column.columns[j];
					if (c.editor) {
						c.editor.listeners = c.editor.listeners || {};
						c.editor.listeners.specialkey = function(textField, e) {
							me.doSpecialkey(textField, e);
						};
						c.hasEditor = true
					}
				}
			}
		}
	},
	doSpecialkey: function(textField, e) {
		var me = this;
		textField.focus();
		var info = me.getKeyInfo(e);
		if (info) {
			me.isSpecialkey = true;
			e.stopEvent();
			me.changeRowIdxAndCelIdx(textField, info.type);
			textField.blur();
		}
	},
	getKeyInfo: function(e) {
		var me = this,
			arr = me.specialkeyArr,
			key = e.getKey();
		var info = null;
		for (var i in arr) {
			var ctrlKey = arr[i].ctrlKey ? true : false;
			var shiftKey = arr[i].shiftKey ? true : false;
			if (arr[i].key == key && ctrlKey == e.ctrlKey && shiftKey == e.shiftKey) {
				if (arr[i].replaceKey) {
					e.keyCode = arr[i].replaceKey;
					info = null;
					break
				} else {
					info = arr[i];
					break
				}
			}
		}
		return info
	},
	changeRowIdxAndCelIdx: function(field, type) {
		var me = this,
			context = field.ownerCt.editingPlugin.context,
			rowIdx = context.rowIdx,
			colIdx = context.colIdx;
		me.rowIdx = rowIdx;
		me.colIdx = colIdx;
		if (type == 'up') {
			me.rowIdx = me.getNextRowIndex(rowIdx, false, false);
		} else if (type == 'down') {
			me.rowIdx = me.getNextRowIndex(rowIdx, true, true, colIdx);
		} else if (type == 'left') {
			me.colIdx = me.getNextColIndex(colIdx, false);
		} else if (type == 'right') {
			me.colIdx = me.getNextColIndex(colIdx, true);
		}

	},
	getNextRowIndex: function(rowIdx, isDown, isRowCycle, colIdx) {
		var me = this,
			count = me.store.getCount(),
			nRowIdx = rowIdx;
		if (count == 0) return null;
		isDown ? nRowIdx++ : nRowIdx--;
		if (isRowCycle) {
			nRowIdx = nRowIdx % count;
			nRowIdx = nRowIdx < 0 ? nRowIdx + count : nRowIdx
		} else {
			if (nRowIdx == count) {
				nRowIdx = count - 1
			}
			if (nRowIdx == -1) {
				nRowIdx = 0
			}
		}

		if (count > 0 && me.rowIdx == count - 1) {
			me.colIdx = me.getNextColIndex(colIdx, true)
		}
		return nRowIdx
	},
	getNextColIndex: function(colIdx, isRight) {
		var me = this,
			columns = me.columns,
			length = columns.length,
			nColIdx = colIdx;
		if (isRight) {
			for (var i = colIdx + 1; i < length; i++) {
				if (columns[i].hasEditor) {
					return i
				}
			}
			if (me.isColCycle) {
				for (var i = 0; i < colIdx; i++) {
					if (columns[i].hasEditor) {
						return i
					}
				}
			}
		} else {
			for (var i = colIdx - 1; i >= 0; i--) {
				if (columns[i].hasEditor) {
					return i
				}
			}
			if (me.isColCycle) {
				for (var i = length - 1; i > colIdx; i--) {
					if (columns[i].hasEditor) {
						return i
					}
				}
			}
		}
		return nColIdx
	}

});
