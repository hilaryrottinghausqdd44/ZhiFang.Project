/**
 * 一维码条码打印模板
 * @author longfc
 * @version 2018-06-12
 */
Ext.define('Shell.class.rea.client.printbarcode.BarCodeModel', {
	/**条码模板类型_本地记录最后一次的选择*/
	_BarcodeModelType_LocalStorageName: 'BarcodeModelType',
	//条码机构名称:默认为空
	_BarCodeOrgCName: "",
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
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 50, //纸宽
		PaperHigh: 30, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128A","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion","{QRCodeVersion}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
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
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 60, //纸宽
		PaperHigh: 30, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128B","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(50*30)*/
	_Model_128C53: {
		/**类型名称*/
		Name: '128C1(50mm*30mm)',
		/**类型编号*/
		Type: '128C53',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 50, //纸宽
		PaperHigh: 30, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128C","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(50*30),停用*/
	_Model_128C532: {
		/**类型名称*/
		Name: '128C2(50mm*30mm)',
		/**类型编号*/
		Type: '128C532',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 50, //纸宽
		PaperHigh: 30, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128C","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","{GoodsName}");' + //{UnitMemo}
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/***
	 * 潍医附院检验科定制模板一(50*30)
	 * 定制带机构名称
	 */
	_Model_128COf1_101825: {
		/**类型名称*/
		Name: '128C含名称(50*30mm)',
		/**类型编号*/
		Type: '128COf1_101825',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","潍医附院检验科");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
		PaperType: "",//纸张选择
		PrintingDirection: "1",//打印方向
		PaperWidth: 50,//纸宽
		PaperHigh: 30,//纸高
		PaperUnit: "mm",//宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128C","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","2mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","10mm","25mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//自定义名称显示
			'LODOP.ADD_PRINT_TEXT("14mm","30mm","20mm","10mm","{BarCodeOrgCName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","10mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","2mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","2mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(50*30)*/
	_Model_128C43: {
		/**类型名称*/
		Name: '128C(40mm*30mm)',
		/**类型编号*/
		Type: '128C43',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"40mm","30mm","试剂系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"40mm","30mm","");',
		PaperType: "",//纸张选择
		PrintingDirection: "1",//打印方向
		PaperWidth: 40,//纸宽
		PaperHigh: 30,//纸高
		PaperUnit: "mm",//宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","2mm","37mm","10mm","128C","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","2mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","2mm","38mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","2mm","38mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
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
		PaperType: "",//纸张选择
		PrintingDirection: "1",//打印方向
		PaperWidth: 50,//纸宽
		PaperHigh: 30,//纸高
		PaperUnit: "mm",//宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128Auto","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			//批号
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","9mm","6mm","批号:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","30mm","6mm","{LotNo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","9mm","3mm","效期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("18mm","12mm","34mm","3mm","{InvalidDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//货品名称
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//规格
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","45mm","3mm","规格:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","12mm","38mm","3mm","{UnitMemo}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**获取打印内容_模板1*/
	_getModelContent_128A: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128A.Type)].Content;

		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length,
			Count = '';
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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取打印内容_模板2*/
	_getModelContent_128B: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128B.Type)].Content;

		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length,
			Count = '';
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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取打印内容_模板3*/
	_getModelContent_128C53: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128C53.Type)].Content;
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length;
		var BarcodeDate = JShell.Date.toString(new Date(), true);
		//条码机构名称描述(自定义字段)
		barcode = barcode.replace(/\{BarCodeOrgCName\}/g, me._BarCodeOrgCName);
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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		//barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**
	 * 定制模板:停用
	 * 获取打印内容_模板3
	 * 优先级的判断:简称--->英文名称--->中文名称
	 * */
	_getModelContent_128C532: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128C532.Type)].Content;
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length;
		var goodsName = "";
		if (data.SName) goodsName = data.SName;
		if (data.EName) goodsName = data.EName;
		if (!goodsName) goodsName = data.GoodsName;

		var BarcodeDate = JShell.Date.toString(new Date(), true);
		//条码机构名称描述(自定义字段)
		barcode = barcode.replace(/\{BarCodeOrgCName\}/g, me._BarCodeOrgCName);
		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, goodsName);
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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		//barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);
		return barcode;
	},
	/**
	 * 潍医附院检验科定制模板一
	 * 条码信息包含机构名称
	 * 获取打印内容_模板3
	 * */
	_getModelContent_128COf1_101825: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128COf1_101825.Type)].Content;
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length;
		var goodsName = data.GoodsName;
		var BarcodeDate = JShell.Date.toString(new Date(), true);
		//条码机构名称描述(自定义字段)
		barcode = barcode.replace(/\{BarCodeOrgCName\}/g, me._BarCodeOrgCName);
		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, goodsName);
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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		//barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取打印内容_模板3*/
	_getModelContent_128C43: function(data, type) {
		var me = this,
			barcode = me['_Model_' + (type || me._Model_128C43.Type)].Content;
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length;

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
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.Barcode);
		//当前序号
		//barcode = barcode.replace(/\{Count\}/g, Count);
		var version = me.getQRCodeVersion(data.Barcode);
		//一维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
		//一维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);

		return barcode;
	},
	/**获取一维码版本*/
	getQRCodeVersion: function(value) {
		var len = //value.length,
			result = {
				QRCodeVersion: 5
			}; //版本5

		len = value.replace(/[^\x00-\xff]/g, 'xxx').length;

		if (len <= 84) { //容错M
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
		if (data.GoodsName) data.GoodsName = data.GoodsName.replace(/\"/g, "");
		if (data.UnitMemo) data.UnitMemo = data.UnitMemo.replace(/\"/g, "");

		switch (type) {
			case me._Model_128A.Type:
				barcode = me._getModelContent_128A(data);
				break;
			case me._Model_128B.Type:
				barcode = me._getModelContent_128B(data);
				break;
			case me._Model_128C53.Type:
				barcode = me._getModelContent_128C53(data);
				break;
			case me._Model_128C532.Type:
				barcode = me._getModelContent_128C532(data);
				break;
			case me._Model_128C43.Type:
				barcode = me._getModelContent_128C43(data);
				break;
			case me._Model_128Auto.Type:
				barcode = me._getModelContent_128B(data, '128Auto');
				break;
			case me._Model_128COf1_101825.Type:
				barcode = me._getModelContent_128COf1_101825(data);
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
		list.push([me._Model_128C53.Type, me._Model_128C53.Name]); //模型3
		//list.push([me._Model_128C532.Type, me._Model_128C532.Name]); //模型3
		list.push([me._Model_128C43.Type, me._Model_128C43.Name]); //模型3
		list.push([me._Model_128A.Type, me._Model_128A.Name]); //模型1
		//list.push([me._Model_128B.Type, me._Model_128B.Name]); //模型2	
		list.push([me._Model_128Auto.Type, me._Model_128Auto.Name]); //模型3		
		//定制模板处理
		list = me.getCustomModelList(list);
		return list;
	},
	/**获取定制模板模板集合*/
	getCustomModelList: function(list) {
		var me = this;
		if (!list) list = [];

		var orgNo = JcallShell.REA.System.CENORG_CODE || "";
		//定制处理
		var CustomStrategy = function() {
			//内部算法集合封装
			var strategy = {
				/***
				 * 测试机构[OrgNo]=101817
				 * @param {Object} list1
				 */
				cenorg_101817: function(list1) {
					me._BarCodeOrgCName = JcallShell.REA.System.CENORG_NAME || "";
					list1.push([me._Model_128COf1_101825.Type, me._Model_128COf1_101825.Name]);
					return list1;
				},
				/***
				 * 潍医附院检验科[OrgNo]=101825
				 * @param {Object} list1
				 */
				cenorg_101825: function(list1) {
					me._BarCodeOrgCName = "潍医附院检验科";
					list1.push([me._Model_128COf1_101825.Type, me._Model_128COf1_101825.Name]);
					return list1;
				}
			};
			//调用接口
			return function(orgNo1, list1) {
				if (!list1) list1 = [];
				var fn1 = "cenorg_" + orgNo1;
				if (strategy[fn1]) {
					return strategy[fn1] && strategy[fn1](list1);
				} else {
					return list1;
				}
			}
		}();
		if (orgNo) list = CustomStrategy(orgNo, list);
		if (!list) list = [];
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
