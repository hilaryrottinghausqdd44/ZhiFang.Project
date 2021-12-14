/**
 * 程序定制通讯表单
 * @author longfc
 * @version 2016-09-29
 */
Ext.define('Shell.class.oa.pgm.program.release.customize.Form', {
	extend: 'Shell.class.oa.pgm.basic.Form',
	/*程序类型:1为通用;2为定制通讯;3为通讯模板*/
	PROGRAMTYPE: '2',
	titleEmptyText: '必填项 ', // 命名规则(通讯-生化-日立-模板名称-版本号-通讯程序版本号)
	
	/**@overwrite 获取列表布局组件附件行*/
	getAddTableLayoutOfFileNameRow: function(items) {
		var me = this;
		me.PGMProgram_BEquip_CName.colspan = 1;
		me.PGMProgram_BEquip_CName.width = me.defaults.width * me.PGMProgram_BEquip_CName.colspan;
		items.push(me.PGMProgram_BEquip_CName);

		me.PGMProgram_File.colspan = 3;
		me.PGMProgram_File.width = me.defaults.width * me.PGMProgram_File.colspan;
		items.push(me.PGMProgram_File);
		switch(me.formtype) {
			case 'edit':
				me.PGMProgram_FileName.colspan = 4;
				me.PGMProgram_FileName.width = me.defaults.width * me.PGMProgram_FileName.colspan;
				items.push(me.PGMProgram_FileName);
				break;
			default:

				break;
		}
		return items;
	}
});