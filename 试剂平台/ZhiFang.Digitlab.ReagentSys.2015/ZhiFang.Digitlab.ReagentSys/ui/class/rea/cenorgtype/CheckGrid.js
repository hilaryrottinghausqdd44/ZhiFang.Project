/**
 * 机构类型选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgtype.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'机构类型选择列表',
    width:550,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'cenorgtype.Visible=1';
		
		//查询框信息
		me.searchInfo = {width:160,emptyText:'中文名/英文名/代码',isLike:true,
			fields:['cenorgtype.CName','cenorgtype.EName','cenorgtype.ShortCode']};
		//数据列
		me.columns = [{
			dataIndex: 'CenOrgType_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_ShortCode',
			text: '代码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'CenOrgType_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		me.callParent(arguments);
	}
});