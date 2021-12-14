/**
 * 普通用户帮助系统查看列表
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.show.Grid', {
	extend: 'Shell.class.qms.file.help.basic.Grid',
	title: '帮助系统查看列表',
	checkOne:true,
	/**获取数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileByBDictTreeId?isPlanish=true',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});