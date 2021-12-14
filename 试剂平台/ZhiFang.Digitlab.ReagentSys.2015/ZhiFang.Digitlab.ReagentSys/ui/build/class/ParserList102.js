/**
 * 列表解析器
 * @version 1.0.2
 * @author Jcall
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ParserList102',{
	extend:'Ext.build.ParserList',
	/**解析器版本号*/
	version:'ParserList 1.0.2',
	/**
	 * 应用参数处理
	 * @private
	 * @param {} appInfo
	 */
	getAppParams:function(appInfo){
		var me = this;
		var id = appInfo.Id + "";
		var list = [];
		if(id == "" || id == "-1"){//新增
			list = me.operateList;
		}else{
			var list = [];
			var oList = Ext.clone(appInfo.BTDAppComponentsOperateList);
			for(var i in me.operateList){
				var bo = false;
				for(var j in oList){
					if(me.operateList[i]['AppComOperateKeyWord'] == oList[j]['AppComOperateKeyWord']){
						oList[j]['DataTimeStamp'] = oList[j]['DataTimeStamp'].split(',');
						list.push(oList[j]);
						bo = true;break;
					}
				}
				if(!bo){
					list.push(me.operateList[i]);
				}
			}
		}
		var appParams = me.getDesignCode(appInfo.DesignCode);
		if(appParams.panelParams.defaultParams && appParams.panelParams.defaultParams){
			appParams.panelParams.defaultParams = appParams.panelParams.defaultParams.replace(/'/g,"##").replace(/\%/g,"@@");
		}
		appInfo.DesignCode = me.getStrByObj(appParams);
		for(var i in list){
			list[i].RowFilterBase = appParams.panelParams.objectName;//行过滤依据对象
		}
		appInfo.BTDAppComponentsOperateList = list && list.length > 0 ? me.changeString(Ext.JSON.encode(list)) : null;
		return appInfo;
	}
});