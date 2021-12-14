/**
 * 小组选择列表
 * @author Jcall
 * @version 2019-11-18
 */
Ext.define('Shell.class.lts.section.role.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'小组选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true',
	//排序字段
	defaultOrderBy: [{property:'LBRight_LBSection_DispOrder',direction:'ASC'}],
    //是否单选
	checkOne:false,
	//已选择过的小组
	checkedIds:null,
	//员工ID
    userId:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		if(me.checkedIds){
			me.defaultWhere += 'lbright.LBSection.Id not in(' + me.checkedIds + ') and ';
		}
		me.defaultWhere += 'lbright.RoleID is null and lbright.EmpID=' + me.userId;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'小组名称/编码',isLike:true,
			fields:['lbright.LBSection.CName','lbright.LBSection.UseCode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'小组名称',dataIndex:'LBRight_LBSection_CName',width:100,defaultRenderer:true
		},{
			text:'小组编码',dataIndex:'LBRight_LBSection_UseCode',width:100,defaultRenderer:true
		},{
			text:'排序',dataIndex:'LBRight_LBSection_DispOrder',width:60,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBRight_LBSection_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	}
});