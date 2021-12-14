/**
 * 货品选择列表:需要过滤当前供应商已维护并启用的货品信息
 * @author longfc
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.GoodsCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '货品选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	width: 860,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByCenOrgId?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**是否能编辑列*/
	canEdit: false,
	/**用户UI配置Key*/
	userUIKey: '.goodsorglink.basic.GoodsCheck',
	/**用户UI配置Name*/
	userUIName: "供应商货品选择列表",

	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "&cenOrgId=" + me.ReaCenOrgId;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'reagoods.Visible=1';
		if(me.canEdit) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
		//查询框信息
		me.searchInfo = {
			width: 320,
			isLike: true,
			itemId: 'Search',
			emptyText: '所属供应商/拼音字头/英文名/货品名称/货品编码/厂商货品编码',
			fields: ['reagoods.ReaCompanyName', 'reagoods.PinYinZiTou', 'reagoods.EName', 'reagoods.CName', 'reagoods.ShortCode', 'reagoods.ReaGoodsNo', 'reagoods.ProdGoodsNo']
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
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			//flex:1,
			width: 210,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoods_BarCodeMgr");
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
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '货品条码类型',
			sortable: false,
			menuDisabled: true,
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoods_IsPrintBarCode',
			text: '货品是否打印条码',
			hidden: true,
			sortable: false,
			menuDisabled: true,
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
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CenOrgGoodsNo',
			text: '<b style="color:blue;">供货商货品编码</b>',
			width: 110,
			editor: {
				allowBlank: true,
				emptyText: '必填项',
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Price',
			text: '<b style="color:blue;">单价</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				emptyText: '必填项',
				minValue: 0,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaGoods_Price', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品平台编号',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ReaCompanyName',
			text: '所属供应商',
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ShortCode',
			text: '同系列码',
			width: 70,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			//list[i].ReaGoods_IsRegister = list[i].ReaGoods_IsRegister == '1' ? true : false;
			list[i].ReaGoods_IsPrintBarCode = list[i].ReaGoods_IsPrintBarCode == '1' ? true : false;
			list[i].ReaGoods_CenOrgGoodsNo = list[i].ReaGoods_ReaGoodsNo;
		}
		data.list = list;
		return data;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.ReaCenOrgId) return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	}
});