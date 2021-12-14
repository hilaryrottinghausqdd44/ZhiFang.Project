/**
 * 客户对比，对比,
 * 将UserFWNo,LRNo1,LRNo2复制到P_Client的LicenceCode,LRNo1,LRNo2
 * liangyl
 * @version 2017-03-31
 */
Ext.define('Shell.class.wfm.business.associate.client.contrast.App', {
	extend: 'Ext.panel.Panel',
	title: '',
	layout: 'border',
	bodyPadding: 1,
	width: 1200,
	height: 600,

	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientByField',
	/**左列表行没有选择时提示信息*/
	alertMsg: "请选择用户列表",
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "",
	GridCalss: 'Shell.class.wfm.business.associate.client.basic.Grid',
	GridCheckCalss: 'Shell.class.wfm.business.associate.client.basic.CUserGrid',
	/**客户更新提交信息*/
	updateParams: null,
	/**客户更新提交实体*/
	updateEntity: null,
	
	editCUserUrl:'/SingleTableService.svc/ST_UDTO_UpdateCUserByField',
	/**授权系统客户更新提交实体*/
	updateCUserParams:null,
	/**授权系统客户更新提交实体*/
	updateCUserEntity:null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		var Grid = me.getComponent('Grid');
		var CUserGrid = me.getComponent('CUserGrid');

		me.on({
			save: function() {
				Grid.getView().refresh();
			},
			cusersave:function(grid,rec){
				CUserGrid.onSearch();
//				if(rec){
//					CUserGrid.onSearch();
//				}else{
//					CUserGrid.getView().refresh();
//				}
			}
		});
		CUserGrid.on({
			onAddPClientClick: function(grid,record,type) {
				var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		        var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		        record.set("IsMapping", 'true');
		        record.set("ContrastId", userID);
				record.set("ContrastCName", userName);
				if(type=='2' || type==2){
					record.set("CheckId", userID);
					record.set("CheckCName", userName);
				}
				record.commit();
				CUserGrid.onSearch();
				Grid.onSearch();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
	    items = [];
	    items.push(Ext.create(me.GridCheckCalss, {
			region: 'center',
			header: false,
			itemId: 'CUserGrid'
		}));
		items.push(Ext.create('Shell.class.wfm.business.associate.basic.BtnPanel', {
			region: 'east',
			title: '',
			hasClient:true,
			hasPayOrg:true,
			ClientMsg:'对照',
			PayOrgMsg:'解除对照',
			BtnWidth:65,
			//border:false,
			width: 82,
			itemId: 'BtnPanel'
		}));	
		items.push(Ext.create(me.GridCalss, {
			region: 'east',
			width: 550,
			header: false,
			//border:false,
			itemId: 'Grid'
		}));
		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//客户表
		var Grid = me.getComponent('Grid');
		
		//授权系统客户列表
		var CUserGrid = me.getComponent('CUserGrid');

		var BtnPanel = me.getComponent('BtnPanel');
		BtnPanel.on({
			onClientClick: function() {
				me.updateEntity = null;
				me.updateParams = null;
//				me.updateType = "PClient";
				var recordPClient = me.onVerification();
//				var recordCuser = me.onVerificationCuser();
				if(me.updateParams != null) {
					me.onSave(recordPClient);
					me.onCUserSave();
				}
			},
			//解除对照
			onPayClick:function() {
				me.updateEntity = null;
				me.updateParams = null;
				var recordPClient = me.onRemoveVerification();
//				
				if(me.updateParams != null) {
					me.onSave(recordPClient);
					me.onCUserSave();
				}
			}
	
		});
		CUserGrid.on({
			onSaveClick: function(grid, rowIndex, colIndex) {
				me.updateCUserEntity = null;
				me.updateCUserParams = null;
				var recordCUser = me.onCheckVerification();
				if(me.updateCUserParams != null) {
					me.onCUserSave(recordCUser);
				}
			}
		});
	},
	/**根据授权编码获取CUSER表信息*/
	getCUserInfo:function(UserFWNo,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchCUserByHQL?isPlanish=false';
		url += '&fields=Id&where=cuser.UserFWNo='+UserFWNo;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false,500);
	},
	/**左列表审核操作列审核前验证*/
	onCheckVerification: function() {
		var me = this;
		var me = this;
		var Grid = me.getComponent('Grid');
		var CUserGrid = me.getComponent('CUserGrid');
		var isExec = true;
		var recordsCUser = CUserGrid.getSelectionModel().getSelection();
		var recordCUser = null;
		if(!recordsCUser || recordsCUser.length != 1) {
			isExec = false;
			JShell.Msg.alert("请选择授权系统客户行信息", null, 1500);
			return;
		} else {
			recordCUser = recordsCUser[0];
		}
		if(isExec) {
			var UserFWNo = "" + recordCUser.get("UserFWNo");
            if(!UserFWNo){
            	isExec = false;
				JShell.Msg.error("授权编码为空不能审核!");
				return;
            }
            var ContrastId = "" + recordCUser.get("ContrastId");
            var IsMapping =  recordCUser.get("IsMapping");
			if (IsMapping!='1' &&  IsMapping!='true' && IsMapping!=true) {
				isExec = false;
				JShell.Msg.error("当前选择行未比对,不能审核!");
				return;
			}
			var CheckId = "" + recordCUser.get("CheckId");
			if(CheckId.length > 0 && (CheckId!='' || CheckId!=null )) {
				isExec = false;
				JShell.Msg.error("当前选择行已经被审核!");
				return;
			}
		}

		me.updateCUserEntity = {
			Id: recordCUser.get(CUserGrid.PKField)
		};
		var fileds = "Id";
		var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		me.updateCUserEntity.CheckId = userID;
		me.updateCUserEntity.CheckCName = userName;
		fileds = fileds + ",CheckId,CheckCName";
		if(isExec == true) {
			me.updateCUserParams = Ext.JSON.encode({
				entity: me.updateCUserEntity,
				fields: fileds
			});
		}
		return recordCUser;
	},

    /**对比客户或对比付款单位前验证*/
	onVerification: function() {
		var me = this;
         //客户列表
		var Grid = me.getComponent('Grid');
		//授权系统客户列表
		var CUserGrid = me.getComponent('CUserGrid');
		
		var isExec = true;
		var recordsCUser = CUserGrid.getSelectionModel().getSelection();
		var recordCUser = null;
		
		var recordsPClient = Grid.getSelectionModel().getSelection();
		var recordPClient = null;
		if(!recordsCUser || recordsCUser.length != 1) {
			isExec = false;
			JShell.Msg.alert('请选择授权系统客户行信息', null, 5000);
			return;
		} else {
			recordCUser = recordsCUser[0];
		}
		var CheckId = "" + recordCUser.get("CheckId");
		if(CheckId.length > 0 && (CheckId!='' || CheckId!=null) ) {
			isExec = false;
			JShell.Msg.error("当前选择行已经被审核!");
			return;
		}
	    if(!recordsPClient || recordsPClient.length != 1) {
			isExec = false;
			JShell.Msg.alert('没有选择客户信息', null, 5000);
			return;
		} else {
			recordPClient = recordsPClient[0];
		}
		me.updateEntity = {
			Id: recordPClient.get(Grid.PKField)
		};
		me.updateCUserEntity = {
			Id: recordCUser.get(CUserGrid.PKField)
		};
		var fileds = "Id";
		var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
        var cuserfileds="Id";
		if(isExec) {
			me.updateEntity.LicenceCode = "" + recordCUser.get('UserFWNo');
			me.updateEntity.LRNo1 = "" + recordCUser.get('LRNo1');
			me.updateEntity.LRNo2 = "" + recordCUser.get('LRNo2');
			me.updateEntity.LicenceClientName = "" + recordCUser.get('UserName');
			fileds = fileds + ",LicenceCode,LRNo1,LRNo2,LicenceClientName";
			//授权客户
			me.updateCUserEntity.ContrastId=userID;
			me.updateCUserEntity.ContrastCName=userName;
			me.updateCUserEntity.IsMapping=1;
			cuserfileds=cuserfileds+",ContrastId,ContrastCName,IsMapping";
			// && recordCUser.get('ContrastId').length>0
		    if(me.operationType == "check") {
				me.updateCUserEntity.CheckId = userID;
				me.updateCUserEntity.CheckCName = userName;
				cuserfileds = cuserfileds + ",CheckId,CheckCName";
			}
		    me.updateParams = Ext.JSON.encode({
				entity: me.updateEntity,
				fields: fileds
			});
			me.updateCUserParams = Ext.JSON.encode({
				entity: me.updateCUserEntity,
				fields: cuserfileds
			});
		}
		return recordPClient;
    },
    
     /**解除对照验证*/
	onRemoveVerification: function() {
		var me = this,isExec=true;
         //客户列表
		var Grid = me.getComponent('Grid');
		var recordsPClient = Grid.getSelectionModel().getSelection();
		var recordPClient = null;
		
	    if(!recordsPClient || recordsPClient.length != 1) {
			isExec = false;
			JShell.Msg.alert('没有选择客户信息');
			return;
		} else {
			recordPClient = recordsPClient[0];
		}    
		var cuserfileds='Id';
		me.getCUserInfo(recordPClient.get('PClient_LicenceCode'),function(data){
        	if(data.value.list){
    			me.updateCUserEntity = {
					Id: data.value.list[0].Id
				};
				me.updateCUserEntity.IsMapping=0;
				cuserfileds=cuserfileds+",ContrastId,ContrastCName,IsMapping,CheckId,CheckCName";
			    me.updateCUserParams = Ext.JSON.encode({
					entity: me.updateCUserEntity,
					fields: cuserfileds
				});
        	}
        });
		me.updateEntity = {
			Id: recordPClient.get(Grid.PKField)
		};
		var fileds = "Id";
		var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(isExec) {
			fileds = fileds + ",LicenceCode,LRNo1,LRNo2,LicenceClientName";
			me.updateParams = Ext.JSON.encode({
				entity: me.updateEntity,
				fields: fileds
			});
		}
		return recordPClient;
    },
    
	onSave: function(recordPClient) {
		var me = this;
		var isSave = true;
		var msg = "";
		if(me.updateParams == null) {
			isSave = false;
			msg = "没有需要的更新信息!";
		}
		if(isSave == true) {
			me.showMask("数据提交保存中...");
			var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
			JShell.Server.post(url, me.updateParams, function(data) {
				if(data.success) {
					me.hideMask(); //隐藏遮罩层
					if(me.updateEntity != null) {
						if(me.updateEntity.LicenceCode) {
							recordPClient.set("PClient_LicenceCode", me.updateEntity.LicenceCode);
						}else{
							recordPClient.set("PClient_LicenceCode", '');
						}
						if(me.updateEntity.LRNo1) {
							recordPClient.set('PClient_LRNo1', me.updateEntity.LRNo1);
						}else{
							recordPClient.set("PClient_LRNo1", '');
						}
						if(me.updateEntity.LRNo2) {
							recordPClient.set('PClient_LRNo2', me.updateEntity.LRNo2);
						}else{
							recordPClient.set('PClient_LRNo2', '');
						}
						if(me.updateEntity.LicenceClientName) {
							recordPClient.set('PClient_LicenceClientName', me.updateEntity.LicenceClientName);
						}else{
							recordPClient.set('PClient_LicenceClientName', '');
						}
						recordPClient.commit();
						var Grid = me.getComponent('Grid');
						var indexOfNum = Grid.store.find(Grid.PKField, recordPClient.get(Grid.PKField));
						if(indexOfNum >= 0) {
							Grid.getSelectionModel().select(indexOfNum);
						}
					}
					JShell.Msg.alert("保存操作成功", null, 1000);
					me.fireEvent('save', me);
				} else {
					me.hideMask();
					JShell.Msg.error("保存操作失败!<br />" + data.msg);
				}
			});
		} else {
			if(msg.length > 0) JShell.Msg.alert(msg, null, 2000);
		}
	},
	onCUserSave: function(recordPClient) {
		var me = this;
		var isSave = true;
		var msg = "";
//		var CUserGrid = me.getComponent('CUserGrid');
//
//		var recordsCUser = CUserGrid.getSelectionModel().getSelection();
////		var recordPClient = null;
//		
//		if(!recordsCUser || recordsCUser.length != 1) {
//			isExec = false;
//			JShell.Msg.alert('请选择授权系统客户行信息');
//			return;
//		} else {
//			recordCUser = recordsCUser[0];
//		}
		if(me.updateCUserParams == null) {
			isSave = false;
			msg = "请先登录!";
		}
		if(isSave == true) {
			me.showMask("数据提交保存中...");
			var url = (me.editCUserUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editCUserUrl;
			JShell.Server.post(url, me.updateCUserParams, function(data) {
				if(data.success) {
					me.hideMask(); //隐藏遮罩层
//					if(me.updateCUserEntity != null) {
//
//						if(me.updateCUserEntity.ContrastId) {
//							recordCUser.set("ContrastId", me.updateCUserEntity.ContrastId);
//						}else{
//							recordCUser.set("ContrastId", '');
//						}
//						if(me.updateCUserEntity.ContrastCName) {
//							recordCUser.set("ContrastCName", me.updateCUserEntity.ContrastCName);
//						}else{
//							recordCUser.set("ContrastCName", '');
//						}
//						if(me.updateCUserEntity.CheckId) {
//							recordCUser.set("CheckId", me.updateCUserEntity.CheckId);
//						}
//						if(me.updateCUserEntity.CheckCName) {
//							recordCUser.set("CheckCName", me.updateCUserEntity.CheckCName);
//						}
//						recordCUser.set("IsMapping",me.updateCUserEntity.IsMapping);
//						recordCUser.commit();
//					}
				
					JShell.Msg.alert("保存操作成功", null, 1000);
					me.fireEvent('cusersave', me,recordPClient);
				} else {
					me.hideMask();
					JShell.Msg.error("保存操作失败!<br />" + data.msg);
				}
			});
		} else {
			if(msg.length > 0) JShell.Msg.alert(msg, null, 2000);
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(false); //启用所有的操作功能
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true;
		var buttonsToolbar = me.getComponent('BtnPanel');
	}
});