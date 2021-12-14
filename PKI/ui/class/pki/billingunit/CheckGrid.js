/**
 * 开票方选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.billingunit.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'开票方选择列表',
    width:280,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBBillingUnitByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:160,emptyText:'开票方名称/编号',isLike:true,fields:['bbillingunit.Name','bbillingunit.UseCode']};
		//数据列
		me.columns = [{
			dataIndex:'BBillingUnit_UseCode',text:'编号',width:60,defaultRenderer:true
		},{
			dataIndex:'BBillingUnit_Name',text:'开票方名称',width:150,defaultRenderer:true
		},{
			dataIndex:'BBillingUnit_Id',text:'ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BBillingUnit_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});