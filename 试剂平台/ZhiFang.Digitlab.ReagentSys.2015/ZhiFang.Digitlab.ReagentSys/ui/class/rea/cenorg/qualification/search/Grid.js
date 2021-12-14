/***
 *  资质证件查看
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.search.Grid', {
	extend: 'Shell.class.rea.cenorg.qualification.basic.Grid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '资质证件查看',

	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.defaultWhere = 'goodsqualification.Visible=1';
		me.callParent(arguments);
	}
});