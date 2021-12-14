/**
 * 经销商选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.DealerCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'经销商选择列表',
    width:370,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBDealerByHQL?isPlanish=true',
	
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:245,emptyText:'经销商名称',isLike:true,fields:['bdealer.Name']};
		//数据列
		me.columns = [{
			dataIndex:'BDealer_Name',text:'经销商名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BDealer_BBillingUnit_Name',text:'默认开票方',defaultRenderer:true
		},{
			dataIndex:'BDealer_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BDealer_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		},{
			dataIndex:'BDealer_BBillingUnit_Id',text:'默认开票方主键ID',hidden:true,hideable:false
		},{
			dataIndex:'BDealer_BBillingUnit_DataTimeStamp',text:'默认开票方时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});