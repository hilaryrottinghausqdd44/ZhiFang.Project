/**
 * 机构类型列表
 * @author Jcall
 * @version 2015-07-02
 * @version 2017-08-10
 */
Ext.define('Shell.classlang.rea.cenorg.type.Grid',{
    extend:'Shell.ux.grid.Panel',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelCenOrgType',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'CenOrgType_DispOrder',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:160,isLike:true,
			emptyText: 
				JcallShell.Lang.CENORG_TYPE_CNAME + '/' + 
				JcallShell.Lang.CENORG_TYPE_ENAME + '/' + 
				JcallShell.Lang.CENORG_TYPE_SHORTCODE,
			fields:['cenorgtype.CName','cenorgtype.EName','cenorgtype.ShortCode']};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenOrgType_CName',
			text: JcallShell.Lang.CENORG_TYPE_CNAME,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_EName',
			text: JcallShell.Lang.CENORG_TYPE_ENAME,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_ShortCode',
			text: JcallShell.Lang.CENORG_TYPE_SHORTCODE,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_Memo',
			text: JcallShell.Lang.CENORG_TYPE_MEMO,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_DispOrder',
			text: JcallShell.Lang.CENORG_TYPE_DISPORDER,
			width: 50,
			align:'center',
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrgType_Visible',
			text: JcallShell.Lang.CENORG_TYPE_VISIBLE,
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'CenOrgType_Id',
			text: 'KEY_ID',
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