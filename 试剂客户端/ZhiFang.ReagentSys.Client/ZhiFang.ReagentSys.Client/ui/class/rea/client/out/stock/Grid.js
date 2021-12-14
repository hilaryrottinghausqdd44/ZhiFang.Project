/**
 * 近效期库存货品
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.out.stock.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	title: '近效期库存货品',

	/**查询库存表数据*/
	/**增加参数isMergeInDocNo=true使得开启近效期时，右边列表能够合并，显示批号与效期*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtl?isPlanish=true&isMergeInDocNo=true',

	/**默认加载*/
	defaultLoad: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_InvalidDate',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: false,
	/**货品ID*/
	GoodsID: null,
	/**库房ID*/
	StorageID: null,
	/**选择行效期,*/
	InvalidDate: null,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**左边列表选择行ID*/
	TabID: null,
	/**后台排序*/
	remoteSort: false,
	//强制近效期出库复选框选择状态
	neareffectChecked:true,
	//强制近效期出库复选框是否禁用
	neareffectDisabled:false,
	/**是否强制近效期出库 1:强制;2:不强制;3:界面选择默认强制;4:界面选择默认不强制;*/
	isOutOfStockInNeartermPeriod: "",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//是否强制近效期出库
		JShell.REA.RunParams.getRunParamsValue("IsOutOfStockInNeartermPeriod", false, function(data1) {
			me.isOutOfStockInNeartermPeriod = JcallShell.REA.RunParams.Lists.IsOutOfStockInNeartermPeriod.Value;
		});
		//初始化检索监听
		me.initFilterListeners();
		me.store.on({
			load: function(s, records, su) {
				if(records.length > 0) {
					me.onOneData();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
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
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			hidden: true,
			sortable: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			sortable: true,
			hidden: true,
			width: 85,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
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
			dataIndex: 'ReaBmsQtyDtl_SName',
			text: '简称',
			sortable: true,
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdOrgName',
			text: '厂家',
			sortable: true,
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '批号',
			sortable: true,
			flex: 1,
			width: 120,
			defaultRenderer: true,
		}, { // 双击调用服务后，用来存储服务返回的上次批号
			dataIndex: 'ReaBmsQtyDtl_LastLotNo',
			text: '上次的批号',
			width: 120,
			hideable: false,
			hidden: true
		}, {
			// isChanageLotNo作为标识列，判断这个货品批号在上次出库中有没有，没有就change
			dataIndex: 'isChanageLotNo',
			text: '是否改变批号',
			width: 120,
			hideable: false,
			hidden: true
		},
		{
			dataIndex: 'ReaBmsQtyDtl_TransportNo',
			text: '货运单号',
			sortable: true,
			flex: 1,
			width: 120,
			defaultRenderer: true,
		}, { // 双击调用服务后，用来存储服务返回的上次货运单号
			dataIndex: 'ReaBmsQtyDtl_LastTransportNo',
			text: '上次的货运单号',
			width: 120,
			hideable: false,
			hidden: true
		}, { // 是否一致isChanageTransportNo作为标识列，判断这个货品单号在上次出库中有没有，没有就change
			 dataIndex: 'isChanageTransportNo',
			 text: '是否改变货运单号',
			 width: 120,
			 hideable: false,
			 hidden: true
		},
		{
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			hidden: true,
			sortable: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			hidden: true,
			sortable: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsNo',
			text: '平台编码',
			hidden: true,
			sortable: true,
			width: 100,
			minWidth: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			hidden: true,
			sortable: true,
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存量',
			hidden: true,
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Price',
			text: '单价',
			hidden: true,
			sortable: true,
			width: 75,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '效期',
			sortable: true,
			width: 85,
			isDate: true,
			width: 100,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_RegisterNo',
			text: '注册证号',
			hidden: true,
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '库存Id',
			hidden: true,
			sortable: true,
			hidden: true,
			isKey: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompanyID',
			text: '本地供应商',
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageID',
			text: '库房ID',
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceID',
			text: '货架ID',
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_TaxRate',
			text: 'TaxRate',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSerial',
			text: 'GoodsSerial',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotSerial',
			text: 'LotSerial',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SysLotSerial',
			text: 'SysLotSerial',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompGoodsLinkID',
			text: 'CompGoodsLinkID',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaServerCompCode',
			text: 'ReaServerCompCode',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: 'BarCodeType',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: 'ProdDate',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Tab',
			text: '供应商+货品+批号+库房+货架',
			hideable: false,
			editor: {},
			sortable: true,
			hidden: true,
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Del',
			text: '被合并行标志',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_AddTag',
			text: '新增行ID',
			hideable: false,
			hidden: true,
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_SelectTag',
			text: '是否已被选择',
			hideable: false,
			hidden: true,
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotQRCode',
			text: '二维批条码',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaCompCode',
			text: '供货方编码',
			hideable: false,
			sortable: true,
			hidden: true,
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSort',
			text: '货品序号',
			sortable: true,
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CurReaGoodsScanCodeList',
			sortable: true,
			hidden: true,
			text: '当次扫码记录集合',
			width: 100,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', {
			xtype: 'checkboxfield',
			margin: '0 0 0 0',
			boxLabel: '强制近效期出库',
			isLocked:true,
			name: 'forceCheck',
			itemId: 'forceCheck'
		});
		return buttonToolbarItems;
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		if(!this.GoodsID) return;
		this.onSearch();
	},
	changeDefaultWhere: function() {
		var me = this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere) {
			var index = me.defaultWhere.indexOf('reabmsqtydtl.GoodsQty>0');
			if(index == -1) {
				me.defaultWhere += ' and reabmsqtydtl.GoodsQty>0';
			}
		} else {
			me.defaultWhere = 'reabmsqtydtl.GoodsQty>0';
		}
	},
	initFilterListeners: function() {
		var me = this;
	},
	getQtyHql: function() {
		var me = this;
		var reabmsqtydtlHql = "reabmsqtydtl.Visible=1 and reabmsqtydtl.GoodsQty>0" +
			" and reabmsqtydtl.StorageID=" + me.StorageID +
			" and reabmsqtydtl.GoodsID=" + me.GoodsID;
		return reabmsqtydtlHql;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
			/**在这里不能增加这个参数'&isMergeInDocNo=' + me.isMergeInDocNo*/
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;

		var qtyHql = me.getQtyHql();
		url += '&qtyHql=' + JShell.String.encode(qtyHql);
		url+='&reaGoodsHql="1=1"';
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		for(var i = 0; i < data.list.length; i++) {
			var ItemPrice = 0;
			//供应商+货品+批号+库房+货架
			var ReaCompanyID = data.list[i].ReaBmsQtyDtl_ReaCompanyID;
			var GoodsID = data.list[i].ReaBmsQtyDtl_GoodsID;
			var LotNo = data.list[i].ReaBmsQtyDtl_LotNo;
			var StorageID = data.list[i].ReaBmsQtyDtl_StorageID;
			var PlaceID = data.list[i].ReaBmsQtyDtl_PlaceID;
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			var InvalidDate2 = data.list[i].ReaBmsQtyDtl_InvalidDate;
			InvalidDate2 = JcallShell.Date.toString(InvalidDate2, true);
			var InvalidDate = JcallShell.Date.toString(me.InvalidDate, true);
			//只显示选择行和当前选择行较早的数据
			// 调整：将下面的if放开
			// if(InvalidDate > InvalidDate2 || InvalidDate === InvalidDate2) {
				data.list[i]["ReaBmsQtyDtl_Tab"] = str;
			// }
		}
		//按照日期返回的排序数组
		data.list = me.quickSort(data.list, "ReaBmsQtyDtl_InvalidDate", false);
		return data;
	},
	quickSort: function(arr, name, snum) {
		var me = this;
		//如果数组<=1,则直接返回
		if(arr.length <= 1) {
			return arr;
		}
		var pivotIndex = Math.floor(arr.length / 2);
		//找基准，并把基准从原数组删除
		var pivot = arr.splice(pivotIndex, 1)[0];
		var middleNum = pivot[name];
		// 定义左右数组
		var left = [];
		var right = [];
		//比基准小的放在left，比基准大的放在right
		if(snum) {
			for(var i = 0; i < arr.length; i++) {
				if(Number(arr[i][name]) <= Number(middleNum)) {
					left.push(arr[i]);
				} else {
					right.push(arr[i]);
				}
			}
		} else {
			for(var i = 0; i < arr.length; i++) {
				if(arr[i][name] <= middleNum) {
					left.push(arr[i]);
				} else {
					right.push(arr[i]);
				}
			}
		}
		//递归,返回所需数组
		return me.quickSort(left, name, snum).concat([pivot], me.quickSort(right, name, snum));
	},
	compare: function(property) {
		return function(a, b) {
			var value1 = a[property];
			var value2 = b[property];
			return value1 - value2;
		}
	},
	/**只有一行数据时（与左边列表相同），不需要显示
	 * */
	onOneData: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		if(!records) return;
		if(len == 1) {
			var ReaCompanyID = records[0].get('ReaBmsQtyDtl_ReaCompanyID');
			var GoodsID = records[0].get('ReaBmsQtyDtl_GoodsID');
			var LotNo = records[0].get('ReaBmsQtyDtl_LotNo');
			var StorageID = records[0].get('ReaBmsQtyDtl_StorageID');
			var PlaceID = records[0].get('ReaBmsQtyDtl_PlaceID');
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			if(me.TabID == str) {
				me.store.removeAll();
			}
		}
	},
	/**
	 * 扫码后显示多行数据的，扫码记录只能是
	 * */
	onOneData: function() {
		var me = this,
			records = me.store.data.items,
			len = records.length;
		if(!records) return;
		if(len == 1) {
			var ReaCompanyID = records[0].get('ReaBmsQtyDtl_ReaCompanyID');
			var GoodsID = records[0].get('ReaBmsQtyDtl_GoodsID');
			var LotNo = records[0].get('ReaBmsQtyDtl_LotNo');
			var StorageID = records[0].get('ReaBmsQtyDtl_StorageID');
			var PlaceID = records[0].get('ReaBmsQtyDtl_PlaceID');
			var str = ReaCompanyID + GoodsID + LotNo + StorageID + PlaceID;
			if(me.TabID == str) {
				me.store.removeAll();
			}
		}
	},
	/**
	 * @description 按运行参数设置强制近效期出库复选框状态
	 * @param {Object} checked 选择默认值
	 * @param {Object} disabled 是否禁用
	 */
	setOutNeartermPeriod: function(checked, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var	com = buttonsToolbar.getComponent('forceCheck');
		if (com) {
			me.neareffectChecked=checked;
			me.neareffectDisabled=disabled;
			com.setValue(checked);
			com.setDisabled(disabled);
		}
		return com;
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this;
		me.callParent(arguments);
		if(me.isOutOfStockInNeartermPeriod!="1"&&me.isOutOfStockInNeartermPeriod!="2"){
			
		}else{
			//重新调用
			me.setOutNeartermPeriod(me.neareffectChecked,me.neareffectDisabled);
		}
	}
});