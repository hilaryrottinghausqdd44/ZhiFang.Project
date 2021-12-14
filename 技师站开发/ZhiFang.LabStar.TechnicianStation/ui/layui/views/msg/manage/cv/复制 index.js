layui.extend({
	uxutil:'ux/util',
	uxdata:'ux/data',
	uxtable:'ux/table',
	echarts:'ux/other/echarts'
}).use(['uxutil','uxdata','uxtable','echarts','layer','carousel'],function(){
	var $=layui.$,
		uxutil = layui.uxutil,
		uxdata = layui.uxdata,
		uxtable = layui.uxtable,
		echarts = layui.echarts,
		uxtable = layui.uxtable,
		carousel = layui.carousel,
		device = layui.device();
		
	//获取消息列表地址
	var GET_MSG_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL";
	//获取的消息字段数组
	var MSG_FIELDS = ["Id","MsgTypeCode","SenderID","SenderName","ConfirmFlag","HandleFlag","RequireConfirmTime"];
	//消息类型编码
	var MSG_TYPE_CODE = 'ZF_LAB_START_CV';
	//消息数据
	var MSG_DATA = new uxdata.Map();
		
	//数据变化监听
	MSG_DATA.listeners.change = function(map,eventName,value,hasKey){
		uxutil.action.delay(function(){
			//危急值饼图变化
			changePie();
		});
	};
	
	//轮播切换
	$('.layui-carousel').each(function(){
		var othis = $(this);
		carousel.render({
			elem:this,
			width:'100%',
			arrow:'none',
			interval:othis.data('interval'),
			autoplay:othis.data('autoplay') === true,
			trigger:(device.ios || device.android) ? 'click' : 'hover',
			anim: othis.data('anim')
		});
	});
	//轮播变化监听
	carousel.on('change(cv_charts)',function(obj){
		var index = obj.index;
		if(index == 0){
			changePie();
		}else if(index == 1){
			changeGrid();
		}
	});
	
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
				orient : 'vertical',
				x : 'left',
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
		
		var pie = echarts.init($("#cv_charts").children('div')[0],layui.echartsTheme);
		pie.setOption(options);
		
		//页面大小变化处理
		$(window).on("resize",function(){
			pie.resize();
		});
	};
	//危急值列表
	function changeGrid(){
		var values = MSG_DATA.values(),
			data = [];
			
		for(var i in values){
			data.push(values[i]);
		}
			
		var config = {
			elem:$("#cv_table"),
			width:'100%',
			height:'280px',
			cols:[[
				{field:'SendSectionName',width:90,title:'小组'},
				{field:'SenderName',width:90,title:'发送者'},
				{field:'RecDeptName',width:100,title:'开单科室',sort:true},
				{field:'DoctorName',width:100,title:'开单医生',sort:true},
				{field:'RecSickTypeName',width:90,title:'就诊类型'},
				{field:'PatientName',width:90,title:'病人姓名'},
				{field:'PatientSex',width:60,title:'性别'},
				{field:'PatientAge',width:60,title:'年龄'},
				{field:'PatNo',width:100,title:'病历号'},
				{field:'SampleNo',width:100,title:'样本号',sort:true},
				{field:'MsgAll',minWidth:200,title:'危急值消息内容'},
				
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
				{field:'Id',title:'ID',width:180,hide:true}
			]],
			data:data
		};
		var tableInd = layui.table.render(config);
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
			if(list[i].HandleFlag == '1'){
				MSG_DATA.del(list[i].Id);
			}else{
				MSG_DATA.set(list[i].Id,list[i]);
			}
		}
	};
	//初始化页面
	function initHtml(){
		//注册消息业务
		onRegisterMsg(function(){
			//加载未处理完毕危急值信息列表
			onLoadUnHandleMsgs(function(list){
				//添加危急值消息数据
				insertMsgData(list);
			});
		});
	};
	initHtml();
});