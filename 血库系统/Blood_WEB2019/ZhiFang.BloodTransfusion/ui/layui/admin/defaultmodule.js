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
						"Tree": [{
							"Tree": [],
							"text": "用血说明维护",
							"expanded": false,
							"leaf": true,
							"icon": "configuration.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html",
							"tid": "5164326499587636967",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
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
						}, {
							"Tree": [],
							"text": "就诊类型",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "4968545458301997231",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "申请类型",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "5023831077536868950",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "项目字典",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "4679646658914060197",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "血制品类型",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "5696884744325257218",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "血制品单位",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "5490990116535175041",
							"pid": "5106839889475604758",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "血制品维护",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "",
							"tid": "5057780935980321192",
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
						"Tree": [{
							"Tree": [],
							"text": "用血申请",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/doctorstation/apply/index.html?type=1",
							"tid": "4719933149826000122",
							"pid": "4771436314726617443",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "用血申请+",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html?type=1",
							"tid": "4719933149826000123",
							"pid": "4771436314726617443",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "上级审核",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/doctorstation/senior/index.html?type=1",
							"tid": "5206146653589787031",
							"pid": "4771436314726617443",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "科主任审核",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/doctorstation/director/index.html?type=1",
							"tid": "4875638971177472497",
							"pid": "4771436314726617443",
							"objectType": null,
							"Para": "",
							"value": null
						}, {
							"Tree": [],
							"text": "医务处审批",
							"expanded": false,
							"leaf": true,
							"icon": "list.PNG",
							"iconCls": null,
							"url": "/ui/layui/views/bloodtransfusion/doctorstation/medical/index.html?type=1",
							"tid": "4850720490879302639",
							"pid": "4771436314726617443",
							"objectType": null,
							"Para": "",
							"value": null
						}],
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
					"text": "血库系统",
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