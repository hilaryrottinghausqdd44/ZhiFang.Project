Ext.Loader.setConfig({
	enabled: true,
	//将当前时间戳附加到脚本文件以防止缓存。
	//disableCaching:false,
	disableCachingParam: 'v',
	//获取当前版本参数
	getDisableCachingParamValue: function() {
		return JShell.System.JS_VERSION;
	},
	paths: {
		'Shell': JShell.System.Path.UI,
		'Ext.ux': JShell.System.Path.ROOT + '/ui/src/extjs/ux'
	}
});
Ext.onReady(function() {
	Ext.QuickTips.init(); //初始化后就会激活提示功能

	JShell.Window = Ext.create('Ext.window.Window', {
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
	});

	function onShowOperation() {
		var me = this;
		var url = JShell.System.Path.getRootPath() +
			'/ui/extjs/interface/one/index.html?className=Shell.class.assist.scoperation.SCOperation';
		layer.open({
			title: '说明',
			content: url,
			area: ['100%', '100%'],
			yes: function(index, layero) {
				layer.close(index);
			},
			cancel: function(index, layero) {
				layer.close(index);
			}
		});
	}
	//onShowOperation();
});
