/**
 * 实验室 仪器列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.lab.Grid',{
	extend: 'Shell.ux.model.IsUseGrid',
	title: '实验室 仪器列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchTestEquipLabByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelTestEquipLab',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateTestEquipLabByField',
	
	/**是否使用字段名*/
    IsUseField:'TestEquipLab_Visible',
    /**是否使用字段的类型，bool/int，默认bool*/
    IsUseType:'int',
	
	/**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'TestEquipLab_DispOrder',
		direction: 'ASC'
	}],
	/**查询框信息*/
	searchInfo: {
		width: 160,
		emptyText: '中文名/英文名/代码',
		isLike: true,
		fields: ['testequiplab.CName', 'testequiplab.EName', 'testequiplab.ShortCode']
	},
	/**数据列*/
	columns: [{
//		dataIndex: 'TestEquipLab_Lab_CName',
//		text: '实验室',
//		width: 100,
//		//sortable: false,
//		defaultRenderer: true
//	}, {
		dataIndex: 'TestEquipLab_CName',
		text: '中文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_EName',
		text: '英文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_TestEquipType_CName',
		text: '仪器分类',
		width: 100,
		//sortable: false,
		defaultRenderer: true
	},  {
		dataIndex: 'TestEquipLab_ProdOrg_CName',
		text: '仪器厂商',
		width: 100,
		//sortable: false,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_LisCode',
		text: 'LIS仪器编号',
		width: 100,
		defaultRenderer: true
	}, {
		xtype: 'checkcolumn',
		text: '使用',
		dataIndex: 'TestEquipLab_Visible',
		width: 40,
		align: 'center',
		//sortable: false,
		menuDisabled: true,
		stopSelection: false,
		type: 'boolean'
	}, {
		dataIndex: 'TestEquipLab_Comp_CName',
		text: '供应商',
		width: 100,
		//sortable: false,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_TestEquipProd_CName',
		text: '厂商仪器',
		width: 100,
		//sortable: false,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_ShortCode',
		text: '代码',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_Memo',
		text: '备注',
		width: 100,
		//sortable: false,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_DispOrder',
		text: '次序',
		width: 50,
		align: 'center',
		type: 'int',
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipLab_DataAddTime',
		text: '新增时间',
		width: 130,
		isDate:true,
		hasTime:true,
		type:'date',
		hidden: true
	}, {
		dataIndex: 'TestEquipLab_DataUpdateTime',
		text: '修改时间',
		width: 130,
		isDate:true,
		hasTime:true,
		type:'date',
		hidden: true
	}, {
		dataIndex: 'TestEquipLab_Id',
		text: '主键ID',
		hidden: true,
		hideable: false,
		isKey: true
	}]
});