/**
 * 机构货品选择列表
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '货品选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 860,
	height: 540,
	/**是否单选*/
	checkOne: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'goodsorglink.CheckGrid',
	/**用户UI配置Name*/
	userUIName: "供应商货品选择列表",
	
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'Search',
			emptyText: '所属供应商/适用机型/拼音字头/货品名称/货品编码',
			fields: ['reagoodsorglink.ReaGoods.ReaCompanyName','reagoodsorglink.ReaGoods.SuitableType','reagoodsorglink.ReaGoods.PinYinZiTou', 'reagoodsorglink.ReaGoods.CName', 'reagoodsorglink.ReaGoods.ReaGoodsNo']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_BarCodeMgr',
			text: '条码类型',
			hidden: true,
			width: 50,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},  {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '货品名称',
			//flex: 1,
			minWidth: 200,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsOrgLink_BarCodeType");
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
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ProdOrgName',
			text: '厂家名称',
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitMemo',
			text: '货品规格',
			hidden: true,
			width: 40,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_Price',
			text: '价格',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_DispOrder',
			text: '优先级',
			width: 55,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '货品平台编号',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ApproveDocNo',
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_BiddingNo',
			text: '招标号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_StorageType',
			text: '储藏条件',
			hidden: true,
			defaultRenderer: true
		}, {
			text: '有效开始',
			dataIndex: 'ReaGoodsOrgLink_BeginTime',
			isDate: true,
			width: 80,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效截止',
			dataIndex: 'ReaGoodsOrgLink_EndTime',
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
					if(record.get("ReaGoodsOrgLink_BeginTime")) {
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
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ReaCompanyName',
			text: '所属供应商',
			//hidden: true,
			width: 120,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_SuitableType',
			text: '适用机型',
			//hidden: true,
			width: 120,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoodsOrgLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_Id',
			text: '货品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_Id',
			text: '机构主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_CName',
			text: '机构',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_OrgNo',
			text: '机构编码',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'ReaGoodsOrgLink_CenOrg_PlatformOrgNo',
			text: '平台机构编码',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_TempRange',
			text: '温度范围',
			hidden: true,
			hideable: false
		},  {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistNo',
			text: '注册证编号',
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '注册证有效期',
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate',
			isDate: true,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_IsPrintBarCode',
			text: '是否打印条码',
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if(value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_BarCodeType',
			text: '条码类型',
			hidden: true,
			hideable: false,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		} ,{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsSort',
			text: '货品序号',
			width: 90,
			hidden:true,
			defaultRenderer: true
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	}
});