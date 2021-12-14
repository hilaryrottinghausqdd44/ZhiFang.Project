/**
 * 仪器选择列表
 * @author zhangda
 * @version 2020-04-13
 */
Ext.define('Shell.class.lts.sample.result.equip.extract.EquipCheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '仪器选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**默认选中*/
	autoSelect: true,
	initComponent: function () {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';

		//查询框信息
		me.searchInfo = {
			width: 145, emptyText: '仪器名称/仪器简称', isLike: true,
			fields: ['lbequip.CName', 'lbequip.SName']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;

		var columns = [{
			text: '仪器名称', dataIndex: 'LBEquip_CName', width: 100,
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '简称', dataIndex: 'LBEquip_SName', width: 100, 
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '快捷码', dataIndex: 'LBEquip_Shortcode', width: 100,
			sortable: false, menuDisabled: true, defaultRenderer: true
		}, {
			text: '主键ID', dataIndex: 'LBEquip_Id', isKey: true, hidden: true, hideable: false
		}];
		return columns;
	}
});