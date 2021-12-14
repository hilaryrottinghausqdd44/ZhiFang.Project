layui.extend({
	uxutil:'ux/util',
	uxdata:'ux/data',
	uxtable:'ux/table',
	echarts:'ux/other/echarts'
}).use(['uxutil','uxdata','uxtable','echarts','layer','element'],function(){
	var $=layui.$,
		uxutil = layui.uxutil,
		uxdata = layui.uxdata,
		uxtable = layui.uxtable,
		echarts = layui.echarts,
		element = layui.element;
		
	//获取消息列表地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL";
	//获取的消息字段数组
	var MSG_FIELDS = ["Id","MsgContent","MsgTypeCode","SenderID","SenderName","ConfirmFlag","HandleFlag","RequireConfirmTime"];
	//消息类型编码
	var MSG_TYPE_CODE = 'ZF_LAB_START_CV';
	//消息数据
	var MSG_DATA = new uxdata.Map();
		
	//数据变化监听
	MSG_DATA.listeners.change = function(map,eventName,value,hasKey){
		uxutil.action.delay(function(){
			//危急值列表变化
			changeGrid();
			//危急值饼图变化
			changePie();
		});
	};
	//危急值列表
	var CV_TABLE_NOW = null;
	//初始化当前处理列表
	function initCVTableNow(){
		var config = {
			elem:$("#cv_table_now"),
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
				
				{field:'ConfirmFlag',width:100,title:'状态',templet:function(d){
					var result = '';
					if(d.HandleFlag == '0' && d.isTimeout){
						if(d.isTimeout){
							result = '<span style="color:#FF5722;">已超时</span>';
						}else{
							result = '<span style="color:#1E9FFF;">未确认</span>';
						}
					}else if(d.ConfirmFlag == '1'){
						result = '<span style="color:#393D49;">待处理</span>';
					}
					return result;
				}},
				{field:'DataAddTime',width:160,title:'产生时间',sort:true},
				{field:'WarningUpLoadDateTime',width:160,title:'上报时间',sort:true},
				{field:'SendSectionName',minWidth:100,title:'检验小组',sort:true},
				{field:'Id',title:'ID',width:180,hide:true}
			]]
		};
		CV_TABLE_NOW = uxtable.render(config);
		
		//监听行双击事件
		CV_TABLE_NOW.table.on('rowDouble(cv_table_now)', function(obj){
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
	//初始化历史数据列表
	function initCVTableHistory(){
		var url = GET_MSG_LIST_URL + "?fields=SCMsg_" + MSG_FIELDS.join(",SCMsg_") + "&where=scmsg.MsgTypeCode='ZF_LAB_START_CV'";
		var config = {
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
				
				{field:'ConfirmFlag',width:100,title:'状态',templet:function(d){
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
				{field:'WarningUpLoadDateTime',width:160,title:'上报时间',sort:true},
				{field:'SendSectionName',minWidth:100,title:'检验小组',sort:true},
				{field:'Id',title:'ID',width:180,hide:true}
			]],
			url:url,
			changeData:function(data){
				return changeData(data);
			}
		};
		var cvTableHistory = uxtable.render(config);
		
		//监听行双击事件
		cvTableHistory.table.on('rowDouble(cv_table_history)', function(obj){
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
	//危急值饼图变化
	function changePie(){
		var serverDateTimes = uxutil.server.date.getTimes(),
			values = MSG_DATA.values(),
			outTime = 0,
			toConfirm = 0,
			toHandle = 0;
			
		for(var i in values){
			if(values[i].ConfirmFlag == "1"){//已确认，待处理
				toHandle++;
			}else{
				if(!values[i].RequireConfirmTime){//没有要求确认时间的不做超时判断，直接为待确认
					toConfirm++;
				}else{
					var times = new Date(values[i].RequireConfirmTime).getTime();
					if(times < serverDateTimes){//已超时
						outTime++;
					}else{//未超时，待确认
						toConfirm++;
					}
				}
			}
		}
		
		var data = [
			{value:outTime, name:'已超时'},
			{value:toConfirm, name:'待确认'},
			{value:toHandle, name:'待处理'}
		];
		
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
				data:['已超时','待确认','待处理']
			},
			series : [{
				name:'访问来源',
				type:'pie',
				radius : '55%',
				center: ['50%', '50%'],
				label:{ 
					show:true, 
					formatter:'{b} : {c} ({d}%)' 
				}, 
				labelLine:{show:true},
				data:data
			}]
		};
		
		var pie = echarts.init($("#cv_charts_pie")[0],layui.echartsTheme);
		pie.setOption(options);
		
		//页面大小变化处理
		$(window).on("resize",function(){
			pie.resize();
		});
	};
	//危急值列表变化
	function changeGrid(){
		var values = MSG_DATA.values(),
			data = [];
			
		for(var i in values){
			data.push(values[i]);
		}
		CV_TABLE_NOW.reload({data:data});
	};
	//注册消息业务
	function onRegisterMsg(callback){
		if(top.layui.msgintegrator){
			//注册危急值消息业务
			top.layui.msgintegrator.register({
				"name":"labstar.ui.layui.views.msg.manage.cv.index.html?t" + new Date().getTime(),
				"codes":[MSG_TYPE_CODE],
				fun:function(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode,ZFSCMsgStatus){
					var where = "scmsg.Id=" + SCMsgId;
					onLoadMsgs(where,function(list){
						insertMsgData(list[0]);
					});
				}
			});
		}else{
			layer.msg("请引入消息集成器，否则无法实时同步消息！");
		}
		if(callback){callback();}
	};
	//加载未处理完毕危急值信息列表
	function onLoadUnHandleMsgs(callback){
		var where = "scmsg.MsgTypeCode='ZF_LAB_START_CV' and scmsg.HandleFlag=0";
		onLoadMsgs(where,function(data){
			callback(data);
		});
	};
	//加载危急值信息列表
	function onLoadMsgs(where,callback){
		var url = GET_MSG_LIST_URL + "?fields=SCMsg_" + MSG_FIELDS.join(",SCMsg_");
		if(where){url += '&where=' + where;}
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//添加危急值消息数据
	function insertMsgData(msg){
		var list = $.isArray(msg) ? msg : [msg];
		for(var i in list){
			list[i] = changeData(list[i]);
			if(list[i].HandleFlag == '1'){
				MSG_DATA.del(list[i].Id);
			}else{
				MSG_DATA.set(list[i].Id,list[i]);
			}
		}
	};
	//数据处理
	function changeData(data){
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
	//初始化历史处理情况
	function initHistoryProgress(){
		var url = GET_MSG_LIST_URL + "?fields=SCMsg_Id&where=scmsg.MsgTypeCode='ZF_LAB_START_CV'&page=1&limit=1";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				//initHistoryProgressHtml((data.value || {}));
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化历史处理情况HTML
	function initHistoryProgressHtml(value){
		var html = [];
		
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>及时确认率</h3>' +
				'<div class="layui-progress-bar layui-bg-blue" lay-percent="58%"></div>' +
			'</div>'
		);
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>完成率</h3>' +
				'<div class="layui-progress-bar" lay-percent="90%"></div>' +
			'</div>'
		);
		html.push(
			'<div class="layui-progress" lay-showPercent="yes">' +
				'<h3>超时率</h3>' +
				'<div class="layui-progress-bar layui-bg-red" lay-percent="1%"></div>' +
			'</div>'
		);
		
		$("#history_progress").html(html.join(""));
		element.render('progress');
	};
	
	//初始化页面
	function initHtml(){
		//注册消息业务
		onRegisterMsg(function(){
			//加载未处理完毕危急值信息列表
			onLoadUnHandleMsgs(function(list){
				//添加危急值消息数据
				insertMsgData(list);
				//初始化当前处理列表
				initCVTableNow();
				//初始化历史数据列表
				initCVTableHistory();
				//初始化历史处理情况
				//initHistoryProgress();
				initHistoryProgressHtml();
			});
		});
	};
	initHtml();
});