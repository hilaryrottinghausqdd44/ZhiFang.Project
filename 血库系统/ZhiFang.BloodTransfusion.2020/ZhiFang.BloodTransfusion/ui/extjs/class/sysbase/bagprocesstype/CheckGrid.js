/**
 * 加工类型
 * @author panjie
 * @version 2020-08-06
 */
Ext.define('Shell.class.sysbase.bagprocesstype.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '加工类型',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/BT_UDTO_SearchBloodBagProcessTypeByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BloodBagProcessType_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bloodbagprocesstype.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['bloodbagprocesstype.CName']
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
			dataIndex: 'BloodBagProcessType_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'ShortCode',
			dataIndex: 'BloodBagProcessType_ShortCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BloodBagProcessType_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});
