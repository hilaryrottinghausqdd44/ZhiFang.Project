/**
 * 一维码条码打印模板
 * @author longfc
 * @version 2020-11-26
 */
Ext.define('Shell.class.assist.printbarcode.gbbarcode.BarCodeModel', {
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
	/**模板3(50*30)*/
	_Model_128C5525: {
		/**类型名称*/
		Name: '128C(55mm*25mm)',
		/**类型编号*/
		Type: '128C5525',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"55mm","25mm","感控系统_打印条码");' +
			'LODOP.SET_PRINT_PAGESIZE(0,"55mm","25mm","");',
		PaperType: "", //纸张选择
		PrintingDirection: "1", //打印方向
		PaperWidth: 55, //纸宽
		PaperHigh: 25, //纸高
		PaperUnit: "mm", //宽高单位
		/**内容体*/
		Content: 'LODOP.NEWPAGE();' +
			//一维码
			'LODOP.ADD_PRINT_BARCODE("2mm","5mm","45mm","10mm","128C","{Url}");' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");' +
			
			//科室
			'LODOP.ADD_PRINT_TEXT("12mm","5mm","15mm","3mm","被检科室:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("12mm","18mm","40mm","3mm","{DeptCName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//货品名称
			'LODOP.ADD_PRINT_TEXT("16mm","5mm","50mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+ 
			/* 
			'LODOP.ADD_PRINT_TEXT("16mm","30mm","25mm","10mm"," {ItemResult1}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' + */
			
			//采样人
			'LODOP.ADD_PRINT_TEXT("21mm","5mm","16mm","6mm","采样人:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("21mm","12mm","30mm","6mm","{Sampler}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			
			//采样日期
			'LODOP.ADD_PRINT_TEXT("21mm","27mm","20mm","3mm","采样日期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("21mm","37mm","34mm","3mm","{SampleDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' 
			
	},
	/**模板3(50*30)*/
	_Model_128C53: {
		/**类型名称*/
		Name: '128C(50mm*30mm)',
		/**类型编号*/
		Type: '128C53',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","感控系统_打印条码");' +
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
			
			//科室
			'LODOP.ADD_PRINT_TEXT("14mm","5mm","45mm","3mm","科室:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","12mm","38mm","3mm","{DeptCName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//货品名称
			'LODOP.ADD_PRINT_TEXT("18mm","5mm","45mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			
			//采样人
			'LODOP.ADD_PRINT_TEXT("22mm","5mm","16mm","6mm","采样人:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("22mm","15mm","30mm","6mm","{Sampler}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			//采样日期
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","20mm","3mm","采样日期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","16mm","34mm","3mm","{SampleDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' 
				
	},
	/**模板3(40*30)*/
	_Model_128C43: {
		/**类型名称*/
		Name: '128C(40mm*30mm)',
		/**类型编号*/
		Type: '128C43',
		/**标题*/
		Title: 'LODOP.PRINT_INITA(0,0,"40mm","30mm","感控系统_打印条码");' +
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
			
			//科室
			'LODOP.ADD_PRINT_TEXT("14mm","2mm","38mm","3mm","科室:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("14mm","10mm","38mm","3mm","{DeptCName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//货品名称
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","38mm","10mm","{GoodsName}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//采样人
			'LODOP.ADD_PRINT_TEXT("22mm","2mm","16mm","6mm","采样人:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("22mm","15mm","30mm","6mm","{Sampler}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			
			//采样日期
			'LODOP.ADD_PRINT_TEXT("26mm","2mm","20mm","3mm","采样日期:");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' +
			'LODOP.ADD_PRINT_TEXT("26mm","16mm","34mm","3mm","{SampleDate}");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");' +
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);' +
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);' 
			
	},
	/**获取打印内容_模板128C5525*/
	_getModelContent_128C5525: function(data, type) {
		var me = this;
		var barcode = me['_Model_' + (type || me._Model_128C5525.Type)].Content;
		
		return me.getChangInfo(data, type,barcode);
	},
	/**获取打印内容_模板128C53*/
	_getModelContent_128C53: function(data, type) {
		var me = this;
		var barcode = me['_Model_' + (type || me._Model_128C53.Type)].Content;
		
		return me.getChangInfo(data, type,barcode);
	},
	/**获取打印内容_模板128C43*/
	_getModelContent_128C43: function(data, type) {
		var me = this;
		var barcode = me['_Model_' + (type || me._Model_128C43.Type)].Content;
		return me.getChangInfo(data, type,barcode);
	},
	getChangInfo:function(data, type,barcode){
		var me = this;
		
		var BarcodeDate = JShell.Date.toString(new Date(), true);
		//科室
		barcode = barcode.replace(/\{DeptCName\}/g, data.DeptCName);	
		
		var itemResult1=data.GoodsName+"("+data.ItemResult1+")";
		//货品名称
		barcode = barcode.replace(/\{GoodsName\}/g, itemResult1);
		//样品信息1
		//barcode = barcode.replace(/\{ItemResult1\}/g, data.ItemResult1);
		
		//采样日期 
		barcode = barcode.replace(/\{SampleDate\}/g, data.SampleDate);
		//
		var sampler=data.Sampler;
		if(data.MonitorType){
			sampler=sampler+data.MonitorType;
		}
		//采样人
		barcode = barcode.replace(/\{Sampler\}/g, sampler);
		
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g, BarcodeDate);
		//申请单号
		barcode = barcode.replace(/\{ReqDocNo\}/g, data.ReqDocNo);
		//一维码
		barcode = barcode.replace(/\{Url\}/g, data.BarCode);
		///**获取一维码版本*/
		var version = me.getQRCodeVersion(data.BarCode);
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
		if (data.DeptCName) data.DeptCName = data.DeptCName.replace(/\"/g, "");

		switch (type) {
			case me._Model_128C5525.Type:
				barcode = me._getModelContent_128C5525(data);
				break;
			case me._Model_128C53.Type:
				barcode = me._getModelContent_128C53(data);
				break;
			case me._Model_128C43.Type:
				barcode = me._getModelContent_128C43(data);
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
			list.push([me._Model_128C5525.Type, me._Model_128C5525.Name]); //模型128C5525
		list.push([me._Model_128C53.Type, me._Model_128C53.Name]); //模型128C53
		list.push([me._Model_128C43.Type, me._Model_128C43.Name]); //模型128C43	
		//数据库模板处理
		//list = me.getCustomModelList(list);
		return list;
	},
	/**获取定制模板模板集合*/
	getCustomModelList: function(list) {
		
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
