/**
 * 下级机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorgcondition.ChildrenGrid',{
    extend:'Shell.class.rea.cenorgcondition.ParentGrid',
    title:'下级机构列表',
    
    /**创建查询框信息*/
	createSearchInfo:function(){
		return {
			width:160,emptyText:'机构编号/中文名/英文名',isLike:true,
			fields:[
				'cenorgcondition.cenorg2.OrgNo',
				'cenorgcondition.cenorg2.CName',
				'cenorgcondition.cenorg2.EName'
			]
		};
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
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
		}];
		
		return columns;
	},
	initDefaultWhere:function(){
		var me = this;
		me.defaultWhere = 'cenorgcondition.cenorg1.Id=' + me.CenOrgId;
	},
	getEntity:function(record){
		var me = this;
		return {
			cenorg2:{Id:record.get('CenOrg_Id')},
			cenorg1:{Id:me.CenOrgId}
		};
	}
});