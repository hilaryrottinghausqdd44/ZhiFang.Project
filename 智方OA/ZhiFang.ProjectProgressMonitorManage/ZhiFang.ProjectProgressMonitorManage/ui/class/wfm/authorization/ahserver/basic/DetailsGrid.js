/**
 * 服务器授权明细基本列表
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.DetailsGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '服务器授权明细',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerProgramLicenceByHQL',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerProgramLicenceByField',
	
	/**默认每页数量*/
	defaultPageSize: 1000,
	/**分页栏下拉框数据*/
	pageSizeList: null,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**后台排序*/
	remoteSort: false,
	/**默认加载*/
	defaultLoad: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否有删除列*/
	hasDeleteColumn: true,
	/**是否可编辑列*/
	isAllowEditing: true,
	IsGetLicenceEndDate: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 480
		});
		me.getLicenceTypeData();
	},
	initComponent: function() {
		var me = this;
		if(!JShell.System.ClassDict.LicenceStatus) {
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {});
		}
		if(me.isAllowEditing == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				editing: true,
				clicksToEdit: 1
			});
		}
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
    
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		return columns;
	},
	/*创建交流列**/
	createInteraction: function() {},
	/*创建授权类型列**/
	createLicenceTypeIdColumn: function() {
		var me = this;
		return {
			text: '授权类型',
			dataIndex: 'LicenceTypeId',
			width: 60,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('LicenceType', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			},
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				displayField: 'text',
				valueField: 'value',
				listClass: 'x-combo-list-small',
				store: new Ext.data.SimpleStore({
					fields: ['value', 'text'],
					data: me.getLicenceTypeData()
				}),
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false)
							com.setReadOnly(true);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('LicenceTypeId', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			})
		};
	},
	/*创建截至日期列**/
	createLicenceDateColumn: function() {
		var me = this;
		return {
			text: '文件内截止日期',
			dataIndex: 'LicenceDate',
			width: 100,
			sortable: false,
			//isDate: true,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if(info) {
					if(record.get('LicenceTypeId') == info.Id) {
						value = '永久';
						record.set("LicenceStatusName", "有效");
						record.set("LicenceStatusId", 1);
					} else {
						if(value != "" && value != null && value != undefined) {
							value = Ext.util.Format.date(value, 'Y-m-d');
							//判断是否10天内到期或过期
//							me.setLicenceStatus(record, value);
						}
					}
				}
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var style = me.getCellStyle(record.get("LicenceStatusId"));
				if(style != "")
					meta.style = style;
				return value;
			},
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d',
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false) {
							com.setReadOnly(true);
						} else {
							me.IsGetLicenceEndDate = true;
							me.comSetReadOnly(com, e);
						}
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						if(newValue != null && newValue != "" && newValue != undefined) {
							var Sysdate = JcallShell.System.Date.getDate();
							var StartDateValue = Ext.util.Format.date(Sysdate, 'Y-m-d');
							var EndDateValue = JcallShell.Date.toString(newValue, true);
							if(StartDateValue > EndDateValue) {
								newValue = "";
								com.setValue("");
								record.set('LicenceDate', "");
								record.set("LicenceStatusName", "");
								record.set("LicenceStatusId", "");
								me.IsGetLicenceEndDate = true;
								//有效期状态处理
								me.setLicenceStatus(record, newValue);
//								me.getView().refresh();
								JShell.Msg.error('截止日期不能小于服务器当前日期!');
							} else {
								newValue = Ext.util.Format.date(newValue.toString(), 'Y-m-d');
								var dateFormat =/^(\d{4})-(\d{2})-(\d{2})$/;
								if(!dateFormat.test(newValue)){
									return;
								}
								if(!com.isValid())return;
								me.IsGetLicenceEndDate=true;
								if(me.IsGetLicenceEndDate == true) {
									newValue = me.getLicenceEndDate(newValue);
									com.setValue(newValue);
								}
								record.set('LicenceDate', newValue);
								//有效期状态处理
								me.setLicenceStatus(record, newValue);
							}

						} else {
							newValue = "";
							com.setValue("");
							record.set('LicenceDate', "");
							record.set("LicenceStatusName", "");
							record.set("LicenceStatusId", "");
							//record.commit();
							me.IsGetLicenceEndDate = true;
						}
					}
				}
			}
		};
	},
	/**节假日顺延*/
	getLicenceEndDate: function(endDate) {
		var me = this,
			Date = '';
		if(me.IsGetLicenceEndDate) {
			var url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_GetLicenceEndDate';
			url = JShell.System.Path.getRootUrl(url);
			if(endDate) {
				url = url + '?endDate=' + endDate;
			}
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.IsGetLicenceEndDate = false;
					Date = data.value.EndDate;
				} else {
					me.IsGetLicenceEndDate = true;
					JShell.Msg.error(data.msg);
				}
			}, false, 100, false);
		} else {
			me.IsGetLicenceEndDate = true;
		}
		return Date;
	},
	createLicenceStatusIdColumn: function() {
		var me = this;
		return {
			text: '有效期状态',
			dataIndex: 'LicenceStatusId',
			width: 75,hidden:true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				value = value || '';
				var style = me.getCellStyle(value);
				if(style != "")
					meta.style = style;
				return record.get("LicenceStatusName");
			}
		};
	},
		
	/*创建LicenceStatusName列**/
	createLicenceStatusNameColumn: function() {
		var me = this;
		return {
			text: '有效期状态',
			dataIndex: 'LicenceStatusName',
			width: 105,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var style = me.getCellStyle(record.get("LicenceStatusId"));
				if(style != "")
					meta.style = style;
				return value;
			}
		};
	},
	sqhValuerenderer: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		return value;
	},
	/*创建SQH列**/
	createSQHColumn: function() {
		var me = this;
		return {
			text: 'SQH',
			dataIndex: 'SQH',
			hidden: false,
			width: 50,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var style = me.getCellStyle(record.get("LicenceStatusId"));
				if(style != "")
					meta.style = style;
				value = me.sqhValuerenderer(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			},
			editor: {
				readOnly: false,
				listeners: {
					focus: function(com, e, eOpts) {
						if(me.isAllowEditing == false) {
							com.setReadOnly(true);
						} else {
							me.comSetReadOnly(com, e);
						}
					}
				}
			}
		};
	},
	comSetReadOnly: function(com, e) {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var record = records[0];
		var isReadOnly = false;
		if(record) {
			var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
			if(info) {
				if(record.get('LicenceTypeId') == info.Id) {
					isReadOnly = true;
				} else {
					isReadOnly = false;
				}
			}
		}
		com.setReadOnly(isReadOnly);
	},
	showQtipValue: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;

		return meta;
	},
	/**获取授权类型列表*/
	getLicenceTypeData: function() {
		var me = this,
			data = [];
		if(!JShell.System.ClassDict.LicenceType) {
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {

			});
		}
		var LicenceTypeList = JShell.System.ClassDict.LicenceType;
		data.push(['', '=空=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in LicenceTypeList) {
			var obj = LicenceTypeList[i];
			var style = ['font-weight:bold;text-align:center'];
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**获取授权状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=空=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			var style = ['font-weight:bold;text-align:center'];
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**查询信息*/
	openShowForm: function(id) {
		var me = this;
	},

	getCellStyle: function(licenceStatusId) {
		var BGColor = "",
			Color = "#ffffff";
		switch(licenceStatusId) {
			case "1":
				BGColor = "#1c8f36";
				break;
			case "2":
				BGColor = "#f4c600";
				break;
			case "3":
				BGColor = "red";
				break;
			default:
				Color = "";
				break;
		}
		var style = "";
		if(BGColor != "")
			style = 'background-color:' + BGColor + ";";
		if(Color != "")
			style = style + 'color:' + Color + ";";
		return style;
	},
	/*有效期状态处理**/
	setLicenceStatus: function(record, newValue) {
		var me = this;
		var LicenceStatusName = "";
		var LicenceStatusId = "";
		var sysdate = JcallShell.System.Date.getDate();
		var curDateStr = Ext.util.Format.date(sysdate, 'Y-m-d');
		var curDate = JcallShell.System.Date.getDate(curDateStr);

		var licenceDate = JcallShell.Date.getNextDate(newValue, 0);
		if(licenceDate <= curDate) {
			LicenceStatusId = 3;
			LicenceStatusName = "失效";
		} else {
			//得到时间戳相减 得到以毫秒为单位的差
			var mmSec = (licenceDate.getTime() - curDate.getTime());
			//单位转换为天并返回 
			var days = parseInt((mmSec / 3600000 / 24));
			if(days >= 10) {
				LicenceStatusId = 1;
				LicenceStatusName = "有效";
			} else if(days > 0 && days < 10) {
				LicenceStatusId = 2;
				LicenceStatusName = "十天内到期";
			} else {
				LicenceStatusId = 3;
				LicenceStatusName = "失效";
			}
		}
		record.set("LicenceStatusName", LicenceStatusName);
		record.set("LicenceStatusId", LicenceStatusId);
		record.commit();
	}
});