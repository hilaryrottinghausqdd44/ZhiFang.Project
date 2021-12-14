/**
 * 条码模板设计
 * @author longfc
 * @version 2018-08-08
 */
Ext.define('Shell.class.rea.client.printbarcode.design.QRCodeModel', {
	/**条码模板类型_本地记录最后一次的选择*/
	_BarcodeModelType_LocalStorageName: 'BarcodeModelType',
	/**
	 *  intOrient：
		打印方向及纸张类型，数字型，
		1---纵(正)向打印，固定纸张；
		2---横向打印，固定纸张；
		3---纵(正)向打印，宽度固定，高度按打印内容的高度自适应；
		0(或其它)----打印方向由操作者自行选择或按打印机缺省设置；
	 * */
	//SET_PRINT_PAGESIZE(intOrient,intPageWidth,intPageHeight,strPageName)设定纸张大小
	//ADD_PRINT_BARCODE(Top,Left,Width,Height,BarCodeType,BarCodeValue);
	//ADD_PRINT_TEXT(intTop,intLeft,intWidth,intHeight,strContent)增加纯文本项

	/**模板1(65mm*40mm)*/
	_Model_1: {
		/**类型名称*/
		Name: '模板1(65mm*40mm)',
		/**类型编号*/
		Type: '1',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"65mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"65mm","40mm","试剂系统_供货单_打印条码");LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_TEXT("5mm","5mm","34mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("17mm","12mm","30mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","编码:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("20mm","12mm","30mm","3mm","03141071190");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("23mm","12mm","34mm","3mm","2018-08-29");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","30mm","6mm","3333");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("32mm","5mm","34mm","5mm","2018-08-08");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",12);LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("5mm","39mm","21mm","5mm","");LODOP.SET_PRINT_STYLEA(0,"Alignment",2);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_BARCODE("10mm","34mm","24.36mm","24.36mm","QRCode","ZFRP|1||03141071190|3333|2018-08-29|101823|4682292261307023151|1|5|盒");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","L");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("32mm","39mm","21mm","5mm","");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",12);LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");LODOP.SET_PRINT_STYLEA(0,"Alignment",2);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板2(60mm*40mm)*/
	_Model_2: {
		/**类型名称*/
		Name: '模板2(60mm*40mm)',
		/**类型编号*/
		Type: '2',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("17mm","12mm","25mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","编码:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("20mm","12mm","25mm","3mm","03141071190");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("23mm","12mm","25mm","3mm","2018-08-29");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","25mm","6mm","3333");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("32mm","5mm","29mm","5mm","2018-08-08");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",12);LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("5mm","34mm","21mm","5mm","");LODOP.SET_PRINT_STYLEA(0,"Alignment",2);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_BARCODE("10mm","33mm","26.36mm","26.36mm","QRCode","ZFRP|1||03141071190|3333|2018-08-29|101823|4682292261307023151|1|5|盒");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","L");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("32mm","34mm","21mm","5mm","");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",12);LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");LODOP.SET_PRINT_STYLEA(0,"Alignment",2);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(48mm*32mm)*/
	_Model_3: {
		/**类型名称*/
		Name: '模板3(48mm*32mm)',
		/**类型编号*/
		Type: '3',
		/**标题*/
		Title: 'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_TEXT("1mm","2mm","24mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("15mm","2mm","8mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",6);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("15mm","7mm","20mm","3mm","2018-08-29");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",6);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","2mm","8mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",6);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","7mm","20mm","6mm","3333");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",6);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("23mm","2mm","25mm","5mm","2018-08-08");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_BARCODE("6mm","22mm","28.36mm","28.36mm","QRCode","ZFRP|1||03141071190|3333|2018-08-29|101823|4682292261307023151|1|5|盒");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","L");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("23mm","25mm","21mm","5mm","");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",12);LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");LODOP.SET_PRINT_STYLEA(0,"Alignment",2);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**获取打印内容_模板1*/
	_getModelContent_1: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_1.Type)].Content;
		var Count = "";
		var BarcodeDate = JShell.Date.toString(new Date(), true);

		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g, data.UnitMemo);
		//厂商货品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g, data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g, data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g, data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g, BarcodeDate);

		//货品类型
		barcode = barcode.replace(/\{GoodsClass\}/g, data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g, Count);
		//console.log("Barcode:"+data.Barcode);
		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取打印内容_模板2*/
	_getModelContent_2: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_2.Type)].Content;

		var Count = "";
		var BarcodeDate = JShell.Date.toString(new Date(), true);

		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g, data.UnitMemo);
		//厂商货品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g, data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g, data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g, data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g, BarcodeDate);

		//货品类型
		barcode = barcode.replace(/\{GoodsClass\}/g, data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g, Count);

		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取打印内容_模板3*/
	_getModelContent_3: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_3.Type)].Content;

		var Count = "";
		var BarcodeDate = JShell.Date.toString(new Date(), true);

		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g, data.UnitMemo);
		//厂商货品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g, data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g, data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g, data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g, BarcodeDate);

		//货品类型
		barcode = barcode.replace(/\{GoodsClass\}/g, data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g, Count);

		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},

	/**获取二维码版本*/
	getQRCodeVersion: function(value) {
		var len = 0, //value.length
			result = {
				QRCodeVersion: 5
			}; //版本5

		len = value.replace(/[^\x00-\xff]/g, 'xxx').length;
		if(len > 106 && len <= 154) { //版本7
			result.QRCodeVersion = 7;
		}
		//console.log("len:"+len);
		if(len <= 106) { // 84 容错L
			result.QRCodeErrorLevel = "L";
		} else { //容错M
			result.QRCodeErrorLevel = "M";
		}
		return result;
	},

	/**获取打印内容*/
	getModelContent: function(type, data) {
		var me = this,
			barcode = "";

		switch(type) {
			case me._Model_1.Type:
				barcode = me._getModelContent_1(data);
				break;
			case me._Model_2.Type:
				barcode = me._getModelContent_2(data);
				break;
			case me._Model_3.Type:
				barcode = me._getModelContent_3(data);
				break;
			default:
				break;
		}
		return barcode;
	},
	/**获取模板列表*/
	getModelList: function() {
		var me = this,
			list = [];

		list.push([me._Model_1.Type, me._Model_1.Name]); //模型1
		list.push([me._Model_2.Type, me._Model_2.Name]); //模型2
		list.push([me._Model_3.Type, me._Model_3.Name]); //模型3

		return list;
	},
	/**获取模板标题*/
	getModelTitle: function(value) {
		return this["_Model_" + value].Title;
	},

	/**获取最后选择条码模板类型*/
	getLastModdelType: function() {
		var me = this;
		return JcallShell.LocalStorage.get(me._BarcodeModelType_LocalStorageName);
	},
	/**获取最后选择条码模板类型*/
	setLastModdelType: function(value) {
		var me = this;
		JcallShell.LocalStorage.set(me._BarcodeModelType_LocalStorageName, value);
	},
	/**获取模板内容*/
	getModelContent: function(value) {
		return this["_Model_" + value].Content;
	}
});