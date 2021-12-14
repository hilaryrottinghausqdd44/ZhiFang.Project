/**
 * 客户端主订单信息列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.apply.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	height: 340,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**下拉状态默认值*/
	defaultStatusValue: "0",
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.defaultWhere = me.defaultWhere || '';
		//var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//if(me.defaultWhere) me.defaultWhere = '(' + me.defaultWhere + ') and ';
		me.defaultWhere = 'bmscenorderdoc.ReaCompID is not null';// and bmscenorderdoc.UserID=' + userId;
		me.callParent(arguments);
	}
});