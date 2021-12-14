/**
 * 仪器选择列表
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.sysbase.bequip.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '仪器选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBEquipByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'BEquip_DispOrder',
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
		me.defaultWhere += 'bequip.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['bequip.CName']
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
			dataIndex: 'BEquip_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器分类',
			dataIndex: 'PGMProgram_EquipType_CName',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器品牌',
			dataIndex: 'BEquip_EquipFactoryBrand_CName',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器型号',
			dataIndex: 'BEquip_Equipversion',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BEquip_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	}
});