/**
 * 开单医生选择列表
 * @author liangyl	
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.doctor.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'开单医生选择列表',
    width:320,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['bdoctoraccount.Name']};
			//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'医生名称',dataIndex:'BDoctorAccount_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医生工号',dataIndex:'BDoctorAccount_HWorkNumberID',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BDoctorAccount_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});