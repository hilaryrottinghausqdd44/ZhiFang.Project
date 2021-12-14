/**
 * 下级机构选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.ChildrenCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'下级机构选择列表',
    width:590,
    height:500,
    /**本机构ID*/
	CenOrgId:null,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionByHQL?isPlanish=true',
	
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' cenorgcondition.cenorg1.Id=' + me.CenOrgId;
		
		//查询框信息
		me.searchInfo = {
			width:160,emptyText:'机构编号/中文名/英文名',isLike:true,
			fields:[
				'cenorgcondition.cenorg2.OrgNo',
				'cenorgcondition.cenorg2.CName',
				'cenorgcondition.cenorg2.EName'
			]
		};
		//数据列
		me.columns = [{
			dataIndex: 'CenOrgCondition_cenorg2_OrgNo',
			text: '机构编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_CenOrgType_CName',
			text: '机构类型',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_Id',
			text: '下级机构ID',
			hidden: true,
			hideable: false
		}];
		
		me.callParent(arguments);
	}
});