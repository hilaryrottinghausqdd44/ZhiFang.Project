/**
	@name：layui.ux.util 工具集
	@author：longfc
	@version 2019-07-3
 */
layui.extend({
	uxutil: '../../ux/util',
	bloodsconfig: '../../config/bloodsconfig'
}).define(['jquery', 'uxutil', 'bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.$;
	var uxutil = layui.uxutil;
	var bloodsconfig = layui.bloodsconfig;

	//CS服务交互
	var csserver = {
		//CS服务调用的超时设置
		CS_TIME_OUT:65000,
		//CS服务端口
		CS_PORT: bloodsconfig.CSServer.CS_PORT || "",
		//CS机构Id
		CS_LABID: bloodsconfig.CSServer.CS_LABID || "",
		//CS服务访问域名称domain
		CS_DOMAIN: bloodsconfig.CSServer.CS_DOMAIN || "",
		CS_LONIN: bloodsconfig.CSServer.CS_LONIN || "",
		//CS密钥
		CS_PMOPERTYPEKEY: bloodsconfig.CSServer.CS_PMOPERTYPEKEY || "A3751787-C218-45A8-A79B-32ACAD2973EC",
		//CS请求头信息
		CS_HEADERS: {
			"Accept": "application/json; charset=utf-8",
			"PMOPERTYPEKEY": bloodsconfig.CSServer.CS_PMOPERTYPEKEY || "A3751787-C218-45A8-A79B-32ACAD2973EC",
		},
		CS_USER: {
			"USERID": "UserNo",
			"USERNAME": "CName",
		},
		//返回参数
		resultParams: {
			success: "success",
			msg: "msg",
			//返回结果的第一级key
			resultRoot: "result",
			//返回结果的数据信息key
			rowCount: "rowCount"
		},
		/***
		 * ajax请求:返回数据集合调用
		 * 可配置参数showError：true返回原始错误信息，false返回替换的错误信息
		 * @param {Object} config
		 * @param {Object} callback
		 * @param {Object} showError
		 * @param {Object} isBool
		 */
		ajax: function(config, callback, showError, isBool) {
			var me = this;
			var con = {
				type: "GET",
				dataType: "json",
				contentType: "application/json; charset=UTF-8",
				async: true,
				timeout: csserver.CS_TIME_OUT
			};

			for(var i in config) {
				con[i] = config[i];
			}

			if(!con.success) {
				con.success = function(data, textStatus) {
					//console.log(JSON.parse(JSON.stringify(data)));
					if(typeof(data) != 'object') {
						callback(data);
						return;
					}
					try {
						//CS返回结果的数据适配
						var msg = "",
							value = "",
							result = data[me.resultParams.resultRoot];
						if(result) {
							if(result.length > 0) {
								result = result[0];
								//console.log(JSON.parse(JSON.stringify(result)));
								if(isBool == true) {
									if(result) {
										data.success = result.success;
										data.msg = result.ErrorInfo;
										data.vaule = [];
									} else {
										data.success = false;
										data.msg = "获取不到调用服务的返回信息!";
									}
								}
							}
							if(result && typeof(result) === "string") {
								result = JSON.parse(result);
								var rowCount = result[me.resultParams.rowCount];
								if(!rowCount) {
									data.success = true;
									data.vaule = result;
								} else {
									data.msg = "调用服务获取信息为空!";
									data.success = false;
									data.vaule = [];
								}
							}
						}
						data.msg = (!data.success && msg && !showError) ? "服务器出错了 " : msg;
						if(value && typeof(value) === "string") {
							if(isNaN(value)) {
								value = eval("(" + value + ")");
							} else {
								value = value + "";
							}
						}
						if(!data.value) data.value = value;

						callback(data);
					} catch(e) {
						//TODO handle the exception
						data.success = false;
						data.value = null;
						data.msg = "解析数据异常!";
						callback(data);
					}

				};
			}
			if(!con.error) {
				con.error = function(XMLHttpRequest, textStatus, errorThrown) {
					callback({
						success: false,
						msg: me.getMsgByStatus(XMLHttpRequest.status)
					});
				};
			}
			con.headers = con.headers || {};

			return $.ajax(con);
		},
		/***
		 * 返回操作成功或失败调用{success: false, ErrorInfo: "失败-没有查到待返HIS的数据！"}
		 * @param {Object} config
		 * @param {Object} callback
		 * @param {Object} showError
		 * @param {Object} isBool
		 */
		csAjax: function(config, callback, showError, isBool) {
			var me = this;
			var con = {
				type: "GET",
				dataType: "json",
				contentType: "application/json; charset=UTF-8",
				async: true,
				timeout: csserver.CS_TIME_OUT
			};

			for(var i in config) {
				con[i] = config[i];
			}

			if(!con.success) {
				con.success = function(data, textStatus) {
					if(typeof(data) != 'object') {
						callback(data);
						return;
					}
					try {
						//CS返回结果的数据适配
						var msg = "",
							value = ""
						if(data) {
							data.msg = data.ErrorInfo;
							data.vaule = [];
						} else {
							data.success = false;
							data.msg = "获取不到调用服务的返回信息!";
						}
						callback(data);
					} catch(e) {
						//TODO handle the exception
						data.success = false;
						data.value = null;
						data.msg = "解析数据异常!";
						callback(data);
					}

				};
			}
			if(!con.error) {
				con.error = function(XMLHttpRequest, textStatus, errorThrown) {
					callback({
						success: false,
						msg: me.getMsgByStatus(XMLHttpRequest.status)
					});
				};
			}
			con.headers = con.headers || {};
			
			return $.ajax(con);
		},
		/**根据状态码获取错误信息*/
		getMsgByStatus: function(status) {
			var msg = "";
			switch(status) {
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
	};

	//暴露接口
	exports('csserver', csserver);
});