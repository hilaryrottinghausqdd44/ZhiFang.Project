/**
 * @name：views/sample/cv/basic/send 危急值报告发送列表
 * @author：zhangda
 * @version 2021-09-06
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable: 'ux/table'
}).define(['uxutil','uxbase', 'uxtable', 'form'], function (exports) {
	"use strict";

	var $ = layui.$,
		form = layui.form,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'send';

	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table">',
		'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<div class="layui-form" style="padding:2px;margin-bottom:2px;border:1px solid #e6e6e6;">',
		'<div class="layui-form-item" style="margin-top:5px;text-align:center;font-weight:bold;">',
		'<div>选中：<span id="{tableId}-checkCount">0</span> 条危急值</div>',
		'</div>',
		'<div class="layui-form-item" style="margin-top:5px;">',
		'<label class="layui-form-label">说明:</label>',
		'<div class="layui-input-block">',
        '<input type="text" id="{tableId}-ReportInfo" name="ReportInfo" placeholder="" autocomplete="off" class="layui-input" />',
        '</div>',
		'</div>',
		'<div class="layui-form-item" style="text-align:center;">',
		'<div class="layui-btn-group">',
		'<button type="button" id="{tableId}-Send" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-ok"></i>发送</button>',
		'<button type="button" id="{tableId}-UnSend" class="layui-btn layui-btn-xs layui-btn-primary"><i class="layui-icon layui-icon-close"></i>不发送</button>',
		'</div>',
		'</div>',
		'</div>',
		'<style>',
		'.layui-table-select{background-color:#5FB878;color:#fff;}',
		'.{tableId}-table .layui-table-body .layui-table-cell{height:18px !important;}',
		/*'.{tableId}-table .layui-table-body .layui-table-cell .layui-form-checkbox{margin-top:30px !important;}',*/
		'</style>'
	];

	//获取数据服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormMsgByHQL?isPlanish=true";
	//阅读数据服务地址
	var READ_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_PanicValueRead";
	//发送数据服务地址
	var SEND_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SendPanicValueMsgToPlatform";
	//默认排序
	var defaultSort = [
		{ "property": "LisTestFormMsg_GTestDate", "direction": "desc" },
		{ "property": "LisTestFormMsg_LisTestForm_LBSection_Id", "direction": "desc" },
		{ "property": "LisTestFormMsg_LisTestForm_GSampleNoForOrder", "direction": "desc" }
	];
	//报告状态
	var ReportStatus = [
		{ value: 0, text: "未发送", color: "red" },
		{ value: 1, text: "已发送", color: "green" },
		{ value: 2, text: "不发送", color: "orange" }
	];
	//通知状态
	var PhoneStatus = [
		{ value: 0, text: "未通知", color: "red" },
		{ value: 1, text: "通知成功", color: "green" },
		{ value: 2, text: "通知失败", color: "orange" }
	];
	//记录服务发送个数
	var count = 0;
	var saveCount = 0;
	var saveErrorCount = 0;
	//医嘱单列表
	var send = {
		tableId: null,//列表ID
		tableToolbarId: null,//列表功能栏ID

		//对外参数
		config: {
			domId: null,
			height: null,
			//模式 1：查看界面 2：检验界面调用
			model: null,
			where: null
		},
		//内部列表参数
		tableConfig: {
			elem: null,
			toolbar: null,
			skin: 'line',//行边框风格
			//even:true,//开启隔行背景
			size: 'sm',//小尺寸的表格
			defaultToolbar: null,
			height: 'full-4',
			defaultLoad: true,
			page: true,
			limit: 100,
			limits: [100, 200, 500, 1000, 1500],
			url: GET_LIST_URL,
			data: [],
			where: {},
			cols: [[]],
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) {//res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
				if (data.list) {
					$.each(data.list, function (i, item) {
						if (item["LisTestFormMsg_ReportStatus"] == 0) item["LAY_CHECKED"] = true;//复选状态
					});
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, send.config, setings);
		me.tableConfig = $.extend({}, me.tableConfig, send.tableConfig);

		if (me.config.height) {
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		//me.tableToolbarId = me.config.domId + "-table-toolbar-top";
		me.tableConfig.elem = "#" + me.tableId;
		//me.tableConfig.toolbar = "#" + me.tableToolbarId;
		//配置列
		me.tableConfig.cols = me.initCols(me.config.model);
		//配置服务地址
		GET_LIST_URL += "&fields=" + me.getStoreFields(true).join(',');//字段
		me.tableConfig.url = GET_LIST_URL + (defaultSort.length > 0 ? "&sort=" + JSON.stringify(defaultSort) : "");
		if (me.config.where) {
			me.tableConfig.where["where"] = me.config.where;
		}
		//数据渲染完的回调
		me.tableConfig.done = function (res, curr, count) {
			if (count > 0) {
				//默认选中第一行
				me.onClickFirstRow();
				//选中危急值条数
				var checkStatus = me.uxtable.table.checkStatus(me.tableId);
				$("#" + me.tableId + "-checkCount").html(checkStatus.data.length);
				//写入阅读人
				var IDList = [];
				$.each(res.data, function (i, item) {
					IDList.push(item["LisTestFormMsg_Id"]);
				});
				me.readCVReport(IDList);
			}
		};

	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g, me.tableId).replace(/{tableToolbarId}/g, me.tableToolbarId);
		$('#' + me.config.domId).append(html);
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this;
		//发送按钮处理
		$("#" + me.tableId + "-Send").on('click', function () {
			var checkStatus = me.uxtable.table.checkStatus(me.tableId),
				IDList = [],
				msg = [];
			if (checkStatus.data.length == 0) {
				uxbase.MSG.onWarn("请至少选择一条数据进行处理!");
				return;
			}
			//验证
			$.each(checkStatus.data, function (i, item) {
				IDList.push(item["LisTestFormMsg_Id"]);
				if (item["LisTestFormMsg_ReportStatus"] != 0)
					msg.push("存在检验小组:" + item["LisTestFormMsg_LisTestForm_LBSection_CName"] + ",检验日期:" + uxutil.date.toString(item["LisTestFormMsg_GTestDate"], true) + ",样本号为:" + item["LisTestFormMsg_LisTestForm_GSampleNo"] + "的样本单,发送状态为:" + me.statusHandle(item["LisTestFormMsg_ReportStatus"], ReportStatus));
			});
			if (msg.length > 0) {
				msg.push("当前执行操作：发送危急值，是否继续执行?");
				layer.confirm(msg.join('<br>'), { icon: 3, title: '提示', area: '700px' }, function (index) {
					me.sendCVReport(IDList, 1);
					layer.close(index);
				});
			} else {
				me.sendCVReport(IDList, 1);
			}
			
		});
		//不发送按钮处理
		$("#" + me.tableId + "-UnSend").on('click', function () {
			var checkStatus = me.uxtable.table.checkStatus(me.tableId),
				IDList = [],
				width = '700px',
				msg = [];
			if (checkStatus.data.length == 0) {
				uxbase.MSG.onWarn("请至少选择一条数据进行处理!");
				return;
			}
			//验证
			$.each(checkStatus.data, function (i, item) {
				IDList.push(item["LisTestFormMsg_Id"]);
				if (item["LisTestFormMsg_ReportStatus"] != 0)
					msg.push("存在检验小组:" + item["LisTestFormMsg_LisTestForm_LBSection_CName"] + ",检验日期:" + uxutil.date.toString(item["LisTestFormMsg_GTestDate"], true) + ",样本号为:" + item["LisTestFormMsg_LisTestForm_GSampleNo"] + "的样本单,发送状态为:" + me.statusHandle(item["LisTestFormMsg_ReportStatus"], ReportStatus));
			});
			if (msg.length > 0) {
				msg.push("当前执行操作：不发送，是否继续执行?");
			} else {
				msg.push("您确认不发送该危急值吗？建议危急值立即发送!");
				width = '400px'
			}
			layer.confirm(msg.join('<br>'), { icon: 3, title: '提示', area: width }, function (index) {
				me.sendCVReport(IDList, 2);
				layer.close(index);
			});
		});
		//复选框监听处理
		me.uxtable.table.on('checkbox('+me.tableId+')', function (obj) {
			var checkStatus = me.uxtable.table.checkStatus(me.tableId); //idTest 即为基础参数 id 对应的值
			$("#" + me.tableId + "-checkCount").html(checkStatus.data.length);
		});
		//触发排序事件 
		me.uxtable.table.on('sort(' + me.tableId +')', function (obj) {
			console.log(obj.field); //当前排序的字段名
			console.log(obj.type); //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
			if (obj.type) {
				defaultSort = [{ "property": obj.field, "direction": obj.type }];
			} else {
				defaultSort = [];
			}
			me.onSearch(me.tableConfig.where.where);
		});
		//触发行单击事件
		//me.uxtable.table.on('row(' + me.tableId + ')', function (obj) {
		//	obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		//});
	};
	//查询处理
	Class.prototype.onSearch = function (where) {
		var me = this;
		me.tableConfig.where.where = where;
		me.uxtable.reload({
			url: GET_LIST_URL + (defaultSort.length > 0 ? "&sort=" + JSON.stringify(defaultSort) : ""),
			where: { where: where }
		});
	};
	//默认选中第一行
	Class.prototype.onClickFirstRow = function () {
		var me = this;
		setTimeout(function () {
			$("#" + me.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
		}, 0);

	};
	//初始化列
	Class.prototype.initCols = function (model) {
		var me = this,
			model = model || 1,
			cols = [];
		switch (String(model)) {
			case "1":
				cols = [
					{ type: 'checkbox', width: 26 },
					{ field: 'LisTestFormMsg_Id', width: 100, title: '主键ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LBSection_Id', width: 100, title: '小组ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LBSection_CName', width: 120, title: '检验小组', sort: true, hide: false },
					{ field: 'LisTestFormMsg_GTestDate', width: 100, title: '检验日期', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_GTestDate"] || ""; return uxutil.date.toString(value, true) || ''; } },
					{ field: 'LisTestFormMsg_LisTestForm_GSampleNoForOrder', width: 100, title: '样本号排序', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_GSampleNo', width: 100, title: '样本号', sort: true, hide: false },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_DeptID', width: 100, title: '科室ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_DeptName', width: 100, title: '科室', sort: true, hide: false },
					{ field: 'LisTestFormMsg_DetailDesc', width: 160, title: '详细信息', sort: true, hide: false },
					{ field: 'LisTestFormMsg_DataAddTime', width: 120, title: '报警时间', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_DataAddTime"] || ""; console.log(value); return uxutil.date.toString(value, false) || ''; } },
					{ field: 'LisTestFormMsg_ReportStatus', width: 80, title: '报告状态', sort: true, hide: false, templet: function (data) { return me.statusHandle(data["LisTestFormMsg_ReportStatus"], ReportStatus); } },
					{ field: 'LisTestFormMsg_ReportTime', width: 120, title: '报告时间', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_ReportTime"] || ""; return uxutil.date.toString(value, false) || ''; } },
					{ field: 'LisTestFormMsg_PhoneStatus', width: 80, title: '电话通知', sort: true, hide: false, templet: function (data) { return me.statusHandle(data["LisTestFormMsg_PhoneStatus"], PhoneStatus); } },
					{ field: 'LisTestFormMsg_PhoneTime', width: 120, title: '电话通知时间', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_PhoneTime"] || ""; return uxutil.date.toString(value, false) || ''; } },
					{ field: 'LisTestFormMsg_MasterDesc', minWidth: 100, title: '临床提示', sort: true, hide: false },
					{ field: 'LisTestFormMsg_ReaderID', width: 100, title: '阅读人ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_Reader', width: 100, title: '阅读人', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_CName', width: 100, title: '姓名', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_GenderName', width: 100, title: '性别', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_DoctorName', width: 100, title: '医生', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_DistrictName', width: 100, title: '病区', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_Bed', width: 100, title: '病床', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_AgeDesc', width: 100, title: '年龄描述', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LisPatient_DiagName', width: 100, title: '临床诊断', sort: true, hide: true }
				];
				break;
			case "2":
				cols = [
					{ type: 'checkbox', width: 26 },
					{ field: 'LisTestFormMsg_Id', width: 100, title: '主键ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LBSection_Id', width: 100, title: '小组ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_LBSection_CName', width: 120, title: '检验小组', sort: true, hide: true },
					{ field: 'LisTestFormMsg_GTestDate', width: 100, title: '检验日期', sort: true, hide: true, templet: function (data) { var value = data["LisTestFormMsg_GTestDate"] || ""; return uxutil.date.toString(value, true) || ''; } },
					{ field: 'LisTestFormMsg_LisTestForm_GSampleNoForOrder', width: 100, title: '样本号排序', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_GSampleNo', width: 100, title: '样本号', sort: true, hide: true },
					{ field: 'LisTestFormMsg_LisTestForm_CName', width: 100, title: '姓名', sort: true, hide: true },
					{ field: 'LisTestFormMsg_DetailDesc', width: 120, title: '详细信息', sort: true, hide: false },
					{ field: 'LisTestFormMsg_ReportStatus', width: 80, title: '报告状态', sort: true, hide: false, templet: function (data) { return me.statusHandle(data["LisTestFormMsg_ReportStatus"], ReportStatus); } },
					{ field: 'LisTestFormMsg_ReportTime', width: 120, title: '报告时间', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_ReportTime"] || ""; return uxutil.date.toString(value, false) || ''; } },
					{ field: 'LisTestFormMsg_DataAddTime', width: 120, title: '报警时间', sort: true, hide: false, templet: function (data) { var value = data["LisTestFormMsg_DataAddTime"] || ""; return uxutil.date.toString(value, false) || ''; } },
					{ field: 'LisTestFormMsg_MasterDesc', minWidth: 100, title: '临床提示', sort: true, hide: false },
					{ field: 'LisTestFormMsg_ReaderID', width: 100, title: '阅读人ID', sort: true, hide: true },
					{ field: 'LisTestFormMsg_Reader', width: 100, title: '阅读人', sort: true, hide: true },
				];
				break;
			default:
				break;
		}

		return [cols];
	};
	//获得列字段
	Class.prototype.getStoreFields = function (isString) {
		var me = this,
			columns = me.tableConfig["cols"][0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//状态处理
	Class.prototype.statusHandle = function (value,list) {
		var me = this,
			list = list || [],
			value = value || null,
			str = "";

		$.each(list, function (i, item) {
			if (item["value"] == value) {
				str = "<span style='color:" + item["color"] + "'>" + item["text"] + "</span>";
				return false;
			}
		});
		return str;
	};
	//发送危急值
	Class.prototype.sendCVReport = function (testFormMsgIDList, ReportStatus) {
		var me = this,
			testFormMsgIDList = testFormMsgIDList || [],
			url = SEND_LIST_URL,
			msgSendMemo = $("#" + me.tableId + "-ReportInfo").val(),
			ReportStatus = ReportStatus,
			entity = {
				testFormMsgIDList: testFormMsgIDList.join(),
				msgSendFlag: ReportStatus,
				msgSendMemo: msgSendMemo
			};

		me.update(url, entity, function (res) {
			if (res.success) {
				$.each(testFormMsgIDList, function (i,item) {
					me.uxtable.updateRowItem({
						LisTestFormMsg_Id: item,
						LisTestFormMsg_ReportStatus: ReportStatus,
						LisTestFormMsg_ReportInfo: msgSendMemo,
					}, "LisTestFormMsg_Id");
				});
				uxbase.MSG.onSuccess("执行成功!");
				layui.event('send', 'save', { tableCache: me.uxtable.table.cache[me.tableId] });
			} else {
				uxbase.MSG.onError(res.ErrorInfo || res.msg);
			}
		});
	};
	//阅读危急值
	Class.prototype.readCVReport = function (testFormMsgIDList) {
		var me = this,
			testFormMsgIDList = testFormMsgIDList || [],
			url = READ_LIST_URL,
			entity = { testFormMsgIDList: testFormMsgIDList.join() };

		me.update(url, entity, function (res) {
			if (res.success) {
				//不用更新
			} else {
				uxbase.MSG.onError(res.ErrorInfo || res.msg);
			}
		});
	};
	//更新服务
	Class.prototype.update = function (url, entity, callback) {
		var me = this,
			params = JSON.stringify(entity),
			load = layer.load(),//显示遮罩层
			config = {
				type: "POST",
				url: url,
				data: params
			};
		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			callback && callback(res);
		})
	};

	//核心入口
	send.render = function (options) {
		var me = new Class(options);

		if (!me.config.domId) {
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化列表
		me.uxtable = uxtable.render(me.tableConfig);
		//监听事件
		me.initListeners();

		return me;
	};

	//暴露接口
	exports(MOD_NAME, send);
});