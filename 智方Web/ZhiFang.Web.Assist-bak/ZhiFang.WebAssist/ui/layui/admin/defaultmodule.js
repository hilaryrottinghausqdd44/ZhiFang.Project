/**
	@name：无集成平台时,血库手工维护的功能模块
	@author：longfc
	@version 2019-08-24
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
}).define(['util', 'uxutil'], function(exports) {
	"use strict";

	var defaultmodule = {
		config: {

		}
	};
	//无集成平台时,血库手工维护的默认功能模块
	defaultmodule.loadDefaultModule = function() {
		var me = this;

		var list = {
			"Tree": [{
					"Tree": [{
						"Tree": [ {
							"Tree": [],
							"text": "字典类型维护",
							"expanded": false,
							"leaf": true,
							"icon": "configuration.PNG",
							"iconCls": null,
							//"url": "#Shell.class.sysbase.dicttype.App",
							//"url": "/ui/layui/views/bloodtransfusion/sysbase/bdicttype/index.html",
							"tid": "5149694234043012172",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "字典维护",
							"expanded": false,
							"leaf": true,
							"icon": "configuration.PNG",
							"iconCls": null,
							"url": "#Shell.class.sysbase.dict.App",
							//"url": "/ui/layui/views/bloodtransfusion/sysbase/bdict/index.html",
							"tid": "5537982840004094800",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}],
						"text": "基础维护",
						"expanded": false,
						"leaf": false,
						"icon": "package.PNG",
						"iconCls": null,
						"url": "",
						"tid": "5106839889475604758",
						"pid": "4615675850682066290",
						"objectType": null,
						"Para": "",
						"value": null
					}, {
						"Tree": [],
						"text": "医生站",
						"expanded": false,
						"leaf": false,
						"icon": "package.PNG",
						"iconCls": null,
						"url": "",
						"tid": "4771436314726617443",
						"pid": "4615675850682066290",
						"objectType": null,
						"Para": "",
						"value": null
					}, {
						"Tree": [],
						"text": "护士站",
						"expanded": false,
						"leaf": true,
						"icon": "package.PNG",
						"iconCls": null,
						"url": "",
						"tid": "5332412256036386096",
						"pid": "4615675850682066290",
						"objectType": null,
						"Para": "",
						"value": null
					}],
					"text": "院感系统",
					"expanded": true,
					"leaf": false,
					"icon": "package.PNG",
					"iconCls": null,
					"url": "",
					"tid": "4615675850682066290",
					"pid": "0",
					"objectType": null,
					"Para": "",
					"value": null
				},
				{
					
					"text": "系统基础设置",
					"expanded": true,
					"leaf": false,
					"icon": "package.PNG",
					"iconCls": null,
					"url": "",
					"tid": "4656964878417007884",
					"pid": "0",
					"objectType": null,
					"Para": "",
					"value": null,
					"Tree": [{
						"Tree": [],
						"text": "系统全局参数",
						"expanded": false,
						"leaf": true,
						"icon": "default.PNG",
						"iconCls": null,
						"url": "#Shell.class.sysbase.bparameter.App?issys=1",
						"tid": "5443848316164652500",
						"pid": "4656964878417007884",
						"objectType": null,
						"Para": "",
						"value": null
					},{
						"Tree": [],
						"text": "功能模块",
						"expanded": false,
						"leaf": true,
						"icon": "default.PNG",
						"iconCls": null,
						"url": "#Shell.class.sysbase.module.App",
						"tid": "100",
						"pid": "0",
						"objectType": null,
						"Para": "",
						"value": null
					}]
				}
			],
			"success": true,
			"ErrorInfo": ""
		};
		return list;
	};
	//暴露接口
	exports('defaultmodule', defaultmodule);
});