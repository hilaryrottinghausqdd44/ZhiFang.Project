/**
 * 机构货品列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	
	title: '',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaGoodsOrgLink',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaGoodsOrgLink',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLinkByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**单个新增时的表单默认值*/
	addFormDefault: null,
	/**应用类型:货品:goods;订货/供货:cenorg*/
	appType: "cenorg",
	/**是否为供货商货品维护:是:true;否:false*/
	hasSupply: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_DataAddTime',
		direction: 'DESC'
	}, {
		property: 'ReaGoodsOrgLink_CenOrgGoodsNo',
		direction: 'ASC'
	}],

	initComponent: function() {
		var me = this;
		me.regStr = new RegExp('"', "g");
		Ext.QuickTips.init();
		Ext.override(Ext.ToolTip, {
			maxWidth: 860
		});
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品编码/供货商货品编码/拼音字头/货品名称',
			fields: ['reagoodsorglink.ReaGoods.ReaGoodsNo', 'reagoodsorglink.CenOrgGoodsNo',
				'reagoodsorglink.ReaGoods.PinYinZiTou', 'reagoodsorglink.ReaGoods.CName'
			]
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaGoodsOrgLink_Id',
			text: '主键ID',
			hidden: true,
			//hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_Id',
			text: '货品主键ID',
			hidden: true
			//hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_Id',
			text: '机构主键ID',
			hidden: true
			//hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_CName',
			text: '机构名称',
			hidden: true,
			sortable: false,
			width: 260,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_OrgNo',
			text: '机构编码',
			hidden: true,
			sortable: false,
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '货品名称',
			//flex: 1,
			width: 200,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsOrgLink_BarCodeType");
				if (!barCodeMgr) barCodeMgr = "";
				if (barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if (value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_IsRegister',
			text: '有注册证',
			width: 60,
			align: 'center',
			type: 'bool',
			hidden: true,
			isBool: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ProdGoodsNo',
			text: '厂商货品编码',
			width: 95,
			hidden: true,
			defaultRenderer: true
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
			xtype: 'checkcolumn',
			dataIndex: 'ReaGoodsOrgLink_Visible',
			text: '<b style="color:blue;">启用</b>',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			dataIndex: 'ReaGoodsOrgLink_Price',
			text: '<b style="color:blue;">价格</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				minValue: 0,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaGoodsOrgLink_Price', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_DispOrder',
			text: '<b style="color:blue;">优先级</b>',
			width: 65,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
						//me.comSetReadOnly(com, e, "1");
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaGoodsOrgLink_DispOrder', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			text: '<b style="color:blue;">供货商货品编码</b>',
			width: 120,
			editor: {
				allowBlank: true,
				emptyText: '必填项',
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '货品平台编码',
			hidden: true,
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 120,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_BarCodeMgr',
			text: '货品条码类型',
			sortable: false,
			menuDisabled: true,
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_IsPrintBarCode',
			text: '货品是否打印条码',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			//hideable: false,
			renderer: function(value, meta, record) {
				var v = "";
				if (value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if (value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_BarCodeType',
			text: '<b style="color:blue;">条码类型</b>',
			width: 60,
			hidden: (me.hasSupply == true ? true : false),
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
				if (value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_IsPrintBarCode',
			text: '<b style="color:blue;">是否打印条码</b>',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			hidden: (me.hasSupply == true ? true : false),
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			text: '有效开始',
			dataIndex: 'ReaGoodsOrgLink_BeginTime',
			isDate: true,
			width: 80,
			hideable: true,
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d'
			},
			defaultRenderer: true
		}, {
			text: '有效截止',
			dataIndex: 'ReaGoodsOrgLink_EndTime',
			//isDate: true,
			width: 80,
			hideable: true,
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d'
			},
			renderer: function(curValue, meta, record, rowIndex, colIndex, s, view) {
				var bgColor = "";
				var value = curValue;
				if (value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);

					if (days < 0) {
						bgColor = "red";
						value = "已失效";
					} else if (days >= 0 && days <= 30) {
						bgColor = "#e97f36";
						value = "30天内到期";
					} else if (days > 30) {
						bgColor = "#568f36";
					}

				} else {
					if (record.get("ReaGoodsOrgLink_BeginTime")) {
						bgColor = "#568f36";
						value = "长期有效";
					} else {
						bgColor = "#e97f36";
						value = "无有效期";
					}
				}
				if (curValue) curValue = Ext.util.Format.date(curValue, 'Y-m-d');
				meta.tdAttr = 'data-qtip="' + curValue + '"';
				if (bgColor) meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitMemo',
			text: '货品规格',
			hidden: true,
			width: 80,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_Memo',
			text: '备注',
			hidden: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}];
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
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
		}, {
			dataIndex: 'ErrorInfo',
			text: '错误信息',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnCheck',
			text: '货品选择',
			tooltip: '新增货品选择',
			handler: function() {
				me.onCheckGoodsClick();
			}
		});
		items.push('-', 'edit', 'save', 'del');
		items.push('-', {
			xtype: 'checkboxfield',
			boxLabel: '显示禁用',
			checked: false,
			inputValue: 0,
			name: 'cboISVisible',
			itemId: 'cboISVisible',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					if (newValue != oldValue)
						me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		items.push('-',{
			xtype: 'button',
			iconCls: 'button-config',
			text: '同步货品',
			tooltip: '同步货品',
			handler: function() {
				me.onReaGoodsOutClick();
			}
		});
		return items;
	},
	onReaGoodsOutClick: function() {
		var me = this;
		var config = {
			/**当前选择的机构Id*/
			ReaCenOrgId: me.ReaCenOrgId,
			listeners: {
				saveSyn: function(p) {
					p.close();
					me.onSearch();
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.goodsorglink.ReaGoodsSynFrom', config).show();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if (!data || !data.list) return data;
		var list = data.list;
		for (var i = 0; i < list.length; i++) {
			list[i].ReaGoodsOrgLink_ReaGoods_IsRegister = list[i].ReaGoodsOrgLink_ReaGoods_IsRegister == '1' ? true : false;
			list[i].ReaGoodsOrgLink_ReaGoods_IsPrintBarCode = list[i].ReaGoodsOrgLink_ReaGoods_IsPrintBarCode == '1' ? true :
				false;

			//兼容原已维护的货品关系
			if (!list[i].ReaGoodsOrgLink_BarCodeType) {
				list[i].ReaGoodsOrgLink_BarCodeType = list[i].ReaGoodsOrgLink_ReaGoods_BarCodeMgr;
			}
			var isPrintBarCode = list[i].ReaGoodsOrgLink_IsPrintBarCode;
			if (isPrintBarCode == "" || isPrintBarCode == undefined) {
				list[i].ReaGoodsOrgLink_IsPrintBarCode = list[i].ReaGoodsOrgLink_ReaGoods_IsPrintBarCode;
			}
		}

		data.list = list;
		return data;
	},
	showQtipValue: function(meta, record) {
		var me = this;
		return meta;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;
		//需要先加验证
		var isSave = true,
			info = "";
		for (var i = 0; i < len; i++) {
			var visible = records[i].get('ReaGoodsOrgLink_Visible');
			if (visible == false || visible == "false") visible = 0;
			if (visible == "1" || visible == "true" || visible == true) visible = 1;

			var price = records[i].get('ReaGoodsOrgLink_Price');
			if (!price) price = 0;

			if (visible == 1 && price < 0) {
				isSave = false;
				info = "货品名称为:" + records[i].get('ReaGoodsOrgLink_ReaGoods_CName') + "的单价小于0!";
				break;
			}
		}
		if (isSave == false) {
			JShell.Msg.alert(info, null, 2000);
			return;
		}

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			me.updateOneInfo(i, records[i]);
		}
	},
	/**修改单个*/
	updateOneInfo: function(index, record) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
		var id = record.get(me.PKField);
		var visible = record.get('ReaGoodsOrgLink_Visible');
		if (visible == false || visible == "false") visible = 0;
		if (visible == "1" || visible == "true" || visible == true) visible = 1;

		var price = record.get('ReaGoodsOrgLink_Price');
		if (!price) price = 0;

		var dispOrder = record.get('ReaGoodsOrgLink_DispOrder');
		if (!dispOrder) dispOrder = 0;

		var isPrintBarCode = record.get('ReaGoodsOrgLink_IsPrintBarCode');
		if (isPrintBarCode == false || isPrintBarCode == "false") isPrintBarCode = 0;
		if (isPrintBarCode == "1" || isPrintBarCode == "true" || isPrintBarCode == true) isPrintBarCode = 1;

		var barCodeType = record.get('ReaGoodsOrgLink_BarCodeType');

		var entity = {
			'Id': id,
			'Visible': visible,
			'IsPrintBarCode': isPrintBarCode,
			'BarCodeType': barCodeType,
			'Price': parseFloat(price),
			'DispOrder': dispOrder,
			'CenOrgGoodsNo': record.get('ReaGoodsOrgLink_CenOrgGoodsNo'),
			'ProdGoodsNo': record.get('ReaGoodsOrgLink_ProdGoodsNo'),
			'BiddingNo': record.get('ReaGoodsOrgLink_BiddingNo'),
			'CenOrg': {
				"Id": record.get('ReaGoodsOrgLink_CenOrg_Id')
			},
			'ReaGoods': {
				"Id": record.get('ReaGoodsOrgLink_ReaGoods_Id')
			}
		};

		var BeginTime = record.get('ReaGoodsOrgLink_BeginTime');
		var EndTime = record.get('ReaGoodsOrgLink_EndTime');
		if (BeginTime) entity.BeginTime = JShell.Date.toServerDate(BeginTime);
		if (EndTime) entity.EndTime = JShell.Date.toServerDate(EndTime);

		var params = JShell.JSON.encode({
			entity: entity,
			fields: 'Id,Visible,Price,CenOrgGoodsNo,BiddingNo,DispOrder,BeginTime,EndTime,BarCodeType,IsPrintBarCode' //
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
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
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.showForm(null);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.showForm(records[0].get(me.PKField));
	},
	/**显示表单*/
	showForm: function(id) {
		var me = this;
		var config = {
			resizable: true,
			appType: me.appType,
			addFormDefault: me.addFormDefault,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.goodsorglink.basic.Form', config).show();
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

		var id = record.get("ReaGoodsOrgLink_Id");
		var config = {
			resizable: true,
			width: 580,
			height: 520,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.ReagentSys.Client', //类域
			className: 'ReaGoodsOrgLinkStatus', //类名
			title: '供货商货品信息操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='ReaGoodsOrgLink'"
		};
		var win = JShell.Win.open('Shell.class.rea.client.scoperation.SCOperation', config);
		win.show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ISVisible = buttonsToolbar.getComponent('cboISVisible'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if (ISVisible) {
			var disabled = ISVisible.getValue();
			//不显示禁用
			if (disabled == false || disabled == 0) {
				disabled = 1;
				params.push('reagoodsorglink.Visible=' + disabled);
			}
		}
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	},
	onCheckGoodsClick: function() {
		var me = this;

		var defaultWhere = " reagoods.Visible=1 ";
		//		var arrIdStr = [],
		//			idStr = "";
		//		me.store.each(function(record) {
		//			var goodId = record.get("ReaGoodsOrgLink_ReaGoods_Id");
		//			if(goodId && Ext.Array.contains(goodId) == false) arrIdStr.push(goodId);
		//		});
		//		if(arrIdStr.length > 0) idStr = arrIdStr.join(",");
		//		if(idStr) defaultWhere = defaultWhere + " and reagoods.Id not in (" + idStr + ")";

		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			/**是否单选*/
			checkOne: false,
			/**当前选择的机构Id*/
			ReaCenOrgId: me.ReaCenOrgId,
			canEdit: true,
			leftDefaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records);
				}
			}
		};
		//需要过滤当前供应商已维护并启用的货品信息 Shell.class.rea.client.goodsorglink.basic.GoodsCheck
		JShell.Win.open('Shell.class.rea.client.goodsorglink.basic.choose.App', config).show();
	},
	getGoodsIdStr: function() {
		var me = this;
		var idStr = "";
		me.store.each(function(record) {
			idStr += (record.get(me.PKField) + ",");
		});
		if (idStr) idStr = idStr.substring(0, idStr.length - 1);
		return idStr;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	onAccept: function(p, records) {
		var me = this;
		if (!records) {
			p.close();
			return;
		}
		var len = records.length;
		if (len == 0) {
			p.close();
			return;
		}
		if (!me.ReaCenOrgId) {
			JShell.Msg.error("机构信息为空!");
			p.close();
			return;
		}
		//需要先加验证,ProdGoodsNo不能为空
		var isSave = true,
			info = "";
		for (var i = 0; i < records.length; i++) {
			var price = records[i].get('ReaGoods_Price');
			if (!price) price = 0;
			price = parseFloat(price);

			if (price < 0) {
				isSave = false;
				info = "货品名称为:" + records[i].get('ReaGoods_CName') + "的单价小于0!";
				break;
			}
		}
		if (isSave == false) {
			JShell.Msg.error(info);
			return;
		}

		p.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for (var i = 0; i < len; i++) {
			me.addSaveOne(i, records[i], p);
		}
	},
	addSaveOne: function(index, record, p) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUrl);
		var price = record.get("ReaGoods_Price");
		if (!price) price = 0;
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";

		var isPrintBarCode = record.get('ReaGoods_IsPrintBarCode');
		if (isPrintBarCode == false || isPrintBarCode == "false") isPrintBarCode = 0;
		if (isPrintBarCode == "1" || isPrintBarCode == "true" || isPrintBarCode == true) isPrintBarCode = 1;

		var barCodeType = record.get('ReaGoods_BarCodeMgr');

		var entity = {
			'Id': -1,
			'Visible': 1,
			'IsPrintBarCode': isPrintBarCode,
			'BarCodeType': barCodeType,
			"Price": parseFloat(price),
			"CenOrgGoodsNo": record.get("ReaGoods_CenOrgGoodsNo"), //
			"ProdGoodsNo": record.get("ReaGoods_ProdGoodsNo"),
			'ReaGoods': {
				"Id": record.get("ReaGoods_Id"),
				"DataTimeStamp": strDataTimeStamp.split(',')
			},
			'CenOrg': {
				"Id": me.ReaCenOrgId,
				"DataTimeStamp": strDataTimeStamp.split(',')
			}
		};
		var Sysdate = JcallShell.System.Date.getDate();
		var BeginTime = JcallShell.Date.toString(Sysdate, true);
		if (BeginTime) entity.BeginTime = JShell.Date.toServerDate(BeginTime);

		var params = JShell.JSON.encode({
			"entity": entity
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					p.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) {
						p.close();
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	}
});
