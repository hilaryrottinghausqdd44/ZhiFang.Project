/**
 * 解析器基类
 * @version 1.0
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserBase',{
	/**解析器版本号*/
	version:'ParserBase 1.0.0',
	/**新增应用信息的服务地址*/
	addAppInfoUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_AddBTDAppComponents',
	/**修改应用信息的服务地址*/
	updateAppInfoUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents',
	/**获取应用信息的服务地址*/
	getAppInfoUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	/**前台需要的应用字段*/
	appFields:[
		'BTDAppComponents_Id',//主键ID
		'BTDAppComponents_CName',//中文名称
		'BTDAppComponents_DesignCode',//设计代码
		'BTDAppComponents_ClassCode',//类代码
		'BTDAppComponents_ModuleOperCode',//功能编码
		'BTDAppComponents_ModuleOperInfo',//功能简介
		'BTDAppComponents_InitParameter',//初始化参数
		'BTDAppComponents_AppType',//应用类型
		
		'BTDAppComponents_EName',//英文名称
		'BTDAppComponents_BuildType',//构建类型
		'BTDAppComponents_Creator',//创建者
		'BTDAppComponents_Modifier',//修改者
		'BTDAppComponents_PinYinZiTou',//拼音字头
		'BTDAppComponents_DataUpdateTime',//更新时间
		'BTDAppComponents_LabID',//LabID
		'BTDAppComponents_DataAddTime',//创建时间
		'BTDAppComponents_DataTimeStamp',//时间戳
		
		'BTDAppComponents_BTDAppComponentsOperateList_Id',//应用操作ID
		'BTDAppComponents_BTDAppComponentsOperateList_AppComOperateKeyWord',//应用操作编号
		'BTDAppComponents_BTDAppComponentsOperateList_AppComOperateName',//应用操作名称
		'BTDAppComponents_BTDAppComponentsOperateList_DataTimeStamp',//应用操作时间戳
		"BTDAppComponents_BTDAppComponentsOperateList_LabID",//关系表实验室ID
		"BTDAppComponents_BTDAppComponentsOperateList_Creatoro",
		"BTDAppComponents_BTDAppComponentsOperateList_Modifier",
		"BTDAppComponents_BTDAppComponentsOperateList_DataAddTime",
		"BTDAppComponents_BTDAppComponentsOperateList_DataUpdateTime",
		"BTDAppComponents_BTDAppComponentsOperateList_RowFilterBase",
		
		"BTDAppComponents_BTDAppComponentsRefList_RefAppComID",//被引用应用GUID
		"BTDAppComponents_BTDAppComponentsRefList_RefAppComIncID",//被引用应用内部ID
		"BTDAppComponents_BTDAppComponentsRefList_LabID",//关系表实验室ID
		"BTDAppComponents_BTDAppComponentsRefList_Id",//关系表GUID
		"BTDAppComponents_BTDAppComponentsRefList_DataTimeStamp",//关系表时间戳
		"BTDAppComponents_BTDAppComponentsRefList_Creatoro",
		"BTDAppComponents_BTDAppComponentsRefList_Modifier",
		"BTDAppComponents_BTDAppComponentsRefList_DataAddTime",
		"BTDAppComponents_BTDAppComponentsRefList_DataUpdateTime"
	],
	/**
	 * 根据ID更新类代码
	 * @public
	 * @param {} id
	 * @param {} callback
	 */
	updateAppInfoById:function(id,callback){
		var me = this;
		var c = function(appInfo){
			me.saveAppInfo(appInfo,callback);
		};
		me.getAppInfoById(id,c);
	},
	/**
	 * 保存应用信息
	 * @public
	 * @param {} appInfo
	 * @param {} callback
	 */
	saveAppInfo:function(config,callback){
		var appInfo = Ext.clone(config);
		var me = this;
		var id = appInfo.Id + "";
		var params = me.getAppParams(appInfo);
		params.DesignCode = me.changeString(params.DesignCode);
		params.ClassCode = me.resolve(appInfo['DesignCode']);
		params.EName = me.version;//版本信息暂时存在EName字段
		var url = (id == "" || id == "-1") ? me.addAppInfoUrl : me.updateAppInfoUrl;
		var defaultPostHeader = 'application/x-www-form-urlencoded';//请求头信息
		//保存应用信息
		postToServer(url,params,callback,defaultPostHeader);
	},
	/**
	 * 获取应用信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppInfoById:function(id,callback){
		var me = this;
		if(id && id != -1){
			var url = me.getAppInfoUrl + "?isPlanish=false&id=" + id + "&fields=" + me.appFields.join(',');
			var c = function(text){
				var result = Ext.JSON.decode(text);
				if(result.success){
					result.ResultDataValue = result.ResultDataValue.replace(/\\n/g,"");
					var data = Ext.JSON.decode(result.ResultDataValue);
					if(data){
						Ext.typeOf(callback) === 'function' && callback(data);
					}else{
						alertError('没有获取到列表信息数据！');
					}
				}else{
					alertError(result.ErrorInfo);
				}
			};
			getToServer(url,c);
		}
	},
	/**
	 * 获取应用参数
	 * 各个扩展类可以根据需要各自做数据修改处理
	 * @private
	 * @param {} params
	 * @return {}
	 */
	getAppParams:function(params){
		return params;
	},
	/**
	 * 解析器基类解析方法
	 * 各个扩展类各自重写解析方法
	 * @private
	 * @param {} DesignCode
	 */
	resolve:function(DesignCode){
		var info = "请重写解析器基类的resolve方法";
		alert(info);
	},
	/**
	 * 设计代码对象化
	 * @private
	 * @param {} DesignCode
	 * @return {}
	 */
	getDesignCode:function(DesignCode){
		var obj = null;
		var type = Ext.typeOf(DesignCode);
		if(type === 'object'){
			obj = DesignCode;
		}else if(type === 'string'){
			var str = Ext.clone(DesignCode);
			str = str.replace(/[\r\n]+/g,"");
			obj = Ext.JSON.decode(str);
		}
		return obj;
	},
	/**
	 * 将字符转化
	 * @private
	 * @param {} value
	 * @return {}
	 */
	changeString:function(value){
		var str = value;
		if(!str || str == ''){return "";}
		
		if(Ext.typeOf(value) === 'object'){
			if(value.formHtml){
				value.formHtml = value.formHtml.replace(/\'/g,"@@");
			}
			str = Ext.JSON.encode(value);
		}
		//var str = str ? str.replace(/\"/g,"'") : "";
		str = str.replace(/\\\"/g,"@@").replace(/\\\\\'/g,"\\'");
		return str;
	},
	/**
	 * 根据排序主键重新给列表排序
	 * @private
	 * @param {} list
	 * @param {} key
	 * @param {} sort asc、desc
	 * @return {}
	 */
	getSortListByKey:function(list,key,sort){
		var me = this;
		var arr = list;
		var s = sort || 'asc';
		
		var num = 0;
		for(var i=0,len=arr.length-1;i<len;i++){
			num = i;
			for(var j=i+1,len2=arr.length;j<len2;j++){
				if(arr[j][key] < arr[num][key]){
					num = j;
				}
			}
			if(num != i){
				var temp = arr[i];
				arr[i] = arr[num];
				arr[num] = temp;
			}
		}
		
		return arr;
	},
	/**
	 * 对象转化成字符串
	 * @private
	 * @param {} obj
	 * @return {}
	 */
	getStrByObj:function(obj){
		var encodeArray = function(o){
			var a = ["[",""];
			var length = o.length;
			for(var i=0;i<length;i++){
				a.push(encodeValue(o[i]),",");
			}
			a[a.length-1] = "]";
			return a.join("");
		};
		var encodeKey = function(value){
			return "'" + value + "'";
		};
		var encodeObj = function(o){
			var a = ["{",""];
			for(var i in o){
				a.push(encodeKey(i),":",encodeValue(o[i]),",")
			}
			a[a.length-1] = "}";
			return a.join("");
		};
		
		var encodeValue = function(value){
			var type = Ext.typeOf(value);
			if(type === 'null' || type === 'undefined'){
				return "null";
			}else if(type === 'number' || type === 'boolean'){
				return value + "";
			}else if(type === 'string'){
				return "'" + value + "'";
			}else if(type === 'array'){
				return encodeArray(value);
			}else if(type === 'object'){
				return encodeObj(value);
			}
		};
		
		return encodeValue(obj);
	}
});