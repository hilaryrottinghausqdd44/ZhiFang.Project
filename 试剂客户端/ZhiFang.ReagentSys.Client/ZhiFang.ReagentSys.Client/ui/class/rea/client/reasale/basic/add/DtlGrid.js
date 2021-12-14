/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.basic.add.DtlGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.DtlGrid',
	title: '供货明细列表',

	/**是否多选行*/
	checkOne: false,
	/**后台排序*/
	remoteSort: false,
	/**新增/编辑/查看*/
	formtype: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsCenSaleDtl_Price")
							me.onPriceOrGoodsQtyChanged(record);
						else if(modified == "ReaBmsCenSaleDtl_GoodsQty")
							me.onPriceOrGoodsQtyChanged(record);
					}
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddAfter', 'onDelAfter', 'onEditAfter');

		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			listeners: {
				edit: function(editor, e, eOpts) {
					me.fireEvent('onEditAfter', me);
				}
			}
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		//me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(0, 0, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var id = record.get("ReaBmsCenSaleDtl_Id");
					if(id == "" || id == "-1" || me.canEdit == true) {
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
		}, {
			xtype: 'actioncolumn',
			text: '复制',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			hidden: me.hiddenCopy,
			items: [{
				getClass: function(v, meta, record) {

					var id = record.get("ReaBmsCenSaleDtl_Id");
					if(id == "" || id == "-1" || me.canEdit == true) {
						meta.tdAttr = 'data-qtip="<b>拆分复制当前货品</b>"';
						return 'button-add hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var dataOne = {};
					dataOne = Ext.apply(dataOne, rec.data);
					me.onCopyRecord(dataOne);
				}
			}]
		});
		return columns;
	},
	/**@description 复制拆分*/
	onCopyRecord: function(dataOne) {
		var me = this;
		var record = {};
		record = Ext.apply(record, dataOne);
		record.ReaBmsCenSaleDtl_Id = -1;
		record.ReaBmsCenSaleDtl_GoodsQty = 0;
		record.ReaBmsCenSaleDtl_LotNo = "";
		record.ReaBmsCenSaleDtl_SumTotal = 0;
		record.ReaBmsCenSaleDtl_InvalidDate = "";
		record.ReaBmsCenSaleDtl_ProdDate = "";
		record.ReaBmsCenSaleDtl_LotSerial = "";
		record.ReaBmsCenSaleDtl_SysLotSerial = "";
		me.store.add(record);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.createFullscreenItems();
		items.push('-', 'refresh');
		if(me.formtype != "show") {
			items.push('-', {
				iconCls: 'button-add',
				itemId: "btnAdd",
				//disabled: true,
				text: '新增',
				tooltip: '新增货品',
				handler: function() {
					me.onAddDtClick();
				}
			});
		}
		items.push('-', {
			fieldLabel: '',
			labelWidth: 0,
			width: 80,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DocBarCodeType',
			emptyText: '条码类型',
			data: JShell.REA.StatusList.Status[me.BarCodeTypeKey].List,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});

		items.push('-', {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '条码打印',
			tooltip: '条码打印',
			handler: function() {
				me.onPrintClick();
			}
		});
		return items;
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		JShell.Msg.overwrite('onAddDtClick');
	},
	/**@description 显示新增货品明细(单列表)*/
	showDtGridCheck2: function(reaCenOrgId) {
		var me = this;
		var linkIdStr = "";
		me.store.each(function(record) {
			if(record.get("ReaBmsCenSaleDtl_CompGoodsLinkID"))
				linkIdStr += record.get("ReaBmsCenSaleDtl_CompGoodsLinkID") + ",";
		});
		linkIdStr = linkIdStr.substring(0, linkIdStr.length - 1);
		var defaultWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id=" + reaCenOrgId;
		if(linkIdStr) defaultWhere = defaultWhere + "and reagoodsorglink.Id not in (" + linkIdStr + ")";

		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		if(maxWidth > 1180) maxWidth = 1180;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAddDtl(p, records);
				}
			}
		};
		if(me.PK) {
			config.formtype = 'edit';
			config.PK = me.PK;
		} else {
			config.formtype = 'add';
		}
		var win = JShell.Win.open('Shell.class.rea.client.reasale.basic.GoodsOrgLinkCheck', config);
		win.show();
	},
	/**@description 显示新增货品明细(双列表)*/
	showDtGridCheck: function(reaCenOrgId) {
		var me = this;
		var linkIdStr = "";
		me.store.each(function(record) {
			if(record.get("ReaBmsCenSaleDtl_CompGoodsLinkID"))
				linkIdStr += record.get("ReaBmsCenSaleDtl_CompGoodsLinkID") + ",";
		});
		linkIdStr = linkIdStr.substring(0, linkIdStr.length - 1);
		var leftDefaultWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id=" + reaCenOrgId;
		if(linkIdStr) leftDefaultWhere = leftDefaultWhere + "and reagoodsorglink.Id not in (" + linkIdStr + ")";

		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.95;
		if(maxWidth > 1180) maxWidth = 1180;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			leftDefaultWhere: leftDefaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onAddDtl(p, records);
				}
			}
		};
		if(me.PK) {
			config.formtype = 'edit';
			config.PK = me.PK;
		} else {
			config.formtype = 'add';
		}
		var win = JShell.Win.open('Shell.class.rea.client.reasale.choose.App', config);
		win.show();
	},
	/**新增明细处理*/
	onAddDtl: function(p, records) {
		var me = this,
			len = records.length,
			arr = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var RegistNoInvalidDate = rec.get('ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate');
			var IsPrintBarCode = rec.get('ReaGoodsOrgLink_IsPrintBarCode');
			if(IsPrintBarCode == true || IsPrintBarCode == "1" || IsPrintBarCode == 1) {
				IsPrintBarCode = true;
			} else {
				IsPrintBarCode = false;
			}
			var obj = {
				ReaBmsCenSaleDtl_Id: "",
				ReaBmsCenSaleDtl_SaleDocID: me.PK,
				ReaBmsCenSaleDtl_ReaCompanyName: rec.get('ReaGoodsOrgLink_CenOrg_CName'),
				ReaBmsCenSaleDtl_ReaCompID: rec.get('ReaGoodsOrgLink_CenOrg_Id'),
				ReaBmsCenSaleDtl_ReaServerCompCode: rec.get('ReaGoodsOrgLink_CenOrg_PlatformOrgNo'),

				ReaBmsCenSaleDtl_BarCodeType: rec.get('ReaGoodsOrgLink_BarCodeType'),
				ReaBmsCenSaleDtl_IsPrintBarCode: IsPrintBarCode,
				ReaBmsCenSaleDtl_ReaGoodsName: rec.get('ReaGoodsOrgLink_ReaGoods_CName'),
				ReaBmsCenSaleDtl_GoodsUnit: rec.get('ReaGoodsOrgLink_ReaGoods_UnitName'),
				ReaBmsCenSaleDtl_UnitMemo: rec.get('ReaGoodsOrgLink_ReaGoods_UnitMemo'),

				ReaBmsCenSaleDtl_LotNo: "",
				ReaBmsCenSaleDtl_InvalidDate: "",
				ReaBmsCenSaleDtl_Memo: "",
				ReaBmsCenSaleDtl_TempRange: rec.get('ReaGoodsOrgLink_ReaGoods_TempRange'),
				ReaBmsCenSaleDtl_ProdDate: "",
				ReaBmsCenSaleDtl_ProdOrgName: rec.get('ReaGoodsOrgLink_ReaGoods_ProdOrgName'), //厂家名称

				ReaBmsCenSaleDtl_BiddingNo: rec.get('ReaGoodsOrgLink_BiddingNo'), //招标号
				ReaBmsCenSaleDtl_RegisterNo: rec.get('ReaGoodsOrgLink_ReaGoods_RegistNo'), //注册证编号
				ReaBmsCenSaleDtl_RegisterInvalidDate: RegistNoInvalidDate, //注册证有效期
				ReaBmsCenSaleDtl_TaxRate: 0,
				ReaBmsCenSaleDtl_CompGoodsLinkID: rec.get('ReaGoodsOrgLink_Id'),
				ReaBmsCenSaleDtl_ReaGoodsID: rec.get('ReaGoodsOrgLink_ReaGoods_Id'),
				ReaBmsCenSaleDtl_ApproveDocNo: rec.get('ReaGoodsOrgLink_ReaGoods_ApproveDocNo'), //批准文号				
				ReaBmsCenSaleDtl_StorageType: rec.get('ReaGoodsOrgLink_ReaGoods_StorageType'), //储藏条件

				ReaBmsCenSaleDtl_ReaGoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_ReaGoodsNo'),
				ReaBmsCenSaleDtl_ProdGoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_ProdGoodsNo'),
				ReaBmsCenSaleDtl_CenOrgGoodsNo: rec.get('ReaGoodsOrgLink_CenOrgGoodsNo'),
				ReaBmsCenSaleDtl_GoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_GoodsNo')
			};

			var GoodsQty = rec.get('GoodsQty');
			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			var Price = rec.get('ReaGoodsOrgLink_Price');
			if(Price) Price = parseFloat(Price);
			var SumTotal = Price * GoodsQty;
			SumTotal = SumTotal ? SumTotal : 0;

			var GoodsSort = rec.get('ReaGoodsOrgLink_ReaGoods_GoodsSort');
			if(GoodsSort) GoodsSort = parseInt(GoodsSort);
			GoodsSort = GoodsSort ? GoodsSort : 0;
			obj["ReaBmsCenSaleDtl_GoodsSort"] = GoodsSort;

			obj["ReaBmsCenSaleDtl_GoodsQty"] = GoodsQty;
			obj["ReaBmsCenSaleDtl_Price"] = Price;
			obj["ReaBmsCenSaleDtl_SumTotal"] = SumTotal;

			arr.push(obj);
		}
		if(arr) me.store.add(arr);
		me.fireEvent('onAddAfter', me);
		p.close();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			list[i].ReaBmsCenSaleDtl_IsPrintBarCode = list[i].ReaBmsCenSaleDtl_IsPrintBarCode == '1' ? true : false;
		}
		data.list = list;
		return data;
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
			me.delOneById(record, 0, id);
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
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('ReaBmsCenSaleDtl_Price');
		var GoodsQty = record.get('ReaBmsCenSaleDtl_GoodsQty');
		if(!Price) Price = 0;
		if(!GoodsQty) GoodsQty = 0;
		var SumTotal = parseFloat(Price) * parseFloat(GoodsQty);
		record.set('ReaBmsCenSaleDtl_SumTotal', SumTotal);
	},
	/**@description 获取明细的保存提交数据*/
	getSaveDtl: function(result) {
		var me = this;
		if(!result) result = me.getSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
		}
		var entity = {
			"Id": me.PK,
			TotalPrice: result.TotalPrice
		};
		var fields = "Id,Status,TotalPrice";
		var params = {
			entity: entity,
			"dtAddList": result.dtAddList
		};
		if(me.formtype == "edit") {
			params.fields = fields;
			params.dtEditList = result.dtEditList;
			params.dtlFields = me.getUpdateFields();
		}

		return params;
	},
	getUpdateFields: function() {
		var me = this;
		var fields =
			"Id,Status,StatusName,DataUpdateTime,ProdID,ProdOrgName,GoodsUnit,UnitMemo,StorageType,TempRange,GoodsQty,Price,SumTotal,TaxRate,LotNo,ProdDate,InvalidDate,BiddingNo,ApproveDocNo,RegisterNo,RegisterInvalidDate,ReaGoodsNo,ProdGoodsNo,CenOrgGoodsNo,GoodsNo,IsPrintBarCode,BarCodeType"; //,Memo
		return fields;
	},
	/**@description 确认提交及审核通过的验证*/
	validToSave: function() {
		var me = this;
		var isValid = true;
		var info = "";
		if(me.store.getCount() <= 0) {
			info = "待验收货品明细为空!";
			isValid = false;
			return;
		}
		var LotNo = "",
			ReaGoodsName = "",
			InvalidDate = "",
			ProdDate = "",
			GoodsQty = 0,
			Price = 0;

		me.store.each(function(record) {
			LotNo = record.get("ReaBmsCenSaleDtl_LotNo");
			GoodsQty = record.get("ReaBmsCenSaleDtl_GoodsQty");
			Price = record.get("ReaBmsCenSaleDtl_Price");
			ReaGoodsName = record.get("ReaBmsCenSaleDtl_ReaGoodsName");
			InvalidDate = record.get("ReaBmsCenSaleDtl_InvalidDate");
			ProdDate = record.get("ReaBmsCenSaleDtl_ProdDate");

			if(GoodsQty) GoodsQty = parseFloat(GoodsQty);
			else GoodsQty = 0;

			if(Price) Price = parseFloat(Price);
			else Price = 0;

			if(!GoodsQty || GoodsQty <= 0) {
				info = "货品为" + ReaGoodsName + ",数量不能为空或小于等于0!";
				return false;
			}
			if(!LotNo) {
				info = "货品为" + ReaGoodsName + ",货品批号为空!";
				return false;
			}
			if(!InvalidDate) {
				info = "货品为" + ReaGoodsName + ",有效期至不能为空!";
				return false;
			}
		});
		if(info) {
			isValid = false;
			JShell.Msg.error(info);
		}
		return isValid;
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var result = {
			TotalPrice: 0,
			dtAddList: [],
			dtEditList: []
		};
		if(me.store.getCount() <= 0) {
			return result;
		}
		//总价
		var TotalPrice = 0;
		if(!me.PK) me.PK = -1;

		me.store.each(function(rec) {
			var id = rec.get("ReaBmsCenSaleDtl_Id");
			if(!id) id = "-1";
			var IsPrintBarCode = rec.get('ReaBmsCenSaleDtl_IsPrintBarCode');
			if(IsPrintBarCode == true || IsPrintBarCode == "1" || IsPrintBarCode == 1) {
				IsPrintBarCode = 1;
			} else {
				IsPrintBarCode = 0;
			}
			var entity = {
				Id: id,
				SaleDocID: me.PK,
				ReaCompanyName: rec.get('ReaBmsCenSaleDtl_ReaCompanyName'),
				ReaCompID: rec.get('ReaBmsCenSaleDtl_ReaCompID'),
				ReaServerCompCode: rec.get('ReaBmsCenSaleDtl_ReaServerCompCode'),

				BarCodeType: rec.get('ReaBmsCenSaleDtl_BarCodeType'),
				IsPrintBarCode: IsPrintBarCode,
				ReaGoodsName: rec.get('ReaBmsCenSaleDtl_ReaGoodsName'),
				GoodsUnit: rec.get('ReaBmsCenSaleDtl_GoodsUnit'),
				UnitMemo: rec.get('ReaBmsCenSaleDtl_UnitMemo'),

				LotNo: rec.get('ReaBmsCenSaleDtl_LotNo'),
				TempRange: rec.get('ReaBmsCenSaleDtl_TempRange'),
				ProdOrgName: rec.get('ReaBmsCenSaleDtl_ProdOrgName'), //厂家名称
				BiddingNo: rec.get('ReaBmsCenSaleDtl_BiddingNo'), //招标号
				RegisterNo: rec.get('ReaBmsCenSaleDtl_RegisterNo'), //注册证编号

				CompGoodsLinkID: rec.get('ReaBmsCenSaleDtl_CompGoodsLinkID'),
				ReaGoodsID: rec.get('ReaBmsCenSaleDtl_ReaGoodsID'),

				ApproveDocNo: rec.get('ReaBmsCenSaleDtl_ApproveDocNo'), //批准文号				
				StorageType: rec.get('ReaBmsCenSaleDtl_StorageType'), //储藏条件

				LotSerial: rec.get('ReaBmsCenSaleDtl_LotSerial'),
				SysLotSerial: rec.get('ReaBmsCenSaleDtl_SysLotSerial'),
				Memo: rec.get('ReaBmsCenSaleDtl_Memo'),
				ReaGoodsNo: rec.get('ReaBmsCenSaleDtl_ReaGoodsNo'),
				ProdGoodsNo: rec.get('ReaBmsCenSaleDtl_ProdGoodsNo'),
				CenOrgGoodsNo: rec.get('ReaBmsCenSaleDtl_CenOrgGoodsNo'),
				GoodsNo: rec.get('ReaBmsCenSaleDtl_GoodsNo')
			};

			var InvalidDate = rec.get('ReaBmsCenSaleDtl_InvalidDate');
			var ProdDate = rec.get('ReaBmsCenSaleDtl_ProdDate');
			var RegisterInvalidDate = rec.get('ReaBmsCenSaleDtl_RegisterInvalidDate');
			if(ProdDate) {
				entity.ProdDate = JShell.Date.toServerDate(ProdDate);
			}
			if(InvalidDate) {
				entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
			}
			if(RegisterInvalidDate) {
				entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);
			}
			var TaxRate = rec.get('ReaBmsCenSaleDtl_TaxRate');
			if(TaxRate) {
				entity.TaxRate = parseFloat(TaxRate);
			}
			var GoodsQty = rec.get('ReaBmsCenSaleDtl_GoodsQty');
			if(GoodsQty) {
				GoodsQty = parseFloat(GoodsQty);
			} else {
				GoodsQty = 0;
			}
			var Price = rec.get('ReaBmsCenSaleDtl_Price');
			if(Price) {
				Price = parseFloat(Price);
			} else {
				Price = 0;
			}

			var SumTotal = Price * GoodsQty;
			TotalPrice += parseFloat(SumTotal);
			SumTotal = SumTotal ? parseFloat(SumTotal) : 0;

			entity["GoodsQty"] = GoodsQty;
			entity["Price"] = Price;
			entity["SumTotal"] = SumTotal;

			var GoodsSort = rec.get('ReaBmsCenSaleDtl_GoodsSort');
			if(GoodsSort) GoodsSort = parseInt(GoodsSort);
			GoodsSort = GoodsSort ? GoodsSort : 0;
			entity["GoodsSort"] = GoodsSort;

			if(id && id != "-1") {
				result.dtEditList.push(entity);
			} else {
				result.dtAddList.push(entity);
			}
		});
		result.TotalPrice = TotalPrice;
		return result;
	},
	onPrintClick: function() {
		var me = this;
		if(!me.PK) {
			JShell.Msg.error("请选择供货单后再操作!");
			return;
		}
		var records = me.getSelectionModel().getSelection();
		if(records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.onShowPrintPanel(records);
	},
	onShowPrintPanel: function(records) {
		var me = this;
		var idStr = [];
		for(var i = 0; i < records.length; i++) {
			var id = records[i].get(me.PKField);
			if(id) {
				idStr.push(id);
			}
		}
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;

		var config = {
			resizable: true,
			PK: me.PK,
			IDStr: idStr.join(","),
			//SUB_WIN_NO: '1',
			width: maxWidth,
			height: height
		};
		var win = JShell.Win.open('Shell.class.rea.client.printbarcode.saledoc.Grid', config);
		win.show();
	},
	nodata: function() {
		var me = this;
		me.store.removeAll();
		var error = me.errorFormat.replace(/{msg}/, "供货明细数据为空!");
		me.getView().update(error);
		me.getComponent('buttonsToolbar').getComponent('DocBarCodeType').setValue("");
	}
});