/**
 * @name：modules/pre/sample/sign/index 样本签收
 * @author：zhangda
 * @version 2020-08-27
 */
layui.extend({
	uxutil: 'ux/util',
	list: 'modules/pre/sample/delivery/list',//条码号列表
}).define(['uxutil', 'table', 'form', 'dropdown', 'list','PreSampleDeliveryParams'], function (exports) {
	"use strict";

	var $ = layui.$,
		form = layui.form,
		dropdown = layui.dropdown,
		uxutil = layui.uxutil,
		table = layui.table,
		list = layui.list,
		PreSampleDeliveryParams = layui.PreSampleDeliveryParams,
		MOD_NAME = 'PreSampleDeliveryIndex';

	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" lay-filter="{domId}-form">',
		'<div class="layui-form-item" style="margin-bottom:0;">',
		'<div class="layui-input-inline">',
		'<input type="text" name="{domId}-barcode" id="{domId}-barcode" placeholder="条码号" autocomplete="off" class="layui-input">',
		'</div>',
		'<div class="layui-input-inline" style="display:none;">',
		'<input type="text" name="{domId}-deliverer" id="{domId}-deliverer" lay-filter="{domId}-deliverer" data-value="" title="条码号处扫描护工号" placeholder="运送人" readonly autocomplete="off" class="layui-input">',
		'</div>',
		'</div>',
		'</div>',
		'<div class="layui-row layui-col-space10 layui-form" lay-filter="{domId}-form-cards" style="background-color:#f2f2f2;padding:0;margin:0;">',
		'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12">',
		'<div id="{domId}-table"></div>',
		'</div>',
		'</div>',
		'<style>',
		'.{domId}-grid-div{padding:2px;margin-bottom:2px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
		//			'.layui-form-onswitch-red{border-color:#FF5722;background-color:#FF5722;}',
		'</style>'
	].join('');

	//送达查询数据服务路径
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampledeliveryGetBarCodeForm';
	//送达更新状态服务路径
	var UPDATE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive';
	//获得运送人（护工）
	var GET_EMP_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampledeliveryGetEmpInfo';
	//所有列表数据
	var LIST_DATA = null;

	////站点类型实例
	//var PreBasicHostTypeInstance = PreBasicHostType.render();
	//功能参数实例
	var PreSampleDeliveryParamsInstance = null;

	//签收列表实例
	var DeliveryListInstance = null;
	//签收列表选中列
	var DeliveryListCheckRowData = [];

	/**
	 * 1.批量确认：需要先扫描护工号获得到护工信息 才可扫描条码号 护工号->条码号(N个)->护工号(扫描完提交)【前后护工号可不同，记录第一个护工号，最后一个只验证是否是护工，同一个护工则清空运送人，不是同一个保留运送人，列表都清空】
	 * 2.单个确认并记录护工：与批量确认类似 护工号->条码号(N个)->护工号(扫描完提交) 成功则加到列表 失败在下面提示 重新扫护工号 则清空列表
	 * 3.单个确认：条码号(1个)【扫描完直接提交】
	 *
	 * 条码号输入框 可扫描条码号和护工号 条码号->查询数据（根据模式判断是否提交） 护工号->查询护工信息并选择性的写入到运送人输入框
	 * **/
	//现有模式
	var MODELS = [1, 2, 3];
	//门诊样本条码
	var PreSampleDeliveryIndex = {
		//对外参数
		config: {
			domId: null,
			modelType: null,
			nodetype: null
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, PreSampleDeliveryIndex.config, setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g, me.config.domId);

		$('#' + me.config.domId).append(html);
		//模式处理
		me.modelHandle();
		//实例化签收列表
		DeliveryListInstance = list.render({
			domId: me.config.domId +"-table",
			height: 'full-140',
			modeltype: me.config.modeltype,
			colsStr: PreSampleDeliveryParamsInstance.get('Pre_OrderExchangeDelivery_DefaultPara_0009')
		});

		form.render();
	};
	//传入模式处理 --不存在这个模式则默认为模式1
	Class.prototype.modelHandle = function () {
		var me = this,
			isExist = false,
			modelType = me.config.modelType,
			arr = MODELS;
		//查询是否支持该模式
		$.each(arr, function (i, item) {
			if (item == modelType) {
				isExist = true;
				return false;
			}
		});
		if (!isExist) me.config.modelType = 1;//默认为模式1
		//模式3不显示运送人
		if (me.config.modelType == 3) {
			$("#" + me.config.domId + "-deliverer").parent().hide();
		} else {
			$("#" + me.config.domId + "-deliverer").parent().show();
		}
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this,
			modelType = me.config.modelType;
		//监听回车按下事件
		$("#"+ me.config.domId).keydown(function (event) {
			var inputID = me.config.domId + "-barcode";
			switch (event.keyCode) {
				case 13:
					//判断焦点是否在该输入框
					if (document.activeElement == document.getElementById(inputID)) {
						var value = $("#" + inputID).val();
						me.onSearch(value);
					}
			}
		});
		//列表单击
		DeliveryListInstance.uxtable.table.on('row(' + DeliveryListInstance.tableId + ')', function (obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			DeliveryListCheckRowData = [];
			DeliveryListCheckRowData.push(obj.data);
			$("#" + DeliveryListInstance.tableId + "-div").html(obj.data["LisBarCodeFormVo_LisBarCodeForm_LisOrderForm_ParItemCName"] || "");

		});
	};
	//条码号获得焦点
	Class.prototype.setFocus = function () {
		var me = this;
		setTimeout(function () {
			$("#" + me.config.domId + "-barcode").focus();
		}, 10);
	};
	//清空按钮-清空列表
	Class.prototype.onClearClick = function () {
		var me = this;
		DeliveryListInstance.onSearch([]);
		me.setFocus();
	};
	//清空条码号并获得焦点
	Class.prototype.onClearBarCode = function () {
		var me = this;
		$("#" + me.config.domId + "-barcode").val("");
		me.setFocus();
	};
	//查询处理
	Class.prototype.onSearch = function (value) {
		var me = this,
			value = value || null;

		if (!value) return;
		
		//根据模式分别做查询处理
		me.onSearchHandleByModel(value);

	};
	//是否是条码号
	Class.prototype.isBarcode = function (value) {
		var me = this,
			value = String(value).trim() || null,
			isHG = PreSampleDeliveryParamsInstance.get('Pre_OrderExchangeDelivery_DefaultPara_0006'),//使用非HG护工号
			BarCodeMinLength = PreSampleDeliveryParamsInstance.get('Pre_OrderExchangeDelivery_DefaultPara_0007');//条码号最小位数
		//存在HG 则为护工号
		if (value.toUpperCase().indexOf('HG') != -1) return false;
		//使用非HG护工号 则需和条码号最小位数比较 小于则为护工号 否则为条码号
		if (isHG && BarCodeMinLength) {
			if (value.length < Number(BarCodeMinLength))
				return false;
		}

		return true;
	};
	//根据模式分别做查询处理
	Class.prototype.onSearchHandleByModel = function (value) {
		var me = this,
			modelType = me.config.modelType,
			value = value || null,
			deliverer = $("#" + me.config.domId + "-deliverer").attr("data-value"),//运送人
			tableCache = DeliveryListInstance.uxtable.table.cache[DeliveryListInstance.tableId],
			isBarcode = me.isBarcode(value);//是否为条码号

		switch (String(modelType)) {
			case "1":
				if (isBarcode) {//条码号
					//是否已扫描过护工
					if (!deliverer) {
						layer.msg("请先扫描护工号!", { icon: 0, anim: 0 });
						return;
					} else {
						//该条码号是否已存在
						var isExist = me.onBarCodeIsExist(value);
						if (isExist) {
							layer.msg("该条码已存在!", { icon: 0, anim: 0 });
							return;
						}
						//调用条码服务 加入到列表
						me.getInfoByBarCode(value,false, function (data) {
							//添加到列表
							tableCache.push.apply(tableCache, data);
							DeliveryListInstance.onSearch(tableCache);
							//清空条码号
							me.onClearBarCode();
						});
					}
				} else {//护工号
					//根据护工号获得护工信息
					me.getHGByCode(value, function () {
						//清空条码号
						me.onClearBarCode();
						//是否存在未更新数据
						if (deliverer && tableCache.length > 0) {
							var BarCodeList = [];
							$.each(tableCache, function (i,item) {
								if (item["LisBarCodeFormVo_LisBarCodeForm_BarCode"])
									BarCodeList.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
							});
							//更新并成功后清空列表
							me.updateBarCodeForm(BarCodeList, function (res) {
								if (res.success) {
									//成功清空列表
									DeliveryListInstance.onSearch([]);
									layer.msg("样本送达成功!", { icon: 0, anim:0 });
								}
							});
						}
					});
				}
				break;
			case "2":
				if (isBarcode) {//条码号
					//是否已扫描过护工
					if (!deliverer) {
						layer.msg("请先扫描护工号!", { icon: 0, anim: 0 });
						return;
					} else {
						//该条码号是否已存在
						var isExist = me.onBarCodeIsExist(value);
						if (isExist) {
							layer.msg("该条码已存在!", { icon: 0, anim: 0 });
							return;
						}
						//调用更新条码服务 成功则加入到列表 失败显示失败信息
						me.getInfoByBarCode(value, true, function (data) {
							//添加到列表
							tableCache.push.apply(tableCache, data);
							DeliveryListInstance.onSearch(tableCache);
							//清空条码号
							me.onClearBarCode();
						});
					}
				} else {//护工号
					//根据护工号获得护工信息
					me.getHGByCode(value, function () {
						//清空列表
						DeliveryListInstance.onSearch([]);
						//清空条码号
						me.onClearBarCode();
					});
				}
				break;
			case "3":
				//该条码号是否已存在
				var isExist = me.onBarCodeIsExist(value);
				if (isExist) {
					layer.msg("该条码已存在!", { icon: 0, anim: 0 });
					return;
				}
				//调用更新条码服务 成功则加入到列表 失败显示失败信息
				me.getInfoByBarCode(value, false, function (data) {
					//添加到列表
					tableCache.push.apply(tableCache, data);
					DeliveryListInstance.onSearch(tableCache);
					//清空条码号
					me.onClearBarCode();
				});
				break;
			default:
				break;
		}
	};
	//根据护工号获得护工具体信息
	Class.prototype.getHGByCode = function (value,callback) {
		var me = this,
			value = value,
			load = layer.load(),
			config = {
				type: "POST",
				url: GET_EMP_URL,
				data: JSON.stringify({ cardno: value })
			};

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.length > 0) {
					callback && callback();
					me.setDeliveryer(res.value);
				} else {
					layer.msg("未找到该员工信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取护工信息失败!", { icon: 5, anim:0 });
			}
		})
	}
	//设置运送人
	Class.prototype.setDeliveryer = function (list) {
		var me = this,
			value = $("#" + me.config.domId + "-deliverer").attr("data-value"),
			list = list || [];
		if (list.length == 0) return;
		$("#" + me.config.domId + "-barcode").val("");//清空条码号框
		$("#" + me.config.domId + "-deliverer").attr("data-value", list[0]["Id"]);
		$("#" + me.config.domId + "-deliverer").val(list[0]["CName"] + "+" + list[0]["StandCode"]);
		//同一个护工号则清空
		if (list[0]["Id"] == value) me.clearDeliveryer();
	};
	//清空运送人
	Class.prototype.clearDeliveryer = function () {
		var me = this;
		$("#" + me.config.domId + "-deliverer").attr("data-value", "");
		$("#" + me.config.domId + "-deliverer").val("");
	};
	//根据条码号获取条码信息
	Class.prototype.getInfoByBarCode = function (value, isUpdate, callback) {
		var me = this,
			value = value,
			isUpdate = isUpdate || false,//是否送达更新状态
			nodetype = me.config.nodetype,//站点类型
			userid = $("#" + me.config.domId + "-deliverer").attr("data-value"),
			username = $("#" + me.config.domId + "-deliverer").val().indexOf('+') != -1 ? $("#" + me.config.domId + "-deliverer").val().split("+")[0] : "",
			fields = DeliveryListInstance.getStoreFields(true).join(),
			load = layer.load(),
			config = {
				type: "POST",
				url: GET_LIST_URL,
				data: JSON.stringify({ nodetype: nodetype, barcodes: value, isUpdate: isUpdate, userid: userid, username: username, fields: fields, isPlanish: true })
			};

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list.length > 0) {
					me.onFailureInfoHandle(res.value.list, function (errorList) {
						if (errorList.length > 0) layer.alert(errorList.join('<br>'), { icon: 0, anim: 0 });
						callback && callback(res.value.list);
					});
				} else {
					layer.msg("未找到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})
	}
	//更新条码信息状态
	Class.prototype.updateBarCodeForm = function (BarCodeList, callback) {
		var me = this,
			BarCodeList = BarCodeList || [],
			nodetype = me.config.nodetype,//站点类型
			userid = $("#" + me.config.domId + "-deliverer").attr("data-value"),
			username = $("#" + me.config.domId + "-deliverer").val().indexOf('+') != -1 ? $("#" + me.config.domId + "-deliverer").val().split("+")[0] : "",
			load = layer.load(),
			config = {
				type: "POST",
				url: UPDATE_LIST_URL,
				data: JSON.stringify({ nodetype: nodetype, barcodes: BarCodeList.join(), userid: userid, username: username })
			};

		if (BarCodeList.length == 0) return;

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				callback && callback(res);
			} else {
				layer.msg(res.ErrorInfo || "更新条码信息失败!", { icon: 5, anim: 0 });
			}
		})
	}
	//是否该条码号已经存在列表
	Class.prototype.onBarCodeIsExist = function (value) {
		var me = this,
			tableCache = DeliveryListInstance.uxtable.table.cache[DeliveryListInstance.tableId],
			isExist = false;
		value = value || null;

		$.each(tableCache, function (i, item) {
			if (value == item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
				isExist = true;
				return false;
			}
		});
		return isExist;
	};
	//参数配置的提示方式处理
	Class.prototype.onFailureInfoHandle = function (list, callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,
			errorList = [];
		$.each(list, function (i, item) {
			var failureInfoArr = item["LisBarCodeFormVo_failureInfo"] ? JSON.parse(item["LisBarCodeFormVo_failureInfo"]) : [];
			$.each(failureInfoArr, function (j, itemJ) {
				if (isOut) return false;
				var alterMode = itemJ["alterMode"],
					failureInfo = itemJ["failureInfo"];

				switch (String(alterMode)) {
					case "4"://用户自行选择
						isOut = true;
						layer.confirm(failureInfo+"?", { icon: 3, title: '提示' }, function (index) {
							isOut = false;
							failureInfoArr[j]["alterMode"] = 0;
							list[i]["LisBarCodeFormVo_failureInfo"] = JSON.stringify(failureInfoArr);
							me.onFailureInfoHandle(list, callback);
							layer.close(index);
						});
						break;
					case "3"://允许且提示
						errorList.push(failureInfo);
						break;
					case "2"://不允许不提示
						isOut = true;
						break;
					case "1"://不允许且提示
						layer.msg(failureInfo, { icon: 0, anim: 0 });
						isOut = true;
						break;
					default:
						break;
				}

			});
			if (isOut) return false;
		});
		if (!isOut) callback && callback(errorList);
		
	};


	//核心入口
	PreSampleDeliveryIndex.render = function (options) {
		var me = new Class(options);

		if (!me.config.domId) {
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		PreSampleDeliveryParamsInstance = PreSampleDeliveryParams.render({ nodetype: me.config.nodetype });
		//初始化功能参数
		PreSampleDeliveryParamsInstance.init(function () {
			//初始化HTML
			me.initHtml();
			//监听事件
			me.initListeners();
		});
		
		return me;
	};

	//暴露接口
	exports(MOD_NAME, PreSampleDeliveryIndex);
});