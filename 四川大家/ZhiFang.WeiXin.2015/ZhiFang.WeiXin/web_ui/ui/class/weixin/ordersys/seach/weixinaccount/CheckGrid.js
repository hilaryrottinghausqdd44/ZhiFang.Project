/**
 * 用户选择列表
 * @author liangyl	
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.seach.weixinaccount.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'用户选择列表',
    width:370,
    height:300,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		

		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称/身份证号',isLike:true,
			fields:['bweixinaccount.IDNumber','bweixinaccount.UserName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'BWeiXinAccount_UserName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'身份证号',dataIndex:'BWeiXinAccount_IDNumber',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医保卡号',dataIndex:'BWeiXinAccount_MediCare',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BWeiXinAccount_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});