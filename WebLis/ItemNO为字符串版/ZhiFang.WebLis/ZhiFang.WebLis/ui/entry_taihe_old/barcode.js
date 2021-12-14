/**
 * 十堰太和
 * 需要在此之前引入
 * 		ui/util/util.js
 * 		ui/print/Print.js
 * 
 * @author Jcall
 * @version 2014-12-24
 * 
 * 对外公开两个方法
 * 条码打印功能 Shell.taida.Print.barcode
 * 条码设计功能 Shell.taida.Print.designAndSave
 */
var Shell = Shell || {};
/**十堰太和*/
Shell.taida = {};

/**十堰太和打印*/
Shell.taida.Print = {
	/**条码模新增存地址*/
	AddLocationBarCodePrintPamaterUrl:Shell.util.Path.rootPath + "/ServiceWCF/dictionaryservice.svc/AddLocationBarCodePrintPamater",
	/**条码模板修改地址*/
	UpdateLocationBarCodePrintPamaterUrl:Shell.util.Path.rootPath + "/ServiceWCF/dictionaryservice.svc/UpdateLocationBarCodePrintPamater",
	/**默认的条码模板文件路径*/
	DefaultModelUrl:Shell.util.Path.uiPath + "/entry_taihe/model.txt",
	/**
	 * 默认模型
	 * 条码 管子颜色
	 * 送检单位
	 * 病人信息(姓名 性别 年龄 就诊类型 病历号)
	 * 组合项目
	 * 采样时间
	 */
	defaultModel:"",
//		"LODOP.PRINT_INITA(0,0,193,113,\"打印条码信息\");" +
//		"LODOP.SET_PRINT_PAGESIZE(1,510,300,\"\");" +
//		"LODOP.ADD_PRINT_BARCODE(1,10,135,35,\"128C\",\"{BarCode}\");" +
//		"LODOP.ADD_PRINT_TEXT(1,150,29,29,\"{ColorName}\");" +
//		"LODOP.SET_PRINT_STYLEA(0,\"FontSize\",16);" +
//		"LODOP.ADD_PRINT_TEXT(40,10,170,15,\"{ClientName}\");" +
//		"LODOP.ADD_PRINT_TEXT(55,10,170,15,\"{Name} {Sex} {Age}{AgeUnit} {SickTypeName} {PatNo}\");" +
//		"LODOP.ADD_PRINT_TEXT(70,10,180,30,\"{ItemList}\");" +
//		"LODOP.ADD_PRINT_TEXT(100,10,170,15,\"{CollectDate}\");",
	/**
	 * 条码打印(支持多个条码一次打印)
	 * @param info 数据信息,支持数组(多条码)和对象(单条码)
	 * @param preview (true/false)是否预览打印,默认直接打印
	 * @param model 模板信息
	 * @example 外部调用时示例
	 * Shell.taida.Print.barcode({
			BarCode:"00120140101005",
			ClientName:"测试送检单位",
			Name:"张三",
			Sex:"男",
			Age:6,
			AgeUnit:"岁",
			SickTypeName:"门诊",
			PatNo:"11223344",
			ColorName:"红",
			ItemList:["组套1","组套2","组套3","组套4"].join(";"),
			CollectDate:"2014-01-01 10:11:15"
		},true)
	 */
	barcode:function(info,preview,model){
		var lodop = Shell.util.Print.getLodopObj("打印条码");
		this.initPage(lodop,info,model);
		//预览打印/直接打印
		if(preview){
			lodop.PREVIEWB();
		}else{
			lodop.PRINT();
		}
	},
	/**条码设计保存功能*/
	designAndSave:function(){
		var me = this,
			info = this.design(),
			msg = "是否保存条码模板设计结果?"//;"配置内容:</br>" + info;
		
		
		$.messager.confirm("条码模板设计保存",msg,function(bo){
			if(!bo) return;
			me.saveToServer(info,function(){
	    		//Shell.util.Cookie.setCookie("BarcodeModel",info);
				$.messager.alert("提示信息","<b style='color:green;'>模板保存成功</b>","info");
    		});
		});
	},
	/**模板保存到服务器*/
	saveToServer:function(info,callback){
		var me = this,
			info = $.trim(info).replace(/\r\n/g,""),
			ZhiFangUserID = Shell.util.Cookie.getCookie("ZhiFangUser"),
			BarcodeModel = Shell.util.Cookie.getCookie("BarcodeModel"),
			url = BarcodeModel ? me.UpdateLocationBarCodePrintPamaterUrl : me.AddLocationBarCodePrintPamaterUrl,
			entity = {AccountId:ZhiFangUserID,ParaMeter:info};
			
		$.ajax({ 
			type:'post',
			dataType:'json',
			contentType:'application/json',
			url:url,
			data:Shell.util.JSON.encode({jsonentity:entity}),
			success:function(result){
				if(result.success){
					callback(result);
				}else{
					$.messager.alert("错误信息","<b style='color:red;'>模板保存失败！错误信息：" + result.ErrorInfo + "</b>","error");
				}
			},
			error:function(request,strError){
				Shell.util.Msg.showLog("模板保存失败！错误信息：" + strError);
				$.messager.alert("错误信息","<b style='color:red;'>模板保存失败！错误信息：" + strError + "</b>","error");
			} 
		});
	},
	/**条码设计*/
	design:function(model){
		var lodop = Shell.util.Print.getLodopObj("打印条码");
		this.initPage(lodop,null,model);
		return lodop.PRINT_DESIGN();
	},
	/**初始化页面信息*/
	initPage:function(LODOP,info,model){
		var me = this,
			BarcodeModel = Shell.util.Cookie.getCookie("BarcodeModel"),
			model = model || BarcodeModel;
		
		if(!model){
			if(!me.defaultModel){
				me.getDefaultModel(function(result){
					me.defaultModel = result;
					me.changeLodop(LODOP,info,me.defaultModel);
				});
			}else{
				me.changeLodop(LODOP,info,me.defaultModel);
			}
		}else{
			me.changeLodop(LODOP,info,model);
		}
	},
	/**更改lodop信息*/
	changeLodop:function(LODOP,info,model){
		model = model.replace(/lodop./g,"LODOP.");
		
		var isArray = (Object.prototype.toString.call(info) === "[object Array]");
		if(isArray){//多个条码
			var len = info.length;
			
			if(len == 0){
				model = this.getContent(info[0],model);
			}else{
				var array = model.split("LODOP."),
					model = "LODOP." + array[1] + "LODOP." + array[2],
					contentModelList = array.slice(3),
					contentList = [];
				for(var i in contentModelList){
					contentModelList[i] = "LODOP." + contentModelList[i];
				}
				var contentModel = contentModelList.join("");
				for(var i=0;i<len;i++){
					var content = this.getContent(info[i],contentModel);
					model += "LODOP.NEWPAGE();" + content;
				}
			}
		}else{//单个条码
			model = this.getContent(info,model);
		}
		eval(model);
	},
	/**获取内容*/
	getContent:function(info,model){
		if(!info) return model;
		
		model = model.replace(/{BarCode}/g,info.BarCode);
		model = model.replace(/{ClientName}/g,info.ClientName);
		model = model.replace(/{ClientName}/g,info.ClientName);
		model = model.replace(/{Name}/g,info.Name);
		model = model.replace(/{Sex}/g,info.Sex);
		model = model.replace(/{Age}/g,info.Age);
		model = model.replace(/{AgeUnit}/g,info.AgeUnit);
		model = model.replace(/{SickTypeName}/g,info.SickTypeName);
		model = model.replace(/{PatNo}/g,info.PatNo);
		model = model.replace(/{ColorName}/g,info.ColorName);
		model = model.replace(/{ItemList}/g,info.ItemList);
		model = model.replace(/{CollectDate}/g,info.CollectDate);
		return model;
	},
	/**获取默认的面板文件内容*/
	getDefaultModel:function(callback){
		var me = this;
		$.ajax({ 
			dataType:'text',
			async:false,
			url:me.DefaultModelUrl,
			success:function(result){
				callback(result);
			},
			error:function(request,strError){
				Shell.util.Msg.showLog("默认的条码模板文件获取失败！错误信息：" + strError);
				$.messager.alert("错误信息","<b style='color:red;'>默认的条码模板文件获取失败！</b>","error");
			} 
		});
	}
};