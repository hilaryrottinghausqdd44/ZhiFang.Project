/**
 * 机构货品选择列表
 * @author longfc
 * @version 2017-12-07
 */
Ext.define('Shell.class.rea.client.confirm.apply.GoodsOrgLinkCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '货品选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 860,
	height: 540,
	/**是否单选*/
	checkOne: true,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_DispOrder',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'Search',
			emptyText: '产品名称/产品编码',
			fields: ['reagoodsorglink.ReaGoods.CName', 'reagoodsorglink.ReaGoods.GoodsNo']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '产品名称',
			flex: 1,
			minWidth: 230,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
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
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '产品编码',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			text: '对方产品编号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ProdGoodsNo',
			text: '货品编号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_BiddingNo',
			text: '招标号',
			width: 80,
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
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitMemo',
			text: '产品规格',
			hidden: true,
			width: 40,
			sortable: false,
			defaultRenderer: true
		},  {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistDate',
			text: '注册日期',
			hidden: true,
			width: 40,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_Id',
			text: '产品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_Id',
			text: '机构主键ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
	}
});