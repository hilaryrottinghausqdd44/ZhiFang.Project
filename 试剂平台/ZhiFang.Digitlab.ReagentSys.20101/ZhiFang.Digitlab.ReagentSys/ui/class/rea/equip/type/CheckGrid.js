/**
 * 仪器类型选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.type.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'仪器类型选择列表',
    width:420,
    height:500,
    
    /**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchTestEquipTypeByHQL?isPlanish=true',
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
		width: 150,
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
		dataIndex: 'TestEquipType_Id',
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
		me.defaultWhere += ' testequiptype.Visible=1';
		
		me.callParent(arguments);
	}
});