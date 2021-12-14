/**
 * 血液单位选择
 * @author longfc
 * @version 2020-04-10
 */
Ext.define('Shell.class.sysbase.bloodunit.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '血液单位选择',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BloodUnit_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	searchInfoVal: "",

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'BloodUnit.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '编码/名称/快捷码',
			isLike: true,
			itemId: "search",
			value: me.searchInfoVal,
			fields: ['bloodunit.Id', 'bloodunit.CName', 'bloodunit.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '编码',
			dataIndex: 'BloodUnit_Id',
			width: 100,
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'BloodUnit_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'BloodUnit_ShortCode',
			width: 100,
			defaultRenderer: true
		}]

		return columns;
	},
	setSearchValue: function(value, isSearch) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		if (search) {
			search.setValue(value);
			if (isSearch) me.onSearch();
		}
	}
});
