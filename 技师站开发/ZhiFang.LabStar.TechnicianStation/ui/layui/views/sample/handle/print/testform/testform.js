/**
   @Name：打印检验清单
   @Author：GHX
   @version 2021-05-20
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'element', 'layer','form','table','laydate'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
		laydate = layui.laydate,
		table = layui.table,
		form = layui.form,
		uxbase = layui.uxbase,
        uxutil = layui.uxutil;
    var app = {};
	app.url={
		//获取样本单数据
		selectUrl:uxutil.path.ROOT
				   +'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormAndItemNameList?isPlanish=true'
	};
	app.col=[];
	app.defaultOrderBy=[{property:'LisTestForm_GTestDate',direction:'ASC'},{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}];
	//公共参数
	app.paramsObj = {		
		SectionID :null,
		statusid:0,
		loadindex:null
	};
    //初始化
    app.init = function () {
        var me = this;
		me.paramsObj.loadindex = layer.load();
        me.getParams();
		$("#MainStatusID").val(me.paramsObj.statusid);
		form.render("select");
		app.initDateListeners();
        me.initListeners();		
		me.initTable();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SECTIONID) {
			me.paramsObj.SectionID = params.SECTIONID;
		}
	};
    //监听
    app.initListeners = function () {
        var me = this;
		$("#seach").click(function(){
			me.paramsObj.loadindex = layer.load();
			var url = me.getLoadUrl();
			table.reload('operateTable',{
				url:url
			});
		});
		$("#print").click(function(){
			var checkedData = table.checkStatus('operateTable').data;
			if(checkedData.length > 0){
				var list = [],data=[[]];
				for (var i in checkedData) {
					list.push(checkedData[i]);
				}
				if(list.length==0)return data;
				data = JSON.stringify([list]).replace(RegExp("LisTestForm_", "g"), "").replace(RegExp("LisPatient_", "g"), "");
				parent.layer.open({
					type: 2,
					area:  ['45%', '55%'],
					fixed: false,
					maxmin: false,
					title: '打印',
					content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=1&ModelType=1&ModelTypeName=检验清单',
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
		$("#design").click(function(){
			parent.layer.open({
				type: 2,
				area:  ['95%', '90%'],
				fixed: false,
				maxmin: false,
				title: '模板设计',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/index.html?type=2&BusinessType=1&ModelType=1',
				end: function () {
				}
			});
		});
    };  
	 app.initTable = function(){
		var me = this;	
			
		me.col = [[ 
				{
					type:'checkbox',field:'LAY_CHECKED'
				},
		 		{
		 			field: 'LisTestForm_Id',	title: '检验单ID',minWidth: 125,hide: true,	sort: false
		 		} , {
		 			field: 'LisTestForm_GTestDate',	title: '检验日期',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_GSampleNo',	title: '样本号',minWidth: 80,sort: false
		 		} , {
		 			field: 'LisTestForm_GSampleNoForOrder',	title: '样本号排序',minWidth: 130,hide:true,sort: false
		 		} , {
		 			field: 'LisTestForm_CName',	title: '姓名',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_BarCode',	title: '条码号',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_PatNo',	title: '病历号',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_GSampleType',	title: '样本类型',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_LisPatient_DeptName',	title: '科室',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisTestForm_ItemNameList',	title: '样本单项目',minWidth: 120,	sort: false
		 		}  
		]];
		var url = me.getLoadUrl();
		table.render({
		 	elem: '#operateTable',
		 	height: 'full-80',
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
		 		if(me.paramsObj.loadindex){
		 			layer.close(me.paramsObj.loadindex);
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
			DateValue = $("#DateValue").val(),
			StartSampleNo = $("#StartSampleNo").val(),
			EndSampleNo = $("#EndSampleNo").val(),
			MainStatusID = $("#MainStatusID").val(),
			TestItem = $("input[name='TestItem']:checked"),
			ItemCName  =$("input[name='ItemCName']:checked"),
			url="",
			where="",
			params = [];	
		url = me.url.selectUrl+"&fields="+me.GetTableFields(me.col[0],true)+"&sort="+JSON.stringify(me.defaultOrderBy);
		//样本号范围
		if(StartSampleNo)url+='&beginSampleNo='+StartSampleNo;
		if(EndSampleNo)url+='&endSampleNo='+ EndSampleNo;
		//检验项目类型与项目名称类型
		if(TestItem)url+='&isOrderItem='+TestItem[0].value;//1组合项目 0单项
		if(ItemCName)url+='&itemNameType='+ ItemCName[0].value;
		//小组Id
		if(me.paramsObj.SectionID)params.push("listestform.LBSection.Id=" + me.paramsObj.SectionID + "");
		//状态
		if(MainStatusID)params.push("listestform.MainStatusID=" + MainStatusID + "");
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
		    params.push("listestform.GTestDate>='" + startDate + "'");
		    params.push("listestform.GTestDate<'" + endDate + "'");
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