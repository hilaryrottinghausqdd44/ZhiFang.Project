layui.extend({
	uxutil: '/ux/util'
}).define(['jquery', 'uxutil', 'layer'], function(exports) {
	"use strict";

	var $ = layui.$;
	var layer = layui.layer;
	var uxutil = layui.uxutil;

	var clodopprint = {
		//LODOP_OB: 'LODOP_OB',
		//LODOP_EM: 'LODOP_EM',
		/**获取LODOP*/
		getLodop: function(isWinOpen) {
			var me = this;
			var lodop = null;
			try {
				if(typeof(CLODOP) == "undefined") {
					if(isWinOpen)
						me.winPromptInstall();
					else
						me.docPromptInstall();
					//JShell.Msg. error('CLODOP打印控件不存在!');
					return;
				} else {
					lodop = getCLodop(); //getLodop(document.getElementById(this.LODOP_OB),document.getElementById(this.LODOP_EM));
					if(!lodop) {
						return;
					}
					lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
					return lodop;
				}
			} catch(e) {
				return;
			}
		},
		/**客户端未安装CLODOP时在浏览器进行弹出窗体提示安装*/
		winPromptInstall: function() {
			var me = this;
			var innerHTML = "";
			try {
				if((typeof(CLODOP) == "undefined") || (typeof(LODOP.VERSION) == "undefined")) {
					if(navigator.userAgent.indexOf('Firefox') >= 0)
						innerHTML = me.getStrHtmFireFox() + innerHTML;
					if(navigator.userAgent.indexOf('Win64') >= 0) {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtm64_Install());
						else
							innerHTML = me.getStrHtm64_Install() + innerHTML;
					} else {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtmInstall());
						else
							innerHTML = me.getStrHtmInstall() + innerHTML;
					}
					//return getCLodop();
				} else if(LODOP.VERSION < "6.1.2.0") {
					if(navigator.userAgent.indexOf('Win64') >= 0) {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtm64_Update());
						else
							innerHTML = me.getStrHtm64_Update() + innerHTML;
					} else {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtmUpdate());
						else
							innerHTML = me.getStrHtmUpdate() + innerHTML;
					}
					//return getCLodop();
				}
				//return getCLodop();
			} catch(err) {
				if(navigator.userAgent.indexOf('Win64') >= 0)
					innerHTML = "Error:" + me.getStrHtm64_Install() + innerHTML;
				else
					innerHTML = "Error:" + me.getStrHtmInstall() + innerHTML;
			}

			if(innerHTML) {
				innerHTML = '<div style="padding:8px;font-weight:bold;font-size:20px;">' + innerHTML + '</div>';
				//页面层
				layer.open({
					type: 1,
					title: "CLODOP打印控件安装提示",
					skin: 'layui-layer-rim', //加上边框
					area: ['340px', '225px'], //宽高
					content: innerHTML,
					btn: ['确定', '取消'],
					yes: function(index, layero) {},
					btn2: function(index, layero) {
						//按钮【取消】的回调
						layer.close(index);
					}
				});

				win.show();
			}
		},
		/**客户端未安装CLODOP时在浏览器进行dom元素追加提示安装*/
		docPromptInstall: function() {
			var me = this;
			try {
				if((typeof(CLODOP) == "undefined") || (typeof(LODOP.VERSION) == "undefined")) {
					if(navigator.userAgent.indexOf('Firefox') >= 0)
						document.documentElement.innerHTML = me.getStrHtmFireFox() + document.documentElement.innerHTML;
					if(navigator.userAgent.indexOf('Win64') >= 0) {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtm64_Install());
						else
							document.documentElement.innerHTML = me.getStrHtm64_Install() + document.documentElement.innerHTML;
					} else {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtmInstall());
						else
							document.documentElement.innerHTML = me.getStrHtmInstall() + document.documentElement.innerHTML;
					}
				} else if(LODOP.VERSION < "6.1.2.0") {
					if(navigator.userAgent.indexOf('Win64') >= 0) {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtm64_Update());
						else
							document.documentElement.innerHTML = me.getStrHtm64_Update() + document.documentElement.innerHTML;
					} else {
						if(navigator.appVersion.indexOf("MSIE") >= 0) document.write(me.getStrHtmUpdate());
						else
							document.documentElement.innerHTML = me.getStrHtmUpdate() + document.documentElement.innerHTML;
					}
					//return getCLodop();
				}
			} catch(err) {
				if(navigator.userAgent.indexOf('Win64') >= 0)
					document.documentElement.innerHTML = "Error:" + me.getStrHtm64_Install() + document.documentElement.innerHTML;
				else
					document.documentElement.innerHTML = "Error:" + me.getStrHtmInstall() + document.documentElement.innerHTML;
			}
		},
		/**@description 32位安装包下载提示*/
		getStrHtmInstall: function() {
			var strHtmInstall = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='" + uxutil.path.getUiPath() + "/src/clodop/resources/" + "CLodop_Setup_for_Win32NT_3.041Extend.exe'>执行安装</a>,安装后请退出系统重新登录。</font>";
			return strHtmInstall;
		},
		/**@description 32位安装包升级提示*/
		getStrHtmUpdate: function() {
			var strHtmUpdate = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + uxutil.path.getUiPath() + "/src/clodop/resources/" + "CLodop_Setup_for_Win32NT_3.041Extend.exe'>执行升级</a>,升级后请退出系统重新登录。</font>";
			return strHtmUpdate;
		},
		/**@description 64位安装包安装提示*/
		getStrHtm64_Install: function() {
			var strHtm64_Install = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='" + uxutil.path.getUiPath() + "/src/clodop/resources/" + "CLodop_Setup_for_Win64NT_3.041Extend.exe'>执行安装</a>,安装后请退出系统重新登录。</font>";
			return strHtm64_Install;
		},
		/**@description 64位安装包升级提示*/
		getStrHtm64_Update: function() {
			var strHtm64_Update = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + uxutil.path.getUiPath() + "/src/clodop/resources/" + "CLodop_Setup_for_Win64NT_3.041Extend.exe'>执行升级</a>,升级后请退出系统重新登录。</font>";
			return strHtm64_Update;
		},
		/**@description 火狐浏览器的额外提示*/
		getStrHtmFireFox: function() {
			var strHtmFireFox = "<br><br><font color='#FF00FF'>注意：<br>1：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它。</font>";
			return strHtmFireFox;
		},

		getCLodop: function(type) {
			var me = this;
			//加载Lodop组件
			//me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
			var LODOP = me.getLodop(true);
			if(!LODOP) {
				//JShell.Msg.error("LODOP打印控件获取出错!");
				return;
			}
			return LODOP;
		},
		/**获取客户端电脑上的打印机集合信息*/
		getPrinterList: function() {
			var me = this;
			var printerList = [];

			var LODOP = me.getCLodop();
			if(!LODOP || !CLODOP) return;

			var iCount = 0;
			if(CLODOP)
				iCount = CLODOP.GET_PRINTER_COUNT();
			var iIndex = 0;
			for(var i = 0; i < iCount; i++) {
				printerList.push([iIndex, CLODOP.GET_PRINTER_NAME(i)]);
				iIndex++;
			}
			return printerList;
		},
		/**缓存当前用户选择的打印机*/
		setDefaultPrinter: function(newValue) {
			var me = this;
			//			me.DefaultPrinter = newValue;
			//			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			//			var key = "printbarcode.Printer." + userId;
			//			var params = {
			//				"Value": me.DefaultPrinter
			//			};
			//			params = JcallShell.JSON.encode(params);
			//			JcallShell.LocalStorage.set(key, params);
		}
	}
	//暴露接口
	exports('clodopprint', clodopprint);
});