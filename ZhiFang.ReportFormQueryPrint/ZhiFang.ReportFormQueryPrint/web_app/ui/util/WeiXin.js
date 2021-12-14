/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**微信处理*/
JcallShell.WeiXin = {
	/**初始化参数*/
	config:{
		appId:'wx401c52ad6d6741db',//公众号的唯一标识
		signature:null,//微信签名
		url:'',//签名的地址
		timestamp:1414587457,//生成签名的时间戳
		nonceStr: "JcallShell",//生成签名的随机串
		jsApiList:[]//需要使用的JS接口列表
	},
	_timeout:null,//超时时间
	/**错误信息*/
	_error_no_signature: "没有获取到微信签名,请刷新页面",
	
	/**获取微信签名*/
	_getSignature: function(callback) {
		if(!callback){
			alert("请传递回调函数callback！");
			return;
		}
		
		var me = this;
		var url = JcallShell.System.Path.ROOT + "/ServerWCF/WeiXinAppService.svc/GetJSAPISignature?" +
			"noncestr=" + me.config.nonceStr + "&timestamp=" + me.config.timestamp + "&url=" + me.config.url;
		JcallShell.Server.ajax({
			url: url
		}, function(data) {
			if (data.success) {
				me.config.signature = data.value.signature;
				me._timeout = data.value.TimeOut;
				//时间点到达最大值后自动重新获取微信签名
				callback({success:true});
			} else {
				callback({
					success:false,
					msg:me._error_no_signature
				});
			}
		});
	},
	/**初始化*/
	init:function(jsApiList,url,callback){
		var me = this;
		me.config.url = url || window.location.href;
		me.config.jsApiList = jsApiList || [];
		
		me._getSignature(function(data){
			wx.config(me.config);
			if(callback){callback(data);}
		});
	},
	/**
	 * 操作API
	 * @param {Object} name API方法名
	 * @param {Object} config API方法参数
	 */
	doAction:function(name,config){
		var me = this;
		//调用微信API处理
		if(me.config.signature){
			wx[name](config);
		}else{
			me._getSignature(function(data){
				if(data.success){
					wx[name](config);
				}else{
					alert(data.msg);
				}
			});
		}
	}
};