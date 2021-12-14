/**
 * 程序通用表单
 * @author longfc
 * @version 2016-09-29
 */
Ext.define('Shell.class.oa.pgm.program.release.common.Form', {
	extend: 'Shell.class.oa.pgm.basic.Form',
	/*程序类型:1为通用;2为定制通讯;3为通讯模板*/
	PROGRAMTYPE: '1',
	titleEmptyText: '必填项 ', // 命名规则(通讯-生化-日立-模板名称-版本号-通讯程序版本号)
	
	/**@overwrite 获取列表布局组件附件行*/
	getAddTableLayoutOfSQH: function(items) {
		var me = this;
		me.PGMProgram_SQH.colspan =4;
		me.PGMProgram_SQH.width = me.defaults.width * me.PGMProgram_SQH.colspan;
		items.push(me.PGMProgram_SQH);

		return items;
	}
});