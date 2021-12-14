/**
 * @description index 免登录入口,自动侵入帐号及密码,自动登录;
 * @description 登录成功后,自动进入主界面
 * @author longfc
 * @version 2020-09-21
 */
Ext.Loader.setConfig({
	enabled: true,
	//disableCaching:false,
	disableCachingParam: 'v',
	//获取当前版本参数
	getDisableCachingParamValue: function() {
		//return Ext.Date.now();
		return JShell.System.JS_VERSION;
	},
	paths: {
		'Shell': JShell.System.Path.UI,
		'Ext.ux': JShell.System.Path.UI + '/extjs/ux'
	}
});
Ext.onReady(function() {

	Ext.QuickTips.init(); //初始化后就会激活提示功能

	var config = {
		layout: 'fit',
		header: false,
		border: false,
		margin: 0,
		padding: 0,
		modal: true,
		plain: true,
		draggable: false,
		resizable: false,
		closeAction: 'hide',
		close: function() {
			return JShell.Window.closeFun();
		}
	};
	var params = JShell.Page.getParams(true);
	var config2={
		orgCode:params["ORGCODE"],//医院代码
		staffCode:params["STAFFCODE"],//员工工号
		deptCode:params["DEPTCODE"],//科室/病区代码
		timestamp:params["TIMESTAMP"],//（时间戳）
		sign:params["SIGN"]//（签名值）
	};
	
	JShell.Window = Ext.create('Ext.window.Window', config);
	var view = Ext.create('Shell.interface3.Viewport',config2);

});
