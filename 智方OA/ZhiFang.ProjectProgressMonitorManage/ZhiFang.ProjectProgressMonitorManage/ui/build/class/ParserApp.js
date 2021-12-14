/**
 * 应用解析器
 * @version 1.0
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserApp',{
	extend:'Ext.build.ParserBase',
	/**解析器版本号*/
	version:'ParserApp 1.0.0',
	/**公开的操作列表*/
	operateList:[],
	/**
	 * 解析方法
	 * @public
	 * @param {} DesignCode
	 * @return {}
	 */
	resolve:function(DesignCode){
		var me = this;
		if(!DesignCode){return null;}
		var appParams = me.getDesignCode(DesignCode);
		me.panelParams = appParams.panelParams;
		me.southParams = appParams.southParams;
		
		var ClassCode = me.getClassCode();
		return ClassCode;
	},
	/**
	 * 应用参数处理
	 * @private
	 * @param {} appInfo
	 */
	getAppParams:function(appInfo){
		var me = this;
		var id = appInfo.Id + "";
			
		if(id == "" || id == "-1"){//新增
			appInfo.BTDAppComponentsOperateList = me.changeString(Ext.JSON.encode(me.operateList));
			appInfo.BTDAppComponentsRefList = me.changeString(me.getStrByObj(appInfo.BTDAppComponentsRefList));
		}else{
			//应用操作
			var list = [];
			var oList = Ext.clone(appInfo.BTDAppComponentsOperateList);
			for(var i in me.operateList){
				var bo = false;
				for(var j in oList){
					if(me.operateList[i]['AppComOperateKeyWord'] == oList[j]['AppComOperateKeyWord'] && 
								oList[j]['DataTimeStamp'] && oList[j]['DataTimeStamp']!= ''){
						if(Ext.typeOf(oList[j]['DataTimeStamp']) === 'string'){
							oList[j]['DataTimeStamp'] = oList[j]['DataTimeStamp'].split(',');
						}
						list.push(oList[j]);
						bo = true;break;
					}
				}
				if(!bo){
					list.push(me.operateList[i]);
				}
			}
			appInfo.BTDAppComponentsOperateList = list && list.length > 0 ? me.changeString(Ext.JSON.encode(list)) : null;
			
			//应用关系
			var appParams = me.getDesignCode(appInfo.DesignCode);
			var appArr = appParams.southParams;
			var southList = [];
			for(var i in appArr){
				var o = {
					RefAppComID:appArr[i]['BTDAppComponents_Id'],//被引用应用GUID
					RefAppComIncID:appArr[i].itemId//被引用应用内部ID
				};
				southList.push(o);
			}
			var list2 = [];
			var rList = Ext.clone(appInfo.BTDAppComponentsRefList);
			
			for(var i in southList){
				var bo = false;
				for(var j in rList){
					if(southList[i]['RefAppComIncID'] == rList[j]['RefAppComIncID'] && 
								southList[i]['RefAppComID'] == rList[j]['RefAppComID'] && 
								rList[j]['DataTimeStamp'] && rList[j]['DataTimeStamp']!= ''){
						if(Ext.typeOf(rList[j]['DataTimeStamp']) === 'string'){
							rList[j]['DataTimeStamp'] = rList[j]['DataTimeStamp'].split(',');
						}
						list2.push(rList[j]);
						bo = true;break;
					}
				}
				if(!bo){
					list2.push(southList[i]);
				}
			}
			
			appInfo.BTDAppComponentsRefList = list2 && list2.length > 0 ? me.changeString(Ext.JSON.encode(list2)) : null;
		}
		return appInfo;
	},
	/**
	 * 获取类代码
	 * @private
	 */
	getClassCode:function(){
		var me = this;
		var params = me.panelParams;
		
		var extend = me.getExtend();//继承组件
		var layout = me.getLayout();//布局
		
		var ClassCode = 
		"Ext.define('" + params.appCode + "',{" + 
			"extend:'" + extend + "'," + 
			"panelType:'" + extend + "'," + 
			"alias:'widget." + params.appCode + "'," + 
			"title:'" + params.titleText + "'," + 
			"width:" + params.width + "," + 
			"height:" + params.height + "," + 
			(layout == "'absolute'" ? "autoScroll:true," : "") + 
			"bodyPadding:1," +
			"layout:" + layout + "," + 
			//"getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById'," + 
			"appInfos:" + me.getAppInfosStr() + "," + 
			"comNum:0," + 
			"afterRender:function(){" + 
				"var me=this;" + 
				"me.callParent(arguments);" + 
				//创建真实的内部应用
				"me.createItems();" + 
			"}," + 
			"createItems:function(){" + 
				"var me = this;" + 
				//所有内部应用信息
				"var appInfos=me.getAppInfos();" + 
				"for(var i in appInfos){" + 
					//应用ID
					"var id=appInfos[i].appId;" + 
					//回调函数
					"var callback=me.getCallback(appInfos[i]);" + 
					//util-根据ID获取应用
					"getAppInfo(id,callback,'BTDAppComponents_ClassCode',false);" + 
				"}" + 
			"}," + 
			"getAppInfos:function(){" + 
				"var me=this;" + 
				"var appInfos=me.appInfos;" + 
				"for(var i in appInfos){" + 
					"if(appInfos[i].title==''){" + 
						 "delete appInfos[i].title;" + 
					"}else if(appInfos[i].title=='_'){" + 
						"appInfos[i].title='';" + 
					"}" + 
				"}" + 
				"return Ext.clone(appInfos);" + 
			"}," + 
			"getCallback:function(appInfo){" + 
				"var me=this;" + 
				"var callback=function(obj){" + 
					"var panel=null;" + 
					"var callback2=function(panel){" + 
						"me.initLink(panel);" + //建立联动关系
					"};" + 
					//模块操作
					"var list=Ext.clone(me.moduleOperList);" + 
					"if(list){" + 
						"var appModuleOperList=[];" + 
						"for(var i=0;i<list.length;i++){" + 
							"var arr=list[i]['RBACModuleOper_UseCode'].split('.');" + 
							"if(arr.length>2&&arr[1]==appInfo.itemId){" + 
								"list[i]['RBACModuleOper_UseCode']=arr.slice(1).join('.');" + 
								"appModuleOperList.push(list[i]);" + 
							"}" + 
						"}" + 
						"var list2=[];" + 
						"for(var i=0;i<appModuleOperList.length;i++){" +  
							"var arr=appModuleOperList[i]['RBACModuleOper_UseCode'].split('.');" + 
							"if(arr.length==2){" + 
								"appInfo[arr[1]]=appModuleOperList[i]['RBACModuleOper_Id'];" + 
							"}else if(arr.length>2){" + 
								"list2.push(appModuleOperList[i]);" + 
							"}" + 
						"}" + 
						"if(list2.length>0){" + 
							"appInfo.moduleOperList=list2;" + 
						"}" + 
					"}" + 
					
					"appInfo.callback=callback2;" + 
					"if(obj.success&&obj.appInfo!=''){" + 
						"var ClassCode=obj.appInfo.ClassCode;" + 
						"var cl=eval(ClassCode);" + 
						"panel=Ext.create(cl,appInfo);" + 
					"}else{" + 
						"appInfo.html=obj.ErrorInfo;" + 
						"panel=Ext.create('Ext.panel.Panel',appInfo);" + 
					"}" + 
					"me.add(panel);" + 
					"if(me.panelType=='Ext.tab.Panel'){" + 
						"if(appInfo.defaultactive){me.defaultactive = appInfo.itemId;}" + 
						"me.setActiveTab(panel);" + 
					"}" + 
				"};" + 
				"return callback;" + 
			"}," + 
			"initLink:function(panel){" + 
				"var me=this;" + 
				"var appInfos=me.getAppInfos();" + 
				"var length=appInfos.length;" + 
				"me.comNum++;" + 
				"if(me.comNum==length){" + 
					"if(me.panelType=='Ext.tab.Panel'){" + 
						"var f=function(){me.setActiveTab(me.defaultactive);me.un('tabchange',f)};" + 
						"me.on('tabchange',f);" + 
					"}" + 
					me.getLinkageValueStr() + //联动关系
					"if(Ext.typeOf(me.callback)=='function'){me.callback(me);}" + 
				"}" + 
			"}" + 
		"});"; 
		return ClassCode;
	},
	/**
	 * 获取布局属性
	 * @private
	 * @return {}
	 */
	getLayout:function(){
		var me = this;
		var params = me.panelParams;
		var layout = "'auto'";//布局方式
		var layoutType = params.layoutType;//布局类型
		if(layoutType == "1"){//绝对定位
			layout = "'absolute'";
		}else if(layoutType == "2"){//border布局
			layout = "{type:'border',regionWeights:" + me.getBorderRegionWeights() + "}";
		}else if(layoutType == "3"){//tab布局
			//不做处理
		}else if(layoutType == "4"){//列布局
			layout = "'column'";
		}else if(layoutType == "5"){//FIT布局
			layout = "'fit'";
		}
		return layout;
	},
	/**
	 * 获取应用继承的组件
	 * @private
	 * @return {}
	 */
	getExtend:function(){
		var me = this;
		var params = me.panelParams;
		var extend = "Ext.panel.Panel";//继承组件
		if(params.layoutType == "3"){extend = "Ext.tab.Panel";}//tab布局
		return extend;
	},
	/**
	 * 获取border布局的版块先后属性
	 * @private
	 * @return {}
	 */
	getBorderRegionWeights:function(){
		var me = this;
		var params = me.panelParams;
		var borderObj = eval("(" + params.borderObjStr + ")");
		var regionWeightsStr = "{";
		for(var i in borderObj){
			if(i == 'top_priority'){
				var bo = (Ext.typeOf(borderObj[i]) === "number");
				regionWeightsStr += "north:" + (bo ? borderObj[i] : 0) + ","; 
			}else if(i == 'bottom_priority'){
				var bo = (Ext.typeOf(borderObj[i]) === "number");
				regionWeightsStr += "south:" + (bo ? borderObj[i] : 0) + ","; 
			}else if(i == 'left_priority'){
				var bo = (Ext.typeOf(borderObj[i]) === "number");
				regionWeightsStr += "west:" + (bo ? borderObj[i] : 0) + ","; 
			}else if(i == 'right_priority'){
				var bo = (Ext.typeOf(borderObj[i]) === "number");
				regionWeightsStr += "east:" + (bo ? borderObj[i] : 0) + ","; 
			}
		}
		if(regionWeightsStr.length > 1){regionWeightsStr = regionWeightsStr.slice(0,-1);}
		regionWeightsStr += "}";
		return regionWeightsStr;
	},
	/**
	 * 获取应用信息列表
	 * @private
	 * @return {}
	 */
	getAppInfos:function(){
		var me = this;
		var params = me.panelParams;
		var layoutType = params.layoutType;//布局类型
		var list = me.getSortList();
		var appInfos = [];
		for(var i in list){
			var obj = list[i];
			var o = {};
			o.appId = obj['BTDAppComponents_Id'];//功能ID
			o.itemId = obj['itemId'];//内部编号
			o.header = obj['header'];//标题栏
			o.title = obj['title'];//标题文字
			o.border = obj['border'];//有边框
			if(layoutType == "1"){//绝对定位
				o.x = parseInt(obj['x']);//x
				o.y = parseInt(obj['y']);//y
			}else if(layoutType == "2"){//border布局
				o.region = obj['region'];//region
				o.split = obj['split'];//可收缩
				o.collapsible = obj['collapsible'];//可收缩
				o.collapsed = obj['collapsed'];//默认收缩
			}else if(layoutType == "3"){//tab布局
				o.sequencenum = parseInt(obj['sequencenum']);//顺序号
				o.defaultactive = obj['defaultactive'];//默认页面
			}else if(layoutType == "5"){//FIT布局
				o.x = 0;//x
				o.y = 0;//y
			}
			if(obj['width'] && obj['width'] > 0){o.width = parseInt(obj['width']);}//宽度
			if(obj['height'] && obj['height'] > 0){o.height = parseInt(obj['height']);}//高度
			
			appInfos.push(o);
		}
		
		return appInfos;
	},
	/**
	 * 获取应用信息字符串
	 * @private
	 * @return {}
	 */
	getAppInfosStr:function(){
		var me = this;
		var appInfos = me.getAppInfos();
		var arr = [];
		for(var i in appInfos){
			var appInfo = appInfos[i];
			var str = "{";
			for(var j in appInfo){
				var type = Ext.typeOf(appInfo[j]);
				if(type == 'string'){
					str += j + ":'" + appInfo[j] + "',";
				}else{
					str += j + ":" + appInfo[j] + ",";
				}
			}
			if(str.length > 1){str = str.slice(0,-1) + "}";}
			arr.push(str);
		}
		return "[" + arr.join(",") + "]";
	},
	/**
	 * 获取排完序的应用信息列表
	 * @private
	 * @return {}
	 */
	getSortList:function(){
		var me = this;
		var list = me.southParams;
		var arr = me.getSortListByKey(list,'sequencenum');
		return arr;
	},
	/**
	 * 联动代码
	 * @private
	 * @return {}
	 */
	getLinkageValueStr:function(){
		var me = this;
		var params = me.panelParams;
		var linkageValue = me.getLink(params.linkageValue);
		var linkageValue2 = me.getLink2(params.linkageValue2);
		var link = linkageValue + linkageValue2;
		return link;
	},
	/**
	 * 获取简单代码
	 * @private
	 * @param {} value
	 * @return {}
	 */
	getLink:function(value){
		var me = this;
		var arr = me.linkageStrToArr(value);
		var v = "";
		for(var i in arr){
			//将联动代码分为主动和被动两端代码
			var linkArr = arr[i].split("==");
			if(linkArr.length == 2){
				//去掉==两端的空格
				linkArr[0] = linkArr[0].trim();
				linkArr[1] = linkArr[1].trim();
				var obj = me.getLinkStr(linkArr,"me");
				if(obj){
					v += obj;
				}else{
					alertError("联动代码解析有误!");
				}
			}else{
				return "";
				alertError("联动代码解析有误!");
			}
		}
		return v;
	},
	/**
	 * 获取复杂代码
	 * @private
	 * @param {} value
	 * @return {}
	 */
	getLink2:function(value){
  		var v = "";
  		if(value && value != ""){
  			var arr = value.split('\n');//根据换行符
	  		for(var i in arr){
	  			v += Ext.String.trim(arr[i]);
	  		}
  		}
		return v;
	},
	/**
	 * 简单代码串转化为代码数组
	 * @private
	 * @param {} value
	 * @return {}
	 */
	linkageStrToArr:function(value){
		var result = [];
		var arr = value.split(";");
		for(var i in arr){
			var str = arr[i].trim();//首尾去掉空格及换行符
			if(str != ""){
				result.push(str);
			}
		}
		return result;
	},
	/**
	 * 生成联动关系代码串
	 * @private
	 * @param {} linkArr
	 * @return {}
	 */
	getLinkStr:function(linkArr,objStr){
		var me = this;
		var result = null;
		
		var act = linkArr[0].split("(");
		var actArr = act[0].split(".");//主动方
		var funArr = linkArr[1].split(".");//被动方
		
		//需要解析的代码不符合格式
		if(actArr.length < 2 || funArr.length < 2){
			return result;
		}
		
		//主动方代码
		var actObjStr = objStr;
		var actObjName = "";
		for(var i=0;i<actArr.length-1;i++){
			actObjStr += ".getComponent('" + actArr[i] + "')";
			actObjName += "_" + actArr[i];
		}
		var testStr = "";
		for(var i=0;i<actObjName.length;i++){
			var num = actObjName.charCodeAt(i);
			if(num > 128){
				testStr += num;
			}else{
				testStr += actObjName[i];
			}
		}
		
		actObjName = testStr;
		
		actObjStr = "var " + actObjName + "=" + actObjStr + ";";
		
		//被动方代码
		var funObjStr = objStr;
		var funObjName = "";
		for(var i=0;i<funArr.length-1;i++){
			funObjStr += ".getComponent('" + funArr[i] + "')";
			funObjName += "_" + funArr[i];
		}
		funObjStr = "var " + funObjName + "=" + funObjStr + ";";
		
		var actEvent= act.length == 2 ? act[1].slice(0,-1) : "";//参数串
		var actEventName = actArr[actArr.length-1];//主动方事件名
		var actEventParNameArr = actEvent.split(",");//主动方参数数组
		var hqlObj = {};//hql信息对象
		if(actEvent == ""){//没有参数
			actEventParNameArr = "";
		}else{//有参数
			if(actEvent.slice(0,2) == "##"){//hql串
				actEventParNameArr = actEvent;
				hqlObj = me.getHql(actEvent.slice(2));
			}
		}
		
		var funFunName = funArr[funArr.length-1];//被动方方法名
		
		
		var str = "";
		str += actObjStr;
		
		//新增、删除[列表、树]、保存[表单]、重置[高级查询]
		if(actEventName == "addClick" || actEventName == "delClick" || actEventName == "saveClick" || actEventName == "resetClick"){
			str += actObjName + ".on({" + actEventName + ":function(but){";
			//str += funObjStr;
			//str += funObjName + "." + funFunName + "();";
			//str += "}});"
			
			if(funFunName == "updateNode"){
				str += "var com=" + actObjName + ";" + 
				"var type=com.type;" + 
				"var values=com.getForm().getValues();" + 
				"var type=com.type;" + 
				"var obj={" +
					"id:values[com.objectName+'_Id']," +
					"pId:values[com.objectName+'_ParentID']," +
					"text:values[com.objectName+'_CName']" + 
				"};";
				str += funObjStr;
				str += funObjName + "." + funFunName + "(type,obj);";
			}else if(funFunName == "setValueByItemId"){
				str += "var list=" + actObjName + ";";
				str += "var records=list.getSelectionModel().getSelection();";
				str += "var reocrd=records[0];";
				var par = [];
				for(var i=0,l=actEventParNameArr.length;i<l;i++){
					var v = actEventParNameArr[i];
					if(v[0] == "{" && v.slice(-1) == "}"){
						str += "var _" + i + "=record.get('" + v.slice(1,-1) + "');";
					}else{
						str += "var _" + i + "=" + v + ";";
					}
					par.push("_" + i);
				}
				str += funObjStr;
				str += funObjName + "." + funFunName + "(" + par.join(",") + ");";
			}else{
				str += funObjStr;
				if(actEventName == "saveClick"){
					str += funObjName + ".autoSelect=" + actObjName + ".getForm().getValues()[" + actObjName + ".objectName+'_Id'];";
					str += funObjName + "." + funFunName + "(true);";
				}else{
					str += funObjName + "." + funFunName + "();";
				}
			}
			str += "}});"
			
		}else if(actEventName == "itemclick" || actEventName == "itemdblclick" || actEventName == "select"){//行单击、行双击事件（列表、树）
			str += actObjName + ".on({" + actEventName + ":function(view,record){";
			//延时处理
			str += "Shell.util.Action.delay(function(){";
			
			if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
				str += "var id=record.get(" + actObjName + ".objectName+'_Id');";
				str += funObjStr;
				str += funObjName + "." + funFunName + "(id);";
			}else{
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
					for(var i in hqlObj.parArr){
						str +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
					}
					str += "var hql=" + hqlObj.hql + ";";
					str += funObjStr;
					str += funObjName + "." + funFunName + "(hql);";
				}else{
//					for(var i in actEventParNameArr){
//						str += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
//					}
//					str += funObjStr;
//					str += funObjName + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
					
					//-----------------------------------
					var par = [];
					for(var i=0,l=actEventParNameArr.length;i<l;i++){
						var v = actEventParNameArr[i];
						if(v[0] == "{" && v.slice(-1) == "}"){
							str += "var _" + i + "=record.get('" + v.slice(1,-1) + "');";
						}else{
							str += "var _" + i + "=" + v + ";";
						}
						par.push("_" + i);
					}
					str += funObjStr;
					str += funObjName + "." + funFunName + "(" + par.join(",") + ");";
				}
			}
			str += "},null,500);";
			str += "}});"
		}else if(actEventName == "editClick" || actEventName == "showClick"){//修改、查看（列表、树）
			str += actObjName + ".on({" + actEventName + ":function(but){";
			str += "var list=" + actObjName + ";";
			str += "var records=list.getSelectionModel().getSelection();";
			str += "if(records.length==1){";
			str += "var record=records[0];";
			if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr == ""){
				str += "var id=record.get(" + actObjName + ".objectName+'_Id');";
				str += funObjStr;
				str += funObjName + "." + funFunName + "(id);";
			}else{
				if(Ext.typeOf(actEventParNameArr) == "string" && actEventParNameArr.slice(0,2) == "##"){//hql串
					for(var i in hqlObj.parArr){
						str +=  "var " + hqlObj.parArr[i] + "=record.get('" + hqlObj.parArr[i] + "');";
					}
					str += "var hql=" + hqlObj.hql + ";";
					str += funObjStr;
					str += funObjName + "." + funFunName + "(hql);";
				}else{
//					for(var i in actEventParNameArr){
//						str += "var " + actEventParNameArr[i] + "=record.get('" + actEventParNameArr[i] + "');";
//					}
//					str += funObjStr;
//					str += funObjName + "." + funFunName + "(" + actEventParNameArr.join(",") + ");";
					
					//-----------------------------------
					var par = [];
					for(var i=0,l=actEventParNameArr.length;i<l;i++){
						var v = actEventParNameArr[i];
						if(v[0] == "{" && v.slice(-1) == "}"){
							str += "var _" + i + "=record.get('" + v.slice(1,-1) + "');";
						}else{
							str += "var _" + i + "=" + v + ";";
						}
						par.push("_" + i);
					}
					str += funObjStr;
					str += funObjName + "." + funFunName + "(" + par.join(",") + ");";
				}
			}
			str += "}else{alertError('请选择一条数据进行操作！');}";
			str += "}});"
		}else if(actEventName == "selectClick"){//查询[高级查询、分组查询]
			str += actObjName + ".on({" + actEventName + ":function(but){";
			str += "var com=" + actObjName + ";";
			str += "var where=com.getValue();";
			str += funObjStr;
			str += funObjName + "." + funFunName + "(where);";
			str += "}});"
		}
		result = str;
		return result;
	},
	/**
	 * 获取hql结果
	 * @private
	 * @param {} value
	 * @return {}
	 */
	getHql:function(value){
		var me = this;
		var result = {parArr:[],hql:''};//参数数组、hql串
		
		result.hql = value.replace(/\'/g,"\\\'");
		var reg = /(\{.*?\})/g;//提取所有的{}中的字符串
		var arr = value.match(reg);
		result.parArr = arr;
		
		result.hql = "'" + result.hql + "'";
		
		for(var i in arr){
			result.hql = result.hql.replace(arr[i],("'+" + arr[i].slice(1,-1) + "+'"));
			arr[i] = arr[i].slice(1,-1);
		}
		if(result.hql.slice(-3) == "+''"){
			result.hql = result.hql.slice(0,-3);
		}
		return result;
	}
});