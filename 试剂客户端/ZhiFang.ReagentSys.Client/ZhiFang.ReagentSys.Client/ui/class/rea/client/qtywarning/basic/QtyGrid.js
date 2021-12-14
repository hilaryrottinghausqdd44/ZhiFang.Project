/**
 * @description 库存预警
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.qtywarning.basic.QtyGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '库存预警',
	width: 800,
	height: 500,

	/**导出数据服务路径*/
	downLoadExcelUrl: '/ReaManageService.svc/RS_UDTO_DownLoadGetExportExcelByStockWarning',
	/**查询数据*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsQtyDtlListByStockWarning?isPlanish=true',
	/**查询预警数据*/
	selectAlertUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsByHQL?isPlanish=true',

	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**预警类型(1:低库存：2：高库存)*/
	warningType: 1,
	/**当前库存百分比值*/
	storePercent: 1,
	/**库存合并条件：默认不合并*/
	groupType: 0,
	/**库存量预警比较值类型选择的值*/
	comparisonType: 1,

	/**库存查询的合并选择项Key*/
	ReaBmsStatisticalTypeKey: "ReaBmsStatisticalType",
	/**库存量预警比较值类型Key*/
	QtyWarningComparisonValueTypeKey: "QtyWarningComparisonValueType",

	defaultStatisticalTypeValue: '1',
	/**库存预警类型:低库存预警*/
	AlertTypeId: '1',
	/**比较值为动态值时的最近几个月的默认值*/
	monthValue: 3,

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			renderer: function(value, meta, record) {
				var bgColor = record.get("ReaBmsQtyDtl_Color");
				if (bgColor) {
					meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: '条码类型',
			width: 100,
			hidden: true,
			renderer: function(value, meta, record) {
				var v = "";
				if (value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
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
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var GoodsID = rec.get('ReaBmsQtyDtl_GoodsID') + '';
					me.openShowOpForm(GoodsID);
				}
			}]
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '货品批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ProdDate',
			text: '生产日期',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_InvalidDate',
			text: '有效期至',
			isDate: true,
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '所属供应商',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_MonthlyUsage',
			text: '理论月用量',
			width: 80,
			//sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StoreLower',
			text: '库存下限',
			width: 80,
			//sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StoreUpper',
			text: '库存上限',
			width: 80,
			//sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存数',
			width: 80,
			renderer: function(value, meta, record) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_ComparisonValue',
			text: '预警值',
			hidden: false,
			sortable: false,
			width: 80,
			renderer: function(value, meta, record) {
				var bgColor = record.get("ReaBmsQtyDtl_Color");
				if (bgColor) {
					meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_CalcComparisonValue',
			text: '预警值',//预警计算值
			hidden: true,
			width: 80,
			sortable: false,
			renderer: function(value, meta, record) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				var bgColor = record.get("ReaBmsQtyDtl_Color");
				if (bgColor) {
					meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_SumTotal',
			text: '总计金额',
			width: 100,
			renderer: function(value, meta, record) {
				var v = value;
				if (v) v = parseFloat(Ext.util.Format.round(v, 2));
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_Memo',
			text: '备注',
			flex: 1,
			sortable: false,
			minWidth: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Color',
			text: '预警颜色',
			hidden: true,
			sortable: false,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		if (!me.storePercent) {
			var msg = me.msgFormat.replace(/{msg}/, "请选择有效的预警值后再查询");
			me.getView().update(msg);
			return false;
		}
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar1.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar1.getComponent('GoodsClassType').getValue();
		//一级分类	
		if (goodsClass) {
			arr.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if (goodsClassType) {
			arr.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		return where;
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		return "reabmsqtydtl.GoodsQty>0";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		//没有选择内容时
		if (!me.groupType) me.groupType = 0;
		url = url + "&warningType=" + me.warningType + "&groupType=" + me.groupType + "&storePercent=" + me.storePercent;
		if (me.comparisonType) url = url + "&comparisonType=" + me.comparisonType;
		if (me.monthValue) url = url + "&monthValue=" + me.monthValue;
		var reaGoodsHql = me.getReaGoodsHql();
		if (reaGoodsHql) url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		return url;
	},
	/**显示操作记录信息*/
	openShowOpForm: function(id) {
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		JShell.Win.open('Shell.class.rea.client.qtyoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			GoodsID: id
		}).show();
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		var me = this;
		me.enableControl(false);
		me.callParent(arguments);
		//var buttonsToolbar = me.getComponent('buttonsToolbar');
		//buttonsToolbar.getComponent('btnInfo').setDisabled(false);
	},
	getWhereHql:function(){
		var me = this,
			arr = [];
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			where= JShell.String.encode(where);
		}
		return where;
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;

		var params = [];
		params.push("warningType=" + me.warningType);
		params.push("groupType=" + me.groupType);
		params.push("operateType=" + operateType);
		params.push("storePercent=" + me.storePercent);
		params.push("comparisonType=" + me.comparisonType);
		params.push("monthValue=" + me.monthValue);
		params.push("where=" + me.getWhereHql());
		var reaGoodsHql = me.getReaGoodsHql();
		if (reaGoodsHql) params.push("reaGoodsHql=" + JShell.String.encode(reaGoodsHql));
		params.push("sort=" + "");
		url += "?" + params.join("&");
		window.open(url);
	},
	/**获取预警颜色信息*/
	getAlertByAlertType: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectAlertUrl;
		url +=
			"&fields=ReaAlertInfoSettings_AlertColor,ReaAlertInfoSettings_StoreLower,ReaAlertInfoSettings_StoreUpper&where=reaalertinfosettings.AlertTypeId=" +
			me.AlertTypeId;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}

});
