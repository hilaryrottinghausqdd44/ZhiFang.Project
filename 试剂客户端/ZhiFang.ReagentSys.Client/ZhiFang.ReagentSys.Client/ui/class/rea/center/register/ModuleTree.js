/**
 * 选择模块树
 * @author liangyl		
 * @version 2018-05-15
 */
Ext.define('Shell.class.rea.center.register.ModuleTree',{
    extend:'Shell.class.sysbase.role.ModuleCheckTree',
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
	/**获取角色模块服务*/
    selectRoleModuleUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
	title:'功能模块',
	width:300,
	height:500,
	margin: '0 0 1 0',
	LabID:null,
	RoleID:null,
	RoleModuleID:null,
	CenOrgID:null,
	/**智方平台修改客户机构的授权变更信息*/
	editUrl:'/ReaManageService.svc/ST_UDTO_UpdateCenOrgAuthorizationModifyOfPlatform',
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.changeChecked(me.RoleModuleID);
		
	},
    initComponent:function(){
		var me = this;
		me.addEvents('save');
//		me.topToolbar = me.topToolbar || ['-',{text:'保存',tooltip:'保存',iconCls:'button-save',
//		handler:function(){
//			me.onSaveClick();
//		}}];
		me.callParent(arguments);
	},
	initFilterListeners:function(){
		var me = this;
	},
	changeData:function(data){
		var me = this;
		me._lastData = data;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
    		}
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	var children = data[me.defaultRootProperty];
    	changeChildren(children);
    	
    	return data;
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this;
		var fields = [
			{name:'checked',type:'bool'},
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'}//默认ID号
		];
		return fields;
	},
	onSaveClick:function(){
		var me =this;
		var url = ( me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;
	    var moduleList=me.getRoleModule();
		var params = Ext.encode({labId:me.LabID,cenOrgId:me.CenOrgID,moduleList:moduleList});
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				JShell.Msg.alert(data.msg,null,5000);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
	},
	/**更改勾选*/
	changeChecked:function(ids){
		var me = this;
			arr = ids ? ids.split(',') : [],
			len = arr.length;
		
		me.autoSelectIds = ids;
		
		if(!me._lastData) return;
		
		me.disableControl();//禁用所有的操作功能
		
		var changeNode = function(node){
    		node['checked'] = false;//还原为不选中
    		node['expanded'] = false;//默认收起
    		
    		for(var i=0;i<len;i++){
    			if(node['tid'] == arr[i]){
    				node['checked'] = true;//选中
    				if(node['leaf']==false){
    					node['expanded'] = true;//展开
    				}
    				break;
    			}
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var root = me.root;
    	root.expanded = true;//默认展开
    	root.Tree = me._lastData.Tree;
    	
    	changeChildren(root.Tree);
    	me.setRootNode(root);
    	//me.getRootNode().expand();
    	me.enableControl();//启用所有的操作功能
	},
	getRoleModule:function(){
		var me =this;
		var nodes=me.getChecked(),
		    nodesLen = nodes.length,
		    list=[];
	    var ModuleIds='';
		for(var i =0 ;i<nodesLen;i++){
			var id = nodes[i].data.tid;
			var text = nodes[i].data.text;
			if(!id) continue;
			var obj={Id:id,CName:text};
//			var obj={RBACModule_Id:id};

			list.push(obj);
	    }
		return  list;
	},
	createDockedItems: function() {
		var me = this;

		var items = [{
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		},{text:'保存',tooltip:'保存',iconCls:'button-save',
		handler:function(){
			me.onSaveClick();
		}}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	}
});