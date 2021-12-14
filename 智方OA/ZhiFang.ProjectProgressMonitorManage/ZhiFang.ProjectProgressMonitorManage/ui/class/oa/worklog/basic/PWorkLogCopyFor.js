/**
 * 工作日志抄送关系
 * @author longfc
 * @version 2016-08-04
 */
Ext.define('Shell.class.oa.worklog.basic.PWorkLogCopyFor', {
	extend: 'Ext.form.Panel',
	alias: 'widget.pworklogcopyfor',
	title: '工作日志抄送关系',
	requires: [
		'Ext.tip.ToolTip',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogCopyForByHQL?isPlanish=true',
	/** 每个组件的默认属性*/
	defaults: {
		labelAlign: 'right'
	},
	layout: {
		type: 'absolute'
	},
	/**默认加载数据*/
	defaultLoad: false,

	fieldLabel: '',
	/**id值*/
	FileCopyUserId: '',
	/**显示值*/
	FileCopyUserName: '',
	/**类型*/
	FileCopyUserType: '',
	/**返回的数组*/
	FileValueUserList: null,
	PWorkLogCopyForType: [
		['-1', '无'],
		['4', '按人员']
	],
	labelWidth: 60,
	height: 28,
	PK: '',
	formtype: 'add',
	/**按人员选择时,员工所属的角色名称*/
	RoleHREmployeeCName: "",
	header: false,
	border: false,
	allowBlank: true,
	emptyText: '请选择',
	/**重写渲染完毕执行*/
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.formtype != 'add' && me.defaultLoad == true && me.PK != "") {
			me.loadDataById(me.PK);
			me.PK = '';
		}

	},
	clearData: function() {
		var me = this;
		var Type = me.getComponent('PWorkLogCopyFor_Type');
		var FileCopyUserID = me.getComponent('PWorkLogCopyFor_FileCopy');
		me.FileValueUserList = {
			EmpIdArr: [],
			EmpNameArr: [],
		};
		me.FileCopyUserId = "";
		me.FileCopyUserName = "";
		Type.setValue("-1");
		FileCopyUserID.setValue("");
	},
	setTypeAdnUserId: function(readOnly, isnull) {
		var me = this;
		var Type = me.getComponent('PWorkLogCopyFor_Type');
		var FileCopyUserID = me.getComponent('PWorkLogCopyFor_FileCopy');
		Type.setReadOnly(readOnly);
		FileCopyUserID.setReadOnly(readOnly);
		if(isnull) {
			Type.setValue("-1");
			FileCopyUserID.setValue("");
		}
	},
	initComponent: function() {
		var me = this;
		Ext.QuickTips.init();
		me.width = me.width || 400;
		me.PWorkLogCopyForType = me.PWorkLogCopyForType || JcallShell.QMS.Enum.getList('PWorkLogCopyForType', false, false, true);
		me.RoleHREmployeeCName = me.RoleHREmployeeCName || "";
		me.items = me.items || me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
			x: 1,
			y: 0,
			fieldLabel: me.fieldLabel,
			labelWidth: me.labelWidth,
			xtype: 'uxSimpleComboBox',
			itemId: 'PWorkLogCopyFor_Type',
			name: 'PWorkLogCopyFor_Type',
			width: 153,
			value: '-1',
			hasStyle: true,
			allowBlank: me.allowBlank,
			emptyText: me.emptyText,
			data: me.PWorkLogCopyForType,
			classConfig: {
				tooltip: '请选择'
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					switch(newValue) {
						case '':
							me.clearData();
							break;
						default:
							break;
					}
					var FileCopyUserID = me.getComponent('PWorkLogCopyFor_FileCopy');
					FileCopyUserID.setValue('');
					FileCopyUserID.setVisible(true);
					FileCopyUserID.allowBlank = me.allowBlank;
					me.FileCopyUserType = newValue;
				}
			}
		}, {
			x: 158,
			y: 0,
			fieldLabel: '',
			width: me.width - 165,
			itemId: 'PWorkLogCopyFor_FileCopy',
			name: 'PWorkLogCopyFor_FileCopy',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			checkOne: false,
			enableKeyEvents: false,
			editable: false,
			classConfig: {
				tooltip: '请选择'
			},
			allowBlank: me.allowBlank,
			emptyText: me.emptyText,
			onTriggerClick: function() {
				var Type = me.getComponent('PWorkLogCopyFor_Type');
				if(Type.getValue() != null && Type.getValue() != '-1') {
					me.createcombo();
				}
			}
		}];
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	createcombo: function() {
		var me = this;
		var com = '';
		var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(EmpID == null || EmpID == undefined) {
			EmpID = '-1';
		}
		var FileCopyUserID = me.getComponent('PWorkLogCopyFor_FileCopy');
		var values = me.getForm().getValues();
		com = 'Shell.class.qms.file.copyuser.CheckEmployeeRoleGrid';
		JShell.Win.open(com, {
			resizable: false,
			hideNodeId: me.PK, //默认隐藏节点ID
			itemId: 'PWorkLogCopyFor_COM',
			PKCheckField: '',
			width: 435,
			height: 495,
			searchInfoWidth:'84%',
			defaultWhere: 'rbacemproles.HREmployee.Id!=' + EmpID,
			RoleHREmployeeCName: me.RoleHREmployeeCName,
			listeners: {
				accept: function(p, record) {
					me.onAccept(record);
					p.close();
				},
				load: function(p) {
					if(me.FileCopyUserId != null && me.FileCopyUserId != '') me.FileCopyUserId = 0 == me.FileCopyUserId.indexOf(',') ? me.FileCopyUserId.substr(1) : me.FileCopyUserId;
					var Arr = [],
						obj = {};
					FileCopyUserobj = me.FileCopyUserId.split(",");
					for(var i = 0; i < FileCopyUserobj.length; i++) {
						var obj = {
							Id: FileCopyUserobj[i]
						};
						Arr.push(obj)
					}
					if(Arr && Arr != null) {
						var result = Ext.isArray(Arr);
						if(result) {
							Ext.Array.each(Arr, function(model) {
								var value = model["Id"];
								var index = '';
								switch(values.PWorkLogCopyFor_Type) {
									case '4':
										index = p.store.findExact('ReceiveEmpID', value);
										if(index != -1) {
											p.getSelectionModel().select(index, true, false)
										}
										break;
									default:
										break
								}
							})
						}
					}
				}
			}
		}).show();
	},
	/**选择人员处理*/
	onAccept: function(record) {
		var me = this;
		var values = me.getForm().getValues();
		var FileCopy = me.getComponent('PWorkLogCopyFor_FileCopy');
		var EmpID = me.getComponent('PWorkLogCopyFor_ReceiveEmpID');
		var Type = me.getComponent('PWorkLogCopyFor_Type');
		me.FileCopyUserId = '';
		me.FileCopyUserName = '';

		FileCopy.setVisible(true);
		for(var i in record) {
			switch(values.PWorkLogCopyFor_Type) {
				case '4': //员工
					me.FileCopyUserId += ',' + record[i].get('RBACEmpRoles_HREmployee_Id');
					me.FileCopyUserName += ',' + record[i].get('RBACEmpRoles_HREmployee_CName');
					break;
				default:
					break
			}
		}
		if(me.FileCopyUserId != null && me.FileCopyUserId != '') me.FileCopyUserId = 0 == me.FileCopyUserId.indexOf(',') ? me.FileCopyUserId.substr(1) : me.FileCopyUserId;
		if(me.FileCopyUserName != null && me.FileCopyUserName != '') me.FileCopyUserName = 0 == me.FileCopyUserName.indexOf(',') ? me.FileCopyUserName.substr(1) : me.FileCopyUserName;
		FileCopy.tooltip = me.FileCopyUserName;
		FileCopy.setValue(me.FileCopyUserName);
		EmpID.setValue(me.FileCopyUserId);
	},

	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			xtype: 'textfield',
			name: 'PWorkLogCopyFor_Id'
		});
		items.push({
			fieldLabel: '人员主键ID',
			hidden: true,
			xtype: 'textfield',
			name: 'PWorkLogCopyFor_ReceiveEmpID',
			itemId: 'PWorkLogCopyFor_ReceiveEmpID'
		});
		items.push({
			fieldLabel: '人员',
			hidden: true,
			xtype: 'textfield',
			name: 'PWorkLogCopyFor_ReceiveEmpName',
			itemId: 'PWorkLogCopyFor_ReceiveEmpName'
		});
		var names = ['PWorkLogCopyFor_HREmployee'];
		for(var i in names) {
			items = items.concat(me.createOneHideItem(names[i]));
		}
		return items;
	},
	/**创建一个隐形组件*/
	createOneHideItem: function(name) {
		return [{
			fieldLabel: '主键ID',
			hidden: true,
			xtype: 'textfield',
			name: name + '_Id',
			itemId: name + '_Id'
		}, {
			fieldLabel: '时间戳',
			hidden: true,
			xtype: 'textfield',
			name: name + '_DataTimeStamp',
			itemId: name + '_DataTimeStamp'
		}];
	},
	/**返回要保存的值*/
	getValue: function() {
		var me = this;
		var Type = me.getComponent('PWorkLogCopyFor_Type');
		me.FileValueUserList = {
			EmpIdArr: [],
			EmpNameArr: [],
		};
		if(Type.getValue() == "4") { //员工中文名称的处理
			var EmpIdArr = me.FileCopyUserId.split(",");
			var EmpNameArr = me.FileCopyUserName.split(",");
			if(EmpIdArr != null || EmpIdArr != "") {
				me.FileValueUserList.EmpIdArr = EmpIdArr;
			}
			if(EmpNameArr != null || EmpNameArr != "") {
				me.FileValueUserList.EmpNameArr = EmpNameArr;
			}
		}

		return me.FileValueUserList;
	},
	/**显示值*/
	getdisplayFieldValue: function() {
		var me = this;
		if(me.FileCopyUserName != null && me.FileCopyUserName != '') me.FileCopyUserName = 0 == name.indexOf(',') ? me.FileCopyUserName.substr(1) : me.FileCopyUserName;
		return me.FileCopyUserName;
	},
	/**对外公开根据Id查询*/
	loadDataById: function(logID) {
		var me = this;
		var Type = me.getComponent('PWorkLogCopyFor_Type');
		var FileCopyUserID = me.getComponent('PWorkLogCopyFor_FileCopy');
		Type.setValue("-1");
		FileCopyUserID.setValue("");
		FileCopyUserID.setVisible(true);
		FileCopyUserID.allowBlank = me.allowBlank;
		me.FileCopyUserId = "";
		me.FileCopyUserName = "";
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') +
			'fields=PWorkLogCopyFor_Id,PWorkLogCopyFor_LogType,PWorkLogCopyFor_LogID,PWorkLogCopyFor_ReceiveEmpID,PWorkLogCopyFor_ReceiveEmpName';
		if(logID != '') {
			url += '&where=' + 'pworklogcopyfor.LogID=' + logID;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var tempObj = {},
					tempModel = {};
				var value = data.value.list;
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				if(value != '' && value != null) {
					me.FileCopyUserType = '4';
					for(var i = 0; i < value.length; i++) {
						switch(me.FileCopyUserType) {
							case '4':
								me.FileCopyUserId += ',' + value[i]['PWorkLogCopyFor_ReceiveEmpID'];
								me.FileCopyUserName += ',' + value[i]['PWorkLogCopyFor_ReceiveEmpName'];
								break;
							default:
								FileCopyUserID.allowBlank = me.allowBlank;
								break
						}
					}
					if(me.FileCopyUserId != null && me.FileCopyUserId != '') me.FileCopyUserId = 0 == me.FileCopyUserId.indexOf(',') ? me.FileCopyUserId.substr(1) : me.FileCopyUserId;
					if(me.FileCopyUserName != null && me.FileCopyUserName != '') me.FileCopyUserName = 0 == me.FileCopyUserName.indexOf(',') ? me.FileCopyUserName.substr(1) : me.FileCopyUserName;
				}
				Type.setValue(me.FileCopyUserType);
				FileCopyUserID.setValue(me.FileCopyUserName);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false, 100, false);

	}
});