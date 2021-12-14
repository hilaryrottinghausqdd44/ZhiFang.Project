/**
 * 服务器仪器授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.basic.EquipLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.DetailsGrid',
	requires: ['Ext.ux.CheckColumn'],

	title: '服务器仪器授权明细',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerEquipLicenceByHQL',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerEquipLicenceByField',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHServerEquipLicence',

	/**上传的授权申请文件的仪器授权明细信息*/
	AHServerEquipLicenceList: null,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**后台排序*/
	remoteSort: true,
	/**是否单选*/
	checkOne: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	
	defaultOrderBy: [{
		property: 'SNo',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.hasButtontoolbar) me.initDate();
	},
	initComponent: function() {
		var me = this;
		me.dockedItems = me.createDockedItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push({
			xtype: 'button',
			text: '全选',
			itemId: 'checkAll',
			iconCls: 'build-button-unchecked',
			handler: function(but, e) {
				var delcheckAll = me.getComponent('buttonsToolbarTop1').getComponent('delcheckAll');
				delcheckAll.setIconCls('build-button-unchecked');
				but.setIconCls('build-button-checked');
				me.store.each(function(record) {
					record.set('IsCheck', true);
				});
			}
		}, {
			xtype: 'button',
			text: '全不选',
			itemId: 'delcheckAll',
			iconCls: 'build-button-unchecked',
			handler: function(but, e) {
				var checkAll = me.getComponent('buttonsToolbarTop1').getComponent('checkAll');
				checkAll.setIconCls('build-button-unchecked');
				but.setIconCls('build-button-checked');
				me.store.each(function(record) {
					record.set('IsCheck', false);
				});
			}
		}, {
			xtype: 'button',
			itemId: 'btnTemp',
			iconCls: 'button-edit',
			text: "临时全选",
			tooltip: "临时全选",
			style: {
				marginRight: '10px'
			},
			handler: function() {
				me.onSelectTemp();
			}
		}, '-', {
			xtype: 'button',
			itemId: 'btnBatchBusinessSetting',
			iconCls: 'button-edit',
			text: "批量商业授权设置",
			tooltip: "批量设置勾选中为商业授权",
			style: {
				marginRight: '10px'
			},
			handler: function() {
				me.onBatchBusinessSettingClick();
			}
		}, '-', {
			xtype: 'button',
			itemId: 'btnBatchSetting',
			iconCls: 'button-edit',
			text: "批量临时授权设置",
			tooltip: "批量设置勾选中行为临时授权<br />如遇节假日,日期顺延(包括春节)",
			style: {
				marginLeft: '10px'
			},
			handler: function() {
				me.onBatchSettingClick();
			}
		}, {
			fieldLabel: '截止日期',
			tooltip: "如遇节假日,日期顺延(包括春节)",
			name: 'LicenceDate',
			itemId: 'LicenceDate',
			labelWidth: 60,
			width: 160,
			emptyText: '',
			allowBlank: false,
			xtype: 'datefield',
			format: 'Y-m-d',
			listeners: {
				focus: function(com, e, eOpts) {
					me.IsGetLicenceEndDate = true;
				},
				change: function(com, newValue) {
					if(!newValue) return;
					me.IsGetLicenceEndDate = true;
					if(me.IsGetLicenceEndDate == true) {
						var EndDate = JShell.Date.toString(newValue, true);
						var dateFormat = /^(\d{4})-(\d{2})-(\d{2})$/;
						if(!dateFormat.test(EndDate)) {
							return;
						}
						if(!com.isValid()) return;
						var EndDateVal = me.getLicenceEndDate(EndDate);
						if(EndDateVal) com.setValue(EndDateVal);
					}
				}
			}
		}, {
			name: 'SelectAddDays',
			itemId: 'SelectAddDays',
			xtype: 'radiogroup',
			fieldLabel: '',
			tooltip: "如遇节假日,日期顺延(包括春节)",
			columns: 4,
			columnWidth: 0.25,
			width: 230,
			height: 22,
			vertical: true,
			items: [{
				boxLabel: '30天',
				name: 'addDays',
				inputValue: 30,
				checked: true
			}, {
				boxLabel: '60天 ',
				name: 'addDays',
				inputValue: 60
			}, {
				boxLabel: '90天',
				name: 'addDays',
				inputValue: 90
			}, {
				boxLabel: '180天',
				name: 'addDays',
				inputValue: 180
			}],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue != null && newValue != undefined) {
						me.addDaysSetLicenceDate(com.getValue().addDays);
					}
				}
			}
		});
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'top',
			itemId: 'buttonsToolbarTop1',
			items: items
		});
	},
	/**初始化开始日期及结束日期*/
	initDate: function() {
		var me = this;
		var SelectAddDays = me.getComponent('buttonsToolbarTop1').getComponent('SelectAddDays');
		var Sysdate = JcallShell.System.Date.getDate();
		var DateValue = Ext.util.Format.date(Sysdate, 'Y-m-d');
		var addDays = 0;
		addDays = SelectAddDays.getValue().addDays;
		DateValue = Ext.util.Format.date(JcallShell.Date.getNextDate(DateValue, addDays), 'Y-m-d');
		if(DateValue != "") {
			me.IsGetLicenceEndDate = true;
			DateValue = me.getLicenceEndDate(DateValue);
		}
		var tbar = me.getComponent("buttonsToolbarTop1");
		if(tbar != undefined) {
			var LicenceDate = tbar.getComponent("LicenceDate");
			LicenceDate.setValue(DateValue);
		}
	},
	/**日期加上天数得到新的日期
	 * 日期dayCount 要增加的天数
	 */
	addDaysSetLicenceDate: function(addDays) {
		var me = this;
		var tbar = me.getComponent("buttonsToolbarTop1");
		if(tbar != undefined) {
			var LicenceDate = tbar.getComponent("LicenceDate");
			var Sysdate = JcallShell.System.Date.getDate();
			var StartDateValue = Ext.util.Format.date(Sysdate, 'Y-m-d');
			var DateValue = Ext.util.Format.date(JcallShell.Date.getNextDate(StartDateValue, addDays), 'Y-m-d');
			me.IsGetLicenceEndDate = true;
			DateValue = me.getLicenceEndDate(DateValue);
			if(DateValue != "") {
				LicenceDate.setValue(DateValue);
			}
		}
	},
	//获取勾选数据，杜怡辉要求改成checkcolumn，需改变获取选择式
	getSelection: function() {
		var me = this;
		var records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;
		var list = [];
		for(var i = 0; i < len; i++) {
			if(records[i].get('IsCheck') == true) list.push(records[i]);

		}
		return list;
	},
	//设置取消选择
	setDeSelect: function() {
		var me = this;
		me.store.each(function(record) {
			record.set('IsCheck', false);
			record.commit();
		});
	},
	/**批量商业授权设置*/
	onBatchBusinessSettingClick: function() {
		var me = this;
		//		var records = me.getSelectionModel().getSelection();
		var records = me.getSelection(), //获取修改过的行记录
			len = records.length;

		//取消选择
		me.setDeSelect();
		if(len > 0) {
			var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
			Ext.Array.each(records, function(record, index, countriesItSelf) {
				record.set("LicenceTypeId", info.Id);
				record.set("LicenceDate", "");
				record.set("LicenceStatusName", "有效");
				record.set("LicenceStatusId", 1);
				//record.commit();
			});
			JShell.Msg.alert("批量设置商业授权完成!", null, 2000);
		} else {
			JShell.Msg.alert("请选择记录行后再操作!");
			return;
		}
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		if(!me.checkOne) {
			//复选框
			columns.push({
				xtype: 'checkcolumn',
				text: '勾选',
				dataIndex: 'IsCheck',
				width: 35,
				align: 'center',
				sortable: false,
				stopSelection: false,
				type: 'boolean'
			});
		}
		columns.push({
			xtype: 'rownumberer',
			text: '序号',
			width: me.rowNumbererWidth,
			dataIndex: 'SNo',
			sortable: false,
			align: 'center',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '变更类型',
			dataIndex: 'TYPE',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var LicenceTypeId = record.get('LicenceTypeId');
				var AHBeforeLicenceTypeId = record.get('AHBeforeLicenceTypeId');
				var statusName = "";
				var LicenceDate = record.get('LicenceDate');
				var strLicenceDate = "";
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if(info && LicenceTypeId == info.Id) {
					strLicenceDate = '永久';
				} else {
					if(LicenceDate != "" && LicenceDate != null && LicenceDate != undefined) {
						strLicenceDate = Ext.util.Format.date(LicenceDate, 'Y-m-d');
					}
				}
				//时间是否相同
				var isIdentical = "";
				var AHBeforeDateTime = "";
				var AHBeforeLicenceTypeName = record.get('AHBeforeLicenceTypeName');
				if(AHBeforeLicenceTypeName == "商业" || record.get('AHBeforeLicenceTypeId') == '1') {
					AHBeforeDateTime = '永久';
				} else {
					AHBeforeDateTime = JShell.Date.toString(record.get("AHBeforeDateTime"), true);
				}
				if(AHBeforeDateTime && strLicenceDate && AHBeforeDateTime != strLicenceDate) {
					isIdentical = '1';
				}
				if(LicenceTypeId && LicenceTypeId != AHBeforeLicenceTypeId) {
					statusName = "其他";
					meta.style = 'background-color:#FFA500;color:#ffffff;';
				}
				//正常
				if(LicenceTypeId && LicenceTypeId == AHBeforeLicenceTypeId && !isIdentical && strLicenceDate) {
					statusName = "正常";
				}
				//临时时间改变
				if(LicenceTypeId == '2' && AHBeforeLicenceTypeId == '2' && isIdentical == '1') {
					statusName = "临时时间改变";
					meta.style = 'background-color:#CD00CD;color:#ffffff;';
				}
				if(AHBeforeLicenceTypeId && LicenceTypeId && AHBeforeLicenceTypeId != LicenceTypeId) {
					//临时变商业
					if(LicenceTypeId == '1' && AHBeforeLicenceTypeId == '2') {
						statusName = "临时变商业";
						meta.style = 'background-color:green;color:#ffffff;';
					}
				}
				var SQH = record.get('SQH');
				var AHBeforeSQH = record.get('AHBeforeSQH');
				if(SQH != AHBeforeSQH) {
					statusName = "SQH改变";
					meta.style = 'background-color:#9400D3;color:#ffffff;';
				}
				return statusName;
			}
		});

		columns.push(me.createLicenceTypeIdColumn());
		columns.push({
			text: '',
			dataIndex: 'TYPE',
			width: 5,
			maxWidth: 5,
			minWidth: 5,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				value = record.get('LicenceDate');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				if(info) {
					if(record.get('LicenceTypeId') == info.Id) {
						value = '永久';
					} else {
						if(value != "" && value != null && value != undefined) {
							value = Ext.util.Format.date(value, 'Y-m-d');
						}
					}
				}
				var AHBeforeDateTime = "";
				var AHBeforeLicenceTypeName = record.get('AHBeforeLicenceTypeName');
				if(AHBeforeLicenceTypeName == "商业" || record.get('AHBeforeLicenceTypeId') == '1') {
					AHBeforeDateTime = '永久';
				} else {
					AHBeforeDateTime = JShell.Date.toString(record.get("AHBeforeDateTime"), true);
				}
				if(AHBeforeDateTime && value && AHBeforeDateTime != value && record.get('LicenceTypeId')) {
					meta.style = 'background-color:#CD00CD';
				}
				return '';
			}
		});
		columns.push(me.createLicenceDateColumn());
		columns.push(me.createLicenceStatusIdColumn());
		columns.push({
			text: 'OA内有效状态',
			dataIndex: 'CalcDays',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				CalcDays = Number(value) || 0;
				var BGColor = "",
					StatusName = "正常";
				if(!record.get('AHBeforeLicenceTypeId')) StatusName = "";
				var AHBeforeLicenceTypeName = record.get('AHBeforeLicenceTypeName');
				if(record.get('AHBeforeLicenceTypeId')) {
					if(AHBeforeLicenceTypeName == "商业" || record.get('AHBeforeLicenceTypeId') == '1') {
						BGColor = "";
						StatusName = "正常";
					} else {
						if(CalcDays < 0) {
							BGColor = "#DCDCDC";
							StatusName = "已过期";
						}
						if(CalcDays <= 30 && CalcDays > 0) {
							BGColor = "#FFA500";
							StatusName = "一个月内过期";
						}
						if(CalcDays <= 7 && CalcDays >= 0) {
							BGColor = "red";
							StatusName = "即将过期";
						}
					}
				}
				if(BGColor != "")
					meta.style = 'background-color:' + BGColor + ';color:#ffffff';
				return StatusName;
			}
		}, {
			text: 'OA上次授权类型名称',
			dataIndex: 'AHBeforeLicenceTypeName',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 130,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
			}
		}, {
			text: 'OA内上次授权截止时间',
			dataIndex: 'AHBeforeDateTime',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 135,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var AHBeforeLicenceTypeName = record.get('AHBeforeLicenceTypeName');
				if(AHBeforeLicenceTypeName == "商业" || record.get('AHBeforeLicenceTypeId') == '1') {
					value = '永久';
				} else {
					value = JShell.Date.toString(value, true);
				}
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: 'OA内SQH号',
			dataIndex: 'AHBeforeSQH',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 80,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return value;
			}
		});
		columns.push(me.createLicenceStatusNameColumn());
		columns.push(me.createISNewApplyColumn());
		if(me.hasDeleteColumn)
			columns.push(me.createDeleteColumn());
		columns.push({
			text: '授权站点',
			dataIndex: 'NodeTableName',
			width: 140,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '仪器编号',
			dataIndex: 'EquipID',
			width: 60,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '用户程序名',
			dataIndex: 'ProgramName',
			width: 90,
			sortable: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);

				return value;
			}
		}, {
			text: '系统程序名',
			dataIndex: 'Equipversion',
			width: 90,
			flex: 1,
			sortable: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				if(record.get("Equipversion") != record.get("ProgramName")) {
					meta.style = "background-color:red;color:#ffffff;";
				}
				return value;
			}
		});

		columns.push({
			text: 'SQH',
			dataIndex: 'SQH',
			width: 50,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				if(record.get("Equipversion") != record.get("ProgramName")) {
					meta.style = "background-color:red;color:#ffffff;";
				}
				return value;
			},
			editor: {
				readOnly: false,
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					}
				}
			}
		});

		columns.push({
			text: '授权Key',
			dataIndex: 'LicenceKey',
			width: 120,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: '授权仪器',
			dataIndex: 'EquipName',
			width: 120,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				return value;
			}
		}, {
			text: 'ID',
			dataIndex: 'Id',
			hidden: true,
			isKey: true,
			hideable: false
		}, {
			text: '服务器授权ID',
			dataIndex: 'ServerLicenceID',
			hidden: true,
			hideable: false
		}, {
			text: 'OA内上次授权类型Id',
			dataIndex: 'AHBeforeLicenceTypeId',
			hidden: true,
			hideable: false
		});
		return columns;
	},

	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
	},
	showQtipValue: function(value, meta, record, rowIndex, colIndex, store, view) {
		var me = this;
		var LicenceKey = record.get("LicenceKey");
		var SQH = record.get("SQH");
		var EquipName = record.get("EquipName");
		var ProgramName = record.get("ProgramName");
		var qtipValue = "";
		if(LicenceKey == undefined || LicenceKey == null)
			LicenceKey = "";

		qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>1.授权类型不是商业时,截止日期不能为空<br />2.当选择的截止日期不是星期三工作日时,系统会自动修改为星期三工作日<br />3.当用户程序名与系统程序名不一致时,用户程序名与系统程序名列背景色为红色</b>" + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权仪器:</b>" + EquipName + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>SQH:</b>" + SQH + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>授权Key:</b>" + LicenceKey + "</p>";
		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	/**查询信息*/
	openShowForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.authorization.ahserver.basic.ShowTabPanel', {
			SUB_WIN_NO: '202', //内部窗口编号
			//resizable:false,
			title: '授权信息',
			PK: id
		}).show();
	},
	/**初始化监听*/
	initListeners: function() {},
	/**@public 根据where条件加载数据*/
	load: function() {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		if(me.AHServerEquipLicenceList != null) {
			me.store.loadData(me.AHServerEquipLicenceList);
		} else {
			me.clearData();
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
	},
	/**删除一行并且序号需要重新排序*/
	deleteRecord: function(record) {
		var me = this;
		var isNewApply = record.get("ISNewApply");
		switch(isNewApply) {
			case '1':
				isNewApply = true;
				break;
			case 1:
				isNewApply = true;
				break;
			case 'true':
				isNewApply = true;
				break;
			default:
				isNewApply = false;
				break;
		}
		switch(isNewApply) {
			case false:
				//删除已经保存在数据库中的信息
				me.delErrorCount = 0;
				me.delCount = 0;
				me.delLength = 1;
				me.delOneById(1, record);
				break;
			default:
				me.store.remove(record);
				//me.setAllSNo();
				JShell.Msg.alert('删除成功！', null, 1000);
				break;
		}
	},
	setAllSNo: function() {
		var me = this;
		//		setTimeout(function() {
		//			var SNo = 1;
		//			me.store.each(function(rec) {
		//				rec.set("SNo", SNo);
		//				SNo++;
		//			});
		//		}, 100);
	},
	/**删除一条数据*/
	delOneById: function(index, record) {
		var me = this;
		var id = record.get(me.PKField);
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				//var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						me.store.remove(record);
						//me.setAllSNo();
						JShell.Msg.alert('删除成功！', null, 800);
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					JShell.Msg.error('删除失败！' + data.msg);
				}
			});
		}, 100 * index);
	},
	/*创建是否是新申请的列**/
	createISNewApplyColumn: function() {
		var me = this;
		return {
			text: '是否为追加',
			dataIndex: 'ISNewApply',
			hidden: true,
			width: 60,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(value, meta, record, rowIndex, colIndex, store, view);
				var style = me.getCellStyle(record.get("LicenceStatusId"));
				if(style != "")
					meta.style = style;
				return value;
			},
			editor: {
				readOnly: true
			}
		};
	},
	/*创建交流列**/
	createDeleteColumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:red;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-del hand',
				handler: function(grid, rowIndex, colIndex) {
					var msg = "请确认是否要删除当前选择行信息? ";
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								var rec = grid.getStore().getAt(rowIndex);
								me.deleteRecord(rec);
							}
						}
					});
				}
			}]
		};
	},
	//选择所有临时授权
	onSelectTemp: function() {
		var me = this;
		var tempInfo = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
		var tempInfoId="2";
		if(tempInfo)tempInfoId=tempInfo.Id;
		
		me.store.each(function(record) {
			//临时授权
			var licenceTypeId = record.get('LicenceTypeId');
			if(!licenceTypeId) { //为空时调整为"临时授权"
				record.set("LicenceTypeId", tempInfoId);
				record.set('IsCheck', true);
			} else if(licenceTypeId && licenceTypeId != '1') {
				record.set('IsCheck', true);
			}
		});
	},
	/**批量设置临时授权时间*/
	onBatchSettingClick: function() {
		var me = this;
		var tbar = me.getComponent("buttonsToolbarTop1");
		var licenceDate = tbar.getComponent("LicenceDate");
		var licenceDateValue = licenceDate.getValue();
		if(!licenceDateValue) {
			JShell.Msg.alert("请选择临时授权日期后再操作!");
			return;
		}
		var dateFormat = /^(\d{4})-(\d{2})-(\d{2})$/;
		if(!dateFormat.test(JShell.Date.toString(licenceDateValue, true))) {
			JShell.Msg.alert("临时授权日期格式不正确,请重新设置!");
			return;
		}
		if(!licenceDate.isValid()) {
			JShell.Msg.alert("临时授权日期格式不正确,请重新设置!");
			return;
		}

		//var records = me.getSelectionModel().getSelection();
		var records = me.getSelection();
		me.setDeSelect();
		if(records.length > 0) {
			var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
			Ext.Array.each(records, function(record, index, countriesItSelf) {
				//var LicenceTypeId = record.get("LicenceTypeId");
				record.set("LicenceTypeId", info.Id);
				record.set("LicenceDate", licenceDateValue);
				//有效期状态处理
				me.setLicenceStatus(record, licenceDateValue);
				//record.commit();
			});
			JShell.Msg.alert("批量设置临时授权完成!", null, 2000);
		} else {
			JShell.Msg.alert("请选择记录行后再操作!");
			return;
		}
	}
});