/**
 * 货品扫码调用服务后,获取到条码货品信息为多个时处理
 * @author longfc
 * @version 2018-01-19
 */
Ext.define('Shell.class.rea.client.confirm.choose.reabarcode.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
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
	defaultLoad: false,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.choose.reabarcode.CheckGrid',
	/**用户UI配置Name*/
	userUIName: "订单验收订单列表",
	
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			width: 180,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BarCodeType");
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
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsNo',
			text: '货品编码',
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsNo',
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '货品规格',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'Price',
			text: '价格',
			width: 55,
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
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ApproveDocNo',
			text: '批准文号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '货品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'LotNo',
			text: '货品批号',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BarCodeType',
			text: '条码类型',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'UsePackSerial',
			text: '使用盒条码',
			hidden: true,
			defaultRenderer: true
		},  {
			dataIndex: 'UsePackQRCode',
			text: '二维盒条码',
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
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		me.buttonToolbarItems.push('->', 'accept');
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
		me.store.removeAll();
		if(!me.defaultLoad) return false;
	},
	/**加载数据*/
	loadData: function(data) {
		var me = this;
		//me.getView().update();
		me.store.removeAll();
		if(!data) return false;
		var dataList = [];
		Ext.Array.each(data, function(model) {
			model["ReaBarCodeVO"] = JcallShell.JSON.encode(model);
			model["ReaGoods_Id"] = model.ReaGoodsID;
			model["ReaGoods_CompGoodsLinkID"] = model.CompGoodsLinkID;
			model["ReaGoods_CName"] = model.CName;
			model["ReaGoods_EName"] = model.EName;
			model["ReaGoods_SName"] = model.SName;
			model["ReaGoods_UnitName"] = model.UnitName;
			model["ReaGoods_UnitMemo"] = model.UnitMemo;
			
			model["ReaGoods_ReaGoodsNo"] = model.ReaGoodsNo;
			model["ReaGoods_ProdGoodsNo"] = model.ProdGoodsNo;
			model["ReaGoods_GoodsNo"] = model.GoodsNo;
			dataList.push(model);
		});
		me.store.loadData(dataList);
	}
});