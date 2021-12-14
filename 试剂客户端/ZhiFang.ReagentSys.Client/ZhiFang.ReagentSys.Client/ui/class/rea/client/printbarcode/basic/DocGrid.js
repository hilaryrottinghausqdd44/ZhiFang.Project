/**
 * 某一入库单的条码信息
 * @author longfc
 * @version 2018-04-25
 */
Ext.define('Shell.class.rea.client.printbarcode.basic.DocGrid', {
	extend: 'Shell.class.rea.client.printbarcode.basic.Grid',
	title: '货品条码信息',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '',

	/**默认加载*/
	defaultLoad: true,
	/**获取条码信息的业务ID*/
	PK: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: "",
	hiddenSearchComboBox: true,
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',
	
	/**需求调整：在选择条码类型时，增加自定义条码*/
	selectCustom: '/SingleTableService.svc/ST_UDTO_SearchBLodopTempletByHQL?isPlanish=true&fields=BLodopTemplet_CName,BLodopTemplet_TemplateCode,BLodopTemplet_TypeCode,BLodopTemplet_PaperType,BLodopTemplet_PrintingDirection,BLodopTemplet_PaperWidth,BLodopTemplet_PaperHigh,BLodopTemplet_PaperUnit,BLodopTemplet_IsUse,BLodopTemplet_DispOrder,BLodopTemplet_Id',
	
	features: [Ext.create("Ext.grid.feature.Grouping", {
		groupByText: "按当前列进行分组",
		showGroupsText: "显示分组",
		groupHeaderTpl: '<tpl for="rows[0].data">' +
			'{ReaGoodsPrintBarCodeVO_GoodsName};一级分类:{ReaGoodsPrintBarCodeVO_GoodsClass}' +
			';数量:{ReaGoodsPrintBarCodeVO_GoodsQty}{ReaGoodsPrintBarCodeVO_GoodsUnit};规格:{ReaGoodsPrintBarCodeVO_UnitMemo}' +
			'</tpl>' +
			'(条码数:{rows.length} {[values.rows.length > 1 ? "条" : "0"]})',
		//startCollapsed: true,//是否收缩
		hideGroupedColumn: true,
		enableGroupingMenu: false,
	})],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//加载一维条码模板组件
		me.BarCodeModel = me.BarCodeModel || Ext.create('Shell.class.rea.client.printbarcode.BarCodeModel');
		me.BarcodeModelList = me.BarCodeModel.getModelList();
		//加载二维条码模板组件
		me.QRCodeModel = me.QRCodeModel || Ext.create('Shell.class.rea.client.printbarcode.QRCodeModel');
		me.QRCodeModelList = me.QRCodeModel.getModelList();
		
		// 需求调整：加载自定义模板组件，从数据库获得
		// 创建打印组件
		Ext.create('Shell.ux.print.Print').init(function(print){
			me.ZFPrint = print;
		});
		Ext.create('Shell.ux.print.Model').init(function(Model){
			me.ZFModel = Model;
		});
		// 回调函数式
		me.getCustomModelList(function(list1,list2) {
			me.CustomModelList1 = list1;
			me.CustomModelList2 = list2;
		});
		
		me.initDefaultInfo();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			pluginId: me.cellpluginId,
			clicksToEdit: 1
		});
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			groupField: 'ReaGoodsPrintBarCodeVO_GroupValue',
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if(data.success) {
						var info = data.value;
						if(info) {
							var type = Ext.typeOf(info);
							if(type == 'object') {
								info = info;
							} else if(type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaGoodsPrintBarCodeVO_LotNo',
			text: '货品批号',
			//hidden: true,
			width: 95,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_PrintCount',
			text: '已打印',
			width: 50,
			align: 'center',
			renderer: function(value, meta, record) {
				var v = '否';
				var style = 'font-weight:bold;';
				meta.style = style;
				if(value && parseInt(value) > 0) {
					style = style + "background-color:#ffffff;color:#1c8f36;";
					v = '是';
				} else {
					style = style + "background-color:#ffffff;color:red;";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_BarCodeType',
			text: '条码类型',
			//hidden: true,
			width: 75,
			menuDisabled: true,
			hideable: false,
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
			dataIndex: 'ReaGoodsPrintBarCodeVO_GoodsUnit',
			text: '单位',
			//hidden: true,
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_DispOrder',
			text: '顺序号',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_UsePackSerial',
			sortable: false,
			width: 120,
			text: '一维条码值',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_UsePackQRCode',
			sortable: false,
			width: 475,
			text: '二维条码值',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_GoodsClass',
			text: '一级分类',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_ReaGoodsNo',
			text: '货品编码',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_CenOrgGoodsNo',
			text: '供货商货品码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_ProdOrgNo',
			text: '品牌编号',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_CompOrgNo',
			text: '供应商编号',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_ProdGoodsNo',
			text: '厂商货品码',
			width: 85,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_GoodsName',
			text: '货品名称',
			width: 150,
			hidden: true,
			columnCountKey: me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsPrintBarCodeVO_BarCodeType");
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
			dataIndex: 'ReaGoodsPrintBarCodeVO_SName',
			text: '货品简称',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_EName',
			text: '英文名称',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_ShortCode',
			text: '货品代码',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_UnitMemo',
			text: '包装规格',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_SaleDocNo',
			text: '单据号',
			hidden: true,
			hideable: false,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_InvalidDate',
			text: '有效期至',
			width: 80,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_Price',
			text: '单价',
			width: 70,
			type: 'float',
			align: 'right',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_GoodsQty',
			text: '条码数量',
			hidden: true,
			width: 70,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_ProdDate',
			text: '生产日期',
			align: 'center',
			width: 90,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_BiddingNo',
			text: '招标号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_RegisterNo',
			sortable: false,
			text: '注册证编号',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_RegisterInvalidDate',
			text: '注册证有效期',
			width: 85,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_PUsePackSerial',
			sortable: false,
			width: 105,
			hidden: true,
			text: '系统父使用盒条码',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			defaultRenderer: true,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_BDocID',
			sortable: false,
			text: '业务主单ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_BDtlID',
			sortable: false,
			text: '业务明细ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_GoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsPrintBarCodeVO_GroupValue',
			sortable: false,
			text: '分组',
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;

		me.createPrinterList();

		var items = ['refresh', '-'];
		items.push({
			fieldLabel: '',
			emptyText: '条码类型选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'BarCodeType',
			allowBlank: false,
			width: 100,
			labelWidth: 0,
			labelAlign: 'right',
			value: me.DefaultBarCodeType,
			hasStyle: true,
			data: [
				['1', '一维条码', 'color:orange;'],
				['2', '二维条码', 'color:green;'],
				// ['3', '自定义条码', 'color:pink;'],// 调整：添加自定义模板
				['3', '自定义一维条码', 'color:pink;'], // 调整：添加自定义模板
				['4', '自定义二维条码', 'color:skyblue;']// 调整：添加自定义模板
			],
			listeners: {
				change: function(field, newValue) {
					// console.log(field);
					me.setDefaultBarCodeType(newValue);
					me.modelTypeLoadData(newValue);
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '模板类型选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'ModelType',
			allowBlank: false,
			width: 155,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.getDataOfModelType(),
			value: me.DefaultBarcodeModel,
			listeners: {
				change: function(field, newValue) {
					// console.log(field);
					me.setDefaultBarcodeModel(newValue);
				}
			}
		}, '-', {
			fieldLabel: '',
			emptyText: '打印机选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'PrinterList',
			width: 200,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.PrinterList,
			value: me.DefaultPrinter,
			listeners: {
				change: function(field, newValue) {
					me.setDefaultPrinter(newValue);
				}
			}
		});
		if(!me.hiddenSearchComboBox)
			items.push('-');

		items.push({
			fieldLabel: '',
			emptyText: '过滤条件选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'cboSearch',
			width: 120,
			labelWidth: 0,
			labelAlign: 'right',
			value: '',
			hasStyle: true,
			hidden: me.hiddenSearchComboBox,
			data: [
				['', '全部显示', 'color:orange;'],
				['1', '仅大包装盒条码', 'color:orange;'],
				['2', '仅小包装盒条码', 'color:green;']
			],
			listeners: {
				select: function(field, record) {
					me.onSearch();
				}
			}
		});

		items.push('-', {
			xtype: 'checkbox',
			boxLabel: '批条码单打',
			itemId: 'printOne',
			checked: false
		}, {
			iconCls: 'button-print',
			text: '直接打印',
			tooltip: '直接打印条码',
			handler: function(btn, e) {
				// 原来的代码
				// me.onBarcodePrint(1);
				if(me.DefaultBarCodeType == '1' || me.DefaultBarCodeType == '2') {
					me.onBarcodePrint(1);
				} else if(me.DefaultBarCodeType == '3' || me.DefaultBarCodeType == '4') {
					me.onCustomBarcodePreview(1);
				}
				
			}
		}, {
			iconCls: 'button-print',
			text: '浏览打印',
			tooltip: '浏览打印条码',
			handler: function(btn, e) {
				if(me.DefaultBarCodeType == '1' || me.DefaultBarCodeType == '2') {
					me.onBarcodePrint(2);
				} else if(me.DefaultBarCodeType == '3' || me.DefaultBarCodeType == '4') {
					me.onCustomBarcodePreview(2);
				}
			}
		}, '-', {
			xtype: 'splitbutton',
			textAlign: 'left',
			iconCls: 'button-print',
			text: '条码打印',
			hidden: true,
			handler: function(btn, e) {
				btn.overMenuTrigger = true;
				btn.onClick(e);
			},
			menu: [{
				text: '直接打印',
				iconCls: 'button-print',
				listeners: {
					click: function(but) {
						me.onBarcodePrint(1);
					}
				}
			}, {
				text: '浏览打印',
				iconCls: 'button-print',
				listeners: {
					click: function(but) {
						me.onBarcodePrint(2);
					}
				}
			}, {
				text: '维护打印',
				iconCls: 'button-print',
				listeners: {
					click: function(but) {
						me.onBarcodePrint(3);
					}
				}
			}, {
				text: '设计打印',
				iconCls: 'button-print',
				listeners: {
					click: function(but) {
						me.onBarcodePrint(4);
					}
				}
			}]
		});
		return items;
	},
	/**获取批条码单打组件*/
	getPrintOne: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('printOne');
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('PrinterList');
	},
	// 回调函数式----添加自定义一维条码和二维条码
	getCustomModelList: function(callback) {
			var me = this;
			var url = (me.selectCustom.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectCustom; 
			JShell.Server.get(url, function(data) {
				if(data.success) {
					var list = data.value.list;
					if(list&&list.length > 0) { // 有自定义模板才能向下进行
						var modelList1 = []; // 存放一维条码合集
						var modelList2 = []; // 存放二维条码合集
						Ext.Array.each(list,function(item) {
							// 存放['模板id','模板名称（模板类型）']
							var arr1 = [];
							var arr2 = [];
							// 判断该条码模板是否使用
							var isUse = item.BLodopTemplet_IsUse;
							if(isUse == 'true') { // 只将用户使用的模板显示
								var BLodopTemplet_TypeCode = item.BLodopTemplet_TypeCode;
								if(BLodopTemplet_TypeCode == 1) { // 一维
									// 模板id
									// var BLodopTemplet_Id = item.BLodopTemplet_Id;
									// arr1.push(BLodopTemplet_Id);
									// 模板代码
									var BLodopTemplet_TemplateCode = item.BLodopTemplet_TemplateCode;
									// 只是进行了部分模板设计，不清楚还有没有其他的字符在里面，目前只看到了</br>
									var noBr_TemplateCode = BLodopTemplet_TemplateCode.replace(/<\/br>/g, "");
									arr1.push(noBr_TemplateCode);
									// 模板名称
									var BLodopTemplet_CName = item.BLodopTemplet_CName;
									// 模板名称+模板类型
									var nameAndType = BLodopTemplet_CName + '(入库一维条码)';
									arr1.push(nameAndType);
									modelList1.push(arr1);
								}else { // 二维
									// 模板id
									// var BLodopTemplet_Id = item.BLodopTemplet_Id;
									// arr2.push(BLodopTemplet_Id);
									// 模板代码
									var BLodopTemplet_TemplateCode = item.BLodopTemplet_TemplateCode;
									var noBr_TemplateCode = BLodopTemplet_TemplateCode.replace(/<\/br>/g, "");
									arr2.push(noBr_TemplateCode);
									// 模板名称
									var BLodopTemplet_CName = item.BLodopTemplet_CName;
									// 模板名称+模板类型
									var nameAndType = BLodopTemplet_CName + '(入库二维条码)';
									arr2.push(nameAndType);
									modelList2.push(arr2);
								}
							} 
						});
						callback(modelList1,modelList2);
						
					} else {
						JShell.Msg.alert('需要进行自定义模板后才能使用！');
						return;
					}
					
				}
			},false);
		},
	/**
	 * 获取模板类型这个下拉框组件的data值
	 * */ 
	getDataOfModelType: function() {
		var me = this;
		if(me.DefaultBarCodeType == '1') {
			return me.BarcodeModelList;
		}else if(me.DefaultBarCodeType == '2') {
			return me.QRCodeModelList;
		}else if(me.DefaultBarCodeType == '3') {
			return me.CustomModelList1;
		} else if(me.DefaultBarCodeType == '4'){
			return me.CustomModelList2;
		}
	}
	
});