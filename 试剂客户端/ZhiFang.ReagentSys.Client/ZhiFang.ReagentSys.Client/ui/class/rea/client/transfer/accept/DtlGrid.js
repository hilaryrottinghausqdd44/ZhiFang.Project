/**
 * 目标库
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.accept.DtlGrid', {
	extend: 'Shell.class.rea.client.transfer.basic.DtlGrid',

	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlByHQL?isPlanish=true',
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDtl',
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',

	defaultLoad: false,
	/**用户UI配置Key*/
	userUIKey: 'transfer.accept.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库确认明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				if (record.dirty) {
					var changedObj = record.getChanges();
					for (var modified in changedObj) {
						if (modified == "ReaBmsTransferDtl_GoodsQty")
							me.onGoodsQtyChanged(record);
					}
				}
			}
		});
		me.onLoadByDocID();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeSumTotal');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
					/**
					 * 2020-07-24
					 * 移库申请确认，允许为0的可以手工删除
					 */
					/* var Id = record.get(me.PKField);
					if(!Id) {
						return 'button-del hand';
					} else {
						return '';
					} */
				},
				handler: function(grid, rowIndex, colIndex) {
					/**
					 * 2020-07-24
					 * 移库申请确认，允许为0的可以手工删除
					 */
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">删除申请操作</div>',
						msg: '确认要删除当前选择的移库申请货品？',
						closable: false,
						multiline: false //多行输入框
					}, function(but, text) {
						if (but != "ok") return;

						me.delErrorCount = 0;
						me.delCount = 0;
						me.delLength = 1;
						var rec = me.getStore().getAt(rowIndex);
						me.deleteRow(0, rec);
						me.getSelectionModel().selectAll();
					});
				}
			}]
		}, {
			dataIndex: 'ReaBmsTransferDtl_SStorageName',
			text: '源库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SStorageID',
			text: '源库房Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceName',
			text: '源货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceID',
			text: '源货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageName',
			text: '目的库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageID',
			text: '目的库房Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceName',
			text: '目的货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceID',
			text: '目的货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdGoodsNo',
			text: '厂商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsTransferDtl_BarCodeType");
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
			dataIndex: 'ReaBmsTransferDtl_SumDtlGoodsQty',
			text: '已申请数',
			sortable: false,
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumCurrentQty',
			text: '现有库存量',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReqGoodsQty',
			text: '申请数',
			sortable: false,
			width: 65,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsQty',
			text: '<b style="color:blue;">移库数</b>',
			sortable: false,
			width: 65,
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
					change: function(com, newValue, oldValue, eOpts) {}
				}
			},
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsUnit',
			width: 65,
			text: '单位',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Price',
			text: '单价',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'DPlaceID',
			text: '目的货架id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyName',
			text: '供应商',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompanyID',
			text: 'ReaCompanyID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsID',
			text: '货品ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Tab',
			text: '合并标签',
			sortable: false,
			hidden: true,
			editor: {},
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotQRCode',
			text: '二维条码号',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			editor: {},
			hidden: true,
			width: 100,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SQtyDtlID',
			text: '库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_BarCodeType',
			text: '条码类型',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteLotNo',
			text: '原批号',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_Id',
			text: '主键ID',
			hidden: true,
			isKey: true,
			width: 200
		}, {
			dataIndex: 'ReaBmsTransferDtl_DefaulteGoodsQty2',
			text: '原库存量222',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnitTab',
			text: '原始单位及信息',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});
		return columns;
	},
	/**
	 * @description 确认提交时，自动处理移库数为0的申请信息
	 */
	onDeleteGoodsQty0: function(callback) {
		var me = this;
		var records = me.store.data.items,
			len = records.length;

		var delList = [];
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			//移库数
			var goodsQty = records[i].get('ReaBmsTransferDtl_GoodsQty');
			if (!goodsQty) goodsQty = 0;
			if (!goodsQty || goodsQty == '0' || Number(goodsQty) < 0) {
				var id = rec.get(me.PKField);
				if (!id) {
					me.delCount++;
					me.store.remove(rec);
					me.fireEvent('delclick', rec.get('ReaBmsTransferDtl_Tab'));
					me.fireEvent('changeSumTotal');
				} else {
					delList.push(rec);
				}
			}
		}

		if (delList.length > 0) {
			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = delList.length;
			for (var i = 0; i < delList.length; i++) {
				var rec = delList[i];
				var id2 = rec.get(me.PKField);
				me.delOneById(i, id2, rec, callback);
			}
		} else {
			var result = {
				isExec: true,
				msg: ""
			};
			if (callback) callback(result);
		}
	},
	/**
	 * @description 删除选择行
	 * */
	deleteRow: function(i, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		if (!id) {
			me.delCount++;
			me.store.remove(rec);
			me.fireEvent('delclick', rec.get('ReaBmsTransferDtl_Tab'));
			me.fireEvent('changeSumTotal');
			me.getSelectionModel().selectAll();
		} else {
			me.delOneById(i, id, rec, null);
		}
	},
	/**删除一条数据*/
	delOneById: function(index, id, record, callback) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if (data.success) {
					me.delCount++;
					me.fireEvent('delclick', record.get('ReaBmsTransferDtl_Tab'));
					me.store.remove(record);
					me.fireEvent('changeSumTotal');
				} else {
					me.delErrorCount++;
					var delInfo = {
						"record": record,
						'ErrorInfo': data.msg
					};
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					record.commit();
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					if (callback) {
						var result = {
							isExec: true,
							msg: ""
						};
						if (me.delErrorCount > 0) {
							result = {
								isExec: false,
								msg: data.msg
							};
						}
						callback(result);
					}
				}
			});
		}, 100 * index);
	},
	/**更新主单货品总额*/
	onUpateTotalClick: function(id, TotalPrice) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = {
			"Id": id,
			"TotalPrice": TotalPrice
		};
		var entity = {
			entity: params,
			fields: "Id,TotalPrice"
		};
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, Ext.JSON.encode(entity), function(data) {
			me.hideMask();
			if (!data.success) {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**
	 * @description 移库数值改变后处理
	 * */
	onGoodsQtyChanged: function(record) {
		var me = this;
		var goodsQty = record.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		goodsQty = parseFloat(goodsQty);
		me.setSumTotal(goodsQty, record);
		me.setReqGoodsQty(goodsQty, record);
		record.commit();
		me.fireEvent('changeSumTotal');
	},
	/**数据改变时刷新现有库存量*/
	onAddListeners: function() {
		var me = this;
		me.store.on({
			datachanged: function() {
				me.getGoodsQty();
			}
		});
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.onSumGoodsQty();
		me.getSelectionModel().selectAll();
	},
	/**
	 * @description 移库确认时新增的移库货品
	 * 在移库数改变后,新增 的移库货品的申请数等于当前移库数
	 * */
	setReqGoodsQty: function(goodsQty, rec) {
		var me = this;
		var id = rec.get("ReaBmsTransferDtl_Id");
		if (!id || id == "" || id == "-1") {
			rec.set("ReaBmsTransferDtl_ReqGoodsQty", goodsQty);
		}
	},
	/***
	 * @description 设置已申请数值
	 */
	onSumGoodsQty: function() {
		var me = this;
		me.getGoodsQty();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for (var i = 0; i < data.list.length; i++) {
			//移库数默认值处理(盒条码的移库数取0,批条或无条码的移库货品默认取申请数)
			var barCodeMgr = data.list[i].ReaBmsTransferDtl_BarCodeType;
			if (barCodeMgr == "1") {
				data.list[i].ReaBmsTransferDtl_GoodsQty = 0;
			} else {
				data.list[i].ReaBmsTransferDtl_GoodsQty = data.list[i].ReaBmsTransferDtl_ReqGoodsQty;
			}
			//供应商+货品+批号+库房+货架
			var reaCompanyID = data.list[i].ReaBmsTransferDtl_ReaCompanyID;
			var goodsID = data.list[i].ReaBmsTransferDtl_GoodsID;
			var lotNo = data.list[i].ReaBmsTransferDtl_LotNo;
			var storageID = data.list[i].ReaBmsTransferDtl_SStorageID;
			var placeID = data.list[i].ReaBmsTransferDtl_SPlaceID;
			var mergeStr = "" + reaCompanyID + goodsID + lotNo + storageID + placeID;
			data.list[i].ReaBmsTransferDtl_Tab = mergeStr;
		}
		return data;
	},
	//根据主单查数据
	onLoadByDocID: function() {
		var me = this;
		if (!me.PK) return;
		me.defaultWhere = 'reabmstransferdtl.TransferDocID=' + me.PK;
		me.onSearch();
	},
	getAddList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtAddList = [];
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			if (rec && !rec.get('ReaBmsTransferDtl_Id')) {
				var entity = me.getEntity(rec);
				dtAddList.push(entity);
			}
		}
		return dtAddList;
	},
	onSetQty: function(record) {
		var me = this;
		//移库数量+1
		var GoodsQty = record.get('ReaBmsTransferDtl_GoodsQty');
		if (!GoodsQty) GoodsQty = 0;
		GoodsQty = Number(GoodsQty) + 1;
		var GoodsCName = record.get('ReaBmsTransferDtl_GoodsCName');
		//现有库存量
		var DefaulteGoodsQty = record.get('ReaBmsTransferDtl_SumCurrentQty');
		if (!DefaulteGoodsQty) DefaulteGoodsQty = 0;
		DefaulteGoodsQty = Number(DefaulteGoodsQty);
		if (DefaulteGoodsQty < GoodsQty) {
			JShell.Msg.alert('产品名称:【' + GoodsCName + '】的申请数量不能不能大于现有库存量', null, 2000);
			return;
		}
		var Price = record.get('ReaBmsTransferDtl_Price');
		if (!Price) Price = 0;
		var SumTotal = Number(Price) * GoodsQty;
		record.set('ReaBmsTransferDtl_GoodsQty', GoodsQty);
		record.set('ReaBmsTransferDtl_SumTotal', SumTotal);
	},
	/**移库新增行*/
	createRowObj: function(rec, UnitArr) {
		var me = this;
		var JObjectBarCode = rec.get('ReaBmsQtyDtl_JObjectBarCode');
		if (JObjectBarCode) var JObjectBarCode = Ext.JSON.decode(JObjectBarCode);
		var obj = {};
		obj = me.changeJObjectBarCode(JObjectBarCode, obj, rec);
		obj.ReaBmsTransferDtl_GoodsQty = 1;
		if (!obj.ReaBmsTransferDtl_Price) obj.ReaBmsTransferDtl_Price = 0;
		var SumTotal = Number(obj.ReaBmsTransferDtl_Price) * obj.ReaBmsTransferDtl_GoodsQty;
		obj.ReaBmsTransferDtl_SumTotal = SumTotal;
		//临时变量
		var UnitTabobj = me.createUnit(obj);
		obj = me.createObj(obj, rec, UnitArr, UnitTabobj, JObjectBarCode);
		return obj;
	},
	/**找到行新增和删除，对外公开*/
	onAddOne: function(rec, barcode) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		//根据供应商+货品+批号+库房+货架判断
		var record = me.doesIsExistRec(rec);
		if (record) {
			me.checkBarCode(record, rec, barcode);
		} else if (!record) {
			var obj = me.createRowObj(rec);
			var id = obj["ReaBmsTransferDtl_Id"];
			if (!id || id == "" || id == "-1") {
				obj["ReaBmsTransferDtl_ReqGoodsQty"] = obj["ReaBmsTransferDtl_GoodsQty"];
			}
			me.store.insert(me.store.getCount(), obj);
			me.fireEvent('changeSumTotal');
		}
		me.getSelectionModel().selectAll();
	},
	/**
	 * @description 获取移库货品当前的库存数
	 * */
	setSumQty: function(rec, list) {
		var me = this;
		me.callParent(arguments);
		//当前库存
		var sumCurrentQty = list.SumCurrentQty;
		if (!sumCurrentQty) sumCurrentQty = 0;
		sumCurrentQty = parseFloat(sumCurrentQty);
		//批条码货品的默认移库数不能大于当前库存数
		me.setGoodsQtyOfSumCurrentQty(rec, sumCurrentQty);
		rec.commit();
	},
	/**
	 * @description 批条码货品的默认移库数不能大于当前库存数
	 * */
	setGoodsQtyOfSumCurrentQty: function(rec, sumCurrentQty) {
		var me = this;
		var barCodeMgr = "" + rec.get("ReaBmsTransferDtl_BarCodeType");
		if (barCodeMgr == "0") {
			var goodsQty = rec.get("ReaBmsTransferDtl_GoodsQty");
			if (!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			if (goodsQty > sumCurrentQty) goodsQty = sumCurrentQty;
			rec.set('ReaBmsTransferDtl_GoodsQty', goodsQty);
			var price = rec.get("ReaBmsTransferDtl_Price");
			if (!price) price = 0;
			price = parseFloat(price);
			var sumTotal = price * goodsQty;
			rec.set('ReaBmsTransferDtl_SumTotal', sumTotal);
		}
	},
	/**
	 * 保存校验
	 * 申请数量，不能为0
	 */
	onSaveCheck: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExec = true;
		var msg = '';
		isExec = true, msg = '';
		if (!len||len == 0) {
			msg = '移库明细,不能为空';
			isExec = false;
		}
		for (var i = 0; i < len; i++) {
			if (!records[i].get('ReaBmsTransferDtl_DStorageID')) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标库房\n为空,不能保存<br>';
				isExec = false;
			}
			if (me.PlaceData && me.PlaceData.length > 0) {
				if (!records[i].get('ReaBmsTransferDtl_DPlaceID')) {
					msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标货架\n为空,不能保存<br>';
					isExec = false;
				}
			}
			//移库数
			var GoodsQty = records[i].get('ReaBmsTransferDtl_GoodsQty');
			if (!GoodsQty) GoodsQty = 0;

			/* if (records[i].get('ReaBmsTransferDtl_GoodsQty') == '0') {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数为0,不能保存<br>';
				isExec = false;
			}
			if (!GoodsQty) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数为空,不能保存<br>';
				isExec = false;
			} 
			if (Number(GoodsQty) < 0) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数小于0,不能保存<br>';
				isExec = false;
			}*/

			//库存数量
			var DefaulteGoodsQty = records[i].get('ReaBmsTransferDtl_SumCurrentQty');
			if (!DefaulteGoodsQty) DefaulteGoodsQty = 0;
			if (Number(DefaulteGoodsQty) < Number(GoodsQty)) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数大于现有库存量,不能保存<br>';
				isExec = false;
			}
			var SStorageID = records[i].get('ReaBmsTransferDtl_SStorageID');
			var SPlaceID = records[i].get('ReaBmsTransferDtl_SPlaceID');
			var DStorageID = records[i].get('ReaBmsTransferDtl_DStorageID');
			var DPlaceID = records[i].get('ReaBmsTransferDtl_DPlaceID');
			var DStoragePlace = DStorageID + DPlaceID;
			var StoragePlace = SStorageID + SPlaceID;
			if (DStorageID && SStorageID) {
				if (DStoragePlace == StoragePlace) {
					msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目的库房货架,源库房货架一致,不能保存<br>';
					isExec = false;
				}
			}
		}
		if (!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	/**
	 * @description 移库数验证
	 */
	onCheckGoodsQty: function() {
		var me = this;
		var records = me.store.data.items,
			len = records.length;
		var isExec = true;
		var msg = '';
		isExec = true, msg = '';
		for (var i = 0; i < len; i++) {
			//移库数
			var goodsQty = records[i].get('ReaBmsTransferDtl_GoodsQty');
			if (!goodsQty) goodsQty = 0;
			if (!goodsQty) {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数为空<br>';
				isExec = false;
			}
			if (Number(goodsQty) < 0|| goodsQty == '0') {
				msg += '货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的移库数小于等于0<br>';
				isExec = false;
			}
		}
		if (!isExec) {
			JShell.Msg.error(msg);
		}
		var result = {
			"isExec": isExec,
			"msg": msg
		};
		return result;
	},
	getEditList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtEditList = [];
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			if (rec && rec.get('ReaBmsTransferDtl_Id')) {
				var entity = me.getEntity(rec);
				dtEditList.push(entity);
			}
		}
		return dtEditList;
	},
	getEntity: function(rec) {
		var me = this;
		var entity = me.callParent(arguments);
		var Id = rec.get('ReaBmsTransferDtl_Id');
		var reqGoodsQty = rec.get('ReaBmsTransferDtl_ReqGoodsQty');
		if (!reqGoodsQty) reqGoodsQty = 0;
		var goodsQty = rec.get('ReaBmsTransferDtl_GoodsQty');
		if (!goodsQty) goodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		goodsQty = parseFloat(goodsQty);
		if (!reqGoodsQty || reqGoodsQty <= 0 || reqGoodsQty != goodsQty) {
			//移库确认时新增的移库货品
			if (!Id || Id == "" || Id == "-1") reqGoodsQty = goodsQty;
		}
		var price = rec.get('ReaBmsTransferDtl_Price');
		if (!price) price = 0;
		price = parseFloat(price);
		entity.ReqGoodsQty = reqGoodsQty;
		entity.GoodsQty = goodsQty;
		entity.Price = price;
		entity.SumTotal = goodsQty * price;
		if (Id) entity.Id = Id;
		return entity;
	}
});
