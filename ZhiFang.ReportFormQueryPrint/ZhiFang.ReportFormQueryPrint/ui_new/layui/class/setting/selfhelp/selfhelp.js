/**
 * 自助页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		element = layui.element,
		$ = layui.jquery;
	var app = {};
	
	app.dataList = [];//查询出的对应站点全局设置
	app.dataListDefult = [];//默认的全局配置
	app.url = {
		//获取全局设置
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetAllPublicSetting', 
		//更新全局设置
		AddUrl: uxutil.path.ROOT + "/ServiceWCF/DictionaryService.svc/UpdatePublicSetting"
	};
	//get参数
	app.paramsObj = {
		ModuleID: '123',//模块id
		module: ''//模块
	};
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		//获取模块id
		if (params.MODULEID) {
			me.paramsObj.ModuleID = params.MODULEID;
		}
		if (params.MODULE) {
			me.paramsObj.module = params.MODULE;
		}

	};
	//初始化  
	app.init = function () {
		var me = this;
		me.getParams();
		me.getGlobalSetting(me.paramsObj.module, "dataList");
		me.getGlobalSetting("自助打印", "dataListDefult");//需要获取默认全局配置，获取ParaDesc
		me.initHtml();
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//保存按钮
		layui.$('#saveSetting').on('click', function () { me.savePublicSetting() });
	}
	/**
	 * 获取对应站点的全局配置
	 * @param {string} pageType
	 * @param {string} dataListName
	 */
	app.getGlobalSetting = function (pageType, dataListName) {
		var me = this;
		var url = me.url.selectUrl + '?pageType=' + pageType;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {

			if (data) {
				if (!data.success) {
					layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;
			} else {
				layer.msg(data.msg);
			}
		});
	}
	/**初始化页面元素 */
	app.initHtml = function () {
		var me = this;
		var settings = {};
		var dataList = me.dataList;//查询出来的设置
		for (var j = 0; j < dataList.length; j++) {
			settings[dataList[j].ParaNo] = dataList[j].ParaValue;
			
		}
		form.val("settingForm", settings);
	}
	/** 保存设置*/
	app.savePublicSetting = function () {
		var me = this;
		var pageType = me.paramsObj.module;
		
		//1.获取对应页面配置项
		//var settings = this.pageTypeHaveSetting[pageType];//获取对应的拥有的setting
		//2.获取页面表单的对象
		var settingForm = form.val("settingForm");//是一个对象{name:value,name:value...}
		//3.获取查询出来的设置
		var dataList = me.dataList;
		//4.处理构造入参
		var list = [];
		for (var key in settingForm) {
			var hash = {};
			hash["ParaValue"] = settingForm[key];
			hash["ParaNo"] = key;
			hash["SName"] = me.paramsObj.module;
			hash["Name"] = "查询打印页面配置";
			hash["ParaType"] = "config";
			for (var j = 0; j < me.dataListDefult.length; j++) {
				if (key == me.dataListDefult[j].ParaNo) {
					hash["ParaDesc"] = me.dataListDefult[j].ParaDesc;
					break;
				}
			}
			list.push(hash);
		}
		
		//5.保存设置
		uxutil.server.ajax({
			url: me.url.AddUrl,
			async: false,
			type: "post",
			data: JSON.stringify({ "models": list })
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("保存成功");
					me.init();
				} else {
					layer.msg("保存失败");
				}

			} else {
				layer.msg("保存失败");
			}
		});
	}
	app.init();
});