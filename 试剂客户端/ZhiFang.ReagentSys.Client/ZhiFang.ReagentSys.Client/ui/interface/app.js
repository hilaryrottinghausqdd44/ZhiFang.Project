Ext.Loader.setConfig({
	enabled: true,
	disableCachingParam: 'v',
	//获取当前版本参数
	getDisableCachingParamValue: function() {
		return JShell.System.JS_VERSION;
	},
	paths: {
		'Shell': JShell.System.Path.UI,
		'Ext.ux': JShell.System.Path.UI + '/extjs/ux'
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

	var view = Ext.create('Shell.interface.Viewport', {
		listeners: {
			login: function(p) {
				onAfterLogin();
			}
		}
	});

	//参数列表
	var _PARAMS = {
		type: {
			//手工验收
			'70': 'Shell.class.rea.client.confirm.manualinput.App',
			//订单验收
			'71': 'Shell.class.rea.client.confirm.reaorder.App',
			//供货验收
			'72': 'Shell.class.rea.client.confirm.reasale.App',
			//入库管理
			'73': 'Shell.class.rea.client.stock.manage.App',
			//入库查询
			'74': 'Shell.class.rea.client.stock.seach.App',
			//入库对帐
			'75': 'Shell.class.rea.client.stock.reconciliations.App',
			//库存初始化
			'80': 'Shell.class.rea.client.stock.manualinput.App',
			//盘库管理
			'81': 'Shell.class.rea.client.inventory.App',
			//库存结转
			'82': 'Shell.class.rea.client.qtybalance.App',
			//结转报表
			'83': 'Shell.class.rea.client.monthly.App',
			//库存查询
			'84': 'Shell.class.rea.client.reastore.App',
			//库存预警
			'85': 'Shell.class.rea.client.qtywarning.App',
			//效期预警
			'86': 'Shell.class.rea.client.validitywarning.App',
			//批号性能验证
			'87': 'Shell.class.rea.client.goodslot.verification.App',
			//移库申请
			'90': 'Shell.class.rea.client.transfer.apply.App',
			//直接移库
			'91': 'Shell.class.rea.client.transfer.App?type=1',
			//申请后移库
			'92': 'Shell.class.rea.client.transfer.App?type=2',
			//直接移库及按移库申请进行确认移库
			'93': 'Shell.class.rea.client.transfer.App?type=3',
			//入库移库
			'94': 'Shell.class.rea.client.transfer.ofin.App',
			//出库申请
			'100': 'Shell.class.rea.client.out.apply.App?type=1',
			//出库申请+
			'101': 'Shell.class.rea.client.out.apply.App?type=2',
			//出库审核
			'102': 'Shell.class.rea.client.out.check.App?type=1',
			//出库审核+
			'103': 'Shell.class.rea.client.out.check.App?type=2',
			//出库审批
			'104': 'Shell.class.rea.client.out.approval.App',
			//直接出库
			'105': 'Shell.class.rea.client.out.use.App?type=1',
			//使用出库(支持直接出库及确认出库)
			'106': 'Shell.class.rea.client.out.use.App?type=3',
			//技师站出库登记
			'107': 'Shell.class.rea.client.out.enrollment.App',
			//简捷出库
			'108': 'Shell.class.rea.client.out.simple.App',
			//库存变化跟踪
			'120': 'Shell.class.rea.client.qtyoperation.App',
			//盒条码操作记录
			'130': 'Shell.class.rea.client.barcodeoperation.App',
			//仪器试剂使用量
			'140': 'Shell.class.rea.client.statistics.consume.equipreagent.App',
			//项目检测量
			'141': 'Shell.class.rea.client.statistics.consume.item.App',
			//理论消耗量
			'142': 'Shell.class.rea.client.statistics.consume.theory.App',
			//消耗比对分析
			'143': 'Shell.class.rea.client.statistics.consume.comparison.App'
		}
	};

	function onAfterLogin() {
		JcallShell.System.Date.init(function() {
			var params = JShell.Page.getParams(true),
				arr = _PARAMS.type[params.TYPE].split('?'),
				className = arr[0],
				configList = [];

			var config = {
				header: false,
				border: false
			};
			if(arr.length > 1) {
				configList = arr[1].split('&');
				for(var i in configList) {
					var vArr = configList[i].split('=');
					if(vArr.length == 2) {
						config[vArr[0].toLocaleUpperCase()] = vArr[1];
					}
				}
			}

			var content = Ext.create(className, config);
			view.add(content);

		});
	}

	//===============================================================
	//屏蔽右键菜单
	Ext.getDoc().on("contextmenu", function(e) {
		e.stopEvent();
	});
	//键盘监听
	if(document.addEventListener) {
		document.addEventListener("keydown", maskBackspace, true);
	} else {
		document.attachEvent("onkeydown", maskBackspace);
	}

	function maskBackspace(event) {
		var event = event || window.event; //标准化事件对象
		var obj = event.target || event.srcElement;
		var keyCode = event.keyCode ? event.keyCode : event.which ?
			event.which : event.charCode;
		if(keyCode == 8) {
			if(obj != null && obj.tagName != null && (obj.tagName.toLowerCase() == "input" ||
					obj.tagName.toLowerCase() == "textarea")) {
				event.returnValue = true;
				if(Ext.getCmp(obj.id)) {
					if(Ext.getCmp(obj.id).readOnly) {
						if(window.event) {
							event.returnValue = false; //or event.keyCode=0  
						} else {
							event.preventDefault(); //for ff
						}
					}
				}
			} else {
				if(window.event) {
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