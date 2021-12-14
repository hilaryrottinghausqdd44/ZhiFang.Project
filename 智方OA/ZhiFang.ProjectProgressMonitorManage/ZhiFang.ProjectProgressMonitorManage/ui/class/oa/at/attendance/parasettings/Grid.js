/**
 * 员工考勤设置列表
 * @author longfc	
 * @version 2016-09-13
 */
Ext.define('Shell.class.oa.at.attendance.parasettings.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '员工考勤设置',
	width: 1200,
	height: 600,
	/**是否查询部门的子孙节点员工信息*/
	IsIncludeSubDept: false,
	/**部门ID*/
	DeptId: null,
	columnLines: true,
	/**获取数据服务路径*/
	selectUrl: '/WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventParaSettingsByField',
	/**删除数据服务路径*/
	delUrl: '/WeiXinAppService.svc/ST_UDTO_DelATEmpAttendanceEventParaSettings',
	/**新增数据服务路径*/
	addUrl: '/WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventParaSettings',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**显示成功信息*/
	showSuccessInfo: true,
	/**消息框消失时间*/
	hideTimes: 1000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 500,

	/**复选框*/
	multiSelect: true,
	//	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	defaultDate: "1970-01-01 ",
	SettingsTypeList: [
		[-1, '未设置'],
		[0, '固定时间'],
		[1, '弹性时间']
	],

	showAddInfo: "",
	initComponent: function() {
		var me = this;
		me.addEvents('addclick');
		me.buttonToolbarItems = ['refresh', {
			xtype: 'checkbox',
			boxLabel: '本部门',
			itemId: 'onlyShowDept',
			checked: !me.IsIncludeSubDept,
			height: 22,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, 'save', '->'];
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	comSetReadOnly: function(com, e, typeValue) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var record = records[0];
		var isReadOnly = false;
		if(record) {
			var oldValue = record.get("SetATEventParaSettingsType").toString();
			if(oldValue == typeValue) {
				isReadOnly = true;
				//e.stopEvent();
				var str = (typeValue == "0" ? "弹性" : "固定");
				JShell.Msg.alert("请设置考勤类型列为" + str + "时间后再操作", null, 1000);
			}
		}
		com.setReadOnly(isReadOnly);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '创建者ID',
			dataIndex: 'CreaterID',
			width: 110,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '创建者',
			dataIndex: 'CreaterName',
			width: 140,
			hidden: true,
			sortable: false,
			menuDisabled: true
		}, {
			text: '员工Id',
			dataIndex: 'EmpID',
			width: 140,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '员工姓名',
			dataIndex: 'EmpName',
			width: 140,
			//locked: true,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(record) {
					var oldValue = record.get("ATEventParaSettingsType").toString();
					if(oldValue == "-1" || oldValue == "") {
						meta.style = 'font-weight:bold;color:red'; //orange
					}
				}
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '复制',
			align: 'center',
			tooltip: '复制',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(value, meta, record, rowIndex, colIndex, store, veiw) {
					var oldValue = record.get("ATEventParaSettingsType").toString();
					if(oldValue == "-1" || oldValue == "") {
						return '';
					} else {
						return 'list-button-copy hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var records = me.getSelectionModel().getSelection();
					if(records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					var copyRecord = records[0];
					var result = me.JudgeSetAction(copyRecord);
					if(result)
						me.openChooseUsersForm(copyRecord);
				}
			}]
		}, {
			xtype: 'checkcolumn',
			text: '启用',
			dataIndex: 'IsUse',
			width: 45,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '考勤类型',
			dataIndex: 'ATEventParaSettingsType',
			width: 140,
			align: 'center',
			hidden: true,
			sortable: true,
			menuDisabled: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '考勤类型',
			dataIndex: 'SetATEventParaSettingsType',
			width: 80,
			align: 'center',
			sortable: true,
			menuDisabled: true,
			renderer: function(value, p, record) {
				var oldValue = record.get("ATEventParaSettingsType");
				if(value == null || value == "") {
					value = oldValue;
				}
				for(var i = 0; i < me.SettingsTypeList.length; i++) {
					if(value.toString() == me.SettingsTypeList[i][0].toString()) {
						return Ext.String.format(me.SettingsTypeList[i][1]);
					}
				}
			},
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'text',
				valueField: 'value',
				listClass: 'x-combo-list-small',
				store: new Ext.data.SimpleStore({
					fields: ['value', 'text'],
					data: me.SettingsTypeList
				}),
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('SetATEventParaSettingsType', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			})
		}, {
			text: '考勤地点坐标',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'ATEventPostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '考勤地点名称',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'ATEventPostionName',
			width: 160,
			sortable: true,
			menuDisabled: false,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '地点设置',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '地点设置',
			width: 65,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					//					if(record) {
					//						var oldValue = record.get("SetATEventParaSettingsType").toString();
					//						if(oldValue == "1") {
					//							isOpen = false;
					//							JShell.Msg.alert("请设置考勤类型列为固定时间后再操作", null, 1200);
					//						}
					//					}
					if(isOpen == true)
						me.openSetMapForm(0, "");
				}
			}]
		}, {
			text: '地点范围(米)',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'ATEventPostionRange',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ATEventPostionRange', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '上班时间',
			sortable: true,
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'SignInTime',
			type: 'time',
			xtype: 'datecolumn',
			width: 70,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						if(newValue != null && newValue != "") {
							if(newValue != null) {
								newValue = Ext.util.Format.date(newValue.toString(), 'H:i');
							}
							if(newValue != "") {
								newValue = JcallShell.Date.getDate(me.defaultDate + newValue);
							}
						}
						var record = com.ownerCt.editingPlugin.context.record;
						//						var EmpWorkLong = 0;
						//						if(record && newValue != "" && record.get("SignOutTime") != "") {
						//							EmpWorkLong = me.diffTime(newValue, record.get("SignOutTime"));
						//						}
						//						record.set('EmpWorkLong', EmpWorkLong);

						record.set('SignInTime', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '下班时间',
			sortable: true,
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'SignOutTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 70,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						if(newValue != null && newValue != "") {
							if(newValue != null) {
								newValue = Ext.util.Format.date(newValue.toString(), 'H:i');
							}
							if(newValue != "") {
								newValue = JcallShell.Date.getDate(me.defaultDate + newValue);
							}
						}
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '工作时长',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'EmpWorkLong',
			width: 65,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('EmpWorkLong', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '定时上报1时间',
			dataIndex: 'TimingOneTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 90,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value == null) value = "";
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '定时上报2时间',
			dataIndex: 'TimingTwoTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 90,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value == null) value = "";
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '定时上报3时间',
			dataIndex: 'TimingThreeTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 90,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value == null) value = "";
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '定时上报4时间',
			dataIndex: 'TimingFourTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 90,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value == null) value = "";
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '定时上报5时间',
			dataIndex: 'TimingFiveTime',
			type: 'date',
			xtype: 'datecolumn',
			width: 90,
			format: 'H:i',
			editor: {
				xtype: 'timefield',
				format: 'H:i',
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(value == null) value = "";
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return me.rendererTime(value, meta, record, rowIndex, colIndex, store, veiw);
			}
		}, {
			text: '定时上报地点1坐标',
			dataIndex: 'TimingOnePostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '定时上报地点1',
			dataIndex: 'TimingOnePostionName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '设置1',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '地点设置1',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					if(isOpen == true)
						me.openSetMapForm(1, "One");
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '定时上报地点2坐标',
			dataIndex: 'TimingTwoPostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '定时上报地点2',
			dataIndex: 'TimingTwoPostionName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '设置2',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '地点设置2',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					if(isOpen == true)
						me.openSetMapForm(1, "Two");
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '定时上报地点3坐标',
			dataIndex: 'TimingThreePostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '定时上报地点3',
			dataIndex: 'TimingThreePostionName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '设置3',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '地点设置3',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					if(isOpen == true)
						me.openSetMapForm(1, "Three");
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '定时上报地点4坐标',
			dataIndex: 'TimingFourPostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '定时上报地点4',
			dataIndex: 'TimingFourPostionName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '设置4',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '地点设置4',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					if(isOpen == true)
						me.openSetMapForm(1, "Four");
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '定时上报地点5坐标',
			dataIndex: 'TimingFivePostion',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			}
		}, {
			text: '定时上报地点5',
			dataIndex: 'TimingFivePostionName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {
				readOnly: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '设置5',
			align: 'center',
			sortable: false,
			menuDisabled: true,
			tooltip: '设置5',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-map hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.getSelectionModel().select(rowIndex);
					var record = me.getStore().getAt(rowIndex);
					var isOpen = true;
					if(isOpen == true)
						me.openSetMapForm(1, "Five");
				}
			}],
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >";
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '上报地点范围(米)',
			dataIndex: 'TimingPostionRange',
			width: 120,
			sortable: false,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('TimingPostionRange', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '上报时间范围(分钟)',
			dataIndex: 'TimingTimeRange',
			width: 120,
			sortable: false,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "0");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('TimingTimeRange', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var infoEmp = "姓名:" + record.get("EmpName") + "<br >" + value;
				meta.tdAttr = 'data-qtip="' + infoEmp + '"';
				return value;
			}
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'DataTimeStamp',
			hidden: true,
			hideable: false
		}, {
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			//hidden: me.hideDelColumn,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}
				return v;
			}
		}, {
			text: '提示信息',
			dataIndex: 'ErrorInfo',
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	rendererTime: function(value, meta, record, rowIndex, colIndex, store, veiw) {
		var me = this;
		if(value != null && value.toString() != "" && value.toString().length > 0) {
			value = value.toString().replace(me.defaultDate, "");
			value = Ext.util.Format.date(value.toString(), 'H:i');
		}
		return value;
	},
	/*判断操作
	 * 获取原来的考勤类型,如果都是-1,需要先设置考勤类型后再操作
	 */
	JudgeSetAction: function(record) {
		var me = this;
		var result = true;
		var oldType = record.get('ATEventParaSettingsType');
		var setType = record.get('SetATEventParaSettingsType');
		if((oldType == "" || oldType.toString() == "-1") && (setType == "" || setType.toString() == "-1")) {
			JShell.Msg.error("请先设置考勤类型后再操作!");
			result = false;
		}
		return result;
	},
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/*保存按钮处理*/
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;
		if(len == 0) return;

		me.showAddInfo = "";
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = 0;

		var rec = null,
			entity = null,
			id = "",
			ATEventParaSettingsType = "";
		var addArr = [],
			addRecords = [],
			editRecords = [],
			editArr = [];

		for(var i = 0; i < len; i++) {
			rec = records[i];
			id = rec.get(me.PKField);

			entity = me.getOneEntityByRecord(rec);

			//获取原来的考勤类型,如果是-1,说明没有设置考勤,是新增
			ATEventParaSettingsType = rec.get('ATEventParaSettingsType');
			switch(ATEventParaSettingsType) {
				case "0": //固定时间
					if(entity != null) {
						entity.Id = id;
						editArr.push(entity);
						editRecords.push(rec);
					}
					break;
				case "1":
					if(entity != null) {
						entity.Id = id;
						editArr.push(entity);
						editRecords.push(rec);
					}
					break;
				default:
					if(entity != null) {
						addArr.push(entity);
						addRecords.push(rec);
					}
					break;
			}
		}
		//新增或修改完是否显示提示信息
		var isSearch = true;

		//新增的考勤信息处理
		if(addArr.length > 0) {
			if(editArr.length > 0) {
				isSearch = false;
			}
			me.showMask(me.saveText); //显示遮罩层
			me.saveCount = 0;
			me.saveErrorCount = 0;
			me.saveLength = addArr.length;
			Ext.Array.each(addArr, function(entity, index) {
				var record = addRecords[index];
				me.addSave(entity, record, index, isSearch);
			});
			me.hideMask(); //隐藏遮罩层
		}
		//修改的考勤信息处理
		if(editArr.length > 0) {
			isSearch = true;
			me.showMask(me.saveText); //显示遮罩层
			me.saveCount = 0;
			me.saveErrorCount = 0;
			me.saveLength = editArr.length;
			Ext.Array.each(editArr, function(entity, index) {
				var record = editRecords[index];
				me.editByField(entity, record, index, isSearch);
			});
			me.hideMask(); //隐藏遮罩层
		}
	},
	/*封装实体信息信息*/
	getToServerDateTime: function(rec, fileName) {
		var me = this;
		var toServerDateTime = rec.get(fileName);
		if(toServerDateTime != null) {
			toServerDateTime = Ext.util.Format.date(toServerDateTime.toString(), 'H:i');
		}
		if(toServerDateTime != "") {
			toServerDateTime = JShell.Date.toServerDate(me.defaultDate + toServerDateTime);
		}
		return toServerDateTime;
	},
	/*封装实体信息信息*/
	getOneEntityByRecord: function(rec) {
		var me = this;
		//获取原来的考勤类型,如果是-1,说明没有设置考勤,是新增
		var OldATEventParaSettingsType = rec.get('ATEventParaSettingsType');

		var SignInTime = me.getToServerDateTime(rec, "SignInTime");
		var SignOutTime = me.getToServerDateTime(rec, "SignOutTime");

		var TimingOneTime = me.getToServerDateTime(rec, "TimingOneTime");
		var TimingTwoTime = me.getToServerDateTime(rec, "TimingTwoTime");
		var TimingThreeTime = me.getToServerDateTime(rec, "TimingThreeTime");
		var TimingFourTime = me.getToServerDateTime(rec, "TimingFourTime");
		var TimingFiveTime = me.getToServerDateTime(rec, "TimingFiveTime");

		var setParaSettingsType = rec.get("SetATEventParaSettingsType");
		if(setParaSettingsType == -1 || setParaSettingsType == "") {
			setParaSettingsType = 0;
		}
		var entity = {
			EmpID: rec.get("EmpID"),
			EmpName: rec.get("EmpName"),
			IsUse: rec.get("IsUse"),
			ATEventParaSettingsType: setParaSettingsType,
			ATEventPostion: rec.get("ATEventPostion"),
			ATEventPostionName: rec.get("ATEventPostionName"),
			ATEventPostionRange: rec.get("ATEventPostionRange"),
			SignInTime: SignInTime,
			SignOutTime: SignOutTime,

			TimingOneTime: TimingOneTime,
			TimingTwoTime: TimingTwoTime,
			TimingThreeTime: TimingThreeTime,
			TimingFourTime: TimingFourTime,
			TimingFiveTime: TimingFourTime,

			TimingOnePostion: rec.get("TimingOnePostion"),
			TimingOnePostionName: rec.get("TimingOnePostionName"),

			TimingTwoPostion: rec.get("TimingTwoPostion"),
			TimingTwoPostionName: rec.get("TimingTwoPostionName"),

			TimingThreePostion: rec.get("TimingThreePostion"),
			TimingThreePostionName: rec.get("TimingThreePostionName"),

			TimingFourPostion: rec.get("TimingFourPostion"),
			TimingFourPostionName: rec.get("TimingFourPostionName"),

			TimingFivePostion: rec.get("TimingFivePostion"),
			TimingFivePostionName: rec.get("TimingFivePostionName"),
			TimingPostionRange: rec.get("TimingPostionRange"),
			TimingTimeRange: rec.get("TimingTimeRange"),
			Id: "-1"
		};

		//工作时长的处理
		var EmpWorkLong = rec.get("EmpWorkLong");
		switch(setParaSettingsType) {
			case 1: //弹性时间
				if(EmpWorkLong && EmpWorkLong != 0) {
					entity.EmpWorkLong = parseInt(EmpWorkLong);;
				} else {
					entity.EmpWorkLong = 8;
				}
				break;
			default:
				break;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//创建人
		if(userName && entity != null) {
			entity.CreaterName = userName;
		}
		if(userId && entity != null) {
			entity.CreaterID = userId;
		}

		return entity;
	},
	/*计算出小时数**/
	diffTime: function(startDate, endDate) {
		if(startDate == null || startDate == "")
			return 0;
		if(endDate == null || endDate == "")
			return 0;
		if(typeof(startDate) == "string") {
			startDate = new Date(startDate.replace(/-/, '/'));
		}
		if(typeof(endDate) == "string") {
			endDate = new Date(endDate.replace(/-/, '/'));
		}
		var diff = endDate.getTime() - startDate.getTime(); //时间差的毫秒数  
		//计算出小时数  
		var leave1 = diff % (24 * 3600 * 1000); //计算天数后剩余的毫秒数  
		var hours = leave1 / (3600 * 1000);
		if(isNaN(hours)) {
			hours = 0;
		}
		hours = parseInt(hours);
		return hours;
	},
	/*保存单个新增信息*/
	addSave: function(entity, record, index, isSearch) {
		var me = this;
		var url = JShell.System.Path.getUrl(me.addUrl);
		var params = Ext.JSON.encode({
			entity: entity
		});
		me.postSave(url, params, record, index, isSearch);
	},
	/*保存单个修改信息*/
	editByField: function(entity, record, index, isSearch) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		//fields: me.getStoreFields(true).join(',')
		var params = Ext.JSON.encode({
			entity: entity,
			fields: 'TimingPostionRange,TimingTimeRange,ATEventParaSettingsType,ATEventPostion,ATEventPostionName,ATEventPostionRange,SignInTime,SignOutTime,EmpWorkLong,TimingOneTime,TimingTwoTime,TimingThreeTime,TimingFourTime,TimingFiveTime,TimingOnePostion,TimingOnePostionName,TimingTwoPostion,TimingTwoPostionName,TimingThreePostion,TimingThreePostionName,TimingFourPostion,TimingFourPostionName,TimingFivePostion,TimingFivePostionName,Id,IsUse' //EmpID,EmpName,
		});
		me.postSave(url, params, record, index, isSearch);
	},
	/*提交新增或者修改信息操作*/
	postSave: function(url, params, record, index, isSearch) {
		var me = this;
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						var empName = record.get("EmpName");
						me.showAddInfo = me.showAddInfo + "员工名称为【" + empName + "】,保存操作失败!<br />";
						record.set(me.DelField, false);
						//record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						if(isSearch == true) {
							me.onSearch();
							JShell.Msg.alert("保存操作成功", null, 1000);
						}

					}
				}

			});
		}, 100 * (index + 1));
	},
	loadByDeptId: function(id) {
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			url = '';
		//根据部门ID查询模式
		url += JShell.System.Path.getUrl(me.selectUrl);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&deptid=' + me.DeptId + "&isincludesubdept=" + me.IsIncludeSubDept;

		return url;
	},
	changeShowType: function(value) {
		var me = this;
		me.IsIncludeSubDept = value ? false : true;
		me.onSearch();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var len = data.list.length;
		for(var i = 0; i < len; i++) {
			data.list[i].SetATEventParaSettingsType = data.list[i].ATEventParaSettingsType;
		}
		return data;
	},
	/*
	 *打开地点设置窗口
	 * */
	openSetMapForm: function(paraSettingsType, timingTpye) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var record = records[0];
		var result = me.JudgeSetAction(record);
		if(result) {
			var formPanel = 'Shell.class.oa.at.attendance.parasettings.Map';
			var maxWidth = document.body.clientWidth * 0.88;
			var height = document.body.clientHeight - 55;
			var config = {
				width: maxWidth,
				height: height,
				checkOne: false,
				SUB_WIN_NO: '1',
				listeners: {
					close: function(win) {

					},
					onSaveClick: function(win) {
						var ATEventPostion = "",
							ATEventPostionName = "";
						switch(paraSettingsType) {
							case 0: //固定考勤地点
								ATEventPostion = "ATEventPostion";
								ATEventPostionName = "ATEventPostionName";

								break;
							default: //弹性地点设置
								if(timingTpye != "") {
									ATEventPostion = "Timing" + timingTpye + "Postion";
									ATEventPostionName = "Timing" + timingTpye + "PostionName";
								}
								break;
						}
						if(ATEventPostion != "") {
							record.set(ATEventPostion, win.MapLatlng);
						}
						if(ATEventPostionName != "") {
							record.set(ATEventPostionName, win.MapAddress);
						}
						//me.fireEvent('onSaveClick', win);
						win.close();
					}
				}
			};
			var ATEventPostion = "",
				ATEventPostionName = "";
			switch(paraSettingsType) {
				case 0: //固定考勤地点
					ATEventPostion = "ATEventPostion";
					ATEventPostionName = "ATEventPostionName";

					break;
				default: //弹性地点设置
					if(timingTpye != "") {
						ATEventPostion = "Timing" + timingTpye + "Postion";
						ATEventPostionName = "Timing" + timingTpye + "PostionName";
					}
					break;
			}
			if(record.get(ATEventPostionName) != "") {
				config.MapAddress = record.get(ATEventPostionName);
			}
			if(record.get(ATEventPostion) != "") {
				config.MapLatlng = record.get(ATEventPostion);
			}
			JShell.Win.open(formPanel, config).show();
		}
	},

	/**打开复制窗口*/
	openChooseUsersForm: function(copyRecord) {
		var me = this;
		var formPanel = 'Shell.class.sysbase.user.CheckApp';
		var maxWidth = document.body.clientWidth * 0.52;
		var height = document.body.clientHeight - 45;

		var config = {
			width: 650,
			height: height,
			checkOne: false,
			SUB_WIN_NO: '2',
			listeners: {
				close: function(win) {

				},
				accept: function(win, records) {
					me.onChooseUsersAccept(win, records, copyRecord);
				}
			}
		};
		var eid = copyRecord.get("EmpID");
		if(eid != "") {
			config.defaultWhere = "hremployee.Id!=" + eid;
		}
		JShell.Win.open(formPanel, config).show();
	},
	/*获取所有的考勤设置信息*/
	getAllSetInfo: function() {
		var me = this;
		var list = [];
		var url = JShell.System.Path.ROOT + "/WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByHQL?isPlanish=true&page=1&limit=500";
		var obj = "ATEmpAttendanceEventParaSettings_";
		url = url + "&fields=" + obj + "Id," + obj + "IsUse," + obj + "EmpID," + obj + "EmpName," + obj + "ATEventParaSettingsType";

		JShell.Server.get(url, function(data) {
			if(data.success) {
				var value = me.changeResult(data.value);
				list = value.list;
			} else {
				JShell.Msg.error(data.msg);
			}
		});
		return list;
	},

	/***
	 * 复制某一个员工的考勤信息给选择好的员工
	 * @param {Object} win 选择员工UI
	 * @param {Object} records 选择的员工数组
	 * @param {Object} copyRecord 待复制的员工考勤信息
	 */
	onChooseUsersAccept: function(win, records, copyRecord) {
		var me = this,
			addArr = [],
			addRecords = [],
			allEmpID = [];
		me.showAddInfo = "";
		if(copyRecord) {

			//获取所有的员工考勤设置信息
			var allSetInfo = me.getAllSetInfo();
			if(allSetInfo == null)
				allSetInfo = [];
			Ext.Array.each(allSetInfo, function(record, index) {
				allEmpID.push(record.get("ATEmpAttendanceEventParaSettings_EmpID"));
			});
			//遍历选择好的员工数组
			Ext.Array.each(records, function(record, index) {
				var empID = record.get("HREmployee_Id");
				var empName = record.get("HREmployee_CName");
				var indexOf = -1;
				if(allEmpID.length > 0) {
					indexOf = allEmpID.toString().indexOf(empID.toString());
				}
				if(indexOf == -1) {
					//考勤实体信息
					var entity = me.getOneEntityByRecord(copyRecord);
					var addEntity = Ext.clone(entity);
					//员工不存在考勤设置信息
					addEntity.Id = "-1";
					addEntity["EmpID"] = empID;
					addEntity["EmpName"] = empName;
					addEntity.IsUse = true;
					addArr.push(addEntity);

					//找到store里的记录行
					var addRecord = me.getStore().findRecord("EmpID", empID);
					addRecords.push(addRecord);
				} else {
					//员工已存在考勤设置信息
					me.showAddInfo = me.showAddInfo + "员工名称为【" + empName + "】,已存在考勤信息!<br />";
				}
			});
		}
		//新增或修改完是否显示提示信息
		var isSearch = true;
		//新增的考勤信息处理
		if(addArr.length > 0) {
			me.saveCount = 0;
			me.saveErrorCount = 0;
			me.saveLength = addArr.length;
			Ext.Array.each(addArr, function(addEntity, index) {
				var record = addRecords[index];
				me.addSave(addEntity, record, index, isSearch);
			});
		}
		win.close();
	}
});