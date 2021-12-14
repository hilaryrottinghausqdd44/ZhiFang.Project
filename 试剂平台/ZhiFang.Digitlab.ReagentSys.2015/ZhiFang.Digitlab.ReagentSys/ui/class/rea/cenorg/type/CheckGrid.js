/**
 * 机构类型选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.type.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'机构类型选择列表',
    width:320,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeByHQL?isPlanish=true',
    /**排序字段*/
	defaultOrderBy:[{property:'CenOrgType_DispOrder',direction:'ASC'}],
	
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
			text: '类型名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_ShortCode',
			text: '类型代码',
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
			dataIndex: 'CenOrgType_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		me.callParent(arguments);
	}
});