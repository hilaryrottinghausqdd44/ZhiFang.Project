/**
 * 供货明细打印模板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.BarcodeModel', {
	/**模板1*/
	_Model_1:{
		/**类型名称*/
		Name:'模板1(48mm*32mm)',
		/**类型编号*/
		Type:'1',
		/**标题*/
		Title:'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			'LODOP.ADD_PRINT_RECT(0,"28mm","9mm","4mm",0,1);'+
			'LODOP.ADD_PRINT_TEXT("1mm","30mm","10mm","5mm","ZFRP");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);'+
			'LODOP.ADD_PRINT_TEXT("1mm","37mm","11mm","3mm","{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT(0,0,"30mm","16mm","{ShortCode}({UnitMemo})");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",11);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("21mm",0,"30mm","4mm","效期:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("25mm",0,"46mm","4mm","批号:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_BARCODE("5mm","27mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'
	},
		
	/**模板2*/
	_Model_2:{
		/**类型名称*/
		Name:'模板2(65mm*40mm)',
		/**类型编号*/
		Type:'2',
		/**标题*/
		Title:'LODOP.PRINT_INITA("1mm","1mm","65mm","40mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			'LODOP.ADD_PRINT_TEXT(0,0,"46mm","10mm","{ShortCode}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",11);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("11mm",0,"46mm","12mm","{GoodsName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",9);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
//			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT(0,"42mm","19mm","5mm","{GoodsClass}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("5mm","42mm","19mm","3mm","No.{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.ADD_PRINT_TEXT("24mm",0,"46mm","3mm","规格:{UnitMemo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			//'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("27mm",0,"46mm","3mm","REF:{ProdGoodsNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			//'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("30mm",0,"46mm","3mm","EXP:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			//'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_TEXT("33mm",0,"65mm","3mm","LOT:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			//'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			'LODOP.ADD_PRINT_RECT("10.5mm","48mm","8mm","3mm",0,1);'+
			'LODOP.ADD_PRINT_TEXT("11mm","49.5mm","10mm","3mm","ZFRP");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",7);'+
			'LODOP.ADD_PRINT_BARCODE("14mm","42mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'
	},
	/**获取二维码版本*/
	getQRCodeVersion:function(value){
		var len = value.length,
			result = {QRCodeVersion:5};//版本5
		
		if(len <= 84){//容错M
			result.QRCodeErrorLevel = "M";
		}else if(len <= 106){//容错L
			result.QRCodeErrorLevel = "L";
		}
		
		return result;
	},
	/**获取打印内容_模板1*/
	_getModelContent_1:function(data,type){
		var me = this,
			barcode = me['_Model_' + (type || me._Model_1.Type)].Content;
			
		//当前的累加数
		barcode = barcode.replace(/\{Count\}/g,data.Count);
		
		//英文简称（较大号字体，最好加底色）
		barcode = barcode.replace(/\{ShortCode\}/g,data.ShortCode);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g,data.UnitMemo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g,data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g,data.LotNo);
		//二维码,扫描试剂二维码包含内容：ZFRP|版本号|品牌编号|产品码|批号|失效期|供应商编号|单据号|单内商品唯一码
		var Url = ['ZFRP','1',data.ProdOrgNo,data.ProdGoodsNo,data.LotNo,data.InvalidDate,data.CompOrgNo,data.SaleDocNo,data.Count];
		Url = Url.join("|");
		barcode = barcode.replace(/\{Url\}/g,Url);
		
		var version = me.getQRCodeVersion(Url);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g,version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g,version.QRCodeErrorLevel);
		
		return barcode;
	},
	/**获取打印内容_模板1*/
	_getModelContent_2:function(data,type){
		var me = this,
			barcode = me['_Model_' + (type || me._Model_2.Type)].Content;
			
		//当前的累加数
		barcode = barcode.replace(/\{Count\}/g,data.Count);
			
		//英文简称（较大号字体，最好加底色）
		barcode = barcode.replace(/\{ShortCode\}/g,data.ShortCode);
		//产品名称
		barcode = barcode.replace(/\{GoodsName\}/g,data.GoodsName);
		//一级分类
		barcode = barcode.replace(/\{GoodsClass\}/g,data.GoodsClass);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g,data.UnitMemo);
		//厂商产品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g,data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g,data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g,data.LotNo);
		//二维码,扫描试剂二维码包含内容：ZFRP|版本号|品牌编号|产品码|批号|失效期|供应商编号|单据号|单内商品唯一码
		var Url = ['ZFRP','1',data.ProdOrgNo,data.ProdGoodsNo,data.LotNo,data.InvalidDate,data.CompOrgNo,data.SaleDocNo,data.Count];
		Url = Url.join("|");
		barcode = barcode.replace(/\{Url\}/g,Url);
		
		var version = me.getQRCodeVersion(Url);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g,version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g,version.QRCodeErrorLevel);
		
		return barcode;
	},
	
	/**获取打印内容*/
	getModelContent:function(type,data){
		var me = this,
			barcode = "";
			
		switch (type){
			case me._Model_1.Type: barcode = me._getModelContent_1(data);
				break;
			case me._Model_2.Type: barcode = me._getModelContent_2(data);
				break;
			default:
				break;
		}
		return barcode;
	},
	/**获取模板列表*/
	getModelList:function(){
		var me = this,
			list = [];
		
		list.push([me._Model_1.Type,me._Model_1.Name]);//模型1
		list.push([me._Model_2.Type,me._Model_2.Name]);//模型2
		
		return list;
	},
	/**获取模板标题*/
	getModelTitle:function(value){
		return this["_Model_" + value].Title;
	}
});