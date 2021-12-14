/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.apply.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.class.wfm.authorization.ahserver.file.SelectAndUploadButton'
	],

	title: '服务器授权申请',
	width: 800,
	height: 500,

	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHAHServerLicenceAndDetails',

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**上传的授权申请文件的主要信息*/
	AHServerLicence: null,

	/**客户名称*/
	PClientName: null,
	/**客户ID*/
	PClientID: null,
	/**授权编码*/
	LicenceCode: null,
	uploaditemId: 0,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
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
			border: false,
			collapsible: false,
			itemId: 'SimpleGrid'
		});
		me.DetailsTabPanel = Ext.create('Shell.class.wfm.authorization.ahserver.apply.DetailsTabPanel', {
			region: 'center',
			header: false,
			border: false,
			height: me.height,
			itemId: 'DetailsTabPanel'
		});
		return [me.SimpleGrid, me.DetailsTabPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'button',
			itemId: 'btnRefresh',
			iconCls: 'button-refresh',
			text: "刷新",
			tooltip: "刷新",
			handler: function() {
				me.onRefreshClick();
			}
		}, '-');
		//默认员工ID
		var defaultUserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		//默认员工名称
		var defaultUserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		items.push({
			fieldLabel: '申请人Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'ApplyID',
			value: defaultUserID,
			name: 'ApplyID'
		}, {
			width: 165,
			labelWidth: 55,
			fieldLabel: '申请人',
			emptyText: '申请人',
			name: 'ApplyName',
			itemId: 'ApplyName',
			xtype: 'uxCheckTrigger',
			value: defaultUserName,
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: '申请人选择',
				height: 380,
				defaultLoad: true
			},
			listeners: {
				check: function(p, record) {
					var buttonsToolbar = me.getComponent('buttonsToolbar');
					var ApplyName = buttonsToolbar.getComponent('ApplyName');
					var ApplyID = buttonsToolbar.getComponent('ApplyID');
					ApplyName.setValue(record ? record.get('HREmployee_CName') : '');
					ApplyID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			}
		});
		items.push({
			fieldLabel: '客户选择',
			labelWidth: 65,
			name: 'PContract_PClientName',
			itemId: 'PContract_PClientName',
			emptyText: '请在上传授权文件前先选择客户',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.authorization.pclient.CheckGrid',
			width: 325,
			listeners: {
				check: function(p, record) {
					var LicenceCode = "";
					if (record != null)
						LicenceCode = record.get('PClient_LicenceCode');
					if (LicenceCode == "" || LicenceCode == null || LicenceCode == undefined) {
						Ext.Msg.show({
							title: '错误提示',
							modal: true,
							msg: "<b>客户的授权编码信息为空,<br />不能进行服务器授权申请,<br />请维护好客户的授权编码后再操作!</b>",
							width: 280,
							buttons: Ext.Msg.YES,
							icon: Ext.Msg.ERROR
						});
					} else {
						var buttonsToolbar = me.getComponent('buttonsToolbar');
						var PContractPClientName = buttonsToolbar.getComponent('PContract_PClientName');
						var btnUpload = buttonsToolbar.getComponent("uploaditem-" + me.uploaditemId);
						if (PContractPClientName != undefined)
							PContractPClientName.setValue(record ? record.get('PClient_Name') : '');
						me.PClientName = (record ? record.get('PClient_Name') : '');
						me.PClientID = (record ? record.get('PClient_Id') : '');
						me.LicenceCode = (record ? LicenceCode : '');
						btnUpload.PClientID = me.PClientID;
						btnUpload.LicenceCode = me.LicenceCode;
						btnUpload.PClientName = me.PClientName;
						p.close();
					}
				}
			}
		});

		items.push(me.createSelectAndUploadButton());
		items.push('-', {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "保存",
			tooltip: "保存",
			style: {
				marginRight: '70px'
			},
			handler: function() {
				me.onSaveClick();
			}
		});
		return items;
	},
	createSelectAndUploadButton: function() {
		var me = this;
		me.uploaditemId++;
		var file = {
			xtype: 'SelectAndUploadButton',
			itemId: "uploaditem-" + me.uploaditemId,
			name: 'file',
			text: "上传",
			tooltip: "上传",
			/**客户名称*/
			PClientName: me.PClientName,
			/**客户ID*/
			PClientID: me.PClientID,
			/**授权编码*/
			LicenceCode: me.LicenceCode,
			listeners: {
				fileselected: function(file, files, newValue) {
					//					if(me.isIE10Less()) {
					//IE10以下版本,选择一个附件后,创建新的上传组件并添加到工具栏中,隐藏上一个的上传组件
					var fileSelectButton = me.createSelectAndUploadButton();
					var tbardown = me.getComponent("buttonsToolbar");
					tbardown.insert(5, fileSelectButton);
					file.setVisible(false);
					//					}
				},
				uploadSuccess: function(event, resultDataValue, fileupload, uploadForm) {
					me.uploadSuccess(event, resultDataValue, fileupload, uploadForm);
				},
				uploadFailure: function(event, response, fileupload, uploadForm) {
					me.uploadFailure(event, response, uploadForm);
				}
			}
		};
		return file;
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
	/**获取状授权类型信息*/
	getLicenceTypeData: function(LicenceTypeList) {
		var me = this,
			data = [];
		for (var i in LicenceTypeList) {
			var obj = LicenceTypeList[i];
			var style = ['font-weight:bold;']; //text-align:center
			if (obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/*左列表的事件监听联动右区域**/
	DetailsShow: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = record.get('Id');
			var code = record.get('Code');
			switch (code) {
				case "BEquip":
					me.DetailsTabPanel.setActiveTab(me.DetailsTabPanel.EquipLicenceGrid);
					break;
				default:
					me.DetailsTabPanel.setActiveTab(me.DetailsTabPanel.ProgramLicenceGrid);
					break;
			}
		}, null, 500);
	},
	/**上传成功*/
	uploadSuccess: function(event, resultDataValue, file, uploadForm) {
		var me = this;
		if (me.isIE10Less()) {
			//IE10以下版本,上传后需要删除上传组件
			var tbardown = me.getComponent("buttonsToolbar");
			tbardown.remove(file);
			if (uploadForm != null)
				Ext.destroy(uploadForm);
		}
		if (resultDataValue != null && resultDataValue != "" && resultDataValue != undefined) {
			//申请的程序类型数据加载
			me.SimpleGrid.LicenceProgramTypeList = resultDataValue.LicenceProgramTypeList;
			if (me.SimpleGrid.LicenceProgramTypeList != null) {
				me.SimpleGrid.store.loadData(me.SimpleGrid.LicenceProgramTypeList);
			} else {
				me.SimpleGrid.clearData();
			}
			//程序明细数据加载
			me.DetailsTabPanel.ProgramLicenceGrid.ApplyProgramInfoList = resultDataValue.ApplyProgramInfoList;
			if (me.DetailsTabPanel.ProgramLicenceGrid.ApplyProgramInfoList != null) {
				me.DetailsTabPanel.ProgramLicenceGrid.store.loadData(me.DetailsTabPanel.ProgramLicenceGrid.ApplyProgramInfoList);
			} else {
				me.DetailsTabPanel.ProgramLicenceGrid.clearData();
			}
			//仪器明细数据加载
			me.DetailsTabPanel.EquipLicenceGrid.AHServerEquipLicenceList = resultDataValue.AHServerEquipLicenceList;
			if (me.DetailsTabPanel.EquipLicenceGrid.AHServerEquipLicenceList != null) {
				me.DetailsTabPanel.EquipLicenceGrid.store.loadData(me.DetailsTabPanel.EquipLicenceGrid.AHServerEquipLicenceList);
			} else {
				me.DetailsTabPanel.EquipLicenceGrid.clearData();
			}
			me.AHServerLicence = resultDataValue.AHServerLicence;
		}
	},
	/**上传失败*/
	uploadFailure: function(event, response, file, uploadForm) {
		var me = this;
		if (me.isIE10Less()) {
			//IE10以下版本,上传后需要删除上传组件
			var tbardown = me.getComponent("buttonsToolbar");
			tbardown.remove(file);
			if (uploadForm != null)
				Ext.destroy(uploadForm);
		}
		var json = Ext.JSON.decode(response);
		JShell.Msg.error(json.ErrorInfo);
	},
	/**刷新方法*/
	onRefreshClick: function() {
		var me = this;
		switch (me.getActiveTab().itemId) {
			case "ProgramLicenceGrid":
				me.DetailsTabPanel.ProgramLicenceGrid.load();
				break;
			case "EquipLicenceGrid":
				me.DetailsTabPanel.EquipLicenceGrid.load();
				break;
			default:
				break;
		}
	},
	/**提交点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var isSave = true;
		var msg = "";
		if (me.AHServerLicence == null) {
			isSave = false;
			msg = msg + "授权申请信息为空!<br />";
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var ApplyName = buttonsToolbar.getComponent('ApplyName').getValue();
		var ApplyID = buttonsToolbar.getComponent('ApplyID').getValue();
		if (!ApplyID) {
			isSave = false;
			msg = msg + "申请人信息为空!<br />";
		}
		var count1 = me.DetailsTabPanel.ProgramLicenceGrid.store.getCount();
		var count2 = me.DetailsTabPanel.EquipLicenceGrid.store.getCount();
		if (count1 < 1 && count2 < 1) {
			isSave = false;
			msg = msg + "授权申请的程序及仪器信息为空!<br />";
		}
		if (isSave == true) {
			var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
			//判断授权类型是否为空,为空不能保存
			me.DetailsTabPanel.ProgramLicenceGrid.store.each(function(record) {
				var LicenceTypeId = record.get("LicenceTypeId");
				if (isSave == true && (LicenceTypeId == undefined || LicenceTypeId == null || LicenceTypeId == "")) {
					isSave = false;
					msg = msg + "程序明细的SQH为" + record.get("SQH") + "的授权类型不能为空!<br />";
				}
				//如果授权类型不是商业,截止日期不能为空
				else if (isSave == true && LicenceTypeId != info.Id) {
					var licenceDate = record.get("LicenceDate");
					var nodeTableCounts = record.get("NodeTableCounts");
					if ((licenceDate == undefined || licenceDate == null || licenceDate == "") && nodeTableCounts != "" &&
						parseInt(nodeTableCounts) > 0) {
						isSave = false;
						var info2 = JShell.System.ClassDict.getClassInfoById('LicenceType', LicenceTypeId)
						msg = msg + "程序明细授权类型为" + info2.Name + ",SQH为" + record.get("SQH") + "截止日期不能为空!<br />";
					}
				}
			});
		}

		if (isSave == true) {
			//申请人信息处理
			me.AHServerLicence.ApplyID = ApplyID;
			me.AHServerLicence.ApplyName = ApplyName;
			//服务器授权备注信息
			var values2 = me.DetailsTabPanel.MemoForm.getForm().getValues();
			me.AHServerLicence.Comment = values2.AHServerLicence_Comment;

			var entity = {
				AHServerLicence: me.AHServerLicence,
				ApplyProgramInfoList: null,
				AHServerEquipLicenceList: null
			};
			
			entity.ApplyProgramInfoList = me.getProgramList();
			entity.AHServerEquipLicenceList = me.getEquipList();

			var params = Ext.JSON.encode({
				entity: entity
			});
			me.showMask("数据提交保存中...");
			var url = (me.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.hideMask(); //隐藏遮罩层
					if (data.value != null && data.value.id)
						me.PK = data.value.id;
					JShell.Msg.alert("服务器授权申请保存成功", null, 1000);
					me.fireEvent('save', me);
				} else {
					me.hideMask();
					JShell.Msg.error("服务器授权申请保存失败!<br />" + data.msg);
				}
			});
		} else {
			JcallShell.Msg.error(msg);
		}
	},
	getProgramList: function() {
		var me = this;
		var programList = [];
		var index = 0;
		me.DetailsTabPanel.ProgramLicenceGrid.store.each(function(record) {
			var obj = {};
			index++;
			var ServerLicenceID = record.get("ServerLicenceID");
			if (ServerLicenceID != null && ServerLicenceID != "") {
				obj["ServerLicenceID"] = record.get("ServerLicenceID");
			} else {
				obj["ServerLicenceID"] = 0;
			}
			obj["SQH"] = record.get("SQH");
			var LicenceTypeId = record.get("LicenceTypeId");
			if (LicenceTypeId != null && LicenceTypeId != "") {
				obj["LicenceTypeId"] = LicenceTypeId;
			}
			//如果是临时的类型,截止日期不能为空
			var licenceDate = record.get("LicenceDate");
			if (licenceDate != null && licenceDate != "") {
				licenceDate = JShell.Date.toServerDate(licenceDate);
				obj["LicenceDate"] = licenceDate;
			}
			var ProgramID = record.get("ProgramID");
			if (ProgramID != null && ProgramID != "") {
				obj["ProgramID"] = ProgramID;
			}
			obj["ProgramName"] = record.get("ProgramName");
			obj["NodeTableCounts"] = record.get("NodeTableCounts");
			programList.push(obj);
		});
		return programList;
	},
	getEquipList: function() {
		var me = this;
		var equipList = [];
		var index = 0;
		me.DetailsTabPanel.EquipLicenceGrid.store.each(function(record) {
			var obj = {};
			index++;
			var ServerLicenceID = record.get("ServerLicenceID");
			if (ServerLicenceID != null && ServerLicenceID != "") {
				obj["ServerLicenceID"] = record.get("ServerLicenceID");
			} else {
				obj["ServerLicenceID"] = 0;
			}
			obj["SNo"] = record.get("SNo");
			var LicenceTypeId = record.get("LicenceTypeId");
			if (LicenceTypeId != null && LicenceTypeId != "") {
				obj["LicenceTypeId"] = LicenceTypeId;
			}
			obj["SQH"] = record.get("SQH");
			var licenceDate = record.get("LicenceDate");
			if (licenceDate != null && licenceDate != "") {
				licenceDate = JShell.Date.toServerDate(licenceDate);
				obj["LicenceDate"] = licenceDate;
			}
			obj["LicenceKey"] = record.get("LicenceKey");
			obj["IsUse"] = 1;
			obj["ISNewApply"] = 1;
			obj["DispOrder"] = index;
			var EquipID = record.get("EquipID");
			if (EquipID != null && EquipID != "") {
				obj["EquipID"] = record.get("EquipID");
			}
			obj["EquipName"] = record.get("EquipName");
			obj["ProgramName"] = record.get("ProgramName");
			obj["NodeTableName"] = record.get("NodeTableName");
			obj["Equipversion"] = record.get("Equipversion");
			equipList.push(obj);
		});
		return equipList;
	},
	/**
	 * Ext JS 4.1
	 * 判断是否IE10以下浏览器
	 * */
	isIE10Less: function() {
		var isIE = false;
		if (Ext.isIE6 || Ext.isIE7 || Ext.isIE8 || Ext.isIE9) isIE = true;
		return isIE;
	}
});
