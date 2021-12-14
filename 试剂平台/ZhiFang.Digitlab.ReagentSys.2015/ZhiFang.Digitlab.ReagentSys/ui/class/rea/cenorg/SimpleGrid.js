/**
 * 简单机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.SimpleGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'简单机构列表',
    width:490,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'CenOrg_DispOrder',direction:'ASC'}],
	
	/**复选框*/
//	multiSelect: true,
//	selType: 'checkboxmodel',
//	hasDel: true,
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:180,emptyText:'中文名/英文名/代码/机构编号',isLike:true,
			fields:['cenorg.CName','cenorg.EName','cenorg.ShortCode','cenorg.OrgNo']};
			
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' cenorg.Visible=1';
			
		//数据列
		me.columns = me.columns || me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenOrg_OrgNo',
			text: '机构编号',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_CenOrgType_CName',
			text: '机构类型',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'CenOrg_Id',
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
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if(i == 'cenorg.OrgNo'){
				if(!isNaN(value)){
					where.push("cenorg.OrgNo=" + value);
				}
				continue;
			}
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	}
});