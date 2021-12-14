/**
 * 机构选择列表
 * @author liangyl
 * @version 2017-11-10
 */
Ext.define('Shell.class.rea.client.cenorg.basic.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'机构选择列表',
    width:500,
    height:300,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
    
    /**是否单选*/
	checkOne:false,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reacenorg.Visible=1';
		
		//查询框信息
		me.searchInfo = {
			width:145,isLike:true,itemId: 'Search',
			emptyText:'机构编号/中文名/代码',
			fields:['reacenorg.OrgNo','reacenorg.CName','reacenorg.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'ReaCenOrg_OrgNo',text: '机构编号',width: 60,defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_CName',text: '名称',width: 100,align: 'center',defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_ShortCode',text: '代码',width: 60,defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Id',text: '主键ID',hidden: true,hideable: false,isKey: true
		}];
		
		return columns;
	}
});