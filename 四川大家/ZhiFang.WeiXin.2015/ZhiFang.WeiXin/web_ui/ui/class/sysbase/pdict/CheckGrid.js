/**
 * 字典选择列表
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.class.sysbase.pdict.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '字典选择列表',
	width: 270,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PDict_DispOrder',
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
		me.defaultWhere += 'pdict.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['pdict.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '名称',
			dataIndex: 'PDict_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'PDict_Memo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PDict_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});