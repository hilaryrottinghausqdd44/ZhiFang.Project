/**
 * 表单解析器
 * @version 1.0.1
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserForm101',{
	extend:'Ext.build.ParserForm',
	/**解析器版本号*/
	version:'ParserForm 1.0.1',
	/**获取应用操作列表服务地址*/
	getAppOperateListUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsOperateByHQL',
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
		me.panelParams = appParams.panelParams || {};
		me.southParams = appParams.southParams || [];
		me.south2Params = appParams.south2Params || [];
		me.hasLab = (me.panelParams.hasLab + '' == 'true' ? true :false);
		if(me.panelParams.formHtml){
			me.panelParams.formHtml = me.panelParams.formHtml.replace(/\"/g,"@@");
		}
		var ClassCode = me.getClassCode();
		return ClassCode;
	},
	/**
	 * 获取应用信息
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppInfoById:function(id,callback){
		var me = this;
		var url = me.getAppInfoUrl + "?isPlanish=true&id=" + id + "&fields=" + me.appFields.join(',');
		var c = function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				var data = {};
				var appInfo = null;
                if(result.ResultDataValue && result.ResultDataValue != ""){
                	result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
                	appInfo = Ext.JSON.decode(result.ResultDataValue);
                	var appParams = Ext.JSON.decode(appInfo['BTDAppComponents_DesignCode']);
                	for(var i in appInfo){
                		data[i.split('_').slice(-1)] = appInfo[i];
                	}
                }
				if(appInfo){
					var ca = function(list){
						data['BTDAppComponentsOperateList'] = list;
						Ext.typeOf(callback) === 'function' && callback(data);
					};
					me.getAppOperateList(id,ca);
				}else{
					alertError('没有获取到列表信息数据！');return;
				}
			}else{
				alertError(result.ErrorInfo);return;
			}
		};
		getToServer(url,c);
	},
	/**
	 * 获取应用操作列表
	 * @private
	 * @param {} id
	 * @param {} callback
	 */
	getAppOperateList:function(id,callback){
		var me = this;
		var fields = "BTDAppComponentsOperate_Id,BTDAppComponentsOperate_AppComOperateKeyWord,BTDAppComponentsOperate_AppComOperateName,BTDAppComponentsOperate_DataTimeStamp";
		var where = "";
		var url = me.getAppOperateListUrl + "?isPlanish=true&&page=1&limit=1000&fields=" + fields + "&where=" + where;
		var c = function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				var list = [];
				if(result.ResultDataValue && result.ResultDataValue != ""){
					var data = Ext.JSON.decode(result.ResultDataValue);
					for(var i in data.list){
						var obj = data.list[i];
						var o = {};
						for(var j in obj){
							o[j.split('_').slice(-1)] = obj[j];
						}
						list.push(o);
					}
				}
				Ext.typeOf(callback) === 'function' && callback(list);
			}else{
				alertError(result.ErrorInfo);return;
			}
		};
		getToServer(url,c);
	}
});