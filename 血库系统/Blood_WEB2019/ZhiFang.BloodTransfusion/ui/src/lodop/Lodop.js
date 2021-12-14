/**
 * 供货明细列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.lodop.Lodop', {
	LODOP_OB: 'LODOP_OB',
	LODOP_EM: 'LODOP_EM',
	/**获取LODOP*/
	getLodop: function(isWinOpen) {
		var me = this;
		/***
		 * 时间：2018-05-02
		 * 描述：替换为CLodop
		 */
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
					//JShell.Msg.error('CLODOP打印控件不存在!');
					return;
				}
				lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
				return lodop;
			}
		} catch(e) {
			//JShell.Msg.error('CLODOP打印控件不存在!');
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
			var config = {
				title: 'CLODOP打印控件安装提示',
				resizable: true,
				SUB_WIN_NO: '1',
				width: 340,
				height: 180,
				html: innerHTML,
				listeners: {
					close: function(p, eOpts) {

					}
				}
			};
			var win = JShell.Win.open('Shell.ux.panel.AppPanel', config);
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
		var strHtmInstall = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='" + JShell.System.Path.UI + "/lodop/resources/" + "CLodop_Setup_for_Win32NT_3.041Extend.exe'>执行安装</a>,安装后请退出系统重新登录。</font>";
		return strHtmInstall;
	},
	/**@description 32位安装包升级提示*/
	getStrHtmUpdate: function() {
		var strHtmUpdate = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + JShell.System.Path.UI + "/lodop/resources/" + "CLodop_Setup_for_Win32NT_3.041Extend.exe'>执行升级</a>,升级后请退出系统重新登录。</font>";
		return strHtmUpdate;
	},
	/**@description 64位安装包安装提示*/
	getStrHtm64_Install: function() {
		var strHtm64_Install = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='" + JShell.System.Path.UI + "/lodop/resources/" + "CLodop_Setup_for_Win64NT_3.041Extend.exe'>执行安装</a>,安装后请退出系统重新登录。</font>";
		return strHtm64_Install;
	},
	/**@description 64位安装包升级提示*/
	getStrHtm64_Update: function() {
		var strHtm64_Update = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + JShell.System.Path.UI + "/lodop/resources/" + "CLodop_Setup_for_Win64NT_3.041Extend.exe'>执行升级</a>,升级后请退出系统重新登录。</font>";
		return strHtm64_Update;
	},
	/**@description 火狐浏览器的额外提示*/
	getStrHtmFireFox: function() {
		var strHtmFireFox = "<br><br><font color='#FF00FF'>注意：<br>1：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它。</font>";
		return strHtmFireFox;
	}
});