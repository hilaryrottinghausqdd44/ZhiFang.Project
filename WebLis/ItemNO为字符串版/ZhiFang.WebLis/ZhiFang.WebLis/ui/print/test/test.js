(function(){
	//打印url
	document.getElementById("printUrl").onclick = function(){
		var lodop = Shell.print.Lodop.getLodopObj("打印选中的信息"),
			url = "http://www.baidu.com";
	
		lodop.SET_PRINT_MODE("PRINT_PAGE_PERCENT","Width:100%;Height:100%");
		lodop.SET_PRINT_MODE("CATCH_PRINT_STATUS",true);
		
		lodop.NEWPAGE();
		lodop.ADD_PRINT_URL(0,0,'100%','100%',url);
		
		lodop.PREVIEW();
	}
	
	//打印条码
	document.getElementById("printBarcode").onclick = function(){
		var lodop = Shell.print.Lodop.getLodopObj("打印选中的信息"),
			barcodeValue = "1210000070",
			top = 10,
			left = 10,
			width = 200,
			height = 40,
			typeList = [
				"128A","128B","128C","128Auto","EAN8","EAN13","EAN128A","EAN128B","EAN128C",
				"Code39","39Extended","2_5interleaved","2_5industrial","2_5matrix","UPC_A",
				"UPC_E0","UPC_E1","UPCsupp2","UPCsupp5","Code93","93Extended","MSI","PostNet",
				"Codabar","QRCode","PDF417"
			],
			len = typeList.length;
			
		for(var i=0;i<len;i++){
			//lodop.ADD_PRINT_BARCODE(top+(height+10)*i,left,width,height,typeList[i],barcodeValue);
			var t = top + (height + 10) * parseInt(i/3),
				l = left + (width + 50) * (i%3);
			lodop.ADD_PRINT_BARCODE(t,l,width,height,typeList[i],barcodeValue);
		}
		
		lodop.PREVIEW();
	}
	
	//获取条码信息
	function getCodeInfo(){
		return [{
			BarCode:"00120140101005",
			ClientName:"测试送检单位",
			Name:"张三",
			Sex:"男",
			Age:6,
			AgeUnit:"岁",
			SickTypeName:"门诊",
			PatNo:"11223344",
			ColorName:"红",
			ItemList:["组套1","组套2dd","组套1","组套ss2","组1套1123","组套2","组套1sd","组套2","组套1","组套2","组套11","组套22"].join(";"),
			CollectDate:"2014-01-01 10:11:15"
		},{
			BarCode:"00120140101005",
			ClientName:"测试送检单位",
			Name:"李四",
			Sex:"男",
			Age:6,
			AgeUnit:"岁",
			SickTypeName:"门诊",
			PatNo:"11223344",
			ColorName:"红",
			ItemList:["组套1","组套2dd","组套1","组套ss2","组1套1123","组套2","组套1sd","组套2","组套1","组套2","组套11","组套22"].join(";"),
			CollectDate:"2014-01-01 10:11:15"
		},{
			BarCode:"00120140101005",
			ClientName:"测试送检单位",
			Name:"王五",
			Sex:"男",
			Age:6,
			AgeUnit:"岁",
			SickTypeName:"门诊",
			PatNo:"11223344",
			ColorName:"红",
			ItemList:["组套1","组套2dd","组套1","组套ss2","组1套1123","组套2","组套1sd","组套2","组套1","组套2","组套11","组套22"].join(";"),
			CollectDate:"2014-01-01 10:11:15"
		}];
	}
	
	//打印条码-预览
	document.getElementById("printMark0").onclick = function(){
		var info = getCodeInfo();
		Shell.taida.Print.barcode(info,null,true);
	};
	//打印条码-直接
	document.getElementById("printMark1").onclick = function(){
		var info = getCodeInfo();
		Shell.taida.Print.barcode(info,null,false);
	};
	//打印条码-设计
	document.getElementById("printMark2").onclick = function(){
		//var info = Shell.taida.Print.design();
		//alert(info);
		Shell.taida.Print.designAndSave();
	};
	function showMsgdiv(value,hide){
		var msgdiv = document.getElementById("msgdiv");
		if(hide){
			msgdiv.style.display = "none";
			return;
		}
		
		var button = "<input type='button' value='确定' onclick='hideMsgdiv();'></input>";
		
		msgdiv.innerHTML = value + "</br>" + button;
		msgdiv.style.top = "5px";
		msgdiv.style.left = "50px";
		msgdiv.style.display = "";
	}
	
	function hideMsgdiv(){
		showMsgdiv(null,true);
	}
	
	//显示div
	document.getElementById("showMsgdiv").onclick = function(){
		showMsgdiv("这是一个测试文本数据");
	}
	//隐藏div
	document.getElementById("hideMsgdiv").onclick = function(){
		hideMsgdiv();
	}
	
	//显示div
	document.getElementById("showMaskdiv").onclick = function(){
		$('#maskdiv').window("open");
		
		
	}
	//隐藏div
	document.getElementById("hideMaskdiv").onclick = function(){
		$('#maskdiv').window("close");
	}
	
	$('#maskdiv').window({
		collapsible:false,
		minimizable:false,
		maximizable:false,
		closable:false,
		closed:true,
		draggable:false,
		resizable:false,
		noheader:true,
	    width:150,
	    height:20,
	    modal:true
	});
})();