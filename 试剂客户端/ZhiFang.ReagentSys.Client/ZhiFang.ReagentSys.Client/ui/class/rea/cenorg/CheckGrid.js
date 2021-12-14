/**
 * 机构选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'机构选择列表',
    width:310,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
    /**排序字段*/
	defaultOrderBy:[{property:'CenOrg_DispOrder',direction:'ASC'}],
	
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' cenorg.Visible=1';
		
		//查询框信息
		me.searchInfo = {width:160,emptyText:'机构编号/名称',isLike:true,fields:['cenorg.OrgNo','cenorg.CName']};
		//数据列
		me.columns = [{
			dataIndex:'CenOrg_OrgNo',text:'机构编号',width:80,defaultRenderer:true
		},{
			dataIndex:'CenOrg_CName',text:'机构名称',width:100,defaultRenderer:true
		},{
			dataIndex: 'CenOrg_CenOrgType_CName',
			text: '机构类型',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex:'CenOrg_Id',text:'ID',hidden:true,hideable:false,isKey:true
		}];
		
		me.callParent(arguments);
	}
});