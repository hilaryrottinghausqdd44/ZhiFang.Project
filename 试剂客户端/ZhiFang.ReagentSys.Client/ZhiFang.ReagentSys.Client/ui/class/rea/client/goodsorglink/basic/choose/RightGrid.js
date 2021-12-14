/**
 * 机构货品选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.choose.RightGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '当前选择货品列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**获取数据服务路径/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true*/
	selectUrl: '',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_DispOrder',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**是否调用服务从后台获取数据*/
	remoteLoad: false,
	/**查询框信息*/
	searchInfo: null,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 500,
	//closable:true,
	/**用户UI配置Key*/
	userUIKey: 'goodsorglink.basic.choose.RightGrid',
	/**用户UI配置Name*/
	userUIName: "机构货品待选货品列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onRemoveClick([record]);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onAccept', 'onRemove', 'onRefresh');
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
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			//flex:1,
			width: 210,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoods_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '货品条码类型',
			sortable: false,
			menuDisabled: true,
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoods_IsPrintBarCode',
			text: '货品是否打印条码',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			hideable: false,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if(value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CenOrgGoodsNo', 
			text: '<b style="color:blue;">供货商货品编码</b>',
			width: 110,
			editor: {
				allowBlank: true,
				emptyText: '必填项',
			},
			defaultRenderer: true
		},  {
			dataIndex: 'ReaGoods_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				emptyText: '必填项',
				minValue: 0,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaGoods_Price', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		},{
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品平台编号',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ReaCompanyName',
			text: '所属供应商',
			width: 95,
			defaultRenderer: true
		},  {
			dataIndex: 'ReaGoods_ShortCode',
			text: '同系列码',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 85,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];//'->'
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '选择移除',
			hidden: true,
			tooltip: '移除列表选择的行',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				me.onRemoveClick(records);
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '全部移除',
			tooltip: '移除列表选择的行',
			handler: function() {
				var records = [];
				me.store.each(function(rec) {
					records.push(rec);
				});
				me.onRemoveClick(records);
			}
		});
		items.push('-', {
			iconCls: 'button-check',
			text: '确定选择',
			tooltip: '确定当前选择并退出',
			handler: function() {
				me.onAcceptClick();
			}
		});

		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//是否调用服务从后台获取数据
		if(!me.remoteLoad) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	},
	onRemoveClick: function(records) {
		var me = this;
		if(!records) records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onRemove', me, records);
	},
	onRefreshClick: function() {
		var me = this;
		me.fireEvent('onRefresh', me);
	}
});