/**
 * 通用打印功能
 * 说明：打印功能开放“直接打印”和“浏览打印”两个方法；
 * 浏览器支持情况：IE9+；
 * 注意：调用打印机会阻塞浏览器，直到PDF文件内容全部发送给打印机，才会恢正常运行；小文件没阻塞感，大文件阻塞感非常明显；
 * @author Jcall
 * @version 2020-01-04
 */
;(function(){
	//对外名称
	var MOD_NAME = 'zfprint';
	//自清理时间
	var CLEAR_TIMES = 3000;
	//每一份PDF文件等待回调时间
	var CALLBACK_TIMES = 2000;
	//组件代码
	var PDF_HTML = 
	'<object id={id} classid="clsid:CA8A9780-280D-11CF-A24D-444553540000"' +
		' style="height:0;width:0;margin:0;padding:0;" border="0">' +
		'<param name="src" value="{url}"/>' +
	'</object>';
	
	//直接打印
	function print(url,callback){
		if(!isValid()) return;
		//创建PDF组件
		var pdfId = createPdfDiv(url);
		//执行打印
		doPrint(pdfId,1,callback);
	};
	//预览打印
	function preview(url,callback){
		if(!isValid()) return;
		//创建PDF组件
		var pdfId = createPdfDiv(url);
		//执行打印
		doPrint(pdfId,2,callback);
	};
	//执行打印
	function doPrint(pdfId,type,callback){
		//pdf文件加载
		pdfLoaded(pdfId,function(pdf){
			try{
				if(type == 1){//直接打印
					pdf.printAll();
				}else if(type == 2){//预览打印
					pdf.printWithDialog();
				}
				setTimeout(function(){
					if(callback){callback(pdf);}
				},CALLBACK_TIMES);
				setTimeout(function(){
					pdf.parentNode.parentNode.removeChild(pdf.parentNode);
				},CLEAR_TIMES);
			}catch(e){
				showError("未安装adobe reader插件，请联系管理员安装！");
			}
		});
	};
	//pdf文件加载
	function pdfLoaded(pdfId,callback){
		var pdf = document.getElementById(pdfId);
		if(pdf){
			if(pdf.readyState == "4"){
				callback(pdf);
			}else{
				setTimeout(function(){
					pdfLoaded(callback);
				},100);
			}
		}else{
			setTimeout(function(){
				pdfLoaded(callback);
			},100);
		}
	};
	//创建PDF组件
	function createPdfDiv(url){
		url += ((url.indexOf('?') > -1) ? '&' : '?') + 't=' + new Date().getTime();
		url = encodeURI(url);//处理中文名称
		
		var pdfId = new Date().getTime();
		var div = document.createElement('div');
		div.innerHTML = PDF_HTML.replace(/{id}/g,pdfId).replace(/{url}/g,url);
		document.body.appendChild(div);
		
		return pdfId;
	};
	
	//判断是否符合插件打印规范
	function isValid(){
		var ieVersion = IEVersion();
		if(ieVersion == 'edge' || ieVersion == -1){//非IE浏览器
			showError('该打印组件不支持非IE浏览器！');
			return false;
		}else if(ieVersion < 9){//IE6/7/8
			showError('该打印组件不支持IE9以下版本！');
			return false;
		}else{
			return true;
		}
	};
	//判断IE版本
	function IEVersion() {
		var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
		var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
		var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
		var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
		if(isIE) {
			var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
			reIE.test(userAgent);
			var fIEVersion = parseFloat(RegExp["$1"]);
			if(fIEVersion == 7) {
				return 7;
			} else if(fIEVersion == 8) {
				return 8;
			} else if(fIEVersion == 9) {
				return 9;
			} else if(fIEVersion == 10) {
				return 10;
			} else {
				return 6;//IE版本<=7
			}   
		} else if(isEdge) {
			return 'edge';//edge
		} else if(isIE11) {
			return 11; //IE11  
		}else{
			return -1;//不是ie浏览器
		}
	};
	//显示错误信息
	function showError(value){
		alert(value);
	};
	
	//暴露接口
	window[MOD_NAME] = {
		//直接打印
		print:print,
		//预览打印
		preview:preview
	};
})();
