/**
 * 条码打印基础列表
 * @author longfc
 * @version 2018-04-25
 */
Ext.define('Shell.class.rea.client.printbarcode.basic.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '货品条码信息',
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**默认选中*/
	autoSelect: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**获取条码信息的业务ID*/
	PK: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: null,
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
	
	/**需求调整：自定义条码模板信息*/
	/**自定义条码条码模板集合信息*/
	CustomModelList1: [],
	CustomModelList2: [],
	
	/**默认选择条码类型*/
	DefaultBarCodeType: '',
	/**默认条码模板值*/
	DefaultBarcodeModel: "",
	/**默认选择的打印机*/
	DefaultPrinter: "",
	/**默认每页数量*/
	defaultPageSize: 20,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[10,10],[20,20],[50,50],[100,100],[200,200]
	],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//me.store.removeAll();
		me.getView().update();
		if(!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "获取条码的参数值(业务ID)为空!");
			me.getView().update(error);
			return false;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
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
	/**条码打印*/
	onBarcodePrint: function(type) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
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
		if(barCodeType == "1") {
			title = me.BarCodeModel.getModelTitle(modelType);
		} else if(barCodeType == '2'){ // 调整
			title = me.QRCodeModel.getModelTitle(modelType);
		} 
		//模板标题
		LodopStr.push(title);
		LodopStr.push(me.getBarcodeContentByCheckedRecords());

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
		}
	},
	/**根据选中的数据产生条码*/
	getBarcodeContentByCheckedRecords: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			list = [];

		for(var i = 0; i < len; i++) {
			list.push(records[i].data);
		}
		return me.getBarcodeContentByRecords(list);
	},
	/**
	 * 需求调整：获取选中的数据
	 * 
	 * */
	getCheckedRecords: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			list = [];
		
		for(var i = 0; i < len; i++) {
			list.push(records[i].data);
		}
		return list;
	},
	/**获取批条码单打组件*/
	getPrintOne: function() {
		var me = this;
		return me.getComponent('buttonsToolbarPrint').getComponent('printOne');
	},
	/**获取打印机选择*/
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbarPrinter').getComponent('PrinterList');
	},
	/**根据数据产生条码*/
	getBarcodeContentByRecords: function(list) {
		var me = this,
			printOne = me.getPrintOne().getValue(),
			len = list.length,
			content = [];
		if(printOne == undefined || printOne == null) {
			printOne = false;
		}
		//条码类型:一维条码:1;二维条码:2;
		var barCodeType2 = me.getComponent('buttonsToolbar').getComponent('BarCodeType').getValue();
		//模板类型
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		var printMode = {};
		if(barCodeType2 == "1") {
			printMode = me.BarCodeModel;
		} else {
			printMode = me.QRCodeModel;
		}
		for(var i = 0; i < len; i++) {
			var rec = list[i];
			var usePackSerial = rec.ReaGoodsPrintBarCodeVO_UsePackSerial;
			if(barCodeType2 == "2") { 
				usePackSerial = rec.ReaGoodsPrintBarCodeVO_UsePackQRCode;
			}
			//条码不存在的不打印
			if(!usePackSerial) continue;

			//打印的数量
			var num = 0;
			var barCodeType = "" + rec.ReaGoodsPrintBarCodeVO_BarCodeType;
			//批条码只打印一份
			if(barCodeType == "0") {
				if(printOne == true) {
					num = 1;
				} else {
					num = parseFloat(rec.ReaGoodsPrintBarCodeVO_GoodsQty);
				}
			} else if(barCodeType == "1") {
				num = 1;
			}
			if(num <= 0) continue;

			for(var j = 0; j < num; j++) {
				//二维码模板
				var barcode = printMode.getModelContent(modelType, {
					GoodsName: rec.ReaGoodsPrintBarCodeVO_GoodsName, //货品名称
					SName: rec.ReaGoodsPrintBarCodeVO_SName, //简称
					EName: rec.ReaGoodsPrintBarCodeVO_EName, //货品英文名称
					ShortCode: rec.ReaGoodsPrintBarCodeVO_ShortCode, //货品代码
					InvalidDate: JShell.Date.toString(rec.ReaGoodsPrintBarCodeVO_InvalidDate, true), //效期
					LotNo: rec.ReaGoodsPrintBarCodeVO_LotNo, //批号
					UnitMemo: rec.ReaGoodsPrintBarCodeVO_UnitMemo, //货品规格
					ProdOrgNo: rec.ReaGoodsPrintBarCodeVO_ProdOrgNo, //品牌编号
					ReaGoodsNo: rec.ReaGoodsPrintBarCodeVO_ReaGoodsNo, //货品码(机构内部编号)
					ProdGoodsNo: rec.ReaGoodsPrintBarCodeVO_ProdGoodsNo, //厂商货品编码
					CenOrgGoodsNo: rec.ReaGoodsPrintBarCodeVO_CenOrgGoodsNo, //供货商货品码
					GoodsNo: rec.ReaGoodsPrintBarCodeVO_GoodsNo, //货品平台码
					CompOrgNo: rec.ReaGoodsPrintBarCodeVO_CompOrgNo, //供应商编号
					SaleDocNo: rec.ReaGoodsPrintBarCodeVO_SaleDocNo, //单据号
					GoodsClass: rec.ReaGoodsPrintBarCodeVO_GoodsClass, //一级分类
					Barcode: usePackSerial //条码
				});
				content.push(barcode);
			}	
		}
		return content.join("");
	},
	/**条码打印后根据选中的条码数据更新条码打印次数*/
	onUpdatePrintCount: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			boxList = [],
			lotList = [];

		for(var i = 0; i < len; i++) {
			var barCodeType = "" + records[i].get("ReaGoodsPrintBarCodeVO_BarCodeType");
			var id = records[i].get("ReaGoodsPrintBarCodeVO_Id");
			if(barCodeType == "1")
				boxList.push(id);
			else if(barCodeType == "0")
				lotList.push(id);
		}
		if(boxList.length <= 0 && lotList <= 0) return;

		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_UpdatePrintCount");
		var params = {
			"boxList": boxList,
			"lotList": lotList,
			"lotType": me.lotType
		};

		params = Ext.encode(params);
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error('更新条码打印次数出错！' + data.msg);
			}
		});
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
	/**初始化默认信息*/
	initDefaultInfo: function() {
		var me = this;

		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		//获取还原当前用户默认选择的条码类型
		var key2 = "printbarcode.BarCodeType." + userId;
		var barcodeModel = JcallShell.LocalStorage.get(key2);
		if(barcodeModel) {
			barcodeModel = JcallShell.JSON.decode(barcodeModel);
			me.DefaultBarCodeType = barcodeModel.Value;
		} else {
			me.DefaultBarCodeType = "2";
		}

		//获取还原当前用户默认选择的条码模板
		var key2 = "printbarcode.BarcodeModel." + userId;
		var barcodeModel = JcallShell.LocalStorage.get(key2);
		if(barcodeModel) {
			barcodeModel = JcallShell.JSON.decode(barcodeModel);
			me.DefaultBarcodeModel = barcodeModel.Value;
		}

		//获取还原当前用户默认选择的打印机
		var key = "printbarcode.Printer." + userId;
		var printer = JcallShell.LocalStorage.get(key);
		if(printer) {
			printer = JcallShell.JSON.decode(printer);
			me.DefaultPrinter = printer.Value;
		}
	},
	/**缓存当前用户选择的条码类型*/
	setDefaultBarCodeType: function(newValue) {
		var me = this;
		me.DefaultBarCodeType = newValue;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "printbarcode.BarCodeType." + userId;
		var params = {
			"Value": me.DefaultBarCodeType
		};
		params = JcallShell.JSON.encode(params);
		JcallShell.LocalStorage.set(key, params);
	},
	/**缓存当前用户选择的条码模板*/
	setDefaultBarcodeModel: function(newValue) {
		var me = this;
		me.DefaultBarcodeModel = newValue;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "printbarcode.BarcodeModel." + userId;
		var params = {
			"Value": me.DefaultBarcodeModel
		};
		params = JcallShell.JSON.encode(params);
		JcallShell.LocalStorage.set(key, params);
	},
	/**缓存当前用户选择的打印机*/
	setDefaultPrinter: function(newValue) {
		var me = this;
		me.DefaultPrinter = newValue;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "printbarcode.Printer." + userId;
		var params = {
			"Value": me.DefaultPrinter
		};
		params = JcallShell.JSON.encode(params);
		JcallShell.LocalStorage.set(key, params);
	},
	modelTypeLoadData: function(newValue) {
		var me = this;
		var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType');
		switch(newValue) {
			case "1":
				modelType.getStore().loadData(me.BarcodeModelList);
				modelType.setValue(me.BarcodeModelList[0][0]);
				break;
			case "3": // 需求调整自定义一维模板
				modelType.getStore().loadData(me.CustomModelList1);
				modelType.setValue(me.CustomModelList1[0][0]);
				break;
			case "4": // 需求调整自定义二维模板
				modelType.getStore().loadData(me.CustomModelList2);
				modelType.setValue(me.CustomModelList2[0][0]);
				break;
			default:
				modelType.getStore().loadData(me.QRCodeModelList);
				modelType.setValue(me.QRCodeModelList[0][0]);
				break;
		}
	},
	/**
	 * 需求调整：自定义条码模板且使用封装好的组件（ux下print）
	 * 浏览打印
	 * */
	 onCustomBarcodePreview: function(type) {
		 var me = this,
		 	records = me.getSelectionModel().getSelection(),
			printOne = me.getPrintOne().getValue(),
		 	len = records.length;
			// 至少选择一条数据的判断
		 if(len == 0) {
		 	JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
		 	return;
		 }
		 var LODOP = me.getCLodop();
		 //打印机选择
		 var printer = me.getPrinter();
		 if(printer)
		 	LODOP.SET_PRINTER_INDEXA(printer.getValue());
		
		 //条码类型:一维条码:3;二维条码:4;
		 var barCodeType = me.getComponent('buttonsToolbar').getComponent('BarCodeType').getValue();
		 //模板
		 var modelTypeCombo = me.getComponent('buttonsToolbar').getComponent('ModelType');
		 var title = modelTypeCombo.displayTplData[0].text; // 下拉框的text值，用做打印的标题
		 var modelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		 // 当数据库中模板信息为空时给用户的提示
		 if(!modelType) {
		 	JShell.Msg.error("请选择打印模板类型后再操作!");
		 	return;
		 }
		 if(printOne == undefined || printOne == null) { 
		 	printOne = false;
		 }
		 
		 // 进入正题
		 var checkedDatas = me.getCheckedRecords(); // 勾选的数据集数组
		 var contentList = []; // 用来存放拼接后的字符串
		 for(var i = 0; i < len; i++) { // 进行数据和模板拼接
		 	var rec = checkedDatas[i];
		 	var usePackSerial = rec.ReaGoodsPrintBarCodeVO_UsePackSerial;
		 	if(barCodeType == "4") { // 添加条码类型为“4”，其本质还是二维码
		 		usePackSerial = rec.ReaGoodsPrintBarCodeVO_UsePackQRCode;
		 	}
		 	//条码不存在的不打印
		 	if(!usePackSerial) continue;
		 	
		 	//打印的数量
			/**由于批条码成一条数据展示，其数量与’条码数量‘这个字段有关*/
		 	var num = 0;
		 	var barCodeTypeOfRec = "" + rec.ReaGoodsPrintBarCodeVO_BarCodeType;
		 	//批条码只打印一份
		 	if(barCodeTypeOfRec == "0") { // 0：批条码
		 		if(printOne == true) {
		 			num = 1;
		 		} else {
		 			num = parseFloat(rec.ReaGoodsPrintBarCodeVO_GoodsQty);
		 		}
		 	} else if(barCodeTypeOfRec == "1") { // 1：盒条码
		 		num = 1;
		 	}
		 	if(num <= 0) continue;
			for(var j = 0;j < num; j++) {
				var content = me.ZFModel.getLodopContentByModel(modelType,{
						GoodsName: rec.ReaGoodsPrintBarCodeVO_GoodsName, //货品名称
						SName: rec.ReaGoodsPrintBarCodeVO_SName, //简称
						EName: rec.ReaGoodsPrintBarCodeVO_EName, //货品英文名称
						ShortCode: rec.ReaGoodsPrintBarCodeVO_ShortCode, //货品代码
						InvalidDate: JShell.Date.toString(rec.ReaGoodsPrintBarCodeVO_InvalidDate, true), //效期
						LotNo: rec.ReaGoodsPrintBarCodeVO_LotNo, //批号
						UnitMemo: rec.ReaGoodsPrintBarCodeVO_UnitMemo, //货品规格
						ProdOrgNo: rec.ReaGoodsPrintBarCodeVO_ProdOrgNo, //品牌编号
						ReaGoodsNo: rec.ReaGoodsPrintBarCodeVO_ReaGoodsNo, //货品码(机构内部编号)
						ProdGoodsNo: rec.ReaGoodsPrintBarCodeVO_ProdGoodsNo, //厂商货品编码
						CenOrgGoodsNo: rec.ReaGoodsPrintBarCodeVO_CenOrgGoodsNo, //供货商货品码
						GoodsNo: rec.ReaGoodsPrintBarCodeVO_GoodsNo, //货品平台码
						CompOrgNo: rec.ReaGoodsPrintBarCodeVO_CompOrgNo, //供应商编号
						SaleDocNo: rec.ReaGoodsPrintBarCodeVO_SaleDocNo, //单据号
						GoodsClass: rec.ReaGoodsPrintBarCodeVO_GoodsClass, //一级分类
						Barcode: usePackSerial //条码
					});
				contentList.push(content);	
				// me.ZFPrint.model.preview(content,'test');
			}
		 }
		 if(type == 1) { //直接打印: 将列表拆开一个一个任务的去打印，效率较高
			Ext.Array.each(contentList, function(item,index) {
				me.ZFPrint.model.print(item,title + (index + 1));
				// if(index < contentList.length) {
					//更新条码打印次数
				me.onUpdatePrintCount();	
				// }
			})
		 } else if(type == 2) { //预览打印: 先将列表整合成一个字符串，再去浏览
				// var str = contentList.join("");
				me.ZFPrint.model.preview(contentList,title); // 这里只有一条数据？？？
		 } 
	 }
	 
});