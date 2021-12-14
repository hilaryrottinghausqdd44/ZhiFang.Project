layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
	echarts:'ux/other/echarts'
}).use(['uxutil','uxtable','echarts','layer','element','form','laydate'],function(){
	var $=layui.$,
		form = layui.form,
		laydate = layui.laydate,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable,
		echarts = layui.echarts,
		element = layui.element;
		
	//获取送检科室列表地址
	var GET_DEPT_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL";
	//获取消息列表地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL";
	//获取的消息字段数组
	var MSG_FIELDS = [
		"Id","MsgContent","MsgTypeCode","SenderID","SenderName",
		"ConfirmFlag","HandleFlag","RequireConfirmTime","ConfirmDateTime","HandlingDateTime",
		"RecDeptName","RecSickTypeName","DataAddTime","WarningUpLoadDateTime"
	];
	//消息类型编码
	var MSG_TYPE_CODE = 'ZF_LAB_START_CV';
	
	//历史危急值列表对象
	var CV_TABLE_HISTORY = null;
	//状态列表-0无，1超时未确认，2待确认，3待处理，4完结，5未完结
	var STATUS_LIST = [
		"",
		"scmsg.ConfirmFlag='0' and scmsg.RequireConfirmTime < CONVERT(varchar(100),GETDATE(),20)",
		"scmsg.ConfirmFlag='0' and (scmsg.RequireConfirmTime is null or scmsg.RequireConfirmTime > CONVERT(varchar(100),GETDATE(),20))",
		"scmsg.ConfirmFlag='1' and scmsg.HandleFlag='0'",
		"scmsg.HandleFlag='1'",
		"scmsg.HandleFlag='0'"
	];
	//历史危急值概况-饼图数据
	var CV_CHARTS_PIE_DATA = null;
	//历史响应情况-效率数据
	var CV_HISTORY_PROGRESS_DATA = null;
	
	//日期时间范围
	var LAYDATE_DATES = laydate.render({
		elem:'#dates',
		range:true,
		done: function(value,date,endDate){
			$('#DateTimeRangeType').val("");
			setTimeout(function(){
				onSearch();
			},100);
		}
	});
	//时间范围类型变化监听
	form.on('select(DateTimeRangeType)', function(data){
		var value = data.value;
		if(value != ""){
			var today = uxutil.date.toString(new Date(),true);
			var start = uxutil.date.toString(uxutil.date.getNextDate(today,1-parseInt(value)),true);
			
			$("#dates").val(start + ' - ' + today);
			LAYDATE_DATES.config.value = start + ' - ' + today;
			onSearch();
		}else{
			$("#dates").val('');
			LAYDATE_DATES.config.value = '';
			onSearch();
		}
	});
	
	//状态下拉监听
	form.on('select(status)', function(data){
		onSearch();
	});
	//开单科室下拉监听
	form.on('select(RecDept)', function(data){
		onSearch();
	});
	//查询按钮点击处理
	$("#search-button").on("click",function(){
		onSearch();
	});
	//清空按钮点击处理
	$("#search-clear").on("click",function(){
		onClearSearchParams();
	});
	//清空查询条件
	function onClearSearchParams(){
		$("#DateTimeRangeType").val('');
		$('#dates').val('');
		$("#status").val('');
		form.render('select');
		onSearch();
	};
	//数据查询
	function onSearch(){
		var dates = $("#dates").val(),
			RecDept = $("#RecDept").val(),
			where = [];
		
		where.push("scmsg.MsgTypeCode='" + MSG_TYPE_CODE + "'");
		
		if(dates){
			var splitField = $("#dates").attr("placeholder");
			var dateArr = dates.split(splitField);
			where.push("scmsg.DataAddTime >='" + dateArr[0] +"' and scmsg.DataAddTime <'" + 
				uxutil.date.toString(uxutil.date.getNextDate(dateArr[1]),true) + "'");
		}
		if(RecDept){
			where.push("scmsg.RecDeptID ='" + RecDept + "'");
		}
		
		where = where.join(' and ');
		
		var loadIndex = layer.load();
		//历史危急值列表-数据变更
		onChangeTableData(where,function(){
			//历史危急值概况-数据变更
			onChangePieData(where,function(){
				//历史响应情况-数据变更
				onChangeHistoryProgressData(where,function(){
					layer.close(loadIndex);
				});
			});
		});
	};
	//历史危急值列表-数据变更
	function onChangeTableData(where,callback){
		var status = $('#status option:selected').val(),
			fields = [],
			thisWhere = where;
			
		if(status){
			thisWhere += ' and ' + STATUS_LIST[status];
		}
		
		for(var i in MSG_FIELDS){
			fields.push('SCMsg_' + MSG_FIELDS[i]);
		}
			
		CV_TABLE_HISTORY.reload({
			url:GET_MSG_LIST_URL,
			where:$.extend({},{
				where:thisWhere
			},{
				fields:fields.join(',')
			}),
			done:function(){
				callback && callback();
			}
		});
	};
	//历史危急值概况-数据变更
	function onChangePieData(where,callback){
		var resultCount = 4,
			resultNum = 0;
			
		CV_CHARTS_PIE_DATA = [];
		
		//超时未确认-获取总数
		getMsgCountByWhere((where + ' and ' + STATUS_LIST[1]),function(count){
			CV_CHARTS_PIE_DATA.push({name:'超时未确认',value:count});
			//待确认-获取总数
			getMsgCountByWhere((where + ' and ' + STATUS_LIST[2]),function(count){
				CV_CHARTS_PIE_DATA.push({name:'待确认',value:count});
				//已确认-获取总数
				getMsgCountByWhere((where + ' and ' + STATUS_LIST[3]),function(count){
					CV_CHARTS_PIE_DATA.push({name:'已确认',value:count});
					//已处理-获取总数
					getMsgCountByWhere((where + ' and ' + STATUS_LIST[4]),function(count){
						CV_CHARTS_PIE_DATA.push({name:'已处理',value:count});
						initChartsPie();
						callback && callback();
					});
				});
			});
		});
	};
	//历史响应情况-数据变更
	function onChangeHistoryProgressData(where,callback){
		var resultCount = 5,
			resultNum = 0;
			
		CV_HISTORY_PROGRESS_DATA = {};
		
		//每加载一个数据执行
		var onLoadOver = function(){
			resultNum++
			if(resultNum == resultCount){
				initHistoryProgressHtml();
				callback && callback();
			}
		};
		//历史总数据-获取总数
		getMsgCountByWhere(where,function(count){
			CV_HISTORY_PROGRESS_DATA.ALL_COUNT = count;
			onLoadOver();
		});
		//及时确认率-获取总数
		var thisWhere = where + " and scmsg.ConfirmDateTime <= scmsg.RequireConfirmTime";
		getMsgCountByWhere(thisWhere,function(count){
			CV_HISTORY_PROGRESS_DATA.CONFIRM_INTIME_COUNT = count;
			onLoadOver();
		});
		//超时率-获取总数：已经确认+ 时间
		var thisWhere = where + " and (" +
			"(scmsg.ConfirmFlag='1' and scmsg.ConfirmDateTime > scmsg.RequireConfirmTime)" +
			" or " +
			"(scmsg.ConfirmFlag='0' and scmsg.RequireConfirmTime<CONVERT(varchar(100),GETDATE(),20))" +
		")";
		getMsgCountByWhere(thisWhere,function(count){
			CV_HISTORY_PROGRESS_DATA.TIMEOUT_COUNT = count;
			onLoadOver();
		});
		//确认率-获取总数
		var thisWhere = where + " and scmsg.ConfirmFlag='1'";
		getMsgCountByWhere(thisWhere,function(count){
			CV_HISTORY_PROGRESS_DATA.CONFIRM_COUNT = count;
			onLoadOver();
		});
		//处理率-获取总数
		var thisWhere = where + " and scmsg.HandleFlag='1'";
		getMsgCountByWhere(thisWhere,function(count){
			CV_HISTORY_PROGRESS_DATA.HANDLE_COUNT = count;
			onLoadOver();
		});
	};
	//按类型加载消息总数
	function getMsgCountByWhere(where,callback){
		uxutil.server.ajax({
			url:GET_MSG_LIST_URL,
			data:{
				fields:'SCMsg_Id',
				where:where
			}
		},function(data){
			if(data.success){
				callback((data.value || {}).count || 0);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//初始化历史数据列表
	function initCVTableHistory(){
		var url = GET_MSG_LIST_URL + "?fields=SCMsg_" + MSG_FIELDS.join(",SCMsg_") + "&where=scmsg.MsgTypeCode='ZF_LAB_START_CV'";
		
		CV_TABLE_HISTORY = uxtable.render({
			elem:$("#cv_table_history"),
			page:true,
			limit:10,
			skin:'line',
			cols:[[
				{field:'RecDeptName',width:100,title:'开单科室',sort:true},
				{field:'DoctorName',width:100,title:'开单医生',sort:true},
				{field:'RecSickTypeName',width:90,title:'就诊类型'},
				//{field:'PatientName',width:90,title:'病人姓名'},
				//{field:'PatientSex',width:60,title:'性别'},
				//{field:'PatientAge',width:60,title:'年龄'},
				
				{field:'ConfirmFlag',width:80,title:'状态',templet:function(d){
					var result = '';
					if(d.HandleFlag == '0' && d.isTimeout){
						if(d.isTimeout){
							result = '<span style="color:#FF5722;">已超时</span>';
						}else{
							result = '<span style="color:#1E9FFF;">未确认</span>';
						}
					}else if(d.HandleFlag == '1'){
						result = '<span style="color:#009688;">已处理</span>';
					}else{
						result = '<span style="color:#393D49;">已确认</span>';
					}
					return result;
				}},
				{field:'DataAddTime',width:160,title:'产生时间',sort:true},
				{field:'ConfirmDateTime',width:160,title:'确认时间',sort:true},
				{field:'HandlingDateTime',width:160,title:'处理时间',sort:true},
				{field:'WarningUpLoadDateTime',width:160,title:'上报时间',sort:true},
				{field:'SendSectionName',minWidth:100,title:'检验小组',sort:true},
				{field:'Id',title:'ID',width:180,hide:true}
			]],
			//url:url,
			changeData:function(data){
				return changeTableResultData(data);
			}
		});
		
		//监听行双击事件
		CV_TABLE_HISTORY.table.on('rowDouble(cv_table_history)', function(obj){
			layer.open({
				title:'危急值消息详情',
				type:2,
				content:'../../form/index.html?isShow=true&id=' + obj.data.Id + '&t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['95%','95%']
			});
		});
	};
	//数据处理
	function changeTableResultData(data){
		var MsgObjJson = $.parseJSON(data.MsgContent),//消息对象
			MsgContent = MsgObjJson.MSG.MSGCONTENT,//消息内容
			MsgTitle = MsgContent.MSGTITLE,//消息标题
			MsgKey = MsgContent.MSGKEY,//病人信息
			MsgList = MsgContent.MSGBODY.MSG;//消息列表
			
		//XML格式的消息支持一个消息多条内容，如果是单条内容的自动转变成数组
		if(!$.isArray(MsgList)){
			MsgList = [MsgList];
		}
		
		data.SendSectionName = MsgKey.SECTIONNAME;//小组名称
		data.SampleNo = MsgKey.SAMPLENO;//样本号
		data.JzType = MsgKey.SICKTYPENAME;//就诊类型
		data.DeptName = MsgKey.DEPTNAME;//开单科室
		data.DoctorName = MsgKey.DOCTOR;//开单医生
		data.PatNo = MsgKey.PATNO;//病历号
		data.PatientName = MsgKey.CNAME;//病人姓名
		data.PatientSex = MsgKey.GENDERNAME;//性别
		data.PatientAge = MsgKey.AGE + MsgKey.AGEUNITNAME;//年龄
		
		var MsgAll = [];
		for(var i in MsgList){
			var status = MsgList[i].RESULTSTATUS;
			if(status.indexOf("H") != -1){
				status = '<span style="color:red;">' + status + '</span>';
			}if(status.indexOf("L") != -1){
				status = '<span style="color:blue;">' + status + '</span>';
			}
			MsgAll.push(MsgList[i].TESTITEMNAME + ' ' + MsgList[i].REPORTVALUEALL + ' ' + status);
		}
		data.MsgAll = MsgAll.join('</br>');
		
		data.ItemName = "";//检查项目
		data.ResultValue = "";//结果值
		data.ResultStatus = "";//状态
		data.OutTimes = 0;//超时分钟
		
		//是否超时
		var serverDateTimes = uxutil.server.date.getTimes(),
			isTimeout = false;
		if(!data.ConfirmDateTime){
			var RequireConfirmTime = data.RequireConfirmTime;
			var timesStr = uxutil.date.difference(RequireConfirmTime,serverDateTimes);
			if(timesStr && timesStr.slice(0,1) != '-'){
				isTimeout = true;
			}
		}
		data.isTimeout = isTimeout;
		
		return data;
	};
	//危急值饼图变化
	function initChartsPie(){
		var options = { 
			title : {
				text: '危急值响应情况',
				x: 'center',
				textStyle: {
					fontSize: 14
				}
			},
			tooltip : {
				trigger: 'item',
				formatter: "{a} <br/>{b} : {c} ({d}%)"
			},
			legend: {
				//orient : 'vertical',
				//x : 'left',
				y:'bottom',
				data:['超时未确认','待确认','已确认','已处理']
			},
			series : [{
				name:'访问来源',
				type:'pie',
				//radius : '55%',
				radius: ['20%', '70%'],
				center: ['50%', '50%'],
				label:{ 
					show:true, 
					formatter:'{b} : {c} ({d}%)' 
				},
				itemStyle:{
					normal:{
						borderWidth: 5, 
						borderColor: '#fff',
						color:function(params){
							var colorList = [          
								'#A52A2A','#808080','#1E9FFF','#009688'
							];
							return colorList[params.dataIndex]
						}
					}
				},
				labelLine:{show:true},
				data:CV_CHARTS_PIE_DATA
			}]
		};
		
		var pie = echarts.init($("#cv_charts_pie")[0],layui.echartsTheme);
		pie.setOption(options);
		
		//页面大小变化处理
		$(window).on("resize",function(){
			pie.resize();
		});
	};
	//初始化历史处理情况HTML
	function initHistoryProgressHtml(){
		var html = [];
		
		var ConfirmInTimeRate = 0,//及时确认率
			TimeoutRate = 0,//超时率
			ConfirmRate = 0,//确认率
			HandleRate = 0;//处理率
			
		if(CV_HISTORY_PROGRESS_DATA.ALL_COUNT > 0){
			ConfirmInTimeRate = Math.floor(10000*CV_HISTORY_PROGRESS_DATA.CONFIRM_INTIME_COUNT/CV_HISTORY_PROGRESS_DATA.ALL_COUNT)/100;
			TimeoutRate = Math.floor(10000*CV_HISTORY_PROGRESS_DATA.TIMEOUT_COUNT/CV_HISTORY_PROGRESS_DATA.ALL_COUNT)/100;
			ConfirmRate = Math.floor(10000*CV_HISTORY_PROGRESS_DATA.CONFIRM_COUNT/CV_HISTORY_PROGRESS_DATA.ALL_COUNT)/100;
			HandleRate = Math.floor(10000*CV_HISTORY_PROGRESS_DATA.HANDLE_COUNT/CV_HISTORY_PROGRESS_DATA.ALL_COUNT)/100;
		}
		
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>(' + CV_HISTORY_PROGRESS_DATA.CONFIRM_INTIME_COUNT + '/' + CV_HISTORY_PROGRESS_DATA.ALL_COUNT + ') 及时确认率</h3>' +
				'<div class="layui-progress-bar layui-bg-blue" lay-percent="' + ConfirmInTimeRate + '%"></div>' +
			'</div>'
		);
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>(' + CV_HISTORY_PROGRESS_DATA.TIMEOUT_COUNT + '/' + CV_HISTORY_PROGRESS_DATA.ALL_COUNT + ') 超时率</h3>' +
				'<div class="layui-progress-bar layui-bg-red" lay-percent="' + TimeoutRate +'%"></div>' +
			'</div>'
		);
		html.push('<hr class="layui-bg-gray" style="margin-top:-40px;">');
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>(' + CV_HISTORY_PROGRESS_DATA.CONFIRM_COUNT + '/' + CV_HISTORY_PROGRESS_DATA.ALL_COUNT + ')确认率</h3>' +
				'<div class="layui-progress-bar layui-bg-blue" lay-percent="' + ConfirmRate + '%"></div>' +
			'</div>'
		);
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>(' + CV_HISTORY_PROGRESS_DATA.HANDLE_COUNT + '/' + CV_HISTORY_PROGRESS_DATA.ALL_COUNT + ') 处理率</h3>' +
				'<div class="layui-progress-bar" lay-percent="' + HandleRate + '%"></div>' +
			'</div>'
		);
		
		$("#cv_history_progress").html(html.join(""));
		element.render('progress');
	};
	
	//初始化开单科室下拉框
	function initRecDeptSelectHtml(callback){
		//获取开单科室列表
		getDeptList(function(list){
			var html = [];
			html.push('<select name="RecDept" id="RecDept" lay-filter="RecDept">')
			html.push('<option value="">选择开单科室</option>');
			
			for(var i in list){
				html.push('<option value="' + list[i].HRDeptIdentity_HRDept_Id + '">' + list[i].HRDeptIdentity_HRDept_CName + '</option>');
			}
			html.push('</select>');
			
			$("#RecDept_Div").html(html.join(""));
			form.render('select');
			
			callback && callback();
		});
	};
	//获取开单科室列表
	function getDeptList(callback){
		uxutil.server.ajax({
			url:GET_DEPT_LIST_URL,
			data:{
				isPlanish:true,
				fields:"HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName",
				where:"hrdeptidentity.TSysCode='1001101' and hrdeptidentity.SystemCode='ZF_LAB_START'"
			}
		},function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//初始化页面
	function initHtml(){
		//初始化历史数据列表
		initCVTableHistory();
		//初始化开单科室下拉框
		initRecDeptSelectHtml(function(){
			//默认查询
			onSearch();
		});
	};
	initHtml();
});

