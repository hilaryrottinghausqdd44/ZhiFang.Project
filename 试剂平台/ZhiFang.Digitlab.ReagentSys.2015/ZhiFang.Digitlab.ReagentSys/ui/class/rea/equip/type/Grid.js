/**
 * 仪器类型列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.type.Grid', {
	extend: 'Shell.ux.model.IsUseGrid',
	title: '仪器类型列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchTestEquipTypeByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelTestEquipType',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateTestEquipTypeByField',
	
	/**是否使用字段名*/
    IsUseField:'TestEquipType_Visible',
    /**是否使用字段的类型，bool/int，默认bool*/
    IsUseType:'int',
	
	/**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'TestEquipType_DispOrder',
		direction: 'ASC'
	}],
	/**查询框信息*/
	searchInfo: {
		width: 160,
		emptyText: '中文名/英文名/代码',
		isLike: true,
		fields: ['testequiptype.CName', 'testequiptype.EName', 'testequiptype.ShortCode']
	},
	/**数据列*/
	columns: [{
		dataIndex: 'TestEquipType_CName',
		text: '中文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipType_EName',
		text: '英文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipType_ShortCode',
		text: '代码',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipType_Memo',
		text: '备注',
		width: 100,
		sortable: false,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipType_DispOrder',
		text: '次序',
		width: 50,
		align: 'center',
		type: 'int',
		defaultRenderer: true
	}, {
		xtype: 'checkcolumn',
		text: '使用',
		dataIndex: 'TestEquipType_Visible',
		width: 40,
		align: 'center',
		//sortable: false,
		menuDisabled: true,
		stopSelection: false,
		type: 'boolean'
	}, {
		dataIndex: 'TestEquipType_DataAddTime',
		text: '新增时间',
		width: 130,
		isDate:true,
		hasTime:true,
		type:'date',
		hidden: true
	}, {
		dataIndex: 'TestEquipType_DataUpdateTime',
		text: '修改时间',
		width: 130,
		isDate:true,
		hasTime:true,
		type:'date',
		hidden: true
	}, {
		dataIndex: 'TestEquipType_Id',
		text: '主键ID',
		hidden: true,
		hideable: false,
		isKey: true
	}]
});