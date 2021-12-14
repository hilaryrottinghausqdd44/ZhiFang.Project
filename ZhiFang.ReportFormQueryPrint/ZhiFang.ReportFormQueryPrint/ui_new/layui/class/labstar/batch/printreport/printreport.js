/**
 * 批量打印报告
 * @author 王耀宗
 * @version 2021-5-7
 */

layui.extend({
	uxutil: 'ux/util',
	printpdf:'class/labstar/batch/printreport/printpdf'
}).use(['element','table','uxutil' ,'form', 'laydate','printpdf'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		$ = layui.jquery,
		form = layui.form,
		laydate = layui.laydate,
		printpdf = layui.printpdf,
		element = layui.element;
	var app = {};
	//小组ID
	app.SECTIONID = '10000000039';
	//服务地址
	app.url = {
		//就诊类型
		getSickTypeUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/LabStarGetSickType?fields=SickTypeNo,CName',
		//getSickTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&page=1&limit=100&fields=LBSickType_CName,LBSickType_Shortcode,LBSickType_DispOrder,LBSickType_Id&where=lbsicktype.IsUse=1',
		//送检科室
		getInspectionDepartmentUrl:  uxutil.path.ROOT + "/ServiceWCF/DictionaryService.svc/LabStarGetDeptListPaging?fields=CName,DeptNo&page=1&limit=1000",
		//getInspectionDepartmentUrl: uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true&page=1&limit=100&fields=HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode,HRDeptIdentity_HRDept_Id&where=(hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001101')",
		//病区	
		getDistrictUrl:  uxutil.path.ROOT + "/ServiceWCF/DictionaryService.svc/LabStarGetDistrict?fields=CName,DistrictNo",	
		//getDistrictUrl: uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true&fields=HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode,HRDeptIdentity_HRDept_Id&where=(hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001102')&_dc=1620716215359&page=1&start=0&limit=100",
		//查询下方列表
		getReportListUrl: uxutil.path.ROOT +'/ServiceWCF/ReportFormService.svc/LabStarSelectReportSort'
	};
	app.cols = {
		left: [
			[
				{ type: 'checkbox'},
				{
					type: 'numbers',
					title: '序号'
				},
				{
					field: 'RECEIVEDATE',width: 100,title: '检验日期',align: 'center',sort: true,
					templet: function (data) {
						var value = data["RECEIVEDATE"],
							v = uxutil.date.toString(value, true) || '';
						return "<div>" +v+ "</div>";
                    }
					
				},
				{field:'SAMPLENO',title:'样本号',width:100,align:'center',sort:true},
				{field:'CNAME',title:'姓名',width:100,align:'center',sort:false},
				{field:'GenderName',title:'性别',width:100,align:'center',sort:false},
				{field:'PatNo',title:'病历号',width:100,align:'center',sort:false},
				{field:'Serialno',title:'条码号',width:100,align:'center',sort:false},
				{field:'SampleTypeName',title:'样本类型',width:100,align:'center',sort:false},
				{field:'DistrictName',title:'病区',width:100,align:'center',sort:true},
				{field:'WardName',title:'病房',width:100,align:'center',sort:true},
				{field:'DeptName',title:'科室',width:100,align:'center',sort:true},
				{field:'SickTypeName',title:'就诊类型',width:100,align:'center',sort:true},
				{
					field: 'PRINTTIMES',title: '打印次数',align: 'center',width: 80,sort: false,
					//style:'background-color: #009688;',
					templet: function (data) {
						var that = this;//本列数据，data--本条数据
						//如果打印过则设置字体为红色
						if (data[that.field] > 0) {
							return "<div style='color:red;'>" + data[that.field] + "</div>";
						} else {
							return data[that.field];
                        }
					}
				},
				//以下不显示
				{ field: 'ReportFormID', width: 80, title: '报告id', sort: false, hide: true },
				{ field: 'RFID', width: 80, title: '检验单id', sort: false, hide: true },
				{ field: 'SECTIONNO', width: 80, title: '小组编号', sort: false, hide: true },
				{ field: 'SectionType', width: 80, title: '小组类型', sort: false, hide: true },
				{ field: 'DistrictNo', width: 80, title: '病区号', sort: false, hide: true },
				
				{ field: 'WardNo', width: 80, title: '病房号', sort: false, hide: true },
				{ field: 'SampleTypeNo', width: 80, title: '样本类型编号', sort: false, hide: true },
				{ field: 'SickTypeNo', width: 80, title: '就诊类型编号', sort: false, hide: true },
				{ field: 'DeptNo', width: 80, title: '科室编号', sort: false, hide: true },
			]
		]
	};
	//get参数
	app.paramsObj = {
		patno: '123',//病例号
		sectionNo:''	
	};
	/**数据条件对象*/
	app.serverParams = null;
	//初始化  
	app.init = function () {
		var me = this;
		//$("#rightContentBody").css("height", ($(window).height() - 40) + "px"); //设置中间容器高度
		me.initDateListeners();
		me.listeners();
		me.initAllSelect();
		//列表初次加载
		table.render({ elem: '#bottomTable', height: 'full-140', url: '', cols: me.cols.left, data: [], size: 'sm',text: {none: '暂无相关数据'}})
	};
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.PATNO) {
			me.paramsObj.patno = params.PATNO;
		}
		if(params.SECTIONNO){
			me.paramsObj.sectionNo = params.SECTIONNO;
		}
	};
	//查询功能
	app.onSearch = function (sortListStr,initSortFields) {
		var me=this;
		var url=me.url.getReportListUrl ;
		var where = "";
		//新开始
		me.getParams();
		where += "(SECTIONNO='"+me.paramsObj.sectionNo+"')" ;
		//未打印
		if($("#NotPrint").prop('checked')){
			where += " and printtimes < 1";
		}
		//检验日期
		if($("#RECEIVEDATE").val()){
			var info=me.isDateRangeValid($("#RECEIVEDATE").val());
			if(info.msg!=""){
				layer.msg(info.msg);
				return;
			}
			where += " and RECEIVEDATE >= '" + info.start + "' and RECEIVEDATE <='" +info.end + "'";
		}
		//审核日期
		if ($("#CheckTime").val()) {
			var info=me.isDateRangeValid($("#CheckTime").val());
			if(info.msg!=""){
				layer.msg(info.msg);
				return;
			}
			where += " and CheckDate >= '" + info.start + "' and CheckDate <='" + info.end + "'";
		}
		
		//送检科室
		if ($("#InspectionDepartmentSelect").val()) {
			where += " and DeptNo =" + $("#InspectionDepartmentSelect").val();
		}
		//病区
		if ($("#DistrictSelect").val()) {
			where += " and DistrictName ='" + $("#DistrictSelect").val()+"'";
		}
		//就诊类型
		if ($("#SickTypeSelect").val()) {
			where += " and SickTypeNo =" + $("#SickTypeSelect").val();
		}
		//样本号
		if ($("#sampleno_min").val()) {
			var samplenolist=$("#sampleno_min").val().split(",");
			var samplenostring='';
			for (var i = 0; i < samplenolist.length; i++) {
				if (samplenolist.length == 1 || i == samplenolist.length-1) {
					samplenostring += "'" + samplenolist[i] + "'"
				} else {
					samplenostring += "'" + samplenolist[i] + "',"
				}
			}
			where += ' and SAMPLENO in ('+samplenostring+')';
		}
		//样本号
		if ($("#sampleno_max").val()) {
			url += '&endSampleNo=' + $("#sampleno_max").val();
		}
		url += '?where=' + where;
		//console.log(me.url.getReportListUrl);
		//排序字段
		if(sortListStr!=null && sortListStr!="" && sortListStr.length>2){
			url +="&sort="+sortListStr;
		}
		me.initBottomTable(url,initSortFields);
		//新结束
	}
	//初始化底部侧列表  
	app.initBottomTable = function (dataUrl,initSortFields) {
		var me = this;
		//page和limit默认会传
		var url = dataUrl+ "&fields=" + me.GetBottomTableFields(me.cols.left[0], true) + "&t=" + new Date().getTime();
		var initSort= { field: 'RECEIVEDATE', type: 'desc' };
		if(initSortFields){
			initSort=initSortFields;
		}
		table.render({
			elem: '#bottomTable',
			height: 'full-140',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			cols: me.cols.left,
			limit: 50,
			limits: [10, 20, 50, 100, 200],
			autoSort: false, //禁用前端自动排序	
			initSort: initSort,//type如果大写的话 不能识别	
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
				//数据列表如果打印次数为0，则默认选中
				if (data.rows) {
					for (var i = 0; i < data.rows.length; i++) {
						if (data.rows[i].PRINTTIMES==0) {
							data.rows[i].LAY_CHECKED = true;
                        }
					}
                }
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.total || 0, //解析数据长度	
					"data": data.rows || []
				};
			},
			done: function (res, curr, count) {
				//table渲染完毕后，右侧预览第一条报告内容
				if (res) {
					var data = res.data ? res.data : [];
					if (data.length > 0) {
						
					}else{
						//添加初始排序后 不显示无数据文本 手动添加
						if ($("#bottomTable+div .layui-table-main .layui-none").length == 0)
							$("#bottomTable+div .layui-table-main").append('<div class="layui-none">暂无相关数据</div>');

					}
				}
			}
		});
	};
	//获取查询Fields	
	app.GetBottomTableFields = function (col, isString) {
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
				fields.push(obj);
			}
		}
		return fields;
	};

	//监听事件	
	app.listeners = function () {
		var me = this;
		//监听左侧列表行单击事件
		table.on('row(bottomTable)', function (obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
		});
		//监听排序
		table.on('sort(bottomTable)', function(obj){ //注：sort 是工具条事件名，test 是 table 原始容器的属性 lay-filter="对应的值"
			//console.log(obj.field); //当前排序的字段名
			//console.log(obj.type); //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
			var type=obj.type;
			if (type == null) {
                type = 'asc';
            }
			var sort="";
			switch (obj.field){
				case "RECEIVEDATE"://日期，样本号
					sort = [{ "property": "RECEIVEDATE", "direction": type }];
					break;
				case "SAMPLENO"://日期，样本号
					sort = [{ "property": "RECEIVEDATE", "direction": type }, { "property": "SAMPLENO", "direction": type }];
					break;
				case "DistrictName"://病区+病房+日期，样本号--病房？报告单无病房信息，患者就诊信息有
					sort=[{ "property": "DistrictNo", "direction": type }, { "property": "WardNo", "direction": type },{ "property": "RECEIVEDATE", "direction": type }];
					break;
				case "DeptName"://科室+日期，样本号
					sort=[{ "property": "DeptNo", "direction": type }, { "property": "RECEIVEDATE", "direction": type }, { "property": "SAMPLENO", "direction": type }];
					break;
				case "SickTypeName"://就诊类型+日期，样本号
					sort=[{ "property": "SickTypeNo", "direction": type }, { "property": "RECEIVEDATE", "direction": type }, { "property": "SAMPLENO", "direction": type }];
					break;
				case "SampleTypeName"://样本类型+日期，样本号
					sort=[{ "property": "SampleTypeNo", "direction": type }, { "property": "RECEIVEDATE", "direction": type }, { "property": "SAMPLENO", "direction": type }];
					break;

			}
			me.onSearch(JSON.stringify(sort),{ field: obj.field, type: type });
		});
		//查询按钮
		$('#ReportListSearch').on('click', function () {
			//提示消息内容div填充
			$("#messageDiv").html("暂无消息");
			me.onSearch(JSON.stringify([ { "property": "RECEIVEDATE", "direction": "desc" }]),null);
		});
		//打印按钮
		$('#printButton').on('click', function () {
			var selectData = layui.table.checkStatus('bottomTable').data || [];//bottomTable是table的id
			if (selectData.length > 0) {
				printpdf.print(selectData);
				//重新将列表置空
				me.onSearch(JSON.stringify([ { "property": "RECEIVEDATE", "direction": "desc" }]),null);
				//table.render({elem: '#bottomTable', url: '', cols: me.cols.left, data: [], size: 'sm',text: {none: '暂无相关数据'}})
			}else{
				layer.open({
					title: '提示'
					, content: '请至少选择一条数据！'
				});
			}
		});
		
	};
	
	//初始化新日期控件
	app.initDate = function () {
		var me = this,
			today = new Date();
		//查询日期
		laydate.render({//存在默认值
			elem: '#RECEIVEDATE',
			eventElem: '#RECEIVEDATE+i.layui-icon',
			type: 'date',
			range: '~',
			show: true,
			//value: uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true),
			done: function (value, date, endDate) { }
		});
	};
	app.initDate1 = function () {
		var me = this,
			today = new Date();
		//查询日期
		laydate.render({//存在默认值
			elem: '#CheckTime',
			eventElem: '#CheckTime+i.layui-icon',
			type: 'date',
			range: '~',
			show: true,
			//value: uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true),
			done: function (value, date, endDate) { }
		});
	};
	//监听新日期控件
	app.initDateListeners = function () {
		var me = this,
			today = new Date();
		//赋值日期框
		$("#RECEIVEDATE").val(uxutil.date.toString(uxutil.date.getNextDate(today, -1), true) + " ~ " + uxutil.date.toString(today, true));
		//监听检验日期图标
		$("#RECEIVEDATE+i.layui-icon").on("click", function () {
			var elemID = $(this).prev().attr("id");
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
		//监听审核日期图标
		$("#CheckTime+i.layui-icon").on("click", function () {
			var elemID = $(this).prev().attr("id");
			var key = $("#" + elemID).attr("lay-key");
			if ($('#layui-laydate' + key).length > 0) {
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
			}
			var datatype = $("#" + elemID).attr("data-type");
			if (datatype == "text") {
				me.initDate1();
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
				var key = $("#" + elemID).attr("lay-key");
				$('#layui-laydate' + key).remove();
			}
		});
		//监听日期input -- 不弹出日期框
        $("#DateType").on('focus', '.DateValue', function () {
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
	
	app.initAllSelect = function () {
		var me = this;
		//送检科室开始
		uxutil.server.ajax({
			url: me.url.getInspectionDepartmentUrl,
			async: false
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
				var tempAjax = "";
				for (var i = 0; i < value.rows.length; i++) {
					tempAjax += "<option value='" + value.rows[i].DeptNo + "'>" + value.rows[i].CName + "</option>";
				}
				$("#InspectionDepartmentSelect").append(tempAjax);
			} else {
				layer.msg(data.msg);
			}
		});
		//送检科室结束
		//病区开始
		uxutil.server.ajax({
			url: me.url.getDistrictUrl,
			async: false
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
				var tempAjax = "";
				for (var i = 0; i < value.rows.length; i++) {
					tempAjax += "<option value='" + value.rows[i].CName + "'>" + value.rows[i].CName + "</option>";
				}
				$("#DistrictSelect").append(tempAjax);
			} else {
				layer.msg(data.msg);
			}
		});
		//病区结束
		//就诊类型开始
		uxutil.server.ajax({
			url: me.url.getSickTypeUrl,
			async: false
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
				var tempAjax = '';
				for (var i = 0; i < value.rows.length; i++) {
					tempAjax += "<option value='" + value.rows[i].SickTypeNo + "'>" + value.rows[i].CName + "</option>";
				}
				$("#SickTypeSelect").append(tempAjax);
			} else {
				layer.msg(data.msg);
			}
		});
		//就诊类型结束
		form.render('select'); //需要渲染一下;
    };
	//判断日期范围格式是否正确：2021-01-01 ~ 2021-02-01
	app.isDateRangeValid=function(DateRange){
		var info={};
		var msg="";
		var start="";
		var end="";
		if (DateRange) {
			//验证日期是否正确
			if (DateRange.indexOf(" ~ ") == -1) {
				msg = "日期格式不正确!";
			}else{
				//验证是否都是日期
				var daterange=DateRange.split(" ~ ");
				if(daterange.length==2){
					start = daterange[0];
					end = daterange[1];
					var DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
					if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
						msg = "日期格式不正确!";
					}
					//验证开始日期是否大于结束日期
					//uxutil.date.difference()
					if (new Date(start).getTime() > new Date(end).getTime()) {
						msg = "开始日期不能大于结束日期!";
					}
					info.start=start;
					info.end=end;
				}else{
					msg = "日期格式不正确!";
				}
			}
		}
		info.msg=msg;
		return info;
	};
	$(function(){
		var tips;
		$('#sampleNoDiv').on({
			mouseenter:function(){
				var that = this;
				tips =layer.tips("<span style='color:#000;'>样本号用逗号隔开</span>",that,{tips:[2,'#fff'],time:0,area: 'auto',maxWidth:500});
			},
			mouseleave:function(){
				layer.close(tips);
			}
		});
	})
	//初始化调用入口
	app.init();
});