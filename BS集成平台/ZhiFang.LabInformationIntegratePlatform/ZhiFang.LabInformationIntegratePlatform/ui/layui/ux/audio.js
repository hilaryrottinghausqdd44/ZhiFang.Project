/**
	@name：layui.ux.uxaudio 声音功能
	@author：Jcall
	@version 2019-03-25
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		CREATE_AUDIO_URL = uxutil.path.ROOT + "/ServerWCF/CommonService.svc/CreatVoice";
		
	var uxaudio = {
		//全局项
		config:{
			index:0,
			map:{}
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,uxaudio.config,setings);
	};
	Class.pt = Class.prototype;
	//初始化声音组件
	Class.pt.initAudio = function(url,tid,callback){
		var me = this;
		var audio = $('#audio-' + me.config.index);
		if(!audio[0]){
			$('body').append('<div id="audio-' + me.config.index + '"></div>');
			audio = $('#audio-' + me.config.index);
		}
		
		if(layui.device().ie){
			audio.html('<embed src="' + url + '" id="' + me.config.index +'" tid="' + tid + '" height="0" width="0" autostart="false"></embed>');
			callback && callback();
		}else{
			audio.html('<audio src="' + url + '" id="' + me.config.index +'" tid="' + tid + '" height="0" width="0"></audio>');
			callback && callback();
		}
	};
	//声音播放
	Class.pt.play = function(text){
		var me = this,
			map = me.config.map;
		
		map[new Date().getTime()] = text;
		
		var hasData = false;
		for(var i in map){
			hasData = true;
			break;
		}
		if(hasData){
			me.onstart();
		}
	};
	//声音播放开始
	Class.pt.onstart = function(){
		var me = this,
			map = me.config.map;
			
		//没有声音需要播报
		if($.isEmptyObject(map)){
			return;
		}
		
		var tid = '',text = '';
		for(var i in map){
			tid = i;
			text = map[i];
			break;
		}
		if(text){
			var url = encodeURI(CREATE_AUDIO_URL + '?txt=' + text);//中文转码
			uxutil.server.ajax({
				url:url
			},function(data){
				if(data.success){
					var url = uxutil.path.ROOT + '/' + data.value;
					me.initAudio(url,tid,function(){
						var audio = $("#" + me.config.index);
						if(layui.device().ie){
							setTimeout(function(){
								var tid = audio.attr('tid');
								me.onended(tid);
							},1000);
						}else{
							document.getElementById(me.config.index).onended = function(){
								var tid = audio.attr('tid');
								me.onended(tid);
							};
						}
						
						try{
							setTimeout(function(){
								audio[0].play();
							},500);
						}catch(e){
							//TODO handle the exception
						}
					});
				}
			});
		}
	};
	//播放完毕处理
	Class.pt.onended = function(tid){
		var me = this,
			map = me.config.map;
			
		if(map[tid]){
			delete map[tid];//播报完毕的删除
		}
		me.onstart();
	};
	//单个声音播放
	Class.pt.playone = function(text){
		var me = this;
		
		if(text){
			var url = encodeURI(CREATE_AUDIO_URL + '?txt=' + text);//中文转码
			uxutil.server.ajax({
				url:url
			},function(data){
				if(data.success){
					var url = uxutil.path.ROOT + '/' + data.value;
					var audioDiv = $('#audio-' + me.config.index);
					if(audioDiv[0]){
						var audio = $("#" + me.config.index);
						try{
							audio[0].pause();
						}catch(e){
							//TODO handle the exception
							console.error(e);
						}
						audioDiv.html('');
					}
					me.initAudio(url,'audio_playone',function(){
						var audio = $("#" + me.config.index);
						try{
							setTimeout(function(){
								audio[0].play();
							},500);
						}catch(e){
							//TODO handle the exception
							console.error(e);
						}
					});
				}
			});
		}
	};
	//核心入口
	uxaudio.render = function(options){
		var me = new Class(options);
		me.config.index = new Date().getTime();
		
		return me;
	};
	
	//暴露接口
	exports('uxaudio',uxaudio);
});