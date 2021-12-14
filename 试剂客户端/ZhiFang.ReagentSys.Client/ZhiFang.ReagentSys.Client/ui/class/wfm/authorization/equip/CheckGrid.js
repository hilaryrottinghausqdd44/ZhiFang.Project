/**
 * 仪器选择列表
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.authorization.equip.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '仪器选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipByHQL?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**仪器类型*/
	EquipTypeID: null,
	/**默认加载*/
	defaultLoad: false,
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bequip.IsUse=1';
		//查询框信息
		me.searchInfo = {
			width: '70%',
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
			width: 180,
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'SQH号',
			dataIndex: 'BEquip_Shortcode',
			width: 55,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器品牌',
			dataIndex: 'BEquip_EquipFactoryBrand_CName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '型号',
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
		}, {
			text: '时间戳',
			dataIndex: 'BEquip_DataTimeStamp',
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	},
	/*
	 * 按仪器类型获取仪器信息
	 **/
	loadGridByEquipTypeID: function(equipTypeID) {
		var me = this;
		if(equipTypeID == "-1") {
			me.defaultWhere = 'bequip.IsUse=1'; //-1为手工添加的全部,查询所有
		} else {
			me.defaultWhere = 'bequip.IsUse=1 and bequip.EquipType.Id=' + equipTypeID;
		}
		me.onSearch();
	}
});