/***
 *  预览资质证件文件
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.basic.PreviewPDF', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '预览资质证件文件',
	layout: 'border',
	width: 500,
	height: 400,
	hasBtn: true,
	hasBtntoolbar: true,
	PK: '',
	operateType: 1,
	isPreview: true,
	hasLoadMask: true,
	/**默认加载数据*/
	defaultLoad: true,
	autoScroll: false,
	selectUrl: '/ReagentSysService.svc/ST_UDTO_GoodsQualificationPreviewPdf',
	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			center: 1,
		}
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad) {
			me.showPdf();
		}
	},
	initComponent: function() {
		var me = this;
		me.items = [];
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	//查看PDF内容
	showPdf: function(isClear) {
		var me = this;
		me.showMask("正在加载中...");
		var a = '%22';
		var url = JShell.System.Path.ROOT + me.selectUrl;
		var where = "?id=" + me.PK + "&operateType=" + me.operateType + "&isPreview=" + me.isPreview;
		url = url + where; // JcallShell.String.encode();
		var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
		if(url) {
			html = '<iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px;" ' + 'src=' + url + '>' +
				'</iframe>';
		}
		if(isClear == true) html = '';
		me.update(html);
		me.hideMask();
	},
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			items.push('->');
			items.push({
				xtype: 'button',
				itemId: 'btnRefresh',
				iconCls: 'button-refresh',
				text: "刷新",
				tooltip: '刷新',
				handler: function() {
					me.showPdf()
				}
			}, {
				xtype: 'button',
				itemId: 'btnColse',
				iconCls: 'button-del',
				text: "关闭",
				tooltip: '关闭',
				handler: function() {
					me.fireEvent('onCloseClick', me);
					me.close();
				}
			});
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	}
});