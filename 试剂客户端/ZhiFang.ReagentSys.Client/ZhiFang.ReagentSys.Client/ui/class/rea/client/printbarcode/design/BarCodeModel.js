/**
 * 条码模板设计
 * @author longfc
 * @version 2018-08-08
 */
Ext.define('Shell.class.rea.client.printbarcode.design.BarCodeModel', {
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

	/**模板1(40mm*65mm)*/
	_Model_128A: {
		/**类型名称*/
		Name: '128A(50mm*30mm)',
		/**类型编号*/
		Type: '128A',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128A","1817180806000375");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","M");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","T5550002");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","2019-08-01");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板128B(60mm*40mm)*/
	_Model_128B: {
		/**类型名称*/
		Name: '128B(60mm*40mm)',
		/**类型编号*/
		Type: '128B',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"60mm","40mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_打印条码");LODOP.SET_PRINT_PAGESIZE(0,"60mm","40mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128B","1817180806000375");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","M");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","T5550002");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","2019-08-01");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(50*30)*/
	_Model_128C: {
		/**类型名称*/
		Name: '128C(50mm*30mm)',
		/**类型编号*/
		Type: '128C',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128C","1817180807000003");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","M");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","3333");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","2018-08-29");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板21(50*30)*/
	_Model_128Auto: {
		/**类型名称*/
		Name: '128Auto(50mm*30mm)',
		/**类型编号*/
		Type: '128Auto',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		/**内容体*/
		Content: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");LODOP.NEWPAGE();LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128Auto","1817180806000375");LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",5);LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","M");LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","T5550002");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","2019-08-01");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","总I型胶原氨基酸端延长肽检测");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","100测试");LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");LODOP.SET_PRINT_STYLEA(0,"FontSize",8);LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},

	/**获取打印内容_模板1*/
	_getModelContent_128A: function(data, type) {},
	/**获取打印内容_模板2*/
	_getModelContent_128B: function(data, type) {},
	/**获取打印内容_模板3*/
	_getModelContent_128C: function(data, type) {},
	/**获取一维码版本*/
	getQRCodeVersion: function(value) {
		var len = //value.length,
			result = {
				QRCodeVersion: 5
			}; //版本5

		len = value.replace(/[^\x00-\xff]/g, 'xxx').length;

		if(len <= 84) { //容错M
			result.QRCodeErrorLevel = "M";
		} else { //容错L
			result.QRCodeErrorLevel = "L";
		}

		return result;
	},

	/**获取打印内容*/
	getModelContent: function(type, data) {
		var me = this,
			barcode = "";

		switch(type) {
			case me._Model_128A.Type:
				barcode = me._getModelContent_128A(data);
				break;
			case me._Model_128B.Type:
				barcode = me._getModelContent_128B(data);
				break;
			case me._Model_128C.Type:
				barcode = me._getModelContent_128C(data);
				break;
			case me._Model_128Auto.Type:
				barcode = me._getModelContent_128B(data, '128Auto');
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
		list.push([me._Model_128C.Type, me._Model_128C.Name]); //模型3
		list.push([me._Model_128A.Type, me._Model_128A.Name]); //模型1
		list.push([me._Model_128B.Type, me._Model_128B.Name]); //模型2	
		list.push([me._Model_128Auto.Type, me._Model_128Auto.Name]); //模型3

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