$(function() {
	//外部传入参数
	var PARAMS = Shell.util.getRequestParams(true);
	//var WEIXIN_URL = Shell.util.Path.uiPath + "/server/weixin_news.json";
	var WEIXIN_URL = Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileById";
	var WEIXIN_FIELDS = ['FFile_WeiXinTitle', 'FFile_WeiXinContent', 'FFile_PublisherDateTime', 'FFile_Counts'];

	//标题
	var Title = "";
	//发布时间
	var PublishTimes = "";
	//阅读数量
	var ReadCount = "";
	//新闻内容
	var Content = "";

	//清空信息
	function clearInfo() {
		Title = "";
		PublishTimes = "";
		ReadCount = "";
		Content = "";
	}

	//显示蒙版
	function showMask() {
		$("#MaskWin").show();
		$("#MaskText").show();
	}
	//隐藏蒙版
	function hideMask() {
		$("#MaskWin").hide();
		$("#MaskText").hide();
	}

	//获取新闻信息
	function initInfo(callback) {
		//显示蒙版
		showMask();
		//微信新闻服务地址
		var url = WEIXIN_URL + '?id=' + PARAMS.ID + '&fields=' + WEIXIN_FIELDS.join(",");

		Shell.util.Server.ajax({
			showError: true,
			url: url
		}, function(data) {
			//清空信息
			clearInfo();
			//隐藏蒙版
			hideMask();

			if(data.success) {
				var value = data.value || {};
				Title = value.WeiXinTitle || "";
				PublishTimes = "发布时间 " + (Shell.util.Date.toString(value.PublisherDateTime, true) || "无");
				ReadCount = "阅读量 " + (value.Counts || "0");
				Content = value.WeiXinContent || "没有内容";
			} else {
				var msg = data.msg || "服务器错误，没有返回错误信息！"
				Content = "<div style='color:red;text-align:center;'>" + msg + "</div>";
				var ButtonDiv =
					"<div style='text-align:center;margin:20px 0;'>" +
					"<div style='position: absolute;left:50%;margin-left:-50px;padding:10px 20px;border:1px solid red;color:red;' onclick='initHtml();'>重新获取</div>" +
					"</div>";
				Content += ButtonDiv;
			}
			callback();
		});
	}
	
	//微信分享功能
	/**微信功能*/
	var weixin = {
		signature: null,
		timeout: null,
		timestamp: 1414587457,
		url: window.document.location.href.split("#")[0],
		nonceStr: "AAAA",
		error_no_signature: "没有获取到微信签名,请刷新页面",
		/**微信功能初始化*/
		init: function(callback) {
			//获取微信签名
			weixin.get_signature(function() {
				weixin.set_sonfig();
				if(callback) {
					callback();
				}
			});
		},
		/**设置微信参数*/
		set_sonfig: function() {
			wx.config({
				debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
				appId: 'wx359def2eeed3abe6', // 必填，公众号的唯一标识
				timestamp: weixin.timestamp, // 必填，生成签名的时间戳
				nonceStr: weixin.nonceStr, // 必填，生成签名的随机串
				signature: weixin.signature, // 必填，签名，见附录1
				jsApiList: [
					'checkJsApi',
					'openLocation',
					'getLocation',
					'onMenuShareTimeline',
					'onMenuShareAppMessage'
				] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
			});
			if(weixin.timeout && weixin.timeout > 0) {
				setTimeout(weixin.get_signature, weixin.timeout * 1000);
			}
		},
		/**获取微信签名*/
		get_signature: function(callback) {
			showMask();
			Shell.util.Server.ajax({
				url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/GetJSAPISignature?" +
					"noncestr=" + weixin.nonceStr + "&timestamp=" + weixin.timestamp +
					"&url=" + weixin.url
			}, function(data) {
				hideMask();
				if(data.success) {
					weixin.signature = data.value.signature;
					weixin.timeout = data.value.TimeOut;
					callback();
				} else {
					var html = '<div style="margin:20px;padding:10px;radius:2px;text-align:center;color:red;border:1px solid red;">' +
						weixin.error_no_signature + '</div>';
					$("#Content").html(html);
				}
			});
		},
		/**分享*/
		onMenuShare: function(title,desc,link,imgUrl) {
			this.doAction(function(){
				onMenuShare(title,desc,link,imgUrl);
			});
		},
		doAction:function(callback){
			if(weixin.signature) {
				callback();
			} else {
				weixin.init(function() {
					callback();
				});
			}
		},
		hideMenuItems:function(){
			this.doAction(function(){
				wx.hideMenuItems({
					// 要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮，所有menu项见附录3
		    		menuList: [
		    			'onMenuShareTimeline',
						'onMenuShareAppMessage'
		    		]
				});
			});
		},
		showMenuItems:function(){
			this.doAction(function(){
				wx.showMenuItems({
					// 要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮，所有menu项见附录3
		    		menuList: [
		    			'onMenuShareTimeline',
						'onMenuShareAppMessage'
		    		]
				});
			});
		}
		
	}
	
	function onMenuShare(title,desc,link,imgUrl){
		//分享给朋友
		wx.onMenuShareAppMessage({
			title: title,
			desc: desc,
			link: link,
			imgUrl: imgUrl,
			trigger: function(res) {
				// 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
				alert('用户点击发送给朋友');
			},
			success: function(res) {
				alert('已分享');
			},
			cancel: function(res) {
				alert('已取消');
			},
			fail: function(res) {
				alert(JSON.stringify(res));
			}
		});
		//分享到朋友圈
		wx.onMenuShareTimeline({
			title: title,
			link: link,
			imgUrl: imgUrl,
			trigger: function(res) {
				// 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
				alert('用户点击分享到朋友圈');
			},
			success: function(res) {
				alert('已分享');
			},
			cancel: function(res) {
				alert('已取消');
			},
			fail: function(res) {
				alert(JSON.stringify(res));
			}
		});
	}
	
	//初始化页面
	function initHtml() {
		initInfo(function(value) {
			$("#Title").html(Title);
			$("#PublishTimes").html(PublishTimes);
			$("#ReadCount").html(ReadCount);
			$("#Content").html(Content);
		});
	}
	window.initHtml = initHtml;

	//初始化页面
	initHtml();
	
	$("#Test_Button").on("click",function(){
		if($(this).html() == '隐藏分享'){
			weixin.hideMenuItems();
			$(this).html('显示分享');
		}else{
			weixin.showMenuItems();
			$(this).html('隐藏分享');
		}
	});
});