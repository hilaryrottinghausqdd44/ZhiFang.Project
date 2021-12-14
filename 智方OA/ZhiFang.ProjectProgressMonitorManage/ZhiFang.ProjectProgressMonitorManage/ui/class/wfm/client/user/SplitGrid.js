/**
 * 待分配客户列表
 * @author liangyl
 * @version 2017-5-10
 */
Ext.define('Shell.class.wfm.client.user.SplitGrid',{
    extend:'Shell.class.wfm.client.user.Grid',
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	/**只显示销售客户列表*/
	isOnlyClient:false,
	/**销售人员ID*/
	SalesManID:null,
	/**销售人员姓名*/
	SalesManName:null,

	/**已分配客户*/
	OnlyClient:false,
		//复选框
	multiSelect: true,
	selType: 'checkboxmodel',
	//只能点击复选框才能选中
	selModel: new Ext.selection.CheckboxModel({checkOnly:true}),
	/**新增、删除数据信息*/
	errorList:[],
	addRecords:[],
	delRecords:[],
	addCount:0,
	delCount:0,
	/**登录者角色*/
	adminArr:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
		me.changeText();
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =me.callParent(arguments);
		buttonToolbarItems.push('-',{
			text:'打勾批量分配',
			iconCls:'button-save',
			itemId:'save',
			tooltip:'打勾批量分配客户',
			handler:function(){
				me.onSaveClick();
			}
		},	{
			text:'导出',iconCls:'file-excel',itemId:'Excel',tooltip:'导出', 
			handler:function(){
				me.onDownExcel();
			}
		});
		buttonToolbarItems.splice(4,0, {
			width:45,boxLabel:'重复',itemId:'IsRepeat',
		    xtype:'checkbox',checked:true,
		     listeners: {
				change: function() {
					me.onSearch();
				}
			}
	    },{
			width:60,boxLabel:'不使用',itemId:'IsNotUser',
		    xtype:'checkbox',checked:false,
		    listeners: {
				change: function() {
					me.onSearch();
				}
			}
	    });
		
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(2,0, {
			xtype: 'actioncolumn',
			text: '单个分配',
			align: 'center',
			width: 65,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
			    getClass: function(v, meta, record) {
			    	var PSalesManClientLinkID = record.get('PClient_PSalesManClientLinkID');
		            if(PSalesManClientLinkID){
		            	return 'button-actionedit hand';
		            }else{
		            	return 'button-edit hand';
		            }
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.addCount =0;
					me.onOneSaveClick(rec);
				}
			}]
		});
		return columns;
	},
	/**保存*/
	onOneSaveClick:function(rec){
		var me = this;
		me.addRecords=[];
		var PSalesManClientLinkID = rec.get('PClient_PSalesManClientLinkID');
		//当选中的数据行的销售客户关系主键ID不存在时，说明这个数据需要新增
		if(!PSalesManClientLinkID){
			me.addRecords.push(rec);
		}
		if(me.addRecords.length > 0 ){
			me.onAddAndDel();
		}
	},

	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items,
			selectRecords = me.getSelectionModel().getSelection();
		
		//清空新增列表
		me.errorList =[];
		me.addRecords = [];
		me.addCount = 0;
			
		var selectRecordsLen = selectRecords.length;
		for(var i=0;i<selectRecordsLen;i++){
			var PSalesManClientLinkID = selectRecords[i].get('PClient_PSalesManClientLinkID');
			//当选中的数据行的销售客户关系主键ID不存在时，说明这个数据需要新增
			if(!PSalesManClientLinkID){
				me.addRecords.push(selectRecords[i]);
			}
		}
		if(me.addRecords.length > 0 ){
			me.onAddAndDel();//新增、删除数据
		}
	},
	
	/**新增一条数据*/
	onAddOne:function(record){
		var me = this,
			index = me.store.indexOfTotal(record),
			url = JShell.System.Path.getRootUrl(me.addUrl);
			
		var params = {
			entity:{
				PClientID:record.get('PClient_Id'),
				PClientName:record.get('PClient_Name'),
				SalesManID:me.SalesManID,
				SalesManName:me.SalesManName
			}
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data) {
			me.addCount++;
			if (!data.success) {
				var info = '【序号：' + (index + 1) + '，新增错误！】' + record.get('PClient_Name');
				me.errorList.push(info);
			}else{
				record.set('PClient_PSalesManClientLinkID',data.value.id);
				record.commit();
			}
			me.onSaveOver();//保存结束
		});
	},
	changeText:function(){
		var me=this,
		isAdmin=false,arr=[];
		var	buttonsToolbar = me.getComponent('buttonsToolbar');
		var	ShowText = buttonsToolbar.getComponent('ShowText');
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)+'所负责的用户';
       	var usernId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		me.getRBACEmpRoles(usernId,function(data){
			arr=[];
	    	if(data.value.list){
	    		for(var i=0;i<data.value.list.length;i++){
	    			var RBACRoleId=data.value.list[i].RBACEmpRoles_RBACRole_Id;
	    			if(RBACRoleId=="5153823459927439789"){//总经理
	    				arr.push(RBACRoleId);
	    			}
	    			if(RBACRoleId=="5579125521166282740"){//副总经理	
	    				arr.push(RBACRoleId);
	    			}
	    			if(RBACRoleId=="5027619732489863317"){//商务经理
	    				arr.push(RBACRoleId);
	    			}
	    		}
	    	}
	    });
	    if(arr.length>0){
	    	isAdmin=true;
	    	arr=[];
	    }
        if(isAdmin){
			username='管理员所负责的用户';
		}
        ShowText.setText(username);
	},
	ShowRolesText:function(Admin){
		var me=this;
		var	buttonsToolbar = me.getComponent('buttonsToolbar');
		var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)+'所负责的用户';
        var usernId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)+'';
        var save=me.getComponent('buttonsToolbar').getComponent('save');
       //管理员有分下来的权限
		if(usernId== me.SalesManID && Admin==false){
			save.disable();
		}else{
			 save.enable();
		}
	},
	
		/**获取登录者信息，判断收否是管理员(总经理  副总经理 ,商务经理)*/
	getRBACEmpRoles:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true';
		url += '&fields=RBACEmpRoles_RBACRole_CName,RBACEmpRoles_RBACRole_Id&where=rbacemproles.HREmployee.Id='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	changeWhere:function(){
		var me=this,
		buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue(),
			searchBman= buttonsToolbar.getComponent('searchBman').getValue(),
			IsRepeat=false,IsNotUser=false,
			IsContract=false,params = [];
			
        IsContract= buttonsToolbar.getComponent('IsContract').getValue();
		IsRepeat= buttonsToolbar.getComponent('IsRepeat').getValue();
		IsNotUser= buttonsToolbar.getComponent('IsNotUser').getValue();
		
		//重复(打钩表示包含重复标记的记录，否则不包含。默认为打钩)
		if(IsRepeat){
			params.push("(pclient.IsRepeat=1 or pclient.IsRepeat=0)");
		}else{
			params.push("pclient.IsRepeat=0");
		}
		//不使用(打钩表示包含不使用标记的记录，否则表示不包括。默认不包括)
		if(IsNotUser){
			params.push("(pclient.IsUse=0 or pclient.IsUse=1)");
		}else{
			params.push("pclient.IsUse=1");
		}
		//合约用户
		if(IsContract){
			params.push("(pclient.IsContract=1 )");
		}else{
			params.push("(pclient.IsContract=0 or pclient.IsContract =1 or pclient.IsContract is null)");
		}
		if(searchBman){
			params.push("(pclient.Bman like '%" + searchBman + "%')");
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
		return me.internalWhere;
	}
});
	