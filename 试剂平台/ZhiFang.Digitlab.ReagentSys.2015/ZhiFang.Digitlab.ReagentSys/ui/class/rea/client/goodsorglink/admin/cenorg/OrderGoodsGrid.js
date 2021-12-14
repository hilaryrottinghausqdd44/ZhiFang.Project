/**
 * 产品采购供应维护
 * @author longfc
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goodsorglink.admin.cenorg.OrderGoodsGrid', {
	extend: 'Shell.class.rea.client.goodsorglink.basic.OrderGoodsGrid',

	/**当前选择的机构Id*/
	ReaCenOrgId: null,
	/**应用类型:产品:goods;订货/供货:cenorg*/
	appType: "cenorg",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '产品名称/产品编码',
			fields: ['reagoodsorglink.ReaGoods.CName','reagoodsorglink.ReaGoods.GoodsNo']
		};
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns = me.callParent(arguments);
		columns.splice(1, 0,  {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_CName',
			text: '机构名称',
			hidden:true,
			sortable: false,
			width: 260,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_OrgNo',
			text: '机构编码',
			hidden:true,
			sortable: false,
			width: 80,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '产品名称',
			//flex: 1,
			width: 230,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '产品编码',
			width: 90,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				meta = me.showQtipValue(meta, record);
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			hidden:true,
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
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_IsRegister',
			text: '有注册证',
			width: 60,
			align: 'center',
			type: 'bool',
			hidden:true,
			isBool: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			hidden:true,
			isBool: true
		});
		return columns;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			list[i].ReaGoodsOrgLink_ReaGoods_IsRegister = list[i].ReaGoodsOrgLink_ReaGoods_IsRegister == '1' ? true : false;
			list[i].ReaGoodsOrgLink_ReaGoods_IsPrintBarCode = list[i].ReaGoodsOrgLink_ReaGoods_IsPrintBarCode == '1' ? true : false;
		}
		data.list = list;
		return data;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			xtype: 'button',
			iconCls: 'button-add',
			itemId: 'btnCheck',
			text: '产品选择新增',
			tooltip: '产品选择',
			handler: function() {
				me.onCheckGoodsClick();
			}
		});
		items.push('add','-', 'edit', 'save', 'del');

		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	onCheckGoodsClick: function() {
		var me = this;
		var defaultWhere = "";
		var config = {
			width: 620,
			height: 500,
			/**是否单选*/
			checkOne: false,
			resizable: true,
			listeners: {
				accept: function(p, records) {
					me.onAccept(p, records);
				}
			}
		};
		//if(defaultWhere) config.defaultWhere = defaultWhere;
		JShell.Win.open('Shell.class.rea.client.goodsorglink.basic.GoodsCheck', config).show();
	},
	getGoodsIdStr: function() {
		var me = this;
		var idStr = "";
		me.store.each(function(record) {
			idStr += (record.get(me.PKField) + ",");
		});
		if(idStr) idStr = idStr.substring(0, idStr.length - 1);
		return idStr;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
		return me.callParent(arguments);
	},
	onAccept: function(p, records) {
		var me = this;
		var len = records.length;
		if(len == 0) {
			p.close();
			return;
		}
		if(!me.ReaCenOrgId) {
			JShell.Msg.error("机构信息为空!");
			p.close();
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.addSaveOne(i, records[i], p);
		}
	},
	addSaveOne: function(index, record, p) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUrl);
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			'Id': -1,
			'Visible': 1,
			'ReaGoods': {
				"Id": record.get("ReaGoods_Id"),
				"DataTimeStamp": strDataTimeStamp.split(',')
			},
			'CenOrg': {
				"Id": me.ReaCenOrgId,
				"DataTimeStamp": strDataTimeStamp.split(',')
			}
		};
		var params = JShell.JSON.encode({
			"entity": entity
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						p.close();
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	}
});