/**
	@name：BS血库字典类
	@author：longfc
	@version 2019-06-25
 */
layui.extend({
	//uxutil:'ux/util'
	//dataadapter: 'ux/dataadapter'
}).define(['jquery', 'uxutil'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil;
	//系统类字典
	var bloodClassDict = {
		//类域
		_classNameSpace: 'ZhiFang.Entity.BloodTransfusion',
		//获取类字典服务
		_classDicUrl: '/CommonService.svc/GetClassDic',
		//获取类字典列表服务
		_classDicListUrl: '/CommonService.svc/GetClassDicList',
		commonDataKey: "BloodClassDict",
		setCache: function(dictKey, data) {
			var me = this;
			var win = me.getTop(window);
			if(!win.CachedOptionList) win.CachedOptionList = {};
			win.CachedOptionList[dictKey] = data;
		},
		getCache: function(dictKey) {
			var me = this;
			var data = "";
			var commonData = "";
			var win = me.getTop(); //layui.bloodSelectData
			if(!win) return commonData;
			if(win.CachedOptionList) {
				data = win.CachedOptionList[dictKey];
			}
			return data;
		},
		getTop: function(curWin) {
			var me = this;
			curWin = curWin || window;
			var win = curWin.top == curWin ? curWin : me.getTop(curWin.top);
			return win;
		},
		/**
			初始化字典信息，支持单个字典，也支持多个字典
			@param {Object} className 类名/类名数组
			@param {Object} callback 回调函数
			@example
			bloodClassDict.init(
				'PContractStatus',
				function(){
					//回调函数处理
				}
			);
		*/
		init: function(className, callback) {
			var me = this,
				type = typeof className;
			me.loadClassInfo(me._classNameSpace, className, function(data) {
				callback(data);
			});
		},
		/**
		 * 加载单个类字典信息
		 * @param {Object} classNameSpace 类域
		 * @param {Object} className 类名
		 * @param {Object} callback 回调函数
		 */
		loadClassInfo: function(classNameSpace, className, callback) {
			var me = this,
				url = uxutil.path.ROOT + me._classDicUrl;
			url += '?classnamespace=' + classNameSpace + '&classname=' + className;

			uxutil.server.ajax({
				url: url
			}, function(data) {
				if (data.success) {
					me.initClassInfo(className, data.value);
				} else {
					me.initClassInfo(className, null);
				}
				if (typeof callback == 'function') {
					callback(data);
				}
			});
		},
		/**
		 * 加载多个类字典信息
		 * @param {Object} classParamList 类字典参数
		 * @param {Object} callback 回调函数
		 * @example
		 * 	bloodClassDict.loadClassInfoList([
		 * 		{classnamespace:'ZhiFang.Entity.BloodTransfusion',classname:'HisABOCode'},
		 * 		{classnamespace:'ZhiFang.Entity.BloodTransfusion',classname:'HisRhCode'}
		 * 	],function(){
		 * 		//回调函数处理
		 * 	});
		 */
		loadClassInfoList: function(classParamList, callback) {
			var me = this,
				url = uxutil.path.ROOT + me._classDicListUrl;

			var params = {
				jsonpara: classParamList
			};
			uxutil.server.ajax({
				url: url,
				type: 'post',
				data: JSON.stringify(params)
			}, function(data) {
				if (data.success) {
					for (var i in classParamList) {
						me.initClassInfo(classParamList[i].classname, data.value[i][classParamList[i].classname]);
					}
				} else {
					for (var i in classParamList) {
						me.initClassInfo(classParamList[i].classname, null);
					}
				}
				if (typeof callback == 'function') {
					callback();
				}
			});
		},
		//初始化字典内容
		initClassInfo: function(className, data) {
			this[className] = data;
		},
		//根据类名+字典内容ID获取字典内容
		getClassInfoById: function(className, id) {
			return this.getClassInfoByKeyAndValue(className, 'id', id);
		},
		//根据类名+字典内容Name获取字典内容
		getClassInfoByName: function(className, name) {
			return this.getClassInfoByKeyAndValue(className, 'name', name);
		},
		//根据类名+字典内容(key+value)获取字典内容
		getClassInfoByKeyAndValue: function(className, key, value) {
			var me = this,
				classInfo = me[className],
				data = null;

			for (var i in classInfo) {
				if (classInfo[i][key] == value) {
					data = classInfo[i];
					break;
				}
			}
			return data;
		},
		//根据类名获取某一字典类内容
		getCalssByClassName: function(className) {
			var me = this;
			var data = me[className];
			if (!data || data.length <= 0) {
				me.loadClassInfo(me._classNameSpace, className, function() {
					data = me[className];
					return data;
				});
			} else {
				return data;
			}
		},
		//初始化全部字典类内容
		initAllDict: function(callback) {
			var me = this;
			var classParamList = [{
					classnamespace: 'ZhiFang.Entity.BloodTransfusion',
					classname: 'HisABOCode'
				},
				{
					classnamespace: 'ZhiFang.Entity.BloodTransfusion',
					classname: 'HisRhCode'
				},
				{
					classnamespace: 'ZhiFang.Entity.BloodTransfusion',
					classname: 'BreqFormStatus'
				}
			];
			me.loadClassInfoList(classParamList, callback);
		}
	};
	//暴露接口
	exports('bloodClassDict', bloodClassDict);
});
