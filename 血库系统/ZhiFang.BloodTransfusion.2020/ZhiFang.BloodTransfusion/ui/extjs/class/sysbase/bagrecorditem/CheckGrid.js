/**
 * 血袋记录项字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.bagrecorditem.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '血袋记录项字典',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordItemByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BloodBagRecordItem_DispOrder',
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
		me.defaultWhere += 'bloodbagrecorditem.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['bloodbagrecorditem.CName']
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
			dataIndex: 'BloodBagRecordItem_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '编码',
			dataIndex: 'BloodBagRecordItem_TransItemCode',
			width: 80,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BloodBagRecordItem_DispOrder',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BloodBagRecordItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});