/**
 * 图形
 * @author liangyl	
 * @version 2021-05-27
 */
layui.extend({
}).define(['uxutil','uxbase', 'form'], function (exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form;


	/**获取样本单项目数据服务路径*/
	var GET_IMAGE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemMergeImageData';
	var UPLOAD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestItemMergeGraph';
	//当前图片数据
	var DATA = [];
	var line_ind = null;
	var echartline = {
		//参数配置
		config: {
			ITEMLIST: []//合并项目数据
		}
	};

	var Class = function (setings) {
		var me = this;
		me.config = $.extend({
		}, me.config, echartline.config, setings);
	};

	Class.pt = Class.prototype;
	//图形生成参数
	Class.pt.getParams = function (items) {
		//获取合并标识为是的行数据toFormID
		var toFormID = "", listLisTestItem = [];
		for (var i = 0; i < items.length; i++) {
			if (items[i].LBMergeItemVO_IsMerge == true) {
				toFormID = items[i].LBMergeItemVO_LisTestItem_LisTestForm_Id;
			}
			var itemObj = {
				Id: items[i].LBMergeItemVO_LisTestItem_Id,
				EquipID: items[i].LBMergeItemVO_LisTestItem_EquipID ? items[i].data.LBMergeItemVO_LisTestItem_EquipID : null,
				LBItem: { Id: items[i].LBMergeItemVO_ChangeItemID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
				ReportValue: items[i].LBMergeItemVO_LisTestItem_ReportValue
			};
			listLisTestItem.push(itemObj);
		}
		var params = {
			listLisTestItem: listLisTestItem,
			toFormID: toFormID
		};
		return params;
	};


	//数据加载
	Class.pt.loadData = function (items) {
		var me = this;
		var config = {
			type: 'post',
			url: GET_IMAGE_LIST_URL,
			data: JSON.stringify(me.getParams(items))
		};
		uxutil.server.ajax(config, function (data) {
			if (data.success) {
				var list = data.value || [];
				DATA = list;
				me.changeData(list, items);
			} else {
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//获取图形信息
	Class.pt.changeData = function (list, items) {
		var me = this;
		var xAxisList = [];
		var PointValueList = [], PointRangeLList = [], PointRangeHList = [], PointYCRangeLList = [], listPointYCRangeHList = [];
		var items = me.resultData(list, items);
		xAxisList = items.xAxisList;
		PointValueList = items.PointValueList;
		PointRangeLList = items.PointRangeLList;
		PointRangeHList = items.PointRangeHList;
		PointYCRangeLList = items.PointYCRangeLList;
		listPointYCRangeHList = items.listPointYCRangeHList;
		var config = {
			tooltip: { trigger: 'axis' },
			//		    animation : false,
			color: ['#0000FF', '#32CD32', '#32CD32', '#FF0000', '#FF0000'],//关键加上这句话，legend的颜色和折线的自定义颜色就一致了
			grid: {
				top: '3%',
				left: '5%',
				right: '6%',
				bottom: '0%',
				containLabel: true
			},
			xAxis: { type: 'category', boundaryGap: false, data: xAxisList },
			yAxis: { type: 'value' },
			series: [
				{
					name: '项目参考值', type: 'line', data: PointValueList,
					showSymbol: true, symbol: 'circle', symbolSize: 10,   //设定实心点的大小
					itemStyle: { normal: { label: { show: $('#cbTip').prop('checked') } } }
				}, {
					name: '项目参考范围低值', type: 'line', data: PointRangeLList,
					smooth: false,//关键点，为true是不支持虚线的，实线就用true
					itemStyle: {
						normal: {
							lineStyle: {
								width: 2, type: 'dotted'  //'dotted'虚线 'solid'实线
							}
						}
					}
				}, {
					name: '项目参考范围高值', type: 'line',
					data: PointRangeHList, smooth: false,
					itemStyle: {
						normal: {
							lineStyle: {
								width: 2,
								type: 'dotted'  //'dotted'虚线 'solid'实线
							}
						}
					}
				}, {
					name: '项目参考范围异常低值', type: 'line', data: PointYCRangeLList, smooth: false,
					itemStyle: {
						normal: {
							lineStyle: {
								width: 2,
								type: 'dotted'  //'dotted'虚线 'solid'实线
							}
						}
					}
				}, {
					name: '项目参考范围异常高值', type: 'line', data: listPointYCRangeHList, smooth: false,
					itemStyle: {
						normal: {
							lineStyle: {
								width: 2,
								type: 'dotted'  //'dotted'虚线 'solid'实线
							}
						}
					}
				}]
		};
		var myChart = echarts.init(document.getElementById("line"));
		myChart.clear();//清除之前的图表
		myChart.setOption(config); // 使用刚指定的配置项和数据显示图表。
	};
	//数据返回解析
	Class.pt.resultData = function (list, items) {
		var me = this;
		var xAxisList = [];
		var PointValueList = [], PointRangeLList = [], PointRangeHList = [], PointYCRangeLList = [], listPointYCRangeHList = [];

		for (var i = 0; i < list.length; i++) {
			var LineName = list[i].LineName;
			switch (LineName) {
				case "PointValue"://项目参考值
					var LinePoint = list[i].LinePoint;
					for (var j = 0; j < LinePoint.length; j++) {
						for (var n = 0; n < items.length; n++) {
							if (LinePoint[j].X == items[n].LBMergeItemVO_ChangeItemID) {
								xAxisList.push(items[n].LBMergeItemVO_ChangeItemName);
								break;
							}
						}
						PointValueList.push(LinePoint[j].Y);
					}
					break;
				case "PointRangeL"://项目参考值低值
					if ($('#cbRefRange').prop('checked')) {
						var PointRangeL = list[i].LinePoint;
						for (var j = 0; j < PointRangeL.length; j++) {
							PointRangeLList.push(PointRangeL[j].Y);
						}
					}
					break;
				case "PointRangeH"://项目参考值高值
					if ($('#cbRefRange').prop('checked')) {
						var PointRangeH = list[i].LinePoint;
						for (var j = 0; j < PointRangeH.length; j++) {
							PointRangeHList.push(PointRangeH[j].Y);
						}
					}
					break;
				case "PointYCRangeL"://项目参考值异常低值
					if ($('#cbAbnormalRange').prop('checked')) {
						var PointYCRangeL = list[i].LinePoint;
						for (var j = 0; j < PointYCRangeL.length; j++) {
							PointYCRangeLList.push(PointYCRangeL[j].Y);
						}
					}
					break;
				case "listPointYCRangeH"://项目参考值异常高值
					if ($('#cbAbnormalRange').prop('checked')) {
						var listPointYCRangeH = list[i].LinePoint;
						for (var j = 0; j < listPointYCRangeH.length; j++) {
							listPointYCRangeHList.push(listPointYCRangeH[j].Y);
						}
					}
					break;
				default:
					break;
			}
		}
		return {
			xAxisList: xAxisList,
			PointValueList: PointValueList,
			PointRangeLList: PointRangeLList,
			PointRangeHList: PointRangeHList,
			PointYCRangeLList: PointYCRangeLList,
			listPointYCRangeHList: listPointYCRangeHList
		}
	};
	//获取图形信息
	Class.pt.ImageInfo = function (items, callback) {
		var me = this;


	};
	/**保存图形*/
	Class.pt.onUploadImg = function () {
		var me = this;
		var testFormID = "";
		for (var i = 0; i < line_ind.config.ITEMLIST.length; i++) {
			if (line_ind.config.ITEMLIST[i].LBMergeItemVO_IsMerge == true) {
				testFormID = line_ind.config.ITEMLIST[i].LBMergeItemVO_LisTestItem_LisTestForm_Id;
				break;
			}
		}
		var graphName = $("#ItemID option:checked").text();
		//图片名称 当有简称取简称 所以不从 me.itemList取值
		if ($("#ItemID option:selected").attr("data-SName")) graphName = $("#ItemID option:selected").attr("data-SName");

		var formData = new FormData();
		var index = layer.load();
		var myChart = echarts.getInstanceByDom(document.getElementById('line'));
		var imgbase64 = myChart.getDataURL({
			pixelRatio: 2, // double pixel
			backgroundColor: '#fff'
		});
		//formData值
		formData.append('graphThumb', imgbase64);
		formData.append('graphBase64', imgbase64);//原图片base64
		formData.append('testFormID', testFormID);//图形名称
		formData.append('graphInfo', DATA);//图形数据说明
		formData.append('graphName', graphName)

		//表单提交
		$.ajax({
			url: UPLOAD_URL,
			type: 'post',
			data: formData,
			cache: false,
			dataType: 'json',
			//contentType: "text",
			processData: false,         // 告诉jQuery不要去处理发送的数据
			contentType: false,        // 告诉jQuery不要去设置Content-Type请求头
			success: function (res) {
				layer.close(index);
				if (res.success) {
					uxbase.MSG.onSuccess("保存成功!");
				} else {
					uxbase.MSG.onError(res.ErrorInfo);
				}
			},
			error: function (e) {
				layer.close(loadIndex);
			}
		});
	};
	Class.pt.initListeners = function (items, callback) {
		var me = this;

		var maxHeight = $('#itemmerge').height() / 2 - 10;
		$('#line').css("height", maxHeight + 'px');
		$('#line').css("width", '100%');

		form.on('checkbox(cbRefRange)', function (data) {
			if (line_ind.config.ITEMLIST.length == 0) return;
			line_ind.loadData(line_ind.config.ITEMLIST);
		});
		form.on('checkbox(cbAbnormalRange)', function (data) {
			if (line_ind.config.ITEMLIST.length == 0) return;
			line_ind.loadData(line_ind.config.ITEMLIST);
		});
		form.on('checkbox(cbTip)', function (data) {
			if (line_ind.config.ITEMLIST.length == 0) return;
			line_ind.loadData(line_ind.config.ITEMLIST);
		});
	};
	Class.pt.clearData = function () {
		var me = this;
		line_ind.config.ITEMLIST = [];
		var myChart = echarts.init(document.getElementById("line"));
		myChart.clear();//清除之前的图表
		//		document.getElementById("line").innerHTML ='<div class="layui-none" style="color:#999; text-align: center;padding-top:20px;">暂无相关数据</div>';
	};

	//主入口
	echartline.render = function (options) {
		var me = this;

		line_ind = new Class(options);
		line_ind.getParams = Class.pt.getParams;
		line_ind.clearData = Class.pt.clearData;
		line_ind.loadData = Class.pt.loadData;
		line_ind.onUploadImg = Class.pt.onUploadImg;
		Class.pt.initListeners();
		Class.pt.clearData();

		return line_ind;
	};

	//暴露接口
	exports('echartline', echartline);
});