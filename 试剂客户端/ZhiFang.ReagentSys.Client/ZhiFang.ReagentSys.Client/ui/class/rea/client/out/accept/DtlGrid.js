/**
 * 出库明细
 * @author longfc
 * @version 2019-03-25
 */
Ext.define('Shell.class.rea.client.out.accept.DtlGrid', {
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
			width: 100,
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
			doSort: function(state) {
				var field="ReaGoods_SName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
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
			xtype: 'numbercolumn',
			format: '0.00',
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
			//hidden: true,
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
			text: '申请时库存',
			sortable: false,
			hidden: true,
			width: 80,
			xtype: 'numbercolumn',
			format: '0.00',
			renderer: function(value, meta) {
				var v = value;
				if(v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsOutDtl_ReqGoodsQty',
			text: '申请数',
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
			dataIndex: 'ReaBmsOutDtl_GoodsQty',
			width: 70,
			text: '<b style="color:blue;">出库数</b>',
			sortable: false,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				listeners: {
					focus: function(field, e, eOpts) {
						me.comSetReadOnlyOfBarCodeType(field, e);
					},
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
		}, {
			dataIndex: 'ReaBmsOutDtl_ReaTestEquipVOList',
			text: '试剂所属仪器信息',
			sortable: false,
			hidden: true,
			width: 100,
			editor: {
				readOnly: true
			},
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
			editor: {
				readOnly: true
			},
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
			editor: {
				readOnly: true
			},
			width: 100,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta, record);
				return v;
			}
		});

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.unshift('refresh', '-');
		return items;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(me.PK && records && records.length > 0) {
			me.onSumGoodsQty();
			me.setReaTestEquipVOList();
		}
	},
	/**
	 * @description 获取出库货品的使用仪器信息
	 * */
	setReaTestEquipVOList: function() {
		var me = this;
		me.store.each(function(record) {
			var goodsID = record.get('ReaBmsOutDtl_GoodsID');
			if(!me.ReaTestEquipVOList || me.ReaTestEquipVOList.length == 0) me.getReaTestEquipVOList(true);
			if(me.ReaTestEquipVOList && me.ReaTestEquipVOList.length > 0) {
				for(var i = 0; i < me.ReaTestEquipVOList.length; i++) {
					var item = me.ReaTestEquipVOList[i];
					if(goodsID == item.GoodsID) {
						record.set('ReaBmsOutDtl_ReaTestEquipVOList', Ext.encode(item.ReaTestEquipVOList));
						//continue;
						break;
					}
				}
			}
		});
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for(var i = 0; i < data.list.length; i++) {
			//合并信息:供应商Id+货品Id+批号+库房Id+货架Id
			var reaCompanyID = data.list[i].ReaBmsOutDtl_ReaCompanyID;
			var goodsID = data.list[i].ReaBmsOutDtl_GoodsID;
			var lotNo = data.list[i].ReaBmsOutDtl_LotNo;
			var storageID = data.list[i].ReaBmsOutDtl_StorageID;
			var placeID = data.list[i].ReaBmsOutDtl_PlaceID;
			var tab1 = reaCompanyID + goodsID + lotNo + storageID + placeID;
			data.list[i]["ReaBmsOutDtl_Tab"] = tab1;

			//出库数默认值处理(盒条码的移库数取0,批条或无条码的出库货品默认取申请数)
			var barCodeMgr = data.list[i].ReaBmsOutDtl_BarCodeType;
			if(barCodeMgr == "1") {
				data.list[i].ReaBmsOutDtl_GoodsQty = 0;
			} else {
				data.list[i].ReaBmsOutDtl_GoodsQty = data.list[i].ReaBmsOutDtl_ReqGoodsQty;
			}
		}
		return data;
	},
	/**
	 * @description 设置当前剩余总库存值
	 * */
	onSumGoodsQty: function() {
		var me = this;
		me.getGoodsQty();
	},
	/**
	 * @description 设置当前剩余总库存值
	 * */
	setSumQty: function(rec, sumCurrentQty) {
		var me = this;
		me.callParent(arguments);
		me.setGoodsQtyOfSumCurrentQty(rec, sumCurrentQty);
	},
	/**
	 * @description 批条码货品的默认出库数不能大于当前库存数
	 * */
	setGoodsQtyOfSumCurrentQty: function(rec, sumCurrentQty) {
		var me = this;
		var barCodeMgr = "" + rec.get("ReaBmsOutDtl_BarCodeType");
		if(barCodeMgr == "0") {
			var goodsQty = rec.get("ReaBmsOutDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			if(goodsQty > sumCurrentQty) goodsQty = sumCurrentQty;
			var price = rec.get("ReaBmsOutDtl_Price");
			if(!price) price = 0;
			price = parseFloat(price);
			var sumTotal = price * goodsQty;
			rec.set('ReaBmsOutDtl_GoodsQty', goodsQty);
			rec.set('ReaBmsOutDtl_SumTotal', sumTotal);
		}
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
				dtEditList.push(entity);
			}
		}
		return dtEditList;
	},
	getEntity: function(rec) {
		var me = this;
		var obj = me.callParent(arguments);
		var Id = rec.get('ReaBmsOutDtl_Id');
		var godsQty = rec.get('ReaBmsOutDtl_GoodsQty');
		var reqGoodsQty = rec.get('ReaBmsOutDtl_ReqGoodsQty');
		if(!reqGoodsQty) reqGoodsQty = 0;
		reqGoodsQty = parseFloat(reqGoodsQty);
		if(!godsQty) godsQty = 0;
		godsQty = parseFloat(godsQty);
		if(Id) obj.Id = Id;
		obj.DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		var DefaulteGoodsQty = rec.get('ReaBmsOutDtl_DefaulteGoodsQty');
		if(DefaulteGoodsQty) obj.ReqCurrentQty = DefaulteGoodsQty;		
		var barCodeQtyDtlID = rec.get('ReaBmsOutDtl_BarCodeQtyDtlID');
		if(barCodeQtyDtlID) {
			obj.QtyDtlID = barCodeQtyDtlID;
		}
		//扫码明细
		var reaBmsInDtlLink = [];
		var scanCodeList = rec.get('ReaBmsOutDtl_CurReaGoodsScanCodeList');
		if(scanCodeList.length > 0) {
			reaBmsInDtlLink = Ext.JSON.decode(scanCodeList);
		}
		obj.ReaBmsOutDtlLinkList = reaBmsInDtlLink;
		return obj;
	},
	/**
	 * 出库保存校验
	 * 直接出库时:出库数量不能为0;
	 * 确认出库时:如果是对出库申请进行确认出库,出库数量可以为0;
	 */
	onSaveCheck: function(isAllowZero) {
		var me = this;
		if(isAllowZero != false) isAllowZero = true;
		var isExec = me.callParent(arguments);
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
		if(record) {
			//条码类型,0批条码,1盒条码
			var barCodeType = record.get('ReaBmsOutDtl_BarCodeType');
			//盒条码扫码验证
			if(barCodeType == '1') {
				var isExec2 = me.checkBarCode(record, barcode);
				if(!isExec2) return;
			}
			me.onSetJObjectBarCode(record, barcode, qtyRec.data);
		}
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
	/**更新货品总额*/
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
		//出库扫码模式判断
		outDtlObj = me.createScanCodeModel(outDtlObj, barcode);
		return outDtlObj;
	}
});