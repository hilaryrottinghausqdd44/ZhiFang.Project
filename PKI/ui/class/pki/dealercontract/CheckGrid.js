/**
 * 经销商/送检单位项目选择列表
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealercontract.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '经销商/送检单位项目选择列表',
	width: 295,
	height: 500,
	/*已经选择的行*/
	selectRecords: [],
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DUnitItem_BTestItem_ItemCharge',
		direction: 'ASC'
	}],
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDUnitItemByHQL?isPlanish=true',
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	hiddenIsStepPrice:true,
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = me.searchInfo || {
			width: '100%',
			emptyText: '项目名称',
			isLike: true,
			fields: ['dunititem.BTestItem.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'DUnitItem_BTestItem_CName',
			text: '项目名称',
			flex:1,
			width: 220,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitItem_IsStepPrice',
			text: '是否阶梯价',
			width: 70,
			hidden:me.hiddenIsStepPrice,
			align: 'center',
			isBool: true,
			type: 'bool',
			editor: {
				xtype: 'uxBoolComboBox'
			}
		}, {
			dataIndex: 'DUnitItem_BTestItem_ItemCharge',
			text: '项目价格',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DUnitItem_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitItem_BTestItem_Id',
			text: '项目ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitItem_BTestItem_DataTimeStamp',
			text: '项目时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitItem_CoopLevel',
			text: '合作级别',
			hidden: true,
			hideable: false
		}];
		me.callParent(arguments);
	}
});