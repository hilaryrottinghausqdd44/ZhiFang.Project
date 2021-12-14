/**
 * 人员选择列表
 * @author Jcall
 * @version 2019-12-19
 */
Ext.define('Shell.class.basic.user.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'人员选择列表',
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true',
	//系统编码
	SystemCode:'ZF_LAB_START',
	
	//序号列宽度
	rowNumbererWidth:35,
    //是否单选
	checkOne:true,
	
	//类型编码
	TSysCode:null,
	
	//排序
	defaultOrderBy:[{property:'HREmpIdentity_DispOrder',direction:'ASC'}],
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += "hrempidentity.IsUse=1 and hrempidentity.SystemCode='" + me.SystemCode + "'";
		
		if(me.TSysCode){
			me.defaultWhere += " and hrempidentity.TSysCode='" + me.TSysCode + "'";
		}
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,fields:['hrempidentity.HREmployee.CName']};
		
		//数据列
		me.columns = [{
			text:'名称',dataIndex:'HREmpIdentity_HREmployee_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'编码',dataIndex:'HREmpIdentity_HREmployee_StandCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'HREmpIdentity_HREmployee_Id',isKey:true,hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	},
	//获取带查询参数的URL
	getLoadUrl: function(){
		var me = this,
			url = me.callParent(arguments),
			exp = new RegExp(JShell.System.Path.ROOT,"g");
		
		url = url.replace(exp,JShell.System.Path.LIIP_ROOT);
		
		return url;
	}
});