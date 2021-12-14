/**
 * 销售选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.seller.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'销售选择列表',
    width:270,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBSellerByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:150,emptyText:'销售名称/编号',isLike:true,fields:['bseller.CName','bseller.UseCode']};
		//数据列
		me.columns = [{
			dataIndex:'BSeller_UseCode',text:'编号',width:60,defaultRenderer:true
		},{
			dataIndex:'BSeller_Name',text:'销售名称',width:140,defaultRenderer:true
		},{
			dataIndex:'BSeller_Id',text:'销售ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BSeller_DataTimeStamp',text:'销售时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});