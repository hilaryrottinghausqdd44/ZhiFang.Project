/**
 * CLodop公共类
 * @author Jcall
 * @version 2018-11-01
 */
Ext.define('Shell.class.lodop.Lodop',{
	Win32_Install_URL:JShell.System.Path.ROOT + '/web_src/lodop/resources/CLodop_Setup_for_Win32NT_3.041Extend.exe',
	Win32_Update_URL:JShell.System.Path.ROOT + '/web_src/lodop/resources/CLodop_Setup_for_Win32NT_3.041Extend.exe',
	Win64_Install_URL:JShell.System.Path.ROOT + '/web_src/lodop/resources/CLodop_Setup_for_Win64NT_3.041Extend.exe',
	Win64_Update_URL:JShell.System.Path.ROOT + '/web_src/lodop/resources/CLodop_Setup_for_Win64NT_3.041Extend.exe',
	//获取LODOP
	getLodop:function(){
		var me = this,
			lodop = null;
			
		try{
			if(typeof(CLODOP) == "undefined") {
				me.winPromptInstall();
				return;
			}else{
				lodop = getCLodop();
				if(!lodop) return;
				
				lodop.SET_LICENSES("北京智方科技开发有限公司","653726269717472919278901905623","","");
				return lodop;
			}
		}catch(e){
			return;
		}
	},
	//客户端未安装CLODOP时在浏览器进行弹出窗体提示安装
	winPromptInstall:function() {
		var me = this,
			html = [];
			
		try {
			if((typeof(CLODOP) == "undefined") || (typeof(LODOP.VERSION) == "undefined")) {
				if(navigator.userAgent.indexOf('Firefox') >= 0){
					html.push(me.getStrHtmFireFox());
				}
				if(navigator.userAgent.indexOf('Win64') >= 0){
					html.push(me.getStrHtm64_Install());
				}else{
					html.push(me.getStrHtmInstall());
				}
			}else if(LODOP.VERSION < "6.1.2.0"){
				if(navigator.userAgent.indexOf('Win64') >= 0){
					html.push(me.getStrHtm64_Update());
				}else{
					html.push(me.getStrHtmUpdate());
				}
			}
		}catch(err) {
			if(navigator.userAgent.indexOf('Win64') >= 0){
				html.push("Error:" + me.getStrHtm64_Install());
			}else{
				html.push("Error:" + me.getStrHtmInstall());
			}
		}

		if(html.length > 0){
			html = '<div style="padding:25px 15px;font-weight:bold;font-size:14px;">' + html.join('<br>') + '</div>';
			JShell.Win.open('Ext.panel.Panel',{
				title:'CLODOP打印控件安装提示',
				resizable:true,
				width:340,
				height:180,
				html:html,
				listeners:{
					close:function(p,eOpts){}
				}
			}).show();
		}
	},
	/**@description 32位安装包下载提示*/
	getStrHtmInstall:function(){
		var me = this;
		var html = "<font color='#FF00FF'>打印控件未安装!点击这里<a href='" + me.Win32_Install_URL + "'>执行安装</a>,安装后请退出系统重新登录。</font>";
		return html;
	},
	/**@description 32位安装包升级提示*/
	getStrHtmUpdate:function(){
		var me = this;
		var html = "<font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + me.Win32_Update_URL + "'>执行升级</a>,升级后请退出系统重新登录。</font>";
		return html;
	},
	/**@description 64位安装包安装提示*/
	getStrHtm64_Install:function(){
		var me = this;
		var html = "<font color='#FF00FF'>打印控件未安装!点击这里<a href='" + me.Win64_Install_URL + "'>执行安装</a>,安装后请退出系统重新登录。</font>";
		return html;
	},
	/**@description 64位安装包升级提示*/
	getStrHtm64_Update:function(){
		var me = this;
		var html = "<font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + me.Win64_Update_URL + "'>执行升级</a>,升级后请退出系统重新登录。</font>";
		return html;
	},
	/**@description 火狐浏览器的额外提示*/
	getStrHtmFireFox:function(){
		var html = "<font color='#FF00FF'>注意：<br>1：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它。</font>";
		return html;
	}
});