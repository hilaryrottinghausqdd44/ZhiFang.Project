/**
 * 上级机构选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.ParentCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'上级机构选择列表',
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
		me.defaultWhere += ' cenorgcondition.cenorg2.Id=' + me.CenOrgId;
		
		//查询框信息
		me.searchInfo = {
			width:160,emptyText:'机构编号/中文名/英文名',isLike:true,
			fields:[
				'cenorgcondition.cenorg1.OrgNo',
				'cenorgcondition.cenorg1.CName',
				'cenorgcondition.cenorg1.EName'
			]
		};
		//数据列
		me.columns = [{
			dataIndex: 'CenOrgCondition_cenorg1_OrgNo',
			text: '机构编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg1_CenOrgType_CName',
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
			dataIndex: 'CenOrgCondition_cenorg1_Id',
			text: '上级机构ID',
			hidden: true,
			hideable: false
		}];
		
		me.callParent(arguments);
	}
});