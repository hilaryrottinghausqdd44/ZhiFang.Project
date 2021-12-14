/**
 * 科室选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.DeptCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'科室选择列表',
    width:270,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBDeptByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {emptyText:'科室名称',isLike:true,fields:['bdept.CName']};
		//数据列
		me.columns = [{
			dataIndex:'BDept_CName',text:'科室名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BDept_Id',text:'ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BDept_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});