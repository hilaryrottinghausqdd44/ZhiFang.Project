layui.extend({
	uxutil:'ux/util',
}).use(['uxutil','table'], function () {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table;
	
	//HIS医嘱信息服务地址(long nodetype, string barcode)
	var HIS_ORDER_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_HISOrderInfo";
	//参数
	var PARAMS = uxutil.params.get(true);
	//站点类型
	var nodetype = PARAMS.NODETYPE;
	//样本条码
	var BARCODE = PARAMS.BARCODE;
	var loadIndex;
	//初始化数据加验证
	function initHtml() {
		if (!nodetype) {
			if (!$("#table").hasClass("layui-hide")) {
				$("#table").addClass("layui-hide");
			}
			$("#content").html('<div style="padding:50px;text-align:center;font-size:24px;color:red;">请传递站点类型参数！</div>');
			return;
		}else if (!BARCODE) {
			if (!$("#table").hasClass("layui-hide")) {
				$("#table").addClass("layui-hide");
			}
			$("#content").html('<div style="padding:50px;text-align:center;font-size:24px;color:red;">请传递样本条码参数！</div>');
			return;
		} else {
			if($("#table").hasClass("layui-hide")) {
				$("#table").removeClass("layui-hide");
			}
			loadIndex = layer.load();//开启加载层
			uxutil.server.ajax({
				url: HIS_ORDER_INFO_URL,
				type: 'post',
				data: JSON.stringify({
					nodetype: nodetype,
					barcode: BARCODE
				})
			}, function (data) {
					if (data.success) {
						if (data.value) {
							initTable(data.value[0]);
						} else {
							$("#content").html('<div style="padding:50px;text-align:center;font-size:24px;color:red;"> 未查询到原始医嘱信息！ </div>');
						}
					} else {
						$("#content").html('<div style="padding:50px;text-align:center;font-size:24px;color:red;">' + data.msg + '</div>');
					}
			}, true);
		}
	};
	//初始化表格
	function initTable(data) {
		console.log(data);	
		table.render({
			elem: '#OrderFormList',
			height: 'full-50',
			defaultToolbar: ['filter'],
			size: 'sm',
			page: false,
			data: data,
			url: "",
			cols: [[
				{ type: 'numbers', title: '行号' },
				{ field: 'BarCode', width: 150, title: '条码号', sort: false },
				{ field: 'OrderFormNo', width: 200, title: '医嘱单号', sort: false },
				{ field: 'OrderTime', width: 100, title: '开单时间', sort: false },
				{ field: 'OrderExecTime', width: 80, title: '医嘱执行时间', sort: false },
				{ field: 'CName', width: 80, title: '姓名', sort: false },
				{ field: 'GenderName', width: 80, title: '性别', sort: false },
				{ field: 'Age', width: 80, title: '年龄', sort: false },
				{ field: 'PatNo', width: 80, title: '病历号', sort: false },
				{ field: 'DistrictName', width: 80, title: '病区', sort: false },
				{ field: 'WardName', width: 80, title: '病房', sort: false },
				{ field: 'Bed', width: 80, title: '床号', sort: false },
				{ field: 'BarCodesItemName', width: 120, title: '采样项目名称', sort: false },
				{ field: 'OrdersItemName', width: 120, title: '医嘱项目名称', sort: false }
			]],
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				console.log(data);
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				layer.close(loadIndex);//关闭加载层
			}
		});
	};
	initHtml();
});