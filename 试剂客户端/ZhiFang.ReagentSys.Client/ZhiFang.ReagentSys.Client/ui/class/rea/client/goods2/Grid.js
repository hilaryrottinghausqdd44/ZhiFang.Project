/**
 * 机构货品维护
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.Grid', {
	extend: 'Shell.class.rea.client.goods2.basic.Grid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
	/**下载Excel文件*/
	downLoadExcelUrl: '/ReaManageService.svc/RS_UDTO_DownLoadExcel',
	/**导出Excel文件*/
	reportExcelUrl: '/ReaManageService.svc/RS_UDTO_GetReaGoodsReportExcelPath',
	/**手工同步服务**/
	manualSynchUrl: '/ReaCustomInterface.svc/RS_GetReaGoodsByInterface',
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	isRowEdit: true,
	editText: '按行编辑',
	isCycle: false,
	isRowCycle: false,
	isColCycle: false,
	rowIdx: null,
	colIdx: null,
	isSpecialkey: false,
	specialkeyArr: [{
		key: Ext.EventObject.ENTER,
		replaceKey: Ext.EventObject.TAB
	}, {
		key: Ext.EventObject.LEFT,
		type: 'left',
		ctrlKey: true
	}, {
		key: Ext.EventObject.RIGHT,
		type: 'right',
		ctrlKey: true
	}],

	//按相同码 ，货品  排序 
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoods_DispOrder',
		direction: 'ASC'
	}],
	/**是否启用序号列*/
	hasRownumberer: true,
	/*厂商*/
	ProdOrg: 'ProdOrg',
	IsCanCel: false,
	/**用户UI配置Key*/
	userUIKey: 'goods2.Grid',
	/**用户UI配置Name*/
	userUIName: "机构货品维护列表",
	/**一级分类集合*/
	GoodsClass:[],
	/**二级分类集合*/
	GoodsClassType:[],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getGoodsClass("GoodsClass",function(){
			
		});
		me.getGoodsClass("GoodsClassType",function(){
			
		});
		
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});
		//初始化检索监听
		me.initFilterListeners();
		me.createCellEditListeners();
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var btnMinUnit =null;
		if(buttonsToolbar) btnMinUnit = buttonsToolbar.getComponent("btnMinUnit");
		me.on({
			select: function(com, record, index, eOpts) {
				var ReaGoodsNo = record.get('ReaGoods_ReaGoodsNo');
				if(!ReaGoodsNo) {
					btnMinUnit.setDisabled(true);
				} else {
					btnMinUnit.setDisabled(false);
				}
			},
			itemclick: function(com, record, item, index, e, eOpts) {
				var ReaGoodsNo = record.get('ReaGoods_ReaGoodsNo');
				if(!ReaGoodsNo) {
					btnMinUnit.setDisabled(true);
				} else {
					btnMinUnit.setDisabled(false);
				}
			},
			itemdblclick: function(view, record,index,e, eOpts ) {
				me.showForm(record.get(me.PKField), record.get('ReaGoods_CName'), record.get('ReaGoods_ReaGoodsNo'));
			},
			validateedit: function(editor, e) {
				var bo = false;
				var edit = me.getPlugin('NewsGridEditing');

				bo = me.fireEvent('cellAvailable', editor, e) === false ? false : true;

				if(me.rowIdx == null && me.colIdx == null) {
					me.colIdx = e.colIdx;
					me.rowIdx = e.rowIdx;
				}
				if(me.rowIdx != null && me.colIdx != null) {
					if(me.isSpecialkey) {
						edit.startEditByPosition({
							row: me.rowIdx,
							column: me.colIdx
						})
					}
					me.rowIdx = null;
					me.colIdx = null;
					me.isSpecialkey = false
				}
				return bo
			}
		});
	},
	initComponent: function() {
		var me = this;
		if(me.isCycle) {
			me.isRowCycle = true;
			me.isColCycle = true
		}
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;
		//默认条件
		me.callParent(arguments);
	},
	getGoodsClass:function(classType,callback){
		var me=this;
		if(me[classType]&&me[classType].length>0)return callback();
		
		var fields="ReaGoodsClassVO_CName,ReaGoodsClassVO_Id";
		var url = JShell.System.Path.ROOT +
			'/ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?isPlanish=true';
		
		url = url + "&fields=" + fields; // JShell.JSON.encode(fields);
		url = url + "&classType=" + classType;
		JShell.Server.get(url, function(data) {
			if (data.success && data.value && data.value.list) {
				me[classType] = data.value.list;
			} else {
				me[classType] = [];
			}
			if (callback) callback();
		}, false);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', 'save', '-', {
			xtype: 'splitbutton',
			textAlign: 'left',
			iconCls: 'button-edit',
			text: '批量修改',
			handler: function(btn, e) {
				btn.overMenuTrigger = true;
				btn.onClick(e);
			},
			menu: [{
					text: '条码类型',
					iconCls: 'button-edit',
					tooltip: '勾选试剂批量修改条码类型',
					listeners: {
						click: function(but) {
							me.onChangeBarcodeType();
						}
					}
				},
				{
					text: '有注册证',
					iconCls: 'button-accept',
					tooltip: '勾选试剂批量修改为"有注册证"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsRegister', 1);
						}
					}
				}, {
					text: '无注册证',
					iconCls: 'button-cancel',
					tooltip: '勾选试剂批量修改为"无注册证"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsRegister', 0);
						}
					}
				}, {
					text: '打印条码',
					iconCls: 'button-accept',
					tooltip: '勾选试剂批量修改为"打印条码"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsPrintBarCode', 1);
						}
					}
				}, {
					text: '不打印条码',
					iconCls: 'button-cancel',
					tooltip: '勾选试剂批量修改为"不打印条码"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsPrintBarCode', 0);
						}
					}
				}, {
					text: '需要性能验证',
					iconCls: 'button-accept',
					tooltip: '勾选试剂批量修改为"需要性能验证"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsNeedPerformanceTest', 1);
						}
					}
				}, {
					text: '不需要性能验证',
					iconCls: 'button-cancel',
					tooltip: '勾选试剂批量修改为"不需要性能验证"',
					listeners: {
						click: function(but) {
							me.onChangeInfo('IsNeedPerformanceTest', 0);
						}
					}
				}
			]
		}, '-', {
			xtype: 'splitbutton',
			textAlign: 'left',
			iconCls: 'file-excel',
			text: '导入导出',
			handler: function(btn, e) {
				btn.overMenuTrigger = true;
				btn.onClick(e);
			},
			menu: [{
				text: '货品导入',
				iconCls: 'file-excel',
				tooltip: 'EXCEL导入试剂',
				listeners: {
					click: function(but) {
						me.onGoodsImportExcel('Comp');
					}
				}
			}, {
				text: '模板下载',
				iconCls: 'button-exp',
				tooltip: '货品模板下载',
				listeners: {
					click: function(but) {
						me.onGoodsExp();
					}
				}
			}, {
				text: '勾选导出',
				iconCls: 'file-excel',
				tooltip: '导出勾选的货品信息到EXCEL',
				listeners: {
					click: function(but) {
						me.onGoodsExportExcel(1);
					}
				}
			}, {
				text: '条件导出',
				iconCls: 'file-excel',
				tooltip: '导出复合条件的货品信息到EXCEL',
				listeners: {
					click: function(but) {
						me.onGoodsExportExcel(2);
					}
				}
			}]
		}, '-', {
			text: '设置最小单位',
			tooltip: '设置最小单位',
			iconCls: 'button-config',
			itemId: 'btnMinUnit',
			handler: function() {
				var records = me.getSelectionModel().getSelection(),
					len = records.length;

				if(len == 0 || len > 1) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				var ReaGoodsNo = records[0].get('ReaGoods_ReaGoodsNo');
				if(!ReaGoodsNo) {
					JShell.Msg.error('没有设置货品编码,不能设置最小单位');
					return;
				}
				me.showConfigForm(ReaGoodsNo);
			}
		}, {
			text: '数据完整性检查',
			tooltip: '数据完整性检查',
			iconCls: 'button-show',
			handler: function() {
				me.showMinUnit();
			}
		}, '-', {
			xtype: 'radiogroup',
			fieldLabel: '',
			width: 130,
			columns: 2,
			vertical: true,
			items: [{
					boxLabel: '行编辑',
					name: 'rb',
					inputValue: '1',
					checked: true
				},
				{
					boxLabel: '列编辑',
					name: 'rb',
					inputValue: '2'
				}
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var val = newValue.rb;
					if(val == '1') {
						me.specialkeyArr = [{
							key: Ext.EventObject.ENTER,
							replaceKey: Ext.EventObject.TAB
						}];
					} else {
						me.specialkeyArr = [{
							key: Ext.EventObject.ENTER,
							type: 'down'
						}];
					}
				}
			}
		}];
		//查询框信息
		me.searchInfo = {
			width: 225,
			isLike: true,
			itemId: 'Search',
			tooltip: '货品编码/名称/英文名/简称/拼音字头',
			emptyText: '货品编码/名称/英文名/简称/拼音字头',
			fields: ['reagoods.ReaGoodsNo', 'reagoods.CName', 'reagoods.EName', 'reagoods.SName','reagoods.ShortCode', 'reagoods.PinYinZiTou']
		};
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		// 新增一个手工同步功能
		items.push({
			text: '手工同步',
			tooltip: '同步',
			iconCls: 'button-config',
			handler: function() {
				me.manualSync();
			}
		
		})
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = {
			xtype: 'toolbar',
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar2',
			items: [{
				emptyText: '一级分类',
				labelWidth: 0,
				width: 100,
				fieldLabel: '',
				itemId: 'GoodsClass',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
				classConfig: {
					title: '一级分类',
					ClassType: "GoodsClass"
				},
				listeners: {
					check: function(p, record) {
						me.onGoodsClass(p, record, 'GoodsClass');
					}
				}
			}, {
				emptyText: '二级分类',
				labelWidth: 0,
				width: 100,
				fieldLabel: '',
				itemId: 'GoodsClassType',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
				classConfig: {
					title: '二级分类',
					ClassType: "GoodsClassType"
				},
				listeners: {
					check: function(p, record) {
						me.onGoodsClass(p, record, 'GoodsClassType');
					}
				}
			}, {
				labelWidth: 0,
				width: 100,
				fieldLabel: '',
				emptyText: '所属品牌',
				name: 'ReaGoods_ProdOrgName',
				emptyText: '品牌',
				itemId: 'ReaGoods_ProdOrgName',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.dict.CheckGrid',
				classConfig: {
					title: '品牌选择',
					defaultWhere: "bdict.BDictType.DictTypeCode='" + this.ProdOrg + "'"
				},
				listeners: {
					check: function(p, record) {
						me.onCompAccept(p, record);
					}
				}
			}, {
				fieldLabel: '厂商主键ID',
				itemId: 'ReaGoods_Prod_Id',
				name: 'ReaGoods_Prod_Id',
				xtype: 'textfield',
				hidden: true
			}, {
				labelWidth: 0,
				width: 75,
				fieldLabel: '',
				emptyText: '条码类型',
				name: 'BarCodeMgr',
				itemId: 'BarCodeMgr',
				xtype: 'uxSimpleComboBox',
				value: '',
				hasStyle: true,
				data: [
					['-1', '请选择', 'font-weight:bold;color:black;'],
					['0', '批条码', 'color:green;'],
					['1', '盒条码', 'color:orange;'],
					['2', '无条码', 'color:black;']
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(!me.IsCanCel) {
							me.onGridSearch();
						}
					}
				}
			}, {
				labelWidth: 0,
				width: 75,
				fieldLabel: '',
				emptyText: '有注册证',
				itemId: 'IsRegister',
				xtype: 'uxSimpleComboBox',
				value: '',
				hasStyle: true,
				data: [
					['-1', '请选择', 'font-weight:bold;color:black;'],
					['1', JShell.All.TRUE, 'font-weight:bold;color:green;'],
					['0', JShell.All.FALSE, 'font-weight:bold;color:red;']
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(!me.IsCanCel) {
							me.onGridSearch();
						}
					}
				}
			}, {
				labelWidth: 0,
				width: 75,
				fieldLabel: '',
				emptyText: '打印条码',
				itemId: 'IsPrintBarCode2',
				xtype: 'uxSimpleComboBox',
				value: '',
				hasStyle: true,
				data: [
					['-1', '请选择', 'font-weight:bold;color:black;'],
					['1', JShell.All.TRUE, 'font-weight:bold;color:green;'],
					['0', JShell.All.FALSE, 'font-weight:bold;color:red;']
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(!me.IsCanCel) {
							me.onGridSearch();
						}
					}
				}
			}, {
				labelWidth: 0,
				width: 75,
				fieldLabel: '',
				emptyText: '最小单位',
				itemId: 'IsMinUnit',
				xtype: 'uxSimpleComboBox',
				value: '',
				hasStyle: true,
				data: [
					['-1', '请选择', 'font-weight:bold;color:black;'],
					['1', JShell.All.TRUE, 'font-weight:bold;color:green;'],
					['0', JShell.All.FALSE, 'font-weight:bold;color:red;']
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(!me.IsCanCel) {
							me.onGridSearch();
						}
					}
				}
			}, {
				labelWidth: 0,
				width: 75,
				fieldLabel: '',
				emptyText: '是否启用',
				itemId: 'Visible',
				xtype: 'uxSimpleComboBox',
				value: '',
				hasStyle: true,
				data: [
					['-1', '请选择', 'font-weight:bold;color:black;'],
					['1', JShell.All.TRUE, 'font-weight:bold;color:green;'],
					['0', JShell.All.FALSE, 'font-weight:bold;color:red;']
				],
				labelWidth: 35,
				width: 90,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(!me.IsCanCel) {
							me.onGridSearch();
						}
					}
				}
			}, {
				boxLabel: '仅显示换算系数为0',
				name: 'cboGonvertQtyIsZero',
				itemId: 'cboGonvertQtyIsZero',
				xtype: 'checkboxfield',
				inputValue: false,
				checked: false,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.onSearch();
					}
				}
			}, '-', {
				text: '清空',
				tooltip: '清空',
				iconCls: 'button-cancel',
				handler: function() {
					me.onCancel();
				}
			}]
		};
		return items;
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onGridSearch();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			dataIndex: 'ReaGoods_GoodsSort',
			text: '货品序号',
			hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_SName',
			text: '简称',
			width: 60,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			renderer: function(value, meta, record) {
				//如货品为"盒条码",又需要打印条码,但转换系数设置为0或为空,货品编码背景颜色以红色提示
				var barCodeMgr = record.get("ReaGoods_BarCodeMgr");
				var isPrintBarCode = record.get("ReaGoods_IsPrintBarCode");
				var gonvertQty = record.get("ReaGoods_GonvertQty");
				if(isPrintBarCode == "1" || isPrintBarCode == 1) isPrintBarCode = true;
				var qtipValue = "";
				if(barCodeMgr == "1" && isPrintBarCode == true && gonvertQty <= 0) {
					qtipValue = "当前货品设置为打印条码,但转换系数设置为0或为空";
					meta.tdAttr = 'data-qtip="<b>' + qtipValue + '</b>"';
					meta.style = 'background-color:red;color:#ffffff;'
				}
				return value;
			}
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品平台编码',
			hidden: true,
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_EName',
			text: '英文名称',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_IsMed',
			text: '是否医疗器械',width: 100,align:'center',
			type:'bool',isBool:true,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdEara',
			text: '产地',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '包装单位',
			width: 100,
			editor: {
				xtype: 'textfield'
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdOrgName',
			text: '品牌',
			hidden: false,
			width: 140,
			editor: {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.dict.CheckGrid',
				//isWinOpen:true,
				classConfig: {
					title: '品牌选择',
					defaultWhere: "bdict.BDictType.DictTypeCode='" + me.ProdOrg + "'"
				},
				listeners: {
					check: function(p, rec) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						records[0].set('ReaGoods_ProdOrgName', rec ? rec.get('BDict_CName') : '');
						//records[0].commit();
						var picker= p.getPicker();
						//取消选择列表的当前选择，防止再次选择失效
						if(picker)picker.getSelectionModel().deselectAll();
						p.close();
						me.getView().refresh();
					}
				}
			}
		}, {
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',
			hidden: false,
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			text: '二级分类',
			dataIndex: 'ReaGoods_GoodsClassType',
			width: 100,
			hidden: false,
			editor: {},
			defaultRenderer: true
		}, {
			text: '部门',
			dataIndex: 'ReaGoods_DeptName',
			width: 100,
			hidden: false,
			editor: {},
			defaultRenderer: true
		}, {
			text: '测试数',
			dataIndex: 'ReaGoods_TestCount',
			width: 100,
			hidden: false,
			editor: {
				minValue: 0,
				xtype: 'numberfield'
			},
			defaultRenderer: true
		}, {
			text: '适用机型',
			dataIndex: 'ReaGoods_SuitableType',
			width: 100,
			hidden: false,
			editor: {},
			defaultRenderer: true
		}, {
			text: '供应商',
			dataIndex: 'ReaGoods_ReaCompanyName',
			width: 100,
			hidden: false,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			editor: {
				xtype: 'uxSimpleComboBox',
				value: '0',
				hasStyle: true,
				data: [
					['0', '批条码', 'color:green;'],
					['1', '盒条码', 'color:orange;'],
					['2', '无条码', 'color:black;']
				]
			},
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_IsPrintBarCode',
			text: '是否打印条码',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_IsRegister',
			text: '有无注册证',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_IsNeedPerformanceTest',
			text: '是否性能验证',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GonvertQty',
			text: '换算系数',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_LinkGroupCode',
			text: '相识货品码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Visible',
			text: '是否启用',
			width: 65,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			}
		}, {
			dataIndex: 'ReaGoods_StorageType',
			text: '储藏条件',
			hidden: false,
			width: 100,
			editor: {},
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta);
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ReaCenOrg_CName',
			text: '机构',
			width: 100,
			hidden: true,
			sortable: false,
			defaultRenderer: true
			//			editor:{}
		}, {
			dataIndex: 'ReaGoods_Price',
			text: '参考价格',
			width: 100,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_RegistNo',
			text: '注册号',
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_RegistDate',
			text: '注册日期',
			width: 90,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_RegistNoInvalidDate',
			text: '注册证有效期',
			width: 90,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			type: 'date', //isDate:true,//defaultRenderer: true
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, {
			dataIndex: 'ReaGoods_DispOrder',
			text: '次序',
			width: 50,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_MonthlyUsage',
			text: '理论月用量',
			width: 100,
			sortable: false,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_MatchCode',
			text: '物资对照码',
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_NetGoodsNo',
			text: '挂网流水号',
			//editor:{},
			width: 100
		}, {
			xtype: 'actioncolumn',
			text: '注册证',
			align: 'center',
			width: 55,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var ReaGoodsNo = record.get('ReaGoods_ReaGoodsNo') + '';
					var isRegister = record.get('ReaGoods_IsRegister') + '';
					//有货品编码的可以维护注册证
					if(ReaGoodsNo) {
						meta.tdAttr = 'data-qtip="<b>注册证编辑</b>"';
						return 'button-edit hand';
					} else {
						return 'button-actionedit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var ReaGoodsNo = rec.get('ReaGoods_ReaGoodsNo') + '';
					var Id = rec.get('ReaGoods_Id') + '';
					var GoodsCName = rec.get('ReaGoods_GoodsCName') + '';
					//有货品编码的可以维护注册证
					if(ReaGoodsNo) {
						me.showRegisterForm(Id, GoodsCName);
					} else {
						JcallShell.Msg.error('请先维护注册证信息');
					}
				}
			}]
		}, {
			dataIndex: 'ErrorInfo',
			text: '错误信息',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}];
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});
		return columns;
	},
	/**导入试剂信息*/
	onGoodsImportExcel: function() {
		var me = this;
		JShell.Win.open('Shell.class.rea.client.goods2.UploadPanel', {
			formtype: 'add',
			resizable: false,
			CenOrg: {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: true
			}, //机构信息
			listeners: {
				save: function(p, records) {
					p.close();
					me.onGridSearch();
				}
			}
		}).show();
	},

	/**显示表单*/
	showForm: function(id, GoodsCName, ReaGoodsNo, IsNeedPerformanceTest) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.98; //* 0.98
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			width: maxWidth,
			height: height,
			/**一级分类集合*/
			GoodsClass:me.GoodsClass,
			/**二级分类集合*/
			GoodsClassType:me.GoodsClassType,
			listeners: {
				load: function() {
					var edit = me.getPlugin('NewsGridEditing');
					edit.cancelEdit();
				},
				close: function() {
					me.onGridSearch();
				}
			}
		};

		if(id) {
			config.formtype = 'edit';
			config.PK = id;
			/**货品ID*/
			config.GoodsID = id;
			/**货品名称*/
			config.GoodsCName = GoodsCName;
			config.ReaGoodsNo = ReaGoodsNo;
			config.IsNeedPerformanceTest = IsNeedPerformanceTest;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.goods2.AddPanel', config).show();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			list[i].Goods_IsBarCodeMgr = list[i].Goods_BarCodeMgr == '1' ? true : false;
			list[i].Goods_IsRegister = list[i].Goods_IsRegister == '1' ? true : false;
			list[i].Goods_IsPrintBarCode = list[i].Goods_IsPrintBarCode == '1' ? true : false;
		}
		data.list = list;
		return data;
	},
	/**批量修改条码类型*/
	onChangeBarcodeType: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			isValid = true,
			Ids = [];

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			Ids.push(rec.get(me.PKField));
		}
		if(!isValid) {
			JShell.Msg.error('供应商和实验室双方都取消确认才能进行操作!');
		} else {
			me.onCheckBarcodeType(Ids);
		}
	},
	/**选择处理*/
	onCheckBarcodeType: function(Ids) {
		var me = this;

		JShell.Win.open('Shell.class.rea.goods.BarcodeTypeCheckForm', {
			Ids: Ids,
			/**修改服务地址*/
			editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
			listeners: {
				save: function(p) {
					p.close();
					me.onGridSearch();
				}
			}
		}).show();
	},
	/**批量修改信息*/
	onChangeInfo: function(key, value) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			list = [];

		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			list.push(rec.get(me.PKField));
		}

		JShell.Msg.confirm({}, function(but) {
			if(but != "ok") return;
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = len;

			me.showMask(me.saveText); //显示遮罩层
			for(var i in list) {
				me.updateOne(i, list[i], key, value);
			}
		});
	},

	/**单个修改数据*/
	updateOne: function(index, Id, key, value) {
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;

		var params = {
			entity: {
				Id: Id
			},
			fields: 'Id'
		};
		params.entity[key] = value;
		params.fields += ',' + key;

		setTimeout(function() {
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						JShell.Msg.alert('批量修改数据成功', null, 1000);
						me.onGridSearch();
					} else {
						JShell.Msg.error('批量修改数据失败，请重新保存！');
					}
				}
			});
		}, 100 * index);
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ProdName = buttonsToolbar.getComponent('ProdName'),
			ProdId = buttonsToolbar.getComponent('ProdID');

		ProdName && ProdName.on({
			check: function(p, record) {
				ProdName.setValue(record ? record.get('ReaCenOrg_CName') : '');
				ProdId.setValue(record ? record.get('ReaCenOrg_Id') : '');
				p.close();
			}
		});
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		//this.fireEvent('addclick');
		var me = this;
		me.showForm(null, null, null);

	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var ReaGoodsNo = records[0].get('ReaGoods_ReaGoodsNo');
		var IsNeedPerformanceTest = records[0].get('ReaGoods_IsNeedPerformanceTest');
		me.showForm(records[0].get(me.PKField), records[0].get('ReaGoods_CName'), ReaGoodsNo, IsNeedPerformanceTest);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getWhere();
		return me.callParent(arguments);
	},

	/**清空查询条件数据*/
	onCancel: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			params = [];
		me.IsCanCel = true;
		if(!buttonsToolbar) return;
		if(!buttonsToolbar2) return;
		buttonsToolbar.getComponent('Search').setValue('');
		buttonsToolbar2.getComponent('IsRegister').setValue('');
		buttonsToolbar2.getComponent('Visible').setValue('');
		buttonsToolbar2.getComponent('IsPrintBarCode2').setValue('');
		buttonsToolbar2.getComponent('IsMinUnit').setValue('');
		buttonsToolbar2.getComponent('ReaGoods_Prod_Id').setValue('');
		buttonsToolbar2.getComponent('GoodsClass').setValue('');
		buttonsToolbar2.getComponent('GoodsClassType').setValue('');
		buttonsToolbar2.getComponent('BarCodeMgr').setValue('');
		buttonsToolbar2.getComponent('ReaGoods_ProdOrgName').setValue('');
		me.onGridSearch();
	},

	/**获取带查询参数的URL*/
	getWhere: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			IsRegister = null,
			Visible = null,
			IsPrintBarCode = null,
			IsMinUnit = null,
			ProdOrgName = null,
			GoodsClass = null,
			GoodsClassType = null,
			BarCodeMgr = null,
			search = null,
			params = [];
		if(!buttonsToolbar) return;
		if(!buttonsToolbar2) return;

		search = buttonsToolbar.getComponent('Search').getValue();
		IsRegister = buttonsToolbar2.getComponent('IsRegister').getValue();
		Visible = buttonsToolbar2.getComponent('Visible').getValue();
		IsPrintBarCode = buttonsToolbar2.getComponent('IsPrintBarCode2').getValue();
		IsMinUnit = buttonsToolbar2.getComponent('IsMinUnit').getValue();
		Prod_Id = buttonsToolbar2.getComponent('ReaGoods_ProdOrgName').getValue();
		GoodsClass = buttonsToolbar2.getComponent('GoodsClass').getValue();
		GoodsClassType = buttonsToolbar2.getComponent('GoodsClassType').getValue();
		barCodeMgr = buttonsToolbar2.getComponent('BarCodeMgr').getValue();
		isZero = buttonsToolbar2.getComponent('cboGonvertQtyIsZero').getValue();

		//改变默认条件
		//		me.changeDefaultWhere();
		me.internalWhere = '';
		if(barCodeMgr && barCodeMgr != '-1') {
			params.push('reagoods.BarCodeMgr=' + barCodeMgr);
		}
		if(IsRegister && IsRegister != '-1') {
			params.push('reagoods.IsRegister=' + IsRegister);
		}
		if(Visible && Visible != '-1') {
			params.push('reagoods.Visible=' + Visible);
		}
		if(IsPrintBarCode && IsPrintBarCode != '-1') {
			params.push('reagoods.IsPrintBarCode=' + IsPrintBarCode);
		}
		if(IsMinUnit && IsMinUnit != '-1') {
			params.push('reagoods.IsMinUnit=' + IsMinUnit);
		}
		if(Prod_Id) {
			params.push("reagoods.ProdOrgName like '%" + Prod_Id + "%'");
		}
		if(GoodsClass) {
			params.push("reagoods.GoodsClass ='" + GoodsClass + "'");
		}
		if(GoodsClassType) {
			params.push("reagoods.GoodsClassType ='" + GoodsClassType + "'");
		}
		if(isZero && isZero == true) {
			params.push("(reagoods.GonvertQty =0 or reagoods.GonvertQty is null)");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.internalWhere;
	},

	/**试剂模板下载*/
	onGoodsExp: function() {
		var me = this;
		var url = JShell.System.Path.UI + '/models/rea/reagoods/' + JShell.REA.Goods.EXCEL + '?v=' + new Date().getTime();
		window.open(url);
	},
	/**
	 * 试剂导出
	 * @param {Object} type 导出类型：勾选导出(1)，条件导出(2)
	 */
	onGoodsExportExcel: function(type) {
		this.onGoodsExportExcelByForm(type);
	},
	/**表单方式提交*/
	onGoodsExportExcelByForm: function(type) {
		var me = this;

		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel', {
			items: [{
					xtype: 'filefield',
					name: 'file'
				},
				{
					xtype: 'textfield',
					name: 'reportType',
					value: "1"
				},
				{
					xtype: 'textfield',
					name: 'idList'
				},
				{
					xtype: 'textfield',
					name: 'where'
				},
				{
					xtype: 'textfield',
					name: 'isHeader',
					value: "0"
				}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({
			idList: '',
			where: ''
		});
		if(type == 1) { //类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];

			if(len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}

			for(var i = 0; i < len; i++) {
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({
					idList: ids.join(",")
				});
			}
		} else if(type == 2) { //类型为条件导出
			var where = me.getWhere();
			if(where.length == 0) {
				where = '1=1';
			} else {
				where = "(" + where + ")";
			}
			me.UpdateForm.getForm().setValues({
				where: where
			});
		}
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + me.reportExcelUrl;

		me.UpdateForm.getForm().submit({
			url: url,
			//waitMsg:JShell.Server.SAVE_TEXT,
			success: function(form, action) {
				me.hideMask();
				var fileName = action.result.ResultDataValue;
				var downloadUrl = JShell.System.Path.ROOT + me.downLoadExcelUrl;
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=客户端货品数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
			},
			failure: function(form, action) {
				me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if(len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}

		// 换算 率    要大于1
		for(var i = 0; i < len; i++) {
			var CName = changedRecords[i].get('ReaGoods_CName');
			var ReaGoodsNo = changedRecords[i].get('ReaGoods_ReaGoodsNo');
			var UnitName = changedRecords[i].get('ReaGoods_UnitName');
			var UnitMemo = changedRecords[i].get('ReaGoods_UnitMemo');

			if(!CName || !ReaGoodsNo || !UnitName || !UnitMemo) {
				JShell.Msg.alert("数据行货品名称、货品编码、单位、规格不能为空！");
				return;
			}
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.updateInfo(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateInfo: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var entity = {
			Id: record.get('ReaGoods_Id'),
			BarCodeMgr: record.get('ReaGoods_BarCodeMgr'),
			IsRegister: record.get('ReaGoods_IsRegister') ? 1 : 0,
			IsPrintBarCode: record.get('ReaGoods_IsPrintBarCode') ? 1 : 0,
			CName: record.get('ReaGoods_CName'),
			SName: record.get('ReaGoods_SName'),
			EName: record.get('ReaGoods_EName'),
			ReaGoodsNo: record.get('ReaGoods_ReaGoodsNo'),
			GoodsNo: record.get('ReaGoods_GoodsNo'),
			UnitName: record.get('ReaGoods_UnitName'),
			UnitMemo: record.get('ReaGoods_UnitMemo'),
			Visible: record.get('ReaGoods_Visible') ? 1 : 0,
			IsNeedPerformanceTest: record.get('ReaGoods_IsNeedPerformanceTest') ? 1 : 0,
			ProdEara: record.get('ReaGoods_ProdEara'),
			Price: record.get('ReaGoods_Price'),
			StorageType: record.get('ReaGoods_StorageType'),
			GoodsClass: record.get('ReaGoods_GoodsClass'),
			GoodsClassType: record.get('ReaGoods_GoodsClassType'),
			SuitableType: record.get('ReaGoods_SuitableType'),
			ReaCompanyName: record.get('ReaGoods_ReaCompanyName'),
			TestCount: record.get('ReaGoods_TestCount'),
			RegistNo: record.get('ReaGoods_RegistNo'),
			RegistDate: JShell.Date.toServerDate(record.get('ReaGoods_RegistDate')),
			RegistNoInvalidDate: JShell.Date.toServerDate(record.get('ReaGoods_RegistNoInvalidDate')),
			DispOrder: record.get('ReaGoods_DispOrder'),
			ProdOrgName: record.get('ReaGoods_ProdOrgName'),
			DeptName: record.get('ReaGoods_DeptName'),
			MatchCode: record.get('ReaGoods_MatchCode')			
		}
		if(record.get('ReaGoods_MonthlyUsage')) {
			entity.MonthlyUsage = record.get('ReaGoods_MonthlyUsage');
		}
		var params = Ext.JSON.encode({
			entity: entity,
			fields: 'Id,BarCodeMgr,IsRegister,IsPrintBarCode,CName,SName,EName,ReaGoodsNo,GoodsNo,UnitName,UnitMemo,Visible,' +
				'ProdEara,Price,StorageType,GoodsClass,DispOrder,' +
				'GoodsClassType,TestCount,ReaCompanyName,ProdOrgName,' +
				'SuitableType,RegistNo,RegistDate,RegistNoInvalidDate,' +
				'DeptName,MonthlyUsage,IsNeedPerformanceTest,MatchCode'
		});
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onGridSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 20 ? v.substring(0, 20) : v);
		if(value.length > 20) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	/**新增注册证表单*/
	showRegisterForm: function(GoodsID, GoodsCName) {
		var me = this,
			config = {
				resizable: false,
				/**货品ID*/
				GoodsID: GoodsID,
				/**货品名称*/
				GoodsCName: GoodsCName,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onGridSearch();
					},
					beforeclose: function(panel, eOpts) {
						var edit = panel.getPlugin('NewsGridEditing');
						edit.cancelEdit();
					},
					load: function() {
						var edit = me.getPlugin('NewsGridEditing');
						edit.cancelEdit();
					}
				}
			};
		config.formtype = 'add';
		JShell.Win.open('Shell.class.rea.client.goods2.register.Grid', config).show();
	},
	createCellEditListeners: function() {
		var me = this,
			columns = me.columns;
		for(var i in columns) {
			var column = columns[i];
			if(column.editor) {
				column.editor.listeners = column.editor.listeners || {};
				column.editor.listeners.specialkey = function(textField, e) {
					me.doSpecialkey(textField, e);
				};
				column.hasEditor = true
			} else if(column.columns) {
				for(var j in column.columns) {
					var c = column.columns[j];
					if(c.editor) {
						c.editor.listeners = c.editor.listeners || {};
						c.editor.listeners.specialkey = function(textField, e) {
							me.doSpecialkey(textField, e);
						};
						c.hasEditor = true
					}
				}
			}
		}
	},
	doSpecialkey: function(textField, e) {
		var me = this;
		textField.focus();
		var info = me.getKeyInfo(e);
		if(info) {
			me.isSpecialkey = true;
			e.stopEvent();
			me.changeRowIdxAndCelIdx(textField, info.type);
			textField.blur();
		}
	},
	getKeyInfo: function(e) {
		var me = this,
			arr = me.specialkeyArr,
			key = e.getKey();
		var info = null;
		for(var i in arr) {
			var ctrlKey = arr[i].ctrlKey ? true : false;
			var shiftKey = arr[i].shiftKey ? true : false;
			if(arr[i].key == key && ctrlKey == e.ctrlKey && shiftKey == e.shiftKey) {
				if(arr[i].replaceKey) {
					e.keyCode = arr[i].replaceKey;
					info = null;
					break
				} else {
					info = arr[i];
					break
				}
			}
		}
		return info
	},
	changeRowIdxAndCelIdx: function(field, type) {
		var me = this,
			context = field.ownerCt.editingPlugin.context,
			rowIdx = context.rowIdx,
			colIdx = context.colIdx;
		me.rowIdx = rowIdx;
		me.colIdx = colIdx;
		if(type == 'up') {
			me.rowIdx = me.getNextRowIndex(rowIdx, false, false);
		} else if(type == 'down') {
			me.rowIdx = me.getNextRowIndex(rowIdx, true, true, colIdx);
		} else if(type == 'left') {
			me.colIdx = me.getNextColIndex(colIdx, false);
		} else if(type == 'right') {
			me.colIdx = me.getNextColIndex(colIdx, true);
		}

	},
	getNextRowIndex: function(rowIdx, isDown, isRowCycle, colIdx) {
		var me = this,
			count = me.store.getCount(),
			nRowIdx = rowIdx;
		if(count == 0) return null;
		isDown ? nRowIdx++ : nRowIdx--;
		if(isRowCycle) {
			nRowIdx = nRowIdx % count;
			nRowIdx = nRowIdx < 0 ? nRowIdx + count : nRowIdx
		} else {
			if(nRowIdx == count) {
				nRowIdx = count - 1
			}
			if(nRowIdx == -1) {
				nRowIdx = 0
			}
		}

		if(count > 0 && me.rowIdx == count - 1) {
			me.colIdx = me.getNextColIndex(colIdx, true)
		}
		return nRowIdx
	},
	getNextColIndex: function(colIdx, isRight) {
		var me = this,
			columns = me.columns,
			length = columns.length,
			nColIdx = colIdx;
		if(isRight) {
			for(var i = colIdx + 1; i < length; i++) {
				if(columns[i].hasEditor) {
					return i
				}
			}
			if(me.isColCycle) {
				for(var i = 0; i < colIdx; i++) {
					if(columns[i].hasEditor) {
						return i
					}
				}
			}
		} else {
			for(var i = colIdx - 1; i >= 0; i--) {
				if(columns[i].hasEditor) {
					return i
				}
			}
			if(me.isColCycle) {
				for(var i = length - 1; i > colIdx; i--) {
					if(columns[i].hasEditor) {
						return i
					}
				}
			}
		}
		return nColIdx
	},

	/**设置相同码*/
	showConfigForm: function(ReaGoodsNo) {
		var me = this,
			config = {
				resizable: false,
				//				idList:idList,
				ReaGoodsNo: ReaGoodsNo,
				listeners: {
					save: function(p) {
						p.close();
						me.onGridSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.goods2.MinUnitGrid', config).show();
	},
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var Id = buttonsToolbar.getComponent('ReaGoods_Prod_Id');
		var CName = buttonsToolbar.getComponent('ReaGoods_ProdOrgName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		me.onGridSearch();
		p.close();
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
			me.IsCanCel = false;
		}, 100);
	},
	/**数据完整性检查*/
	showMinUnit: function() {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p) {
						p.close();
						me.onSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.goods2.DataCheckGrid', config).show();
	},
	/**手工同步操作*/
	manualSync: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + me.manualSynchUrl;
		JShell.Server.get(url,function(data) {
			if(data.success) {
				JShell.Msg.alert('手工同步成功！', null, 1500);
			}else {
				JShell.Msg.error(data.msg);
			}
		});

	}
});