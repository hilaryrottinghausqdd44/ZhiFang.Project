/**
 * 程序基础配置页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element','layer'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		element = layui.element,
		$ = layui.jquery,
		layer = layui.layer;
	var app = {};
	app.dataList = [];
	app.url = {
		//获取全局设置
		selectUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/LoadWebConfig",
		AddUrl: uxutil.path.ROOT +"/ServiceWCF/ReportFormService.svc/UpdateWebConfig"
	};
	//初始化  
	app.init = function () {
		var me = this;
		me.GetAllSetting();
		me.initHtml();
		layer.open({
			title: '提示信息'
			, content: '修改配置时请停止使用程序防止出现故障！'
			, skin: 'layui-layer-molv' //样式类名
		});
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//保存按钮
		layui.$('#saveSetting').on('click', function () { me.savePublicSetting() });
	}
	app.GetAllSetting = function () {
		
		var me = this;
		var url = me.url.selectUrl;
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
						me.dataList = value;
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
		var items = me.dataList;//查询出来的设置
		var settings = {};
		for (var i = 0; i < items.length; i++) {
			//settings[dataList[j].ParaNo] = dataList[j].ParaValue;
			if (items[i].key.indexOf("ConnectionString") >= 0) {
				var values = items[i].value.split(';');//获取键对应的value值并根据;分割data source=192.168.31.111;user id=sa;password=sqlserver@123;initial catalog=digitlab
				for (var a = 0; a < values.length; a++) {//将value拆分成单个配置项
					var sonitem = values[a].split('=');
					var inputName = items[i].key + "_" + sonitem[0];//拼接组件的name
					settings[inputName] = sonitem[1];//对应赋值
				}
			} else {
				settings[items[i].key] = items[i].value;
			}
		}
		form.val("settingForm", settings);
	}
	app.savePublicSetting = function () {
		var me = this;
		
		//2.获取页面表单的对象
		var settingForm = form.val("settingForm");//是一个对象{name:value,name:value...}
		
		//4.处理构造入参
		var list = [];
		var connectionStringObject = {};
		for (var key in settingForm) {
			var hash = {};
			if (key.indexOf("ConnectionString") >= 0) {//key:ReportFormQueryPrintConnectionString_data source
				var keyNameList = key.split("_");//keyNameList[0]:ReportFormQueryPrintConnectionString ,keyNameList[1]:data source
				var object = {};
				object[keyNameList[1]] = settingForm[key];
				if (connectionStringObject[keyNameList[0]]) {
					connectionStringObject[keyNameList[0]][keyNameList[1]] = settingForm[key];
				} else {
					connectionStringObject[keyNameList[0]] = object;
                }
				
				
			} else {
				hash["key"] = key;
				hash["value"] = settingForm[key]; 
				list.push(hash);
            }
		}
		for (var key in connectionStringObject) {
			var hash = {};
			var value = "";
			var object = connectionStringObject[key];
			for (var key1 in object) {
				value += key1 + "=" + object[key1];
				if (key1 !="initial catalog") {
					value +=";"
                }
            }
			hash.key = key;
			hash.value = value;
			list.push(hash);
		}
		//console.log(JSON.stringify(list));
		//5.保存设置
		uxutil.server.ajax({
			url: me.url.AddUrl,
			async: false,
			type: "post",
			data: JSON.stringify({ "model": list })
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("保存成功");
					
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