/**
 * 客户选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.client.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'客户选择列表',
    width:510,
    height:400,
    
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'PClient_ProvinceName',
		direction: 'ASC'
	},{
		property: 'PClient_Name',
		direction: 'ASC'
	}],
    /**是否单选*/
	checkOne:true,
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pclient.IsUse=1';
		
	    //创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
		me.searchInfo ={width:'65%',emptyText:'名称/用户服务编号',isLike:true,itemId:'search',fields:['pclient.Name','pclient.LicenceCode']};
		buttonToolbarItems.push({
			width:70,boxLabel:'合约用户',itemId:'IsContract',
		    xtype:'checkbox',checked:false,
		    listeners:{
		    	change:function(com,  newValue,  oldValue,  eOpts ){
					me.onSearch();
				}
		    }
	    });
		
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'省份',dataIndex:'PClient_ProvinceName',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'客户名称',dataIndex:'PClient_Name',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'授权名称',dataIndex:'PClient_LicenceClientName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'用户服务编号',dataIndex:'PClient_LicenceCode',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'客户简称',dataIndex:'PClient_SName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'区域',dataIndex:'PClient_ClientAreaName',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'客户类型',dataIndex:'PClient_ClientTypename',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'PClient_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'PClient_DataTimeStamp',hidden:true,hideable:false
		},{
			text:'省份Id',dataIndex:'PClient_ProvinceID',hidden:true,hideable:false
		},{
			text:'省份',dataIndex:'PClient_ProvinceName',hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue(),
			IsContract=false,params = [];
			
        IsContract= buttonsToolbar.getComponent('IsContract').getValue();
		 //合约用户
		if(IsContract){
			params.push("(pclient.IsContract=1 )");
		}else{
			params.push("(pclient.IsContract=0 or pclient.IsContract =1 or pclient.IsContract is null)");
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});