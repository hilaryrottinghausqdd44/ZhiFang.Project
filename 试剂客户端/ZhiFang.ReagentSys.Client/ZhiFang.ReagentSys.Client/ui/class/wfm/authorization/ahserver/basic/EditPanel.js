/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchAHServerLicenceAndAndDetailsById',
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateAHServerLicenceAndDetailsByField',
	title: '服务器授权申请',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**通过按钮文字*/
	OverButtonName: '',
	/**不通过按钮文字*/
	BackButtonName: '',
	/**通过状态文字*/
	OverName: '',
	/**不通过状态文字*/
	BackName: '',
	/**处理意见字段*/
	OperMsgField: '',
	/**处理时间字段*/
	AuditDataTimeMsgField: '',
	/**上传的授权申请文件的主要信息*/
	AHServerLicence: null,
	LicenceProgramTypeList: null,
	AHServerEquipLicenceList: null,
	/**是否包含是否特批复选框(只有审核时才显示)*/
	hasIsSpecially: false,
	/**是否特批复选框选择值*/
	IsSpeciallyValue: false,
	/**是否联动页签*/
	IsLinkpageTabPage: true,
	PK: null,
	PClientID: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.loadData();
	},
	initListeners: function() {
		var me = this;
		me.SimpleGrid.on({
			itemclick: function(v, record, item, index, e, eOpts) {
				me.DetailsShow(record);
			},
			select: function(rowModel, record, index, eOpts) {
				me.DetailsShow(record);
			},
			nodata: function(p) {
				//me.Grid.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.SimpleGrid = Ext.create('Shell.class.wfm.authorization.ahserver.basic.SimpleGrid', {
			region: 'west',
			width: 200,
			header: true,
			split: true,
			collapsible: false,
			itemId: 'SimpleGrid',
			PK: me.PK
		});
		me.EditTabPanel = Ext.create('Shell.class.wfm.authorization.ahserver.basic.EditTabPanel', {
			region: 'center',
			header: false,
			ProgramGrid: me.ProgramGrid,
			EquipGrid: me.EquipGrid,
			itemId: 'EditTabPanel',
			AHServerLicence: me.AHServerLicence,
			LicenceProgramTypeList: me.LicenceProgramTypeList,
			AHServerEquipLicenceList: me.AHServerEquipLicenceList,
			PK: me.PK,
			PClientID: me.PClientID
		});

		return [me.SimpleGrid, me.EditTabPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			
			items.push(Ext.create('Ext.toolbar.Toolbar', {
				dock: 'bottom',
				itemId: 'buttonsToolbar',
				items: me.createButtontoolbar()
			}));
			
			//审核及审批时用
			items.push(Ext.create('Ext.toolbar.Toolbar', {
				dock: 'bottom',
				hidden: true,
				itemId: 'buttonsToolbar1',
				items: [{
					fieldLabel: '',
					xtype: 'displayfield',
					itemId: 'LRNoIsIdentical',
					border:false,
					labelStyle: 'font-size:14px;font-weight:bold;color:#FF0000', //00F
					fieldStyle: 'background:none;border:0;border-bottom:0px;font-size:14px;font-weight:bold;color:#FF0000;',
					labelWidth: 0,
					value: ''
				}]
			}));

			
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push('-', '->');
		if(me.hasIsSpecially) {
			items.push({
				boxLabel: '是否特批',
				itemId: 'checkIsSpecially',
				checked: me.IsSpeciallyValue,
				value: me.IsSpeciallyValue,
				inputValue: false,
				xtype: 'checkbox',
				style: {
					marginRight: '10px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.IsSpeciallyValue = true;
						} else {
							me.IsSpeciallyValue = false;
						}
					}
				}
			}, '-');
		}
		if(me.OverButtonName) {
			items.push({
				iconCls: 'button-save',
				text: me.OverButtonName,
				tooltip: me.OverButtonName,
				handler: function() {
					me.onOver();
				}
			});
		}
		if(me.BackButtonName) {
			items.push({
				iconCls: 'button-save',
				text: me.BackButtonName,
				tooltip: me.BackButtonName,
				handler: function() {
					me.onBack();
				}
			});
		}
		return items;
	},
	/**@public加载数据*/
	loadData: function() {
		var me = this;
		//me.showMask("数据加载中...");
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl + "?id=" + me.PK;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				//me.hideMask(); //隐藏遮罩层
				if(data.value != null) {
					me.SimpleGrid.LicenceProgramTypeList = data.value.LicenceProgramTypeList;
					if(me.SimpleGrid.LicenceProgramTypeList != null) {
						me.SimpleGrid.store.loadData(me.SimpleGrid.LicenceProgramTypeList);
					} else {
						me.SimpleGrid.clearData();
					}
					//程序明细数据加载
					me.EditTabPanel.ProgramLicenceGrid.ApplyProgramInfoList = data.value.ApplyProgramInfoList;
					if(me.EditTabPanel.ProgramLicenceGrid.ApplyProgramInfoList != null) {
						me.EditTabPanel.ProgramLicenceGrid.store.loadData(me.EditTabPanel.ProgramLicenceGrid.ApplyProgramInfoList);
					} else {
						me.EditTabPanel.ProgramLicenceGrid.clearData();
					}
					//仪器明细数据加载
					me.EditTabPanel.EquipLicenceGrid.AHServerEquipLicenceList = data.value.AHServerEquipLicenceList;
					if(me.EditTabPanel.EquipLicenceGrid.AHServerEquipLicenceList != null) {
						me.EditTabPanel.EquipLicenceGrid.store.loadData(me.EditTabPanel.EquipLicenceGrid.AHServerEquipLicenceList);
					} else {
						me.EditTabPanel.EquipLicenceGrid.clearData();
					}
					me.AHServerLicence = data.value.AHServerLicence;
					var IsIdentical=""+data.value.LRNoIsIdentical;
					if(IsIdentical=="false"||IsIdentical=="0"){
						var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
						var LRNoIsIdentical = buttonsToolbar1.getComponent('LRNoIsIdentical');						
						var showValue="  原主服务器申请号:"+data.value.PClientLRNo1+"  ";						
						showValue=showValue+"原备服务器申请号:"+data.value.PClientLRNo1+"  ";
						var LRNo1=me.AHServerLicence.LRNo1;
						if(LRNo1==null)LRNo1="";
						var LRNo2=me.AHServerLicence.LRNo2;
						if(LRNo2==null)LRNo2="";
						showValue=showValue+"当前主服务器申请号:"+LRNo1+"  ";
						showValue=showValue+"当前备服务器申请号:"+LRNo2+"  ";
						LRNoIsIdentical.setValue(showValue);
						buttonsToolbar1.setVisible(true);
					}
				}
			} else {
				//me.hideMask();
				JShell.Msg.error("获取服务器授权信息失败!<br />" + data.msg);
			}
		});
	},
	/*左列表的事件监听联动右区域**/
	DetailsShow: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get('Id');
			var code = record.get('Code');
			var index = 0;
			switch(code) {
				case "BEquip":
					index = 1;
					me.EditTabPanel.setActiveTab(me.EditTabPanel.EquipLicenceGrid);
					break;
				default:
					//me.EditTabPanel.showTab(0);
					//me.EditTabPanel.hideTab(1);
					index = 0;
					me.EditTabPanel.setActiveTab(me.EditTabPanel.ProgramLicenceGrid);
					break;
			}
		}, null, 500);
	},
	/**通过*/
	onOver: function() {
		var me = this;
		if(me.OperMsgField) {
			//处理意见
			JShell.Msg.confirm({
				title: '<div style="text-align:center;">处理意见</div>',
				msg: '',
				closable: false,
				multiline: true //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSaveClick(me.OverName);
			});
		} else {
			//确定进行该操作
			//			JShell.Msg.confirm({
			//				msg: '确定进行该操作？'
			//			}, function(but, text) {
			//				if(but != "ok") return;
			//				
			//			});
			me.onSaveClick(me.OverName);
		}
	},
	/**未通过*/
	onBack: function() {
		var me = this;
		if(me.OperMsgField) {
			//处理意见
			JShell.Msg.confirm({
				title: '<div style="text-align:center;">处理意见</div>',
				msg: '',
				closable: false,
				multiline: true //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSaveClick(me.BackName);
			});
		} else {
			//确定进行该操作
			//			JShell.Msg.confirm({
			//				msg: '确定进行该操作？'
			//			}, function(but, text) {
			//				if(but != "ok") return;
			//				
			//			});
			me.onSaveClick(me.BackName);
		}
	},
	/**获取状授权类型信息*/
	getLicenceTypeData: function(LicenceTypeList) {
		var me = this,
			data = [];
		for(var i in LicenceTypeList) {
			var obj = LicenceTypeList[i];
			var style = ['font-weight:bold;']; //text-align:center
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**刷新方法*/
	onRefreshClick: function() {
		var me = this;
		switch(me.getActiveTab().itemId) {
			case "ProgramLicenceGrid":
				me.EditTabPanel.ProgramLicenceGrid.load();
				break;
			case "EquipLicenceGrid":
				me.EditTabPanel.EquipLicenceGrid.load();
				break;
			default:
				break;
		}
	},
	/**提交点击处理方法*/
	onSaveClick: function(StatusName) {
		var me = this;
		var isSave = true;
		var msg = "";
		if(me.AHServerLicence == null) {
			isSave = false;
			msg = msg + "授权申请信息为空!<br />";
		}
		var count1 = me.EditTabPanel.ProgramLicenceGrid.store.getCount();
		var count2 = me.EditTabPanel.EquipLicenceGrid.store.getCount();
		if(count1 < 1 && count2 < 1) {
			isSave = false;
			msg = msg + "授权申请的程序及仪器信息为空!<br />";
		}
		if(isSave) {
			if(!JShell.System.ClassDict.LicenceStatus) {
				JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
					if(!JShell.System.ClassDict.LicenceStatus) {
						isSave = false;
						JShell.Msg.error('未获取到授权状态，请重新保存!');
						return;
					}
				});
			}
			if(isSave) {
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', StatusName);
				if(info == null || info == undefined) {
					JShell.Msg.error('获取授权状态出错，请重新保存!');
					return;
				} else {
					me.onSave(info.Id);
				}
			}
		} else {
			JcallShell.Msg.error(msg);
		}
	},
	onSave: function(Status) {
		var me = this;
		var isSave = true;
		var msg = "";

		if(me.AHServerLicence == null) {
			isSave = false;
			msg = msg + "授权申请信息为空!<br />";
		}
		var count1 = me.EditTabPanel.ProgramLicenceGrid.store.getCount();
		var count2 = me.EditTabPanel.EquipLicenceGrid.store.getCount();
		if(count1 < 1 && count2 < 1) {
			isSave = false;
			msg = msg + "授权申请的程序及仪器信息为空!<br />";
		}
		if(isSave == true) {
			var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
			//判断授权类型是否为空,为空不能保存
			me.EditTabPanel.ProgramLicenceGrid.store.each(function(record) {
				var LicenceTypeId = record.get("LicenceTypeId");
				if(isSave == true && (LicenceTypeId == undefined || LicenceTypeId == null || LicenceTypeId == "")) {
					isSave = false;
					msg = msg + "程序明细的SQH为" + record.get("SQH") + "的授权类型不能为空!<br />";
				}
				//如果授权类型不是商业,截止日期不能为空
				else if(isSave == true && LicenceTypeId != info.Id) {
					var licenceDate = record.get("LicenceDate");
					var nodeTableCounts = record.get("NodeTableCounts");
					if((licenceDate == undefined || licenceDate == null || licenceDate == "") && nodeTableCounts != "" && parseInt(nodeTableCounts) > 0) {
						isSave = false;
						var info2 = JShell.System.ClassDict.getClassInfoById('LicenceType', LicenceTypeId)
						msg = msg + "程序明细授权类型为" + info2.Name + ",SQH为" + record.get("SQH") + "截止日期不能为空!<br />";
					}
				}
			});
		}

		if(isSave == true) {
			var Sysdate = JcallShell.System.Date.getDate();
			var ReviewDate = JcallShell.Date.toString(Sysdate);
			var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
			me.AHServerLicence["Status"] = Status;

			var AHServerLicence = {};
			AHServerLicence.Id = me.AHServerLicence.Id;
			AHServerLicence.LRNo1 = me.AHServerLicence.LRNo1;
			AHServerLicence.LRNo2 = me.AHServerLicence.LRNo2;
			AHServerLicence.LRNo = me.AHServerLicence.LRNo;

			AHServerLicence.Status = Status;
			var isSpecially = 0;
			if(me.IsSpeciallyValue == true)
				isSpecially = 1;
			AHServerLicence["IsSpecially"] = isSpecially;

			//处理意见
			if(me.OperMsgField) {
				if(me.OperMsg != null && me.OperMsg != undefined) {
					me.OperMsg = me.OperMsg.replace(/\\/g, '&#92');
					me.OperMsg = me.OperMsg.replace(/[\r\n]/g, '<br />');
				}
				AHServerLicence[me.OperMsgField] = me.OperMsg;
			}
			if(me.AuditDataTimeMsgField && ReviewDateStr) {
				AHServerLicence[me.AuditDataTimeMsgField] = ReviewDateStr;
			}
			var entity = {
				AHServerLicence: AHServerLicence,
				ApplyProgramInfoList: null,
				AHServerEquipLicenceList: null
			};
			var programList = [],
				equipList = [];
			var index = 0;
			var licencePass = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权通过');
			var specialApprovalPass = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '特批授权通过');
			//找出所有的手工新增的站点的行及被修改过的行
			me.EditTabPanel.ProgramLicenceGrid.store.each(function(record) {
				//行是否被修改过
				var dirty = true; // record.dirty;
				//如果是商务授权通过并且不需要特批或者是特批授权通过,需要全部明细传回
				//				if(AHServerLicence.Status == licencePass.Id && AHServerLicence.IsSpecially == 0) {
				//					dirty = true;
				//				} else if(AHServerLicence.Status == specialApprovalPass.Id) {
				//					dirty = true;
				//				}
				if(dirty) {
					var obj = {};
					var ServerLicenceID = record.get("ServerLicenceID");
					if(ServerLicenceID != null && ServerLicenceID != "") {
						obj["ServerLicenceID"] = record.get("ServerLicenceID");
					} else {
						obj["ServerLicenceID"] = me.PK;
					}
					obj["SQH"] = record.get("SQH");
					var LicenceTypeId = record.get("LicenceTypeId");
					if(LicenceTypeId != null && LicenceTypeId != "") {
						obj["LicenceTypeId"] = LicenceTypeId;
					}
					//如果是临时的类型,截止日期不能为空
					var licenceDate = record.get("LicenceDate");
					if(licenceDate != null && licenceDate != "") {
						licenceDate = JShell.Date.toServerDate(licenceDate);
						obj["LicenceDate"] = licenceDate;
					}
					var ProgramID = record.get("ProgramID");
					if(ProgramID != null && ProgramID != "") {
						obj["ProgramID"] = ProgramID;
					}
					obj["ProgramName"] = record.get("ProgramName");
					obj["NodeTableCounts"] = record.get("NodeTableCounts");
					programList.push(obj);
				}
			});

			index = 0;
			me.EditTabPanel.EquipLicenceGrid.store.each(function(record) {
				//行是否被修改过
				var dirty = true; // record.dirty;
				if(dirty) {
					var obj = {};
					index++;
					var ServerLicenceID = record.get("ServerLicenceID");
					if(ServerLicenceID != null && ServerLicenceID != "") {
						obj["ServerLicenceID"] = record.get("ServerLicenceID");
					} else {
						obj["ServerLicenceID"] = me.PK;
					}
					obj["SNo"] = record.get("SNo");
					var LicenceTypeId = record.get("LicenceTypeId");
					if(LicenceTypeId != null && LicenceTypeId != "") {
						obj["LicenceTypeId"] = LicenceTypeId;
					}
					obj["SQH"] = record.get("SQH");
					var licenceDate = record.get("LicenceDate");
					if(licenceDate != null && licenceDate != "") {
						licenceDate = JShell.Date.toServerDate(licenceDate);
						obj["LicenceDate"] = licenceDate;
					}
					obj["LicenceKey"] = record.get("LicenceKey");
					obj["IsUse"] = 1;
					obj["ISNewApply"] = 0;
					obj["DispOrder"] = index;
					var EquipID = record.get("EquipID");
					if(EquipID != null && EquipID != "") {
						obj["EquipID"] = record.get("EquipID");
					}
					obj["EquipName"] = record.get("EquipName");
					obj["ProgramName"] = record.get("ProgramName");
					obj["NodeTableName"] = record.get("NodeTableName");
					obj["Equipversion"] = record.get("Equipversion");
					obj["Id"] = record.get("Id");
					equipList.push(obj);
				}
			});

			entity.ApplyProgramInfoList = programList;
			entity.AHServerEquipLicenceList = equipList;

			var params = Ext.JSON.encode({
				entity: entity,
				fields: 'Id,Status'
			});
			me.showMask("数据提交保存中...");
			var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					me.hideMask(); //隐藏遮罩层
					if(data.value != null && data.value.id)
						me.PK = data.value.id;
					JShell.Msg.alert("服务器授权信息保存成功", null, 1000);
					me.fireEvent('save', me);
				} else {
					me.hideMask();
					JShell.Msg.error("服务器授权信息保存失败!<br />" + data.msg);
				}
			});
		} else {
			JShell.Msg.error(msg);
		}
	}
});