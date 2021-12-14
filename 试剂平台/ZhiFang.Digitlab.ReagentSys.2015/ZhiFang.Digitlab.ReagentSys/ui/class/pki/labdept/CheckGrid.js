/**
 * 送检单位科室选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.labdept.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'送检单位科室选择列表',
    
    width:270,
    height:400,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBLabDeptByHQL?isPlanish=true',
	
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:150,emptyText:'科室名称',isLike:true,fields:['blabdept.BDept.CName']};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{type:'search',info:me.searchInfo}];
		//数据列
		me.columns = [{
			dataIndex:'BLabDept_BLaboratory_CName',text:'送检单位名称',width:100,defaultRenderer:true
		},{
			dataIndex:'BLabDept_BDept_CName',text:'科室名称',width:100,defaultRenderer:true
		},{
			dataIndex:'BLabDept_BDept_Id',text:'科室主键ID',hidden:true,hideable:false
		},{
			dataIndex:'BLabDept_BDept_DataTimeStamp',text:'科室时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});