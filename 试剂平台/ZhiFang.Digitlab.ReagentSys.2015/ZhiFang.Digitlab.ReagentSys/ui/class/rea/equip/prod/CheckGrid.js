/**
 * 厂商仪器选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.prod.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'厂商仪器选择列表',
    width:570,
    height:500,
    
    /**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchTestEquipProdByHQL?isPlanish=true',
    /**排序字段*/
	defaultOrderBy: [{
		property: 'TestEquipProd_DispOrder',
		direction: 'ASC'
	}],
	/**查询框信息*/
	searchInfo: {
		width: 160,
		emptyText: '中文名/英文名/代码',
		isLike: true,
		fields: ['testequipprod.CName', 'testequipprod.EName', 'testequipprod.ShortCode']
	},
	/**数据列*/
	columns: [{
		dataIndex: 'TestEquipProd_ProdOrg_CName',
		text: '厂商名称',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipProd_TestEquipType_CName',
		text: '仪器类型',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipProd_CName',
		text: '中文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipProd_EName',
		text: '英文名',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipProd_ShortCode',
		text: '代码',
		width: 100,
		defaultRenderer: true
	}, {
		dataIndex: 'TestEquipProd_Id',
		text: '主键ID',
		hidden: true,
		hideable: false,
		isKey: true
	}],
	
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' testequipprod.Visible=1';
		
		me.callParent(arguments);
	}
});