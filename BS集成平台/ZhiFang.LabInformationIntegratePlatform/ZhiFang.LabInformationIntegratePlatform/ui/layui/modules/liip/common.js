/**
	@name：modules.liip.common 系统公共方法
	@author：Jcall
	@version 2019-05-06
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','layer'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		MOD_NAME = 'liip_common';
	
	var common = {};
	//类字典
	common.classdict = {
		//类域
		_classNameSpace:'ZhiFang.Entity.LIIP',
		//获取类字典服务
		_classDicUrl:'/ServerWCF/CommonService.svc/GetClassDic',
		//获取类字典列表服务
		_classDicListUrl:'/ServerWCF/CommonService.svc/GetClassDicList',
		/**
			初始化字典信息，支持单个字典，也支持多个字典
			@param {Object} className 类名/类名数组
			@param {Object} callback 回调函数
			@example
			JcallShell.System.ClassDict.init(
				'PContractStatus',
				function(){
					//回调函数处理
				}
			);
			JcallShell.System.ClassDict.init(
				['PContractStatus','PContractStatus2'],
				function(){
					//回调函数处理
				}
			});
		*/
		init:function(className,callback){
			var me = this,
				type = typeof className;
			
			if(type == 'string'){
				//单个字典
				if(me[className]){
					if(typeof callback == 'function'){
						callback();
					}
				}else{
					me.loadClassInfo(me._classNameSpace,className,callback);
				}
			}else if($.isArray(className)){
				var classParamList = className,
					hasData = true;
					
				for(var i in classParamList){
					if(!me[classParamList[i]]){
						hasData = false;
						break;
					}
				}
				
				if(hasData){
					if(typeof callback == 'function'){
						callback();
					}
				}else{
					for(var i in classParamList){
						classParamList[i] = {
							"classnamespace":me._classNameSpace,
							"classname":classParamList[i]
						};
					}
					me.loadClassInfoList(classParamList,callback);
				}
			}
		},
		/**
		 * 加载单个类字典信息
		 * @param {Object} classNameSpace 类域
		 * @param {Object} className 类名
		 * @param {Object} callback 回调函数
		 */
		loadClassInfo:function(classNameSpace,className,callback){
			var me = this,
				url = uxutil.path.ROOT + me._classDicUrl;
				
			url += '?classnamespace=' + classNameSpace + '&classname=' + className;
			
			uxutil.server.ajax({
				url:url
			},function(data){
				if(data.success){
					me.initClassInfo(className,data.value);
				}else{
					me.initClassInfo(className,null);
				}
				if(typeof callback == 'function'){
					callback();
				}
			});
		},
		/**
		 * 加载多个类字典信息
		 * @param {Object} classParamList 类字典参数
		 * @param {Object} callback 回调函数
		 * @example
		 * 	JcallShell.System.ClassDict.loadClassInfoList([
		 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PContractStatus'},
		 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PTaskStatus'}
		 * 	],function(){
		 * 		//回调函数处理
		 * 	});
		 */
		loadClassInfoList:function(classParamList,callback){
			var me = this,
				url = uxutil.path.ROOT + me._classDicListUrl;
				
			var params = {jsonpara:classParamList};
			uxutil.server.ajax({
				url:url,
				type:'post',
				data:JSON.stringify(params)
			},function(data){
				if(data.success){
					for(var i in classParamList){
						me.initClassInfo(classParamList[i].classname,data.value[i][classParamList[i].classname]);
					}
				}else{
					for(var i in classParamList){
						me.initClassInfo(classParamList[i].classname,null);
					}
				}
				if(typeof callback == 'function'){
					callback();
				}
			});
		},
		//初始化字典内容
		initClassInfo:function(className,data){
			this[className] = data;
		},
		//根据类名+字典内容ID获取字典内容
		getClassInfoById:function(className,id){
			return this.getClassInfoByKeyAndValue(className,'id',id);
		},
		//根据类名+字典内容Name获取字典内容
		getClassInfoByName:function(className,name){
			return this.getClassInfoByKeyAndValue(className,'name',name);
		},
		//根据类名+字典内容(key+value)获取字典内容
		getClassInfoByKeyAndValue:function(className,key,value){
			var me = this,
				classInfo = me[className],
				data = null;
			
			for(var i in classInfo){
				if(classInfo[i][key] == value){
					data = classInfo[i];
					break;
				}
			}
			return data;
		}
	};
	//系统列表
	common.system = {
		//服务地址
		_url:uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL',
		//系统列表
		_LIST:null,
		//系统MAP
		_MAP:{},
		//获取系统列表
		getSystemList:function(callback){
			var me = this,
				fields = ['SystemCode','SystemName','SystemHost'];
				url = me._url + '?fields=IntergrateSystemSet_' + fields.join(',IntergrateSystemSet_');
			
			layer.load();
			uxutil.server.ajax({
				url:url
			},function(data){
				layer.closeAll('loading');
				if(data.success){
					me._LIST = (data.value || {}).list || [];
					for(var i in me._LIST){
						me._MAP[me._LIST[i].SystemCode] = me._LIST[i];
					}
					callback();
				}else{
					layer.msg(data.ErrorInfo,{icon:5});
				}
			});
		},
		//@public 获取系统信息列表
		getList:function(callback){
			var me = this;
			if(me._LIST){
				if(typeof callback == 'function')callback(me._LIST);
			}else{
				me.getSystemList(function(){
					if(typeof callback == 'function')callback(me._LIST);
				});
			}
		},
		//@public 获取系统信息Map
		getMap:function(callback){
			var me = this;
			if(me._LIST){
				if(typeof callback == 'function')callback(me._MAP);
			}else{
				me.getSystemList(function(){
					if(typeof callback == 'function')callback(me._MAP);
				});
			}
		}
	};
	//暴露接口
	exports(MOD_NAME,common);
});