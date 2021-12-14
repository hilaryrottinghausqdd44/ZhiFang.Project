/**
 * 公共操作记录
 * @author longfc
 * @version 2017-11-07
 */
Ext.define('Shell.class.rea.client.goodsorglink.cenorg.scoperation.SCOperation', {
	extend: 'Shell.class.rea.client.scoperation.SCOperation',
	title: '供应商与货品信息操作记录',
	autoScroll: true,

	bodyPadding: 10,

	classNameSpace: 'ZhiFang.Digitlab.Entity.ReagentSys', //类域
	className: 'ReaGoodsOrgLinkStatus', //类名
	/**业务对象ID*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	}
});