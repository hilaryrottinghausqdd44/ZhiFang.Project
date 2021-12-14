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
		//'Ext.ux':JShell.System.Path.UI + '/extjs/ux'
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

	var view = Ext.create('Shell.interface.one.Viewport', {
		listeners: {
			login: function(p) {
				initView();
			}
		}
	});

	var params = JShell.Page.getParams(true),
		panel = null,
		ClassName = '',
		config = {
			header: false,
			border: false
		};

	for (var i in params) {
		if (i == 'CLASSNAME') {
			ClassName = params.CLASSNAME
		} else {
			config[i] = params[i];
		}
	}

	if (ClassName) {
		panel = Ext.create(ClassName, config);
	} else {
		panel = Ext.create('Ext.panel.Panel', {
			html: '<div style="margin:20px;text-align:center;color:red;font-weight:bold;">请传递ClassName参数</div>'
		});
	}
	view.add(panel);

	function initView() {
		if (JcallShell.System.Date.getDate()) {
			onDBUpdate(function() {
				if (JcallShell && JcallShell.BLTF) {
					JcallShell.BLTF.RunInfo.initAll(function() {
						//初始化字典信息
						ininDictList(function() {
							onAfterLogin();
						});
					});
				} else {
					//初始化字典信息
					ininDictList(function() {
						onAfterLogin();
					});
				}
			});
		} else {
			JcallShell.System.Date.init(function() {
				onDBUpdate(function() {
					if (JcallShell.BLTF) {
						JcallShell.BLTF.RunInfo.initAll(function() {
							//初始化字典信息
							ininDictList(function() {
								onAfterLogin();
							});
						});
					} else {
						//初始化字典信息
						ininDictList(function() {
							onAfterLogin();
						});
					}

				});
			});
		}
	};

	function onAfterLogin() {};

	/**
	 * 手工调用数据库升级服务
	 * @param {Object} callback
	 */
	function onDBUpdate(callback) {
		var isDBUpdate = JcallShell.BLTF.cachedata.getCache("BT_SYS_DBUpdate");
		if (isDBUpdate == true) {
			callback();
		} else {
			//显示遮罩层
			Ext.Msg.wait("加载处理中...");
			var url = JShell.System.Path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_SYS_DBUpdate";
			JShell.Server.get(url, function(data) {
				//隐藏遮罩层
				Ext.Msg.close();
				JShell.BLTF.cachedata.setCache("BT_SYS_DBUpdate", true);
				callback(data);
			});
		}
	};
	/**
	 * 初始化字典信息
	 */
	function ininDictList(callback) {
		if (callback) callback();
	};
	//===============================================================
	//屏蔽右键菜单
	//Ext.getDoc().on("contextmenu",function(e){e.stopEvent();});
	//键盘监听
	if (document.addEventListener) {
		document.addEventListener("keydown", maskBackspace, true);
	} else {
		document.attachEvent("onkeydown", maskBackspace);
	}

	function maskBackspace(event) {
		var event = event || window.event; //标准化事件对象
		var obj = event.target || event.srcElement;
		var keyCode = event.keyCode ? event.keyCode : event.which ?
			event.which : event.charCode;
		if (keyCode == 8) {
			if (obj != null && obj.tagName != null && (obj.tagName.toLowerCase() == "input" ||
					obj.tagName.toLowerCase() == "textarea")) {
				event.returnValue = true;
				if (Ext.getCmp(obj.id)) {
					if (Ext.getCmp(obj.id).readOnly) {
						if (window.event) {
							event.returnValue = false; //or event.keyCode=0  
						} else {
							event.preventDefault(); //for ff
						}
					}
				}
			} else {
				if (window.event) {
					event.returnValue = false; //or event.keyCode=0  
				} else {
					event.preventDefault(); //for ff
				}
			}
		}
	}

	var map = new Ext.KeyMap(document, [{
		key: [116], //F5
		fn: function() {},
		stopEvent: true,
		scope: this
	}, {
		key: [37, 39, 115], //方向键左,右,F4
		alt: true,
		fn: function() {},
		stopEvent: true,
		scope: this
	}, {
		key: [82], //ctrl + R
		ctrl: true,
		fn: function() {},
		stopEvent: true,
		scope: this
	}]);
	map.enable();

});
