/**
 * 二维码条码打印模板
 * @author longfc
 * @version 2018-06-12
 */
Ext.define('Shell.class.rea.client.printbarcode.QRCodeModel', {
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
		Name: '模板纵向(65mm*40mm)',
		/**类型编号*/
		Type: '1',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"65mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 65, //纸宽
		PaperHigh: 40, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","33mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			
			//厂家货品编码
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","编码:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("17mm","12mm","30mm","3mm","{ProdGoodsNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("20mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("23mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","6mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","30mm","6mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("33mm","5mm","34mm","5mm","{BarcodeDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",11);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品类型
			'LODOP.ADD_PRINT_TEXT("5mm","39mm","21mm","5mm","{GoodsClass}");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//二维码
			//'LODOP.ADD_PRINT_BARCODE("10mm","39mm","21mm","21mm","QRCode","{Url}");' + +
			'LODOP.ADD_PRINT_BARCODE("10mm","35mm","24.36mm","24.36mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' 
			//当前序号
			/* 'LODOP.ADD_PRINT_TEXT("32mm","39mm","21mm","5mm","{Count}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' */
	},
	/**模板2(60mm*40mm)*/
	_Model_2: {
		/**类型名称*/
		Name: '模板纵向(60mm*40mm)',
		/**类型编号*/
		Type: '2',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 60, //纸宽
		PaperHigh: 40, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//厂家货品编码
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","编码:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("17mm","12mm","25mm","3mm","{ProdGoodsNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("20mm","12mm","25mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("23mm","12mm","25mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","6mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","25mm","6mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("33mm","5mm","29mm","5mm","{BarcodeDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",11);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//货品类型
			'LODOP.ADD_PRINT_TEXT("5mm","34mm","21mm","5mm","{GoodsClass}");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//二维码
			//'LODOP.ADD_PRINT_BARCODE("10mm","34mm","21mm","21mm","QRCode","{Url}");' +
			'LODOP.ADD_PRINT_BARCODE("10mm","34mm","26.36mm","26.36mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' 
			//当前序号
			/* 'LODOP.ADD_PRINT_TEXT("32mm","34mm","21mm","5mm","{Count}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' */
	},
	/**模板3(48mm*32mm)*/
	_Model_48_32_1: {
		/**类型名称*/
		Name: '模板纵向(48mm*32mm)',
		/**类型编号*/
		Type: '48_32_1',
		/**标题*/
		Title: 'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 48, //纸宽
		PaperHigh: 32, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("5mm","2mm","24mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","8mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","7mm","20mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("21mm","2mm","8mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("21mm","7mm","20mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("24mm","2mm","25mm","5mm","{BarcodeDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//二维码
			//'LODOP.ADD_PRINT_BARCODE("2mm","25mm","21mm","21mm","QRCode","{Url}");' +
			'LODOP.ADD_PRINT_BARCODE("6mm","23mm","28.36mm","28.36mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'
		//			//当前序号
		//			'LODOP.ADD_PRINT_TEXT("23mm","25mm","21mm","5mm","{Count}");' +
		//			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
		//			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
		//			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
		//			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
		//			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(48mm*32mm)*/
	_Model_48_32_2: {
		/**类型名称*/
		Name: '纵向(48*32mm带规格)',
		/**类型编号*/
		Type: '48_32_2',
		/**标题*/
		Title: 'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 48, //纸宽
		PaperHigh: 32, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("4mm","2mm","22mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","8mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","7mm","20mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("21mm","2mm","8mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("21mm","7mm","20mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//包装规格
			'LODOP.ADD_PRINT_TEXT("24mm","2mm","40mm","10mm","规格:{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			//'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//二维码28.36mm
			//'LODOP.ADD_PRINT_BARCODE("2mm","25mm","21mm","21mm","QRCode","{Url}");' +
			'LODOP.ADD_PRINT_BARCODE("4mm","22mm","21mm","21mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'
		//当前序号
		/* 'LODOP.ADD_PRINT_TEXT("23mm","25mm","21mm","5mm","{Count}");' +
		'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
		'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
		'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
		'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
		'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' */
	},
	/**模板21(60*40带名称)*/
	_Model_21: {
		/**类型名称*/
		Name: '模板21(60*40带名称)',
		/**类型编号*/
		Type: '21',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 60, //纸宽
		PaperHigh: 40, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("17mm","12mm","25mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//厂家货品编码
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","编码:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("20mm","12mm","25mm","3mm","{ProdGoodsNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("23mm","12mm","25mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","25mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("32mm","5mm","29mm","5mm","{BarcodeDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//实验室名称
			'LODOP.ADD_PRINT_TEXT("3mm","33mm","22mm","3mm","内分泌实验室");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品类型
			'LODOP.ADD_PRINT_TEXT("7mm","34mm","21mm","3mm","{GoodsClass}管理");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//二维码
			'LODOP.ADD_PRINT_BARCODE("10mm","34mm","28.36mm","28.36mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//当前序号
			'LODOP.ADD_PRINT_TEXT("32mm","34mm","21mm","5mm","{Count}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**定制模板横向(40mm*60mm)*/
	_Model_4060: {
		/**类型名称*/
		Name: '模板横向(40mm*60mm)',
		/**类型编号*/
		Type: '4060',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(2,"60mm","40mm","");', //横向
		PaperType: "", //纸张选择
		PrintingDirection: "2", //打印方向
		PaperWidth: 60, //纸宽
		PaperHigh: 40, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//厂家货品编码
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","9mm","3mm","编码:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("17mm","12mm","25mm","3mm","{ProdGoodsNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("20mm","12mm","25mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("23mm","12mm","25mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","9mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","25mm","6mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("32mm","5mm","29mm","5mm","{BarcodeDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +

			//货品类型
			'LODOP.ADD_PRINT_TEXT("5mm","34mm","21mm","5mm","{GoodsClass}");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//二维码
			//'LODOP.ADD_PRINT_BARCODE("10mm","34mm","21mm","21mm","QRCode","{Url}");' +
			'LODOP.ADD_PRINT_BARCODE("10mm","34mm","26.36mm","26.36mm","QRCode","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//当前序号
			'LODOP.ADD_PRINT_TEXT("32mm","34mm","21mm","5mm","{Count}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");' +
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
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
	_getModelContent_48_32_1: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_48_32_1.Type)].Content;
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
	_getModelContent_48_32_2: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_48_32_2.Type)].Content;
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
		if(data.GoodsName) data.GoodsName = data.GoodsName.replace(/\"/g, "");
		if(data.UnitMemo) data.UnitMemo = data.UnitMemo.replace(/\"/g, "");

		switch(type) {
			case me._Model_1.Type:
				barcode = me._getModelContent_1(data);
				break;
			case me._Model_2.Type:
				barcode = me._getModelContent_2(data);
				break;
			case me._Model_48_32_1.Type:
				barcode = me._getModelContent_48_32_1(data);
				break;
			case me._Model_48_32_2.Type:
				barcode = me._getModelContent_48_32_2(data);
				break;
			case me._Model_21.Type:
				barcode = me._getModelContent_2(data, '21');
				break;
			case me._Model_4060.Type:
				barcode = me._getModelContent_2(data);
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
		list.push([me._Model_48_32_2.Type, me._Model_48_32_2.Name]); //模型3
		list.push([me._Model_48_32_1.Type, me._Model_48_32_1.Name]); //模型4
		list.push([me._Model_4060.Type, me._Model_4060.Name]); //模型5
		//list.push([me._Model_21.Type,me._Model_21.Name]);//模型6
		return list;
	},
	/**获取选择模板实体信息*/
	getSelectModel: function(key1) {
		return this["_Model_" + key1];
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
	}
});