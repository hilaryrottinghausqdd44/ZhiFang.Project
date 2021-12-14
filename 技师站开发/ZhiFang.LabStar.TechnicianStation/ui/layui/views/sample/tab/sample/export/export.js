/**
   @Name：项目结果查看与导出
   @Author：GHX
   @version 2021-05-24
 */
var fireEventSaveInfo;
var ItemData = null;
window.fireEventSaveInfoFun = function(ItemData,callback){
	ItemData = ItemData;
	fireEventSaveInfo = callback;
};
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'element', 'layer','form','laydate','table'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
		laydate = layui.laydate,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table,
		form = layui.form;
    var app = {};
	app.config = {
		loadindex:null
	}
	//服务地址
	app.url={
		//项目输出查询
		LisTestFormReCheck:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemBySampleNo?isPlanish=true',
		//小组项目查询
		SearchLBSectionItem:uxutil.path.ROOT
						+'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true'									
	};
app.defaultOrderBy=[{property:'LisTestItem_LisTestForm_GTestDate',direction:'ASC'},{property:'LisTestItem_LisTestForm_GSampleNoForOrder',direction:'ASC'}];
	//get参数
	app.paramsObj = {
		SectionNo:null,
		SectionName:null
	};
    //初始化
    app.init = function () {
        var me = this;		
		me.getParams();
		me.inithtml();
		me.initDateListeners();
        me.initListeners();
		me.inittalbe();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if(params.SECTIONNO){
			me.paramsObj.SectionNo = params.SECTIONNO;
		}
		if(params.SECTIONNAME){
			me.paramsObj.SectionName = params.SECTIONNAME;
		}
	};
	app.inithtml = function(){
		var me = this;
		$("#export_centre").css("height",$(window).height()-100+"px");
		if(me.paramsObj.SectionName){
			$("#export_section").val(me.paramsObj.SectionName);
		}		
		app.initsectionitem();
	};
	app.initsectionitem = function(){
		var me = this;
		var url = me.url.SearchLBSectionItem+"&fields=LBSectionItem_LBItem_Id,LBSectionItem_LBItem_CName";
			url += "&where=lbsectionitem.IsUse=1 and lbsectionitem.LBSection.Id="+me.paramsObj.SectionNo;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = '';
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].LBSectionItem_LBItem_Id + "'>" + value.list[i].LBSectionItem_LBItem_CName + "</option>";
					$("#export_testitem").empty();
					$("#export_testitem").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
				if(ItemData){
					$("#export_testitem").val(ItemData[0].LisTestItem_LBItem_Id);
					form.render('select');
				}
			} else {
				uxbase.MSG.onError(data.msg);
			}
		}); 
	 };
	 //设置dom元素高度
	 app.setDomHeight = function () {
	     var me = this;
	     //设置iframe父元素高度
		 $("#export_centre").css("height",$(window).height()-100+"px");
	 };
    //监听
    app.initListeners = function () {
        var me = this;
		/* $(window).resize(function () {
		    clearTimeout(iTime);
		    iTime = setTimeout(function () {
		        me.setDomHeight();
		    }, 500);
		}); */ 
		element.on('tab(Tab)', function (data) {
		   if(data.index == 0){//检验确认人
			   me.inittalbe();
		   }else if (data.index == 1){//审核人设置
			  
		   }
		});
		$("#search").click(function(){
			var url = me.getLoadUrl();	
			me.config.loadindex = layer.load();
			table.reload("export_talbe",{
				url : url
			});
		});
		$("#export").click(function(){
			var checkedData = table.checkStatus('export_talbe').data;
			if(checkedData.length > 0){
				var list = [],data=[[]];
				for (var i in checkedData) {
					list.push(checkedData[i]);
				}
				if(list.length==0)return data;
				data = JSON.stringify([list]).replace(RegExp("LisTestItem_", "g"), "");
				parent.layer.open({
					type: 2,
					area:  ['45%', '55%'],
					fixed: false,
					maxmin: false,
					title: '导出',
					content: uxutil.path.ROOT + '/ui/layui/views/system/comm/template/print/index.html?BusinessType=1&ModelType=5&isShowPrintInfo=false&IsHasExportExcelBtn=true&ModelTypeName=项目结果导出',
					success:function(layero,index){
						var iframe = $(layero).find("iframe")[0].contentWindow;
						iframe.PrintDataStr = data;
					},
					end: function () {
					}
				});
			}else{
				uxbase.MSG.onWarn("请选择数据!");
			}
		});
		$("#print").click(function(){
			var checkedData = table.checkStatus('export_talbe').data;
			if(checkedData.length > 0){
				var list = [],data=[[]];
				for (var i in checkedData) {
					list.push(checkedData[i]);
				}
				if(list.length==0)return data;
				data = JSON.stringify([list]).replace(RegExp("LisTestItem_", "g"), "");
				parent.layer.open({
					type: 2,
					area:  ['45%', '55%'],
					fixed: false,
					maxmin: false,
					title: '打印',
					content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=1&ModelType=5&ModelTypeName=项目结果打印',
					success:function(layero,index){
						var iframe = $(layero).find("iframe")[0].contentWindow;
						iframe.PrintDataStr = data;
					},
					end: function () {
					}
				});
			} else {
				uxbase.MSG.onWarn("请选择数据!");
			}
		});
		$("#design").click(function(){
			parent.layer.open({
				type: 2,
				area:  ['95%', '90%'],
				fixed: false,
				maxmin: false,
				title: '模板设计',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/index.html?type=2&BusinessType=1&ModelType=5',
				end: function () {
				}
			});
		});
    };
	app.inittalbe = function(){
		var me = this;
		me.col = [[ 
				{
					type:'checkbox',field:'LAY_CHECKED'
				},
		 		{
		 			field: 'LisTestItem_Id',	title: '检验单ID',minWidth: 125,hide: true,	sort: false
		 		} , {
		 			field: 'LisTestItem_LisTestForm_GTestDate',	title: '检验日期',minWidth: 80,	sort: false,
					templet:function(data){
						if(data.LisTestItem_LisTestForm_GTestDate){
							return data.LisTestItem_LisTestForm_GTestDate.split(" ")[0];
						}else {
							return "";
						}
					}
		 		}, {
		 			field: 'LisTestItem_LisTestForm_TestType',	title: '类型',minWidth: 20,	sort: false,
					templet:function(data){
						if(data.LisTestItem_LisTestForm_TestType == 1){
							return "常";
						}else if(data.LisTestItem_LisTestForm_TestType == 2){
							return "急";
						}else if(data.LisTestItem_LisTestForm_TestType == 3){
							return "质";
						}
					}
		 		} , {
		 			field: 'LisTestItem_LisTestForm_GSampleNo',	title: '样本号',minWidth: 80,sort: false
		 		} , {
		 			field: 'LisTestItem_LisTestForm_GSampleNoForOrder',	title: '样本号排序',minWidth: 130,hide:true,sort: false
		 		} , {
		 			field: 'LisTestItem_LisTestForm_CName',	title: '姓名',minWidth: 80,	sort: false
		 		} , {
		 			field: 'LisTestItem_LisTestForm_BarCode',	title: '条码号',minWidth: 120,	sort: false,hide:true
		 		} , {
		 			field: 'LisTestItem_LisTestForm_PatNo',	title: '病历号',minWidth: 90,	sort: false
		 		} , {
		 			field: 'LisTestItem_ReportValue',	title: '结果值',minWidth: 90,	sort: false
		 		}, {
		 			field: 'LisTestItem_QuanValue',	title: '定量结果',minWidth: 90,	sort: false
		 		} , {
		 			field: 'LisTestItem_ResultStatus',	title: '检验结果状态显示',minWidth: 110,	sort: false
		 		} , {
		 			field: 'LisTestItem_LisTestForm_LBSection_Id',	title: '',minWidth: 120,	sort: false,hide:true
		 		} , {
		 			field: 'LisTestItem_LisTestForm_LBSection_CName',	title: '',minWidth: 120,	sort: false,hide:true
		 		}, {
		 			field: 'LisTestItem_LBItem_Id',	title: '',minWidth: 120,	sort: false,hide:true
		 		}, {
		 			field: 'LisTestItem_LBItem_CName',	title: '',minWidth: 120,	sort: false,hide:true
		 		}
		]];
		var url = app.getLoadUrl();
		table.render({
		 	elem: '#export_talbe',
		 	height: 'full-140',
		 	size: 'sm',
		 	page: false,
		 	//data: data,
		 	url: url,
		 	cols:me.col,
		 	limit: 99999999,
		 	autoSort: true, //禁用前端自动排序
		 	text: {
		 		none: '暂无相关数据'
		 	},
		 	response: function() {
		 		return {
		 			statusCode: true, //成功状态码
		 			statusName: 'code', //code key
		 			msgName: 'msg ', //msg key
		 			dataName: 'data' //data key
		 		}
		 	},
		 	parseData: function(res) { //res即为原始返回的数据
		 		if (!res) return;
		 		var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				if(data.count > 0){
					for(var i=0;i<data.count;i++){
						data.list[i]["LAY_CHECKED"] = true;
					}	
				}
		 		return {
		 			"code": res.success ? 0 : 1, //解析接口状态
		 			"msg": res.ErrorInfo, //解析提示文本
		 			"count": data.count || 0, //解析数据长度
		 			"data": data.list || []
		 		};
		 	},
		 	done: function(res, curr, count) {	
		 		if(me.config.loadindex){
		 			layer.close(me.config.loadindex);
		 		}
		 	}
		 });
	};
	//获取查询Fields
	app.GetTableFields = function(col, isString) {
		var me = this,
			columns = col || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				if(obj != "LAY_CHECKED"){
					fields.push(obj);
				}
			}
		}
		return fields;
	};
	app.getLoadUrl = function(){
		var me = this,
			sectionno  = me.paramsObj.SectionNo,
			request = $("#export_request").prop("checked"),
			report = $("#export_report").prop("checked"),
			startsampleno =	$("#StartSampleNo").val(),
			endsampleno = $("#EndSampleNo").val(),
			testitem = $("#export_testitem").val(),
			DateValue = $("#DateValue").val(),
			url="",
			where="",
			params = [];	
		url = me.url.LisTestFormReCheck+"&fields="+me.GetTableFields(me.col[0],true)+"&sort="+JSON.stringify(me.defaultOrderBy);
		if(!sectionno || !DateValue){
			uxbase.MSG.onWarn("检验小组和项目不可为空!");
			return;
		}
		//样本号范围
		if(startsampleno)url+='&beginSampleNo='+startsampleno;
		if(endsampleno)url+='&endSampleNo='+ endsampleno;
		//小组和项目
		params.push("listestitem.LisTestForm.LBSection.Id="+sectionno);
		params.push("listestitem.LBItem.Id="+testitem);
		//审定状态
		if(request && report){
			params.push("listestitem.LisTestForm.MainStatusID in (0,1,2)");
		}else if(!request && report){
			params.push("listestitem.LisTestForm.MainStatusID in (2)");
		}else if(request && !report){
			params.push("listestitem.LisTestForm.MainStatusID in (0,1)");
		}else{
			uxbase.MSG.onWarn("审定前后至少选择一种!");
			return;
		}
		//时间范围
		if (DateValue) {
			//验证日期是否正确
			var msg = "";
			if (DateValue.indexOf(" - ") == -1) {
				msg = "日期格式不正确!";
			}
			//验证是否都是日期
			var start = DateValue.split(" - ")[0],
				end = DateValue.split(" - ")[1],
				DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
			if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
				msg = "日期格式不正确!";
			}
			//验证开始日期是否大于结束日期
			if (new Date(start).getTime() > new Date(end).getTime()) {
				msg = "开始日期不能大于结束日期!";
			}
			if (msg != "") {
				uxbase.MSG.onWarn(msg);
				return false;
			}
			var startDate = DateValue.split(" - ")[0],
				endDate = uxutil.date.toString(uxutil.date.getNextDate(DateValue.split(" - ")[1], 1), true);
			params.push("listestitem.LisTestForm.GTestDate>='" + startDate + "'");
			params.push("listestitem.LisTestForm.GTestDate<'" + endDate + "'");
		}else{
			uxbase.MSG.onWarn("请选择时间!");
			return;
		}	
		
		if(params.length > 0){
			url += "&where="+params.join(" and ");
		}
		return url;
	};
	//初始化新日期控件
	app.initDate = function () {
		 var me = this,
			 today = new Date();
		 //查询日期
		 laydate.render({//存在默认值
			 elem: '#DateValue',
			 eventElem:'#DateValue+i.layui-icon',
			 type: 'date',
			 range: true,
			 show:true,
			 //value: uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true),
			 done: function (value, date, endDate) { }
		 });
	};
	//监听新日期控件
	app.initDateListeners = function () {
		 var me = this,
			 today = new Date();
		 //赋值日期框
		 $("#DateValue").val(uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true));
		 //监听日期图标
		 $("#DateValue+i.layui-icon").on("click", function () {
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
				 me.initDate();
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
				 var key = $("#" + elemID).attr("lay-key");
				 $('#layui-laydate' + key).remove();
			 }
		 });
		 //监听日期input -- 不弹出日期框
		 $("#baseDomHeight").on('focus', '#DateValue', function () {
			 me.preventDefault();
			 layui.stope(window.event);
			 return false;
		 });
	};
	//阻止默认事件
	app.preventDefault = function () {
		 var me = this,
			 device = layui.device();
		 if (device.ie) {
			 window.event.returnValue = false;
		 } else {
			 window.event.preventDefault();
		 }
	};
    //初始化
    app.init();
});