/**
 * 简捷出库
 * @author longfc
 * @version 2019-03-30
 */
Ext.define('Shell.class.rea.client.out.simple.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '简捷出库',

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			save: function() {
				//JShell.Msg.alert('保存成功', null, 2000);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.simple.DtlGrid', {
			header: false,
			region: 'center',
			itemId: 'DtlGrid'
		});
		return [me.DtlGrid];
	}
});