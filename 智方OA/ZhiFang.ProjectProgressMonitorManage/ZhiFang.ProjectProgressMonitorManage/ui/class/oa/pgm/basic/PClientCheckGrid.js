/**
 * 厂家/用户选择列表
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.oa.pgm.basic.PClientCheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '厂家选择',
	width: 270,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PClient_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pclient.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称/快捷码',
			isLike: true,
			fields: ['pclient.Name','pclient.Shortcode']
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
			dataIndex: 'PClient_Name',
			//width: 100,
			flex:1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PClient_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});