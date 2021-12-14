/**
 * 移库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.transfer.apply.DtlGrid', {
	extend: 'Shell.class.rea.client.transfer.basic.DtlGrid',
	title: '移库申请明细列表',

	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlByHQL?isPlanish=true',
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDtl',
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',
	/**获取货品数据服务路径*/
	selectReaGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/**移库管理类型：1-直接移库 ，2-移库管理(申请)，3-移库管理(全部）*/
	TYPE: '2',
	PK: null,
	defaultLoad: false,
	/**用户UI配置Key*/
	userUIKey: 'transfer.apply.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "移库确认明细列表",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			update: function(store, record) {
				if(record.dirty) {
					var changedObj = record.getChanges();
					for(var modified in changedObj) {
						if(modified == "ReaBmsTransferDtl_ReqGoodsQty")
							me.onReqGoodsQtyChanged(record);
					}
				}
			}
		});
		if(me.PK) {
			me.onLoadByDocID();
		}
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
					var Id = record.get(me.PKField);
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('delclick', rec.get('ReaBmsTransferDtl_Tab'));
					if(rec.get('ReaBmsTransferDtl_Id')) {
						var id = rec.get(me.PKField);
						me.delOneById(id, rec);
					} else {
						me.store.remove(rec);
					}
					me.fireEvent('changeSumTotal');
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
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_SPlaceName',
			text: '源货架',
			sortable: false,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageName',
			text: '目的库房',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceName',
			text: '目的货架',
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
			dataIndex: 'ReaBmsTransferDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			sortable: false,
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
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
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
			text: '现有库存',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReqGoodsQty',
			text: '<b style="color:blue;">申请数</b>',
			width: 80,
			sortable: false,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {}
				}
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsQty',
			text: '移库数',
			hidden: true,
			sortable: false,
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_GoodsUnit',
			text: '单位',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DPlaceID',
			text: '目的货架Id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_DStorageID',
			text: '目的库房id',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 90,
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
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsTransferDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 70,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
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
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 120,
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
			text: '货品iD',
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
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsTransferDtl_LotQRCode',
			text: '二维条码号',
			sortable: false,
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
			editor: {},
			sortable: false,
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
			isKey: true
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
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(me.PK) {
			me.onSumGoodsQty();
		}
		me.getSelectionModel().selectAll();
	},
	/**
	 * @description 申请数值改变后处理
	 * */
	onReqGoodsQtyChanged: function(record) {
		var me = this;
		var reqGoodsQty = record.get('ReaBmsTransferDtl_ReqGoodsQty');
		if(!reqGoodsQty) reqGoodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);

		me.setSumTotal(reqGoodsQty, record);
		record.commit();
		me.fireEvent('changeSumTotal');
	},
	onSumGoodsQty: function() {
		var me = this;
		me.getGoodsQty();
	},
	//根据主单查数据
	onLoadByDocID: function() {
		var me = this;
		if(!me.PK) return;
		me.defaultWhere = 'reabmstransferdtl.TransferDocID=' + me.PK;
		me.onSearch();
	},
	getAddList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			if(rec && !rec.get('ReaBmsTransferDtl_Id')) {
				var entity = me.getEntity(rec);
				dtAddList.push(entity);
			}
		}
		return dtAddList;
	},
	/**删除一条数据*/
	delOneById: function(id, record) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(record) {
					record.set(me.DelField, true);
					record.commit();
					me.store.remove(record);
					var Total = me.getSumTotal();
					me.onUpateTotalClick(me.PK, Total);
				}
			} else {
				if(record) {
					record.set(me.DelField, false);
					record.set('ErrorInfo', data.msg);
					JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					record.commit();
				}
			}
		}, false);
	},
	/**更新主单货品总额*/
	onUpateTotalClick: function(id, TotalPrice) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.editUrl;

		var params = {
			Id: id,
			TotalPrice: TotalPrice
		};
		var entity = {
			entity: params,
			fields: "Id,TotalPrice"
		};
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, Ext.JSON.encode(entity), function(data) {
			me.hideMask();
			if(!data.success) {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**找到行新增和删除，对外公开*/
	onAddOne: function(rec, barcode, UnitArr) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		//根据供应商+货品+批号+库房+货架判断
		var record = me.doesIsExistRec(rec);
		if(record) me.checkBarCode(record, rec, barcode);
		//查找当前选择数据行
		if(!record) {
			var obj = me.createRowObj(rec, UnitArr);
			me.getAddQtyCount(obj, function(data) {
				var list = data.value;
				//当前库存
				var SumCurrentQty = list.SumCurrentQty;
				//已申请数
				var SumDtlGoodsQty = list.SumDtlGoodsQty;
				if(!SumCurrentQty) SumCurrentQty = 0;
				if(!SumDtlGoodsQty) SumDtlGoodsQty = 0;
				obj.ReaBmsTransferDtl_SumCurrentQty = SumCurrentQty;
				obj.ReaBmsTransferDtl_SumDtlGoodsQty = SumDtlGoodsQty;
			});
			JShell.Action.delay(function() {
				me.store.insert(me.store.getCount(), obj);
				me.fireEvent('changeSumTotal');
			}, null, 100);
		}
		me.getSelectionModel().selectAll();
	},
	/**@overwrite 改变返回的数据
	 * ReaBmsQtyDtl_GoodsUnitTab 临时变量
	 * UnitArr 可选单位
	 * */
	changeResult: function(data) {
		var me = this,
			result = {},
			list = [],
			arr = [];
		for(var i = 0; i < data.list.length; i++) {
			var rec = data.list[i];
			var unitTabobj = {
				GoodsUnit: data.list[i].ReaBmsTransferDtl_GoodsUnit,
				ReqGoodsQty: data.list[i].ReaBmsTransferDtl_ReqGoodsQty,
				//GoodsQty: data.list[i].ReaBmsTransferDtl_GoodsQty,
				Price: data.list[i].ReaBmsTransferDtl_Price,
				ReaGoodsNo: data.list[i].ReaBmsTransferDtl_ReaGoodsNo,
				GoodsName: data.list[i].ReaBmsTransferDtl_GoodsName,
				RegistNo: data.list[i].ReaBmsTransferDtl_RegistNo,
				UnitMemo: data.list[i].ReaBmsTransferDtl_UnitMemo,
				GoodsID: data.list[i].ReaBmsTransferDtl_GoodsID,
				ProdGoodsNo: data.list[i].ReaBmsTransferDtl_ProdGoodsNo,
				GoodsNo: data.list[i].ReaBmsTransferDtl_GoodsNo,
				SumTotal: data.list[i].ReaBmsTransferDtl_SumTotal
			}
			var unitArr = [];
			var ItemPrice = 0;
			var GoodsID = data.list[i].ReaBmsTransferDtl_GoodsID;
			var LotNo = data.list[i].ReaBmsTransferDtl_LotNo;
			//供应商+货品+批号+库房+货架
			var ReaCompanyID = data.list[i].ReaBmsTransferDtl_ReaCompanyID;
			var StorageID = data.list[i].ReaBmsTransferDtl_SStorageID;
			var PlaceID = data.list[i].ReaBmsTransferDtl_SPlaceID;
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			var obj1 = {
				ReaBmsTransferDtl_DefaulteGoodsID: GoodsID,
				ReaBmsTransferDtl_DefaulteLotNo: LotNo,
				ReaBmsTransferDtl_ReqGoodsQty: data.list[i].ReaBmsTransferDtl_ReqGoodsQty,
				//ReaBmsTransferDtl_GoodsQty: data.list[i].ReaBmsTransferDtl_ReqGoodsQty,
				ReaBmsTransferDtl_Tab: str,
				ReaBmsQtyDtl_GoodsUnitTab: Ext.encode(unitTabobj)
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	},
	/**移库新增行*/
	createRowObj: function(rec, UnitArr) {
		var me = this;
		var JObjectBarCode = rec.get('ReaBmsQtyDtl_JObjectBarCode');
		if(JObjectBarCode) var JObjectBarCode = Ext.JSON.decode(JObjectBarCode);
		var obj = {};
		obj = me.changeJObjectBarCode(JObjectBarCode, obj, rec);
		obj.ReaBmsTransferDtl_ReqGoodsQty = 1;
		obj.ReaBmsTransferDtl_GoodsQty = 0;
		if(!obj.ReaBmsTransferDtl_Price) obj.ReaBmsTransferDtl_Price = 0;
		var SumTotal = Number(obj.ReaBmsTransferDtl_Price) * obj.ReaBmsTransferDtl_ReqGoodsQty;
		obj.ReaBmsTransferDtl_SumTotal = SumTotal;
		//临时变量
		var UnitTabobj = me.createUnit(obj);
		obj = me.createObj(obj, rec, UnitArr, UnitTabobj, JObjectBarCode);
		return obj;
	},
	getEditList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtEditList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			if(rec && rec.get('ReaBmsTransferDtl_Id')) {
				var entity = me.getEntity(rec);
				dtEditList.push(entity);
			}
		}
		return dtEditList;
	},
	getEntity: function(rec) {
		var me = this;
		var entity = me.callParent(arguments);
		var price = rec.get('ReaBmsTransferDtl_Price');
		if(!price) price = 0;
		price = parseFloat(price);
		entity.Price = price;
		//移库申请时移库数为0
		entity.GoodsQty = 0;
		var reqGoodsQty = rec.get('ReaBmsTransferDtl_ReqGoodsQty');
		if(!reqGoodsQty) reqGoodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		entity.SumTotal = 0;
		entity.ReqGoodsQty = reqGoodsQty;
		return entity;
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
		if(len == 0) {
			msg = '移库明细,不能为空';
			isExec = false;
		}
		for(var i = 0; i < len; i++) {
			if(!records[i].get('ReaBmsTransferDtl_DStorageID')) {
				msg += '移库明细,货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标库房\n为空,不能保存<br>';
				isExec = false;
			}
			if(me.PlaceData && me.PlaceData.length > 0) {
				if(!records[i].get('ReaBmsTransferDtl_DPlaceID')) {
					msg += '移库明细,货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目标货架\n为空,不能保存<br>';
					isExec = false;
				}
			}
			if(records[i].get('ReaBmsTransferDtl_ReqGoodsQty') == '0') {
				msg += '移库明细,货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的申请数量为0,不能保存<br>';
				isExec = false;
			}
			//申请数量
			var ReqGoodsQty = records[i].get('ReaBmsTransferDtl_ReqGoodsQty');
			if(!ReqGoodsQty) {
				msg += '移库明细,货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的申请数量为空,不能保存<br>';
				isExec = false;
			}
			var SStorageID = records[i].get('ReaBmsTransferDtl_SStorageID');
			var SPlaceID = records[i].get('ReaBmsTransferDtl_SPlaceID');
			var DStorageID = records[i].get('ReaBmsTransferDtl_DStorageID');
			var DPlaceID = records[i].get('ReaBmsTransferDtl_DPlaceID');
			var DStoragePlace = DStorageID + DPlaceID;
			var StoragePlace = SStorageID + SPlaceID;
			if(DStorageID && SStorageID) {
				if(DStoragePlace == StoragePlace) {
					msg += '移库明细,货品名称:【' + records[i].get('ReaBmsTransferDtl_GoodsCName') + '】的目的库房货架,源库房货架一致,不能保存<br>';
					isExec = false;
				}
			}
		}
		if(!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	}
});