/**
 * 区域列表
 * @author ghx
 * @version 2021-01-11
 */
Ext.define('Shell.class.weixin.clientele.dic.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '站点列表',
	width: 270,
	height: 300,
	itemId:'CheckGrid',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaByHQL?isPlanish=true',
	isWinOpen:false,
	/**是否单选*/
	checkOne: true,
	/**默认选中*/
	autoSelect: true,
	initComponent: function () {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';

		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}

		//查询框信息
		me.searchInfo = {
			width: 145, emptyText: '名称/简称', isLike: true,
			fields: ['clientelearea.AreaCName', 'clientelearea.AreaShortName']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;

		var columns = [{
			text: '区域名称', dataIndex: 'ClientEleArea_AreaCName', width: 100,
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '快捷码', dataIndex: 'ClientEleArea_AreaShortName', width: 100,
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '主键ID', dataIndex: 'ClientEleArea_Id', isKey: true, hidden: true, hideable: false
		}];
		return columns;
	}
});