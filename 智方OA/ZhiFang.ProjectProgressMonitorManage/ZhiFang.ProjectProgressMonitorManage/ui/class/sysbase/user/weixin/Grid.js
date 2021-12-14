/**
 * 员工微信列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.weixin.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires:['Ext.ux.RowExpander'],
    
    title:'员工微信列表',
    width:360,
    height:600,
    
    /**获取数据服务路径*/
	selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLinkByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl:'/WeiXinAppService.svc/ST_UDTO_DelBWeiXinEmpLink',
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:100,
		
	/**是否有复选删除按钮*/
	HASDEL:false,
	
	initComponent:function(){
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:130,emptyText:'员工姓名/微信昵称',isLike:true,
			itemId:'search',fields:['bweixinemplink.EmpName','bweixinemplink.BWeiXinAccount.UserName']};
		
		me.buttonToolbarItems = ['refresh','-',{
			type: 'search',
			info: me.searchInfo
		}];
		
		if(me.HASDEL){
			//复选框
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
			me.buttonToolbarItems.push('-',{
				text:'解绑',
				iconCls:'button-text-relieve',
				tooltip:'解除用户和微信的绑定',
				handler:function(){
					me.onDelClick();
				}
			});
		}
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'员工姓名',dataIndex:'BWeiXinEmpLink_EmpName',width:100,
			renderer:me.showRenderer
		},{
			text:'微信昵称',dataIndex:'BWeiXinEmpLink_BWeiXinAccount_UserName',width:150,
			renderer:me.showRenderer
		},{
			text:'微信头像',dataIndex:'BWeiXinEmpLink_BWeiXinAccount_HeadImgUrl',hidden:true,hideable:false
		},{
			text:'员工ID',dataIndex:'BWeiXinEmpLink_EmpID',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BWeiXinEmpLink_Id',isKey:true,hidden:true,hideable:false
		},{
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls:'button-text-relieve hand',
				tooltip:'解除用户和微信的绑定',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex),
						id = rec.get(me.PKField);
						
					me.onRelieveById(id);
				}
			}]
		}];
		return columns;
	},
	showRenderer:function(v,meta,record){
		var html = 
		"<div style='text-align:center;padding:5px;'>" +
			"<img style='width:64px;margin:5px;' alt='没有头像' src='" + record.get('BWeiXinEmpLink_BWeiXinAccount_HeadImgUrl') + "'/>" +
        	"<p><b>微信昵称</br></b> " + record.get('BWeiXinEmpLink_BWeiXinAccount_UserName') + "</p>" +
		"</div>";
		
		meta.tdAttr = 'data-qtip="' + html + '"';
		
		return v;
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Msg.confirm({
			msg:'确定要解除绑定吗？'
		},function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.delOneById(i, id);
			}
		});
	},
	/**解绑一条数据*/
	onRelieveById: function(id) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.delUrl) + '?id=' + id;

		JShell.Msg.confirm({
			msg:'确定要解除绑定吗？'
		},function(but) {
			if (but != "ok") return;
			
			me.showMask(me.delText); //显示遮罩层
			
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					JShell.Msg.alert("解绑成功！",null,1000);
					me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
	}
});