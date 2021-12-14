/**
 * 出库明细
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.apply.DtlGrid', {
	extend: 'Shell.class.rea.client.out.basic.DtlGrid',

	title: '出库明细列表',
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsOutDtlByHQL?isPlanish=true',
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsOutDtl',
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField',
	/**获取货品数据服务路径*/
	selectReaGoodsUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',

	/**试剂单位信息*/
	ReaGoodsList: [],
	/**出库类型默认值*/
	defaluteOutType: '1',
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('delclick');
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
		var columns = [];
		columns.push({
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center'
		}, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('delclick', rec.get('ReaBmsOutDtl_Tab'));
					if(rec.get('ReaBmsOutDtl_Id')) {
						var id = rec.get(me.PKField);
						me.delOneById(id, rec);
					} else {
						me.store.remove(rec);
					}
					me.fireEvent('changeSumTotal');
				}
			}]
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdGoodsNo',
			text: '厂商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsCName',
			text: '货品名称',
			sortable: false,
			width: 120,
			minWidth: 100,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsOutDtl_BarCodeType");
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
			dataIndex: 'ReaBmsOutDtl_SName',
			text: '简称',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdOrgName',
			text: '品牌',
			width: 90,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'ReaBmsOutDtl_Price',
			text: '单价',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_UnitMemo',
			text: '规格',
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumDtlGoodsQty',
			text: '已申请数',
			sortable: false,
			hidden: false,
			width: 70,
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsQty',
			text: '货品库存',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_SumCurrentQty',
			text: '剩余总库存',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_ReqCurrentQty',
			text: '申请时库存数',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '<b style="color:blue;">申请数</b>',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						if(newValue < 0) newValue = 0;
						me.setSumTotal(newValue, records[0]);
						me.fireEvent('changeSumTotal');
					}
				}
			},
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			width: 70,
			text: '出库数',
			hidden: true,
			sortable: false,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsUnit',
			width: 70,
			text: '单位',
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipID',
			text: '仪器Id',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});
		columns.push(me.createEquipNameColumn());
		columns.push({
			dataIndex: 'ReaBmsOutDtl_BarCodeType',
			text: '条码类型',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SumTotal',
			text: '金额',
			sortable: false,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_LotNo',
			text: '批号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageName',
			text: '库房名称',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceName',
			text: '货架名称',
			sortable: false,
			hidden: false,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_InvalidDate',
			text: '效期',
			sortable: false,
			width: 85,
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompanyID',
			text: '供应商Id',
			hidden: true,
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompanyName',
			text: '供应商',
			sortable: false,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_RegisterNo',
			text: '注册证号',
			sortable: false,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_StorageID',
			text: '库房ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_PlaceID',
			text: '货架ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSerial',
			text: '货品条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotSerial',
			text: '批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_SysLotSerial',
			text: '系统内部批号条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaServerCompCode',
			text: '供应商机平台构码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ProdDate',
			text: '生产日期',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsID',
			text: '货品iD',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Memo',
			text: 'Memo',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TaxRate',
			text: 'TaxRate',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_QtyDtlID',
			text: 'QtyDtlID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Tab',
			text: '合并标签', //供应商Id+货品Id+批号+库房Id+货架Id
			hideable: false,
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_DefaulteGoodsID',
			text: '原货品ID',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_LotQRCode',
			text: '二维批条码',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: false,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_GoodsSort',
			text: '货品序号',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_BarCodeQtyDtlID',
			text: '本次扫码库存ID',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaBmsOutDtl_TestEquipName',
			text: '仪器名称',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsOutDtl_CurReaGoodsScanCodeList',
			text: '当前扫码记录',
			sortable: false,
			hidden: true,
			editor: {},
			width: 100,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaTestEquipVOList',
			text: '仪器数组',
			sortable: false,
			hidden: true,
			width: 100,
			defaultRenderer: true
		});

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items = me.createTestEquipItems(items);
		return items;
	},
	createOutDtlObj: function(recQty) {
		var me = this;
		var obj = me.callParent(arguments);
		obj.ReaBmsOutDtl_ReqGoodsQty = 1;
		return obj;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(me.PK) {
			me.onSumGoodsQty();
		}
		me.store.each(function(record) {
			var goodsID = record.get('ReaBmsOutDtl_GoodsID');
			if(!me.ReaTestEquipVOList || me.ReaTestEquipVOList.length == 0) me.getReaTestEquipVOList(true);
			if(me.ReaTestEquipVOList && me.ReaTestEquipVOList.length > 0) {
				for(var i = 0; i < me.ReaTestEquipVOList.length; i++) {
					var item = me.ReaTestEquipVOList[i];
					if(goodsID == item.GoodsID) {
						record.set('ReaBmsOutDtl_ReaTestEquipVOList', Ext.encode(item.ReaTestEquipVOList));
						continue;
					}
				}
			}
		});
	},
	onSumGoodsQty: function() {
		var me = this;
		me.getGoodsQty();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for(var i = 0; i < data.list.length; i++) {
			var GoodsID = data.list[i].ReaBmsOutDtl_GoodsID;
			var LotNo = data.list[i].ReaBmsOutDtl_LotNo;
			//供应商+货品+批号+库房+货架
			var ReaCompanyID = data.list[i].ReaBmsOutDtl_ReaCompanyID;
			var StorageID = data.list[i].ReaBmsOutDtl_StorageID;
			var PlaceID = data.list[i].ReaBmsOutDtl_PlaceID;
			var tab1 = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			data.list[i]["ReaBmsOutDtl_DefaulteGoodsID"] = GoodsID;
			data.list[i]["ReaBmsOutDtl_Tab"] = tab1;
		}
		return data;
	},
	getAddList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtAddList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			if(rec && !rec.get('ReaBmsOutDtl_Id')) {
				var entity = me.getEntity(rec);
				//实际出库数先置为0,在确认出库时才处理
				entity.GoodsQty = 0;
				//清空出库货品明细对应的库存ID,在进行出库确认时才进行绑定

				dtAddList.push(entity);
			}
		}
		return dtAddList;
	},
	getEditList: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var dtEditList = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			if(rec && rec.get('ReaBmsOutDtl_Id')) {
				var entity = me.getEntity(rec);
				//实际出库数先置为0,在确认出库时才处理
				entity.GoodsQty = 0;
				//清空出库货品明细对应的库存ID,在进行出库确认时才进行绑定

				dtEditList.push(entity);
			}
		}
		return dtEditList;
	},
	getEntity: function(rec) {
		var me = this;
		var obj = me.callParent(arguments);
		obj.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		var DefaulteGoodsQty = rec.get('ReaBmsOutDtl_DefaulteGoodsQty');
		if(DefaulteGoodsQty) obj.ReqCurrentQty = DefaulteGoodsQty;
		//出库申请时,实际出库数为0
		obj.GoodsQty = 0;
		return obj;
	},
	/**
	 * 保存校验
	 * 申请数量，不能为0
	 */
	onSaveCheck: function(isAllowZero) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		var isExec = true;
		var msg = '';
		if(len == 0) {
			msg = '出库申请明细不能为空';
			isExec = false;
		}
		for(var i = 0; i < len; i++) {
			var goodsCName = records[i].get('ReaBmsOutDtl_GoodsCName');
			//实际出库数量
			var godsQty = records[i].get('ReaBmsOutDtl_GoodsQty');
			var reqGoodsQty = records[i].get('ReaBmsOutDtl_ReqGoodsQty');
			if(!reqGoodsQty) reqGoodsQty = 0;
			reqGoodsQty = parseFloat(reqGoodsQty);
			if(!godsQty) godsQty = 0;
			if(reqGoodsQty <= 0) {
				msg += '货品名称:【' + goodsCName + '】的申请数必须大于0<br>';
				isExec = false;
			}
			var goodsUnit = records[i].get('ReaBmsOutDtl_GoodsUnit');
			var lotNo = records[i].get('ReaBmsOutDtl_LotNo');
			var inventory = records[i].get('ReaBmsOutDtl_SumCurrentQty');
			if(!inventory) inventory = 0;
			inventory = parseFloat(inventory);
			//xx货品出库数为15瓶,货品批号为****的现有库存数为10瓶,不能出库
			if(inventory < reqGoodsQty) {
				msg += '货品名称:【' + goodsCName + '】的申请数为' + reqGoodsQty + goodsUnit + '货品批号为【' + lotNo + '】的现有库存数为' + inventory + goodsUnit + ',不能出库<br>';
				isExec = false;
			}
			var EquipID = records[i].get('ReaBmsOutDtl_TestEquipID');
			//使用出库才需要填写仪器
			if(me.IsEquip == '1' && !EquipID && me.defaluteOutType == '1') {
				msg += '货品名称:【' + goodsCName + '】的仪器为空,不能保存<br>';
				isExec = false;
			}
		}
		if(!isExec) {
			JShell.Msg.error(msg);
		}
		return isExec;
	},
	/**找到行新增和删除，对外公开
	 * 1.根据供应商+货品+批号+库房+货架判断，
	 * 明细存在一样的就不再添加
	 * */
	addRecordOne: function(qtyRec, barcode) {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		if(!qtyRec) return;
		var record = me.doesItExistRec(qtyRec);
		if(record) me.onSetJObjectBarCode(record, barcode, qtyRec.data);
		if(!record) {
			var obj = me.createRowObj(qtyRec, barcode);
			me.getAddQtyCount(obj, function(data) {
				var list = data.value;
				//当前库存
				var SumCurrentQty = list.SumCurrentQty;
				//已申请数
				var SumDtlGoodsQty = list.SumDtlGoodsQty;
				if(!SumCurrentQty) SumCurrentQty = 0;
				if(!SumDtlGoodsQty) SumDtlGoodsQty = 0;
				obj.ReaBmsOutDtl_SumCurrentQty = SumCurrentQty;
				obj.ReaBmsOutDtl_SumDtlGoodsQty = SumDtlGoodsQty;
			});
			JShell.Action.delay(function() {
				me.store.insert(me.store.getCount(), obj);
				me.fireEvent('changeSumTotal');
			}, null, 100);
		}
	},
	/**创建新增行数据*/
	createRowObj: function(recQty, barcode) {
		var me = this;
		var dataList = [];
		if(!me.ReaTestEquipVOList || me.ReaTestEquipVOList.length == 0) me.getReaTestEquipVOList(true);
		if(me.ReaTestEquipVOList && me.ReaTestEquipVOList.length > 0) {
			for(var i = 0; i < me.ReaTestEquipVOList.length; i++) {
				dataList = [];
				var item = me.ReaTestEquipVOList[i];
				if(recQty.get('ReaBmsQtyDtl_GoodsID') == item.GoodsID) {
					dataList = item.ReaTestEquipVOList;
					break;
				}
			}
		}
		var outDtlObj = me.createOutDtlObj(recQty.data);
		outDtlObj = me.createBarcodeJObject(outDtlObj, recQty, barcode);
		//仪器处理
		outDtlObj = me.setOutDtlObjTestEquipValue(recQty, outDtlObj);
		return outDtlObj;
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
	getQtyCount: function(rec) {
		var me = this;
		var GoodsID = rec.get('ReaBmsOutDtl_DefaulteGoodsID');
		if(!GoodsID) GoodsID = rec.get('ReaBmsOutDtl_GoodsID');
		var LotNo = rec.get('ReaBmsOutDtl_LotNo');
		var ReaCompanyID = rec.get('ReaBmsOutDtl_ReaCompanyID');
		var StorageID = rec.get('ReaBmsOutDtl_StorageID');
		var PlaceID = rec.get('ReaBmsOutDtl_PlaceID');
		var InvalidDate = rec.get('ReaBmsOutDtl_InvalidDate');
		var ReaGoodsNo = rec.get('ReaBmsOutDtl_ReaGoodsNo');
		var url = JShell.System.Path.ROOT + me.selectStoreUrl;
	
		var qtyHql = "reabmsqtydtl.ReaGoodsNo='" + ReaGoodsNo + "'" +
			" and reabmsqtydtl.LotNo='" + LotNo + "'" +
			" and reabmsqtydtl.ReaCompanyID='" + ReaCompanyID + "'" +
			" and reabmsqtydtl.StorageID='" + StorageID + "'" +
			" and reabmsqtydtl.GoodsQty>0";
		if(PlaceID) {
			qtyHql += " and reabmsqtydtl.PlaceID='" + PlaceID + "'";
		}
		var dtlHql = "reabmsoutdtl.ReaGoodsNo='" + ReaGoodsNo + "'" +
			" and reabmsoutdtl.LotNo='" + LotNo + "'" +
			" and reabmsoutdtl.ReaCompanyID='" + ReaCompanyID + "'" +
			" and reabmsoutdtl.StorageID='" + StorageID + "'" +
			" and reabmsoutdtl.GoodsQty>0";
		if(PlaceID) {
			dtlHql += " and reabmsoutdtl.PlaceID='" + PlaceID + "'";
		}
		qtyHql=JShell.String.encode(qtyHql);
		dtlHql=JShell.String.encode(dtlHql);
		
		url += '?dtlType=ReaBmsOutDtl&qtyHql=' + qtyHql + '&dtlHql=' + dtlHql + '&goodsId=' + GoodsID;
		
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value;
				//当前剩余总库存
				var SumCurrentQty = list.SumCurrentQty;
				//已申请总数
				var SumDtlGoodsQty = list.SumDtlGoodsQty;
				if(!SumCurrentQty) SumCurrentQty = 0;
				if(!SumDtlGoodsQty) SumDtlGoodsQty = 0;
				rec.set('ReaBmsOutDtl_SumCurrentQty', SumCurrentQty);
				rec.set('ReaBmsOutDtl_SumDtlGoodsQty', SumDtlGoodsQty);
				rec.commit();
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});