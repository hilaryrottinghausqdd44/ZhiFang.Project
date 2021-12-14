/**
 * 底部信息
 * @author GHX
 * @version 2021-05-20
 */
layui.extend({
}).define(['uxutil'], function (exports) {
	"use strict";

	var $ = layui.jquery,
		uxutil = layui.uxutil;
	/**
	 * 当前检验者 审核者 只有在检验确认设置、审核设置界面设置保存后才会有 如果预授权时间已过 也是不显示的 只有再次设置才会存在
	 * */
	var app = {};
	//初始化
	app.init = function () {    
		var me = this;
		me.onConfigChange();
		me.updateSampleTotal(0);
		me.updateItemTotal(0);
		me.updateHistoryTotal(0);
	};
	//设置修改后处理
	app.onConfigChange=function(){
		var me = this;
		var local = uxutil.localStorage.get("LabStar_TS", true),
			userid = uxutil.cookie.get(uxutil.cookie.map.USERID),
			hisok = false,
			cisok = false;
			
		if (local && local[userid] && local[userid]['CheckerInfo']) {
			var CheckerInfo = local[userid]['CheckerInfo'];
			if(CheckerInfo["OperaterType"] == "0" && CheckerInfo['Id']){
				me.updateCheckName(CheckerInfo.Name);
			}else{
				if(CheckerInfo['Id'] && CheckerInfo.BeginTime && CheckerInfo.EndTime){
					var presentdatetime = new Date().getTime(),
						bdt= new Date(CheckerInfo.BeginTime).getTime(),
						edt= new Date(CheckerInfo.EndTime).getTime();
					if(bdt <= presentdatetime && presentdatetime <= edt){
						me.updateCheckName(CheckerInfo.Name);
					}else{
						cisok = true;
					}
				}else{
					cisok = true;
				}
			}
		}else{
			cisok = true;
		}
		if(cisok){
			me.updateCheckName("");
		}
		if (local && local[userid] && local[userid]['HandlerInfo']) {
			var HandlerInfo = local[userid]['HandlerInfo'];
			if(HandlerInfo["OperaterType"] == "0" && HandlerInfo['Id']){
					me.updateUserName(HandlerInfo.Name);
			}else{
				if(HandlerInfo['Id'] && HandlerInfo.BeginTime && HandlerInfo.EndTime){
					var presentdatetime = new Date().getTime(),
						bdt= new Date(HandlerInfo.BeginTime).getTime(),
						edt= new Date(HandlerInfo.EndTime).getTime();
					if(bdt <= presentdatetime && presentdatetime <= edt){
						me.updateUserName(HandlerInfo.Name);
					}else{
						hisok = true;
					}
				}else{
						hisok = true;
				}
			}
		}else{
			hisok = true;
		}	
		if(hisok){
			me.updateUserName("");
		}
	};
	//更改显示-当前检验者
	app.updateUserName=function(value){
		var me = this;
		$("#bottom_username").attr("title", value).html("当前检验者："+value);
	};
	//更改显示-当前审核者
	app.updateCheckName=function(value){
		var me = this;
		$("#bottom_CheckName").attr("title", value).html("当前审核者："+value);
	};
	//更改显示-样本总数
	app.updateSampleTotal=function(value){
		var me = this;
		$("#bottom_SampleTotal").attr("title", value).html("样本总数："+value);
	};
	//更改显示-项目总数
	app.updateItemTotal=function(value){
		var me = this;
		$("#bottom_ItemTotal").attr("title", value).html("项目总数："+value);
	};
	//更改显示-历史例数
	app.updateHistoryTotal=function(value){
		var me = this;
		$("#bottom_HistoryTotal").attr("title", value).html("历史例数："+value);
	};
	//暴露接口
	exports('bottomToolBar', app);
});
