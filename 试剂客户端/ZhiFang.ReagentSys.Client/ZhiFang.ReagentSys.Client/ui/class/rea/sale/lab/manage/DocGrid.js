/**
 * 供货单管理-主单列表-实验室专用
 * @author Jcall
 * @version 2018-01-12
 */
Ext.define('Shell.class.rea.sale.lab.manage.DocGrid', {
	extend: 'Shell.class.rea.sale.comp.manage.DocGrid',
	title: '供货单管理-主单列表-实验室专用',
	
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
		
		var defaultWhere = me.defaultWhere.split('bmscensaledoc.Comp.Id=');
		me.defaultWhere = defaultWhere[0] + 'bmscensaledoc.Lab.Id=' + JShell.REA.System.CENORG_ID;
	}
});