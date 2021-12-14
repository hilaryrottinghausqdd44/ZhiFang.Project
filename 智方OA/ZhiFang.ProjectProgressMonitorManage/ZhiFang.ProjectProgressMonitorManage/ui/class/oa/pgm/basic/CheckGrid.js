/**
 * 程序选择列表
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.oa.pgm.basic.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '程序选择',
	width: 270,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PGMProgram_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pgmprogram.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '程序标题/版本号',
			isLike: true,
			fields: ['pgmprogram.Title','pgmprogram.VersionNo']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '标题',
			dataIndex: 'PGMProgram_Title',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '版本号',
			dataIndex: 'PGMProgram_VersionNo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PGMProgram_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});