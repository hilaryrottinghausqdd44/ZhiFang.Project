/**
 * @name：views/pre/comm/BarCodePrint/print
 * @author：zhangda
 * @version 2021-08-16
 * 
 * 使用：
        modelInstance = Model.render();//初始化打印模板实例
		//list = [{},{}];
        //data:当前打印数据 isLastOne：是否是最后一个
        modelInstance.print(list, "Microsoft Print to PDF", function (data, isLastOne) { console.log(data); console.log(isLastOne); });
 * 
 */
layui.extend({
	uxutil: 'ux/util',
	print: 'ux/print'
}).define(['uxutil', 'form', 'print'], function (exports) {
	"use strict";

	var $ = layui.$,
		form = layui.form,
		print = layui.print,
		uxutil = layui.uxutil,
		MOD_NAME = 'Model';

	//打印模板控件地址
	var ModelFileUrl = uxutil.path.UI + '/src/zhifang/print/Model.js?t=' + new Date().getTime();

	//医嘱单列表
	var Model = {
		//对外参数
		config: {
			printer: null,//打印机
			modelUrl: uxutil.path.UI + '/layui/views/system/comm/BarCodePrint/model/model.txt'
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, Model.config, setings);
	};
	//初始化
	Class.prototype.init = function () {
		var me = this;
		//加载打印基础文件
		me.loadFile(function () { });
	};
	//加载打印基础文件
	Class.prototype.loadFile = function (callback) {
		var me = this;
		$.getScript(ModelFileUrl).done(function () {
			callback && callback();
		}).fail(function () {
			layer.msg("Model打印组件加载失败，请重新尝试打印！");
		});
	};
	//打印
	Class.prototype.print = function (list,printer,callback) {
		var me = this,
			arr = list || [],//打印数据 数组
			modelUrl = me.config.modelUrl + '?v=' + new Date().getTime(),
			printer = printer || me.config.printer || null;
		if (arr.length == 0) {
			layer.msg("缺少打印数据参数!", { icon: 0, anim: 0 });
			return;
		}
		print.instance._initLicenses(function () {
			var printerCount = LODOP.GET_PRINTER_COUNT();
			if (printerCount <= 0) {
				layer.msg("找不到打印机!", { icon: 0, anim: 0 });
				return;
			} else {
				layer.msg('正在打印...', { offset: '15px', icon: 1, time: 2000 });
				$.each(arr, function (i, item) {
					ZFPrintModel.getLodopContentByModelFile(modelUrl, item, function (content) {
						ZFPrint.model.print(content, 'test' + i, function () {
							var isLastOne = i == arr.length - 1;
							callback && callback(item, isLastOne);//item:当前打印数据 isLastOne：是否是最后一个
						}, function () {
							if (printer) LODOP.SET_PRINTER_INDEX(printer);//指定打印机
						});
					});
				});
			}

		});
	};
	//修改初始化的打印机
	Class.prototype.printerChange = function (printer,callback) {
		var me = this,
			printer = printer || null;

		if (printer) me.config.printer = printer;

		callback && callback();
		
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this;
	};

	//核心入口
	Model.render = function (options) {
		var me = new Class(options);
		//初始化
		me.init();
		//监听事件
		me.initListeners();
		return me;
	};

	//暴露接口
	exports(MOD_NAME, Model);
});