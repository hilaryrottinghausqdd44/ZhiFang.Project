/**
 * 订货明细列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.EditDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderDtlGrid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsCenOrderDtl',

	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',

	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**是否多选行*/
	checkOne: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddAfter', 'onDelAfter', 'onEditAfter');
		if(!me.checkOne) me.setCheckboxModel();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			listeners: {
				edit: function(editor, e, eOpts) {
					me.fireEvent('onEditAfter', me);
				}
			}
		});
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var id = record.get("BmsCenOrderDtl_Id");
					if(id == "" || id == "-1") {
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						return 'button-del hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">删除操作确认</div>',
						msg: '请确认是否删除?',
						closable: true
					}, function(but, text) {
						if(but != "ok") return;
						var rec = grid.getStore().getAt(rowIndex);
						me.deleteOne(rec);
					});
				}
			}]
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		if(me.OTYPE != "upload") {
			items.push('-', {
				iconCls: 'button-add',
				itemId: "btnAdd",
				disabled: true,
				text: '新增明细',
				tooltip: '新增货品明细',
				handler: function() {
					me.onAddDtClick();
				}
			});
			items.push('-', {
				fieldLabel: '',
				emptyText: '模板选择',
				labelWidth: 0,
				width: 180,
				name: 'cboGoodstemplate',
				itemId: 'cboGoodstemplate',
				xtype: 'uxCheckTrigger',
				classConfig: {
					width: 385,
					height: 460,
					checkOne: true,
					TemplateType: "ReaOrderDtl",
					defaultWhere: " reachoosegoodstemplate.SName='ReaOrderDtl'"
				},
				className: 'Shell.class.rea.client.goodstemplate.CheckGrid',
				listeners: {
					beforetriggerclick: function(p) {
						if(!p.classConfig ||!p.classConfig.OrgID) me.setGoodstemplateClassConfig();
						if(!p.classConfig ||!p.classConfig.OrgID) {
							JShell.Msg.warning('获取供货方信息为空,请选择供货方后再操作!');
							return false;
						}
					},
					check: function(p, record) {
						p.close();
						me.onAddRecords(record);
					}
				}
			}, {
				xtype: 'button',
				iconCls: 'button-save',
				itemId: "btnSaveTemplate",
				text: '保存模板',
				tooltip: '保存模板',
				handler: function() {
					me.onSaveTemplate();
				}
			});
		}
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名',
			fields: ['bmscenorderdtl.GoodsName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		if(me.ReaCompID) {
			me.showDtGridCheck();
		} else {
			JShell.Msg.alert("请选择供应商后再操作!", null, 1000);
		}
	},
	/**@description 显示新增货品明细*/
	showDtGridCheck: function() {
		var me = this;
		var goodIdStr = "";
		me.store.each(function(record) {
			if(record.get("BmsCenOrderDtl_OrderGoodsID"))
				goodIdStr += record.get("BmsCenOrderDtl_OrderGoodsID") + ",";
		});
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		var Sysdate = JcallShell.System.Date.getDate();
		var curTime = JcallShell.Date.toString(Sysdate);

		var defaultWhere = " reagoodsorglink.Visible=1 and (reagoodsorglink.BeginTime<='" + curTime + "' and (reagoodsorglink.EndTime is null or reagoodsorglink.EndTime>='" + curTime + "')) and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompID;
		if(goodIdStr) defaultWhere = defaultWhere + "and reagoodsorglink.Id not in (" + goodIdStr + ")";

		var config = {
			resizable: true,
			width: 860,
			height: 540,
			ReaCompID: me.ReaCompID,
			Status: me.Status,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onDtCheck(p, records);
				}
			}
		};
		if(me.PK) {
			config.formtype = 'edit';
			config.PK = me.PK;
		} else {
			config.formtype = 'add';
		}
		var win = JShell.Win.open('Shell.class.rea.client.order.GoodsOrgLinkCheck', config);
		win.show();
	},
	/**@description 新增明细选择后处理方法*/
	onDtCheck: function(p, records) {
		var me = this;
		me.onDtAddOfNew(p, records);
	},
	/**修改时新增明细处理*/
	onDtAddOfNew: function(p, records) {
		var me = this,
			len = records.length,
			arr = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = {
				BmsCenOrderDtl_Id: "",
				BmsCenOrderDtl_ProdGoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_ProdGoodsNo'),
				BmsCenOrderDtl_BiddingNo: rec.get('ReaGoodsOrgLink_BiddingNo'),
				BmsCenOrderDtl_ReaGoodsID: rec.get('ReaGoodsOrgLink_ReaGoods_Id'),
				BmsCenOrderDtl_OrderGoodsID: rec.get('ReaGoodsOrgLink_Id'),
				BmsCenOrderDtl_ReaGoodsName: rec.get('ReaGoodsOrgLink_ReaGoods_CName'),
				BmsCenOrderDtl_GoodsUnit: rec.get('ReaGoodsOrgLink_ReaGoods_UnitName'),
				BmsCenOrderDtl_UnitMemo: rec.get('ReaGoodsOrgLink_ReaGoods_UnitMemo')
			};
			var GoodsQty = rec.get('GoodsQty');
			if(GoodsQty) GoodsQty = parseInt(GoodsQty);
			var Price = rec.get('ReaGoodsOrgLink_Price');
			if(Price) Price = parseFloat(Price);
			var SumTotal = Price * GoodsQty;
			SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;
			obj["BmsCenOrderDtl_GoodsQty"] = GoodsQty;
			obj["BmsCenOrderDtl_Price"] = Price;
			obj["BmsCenOrderDtl_SumTotal"] = SumTotal;
			arr.push(obj);
		}
		if(arr) me.store.add(arr);
		me.fireEvent('onAddAfter', me);
		p.close();
	},
	/**@description 获取明细的保存提交数据*/
	getDtSaveParams: function() {
		var me = this;
		var result = {
			TotalPrice: 0,
			dtAddList: [],
			dtEditList: []
		};
		if(me.store.getCount() <= 0) return result;
		//订单总价
		var TotalPrice = 0;
		me.store.each(function(record) {
			var id = record.get("BmsCenOrderDtl_Id");
			if(!id) id = "-1";
			var entity = {
				"Id": id,
				"ProdGoodsNo": record.get("BmsCenOrderDtl_ProdGoodsNo"),
				"BiddingNo": record.get("BmsCenOrderDtl_BiddingNo"),
				"ReaGoodsID": record.get("BmsCenOrderDtl_ReaGoodsID"),
				"OrderGoodsID": record.get("BmsCenOrderDtl_OrderGoodsID"),
				"ReaGoodsName": record.get("BmsCenOrderDtl_ReaGoodsName"),
				"GoodsUnit": record.get("BmsCenOrderDtl_GoodsUnit"),
				"UnitMemo": record.get("BmsCenOrderDtl_UnitMemo")
			};
			var GoodsQty = record.get('BmsCenOrderDtl_GoodsQty');
			if(GoodsQty) GoodsQty = parseInt(GoodsQty);
			var Price = record.get('BmsCenOrderDtl_Price');
			if(Price) Price = parseFloat(Price);
			var SumTotal = Price * GoodsQty;
			TotalPrice += parseFloat(SumTotal);
			SumTotal = SumTotal ? SumTotal.toFixed(2) : 0;

			entity["GoodsQty"] = GoodsQty;
			entity["Price"] = Price;
			entity["SumTotal"] = SumTotal;

			if(id && id != "-1") {
				result.dtEditList.push(entity);
			} else {
				result.dtAddList.push(entity);
			}
		});
		TotalPrice = TotalPrice.toFixed(2);
		result.TotalPrice = TotalPrice;
		return result;
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function(result) {
		var me = this;
		if(!result) result = me.getDtSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
			me.Status = "0";
		}
		var entity = {
			"Id": me.PK,
			"Status": me.Status,
			TotalPrice: result.TotalPrice
		};
		var fields = "Id,Status,TotalPrice";
		var params = {
			entity: entity,
			"fields": fields,
			"dtAddList": result.dtAddList,
			"dtEditList": result.dtEditList
		};
		return params;
	},
	/**@description编辑新增明细的保存处理*/
	onSaveOfUpdate: function(result, callback) {
		var me = this;
		var params = me.getSaveParams(result);
		if(!params) return false;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
			me.hideMask(); //隐藏遮罩层
			callback(data);
		});
	},
	deleteOne: function(record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if(!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if(showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, i, id);
		}
	},
	/**删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},
	/**订单选择改变或部门选择改变后更新模板选择的配置项*/
	setGoodstemplateClassConfig: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
			if(cbo) {
				cbo.setValue("");
				cbo.changeClassConfig({
					"OrgID": me.ReaCompID,
					"defaultWhere": " reachoosegoodstemplate.SName='ReaOrderDtl' and reachoosegoodstemplate.OrgID=" + me.ReaCompID
				});
			}
		}
	},
	/**@description 模板保存*/
	onSaveTemplate: function() {
		var me = this;
		if(me.store.getCount() <= 0) {
			JShell.Msg.error('当前选择的货品明细为空!');
			return;
		}
		if(!me.ReaCompID) {
			JShell.Msg.error('获取供应商信息为空!');
			return;
		}
		var tempArr = [];
		me.store.each(function(record) {
			var data = {};
			data = Ext.apply(data, record.data);
			data["BmsCenOrderDtl_Id"] = "";
			tempArr.push(data);
		});
		var config = {
			resizable: true,
			formtype: "add",
			OrgID: me.ReaCompID,
			OrgName: me.ReaCompCName,
			TemplateType: "ReaOrderDtl",
			ContextJson: JcallShell.JSON.encode(tempArr),
			listeners: {
				save: function(p, id) {
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodstemplate.ApplyForm', config);
		win.show();
	},
	/**明细模板选择后,申请明细列表的货品信息处理*/
	onAddRecords: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
		if(record) cbo.setValue(record.get("ReaChooseGoodsTemplate_CName"));
		else cbo.setValue("");

		if(!record) return;

		var contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
		if(contextJson) contextJson = JcallShell.JSON.decode(contextJson);
		if(!contextJson) return;

		Ext.Array.each(contextJson, function(data) {
			var goodId = data["BmsCenOrderDtl_ReaGoodsID"];
			var indexOf = -1;
			if(goodId) indexOf = me.store.findExact("BmsCenOrderDtl_ReaGoodsID", goodId);
			//模板的货品不存在明细列表里
			if(goodId && indexOf == -1) {
				data["BmsCenOrderDtl_Id"] = "";
				me.store.add(data);
			}
		});
	}
});