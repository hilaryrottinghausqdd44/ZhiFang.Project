/**
 * 人员信息维护
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.puser.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Ext.ux.CheckColumn'
	],
	title: '人员信息列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdatePUserByField',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelPUser',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'PUser_DispOrder',
		direction: 'ASC'
	}],
	/**医生集合信息*/
	doctorList: [],
	/**用户UI配置Key*/
	userUIKey: 'puser.Grid',
	/**用户UI配置Name*/
	userUIName: "人员信息列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '用户编码/名称/Code1/Code2/Code3',
			itemId: "search",
			isLike: true,
			fields: ['puser.Id', 'puser.CName', 'puser.Code1', 'puser.Code2', 'puser.Code3']
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'GridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '用户编码',
			dataIndex: 'PUser_Id',
			isKey: true,
			width: 85,
			defaultRenderer: true
		}, {
			text: '帐号',
			dataIndex: 'PUser_ShortCode',
			width: 85,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'PUser_CName',
			width: 85,
			defaultRenderer: true
		}, {
			text: '密码',
			dataIndex: 'PUser_Password',
			width: 85,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '医生编码',
			dataIndex: 'PUser_Doctor_Id',
			width: 65,
			defaultRenderer: true
		}, {
			text: '绑定医生',
			dataIndex: 'PUser_Doctor_CName',
			width: 65,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '绑定',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			hidden: true,
			menuDisabled: true,
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					//me.onShowBindDoctor(rec);
				}
			}]
		}, {
			text: '身份类型',
			dataIndex: 'PUser_Usertype',
			width: 85,
			editor: {
				xtype: 'uxSimpleComboBox',
				value: '0',
				hasStyle: true,
				data: [
					['检验技师', '检验技师', 'color:green;'],
					['医生', '医生', 'color:black;'],
					['护士', '护士', 'color:black;'],
					['护工', '护工', 'color:black;']
				]
			},
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'PUser_Visible',
			width: 40,
			align: 'center',
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '绑定医生Code1',
			dataIndex: 'PUser_Doctor_Code1',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '用户Code1',
			dataIndex: 'PUser_Code1',
			width: 95,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '用户Code2',
			dataIndex: 'PUser_Code2',
			width: 95,
			editor: {},
			defaultRenderer: true
		}, {
			text: '用户Code3',
			dataIndex: 'PUser_Code3',
			width: 95,
			editor: {},
			defaultRenderer: true
		}, {
			text: '用户Code4',
			dataIndex: 'PUser_Code4',
			width: 95,
			editor: {},
			defaultRenderer: true
		}, {
			text: '用户Code5',
			dataIndex: 'PUser_Code5',
			width: 95,
			editor: {},
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'PUser_DispOrder',
			width: 75,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];
		columns.push({
			dataIndex: me.DelField,
			text: '操作结果',
			width: 60,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if (value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if (value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if (msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		if (me.hasRefresh) items.push('refresh');
		if (me.hasAdd) items.push('add');
		if (me.hasEdit) items.push('edit');
		if (me.hasDel) items.push('del');
		if (me.hasShow) items.push('show');

		items.push('-', {
			fieldLabel: '', //身份类型
			labelWidth: 0,
			width: 100,
			name: 'Usertype',
			itemId: 'Usertype',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: [
				['', '全部', 'color:black;'],
				['检验技师', '检验技师', 'color:green;'],
				['医生', '医生', 'color:black;'],
				['护士', '护士', 'color:black;'],
				['护工', '护工', 'color:black;']
			],
			listeners: {
				change: function(p, newV, oldV, e) {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '', //是否绑定
			labelWidth: 0,
			width: 95,
			name: 'IsBindDoctor',
			itemId: 'IsBindDoctor',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: [
				["", '全部', 'color:black;'],
				["puser.Doctor.Id is not null", "已绑定医生", 'color:green;'],
				["puser.Doctor.Id is null", "未绑定医生", 'color:orange;']
			],
			listeners: {
				change: function(p, newV, oldV, e) {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 75,
			name: 'OfSyncField',
			itemId: 'OfSyncField',
			xtype: 'uxSimpleComboBox',
			value: 'PUser_Code1',
			//hidden: true,
			hasStyle: true,
			data: [
				['PUser_Id', '按编码', 'color:black;'],
				['PUser_CName', '按名称', 'color:black;'],
				['PUser_Code1', '按Code1', 'color:black;'],
				['PUser_Code2', '按Code2', 'color:black;'],
				['PUser_Code3', '按Code3', 'color:black;'],
				['PUser_Code4', '按Code4', 'color:black;'],
				['PUser_Code4', '按Code5', 'color:black;']
			]
		}, {
			xtype: 'button',
			text: '绑定医生',
			iconCls: 'button-edit',
			handler: function() {
				me.onBindDoctor();
			}
		});
		if (me.hasSave) items.push('save');

		items.push('-', {
			xtype: 'splitbutton',
			text: '同步',
			iconCls: 'button-down',
			menu: [{
				iconCls: 'button-exp',
				text: '将人员导入到集成平台',
				handler: function() {
					me.onSync("emptolimp");
				}
			}, {
				iconCls: 'button-exp',
				text: '将帐号导入到集成平台',
				handler: function() {
					//前台调用后台服务
					//me.onSync("usertolimp");

					//前台调用集成平台服务
					var limpUrl = JcallShell.BLTF.RunParams.Lists.IPlatformServiceAccessURL.Value;
					if (!limpUrl) {
						JShell.BLTF.RunParams.getRunParamsValue("IPlatformServiceAccessURL", false, function(data) {
							limpUrl = JcallShell.BLTF.RunParams.Lists.IPlatformServiceAccessURL.Value;
							me.onSyncLIMP(limpUrl);
						});
					} else {
						me.onSyncLIMP(limpUrl);
					}
				}
			}, {
				iconCls: 'button-import',
				text: '从集成平台导入人员帐号',
				hidden: true,
				handler: function() {
					me.onSync("oflimp");
				}
			}]
		});
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},

	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		me.internalWhere = params.join(" and ");
		return me.callParent(arguments);
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var usertype = buttonsToolbar.getComponent('Usertype');
		var isBindDoctor = buttonsToolbar.getComponent('IsBindDoctor');
		var params = [];
		if (usertype) {
			var value = usertype.getValue();
			if (value) {
				params.push("puser.Usertype='" + value + "'");
			}
		}
		if (isBindDoctor) {
			var value1 = "" + isBindDoctor.getValue();
			if (value1) {
				params.push(value1);
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				params.push("(" + me.getSearchWhere(value) + ")");
			}
		}
		return params;
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("PUser_Id");
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.BloodTransfusion', //类域
			className: 'UpdateOperationType', //类名
			title: '人员信息操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='PUser'"
		};
		var win = JShell.Win.open('Shell.class.blood.scoperation.SCOperation', config);
		win.show();
	},
	/**
	 * @description 同步数据
	 * @param {Object} type1
	 */
	onSync: function(type1) {
		var me = this;
		var url = JShell.System.Path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfHREmployeeToLIMP";
		if (type1 == "usertolimp") {
			url = JShell.System.Path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfRBACUseToLIMP";
		}
		var params = Ext.JSON.encode({
			syncType: type1
		});
		setTimeout(function() {
			me.showMask("导入处理中,请耐心等待!"); //显示遮罩层
			JShell.Server.post(url, params, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {

				} else {

				}
			});
		}, 100 * 1);
	},
	/**
	 * @description 前台直接调用集成平台服务
	 * @description 导入数据
	 * @param {Object} type1
	 */
	onSyncLIMP: function(limpUrl) {
		var me = this;
		var records = me.getSelectionModel().getSelection(), //获取修改过的行记录
			len = records.length;
		if (len <= 0) return;

		me.showMask("数据导入处理中,请稍等...."); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			if (parseInt(id) <= 0) {
				me.saveCount++;
				rec.set(me.DelField, true);
				rec.commit();
				continue;
			}
			me.onAddUser(i, rec, limpUrl);
		}
	},
	onAddUser: function(index, rec, limpUrl) {
		var me = this;
		var id = rec.get(me.PKField);
		//http://localhost/ZhiFang.LabInformationIntegratePlatform
		if (limpUrl.indexOf("/") < 0) limpUrl = limpUrl + "/";
		var url = limpUrl + "ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACUser";

		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var password = rec.get("PUser_Password");
		var params = Ext.JSON.encode({
			entity: {
				LabID: 0,
				Id: id,
				IsUse: 1,
				EnMPwd: 1,
				PwdExprd: 1,
				AccLock: 0,
				CName: rec.get("PUser_CName"),
				ShortCode: rec.get("PUser_ShortCode"),
				Account: rec.get("PUser_ShortCode"),
				PWD: password,
				StandCode: rec.get("PUser_Code1"),
				SName: rec.get("PUser_Code2"),
				DispOrder: rec.get("PUser_DispOrder"),
				HREmployee: {
					Id: id,
					DataTimeStamp: dataTimeStamp
				}
			}
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	onBindDoctor: function() {
		var me = this;
		if (me.doctorList.length > 0) {
			me.setBindDoctor();
		} else {
			me.showMask("获取医生信息中..."); //显示遮罩层
			var fields = "Doctor_Id,Doctor_CName,Doctor_Code1,Doctor_Code2,Doctor_Code3";
			var url = JShell.System.Path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true';
			url += "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if (data.success) {
					me.doctorList = data.value.list;
					me.setBindDoctor();
				} else {

				}
			});
		}
	},
	setBindDoctor: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var ofSyncField = buttonsToolbar.getComponent('OfSyncField').getValue();
		if (!ofSyncField || !me.doctorList || me.doctorList.length <= 0) return;

		me.store.each(function(record) {
			if (record.get("PUser_Doctor_Id")) {
				//return false;
			}
			var puserVal = "" + record.get(ofSyncField);
			if (!puserVal) {
				//return false;
			}

			var doctorCode = record.get("Doctor_Code1");
			if (ofSyncField == "PUser_Code1") {
				doctorCode = "Doctor_Code1";
			} else if (ofSyncField == "PUser_Code2") {
				doctorCode = "Doctor_Code2";
			} else if (ofSyncField == "PUser_Code3") {
				doctorCode = "Doctor_Code3";
			} else if (ofSyncField == "PUser_Code4") {
				doctorCode = "Doctor_Code4";
			} else if (ofSyncField == "PUser_Code5") {
				doctorCode = "Doctor_Code5";
			} else if (ofSyncField == "PUser_Id") {
				doctorCode = "Doctor_Id";
			} else if (ofSyncField == "PUser_CName") {
				doctorCode = "Doctor_CName";
			}

			for (var i = 0; i < me.doctorList.length; i++) {
				var doctorVal = "" + me.doctorList[i][doctorCode];
				if (!doctorVal) {
					continue;
				}
				if (puserVal == doctorVal) {
					record.set("PUser_Doctor_Id", me.doctorList[i]["Doctor_Id"]);
					record.set("PUser_Doctor_CName", me.doctorList[i]["Doctor_CName"]);
					record.set("PUser_Doctor_Code1", me.doctorList[i]["Doctor_Code1"]);
					if (!record.get("PUser_Usertype")) record.set("PUser_Usertype", "医生");

					break;
				}
			}
		});
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var IsUse = rec.get('PUser_Visible');
		if (IsUse == true || IsUse == 1) {
			IsUse = 1;
		} else {
			IsUse = 0;
		}
		var entity = {
			Id: id,
			Visible: IsUse,
			Doctor: null,
			Usertype: rec.get("PUser_Usertype"),
			Code1: rec.get('PUser_Code1'),
			Code2: rec.get('PUser_Code2'),
			Code3: rec.get('PUser_Code3'),
			Code4: rec.get('PUser_Code4'),
			Code5: rec.get('PUser_Code5'),
			DispOrder: rec.get('PUser_DispOrder')
		};
		if (rec.get("PUser_Doctor_Id")) {
			entity.Doctor = {
				Id: rec.get("PUser_Doctor_Id")
			};
		}
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var params = Ext.JSON.encode({
			entity: entity,
			empID: empID,
			empName: empName,
			fields: 'Id,Visible,Usertype,Doctor_Id,Code1,Code2,Code3,Code4,Code5,DispOrder'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	}
});
