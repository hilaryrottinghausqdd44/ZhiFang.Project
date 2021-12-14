/**
 * 供货明细打印模板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.basic.BarcodeModel', {
	/**条码模板类型_本地记录最后一次的选择*/
	_BarcodeModelType_LocalStorageName:'BarcodeModelType',
	
	/**模板1(65mm*40mm)*/
	_Model_1:{
		/**类型名称*/
		Name:'模板1(65mm*40mm)',
		/**类型编号*/
		Type:'1',
		/**标题*/
		Title:'LODOP.PRINT_INITA(0,0,"65mm","40mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"65mm","40mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			//产品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","34mm","10mm","{GoodsName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//规格
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","34mm","3mm","规格:{UnitMemo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//厂家产品编号
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","34mm","3mm","编码:{ProdGoodsNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","34mm","3mm","批号:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//效期
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","34mm","3mm","效期:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("31mm","5mm","34mm","5mm","{BarcodeDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//产品类型
			'LODOP.ADD_PRINT_TEXT("5mm","39mm","21mm","5mm","{GoodsClass}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//二维码
			'LODOP.ADD_PRINT_BARCODE("10mm","39mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'+
			//当前序号
			'LODOP.ADD_PRINT_TEXT("31mm","39mm","21mm","5mm","{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板2(60mm*40mm)*/
	_Model_2:{
		/**类型名称*/
		Name:'模板2(60mm*40mm)',
		/**类型编号*/
		Type:'2',
		/**标题*/
		Title:'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			//产品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","{GoodsName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//规格
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","29mm","3mm","规格:{UnitMemo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//厂家产品编号
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","29mm","3mm","编码:{ProdGoodsNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","29mm","3mm","批号:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//效期
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","29mm","3mm","效期:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("31mm","5mm","29mm","5mm","{BarcodeDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//产品类型
			'LODOP.ADD_PRINT_TEXT("5mm","34mm","21mm","5mm","{GoodsClass}");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//二维码
			'LODOP.ADD_PRINT_BARCODE("10mm","34mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'+
			//当前序号
			'LODOP.ADD_PRINT_TEXT("31mm","34mm","21mm","5mm","{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板3(48mm*32mm)*/
	_Model_3:{
		/**类型名称*/
		Name:'模板3(48mm*32mm)',
		/**类型编号*/
		Type:'3',
		/**标题*/
		Title:'LODOP.PRINT_INITA("1mm","1mm","48mm","32mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"48mm","32mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			//产品名称
			'LODOP.ADD_PRINT_TEXT("2mm","2mm","25mm","10mm","{GoodsName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//批号
			'LODOP.ADD_PRINT_TEXT("15mm","2mm","25mm","3mm","批号:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//效期
			'LODOP.ADD_PRINT_TEXT("18mm","2mm","25mm","3mm","效期:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",6);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("23mm","2mm","25mm","5mm","{BarcodeDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//二维码
			'LODOP.ADD_PRINT_BARCODE("2mm","25mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'+
			//当前序号
			'LODOP.ADD_PRINT_TEXT("23mm","25mm","21mm","5mm","{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	/**模板21(60*40带名称)*/
	_Model_21:{
		/**类型名称*/
		Name:'模板21(60*40带名称)',
		/**类型编号*/
		Type:'21',
		/**标题*/
		Title:'LODOP.PRINT_INITA(0,0,"60mm","40mm","试剂系统_供货单_打印条码");'+
			'LODOP.SET_PRINT_PAGESIZE(1,"60mm","40mm","");',
		/**内容体*/
		Content:'LODOP.NEWPAGE();'+
			//产品名称
			'LODOP.ADD_PRINT_TEXT("5mm","5mm","29mm","10mm","{GoodsName}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//规格
			'LODOP.ADD_PRINT_TEXT("17mm","5mm","29mm","3mm","规格:{UnitMemo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//厂家产品编号
			'LODOP.ADD_PRINT_TEXT("20mm","5mm","29mm","3mm","编码:{ProdGoodsNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//批号
			'LODOP.ADD_PRINT_TEXT("23mm","5mm","29mm","3mm","批号:{LotNo}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//效期
			'LODOP.ADD_PRINT_TEXT("26mm","5mm","29mm","3mm","效期:{InvalidDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//条码打印日期
			'LODOP.ADD_PRINT_TEXT("31mm","5mm","29mm","5mm","{BarcodeDate}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#0000FF");'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			
			//实验室名称
			'LODOP.ADD_PRINT_TEXT("3mm","33mm","22mm","3mm","内分泌实验室");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",8);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//产品类型
			'LODOP.ADD_PRINT_TEXT("7mm","34mm","21mm","3mm","{GoodsClass}管理");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'+
			//二维码
			'LODOP.ADD_PRINT_BARCODE("10mm","34mm","21mm","21mm","QRCode","{Url}");'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});'+
			'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");'+
			'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'+
			//当前序号
			'LODOP.ADD_PRINT_TEXT("31mm","34mm","21mm","5mm","{Count}");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontName","微软雅黑");'+
			'LODOP.SET_PRINT_STYLEA(0,"FontSize",12);'+
			'LODOP.SET_PRINT_STYLEA(0,"FontColor","#FF0000");'+
			'LODOP.SET_PRINT_STYLEA(0,"Alignment",2);'+
			'LODOP.SET_PRINT_STYLEA(0,"Bold",1);'
	},
	
	/**获取打印内容_模板1*/
	_getModelContent_1:function(data,type){
		var me = this,
			barcode = me['_Model_' + (type || me._Model_1.Type)].Content;
			
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length,
			Count = '';
			
		if(codeArrayLen == 9){
			Count = codeArray[codeArray.length-1];
		}else if(codeArrayLen == 10){
			Count = codeArray[codeArrayLen - 1] + '-' + codeArray[codeArrayLen - 2];
		}
		var BarcodeDate = JShell.Date.toString(new Date(),true);
		
		//产品名称
		barcode = barcode.replace(/\{GoodsName\}/g,data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g,data.UnitMemo);
		//厂商产品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g,data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g,data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g,data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g,BarcodeDate);
		
		//产品类型
		barcode = barcode.replace(/\{GoodsClass\}/g,data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g,data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g,Count);
		
		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g,version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g,version.QRCodeErrorLevel);
		
		return barcode;
	},
	/**获取打印内容_模板2*/
	_getModelContent_2:function(data,type){
		var me = this,
			barcode = me['_Model_' + (type || me._Model_2.Type)].Content;
			
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length,
			Count = '';
			
		if(codeArrayLen == 9){
			Count = codeArray[codeArray.length-1];
		}else if(codeArrayLen == 10){
			Count = codeArray[codeArrayLen - 1] + '-' + codeArray[codeArrayLen - 2];
		}
		var BarcodeDate = JShell.Date.toString(new Date(),true);
		
		//产品名称
		barcode = barcode.replace(/\{GoodsName\}/g,data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g,data.UnitMemo);
		//厂商产品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g,data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g,data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g,data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g,BarcodeDate);
		
		//产品类型
		barcode = barcode.replace(/\{GoodsClass\}/g,data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g,data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g,Count);
		
		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g,version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g,version.QRCodeErrorLevel);
		
		return barcode;
	},
	/**获取打印内容_模板3*/
	_getModelContent_3:function(data,type){
		var me = this,
			barcode = me['_Model_' + (type || me._Model_3.Type)].Content;
			
		//当前的累加数
		//混合条码规则：ZFRP|1|prodOrgNo|prodGoodsNo|lotNo|invalidDate|compOrgNo|saleDtlId|当前序号|明细数量
		var codeArray = data.Barcode.split('|'),
			codeArrayLen = codeArray.length,
			Count = '';
			
		if(codeArrayLen == 9){
			Count = codeArray[codeArray.length-1];
		}else if(codeArrayLen == 10){
			Count = codeArray[codeArrayLen - 1] + '-' + codeArray[codeArrayLen - 2];
		}
		var BarcodeDate = JShell.Date.toString(new Date(),true);
		
		//产品名称
		barcode = barcode.replace(/\{GoodsName\}/g,data.GoodsName);
		//规格
		barcode = barcode.replace(/\{UnitMemo\}/g,data.UnitMemo);
		//厂商产品编码
		barcode = barcode.replace(/\{ProdGoodsNo\}/g,data.ProdGoodsNo);
		//效期
		barcode = barcode.replace(/\{InvalidDate\}/g,data.InvalidDate);
		//批号
		barcode = barcode.replace(/\{LotNo\}/g,data.LotNo);
		//条码打印日期
		barcode = barcode.replace(/\{BarcodeDate\}/g,BarcodeDate);
		
		//产品类型
		barcode = barcode.replace(/\{GoodsClass\}/g,data.GoodsClass);
		//二维码
		barcode = barcode.replace(/\{Url\}/g,data.Barcode);
		//当前序号
		barcode = barcode.replace(/\{Count\}/g,Count);
		
		var version = me.getQRCodeVersion(data.Barcode);
		//二维码版本
		barcode = barcode.replace(/\{QRCodeVersion\}/g,version.QRCodeVersion);
		//二维码容错级别
		barcode = barcode.replace(/\{QRCodeErrorLevel\}/g,version.QRCodeErrorLevel);
		
		return barcode;
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
	
	/**获取打印内容*/
	getModelContent:function(type,data){
		var me = this,
			barcode = "";
			
		switch (type){
			case me._Model_1.Type: barcode = me._getModelContent_1(data);
				break;
			case me._Model_2.Type: barcode = me._getModelContent_2(data);
				break;
			case me._Model_3.Type: barcode = me._getModelContent_3(data);
				break;
			case me._Model_21.Type: barcode = me._getModelContent_2(data,'21');
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
		list.push([me._Model_3.Type,me._Model_3.Name]);//模型3
		list.push([me._Model_21.Type,me._Model_21.Name]);//模型3
		
		return list;
	},
	/**获取模板标题*/
	getModelTitle:function(value){
		return this["_Model_" + value].Title;
	},
	
	/**获取最后选择条码模板类型*/
	getLastModdelType:function(){
		var me = this;
		return JcallShell.LocalStorage.get(me._BarcodeModelType_LocalStorageName);
	},
	/**获取最后选择条码模板类型*/
	setLastModdelType:function(value){
		var me = this;
		JcallShell.LocalStorage.set(me._BarcodeModelType_LocalStorageName,value);
	}
});