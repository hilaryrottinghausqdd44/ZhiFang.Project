/**
 * 打印程序 
 * @author Jcall
 * @version 2014-08-04
 */
var Shell = Shell || {};

Shell.print = {};

/**Lodop打印控件*/
Shell.print.Lodop = {
	objectId:'LODOP_OB',
	embedId:'LODOP_EM',
	E32:Shell.util.Path.uiPath + '/print/resources/install_lodop32.exe',
	E64:Shell.util.Path.uiPath + '/print/resources/install_lodop64.exe',
	
	
	/**获取打印组件*/
	getLodopObj:function(taskName,E32,E64){
		if(!this.objectId) return null;
		
		var lodop = this.getLodop(document.getElementById(this.objectId),document.getElementById(this.embedId),E32,E64);
	    lodop.PRINT_INIT(taskName);
	    //lodop.SET_PRINT_STYLE("FontSize", 18);
//	    lodop.SET_PRINT_STYLEA(0,"HOrient",3);
//		lodop.SET_PRINT_STYLEA(0,"VOrient",3);
	    lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
	    return lodop;
	},
	/**获取打印组件对象*/
	getLodop:function(object,embed,E32,E64){
		var noLodop = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='{0}'>执行安装</a>,安装后请刷新页面或重新进入。</font>",
			updatelodop = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='{0}'>执行升级</a>,升级后请重新进入。</font>",
			strHtmInstall = noLodop.replace("{0}",(E32 || this.E32)),
			strHtm64_Install = noLodop.replace("{0}",(E64 || this.E64)),
			strHtmUpdate = updatelodop.replace("{0}",(E32 || this.E32)),
			strHtm64_Update = updatelodop.replace("{0}",(E64 || this.E64)),
			strHtmFireFox = "<font color='red'>注意：如曾安装过Lodop旧版附件npActiveXPLugin,</br>" +
				"请在【工具】->【附加组件】->【扩展】中先卸它。</font>",
			lodop = embed,
			errorInfo = [];
					
		try{		     
			if(navigator.appVersion.indexOf("MSIE") >= 0) lodop = object;
			if((lodop == null) || (typeof(lodop.VERSION) == "undefined")){
				if(navigator.userAgent.indexOf('Firefox') >= 0)
				 	errorInfo.push(strHtmFireFox);
				if(navigator.userAgent.indexOf('Win64') >= 0){
					errorInfo.push(strHtm64_Install);
				}else{
					errorInfo.push(strHtmInstall);
				}
				
				if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
				return lodop;
			}else if(lodop.VERSION < "6.1.2.0"){
				if(navigator.userAgent.indexOf('Win64') >= 0){
					errorInfo.push(strHtm64_Update);
				}else{
					errorInfo.push(strHtmUpdate);
				}
				
				if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
				
			 	return lodop;
			}
			
			if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
			return lodop; 
		}catch(err){
			if(navigator.userAgent.indexOf('Win64') >=0){
				errorInfo.push("Error:" + strHtm64_Install);
			}else{
				errorInfo.push("Error:" + strHtmInstall);
			}
			
			if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
		    return lodop; 
		}
	},
	/**显示错误信息*/
	showError:function(info){
		document.documentElement.innerHTML = info + document.documentElement.innerHTML;
	}
};