/**
 * 微信员工关系列表
 * @author Jcall
 * @version 2017-08-29
 */
Ext.define('Shell.class.sysbase.user.weixin.link.Grid',{
    extend:'Shell.ux.grid.Panel',
    title:'微信员工关系列表',
    width:800,
    height:600,
    
    /**获取数据服务路径*/
	selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLinkByHQL?isPlanish=true',
	/**新增关系数据服务路径*/
	addLinkUrl:'/WeiXinAppService.svc/ST_UDTO_AddBWeiXinEmpLink',
	/**删除关系数据服务路径*/
	delLinkUrl:'/WeiXinAppService.svc/ST_UDTO_DelBWeiXinEmpLink',
	
	/**默认加载数据*/
	defaultLoad: true,
	
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent:function(){
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width: 100,
			emptyText: '员工/昵称',
			itemId:'search',
			isLike: true,
			fields: ['bweixinemplink.EmpName','bweixinemplink.BWeiXinAccount.UserName']
		};
		me.buttonToolbarItems = ['refresh','-',{
			type: 'search',
			info: me.searchInfo
		},'-','->', {
			text:'绑定',
			tooltip:'将用户与微信绑定',
			iconCls:'button-save',
			itemId:'AddLinkButton',
			disabled:true,
			handler:function(){me.onAddLinkClick();}
		}, {
			text:'解绑',
			tooltip:'将用户与微信解绑',
			iconCls:'button-save',
			itemId:'DelLinkButton',
			disabled:true,
			handler:function(){me.onDelLinkClick();}
		}];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'员工',dataIndex:'BWeiXinEmpLink_EmpName',width:120,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'昵称',dataIndex:'BWeiXinEmpLink_BWeiXinAccount_UserName',width:120,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'手机号',dataIndex:'BWeiXinEmpLink_BWeiXinAccount_MobileCode',width:100,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'主键ID',dataIndex:'BWeiXinEmpLink_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'员工ID',dataIndex:'BWeiXinEmpLink_EmpID',hidden:true,hideable:false
		},{
			text:'微信头像',dataIndex:'BWeiXinEmpLink_BWeiXinAccount_HeadImgUrl',hidden:true,hideable:false
		}];
		
		return columns;
	},
	showRenderer:function(v,meta,record){
		var html = 
		"<div style='text-align:center;padding:5px;'>" +
			"<img style='width:64px;margin:5px;' alt='没有头像' src='" + record.get('BWeiXinEmpLink_BWeiXinAccount_HeadImgUrl') + "'/>" +
        	"<p>" + record.get('BWeiXinEmpLink_BWeiXinAccount_UserName') + "</p>" +
		"</div>";
		
		meta.tdAttr = 'data-qtip="' + html + '"';
		
		return v;
	}
});