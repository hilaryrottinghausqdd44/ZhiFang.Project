/**
 * 查看某一试剂的库存变化操作记录
 * @author longfc
 * @version 2018-03-15
 */
Ext.define('Shell.class.rea.client.qtyoperation.ShowGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',

	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '库存操作记录',
	width: 800,
	height: 500,

	/**试剂ID*/
	GoodsID: null,
	/**查询数据*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlOperationByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtlOperation_DataAddTime',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	OperTypeList: [],
	/**操作记录操作类型枚举*/
	OperTypeEnum: {},
	/**操作记录操作类型背景颜色枚举*/
	OperTypeBGColorEnum: {},
	OperTypeFColorEnum: {},
	OperTypeBGColorEnum: {},
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',
	/**用户UI配置Key*/
	userUIKey: 'qtyoperation.ShowGrid',
	/**用户UI配置Name*/
	userUIName: "库存操作记录列表",

	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: me.cellpluginId
		});
		me.getQtyDtlOperationOperTypeList();
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
			dataIndex: 'ReaBmsQtyDtlOperation_BarCodeType',
			text: '条码类型',
			width: 60,
			hidden: true,
			renderer: function(value, meta, record) {
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
			dataIndex: 'ReaBmsQtyDtlOperation_CompanyName',
			text: '供应商',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_OperTypeID',
			text: '操作类型Id',
			width: 60,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_OperTypeName',
			text: '操作类型',
			width: 90,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = record.get("ReaBmsQtyDtlOperation_OperTypeID");
				if(me.OperTypeEnum != null)
					value = me.OperTypeEnum[v];
				var bColor = "";
				if(me.OperTypeBGColorEnum != null)
					bColor = me.OperTypeBGColorEnum[v];
				var fColor = "";
				if(me.OperTypeFColorEnum != null)
					fColor = me.OperTypeFColorEnum[v];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				meta.style = style;
				return value;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDocNo',
			text: '单据号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDocID',
			text: '单据Id',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_BDtlID',
			text: '明细Id',
			width: 145,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_CreaterName',
			text: '操作者',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_DataAddTime',
			text: '操作时间',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsID',
			text: '货品Id',
			width: 145,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsName',
			text: '货品名称',
			width: 130,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtlOperation_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsQty',
			text: '变化数量',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_GoodsUnit',
			text: '规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Price',
			text: '单价',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_SumTotal',
			text: '总计金额',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_LotNo',
			text: '货品批号',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_ProdDate',
			text: '生产日期',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_InvalidDate',
			text: '有效期至',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_StorageName',
			text: '库房',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_PlaceName',
			text: '货架',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtlOperation_Id',
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
		var items = ['refresh'];

		items.push('-', {
			xtype: 'button',
			text: '近30天',
			tooltip: '近30天',
			itemId: "Day30",
			enableToggle: false,
			handler: function(button, e) {
				me.onQuickSearch(-30, button);
			}
		}, '-', {
			xtype: 'button',
			text: '近60天',
			tooltip: '近60天',
			itemId: "Day60",
			enableToggle: false,
			handler: function(button, e) {
				me.onQuickSearch(-60, button);
			}
		}, '-', {
			xtype: 'button',
			text: '近90天',
			tooltip: '近90天',
			itemId: "Day90",
			enableToggle: false,
			handler: function(button, e) {
				me.onQuickSearch(-90, button);
			}
		}, '-', {
			xtype: 'button',
			text: '全部',
			tooltip: '全部',
			itemId: "All",
			enableToggle: false,
			handler: function(button, e) {
				me.onQuickSearch(null, button);
			}
		});

		items.push('-', {
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 55,
			labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});

		return items;
	},
	/**按日期快捷查询*/
	onQuickSearch: function(day, button) {
		var me = this;
		if(!me.validDateType()) return;
		me.onSetDateArea(day);
		me.setButtonDayToggle(button);
		me.onSearch();
	},
	/**@description 验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		return true;
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;

		var dateareaToolbar = me.getComponent('buttonsToolbar'),
			date = dateareaToolbar.getComponent('date');
		var dateAreaValue = me.calcDateArea(day);
		if(date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) {
			return {
				start: "",
				end: ""
			};
		}
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**按日期按钮点击后样式设置*/
	setButtonDayToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if(item && item.xtype == "button") item.toggle(false);
		});
		button.toggle(true);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		//试剂ID	
		if(me.GoodsID)
			where.push('reabmsqtydtloperation.GoodsID=' + me.GoodsID);

		var date = buttonsToolbar.getComponent('date');
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push('reabmsqtydtloperation.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push('reabmsqtydtloperation.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		var search = buttonsToolbar.getComponent('Search');
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;

		me.getView().update();
		//试剂ID	
		if(!me.GoodsID) {
			me.store.removeAll();
			return;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取库存操作记录操作类型*/
	getQtyDtlOperationOperTypeList: function(callback) {
		var me = this;
		if(me.OperTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsQtyDtlOperationOperType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.OperTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsQtyDtlOperationOperType.length > 0) {
						me.OperTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsQtyDtlOperationOperType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.OperTypeFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
								me.OperTypeBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.OperTypeEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.OperTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	}
});