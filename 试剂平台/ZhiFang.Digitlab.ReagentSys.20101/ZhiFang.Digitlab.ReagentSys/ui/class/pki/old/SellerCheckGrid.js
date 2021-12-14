/**
 * 销售选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.SellerCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'销售选择列表',
    width:300,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBSellerByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:160,emptyText:'销售名称',isLike:true,fields:['bseller.CName']};
		//数据列
		me.columns = [{
			dataIndex:'BSeller_Name',text:'销售名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BSeller_Id',text:'销售ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BSeller_DataTimeStamp',text:'销售时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});