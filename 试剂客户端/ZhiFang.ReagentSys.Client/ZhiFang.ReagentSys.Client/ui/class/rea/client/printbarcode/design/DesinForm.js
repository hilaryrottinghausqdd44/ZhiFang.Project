/**
 * 条码模板设计
 * @author longfc
 * @version 2018-08-08
 */
Ext.define('Shell.class.rea.client.printbarcode.design.DesinForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '条码模板设计',
	formtype: 'add',
	width: 680,
	height: 620,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/',

	buttonDock: "top",
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '5px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	/**客户端电脑上的打印机集合信息*/
	PrinterList: [],

	/**一维条码模板信息*/
	BarcodeModel: null,
	/**一维条码模板集合信息*/
	BarcodeModelList: [],
	/**二维条码模板信息*/
	QRCodeModel: null,
	/**二维条码模板集合信息*/
	QRCodeModelList: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = parseInt(me.width - 20 / me.layout.columns);
		if(me.defaults.width < 185) me.defaults.width = 185;

		//加载一维条码模板组件
		me.BarCodeModel = me.BarCodeModel || Ext.create('Shell.class.rea.client.printbarcode.design.BarCodeModel');
		me.BarcodeModelList = me.BarCodeModel.getModelList();
		//加载二维条码模板组件
		me.QRCodeModel = me.QRCodeModel || Ext.create('Shell.class.rea.client.printbarcode.design.QRCodeModel');
		me.QRCodeModelList = me.QRCodeModel.getModelList();

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'Memo',
			itemId: 'Memo',
			colspan: 4,
			width: 780, // me.defaults.width * 4,
			height: me.height - 80
		});
		return items;
	},
	getCLodop: function(type) {
		var me = this;
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		var LODOP = me.Lodop.getLodop(true);
		if(!LODOP) {
			//JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		return LODOP;
	},
	/**获取客户端电脑上的打印机集合信息*/
	createPrinterList: function(element) {
		var me = this;
		me.PrinterList = [];

		var LODOP = me.getCLodop();
		if(!LODOP || !CLODOP) return;

		var iCount = 0;
		if(CLODOP)
			iCount = CLODOP.GET_PRINTER_COUNT();
		var iIndex = 0;
		for(var i = 0; i < iCount; i++) {
			me.PrinterList.push([iIndex, CLODOP.GET_PRINTER_NAME(i)]);
			iIndex++;
		}
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		me.createPrinterList();

		var items = [];
		items.push({
			fieldLabel: '',
			emptyText: '',
			xtype: 'uxSimpleComboBox',
			itemId: 'BarCodeType',
			allowBlank: false,
			width: 90,
			labelWidth: 0,
			labelAlign: 'right',
			xtype: 'uxSimpleComboBox',
			value: me.DefaultBarCodeType,
			hasStyle: true,
			data: [
				['1', '一维条码', 'color:orange;'],
				['2', '二维条码', 'color:green;']
			],
			listeners: {
				change: function(field, newValue) {
					//me.setDefaultBarCodeType(newValue);
					me.modelTypeLoadData(newValue);
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '模板类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'ModelType',
			allowBlank: false,
			width: 150,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.DefaultBarCodeType == "2" ? me.QRCodeModelList : me.BarcodeModelList,
			value: me.DefaultBarcodeModel,
			listeners: {
				change: function(field, newValue) {
					//me.setDefaultBarcodeModel(newValue);
				}
			}
		}, '-', {
			fieldLabel: '打印机选择',
			emptyText: '打印机选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'PrinterList',
			width: 280,
			labelWidth: 75,
			labelAlign: 'right',
			data: me.PrinterList,
			value: me.DefaultPrinter,
			listeners: {
				change: function(field, newValue) {}
			}
		}, '-', {
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
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	modelTypeLoadData: function(newValue) {
		var me = this;
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType');
		switch(newValue) {
			case "1":
				modelType.getStore().loadData(me.BarcodeModelList);
				modelType.setValue(me.BarcodeModelList[0][0]);
				break;
			default:
				modelType.getStore().loadData(me.QRCodeModelList);
				modelType.setValue(me.QRCodeModelList[0][0]);
				break;
		}
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbar').getComponent('PrinterList');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var reg = new RegExp("<br />", "g");
		data.Memo = data.Memo.replace(reg, "\r\n");
		return data;
	},
	/**条码打印*/
	onBarcodePrint: function(type) {
		var me = this;
		var LODOP = me.getCLodop();
		//条码类型:一维条码:1;二维条码:2;
		var barCodeType = me.getComponent('buttonsToolbar').getComponent('BarCodeType').getValue();
		//模板类型
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		if(!modelType) {
			JShell.Msg.error("请选择打印模板类型后再操作!");
			return;
		}

		//Lodop打印内容字符串数组
		var LodopStr = [];
		var title = "";
		var contentStr = "";
		if(barCodeType == "1") {
			title = me.BarCodeModel.getModelTitle(modelType);
			contentStr = me.BarCodeModel.getModelContent(modelType);
		} else {
			title = me.QRCodeModel.getModelTitle(modelType);
			contentStr = me.QRCodeModel.getModelContent(modelType);
		}

		//模板标题
		LodopStr.push(title);
		LodopStr.push(contentStr);
		//console.log(contentStr);
		eval(LodopStr.join(""));

		//打印机选择
		var printer = me.getPrinter();
		if(printer)
			LODOP.SET_PRINTER_INDEXA(printer.getValue());
		var result = null;
		if(type == 1) { //直接打印
			result = LODOP.PRINT();
			//更新条码打印次数
			if(result != 0) {
				me.onUpdatePrintCount();
			}
		} else if(type == 2) { //预览打印
			result = LODOP.PREVIEW();
		} else if(type == 3) { //维护打印
			result = LODOP.PRINT_SETUP();
		} else if(type == 4) { //设计打印
			result = LODOP.PRINT_DESIGN();
			if(LODOP.CVERSION) CLODOP.On_Return = function(TaskID, Value) {
				me.getComponent('Memo').setValue(Value);
			};
			me.getComponent('Memo').setValue(result);
		}
	}
});