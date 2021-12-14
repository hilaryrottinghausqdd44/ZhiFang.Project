/**
 * 文档抄送/阅读对象
 * @author
 * @version 2016-06-23
 * @version 2017-11-29 添加对localStorage的支持
 */
Ext.define('Shell.class.qms.file.copyuser.userComboBox', {
	extend: 'Ext.form.Panel',
	alias: 'widget.userComboBox',
	title: '',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	objectEName: 'FFileCopyUser',
	/**获取数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileCopyUserByHQL?isPlanish=true',
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
	IdStrValue: '',
	/**显示值*/
	DisplayValue: '',
	/**类型*/
	defaultTypeValue: null,
	/**返回的数组*/
	FileValueUserList: null,

	labelWidth: 60,
	height: 28,
	valueArr: [],
	PK: '',
	formtype: 'add',
	/**按人员选择时,员工所属的角色名称*/
	RoleHREmployeeCName: "",
	header: false,
	split: false,
	border: false,
	allowBlank: true,
	emptyText: '请选择',
	/*类型数组*/
	TypeArrData: null,
	hasNull: true,
	/**是否启用localStorage*/
	openLocalStorage: true,
	/**localStorage的key值*/
	itemKey: "",
	/**重写渲染完毕执行*/
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.linkage();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeResult');
		me.width = me.width || 400;
		me.TypeArrData = me.TypeArrData || JcallShell.QMS.Enum.getList('FFileCopyUserType', false, false, me.hasNull);
		me.defaultTypeValue = (me.defaultTypeValue != null ? me.defaultTypeValue : me.TypeArrData[0][0]);
		me.RoleHREmployeeCName = me.RoleHREmployeeCName || "";
		me.items = me.items || me.createItems();

		me.callParent(arguments);
	},
	linkage: function() {
		var me = this;
		if(me.formtype != 'add' && me.defaultLoad == true && me.PK != "") {
			me.loadDataById(me.PK);
			me.PK = '';
		}
		if(me.formtype == 'add') me.setValue();
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
			itemId: 'FFileCopyUser_Type',
			name: 'FFileCopyUser_Type',
			width: 153,
			hasStyle: true,
			allowBlank: me.allowBlank,
			emptyText: me.emptyText,
			data: me.TypeArrData,
			value: me.defaultTypeValue,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
					FileCopy.setValue('');
					switch(newValue) {
						case '-1':
							FileCopy.setVisible(false);
							FileCopy.allowBlank = true;
							break;
						case '1':
							FileCopy.setVisible(false);
							FileCopy.allowBlank = true;
							break;
						default:
							FileCopy.setVisible(true);
							FileCopy.allowBlank = me.allowBlank;
							break;
					}
					me.defaultTypeValue = newValue;
					me.fireEvent('changeResult', me, me.getValue());
				}
			}
		}, {
			x: 158,
			y: 0,
			width: me.width - 165,
			fieldLabel: '',
			name: 'FFileCopyUser_FileCopy',
			itemId: 'FFileCopyUser_FileCopy',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			checkOne: false,
			enableKeyEvents: false,
			editable: false,
			value: me.ParentName,
			hidden: true,
			allowBlank: me.allowBlank,
			emptyText: me.emptyText,
			onTriggerClick: function() {
				var Type = me.getComponent('FFileCopyUser_Type');
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
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
		var values = me.getForm().getValues();
		switch(values.FFileCopyUser_Type) {
			case '2': //按部门
				com = 'Shell.class.qms.file.copyuser.DeptCheckTree';
				break;
			case '3': //按角色
				com = 'Shell.class.qms.file.copyuser.CheckRoleGrid';
				break;
			case '4': //按人员
				com = 'Shell.class.qms.file.copyuser.CheckApp';
				//'Shell.class.qms.file.copyuser.CheckEmployeeRoleGrid';
				break;
			default:
				break;
		}
		JShell.Win.open(com, {
			selectId: me.ParentID, //默认选中节点ID
			hideNodeId: me.PK, //默认隐藏节点ID
			itemId: 'FFileCopyUser_COM',
			PKCheckField: '',
			searchInfoWidth: '80%',
			RoleHREmployeeCName: me.RoleHREmployeeCName,
			listeners: {
				accept: function(p, record) {
					me.onParentModuleAccept(record);
					p.close();
				},
				loadtree:function(p){//部门选择还原
					var Ids = [],strIds="";
					if(me.IdStrValue != null && me.IdStrValue != '') me.IdStrValue = 0 == me.IdStrValue.indexOf(',') ? me.IdStrValue.substr(1) : me.IdStrValue;
					var Arr = [],
						obj = {};
					FileCopyUserobj = me.IdStrValue.split(",");
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
								Ids.push(value);
							});
						}
					}
					if(Ids.length>0)strIds=Ids.splice(',');
					if(strIds.length>0){
						var str =strIds.join(',');
						p.selectId = str;
					}
				},
				load: function(p) {
					if(me.IdStrValue != null && me.IdStrValue != '') me.IdStrValue = 0 == me.IdStrValue.indexOf(',') ? me.IdStrValue.substr(1) : me.IdStrValue;
					var Arr = [],
						obj = {};
					FileCopyUserobj = me.IdStrValue.split(",");
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
								switch(values.FFileCopyUser_Type) {
									case '-1':
										FileCopy.setVisible(false);
										FileCopy.allowBlank = true;
										break;
									case '1':
										FileCopy.setVisible(false);
										FileCopy.allowBlank = true;
										break;
									case '2':
//										index = p.store.findExact('HRDept_Id', value);
//										if(index != -1) {
//											p.getSelectionModel().select(index, true, false)
//										}
										break;
									case '3':
										index = p.store.findExact('RBACRole_Id', value);
										if(index != -1) {
											p.getSelectionModel().select(index, true, false)
										}
										break;
									case '4':
										index = p.store.findExact('HREmployee_Id', value);
										if(index != -1) {
											p.getSelectionModel().select(index, true, false)
										}
										break;
									default:
										break;
								}
							})
						}
					}
				}
			}
		}).show();
	},
	setTypeAdnUserId: function(readOnly, isnull) {
		var me = this;
		var Type = me.getComponent('FFileCopyUser_Type');
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
		Type.setReadOnly(readOnly);
		FileCopy.setReadOnly(readOnly);
		if(isnull) {
			Type.setValue("");
			FileCopy.setValue("");
		}
		FileCopy.setVisible(false);
	},
	/**选择上级机构*/
	onParentModuleAccept: function(record) {
		var me = this;
		var values = me.getForm().getValues();
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
		var ParentID = me.getComponent('FFileCopyUser_HRDept_Id');
		var RoleID = me.getComponent('FFileCopyUser_RBACRole_Id');
		var EmpID = me.getComponent('FFileCopyUser_HREmployee_Id');
		var Type = me.getComponent('FFileCopyUser_Type');
		me.IdStrValue = '';
		me.DisplayValue = '';
		me.FileCopyUserArr = {};
		var entity = {};
		me.valueArr = [];

		var tempObj = {},
			tempModel = {};
		FileCopy.setVisible(true);
		for(var i in record) {
			switch(values.FFileCopyUser_Type) {
				case '-1':
					FileCopy.setVisible(false);
					break;
				case '1':
					FileCopy.setVisible(false);
					break;
				case '2':
				    me.IdStrValue += ',' + record[i].raw.tid;
					me.DisplayValue += ',' + record[i].raw.text;

//					me.IdStrValue += ',' + record[i].get('HRDept_Id');
//					me.DisplayValue += ',' + record[i].get('HRDept_CName');
					tempModel = {
						Id: record[i].raw.tid
//						Id: record[i].get('HRDept_Id')
					};
					tempObj = {
						"HRDept": tempModel
					};
					me.valueArr.push(tempObj);
					break;
				case '3':
					var RBACRole_Id = record[i].get('RBACRole_Id');
					var RBACRole_CName = record[i].get('RBACRole_CName');
					tempModel = {
						Id: RBACRole_Id
					}
					tempObj = {
						"RBACRole": tempModel
					};
					me.valueArr.push(tempObj);
					me.IdStrValue += ',' + RBACRole_Id;
					me.DisplayValue += ',' + RBACRole_CName;
					break;
				case '4': //员工
					me.IdStrValue += ',' + record[i].get('HREmployee_Id');
					me.DisplayValue += ',' + record[i].get('HREmployee_CName');
					//RBACEmpRoles_HREmployee_Id,RBACEmpRoles_HREmployee_CName
					tempModel = {
						Id: record[i].get('HREmployee_Id')
					}
					tempObj = {
						"User": tempModel
					};
					me.valueArr.push(tempObj);
					break;
				default:
					break;
			}
		}
		if(me.IdStrValue != null && me.IdStrValue != '') me.IdStrValue = 0 == me.IdStrValue.indexOf(',') ? me.IdStrValue.substr(1) : me.IdStrValue;
		if(me.DisplayValue != null && me.DisplayValue != '') me.DisplayValue = 0 == me.DisplayValue.indexOf(',') ? me.DisplayValue.substr(1) : me.DisplayValue;
		ParentID.setValue(me.IdStrValue);
		FileCopy.setValue(me.DisplayValue);
		RoleID.setValue(me.IdStrValue);
		FileCopy.setValue(me.DisplayValue);
		EmpID.setValue(me.IdStrValue);
		FileCopy.setValue(me.DisplayValue);

		me.fireEvent('changeResult', me, me.getValue());
	},

	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_Id'
		});
		items.push({
			fieldLabel: '组织结构ID',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_HRDept_Id',
			itemId: 'FFileCopyUser_HRDept_Id'
		});
		items.push({
			fieldLabel: '组织结构',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_HRDept_ParentName',
			itemId: 'FFileCopyUser_HRDept_ParentName'
		});
		items.push({
			fieldLabel: '角色ID',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_RBACRole_Id',
			itemId: 'FFileCopyUser_RBACRole_Id'
		});
		items.push({
			fieldLabel: '角色',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_RBACRole_CName',
			itemId: 'FFileCopyUser_RBACRole_CName'
		});
		items.push({
			fieldLabel: '人员主键ID',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_HREmployee_Id',
			itemId: 'FFileCopyUser_HREmployee_Id'
		});
		items.push({
			fieldLabel: '人员',
			hidden: true,
			xtype: 'textfield',
			name: 'FFileCopyUser_HREmployee_CName',
			itemId: 'FFileCopyUser_HREmployee_CName'
		});
		var names = [
			'FFileCopyUser_HRDept', 'FFileCopyUser_RBACRole', 'FFileCopyUser_HREmployee'
		];
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
	clearData: function() {
		var me = this;
		var Type = me.getComponent('FFileCopyUser_Type');
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
		me.FileValueUserList = {
			valueType: '',
			IdStrValue: "",
			DisplayValue: "",
			list: [],
			EmpIdArr: [],
			EmpNameArr: []
		};
		FileCopy.setValue("");
		Type.setValue(me.TypeArrData[0][0]);

		me.IdStrValue = "";
		me.DisplayValue = "";
	},
	/**返回要保存的值*/
	setValue: function(objValue) {
		var me = this;
		if(!objValue)
			objValue = {
				valueType: me.TypeArrData[0][0],
				list: [],
				IdStrValue: "",
				DisplayValue: ""
			};

		var Type = me.getComponent('FFileCopyUser_Type');
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');

		me.defaultTypeValue = objValue.valueType;
		me.valueArr = objValue.list;
		me.IdStrValue = objValue.IdStrValue;
		me.DisplayValue = objValue.DisplayValue;

		switch(me.defaultTypeValue) {
			case '-1':
				FileCopy.setVisible(false);
				FileCopy.allowBlank = true;
				break;
			case '1':
				FileCopy.setVisible(false);
				FileCopy.allowBlank = true;
				break;
			case '2': //按部门
				FileCopy.setVisible(true);
				break;
			case '3': //按角色
				FileCopy.setVisible(true);
				break;
			case '4': //按人员
				FileCopy.setVisible(true);
				break;
			default:
				FileCopy.allowBlank = me.allowBlank;
				break;
		}
		Type.setValue(me.defaultTypeValue);
		FileCopy.setValue(me.DisplayValue);
	},
	/**返回要保存的值*/
	getValue: function() {
		var me = this;
		var Type = me.getComponent('FFileCopyUser_Type');
		me.FileValueUserList = {
			valueType: Type.getValue(),
			list: me.valueArr,
			IdStrValue: me.IdStrValue,
			DisplayValue: me.DisplayValue,
			EmpIdArr: [],
			EmpNameArr: []
		};
		if(Type.getValue() == "4") { //员工中文名称的处理
			var EmpIdArr = me.IdStrValue.split(",");
			var EmpNameArr = me.DisplayValue.split(",");
			me.FileValueUserList.EmpIdArr = EmpIdArr;
			me.FileValueUserList.EmpNameArr = EmpNameArr;
		}
		return me.FileValueUserList;
	},
	/**显示值*/
	getdisplayFieldValue: function() {
		var me = this;
		if(me.DisplayValue != null && me.DisplayValue != '') me.DisplayValue = 0 == name.indexOf(',') ? me.DisplayValue.substr(1) : me.DisplayValue;
		return me.DisplayValue;
	},
	/**对外公开根据Id查询*/
	loadDataById: function(ffileId) {
		var me = this;
		var Type = me.getComponent('FFileCopyUser_Type');
		var FileCopy = me.getComponent('FFileCopyUser_FileCopy');
		Type.setValue("");
		FileCopy.setValue("");
		FileCopy.setVisible(true);
		FileCopy.allowBlank = me.allowBlank;
		me.valueArr = [];
		me.IdStrValue = "";
		me.DisplayValue = "";
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.objectEName + '_HRDept_Id,' + me.objectEName + '_HRDept_CName,' + me.objectEName + '_RBACRole_Id,' + me.objectEName + '_RBACRole_CName,' + me.objectEName + '_User_Id,' + me.objectEName + '_User_CName,' + me.objectEName + '_Type';
		if(ffileId != '') {
			url += '&where=' + Ext.util.Format.lowercase(me.objectEName) + '.FFile.Id=' + ffileId;
		}
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.valueArr = [];
				var tempObj = {},
					tempModel = {};
				var value = data.value.list;
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				if(value != '' && value != null) {
					me.defaultTypeValue = value[0][me.objectEName + '_Type'];
					for(var i = 0; i < value.length; i++) {
						switch(me.defaultTypeValue) {
							case '-1':
								FileCopy.setVisible(false);
								FileCopy.allowBlank = true;
								break;
							case '1':
								FileCopy.setVisible(false);
								FileCopy.allowBlank = true;
								break;
							case '2': //按部门
								FileCopy.setVisible(true);
								me.IdStrValue += ',' + value[i][me.objectEName + '_HRDept_Id'];
								me.DisplayValue += ',' + value[i][me.objectEName + '_HRDept_CName'];
								tempModel = {
									Id: value[i][me.objectEName + '_HRDept_Id']
								};
								tempObj = {
									"HRDept": tempModel
								};
								me.valueArr.push(tempObj);
								break;
							case '3': //按角色
								FileCopy.setVisible(true);
								me.IdStrValue += ',' + value[i][me.objectEName + '_RBACRole_Id'];
								me.DisplayValue += ',' + value[i][me.objectEName + '_RBACRole_CName'];
								tempModel = {
									Id: value[i][me.objectEName + '_RBACRole_Id']
								}
								tempObj = {
									"RBACRole": tempModel
								};
								me.valueArr.push(tempObj);
								break;
							case '4': //按人员
								FileCopy.setVisible(true);
								me.IdStrValue += ',' + value[i][me.objectEName + '_User_Id'];
								me.DisplayValue += ',' + value[i][me.objectEName + '_User_CName'];
								tempModel = {
									Id: value[i][me.objectEName + '_User_Id']
								};
								tempObj = {
									"User": tempModel
								};
								me.valueArr.push(tempObj);
								break;
							default:
								FileCopy.setVisible(true);
								FileCopy.allowBlank = me.allowBlank;
								break;
						}
					}
					if(me.IdStrValue != null && me.IdStrValue != '') me.IdStrValue = 0 == me.IdStrValue.indexOf(',') ? me.IdStrValue.substr(1) : me.IdStrValue;
					if(me.DisplayValue != null && me.DisplayValue != '') me.DisplayValue = 0 == me.DisplayValue.indexOf(',') ? me.DisplayValue.substr(1) : me.DisplayValue;
				}
				Type.setValue(me.defaultTypeValue);
				FileCopy.setValue(me.DisplayValue);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false, 2000, false);
		switch(me.formtype) {
			case "show":
				Type.setRealOnly(true);
				FileCopy.setRealOnly(true);
				break;
			default:
				break;
		}
	}
});