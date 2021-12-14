/**
 * 员工微信关系绑定
 * @author Jcall
 * @version 2017-02-27
 */
Ext.define('Shell.class.sysbase.user.weixin.LinkGrid',{
    extend:'Shell.ux.grid.Panel',
    requires:['Ext.ux.RowExpander'],
    title:'员工微信绑定',
    width:300,
    height:600,
    
    /**获取数据服务路径*/
	selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?isPlanish=true',
	/**获取关系数据服务路径*/
	selectLinkUrl:'/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLinkByHQL',
	/**新增关系数据服务路径*/
	addLinkUrl:'/WeiXinAppService.svc/ST_UDTO_AddBWeiXinEmpLink',
	/**删除关系数据服务路径*/
	delLinkUrl:'/WeiXinAppService.svc/ST_UDTO_DelBWeiXinEmpLink',
	
	/**用户ID*/
	EmpId:null,
	/**用户名*/
	EmpName:null,
	/**存在关系数据*/
	hasLinkData:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//加载关系数据
		me.loadLinkData();
	},
	
	initComponent:function(){
		var me = this;
		
		//默认不加载微信列表数据
		me.defaultLoad = false;
		
		//查询框信息
		me.searchInfo = {
			width: 100,
			emptyText: '昵称',
			itemId:'search',
			isLike: true,
			fields: ['bweixinaccount.UserName']
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
			text:'昵称',dataIndex:'BWeiXinAccount_UserName',width:120,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'手机号',dataIndex:'BWeiXinAccount_MobileCode',width:100,
			sortable:false,menuDisabled:true,renderer:me.showRenderer
		},{
			text:'微信头像',dataIndex:'BWeiXinAccount_HeadImgUrl',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BWeiXinAccount_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'关系主键ID',dataIndex:'LinkId',hidden:true,hideable:false
		}];
		
		return columns;
	},
	showRenderer:function(v,meta,record){
		var html = 
		"<div style='text-align:center;padding:5px;'>" +
			"<img style='width:64px;margin:5px;' alt='没有头像' src='" + record.get('BWeiXinAccount_HeadImgUrl') + "'/>" +
        	"<p><b>微信昵称</br></b> " + record.get('BWeiXinAccount_UserName') + "</p>" +
		"</div>";
		
		meta.tdAttr = 'data-qtip="' + html + '"';
		
		return v;
	},
	/**加载关系数据*/
	loadLinkData:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectLinkUrl + '?page=1&limit=10';
		
		var fields = [
			'BWeiXinEmpLink_Id',
			'BWeiXinEmpLink_BWeiXinAccount_UserName',
			'BWeiXinEmpLink_BWeiXinAccount_MobileCode',
			'BWeiXinEmpLink_BWeiXinAccount_HeadImgUrl'
		];
		
		url += '&fields=' + fields.join(',');
		url += '&where=bweixinemplink.EmpID=' + me.EmpId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.list && data.value.list.length > 0){
					me.onInsertLinkData(data.value.list);
				}else{
					me.onLoadWeixinData();
				}
			}else{
				var error = me.errorFormat.replace(/{msg}/,data.msg);
				me.getView().update(error);
			}
		});
	},
	/**插入关系数据*/
	onInsertLinkData:function(list){
		var me = this,
			len = list.length,
			insertList = [];
		
		//显示关系按钮
		me.hasLinkData = true;
		me.showLinkButtons(true);
		
		for(var i=0;i<len;i++){
			insertList.push({
				LinkId:list[i].Id,
				BWeiXinAccount_UserName:list[i].BWeiXinAccount.UserName,
				BWeiXinAccount_MobileCode:list[i].BWeiXinAccount.MobileCode,
				BWeiXinAccount_HeadImgUrl:list[i].BWeiXinAccount.HeadImgUrl
			});
		}
		//插入数据
		me.store.loadData(insertList);
	},
	/**加载微信账号数据*/
	onLoadWeixinData:function(){
		var me = this;
		//显示关系按钮
		me.hasLinkData = false;
		me.showLinkButtons(false);
		//加载数据
		me.onSearch();
	},
	/**显示关系按钮*/
	showLinkButtons:function(bo){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			refresh = buttonsToolbar.getComponent('refresh'),
			search = buttonsToolbar.getComponent('search'),
			AddLinkButton = buttonsToolbar.getComponent('AddLinkButton'),
			DelLinkButton = buttonsToolbar.getComponent('DelLinkButton'),
			pagingToolbar = me.getComponent('pagingToolbar');
		
		if(bo){
			if(refresh) refresh.disable();
			if(search) search.disable();
			if(pagingToolbar) pagingToolbar.disable();
			if(AddLinkButton) AddLinkButton.disable();
			if(DelLinkButton) DelLinkButton.enable();
		}else{
			if(refresh) refresh.enable();
			if(search) search.enable();
			if(pagingToolbar) pagingToolbar.enable();
			if(AddLinkButton) AddLinkButton.enable();
			if(DelLinkButton) DelLinkButton.disable();
		}
	},
	/**绑定关系*/
	onAddLinkClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		Ext.Msg.confirm('操作确认','确定绑定员工与微信账号的关系吗',function(but) {
			if (but != "yes") return;
			var id = records[0].get(me.PKField);
			me.saveLinkData(id);
		});
	},
	/**保存关系数据*/
	saveLinkData:function(id){
		var me = this,
			url = JShell.System.Path.ROOT + me.addLinkUrl;
		
		var params = {
			entity:{
				EmpID:me.EmpId,
				EmpName:me.EmpName,
				BWeiXinAccount:{
					Id:id,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				}
			}
		};
		
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.close();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**解绑关系*/
	onDelLinkClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		Ext.Msg.confirm('操作确认','确定解除员工与微信账号的关系吗',function(but) {
			if (but != "yes") return;
			var id = records[0].get('LinkId');
			me.delLinkData(id);
		});
	},
	/**删除关系数据*/
	delLinkData:function(id){
		var me = this,
			url = JShell.System.Path.ROOT + me.delLinkUrl + '?id=' + id;
		
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.loadLinkData();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**启用所有的操作功能*/
	enableControl:function(bo){
		var me = this;
		me.showLinkButtons(me.hasLinkData);
	}
});