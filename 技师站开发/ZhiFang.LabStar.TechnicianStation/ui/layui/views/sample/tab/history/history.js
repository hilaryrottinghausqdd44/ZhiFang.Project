/**
 * 历史对比
 * @author GHX
 * @version 2021-05-06
 */
layui.extend({
}).define(['uxutil', 'table', 'form', 'laydate','uxbase'], function (exports) {
	"use strict";
	var $ = layui.jquery,
		table = layui.table,
		form = layui.form,
		laydate = layui.laydate,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
	var app = {};
	app.url = {
		LabStartUrl: uxutil.path.REPORTFORMQUERYPRINTROOTPATH + '/ui_new/layui/class/labstar/history/history.html',
		sampleUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
		//查询历史对比小组url
		queryLBSectionHisCompUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompByHQL?isPlanish=true',
		queryLBSectionurl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
		//获取常规检验参数分类列表.  --个性设置->默认设置->出厂设置
		getParamsListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaTypeCode?isPlanish=true',
	};
	app.config = {
		isLoad: false//是否加载过
	};
	app.params = {
		recode: null,
		isReadOnly: true,
		sectionId: null,//当前页签小组
		//系统参数
		system: {
			DateRange: 90,//历史对比日期范围
			IsSampleType: false,//历史对比区分样本类型
			SearchFields: 'PatNo'//历史对比查询字段列表  多字段用“|”分割
		}
	};
	//初始化
	app.init = function (testFormRecord, isReadOnly, sectionId) {
		var me = this,
			time = 0;
		//首次加载
		if (!me.config.isLoad) {
			me.SampleInit();//初始化样本类型
			time = 1000;
			me.config.isLoad = true;
			//监听
			me.listeners();
		}
		//加载小组
		if (!me.config.isLoad || !testFormRecord || !me.params.recode || testFormRecord["LisTestForm_LBSection_Id"] != me.params.recode["LisTestForm_LBSection_Id"]) {
			var SectionID = testFormRecord ? testFormRecord["LisTestForm_LBSection_Id"] : (sectionId ? sectionId : null);
			me.getHistoryParams(SectionID);//获得历史对比参数
			me.createSectionCheckBox(SectionID);//创建小组复选框
		}
			
		//设置参数
		me.params.recode = testFormRecord || null;
		me.params.isReadOnly = isReadOnly;
		me.params.sectionId = sectionId;
		//赋值样本类型复选框
		$("#HistorySampleType").prop("checked", me.params.system.IsSampleType);
		//赋值样本类型
		setTimeout(function () { $("#resultContrastSample").val(testFormRecord ? testFormRecord["LisTestForm_GSampleTypeID"] : ""); form.render(); }, time);
		//初始化日期
		me.initDateListeners();
		//设置高度
		var height = ($(window).height() - 120) + "px";
		var height2 = ($(window).height() - 314) + "px";
		$("#normaliframe").css("height",height);
		$("#historySectionForm").css("height", height2)
		form.render();
		$("#confirmHistoryc").click();
	};
	app.listeners = function () {
		var me = this;
		//查询监听
		$("#confirmHistoryc").on("click", function () {
			if (!me.params.recode) {
				uxbase.MSG.onWarn("请选择一条检验单进行查询!");
				return;
			};
			if (!me.params.system.SearchFields) {
				uxbase.MSG.onWarn("系统参数中历史对比查询字段列表不能设置为空!");
				return;
			};
			var arr = me.params.system.SearchFields.split("|"),
				record = me.params.recode,
				fieldsList = [],//查询字段集合 逗号分割
				valueList = [],//查询字段值集合 逗号分割
				where = "1=1",
				sectionid = record.LisTestForm_LBSection_Id,
				testFormId = record.LisTestForm_Id,
				HistorySampleType = $("#HistorySampleType").prop("checked"),
				HistoryAtPresent = $("#HistoryAtPresent").prop("checked"),
				startdate = $("#HistoryStartDate").val(),
				enddate = $("#HistoryEndDate").val(),
				checkedBox = $("#historySectionForm").find("input[type=checkbox]:checked");

			$.each(arr, function (i, item) {
				fieldsList.push(item);
				valueList.push(record["LisTestForm_" + item]);
				where += " and " + item + "='" + record["LisTestForm_" + item]+"'";
			});
			//拼接其他条件
			if(startdate == "" && enddate != ""){
				where += " and GTestDate = '"+enddate+"'";
			}else if (startdate != "" && enddate == ""){
				where += " and GTestDate = '"+startdate+"'";
			}else if(startdate != "" && enddate != ""){
				where += " and GTestDate >= '"+startdate+"' and GTestDate <= '"+enddate+"'";
			}
			if(HistoryAtPresent){
				where += " and SectionId = " + sectionid + " and ReprotFormID = "+ testFormId;
			}else{
				if(HistorySampleType){
					var  sampletype = $("#resultContrastSample").val();
					if(sampletype != "" && sampletype != null){
						where += " and GSampleTypeID = " + sampletype;
					}			
				}
				var sectionno = sectionid;
				for(var i=0;i<checkedBox.length;i++){
					sectionno +=","+checkedBox[i].name.split('_')[1];
				}
				where += " and SectionId in ("+sectionno+")";
			}
			var reporturl = me.url.LabStartUrl + "?where=" + where + "&fieldsList=" + fieldsList.join() + "&valueList=" + valueList.join() +
				"&GTestDate=" + record.LisTestForm_GTestDate.split(" ")[0] + "&CName=" + record.LisTestForm_CName;
			var html = '<iframe src="' + reporturl + '" style="border: medium none;height:100%;width:100%;"></iframe>';
			$("#normaliframe").html(html);
		});
	};
	app.SampleInit = function () {
		var me = this;
		var url = me.url.sampleUrl + '&fields=LBSampleType_Id,LBSampleType_CName&sort=[{ "property":"LBSampleType_DispOrder","direction":"ASC"}]';
		url+="&where=lbsampletype.IsUse=1";
		uxutil.server.ajax({
			url: url
		}, function (data) {
			if (data) {
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (!value) return;
				var tempAjax = "<option value=''>请选择</option>";
				for (var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].LBSampleType_Id + "'>" + value.list[i].LBSampleType_CName + "</option>";
					$("#resultContrastSample").empty();
					$("#resultContrastSample").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	};
	app.createSectionCheckBox = function (sectionId){
		var me = this,
			sectionId = sectionId || null;
		if (!sectionId) {
			$("#historySectionForm").html("");
			return;
		}
		var queryLBSectionHisCompUrl = me.url.queryLBSectionHisCompUrl+ "&where=LBSection.id="+sectionId+"&fields=LBSectionHisComp_LBSection_Id,LBSectionHisComp_LBSection_CName,LBSectionHisComp_HisComp_Id,LBSectionHisComp_HisComp_CName",
			queryLBSectionurl = me.url.queryLBSectionurl+ "&where=lbsection.id!="+sectionId+"&fields=LBSection_Id,LBSection_CName";
		// uxutil.server.ajax({
		// 	url: queryLBSectionHisCompUrl
		// }, function (data) {
		// 	if (data) {
		// 		if(data.value){
		// 			if(data.value.count > 0){
		// 				var html = "";
		// 				for(var i=0;i<data.value.count;i++){
		// 					html += '<div class="layui-col-xs12"><div class="layui-form-item"><div class="layui-inline">'
		// 						+ '<input type="checkbox" name="IsUseSectionId_' + data.value.list[i].LBSectionHisComp_HisComp_Id + '" title="' + data.value.list[i].LBSectionHisComp_HisComp_CName+'" lay-skin="primary">'
		// 						+'</div></div></div>';
		// 				}
		// 				$("#historySectionForm").html(html);
		// 				form.render();
		// 			}
		// 		}else{
		// 			uxutil.server.ajax({
		// 				url: queryLBSectionurl
		// 			}, function (sdata) {
		// 				if(sdata){
		// 					if(sdata.value.count > 0){
		// 						var html = "";
		// 						for(var i=0;i<sdata.value.count;i++){
		// 							html += '<div class="layui-col-xs12"><div class="layui-form-item"><div class="layui-inline">'
		// 								+'<input type="checkbox" name="IsUseSectionId_'+sdata.value.list[i].LBSection_Id+'" title="'+sdata.value.list[i].LBSection_CName+'" lay-skin="primary">'
		// 								+'</div></div></div>';
		// 						}
		// 						$("#historySectionForm").html(html);
		// 						form.render();
		// 					}
		// 				}else{
		// 					uxbase.MSG.onError(sdata.msg);
		// 				}
		// 			});
		// 		}
		// 	} else {
		// 		uxbase.MSG.onError(data.msg);
		// 	}
		// });
		//查所有小组，如果当前小组配置了对比小组范围，则把配置的小组默认选中
		var LBSectionHisCompList=[];
		uxutil.server.ajax({
			url: queryLBSectionHisCompUrl
		}, function (data) {
			if (data) {
				if(data.value){
					if(data.value.count > 0){
						LBSectionHisCompList=data.value.list
					}
				}
			} else {
				//uxbase.MSG.onError(data.msg);
			}
		});
		uxutil.server.ajax({
			url: queryLBSectionurl
		}, function (sdata) {
			if(sdata){
				if(sdata.value.count > 0){
					var html = "";
					for(var i=0;i<sdata.value.count;i++){
						if(LBSectionHisCompList.length>0){
							for(var j=0;j<LBSectionHisCompList.length;j++){
								if(LBSectionHisCompList[j].LBSectionHisComp_HisComp_Id==sdata.value.list[i].LBSection_Id){
									html += '<div class="layui-col-xs12"><div class="layui-form-item"><div class="layui-inline">'
									+'<input checked="" type="checkbox" name="IsUseSectionId_'+sdata.value.list[i].LBSection_Id+'" title="'+sdata.value.list[i].LBSection_CName+'" lay-skin="primary">'
									+'</div></div></div>';
								}else{
									html += '<div class="layui-col-xs12"><div class="layui-form-item"><div class="layui-inline">'
									+'<input type="checkbox" name="IsUseSectionId_'+sdata.value.list[i].LBSection_Id+'" title="'+sdata.value.list[i].LBSection_CName+'" lay-skin="primary">'
									+'</div></div></div>';
								}
							}
						}else{
							html += '<div class="layui-col-xs12"><div class="layui-form-item"><div class="layui-inline">'
							+'<input type="checkbox" name="IsUseSectionId_'+sdata.value.list[i].LBSection_Id+'" title="'+sdata.value.list[i].LBSection_CName+'" lay-skin="primary">'
							+'</div></div></div>';
						}
						
						
					}
					$("#historySectionForm").html(html);
					form.render();
				}
				
			}else{
				uxbase.MSG.onError(sdata.msg);
			}
		});
	};
	//初始化yyyy-mm-dd
	app.initDate = function (id) {
		var me = this;
		//检测日期 yyyy-MM-dd
		laydate.render({//没有默认值
			elem: '#' + id,//'#LisTestForm_GTestDate',
			eventElem: '#' + id + '+i.layui-icon',
			type: 'date',
			show: true
		});
	};
	//监听新日期控件
	app.initDateListeners = function () {
		var me = this,
			today = new Date(),
			GTestDate = me.params.recode && me.params.recode["LisTestForm_GTestDate"] ? me.params.recode["LisTestForm_GTestDate"] : null,
			val = GTestDate ? uxutil.date.toString(GTestDate) : today;
		//赋值日期框
		$("#HistoryStartDate").val(uxutil.date.toString(uxutil.date.getNextDate(val, 1 - me.params.system.DateRange), true));
		$("#HistoryEndDate").val(uxutil.date.toString(val, true));
		//监听日期图标
		$("#historyForm input.myDate+i.layui-icon").on("click", function () {
			var elemID = $(this).prev().attr("id");
			if ($("#" + elemID).hasClass("layui-disabled")) return false;
			var key = $("#" + elemID).attr("lay-key");
			if ($('#layui-laydate' + key).length > 0) {
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
			}
			var datatype = $("#" + elemID).attr("data-type");
			if (datatype == "text") {
				me.initDate(elemID);
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
				var key = $("#" + elemID).attr("lay-key");
				$('#layui-laydate' + key).remove();
			}
		});
		//监听日期input -- 不弹出日期框
		$("#historyForm").on('focus', '.myDate', function () {
			var device = layui.device();
			if (device.ie) {
				window.event.returnValue = false;
			} else {
				window.event.preventDefault();
			}
			layui.stope(window.event);
			return false;
		});
	};
	//获得历史对比参数
	app.getHistoryParams = function (sectionId) {
		var me = this,
			paraTypeCode = 'NTestType_ItemResult_HistoryCompare_Para',
			url = me.url.getParamsListUrl;
		url += "&paraTypeCode=" + paraTypeCode + '&objectID=' + sectionId + "&fields=BPara_ParaNo,BPara_CName,BPara_TypeCode,BPara_ParaValue,BPara_IsUse";
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (res) {
			if (res.success) {
				if (res.ResultDataValue) {
					var data = JSON.parse(res.ResultDataValue).list;
					if (data.length > 0) {
						$.each(data, function (i, item) {
							var ParaNo = item["BPara_ParaNo"];
							switch (ParaNo) {
								//日期范围
								case "NTestType_ItemResult_HistoryCompare_0011":
									me.params.system.DateRange = item["BPara_ParaValue"];
									break;
								//区分样本类型
								case "NTestType_ItemResult_HistoryCompare_0013":
									me.params.system.IsSampleType = item["BPara_ParaValue"] == "0" || item["BPara_ParaValue"] == "false" ? false : true;
									break;
								//查询字段列表
								case "NTestType_ItemResult_HistoryCompare_0014":
									me.params.system.SearchFields = item["BPara_ParaValue"];
									break;
								//其他
								default:
									break;
							}
						});
					}
				}
			} else {
				uxbase.MSG.onError(res.msg);
			}
		});
	};
	//暴露接口
	exports('history', app);
});
