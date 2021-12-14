/**
 * 货品扫码调用服务后,获取到条码货品信息为多个时处理
 * @author longfc
 * @version 2018-01-19
 */
Ext.define('Shell.class.rea.client.confirm.choose.reabarcode.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '货品选择列表',
	width: 860,
	height: 540,
	/**是否单选*/
	checkOne: true,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			flex: 1,
			minWidth: 210,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_EName',
			text: '英文名',
			hidden: true,
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_SName',
			text: '简称',
			hidden: true,
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '产品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '产品规格',
			hidden: true,
			width: 40,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'Price',
			text: '价格',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ProdGoodsNo',
			text: '货品编号',
			width: 80,
			defaultRenderer: true
		}, {
			text: '有效期',
			dataIndex: 'InvalidDate',
			hidden: true,
			width: 80,
			sortable: false,
			hideable: true,
			renderer: function(curValue, meta, record, rowIndex, colIndex, s, view) {
				var bgColor = "";
				var value = curValue;
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);

					if(days < 0) {
						bgColor = "red";
						value = "已失效";
					} else if(days >= 0 && days <= 30) {
						bgColor = "#e97f36";
						value = "30天内到期";
					} else if(days > 30) {
						bgColor = "#568f36";
					}

				} else {
					if(record.get("BeginTime")) {
						bgColor = "#568f36";
						value = "长期有效";
					} else {
						bgColor = "#e97f36";
						value = "无有效期";
					}
				}
				if(curValue) curValue = Ext.util.Format.date(curValue, 'Y-m-d');
				meta.tdAttr = 'data-qtip="' + curValue + '"';
				if(bgColor) meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				return value;
			}
		}, {
			dataIndex: 'BiddingNo',
			text: '招标号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '产品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLinkID',
			text: '货品机构关系ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'LotNo',
			text: '货品批号',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'UsePackSerial',
			text: '使用盒条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'OtherPackSerial',
			text: '使用盒条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'SysPackSerial',
			text: '系统内部盒条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBarCodeVO',
			text: '原货品条码实体信息',
			hidden: true,
			defaultRenderer: true
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			where = [];
		return me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.defaultLoad) return false;
	},
	/**加载数据*/
	loadData: function(data) {
		var me = this;
		me.getView().update();
		if(!data) return false;
		var dataList = [];
		Ext.Array.each(data, function(model) {
			model["ReaBarCodeVO"] = model.ReaGoods;
			model["ReaGoods_CName"] = model.ReaGoods.CName;
			model["ReaGoods_EName"] = model.ReaGoods.EName;
			model["ReaGoods_SName"] = model.ReaGoods.SName;
			model["ReaGoods_UnitName"] = model.ReaGoods.UnitName;
			model["ReaGoods_UnitMemo"] = model.ReaGoods.UnitMemo;
			model["ReaGoods_GoodsNo"] = model.ReaGoods.GoodsNo;
			dataList.push(model);
		});
		me.store.loadData(dataList);
	}
});