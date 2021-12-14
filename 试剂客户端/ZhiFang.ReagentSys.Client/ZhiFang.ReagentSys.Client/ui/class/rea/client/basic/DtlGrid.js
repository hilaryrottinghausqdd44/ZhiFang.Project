/**
 * 基本明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.RowExpander'
	],
	
	title: '明细列表',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否默认开启全屏模式*/
	isLaunchFullscreen: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
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
	}
});