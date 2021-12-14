/**
 * 程序信息列表
 * @author longfc
 * @version 2016-09-27
 */
Ext.define('Shell.class.oa.pgm.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger'
	],
	/**所属模块的子序号*/
	//SUB_WIN_NO:'',
	title: '程序信息',
	width: 1200,
	height: 800,
	/**获取数据服务路径PGM_UDTO_SearchPGMProgramByHQL*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByBDictTreeId?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
	    property: 'PGMProgram_PublisherDateTime',
	    direction: 'DESC'
	}, {
		property: 'PGMProgram_SubBDictTree_CName',
		direction: 'ASC'
	}, {
		property: 'PGMProgram_Status',
		direction: 'ASC'
	}, {
		property: 'PGMProgram_DataAddTime',
		direction: 'ASC'
	}, {
		property: 'PGMProgram_ClientName',
		direction: 'ASC'
	}, {
		property: 'PGMProgram_OtherFactoryName',
		direction: 'ASC'
	}, {
		property: 'PGMProgram_Title',
		direction: 'ASC'
	}],
	/**删除标志字段*/
	DelField: 'delState',
	multiSelect: true,
	defaultWhere: '',
	hasShow: false,
	hasEdit: true,
	hasDel: false,
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**所属字典树上级节点Id*/
	PBDictTreeId: "",
	PBDictTreeCName: '',
	/**所属字典树Id*/
	SubBDictTreeId: "",
	checkOne: false,
	autoHeight: false,
	/**是否隐藏工具栏查询条件*/
	hiddenSearch: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	/**文档状态选择项的默认值*/
	defaultStatusValue: "0",
	/**是否隐藏文档状态选择项*/
	hiddenFFileStatus: false,
	hasRefresh: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'pgmprogram.DataAddTime',
	isSearchChildNode: true,
	/*是否带内容类型复选**/
	hasCheckBDictTree: true,
	autoScroll: true,
	/**默认每页数量*/
	defaultPageSize: 20,
	/**当前树选择的节点是否是叶子节点*/
	isLeaf: true,
	/**是否显示被禁用的数据*/
	isShowDel: false,
	/**是否隐藏程序附件大小*/
	hiddenPGMProgramSize: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(me.hiddenbuttonsToolbar) {
			buttonsToolbar.setVisible(false);
		}
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 780
		});
	},
	initComponent: function() {
		var me = this;
		me.regStr = new RegExp('"', "g");
		me.IDS = me.IDS || "";
		me.isLeaf = me.isLeaf || true;
		if(!me.checkOne) {
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		if(me.hiddenPGMProgramSize == false) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
		//创建数据列
		me.columns = me.createGridColumns();
		//初始化功能按钮栏内容
		me.createSearchtoolbar();
		//初始化默认条件
		me.initDefaultWhere();
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onShowTabPanelClick', me);
		me.callParent(arguments);
	},
	/**对外公开,由外面传入列信息*/
	createNewColumns: function() {
		var me = this;
		//创建数据列
		var tempColumns = [];
		return tempColumns;
	},
	createGridColumns: function() {
		var me = this;
		//创建数据列
		var columns = [];
		columns = me.createNewColumns().length > 0 ? me.createNewColumns() : me.createDefaultColumns();
		return columns;
	},
	/**初始化查询栏内容*/
	createSearchtoolbar: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {};
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		items.push('->', "-");
		//是否带内容类型复选
		if(me.hasCheckBDictTree) {
			items.push({
				boxLabel: '本节点',
				itemId: 'checkBDictTreeId',
				checked: false,
				value: false,
				inputValue: false,
				xtype: 'checkbox',
				style: {
					marginRight: '4px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.isSearchChildNode = false;
						} else {
							me.isSearchChildNode = true;
						}
						me.onSearch();
					}
				}
			});
		}
		items.push({
			boxLabel: '显示禁用',
			itemId: 'checkIsUse',
			checked: me.isShowDel,
			value: me.isShowDel,
			inputValue: false,
			xtype: 'checkbox',
			style: {
				marginRight: '4px'
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue == true) {
						me.isShowDel = true;
					} else {
						me.isShowDel = false;
					}
					me.onSearch();
				}
			}
		});

		items.push({
			fieldLabel: '仪器Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'EquipType_Id',
			name: 'EquipType_Id'
		}, {
			fieldLabel: '仪器Id',
			hidden: true,
			xtype: 'textfield',
			itemId: 'EquipFactoryBrand_Id',
			name: 'EquipFactoryBrand_Id'
		}, {
			width: 100,
			emptyText: '仪器分类选择',
			name: 'EquipType_CName',
			itemId: 'EquipType_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: '仪器分类选择',
				height: 480,
				defaultWhere: "pdict.PDictType.Id=" + me.ETYPEID
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('EquipType_CName');
					var Id = me.getComponent('buttonsToolbar').getComponent('EquipType_Id');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					p.close();
					me.onSearch();
				}
			}
		}, {
			width: 100,
			emptyText: '仪器品牌选择',
			labelWidth: 0,
			name: 'EquipFactoryBrand_CName',
			itemId: 'EquipFactoryBrand_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: '仪器品牌选择',
				height: 480,
				defaultWhere: "pdict.PDictType.Id=" + me.EBRADID
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('EquipFactoryBrand_CName');
					var Id = me.getComponent('buttonsToolbar').getComponent('EquipFactoryBrand_Id');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					p.close();
					me.onSearch();
				}
			}
		});
		items.push("-");
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JcallShell.QMS.Enum.getList('PGMProgramDateType', false, false, true, true, false),
			value: me.defaultFFileDateTypeValue,
			width: 72,
			labelWidth: 0,
			fieldLabel: '',
			emptyText: '日期选择',
			tooltip: '日期选择',
			itemId: 'dateType',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			width: 95,
			labelWidth: 1,
			labelAlign: 'right',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JcallShell.QMS.Enum.getList('PGMProgramStatus', true, false, true, true, false),
			value: me.defaultStatusValue,
			width: 70,
			labelWidth: 0,
			hidden: me.hiddenFFileStatus,
			fieldLabel: '',
			tooltip: '状态选择',
			emptyText: '状态选择',
			itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push("-");
		items.push({
			xtype: 'uxSimpleComboBox',
			value: "pgmprogram.Title",
			width: 85,
			labelWidth: 0,
			fieldLabel: '',
			itemId: 'CustomSearchType',
			tooltip: '按自定义选择项搜索',
			data: [
				["-1", "请选择"],
				["pgmprogram.Title", "程序标题"],
				["pgmprogram.No", "程序编号"],
				["pgmprogram.VersionNo", "程序版本号"],
				["pgmprogram.SQH", "程序授权号"],
				["pgmprogram.PublisherName", "发布人"],
				["pgmprogram.ClientName", "用户名称"],
				["pgmprogram.OtherFactoryName", "厂家名称"],
				["pgmprogram.BEquip.CName", "仪器名称"],
				["pgmprogram.BEquip.EquipType.CName", "仪器分类"],
				["pgmprogram.BEquip.EquipFactoryBrand.CName", "仪器品牌"],
				["pgmprogram.BEquip.Equipversion", "仪器版本号"],
				["pgmprogram.BEquip.Shortcode", "仪器SQH号"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && newValue != "-1") {
						var buttonsToolbar = me.getComponent('buttonsToolbar'),
							customSearch = buttonsToolbar.getComponent('CustomSearch').getValue();
						if(customSearch && customSearch != "")
							me.onSearch();
					}
				}
			}
		}, {
			emptyText: '',
			width: 110,
			xtype: 'textSearchTrigger',
			name: 'CustomSearch',
			itemId: 'CustomSearch',
			emptyText: '自定义选择项输入',
			style: {
				marginRight: '5px'
			},
			listeners: {
				onSearchClick: function(com, value) {
					if(value && value != "") {
						var buttonsToolbar = me.getComponent('buttonsToolbar'),
							customSearchType = buttonsToolbar.getComponent('CustomSearchType').getValue();
						if(customSearchType && customSearchType != "")
							me.onSearch();
					}
				},
				onClearClick: function() {
					me.onSearch();
				}
			}
		});
		me.buttonToolbarItems = items;
	},
	showQtipValue: function(meta, record) {
		var me = this;
		var qtipTitleValue = record.get("PGMProgram_Title");
		qtipTitleValue = qtipTitleValue.replace(me.regStr, "'");
		var qtipMemoValue = record.get("PGMProgram_Memo");
		qtipMemoValue = qtipMemoValue.replace(me.regStr, "'");
		var qtipContentValue = record.get("PGMProgram_Content");
		qtipContentValue = qtipContentValue.replace(me.regStr, "'");

		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>程序标题:</b>" + qtipTitleValue + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>概要说明:</b>" + qtipMemoValue + "</p>";
		qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>详细说明:</b>" + qtipContentValue + "</p>";

		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	/**创建数据列*/
	createDefaultColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '主键ID',
			dataIndex: 'PGMProgram_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '仪器ID',
			dataIndex: 'PGMProgram_BEquip_Id',
			hidden: true,
			hideable: false
		}, {
			text: '上级字典树Id',
			dataIndex: 'PGMProgram_PBDictTree_Id',
			hidden: true,
			hideable: false
		}, {
			text: '所属字典树Id',
			dataIndex: 'PGMProgram_SubBDictTree_Id',
			hidden: true,
			hideable: false
		}, {
			text: '是否允许评论',
			dataIndex: 'PGMProgram_IsDiscuss',
			hidden: true,
			hideable: false
		}, {
		    text: '发布时间',
		    dataIndex: 'PGMProgram_PublisherDateTime',
		    width: 130,
		    isDate: true,
		    hasTime: true,
		    renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
		        meta = me.showQtipValue(meta, record);
		        return value;
		    }
		}, {
			text: '分类',
			dataIndex: 'PGMProgram_SubBDictTree_CName',
			hidden: false,
			width: 70,
			hidden: false,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '标题',
			dataIndex: 'PGMProgram_Title',
			hidden: false,
			//flex: 1,
			width: 220,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '编号',
			dataIndex: 'PGMProgram_No',
			hidden: false,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '版本号',
			dataIndex: 'PGMProgram_VersionNo',
			width: 60,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '状态',
			dataIndex: 'PGMProgram_Status',
			width: 70,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.PGMProgramStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.PGMProgramStatusColor[value];
				return v;
			}
		}, {
			text: '上级字典树',
			dataIndex: 'PGMProgram_PBDictTree_CName',
			hidden: false,
			hidden: true,
			width: 120,
			hideable: false
		});
		columns.push({
			text: '启用',
			dataIndex: 'PGMProgram_IsUse',
			width: 40,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				if(value.toString() == "true" || value.toString() == "1") {
					value = "是";
				} else {
					value = "否";
					meta.style = 'font-weight:bold;color:red';
				}
				return value;
			}
		});
		columns.push(me.createIsUse());
		//发布列
		columns.push(me.createRelease());
		//查看操作列
		columns.push(me.createShowCcolumn());
		//交流列
		columns.push(me.createInteraction());
		//操作记录查看列
		columns.push(me.createOperation());

		//总阅读数列
		columns.push({
			text: '调整附件大小',
			dataIndex: 'PGMProgram_Size',
			hidden: me.hiddenPGMProgramSize,
			width: 105,
			hideable: false,
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var size = "";
				if(value) {
					size = JShell.Bytes.toSize(parseFloat(value));
				}
				var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>作用于:</b>手工拷贝附件到服务器时调整</p>";
				qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>提示:</b>保存时以字节为单位</p>";
				qtipValue = qtipValue + "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>附件大小:</b>" + size + "</p>";
				meta.tdAttr = 'data-qtip="' + qtipValue + '"';
				return value;
			},
			editor: {
				xtype: 'numberfield',
				allowDecimals: false, // 是否与许小数 
				decimalPrecision: 0, // 小数位精度 
				allowNegative: false, // 是否允许负数 
				allowBlank: true,
				listeners: {
					change: function(com, newValue, oldValue) {
						if(newValue && newValue != oldValue) {
							JShell.Action.delay(function() {
								var record = com.ownerCt.editingPlugin.context.record;
								me.UpdateSizeById(record, newValue);
							}, null, 1800);
						}
					}
				}
			}
		}, {
			text: '程序授权号',
			dataIndex: 'PGMProgram_SQH',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '发布人',
			dataIndex: 'PGMProgram_PublisherName',
			hidden: false,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},  {
			text: '阅读总数',
			dataIndex: 'PGMProgram_Counts',
			hidden: false,
			width: 65,
			hideable: false
		}, {
			text: '用户名称',
			dataIndex: 'PGMProgram_ClientName',
			width: 120,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '厂家',
			dataIndex: 'PGMProgram_OtherFactoryName',
			width: 120,
			sortable: true,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '仪器',
			dataIndex: 'PGMProgram_BEquip_CName',
			width: 120,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '仪器分类',
			dataIndex: 'PGMProgram_BEquip_EquipType_CName',
			width: 120,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '仪器品牌',
			dataIndex: 'PGMProgram_BEquip_EquipFactoryBrand_CName',
			width: 120,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '仪器型号',
			dataIndex: 'PGMProgram_BEquip_Equipversion',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '仪器SQH号',
			dataIndex: 'PGMProgram_BEquip_Shortcode',
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '概要说明',
			dataIndex: 'PGMProgram_Memo',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			text: '详细说明',
			dataIndex: 'PGMProgram_Content',
			hidden: true,
			width: 80,
			hideable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		});
		return columns;
	},
	/*创建交流列**/
	createShowCcolumn: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '查看',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowTabPanelClick(grid, rec, rowIndex, colIndex);
				}
			}]
		};
	},
	/*打开查看应用*/
	showPGMProgramById: function() {
		var me = this;
	},
	/*创建程序启用或禁用列**/
	createIsUse: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '启/禁',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('PGMProgram_IsUse') == "true") {
						meta.tdAttr = 'data-qtip="<b>程序禁用</b>"';
						//meta.style = 'background-color:green;';
						return 'button-edit hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>程序启用</b>"';
						//meta.style = 'background-color:red;';
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isUse = rec.get('PGMProgram_IsUse');
					var msg = isUse.toString() == "true" ? "是否禁用该程序" : "是否启用程序";
					var newIsUse = isUse.toString() == "true" ? false : true;
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.UpdateIsUseByStrIds(rec, newIsUse);
							}
						}
					});
				}
			}]
		};
	},
	/*创建发布列**/
	createRelease: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '发布',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var status = rec.get("PGMProgram_Status");
					var isUse = rec.get("PGMProgram_IsUse");
					var msgInfo = "";
					var isExec = true;
					if(isUse.toLowerCase().toString() == "false" || isUse.toString() == "0") {
						isExec = false;
						msgInfo = "当前程序已被禁用,不能发布!<br />";
					}
					if(isExec == true && status.toString() == "3") {
						isExec = false;
						msgInfo = msgInfo + "当前程序已经发布!";
					}
					if(isExec == true) {
						me.UpdateStatusByStrIds(rec, "3");
					} else {
						JShell.Msg.alert(msgInfo, null, 1000);
					}
				}
			}]
		};
	},
	/*程序启用或禁用操作**/
	UpdateIsUseByStrIds: function(rec, newIsUse) {
		var me = this;
		var id = rec.get(me.PKField);
		var isUse = rec.get('PGMProgram_IsUse');
		var url = "/PDProgramManageService.svc/PGM_UpdateIsUseByStrIds";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = {
			strIds: id,
			isUse: newIsUse
		};
		var msgInfo = "";
		if(isUse.toLowerCase().toString() == "true" || isUse.toString() == "1") {
			msgInfo = "程序禁用";
		} else {
			msgInfo = "程序启用";
		}
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.set("PGMProgram_IsUse", newIsUse);
				//me.getView().refresh();
				rec.commit();
				JShell.Msg.alert(msgInfo + "成功", null, 1000);
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo);

			}
		});
	},
	/*修改程序状态**/
	UpdateStatusByStrIds: function(rec, status) {
		var me = this;
		var id = rec.get(me.PKField);
		var url = "/PDProgramManageService.svc/PGM_UpdateStatusByStrIds";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = {
			strIds: id,
			status: status
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.set("PGMProgram_Status", status);
				//me.getView().refresh();
				rec.commit();
				JShell.Msg.alert("发布成功", null, 1000);
				//me.onSearch();
			} else {
				var msg = data.msg;
				msg = '发布失败';
				JShell.Msg.error(msg);
			}
		});
	},
	/*修改程序附件大小**/
	UpdateSizeById: function(rec, size) {
		var me = this;
		var id = rec.get(me.PKField);
		var url = "/PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgramByField";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var entity = {
			Id: id,
			Size: size
		};
		var params = {
			entity: entity,
			fields: "Id,Size"
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.set("PGMProgram_Size", size);
				rec.commit();
				me.getView().refresh();				
				//JShell.Msg.alert("修改程序附件大小成功", null, 1000);
			} else {
				var msg = data.msg;
				msg = '修改程序附件大小失败';
				JShell.Msg.error(msg);
			}
		});
	},
	/*创建操作记录列**/
	createOperation: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 55,
			hidden: false,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openOperationGrid(rec);
				}
			}]
		};
	},
	/**打开操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var config = {
			showSuccessInfo: false,
			resizable: false,
			hasButtontoolbar: false,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.oa.sc.operation.Grid', config).show();
	},
	/*创建交流列**/
	createInteraction: function() {
		var me = this;
		return {
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var isInteraction = rec.get("PGMProgram_IsDiscuss");
					if(isInteraction) {
						me.showInteractionById(rec);
					} else {
						JShell.Msg.error('当前文档不允许交流');
					}
				}
			}]
		};
	},

	/**根据ID查看交流*/
	showInteractionById: function(record) {
		var me = this;
		var id = record.get('PGMProgram_Id');
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.sysbase.scinteraction.App', {
			PK: id,
			height: height,
			width: maxWidth
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			customSearch = buttonsToolbar.getComponent('CustomSearch').getValue(),
			customSearchType = buttonsToolbar.getComponent('CustomSearchType').getValue(),
			EquipTypeId = buttonsToolbar.getComponent('EquipType_Id').getValue(),
			EquipFactoryBrandId = buttonsToolbar.getComponent('EquipFactoryBrand_Id').getValue(),
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue(),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			dateType = buttonsToolbar.getComponent('dateType').getValue();
		var checkPDictId = buttonsToolbar.getComponent('checkPDictId');
		var params = [];

		//仪器分类
		if(EquipTypeId) {
			params.push("pgmprogram.BEquip.EquipType.Id=" + EquipTypeId + "");
		}
		//仪器品牌
		if(EquipFactoryBrandId) {
			params.push("pgmprogram.BEquip.EquipFactoryBrand.Id=" + EquipFactoryBrandId + "");
		}
		if(selectStatus) {
			params.push("pgmprogram.Status=" + selectStatus + "");
		}
		if(dateType == "") {
			dateType = "pgmprogram.DataAddTime";
		}
		if(BeginDate) {
			params.push(dateType + ">='" + JShell.Date.toString(BeginDate, true) + "'");
		}
		//自定义搜索
		if(customSearchType && customSearchType != "-1") {
			if(customSearch && customSearch != "") {
				params.push(customSearchType + " like '%" + customSearch + "%'");
			}
		}
		//是否显示被禁用的数据,如果不显示
		if(me.isShowDel == false) {
			params.push("pgmprogram.IsUse=1");
		}
		var where = "",
			url = JShell.System.Path.ROOT + me.selectUrl + "&isSearchChildNode=" + me.isSearchChildNode;

		//根节点查询处理,如果是根节点,不按SubBDictTreeId查询
		if(me.SubBDictTreeId && me.SubBDictTreeId.toString().length > 0 && me.IDS.toString() == me.SubBDictTreeId.toString()) {
			url = JShell.System.Path.ROOT + "/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByHQL?isPlanish=true";
		} else if(me.SubBDictTreeId && me.SubBDictTreeId.toString().length > 0) {
			where = 'id=' + me.SubBDictTreeId + '^';
		}

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			params.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			params.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			params.push(me.externalWhere);
		}
		if(params.length > 0) {
			where += '(' + params.join(" and ") + ')';
		}

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + JcallShell.String.encode(where) + '&fields=' + me.getStoreFields(true).join(',');

		return url;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}
	},
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onShowClick', me, records[0]);
	},
	onDelClick: function() {
		var me = this;
		me.fireEvent('onDelClick', me);
	},
	/**查看应用*/
	onShowTabPanelClick: function(grid, rec, rowIndex, colIndex) {
		var me = this;
		me.fireEvent('onShowTabPanelClick', grid, rec, rowIndex, colIndex);
	}
});