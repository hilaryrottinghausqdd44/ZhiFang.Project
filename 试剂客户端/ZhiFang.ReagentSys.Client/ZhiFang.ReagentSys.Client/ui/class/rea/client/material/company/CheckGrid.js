/**
 * 供应商选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.material.company.CheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'机构选择列表',
    width:500,
    height:300,
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:false,
	/**已存在的供应商*/
	Ids:null,
	defaultWhere :'reacenorg.OrgType=0',
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
			emptyText:'供应商编码/中文名/代码',
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
			dataIndex: 'ReaCenOrg_OrgNo',text: '供应商编码',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_CName',text: '供应商名称',width: 200,defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_ShortCode',text: '代码',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Id',text: '主键ID',hidden: true,hideable: false,isKey: true
		},{
			dataIndex: 'ReaCenOrg_PlatformOrgNo',text: '平台机构码',width: 100,defaultRenderer: true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		me.internalWhere = "";
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if(me.Ids) {
			params.push('reacenorg.Id not in ('+me.Ids+')');
		}
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	}
	
});