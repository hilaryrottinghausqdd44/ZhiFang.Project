/**
 * 预览库存结转报表PDF
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.monthly.PdfPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '预览库存结转报表PDF',
	layout: 'border',
	width: 500,
	height: 400,
	
	hasBtntoolbar: true,
	PK: '',
	templetName: "",
	operateType: 1,
	isPreview: true,
	hasLoadMask: true,
	/**默认加载数据*/
	defaultLoad: false,
	autoScroll: true,
	/**是否默认开启全屏模式*/
	isLaunchFullscreen: false,

	selectUrl: '/ReaManageService.svc/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf',
	
	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			center: 1
		}
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad)
			me.showPdf();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		me.title = me.title || "预览库存结转报表PDF";
		me.MonthlyPDF = Ext.create('Ext.panel.Panel', {
			header: false,
			itemId: 'MonthlyPDF',
			region: 'center',
			split: false,
			collapsible: false,
			collapsed: false
		});
		me.items = [me.MonthlyPDF];
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createFullscreenItems: function() {
		var me = this;
		var items = [];
		//开启全屏、关闭全屏
		items.push({
			text: '开启全屏',
			itemId: 'launchFullscreen',
			iconCls: 'button-arrow-out',
			hidden: me.isLaunchFullscreen,
			handler: function() {
				me.launchFullscreen();
			}
		}, {
			text: '关闭全屏',
			itemId: 'exitFullscreen',
			iconCls: 'button-arrow-in',
			hidden: !me.isLaunchFullscreen,
			handler: function() {
				me.exitFullscreen();
			}
		});
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	/**开启全屏模式*/
	launchFullscreen: function() {
		var me = this,
			toolbar = me.getComponent('buttonsToolbar'),
			launchFullscreen = toolbar.getComponent('launchFullscreen'),
			exitFullscreen = toolbar.getComponent('exitFullscreen');
		JShell.Win.frame.launchFullscreen();
		launchFullscreen.hide();
		exitFullscreen.show();
		me.fireEvent('onLaunchFullScreen', me);
	},
	/**关闭全屏模式*/
	exitFullscreen: function() {
		var me = this,
			toolbar = me.getComponent('buttonsToolbar'),
			launchFullscreen = toolbar.getComponent('launchFullscreen'),
			exitFullscreen = toolbar.getComponent('exitFullscreen');
		JShell.Win.frame.exitFullscreen();
		exitFullscreen.hide();
		launchFullscreen.show();
		me.fireEvent('onExitFullScreen', me);
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.MonthlyPDF.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.MonthlyPDF.body.unmask();
		} //隐藏遮罩层
	},
	loadData: function(id) {
		var me = this;
		if(id)
			me.PK = id;
		me.showPdf(true);
	},
	clearData: function() {
		var me = this;
		me.MonthlyPDF.update("");
	},
	//查看PDF内容
	showPdf: function(isClear) {
		var me = this;
		if(!me.PK) {
			me.clearData();
			return;
		}
		me.showMask("正在加载中...");
		var a = '%22';
		var url = JShell.System.Path.ROOT + me.selectUrl + "?id=" + me.PK + "&frx=" + me.templetName + "&operateType=" + me.operateType + "&isPreview=" + me.isPreview;
		var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';

		if(url) {
			html = '<iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px;" ' + 'src=' + url + '>' +
				'</iframe>';
		}
		//if(isClear == true) html = '';
		me.MonthlyPDF.update(html);
		//me.update();
		me.hideMask();
	},
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items = me.createFullscreenItems();
		items.push('-');
		items.push({
			xtype: 'button',
			itemId: 'btnRefresh',
			iconCls: 'button-refresh',
			text: "刷新",
			tooltip: '刷新',
			handler: function() {
				me.showPdf()
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	}
});