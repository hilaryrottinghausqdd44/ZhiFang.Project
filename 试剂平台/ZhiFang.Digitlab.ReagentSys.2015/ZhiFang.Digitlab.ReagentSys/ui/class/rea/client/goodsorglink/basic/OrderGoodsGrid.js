/**
 * 机构产品列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
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
	/**应用类型:产品:goods;订货/供货:cenorg*/
	appType: "goods",
	/**单个新增时的表单默认值*/
	addFormDefault: null,

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
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
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
			text: '产品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_Id',
			text: '机构主键ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},{
			xtype: 'checkcolumn',
			dataIndex: 'ReaGoodsOrgLink_Visible',
			text: '<b style="color:blue;">使用</b>',
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
		},{
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
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},{
			dataIndex: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			text: '<b style="color:blue;">对方产品编号</b>',
			width: 120,
			editor: {
				allowBlank: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_BiddingNo',
			text: '<b style="color:blue;">招标号</b>',
			width: 140,
			editor: {
				allowBlank: true
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			text: '有效开始',
			dataIndex: 'ReaGoodsOrgLink_BeginTime',
			isDate: true,
			width: 80,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效截止',
			dataIndex: 'ReaGoodsOrgLink_EndTime',
			//isDate: true,
			width: 80,
			hideable: true,
			renderer: function(curValue, meta, record, rowIndex, colIndex, s, view) {
				var bgColor = "";
				var value=curValue;
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);

					if(days < 0) {
						bgColor = "red";
						value = "已失效";
					} else if(days >= 0 && days <= 30) {
						bgColor = "#e97f36";
						value = "30天内到期";
					} else if(days > 30) {
						bgColor = "#568f36";
					}

				} else {
					if(record.get("ReaGoodsOrgLink_BeginTime")) {
						bgColor = "#568f36";
						value = "长期有效";
					}else{
						bgColor = "#e97f36";
						value = "无有效期";
					}
				}
				if(curValue)curValue = Ext.util.Format.date(curValue, 'Y-m-d');
				meta.tdAttr = 'data-qtip="' + curValue + '"';
				if(bgColor) meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitMemo',
			text: '产品规格',
			hidden: true,
			width: 40,
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

		return columns;
	},
	showQtipValue: function(meta, record) {
		var me = this;
		var tempStr = "";

		var qtipMemoValue = record.get("ReaGoodsOrgLink_Memo");
		qtipMemoValue = qtipMemoValue.replace(me.regStr, "'");

		var pStr = "<p border=0 style='vertical-align:top;font-size:13px;'>";

		tempStr = pStr + "<b>机构名称:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_CenOrg_CName") + "</p>";
		tempStr += pStr + "<b>机构编码:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_CenOrg_OrgNo") + "</p>";

		tempStr += pStr + "<b>产品名称:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_ReaGoods_CName") + "</p>";
		tempStr += pStr + "<b>产品单位:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_ReaGoods_UnitName") + "</p>";
		tempStr += pStr + "<b>产品规格:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_ReaGoods_UnitMemo") + "</p>";
		tempStr += pStr + "<b>货品编码:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_ReaGoods_GoodsNo") + "</p>";
		tempStr += pStr + "<b>有效开始:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_BeginTime") + "</p>";
		tempStr += pStr + "<b>有效截止:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_EndTime") + "</p>";

		var qtipValue = tempStr;
		qtipValue += pStr + "<b>价&nbsp;&nbsp;&nbsp;&nbsp;格:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_Price") + "</p>";
		qtipValue += pStr + "<b>对方产品编号:&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_CenOrgGoodsNo") + "</p>";
		qtipValue += pStr + "<b>招标号:&nbsp;&nbsp;&nbsp;&nbsp;</b>" + record.get("ReaGoodsOrgLink_BiddingNo") + "</p>";
		qtipValue += pStr + "<b>备&nbsp;&nbsp;&nbsp;&nbsp;注:&nbsp;&nbsp;&nbsp;&nbsp;</b>" + qtipMemoValue + "</p>";

		if(qtipValue) {
			meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		}
		return meta;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.updateOneInfo(i, records[i]);
		}
	},
	/**修改单个*/
	updateOneInfo: function(index, record) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
		var id = record.get(me.PKField);
		var Visible = record.get('ReaGoodsOrgLink_Visible');
		if(Visible == false || Visible == "false") Visible = 0;
		if(Visible == "1" || Visible == "true" || Visible == true) Visible = 1;

		var Price = record.get('ReaGoodsOrgLink_Price');
		if(!Price)Price=0;
		var CenOrgGoodsNo = record.get('ReaGoodsOrgLink_CenOrgGoodsNo');
		var DispOrder= record.get('ReaGoodsOrgLink_DispOrder');
		if(!DispOrder)DispOrder=0;
		var entity = {
			'Id': id,
			'Visible': Visible,
			'Price': Price,
			'DispOrder': DispOrder,
			'CenOrgGoodsNo': CenOrgGoodsNo,
			'BiddingNo': record.get('ReaGoodsOrgLink_BiddingNo')
		};
		var params = JShell.JSON.encode({
			entity: entity,
			fields: 'Id,Visible,Price,CenOrgGoodsNo,BiddingNo,DispOrder'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				//var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
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
		if(records.length != 1) {
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
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.goodsorglink.admin.basic.Form', config).show();
	}
});