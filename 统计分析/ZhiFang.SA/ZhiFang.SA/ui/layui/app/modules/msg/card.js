/**
	@name：layui.ux.modules.msg.card 消息卡片
	@author：Jcall
	@version 2019-05-06
 */
layui.extend({
	uxutil:'ux/util',
	commonzf:'app/modules/common/zf',
	msglist:'app/modules/msg/list'
}).define(['uxutil','layer','commonzf','msglist'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		commonzf = layui.commonzf,
		msglist = layui.msglist;
	
	//卡片初始模板
	var _CARD_INIT_TEMPLET = 
	'<div itemid="card" class="mine-move" style="background-color:unset;border-bottom:unset;padding:0;">' +
		'<div class="layui-layer-loading" style="padding:{padding}">' +
			'<div class="layui-layer-loading0"></div>' +
		'</div>' +
	'</div>';
	//卡片内容模板
	var _CARD_CONTENT_ROW_TEMPLET = 
	'<div cardtype="msg_card" class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12" code="{code}"' +
	' style="border:1px solid #ffffff;padding:{padding};text-align:center;width:{width};height:{height};' +
	'color:{color};background-color:{bgcolor};" title="双击查看详细信息">' +
		'<div class="{flickerClass}" cardtype="msg_text">{text}</div>' +
		'<div class="{flickerClass}" cardtype="msg_count">{count}</div>' +
	'</div>';
	//连接次数
	var connectionCount = 0;
	
	//外部接口
	var msgcard = {
		//全局项
		config:{
			//标题
			title:false,//'危急值消息',
			//默认位置
			offset:'rt',
			//宽度高度
			area:['100px','100px'],
			//单块宽度
			cardWidth:100,
			//单块高度
			cardHeight:50,
			loadingWidth:60,
			loadingHeight:24,
			isPage:false
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,msgcard.config,setings);
	};
	
	Class.pt = Class.prototype;
	
	//默认配置
	Class.pt.config = {
		//遮罩
		shade:0,
		//关闭按钮
		closeBtn:0,
		//类型
		type:1,
		//是否允许拉伸
		resize:false,
		//触发拖动的元素
		move:'.mine-move',
		//系统编码
		SystemCode:'ZF_LabStar',
		//获取危急值员工部门列表
		getEmpDeptUrl:uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL',
		//获取未读的消息列表
		getUnShowMsgUrl:uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgByHQL',
		//获取消息类型列表
		getMsgTypeListUrl:uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL',
		//元素闪烁样式名
		flickerClass:"flicker",
		//消息类型
		msgTypeMap:{
			"ToDo":{"count":0,"list":[],"text":"待处理","color":"#ffffff","bgcolor":"orange"},
			"OutTime":{"count":0,"list":[],"text":"超时","color":"#ffffff","bgcolor":"red"}
		},
		//实时通信基础文件
		signalrFiles:[
			uxutil.path.LAYUI + '/css/style.css',
			uxutil.path.UI + '/src/jquery-1.11.1.min.js',
			uxutil.path.UI + '/src/jquery.signalR-2.1.2.js',
			uxutil.path.ROOT + '/signalr/hubs'
		],
		//员工ID
		empId:uxutil.cookie.get(uxutil.cookie.map.USERID),
		//危急值部门列表
		deptIds:[]
	};
	
	//加载实时通信基础文件
	Class.pt.loadSignalrFiles = function(callback,index){
		var me = this,
			index = index || 0,
			signalrFiles = me.config.signalrFiles || [],
			len = signalrFiles.length;
		
		if(len > 0){
			if(index >= len){
				callback();
			}else{
				var url = signalrFiles[index];
				var att = url.split('.');
				//地址后缀
				var ext = att[att.length - 1].toLowerCase();
				if(ext == "css"){
					$('head').append('<link href="' + url + '" rel="stylesheet" type="text/css"/>');
					me.loadSignalrFiles(callback,++index);
				}else{
					$.getScript(url,function(){
						me.loadSignalrFiles(callback,++index);
					});
				}
			}
		}else{
			callback();
		}
	};
	
	//显示小卡片
	Class.pt.showCard = function(options){
		var me = this,
			win = $(window),
			width = parseFloat(me.config.area[0]),
			height = parseFloat(me.config.area[1]),
			loadingPaddingLeft = (width - me.config.loadingWidth)/2,
			loadingPaddingTop = (height - me.config.loadingHeight)/2;
		
		var padding = loadingPaddingTop + 'px ' + loadingPaddingLeft + 'px;';
		me.config.content = _CARD_INIT_TEMPLET.replace(/{padding}/g,padding);
		
		//获取扩展定位属性值
		var offsetP = me.getOffsetP();
		//存在扩展定位属性设置
		if(offsetP.length == 2){
			me.config.offset = [offsetP[0],offsetP[1]];
		}
		
		//页面方式
		if(me.config.isPage){
			$("body").append(me.config.content);
			return;
		}
		
		//弹出层
//		me.config.zIndex = layer.zIndex;//重点1
//		me.config.success = function(layero){
//			layer.setTop(layero);//重点2
//		}
		var index = layer.open(me.config);
		me.config.index = index;
		//存在扩展定位属性设置
		if(offsetP.length == 2){
			win.on("resize",function(){
				var offsetP = me.getOffsetP();
				//重新定位卡片位置
				$("#layui-layer" + index).css({
					top:offsetP[0],
					left:offsetP[1]
				});
			});
		}
	};
	//获取扩展定位属性值
	Class.pt.getOffsetP = function(){
		var me = this,
			win = $(window),
			width = parseFloat(me.config.area[0]),
			height = parseFloat(me.config.area[1]),
			offsetP = [];
			
		if(me.config.offsetP && !$.isEmptyObject(me.config.offsetP)){
			//Top属性值
			if(me.config.offsetP.top || me.config.offsetP.top === 0){
				offsetP[0] = me.config.offsetP.top ;
			}else if(me.config.offsetP.bottom || me.config.offsetP.bottom === 0){
				offsetP[0] = win.height() - me.config.offsetP.bottom - height;
			}else{
				offsetP[0] = 0;
			}
			//Left属性值
			if(me.config.offsetP.left){
				offsetP[1] = me.config.offsetP.left;
			}else if(me.config.offsetP.right || me.config.offsetP.right === 0){
				offsetP[1] = win.width() - me.config.offsetP.right - width;
			}else{
				offsetP[1] = 0.000000001;
			}
			
			//如果定位后部分框不在可视范围内，则自动重新定位到可视范围
			if(win.height() - height < 0){
				offsetP[0] = 0;
			}else if(win.height() - height < me.config.offset[0]){
				offsetP[0] = win.height() - height;
			}
			if(win.width() - width < 0){
				offsetP[1] = 0.000000001;
			}else if(win.width() - width < me.config.offset[1]){
				offsetP[1] = win.width() - width;
			}
			
			//没有配置属性时采用默认位置
			if(offsetP.length == 2 && !offsetP[0] && !offsetP[1]){
				offsetP = [];
			}
		}
		
		return offsetP;
	};
	//显示卡片内容
	Class.pt.showCardContent = function(options){
		var me = this;
		//清空数据
		me.clearData();
		//开始建立通信连接
		me.initConnection(function(){
			
		});
	};
	//初始化未读信息列表
	Class.pt.initUnShowMsgs = function(){
		var me = this;
		//清空数据
		me.clearData();
		
		//获取危急值员工部门列表
		me.loadDeptIds(function(){
			//获取未读的消息列表
			me.loadUnShowMsgs(function(){
				//初始化消息卡片内容
				me.initMsgCardContentHtml();
			});
		});
		
	};
	//清空数据
	Class.pt.clearData = function(){
		var me = this;
		me.config.msgTypeMap.ToDo.count = 0;
		me.config.msgTypeMap.ToDo.list = [];
		me.config.msgTypeMap.OutTime.count = 0;
		me.config.msgTypeMap.OutTime.list = [];
	};
	//初始化消息卡片内容
	Class.pt.initMsgCardContentHtml = function(){
		var me = this,
			win = $(window),
			width = parseFloat(me.config.area[0]),
			height = parseFloat(me.config.area[1]),
			view = me.config.isPage ? $("body") : $("#layui-layer" + me.config.index),
			card = view.find("div[itemid='card']"),
			msgTypeMap = me.config.msgTypeMap,
			html = [];
		
		//清空卡片内容
		card.children().remove();
		
		for(var i in msgTypeMap){
			var flickerClass = msgTypeMap[i].count ? me.config.flickerClass : "";
			var content = _CARD_CONTENT_ROW_TEMPLET
				.replace(/{code}/g,i)
				.replace(/{padding}/g,'6' + 'px')
				.replace(/{width}/g,me.config.cardWidth + 'px')
				.replace(/{height}/g,me.config.cardHeight + 'px')
				.replace(/{color}/g,msgTypeMap[i].color)
				.replace(/{bgcolor}/g,msgTypeMap[i].bgcolor)
				.replace(/{flickerClass}/g,flickerClass)
				.replace(/{text}/g,msgTypeMap[i].text)
				.replace(/{count}/g,msgTypeMap[i].count);
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
				//显示该类型消息具体内容
				me.openInfoWin(dom);
			});
		});
	};
	//显示该类型消息具体内容
	Class.pt.openInfoWin = function(dom){
		var me = this,
			code = $(dom).attr("code"),
			title = $(dom).find("div[cardtype='msg_text']").text(),
			count = $(dom).find("div[cardtype='msg_count']").text();
			
		msglist.render({
			title:title,
			data:me.config.msgTypeMap[code].list || []
		});
	};
	
	//获取危急值员工部门列表
	Class.pt.loadDeptIds = function(callback){
		var me = this,
			url = me.config.getEmpDeptUrl,
			empId = me.config.empId,
			DEPTID = uxutil.cookie.get(uxutil.cookie.map.DEPTID),
			deptIds = [DEPTID];
			
		url += '?fields=CVCriticalValueEmpIdDeptLink_DeptID' +
			'&where=cvcriticalvalueempiddeptlink.IsUse=1 and cvcriticalvalueempiddeptlink.EmpID=' + empId;
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || []
				for(var i in list){
					if(list[i].DeptID != DEPTID){
						deptIds.push(list[i].DeptID);
					}
				}
			}
			me.config.deptIds = deptIds;
			callback();
		});
	};
	//获取未读的消息列表
	Class.pt.loadUnShowMsgs = function(callback){
		var me = this,
			url = me.config.getUnShowMsgUrl,
			empId = me.config.empId,
			deptIds = me.config.deptIds;
			
		//条件=使用+所属系统代码+接收科室+
		url += "?fields=SCMsg_Id,SCMsg_MsgContent,SCMsg_MsgTypeCode,SCMsg_SenderID,SCMsg_SenderName" +
			"&where=scmsg.IsUse=1 and scmsg.SystemCode='" + me.config.SystemCode + 
			"' and (scmsg.RecDeptID in (" + deptIds.join(',') + ") or scmsg.SenderID=" + empId + ")";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var list = data.value.list || [],
					arr = [];
				for(var i in list){
					arr.push(me.changeData(list[i]));
				}
				me.config.msgTypeMap.ToDo.count = arr.length;
				me.config.msgTypeMap.ToDo.list = arr;
			}
			callback();
		});
	};
	//开始建立通信连接
	Class.pt.initConnection = function(callback){
		var me = this,
			chat = jQuery.connection.mainMessageHub;
			
		//服务器返回危急值信息处理
		chat.client.receiveMessageByEmpId_CV = function(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode){
			me.addMsg(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode);
		}
		//开始连接
		jQuery.connection.hub.start().done(function(){
			//连接成功
			//初始化连接次数归0
			connectionCount = 0;
			//清空数据
			me.clearData();
			//初始化未读信息列表
			me.initUnShowMsgs();
		});
		//连接断开重连
		jQuery.connection.hub.disconnected(function(){
			connectionCount++;
			if(connectionCount > 10){
				layer.alert("即时通讯连接失败，请点击重连！",{
					btn:"重连"
				},function(index){
					layer.close(index);
					connectionCount = 0;
					jQuery.connection.hub.start();
				});
			}else{
				setTimeout(function(){
					jQuery.connection.hub.start();
				},500);
			}
		});
		callback();
	}
	//服务器返回信息处理
	Class.pt.addMsg = function(FormUserEmpId,FormUserEmpName,Message,SCMsgId,SCMsgTypeCode){
		var me = this,
			msgTypeMap = me.config.msgTypeMap,
			hasMsg = false;
			
		//1001危急值发送、1002危急值撤回、1003危急值上报、1004危急值处理
		//1005危机值处理撤销、1006危急值确认、1007危急值确认撤销
		switch (SCMsgTypeCode){
			case "1001":
				msgTypeMap.ToDo.count++;
				
				break;
			case "1002":
			case "1005":
			case "1007":
				break;
			case "1003":
				break;
			case "1006":
				break;
			case "1004":
				break;
			default:
				break;
		}
		
		for(var index in msgTypeMap){
			var list = msgTypeMap[index].list;
			//同一条消息，相同状态不处理，不同状态处理
			for(var i in list){
				if(list[i].SCMsgId == SCMsgId){
					hasMsg = true;
					break;
				}
			}
			if(hasMsg) break;
		}
		
		//不存在该条信息时添加
		if(!hasMsg){
			msgTypeMap.ToDo.count++;
			msgTypeMap.ToDo.list.push({
				Id:SCMsgId,
				MsgTypeCode:SCMsgTypeCode,
				SenderID:FormUserEmpId,
				SenderName:FormUserEmpName,
				MsgContent:Message
			});
			
			var view = me.config.isPage ? $("body") : $("#layui-layer" + me.config.index),
				card = view.find("div[itemid='card']"),
				ToDoCountDiv = $(card).find("div[code='ToDo']").find("div[cardtype='msg_count']");
				OutTimeCountDiv = $(card).find("div[code='OutTime']").find("div[cardtype='msg_count']");
				
			ToDoCountDiv.html(msgTypeMap.ToDo.count);
			OutTimeCountDiv.html(msgTypeMap.OutTime.count);
			if(msgTypeMap.ToDo.count == 0){
				ToDoCountDiv.removeClass(me.config.flickerClass);
			}
			if(msgTypeMap.OutTime.count == 0){
				OutTimeCountDiv.removeClass(me.config.flickerClass);
			}
		}
	}
	//数据处理
	Class.pt.changeData = function(data){
		var me = this,
			MsgContent = data.MsgContent;
		
		data.SendSectionName = "";//小组名称
		data.SampleNo = "";//样本号
		data.JzType = "";//就诊类型
		data.DeptName = "";//开单科室
		data.DoctorName = "";//开单医生
		data.PatNo = "";//病历号
		data.PatientName = "";//病人姓名
		data.PatientSex = "";//性别
		data.PatientAge = "";//年龄
		data.ItemName = "";//检查项目
		data.ResultValue = "";//结果值
		data.ResultStatus = "";//状态
		data.DataAddTime = "";//产生时间
		data.OutTimes = 0;//超时分钟
		
		return data;
	}
	
	//对外公开返回对象
	Class.pt.result = function(that){
		return {
			index:that.config.index
		}
	};
	
	//核心入口
	msgcard.render = function(options){
		var me = new Class(options);
		//显示小卡片
		me.showCard(options);
		//加载消息字典
		commonzf.classdict.init(['ZFSystem','ZFSCMsgType'],function(){
			//加载实时通信基础文件
			me.loadSignalrFiles(function(){
				//显示卡片内容
				me.showCardContent(options);
			});
		});
		return me.result(me);
	};
	
	//暴露接口
	exports('msgcard',msgcard);
});