/**
 * 合同,发票,收款的对比及审核的对比关联客户及付款单位
 * @author longfc
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.basic.App', {
	extend: 'Ext.panel.Panel',
	title: '',

	layout: 'border',
	bodyPadding: 1,
	width: 1200,
	height: 600,

	/**修改服务地址*/
	editUrl: '',
	/**左列表行没有选择时提示信息*/
	alertMsg: "",
	/**操作类型:contrast:对比;check:审核;*/
	operationType: "",
	GridCalss: 'Shell.class.wfm.business.associate.contract.basic.Grid',

	/**更新信息类型:PClient;PayOrg*/
	updateType: 'PClient',
	/**更新提交信息*/
	updateParams: null,
	/**更新提交实体*/
	updateEntity: null,
	/**左列表的客户ID列名*/
	PClientID: "PClientID",
	
	/**左列表的客户名称列名*/
	PClientName: "PClientName",
	/**左列表的收款名称列名*/
	PayOrgName: "PayOrgName",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		var Grid = me.getComponent('Grid');
		me.on({
			save: function() {
				Grid.getView().refresh();
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
		items.push(Ext.create(me.GridCalss, {
			region: 'center',
			header: false,
			//border:false,
			itemId: 'Grid'
		}));
		items.push(Ext.create('Shell.class.wfm.business.associate.basic.BtnPanel', {
			region: 'east',
			title: '',
			//border:false,
			width: 100,
			itemId: 'BtnPanel'
		}));
		items.push(Ext.create('Shell.class.wfm.business.associate.basic.ClientGrid', {
			region: 'east',
			width: 335,
			//border:false,
			header: false,
			itemId: 'ClientGrid'
		}));
		return items;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		var BtnPanel = me.getComponent('BtnPanel');
		BtnPanel.on({
			onClientClick: function() {
				me.updateEntity = null;
				me.updateParams = null;
				me.updateType = "PClient";
				var recordPContract = me.onVerification();
				if(me.updateParams != null) {
					me.onSave(recordPContract);
				}
			},
			onPayClick: function() {
				me.updateEntity = null;
				me.updateParams = null;
				me.updateType = "PayOrg";
				var recordPContract = me.onVerification();
				if(me.updateParams != null) {
					me.onSave(recordPContract);
				}
			}
		});
		Grid.on({
			onSaveClick: function(grid, rowIndex, colIndex) {
				me.updateEntity = null;
				me.updateParams = null;
				me.updateType = "PClient";
				var recordPContract = me.onCheckVerification();
				if(me.updateParams != null) {
					me.onSave(recordPContract);
				}
			}
		});
	},
	/**左列表审核操作列审核前验证*/
	onCheckVerification: function() {
		var me = this;
		var me = this;
		var Grid = me.getComponent('Grid');
		var ClientGrid = me.getComponent('ClientGrid');
		var isExec = true;
		var recordsPContract = Grid.getSelectionModel().getSelection();
		var recordPContract = null;
		if(!recordsPContract || recordsPContract.length != 1) {
			isExec = false;
			JShell.Msg.alert(me.alertMsg, null, 1500);
			return;
		} else {
			recordPContract = recordsPContract[0];
		}
		var oldPClientID = "",
			oldPayOrgID = "";
		if(isExec) {
			var CheckId = "" + recordPContract.get("CheckId");
			oldPClientID = "" + recordPContract.get(me.PClientID);
			oldPayOrgID = "" + recordPContract.get("PayOrgID");
			if(CheckId.length > 0) {
				isExec = false;
				JShell.Msg.error("当前选择行已经被审核!");
				return;
			}
			if(oldPClientID == "" || oldPayOrgID == "") {
				isExec = false;
				JShell.Msg.error("未对比客户或付款单位,不能审核!");
				return;
			}
		}

		me.updateEntity = {
			Id: recordPContract.get(Grid.PKField)
		};
		var fileds = "Id";
		var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		me.updateEntity.CheckId = userID;
		me.updateEntity.CheckCName = userName;
		fileds = fileds + ",CheckId,CheckCName";
		if(isExec == true) {
			me.updateParams = Ext.JSON.encode({
				entity: me.updateEntity,
				fields: fileds
			});
		}
		return recordPContract;
	},

	/**对比客户或对比付款单位前验证*/
	onVerification: function() {
		var me = this;
		var Grid = me.getComponent('Grid');
		var ClientGrid = me.getComponent('ClientGrid');
		var isExec = true;
		var recordsPContract = Grid.getSelectionModel().getSelection();
		var recordPContract = null;
		var oldPClientID = "",
			oldPayOrgID = "";
		if(!recordsPContract || recordsPContract.length != 1) {
			isExec = false;
			JShell.Msg.alert(me.alertMsg, null, 5000);
			return;
		} else {
			recordPContract = recordsPContract[0];
		}

		if(isExec) {
			var CheckId = "" + recordPContract.get("CheckId");
			oldPClientID = "" + recordPContract.get(me.PClientID);
			oldPayOrgID = "" + recordPContract.get("PayOrgID");
			if(CheckId.length > 0) {
				isExec = false;
				JShell.Msg.error("当前选择行已经被审核!");
				return;
			}
		}

		var recordsPClient = ClientGrid.getSelectionModel().getSelection();
		var recordPClient = null;

		me.updateEntity = {
			Id: recordPContract.get(Grid.PKField)
		};
		var fileds = "Id";
		var userID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		if(isExec) {
			switch(me.updateType) {
				case 'PClient':
					if(!recordsPClient || recordsPClient.length != 1) {
						isExec = false;
						JShell.Msg.error("没有选择用户信息!");
						return;
					} else {
						recordPClient = recordsPClient[0];
					}
					if(isExec == true) {
						me.updateEntity[me.PClientID] = "" + recordPClient.get(ClientGrid.PKField);
						me.updateEntity.ContrastId = userID;
						me.updateEntity.ContrastCName = userName;
						fileds = fileds + ",ContrastId,ContrastCName";
						//me.updateEntity[me.PClientName] = "" + recordPClient.get("PClient_Name");
						fileds = fileds + "," + me.PClientID; // + "," + me.PClientName;
						if(oldPClientID == "") oldPClientID = me.updateEntity[me.PClientID];
					}
					break;
				case 'PayOrg':
					if(!recordsPClient || recordsPClient.length != 1) {
						isExec = false;
						JShell.Msg.error("没有选择付款单位信息!");
						return;
					} else {
						recordPClient = recordsPClient[0];
					}
					if(isExec == true) {
						me.updateEntity.ContrastId = userID;
						me.updateEntity.ContrastCName = userName;
						fileds = fileds + ",ContrastId,ContrastCName";
						me.updateEntity.PayOrgID = "" + recordPClient.get(ClientGrid.PKField);
						//me.updateEntity[me.PayOrgName] = "" + recordPClient.get("PClient_Name");
						if(me.operationType == "check" && oldPClientID.length > 0 && oldPayOrgID.length > 0) {
							me.updateEntity.CheckId = userID;
							me.updateEntity.CheckCName = userName;
							fileds = fileds + ",CheckId,CheckCName";
						}
						fileds = fileds + ",PayOrgID"; // + ","+me.PayOrgName;
						if(oldPayOrgID == "") oldPayOrgID = me.updateEntity.PayOrgID;
					}
					break;
				default:
					break;
			}
		}
		if(isExec == true) {
			me.updateParams = Ext.JSON.encode({
				entity: me.updateEntity,
				fields: fileds
			});
		}
		return recordPContract;
	},
	onSave: function(recordPContract) {
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

						if(me.updateEntity.PayOrgID) {
							recordPContract.set("PayOrgID", me.updateEntity.PayOrgID);
						}
						if(me.updateEntity[me.PayOrgName]) {
							recordPContract.set(me.PayOrgName, me.updateEntity[me.PayOrgName]);
						}
						if(me.updateEntity[me.PClientID]) {
							recordPContract.set(me.PClientID, me.updateEntity[me.PClientID]);
						}
//						if(me.updateEntity[me.PClientName]) {
//							recordPContract.set(me.PClientName, me.updateEntity[me.PClientName]);
//						}

						if(me.updateEntity.ContrastId) {
							recordPContract.set("ContrastId", me.updateEntity.ContrastId);
						}
						if(me.updateEntity.ContrastCName) {
							recordPContract.set("ContrastCName", me.updateEntity.ContrastCName);
						}
						if(me.updateEntity.CheckId) {
							recordPContract.set("CheckId", me.updateEntity.CheckId);
						}
						if(me.updateEntity.CheckCName) {
							recordPContract.set("CheckCName", me.updateEntity.CheckCName);
						}
						recordPContract.commit();
						var Grid = me.getComponent('Grid');
						var indexOfNum = Grid.store.find(Grid.PKField, recordPContract.get(Grid.PKField));
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
		//		var BtnClient = buttonsToolbar.getComponent('BtnClient');
		//		BtnClient.setDisabled(enable);
	}
});