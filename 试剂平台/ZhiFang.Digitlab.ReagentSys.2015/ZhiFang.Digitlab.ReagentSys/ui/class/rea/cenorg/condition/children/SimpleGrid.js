/**
 * 简单下级机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.condition.children.SimpleGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'简单下级机构列表',
    width:280,//420,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionByHQL?isPlanish=true',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:false,
	/**排序字段*/
	//defaultOrderBy:[{property:'CenOrgCondition',direction:'ASC'}],
	
	/**复选框*/
//	multiSelect: true,
//	selType: 'checkboxmodel',
//	hasDel: true,
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:160,emptyText:'机构编号/中文名/英文名',isLike:true,
			fields:[
				'cenorgcondition.cenorg2.OrgNo',
				'cenorgcondition.cenorg2.CName',
				'cenorgcondition.cenorg2.EName'
			]
		};
			
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'cenorgcondition.cenorg2.Visible=1';
			
		//数据列
		me.columns = me.columns || me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenOrgCondition_cenorg2_OrgNo',
			text: '机构编号',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_CName',
			text: '中文名',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_CenOrgType_CName',
			text: '机构类型',
			width: 60,
			defaultRenderer: true,
			hidden: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_EName',
			text: '英文名',
			width: 80,
			defaultRenderer: true,
			hidden: true
		},{
			dataIndex: 'CenOrgCondition_cenorg2_Id',
			text: '下级机构主键ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'CenOrgCondition_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick',me,records[0].get(me.PKField));
	}
});