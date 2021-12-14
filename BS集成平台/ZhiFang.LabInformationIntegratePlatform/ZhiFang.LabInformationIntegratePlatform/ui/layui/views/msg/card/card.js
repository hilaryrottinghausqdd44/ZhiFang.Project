layui.extend({
	uxutil:'ux/util',
	uxdata:'ux/data',
	uxaudio:'ux/audio'
}).define(['uxutil','uxdata','uxaudio','layer'],function(exports){
	var $=layui.$,
		uxutil = layui.uxutil,
		uxdata = layui.uxdata,
		uxaudio = layui.uxaudio,
		topLayer = layer;
		
	try{
		topLayer = top.layer;
	}catch(e){
		//console.log(e);
	}
	
	//获取危急值员工部门列表
	var GET_EMP_DEPT_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL";
	//获取消息列表
	var GET_MSG_LIST_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL";
	//获取消息类型列表
	var GET_MSG_TYPE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL";
	//获取系统列表
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL";
	//消息声音文件地址
	var MSG_AUDIO_URL = uxutil.path.LAYUI + "/audio/msg.wav";
	
	//获取的消息字段数组
	var MSG_FIELDS = [
		"Id","MsgContent","MsgTypeCode","SenderID","SenderName",
		"ConfirmFlag","HandleFlag","RequireConfirmTime"
	];
	//闪烁样式
	var FLICKER_CLASS = 'flicker';
	
	//小块MAP
	var DIV_MAP = {
		//待处理
		ToDo:{
			"text":"待处理","color":"#ffffff","bgcolor":"orange",
			"data":new uxdata.Map(),"win":null
		},
		//超时
		OutTime:{
			"text":"超时","color":"#ffffff","bgcolor":"red",
			"data":new uxdata.Map(),"win":null
		}
	};
	
	//初始化声音组件
	var thatUxaudio = uxaudio.render({url:MSG_AUDIO_URL});
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//存在标题
	var HASTITLE = PARAMS.HASTITLE;
	//消息类型编码串
	var TYPECODES = (PARAMS.MSGTYPECODELIST || '').split(',');
	//系统列表
	var SYSTEM_LIST = null;
	//消息类型列表
	var MSG_TYPE_LIST = null;
	//员工主职和挂职部门ID
	var DEPT_IDS = null;
	
	//每块高度
	var CARD_DIV_HEIGHT = HASTITLE ? ($(window).height() - 43)/2 : $(window).height()/2;
	//卡片内容模板
	var _CARD_CONTENT_ROW_TEMPLET = [
		'<div cardtype="msg_card" class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12" code="{code}"',
		' style="border:1px solid #ffffff;padding:6px;text-align:center;width:100%;height:' + CARD_DIV_HEIGHT + 'px;',
		'color:{color};background-color:{bgcolor};" title="双击查看详细信息">',
			'<div class="{flickerClass}" cardtype="msg_text">{text}</div>',
			'<div class="{flickerClass}" cardtype="msg_count">{count}</div>',
		'</div>'
	];
	
	//显示卡片内容
	function showCardContent(){
		//清空数据
		clearData();
		//开始建立通信连接
		initConnection(function(){});
	};
	//清空数据
	function clearData(){
		DIV_MAP.ToDo.data.clear();
		DIV_MAP.OutTime.data.clear();
	};
	//开始建立通信连接
	function initConnection(callback){
		var jq  = jQuery,
			chat = jq.connection.mainMessageHub;
			
		//服务器返回危急值信息处理
		chat.client.receiveMessageByEmpId_CV = function(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode){
			var where = "scmsg.Id=" + SCMsgId;
			onLoadMsgs(where,function(list){
				thatUxaudio.play();
				insetData(list[0]);
			});
		}
		//开始连接
		jq.connection.hub.start().done(function(){
			//连接成功
			//清空数据
			clearData();
			//初始化未读信息列表
			initUnShowMsgs();
		});
		//连接断开重连
		jq.connection.hub.disconnected(function(){
			topLayer.alert("即时通讯连接失败，请点击重连！",{
				btn:"重连"
			},function(index){
				topLayer.close(index);
				jq.connection.hub.start();
			});
		});
		callback();
	};
	//添加消息到内存
	function insetData(info){
		var serverDateTimes = uxutil.server.date.getTimes(),
			times = new Date(info.RequireConfirmTime).getTime();
			
		//已处理
		if(info.HandleFlag == "1"){
			//待处理删除
			DIV_MAP.ToDo.data.delete(info.Id);
			//超时增加
			DIV_MAP.OutTime.data.delete(info.Id);
		}else{
			if(times < serverDateTimes){
				//超时增加
				DIV_MAP.OutTime.data.set(info.Id,info);
				//待处理删除
				DIV_MAP.ToDo.data.delete(info.Id);
			}else{
				//待处理增加
				DIV_MAP.ToDo.data.set(info.Id,info);
				//超时删除
				DIV_MAP.OutTime.data.delete(info.Id);
			}
		}
		
		//卡片内容变化
		changeCardInfo();
	};
	//初始化未读信息列表
	function initUnShowMsgs(){
		//清空数据
		clearData();
		
		//获取危急值员工部门列表
		loadDeptIds(function(){
			//获取未读的消息列表
			loadUnShowMsgs(function(){
				//初始化消息卡片内容
				initMsgCardContentHtml();
			});
		});
		
	};
	//卡片内容变化
	function changeCardInfo(){
		//待处理+超时样式变更
		var view = $("body"),
			card = view.find("div[itemid='card']"),
			ToDoCountDiv = $(card).find("div[code='ToDo']").find("div[cardtype='msg_count']"),
			OutTimeCountDiv = $(card).find("div[code='OutTime']").find("div[cardtype='msg_count']"),
			toDoData = DIV_MAP.ToDo.data,
			outTimeData = DIV_MAP.OutTime.data;
			
		ToDoCountDiv.html(toDoData.size);
		OutTimeCountDiv.html(outTimeData.size);
		if(toDoData.size == 0){
			ToDoCountDiv.removeClass(FLICKER_CLASS);
		}
		if(outTimeData.size == 0){
			OutTimeCountDiv.removeClass(FLICKER_CLASS);
		}
	};
	//初始化消息卡片内容
	function initMsgCardContentHtml(){
		var view = $("body"),
			card = view.find("div[itemid='card']"),
			html = [];
		
		//清空卡片内容
		card.children().remove();
		
		for(var i in DIV_MAP){
			var flickerClass = DIV_MAP[i].data.size ? FLICKER_CLASS : "";
			var content = _CARD_CONTENT_ROW_TEMPLET.join('')
				.replace(/{code}/g,i)
				.replace(/{color}/g,DIV_MAP[i].color)
				.replace(/{bgcolor}/g,DIV_MAP[i].bgcolor)
				.replace(/{flickerClass}/g,flickerClass)
				.replace(/{text}/g,DIV_MAP[i].text)
				.replace(/{count}/g,DIV_MAP[i].data.size);
			html.push(content);
		}
		card.html(html.join(''));
		
		var rows = $(card).find("div[cardtype='msg_card']");
		rows.each(function(index,e){
			var dom = $(this);
			dom.on('mousedown', function(e){
				if(e.button == 0 && e.detail >= 2){//左键+双击
					dom.dblclick();
				}
			});
			dom.on("dblclick",function(){
				uxutil.action.delay(function(){
					//显示该类型消息具体内容
					openInfoWin(dom);
				},100);
			});
		});
	};
	//显示该类型消息具体内容
	function openInfoWin(dom){
		var code = $(dom).attr('code'),
			status = code == 'OutTime' ? '1' : '4',
			win = $(window),
			maxWidth = win.width(),
			maxHeight = win.height();
		
		var url = uxutil.path.LIIP_ROOT + '/ui/layui/views/msg/card/list.html?typeCodes=' +
			TYPECODES.join(',') + '&status=' + status + '&t=' + new Date().getTime();
			
		//var width = maxWidth > 1461 ? 1461 : maxWidth;
		//var height = maxHeight > 620 ? 620 : maxHeight;
		topLayer.open({
			title:'消息列表',
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%']
		});
	};
	//数据变化监听
	function dataListeners(){
		var toDoData = DIV_MAP.ToDo.data,
			outTimeData = DIV_MAP.OutTime.data;
			
		//map：字典对象；
		//eventName：动作名称；set/delete/clear
		//value：value/key值；set(Value添加的数据)，delete(key删除的主键)
		//hasKey：是否存在主键
		
		//数据变化监听
		toDoData.listeners.change = function(map,eventName,value,hasKey){
			if(eventName == 'set'){//添加数据
				
			}else if(eventName == 'delete'){//删除数据
				changeCardInfo();
			}else if(eventName == 'clear'){//清空数据
				changeCardInfo();
			}
		};
		//数据变化监听
		outTimeData.listeners.change = function(map,eventName,value,hasKey){
			if(eventName == 'set'){//添加数据
				
			}else if(eventName == 'delete'){//删除数据
				changeCardInfo();
			}else if(eventName == 'clear'){//清空数据
				changeCardInfo();
			}
		};
		//超时监听
		TimeoutListeners();
	};
	//超时监听
	function TimeoutListeners(){
		var toDoData = DIV_MAP.ToDo.data,
			outTimeData = DIV_MAP.OutTime.data;
			
		function isTimeout(){
			var list = toDoData.values(),
				serverDateTimes = uxutil.server.date.getTimes(),
				times = null
			for(var i in list){
				times = new Date(list[i].RequireConfirmTime).getTime();
				if(times < serverDateTimes){
					//超时增加
					outTimeData.set(list[i].Id,list[i]);
					//待处理删除
					toDoData.delete(list[i].Id);
				}
			}
			setTimeout(function(){
				isTimeout();
			},1000);
		}
		isTimeout();
	};
	
	//获取危急值员工部门列表
	function loadDeptIds(callback){
		var url = GET_EMP_DEPT_URL,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID),
			deptId = uxutil.cookie.get(uxutil.cookie.map.DEPTID),
			deptIds = [deptId];
			
		url += '?fields=CVCriticalValueEmpIdDeptLink_DeptID' +
			'&where=cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=' + empId;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || []
				for(var i in list){
					if(list[i].DeptID != deptId){
						deptIds.push(list[i].DeptID);
					}
				}
			}
			DEPT_IDS = deptIds;
			callback();
		});
	};
	//获取未读的消息列表
	function loadUnShowMsgs(callback){
		var empId = uxutil.cookie.get(uxutil.cookie.map.USERID),
			deptIds = DEPT_IDS,
			typeCodes = TYPECODES,
			len = typeCodes.length,
			typeWhere = [];
			
		for(var i=0;i<len;i++){
			typeWhere.push("scmsg.MsgTypeCode='" + typeCodes[i].Code + "'");
		}
			
		//条件=使用+消息类型编码+接收科室
		var where = "scmsg.IsUse=1 and scmsg.HandleFlag='0'" + 
			" and (" + typeWhere.join(' or ') + ')' +
			" and (scmsg.RecDeptID in (" + deptIds.join(',') + ") or scmsg.SenderID=" + empId + ")";
			
		onLoadMsgs(where,function(list){
			for(var i in list){
				insetData(list[i]);
			}
			callback();
		});
	};
	//获取未读的消息列表
	function onLoadMsgs(where,callback){
		var url = GET_MSG_LIST_URL;
			
		//条件=使用+所属系统代码+接收科室
		url += "?fields=SCMsg_" + MSG_FIELDS.join(",SCMsg_") + "&where=" + where;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [];
				callback(list);
			}else{
				topLayer.msg(data.msg);
			}
		});
	};
	
	//获取系统列表
	function loadSystemList(callback){
		var url = GET_SYSTEM_LIST_URL,
			fields = ['Id','SystemName','SystemCode','SystemHost'];
			
		url += '?fields=IntergrateSystemSet_' + fields.join(',IntergrateSystemSet_');
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				SYSTEM_LIST = data.value.list || [];
				callback();
			}else{
				topLayer.msg(data.msg);
			}
		});
	};
	//获取消息类型
	function loadMsgTypeList(callback){
		var url = GET_MSG_TYPE_LIST_URL,
			fields = ['Id','CName','Code','Url','SystemID'],
			codeWhere = [];
			
		for(var i in TYPECODES){
			codeWhere.push("scmsgtype.Code='" + TYPECODES[i] + "'");
		}
			
		url += '?fields=SCMsgType_' + fields.join(',SCMsgType_') + '&where=' + codeWhere.join(' or ');
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				MSG_TYPE_LIST = data.value.list || [];
				callback();
			}else{
				topLayer.msg(data.msg);
			}
		});
	};
	//初始化
	function init(){
		//加载系统列表
		loadSystemList(function(){
			//加载消息类型
			loadMsgTypeList(function(){
				//显示卡片内容
				showCardContent();
				//数据变化监听
				dataListeners();
			});
		});
	};
	//初始化
	init();
});