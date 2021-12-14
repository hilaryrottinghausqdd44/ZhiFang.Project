/**
 * 项目选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.item.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '项目选择列表',
	width: 350,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBTestItemByHQL?isPlanish=true',
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 250,
			emptyText: '项目名称',
			isLike: true,
			fields: ['btestitem.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'BTestItem_CName',
			text: '项目名称',
			flex: 1,//width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BTestItem_IsCombiItem',
			text: '是否组套',
			width: 70,
			isBool: true,
			align: 'center',
			type: 'bool',defaultRenderer: true
		}, {
			dataIndex: 'DUnitItem_IsStepPrice',
			text: '是否阶梯价',
			width: 70,
			isBool: true,
			hidden: true,
			align: 'center',
			type: 'bool',
			editor: {
				xtype: 'uxBoolComboBox'
			}
		}, {
			dataIndex: 'BTestItem_Id',
			text: '项目ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BTestItem_DataTimeStamp',
			text: '项目时间戳',
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	}
});