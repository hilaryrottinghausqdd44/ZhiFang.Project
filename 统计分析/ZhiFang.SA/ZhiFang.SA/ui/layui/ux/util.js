/**
	@name：layui.ux.util 工具集
	@author：Jcall
	@version 2019-03-25
 */
layui.define('jquery',function(exports){
	"use strict";
	
	var $ = layui.$;
	
	//外部接口
	var uxutil = {
		//登录成功后,是否调用数据库升级服务
		loginAfterIsUpdateDB:true,
		//静态地址
		path:{
			LOCAL:'',//主机地址
			ROOT:'',//主机地址/项目名
			UI:'',//主机地址/项目名/UI包名
			EXTJS:'',
			LAYUI:'',
			LIIP_ROOT:'http://localhost/ZhiFang.SA.RBAC/',
			init: function() {
				this.LOCAL = this.getLocal();
				this.ROOT = this.getRootPath();
				this.UI = this.getUiPath();
				this.EXTJS = this.getExtjsPath();
				this.LAYUI = this.getLayuiPath();
			},
			getLocal: function(){
				var location = window.document.location,
					curWwwPath = location.href,
					pathName = location.pathname,
					pos = curWwwPath.indexOf(pathName),
					localhostPath = curWwwPath.substring(0,pos);
					
				return localhostPath;
			},
			getRootPath: function(){
				var pathName = window.document.location.pathname,
					projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1),
					rootPath = this.getLocal() + projectName;
					
				return rootPath;
			},
			getUiPath: function(){
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name;
					
				return uiPath;
			},
			getExtjsPath: function(){
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name + '/extjs';
					
				return uiPath;
			},
			getLayuiPath: function(){
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name + '/layui';
					
				return uiPath;
			}
		},
		//cookie信息
		cookie:{
			//COOKIE默认缓存时间:单位小时
			DEFAULT_COOKIE_HOUSE: 24,
			//cookie键值映射
			map:{
				'LABID': '000100', //实验室ID
				'LABNAME': '000101', //实验室名称
				'USERID': '000200', //员工ID
				'USERNAME': '000201', //员工姓名
				'ACCOUNTID': '000300', //员工账户ID
				'ACCOUNTNAME': '000301', //员工账户名
				'ACCOUNTCODE': '000302', //员工代码
				'DEPTID': '000400', //部门ID
				'DEPTNAME': '000401', //部门名称
				'DEPTCODE': '000402', //部门编号
				'MODULEID': '000500', //模块ID
				'MODULENAME': '000501', //模块名称
				'MODULEOPERID': '000600', //模块操作ID
				'MODULEOPERNAME': '000601', //模块操作名称
				'DEFAULTMODULEID': '100001', //默认模块ID 608EE9C7CA151681C73
				'ISLOCKED': '100002', //锁定标记
				'OLDACCOUNTNAME': '100003', //历史账户名
				'OPENEDMODULEIDS': '100004', //历史打开记录
				'REMEMBERACCOUNT': '100005', //是否记住用户名
				'REMEMBERPWD': '100006' //是否记住密码
			},
			//根据名称获取cookie值
			get: function(name) {
				var nameT = this.map[name] || name,
					reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
					arr = document.cookie.match(reg);
	
				if (arr) {
					return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
				}
	
				return null;
			},
			/**
			 * @name 设置cookie键值内容
			 * @param {Array Object} value Cookie键值对象或数组
			 * @param {Number} days 过期时间
			 * @example [{name:'a_key',value:'a_value',expires:{Date}}]
			 */
			set: function(value, days) {
				if (!value) return;
	
				var type = typeof(value);
				var list = type == 'object' ? [value] : value;
	
				//默认过期时间30天
				var dayTimes = 24 * 60 * 60 * 1000,
					days = days || 30,
					exp = new Date();
	
				exp.setTime(exp.getTime() + dayTimes * days);
	
				var len = list.length;
				for (var i = 0; i < len; i++) {
					var name = list[i].name;
					var value = list[i].value;
					if (list[i].expires) {
						exp = list[i].expires;
					}
					var nameT = this.map[name] || name;
					document.cookie = name + "=" + encodeURI(value) + "; expires=" + exp.toGMTString() + "; path=/";
				}
			},
			//删除cookie
			delCookie: function(name) {
				var cval = this.get(name);
				if(!cval) return;
				this.set({name:name,value:''},-1);
			},
			//获取所有cookie
			getAllCookie: function() {
				var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
				var list = [];
				if(keys) {
					for(var i = 0; i < keys.length; i++) {
						list.push([keys[i], this.get(keys[i])]);
					}
				}
				return list;
			},
			//清理所有cookie
			clearCookie: function() {
				var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
				if(keys) {
					for(var i = 0; i < keys.length; i++) {
						this.delCookie(keys[i]);
					}
				}
			}
		},
		//参数
		params:{
			//获取页面传递的参数
			//@param toUpperCase 是否将参数名转化为大写
			get: function(toUpperCase) {
				var url = location.search; //获取url中"?"符后的字串
		
				if (url.indexOf("?") == -1) return {};
		
				var str = url.substr(1),
					strs = str.split("&"),
					len = strs.length,
					params = {};
		
				for (var i = 0; i < len; i++) {
					var arr = strs[i].split("=");
					if (toUpperCase) {
						arr[0] = arr[0].toLocaleUpperCase();
					}
					params[arr[0]] = decodeURI(arr[1]);
				}
		
				return params;
			}
		},
		//服务器交互
		server:{
			//返回参数
			resultParams: {
				success: "success",
				msg: "ErrorInfo",
				value: "ResultDataValue"
			},
			//ajax请求
			//可配置参数showError：true返回原始错误信息，false返回替换的错误信息
			ajax: function(config, callback,showError) {
				var me = this;
				var con = {
					type: "get",
					dataType: "json",
					contentType:"application/json; charset=utf-8",
					async: true
				};
		
				for (var i in config) {
					con[i] = config[i];
				}
				
				if(!con.success){
					con.success = function(data, textStatus) {
						if(typeof(data) != 'object'){
							callback(data);
							return;
						}
						
						var msg = data[me.resultParams.msg],
							value = data[me.resultParams.value];
						
						data.msg = (!data.success && msg && !showError) ? "服务器出错了 " : msg;
						
						if(value && typeof(value) === "string"){
							if(isNaN(value)){
								value = eval("(" + value + ")");
							}else{
								value = value + "";
							}
						}
						
						data.value = value;
		
						callback(data);
					};
				}
				if(!con.error){
					con.error = function(XMLHttpRequest, textStatus, errorThrown) {
						callback({
							success: false,
							msg: me.getMsgByStatus(XMLHttpRequest.status)
						});
					};
				}
				con.headers = con.headers || {};
				//智方科技MD5加密值
				con.headers.Type = "503DC7557EB732E2E9ACBC1F4DE5455B";
		
				return $.ajax(con);
			},
			/**根据状态码获取错误信息*/
			getMsgByStatus: function(status) {
				var msg = "";
				switch (status) {
					case 404:
						msg = "地球上找不到这个地址";
						break;
					case 500:
						msg = "服务器出错了";
						break;
					default:
						msg = "未定义错误：" + status;
						break;
				}
				return msg;
			}
		}
	};
	
	uxutil.path.init();
	//暴露接口
	exports('uxutil',uxutil);
});
