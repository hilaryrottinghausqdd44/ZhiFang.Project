/**
 * @description 用户订单(给供应商查看客户端用户已上传的订单信息)
 * @author longfc
 * @version 2018-03-06
 */
Ext.define('Shell.class.rea.client.order.comp.OrderDtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	hasDel: true,
	/**新增明细或删除明细按钮的启用状态*/
	buttonsDisabled: true,

	ReaCompCName: null,
	/**录入:entry/审核:check*/
	OTYPE: "comp",
	/**是否多选行*/
	checkOne: false,
	/**隐藏货品平台编码列*/
	hiddenGoodsNo:true,
	/**用户UI配置Key*/
	userUIKey: 'order.comp.OrderDtlGrid',
	/**用户UI配置Name*/
	userUIName: "用户订单明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function(p) {
				me.enableControl(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		if(!me.checkOne) me.setCheckboxModel();
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		//只能点击复选框才能选中
		me.selModel = new Ext.selection.CheckboxModel({
			checkOnly: true
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsName',
			text: '货品名称',
			sortable: true,
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCenOrderDtl_BarCodeType");
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
		},{
			dataIndex: 'ReaBmsCenOrderDtl_ProdGoodsNo',
			text: '厂商货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 90,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenOrderDtl_GoodsNo',
			text: '平台货品编码',
			width: 90,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenOrderDtl_ArrivalTime',
			text: '<b style="color:blue;">要求到货时间</b>',
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			width: 85,
			sortable: false,
			isDate: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenOrderDtl_ReqGoodsQty',
			text: '申请数',
			width: 80,
			type: 'float',
			hidden:true
		},{
			dataIndex: 'ReaBmsCenOrderDtl_GoodsQty',
			text: '订货数量',
			width: 80,
			type: 'float'
		},{
			dataIndex: 'ReaBmsCenOrderDtl_SuppliedQty',
			text: '已供数量',
			width: 80
		},{
			dataIndex: 'ReaBmsCenOrderDtl_UnSupplyQty',
			text: '未供数量',
			width: 80
		},{
			dataIndex: 'ReaBmsCenOrderDtl_Price',
			sortable: true,
			text: '单价',
			width: 80,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_SumTotal',
			sortable: true,
			text: '总价',
			width: 80,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_GoodsUnit',
			sortable: true,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_ReaGoodsID',
			sortable: false,
			text: '货品Id',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDtl_CompGoodsLinkID',
			sortable: false,
			text: '供货商货品机构关系ID',
			hidden: true,
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenOrderDtl_BarCodeType',
			sortable: false,
			text: '货品条码类型',
			hidden: true,
			renderer: function(value, meta, record) {
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
			dataIndex: 'ReaBmsCenOrderDtl_IsPrintBarCode',
			sortable: false,
			text: '是否打印条码',
			hidden: true,
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
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items=me.createFullscreenItems();
		items.push('-','refresh');
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名/货品平台编码',
			fields: ['reabmscenorderdtl.ReaGoodsName','reabmscenorderdtl.GoodsNo']
		};
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnCheck",
			text: '确认结单',
			tooltip: '确认结单',
			handler: function() {
				var records = me.ownerCt.ownerCt.OrderGrid.getSelectionModel().getSelection();
				var SupplyStatus=records[0].get("ReaBmsCenOrderDoc_SupplyStatus");
				var Status=records[0].get("ReaBmsCenOrderDoc_Status");
				if(Status==10){
					if(SupplyStatus!=4){
						me.onEndClick();
					}else{
						JShell.Msg.error('已经全部供货，不需要确认结单！');
					}
				}else{
					JShell.Msg.error('单据状态不是订单转供货，无法进行确认结单操作！');
				}
			}
		});
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		
		return items;
	},
	
	onEndClick:function(){
		var me=this;
		
		var id=me.PK;
		
		var entity = {
			"Id": id,
			"SupplyStatus": 3
		};
		var url = JShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocByField";
		var params = {
			"entity": entity,
			"fields": "Id,SupplyStatus"
		};
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			if (data.success) {
				me.ownerCt.ownerCt.OrderGrid.onSearch();
			} else {
				JShell.Msg.error('确认结单失败！' + data.msg);
			}
		});
		
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.PK) return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.setButtonsDisabled(me.buttonsDisabled);
		}, null, 2000);
	},
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsCenOrderDtl_Price');
		var GoodsQty = record.get('ReaBmsCenOrderDtl_GoodsQty');

		var SumTotal = Price * GoodsQty;
		var TotalPrice = SumTotal ? SumTotal : 0;
		record.set('ReaBmsCenOrderDtl_SumTotal', TotalPrice);
		record.commit();
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
		me.setBtnDisabled("btnAdd", disabled);
		me.setBtnDisabled("btnDel", disabled);
		me.setBtnDisabled("btnSave", disabled);
		me.setBtnDisabled("cboGoodstemplate", disabled);
	}
});